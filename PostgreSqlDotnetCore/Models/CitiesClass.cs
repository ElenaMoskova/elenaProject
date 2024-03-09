using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PostgreSqlDotnetCore.Models
{
    [Table("cities", Schema = "project")]
    public class CitiesClass
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "The cities name is required")]
        public string name { get; set; }
       
    }
}