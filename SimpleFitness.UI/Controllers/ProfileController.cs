using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SimpleFitness.Backend.Database;
using SimpleFitness.Backend.Models;
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


        // Get: /Account/CreateProfile
        public ActionResult CreateProfile() {
            User viewModel = new User();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateProfile(User model) {
            if (ModelState.IsValid) {
                var user = await GetUser();

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
            User user = await GetUser();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(User model) {
            if (ModelState.IsValid) {
                User user = await GetUser();

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


        public async Task<ActionResult> MacroTracker() {
            User user = await GetUser();
            DBContext dBContext = new DBContext();
            DBAccess<DailyMacroTracker> access = new DBAccess<DailyMacroTracker>(dBContext);  

            List<DailyMacroTracker> trackers = access.Collection().ToList();

            DateTime currentDate = DateTime.Now.Date;
            DailyMacroTracker tracker = trackers.FirstOrDefault(t => currentDate == t.Day);

            if (tracker == null) {

                //For some reason i needed to make an empty list to where i then insert it into the database.
                //Not sure why its that way, but it works.
                DailyMacroTracker newTracker = new DailyMacroTracker();

                List<DailyMacroTracker> newTrackers = new List<DailyMacroTracker> { newTracker };

                user.MacroTrackers = newTrackers;

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
                User user = await GetUser();
                DBContext dBContext = new DBContext();
                DBAccess<DailyMacroTracker> access = new DBAccess<DailyMacroTracker>(dBContext);

                List<DailyMacroTracker> trackers = access.Collection().ToList();

                DateTime currentDate = DateTime.Now.Date;
                DailyMacroTracker currentTracker = trackers.FirstOrDefault(t => currentDate == t.Day);

                currentTracker.Carbs += tracker.Carbs;
                currentTracker.Protein += tracker.Protein;
                currentTracker.Fat += tracker.Fat;
                currentTracker.UpdateCalories();

                access.Update(currentTracker);
                access.Commit();

                return RedirectToAction("MacroTracker", "Profile");
            }

            return View(tracker);
        }




        //Helper functions
        private void AddErrors(IdentityResult result) {
            foreach (var error in result.Errors) {
                ModelState.AddModelError("", error);
            }
        }

        private async Task<User> GetUser() {
            User user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            return user;
        }
    }
}