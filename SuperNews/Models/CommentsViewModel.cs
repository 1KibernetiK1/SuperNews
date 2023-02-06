using Microsoft.AspNetCore.Mvc;
using SuperNews.Domains;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SuperNews.Models
{
    public class CommentsViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public long? CommentId { get; set; }

        [Required(ErrorMessage = "Напишите сообщение")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public virtual ICollection<News> ArticlesOfComments { get; set; }

        public CommentsViewModel()
        {

        }

      
    }
}
