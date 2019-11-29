using Data.Interfaces;
using System.IO;

namespace Data.Contexts
{
    public class VideoContextSQL : IVideoContext
    {
        public void UploadVideo()
        {

        }

        public void DeleteVideo()
        {

        }

        public void RestrictVideo()
        {

        }

        public string[] GetAllVideos(string userName)
        {
            try
            {
                string pathString = Path.Combine(@"D:\Stream", userName);
                if (!Directory.Exists(pathString))
                {
                    ;
                }
                string[] stringList = Directory.GetFiles(pathString, "*", SearchOption.TopDirectoryOnly);

                int delete = userName.Length + 11;
                for (int i = 0; i<stringList.Length; i++)
                {
                    stringList[i] = stringList[i].Remove(0, delete);
                }
                
                return stringList;
            }
            catch (System.Exception)
            {
                throw;
            }

        }
    }
}
