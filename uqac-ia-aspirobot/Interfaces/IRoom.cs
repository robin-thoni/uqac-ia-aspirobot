namespace uqac_ia_aspirobot.Interfaces
{
    public interface IRoom
    {
        int X { get; set; }

        int Y { get; set; }

        RoomState State { get; }

        void AddDust();

        void RemoveDust();

        void AddJewel();

        void RemoveJewel();
    }
}