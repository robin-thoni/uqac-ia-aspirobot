using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using uqac_ia_aspirobot.Agent.Interfaces.Sensors;
using uqac_ia_aspirobot.Common;
using uqac_ia_aspirobot.Extensions;
using uqac_ia_aspirobot.Interfaces;

namespace uqac_ia_aspirobot.Agent.FakeEnv
{
    public class AgEnvironment : IEnvironment
    {
        private readonly IAgDustSensor _agDustSensor;
        private readonly IAgBatterySensor _agBatterySensor;
        private readonly IAgPickedSensor _agPickedSensor;
        private readonly IAgVaccumSensor _agVaccumSensor;

        private readonly IServiceProvider _serviceProvider;

        private readonly ArClient _arClient;

        private int _width;

        private int _height;

        private readonly IDictionary<string, IRoom> _rooms = new Dictionary<string, IRoom>();

        public AgEnvironment(IServiceProvider serviceProvider, ArClient arClient,
            IAgDustSensor agDustSensor, IAgBatterySensor agBatterySensor, IAgPickedSensor agPickedSensor, IAgVaccumSensor agVaccumSensor)
        {
            _serviceProvider = serviceProvider;
            _arClient = arClient;
            _agDustSensor = agDustSensor;
            _agBatterySensor = agBatterySensor;
            _agPickedSensor = agPickedSensor;
            _agVaccumSensor = agVaccumSensor;
        }

        public void Setup()
        {
            _arClient.Setup();
            _width = _arClient.GetEnvWidth();
            _height = _arClient.GetEnvWidth();
            for (var x = 0; x < _width; ++x)
            {
                for (var y = 0; y < _height; ++y)
                {
                    var room = _serviceProvider.GetService<IRoom>();
                    room.X = x;
                    room.Y = y;
                    _rooms.Add(GetKey(x, y), room);
                }
            }
        }

        public void Update()
        {
            this.ForeachRoom<AgRoom>(room =>
            {
                room.Update();
                return true;
            });

            _agDustSensor.Update();
            _agBatterySensor.Update();
            _agPickedSensor.Update();
            _agVaccumSensor.Update();
        }

        protected string GetKey(int x, int y)
        {
            return $"{x},{y}";
        }

        public int GetWidth()
        {
            return _width;
        }

        public int GetHeight()
        {
            return _height;
        }

        public IRoom GetRoom(int x, int y)
        {
            return _rooms[GetKey(x, y)];
        }
    }
}