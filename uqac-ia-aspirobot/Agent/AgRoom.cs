using uqac_ia_aspirobot.Common;
using uqac_ia_aspirobot.Extensions;
using uqac_ia_aspirobot.Interfaces;

namespace uqac_ia_aspirobot.Agent
{
    public class AgRoom : IRoom
    {
        private readonly ArClient _arClient;

        public int X { get; set; }

        public int Y { get; set; }

        public RoomState State { get; protected set; }

        public AgRoom(ArClient arClient)
        {
            _arClient = arClient;
            State = RoomState.Unknown;
        }

        public void AddDust()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveDust()
        {
            _arClient.RemoveDust(this);
        }

        public void AddJewel()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveJewel()
        {
            _arClient.RemoveJewel(this);
        }

        public void Update()
        {
            State = _arClient.GetRoomState(this);
        }
    }
}