using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PostgreSqlDotnetCore.Models
{
    [Table("breeds", Schema = "project")]
    public class BreedsClass
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "The breed's name is required")]
        public string name { get; set; }
    
    }
}