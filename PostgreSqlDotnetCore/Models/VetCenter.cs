using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;

namespace PostgreSqlDotnetCore.Models
{
    [Table("vet_centers", Schema = "project")]
    public class VetCenter
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "The name is required")]
        public string name { get; set; }
        [Required(ErrorMessage = "The adress is required")]
        public string adress { get; set; }
        public string description { get; set; }
        public string workinghours { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public string phonenumber { get; set; }
        public int citiesid { get; set; }


    }
}