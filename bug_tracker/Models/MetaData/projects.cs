using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bug_tracker.Models
{
    [MetadataType(typeof(projectsMetaData))]
    public partial class projects
         
    {

        private class projectsMetaData
        {
            [Key]
            public int rowid { get; set; }
            [Display(Name = "專案編號")]
            public string pid { get; set; }
            [Display(Name = "專案名稱")]
            public string pname { get; set; }
            [Display(Name = "專案描述")]
            [DataType(DataType.MultilineText)]
            public string pdescription { get; set; }
            [Display(Name = "專案管理員")]
            public string pmanager_id { get; set; }
            [Display(Name = "專案建立時間")]
            public Nullable<System.DateTime> pinit_time { get; set; }
            [Display(Name = "專案修改時間")]
            public Nullable<System.DateTime> pmodified_time { get; set; }
    }
}
}