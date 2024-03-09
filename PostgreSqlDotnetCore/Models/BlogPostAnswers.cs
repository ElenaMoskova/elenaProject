using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostgreSqlDotnetCore.Models
{
    
    [Table("blog_post_answers", Schema = "project")]
    public class BlogPostAnswers
    {
        [Key]
        public int id { get; set; }
        public int parent_id { get; set; }
        public string reply { get; set; }
        public int? blogpostconsultationid { get; set; }
        public int usersid { get; set; }
    }
}
