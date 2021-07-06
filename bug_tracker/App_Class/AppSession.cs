using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Session 類別
/// </summary>
public class AppSession
{
    /// <summary>
    /// 表頭的目前頁數
    /// </summary>
    public static int MasterPage
    {
        get { return (HttpContext.Current.Session["MasterPage"] == null) ? 1 : (int)HttpContext.Current.Session["MasterPage"]; }
        set { HttpContext.Current.Session["MasterPage"] = value; }
    }
    /// <summary>
    /// 表頭的單頁筆數
    /// </summary>
    public static int MasterPageSize
    {
        get { return (HttpContext.Current.Session["MasterPageSize"] == null) ? 1 : (int)HttpContext.Current.Session["MasterPageSize"]; }
        set { HttpContext.Current.Session["MasterPageSize"] = value; }
    }
    /// <summary>
    /// 明細的目前頁數
    /// </summary>
    public static int DetailPage
    {
        get { return (HttpContext.Current.Session["DetailPage"] == null) ? 1 : (int)HttpContext.Current.Session["DetailPage"]; }
        set { HttpContext.Current.Session["DetailPage"] = value; }
    }
    /// <summary>
    /// 明細的單頁筆數
    /// </summary>
    public static int DetailPageSize
    {
        get { return (HttpContext.Current.Session["DetailPageSize"] == null) ? 1 : (int)HttpContext.Current.Session["DetailPageSize"]; }
        set { HttpContext.Current.Session["DetailPageSize"] = value; }
    }
}
