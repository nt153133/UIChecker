using System;
using ff14bot;

namespace UI_Checker
{
    public class ItemSearchResult
    {
        public static SearchResultLayout[] ReadSearchResults()
        {
            GreyMagic.PatternFinder patternFinder = new GreyMagic.PatternFinder(Core.Memory);
            var off = patternFinder.Find("48 8B 0D ? ? ? ? 0F 95 C2 E8 ? ? ? ? C7 44 24 ? ? ? ? ? Add 3 TraceRelative");
            var addr1 = Core.Memory.Read<IntPtr>(off);
            var addr2 = Core.Memory.Read<IntPtr>(addr1 + 0x308);
            var addr3 = Core.Memory.Read<IntPtr>(addr2 + 0xA8);
            var finalAddr = addr3 + 0x310;
            var value = finalAddr + (0x1f6 * 4); //1fc 2nd 202 3rd
            var resultCount = finalAddr + 0x7d4; 
            IntPtr value1;
            int count = Core.Memory.Read<int>(resultCount);
            
            return Core.Memory.ReadArray<SearchResultLayout>(value, count);
        }
        
        public static void test()
        {
            GreyMagic.PatternFinder patternFinder = new GreyMagic.PatternFinder(Core.Memory);
            var off = patternFinder.Find("48 8B 0D ? ? ? ? 0F 95 C2 E8 ? ? ? ? C7 44 24 ? ? ? ? ? Add 3 TraceRelative");
            var addr1 = Core.Memory.Read<IntPtr>(off);
            var addr2 = Core.Memory.Read<IntPtr>(addr1 + 0x308);
            var addr3 = Core.Memory.Read<IntPtr>(addr2 + 0xA8);
            var finalAddr = addr3 + 0x310;
            var value = finalAddr + (0x1f6 * 4); //1fc 2nd 202 3rd
            var resultCount = finalAddr + 0x7d4; 
            IntPtr value1;
            int count = Core.Memory.Read<int>(resultCount);
            
            
            for (int index = 0; index < count; index++)
            {
                value1 = value + (index * 0x18);
              //  Log(Core.Memory.Read<int>(value1));//Price
               // Log(Core.Memory.Read<int>(value1 + 4));//Qty
               // Log(Core.Memory.Read<int>(value1 + 8));//HQ
               // Log(Core.Memory.Read<int>(value1 + 12));//icon
            }
           // Log(Core.Memory.Read<int>(resultCount));
        }
        
        public struct SearchResultLayout
        {
            public int price;
            public int qty;
            public int HQ;
            public int icon;
            public int int_1;
            public int int_2;
        }
    }
    
    
}