using uqac_ia_aspirobot.Agent.Interfaces.Effectors;
using uqac_ia_aspirobot.Agent.Interfaces.Sensors;
using uqac_ia_aspirobot.Common;
using uqac_ia_aspirobot.Extensions;
using uqac_ia_aspirobot.Interfaces;

namespace uqac_ia_aspirobot.Agent.FakeEnv.Effectors
{
    public class AgVaccumEffector : IAgVaccumEffector
    {
        private readonly IAgEngineEffector _engineEffector;
        private readonly ArClient _arClient;
        private readonly IEnvironment _environment;
        private readonly IAgBatterySensor _agBatterySensor;
        private readonly IAgVaccumSensor _agVaccumSensor;
        private readonly IAgPickedSensor _agPickedSensor;

        public AgVaccumEffector(IAgEngineEffector engineEffector, ArClient arClient, IEnvironment environment,
            IAgBatterySensor agBatterySensor, IAgVaccumSensor agVaccumSensor, IAgPickedSensor agPickedSensor)
        {
            _engineEffector = engineEffector;
            _arClient = arClient;
            _environment = environment;
            _agBatterySensor = agBatterySensor;
            _agVaccumSensor = agVaccumSensor;
            _agPickedSensor = agPickedSensor;
        }

        public void Vaccum()
        {
            _agBatterySensor.Add(1);
            _agVaccumSensor.Increase();
            _environment.RemoveDust(_engineEffector.X, _engineEffector.Y);
        }

        public void Pick()
        {
            _agBatterySensor.Add(1);
            _agPickedSensor.Increase();
            _arClient.RemoveJewel(_engineEffector.X, _engineEffector.Y);
        }
    }
}