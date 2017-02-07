using System.IO.Pipes;
using Microsoft.Extensions.Options;
using uqac_ia_aspirobot.Interfaces;

namespace uqac_ia_aspirobot.Common
{
    public class ArClient
    {
        private readonly ArConfig _options;

        private NamedPipeClientStream _client;

        private ArStreamString _stream;

        public ArClient(IOptions<ArConfig> options)
        {
            _options = options.Value;
        }

        public void Setup()
        {
            _client = new NamedPipeClientStream(_options.PipeServer, _options.PipeName, PipeDirection.InOut);
            _client.Connect();
            _stream = new ArStreamString(_client);
        }

        public int GetEnvWidth()
        {
            _stream.Write(ArServer.ArCommands.EnvGetWidth);
            return _stream.ReadInt();
        }

        public RoomState GetRoomState(int x, int y)
        {
            _stream.Write(ArServer.ArCommands.RoomGetState);
            _stream.Write(x);
            _stream.Write(y);
            return _stream.ReadEnum<RoomState>();
        }

        public void RemoveDust(int x, int y)
        {
            _stream.Write(ArServer.ArCommands.RoomRemoveDust);
            _stream.Write(x);
            _stream.Write(y);
        }

        public void RemoveJewel(int x, int y)
        {
            _stream.Write(ArServer.ArCommands.RoomRemoveJewel);
            _stream.Write(x);
            _stream.Write(y);
        }
    }
}