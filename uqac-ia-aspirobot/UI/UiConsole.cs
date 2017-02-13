using System;
using System.Runtime.InteropServices;
using uqac_ia_aspirobot.Agent;
using uqac_ia_aspirobot.Agent.Interfaces.Effectors;
using uqac_ia_aspirobot.Agent.Interfaces.Sensors;
using uqac_ia_aspirobot.Extensions;
using uqac_ia_aspirobot.Interfaces;

namespace uqac_ia_aspirobot.UI
{
    public class UiConsole : IUi
    {
        private readonly IEnvironment _environment;
        private readonly IAgEngineEffector _agEngineEffector;
        private readonly IAgPickedSensor _agPickedSensor;
        private readonly IAgBatterySensor _agBatterySensor;
        private readonly IAgVaccumSensor _agVaccumSensor;
        private readonly AgState _agState;

        public UiConsole(IEnvironment environment, IAgEngineEffector agEngineEffector, IAgPickedSensor agPickedSensor,
            IAgBatterySensor agBatterySensor, IAgVaccumSensor agVaccumSensor, AgState agState)
        {
            _environment = environment;
            _agEngineEffector = agEngineEffector;
            _agPickedSensor = agPickedSensor;
            _agBatterySensor = agBatterySensor;
            _agVaccumSensor = agVaccumSensor;
            _agState = agState;
        }

        public void SetBackgroundColor(ConsoleColor? color)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.BackgroundColor = color.GetValueOrDefault(ConsoleColor.Black);
            }
            else
            {
                var str = "";
                if (color == null)
                {
                    str = $"{(char)27}[1;49m";
                }
                if (color == ConsoleColor.Black)
                {
                    str = $"{(char)27}[1;40m";
                }
                if (color == ConsoleColor.Red)
                {
                    str = $"{(char)27}[1;41m";
                }
                if (color == ConsoleColor.Green)
                {
                    str = $"{(char)27}[1;42m";
                }
                if (color == ConsoleColor.Yellow)
                {
                    str = $"{(char)27}[1;43m";
                }
                if (color == ConsoleColor.Blue)
                {
                    str = $"{(char)27}[1;44m";
                }
                if (color == ConsoleColor.Magenta)
                {
                    str = $"{(char)27}[1;45m";
                }
                if (color == ConsoleColor.Cyan)
                {
                    str = $"{(char)27}[1;46m";
                }
                if (color == ConsoleColor.Gray)
                {
                    str = $"{(char)27}[1;47m";
                }
                Console.Write(str);
            }
        }

        public void SetForegroundColor(ConsoleColor? color)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.ForegroundColor = color.GetValueOrDefault(ConsoleColor.Black);
            }
            else
            {
                var str = "";
                if (color == null)
                {
                    str = $"{(char)27}[1;39m";
                }
                if (color == ConsoleColor.Black)
                {
                    str = $"{(char)27}[1;30m";
                }
                if (color == ConsoleColor.Red)
                {
                    str = $"{(char)27}[1;31m";
                }
                if (color == ConsoleColor.Green)
                {
                    str = $"{(char)27}[1;32m";
                }
                if (color == ConsoleColor.Yellow)
                {
                    str = $"{(char)27}[1;33m";
                }
                if (color == ConsoleColor.Blue)
                {
                    str = $"{(char)27}[1;34m";
                }
                if (color == ConsoleColor.Magenta)
                {
                    str = $"{(char)27}[1;35m";
                }
                if (color == ConsoleColor.Cyan)
                {
                    str = $"{(char)27}[1;36m";
                }
                if (color == ConsoleColor.Gray)
                {
                    str = $"{(char)27}[1;37m";
                }
                Console.Write(str);
            }
        }

        public void Clear()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.Clear();
            }
            else
            {
                Console.WriteLine();
            }
        }

        public void Update()
        {
            Clear();
            var bgColor = Console.BackgroundColor;
            for (var x = 0; x < _environment.GetWidth(); ++x)
            {
                Console.Write("|");
                for (var y = 0; y < _environment.GetHeight(); ++y)
                {
                    var state = _environment.GetRoomState(x, y);
                    ConsoleColor? color = null;
                    if (state == RoomState.Dust)
                    {
                        color = ConsoleColor.Gray;
                    }
                    else if (state == RoomState.Jewel)
                    {
                        color = ConsoleColor.Cyan;
                    }
                    else if (state == RoomState.Unknown)
                    {
                        color = ConsoleColor.Yellow;
                    }
                    else if (state == (RoomState.Jewel | RoomState.Dust))
                    {
                        color = ConsoleColor.Red;
                    }
                    SetBackgroundColor(color);
                    var isDest = _agState.Destination?.IsInPosition(x, y) ?? false;
                    if (_agEngineEffector.IsInPosition(x, y))
                    {
                        Console.Write("X");
                    }
                    else if (isDest)
                    {
                        SetForegroundColor(ConsoleColor.Red);
                        Console.Write("+");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                    SetForegroundColor(null);
                    SetBackgroundColor(null);
                }
                Console.WriteLine("|");
            }
            SetBackgroundColor(bgColor);
            Console.WriteLine($"Battery: {_agBatterySensor.Spent}");
            Console.WriteLine($"Vaccumed: {_agVaccumSensor.Vaccumed}");
            Console.WriteLine($"Picked: {_agPickedSensor.Picked}");
            Console.WriteLine($"Think: {_agState.ThinkTimeInterval}ms");
        }
    }
}