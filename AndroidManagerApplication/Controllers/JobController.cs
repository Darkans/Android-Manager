using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AndroidManagerApplication.Models.Entities;
using AndroidManagerApplication.Models.ViewModels;
using AndroidManagerApplication.Models.Managers;

namespace AndroidManagerApplication.Controllers
{
    public class JobController : Controller
    {
        private AndroidManager _androidManager;
        private JobManager _jobManager;
        private SkillManager _skillManager;
        private ImageManager _imageManager;
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public JobController()
        {
            _androidManager = new AndroidManager();
            _jobManager = new JobManager();
            _skillManager = new SkillManager();
            _imageManager = new ImageManager();
        }

        [Authorize]
        public ActionResult JobList()
        {
            _imageManager.ReloadList();
            _androidManager.ReloadList();
            var jobList = _jobManager.GetList();
            ViewBag.JobList = jobList;
            return View();
        }

        [Authorize]
        public ActionResult NewJob()
        {
            return View();
        }

        [Authorize]
        public ActionResult CreateJob(JobViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("NewJob", model);
            }

            var newJob = new Job()
            {
                Name = model.Name,
                Describtion = model.Describtion,
                Complexity = model.Complexity
            };

            _jobManager.Add(newJob);

            return RedirectToAction("JobList", "Job");
        }

        [Authorize]
        public ActionResult DeleteJob(int jobId)
        {
            try
            {
                _jobManager.DeleteById(jobId);
            } catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return RedirectToAction("JobList", "Job");
        }

        [Authorize]
        public ActionResult AssignedAndroids(int jobId)
        {
            try
            {
                _jobManager.ReloadList();
                var job = _jobManager.GetById(jobId);
                var model = 
                    new AndroidsAssignedToJobViewModel()
                    {
                        CurrentJob = job,
                        AndroidList = _androidManager.GetList()
                    };

                return PartialView("AndroidListAssignedToJobPartial", model);
            } catch (Exception e)
            {
                var errors = new List<string>();
                do
                {
                    errors.Add(e.Message);
                    e = e.InnerException;
                } while (e.InnerException != null);

                ViewBag.Errors = errors;
            }
            return RedirectToAction("JobList", "Job");
        }

        [Authorize]
        public ActionResult AssignAndroidToJob(int androidId, int jobId)
        {
            try
            {
                var android = _androidManager.GetById(androidId);
                var job = _jobManager.GetById(jobId);
                _androidManager.ChangeJob(android, job);
                var model =
                    new AndroidsAssignedToJobViewModel()
                    {
                        CurrentJob = job,
                        AndroidList = _androidManager.GetList()
                    };

                return PartialView("AndroidListAssignedToJobPartial", model);
            }
            catch (Exception e)
            {
                var errors = new List<string>();
                do
                {
                    errors.Add(e.Message);
                    e = e.InnerException;
                } while (e.InnerException != null);

                ViewBag.Errors = errors;
            }
            return PartialView("AndroidListAssignedToJobPartial", null);
        }

        [Authorize]
        public ActionResult UnassignAndroidToJob(int androidId)
        {
            try
            {
                var android = _androidManager.GetById(androidId);
                var job = android.CurrentJob;
                _androidManager.RemoveJob(android);
                var model =
                    new AndroidsAssignedToJobViewModel()
                    {
                        CurrentJob = job,
                        AndroidList = _androidManager.GetList()
                    };

                return PartialView("AndroidListAssignedToJobPartial", model);
            }
            catch (Exception e)
            {
                var errors = new List<string>();
                do
                {
                    errors.Add(e.Message);
                    e = e.InnerException;
                } while (e != null);

                ViewBag.Errors = errors;
            }
            return PartialView("AndroidListAssignedToJobPartial", null);
        }
    }
}