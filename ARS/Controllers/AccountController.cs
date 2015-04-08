using ARS.Common;
using ARS.Models;
using MySql.Data.MySqlClient;
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
                //var context = DbHelper.getInstance().createDbContext();

                //var employees = context.GetTable<Employee>();
                var db = new ARSEntitis();
                var query = from e in db.Employees
                            where e.username == User.Identity.Name
                            select e;

                Session["user"] = query.Single();

                return RedirectToAction("Index", "AttendanceRecord");
            }



            //.Departments.Add(dep);
            //db.SaveChanges(); 


            return View();
        }

        [HttpPost]
        public ActionResult LogIn(string username, string encryptPassword, bool rememberMe)
        {

            //string connectionString = "server=localhost;port=3306;database=ars;uid=root;";

            //using (MySqlConnection connection = new MySqlConnection(connectionString))
            //{
            //    // Create database if not exists
            //    using (ARSEntitis contextDB = new ARSEntitis(connection, false))
            //    {
            //        contextDB.Database.CreateIfNotExists();
            //    }

            //    connection.Open();
            //    MySqlTransaction transaction = connection.BeginTransaction();

            //    try
            //    {
            //        // DbConnection that is already opened
            //        using (ARSEntitis context = new ARSEntitis(connection, false))
            //        {

            //            // Interception/SQL logging
            //            context.Database.Log = (string message) => { Console.WriteLine(message); };

            //            // Passing an existing transaction to the context
            //            context.Database.UseTransaction(transaction);

            //            // DbSet.AddRange
            //            //List<Employee> Employees = new List<Employee>();

            //            //Employees.Add(new Employee { Manufacturer = "Nissan", Model = "370Z", Year = 2012 });
            //            //Employees.Add(new Employee { Manufacturer = "Ford", Model = "Mustang", Year = 2013 });
            //            //Employees.Add(new Employee { Manufacturer = "Chevrolet", Model = "Camaro", Year = 2012 });
            //            //Employees.Add(new Employee { Manufacturer = "Dodge", Model = "Charger", Year = 2013 });
            //            Employee em = new Employee {  username = username, password = encryptPassword, truename = "sdd" };

            //            context.Employees.Add(em);

            //            context.SaveChanges();
            //        }

            //        transaction.Commit();
            //    }
            //    catch
            //    {
            //        transaction.Rollback();
            //        throw;
            //    }
            //}


            string ip;
            if (this.HttpContext.Request.ServerVariables["HTTP_VIA"] != null)
            {
                ip = this.HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else
            {
                ip = this.HttpContext.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }


            var db = new ARSEntitis();
            //var employees = ;

            var query = from e in db.Employees
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
