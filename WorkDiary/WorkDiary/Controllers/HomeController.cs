using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkDiary.Models;

namespace WorkDiary.Controllers
{
    public class HomeController : Controller
    {
        private readonly DiaryContext db;
        private User CurUser => GetUserById(int.Parse(Request.Cookies["user"]));
        public HomeController(DiaryContext context)
        {
            db = context;
        }

        private string HashToHex(byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);

            foreach (var t in bytes)
                result.Append(t.ToString(upperCase ? "X2" : "x2"));

            return result.ToString();
        }

        private void AddLog(string message, int? usrId = null)
        {
            db.Logs.Add(new Log(usrId, message));
        }
        private User GetUserById(int id)
        {
            return db.Find(typeof(User), id) as User;
        }
        private void DeleteUserById(int id)
        {
            db.Remove(GetUserById(id));
        }

        public IActionResult Index()
        {
            if (!Request.Cookies.ContainsKey("user"))
                return RedirectToAction("Login");
            else
            {
                
                switch (CurUser.AccessLevel)
                {
                    case 0: return View("UserInfo", CurUser);
                    case 1:
                    case 2: return UserList(db.Users);
                    default: return RedirectToAction("Logout");
                }
            }
        }

        public IActionResult UserList(IEnumerable<User> users)
        {
            if (CurUser.AccessLevel > 0)
                return View("AllUsers", users);
            else
                return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string passHash)
        {
            if (email == null || passHash == null)
                return View();
            else
            {
                System.Security.Cryptography.HashAlgorithm hashAlg = System.Security.Cryptography.SHA256.Create();
                byte[] hash = hashAlg.ComputeHash(passHash.Select(c => (byte)c).ToArray());
                passHash = HashToHex(hash, true);
                User user = db.Users.First(u => u.Email == email);
                if (user.PassHash == passHash)
                {
                    Response.Cookies.Append("user", user.Id.ToString());
                    AddLog("Logged In", user.Id);
                    return RedirectToAction("Index");
                }
                else
                    return View();
            }
        }

        public IActionResult EditUser(int id)
        {
            ViewBag.AccessLevel = CurUser.AccessLevel;
            if (CurUser.AccessLevel > 0)
                return View(GetUserById(id));
            else
                return RedirectToAction("Index");
        }

        public IActionResult DeleteUser(int id)
        {
            if (CurUser.AccessLevel > 1 && int.Parse(Request.Cookies["user"]) != id)
                DeleteUserById(id);
            return RedirectToAction("Index");
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("user");
            return RedirectToAction("Index");
        }
    }
}