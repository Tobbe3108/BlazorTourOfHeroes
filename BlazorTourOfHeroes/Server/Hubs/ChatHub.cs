using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorTourOfHeroes.Shared;
using Microsoft.AspNetCore.SignalR;

namespace BlazorTourOfHeroes.Server.Hubs
{
    public class ChatHub : Hub
    {
        List<Hero> HeroList = new List<Hero>()
        {
            new Hero()
            {
                Id = 1,
                Name = "Windstorm"
            }
        };

        public async Task GetHero(string userID, int Id)
        {
            Hero hero = HeroList.Find(h => h.Id == Id);
            await Clients.Client(userID).SendAsync("ReceiveHero", hero);
        }

        public async Task GetHeroList(string userID)
        {
            await Clients.Client(userID).SendAsync("ReceiveHeroList", HeroList);
        }

        public async Task SaveHero(Hero hero)
        {
            HeroList[HeroList.FindIndex(h => h.Id == hero.Id)] = hero;
            await Clients.All.SendAsync("ReceiveHeroList", HeroList);
        }
    }
}

