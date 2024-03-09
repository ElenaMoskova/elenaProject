using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PostgreSqlDotnetCore.Models
{
    [Table("orders", Schema = "project")]
    public class OrdersClass
    {
        [Key]
        public int id { get; set; }
        public int quantity { get; set; }

    }
}