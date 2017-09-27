using System;
using System.Data;
using GenericParsing;

namespace ReleaseNotesParser
{
    public class CsvReader
    {
        public CsvReader()
        {
			string pathOfCsvFile = @"C:\MyFile.csv";
			GenericParserAdapter adapter = new GenericParserAdapter(pathOfCsvFile);
			DataTable data = adapter.GetDataTable();
        }

        public static DataTable ConvertCsvToDataTable(string CsvPathFromConsole)
        {
            string pathOfCsvFile = CsvPathFromConsole;
			GenericParserAdapter adapter = new GenericParserAdapter(pathOfCsvFile);
			DataTable data = adapter.GetDataTable();
            return data;
        }
    }
}
