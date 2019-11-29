using System;

namespace Models
{
    public class Video
    {
        public int VideoId { get; }
        public Category VideoCategory { get; }
        public string Name { get; }
        public string Description { get; }
        public DateTime DateOfUpload { get; }
        public int VideoLength { get; }
        public int Views { get; }

        public Video()
        {
            ;
        }

        public Video(int videoId, Category videoCategory, string description, string name, DateTime dateOfUpload, int videoLength, int views)
        {
            VideoId = videoId;
            VideoCategory = videoCategory;
            Description = description;
            Name = name;
            DateOfUpload = dateOfUpload;
            VideoLength = videoLength;
            Views = views;
        }

        public Video(Category videoCategory, string name, string description, DateTime dateOfUpload, int videoLength, int views)
        {
            VideoCategory = videoCategory;
            Name = name;
            Description = description;
            DateOfUpload = dateOfUpload;
            VideoLength = videoLength;
            Views = views;
        }

        public Video(string name)
        {
            Name = name;
        }
    }
}
