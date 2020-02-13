using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ff14bot;
using ff14bot.Managers;
using ff14bot.RemoteWindows;
using ICSharpCode.SharpZipLib.Zip;

namespace UI_Checker
{
    public class MasterPieceSupply
    {
        private static readonly string WindowName = "MasterPieceSupply";

        public static bool IsOpen => RaptureAtkUnitManager.GetWindowByName(WindowName) != null;
        
        public static bool HasAgentInterfaceId => GetAgentInterfaceId() != 0;
        
        private static readonly int offset0 = 0x1CA;
        private static readonly int offset2 = 0x160;
/*      Can Also do this: Will pull the same offsets Mastahg stores in RB
        var off = typeof(Core).GetProperty("Offsets", BindingFlags.NonPublic | BindingFlags.Static);
        var struct158 = off.PropertyType.GetFields()[72];
        var offset0 = (int)struct158.FieldType.GetFields()[0].GetValue(struct158.GetValue(off.GetValue(null)));
        var offset2 = (int)struct158.FieldType.GetFields()[2].GetValue(struct158.GetValue(off.GetValue(null)));
*/
        

        public static int GetAgentInterfaceId()
        {
            AtkAddonControl windowByName = RaptureAtkUnitManager.GetWindowByName(WindowName);
            if (windowByName == null)
                return 0;
            
            var test = windowByName.TryFindAgentInterface();
            
            if (test == null)
                return 0;
            
            
            for (int i = 0; i < AgentModule.AgentPointers.Count; i++)
            {
                if (test.Pointer.ToInt64() == AgentModule.AgentPointers.ToArray()[i].ToInt64())
                    return i;
            }

            return 0;
        }

        public static int GetNumberOfTurnins() => IsOpen ? ___Elements()[0].TrimmedData : 0;

        public static List<Item> GetTurninItems()
        {
            List<Item> result = new List<Item>();

            ArraySegment<TwoInt> itemElements = new ArraySegment<TwoInt>(___Elements(), 87, GetNumberOfTurnins());

            foreach (var item in itemElements)
            {
                //LlamaUI.Log($"{item.TrimmedData}");
                result.Add(DataManager.GetItem((uint) (item.TrimmedData - 500000)));
            }

            return result;
        }
        
        public static Dictionary<Item,bool> GetTurninItemsStarred()
        {
            Dictionary<Item,bool> result = new Dictionary<Item, bool>();
            
            var itemElements = new ArraySegment<TwoInt>(___Elements(), 87, GetNumberOfTurnins()).ToArray();
            var starElements = new ArraySegment<TwoInt>(___Elements(), 447, GetNumberOfTurnins()).ToArray();

            for (int i = 0; i < GetNumberOfTurnins(); i++)
            {
                //LlamaUI.Log($"{itemElements[i].TrimmedData}   {DataManager.GetItem((uint) (itemElements[i].TrimmedData - 500000))}");
                result.Add(DataManager.GetItem((uint) (itemElements[i].TrimmedData - 500000)), (starElements[i].TrimmedData == 1));
            }

            return result;

        }

        public static int ClassSelected
        {
            get => ___Elements()[45].TrimmedData;
            set
            {
                var windowByName = RaptureAtkUnitManager.GetWindowByName(WindowName);
                if (windowByName != null && ___Elements()[45].TrimmedData != value)
                    windowByName.SendAction(2,1,2,1,(ulong) value);
            }
        }

        public static void ClickItem(int index)
        {
            var windowByName = RaptureAtkUnitManager.GetWindowByName(WindowName);
            if (windowByName != null)
                windowByName.SendAction(2,3,1,3,(ulong) index);
        }
        
        private static TwoInt[] ___Elements()
        {
            var windowByName = RaptureAtkUnitManager.GetWindowByName(WindowName);
            if (windowByName == null) return null;
            var elementCount = ElementCount();
            var addr = Core.Memory.Read<IntPtr>(windowByName.Pointer + offset2);
            return Core.Memory.ReadArray<TwoInt>(addr, elementCount);
        }

        private static ushort ElementCount()
        {
            var windowByName = RaptureAtkUnitManager.GetWindowByName(WindowName);
            return windowByName != null ? Core.Memory.Read<ushort>(windowByName.Pointer + offset0) : (ushort) 0;
        }

        private static int test()
        {
            return 0;
        }
    }
}