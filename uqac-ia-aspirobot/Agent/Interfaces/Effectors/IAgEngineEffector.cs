namespace uqac_ia_aspirobot.Agent.Interfaces.Effectors
{
    public interface IAgEngineEffector : IEffector
    {

        int X { get; }

        int Y { get; }

        void MoveTo(int x, int y);
    }
}