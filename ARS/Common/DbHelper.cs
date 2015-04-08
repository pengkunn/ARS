using System;
using System.Collections.Generic;
using System.Data.Linq;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MySql.Data.Entity;
using ARS.Models;

namespace ARS.Common
{
    public class DbHelper
    {
        static DbHelper _dbHelper = null;

        static public DbHelper getInstance()
        {
            if (_dbHelper == null)
            {
                _dbHelper = new DbHelper();
            }

            return _dbHelper;
        }

        DataContext _context = null;
        ARSEntitis _db = null;

        public DbHelper()
        {
            //var strCon = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            //MySqlConnection con = new MySqlConnection(strCon);
            //_context = new DataContext(con);
        }

        public DataContext getDbContext()
        {
            return _context;
        }

        public DataContext createDbContext()
        {
            var strCon = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            MySqlConnection con = new MySqlConnection(strCon);
            return new DataContext(con);
        }

        public ARSEntitis getDB()
        {
            if (_db==null)
            {
                _db = new ARSEntitis();

            }
            return _db;
        }  
    }
}