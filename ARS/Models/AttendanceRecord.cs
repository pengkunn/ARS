using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

namespace ARS.Models
{
    [Table(Name = "AttendanceRecord")]
    public class AttendanceRecord
    {
        [Column(Name = "id", IsPrimaryKey = true)]
        public int? id { get; set; }

        [Column(Name = "user_id")]
        public int user_id { get; set; }

        [Column(Name = "type")]
        public int type { get; set; }

        [Column(Name = "sign_time")]
        public DateTime sign_time { get; set; }
    }
}