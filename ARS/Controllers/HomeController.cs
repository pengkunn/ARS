using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SQLite;
using System.Data;
using System.Data.Linq;
using ARS.Models;

namespace ARS.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {

            var strCon = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SQLiteConnection con = new SQLiteConnection(strCon);
            //SQLiteCommand cmd = new SQLiteCommand("select * from Employee", con);
            //SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //var s = dt.Rows[0].ItemArray[0];
            //dataGridView1.DataSource = dt;


            var context = new DataContext(con);

            var employees = context.GetTable<Employee>();

            Employee newEmployee = new Employee() {id=2,password="1",truename="test",username="test" };

            employees.InsertOnSubmit(newEmployee);

            context.SubmitChanges();

            return View();
        }

    }
}
