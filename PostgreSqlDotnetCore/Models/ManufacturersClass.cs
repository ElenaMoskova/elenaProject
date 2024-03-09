using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PostgreSqlDotnetCore.Models
{
    [Table("manufacturers", Schema = "project")]
    public class ManufacturersClass
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "The manufacturer's name is required")]
        public string name { get; set; }
        [Required(ErrorMessage = "The city is required")]
        public string description { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        
    }
}