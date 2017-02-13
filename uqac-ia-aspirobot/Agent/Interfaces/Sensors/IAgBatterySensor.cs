namespace uqac_ia_aspirobot.Agent.Interfaces.Sensors
{
    public interface IAgBatterySensor : ISensor
    {
        int Spent { get; }

        void Add(int v);
    }
}