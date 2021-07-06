using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bug_tracker.Models;

public class tblRole : BaseClass
{
    public IRepository<roles> repoRole;
    public tblRole()
    {
        repoRole = new EFGenericRepository<roles>(new bug_trackerEntities());
    }

    public string GetStatusName(string No)
    {
        string str_name = "";
        var data = repoRole.ReadSingle(m => m.rrule == No);
        if (data != null) str_name = data.rname;
        return str_name;
    }

    public List<SelectListItem> GetStatusList(string No)
    {
        List<SelectListItem> lst_values = new List<SelectListItem>();
        var datas = repoRole.ReadAll(m => m.rrule == No).OrderBy(m => m.rowid);
        if (datas != null)
        {
            foreach (var data in datas)
            {
                SelectListItem item = new SelectListItem();
                item.Value = data.rrule;
                item.Text = data.rname;
                lst_values.Add(item);
            }
            lst_values.First().Selected = true;
        }
        return lst_values;
    }
}

