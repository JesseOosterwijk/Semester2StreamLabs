using Data.Interfaces;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace Data.Memory
{
    public class VideoMemory : IVideoContext
    {
        private readonly List<Video> TestVideos = new List<Video>();

        public VideoMemory(List<Video> VideoInput)
        {
            TestVideos = VideoInput;
        }

        public void SaveVideo(Video video)
        {
            ;
        }

        public void DeleteVideo(Video video)
        {
            ;
        }

        public void RestrictVideo(Video video)
        {
            ;
        }

        public List<Video> GetVideos()
        {
            return TestVideos;
        }

        public List<Video> SearchForVideos(string searchTerm)
        {
            List<Video> resultList = new List<Video>();

            foreach (var item in TestVideos.Where(x => x.Name == searchTerm))
            {
                resultList.Add(item);
            }
            return resultList;
        }

        public Video GetVideoById(int videoId)
        {
            Video result = new Video();
            foreach (var item in TestVideos.Where(x => x.VideoId == videoId))
            {
                result = item;
            }
            return result;
        }

        public List<Video> GetVideosUser(int userId)
        {
            return new List<Video>();
        }

        public void SetVideosToDefaultCategory(int categoryId)
        {

        }

        public List<Video> GetVideosWithCategory(int categoryId)
        {
            return new List<Video>();
        }
    }
}
