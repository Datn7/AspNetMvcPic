using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetMvcPic.Models
{
    public enum SexType
    {
        Male,
        Female
    }

    public class Human
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SkinColor { get; set; }
        public SexType Sex { get; set; }

    }
}
