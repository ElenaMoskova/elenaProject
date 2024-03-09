using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PostgreSqlDotnetCore.Models
{
    [Table("jobs", Schema = "project")]
    public class JobsClass
    {
        [Key]
        public int id { get; set; }

        
        public string description { get; set; }
        [Required(ErrorMessage = "The customer lastname is required")]
        public string predictedsalery { get; set; }


        public int vetcentersid { get; set; }


    }
}