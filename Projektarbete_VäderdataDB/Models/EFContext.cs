using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Projektarbete_VäderdataDB.Models
{
    class EFContext : DbContext
    {
        public DbSet<Temperature> Temperatures { get; set; }

        const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NimbusWeatherDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }


        
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Temperature>.HasData(
        //        new Temperature
        //        {

        //        }
        //    );
        //}
    }
}
