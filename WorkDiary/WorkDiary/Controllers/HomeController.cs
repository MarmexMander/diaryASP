using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkDiary.Models;

namespace WorkDiary.Controllers
{
    public class HomeController : Controller
    {
        DiaryContext db;
        public HomeController(DiaryContext context)
        {
            db = context;
        }

        //public IActionResult Index()
        //{
        //    return View(db.Product.ToList());
        //}

        //[HttpGet]
        //public IActionResult Buy(int? id)
        //{
        //    if (id == null) return RedirectToAction("Index");
        //    ViewBag.ProductsId = id;
        //    return View();
        //}

        //[HttpPost]
        //public string Buy(Order order)
        //{
        //    db.Orders.Add(order);
        //    // сохраняем в бд все изменения
        //    db.SaveChanges();
        //    return "Спасибо, " + order.User + ", за покупку!";
        //}

    }
}
