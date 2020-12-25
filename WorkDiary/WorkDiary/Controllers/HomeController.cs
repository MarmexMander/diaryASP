using System;
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
            var users = db.Users;
            var positions = db.Positions.ToList();
            foreach (var dbUser in users)
            {
                int id = dbUser.PositionId;
                var pos = positions.Find(p => p.PositionId == id);
                dbUser.Position = pos;
            }

            db.Users = users;
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
            db.Logs.Add(new Log(message, GetUserById(usrId)));
            db.SaveChanges();
        }

        private User GetUserById(int? id)
        {
            if (id == null)
                return null;
            return db.Find(typeof(User), id) as User;
        }

        private double GetUserWage(User user)
        {
            double wage = 0;
            List<Log> userLogs = db.Logs.Where(
                l =>
                l.Date.Month == DateTime.Now.Month &&
                l.User.Id == user.Id &&
                (
                    l.Message == Constants.LogMessages.LOG_OUT ||
                     l.Message == Constants.LogMessages.LOG_IN
                )
                ).ToList();
            for (var i = 0; i < userLogs.Count; i++)
            {
                TimeSpan daySpan;
                if (userLogs[i].Message == Constants.LogMessages.LOG_OUT)
                    continue;
                daySpan = userLogs[i].Date - userLogs[i + 1].Date;
                if (userLogs[i + 1].Message == Constants.LogMessages.LOG_OUT)
                {
                    daySpan = userLogs[i].Date - userLogs[i + 1].Date;
                    i++;
                }
                else
                    daySpan = TimeSpan.FromHours(8);

                wage += user.Position.Wage * daySpan.TotalHours;
            }

            return wage;
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
                case 0: return UserInfo(CurUser.Id);
                case 1:
                case 2: return UserList(db.Users);
                default: return RedirectToAction("Logout");
            }
        }

        public IActionResult UserList(IEnumerable<User> users)
        {

            ViewBag.AccessLevel = CurUser.AccessLevel;
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
            var hash = hashAlg.ComputeHash(passHash.Select(c => (byte)c).ToArray());
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
            ViewBag.Positions = db.Positions.ToList();
            ViewBag.AccessLevel = CurUser.AccessLevel;
            if (CurUser.AccessLevel > 0)
            {
                var user = GetUserById(id);
                user.PassHash = "";
                return View(new UserViewModel() { Model = user, Positions = db.Positions.ToList() });
            }

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
            AddLog("Logged Out", CurUser.Id);
            return RedirectToAction("Index");
        }

        public IActionResult UserInfo(int userId)//BUG: Info didn`t showing
        {
            User user = db.Users.Find(userId);
            ViewBag.UserWage = GetUserWage(user);
            return View(user);
        }

        [HttpPost]
        public IActionResult EditUser(UserViewModel userModel)
        {

            if (userModel.Model.PassHash != "")
            {
                HashAlgorithm hashAlg = SHA256.Create();
                var hash = hashAlg.ComputeHash(userModel.Model.PassHash.Select(c => (byte)c).ToArray());
                userModel.Model.PassHash = HashToHex(hash, true);
            }

            userModel.Model.Position = db.Positions.Find(userModel.Model.Position.PositionId);
            db.Users.Update(userModel.Model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult EventList(IEnumerable<Event> events)
        {
            return View("AllEvents", events);
        }
        [HttpGet]
        public IActionResult CreateUser()
        {
            ViewBag.Positions = db.Positions.ToList();
            return View("NewUser", new UserViewModel() { Positions = db.Positions.ToList() });
        }
        [HttpPost]
        public IActionResult CreateUser(UserViewModel userModel)
        {
            ViewBag.Positions = db.Positions.ToList();
            HashAlgorithm hashAlg = SHA256.Create();
            var hash = hashAlg.ComputeHash(userModel.Model.PassHash.Select(c => (byte)c).ToArray());
            userModel.Model.PassHash = HashToHex(hash, true);
            userModel.Model.Position = db.Positions.Find(userModel.Model.Position.PositionId);
            db.Users.Add(userModel.Model);
            db.SaveChanges();
            return View("Index");
        }

        [HttpGet]
        public IActionResult CreateEvent()
        {
            return View("NewEvent");
        }

        [HttpPost]
        public IActionResult CreateEvent(Event @event)
        {
            db.Events.Add(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
};