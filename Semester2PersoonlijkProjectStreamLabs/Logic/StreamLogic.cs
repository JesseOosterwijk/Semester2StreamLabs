using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;
using Models;

namespace Logic
{
    public class StreamLogic
    {
        private readonly IStreamContext _stream;

        public StreamLogic(IStreamContext stream)
        {
            _stream = stream;
        }
    }
}
