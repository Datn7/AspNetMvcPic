using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetMvcPic.ViewModel
{
    public class ListItemViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public IFormFile ProfileImage { get; set; }
    }
}
