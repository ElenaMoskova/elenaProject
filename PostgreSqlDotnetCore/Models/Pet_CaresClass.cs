using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PostgreSqlDotnetCore.Models
{
    [Table("pet_cares", Schema = "project")]
    public class Pet_CaresClass
    {
        [Key]
        public int id { get; set; }

        // [Required(ErrorMessage = "The customer name is required")]
        public string title { get; set; }
        //[Required(ErrorMessage = "The customer lastname is required")]
        public string description { get; set; }
        public DateTime dateending { get; set; }
        public int usersid { get; set; }
        public int vetcentersid { get; set; }


    }
}