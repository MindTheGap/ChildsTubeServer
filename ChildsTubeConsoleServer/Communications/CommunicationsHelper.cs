﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildsTubeConsoleServer.Communications
{
    public static class CommunicationsHelper
    {
        public enum UserToServerMessage
        {
            GetTvSeriesDetails,
            SearchPhrase
        }

        public enum ServerToUserMessage
        {
            OK,
            Error
        }
    }
}
