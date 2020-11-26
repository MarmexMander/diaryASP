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
        User GetUserById(int id)
        {
            return db.Find(typeof(Models.User), id) as User;
        }
        public IActionResult Index()
        {
            if (!Request.Cookies.ContainsKey("user"))
                return RedirectToAction("Login");
            else 
            {
                User user = GetUserById(int.Parse(Request.Cookies["user"]));
                switch (user.AccessLevel)
                {
                    case 0: return View("UserInfo", user); break;
                    case 1: throw new Exception("Has not moder gui view"); break;//return View(...);
                    case 2: throw new Exception("Has not admin gui view"); break;//return View(...);
                    default : return RedirectToAction("Logout");
                }
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
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
