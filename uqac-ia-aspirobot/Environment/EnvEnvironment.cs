using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using uqac_ia_aspirobot.Interfaces;

namespace uqac_ia_aspirobot.Environment
{
    public class EnvEnvironment : IEnvironment
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly EnvConfig _options;

        private readonly IDictionary<string, IRoom> _rooms = new Dictionary<string, IRoom>();

        private string GetKey(int x, int y)
        {
            return $"{x},{y}";
        }

        public EnvEnvironment(IServiceProvider serviceProvider, IOptions<EnvConfig> options)
        {
            _serviceProvider = serviceProvider;
            _options = options.Value;
        }

        public void Setup()
        {
            for (var x = 0; x < _options.Width; ++x)
            {
                for (var y = 0; y < _options.Height; ++y)
                {
                    var room = _serviceProvider.GetService<IRoom>();
                    room.X = x;
                    room.Y = y;
                    _rooms.Add(GetKey(x, y), room);
                }
            }
        }

        public int GetWidth()
        {
            return _options.Width;
        }

        public int GetHeight()
        {
            return _options.Height;
        }

        public IRoom GetRoom(int x, int y)
        {
            return _rooms[GetKey(x, y)];
        }

        public void Update()
        {
        }
    }
}