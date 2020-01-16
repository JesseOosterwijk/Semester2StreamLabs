using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Semester2PersoonlijkProjectStreamLabs.Models
{
    public class VideoViewModel
    {
        public int VideoId { get; set; }
        [Required(ErrorMessage = "Please select a category.")]
        public Category VideoCategory { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a description.")]
        public string Description { get; set; }
        public DateTime DateOfUpload { get; set; }
        public int VideoLength { get; set; }
        public int Views { get; set; }
        public string ContentUrl { get; set; }
        public int CategoryId { get; set; }
        public IFormFile Video { get; set; }

        public VideoViewModel(Video video)
        {
            VideoId = video.VideoId;
            VideoCategory = video.VideoCategory;
            Name = video.Name;
            Description = video.Description;
            DateOfUpload = video.DateOfUpload;
            ContentUrl = video.VideoUrl;
        }

        public VideoViewModel(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public VideoViewModel()
        {

        }
    }
}
