using System;
using Microsoft.Extensions.Options;
using System.IO.Pipes;
using System.Threading;
using uqac_ia_aspirobot.Extensions;
using uqac_ia_aspirobot.Interfaces;

namespace uqac_ia_aspirobot.Common
{
    public class ArServer
    {
        private readonly IEnvironment _environment;

        public enum ArCommands
        {
            EnvGetWidth,
            EnvGetHeight,
            RoomGetState,
            RoomRemoveDust,
            RoomRemoveJewel
        }

        private readonly ArConfig _options;

        private Thread[] _servers;

        public ArServer(IOptions<ArConfig> options, IEnvironment environment)
        {
            _environment = environment;
            _options = options.Value;
        }

        public void Setup()
        {
            _servers = new Thread[_options.ServerThreadCount];
            for (var i = 0; i < _servers.Length; i++)
            {
                _servers[i] = new Thread(ServerThread)
                {
                    Name = $"ArServer-{i}"
                };
                _servers[i].Start();
            }
        }

        private void ServerThread()
        {
            var server = new NamedPipeServerStream(_options.PipeName, PipeDirection.InOut, _servers.Length);
            server.WaitForConnection();

            var stream = new ArStreamString(server);

            while (true)
            {
                var command = stream.ReadEnum<ArCommands>();
                if (command == ArCommands.EnvGetWidth)
                {
                    stream.Write(_environment.GetWidth());
                }
                else if (command == ArCommands.EnvGetHeight)
                {
                    stream.Write(_environment.GetHeight());
                }
                else if (command == ArCommands.RoomGetState)
                {
                    var x = stream.ReadInt();
                    var y = stream.ReadInt();
                    stream.Write(_environment.GetRoomState(x, y));
                }
                else if (command == ArCommands.RoomRemoveDust)
                {
                    var x = stream.ReadInt();
                    var y = stream.ReadInt();
                    _environment.GetRoom(x, y).RemoveDust();
                }
                else if (command == ArCommands.RoomRemoveJewel)
                {
                    var x = stream.ReadInt();
                    var y = stream.ReadInt();
                    _environment.GetRoom(x, y).RemoveJewel();
                }
            }
        }
    }
}