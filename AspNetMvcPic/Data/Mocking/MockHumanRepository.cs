using AspNetMvcPic.Data.Repositories;
using AspNetMvcPic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetMvcPic.Data.Mocking
{
    public class MockHumanRepository : IHumanRepository
    {
        public IEnumerable<Human> AllHumans => new List<Human>
        {
            new Human{Id=1,Name="Giorgi",Sex=SexType.Male, SkinColor="White"},
            new Human{Id=2,Name="Jessica",Sex=SexType.Female, SkinColor="Black"},
            new Human{Id=3,Name="Anlella",Sex=SexType.Female, SkinColor="White"},
        };

        public Human GetHumanById(int humanId)
        {
            return AllHumans.FirstOrDefault(h => h.Id == humanId);
        }
    }
}
