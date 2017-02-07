using uqac_ia_aspirobot.Interfaces;

namespace uqac_ia_aspirobot.Environment
{
    public class EnvRoom : IRoom
    {
        public int X { get; set; }

        public int Y { get; set; }

        public RoomState State { get; protected set; }

        public void AddDust()
        {
            State |= RoomState.Dust;
        }

        public void RemoveDust()
        {
            State &= ~RoomState.Dust;
        }

        public void AddJewel()
        {
            State |= RoomState.Jewel;
        }

        public void RemoveJewel()
        {
            State &= ~RoomState.Jewel;
        }
    }
}