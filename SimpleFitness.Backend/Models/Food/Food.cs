using SimpleFitness.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFitness.Backend.Food.Models {
    public class Food : BaseModel {
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int Carbs { get; set; }
        public int Calories { get; set; }
        public string FoodDescription { get; set; }

        public void UpdateCalories() {
            this.Calories = (Carbs * 4) + (Protein * 4) + (Fat * 9);
        }
    }
}
