using System;
using System.Threading.Tasks;
using BlazorTourOfHeroes.Shared;
using Microsoft.AspNetCore.SignalR;

namespace BlazorTourOfHeroes.Server.Hubs
{
    public class HeroHub : Hub
    {
        public async Task SaveHero(Hero hero)
        {
            await Clients.All.SendAsync("ReceiveUpdatedHero", hero);
        }
    }
}