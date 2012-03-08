using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;

namespace Cecelia {
    class Program {
        static Range range;
        static void Main(string[] args) {

            CeceliaDataProvider dp = new CeceliaDataProvider();
            ApplicationClass applicationClass = new ApplicationClass();
            Workbook workBook = applicationClass.Workbooks.Open("C:\\2012_RX_Final.xls", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Worksheet workSheet = (Worksheet)workBook.Worksheets.get_Item(1);
            range = workSheet.UsedRange;
            List<Product> products = new List<Product>();
            List<Product> unAddedRows = new List<Product>();
            Product product;
            int rCnt = 0;
            Console.WriteLine("Extracting Products");
            for (rCnt = 1; rCnt < range.Rows.Count; rCnt++) {
                product = new Product();
                product.Category = GetValueFromCell(rCnt, 1);
                product.CompanyName = GetValueFromCell(rCnt, 2);
                product.Type1 = GetValueFromCell(rCnt, 3);
                product.Type2 = GetValueFromCell(rCnt, 4);
                product.Flavor = GetValueFromCell(rCnt, 5);
                product.LastUpdated = DateTime.Now;
                if (product.CompanyName == string.Empty && product.Type1 == string.Empty && product.Type2 == string.Empty && product.Flavor == string.Empty) {
                    product.Id = rCnt;
                    unAddedRows.Add(product);
                    continue;
                }
                products.Add(product);

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

        private static string GetValueFromCell(int row, int cell) {
            string value = string.Empty;
            if ((range.Cells[row, cell] as Range).Value2 != null) {
                value = (range.Cells[row, cell] as Range).Value2.ToString();
            }
            return value;
        }
    }

}