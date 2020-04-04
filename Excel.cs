using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _excel = Microsoft.Office.Interop.Excel;
using Microsoft.CSharp;

namespace Zillow.Services
{
    class Excel
    {
        string path = "";
        _Application excel = new _excel.Application();
        Workbook wb;
        Worksheet ws;

        public Excel(string path, int Sheet)
        {
            this.path = path;
            this.wb = excel.Workbooks.Open(path);
            this.ws = wb.Worksheets[Sheet];
        }

        public double readCell(int row, int col)
        {
            // EXCEL DOES NOT START FROM INDEX 0,0
            // IT STARTS FROM 1,1
            row++;
            col++;

            if (ws.Cells[row, col].Value2 != null)
            {
                return ws.Cells[row, col].Value2;
            }
            else
            {
                return 0;
            }
        }
    }
}
