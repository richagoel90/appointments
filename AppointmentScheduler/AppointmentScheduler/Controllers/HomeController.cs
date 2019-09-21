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
                catch(Exception e)
                {
                    output = false;
                }
                if (output)
                {
                    UserInfo userModel = new UserInfo()
                    {
                        FirstName = resultuser.FirstName,
                        LastName = resultuser.LastName,
                        EmailID = resultuser.EmailID,
                        PhoneNumber = resultuser.PhoneNumber,
                        UserName = resultuser.UserName,
                        Password = resultuser.Password
                    };

                    ////HttpCookie cookie = new HttpCookie("Login");
                    //cookie["UserName"] = user.UserName;
                    //cookie["FirstName"] = user.FirstName;
                    //cookie.Expires.Add(new TimeSpan(1, 0, 0));
                    //Response.Cookies.Add(cookie);
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
                    ModelState.AddModelError(string.Empty,"Password has not been changed");
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
                List<string> UserList = new List<string>();
                DatabaseConnection dbConnect = new DatabaseConnection();

                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        //[HttpGet]
        //public ActionResult GoogleOAuth()
        //{
        //    GoogleOAuthAPICall gAuth = new GoogleOAuthAPICall();
        //    gAuth.code = Request.QueryString["code"];
        //    return V);
        //}
        //[HttpPost]
        //public ActionResult GoogleOAuth()
        //{
        //    return RedirectToAction("Login");
        //}

        private UserInfo GetLoggedInUser()
        {
            return Session["User"] as UserInfo;
        }
    }
}
/*List<UserData> Result = new List<UserData>();
            string Command = "SELECT * from Users";
            using (SqlConnection mConnection = new SqlConnection(connectionString))
            {
                mConnection.Open();
                using (SqlCommand cmd = new SqlCommand(Command, mConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserData user = new UserData();
                            user.UserId = (long)reader["UserId"];
                            user.UserName = (string)reader["UserName"];
                            user.FirstName = (string)reader["FirstName"];
                            Result.Add(user);
                        }
                    }
                }
            }
            return View("AllUsers", new AllUsers() { Users = Result });
            */
