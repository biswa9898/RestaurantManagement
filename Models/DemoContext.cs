using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RestaurantManagement.Models
{
    public class DemoContext:DbContext
    {
        public DemoContext(DbContextOptions options):base(options)
        { }

        public DemoContext()
        { }

        public virtual DbSet<Users> Users { get; set; } 
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
    }
}
