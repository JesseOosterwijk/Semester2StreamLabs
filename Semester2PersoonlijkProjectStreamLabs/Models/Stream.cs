using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Stream
    {
        public string Title { get; }
        public Category StreamCategory { get; }
        public string Content { get; }
        public string StreamDescription { get; }

        public Stream(string title, Category category, string content, string streamDescription)
        {
            Title = title;
            StreamCategory = category;
            Content = content;
            StreamDescription = streamDescription;
        }
    }
}
