using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.Export.Xl;
using DevExpress.XtraSpreadsheet;
using DevExpress.Spreadsheet;
using System.Diagnostics;

namespace DAL_BAL
{
    public class Export_Execel
    {
        QL_CHTLDataContext db = new QL_CHTLDataContext();
        //public void xuatExcle(DateTime ngayban)
        //{
        //    var dt = from hd in db.HOADONs
        //             where hd.NGAYBAN.Equals(ngayban)
        //             select new { hd.MAHD, hd.NGAYBAN, hd.THANHTOAN };


        //    SpreadsheetControl spreadsheetControl1 = new SpreadsheetControl();
        //    IWorkbook workbook = spreadsheetControl1.Document;
        //    workbook.LoadDocument("C:\\Users\\Admin\\Desktop\\BaoCaoNgay.xlsx", DocumentFormat.Xlsx);
        //    Worksheet sheet = workbook.Worksheets["Sheet1"];
        //    workbook.BeginUpdate();

        //    try
        //    {
        //        ExternalDataSourceOptions options = new ExternalDataSourceOptions() { ImportHeaders = true };
        //        // Bắt đầu ghi từ column thứ 7
        //        Table table = sheet.Tables.Add(dt, 7, 1, options);
        //        TableStyleCollection tableStyles = workbook.TableStyles;
        //        TableStyle tableStyle = tableStyles[BuiltInTableStyleId.TableStyleMedium2]; // Đổi style table ở đây

        //        // Apply the table style to the existing table.
        //        table.Style = tableStyle;
                
        //        table.Columns[0].Name = "Mã Hóa Đơn";
        //        table.Columns[1].Name = "Ngày Bán";
        //        table.Columns[2].Name = "Thanh Toán";
        //        //TableColumn subtotalColumn = table.Columns.Add();
        //        //subtotalColumn.Name = "Thành tiền";
        //        //subtotalColumn.Formula = "=[Số lượng] * [Đơn giá]";
        //        table.Columns[2].TotalRowFunction = TotalRowFunction.Sum;
        //        table.ShowTotals = true;
        //        table.Columns[1].TotalRowLabel = "TỔNG CỘNG";
        //        //table.Columns[0].TotalRowLabel = "";
        //        table.Columns[2].DataRange.NumberFormat = "$#,##0.00";
        //        // sử dụng custom style
        //        //TableStyle customTableStyle = workbook.TableStyles.Add("testTableStyle");
        //        //TableStyleElement totalRowStyle = customTableStyle.TableStyleElements[TableStyleElementType.TotalRow];
        //        //customTableStyle.BeginUpdate();
        //        //totalRowStyle.Fill.BackgroundColor = Color.Green;
        //        //totalRowStyle.Font.Color = Color.White;
        //        //totalRowStyle.Font.Bold = true;
        //        //customTableStyle.EndUpdate();

        //        //table.Style = customTableStyle;

        //        // sheet.MergeCells(sheet.Range["B2764:G2764"]);
        //        }

            
        //    finally
        //    {
        //        workbook.EndUpdate();
        //    }

        //    spreadsheetControl1.SaveDocument("BaoCaoNgay4.xlsx", DocumentFormat.Xlsx);
        //    Process.Start("BaoCaoNgay4.xlsx");
        //}
    }
}
