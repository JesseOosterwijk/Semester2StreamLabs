using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IVideoContext
    {
        void UploadVideo();
        void DeleteVideo();
        void RestrictVideo();
    }
}
