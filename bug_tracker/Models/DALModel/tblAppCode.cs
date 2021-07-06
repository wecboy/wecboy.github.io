using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bug_tracker.Models;

public class tblAppCode : BaseClass
{
    public IRepository<AppCode> repoAppCode;
    public tblAppCode()
    {
        repoAppCode = new EFGenericRepository<AppCode>(new bug_trackerEntities());
    }

    public string GetCodeName(string typeNo, string codeNo)
    {
        string str_name = "";
        var data = repoAppCode.ReadSingle(m => m.type_no == typeNo && m.mno == codeNo);
        if (data != null) str_name = data.mname;
        return str_name;
    }

    public List<SelectListItem> GetCodeList(string typeNo)
    {
        List<SelectListItem> lst_values = new List<SelectListItem>();
        var datas = repoAppCode.ReadAll(m => m.type_no == typeNo).OrderBy(m => m.mno);
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