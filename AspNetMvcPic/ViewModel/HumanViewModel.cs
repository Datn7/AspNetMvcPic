using AspNetMvcPic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetMvcPic.ViewModel
{
    public class HumanViewModel
    {
        public IEnumerable<Human> Humans { get; set; }
    }
}
