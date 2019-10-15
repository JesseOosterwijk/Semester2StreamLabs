using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface ICommentContext
    {
        void CommentOnVideo();
        void DeleteComment();
        void EditComment();
    }
}
