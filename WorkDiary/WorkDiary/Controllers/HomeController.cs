using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
        private string HashToHex(byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);

            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));

            return result.ToString();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            ViewBag.ProductsId = id;
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Email, string PassHash)
        {
            if (Email == null || PassHash == null)
                return View();
            else
            {
                System.Security.Cryptography.HashAlgorithm hashAlg = System.Security.Cryptography.SHA256.Create();
                byte[] hash = hashAlg.ComputeHash(PassHash.Select(c => (byte)c).ToArray());
                PassHash = HashToHex(hash, false);
                User user = db.Users.Where(u=>u.Email == Email).First();
                if (user.PassHash == PassHash)
                {
                    Response.Cookies.Append("user", user.Id.ToString());
                    return RedirectToAction("Index");
                }
                else
                    return View();
            }
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("user");
            return RedirectToAction("Index");
        }
    }
}
