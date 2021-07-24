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
using System.Text.RegularExpressions;
using DevExpress.XtraReports.UI;

namespace GUI
{
    public partial class frmQLHoaDon : DevExpress.XtraEditors.XtraForm
    {
        public frmQLHoaDon()
        {
            InitializeComponent();
        }

        private void frmQLHoaDon_Load(object sender, EventArgs e)
        {
            using (QL_CHTLDataContext db = new QL_CHTLDataContext())
            {
                var hoadon = from hd in db.HOADONs
                             select new { hd.MAHD, hd.MAKH, hd.MANV, hd.NGAYBAN, hd.TAMTINH, hd.THANHTOAN, hd.GIAMGIA };
                dataGridView1.DataSource = hoadon;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rptHoaDon rpt = new rptHoaDon();
            rpt.FilterString = "[MAHD] = '" + txt_mahd.Text + "'";
            rpt.CreateDocument();
            rpt.ShowPreviewDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_mahd.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        }
    }
}