using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SimpleFitness.Backend.Database;
using SimpleFitness.Backend.Food.Models;
using SimpleFitness.Backend.Models;
using SimpleFitness.Backend.Workout.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SimpleFitness.UI.Controllers {
    [Authorize]
    public class ProfileController : Controller {

        private ApplicationUserManager _userManager;

        public ProfileController() {
        }

        public ProfileController(ApplicationUserManager userManager) {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set {
                _userManager = value;
            }
        }


        // Get: /Profile/CreateProfile
        public ActionResult CreateProfile() {
            User viewModel = new User();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateProfile(User model) {
            if (ModelState.IsValid) {
                User user = await _userManager.FindByIdAsync(User.Identity.GetUserId());

                user.Age = model.Age;
                user.Gender = model.Gender;
                user.Height = model.Height;
                user.Weight = model.Weight;
                user.DailyActivity = model.DailyActivity;
                user.Goal = model.Goal;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded) {
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            return View(model);
        }

        //GET : /Profile/
        public async Task<ActionResult> Index() {
            User user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(User model) {
            if (ModelState.IsValid) {
                User user = await _userManager.FindByIdAsync(User.Identity.GetUserId());

                user.Age = model.Age;
                user.Gender = model.Gender;
                user.Height = model.Height;
                user.Weight = model.Weight;
                user.DailyActivity = model.DailyActivity;
                user.Goal = model.Goal;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded) {
                    return View(user);
                }

                AddErrors(result);
            }

            return View(model);
        }

        //Test meal plan
        private MealPlan GetMealPlan() {
            MealPlan mealPlan = new MealPlan();
            mealPlan.CalorieLimit = 2000;


            Food f1 = new Food();
            f1.FoodDescription = "Chicken";
            f1.Protein = 10;
            f1.Fat = 5;
            f1.Carbs = 0;
            f1.UpdateCalories();
            Food f2 = new Food();
            f2.FoodDescription = "Broccoli";
            f2.Protein = 1;
            f2.Fat = 0;
            f2.Carbs = 12;
            f2.UpdateCalories();
            Food f3 = new Food();
            f3.FoodDescription = "Rice";
            f3.Protein = 0;
            f3.Fat = 1;
            f3.Carbs = 23;
            f3.UpdateCalories();

            Meal m1 = new Meal();
            m1.DayOfWeek = "Monday";
            m1.MealType = "Dinner";
            m1.Foods.Add(f1);
            m1.Foods.Add(f2);
            m1.Foods.Add(f3);
            m1.UpdateNutritionTotals();

            mealPlan.Meals.Add(m1);

            return mealPlan;
        }

        //Test Workout Plan
        private WorkoutPlan GetWorkoutPlan() {
            WorkoutPlan workoutPlan = new WorkoutPlan();

            Exercise e1 = new Exercise();
            e1.Name = "Bench Press";
            e1.Sets = 3;
            e1.Reps = "10-12";
            Exercise e2 = new Exercise();
            e2.Name = "Incline Dumbbell Press";
            e2.Sets = 4;
            e2.Reps = "10-12";
            Exercise e3 = new Exercise();
            e3.Name = "Cable Fly";
            e3.Sets = 4;
            e3.Reps = "15";

            Workout w1 = new Workout();
            w1.DayOfWeek = "Tuesday";
            w1.Exercises.Add(e1);
            w1.Exercises.Add(e2);
            w1.Exercises.Add(e3);

            workoutPlan.Workouts.Add(w1);


            return workoutPlan;
        }


        public async Task<ActionResult> MacroTracker() {
            string id = User.Identity.GetUserId();

            User user = await _userManager.Users
                .Include(u => u.MacroTrackers)
                .SingleOrDefaultAsync(u => u.Id == id);

            DateTime currentDate = DateTime.Now.Date;
            DailyMacroTracker tracker = user.MacroTrackers.FirstOrDefault(t => currentDate == t.Day);

            if (tracker == null) {
                DailyMacroTracker newTracker = new DailyMacroTracker();

                user.MacroTrackers.Add(newTracker);

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded) {
                    return View(newTracker);
                }

                AddErrors(result);
            }

            return View(tracker);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MacroTracker(DailyMacroTracker tracker) {
            if (ModelState.IsValid) {
                string id = User.Identity.GetUserId();

                User user = await _userManager.Users
                    .Include(u => u.MacroTrackers)
                    .SingleOrDefaultAsync(u => u.Id == id);

                DateTime currentDate = DateTime.Now.Date;
                DailyMacroTracker currentTracker = user.MacroTrackers.FirstOrDefault(t => currentDate == t.Day);

                currentTracker.Carbs += tracker.Carbs;
                currentTracker.Protein += tracker.Protein;
                currentTracker.Fat += tracker.Fat;
                currentTracker.UpdateCalories();

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded) {
                    return RedirectToAction("MacroTracker", "Profile");
                }

                AddErrors(result);
            }

            return View(tracker);
        }


        public async Task<ActionResult> MealPlan() {
            string id = User.Identity.GetUserId().ToString();

            User user = await _userManager.Users
                .Include(u => u.MealPlan.Meals)
                .Include(u => u.MealPlan.Meals.Select(m => m.Foods))
                .SingleOrDefaultAsync(u => u.Id.ToString() == id);

            return View(user.MealPlan);
        }


        public async Task<ActionResult> WorkoutPlan() {
            string id = User.Identity.GetUserId().ToString();

            User user = await _userManager.Users
                .Include(u => u.WorkoutPlan.Workouts)
                .Include(u => u.WorkoutPlan.Workouts.Select(m => m.Exercises))
                .SingleOrDefaultAsync(u => u.Id.ToString() == id);

            return View(user.WorkoutPlan);
        }


        //Helper functions
        private void AddErrors(IdentityResult result) {
            foreach (var error in result.Errors) {
                ModelState.AddModelError("", error);
            }
        }
    }
}