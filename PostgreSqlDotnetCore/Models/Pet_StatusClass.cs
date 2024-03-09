using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PostgreSqlDotnetCore.Models
{
    [Table("pet_status", Schema = "project")]
    public class Pet_StatusClass
    {
        [Key]
        public int id { get; set; }

       // [Required(ErrorMessage = "The customer name is required")]
        public string type { get; set; }
       // [Required(ErrorMessage = "The customer lastname is required")]
        public string node { get; set; }
     
   
    }
}