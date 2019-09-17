using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;
using Models;

namespace Logic
{
    public class StreamerLogic
    {
        private readonly IStreamerContext _streamer;

        public StreamerLogic(IStreamerContext streamer)
        {
            _streamer = streamer;
        }
    }
}
