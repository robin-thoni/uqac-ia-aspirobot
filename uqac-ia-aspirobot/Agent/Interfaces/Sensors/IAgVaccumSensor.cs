namespace uqac_ia_aspirobot.Agent.Interfaces.Sensors
{
    public interface IAgVaccumSensor : ISensor
    {
        int Vaccumed { get; }

        void Increase();
    }
}