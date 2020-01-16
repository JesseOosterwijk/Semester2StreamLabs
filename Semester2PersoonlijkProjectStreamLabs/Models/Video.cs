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
        public int UserId { get; }

        public Video()
        {

        }

        public Video(int videoId, int userId, Category videoCategory, string description, string name, DateTime dateOfUpload, string url)
        {
            VideoId = videoId;
            UserId = userId;
            VideoCategory = videoCategory;
            Description = description;
            Name = name;
            DateOfUpload = dateOfUpload;
            VideoUrl = url;
        }

        public Video(int userId, int videoId, string description, string name, DateTime dateOfUpload, string url, int categoryId)
        {
            UserId = userId;
            VideoId = videoId;
            Description = description;
            Name = name;
            DateOfUpload = dateOfUpload;
            VideoUrl = url;
            CategoryId = categoryId;
        }

        public Video(int userId, Category videoCategory, string name, string description, DateTime dateOfUpload, string videoUrl)
        {
            UserId = userId;
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
