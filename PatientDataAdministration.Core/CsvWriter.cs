using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;

namespace PatientDataAdministration.Core
{
    public class CommaSeparatedValuesWriter
    {
        public static void WriteToFile(IEnumerable<dynamic> list, string expectedFileName)
        {
            try
            {
                using (TextWriter writer =
                    new StreamWriter(expectedFileName, false, System.Text.Encoding.UTF8))
                {
                    var csv = new CsvWriter(writer);
                    csv.WriteRecords(list);
                    csv.Dispose();

                    writer.Close();
                }
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }
    }
}
