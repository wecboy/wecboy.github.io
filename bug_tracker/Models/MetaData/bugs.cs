using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bug_tracker.Models
{
    [MetadataType(typeof(bugsMetaData))]
    public partial class bugs

    {
        private class bugsMetaData
        {
            [Key]
            public int rowid { get; set; }
            [Display(Name = "缺陷編號")]
            public string bid { get; set; }
            [Display(Name = "缺陷簡述")]
            //[Required(ErrorMessage = "缺陷簡述不可空白!!")]
            public string bsummary { get; set; }
            [Display(Name = "回報者")]
            //[Required(ErrorMessage = "回報者不可空白!!")]
            public string bcreator { get; set; }
            [Display(Name = "狀態")]
            public string bstatus_id { get; set; }
            [Display(Name = "優先權")]
            public string bpriority_id { get; set; }
            [Display(Name = "缺陷建立日期")]
            public Nullable<System.DateTime> binit_time { get; set; }
            [Display(Name = "缺陷修改日期")]
            public Nullable<System.DateTime> bmodified_time { get; set; }
            [Display(Name = "專案名稱")]
            public string bpid { get; set; }
            [Display(Name = "委派給")]
            public string basignee { get; set; }
            [Display(Name = "缺陷描述")]
            [DataType(DataType.MultilineText)]
            public string bdescription { get; set; }
        }
    }
}