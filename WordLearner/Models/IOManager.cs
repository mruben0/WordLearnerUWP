using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordLearner.Models
{
    public class IOManager
    {

        public bool IsValidFormat(string format, string path)
        {
            if (path.Length >= format.Length)
            {
                return false;
            }

            string pathFormat = path.Substring(path.Length - format.Length, format.Length);

            if (pathFormat == format)
            {
                return true;
            }
            else { return false; }
        }

        public List<string> AddToList(string path, string label = "A")
        {
            List<string> list = new List<string>();

            using (SpreadsheetDocument document =
                SpreadsheetDocument.Open(path, false))
            {
                WorkbookPart wbPart = document.WorkbookPart;
                int wbPartRowCount = document.WorkbookPart.WorksheetParts.First().Worksheet.Elements<SheetData>().First().Elements<Row>().Count();

                Sheet theSheet = wbPart.Workbook.Descendants<Sheet>().First();

                if (theSheet == null)
                {
                    throw new ArgumentException("sheetName");
                }

                WorksheetPart wsPart = (WorksheetPart)(wbPart.GetPartById(theSheet.Id));

                for (int i = 1; i <= wbPartRowCount; i++)
                {
                    list.Add(GetCellData(label,i,wsPart, wbPart));
                }
            }
            return list;
        }

        public string GetCellData(string label, int i, WorksheetPart wsPart, WorkbookPart wbPart)
        {
            string value = "";
            Cell theCell = wsPart.Worksheet.Descendants<Cell>().
               Where(c => c.CellReference == $"{label}{i}").FirstOrDefault();

            // If the cell does not exist, return an empty string.
            if (theCell != null)
            {
                value = theCell.InnerText;

                // If the cell represents an integer number, you are done. 
                // For dates, this code returns the serialized value that 
                // represents the date. The code handles strings and 
                // Booleans individually. For shared strings, the code 
                // looks up the corresponding value in the shared string 
                // table. For Booleans, the code converts the value into 
                // the words TRUE or FALSE.
                if (theCell.DataType != null)
                {
                    switch (theCell.DataType.Value)
                    {
                        case CellValues.SharedString:

                            // For shared strings, look up the value in the
                            // shared strings table.
                            var stringTable =
                                wbPart.GetPartsOfType<SharedStringTablePart>()
                                .FirstOrDefault();

                            // If the shared string table is missing, something 
                            // is wrong. Return the index that is in
                            // the cell. Otherwise, look up the correct text in 
                            // the table.
                            if (stringTable != null)
                            {
                                value =
                                    stringTable.SharedStringTable
                                    .ElementAt(int.Parse(value)).InnerText;
                            }
                            else value = "00";
                            break;

                        case CellValues.Boolean:
                            switch (value)
                            {
                                case "0":
                                    value = "FALSE";
                                    break;
                                default:
                                    value = "TRUE";
                                    break;
                            }
                            break;
                    }

                }
            }
            else value = "--";
            return value;
        }
    }
}