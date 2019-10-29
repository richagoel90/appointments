using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using AppointmentDatabase;
using AppointmentScheduler.Models;
namespace AppointmentScheduler.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {       
        [HttpGet]
        public ActionResult Index()
        {
            UserInfo user = GetLoggedInUser();
            if (user != null)
            {
                return RedirectToAction("UserAccount");
            }
            ViewBag.Message = "Your Appointment page.";
            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            UserInfo user = GetLoggedInUser();
            if (user != null)
            {
                return RedirectToAction("UserAccount");
            }
            ViewBag.Message = "Your Registration page.";
            return View();
        }

        [HttpPost]
        public ActionResult Registration(UserInfo user)
        {
            if (ModelState.IsValid)
            {
                var hash = Crypto.HashPassword(user.Password);

                DbUsers dbuser = new DbUsers();
                dbuser.FirstName = user.FirstName;
                dbuser.LastName = user.LastName;
                dbuser.EmailID = user.EmailID;
                dbuser.Password = hash;
                dbuser.PhoneNumber = user.PhoneNumber;
                dbuser.UserName = user.UserName;


                DatabaseConnection dbconnect = new DatabaseConnection();

                ReturnCode.result result = dbconnect.UserRegistration(dbuser);
                

                if (result.Equals(ReturnCode.result.fail))
                {
                    ModelState.AddModelError("CustomError", "Registration not Successful.Please try later");                    
                    return View();
                }
                else if (result.Equals(ReturnCode.result.userexist))
                {
                    ModelState.AddModelError("CustomError","EmailID/UserName already exist");
                    return View();
                }
                else if(result.Equals(ReturnCode.result.success))
                {
                    ViewBag.Message = "Registration successful.Please login";
                    return RedirectToAction("Login");
                }
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Login()
        {
            UserInfo user = GetLoggedInUser();
            if (user != null)
            {
                return RedirectToAction("UserAccount");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoggedinInfo user)
        {
            DbUsers dbuser = new DbUsers();
            dbuser.UserName = user.UserName;
            if(ModelState.IsValid)
            {
                bool output = false;
                DatabaseConnection dbconnect = new DatabaseConnection();
                DbUsers resultuser = dbconnect.UserLogin(dbuser);
                try
                {
                    output = Crypto.VerifyHashedPassword(resultuser.Password, user.Password);
                }
                catch
                {
                    output = false;
                }
                if (output)
                {
                    UserInfo userModel = new UserInfo()
                    {
                        UserId = resultuser.UserId,
                        FirstName = resultuser.FirstName,
                        LastName = resultuser.LastName,
                        EmailID = resultuser.EmailID,
                        PhoneNumber = resultuser.PhoneNumber,
                        UserName = resultuser.UserName,
                        Password = resultuser.Password
                    };
                    Session["User"] = userModel;
                    return RedirectToAction("UserAccount");
                }
                else
                {
                    ModelState.AddModelError("CustomError", "Incorrect Username and password");
                    return View();
                }
            }
            return View();
            
        }
  
        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword(LoggedinInfo user)
        {
            var hash = Crypto.HashPassword(user.Password);
            DbUsers dbuser = new DbUsers();
            dbuser.UserName = user.UserName;
            dbuser.Password = hash;
            if (ModelState.IsValid)
            {
                DatabaseConnection dbconnect = new DatabaseConnection();
                ReturnCode.result result = dbconnect.changePassword(dbuser);
                if (result.Equals(ReturnCode.result.success))
                {
                    TempData["Message"] = "Password has been changed.Please Login";
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("CustomError","Password has not been changed");
                    return View();
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Remove("User");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult RequestAppointment()
        {
            UserInfo user = GetLoggedInUser();
            if (user != null)
            {
                DatabaseConnection dbConnect = new DatabaseConnection();
                List<string> UserList = dbConnect.getUsersList();
                UserList.Remove(String.Concat(user.FirstName, ',', user.LastName));
                return View("RequestAppointment",UserList);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult UserAccount()
        {
            UserInfo user = GetLoggedInUser();
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult CurrentAppointment()
        {
            UserInfo user = GetLoggedInUser();
            if (user != null)
            {
                DatabaseConnection dbconnect = new DatabaseConnection();
                List<DbAppointmentInfo> result = dbconnect.CurrentAppointment(user.UserId);
                AllAppointments allAppoint = new AllAppointments();

                foreach (var value in result)
                {
                    
                    allAppoint.Appointment.Add(new AppointmentInfo
                    {
                        HostUser = value.HostUser,
                        GuestUser = value.GuestUser,
                        DatenTime = value.DatenTime,
                        Subject = value.Subject
                    });
                }
                
                return View("CurrentAppointment",allAppoint);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        private UserInfo GetLoggedInUser()
        {
            return Session["User"] as UserInfo;
        }
    }
}