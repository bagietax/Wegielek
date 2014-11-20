using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Security;
using MvcMovie.Models;
using WebMatrix.WebData;

namespace MvcMovie.Dal
{
    public class ContextInitializer : CreateDatabaseIfNotExists<CoalCubeDbContex>
    {
        protected override void Seed(CoalCubeDbContex context)
        {
            ClientSeeder seeder = new ClientSeeder();
            seeder.Seed(context);
        }
    }
}