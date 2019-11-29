﻿using Models;
using System;

namespace Semester2PersoonlijkProjectStreamLabs.Models
{
    public class VideoViewModel
    {
        public int VideoId { get; set; }
        public Category VideoCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateOfUpload { get; set; }
        public int VideoLength { get; set; }
        public int Views { get; set; }

        public VideoViewModel(Video video)
        {
            VideoId = video.VideoId;
            VideoCategory = video.VideoCategory;
            Name = video.Name;
            Description = video.Description;
            DateOfUpload = video.DateOfUpload;
            VideoLength = video.VideoLength;
            Views = video.Views;
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
