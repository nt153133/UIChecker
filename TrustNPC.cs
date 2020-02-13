using System.Linq;

namespace UI_Checker
{
    public class TrustNPC
    {
        

        public string Name { get; }
        public int Id1 { get; }
        public int Id2 { get; }
        public int ClassId { get; }

        public TrustNPC(string name, int id1, int id2, int classId)
        {
            Name = name;
            Id1 = id1;
            Id2 = id2;
            ClassId = classId;
        }


        public string Class()
        {
            int[] tank = new[] {3, 8};
            int[] dps = new[] {7,2,6};
            int[] heal = new[] {1, 5};

            if (tank.Contains(ClassId))
                return "Tank";
            
            if (dps.Contains(ClassId))
                return "DPS";
                            
            if (heal.Contains(ClassId))
                return "Healer";

            return "error";

        }
        
    }
}