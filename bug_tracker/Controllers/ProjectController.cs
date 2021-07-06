using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bug_tracker.Models;
using PagedList;

namespace bug_tracker.Controllers
{
    public class ProjectController : Controller
    {
  
        [HttpGet]
        [LoginAuthorize(RoleList = "Admin,Manager")]
        public ActionResult TList()
        {
            using (tblProject Projecties = new tblProject())
            {
                var model = Projecties.GetProjectList();
                return View(model);
            }
        }


        [LoginAuthorize(RoleList = "Admin,Manager")]
        public ActionResult TCreate()
        {
            using (tblUser users = new tblUser())
            {
                projects model = new projects();
                ViewBag.ManagerNoList = users.GetUserNoList();
                ViewBag.ManagerNameList = users.GetUserNameList();
                return View(model);
            }
        }

        [LoginAuthorize(RoleList = "Admin,Manager")]
        [HttpPost]
        public ActionResult TCreate(projects model)
        {
            if (!ModelState.IsValid) return View(model);
            using (tblProject projects = new tblProject())
            {
                using (tblUser users = new tblUser())
                {

                    //model.pmanager_id = users.GetUserName(model.pmanager_id);
                    model.pmanager_id = model.pmanager_id;
                    model.pinit_time = DateTime.Now;
                    projects.repoProject.Create(model);
                    projects.repoProject.SaveChanges();
                    return RedirectToAction("TList");
                }
            }
        }

        [LoginAuthorize(RoleList = "Admin,Manager")]
        public ActionResult TEdit(int id)
        {
            using (tblUser users = new tblUser())
            {
                using (tblProject projects = new tblProject())
                {
                    var model = projects.repoProject.ReadSingle(m => m.rowid == id);
                    ViewBag.ManagerNoList = users.GetUserNoList();
                    return View(model);
                }
            }
        }

        [LoginAuthorize(RoleList = "Admin,Manager")]
        [HttpPost]
        public ActionResult TEdit(projects model)
        {
            using (tblUser users = new tblUser())
            {
                ViewBag.ManagerNoList = users.GetUserNoList();
                if (!ModelState.IsValid) return View(model);
                using (tblProject projects = new tblProject())
                {
                    var data = projects.repoProject.ReadSingle(m => m.rowid == model.rowid);
                    if (data != null)
                    {
                        data.pname = model.pname;
                        data.pdescription = model.pdescription;
                        data.pmanager_id = model.pmanager_id;
                        data.pmodified_time = DateTime.Now;

                        projects.repoProject.Update(data);
                        projects.repoProject.SaveChanges();
                    }
                    return RedirectToAction("TList");
                }
            }
        }

        [LoginAuthorize(RoleList = "Admin,Manager")]
        public ActionResult TDelete(int id)
        {
            using (tblProject projects = new tblProject())
            {
                var model = projects.repoProject.ReadSingle(m => m.rowid == id);
                if (model != null)
                {
                    projects.repoProject.Delete(model);
                    projects.repoProject.SaveChanges();
                }
                return RedirectToAction("TList");
            }
        }



        }


    }