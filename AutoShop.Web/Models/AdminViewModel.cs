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
        public UserManager<AppUser> _userManager;
        public AdminViewModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
    }
}
