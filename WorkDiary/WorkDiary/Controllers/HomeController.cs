using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using WorkDiary.Models;

namespace WorkDiary.Controllers
{
    public class HomeController : Controller
    {
        private readonly DiaryContext db;

        public HomeController(DiaryContext context)
        {
            db = context;
        }

        private User CurUser => GetUserById(int.Parse(Request.Cookies["user"]));

        private string HashToHex(byte[] bytes, bool upperCase)
        {
            var result = new StringBuilder(bytes.Length * 2);

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
            switch (CurUser.AccessLevel)
            {
                case 0: return View("UserInfo", CurUser);
                case 1:
                case 2: return UserList(db.Users);
                default: return RedirectToAction("Logout");
            }
        }

        public IActionResult UserList(IEnumerable<User> users)
        {
            if (CurUser.AccessLevel > 0)
                return View("AllUsers", users);
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
            if (email == null || passHash == null) return View();

            HashAlgorithm hashAlg = SHA256.Create();
            var hash = hashAlg.ComputeHash(passHash.Select(c => (byte) c).ToArray());
            passHash = HashToHex(hash, true);
            var user = db.Users.First(u => u.Email == email);
            if (user.PassHash == passHash)
            {
                Response.Cookies.Append("user", user.Id.ToString());
                AddLog("Logged In", user.Id);
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult EditUser(int id)
        {
            ViewBag.AccessLevel = CurUser.AccessLevel;
            if (CurUser.AccessLevel > 0)
                return View(GetUserById(id));
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