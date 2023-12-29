using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleFitness.Backend.Food.Models;
using SimpleFitness.Backend.Workout.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFitness.Backend.Models {
    public class User : IdentityUser {
        //Custom properties
        public virtual WorkoutPlan WorkoutPlan { get; set; }
        public virtual MealPlan MealPlan { get; set; }
        public virtual ICollection<DailyMacroTracker> MacroTrackers { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Height { get; set; } //In feet/in
        public int Weight { get; set; }    //In pounds
        [DisplayName("Daily Activity")]
        public string DailyActivity { get; set; }
        public string Goal { get; set; }

        public User() { 
            this.MacroTrackers = new List<DailyMacroTracker>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager) {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
