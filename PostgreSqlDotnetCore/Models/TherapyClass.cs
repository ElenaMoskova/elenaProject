using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PostgreSqlDotnetCore.Models
{
    [Table("therapy", Schema = "project")]
    public class TherapyClass
    {
        [Key]
        public int id { get; set; }
        public string description { get; set; }
        public DateTime appoitmentdate { get; set; }
        public int diagnosticsid { get; set; }
        public int pet_statusid { get; set; }

    }
}