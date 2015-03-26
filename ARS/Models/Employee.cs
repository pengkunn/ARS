using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

namespace ARS.Models
{
    [Table(Name = "Employee")]
    public class Employee
    {
        [Column(Name = "id",IsPrimaryKey=true)]
        public int? id { get; set; }

        [Column(Name = "username")]
        public string username { get; set; }

        [Column(Name = "password")]
        public string password { get; set; }

        [Column(Name = "truename")]
        public string truename { get; set; }

        [Column(Name = "mac_address")]
        public string mac_address { get; set; }

    }
}