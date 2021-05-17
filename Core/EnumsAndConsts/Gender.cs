using System;
using System.Collections.Generic;
using System.Text;

namespace Core.EnumsAndConsts
{
    public class Gender
    {
        public static int Male = 1;
        public static int Female = 2;
        public static int Other = 3; 

        public static string GetName(int id)
        {

            if(id == 1)
            {
                return "Male";
            }
            else if (id == 2)
            {
                return "Female";
            }
            else if (id == 3)
            {
                return "Others";
            }
            else
            {
                return "Invalid Option";
            }
        }
            
    }
}
