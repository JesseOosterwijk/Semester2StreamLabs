using Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Models;
using System.Collections.Generic;

namespace Logic
{
    public class VideoLogic
    {
        private readonly IVideoContext _video;

        public VideoLogic(IVideoContext video)
        {
            _video = video;
        }

        public void SaveVideo(Video video)
        {
            _video.SaveVideo(video);
        }

        public void DeleteVideo(Video video)
        {
            _video.DeleteVideo(video);
        }

        public void RestrictVideo(Video video)
        {
            _video.RestrictVideo(video);
        }

        public List<Video> GetVideos()
        {
            return _video.GetVideos();
        }
    }
}
