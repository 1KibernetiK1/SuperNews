using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperNews.Domains
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public long? CommentId { get; set; }

        public string Description { get; set; }

        public virtual ICollection<News> ArticlesOfComments { get; set; }
    }
}
