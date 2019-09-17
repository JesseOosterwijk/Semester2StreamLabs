using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Data.Contexts
{
    public class UserContextSQL : IUserContext
    {

        private readonly SqlConnection _conn = Connection.GetConnection();

        public void CreateUser(User user)
        {
            try
            {
                _conn.Open();
                using (SqlCommand cmd = new SqlCommand("CreateUser", _conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("User not Created");
            }
            finally
            {
                _conn.Close();
            }
        }
        
        public void EditUser()
        {

        }

        public void DeleteUser()
        {

        }
        
        public List<User> GetAllUsers()
        {
            return new List<User>();
        }

        public void TimeOutUser()
        {

        }

        public void BanUser()
        {

        }

        public bool CheckIfUserAlreadyExists()
        {
            return true;
        }

        public bool CheckIfAccountIsActive()
        {
            return true;
        }

        public bool CheckIfEmailIsValid()
        {
            return true;
        }

        public List<User> GetAllStreamers()
        {
            List<User> streamers = new List<User>();

            try
            {
                SqlCommand cmd = new SqlCommand("GetAllStreamers", _conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    streamers.Add(new Streamer());
                }

                return streamers;
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

        public User GetUserInfo()
        {
            return new Streamer();
        }

        public User GetUserById()
        {
            return new Streamer();
        }

    }
}
