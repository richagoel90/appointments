using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using AppointmentDatabase;

namespace AppointmentScheduler.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            // return view
            return View();

        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Appointment()
        {
            ViewBag.Message = "Your Appointment page.";

            return View();
        }
        public ActionResult Registration()
        {
            ViewBag.Message = "Your Registration page.";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(Models.Users user)
        {
            var hash = Crypto.HashPassword(user.Password);
            if (ModelState.IsValid)
            {
                DbUsers dbuser = new DbUsers();
                dbuser.FirstName = user.FirstName;
                dbuser.LastName = user.LastName;
                dbuser.EmailID = user.EmailID;
                dbuser.Password = hash;
                dbuser.PhoneNumber = user.PhoneNumber;
                dbuser.UserName = user.UserName;


                DatabaseConnection dbconnect = new DatabaseConnection();

                int result = dbconnect.UserRegistration(dbuser);

                if (result == -1)
                {
                    ViewBag.Message = "Registration not Successful.Please try later";
                    return View();
                }
                else if (result == 400)
                {
                    ViewBag.Message = "EmailID already exist";
                    return View();
                }
                else
                {
                    ViewBag.Message = "Registration Successful.Please login";
                    return View("Login");
                }


            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Login(Models.Users user)
        {
            if (ModelState.IsValid)
            {
                DbUsers dbuser = new DbUsers();
                dbuser.UserName = user.UserName;
                dbuser.Password = user.Password;

                DatabaseConnection dbconnect = new DatabaseConnection();
                DbUsers result = dbconnect.UserLogin(dbuser);

                if (String.IsNullOrEmpty(result.FirstName))
                {
                    ViewBag.Message = "Incorrect Username and password";
                    return View();
                }
                else
                {
                    user.FirstName = result.FirstName;
                    user.LastName = result.LastName;
                    user.EmailID = result.EmailID;
                    user.PhoneNumber = result.PhoneNumber;
                    return View("UserAccount", user);
                }

            }
            else
                return View();

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
