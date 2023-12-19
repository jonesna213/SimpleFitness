using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFitness.Backend.Database {
    public class DataContext : DbContext {

        DataContext() : base("DefaultConnection") {

        }

        //Add which models that you want in tables like below
        //public DbSet<Product> Products { get; set; }

    }
}
