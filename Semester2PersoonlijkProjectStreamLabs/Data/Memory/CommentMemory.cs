using Data.Interfaces;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace Data.Memory
{
    public class CommentMemory : ICommentContext
    {
        private List<Comment> Comments;

        public CommentMemory(List<Comment> _commentList)
        {
            Comments = _commentList;
        }

        public void CommentOnVideo(Comment comment)
        {
            Comments.Add(comment);
        }

        public void DeleteComment(Comment comment)
        {
        }

        public void EditComment(Comment comment)
        {

        }

        public List<Comment> GetAllCommentsOnVideo(int videoId)
        {
            List<Comment> commentList = new List<Comment>();
            foreach (var comment in Comments.Where(x => x.VideoId == videoId))
            {
                commentList.Add(comment);
            }
            return commentList;
        }

        public List<Comment> GetAllCommentsByUser(int userId)
        {
            List<Comment> commentList = new List<Comment>();
            foreach (var comment in Comments.Where(x => x.UserId == userId))
            {
                commentList.Add(comment);
            }
            return commentList;
        }
    }
}
