using System.Threading;
using uqac_ia_aspirobot.Agent;
using uqac_ia_aspirobot.Common;
using uqac_ia_aspirobot.Environment;

namespace uqac_ia_aspirobot
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main";
            var arConfig = new ArConfig
            {
                PipeName = "aspirobot-1.0",
                PipeServer = ".",
                ServerThreadCount = 1
            };

            EnvThread.Start(new EnvConfig
            {
                Height = 5,
                Width = 5,
                ActionPropability = 30.0f,
                AddDustProbability = 50.0f,
                AddJewelProbability = 50.0f,
//                RemoveJewelProbability = 50.0f,
                RemoveJewelProbability = 0,
                SleepTime = 1000
            }, arConfig);

            Thread.Sleep(2);

            AgAgent.Start(new AgConfig
            {
                SleepTime = 1000,
                StartX = 0,
                StartY = 0,
                AutoAdjustSleepTime = false
            }, arConfig);

            AgAgent.Join();
            EnvThread.Join();
        }
    }
}