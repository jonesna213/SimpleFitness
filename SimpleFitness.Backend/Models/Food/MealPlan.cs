using SimpleFitness.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFitness.Backend.Food.Models {
    public class MealPlan : BaseModel {
        public int CalorieLimit { get; set; }
        public ICollection<Meal> Meals { get; set; }

        public MealPlan() {
            this.Meals = new List<Meal>();
        }
    }
}
