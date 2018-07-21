using System;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AndroidManagerApplication.Models.Entities;
using AndroidManagerApplication.Models.ViewModels;
using AndroidManagerApplication.Models.Managers;

namespace AndroidManagerApplication.Controllers
{
    public class AndroidController : Controller
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

        public AndroidController()
        {
            _androidManager = new AndroidManager();
            _jobManager = new JobManager();
            _skillManager = new SkillManager();
            _imageManager = new ImageManager();
        }

        [Authorize]
        public ActionResult AndroidList()
        {
            _skillManager.ReloadList();
            _imageManager.ReloadList();
            _jobManager.ReloadList();

            ViewBag.SkillList = _skillManager.GetList();

            var androidList = _androidManager.GetList();
            return View(androidList);
        }

        [Authorize]
        public ActionResult NewAndroid()
        {
            var android = new EditAndroidViewModel();
            var jobList = new SelectList(_jobManager.GetList(), "Id", "Name", android.JobId);
            ViewBag.JobList = jobList;
            return View(android);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateAndroid(EditAndroidViewModel model)
        {
            var jobList = new SelectList(_jobManager.GetList(), "Id", "Name", model.JobId);
            ViewBag.JobList = jobList;

            if (!ModelState.IsValid)
            {
                return View("NewAndroid", model);
            }

            // Avatar
            Image image;
            Stream imageStream = new MemoryStream();
            Resources.DefaultImage.Save(imageStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            if (model.Avatar != null)
            {
                image = new Image();
                imageStream = model.Avatar.InputStream;

                using (Stream inputStream = imageStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    image.ImageData = memoryStream.ToArray();
                }
                _imageManager.Add(image);
            }
            else image = _imageManager.GetDefaultImage();

            // Skills
            var skillNames = model.Skills.Split(',');
            var skillList = skillNames.Select(s => _skillManager.GetOrCreateSkillByName(s)).ToList();

            var android = new Android()
            {
                Name = model.Name,
                Avatar = image,
                Skills = skillList,
                Reliability = model.Reliability
            };
            _androidManager.Add(android);

            // Job
            if (model.JobId != null)
            {
                var job = _jobManager.GetById((int)model.JobId);
                _jobManager.AssignAndroidToJob(job, android);
            }

            return RedirectToAction("AndroidList", "Android");
        }

        [Authorize]
        public ActionResult DeleteAndroid(int androidId)
        {
            try
            {
                _androidManager.DeleteById(androidId);
            } catch(Exception e)
            {
                ViewBag.Error = e.Message;
            }
            
            return RedirectToAction("AndroidList", "Android");
        }
    }
}