using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Data.Contexts
{
    public class ChatContextSQL : IChatContext
    {
        private readonly SqlConnection _conn = Connection.GetConnection();

        public void SendChat()
        {

        }

        public void DeleteChat()
        {

        }

    }
}
