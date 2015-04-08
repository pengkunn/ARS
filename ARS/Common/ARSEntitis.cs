using ARS.Models;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ARS.Common
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ARSEntitis : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }

        //public ARSEntitis()
        //    : base("MySqlServer")
        //{

        //}

        //// Constructor to use on a DbConnection that is already opened
        //public ARSEntitis(DbConnection existingConnection, bool contextOwnsConnection)
        //    : base(existingConnection, contextOwnsConnection)
        //{

        //}

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Employee>().MapToStoredProcedures();
        //    modelBuilder.Entity<AttendanceRecord>().MapToStoredProcedures();
        //}
    }
}