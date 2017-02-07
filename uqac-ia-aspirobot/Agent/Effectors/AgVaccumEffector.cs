using uqac_ia_aspirobot.Common;
using uqac_ia_aspirobot.Extensions;
using uqac_ia_aspirobot.Interfaces;

namespace uqac_ia_aspirobot.Agent.Effectors
{
    public class AgVaccumEffector
    {
        private readonly AgEngineEffector _engineEffector;
        private readonly ArClient _arClient;
        private readonly IEnvironment _environment;
        private readonly AgBatterySensor _agBatterySensor;
        private readonly AgVaccumSensor _agVaccumSensor;
        private readonly AgPickedSensor _agPickedSensor;

        public AgVaccumEffector(AgEngineEffector engineEffector, ArClient arClient, IEnvironment environment,
            AgBatterySensor agBatterySensor, AgVaccumSensor agVaccumSensor, AgPickedSensor agPickedSensor)
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