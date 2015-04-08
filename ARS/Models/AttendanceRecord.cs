using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

namespace ARS.Models
{

    public class AttendanceRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? id { get; set; }

        [DisplayName("用户id")]
        public int user_id { get; set; }

        [DisplayName("动作")]
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
        public DateTime sign_time { get; set; }
    }
}