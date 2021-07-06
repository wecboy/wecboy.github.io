using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bug_tracker.Controllers;
using bug_tracker.Models;

namespace bug_tracker.Controllers
{
    public class MessageController : Controller
    {
        [LoginAuthorize()]
        public ActionResult Index(int page = 1, int pageSize = 10, int rowID = 0)
        {
            using (tblUserMessage userMessage = new tblUserMessage())
            {
                AppService.PageNumber = page;
                var model = userMessage.GetUserMessagePageList(UserAccount.UserNo, page, pageSize);

                string str_content = "";
                if (rowID > 0)
                {
                    var data = userMessage.repoUserMessage.ReadSingle(m => m.rowid == rowID);
                    if (data != null) str_content = data.text_content;
                }
                ViewBag.MessageID = rowID;
                ViewBag.MessageContent = str_content;
                return View(model);
            }
        }

        [LoginAuthorize()]
        public ActionResult Details(int id)
        {
            using (tblUserMessage userMessage = new tblUserMessage())
            {
                userMessage.ReadMessage(id);
                return RedirectToAction("Index", new { page = AppService.PageNumber, rowID = id });
            }
        }

        [LoginAuthorize()]
        public ActionResult Create()
        {
            using (tblAppCode appCode = new tblAppCode())
            {
                using (tblUser user = new tblUser())
                {
                    UserMessage model = new UserMessage();
                    ViewBag.CodeNoList = appCode.GetCodeList("Message");
                    ViewBag.ReceiveNoList = user.GetUserNoList();
                    ViewBag.ReceiveNameList = user.GetUserNameList();
                    return View(model);
                }
            }
        }

        [LoginAuthorize()]
        [HttpPost]
        public ActionResult Create(UserMessage model)
        {
            if (!ModelState.IsValid) return View(model);
            using (tblUserMessage userMessage = new tblUserMessage())
            {
                using (tblUser user = new tblUser())
                {
                    model.receive_name = user.GetUserName(model.receive_no);
                    model.date_sender = DateTime.Now;
                    model.is_read = false;
                    model.sender_no = UserAccount.UserNo;
                    model.sender_name = UserAccount.UserName;
                    model.remark = "";
                    userMessage.repoUserMessage.Create(model);
                    userMessage.repoUserMessage.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
        }

        [LoginAuthorize()]
        public ActionResult Delete(int id)
        {
            using (tblUserMessage userMessage = new tblUserMessage())
            {
                var data = userMessage.repoUserMessage
                    .ReadSingle(m => m.rowid == id);

                userMessage.repoUserMessage.Delete(data);
                userMessage.repoUserMessage.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [LoginAuthorize()]
        public ActionResult ReadAllMessage()
        {
            using (tblUserMessage userMessage = new tblUserMessage())
            {
                userMessage.ReadAllMessage(UserAccount.UserNo);
                return RedirectToAction("Index", new { page = AppService.PageNumber });
            }
        }
    }
}