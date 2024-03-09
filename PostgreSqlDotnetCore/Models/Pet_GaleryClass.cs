using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PostgreSqlDotnetCore.Models
{
    [Table("pet_galery", Schema = "project")]
    public class Pet_GaleryClass
    {
        [Key]
        public int id { get; set; }

       // [Required(ErrorMessage = "The customer name is required")]
        public string title { get; set; }
        //[Required(ErrorMessage = "The customer lastname is required")]
        public string picture { get; set; }
        

    }
}