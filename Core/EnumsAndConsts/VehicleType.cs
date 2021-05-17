using System;
using System.Collections.Generic;
using System.Text;

namespace Core.EnumsAndConsts
{
    public class VehicleType
    {
        public static int LARGEBUS = 1;
        public static int MINIBUS = 2;
        public static int SCHOOLBUS = 3; 
        public static int LIMOUSINE = 4; 
        public static int VAN = 5; 

        public static string GetName(int id)
        {

            if(id == 1)
            {
                return "Large Bus";
            }
            else if (id == 2)
            {
                return "Mini Bus";
            }
            else if (id == 3)
            {
                return "School Bus";
            }
            else if (id == 4)
            {
                return "Limousine";
            }
            else if (id == 5)
            {
                return "Van";
            }
            else
            {
                return "Invalid Option";
            }
        }

        public static int GetId(string name)
        {

            if (name == "LARGEBUS")
            {
                return LARGEBUS;
            }
            else if (name == "MINIBUS")
            {
                return MINIBUS;
            }
            else if (name == "SCHOOLBUS")
            {
                return SCHOOLBUS;
            }
            else if (name == "LIMOUSINE")
            {
                return LIMOUSINE;
            }
            else if (name == "VAN")
            {
                return VAN;
            }
            else
            {
                return 0;
            }
        }
    }
}
