using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SQLite;
using System.Linq;
using System.Web;

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

        public DbHelper()
        {
            var strCon = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SQLiteConnection con = new SQLiteConnection(strCon);
            _context = new DataContext(con);
        }

        public DataContext getDbContext()
        {
            return _context;
        }

        public DataContext createDbContext()
        {
            var strCon = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SQLiteConnection con = new SQLiteConnection(strCon);
            return new DataContext(con);
        }
    }
}