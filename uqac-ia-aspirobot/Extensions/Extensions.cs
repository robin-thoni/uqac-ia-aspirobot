using System;
using System.Collections.Generic;
using System.Linq;
using uqac_ia_aspirobot.Agent.Effectors;
using uqac_ia_aspirobot.Common;
using uqac_ia_aspirobot.Interfaces;

namespace uqac_ia_aspirobot.Extensions
{
    public static class Extensions
    {
        public static void AddDust(this IEnvironment env, int x, int y)
        {
            env.GetRoom(x, y).AddDust();
        }

        public static void RemoveDust(this IEnvironment env, int x, int y)
        {
            env.GetRoom(x, y).RemoveDust();
        }

        public static void AddJewel(this IEnvironment env, int x, int y)
        {
            env.GetRoom(x, y).AddJewel();
        }

        public static void RemoveJewel(this IEnvironment env, int x, int y)
        {
            env.GetRoom(x, y).RemoveJewel();
        }

        public static RoomState GetRoomState(this IEnvironment env, int x, int y)
        {
            return env.GetRoom(x, y).State;
        }

        public static bool ForeachRoom<T>(this IEnvironment env, Func<T, bool> action)
            where T : IRoom
        {
            for (var x = 0; x < env.GetWidth(); ++x)
            {
                for (var y = 0; y < env.GetHeight(); ++y)
                {
                    if (!action((T) env.GetRoom(x, y)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool ForeachRoom(this IEnvironment env, Func<IRoom, bool> action)
        {
            return env.ForeachRoom<IRoom>(action);
        }

        public static IList<T> FindDustyRooms<T>(this IEnvironment env)
            where T : IRoom
        {
            var rooms = new List<T>();
            ForeachRoom<T>(env, room =>
            {
                if (room.State.HasFlag(RoomState.Dust))
                {
                    rooms.Add(room);
                }
                return true;
            });
            return rooms;
        }

        public static IList<IRoom> FindDustyRooms(this IEnvironment env)
        {
            return env.FindDustyRooms<IRoom>();
        }

        public static IList<T> FindDustyRoomsWithoutJewel<T>(this IEnvironment env)
            where T : IRoom
        {
            var rooms = new List<T>();
            ForeachRoom<T>(env, room =>
            {
                if (room.State.CanBeVaccumed())
                {
                    rooms.Add(room);
                }
                return true;
            });
            return rooms;
        }

        public static IList<IRoom> FindDustyRoomsWithoutJewel(this IEnvironment env)
        {
            return env.FindDustyRoomsWithoutJewel<IRoom>();
        }

        public static RoomState GetRoomState(this ArClient client, IRoom room)
        {
            return client.GetRoomState(room.X, room.Y);
        }

        public static void RemoveDust(this ArClient client, IRoom room)
        {
            client.RemoveDust(room.X, room.Y);
        }

        public static void RemoveJewel(this ArClient client, IRoom room)
        {
            client.RemoveJewel(room.X, room.Y);
        }

        public static int Distance(this IRoom room, int x, int y)
        {
            return Math.Abs(room.X - x) + Math.Abs(room.Y - y);
        }

        public static int Distance(this IRoom room, AgEngineEffector effector)
        {
            return room.Distance(effector.X, effector.Y);
        }

        public static int Distance(this AgEngineEffector effector, int x, int y)
        {
            return Math.Abs(effector.X - x) + Math.Abs(effector.Y - y);
        }

        public static int Distance(this AgEngineEffector effector, IRoom room)
        {
            return effector.Distance(room.X, room.Y);
        }

        public static bool CanBeVaccumed(this RoomState state)
        {
            return state.HasFlag(RoomState.Dust) && !state.HasFlag(RoomState.Jewel);
        }

        public static bool IsInPosition(this AgEngineEffector engine, int x, int y)
        {
            return engine.X == x && engine.Y == y;
        }

        public static bool IsInRoom(this AgEngineEffector engine, IRoom room)
        {
            return engine.IsInPosition(room.X, room.Y);
        }

        public static void Move(this AgEngineEffector engine, int dx, int dy)
        {
            engine.MoveTo(engine.X + dx, engine.Y + dy);
        }
    }
}