using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// 應用程式服務類別
/// </summary>
public static class AppService
{
    public static string PrgNo
    {
        get { return (HttpContext.Current.Session["PrgNo"] == null) ? "" : (HttpContext.Current.Session["PrgNo"].ToString()); }
        set { HttpContext.Current.Session["PrgNo"] = value; }
    }
    public static string PrgName
    {
        get { return (HttpContext.Current.Session["PrgName"] == null) ? "" : (HttpContext.Current.Session["PrgName"].ToString()); }
        set { HttpContext.Current.Session["PrgName"] = value; }
    }
    public static string ModuleNo
    {
        get { return (HttpContext.Current.Session["ModuleNo"] == null) ? "" : (HttpContext.Current.Session["ModuleNo"].ToString()); }
        set { HttpContext.Current.Session["ModuleNo"] = value; }
    }
    public static string ModuleName
    {
        get { return (HttpContext.Current.Session["ModuleName"] == null) ? "" : (HttpContext.Current.Session["ModuleName"].ToString()); }
        set { HttpContext.Current.Session["ModuleName"] = value; }
    }
    public static int PageNumber
    {
        get { return (HttpContext.Current.Session["PageNumber"] == null) ? 1 : ((int)HttpContext.Current.Session["PageNumber"]); }
        set { HttpContext.Current.Session["PageNumber"] = value; }
    }
    public static int PageSize
    {
        get { return (HttpContext.Current.Session["PageSize"] == null) ? 10 : ((int)HttpContext.Current.Session["PageSize"]); }
        set { HttpContext.Current.Session["PageSize"] = value; }
    }
    /// <summary>
    /// 應用程式名稱
    /// </summary>
    public static string AppName
    {
        get
        {
            object obj_value = WebConfigurationManager.AppSettings["AppName"];
            return (obj_value == null) ? "未定義名稱" : obj_value.ToString(); 
        }
    }
    public static string GetAppSettingValue(string appName, string defaultValue)
    {
        object obj_value = WebConfigurationManager.AppSettings[appName];
        string str_value = (obj_value == null) ? defaultValue : obj_value.ToString();
        return str_value;
    }
    public static bool IsDebugMode
    {
        get
        {
            if (HttpContext.Current.Session["IsDebugMode"] == null)
                HttpContext.Current.Session["IsDebugMode"] = (GetAppSettingValue("IsDebugMode", "0") == "1");
            return (bool)HttpContext.Current.Session["IsDebugMode"];
        }
    }
    /// <summary>
    /// 除錯模式
    /// </summary>
    public static bool DebugMode
    {
        get
        {
            object obj_value = WebConfigurationManager.AppSettings["DebugMode"];
            string str_value = (obj_value == null) ? "0" : obj_value.ToString();
            return (str_value == "1");
        }
    }
    public static bool IsMessage
    {
        get
        {
            if (HttpContext.Current.Session["IsMessage"] == null)
                HttpContext.Current.Session["IsMessage"] = (GetAppSettingValue("IsMessage", "0") == "1");
            return (bool)HttpContext.Current.Session["IsMessage"];
        }
    }

    /// <summary>
    /// 首頁下方選單是否有 [系統通知] 功能
    /// </summary>
    public static bool IsNotification
    {
        get
        {
            if (HttpContext.Current.Session["IsNotification"] == null)
                HttpContext.Current.Session["IsNotification"] = (GetAppSettingValue("IsNotification", "0") == "1");
            return (bool)HttpContext.Current.Session["IsNotification"];
        }
    }
}
