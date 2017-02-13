namespace uqac_ia_aspirobot.Agent.Interfaces.Sensors
{
    public interface IAgPickedSensor : ISensor
    {
        int Picked { get; }

        void Increase();
    }
}