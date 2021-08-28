using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Web.Models
{
    public class CarViewModel
    {
        public int Id { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string PathImg { get; set; }
    }
    public class SearchCarIndexModel
    {
        public string searchAttribute { get; set; }
    }

    public class CarCreateViewModel
    {
        [Display(Name ="Марка")]
        public string Mark { get; set; }
        [Display(Name ="Модель")]
        public string Model { get; set; }
        [Display(Name = "Фото")]
        public IFormFile Image { get; set; }
        [Display(Name ="Рік")]
        public int Year { get; set; }
    }
    public class CarDeleteViewModel
    {
        [Display(Name = "ID машини в списку")]
        public int Id { get; set; }
        [Display(Name = "Марка")]
        public string Mark { get; set; }
        [Display(Name = "Модель")]
        public string Model { get; set; }
        [Display(Name = "Рік")]
        public int Year { get; set; }
    }
    public class CarChangeViewModel
    {
        [Display(Name = "ID машини в списку")]
        public int Id { get; set; }
        [Display(Name = "Фото")]
        public IFormFile NewImage { get; set; }
        [Display(Name = "Нова Марка")]
        public string Mark_For_Add { get; set; }
        [Display(Name = "Нова Модель")]
        public string Model_For_Add { get; set; }
        [Display(Name = " Новий Рік випуску для машини")]
        public int Year_For_Add { get; set; }
    }
}
