using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PostgreSqlDotnetCore.Models
{
    [Table("products", Schema = "project")]
    public class ProductsClass
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "The product name is required")]
        public string name { get; set; }
        public string description { get; set; }
        public string price { get; set; }
        public bool isactive { get; set; }
        public DateTime dateadded { get; set; }
        public string category { get; set; }
        

    }
}