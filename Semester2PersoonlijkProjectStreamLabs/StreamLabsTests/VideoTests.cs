using Data.Interfaces;
using Data.Memory;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Models;
using System;
using System.Linq;

namespace StreamLabsTests
{
    [TestClass]
    public class VideoTests
    {
        private VideoLogic _videoLogic;
        private IVideoContext _videoContext;
        public List<Video> videoList = new List<Video>();
        private void InstanceLogic()
        {
            videoList.Add(new Video(1, 2, "description", "test", DateTime.Now, "url", 1));
            videoList.Add(new Video(2, 2, "description", "test2", DateTime.Now, "url", 1));
            videoList.Add(new Video(3, 2, "description", "test3", DateTime.Now, "url", 1));
            videoList.Add(new Video(2, 2, "description", "test4", DateTime.Now, "url", 1));
            videoList.Add(new Video(2, 2, "description", "test5", DateTime.Now, "url", 1));
            videoList.Add(new Video(2, 2, "description", "test6", DateTime.Now, "url", 1));
            videoList.Add(new Video(3, 2, "description", "test7", DateTime.Now, "url", 1));
            _videoContext = new VideoMemory(videoList);
            _videoLogic = new VideoLogic(_videoContext);
        }

        [TestMethod]
        public void GetVideoById()
        {
            InstanceLogic();

            Video expected = new Video();
            foreach (var item in videoList.Where(x => x.VideoId == videoList[1].VideoId))
            {
                expected = item;
            }
            var result = _videoLogic.GetVideoById(videoList[1].VideoId);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetVideos()
        {
            InstanceLogic();

            var expected = videoList;
            var result = _videoLogic.GetVideos();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void SearchForVideos()
        {
            InstanceLogic();
            List<Video> expectedList = new List<Video>();
            foreach (var item in videoList.Where(x => x.Name == "test"))
            {
                expectedList.Add(item);
            }
            var result = _videoLogic.SearchForVideos("test");

            for (int i = 0; i < expectedList.Count; i++)
            {
                Assert.AreEqual(expectedList[i], result[i]);
            }
        }
    }
}