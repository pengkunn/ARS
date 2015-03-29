using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DisplayName("用户id")]
        [Column(Name = "user_id")]
        public int user_id { get; set; }

        [DisplayName("动作")]
        [Column(Name = "type")]
        public int type { get; set; }

        [DisplayName("动作")]
        public string typeName
        {
            get { 
                string value = "";
                if (this.type == 1)
                    value = "签到";
                else if (this.type == 2)
                    value = "签退";

                return value;
            }
        }

        [DisplayName("时间")]
        [Column(Name = "sign_time")]
        public DateTime sign_time { get; set; }
    }
}