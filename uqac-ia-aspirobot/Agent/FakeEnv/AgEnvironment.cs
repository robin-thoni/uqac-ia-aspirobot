using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using uqac_ia_aspirobot.Common;
using uqac_ia_aspirobot.Extensions;
using uqac_ia_aspirobot.Interfaces;

namespace uqac_ia_aspirobot.Agent.FakeEnv
{
    public class AgEnvironment : IEnvironment
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly ArClient _arClient;

        protected int _width;

        protected int _height;

        protected readonly IDictionary<string, IRoom> _rooms = new Dictionary<string, IRoom>();

        public AgEnvironment(IServiceProvider serviceProvider, ArClient arClient)
        {
            _serviceProvider = serviceProvider;
            _arClient = arClient;
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