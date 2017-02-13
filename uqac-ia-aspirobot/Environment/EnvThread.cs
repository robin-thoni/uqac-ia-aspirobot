using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using uqac_ia_aspirobot.Common;
using uqac_ia_aspirobot.Extensions;
using uqac_ia_aspirobot.Interfaces;

namespace uqac_ia_aspirobot.Environment
{
    public class EnvThread
    {
        private readonly IEnvironment _environment;

        private readonly ArServer _server;

        private readonly EnvConfig _config;

        private readonly Random _rand;

        private enum Actions
        {
            NoAction = 0,
            AddDust = 1,
            AddJewel = 2,
            RemoveJewel = 3
        }

        private static Thread _thread;

        public static void Start(EnvConfig envConfig, ArConfig arConfig)
        {
            var envServices = new ServiceCollection();
            envServices.AddOptions();
            envServices.AddSingleton<EnvThread>();
            envServices.AddSingleton<ArServer>();
            envServices.AddSingleton<IEnvironment, EnvEnvironment>();
            envServices.AddTransient<IRoom, EnvRoom>();
            envServices.Configure<EnvConfig>(envConfig.CopyTo);
            envServices.Configure<ArConfig>(arConfig.CopyTo);
            var envProvider = envServices.BuildServiceProvider();
            var envThread = envProvider.GetService<EnvThread>();
            _thread = new Thread(envThread.Run)
            {
                Name = nameof(EnvThread)
            };
            _thread.Start();
        }

        public static void Join()
        {
            _thread.Join();
        }

        public EnvThread(IOptions<EnvConfig> config, IEnvironment environment, ArServer server)
        {
            _environment = environment;
            _server = server;
            _config = config.Value;
            _rand = new Random();
        }

        private bool GenerateProba(float proba)
        {
            return _rand.NextDouble() * 100.0 <= proba;
        }

        private Actions GenerateAction()
        {
            if (GenerateProba(_config.ActionPropability))
            {
                if (GenerateProba(_config.AddDustProbability))
                {
                    return Actions.AddDust;
                }
                if (GenerateProba(_config.RemoveJewelProbability))
                {
                    return Actions.RemoveJewel;
                }
                if (GenerateProba(_config.AddJewelProbability))
                {
                    return Actions.AddJewel;
                }
            }
            return Actions.NoAction;
        }

        private void Run()
        {
            var running = true;

            _environment.Setup();
            _server.Setup();

            while (running)
            {
                if (_config.SleepTime > 0)
                {
                    Thread.Sleep(_config.SleepTime);
                }
                var action = GenerateAction();
                if (action == Actions.AddDust || action == Actions.AddJewel)
                {
                    var x = _rand.Next(0, _environment.GetWidth());
                    var y = _rand.Next(0, _environment.GetWidth());
                    if (action == Actions.AddDust)
                    {
                        _environment.AddDust(x, y);
                    }
                    else if (action == Actions.AddJewel)
                    {
                        _environment.AddJewel(x, y);
                    }
                }
                else if (action == Actions.RemoveJewel)
                {
                    _environment.ForeachRoom(room =>
                    {
                        if (room.State.HasFlag(RoomState.Jewel))
                        {
                            room.RemoveJewel();
                            return false;
                        }
                        return true;
                    });
                }
            }
        }
    }
}