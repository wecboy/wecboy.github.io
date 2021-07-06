using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bug_tracker.Models;
using DevStudio.RandomData;

namespace AppDemo
{
    public class DemoData : BaseClass
    {
        public List<users> UserDemoList(bool addUser)
        {
            using (Cryptographys cryp = new Cryptographys())
            {
                int int_count = 21;
                int int_start = 0;
                List<users> datas = new List<users>();
                RandomNumber rno = new RandomNumber(int_count, 1, int_count, 3, true, true);
                RandomName rname = new RandomName(int_count, NameGenderType.All);
                RandomPhone rphone = new RandomPhone(int_count);
                RandomAddress raddress = new RandomAddress(int_count);
                RandomDate rdate = new RandomDate(int_count, DateTime.Today.AddYears(-3), DateTime.Today);

                if (!addUser) int_start = 20;
                for (int i = int_start; i < int_count; i++)
                {
                    datas.Add(
                        new users()
                        {
                            urole = (i == (int_count - 1)) ? "A" : "U",
                            umno = (i == (int_count - 1)) ? "admin" : rno.DataNumberList[i],
                            uname = (i == (int_count - 1)) ? "管理者" : rname.DataChineseNameList[i],
                            upassword = cryp.SHA256Encode((i == (int_count - 1)) ? "admin" : rno.DataNumberList[i]),
                            uemail = rname.DataEmailList[i],
                            uphone = rphone.DataTelephoneList[i],
                            //adr_contact = raddress.DataAddressList[i],
                            isvarify = true,
                            uinit_time = rdate.DataDateList[i],
                            varify_code = Guid.NewGuid().ToString().ToUpper().Replace("-", ""),
                            remark = ""
                        });
                }
                return datas;
            }
        }
    }
}