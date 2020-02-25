using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorTourOfHeroes.Shared;
using Microsoft.AspNetCore.SignalR;

namespace BlazorTourOfHeroes.Server.Hubs
{
    public class ChatHub : Hub
    {
        public List<Hero> _HeroList { get; set; }
        public List<Hero> _TopHeroList { get; set; }
        public ChatHub(IHeroList heroList)
        {
            _HeroList = heroList._HeroList;
            _TopHeroList = heroList._TopHeroes;
        }

        public async Task GetHero(string userID, int Id)
        {
            Hero hero = _HeroList.Find(h => h.Id == Id);
            await Clients.Client(userID).SendAsync("ReceiveHero", hero);
        }

        public async Task GetHeroList(string userID)
        {
            await Clients.Client(userID).SendAsync("ReceiveHeroList", _HeroList);
        }

        public async Task GetTopHeroList(string userID)
        {
            await Clients.Client(userID).SendAsync("ReceiveTopHeroList", _TopHeroList);
        }

        public async Task SaveHero(Hero hero)
        {
            _HeroList[_HeroList.FindIndex(h => h.Id == hero.Id)] = hero;
            _TopHeroList.RemoveAt(0);
            _TopHeroList.Add(hero);
            await Clients.All.SendAsync("ReceiveHeroList", _HeroList);
            await Clients.All.SendAsync("ReceiveTopHeroList", _TopHeroList);
            await Clients.All.SendAsync("UpdateHero", true);
        }
    }
}

