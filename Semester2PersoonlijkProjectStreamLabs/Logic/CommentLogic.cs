using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;
using Models;

namespace Logic
{
    public class CommentLogic
    {
        private readonly ICommentContext _comment;

        public CommentLogic(ICommentContext comment)
        {
            _comment = comment;
        }

        public void CommentOnVideo()
        {
            _comment.CommentOnVideo();
        }

        public void DeleteComment()
        {
            _comment.DeleteComment();
        }

        public void EditComment()
        {
            _comment.EditComment();
        }

    }
}
