using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PostgreSqlDotnetCore.Models
{
    [Table("reports", Schema = "project")]
    public class ReportsClass
    {
        [Key]
        public int id { get; set; }
        public string description { get; set; }
        public int usersid { get; set; }
        public int petsid { get; set; }

    }
}