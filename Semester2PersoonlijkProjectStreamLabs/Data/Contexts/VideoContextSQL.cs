using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Data.Contexts
{
    public class VideoContextSQL : IVideoContext
    {
        private readonly SqlConnection _conn = Connection.GetConnection();

        public void SaveVideo(Video video)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SaveVideo", _conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", SqlDbType.Int).Value = video.UserId;
                    cmd.Parameters.AddWithValue("@Name", SqlDbType.NVarChar).Value = video.Name;
                    cmd.Parameters.AddWithValue("@Description", SqlDbType.NVarChar).Value = video.Description;
                    cmd.Parameters.AddWithValue("@ContentUrl", SqlDbType.NVarChar).Value = video.VideoUrl;
                    cmd.Parameters.AddWithValue("@DateOfUpload", SqlDbType.NVarChar).Value = video.DateOfUpload;
                    cmd.Parameters.AddWithValue("@CategoryID", SqlDbType.Int).Value = video.CategoryId;

                    _conn.Open();
                    cmd.ExecuteNonQuery();
                };
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public void DeleteVideo(Video video)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteVideo", _conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("VideoId", SqlDbType.Int).Value = video.VideoId;

                _conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new ArgumentException("Video not deleted");
            }
            finally
            {
                _conn.Close();
            }
        }

        public void RestrictVideo(Video video)
        {
            try
            {

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public Video GetVideoById(int videoId)
        {
            try
            {
                _conn.Open();
                SqlDataAdapter cmd = new SqlDataAdapter
                {
                    SelectCommand = new SqlCommand("GetVideoById", _conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };

                cmd.SelectCommand.Parameters.Add("@VideoId", SqlDbType.Int).Value = videoId;

                DataTable dt = new DataTable();
                cmd.Fill(dt);

                string name = dt.Rows[0].ItemArray[0].ToString();
                string description = dt.Rows[0].ItemArray[1].ToString();
                DateTime dateOfUpload = (DateTime)dt.Rows[0].ItemArray[2];
                Category category = new Category(0, dt.Rows[0].ItemArray[7].ToString(), null);
                string url = dt.Rows[0].ItemArray[5].ToString();
                int userId = (int)dt.Rows[0].ItemArray[6];

                return new Video(videoId, userId, category, name, description, dateOfUpload, url);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public List<Video> GetVideosUser(int userId)
        {
            try
            {
                _conn.Open();

                List<Video> videoList = new List<Video>();

                SqlCommand cmd = new SqlCommand("GetAllVideosUser", _conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.AddWithValue(@"UserId", SqlDbType.Int).Value = userId;

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int videoId = (int)dr["VideoId"];
                    string videoName = dr["Name"].ToString();
                    Category videoCategory = new Category(0, dr["CatName"].ToString(), null);
                    string description = dr["Description"].ToString();
                    DateTime dateOfUpload = (DateTime)dr["DateOfUpload"];
                    string videoUrl = dr["VideoPath"].ToString();
                    Video video = new Video(videoId, videoCategory, description, videoName, dateOfUpload, videoUrl);
                    videoList.Add(video);
                }
                return videoList;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }
        }

        public List<Video> GetVideos()
        {
            try
            {
                List<Video> videoList = new List<Video>();

                _conn.Open();
                SqlCommand cmd = new SqlCommand("GetAllVideos", _conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int videoId = (int)dr["VideoId"];
                    string videoName = dr["Name"].ToString();
                    Category videoCategory = new Category(0, dr["CatName"].ToString(), null);
                    string description = dr["Description"].ToString();
                    DateTime dateOfUpload = (DateTime)dr["DateOfUpload"];
                    string videoUrl = dr["VideoPath"].ToString();
                    Video video = new Video(videoId, videoCategory, description, videoName, dateOfUpload, videoUrl);
                    videoList.Add(video);
                }
                return videoList;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public List<Video> SearchForVideos(string searchTerm)
        {
            try
            {
                List<Video> videos = new List<Video>();

                _conn.Open();
                SqlCommand cmd = new SqlCommand("SearchVideos", _conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int videoId = (int)dr["VideoId"];
                    string videoName = dr["Name"].ToString();
                    Category videoCategory = new Category(0, dr["CatName"].ToString(), null);
                    string description = dr["Description"].ToString();
                    DateTime dateOfUpload = (DateTime)dr["DateOfUpload"];
                    string videoUrl = dr["VideoPath"].ToString();
                    int userId = (int)dr["UserId"];
                    Video video = new Video(videoId, userId, videoCategory, description, videoName, dateOfUpload, videoUrl);
                    videos.Add(video);
                }
                return videos;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public void SetVideosToDefaultCategory(int videoId)
        {
            try
            {
                _conn.Open();
                using (SqlCommand cmd = new SqlCommand("SetToDefaultCategory", _conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@VideoId", SqlDbType.Int).Value = videoId;
                    cmd.Parameters.AddWithValue("@CatId", SqlDbType.Int).Value = 10;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public List<Video> GetVideosWithCategory(int categoryId)
        {
            try
            {
                List<Video> videos = new List<Video>();

                _conn.Open();
                SqlCommand cmd = new SqlCommand("GetVideosWithCategory", _conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int videoId = (int)dr["VideoId"];
                    string videoName = dr["Name"].ToString();
                    Category videoCategory = new Category(0, dr["CatName"].ToString(), null);
                    string description = dr["Description"].ToString();
                    DateTime dateOfUpload = (DateTime)dr["DateOfUpload"];
                    string videoUrl = dr["VideoPath"].ToString();
                    int userId = (int)dr["UserId"];
                    Video video = new Video(videoId, userId, videoCategory, description, videoName, dateOfUpload, videoUrl);
                    videos.Add(video);
                }
                return videos;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }
    }
}
