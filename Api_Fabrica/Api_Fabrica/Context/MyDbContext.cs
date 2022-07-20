using Api_Fabrica.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Fabrica.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        // relacion de entidades
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }

        public static readonly ILoggerFactory ConsoleLoggerFactory
           = LoggerFactory.Create(builder =>
           {
               builder
               .AddFilter((category, level) =>
                   category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Debug)
               .AddConsole();
           });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder
               .UseLoggerFactory(ConsoleLoggerFactory);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

           



            base.OnModelCreating(modelBuilder);
        }
    }
}
