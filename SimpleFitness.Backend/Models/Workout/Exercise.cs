using SimpleFitness.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFitness.Backend.Workout.Models {
    public class Exercise : BaseModel {
        public string Name { get; set; }
        public string Description { get; set; } //Optional
        public int Sets { get; set; }
        public string Reps { get; set; }
        public string AmountOfTime { get; set; } //Optional - cardio


    }
}
