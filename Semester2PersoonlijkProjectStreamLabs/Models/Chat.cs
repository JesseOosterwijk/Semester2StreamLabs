using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Chat
    {
        public string Content { get; }

        public Chat(string content)
        {
            Content = content;
        }
    }
}
