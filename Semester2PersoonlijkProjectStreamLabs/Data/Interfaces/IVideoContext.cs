using Models;
using System.Collections.Generic;

namespace Data.Interfaces
{
    public interface IVideoContext
    {
        void SaveVideo(Video video);
        void DeleteVideo(Video video);
        void RestrictVideo(Video video);
        List<Video> GetVideos();
        List<Video> SearchForVideos(string searchTerm);
        Video GetVideoById(int videoId);
        List<Video> GetVideosUser(int userId);
    }
}
