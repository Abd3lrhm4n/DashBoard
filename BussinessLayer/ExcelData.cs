using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.IO;
using DataAccessLayer;
using System.Runtime.InteropServices;

namespace BussinessLayer
{
    public class ExcelData
    {
        public string ReadExcel(string FileName, int SheetNum)
        {
            Application xlApp = new Application();
            Workbook xlWorkbook = xlApp.Workbooks.Open(FileName);
            _Worksheet xlWorksheet = xlWorkbook.Sheets[SheetNum];
            Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            try
            {
                for (int i = 1; i < rowCount; i++)
                {
                    for (int j = 1; j < 2; j++)
                    {
                        if (xlRange.Cells[i + 1, j] != null && xlRange.Cells[i + 1, j].Value2 != null)
                        {
                            BL.InsertItem(
                            new Item()
                            {
                                BarCode = xlRange.Cells[i + 1, 1].Value2.ToString(),
                                Name = xlRange.Cells[i + 1, 2].Value2.ToString(),
                                Price = Convert.ToDecimal(xlRange.Cells[i + 1, 3].Value2)
                            });

                        }
                    }
                }

                //cleanup
                GC.Collect();
                GC.WaitForPendingFinalizers();

                //release com objects to fully kill excel process from running in the background
                Marshal.ReleaseComObject(xlRange);
                Marshal.ReleaseComObject(xlWorksheet);

                //close and release
                xlWorkbook.Close();
                Marshal.ReleaseComObject(xlWorkbook);

                //quit and release
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);
                return "تم قرآءه الملف بنجاح";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }
    }
}
