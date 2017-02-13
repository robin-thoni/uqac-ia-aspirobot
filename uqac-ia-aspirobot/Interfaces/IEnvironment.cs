using System;

namespace uqac_ia_aspirobot.Interfaces
{
    [Flags]
    public enum RoomState
    {
        Unknown = -1,
        Clean = 0,
        Dust = 1,
        Jewel = 2
    }

    public interface IEnvironment
    {
        void Setup();

        int GetWidth();

        int GetHeight();

        IRoom GetRoom(int x, int y);

        void Update();
    }
}