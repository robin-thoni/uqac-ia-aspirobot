using uqac_ia_aspirobot.Agent.Interfaces.Sensors;

namespace uqac_ia_aspirobot.Agent.FakeEnv.Sensors
{
    public class AgVaccumSensor : IAgVaccumSensor
    {
        public int Vaccumed { get; protected set; }

        public void Increase()
        {
            ++Vaccumed;
        }

        public void Update()
        {
        }
    }
}