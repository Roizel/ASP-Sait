using AutoShop.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Web.Models
{
    public class AdminViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Required")]
        [EmailAddress(ErrorMessage = "Required")]
        public string Email { get; set; }
    }
    public class CreateUserViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Required")]
        [EmailAddress(ErrorMessage = "Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }
    }
    public class EditUserViewModel
    {
        [Required(ErrorMessage = "Required")]
        public string Id { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Required")]
        [EmailAddress(ErrorMessage = "Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }
    }
    public class DeleteUserViewModel
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "Required")]
        public string Id { get; set; }
    }
}
