using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Models.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class NorthwindContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=OZGUN;Database=Northwind;Trusted_Connection=true");
        }

        public DbSet<User> Users { get; set; }


    }
}
