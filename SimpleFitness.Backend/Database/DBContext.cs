using Microsoft.AspNet.Identity.EntityFramework;
using SimpleFitness.Backend.Food.Models;
using SimpleFitness.Backend.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFitness.Backend.Database {
    public class DBContext : IdentityDbContext<User> {
        //Add which models that you want in tables like below
        //public DbSet<Product> Products { get; set; }


        public DBContext() : base("DefaultConnection", throwIfV1Schema: false) {

        }

        public static DBContext Create() {
            return new DBContext();
        }
    }
}
