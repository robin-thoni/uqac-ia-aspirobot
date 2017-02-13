using uqac_ia_aspirobot.Agent.Interfaces.Sensors;
using uqac_ia_aspirobot.Interfaces;

namespace uqac_ia_aspirobot.Agent.FakeEnv.Sensors
{
    public class AgDustSensor : IAgDustSensor
    {
        private readonly AgEnvironment _environment;

        public AgDustSensor(IEnvironment environment)
        {
            _environment = environment as AgEnvironment;
        }

        public void Update()
        {
        }
    }
}