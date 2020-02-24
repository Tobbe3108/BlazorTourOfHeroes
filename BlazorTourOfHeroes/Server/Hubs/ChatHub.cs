﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorTourOfHeroes.Shared;
using Microsoft.AspNetCore.SignalR;

namespace BlazorTourOfHeroes.Server.Hubs
{
    public class ChatHub : Hub
    {
        public List<Hero> _HeroList { get; set; }
        public ChatHub(IHeroList heroList)
        {
            _HeroList = heroList._HeroList;
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

        public async Task SaveHero(Hero hero)
        {
            _HeroList[_HeroList.FindIndex(h => h.Id == hero.Id)] = hero;
            await Clients.All.SendAsync("ReceiveHeroList", _HeroList);
            await Clients.All.SendAsync("UpdateHero", true);
        }
    }
}
