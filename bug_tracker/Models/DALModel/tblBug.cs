using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bug_tracker.Models;
using PagedList;

public class tblBug : BaseClass
{
    public IRepository<bugs> repoBug;
    public tblBug()
    {
        repoBug = new EFGenericRepository<bugs>(new bug_trackerEntities());
    }
    public List<bugs> GetBugList()
    {
        var data = repoBug.ReadAll(m => m.rowid == m.rowid)
            .Where(m => m.bcreator == UserAccount.UserNo)
             .OrderByDescending(m => m.rowid)
             .ToList();
        if (data != null)
        {
            using (tblAppCode appCode = new tblAppCode())

            {
                using (tblProject projects = new tblProject())
                {


                    for (int i = 0; i < data.Count; i++)
                    {
                        data[i].bstatus_id = appCode.GetCodeName("Status", data[i].bstatus_id);
                        data[i].bpriority_id = appCode.GetCodeName("Priority", data[i].bpriority_id);
                        data[i].bpid = projects.GetProjectName(data[i].bpid);
                    }
                }
            }
        }
        return data;
    }


    public List<bugs> GetABugList()
    {
        var data = repoBug.ReadAll(m => m.rowid == m.rowid)
            .Where(m => m.basignee == UserAccount.UserNo)
             .OrderByDescending(m => m.rowid)
             .ToList();
        if (data != null)
        {
            using (tblAppCode appCode = new tblAppCode())

            {
                using (tblProject projects = new tblProject())
                {


                    for (int i = 0; i < data.Count; i++)
                    {
                        data[i].bstatus_id = appCode.GetCodeName("Status", data[i].bstatus_id);
                        data[i].bpriority_id = appCode.GetCodeName("Priority", data[i].bpriority_id);
                        data[i].bpid = projects.GetProjectName(data[i].bpid);
                    }
                }
            }
        }
        return data;
    }

    public List<bugs> GetAllBugList()
    {
        var data = repoBug.ReadAll(m => m.rowid == m.rowid)
             .OrderByDescending(m => m.rowid)
             .ToList();
        if (data != null)
        {
            using (tblAppCode appCode = new tblAppCode())

            {
                using (tblProject projects = new tblProject())
                {


                    for (int i = 0; i < data.Count; i++)
                    {
                        data[i].bstatus_id = appCode.GetCodeName("Status", data[i].bstatus_id);
                        data[i].bpriority_id = appCode.GetCodeName("Priority", data[i].bpriority_id);
                        data[i].bpid = projects.GetProjectName(data[i].bpid);
                    }
                }
            }
        }
        return data;
    }

    public bool NoDuplicate(int rowID, string memberNo)
    {
        var data = repoBug.ReadSingle(m => m.bid == memberNo && m.rowid != rowID);
        if (data != null) return true;
        return false;
    }
}
