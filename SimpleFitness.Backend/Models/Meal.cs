using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFitness.Backend.Models {
    public class Meal : BaseModel {
        public string DayOfWeek { get; set; }
        public int Calories { get; set; }

    }
}
