using Microsoft.Extensions.Options;
using uqac_ia_aspirobot.Extensions;

namespace uqac_ia_aspirobot.Agent.Effectors
{
    public class AgEngineEffector
    {
        private readonly AgBatterySensor _agBatterySensor;

        private readonly AgConfig _options;

        public int X { get; protected set; }

        public int Y { get; protected set; }

        public AgEngineEffector(IOptions<AgConfig> options, AgBatterySensor agBatterySensor)
        {
            _agBatterySensor = agBatterySensor;
            _options = options.Value;
            X = _options.StartX;
            Y = _options.StartY;
        }

        public void MoveTo(int x, int y)
        {
            _agBatterySensor.Add(this.Distance(x, y));
            X = x;
            Y = y;
        }
    }
}