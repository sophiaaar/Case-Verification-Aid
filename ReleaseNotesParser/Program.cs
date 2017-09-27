using System;
using System.Data;
using System.Collections.Generic;

namespace ReleaseNotesParser
{
    class MainClass
    {
        //public static CsvReader m_CsvReader;
        //List<string> bugNumbersInCurrentVersion = new List<string>();

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Enter path to CSV: ");
            string path = GetCsvPath();


            Console.WriteLine("Enter version number in the format stated in the notes (eg 2017.3.0b1): ");
            string versionNumber = GetVersionNumber();


            DataTable dataFromCsv = CsvReader.ConvertCsvToDataTable(path);
            GetBugsInVersion(versionNumber, dataFromCsv);
        }

        public static string GetCsvPath()
        {
            string CsvPath = Console.ReadLine();
            return CsvPath;
        }

		public static string GetVersionNumber()
		{
			string versionNumber = Console.ReadLine();
            return versionNumber;
		}

        public static List<string> GetBugsInVersion(string versionNumber, DataTable table)
        {
            List<string> ListOfBugsInVersion = new List<string>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                DataColumn versionColumn = table.Columns[0];
                DataColumn bugNumberColumn = table.Columns[1];

                string versionInCell = row[versionColumn].ToString();

				if (versionInCell == versionNumber)
				{
                    ListOfBugsInVersion.Add(row[bugNumberColumn].ToString());
				}
            }

            return ListOfBugsInVersion;
        }
    }
}
