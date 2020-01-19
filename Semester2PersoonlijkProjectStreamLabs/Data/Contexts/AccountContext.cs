using Data.Interfaces;
using Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Data.Contexts
{
    public class AccountContext : IAccountContext
    {
        private readonly Connection _connection;
        public AccountContext(Connection connection)
        {
            _connection = connection;
        }

        static string RandomString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            return res.ToString();
        }

        public string ChangePassword(int id)
        {
            try
            {
                string query = "UPDATE [User] SET Password = @Password WHERE UserId = @UserId";
                string password = RandomString(10);
                string passwordHash = Hasher.SecurePasswordHasher.Hash(password);

                _connection.conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, _connection.conn))
                {
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = passwordHash;

                    cmd.ExecuteNonQuery();
                }
                return password;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _connection.conn.Close();
            }
        }

        public void DeleteUser(int userId)
        {
            try
            {
                string query = "DELETE FROM [User] WHERE UserId = @UserId";
                using (SqlCommand com = new SqlCommand(query, _connection.conn))
                {
                    _connection.conn.Open();
                    com.Parameters.AddWithValue("@UserId", userId);
                    com.ExecuteNonQuery();
                    _connection.conn.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _connection.conn.Close();
            }
        }

        public void UpdateStatus(int id, bool status)
        {
            try
            {
                string query = "UPDATE [User] SET Status = @Status WHERE UserId = @UserId";

                _connection.conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, _connection.conn))
                {
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = status;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("User not edited");
            }
            finally
            {
                _connection.conn.Close();
            }
        }
    }
}
