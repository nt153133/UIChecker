using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using ff14bot;
using ff14bot.Managers;
using ff14bot.RemoteWindows;


namespace UI_Checker
{
    public static class LlamaUI
    {
        static LlamaUI()
        {
            NpcList = new List<TrustNPC>();
            
            NpcList.Add(new TrustNPC("Alphinaud", 82061, 82081, 1));
            NpcList.Add(new TrustNPC("Alisaie", 82062, 82082, 2));
            NpcList.Add(new TrustNPC("Thancred", 82063, 82083, 3));
            NpcList.Add(new TrustNPC("Minfilia", 82064, 82084, 4));
            NpcList.Add(new TrustNPC("Urianger", 82065, 82085, 5));
            NpcList.Add(new TrustNPC("Y'shtola", 82066, 82086, 6));
            NpcList.Add(new TrustNPC("Ryne", 82067, 82087, 7));
            NpcList.Add(new TrustNPC("Lyna", 82068, 82088, 8));
            NpcList.Add(new TrustNPC("Crystal Exarch", 82069, 82089, 9));
            NpcList.Add(new TrustNPC("Crystal Exarch", 82069, 82089, 9));
            NpcList.Add(new TrustNPC("Crystal Exarch", 82069, 82089, 9));
        }

        private static  string Name => "LlamaUI";
        private static int offset0 = 458;
        private static int offset2 = 352;
        public static List<TrustNPC> NpcList;
        
        public static void Log(string text)
        {
            Helpers.LogExternal(Name, text, Colors.PeachPuff);
        }
        
        public static TwoInt[] ___Elements(string name)
        {

            AtkAddonControl windowByName = RaptureAtkUnitManager.GetWindowByName(name);
            if (windowByName != null)
            {
                ushort elementCount = ElementCount(name);
                
                IntPtr addr = Core.Memory.Read<IntPtr>(windowByName.Pointer + offset2);
                return Core.Memory.ReadArray<TwoInt>(addr, elementCount);
            }
            return null;

        }
        
        public static ushort ElementCount(string name)
        {

            AtkAddonControl windowByName = RaptureAtkUnitManager.GetWindowByName(name);
            if (windowByName != null)
            {
                return Core.Memory.Read<ushort>(windowByName.Pointer + offset0);
            }
            return 0;

        }

        public static TrustNPC GetTrustNpc(int id)
        {
            return NpcList.FirstOrDefault(i => i.Id1 == id || i.Id2 == id);
        }
        
        
    }
}