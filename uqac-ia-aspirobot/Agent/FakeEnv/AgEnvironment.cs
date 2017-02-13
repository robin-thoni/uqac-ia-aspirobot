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
        private IAgDustSensor _agDustSensor;
        private IAgBatterySensor _agBatterySensor;
        private IAgPickedSensor _agPickedSensor;
        private IAgVaccumSensor _agVaccumSensor;
        private IAgPerformanceSensor _agPerformanceSensor;

        private readonly IServiceProvider _serviceProvider;

        private readonly ArClient _arClient;

        private int _width;

        private int _height;

        private readonly IDictionary<string, IRoom> _rooms = new Dictionary<string, IRoom>();

        public AgEnvironment(IServiceProvider serviceProvider, ArClient arClient)
        {
            _serviceProvider = serviceProvider;
            _arClient = arClient;
        }

        public void Setup()
        {
            _agDustSensor = _serviceProvider.GetService<IAgDustSensor>();
            _agBatterySensor = _serviceProvider.GetService<IAgBatterySensor>();
            _agPickedSensor = _serviceProvider.GetService<IAgPickedSensor>();
            _agVaccumSensor = _serviceProvider.GetService<IAgVaccumSensor>();
            _agPerformanceSensor = _serviceProvider.GetService<IAgPerformanceSensor>();

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
            _agPerformanceSensor.Update();
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