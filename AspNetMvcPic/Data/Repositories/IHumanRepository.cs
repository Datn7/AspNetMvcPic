using AspNetMvcPic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetMvcPic.Data.Repositories
{
    public interface IHumanRepository
    {
        IEnumerable<Human> AllHumans { get; }
        Human GetHumanById(int humanId);
    }
}
