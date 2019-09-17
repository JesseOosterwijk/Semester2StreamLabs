using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;
using Models;

namespace Logic
{
    public class ChatLogic
    {
        private readonly IChatContext _chat;

        public ChatLogic(IChatContext chat)
        {
            _chat = chat;
        }
    }
}
