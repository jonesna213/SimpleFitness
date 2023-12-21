using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SimpleFitness.Backend.Models;
using SimpleFitness.UI.Models;
using System;
using System.Collections.Generic;
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

        public ProfileController(ApplicationUserManager userManager, ApplicationSignInManager signInManager) {
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
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());

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
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());

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
            User user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            List<DailyMacroTracker> trackers = user.MacroTrackers.ToList();
            if (trackers.Count > 0) {
                DateTime currentDate = DateTime.Now.Date;
                trackers.FirstOrDefault(t => {
                    if (currentDate == t.Day) {
                        return true;
                    }

                    return false;
                });
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MacroTracker(DailyMacroTracker tracker) {
            if (ModelState.IsValid) {
                
            }

            return View(tracker);
        }




        //Helper function
        private void AddErrors(IdentityResult result) {
            foreach (var error in result.Errors) {
                ModelState.AddModelError("", error);
            }
        }
    }
}