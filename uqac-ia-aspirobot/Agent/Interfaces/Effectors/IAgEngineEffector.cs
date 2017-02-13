namespace uqac_ia_aspirobot.Agent.Interfaces.Effectors
{
    public interface IAgEngineEffector
    {

        int X { get; }

        int Y { get; }

        void MoveTo(int x, int y);
    }
}