using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Data.Contexts
{
    public class StreamContextSQL : IStreamContext
    {
        private readonly SqlConnection _conn = Connection.GetConnection();

        public void MakeNewStream()
        {

        }

        public void EditStream()
        {

        }

        public void DeleteStream()
        {

        }

    }
}
