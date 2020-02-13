using System;
using ff14bot;

namespace UI_Checker
{
    public class ItemSearch
    {

        public static int GetCount(IntPtr windowPtr)
        {
            return Core.Memory.Read<byte>(windowPtr + 0x0aa4);
        }
        
        public static ResultLayout[] ReadResults(IntPtr windowPtr, int count)
        {
            return Core.Memory.ReadArray<ResultLayout>(windowPtr + 0x0ab8, count);
        }
    }
    
    public struct ResultLayout
    {
        public IntPtr itemPointer;
        public ushort ushort_0;
        public byte count;
        public byte byte1;
        public int int_1;
        public int int_2;
        public ushort ushort_1;
        public ushort ushort_2;
        public int int_3;
        public int int_4;
    }
}