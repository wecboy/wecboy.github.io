using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bug_tracker.Models;
using PagedList;

namespace bug_tracker.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            UserAccount.Logout();
            LoginViewModel model = new LoginViewModel()
            {
                UserNo = "",
                Password = ""
            };
            return View(model);
        }
        [AllowAnonymous]
        [HttpPost]
        //model為使用這輸入資料
        public ActionResult Login(LoginViewModel model)
        {
            //過濾LoginViewModel條件(如果條件不符合回到View(model))
            if (!ModelState.IsValid) return View(model);
            using (bug_trackerEntities db = new bug_trackerEntities())
            {
                using (Cryptographys cryp = new Cryptographys())
                {
                    string str_password = cryp.SHA256Encode(model.Password);
                    var data = db.users
                        .Where(m => m.umno == model.UserNo)
                        .Where(m => m.upassword == str_password)
                         .Where(m => m.isvarify == true)
                        //.FirstOrDefault()只抓第一筆資料
                        .FirstOrDefault();
                    if (data == null)
                    {
                        // 登入帳號或密碼不符合!!畫面顯示在UserNo下面
                        ModelState.AddModelError("UserNo", "登入帳號或密碼不符合!!");
                        return View(model);
                    }
                    UserAccount.UserNo = model.UserNo;
                    UserAccount.Login();
                    return RedirectToAction("ALLTList", "Bug");
                }
            }
        }
       
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            using (bug_trackerEntities db = new bug_trackerEntities())
            {
                var data = db.users.Where(m => m.umno == model.UserNo).FirstOrDefault();
                if (data != null)
                {
                    ModelState.AddModelError("UserNo", "帳號重覆註冊!!");
                    return View(model);
                }
                data = db.users.Where(m => m.uemail == model.UserEmail).FirstOrDefault();
                if (data != null)
                {
                    ModelState.AddModelError("UserEmail", "電子信箱重覆註冊!!");
                    return View(model);
                }

                using (Cryptographys cryp = new Cryptographys())
                {
                    DateTime dtm_birth = DateTime.MinValue;
                    users new_user = new users();
                    new_user.umno = model.UserNo;
                    new_user.uname = model.UserName;
                    new_user.urole = "U";
                    new_user.isvarify = false;
                    new_user.upassword = cryp.SHA256Encode(model.Password);
                    new_user.uemail = model.UserEmail;
                    new_user.uphone = model.UserPhone;
                    new_user.remark = Guid.NewGuid().ToString().Replace("-", "").ToLower();
                    DateTime.TryParse(model.BirthDay, out dtm_birth);
                    if (dtm_birth != DateTime.MinValue) new_user.ubirthday = dtm_birth;

                    db.users.Add(new_user);
                    db.SaveChanges();

                    //寄出註冊驗證信件
                    string str_message = "";
                    using (AppMail mail = new AppMail())
                    {
                        str_message = mail.UserRegister(model.UserNo);
                    }
                    TempData["MessageHeader"] = "使用者註冊通知";
                    TempData["MessageText"] = (string.IsNullOrEmpty(str_message)) ? "系統已寄出一封確認信,請檢查!!" : str_message;

                }
                return RedirectToAction("MessagePage");
            }

        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Verify(string id)
        {
            using (bug_trackerEntities db = new bug_trackerEntities())
            {
                bool bln_valid = false;
                TempData["MessageHeader"] = "使用者註冊電子信箱驗證";
                var data = db.users.Where(m => m.remark == id).FirstOrDefault();
                if (data == null)
                {
                    TempData["MessageText"] = "驗證碼找不到!!";
                    return RedirectToAction("MessagePage");
                }
                bln_valid = data.isvarify;
                if (bln_valid)
                {
                    TempData["MessageText"] = "帳號電子信箱已驗證過,不可重覆驗證!!";
                    return RedirectToAction("MessagePage");
                }

                data.remark = "";
                data.isvarify = true;
                db.SaveChanges();
                TempData["MessageText"] = "帳號電子信箱已驗證成功!!";
                return RedirectToAction("MessagePage");
            }
        }

        [HttpGet]
        [LoginAuthorize(RoleList = "Admin,User,Manager,Devloper")]
        public ActionResult ResetPassword()
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel();
            return View(model);
        }

        [HttpPost]
        [LoginAuthorize(RoleList = "Admin,User,Manager,Devloper")]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            using (Cryptographys cryp = new Cryptographys())
            {
                string str_password = cryp.SHA256Encode(model.NewPassword);
                using (bug_trackerEntities db = new bug_trackerEntities())
                {
                    TempData["MessageHeader"] = "使用者密碼變更";
                    var data = db.users.Where(m => m.umno == UserAccount.UserNo).FirstOrDefault();
                    if (data != null)
                    {
                        data.upassword = str_password;
                        db.SaveChanges();
                        TempData["MessageText"] = "密碼已更新,下次登入請使用新的密碼!!";
                    }
                    else
                    {
                        TempData["MessageText"] = "帳號不存在 , 密碼未更新!!";
                    }
                    return RedirectToAction("MessagePage");
                }
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgetPassword()
        {
            using (bug_trackerEntities db = new bug_trackerEntities())
            {

                return View();
            }
        }
        [HttpPost]
        [AllowAnonymous]
        //[LoginAuthorize(RoleList = "Guest")]
        public ActionResult ForgetPassword(ForgetPasswordViewModel model)
        {

            if (!ModelState.IsValid) return View(model);

            using (bug_trackerEntities db = new bug_trackerEntities())
            {
                using (Cryptographys cryp = new Cryptographys())
                {
                    var data = db.users.Where(m => m.uemail == model.UserEmail).FirstOrDefault();


                    if (data == null)
                    {
                        ModelState.AddModelError("email", "此電子郵件不存在!!");
                        return View(model);
                    }
                    else

                    {
                        string allowedChars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ123456789";
                        int passwordLength = 6;//密碼長度
                        char[] chars = new char[passwordLength];
                        Random rd = new Random();

                        for (int i = 0; i < passwordLength; i++)
                        {
                            //allowedChars -> 這個String ，隨機取得一個字，丟給chars[i]
                            chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
                        }

                        string password = new string(chars);

                        data.remark = password;
                      
                       data.upassword = cryp.SHA256Encode(password);                   
                        db.SaveChanges();

                    }
                    using (AppMail mail = new AppMail())
                    {
                        mail.UserForget(model.UserEmail);
                    }
                    TempData["MessageHeader"] = "帳號及新密碼已發送至電子信箱";
                    TempData["MessageText"] = "請登入後修改密碼!!";
                }
                return RedirectToAction("MessagePage");

            }
        }

        [HttpGet]
        public ActionResult MessagePage()
        {
            ViewBag.MessageHeader = TempData["MessageHeader"].ToString();
            ViewBag.MessageText = TempData["MessageText"].ToString();
            return View();
        }
       

        [LoginAuthorize(RoleList = "Admin,Manager")]
        public ActionResult TList()
        {
            using (tblUser users = new tblUser())
            {
                var model = users.GetUserPageList();
                return View(model);
            }
        }

        [LoginAuthorize(RoleList = "Admin,Manager")]
        public ActionResult TCreate()
        {
            using (tblAppCode appCode = new tblAppCode())
            {
                users model = new users();
                ViewBag.RoleNoList = appCode.GetCodeList("Role");
                return View(model);
            }
        }

        [LoginAuthorize(RoleList = "Admin,Manager")]
        [HttpPost]
        public ActionResult TCreate(users model)
        {
            using (tblAppCode appCode = new tblAppCode())
            {
                using (Cryptographys cryp = new Cryptographys())
                {
                    ViewBag.RoleNoList = appCode.GetCodeList("Role");
                    if (!ModelState.IsValid) return View(model);
                    using (tblUser users = new tblUser())
                    {
                        if (users.NoDuplicate(0, model.umno))
                        {
                            ModelState.AddModelError("umno", "帳號重覆註冊!!");
                            return View(model);
                        }

                        if (users.NoDuplicatemail(0, model.uemail))
                        {
                            ModelState.AddModelError("uemail", "電子郵箱重覆註冊!!");
                            return View(model);
                        }
                        model.upassword = cryp.SHA256Encode(model.upassword);
                        model.uinit_time = DateTime.Now;
                        users.repoUser.Create(model);
                        users.repoUser.SaveChanges();
                        return RedirectToAction("TList");
                    }
                }
            }
        }

        [LoginAuthorize(RoleList = "Admin,Manager")]
        public ActionResult TEdit(int id)
        {
            using (tblAppCode appCode = new tblAppCode())
            {
                using (tblUser users = new tblUser())
                {
                    var model = users.repoUser.ReadSingle(m => m.rowid == id);
                    ViewBag.RoleNoList = appCode.GetCodeList("Role");
                    return View(model);
                }
            }
        }

        [LoginAuthorize(RoleList = "Admin,Manager")]
        [HttpPost]
        public ActionResult TEdit(users model)
        {
            using (tblAppCode appCode = new tblAppCode())
            {
                ViewBag.RoleNoList = appCode.GetCodeList("Role");
                if (!ModelState.IsValid) return View(model);
                using (tblUser users = new tblUser())
                {
                    var data = users.repoUser.ReadSingle(m => m.rowid == model.rowid);
                    if (data != null)
                    {
                        data.umno = model.umno;
                        data.uname = model.uname;
                        data.urole = model.urole;
                        data.ubirthday = model.ubirthday;
                        data.uemail = model.uemail;
                        data.remark = model.remark;
                        data.isvarify = model.isvarify;
                        data.umodified_time = DateTime.Now;
                        users.repoUser.Update(data);
                        users.repoUser.SaveChanges();
                    }
                    return RedirectToAction("TList");
                }
            }
        }

        [LoginAuthorize(RoleList = "Admin,Manager")]
        public ActionResult TDelete(int id)
        {
            using (tblUser users = new tblUser())
            {
                var model = users.repoUser.ReadSingle(m => m.rowid == id);
                if (model != null)
                {
                    users.repoUser.Delete(model);
                    users.repoUser.SaveChanges();
                }
                return RedirectToAction("TList");
            }
        }



    }
}