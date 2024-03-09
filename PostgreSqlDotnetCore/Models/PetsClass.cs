using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;

namespace PostgreSqlDotnetCore.Models
{
    [Table("pets", Schema = "project")]
    public class PetsClass
    {
        [Key]
        public int id { get; set; }
        public string color { get; set; }

        public string description { get; set; }
       // public DateTime dateofbirthday { get; set; }
        public DateOnly dateofbirthday { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public int usersid { get; set; }

        public int typeofpetsid { get; set; }


  

    }
}