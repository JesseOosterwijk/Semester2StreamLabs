using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class VideoLogic
    {
        private readonly IVideoContext _video;

        public VideoLogic(IVideoContext video)
        {
            _video = video;
        }

        public void UploadVideo()
        {
            _video.UploadVideo();
        }

        public void DeleteVideo()
        {
            _video.UploadVideo();
        }

        public void RestrictVideo()
        {
            _video.RestrictVideo();
        }
    }
}
