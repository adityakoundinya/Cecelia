using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;

namespace Cecelia {
    class Program {
        static void Main(string[] args) {
            CeceliaDataProvider dp = new CeceliaDataProvider();
            ApplicationClass applicationClass = new ApplicationClass();
            Workbook workBook = applicationClass.Workbooks.Open("C:\\Dainis - Master GF Database.xls", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Worksheet workSheet = (Worksheet)workBook.Worksheets.get_Item(1);
            Range range = workSheet.UsedRange;
            List<Product> products = new List<Product>();
            Product product;
            int rCnt = 0;
            //int cCnt = 0;
            Console.WriteLine("Extracting Products");
            for (rCnt = 2; rCnt < range.Rows.Count; rCnt++) {
                bool isSkip = false;
                product = new Product();
                try {
                    product.Category = (string)(range.Cells[rCnt, 1] as Range).Value2.ToString();
                } catch {
                    product.Category = string.Empty;
                }
                try {
                    product.CompanyName = (string)(range.Cells[rCnt, 2] as Range).Value2.ToString();
                } catch {
                    product.CompanyName = string.Empty;
                    isSkip = true;
                }
                try {
                    if (!isSkip)
                    product.Type1 = (string)(range.Cells[rCnt, 3] as Range).Value2.ToString();
                } catch {
                    product.Type1 = string.Empty;
                }
                try {
                    if (!isSkip)
                    product.Type2 = (string)(range.Cells[rCnt, 4] as Range).Value2.ToString();
                } catch {
                    product.Type2 = string.Empty;
                }
                try {
                    if (!isSkip)
                    product.Flavor = (string)(range.Cells[rCnt, 5] as Range).Value2.ToString();
                } catch {
                    product.Flavor = string.Empty;
                }
                product.LastUpdated = DateTime.Now;
                if (!isSkip) {
                    products.Add(product);
                }
            }
            Console.WriteLine(products.Count.ToString() + " Products Extracted");
            Console.WriteLine("Saving Products");
            int i = 0;
            foreach (Product p in products) {
                try {
                    dp.AddProduct(p);
                    i++;
                } catch { }
            }
            Console.WriteLine(i.ToString() + "Products Saved");
            workBook.Close(true, null, null);
            applicationClass.Quit();
            Console.Read();
        }
    }
    
}
//    Excel.Application xlApp;
        //    Excel.Workbook xlWorkBook;
        //    Excel.Worksheet xlWorkSheet;
        //    Excel.Range range;
        //    List<DupConference> conferences = new List<DupConference>();
        //    DupConference c = null;
        //    object misValue = System.Reflection.Missing.Value;
        //    string str;
        //    int rCnt = 0;
        //    int cCnt = 0;
        //    xlApp = new Excel.ApplicationClass();
        //    xlWorkBook = xlApp.Workbooks.Open("C:\\Daily_Video_Conferences326.xls", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
        //    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
        //    range = xlWorkSheet.UsedRange;
        //    for (rCnt = 5; rCnt <= range.Rows.Count; rCnt++) {
        //        str = (string)(range.Cells[rCnt, 1] as Excel.Range).Value2.ToString();
        //        if (str != " ") {
        //            c = new DupConference();
        //            c.Id = int.Parse((range.Cells[rCnt, 1] as Excel.Range).Value2.ToString());
        //            c.Date = (DateTime)(range.Cells[rCnt, 2] as Excel.Range).get_Value(Type.Missing);
        //            c.Setup = int.Parse((range.Cells[rCnt, 3] as Excel.Range).Value2.ToString());
        //            c.StartTime = DateTime.FromOADate((double)(range.Cells[rCnt, 4] as Excel.Range).get_Value(Type.Missing));
        //            c.Endtime = DateTime.FromOADate((double)(range.Cells[rCnt, 5] as Excel.Range).get_Value(Type.Missing));
        //            c.Title = (string)(range.Cells[rCnt, 6] as Excel.Range).Value2.ToString();
        //            c.ParticipantName = (string)(range.Cells[rCnt, 7] as Excel.Range).Value2.ToString();
        //            conferences.Add(c);
        //        }
        //    }