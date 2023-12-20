using SimpleFitness.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFitness.Backend.Workout.Models {
    public class Workout : BaseModel {
        public string DayOfWeek { get; set; }
        public ICollection<Exercise> Exercises { get; set; }

        public Workout() { 
            this.Exercises = new List<Exercise>();
        }
    }
}
