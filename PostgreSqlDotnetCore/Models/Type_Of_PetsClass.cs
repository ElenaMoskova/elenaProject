using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PostgreSqlDotnetCore.Models
{
    [Table("type_of_pets", Schema = "project")]
    public class Type_Of_PetsClass
    {
        [Key]
        public int id { get; set; }
     
        public string description { get; set; }

        public string name { get; set; }
        
    }
}