using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PostgreSqlDotnetCore.Models
{
    [Table("medecines", Schema = "project")]
    public class MedecinesClass
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "The customer name is required")]
        public string name { get; set; }
        public string description { get; set; }
        public int diagnosticsid { get; set; }

        public int manufacturersid { get; set; }
    }
}