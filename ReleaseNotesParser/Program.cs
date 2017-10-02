using System;
using System.Data;
using System.Collections.Generic;
using System.IO;

using Peasouper.Domain;
//using Peasouper.FogBugzClient;
using CsvHelper;

namespace ReleaseNotesParser
{
    class MainClass
    {
        //public static CsvReader m_CsvReader;
        //List<string> bugNumbersInCurrentVersion = new List<string>();
        public static Peasouper.FogBugzClient m_fbClient;

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Enter path to CSV: ");
            string path = GetCsvPath();


            Console.WriteLine("Enter version number in the format stated in the notes (eg 2017.3.0b1): ");
            string versionNumber = GetVersionNumber();


            DataTable dataFromCsv = CsvReader.ConvertCsvToDataTable(path);
            List<string> listOfBugsInVersion = GetBugsInVersion(versionNumber, dataFromCsv);
            List<Case> listOfCases = GetInfoFromFogbugz(listOfBugsInVersion);
            TranslateFogbugzInfoToTable(listOfCases);
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
            List<string> listOfBugsInVersion = new List<string>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                DataColumn versionColumn = table.Columns[0];
                DataColumn bugNumberColumn = table.Columns[1];

                string versionInCell = row[versionColumn].ToString();

				if (versionInCell == versionNumber)
				{
                    listOfBugsInVersion.Add(row[bugNumberColumn].ToString());
				}
            }

            return listOfBugsInVersion;
        }

        public static List<Case> GetInfoFromFogbugz(List<string> listOfBugNumbers)
        {
            List<Case> listOfCases = new List<Case>();
            foreach (string bugNumber in listOfBugNumbers)
            {
                Case fbCase = m_fbClient.GetCase((CaseId.FromInt(Convert.ToInt32(bugNumber)).Value));
                listOfCases.Add(fbCase);

                // fbCase.AssignedTo; << use this to put necessary info into a new datatable/csv
            }
            return listOfCases;
        }

        public static void TranslateFogbugzInfoToTable(List<Case> listOfCases)
        {
            DataTable generatedTableOfCases = new DataTable();

			StreamWriter streamWriter = new StreamWriter("hopefullythisworks.csv");

			CsvWriter csv = new CsvWriter(streamWriter);
            foreach (Case fbCase in listOfCases)
            {
                //generatedTableOfCases.Rows.Add();

                // case id - column one
                string title = fbCase.Title; //column 2
                csv.WriteField(title);
                string assignee = fbCase.AssignedTo.ToString(); //column 3
                csv.WriteField(assignee);
                string milestone = fbCase.FixFor.ToString(); //column 4
                csv.WriteField(milestone);

                //backports?
            }

			//create datatable
            //generate csv
			/*
             * using( var dt = new DataTable() )
                {
                    dt.Load( dataReader );
                    foreach( DataColumn column in dt.Columns )
                    {
                        csv.WriteField( column.ColumnName );
                    }
                    csv.NextRecord();

                    foreach( DataRow row in dt.Rows )
                    {
                        for( var i = 0; i < dt.Columns.Count; i++ )
                        {
                            csv.WriteField( row[i] );
                        }
                        csv.NextRecord();
                    }
                }
            */

        }



    }
}
