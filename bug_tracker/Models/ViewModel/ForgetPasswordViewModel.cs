using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bug_tracker.Models
{
    public class ForgetPasswordViewModel
    {
        [Display(Name = "請輸入電子信箱")]
        [Required(ErrorMessage = "電子郵件不可空白")]
        [EmailAddress(ErrorMessage = "電子信箱格式錯誤!!")]
        public string UserEmail { get; set; }
    }
}