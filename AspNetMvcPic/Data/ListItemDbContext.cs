using AspNetMvcPic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetMvcPic.Data
{
    public class ListItemDbContext : IdentityDbContext<IdentityUser> //changed DbContext
    {
        public ListItemDbContext( DbContextOptions<ListItemDbContext> options):base(options)
        {

        }

        public DbSet<ListItem> Items { get; set; }

    }
}
