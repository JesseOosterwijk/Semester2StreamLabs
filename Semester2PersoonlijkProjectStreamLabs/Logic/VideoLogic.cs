using Data.Interfaces;
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

        public List<Video> SearchForVideos(string searchTerm)
        {
            return _video.SearchForVideos(searchTerm);
        }

        public List<Video> GetVideosUser(int userId)
        {
            return _video.GetVideosUser(userId);
        }

        public Video GetVideoById(int videoId)
        {
            return _video.GetVideoById(videoId);
        }
    }
}
