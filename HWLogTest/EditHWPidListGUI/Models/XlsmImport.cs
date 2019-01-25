using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditHWPidListGUI.Models
{
    public static class XlsmImport
    {
        public static List<Dictionary<string, string>> ReadExcelFile(string PathToXLSX, string SheetName = "First")
        {
            List<Dictionary<string, string>> RadioTableFromExcel = new List<Dictionary<string, string>>();
            //using OfficeOpenXml; (EPPlus)
            using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(PathToXLSX)))
            {
                var myWorksheet = xlPackage.Workbook.Worksheets.First();
                if (SheetName != "First")
                     myWorksheet = xlPackage.Workbook.Worksheets[SheetName]; //select sheet here

                var totalRows = myWorksheet.Dimension.End.Row;
                var totalColumns = myWorksheet.Dimension.End.Column;

                string[] ColNames = new string[totalColumns];

                for (int rowNum = 1; rowNum <= totalRows; rowNum++) //select starting row here
                {
                    var row = myWorksheet.Cells[rowNum, 1, rowNum, totalColumns].Select(c => c.Value == null ? string.Empty : c.Value.ToString());

                    //Start by reading the titles
                    if (rowNum == 1)
                    {
                        int i = 0;
                        foreach (var c in row)
                        {
                            ColNames[i] = c;
                            i++;
                        }
                    }
                    else //Add column values in current row
                    {
                        int i = 0;
                        Dictionary<string, string> ColValue = new Dictionary<string, string>();
                        foreach (var c in row)
                        {
                            ColValue.Add(ColNames[i], c);
                            i++;
                        }
                        RadioTableFromExcel.Add(ColValue);
                    }
                }
            }
            return RadioTableFromExcel;
        }
    }
}
