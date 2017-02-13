using uqac_ia_aspirobot.Agent.Interfaces.Sensors;
using uqac_ia_aspirobot.Extensions;
using uqac_ia_aspirobot.Interfaces;

namespace uqac_ia_aspirobot.Agent.Sensors
{
    public class AgPerformanceSensor : IAgPerformanceSensor
    {
        private readonly IAgBatterySensor _agBatterySensor;

        private readonly IAgPickedSensor _agPickedSensor;

        private readonly IAgVaccumSensor _agVaccumSensor;
        private readonly IEnvironment _environment;

        public float Performance { get; protected set; }

        public AgPerformanceSensor(IAgBatterySensor agBatterySensor, IAgPickedSensor agPickedSensor,
            IAgVaccumSensor agVaccumSensor, IEnvironment environment)
        {
            _agBatterySensor = agBatterySensor;
            _agPickedSensor = agPickedSensor;
            _agVaccumSensor = agVaccumSensor;
            _environment = environment;
        }

        public void Update()
        {
            var dustyCount = _environment.FindDustyRooms().Count;
            var sum = _agPickedSensor.Picked + _agVaccumSensor.Vaccumed;
            Performance = (_agBatterySensor.Spent == 0 ? 0 : (float)sum / _agBatterySensor.Spent) - dustyCount;
        }
    }
}