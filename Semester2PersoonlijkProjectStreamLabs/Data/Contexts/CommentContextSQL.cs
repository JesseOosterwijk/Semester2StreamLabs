using Data.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Data.Contexts
{
    public class CommentContextSQL : ICommentContext
    {
        private readonly SqlConnection _conn = Connection.GetConnection();

        public void CommentOnVideo()
        {
            try
            {
                _conn.Open();
                using (SqlCommand cmd = new SqlCommand("CommentOnVideo", _conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
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

        public void EditComment()
        {
            try
            {
                _conn.Open();
                using (SqlCommand cmd = new SqlCommand("EditComment", _conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

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

        public void DeleteComment()
        {
            try
            {
                _conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteComment", _conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
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

    }
}
