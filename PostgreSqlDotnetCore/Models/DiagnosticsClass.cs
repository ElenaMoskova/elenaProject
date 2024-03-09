using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PostgreSqlDotnetCore.Models
{
    [Table("diagnostics", Schema = "project")]
    public class DiagnosticsClass
    {
        [Key]
        public int id { get; set; }
        public string description { get; set; }
        public int usersid { get; set; }

    }
}