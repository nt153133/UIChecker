
using System;
using System.Collections.Generic;
using System.Windows.Media;
using ff14bot;
using ff14bot.Helpers;
using ff14bot.Managers;

namespace UI_Checker
{
    public static class Helpers
    {
        internal static void LogExternal(string caller, string message, Color color)
        {
            Logging.Write(color, $"[{caller}]" + message);
        }
        
        private static void test()
        {
            var director = DirectorManager.ActiveDirector.Pointer;
            int DDMapGroup = 0x1E4A;
            int Map5xStart = 0x1450;
            int WallStartingPoint = 0x332;
            int Map5xSize = 0x338;
            int Starting = 0x140;
            int WallGroupEnabled = 0x1E30;
            int UNK_StartingCircle = 0x13C;



            var v187A = Core.Memory.Read<byte>(director + DDMapGroup);
            Log($"v187A (DDMapGroup) {v187A}");


//var v187A = Core.Memory.Read<byte>(director + Offsets.DDMapGroup);

            var v3 = director + Map5xStart + v187A * Map5xSize;
            var v332 = Core.Memory.Read<ushort>(v3 + WallStartingPoint);
            Log($"v332 {v332}");

            var v29 = v3 + 0x10;
            var v7_location = v29;


            var v7 = Core.Memory.ReadArray<short>(v7_location, 5);
            Log($"v7 count {v7.Length}");
            //var wallset = new HashSet<uint>();

            var v5 = 0;

            var types = new uint[] {1, 2, 4, 8}; //taken from the client

            for (var v30 = 5; v30 > 1; v30--)
            {
                for (var v8 = 0; v8 < 5; v8++)
                {
                    if (v7[v8] != 0)
                    {
                        var v9 = v3 + 0x14 * (v7[v8] - v332);

                        // var wall = Core.Memory.Read<uint>(v9 + Offsets.UNK_StartingCircle);
                        //wallset.Add(wall);

                        var @byte = Core.Memory.Read<byte>(director + v5 + WallGroupEnabled);
                        var walls = Core.Memory.ReadArray<uint>(v9 + Starting, 4);
                        //Log($"Walls count {walls.Length}");
                        //Log($"Walls byte {@byte}");
                        for (var v16 = 0; v16 < 4; v16++)
                        {
                            if (walls[v16] < 2)
                                continue;

                            if ((@byte & types[v16]) != 0) //==0 is closed != 0 is "open"
                                Log($"Would Add wall {walls[v16]}");

                        } //for3
                    }

                    v5++;
                }

                v7_location = v29 + 0xc;
                v7 = Core.Memory.ReadArray<short>(v7_location, 5);
                v29 = v29 + 0xc;
            }

        }
        
        private static void Log(string a)
        {}
    }
    

}