using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bug_tracker.Models;

public class tblPriority : BaseClass
{
    public IRepository<priorities> repoPriority;
    public tblPriority()
    {
        repoPriority = new EFGenericRepository<priorities>(new bug_trackerEntities());
    }

    public string GetPriorityName(string No)
    {
        string str_name = "";
        var data = repoPriority.ReadSingle(m => m.mno == No);
        if (data != null) str_name = data.mname;
        return str_name;
    }

    public List<SelectListItem> GetPriorityList(string No)
    {
        List<SelectListItem> lst_values = new List<SelectListItem>();
        var datas = repoPriority.ReadAll(m => m.mno == No).OrderBy(m => m.mno);
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
