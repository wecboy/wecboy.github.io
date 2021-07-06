using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bug_tracker.Models;
using PagedList;

public class tblProject : BaseClass
{
    public IRepository<projects> repoProject;
    public tblProject()
    {
        repoProject = new EFGenericRepository<projects>(new bug_trackerEntities());
    }

    public List<projects> GetProjectList()
    {
        var data = repoProject.ReadAll(m => m.rowid == m.rowid)
             .OrderByDescending(m => m.rowid)
             .ToList();
        if (data != null)
        {
            using (tblUser users = new tblUser())

            {        
                    for (int i = 0; i < data.Count; i++)
                    {
                        data[i].pmanager_id= users.GetUserName (data[i].pmanager_id);
                    }        
            }
        }
        return data;
    }

    public string GetProjectName(string No)
    {
        string str_name = "";
        var data = repoProject.ReadSingle(m => m.pid == No);
        if (data != null) str_name = data.pname;
        return str_name;
    }

   
    public List<SelectListItem> GetProjectNoList()
    {
        List<SelectListItem> lst_values = new List<SelectListItem>();
        var datas = repoProject.ReadAll(m => m.rowid == m.rowid).OrderBy(m => m.pname);
        if (datas != null)
        {
            foreach (var data in datas)
            {
                SelectListItem item = new SelectListItem();
                item.Value = data.pid;
                item.Text = string.Format("{0}", data.pid);
                lst_values.Add(item);
            }
            lst_values.First().Selected = true;
        }
        return lst_values;
    }
    public List<SelectListItem> GetProjectNameList()
    {
        List<SelectListItem> lst_values = new List<SelectListItem>();
        var datas = repoProject.ReadAll(m => m.rowid == m.rowid).OrderBy(m => m.pname);
        if (datas != null)
        {
            foreach (var data in datas)
            {
                SelectListItem item = new SelectListItem();
                item.Value = data.pid;
                item.Text = string.Format("{0}", data.pname);
                lst_values.Add(item);
            }
            lst_values.First().Selected = true;
        }
        return lst_values;
    }

    

}


   

