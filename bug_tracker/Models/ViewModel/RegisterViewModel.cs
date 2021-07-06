using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bug_tracker.Models
{
    public class RegisterViewModel
    {
        [Display(Name = "登入帳號")]
        [Required(ErrorMessage = "登入帳號不可空白!!")]
        public string UserNo { get; set; }
        [Display(Name = "帳號名稱")]
        [Required(ErrorMessage = "帳號名稱不可空白!!")]
        public string UserName { get; set; }
        [Display(Name = "登入密碼")]
        [Required(ErrorMessage = "登入密碼不可空白!!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "電話")]
        [Required(ErrorMessage = "電話不可空白!!")]
        public string UserPhone { get; set; }
        [Display(Name = "電子信箱")]
        [EmailAddress(ErrorMessage = "電子信箱格式錯誤!!")]
        public string UserEmail { get; set; }
        [Display(Name = "出生日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public string BirthDay { get; set; }
    }
}