using Microsoft.Extensions.Options;
using uqac_ia_aspirobot.Agent.Interfaces.Effectors;
using uqac_ia_aspirobot.Agent.Interfaces.Sensors;
using uqac_ia_aspirobot.Extensions;

namespace uqac_ia_aspirobot.Agent.FakeEnv.Effectors
{
    public class AgEngineEffector : IAgEngineEffector
    {
        private readonly IAgBatterySensor _agBatterySensor;

        public int X { get; protected set; }

        public int Y { get; protected set; }

        public AgEngineEffector(IOptions<AgConfig> options, IAgBatterySensor agBatterySensor)
        {
            _agBatterySensor = agBatterySensor;
            X = options.Value.StartX;
            Y = options.Value.StartY;
        }

        public void MoveTo(int x, int y)
        {
            _agBatterySensor.Add(this.Distance(x, y));
            X = x;
            Y = y;
        }
    }
}