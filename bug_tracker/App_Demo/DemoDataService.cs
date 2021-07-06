using System;
using System.Collections.Generic;
using System.Linq;
using bug_tracker.Models;
using AppDemo;
using AutoMapper;

public class DemoDataService
{
    public void SetAppInitData()
    {
        AddUserInitData();
    }

    public void SetAppDemoData()
    {
        AddProgramDemoData();
        AddUserMessageDemoData();
        AddUserNotificationDemoData();
        AddAppDemoData();
    }

    public void AddUserInitData()
    {
        using (DemoData demoData = new DemoData())
        {
            using (tblUser userTbl = new tblUser())
            {
                foreach (var userData in demoData.UserDemoList(AppService.IsDebugMode))
                {
                    userTbl.repoUser.Create(userData);
                }
                userTbl.repoUser.SaveChanges();
            }
        }
    }

    public void AddProgramDemoData()
    {
        using (tblUser user = new tblUser())
        {

        }
    }

    public void AddUserMessageDemoData()
    {
        using (tblUser user = new tblUser())
        {

        }
    }

    public void AddUserNotificationDemoData()
    {
        using (tblUser user = new tblUser())
        {

        }
    }
    public void AddAppDemoData()
    {
        using (tblUser user = new tblUser())
        {

        }
    }
}
