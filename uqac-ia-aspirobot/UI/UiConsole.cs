using System;
using System.Runtime.InteropServices;
using uqac_ia_aspirobot.Agent;
using uqac_ia_aspirobot.Agent.Effectors;
using uqac_ia_aspirobot.Extensions;
using uqac_ia_aspirobot.Interfaces;

namespace uqac_ia_aspirobot.UI
{
    public class UiConsole : IUi
    {
        private readonly IEnvironment _environment;
        private readonly AgEngineEffector _agEngineEffector;
        private readonly AgPickedSensor _agPickedSensor;
        private readonly AgBatterySensor _agBatterySensor;
        private readonly AgVaccumSensor _agVaccumSensor;

        public UiConsole(IEnvironment environment, AgEngineEffector agEngineEffector, AgPickedSensor agPickedSensor,
            AgBatterySensor agBatterySensor, AgVaccumSensor agVaccumSensor)
        {
            _environment = environment;
            _agEngineEffector = agEngineEffector;
            _agPickedSensor = agPickedSensor;
            _agBatterySensor = agBatterySensor;
            _agVaccumSensor = agVaccumSensor;
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
                    if (state == RoomState.Clean)
                    {
                        color = null;
                    }
                    else if (state == RoomState.Dust)
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
                    Console.Write(_agEngineEffector.IsInPosition(x, y) ? "X" : " ");
                    SetBackgroundColor(null);
                }
                Console.WriteLine("|");
            }
            SetBackgroundColor(bgColor);
            Console.WriteLine($"Battery: {_agBatterySensor.Spent}");
            Console.WriteLine($"Vaccumed: {_agVaccumSensor.Vaccumed}");
            Console.WriteLine($"Picked: {_agPickedSensor.Picked}");
        }
    }
}