using System;
using System.Collections.Generic;
using System.IO;
using NLog;

namespace BusinessLayer.Parsers
{
    public class CsvParser : IParser<CsvLine>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public IEnumerable<CsvLine> ParseFile(string fileName)
        {
            try
            {
                var employeeName = new FileInfo(fileName).Name.Split('_')[0];
                var list = new List<CsvLine>(0);
                using (var stream = new StreamReader(fileName))
                {
                    string line;
                    while ((line = stream.ReadLine()) != null)
                        try
                        {
                            list.Add(new CsvLine(employeeName, line.Split(',')));
                        }
                        catch (Exception e)
                        {
                            Logger.Error($"The line <{line}> was not added. Cause: {e.Message}");
                        }
                }

                return list;
            }
            catch (Exception e)
            {
                Logger.Error($"Error while reading file: {e}");
            }

            return null;
        }
    }
}