using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using bug_tracker.Models;

/// <summary>
/// 使用者資訊類別
/// </summary>
public static class UserAccount
{
    /// <summary>
    /// 登入使用者角色
    /// </summary>
    public static EnumList.LoginRole Role
    {
        get { return (HttpContext.Current.Session["Role"] == null) ? EnumList.LoginRole.Guest : (EnumList.LoginRole)HttpContext.Current.Session["Role"]; }
        set { HttpContext.Current.Session["Role"] = value; }
    }
    /// <summary>
    /// 登入使用者角色名稱
    /// </summary>
    public static string RoleName { get { return EnumList.GetRoleName(Role); } }
    /// <summary>
    /// 使用者帳號
    /// </summary>
    public static string UserNo 
    {
        get { return (HttpContext.Current.Session["UserNo"] == null) ? "" : HttpContext.Current.Session["UserNo"].ToString(); }
        set { HttpContext.Current.Session["UserNo"] = value; }
    }
    /// <summary>
    /// 使用者名稱
    /// </summary>
    public static string UserName
    {
        get { return (HttpContext.Current.Session["UserName"] == null) ? "" : HttpContext.Current.Session["UserName"].ToString(); }
        set { HttpContext.Current.Session["UserName"] = value; }
    }
    /// <summary>
    /// 使用者電子信箱
    /// </summary>
    public static string UserEmail
    {
        get { return (HttpContext.Current.Session["UserEmail"] == null) ? "" : HttpContext.Current.Session["UserEmail"].ToString(); }
        set { HttpContext.Current.Session["UserEmail"] = value; }
    }
    /// <summary>
    /// 使用者是否已登入
    /// </summary>
    public static bool IsLogin
    {
        get { return (HttpContext.Current.Session["IsLogin"] == null) ? false : (bool)HttpContext.Current.Session["IsLogin"]; }
        set { HttpContext.Current.Session["IsLogin"] = value; }
    }


    public static void Login()
    {
        using (bug_trackerEntities db = new bug_trackerEntities())
        {
            var data = db.users.Where(m => m.umno == UserNo).FirstOrDefault();
            if (data == null)
                Logout();
            else
            {
                IsLogin = true;
                UserName = data.uname;
                UserEmail = data.uemail;
                Role = EnumList.GetRoleType(data.urole);
            }
        }
    }

    public static void Logout()
    {
        IsLogin = false;
        Role = EnumList.LoginRole.Guest;
        UserNo = "";
        UserName = "";
        UserEmail = "";
    }
    
    public static string UserInfo { get { return string.Format("{0} {1}", UserNo, UserName); } }
    public static string UserImage { get { return GetUserImageUrl(UserNo); } }
    public static int MessageUnRead
    {
        get
        {
            using (bug_trackerEntities db = new bug_trackerEntities())
            {
                int int_count = db.UserMessage
                    .Where(m => m.receive_no == UserNo)
                    .Where(m => m.is_read == false)
                    .Count();
                return int_count;
            }
        }
    }
    public static bool UploadImageMode
    {
        get
        {
            bool bln_value = false;
            if (HttpContext.Current.Session["UploadImage"] != null)
            {
                string str_value = HttpContext.Current.Session["UploadImage"].ToString();
                bln_value = (str_value == "1");
            }
            return bln_value;
        }
        set
        { HttpContext.Current.Session["UploadImage"] = (value) ? "1" : "0"; }
    }

    public static string UserImageUrl
    {
        get
        {
            string str_url = "~/Images/user/guest.jpg";
            string str_file = string.Format("~/Images/user/{0}.jpg", UserNo);
            if (File.Exists(HttpContext.Current.Server.MapPath(str_file))) str_url = str_file;
            str_url += string.Format("?{0}", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            return str_url;
        }
    }
   
    public static string GetUserImageUrl(string userNo)
    {
        string str_image = string.Format("~/Images/User/{0}.jpg", userNo);
        if (!File.Exists(HttpContext.Current.Server.MapPath(str_image)))
            str_image = "~/Images/User/guest.jpg";

        str_image += string.Format("?t={0}", DateTime.Now.ToString("yyyyMMddHHmmss"));
        return str_image;
    }

    public static List<UserMessage> GetUserMessageList(int rowCount)
    {
        using (tblUserMessage userMessage = new tblUserMessage())
        {
            var data = userMessage.GetUserMessageList(UserNo, rowCount);
            return (data);
        }
    }
    
}
