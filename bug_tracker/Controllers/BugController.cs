using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bug_tracker.Models;
using PagedList;

namespace bug_tracker.Controllers
{
    public class BugController : Controller
    {
  
       
        [LoginAuthorize(RoleList = "Admin,User,Manager,Devloper")]
        public ActionResult TList()
        {
            using (tblBug bugs = new tblBug())
            {
                var model = bugs.GetBugList();
                return View(model);
            }
        }

        [LoginAuthorize(RoleList = "Admin,User,Manager,Devloper")]
        public ActionResult ATList()
        {
            using (tblBug bugs = new tblBug())
            {
                var model = bugs.GetABugList();
                return View(model);
            }
        }

        [LoginAuthorize(RoleList = "Admin,User,Manager,Devloper")]
        public ActionResult ALLTList()
        {
            using (tblBug bugs = new tblBug())
            {
                var model = bugs.GetAllBugList();
                return View(model);
            }
        }

        [LoginAuthorize(RoleList = "Admin,User,Manager,Devloper")]
        public ActionResult TCreate()
        {
            using (tblAppCode appCode = new tblAppCode())
            {
                using (tblProject projects = new tblProject())
                {
                    using (tblUser users = new tblUser())
                    {
                        bugs model = new bugs();
                        ViewBag.StatusNoList = appCode.GetCodeList("Status");
                        ViewBag.PriorityNoList = appCode.GetCodeList("Priority");
                        ViewBag.ProjectNoList = projects.GetProjectNameList();
                        ViewBag.UserNoList = users.GetUserNoList();
                        return View(model);
                    }
                }
            }
        }

        [LoginAuthorize(RoleList = "Admin,User,Manager,Devloper")]
        [HttpPost]
        public ActionResult TCreate(bugs model)
        {
            using (tblAppCode appCode = new tblAppCode())
            {
                using (tblProject projects = new tblProject())
                {
                    using (tblUser users = new tblUser())
                    {
                        ViewBag.StatusNoList = appCode.GetCodeList("Status");
                        ViewBag.PriorityNoList = appCode.GetCodeList("Priority");
                        ViewBag.ProjectNoList = projects.GetProjectNameList();
                        ViewBag.UserNoList = users.GetUserNoList();
                        if (!ModelState.IsValid) return View(model);
                        using (tblBug bugs = new tblBug())
                        {

                            model.bcreator = UserAccount.UserNo;
                            model.binit_time = DateTime.Now;
                            bugs.repoBug.Create(model);
                            bugs.repoBug.SaveChanges();
                            return RedirectToAction("ALLTList");
                        }
                    }
                }
            }
        }

        [LoginAuthorize(RoleList = "Admin,User,Manager,Devloper")]
        public ActionResult TEdit(int id)
        {
            using (tblAppCode appCode = new tblAppCode())
            {
                using (tblProject projects = new tblProject())
                {
                    using (tblUser users = new tblUser())
                    {
                        using (tblBug bugs = new tblBug())
                        {
                            var model = bugs.repoBug.ReadSingle(m => m.rowid == id);
                            ViewBag.StatusNoList = appCode.GetCodeList("Status");
                            ViewBag.PriorityNoList = appCode.GetCodeList("Priority");
                            ViewBag.ProjectNoList = projects.GetProjectNameList();
                            ViewBag.UserNoList = users.GetUserNoList();
                            return View(model);
                        }
                    }
                }
            }
        }

        [LoginAuthorize(RoleList = "Admin,User,Manager,Devloper")]
        [HttpPost]
        public ActionResult TEdit(bugs model)
        {
            using (tblAppCode appCode = new tblAppCode())
            {
                using (tblProject projects = new tblProject())



                {
                    using (tblUser users = new tblUser())
                    {
                        ViewBag.StatusNoList = appCode.GetCodeList("Status");
                        ViewBag.PriorityNoList = appCode.GetCodeList("Priority");
                        ViewBag.ProjectNoList = projects.GetProjectNameList();
                        ViewBag.UserNoList = users.GetUserNoList();
                        if (!ModelState.IsValid) return View(model);
                        using (tblBug bugs = new tblBug())
                        {
                            var data = bugs.repoBug.ReadSingle(m => m.rowid == model.rowid);
                            if (data != null)
                            {
                                data.bsummary = model.bsummary;
                                data.bcreator = data.bcreator;
                                data.bstatus_id = model.bstatus_id;
                                data.bpriority_id = model.bpriority_id;
                                data.basignee = model.basignee;
                                data.bdescription = model.bdescription;
                                data.bmodified_time = DateTime.Now;
                                bugs.repoBug.Update(data);
                                bugs.repoBug.SaveChanges();
                            }
                            return RedirectToAction("ALLTList");
                        }
                    }
                }
            }
        }

        [LoginAuthorize(RoleList = "Admin,User,Manager,Devloper")]
        public ActionResult TDelete(int id)
        {
            using (tblBug bugs = new tblBug())
            {
                var model = bugs.repoBug.ReadSingle(m => m.rowid == id);
                if (model != null)
                {
                    bugs.repoBug.Delete(model);
                    bugs.repoBug.SaveChanges();
                }
                return RedirectToAction("ALLTList");
            }
        }








    }
}