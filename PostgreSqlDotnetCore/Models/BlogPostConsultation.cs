using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PostgreSqlDotnetCore.Models
{
    [Table("blog_post_for_consultations", Schema = "project")]
    public class BlogPostConsultation
    {
        [Key]
        public int id { get; set; }
        //public DateTime date_askes { get; set; }
        //public DateTime date_askes { get; set; } = DateTime.UtcNow.Date;
        public DateOnly date_askes { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        public string title { get; set; }
       
        public string description { get; set; }
        public int users_id { get; set; }

        public List<BlogPostAnswers> ?BlogPostAnswers { get; set; }

    }
}