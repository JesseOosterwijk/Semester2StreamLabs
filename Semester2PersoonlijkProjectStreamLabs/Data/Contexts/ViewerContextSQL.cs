using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Data.Interfaces;

namespace Data.Contexts
{
    public class ViewerContextSQL : IViewerContext
    {
        private readonly SqlConnection _conn = Connection.GetConnection();

    }
}
