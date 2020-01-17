using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Data.Contexts
{
    public class UserContextSQL : IUserContext
    {

        private readonly SqlConnection _conn = Connection.GetConnection();

        public void CreateUser(User newUser)
        {
            try
            {
                _conn.Open();
                using (SqlCommand cmd = new SqlCommand("CreateUser", _conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", SqlDbType.NVarChar).Value = newUser.UserName;
                    cmd.Parameters.AddWithValue("@FirstName", SqlDbType.NVarChar).Value = newUser.FirstName;
                    cmd.Parameters.AddWithValue("@LastName", SqlDbType.NVarChar).Value = newUser.LastName;
                    cmd.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = newUser.EmailAddress;
                    cmd.Parameters.AddWithValue("@Address", SqlDbType.NVarChar).Value = newUser.Address;
                    cmd.Parameters.AddWithValue("@BirthDate", SqlDbType.DateTime).Value = newUser.BirthDate;
                    cmd.Parameters.AddWithValue("@City", SqlDbType.NVarChar).Value = newUser.City;
                    cmd.Parameters.AddWithValue("@Sex", SqlDbType.Bit).Value = newUser.UserGender;
                    cmd.Parameters.AddWithValue("@Password", SqlDbType.NVarChar).Value = newUser.Password;
                    cmd.Parameters.AddWithValue("@PostalCode", SqlDbType.NChar).Value = newUser.PostalCode;
                    cmd.Parameters.AddWithValue("@AccountType", SqlDbType.NVarChar).Value = newUser.UserAccountType.ToString();
                    cmd.Parameters.AddWithValue("@Status", SqlDbType.Bit).Value = true;
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

        public void EditUser(User user)
        {
            try
            {
                _conn.Open();
                using (SqlCommand cmd = new SqlCommand("EditUser", _conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = user.UserName;
                    cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = user.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = user.LastName;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.EmailAddress;
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = user.Address;
                    cmd.Parameters.Add("@BirthDate", SqlDbType.DateTime).Value = user.BirthDate;
                    cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = user.City;
                    cmd.Parameters.Add("@Gender", SqlDbType.Bit).Value = user.UserGender;
                    cmd.Parameters.Add("@PostalCode", SqlDbType.NChar).Value = user.PostalCode;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = user.UserId;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("User not edited");
            }
            finally
            {
                _conn.Close();
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                List<User> allUsers = new List<User>();

                _conn.Open();
                SqlCommand cmd = new SqlCommand("GetAllUsers", _conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int userId = (int)dr["UserId"];
                    string accountType = dr["AccountType"].ToString();
                    string userName = dr["UserName"].ToString();
                    string firstName = dr["FirstName"].ToString();
                    string lastName = dr["LastName"].ToString();
                    DateTime birthDate = (DateTime)dr["BirthDate"];
                    User.Gender gender = (User.Gender)Enum.Parse(typeof(User.Gender), dr["Sex"].ToString());
                    string email = dr["Email"].ToString();
                    string address = dr["Address"].ToString();
                    string postalCode = dr["PostalCode"].ToString();
                    string city = dr["City"].ToString();
                    bool status = Convert.ToBoolean(dr["Status"].ToString());
                    if (accountType == "Admin")
                    {
                        User user = new Admin(userId, userName, firstName, lastName, User.AccountType.Admin, birthDate, gender, email, address, postalCode, city, status);
                        allUsers.Add(user);
                    }
                    else if (accountType == "Viewer")
                    {
                        User user = new Viewer(userId, userName, firstName, lastName, User.AccountType.Viewer, birthDate, gender, email, address, postalCode, city, status);
                        allUsers.Add(user);
                    }
                }
                return allUsers;
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

        public void BanUser(int userId)
        {

        }

        public bool CheckIfUserAlreadyExists(string email)
        {
            try
            {

                _conn.Open();

                SqlCommand cmd = new SqlCommand("CheckIfUserAlreadyExists", _conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@email", email);

                int numberofAccounts = (int)cmd.ExecuteScalar();

                if (numberofAccounts != 0)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Check failed");
            }
            finally
            {
                _conn.Close();
            }
            return true;
        }

        public bool CheckIfAccountIsActive(string email)
        {
            try
            {
                _conn.Open();

                SqlParameter emailParam = new SqlParameter
                {
                    ParameterName = "@email"
                };

                SqlCommand cmd = new SqlCommand("CheckIfAccountIsActive", _conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                emailParam.Value = email;
                cmd.Parameters.Add(emailParam);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.GetBoolean(0))
                        {
                            _conn.Close();
                            return true;
                        }
                    }
                }
                else
                {
                    return false;
                }
                reader.Close();
            }
            finally
            {
                _conn.Close();
            }
            return false;
        }

        public bool CheckIfEmailIsValid(string userEmail)
        {
            if (string.IsNullOrWhiteSpace(userEmail))
            {
                return false;
            }
            try
            {
                userEmail = Regex.Replace(userEmail, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                string DomainMapper(Match match)
                {
                    IdnMapping idn = new IdnMapping();

                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            try
            {
                return Regex.IsMatch(userEmail,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public User GetUserInfo(string email)
        {
            try
            {
                string query =
                    "SELECT UserID, AccountType, UserName, FirstName, LastName, Birthdate, Sex, Email, Address, City, PostalCode, Password, Status " +
                    "FROM [User] " +
                    "WHERE Email = @email";

                _conn.Open();
                SqlParameter emailParam = new SqlParameter
                {
                    ParameterName = "@email"
                };

                SqlCommand cmd = new SqlCommand(query, _conn);
                emailParam.Value = email;
                cmd.Parameters.Add(emailParam);
                User currentUser = new Viewer(1, User.AccountType.Viewer, "", "", "", DateTime.Now, User.Gender.Male, "", "", "", "", "", false);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string accountType = reader.GetString(1);
                        User.Gender gender = (User.Gender)Enum.Parse(typeof(User.Gender), reader.GetString(5));


                        if (accountType == "Admin")
                        {
                            currentUser = new Admin(reader.GetInt32(0), User.AccountType.Admin, reader.GetString(2), reader.GetString(3), 
                                reader.GetString(4), reader.GetDateTime(5), gender, reader.GetString(7), reader.GetString(8), reader.GetString(9), 
                                reader.GetString(10), reader.GetString(11), reader.GetBoolean(12));
                        }
                        else if (accountType == "Viewer")
                        {
                            currentUser = new Viewer(reader.GetInt32(0), User.AccountType.Admin, reader.GetString(2), reader.GetString(3), 
                                reader.GetString(4), reader.GetDateTime(5), gender, reader.GetString(7), reader.GetString(8), reader.GetString(9), 
                                reader.GetString(10), reader.GetString(11), reader.GetBoolean(12));
                        }

                        return currentUser;
                    }
                    return currentUser;
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                _conn.Close();
            }
        }

        public User GetUserById(int userId)
        {
            try
            {
                string query =
                    "SELECT AccountType, UserName, FirstName, LastName, Birthdate, Sex, Email, Address, PostalCode, City, Status, Password " +
                    "FROM [User] " +
                    "WHERE [UserID] = @UserId";
                _conn.Open();

                SqlDataAdapter cmd = new SqlDataAdapter
                {
                    SelectCommand = new SqlCommand(query, _conn)
                };

                cmd.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                DataTable dt = new DataTable();
                cmd.Fill(dt);

                string accountType = dt.Rows[0].ItemArray[0].ToString();
                string userName = dt.Rows[0].ItemArray[1].ToString();
                string firstName = dt.Rows[0].ItemArray[2].ToString();
                string lastName = dt.Rows[0].ItemArray[3].ToString();
                DateTime birthDate = Convert.ToDateTime(dt.Rows[0].ItemArray[4].ToString());
                User.Gender gender = (User.Gender)Enum.Parse(typeof(User.Gender), dt.Rows[0].ItemArray[5].ToString());
                string email = dt.Rows[0].ItemArray[6].ToString();
                string address = dt.Rows[0].ItemArray[7].ToString();
                string postalCode = dt.Rows[0].ItemArray[8].ToString();
                string city = dt.Rows[0].ItemArray[9].ToString();
                bool status = Convert.ToBoolean(dt.Rows[0].ItemArray[10].ToString());
                string password = dt.Rows[0].ItemArray[11].ToString();

                if (accountType == "Admin")
                {
                    return new Admin(userId, User.AccountType.Admin, userName, firstName, lastName, birthDate, gender, email, address, city, postalCode,
                        password, status);
                }

                else if (accountType == "Viewer")
                {
                    return new Viewer(userId, User.AccountType.Viewer, userName, firstName, lastName, birthDate, gender, email, address, city, postalCode,
                        password, status);
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public User CheckValidityUser(string emailAdress, string password)
        {
            try
            {
                string query =
                    "SELECT UserID, AccountType, UserName, FirstName, LastName, Birthdate, Sex, Address, PostalCode, City, Status, Password " +
                    "FROM [User] " +
                    "WHERE [Email] = @Email";
                _conn.Open();

                SqlDataAdapter cmd = new SqlDataAdapter
                {
                    SelectCommand = new SqlCommand(query, _conn)
                };

                cmd.SelectCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = emailAdress;
                cmd.SelectCommand.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;

                DataTable dt = new DataTable();
                cmd.Fill(dt);

                int userId = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                string accountType = dt.Rows[0].ItemArray[1].ToString();
                string userName = dt.Rows[0].ItemArray[2].ToString();
                string firstName = dt.Rows[0].ItemArray[3].ToString();
                string lastName = dt.Rows[0].ItemArray[4].ToString();
                DateTime birthDate = Convert.ToDateTime(dt.Rows[0].ItemArray[5]);
                User.Gender gender = (User.Gender)Enum.Parse(typeof(User.Gender), dt.Rows[0].ItemArray[6].ToString());
                string address = dt.Rows[0].ItemArray[7].ToString();
                string postalCode = dt.Rows[0].ItemArray[8].ToString();
                string city = dt.Rows[0].ItemArray[9].ToString();
                bool status = Convert.ToBoolean(dt.Rows[0].ItemArray[10]);
                string hashedPassword = dt.Rows[0].ItemArray[11].ToString();


                if (!Hasher.SecurePasswordHasher.Verify(password, hashedPassword))
                    throw new ArgumentException("Password invalid");

                switch (accountType)
                {
                    case "Admin":
                        return new Admin(userId, User.AccountType.Admin, userName, firstName, lastName, birthDate, gender, emailAdress, address, city, postalCode,
                        hashedPassword, status);
                    case "Viewer":
                        return new Viewer(userId, User.AccountType.Viewer, userName, firstName, lastName, birthDate, gender, emailAdress, address, city, postalCode,
                        hashedPassword, status);
                    default:
                        throw new AggregateException("User not found");
                }

            }
            catch (Exception)
            {
                throw new ArgumentException("User cannot be checked");
            }
            finally
            {
                _conn.Close();
            }
        }

        public bool SendEmail(string emailaddress, string newPassword)
        {
            try
            {
                MailAddress fromAddress = new MailAddress("semester2labs.noreply@gmail.com", "NoReply StreamLabs");
                MailAddress toAddress = new MailAddress(emailaddress);
                const string subject = "New password";
                string body = "L.S.,\n" +
                                    "U heeft een nieuw wachtwoord aangevraagd!\n" +
                                    "Uw nieuwe wachtwoord is: " + newPassword + ".\n" +
                                    "Met vriendelijke groet,\n" +
                                    "Het administratorteam van StreamLabs";
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, "domww112")
                };
                using (MailMessage message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
