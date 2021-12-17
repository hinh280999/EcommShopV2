using EcommShop.DataAccessor.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommShop.DataAccessor.DBContext
{
    public class EcommShopDBContext : DbContext
    {
        public EcommShopDBContext(DbContextOptions<EcommShopDBContext> options) : base(options){}

        public DbSet<User> Users { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductRating> ProductRatings { get; set; }
    }
}
