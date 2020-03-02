using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorTourOfHeroes.Shared
{
    public class HeroState
    {
        public List<Hero> HeroList { get; set; } = new List<Hero>()
            {
                new Hero()
                {
                    Id = 11,
                    Name =  "Dr Nice"
                },
                new Hero()
                {
                    Id = 12,
                    Name = "Narco"
                },
                new Hero()
                {
                    Id = 13,
                    Name = "Bombasto"
                },
                new Hero()
                {
                    Id = 14,
                    Name = "Celeritas"
                },
                new Hero()
                {
                    Id = 15,
                    Name = "Magneta"
                },
                new Hero()
                {
                    Id = 16,
                    Name = "RubberMan"
                },
                new Hero()
                {
                    Id = 17,
                    Name = "Dynama"
                },
                new Hero()
                {
                    Id = 18,
                    Name = "Dr IQ"
                },
                new Hero()
                {
                    Id = 19,
                    Name = "Magma"
                },
                new Hero()
                {
                    Id = 20,
                    Name = "Tornado"
                }
            };

        public List<Hero> TopHeroes { get; set; } = new List<Hero>()
            {
                new Hero()
                {
                    Id = 12,
                    Name = "Narco"
                },
                new Hero()
                {
                    Id = 13,
                    Name = "Bombasto"
                },
                new Hero()
                {
                    Id = 14,
                    Name = "Celeritas"
                },
                new Hero()
                {
                    Id = 15,
                    Name = "Magneta"
                },
            };

        private HubConnection connection;

        public HeroState()
        {
            connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:44314/HeroHub")
            .WithAutomaticReconnect()
            .Build();

            connection.On<Hero>("ReceiveUpdatedHero", (updatedHero) =>
            {
                HeroList[HeroList.FindIndex(hero => hero.Id == updatedHero.Id)] = updatedHero;

                Hero oldHero = TopHeroes.Find(hero => hero.Id == updatedHero.Id);
                if (oldHero != null)
                {
                    TopHeroes.Remove(oldHero);
                    TopHeroes.Add(updatedHero);
                }
                else
                {
                    TopHeroes.RemoveAt(0);
                    TopHeroes.Add(updatedHero);
                }
                OnStateChanged?.Invoke();
            });

            Task.Run(() => connection.StartAsync());
        }

        public event Action OnStateChanged;
    }
}