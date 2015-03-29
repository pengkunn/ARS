using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SQLite;
using System.Data;
using System.Data.Linq;
using ARS.Models;
using ARS.Common;

namespace ARS.Controllers
{
    public class HomeController : Controller
    {
        public string username = "";
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string encryptPassword, bool rememberMe)
        {
            string ip;
            if (this.HttpContext.Request.ServerVariables["HTTP_VIA"] != null)
            {
                ip = this.HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else
            {
                ip = this.HttpContext.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }

            var context = DbHelper.getInstance().getDbContext();

            var employees = context.GetTable<Employee>();

            var query = from e in employees
                        where e.username == username &&
                        e.password == encryptPassword
                        select e;

            if (query.Count() > 0)
            {
                Session["user"] = query.Single();
                //TempData["user"] = query.Single();
                return RedirectToAction("Index", "AttendanceRecord");
            }

            return View();
        }



    }
}
