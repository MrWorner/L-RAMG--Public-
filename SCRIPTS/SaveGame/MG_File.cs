////////////////////////////////////////////////////////////////////////////////
//
//	MG_File.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using GTA;
using System.Collections.Generic;
using System.IO;

namespace MG_Liquidator
{


    public static class MG_File
    {
        //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-write-to-a-text-file
        //https://stackoverflow.com/questions/1126915/how-do-i-split-a-string-by-a-multi-character-delimiter-in-c


        public static string HistoryFile { get; set; } = @"scripts\HW_LIQUIDATOR2021\MG_Liquidator2021_History.db";

        public static void SaveProgress(string totalCompletedAssasinations)
        {
            //UI.ShowSubtitle("SAVING");
            string[] lines = { totalCompletedAssasinations };
            System.IO.File.WriteAllLines(@"scripts\HW_LIQUIDATOR2021\MG_Liquidator2021.db", lines);
            //UI.ShowSubtitle("SAVED!");
        }

        public static void SaveHistory(string history)
        {

            Directory.CreateDirectory(@"scripts\HW_LIQUIDATOR2021");
            TextWriter tw = new StreamWriter(HistoryFile, true);
            //foreach (string s in history)
            tw.WriteLine(history);
            tw.Close();
        }

        public static void Debug(string text)
        {
            string[] lines = { text };
            System.IO.File.WriteAllLines(@"scripts\HW_LIQUIDATOR2021\DEBUG.db", lines);
        }

        public static void Debug(string[] lines, string dbName)
        {

            System.IO.File.WriteAllLines(@"scripts\HW_LIQUIDATOR2021\DEBUG_" + dbName + ".db", lines);
        }

        public static string[] LoadProgress()
        {
            string[] lines;

            if (File.Exists(@"scripts\\HW_LIQUIDATOR2021\\MG_Liquidator2021.db"))
            {
                lines = System.IO.File.ReadAllLines(@"scripts\\HW_LIQUIDATOR2021\\MG_Liquidator2021.db");
                return lines;
            }
            else
            {
                lines = new string[] { "0" };
            }

            return lines;
        }

        public static int GetLineCountsInHistoryDB()
        {
            int lines;

            if (File.Exists(@"scripts\\HW_LIQUIDATOR2021\\MG_Liquidator2021_History.db"))
            {
                return System.IO.File.ReadAllLines(@"scripts\\HW_LIQUIDATOR2021\\MG_Liquidator2021_History.db").Length;
           
            }
            else
            {
                lines = 0;
            }

            return lines;
        }

    }
}
