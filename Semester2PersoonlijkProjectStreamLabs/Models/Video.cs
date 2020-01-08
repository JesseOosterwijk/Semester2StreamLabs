using System;
using System.IO;

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
        public int CategoryId { get; }
        public int Views { get; }
        public string VideoUrl { get; }

        public Video()
        {
            ;
        }

        public Video(int videoId, Category videoCategory, string description, string name, DateTime dateOfUpload, string url)
        {
            VideoId = videoId;
            VideoCategory = videoCategory;
            Description = description;
            Name = name;
            DateOfUpload = dateOfUpload;
            VideoUrl = url;
        }

        public Video(int videoId, string description, string name, DateTime dateOfUpload, string url, int categoryId)
        {
            VideoId = videoId;
            Description = description;
            Name = name;
            DateOfUpload = dateOfUpload;
            VideoUrl = url;
            CategoryId = categoryId;
        }

        public Video(Category videoCategory, string name, string description, DateTime dateOfUpload, string videoUrl)
        {
            VideoCategory = videoCategory;
            Name = name;
            Description = description;
            DateOfUpload = dateOfUpload;
            VideoUrl = videoUrl;
        }

        public Video(string name, string videoUrl)
        {
            Name = name;
            VideoUrl = Path.Combine(videoUrl, name);
        }
    }
}
