using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace Projektarbete_VäderdataDB.Models.Tools
{
    class Utils
    {
        public static string TempInfoString(Temperature temp)
        {
            string tempInfo = $"{temp.ID}  {temp.Date}  {temp.Place}  {temp.Temp}  {temp.Humidity}";
            return tempInfo;
        }

        public static void InfoPlacer(int top, string a = "", string b = "", string c = "", string d = "", string e = "")
        {
            for (int x = 0; x < 5; x++)
            {
                switch (x)
                {
                    case 0:
                        SetCursorPosition(0, top);
                        Console.Write(a);
                        break;
                    case 1:
                        SetCursorPosition(15, top);
                        Console.Write(b);
                        break;
                    case 2:
                        SetCursorPosition(30, top);
                        Console.Write(c);
                        break;
                    case 3:
                        SetCursorPosition(45, top);
                        Console.Write(d);
                        break;
                    case 4:
                        SetCursorPosition(60, top);
                        Console.Write(e);
                        break;
                    default:
                        break;
                }
            }
        }

        public static void HeaderPlacer(int top, string a = "", string b = "", string c = "", string d = "", string e = "")
        {
            for (int x = 0; x < 5; x++)
            {
                switch (x)
                {
                    case 0:
                        SetCursorPosition(0, top);
                        Console.Write(a);
                        break;
                    case 1:
                        SetCursorPosition(15, top);
                        Console.Write(b);
                        break;
                    case 2:
                        SetCursorPosition(30, top);
                        Console.Write(c);
                        break;
                    case 3:
                        SetCursorPosition(45, top);
                        Console.Write(d);
                        break;
                    case 4:
                        SetCursorPosition(60, top);
                        Console.Write(e);
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine();
            Console.WriteLine("******************************************************************************************");

        }
    }
}
