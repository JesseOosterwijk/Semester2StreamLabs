using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Data.Contexts
{
    public class CommentContextSQL : ICommentContext
    {
        private readonly SqlConnection _conn = Connection.GetConnection();

        public void CommentOnVideo(Comment comment)
        {
            try
            {
                _conn.Open();
                using (SqlCommand cmd = new SqlCommand("CommentOnVideo", _conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@VideoId", SqlDbType.Int).Value = comment.VideoId;
                    cmd.Parameters.AddWithValue("@UserId", SqlDbType.Int).Value = comment.UserId;
                    cmd.Parameters.AddWithValue("@Content", SqlDbType.Int).Value = comment.Content;
                    cmd.Parameters.AddWithValue("@TimeStamp", SqlDbType.Int).Value = comment.TimeStamp;
                    cmd.ExecuteNonQuery();
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

        public void EditComment(Comment comment)
        {
            try
            {
                _conn.Open();
                using (SqlCommand cmd = new SqlCommand("EditComment", _conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(@"CommentId", SqlDbType.Int).Value = comment.CommentId;
                    cmd.Parameters.AddWithValue("@VideoId", SqlDbType.Int).Value = comment.VideoId;
                    cmd.Parameters.AddWithValue("@UserId", SqlDbType.Int).Value = comment.UserId;
                    cmd.Parameters.AddWithValue("@Content", SqlDbType.Int).Value = comment.Content;
                    cmd.Parameters.AddWithValue("@TimeStamp", SqlDbType.Int).Value = comment.TimeStamp;
                    cmd.ExecuteNonQuery();
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

        public void DeleteComment(Comment comment)
        {
            try
            {
                _conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteComment", _conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CommentId", SqlDbType.Int).Value = comment.CommentId;
                    cmd.ExecuteNonQuery();
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

        public List<Comment> GetAllCommentsOnVideo(int videoId)
        {
            try
            {
                List<Comment> comments = new List<Comment>();

                _conn.Open();
                SqlCommand cmd = new SqlCommand("GetAllCommentsVideo", _conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@VideoId", SqlDbType.Int).Value = videoId;



                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int commentId = (int)dr["CommentId"];
                    int userId = (int)dr["UserId"];
                    string content = dr["Content"].ToString();
                    DateTime timeStamp = (DateTime)dr["TimeStamp"];
                    Comment comment = new Comment(commentId, videoId, userId, content, timeStamp);
                    comments.Add(comment);
                }
                return comments;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public List<Comment> GetAllCommentsByUser(int userId)
        {
            try
            {
                List<Comment> comments = new List<Comment>();

                _conn.Open();
                SqlCommand cmd = new SqlCommand("GetAllCommentsUser", _conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@userId", userId);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int commentId = (int)dr["CommentId"];
                    int videoId = (int)dr["VideoId"];
                    string content = dr["Content"].ToString();
                    DateTime timeStamp = (DateTime)dr["TimeStamp"];
                    Comment comment = new Comment(commentId, videoId, userId, content, timeStamp);
                    comments.Add(comment);
                }
                return comments;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

    }
}
