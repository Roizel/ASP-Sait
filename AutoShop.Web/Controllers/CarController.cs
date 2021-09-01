using AutoMapper;
using AutoShop.Domain;
using AutoShop.Domain.Entities;
using AutoShop.Web.Models;
using Bogus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutoShop.Web.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly AppEFContext _context; /*Тут данні з БД чі для БД*/
        private Car car = new Car(); /*Просто екз машини*/
        //використовуємо авто мепер
        private readonly IMapper _mapper;

        public CarController(AppEFContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            //GenerateAuto(); /*НЕ включать якщо не хочеш більше машин)*/
        }

        //private void GenerateAuto()
        //{
        //    var endDate = DateTime.Now;
        //    var startDate = new DateTime(endDate.Year - 10,
        //        endDate.Month, endDate.Day);
        //    //використовуємо богус для генерації
        //    var faker = new Faker<Car>("uk")
        //        .RuleFor(x => x.Mark, f => f.Vehicle.Manufacturer())
        //        .RuleFor(x => x.Model, f => f.Vehicle.Model())
        //        .RuleFor(x => x.Year, f => f.Date.Between(startDate, endDate).Year);
        //    int n = 1000;
        //    for (int i = 0; i < n; i++)
        //    {
        //        var car = faker.Generate();
        //        _context.Cars.Add(car);
        //        _context.SaveChanges();
        //    }

        //}
        public IActionResult Index(SearchCarIndexModel search, int page = 1)
        {
            string markofcar = ""; /*From Search(On Sait)*/
            string modelofcar = ""; /*From Search(On Sait)*/
            string yearofcar = ""; /*From Search(On Sait)*/

            int showItems = 10;
            var query = _context.Cars.AsQueryable();
            if (!string.IsNullOrEmpty(search.searchAttribute))
            {
                string s = search.searchAttribute;
                var words = s.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries); /*Check on comma(кома)*/
                int size = words.Length;
                if (size > 0)
                {
                    if (!string.IsNullOrEmpty(words[0]))
                    {
                        markofcar = words[0];
                        query = query.Where(
                        x => x.Mark.ToLower().Contains(markofcar.ToLower())
                        );
                    }
                }
                if (size > 1)
                {
                    if (!string.IsNullOrEmpty(words[1]))
                    {
                        modelofcar = words[1];
                        query = query.Where(
                        x => x.Model.ToLower().Contains(modelofcar.ToLower())
                        );
                    }
                }
                if (size > 2)
                {
                    if (!string.IsNullOrEmpty(words[2]))
                    {
                        yearofcar = words[2];
                        query = query.Where(
                        x => x.Year.ToString().Contains(yearofcar)
                        );
                    }
                }
                //query = query.Where(
                //    x => x.Mark.ToLower().Contains(markofcar.ToLower()) &&
                //    x.Model.ToLower().Contains(modelofcar.ToLower()) &&
                //    x.Year.ToString().Contains(yearofcar)
                //    );
            }
          
            //кількість записів, що є в БД
            int countItems = query.Count();
            //12 штук, на 1 сторінці 3 записа, скільки буде сторінок
            //13/3 = 4,0 - 5 сторінок
            var pageCount = (int)Math.Ceiling(countItems / (double)showItems);

            if (pageCount == 0) pageCount = 1;

            if (page > pageCount)
            {
                return RedirectToAction(nameof(this.Index), new { page = pageCount });
            }
            int skipItems = (page - 1) * showItems;
            query = query.Skip(skipItems).Take(showItems);

            /*Search*/
            HomeIndexViewModel model = new HomeIndexViewModel();
            model.Cars = query
                .Select(x => _mapper.Map<CarViewModel>(x))
                .ToList();
            model.Page = page;
            model.PageCount = pageCount;
            model.Search = search;

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Change()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Change(CarChangeViewModel carChange)
        {
            if (!ModelState.IsValid)
                return View(carChange);
            try
            {
                Car NewCar = new Car()
                {
                    Id = carChange.Id,
                    Mark = carChange.Mark_For_Add,
                    Model = carChange.Model_For_Add, /*Записуємо в car те, що хоче змінити користувач(Видалили стару машну, добавляємо нову)*/
                    Year = carChange.Year_For_Add
                };
                if (carChange.NewImage != null)
                {
                    if (NewCar.PathImg != null)
                    {
                        var directory = Path.Combine(Directory.GetCurrentDirectory(), "images");
                        var FilePath = Path.Combine(directory, NewCar.PathImg);
                        System.IO.File.Delete(FilePath);
                    }
                    string fileName = "";
                    if (carChange.NewImage != null)
                    {
                        var ext = Path.GetExtension(carChange.NewImage.FileName);
                        fileName = Path.GetRandomFileName() + ext;
                        var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");
                        var filePath = Path.Combine(dir, fileName);
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            carChange.NewImage.CopyTo(stream);
                        }
                        NewCar.PathImg = fileName;
                    }
                }
                else
                {
                    NewCar.Mark = "loh";
                }
                if (NewCar != null) /*Перевірка, чі нормально записались данні*/
                {
                    _context.Entry(NewCar).State = EntityState.Modified; /*Кажем, що такій об'єкт в базі данних вже є і нам потрібно лише редагувати його*/
                    _context.SaveChanges(); /*Сохраняємо*/
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View(carChange);
            }
            return View(carChange);
        }
        [HttpPost]
        public IActionResult Create(CarCreateViewModel carCreateView)
        {
            if (!ModelState.IsValid)
                return View(carCreateView);

            string fileName = "";
            if (carCreateView.Image != null)
            {
                var ext = Path.GetExtension(carCreateView.Image.FileName);
                fileName = Path.GetRandomFileName() + ext;
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");
                var filePath = Path.Combine(dir, fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    carCreateView.Image.CopyTo(stream);
                }
            }

            Car car = new Car
            {
                Mark= carCreateView.Mark,
                Model= carCreateView.Model,
                Year= carCreateView.Year,
                PathImg = fileName
            };
            _context.Cars.Add(car);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(CarDeleteViewModel carDeleteView)
        {
            try
            {
                if (!ModelState.IsValid) /*Перевірка на те, що воно взагалі робе*/
                    return View(carDeleteView);

                List<CarViewModel> model = _context.Cars  /*Витягуємо данні з БД*/
                   .Select(x => new CarViewModel
                   {
                       Id = x.Id,
                       Mark = x.Mark,
                       Model = x.Model,
                       Year = x.Year
                   }).ToList();

                if (model == null) /*Перевіряєм, чі в нормально записалось з БД*/
                    return View(carDeleteView); /*Нічого не виводим, просто не нажимається кнопка*/

                if (carDeleteView != null) /*Перевірка на входящі данні*/
                {
                    var tmp = from CarViewModel in model
                              where
                              CarViewModel.Id == carDeleteView.Id
                              ||CarViewModel.Mark == carDeleteView.Mark /*Звіряємо машини по моделі року і марці  */
                              || CarViewModel.Model == carDeleteView.Model
                              || CarViewModel.Year == carDeleteView.Year
                              select CarViewModel;

                    if (tmp == null) /*Перевірка, чі нормально витягнулись данні*/
                        return View(carDeleteView);

                    foreach (CarViewModel item in tmp) /*Перебираємо все що отримали з машини і записуємо данні в нову машину*/
                    {
                        car.Id = item.Id;
                        car.Mark = item.Mark;
                        car.Model = item.Model;
                        car.Year = item.Year;
                    }
                    if (car != null) /*Перевірка, чі нормально записались данні*/
                    {
                        _context.Cars.Remove(car); /*Видаляємо*/
                        _context.SaveChanges(); /*Сохраняємо*/
                        return RedirectToAction("Index"); /*Вертаємся до списку авто*/
                    }
                    return View(carDeleteView);
                }
            }
            catch
            {

                return View(carDeleteView);
            }
            return View(carDeleteView);
        }
    }
}
