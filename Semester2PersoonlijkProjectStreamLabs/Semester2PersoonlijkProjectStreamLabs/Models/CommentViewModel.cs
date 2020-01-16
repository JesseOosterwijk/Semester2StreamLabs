using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Semester2PersoonlijkProjectStreamLabs.Models
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public int VideoId { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please enter a comment")]
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public string UserName { get; set; }
        public bool Status { get; set; }

        public CommentViewModel(int commentId, int videoId, int userId, string content, DateTime timeStamp)
        {
            CommentId = commentId;
            VideoId = videoId;
            UserId = userId;
            Content = content;
            TimeStamp = timeStamp;
        }

        public CommentViewModel(int videoId, int userId, string content, DateTime timeStamp)
        {
            VideoId = videoId;
            UserId = userId;
            Content = content;
            TimeStamp = timeStamp;
        }

        public CommentViewModel(Comment comment)
        {
            CommentId = comment.CommentId;
            VideoId = comment.VideoId;
            UserId = comment.UserId;
            Content = comment.Content;
            TimeStamp = comment.TimeStamp;
        }

        public CommentViewModel(int videoId, string content)
        {
            VideoId = videoId;
            Content = content;
        }

        public CommentViewModel()
        {
            ;
        }
    }
}
