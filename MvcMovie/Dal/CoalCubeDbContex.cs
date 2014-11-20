using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using MvcMovie.Models;

namespace MvcMovie.Dal
{
    public class CoalCubeDbContex:DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<SmsSenderSettings> SmsSender { get; set; }
        public DbSet<Sms> Sms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
            Database.SetInitializer(new ContextInitializer());
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}