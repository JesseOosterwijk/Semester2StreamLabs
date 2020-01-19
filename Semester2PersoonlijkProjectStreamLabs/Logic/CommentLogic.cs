using Data.Interfaces;
using Models;
using System.Collections.Generic;

namespace Logic
{
    public class CommentLogic
    {
        private readonly ICommentContext _comment;

        public CommentLogic(ICommentContext comment)
        {
            _comment = comment;
        }

        public void CommentOnVideo(Comment comment)
        {
            _comment.CommentOnVideo(comment);
        }

        public void DeleteComment(Comment comment)
        {
            _comment.DeleteComment(comment);
        }

        public void EditComment(Comment comment)
        {
            _comment.EditComment(comment);
        }

        public List<Comment> GetAllCommentsOnVideo(int videoId)
        {
            return _comment.GetAllCommentsOnVideo(videoId);
        }

        public List<Comment> GetAllCommentsByUser(int userId)
        {
            return _comment.GetAllCommentsByUser(userId);
        }

        public void DeleteAllCommentsOnVideo(int videoId)
        {
            _comment.DeleteAllCommentsOnVideo(videoId);
        }
    }
}
