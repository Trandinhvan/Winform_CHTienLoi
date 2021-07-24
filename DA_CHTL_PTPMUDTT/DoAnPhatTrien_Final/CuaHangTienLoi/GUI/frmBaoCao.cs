using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using DevExpress.XtraSpreadsheet;
using DevExpress.Spreadsheet;
using System.Diagnostics;

namespace GUI
{
    public partial class frmBaoCao : DevExpress.XtraEditors.XtraForm
    {
        public frmBaoCao()
        {
            InitializeComponent();
        }

        private void frmBaoCao_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Properties.Settings.Default.QL_CHTLConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Hoadontheongay";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.Add(new SqlParameter("@Ngayban", dateTimePicker1.Value.Date));
            DataSet ds = new DataSet();
            SqlDataAdapter dap = new SqlDataAdapter(cmd);
            dap.Fill(ds);
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            reportViewer1.LocalReport.ReportPath = "Baocaohd.rdlc";
            if (ds.Tables[0].Rows.Count > 0)
            {
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "DataSet1";
                rds.Value = ds.Tables[0];
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.RefreshReport();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            using (QL_CHTLDataContext db = new QL_CHTLDataContext())
            {
                var dt = from hd in db.HOADONs
                         where hd.NGAYBAN.Equals(dateTimePicker1.Value.Date)
                         select new { hd.MAHD, hd.NGAYBAN, hd.THANHTOAN };

                SpreadsheetControl spreadsheetControl1 = new SpreadsheetControl();
                IWorkbook workbook = spreadsheetControl1.Document;
                workbook.LoadDocument("D:\\Final_DOTNET\\DA_CHTL_PTPMUDTT\\DoAnPhatTrien_Final\\BaoCaoNgay.xlsx", DocumentFormat.Xlsx);
                Worksheet sheet = workbook.Worksheets[0];
                workbook.BeginUpdate();

                try
                {
                    ExternalDataSourceOptions options = new ExternalDataSourceOptions() { ImportHeaders = true };
                    // Bắt đầu ghi từ column thứ 7
                    Table table = sheet.Tables.Add(dt, 7, 1, options);
                    TableStyleCollection tableStyles = workbook.TableStyles;
                    TableStyle tableStyle = tableStyles[BuiltInTableStyleId.TableStyleMedium2]; // Đổi style table ở đây

                    // Apply the table style to the existing table.
                    table.Style = tableStyle;

                    table.Columns[0].Name = "Mã Hóa Đơn";
                    table.Columns[1].Name = "Ngày Bán";
                    table.Columns[2].Name = "Thanh Toán";
                    //TableColumn subtotalColumn = table.Columns.Add();
                    //subtotalColumn.Name = "Thành tiền";
                    //subtotalColumn.Formula = "=[Số lượng] * [Đơn giá]";
                    table.Columns[2].TotalRowFunction = TotalRowFunction.Sum;
                    table.ShowTotals = true;
                    table.Columns[1].TotalRowLabel = "TỔNG CỘNG";
                    //table.Columns[0].TotalRowLabel = "";
                    table.Columns[2].DataRange.NumberFormat = "$#,##0.00";
                    // sử dụng custom style
                    //TableStyle customTableStyle = workbook.TableStyles.Add("testTableStyle");
                    //TableStyleElement totalRowStyle = customTableStyle.TableStyleElements[TableStyleElementType.TotalRow];
                    //customTableStyle.BeginUpdate();
                    //totalRowStyle.Fill.BackgroundColor = Color.Green;
                    //totalRowStyle.Font.Color = Color.White;
                    //totalRowStyle.Font.Bold = true;
                    //customTableStyle.EndUpdate();

                    //table.Style = customTableStyle;

                    // sheet.MergeCells(sheet.Range["B2764:G2764"]);

                }
                finally
                {
                    workbook.EndUpdate();
                }

                spreadsheetControl1.SaveDocument("BaoCaoNgay4.xlsx", DocumentFormat.Xlsx);
                Process.Start("BaoCaoNgay4.xlsx");
            }
        }
    }
}