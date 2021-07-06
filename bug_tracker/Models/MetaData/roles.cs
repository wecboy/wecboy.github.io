using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace bug_tracker.Models
{
    [MetadataType(typeof(rolesMetaData))]
    public partial class roles
    {
        private class rolesMetaData
        {
            [Key]
            public int rowid { get; set; }
            [Display(Name = "編號")]
            public string rid { get; set; }
            [Display(Name = "角色編號")]
            [Required(ErrorMessage = "角色代號不可空白!!")]
            public string rrule { get; set; }
            [Display(Name = "角色名稱")]
            [Required(ErrorMessage = "角色名稱不可空白!!")]
            public string rname { get; set; }
            [Display(Name = "備註")]
            public string rremark { get; set; }
        }
    }
}
