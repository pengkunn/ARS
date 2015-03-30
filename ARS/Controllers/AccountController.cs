using ARS.Common;
using ARS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ARS.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult LogIn()
        {
            if (Request.IsAuthenticated)
            {
                var context = DbHelper.getInstance().createDbContext();

                var employees = context.GetTable<Employee>();

                var query = from e in employees
                            where e.username == User.Identity.Name
                            select e;

                Session["user"] = query.Single();

                return RedirectToAction("Index", "AttendanceRecord");
            }
                

            return View();
        }

        [HttpPost]
        public ActionResult LogIn(string username, string encryptPassword, bool rememberMe)
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

            var context = DbHelper.getInstance().createDbContext();

            var employees = context.GetTable<Employee>();

            var query = from e in employees
                        where e.username == username &&
                        e.password == encryptPassword
                        select e;

            if (query.Count() > 0)
            {
                var user = query.Single();
                FormsAuthentication.SetAuthCookie(user.username, true);

                Session["user"] = query.Single();
                //TempData["user"] = query.Single();
                return RedirectToAction("Index", "AttendanceRecord");
            }

            return View();
        }


        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn");
        }

        public ActionResult Manage(string encryptOldPassword, string encryptNewPassword, string encryptConfirmPassword)
        {
            var context = DbHelper.getInstance().createDbContext();

            var employees = context.GetTable<Employee>();

            var query = from e in employees
                        where e.username == User.Identity.Name
                        select e;

            var user = query.Single();

            if (user.password != encryptOldPassword)
            {
                //ViewBag.Success = false;
                ViewBag.StatusMessage = "当前密码不正确";
                return View();
            }


            if (encryptNewPassword != encryptConfirmPassword)
            {
               // ViewBag.Success = false;
                ViewBag.StatusMessage = "两次输入密码不一致";
                return View();
            }

            user.password = encryptNewPassword;
            //employees.Attach(user);
            context.SubmitChanges();
            ViewBag.StatusMessage = "修改成功";
            return View();
        }
    }
}
