using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorTourOfHeroes.Shared
{
    public interface IHeroList
    {
        List<Hero> _HeroList { get; set; }
    }

    public class HeroList : IHeroList
    {
        public List<Hero> _HeroList { get; set; } = new List<Hero>()
        {
            new Hero()
            {
                Id = 1,
                Name = "Windstorm"
            }
        };
    }
}
