using SimpleFitness.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFitness.Backend.Workout.Models {
    public class WorkoutPlan : BaseModel {
        public ICollection<Workout> Workouts { get; set; }

        public WorkoutPlan() { 
            this.Workouts = new List<Workout>();
        }
    }
}
