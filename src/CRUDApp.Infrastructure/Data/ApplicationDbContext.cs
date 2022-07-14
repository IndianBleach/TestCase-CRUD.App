using CRUDApp.Core.Entities.OrderEntity;
using CRUDApp.Core.Entities.OrderItemEntity;
using CRUDApp.Core.Entities.ProviderEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CRUDApp.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Provider> Providers { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            base.OnModelCreating(builder);            
        }
    }
}