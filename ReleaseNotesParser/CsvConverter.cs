using System;
using System.Data;
using GenericParsing;
using Peasouper;
using CsvHelper;
using System.IO;

namespace ReleaseNotesParser
{
    public class CsvConverter
    {
        public CsvConverter()
        {
			//string pathOfCsvFile = @"C:\MyFile.csv";
			//GenericParserAdapter adapter = new GenericParserAdapter(pathOfCsvFile);
			//DataTable data = adapter.GetDataTable();
        }

        public static DataTable ConvertCsvToDataTable(string CsvPathFromConsole)
        {
			/*
			 * var csv = new CsvReader( textReader );
             * while( csv.Read() )
                {
                    var row = dt.NewRow();
                    foreach( DataColumn column in dt.Columns )
                    {
                        row[column.ColumnName] = csv.GetField( column.DataType, column.ColumnName );
                    }
                    dt.Rows.Add( row );
                }
            */



			string pathOfCsvFile = CsvPathFromConsole;
            //CsvHelper.CsvParser(pathOfCsvFile);

            while (pathOfCsvFile.Read())
            {
                
            }


            StreamReader streamReader = new StreamReader(CsvPathFromConsole);

            CsvParser parser = new CsvParser(streamReader);

            //GenericParserAdapter adapter = new GenericParserAdapter(pathOfCsvFile);
            //DataTable data = adapter.GetDataTable();

            DataTable table = csv.Read();

            return data;
        }
    }
}
