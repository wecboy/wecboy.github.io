using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using bug_tracker.Models;

public class tblUserMessage : BaseClass
{
    public IRepository<UserMessage> repoUserMessage;
    public tblUserMessage()
    {
        repoUserMessage = new EFGenericRepository<UserMessage>(new bug_trackerEntities());
    }

    public List<UserMessage> GetUserMessageList(string userNo, int rowCount)
    {
        var data = repoUserMessage.ReadAll(m => m.receive_no == userNo)
             .OrderByDescending(m => m.date_sender)
             .Take(rowCount)
             .ToList();
        return data;
    }

    public void ReadAllMessage(string userNo)
    {
        var data = repoUserMessage.ReadAll(m => m.receive_no == userNo);
        if (data != null)
        {
            foreach (var item in data)
            {
                ReadMessage(item.rowid);
            }
        }
    }

    public void ReadMessage(int id)
    {
        var data = repoUserMessage.ReadSingle(m => m.rowid == id);
        if (data != null)
        {
            if (!data.is_read || data.date_read == null)
            {
                data.is_read = true;
                data.date_read = DateTime.Now;
                repoUserMessage.Update(data);
                repoUserMessage.SaveChanges();
            }
        }
    }

    public IPagedList<UserMessage> GetUserMessagePageList(string userNo, int page = 1, int pageSize = 10)
    {
        var data = repoUserMessage
             .ReadAll(m => m.receive_no == userNo)
             .OrderByDescending(m => m.date_sender)
             .ToPagedList(page, pageSize);
        if (data != null)
        {
            using (tblAppCode appCode = new tblAppCode())
            {
                for (int i = 0; i < data.Count; i++)
                {
                    data[i].code_no = appCode.GetCodeName("Message", data[i].code_no);
                }
            }
        }
        return data;
    }

    public void SendAllMessage(string senderNo, string codeNo, string textTitle, string textContent)
    {
        using (tblUser user = new tblUser())
        {
            var users = user.repoUser
                .ReadAll(m => m.rowid == m.rowid)
                .Where(m => m.isvarify == true)
                .ToList();
            if (user != null)
            {
                foreach (var item in users)
                {
                    if (item.umno != senderNo)
                    {
                        SendUserMessage(senderNo, item.umno, codeNo, textTitle, textContent);
                    }
                }
            }
        }
    }

    public void SendUserMessage(string senderNo, string receiveNo, string codeNo, string textTitle, string textContent)
    {
        using (tblUser user = new tblUser())
        {
            if (senderNo != receiveNo)
            {
                UserMessage data = new UserMessage();
                data.sender_no = senderNo;
                data.sender_name = user.GetUserName(senderNo);
                data.receive_no = receiveNo;
                data.receive_name = user.GetUserName(receiveNo);
                data.code_no = codeNo;
                data.date_sender = DateTime.Now;
                data.is_read = false;
                data.text_title = textTitle;
                data.text_content = textContent;
                data.remark = "";

                repoUserMessage.Create(data);
                repoUserMessage.SaveChanges();
            }
        }
    }
}