using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFitness.Backend.Models {
    public class Macros : BaseModel {
        public DateTime Day { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int Carbs { get; set; }
        public int TotalCalories { get; set; }

        public Macros() { 
            Day = DateTime.Now;
        }
        
        //Updates the TotalCalories to an updated value from the macros
        public void UpdateCalories() {
            this.TotalCalories = (Carbs * 4) + (Protein * 4) + (Fat * 9);
        }
    }
}
