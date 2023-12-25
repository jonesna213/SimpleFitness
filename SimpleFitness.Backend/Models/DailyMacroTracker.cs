using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFitness.Backend.Models {
    public class DailyMacroTracker : BaseModel {
        public DateTime Day { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int Carbs { get; set; }
        public int TotalCalories { get; set; }

        //public string User_Id { get; set; }

        public DailyMacroTracker() { 
            this.Day = DateTime.Now.Date;
            this.Protein = 0;
            this.Fat = 0;
            this.Carbs = 0;
            this.TotalCalories = 0;
        }
        
        //Updates the TotalCalories to an updated value from the macros
        public void UpdateCalories() {
            this.TotalCalories = (Carbs * 4) + (Protein * 4) + (Fat * 9);
        }
    }
}
