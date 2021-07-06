using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bug_tracker.Models;

public class tblStatus : BaseClass
{
    public IRepository<status> repoStatus;
    public tblStatus()
    {
        repoStatus = new EFGenericRepository<status>(new bug_trackerEntities());
    }

    public string GetStatusName(string No)
    {
        string str_name = "";
        var data = repoStatus.ReadSingle(m => m.mno == No);
        if (data != null) str_name = data.mname;
        return str_name;
    }

    public List<SelectListItem> GetStatusList(string No)
    {
        List<SelectListItem> lst_values = new List<SelectListItem>();
        var datas = repoStatus.ReadAll(m => m.mno == No).OrderBy(m => m.mno);
        if (datas != null)
        {
            foreach (var data in datas)
            {
                SelectListItem item = new SelectListItem();
                item.Value = data.mno;
                item.Text = data.mname;
                lst_values.Add(item);
            }
            lst_values.First().Selected = true;
        }
        return lst_values;
    }
}
