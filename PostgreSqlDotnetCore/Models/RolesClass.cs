using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PostgreSqlDotnetCore.Models
{
    [Table("roles", Schema = "project")]
    public class RolesClass
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "The customer type is required")]
        public string type { get; set; }
      
    }
}