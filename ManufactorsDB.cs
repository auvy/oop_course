using System;
namespace task2
{
    class AllManufactors
    {
        protected static Manufactor[] availableManufactors;

        public AllManufactors(Manufactor[] manufactors)
        {
            availableManufactors = manufactors;
        }
        public Manufactor[] GetAccess()
        {
            return availableManufactors;
        }
        public void printManufactors()
        {
            Manufactor m;
            for(int i = 0; i < availableManufactors.Length; i++)
            {
                m = availableManufactors[i];
                if(m.freeShare > 0)
                {
                    Console.WriteLine($"[{i+1}] {m.name}, {m.material}, exp of {m.experience}, share of {m.freeShare}.");
                }
            } 
        }  
    }
}