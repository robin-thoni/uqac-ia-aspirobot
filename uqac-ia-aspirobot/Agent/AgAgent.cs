﻿using System;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using uqac_ia_aspirobot.Agent.FakeEnv;
using uqac_ia_aspirobot.Agent.FakeEnv.Effectors;
using uqac_ia_aspirobot.Agent.FakeEnv.Sensors;
using uqac_ia_aspirobot.Agent.Interfaces.Effectors;
using uqac_ia_aspirobot.Agent.Interfaces.Sensors;
using uqac_ia_aspirobot.Common;
using uqac_ia_aspirobot.Extensions;
using uqac_ia_aspirobot.Interfaces;
using uqac_ia_aspirobot.UI;

namespace uqac_ia_aspirobot.Agent
{
    public class AgAgent
    {
        private readonly IAgDustSensor _agDustSensor;
        private readonly IAgBatterySensor _agBatterySensor;
        private readonly IAgPickedSensor _agPickedSensor;
        private readonly IAgVaccumSensor _agVaccumSensor;

        private readonly IAgEngineEffector _engineEffector;

        private readonly IAgVaccumEffector _vaccumEffector;

        private readonly IEnvironment _environment;

        private readonly AgConfig _options;

        private readonly AgState _state;
        private readonly IUi _ui;

        private static Thread _thread;

        public static void Start(AgConfig agConfig, ArConfig arConfig)
        {
            var agServices = new ServiceCollection();
            agServices.AddOptions();
            agServices.AddSingleton<AgState>();
            agServices.AddSingleton<AgAgent>();
            agServices.AddSingleton<ArClient>();

            agServices.AddSingleton<IAgDustSensor, AgDustSensor>();
            agServices.AddSingleton<IAgBatterySensor, AgBatterySensor>();
            agServices.AddSingleton<IAgVaccumSensor, AgVaccumSensor>();
            agServices.AddSingleton<IAgPickedSensor, AgPickedSensor>();

            agServices.AddSingleton<IAgVaccumEffector, AgVaccumEffector>();
            agServices.AddSingleton<IAgEngineEffector, AgEngineEffector>();

            agServices.AddSingleton<IUi, UiConsole>();
            agServices.AddSingleton<IEnvironment, AgEnvironment>();
            agServices.AddTransient<IRoom, AgRoom>();
            agServices.Configure<AgConfig>(agConfig.CopyTo);
            agServices.Configure<ArConfig>(arConfig.CopyTo);
            var agProvider = agServices.BuildServiceProvider();
            var agThread = agProvider.GetService<AgAgent>();
            _thread = new Thread(agThread.Run)
            {
                Name = nameof(AgAgent)
            };
            _thread.Start();
        }

        public static void Join()
        {
            _thread.Join();
        }

        public AgAgent(IOptions<AgConfig> options,
            IEnvironment environment, AgState state, IUi ui,
            IAgDustSensor agDustSensor, IAgBatterySensor agBatterySensor, IAgPickedSensor agPickedSensor, IAgVaccumSensor agVaccumSensor,
            IAgEngineEffector engineEffector, IAgVaccumEffector vaccumEffector)
        {
            _agDustSensor = agDustSensor;
            _agBatterySensor = agBatterySensor;
            _agPickedSensor = agPickedSensor;
            _agVaccumSensor = agVaccumSensor;

            _engineEffector = engineEffector;
            _vaccumEffector = vaccumEffector;

            _environment = environment;
            _state = state;
            _ui = ui;

            _options = options.Value;
            _state.LastThinkTime = DateTime.MinValue;
            _state.ThinkTimeInterval = _options.ThinkTimeInterval;
        }

        private void Run()
        {
            var running = true;

            _environment.Setup();
            _state.DustyRooms = _environment.FindDustyRoomsWithoutJewel();

            while (running)
            {
                if (_options.SleepTime > 0)
                {
                    Thread.Sleep(_options.SleepTime);
                }
                UpdateSensors();
                _ui.Update();
                UpdateState();
                Think();
                Work();
            }
        }

        public void UpdateSensors()
        {
            _agDustSensor.Update();
            _agBatterySensor.Update();
            _agPickedSensor.Update();
            _agVaccumSensor.Update();
        }

        public void UpdateState()
        {
            if (_options.AutoAdjustThinkTimeInterval)
            {
                var dustyRooms = _environment.FindDustyRooms();
                if ((dustyRooms.Count < _state.DustyRooms.Count || !dustyRooms.Any()) && _state.ThinkTimeInterval < 7500)
                {
                    _state.ThinkTimeInterval += _state.ThinkTimeInterval / 2;
                }
                else if (dustyRooms.Count > _state.DustyRooms.Count && _state.ThinkTimeInterval > 100)
                {
                    _state.ThinkTimeInterval -= _state.ThinkTimeInterval / 2;
                }
                _state.DustyRooms = dustyRooms;
            }
            else
            {
                _state.DustyRooms = _environment.FindDustyRooms();
            }
        }

        public void Think()
        {
            if (_state.LastThinkTime.AddMilliseconds(_state.ThinkTimeInterval) <= DateTime.Now)
            {
                if (_state.Destination == null && _state.DustyRooms.Any())
                {
                    _state.Destination = _state.DustyRooms.OrderBy(room => room.Distance(_engineEffector)).First();
                }
                _state.LastThinkTime = DateTime.Now;
            }
        }

        public void Work()
        {
            if (_environment.GetRoomState(_engineEffector.X, _engineEffector.Y).HasFlag(RoomState.Jewel))
            {
                _vaccumEffector.Pick();
            }
            if (_environment.GetRoomState(_engineEffector.X, _engineEffector.Y).HasFlag(RoomState.Dust))
            {
                _vaccumEffector.Vaccum();
            }
            if (_state.Destination != null && _engineEffector.IsInRoom(_state.Destination))
            {
                _state.Destination = null;
            }
            if (_state.Destination != null)
            {
                if (_state.Destination.X < _engineEffector.X)
                {
                    _engineEffector.Move(-1, 0);
                }
                else if (_state.Destination.X > _engineEffector.X)
                {
                    _engineEffector.Move(1, 0);
                }
                else if (_state.Destination.Y < _engineEffector.Y)
                {
                    _engineEffector.Move(0, -1);
                }
                else if (_state.Destination.Y > _engineEffector.Y)
                {
                    _engineEffector.Move(0, 1);
                }
            }
        }
    }
}