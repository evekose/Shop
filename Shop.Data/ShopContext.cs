using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;
using Shop.Core.Domain.Car;
using Shop.Core.Domain.Spaceship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options) { }

        public DbSet<Spaceship> Spaceships { get; set; }
        public DbSet<Car> Cars { get; set; }
		public DbSet<FileToDatabase> FileToDatabase { get; set; }
	}
}
