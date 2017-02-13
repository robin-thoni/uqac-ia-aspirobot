using System;
using System.Collections.Generic;
using uqac_ia_aspirobot.Interfaces;

namespace uqac_ia_aspirobot.Agent
{
    public class AgState
    {
        public int ThinkTimeInterval { get; set; }

        public DateTime LastThinkTime { get; set; }

        public IList<IRoom> DustyRooms { get; set; }

        public IRoom Destination { get; set; }
    }
}