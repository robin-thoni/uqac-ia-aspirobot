using uqac_ia_aspirobot.Agent.Interfaces.Sensors;

namespace uqac_ia_aspirobot.Agent.FakeEnv.Sensors
{
    public class AgBatterySensor : IAgBatterySensor
    {
        public int Spent { get; protected set; }

        public void Add(int v)
        {
            if (v > 0)
            {
                Spent += v;
            }
        }

        public void Update()
        {
        }
    }
}