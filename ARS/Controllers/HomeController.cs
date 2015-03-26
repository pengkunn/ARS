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
                       where e.username == "test1"
                       select e;

            //Employee newEmployee = new Employee() { id = null, password = "1", truename = "test4", username = "test4", mac_address = ip };

            //employees.InsertOnSubmit(newEmployee);

            TempData["user"] = query.Single();
            //ViewBag.employee = query.Single();


            return View();
        }

        public ActionResult Signin()
        {
            if (TempData["user"] == null)
                return View();

            var context = DbHelper.getInstance().getDbContext();

            var attendanceRecords = context.GetTable<AttendanceRecord>();

            var ar = new AttendanceRecord() { id = null, user_id = ((Employee)TempData["user"]).id.Value, type = 1, sign_time = DateTime.Now};

            attendanceRecords.InsertOnSubmit(ar);
            context.SubmitChanges();

            return View();
        }

    }
}
