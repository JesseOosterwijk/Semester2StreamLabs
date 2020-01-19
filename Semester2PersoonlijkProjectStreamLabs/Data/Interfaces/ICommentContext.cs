using Models;
using System.Collections.Generic;

namespace Data.Interfaces
{
    public interface ICommentContext
    {
        void CommentOnVideo(Comment comment);
        void DeleteComment(Comment comment);
        void EditComment(Comment comment);
        List<Comment> GetAllCommentsOnVideo(int videoId);
        List<Comment> GetAllCommentsByUser(int userId);
        void DeleteAllCommentsOnVideo(int videoId);
    }
}
