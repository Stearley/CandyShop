using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandyShop.Domain.Entities;
using System.Data.Entity;

namespace CandyShop.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Sweet> Sweets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SwCat> SwCat { get; set; }
        public DbSet<Byer> Byers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
