using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Comment
    {
        public string Content { get; }

        public Comment(string content)
        {
            Content = content;
        }
    }
}
