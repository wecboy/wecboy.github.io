using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bug_tracker.Models
{

    [MetadataType(typeof(usersMetaData))]
    public partial class users
    {
        private class usersMetaData
        {
            [Key]
            public int rowid { get; set; }
            public string uid { get; set; }
            [Display(Name = "帳號")]
            public string umno { get; set; }
            [Display(Name = "名稱")]
            public string uname { get; set; }
            [Display(Name = "角色")]
            public string urole { get; set; }
            [Display(Name = "密碼")]
            [DataType(DataType.Password)]
            public string upassword { get; set; }
            [Display(Name = "電話")]
            public string uphone { get; set; }
            [Display(Name = "電子信箱")]
            [EmailAddress(ErrorMessage = "電子信箱格式錯誤!!")]
            public string uemail { get; set; }
            [Display(Name = "出生日期")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
            public Nullable<System.DateTime> ubirthday { get; set; }
            [Display(Name = "是否審核")]
            public Nullable<bool> isvarify { get; set; }
            [Display(Name = "備註")]
            public string remark { get; set; }
            public string varify_code { get; set; }
            [Display(Name = "建立日期")]
            public Nullable<System.DateTime> uinit_time { get; set; }
            [Display(Name = "修改日期")]
            public Nullable<System.DateTime> umodified_time { get; set; }           
        }
    }

}