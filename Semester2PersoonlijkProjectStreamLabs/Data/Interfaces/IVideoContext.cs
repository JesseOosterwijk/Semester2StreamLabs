using Microsoft.AspNetCore.Http;
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
    }
}
