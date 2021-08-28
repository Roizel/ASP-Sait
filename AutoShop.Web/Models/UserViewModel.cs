using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Web.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string username { get; set; }
        public string userpassword { get; set; }
        public string useremail { get; set; }
    }
    public class UserCreateViewModel
    {
        [Required(ErrorMessage = "Не вказаний логін")]
        [Display(Name = "Логін")]
        public string username { get; set; }
        [Required(ErrorMessage = "Не вказаний пароль")]
        [Display(Name = "Пароль")]
        public string userpassword { get; set; }
        [Required(ErrorMessage = "Не вказаний Email")]
        [Display(Name = "Електронна пошта")]
        public string useremail { get; set; }
    }
    public class UserAuthorizationViewModel
    {
        [Required]
        [Display(Name = "Логін")]
        public string username { get; set; }
        [Required]
        [Display(Name = "Пароль")]
        public string userpassword { get; set; }
        public bool IsAuth { get; set; }
        //[Required]
        //[Display(Name = "Електронна пошта")]
        //public string useremail { get; set; }
    }
    public class ProfilePageViewModel
    {
        [Display(Name = "Логін")]
        public string username { get; set; }
        [Display(Name = "Пароль")]
        public string userpassword { get; set; }
        [Display(Name = "Електронна пошта")]
        public string useremail { get; set; }
    }
}
