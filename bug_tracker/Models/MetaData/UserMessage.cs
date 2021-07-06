using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bug_tracker.Models
{
    [MetadataType(typeof(UserMessageMetaData))]
    public partial class UserMessage
    {
        private class UserMessageMetaData
        {
            [Key]
            public int rowid { get; set; }
            [Display(Name = "類別")]
            [Required(ErrorMessage = "類別不可空白!!")]
            public string code_no { get; set; }
            [Display(Name = "發送時間")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
            public Nullable<System.DateTime> date_sender { get; set; }
            [Display(Name = "讀取時間")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
            public Nullable<System.DateTime> date_read { get; set; }
            [Display(Name = "寄出帳號")]
            public string sender_no { get; set; }
            [Display(Name = "寄出名稱")]
            public string sender_name { get; set; }
            [Display(Name = "收訊帳號")]
            [Required(ErrorMessage = "收訊帳號不可空白!!")]
            public string receive_no { get; set; }
            [Display(Name = "收訊名稱")]
            public string receive_name { get; set; }
            [Display(Name = "已讀")]
            public bool is_read { get; set; }
            [Display(Name = "訊息標題")]
            [Required(ErrorMessage = "訊息標題不可空白!!")]
            public string text_title { get; set; }
            [Display(Name = "訊息內文")]
            [DataType(DataType.MultilineText)]
            public string text_content { get; set; }
            [Display(Name = "備註")]
            public string remark { get; set; }
        }
    }
}