using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Video
    {
        public Category VideoCategory { get; }
        public string Name { get; }
        public DateTime DateOfUpload { get; }
        public string VideoLength { get; }

        public Video()
        {
            ;
        }

        public Video(Category videoCategory, string name, DateTime dateOfUpload, string videoLength)
        {
            VideoCategory = videoCategory;
            Name = name;
            DateOfUpload = dateOfUpload;
            VideoLength = videoLength;
        }
    }
}
