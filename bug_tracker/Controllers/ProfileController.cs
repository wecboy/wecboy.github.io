using bug_tracker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bug_tracker.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }
        [LoginAuthorize(RoleList = "Admin,User,Manager,Devloper")]
        public ActionResult UserProfile()
        {
            using (bug_trackerEntities db = new bug_trackerEntities())
            {
                var model = db.users
                    .Where(m => m.umno == UserAccount.UserNo)
                    .FirstOrDefault();
                return View(model);
            }
        }

        [HttpGet]
        [LoginAuthorize(RoleList = "Admin,User,Manager,Devloper")]
        public ActionResult EditProfile()
        {
            using (bug_trackerEntities db = new bug_trackerEntities())
            {
                var model = db.users
                .Where(m => m.umno == UserAccount.UserNo)
                .FirstOrDefault();
                return View(model);
            }
        }

        [HttpPost]
        [LoginAuthorize(RoleList = "Admin,User,Manager,Devloper")]
        public ActionResult EditProfile(users model)
        {
            if (!ModelState.IsValid) return View(model);

            using (bug_trackerEntities db = new bug_trackerEntities())
            {
                var user = db.users
                .Where(m => m.rowid == model.rowid)
                .FirstOrDefault();

                if (user != null)
                {
                    user.uname = model.uname;
                    user.uemail = model.uemail;
                    user.uphone = model.uphone;
                    user.ubirthday = model.ubirthday;
                    user.remark = model.remark;
                    db.SaveChanges();
                }

                return RedirectToAction("UserProfile");
            }
        }

        [LoginAuthorize(RoleList = "Admin,User,Manager,Devloper")]
        public ActionResult UploadImage()
        {
            UserAccount.UploadImageMode = true;
            return RedirectToAction("UserProfile");
        }

        [HttpPost]
        [LoginAuthorize(RoleList = "Admin,User,Manager,Devloper")]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = UserAccount.UserNo + ".jpg";
                    var path = Path.Combine(Server.MapPath("~/Images/user"), fileName);
                    if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                    file.SaveAs(path);
                }
            }
            UserAccount.UploadImageMode = false;
            return RedirectToAction("UserProfile");
        }

        [LoginAuthorize(RoleList = "Admin,User,Manager,Devloper")]
        public ActionResult UploadCancel()
        {
            UserAccount.UploadImageMode = false;
            return RedirectToAction("UserProfile");
        }
    }
}