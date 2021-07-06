using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bug_tracker.Models
{
    public class LoginViewModel
    {

        [Display(Name = "登入帳號")]
        [Required(ErrorMessage = "登入帳號不可空白!!")]
        [StringLength(8, ErrorMessage = "登入帳號長度不可超過8個字!!")]
        public string UserNo { get; set; }
        [Display(Name = "登入密碼")]
        [Required(ErrorMessage = "登入密碼不可空白!!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}