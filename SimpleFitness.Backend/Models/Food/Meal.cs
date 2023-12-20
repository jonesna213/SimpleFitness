using SimpleFitness.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFitness.Backend.Food.Models {
    public class Meal : BaseModel {
        public string DayOfWeek { get; set; }
        public ICollection<Food> Foods { get; set; }
        public string MealType { get; set; }
        public string Description { get; set; }
        public int TotalProtein { get; set; }
        public int TotalFat { get; set; }
        public int TotalCarbs { get; set; }
        public int TotalCalories { get; set; }
        
        public Meal() {
            this.Foods = new List<Food>();
        }

        //Goes through the list of foods and sets the total macros/calories
        public void UpdateNutritionTotals() {
            foreach (var food in Foods) {
                TotalProtein += food.Protein;
                TotalFat += food.Fat;
                TotalCarbs += food.Carbs;
                TotalCalories += food.Calories;
            }
        }

        //Maybe future to edit/remove/replace foods with custom
    }
}
