using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppointmentScheduler.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }
        //[HttpGet]
        //public ActionResult GoogleOAuth()
        //{
        //    GoogleOAuthAPICall gAuth = new GoogleOAuthAPICall();
        //    gAuth.code = Request.QueryString["code"];
        //    return R;
        //}
        //[HttpPost]
        //public ActionResult GoogleOAuth()
        //{
        //    return RedirectToAction("Login");
        //}

    }
}
