using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Data.Memory
{
    public class AccountMemory : IAccountContext
    {
        private List<User> Users = new List<User>();

        public AccountMemory(List<User> userList)
        {
            foreach(User user in userList)
            {
                Users.Add(user);
            }
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

        public string ChangePassword(int userId)
        {
            User currentUser = new Viewer();
            foreach (var item in Users.Where(c => c.UserId == userId))
            {
                currentUser = item;
            }

            string pass = RandomString(10);
            currentUser.Password = pass;
            return currentUser.Password;
        }

        public void DeleteUser(int userId)
        {

        }

        public void UpdateStatus(int userId, bool status)
        {

        }
    }
}
