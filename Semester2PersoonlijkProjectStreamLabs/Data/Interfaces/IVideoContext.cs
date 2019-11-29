namespace Data.Interfaces
{
    public interface IVideoContext
    {
        void UploadVideo();
        void DeleteVideo();
        void RestrictVideo();
        string[] GetAllVideos(string userName);
    }
}
