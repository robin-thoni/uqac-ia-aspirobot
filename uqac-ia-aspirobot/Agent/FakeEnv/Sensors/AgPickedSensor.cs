using uqac_ia_aspirobot.Agent.Interfaces.Sensors;

namespace uqac_ia_aspirobot.Agent.FakeEnv.Sensors
{
    public class AgPickedSensor : IAgPickedSensor
    {
        public int Picked { get; protected set; }

        public void Increase()
        {
            ++Picked;
        }

        public void Update()
        {
        }
    }
}