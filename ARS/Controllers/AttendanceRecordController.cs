using ARS.Common;
using ARS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ARS.Controllers
{
    public class AttendanceRecordController : Controller
    {
        //
        // GET: /AttendanceRecord/

        public ActionResult Index()
        {
            Employee user = (Employee)Session["user"];

            ViewBag.truename = user.truename;

            var context = DbHelper.getInstance().createDbContext();
            var attendanceRecords = context.GetTable<AttendanceRecord>();

            var query = from ar in attendanceRecords
                        where ar.user_id == user.id.Value
                        orderby ar.sign_time descending
                        select ar;

            //查找当天的签到记录
            var query2 = from ar2 in attendanceRecords
                        where ar2.user_id == user.id.Value &&
                        ar2.sign_time > DateTime.Now.Date &&
                        ar2.sign_time < DateTime.Now.AddDays(1).Date &&
                        ar2.type == 1
                        select ar2;

            ViewBag.isSignIn = (query2.Count() > 0);

            //查找当天的签退记录
            var query3 = from ar3 in attendanceRecords
                         where ar3.user_id == user.id.Value &&
                         ar3.sign_time > DateTime.Now.Date &&
                         ar3.sign_time < DateTime.Now.AddDays(1).Date &&
                         ar3.type == 2
                         select ar3;

            ViewBag.isSignOut = (query3.Count() > 0);







            return View(query.ToList());
        }

        //
        // GET: /AttendanceRecord/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /AttendanceRecord/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AttendanceRecord/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /AttendanceRecord/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /AttendanceRecord/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /AttendanceRecord/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /AttendanceRecord/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult SignIn()
        {
            //未登录用户，不允许签到
            if (Session["user"] == null)
                return RedirectToAction("Index","Home");

            var context = DbHelper.getInstance().createDbContext();

            var attendanceRecords = context.GetTable<AttendanceRecord>();

            //已存在签到记录，不允许签到
            var query = from ar in attendanceRecords
                        where ar.user_id == ((Employee)Session["user"]).id.Value &&
                        ar.sign_time > DateTime.Now.Date &&
                        ar.sign_time < DateTime.Now.AddDays(1).Date &&
                        ar.type == 1
                        select ar;
            if (query.Count() > 0)
            {
                TempData["SignInSuccess"] = false;
            }
            else
            {
                TempData["SignInSuccess"] = true;
                var signinRecord = new AttendanceRecord() { id = null, user_id = ((Employee)Session["user"]).id.Value, type = 1, sign_time = DateTime.Now };

                attendanceRecords.InsertOnSubmit(signinRecord);
                context.SubmitChanges();

            }

            return RedirectToAction("Index");
        }

        public ActionResult Signout()
        {
            //未登录用户，不允许签退
            if (Session["user"] == null)
                return RedirectToAction("Index","Home");

            var context = DbHelper.getInstance().createDbContext();

            var attendanceRecords = context.GetTable<AttendanceRecord>();

            //没有签到记录，不允许签退
            var query = from ar in attendanceRecords
                        where ar.user_id == ((Employee)Session["user"]).id.Value &&
                        ar.sign_time > DateTime.Now.Date &&
                        ar.sign_time < DateTime.Now.AddDays(1).Date &&
                        ar.type == 1
                        select ar;
            if (query.Count() == 0)
            {
                TempData["SignOutSuccess"] = false;
            }
            else
            {
                TempData["SignOutSuccess"] = true;
                var signoutRecord = new AttendanceRecord() { id = null, user_id = ((Employee)Session["user"]).id.Value, type = 2, sign_time = DateTime.Now };

                attendanceRecords.InsertOnSubmit(signoutRecord);
                context.SubmitChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
