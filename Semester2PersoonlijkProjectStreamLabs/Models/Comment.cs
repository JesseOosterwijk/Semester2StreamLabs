using System;

namespace Models
{
    public class Comment
    {
        public int CommentId { get; }
        public int VideoId { get; }
        public int UserId { get; }
        public string Content { get; }
        public DateTime TimeStamp { get; }

        public Comment(int commentId, int videoId, int userId, string content, DateTime timeStamp)
        {
            CommentId = commentId;
            VideoId = videoId;
            UserId = userId;
            Content = content;
            TimeStamp = timeStamp;
        }

        public Comment(int videoId, int userId, string content, DateTime timeStamp)
        {
            VideoId = videoId;
            UserId = userId;
            Content = content;
            TimeStamp = timeStamp;
        }

        public Comment()
        {
            ;
        }
    }
}
