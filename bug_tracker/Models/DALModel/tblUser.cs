using bug_tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

public class tblUser : BaseClass
{
    public IRepository<users> repoUser;
    public tblUser()
    {
        repoUser = new EFGenericRepository<users>(new bug_trackerEntities());
    }

    public List<SelectListItem> GetUserNoList()
    {
        List<SelectListItem> lst_values = new List<SelectListItem>();
        var datas = repoUser.ReadAll(m => m.rowid ==m.rowid).OrderBy(m => m.uname);
        if (datas != null)
        {
            foreach (var data in datas)
            {
                SelectListItem item = new SelectListItem();
                item.Value = data.umno;
                item.Text = string.Format("{0}", data.umno);
                lst_values.Add(item);
            }
            lst_values.First().Selected = true;
        }
        return lst_values;
    }
    public List<SelectListItem> GetUserNameList()
    {
        List<SelectListItem> lst_values = new List<SelectListItem>();
        var datas = repoUser.ReadAll(m => m.rowid == m.rowid).OrderBy(m => m.uname);
        if (datas != null)
        {
            foreach (var data in datas)
            {
                SelectListItem item = new SelectListItem();
                item.Value = data.umno;
                item.Text = string.Format("{0}", data.uname);
                lst_values.Add(item);
            }
            lst_values.First().Selected = true;
        }
        return lst_values;
    }
    public List<users> GetUserPageList()
    {
        var data = repoUser
             .ReadAll()
             .OrderByDescending(m => m.umno)
             .ToList();
        if (data != null)
        {
            using (tblAppCode appCode = new tblAppCode())
            {
                for (int i = 0; i < data.Count; i++)
                {
                    data[i].urole = appCode.GetCodeName("Role", data[i].urole);
                }
            }
        }
        return data;
    }
    public bool NoDuplicate(int rowID, string memberNo)
    {
        var data = repoUser.ReadSingle(m => m.umno == memberNo && m.rowid != rowID);
        if (data != null) return true;
        return false;
    }

    public bool NoDuplicatemail(int rowID, string memberNo)
    {
        var data = repoUser.ReadSingle(m => m.uemail == memberNo && m.rowid != rowID);
        if (data != null) return true;
        return false;
    }

    public string GetUserName(string userNo)
    {
        string str_name = "";
        var data = repoUser.ReadSingle(m => m.umno == userNo);
        if (data != null) str_name = data.uname;
        return str_name;
    }

    public bool CheckLogin(string userNo, string userPassword)
    {
        using (Cryptographys cryp = new Cryptographys())
        {
            int int_count = repoUser.ReadAll().Count();
            if (int_count == 0)
            {
                DemoDataService service = new DemoDataService();
                service.SetAppInitData();
            }
            string str_password = cryp.SHA256Encode(userPassword);
            var data = repoUser
                .ReadSingle(m => m.umno == userNo && m.upassword == str_password);
            return (data != null);
        }
    }

}