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
using DAL_BAL;

namespace GUI
{
    public partial class frmNhapHang : DevExpress.XtraEditors.XtraForm
    {
        public frmNhapHang()
        {
            InitializeComponent();
        }
        NhaCungCap_DAL_BAL ncc = new NhaCungCap_DAL_BAL();
        private void frmNhapHang_Load(object sender, EventArgs e)
        {
            using (QL_CHTLDataContext db = new QL_CHTLDataContext())
            {
                cboTT.Enabled = false;
                cboNCC.Items.Add("");
                foreach (string tenncc in db.NHACCs.Select(t => t.TENNCC).ToList())
                {
                    cboNCC.Items.Add(tenncc);
                }
                cboTT.Items.Add("");
                cboTT.Items.Add("CÒN HÀNG");
                cboTT.Items.Add("HẾT HÀNG");
            }
        }

        private void cboNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNCC.Text == "")
            {
                cboTT.Enabled = false;
            }
            else
            {
                cboTT.Enabled = true;
            }
            using (QL_CHTLDataContext db = new QL_CHTLDataContext())
            {
                if (cboTT.Text == "")
                {
                    var data1 = from dt in db.HANGs.Where(hang => hang.MANCC == db.NHACCs.Where(t => t.TENNCC == cboNCC.Text).Select(y => y.MANCC).FirstOrDefault())
                                select new { dt.MAHG, dt.TENHG, dt.DVT, dt.SOLUONG, dt.MANCC, dt.DONGIA, dt.TINHTRANG };
                    dgvHang.DataSource = data1;
                }
                else
                {
                    var data1 = from dt in db.HANGs.Where(hang => hang.MANCC == db.NHACCs.Where(t => t.TENNCC == cboNCC.Text).Select(y => y.MANCC).FirstOrDefault() && hang.TINHTRANG == cboTT.Text)
                                select new { dt.MAHG, dt.TENHG, dt.DVT, dt.SOLUONG, dt.MANCC, dt.DONGIA, dt.TINHTRANG };
                    dgvHang.DataSource = data1;
                }
            }
        }

        private void cboTT_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (QL_CHTLDataContext db = new QL_CHTLDataContext())
            {
                if (cboTT.Text == "")
                {
                    var data1 = from dt in db.HANGs.Where(hang => hang.MANCC == db.NHACCs.Where(t => t.TENNCC == cboNCC.Text).Select(y => y.MANCC).FirstOrDefault())
                                select new { dt.MAHG, dt.TENHG, dt.DVT, dt.SOLUONG, dt.MANCC, dt.DONGIA, dt.TINHTRANG };
                    dgvHang.DataSource = data1;
                }
                else
                {
                    var data1 = from dt in db.HANGs.Where(hang => hang.MANCC == db.NHACCs.Where(t => t.TENNCC == cboNCC.Text).Select(y => y.MANCC).FirstOrDefault() && hang.TINHTRANG == cboTT.Text)
                                select new { dt.MAHG, dt.TENHG, dt.DVT, dt.SOLUONG, dt.MANCC, dt.DONGIA, dt.TINHTRANG };
                    dgvHang.DataSource = data1;
                }
            }
        }

        private void dgv_a_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<int> lst = new List<int>();
            List<int> lstPN = new List<int>();
            foreach (DataGridViewRow row in dgv_PhieuNhap.Rows)
            {
                lst.Add((int)row.Cells[4].Value);
            }
            lst = lst.Distinct().ToList();

            foreach (int i in lst)
            {
                PHIEUNHAP pn = new PHIEUNHAP();
                using (QL_CHTLDataContext db = new QL_CHTLDataContext())
                {
                    pn.NGAYNHAP = DateTime.Now;
                    pn.MANCC = i;
                    pn.TIENNHAP = 0;
                    db.PHIEUNHAPs.InsertOnSubmit(pn);
                    db.SubmitChanges();
                }
                lstPN.Add(pn.MAPN);
            }

            foreach (DataGridViewRow row in dgv_PhieuNhap.Rows)
            {
                CHITIETPN pn = new CHITIETPN();
                using (QL_CHTLDataContext db = new QL_CHTLDataContext())
                {
                    pn.MAHG = (int)row.Cells[0].Value;
                    pn.SOLUONG = int.Parse(row.Cells[3].Value.ToString());
                    pn.DONGIA = (double)row.Cells[5].Value;
                    pn.TONGCONG = pn.SOLUONG * pn.DONGIA;
                    pn.MAPN = lstPN[lst.IndexOf((int)row.Cells[4].Value)];
                    db.CHITIETPNs.InsertOnSubmit(pn);
                    db.SubmitChanges();
                }
            }
            MessageBox.Show("Nhập hàng thành công");
            dgv_PhieuNhap.Rows.Clear();
            using (QL_CHTLDataContext db = new QL_CHTLDataContext())
            {
                if (cboTT.Text == "")
                {
                    var data1 = from dt in db.HANGs.Where(hang => hang.MANCC == db.NHACCs.Where(t => t.TENNCC == cboNCC.Text).Select(y => y.MANCC).FirstOrDefault())
                                select new { dt.MAHG, dt.TENHG, dt.DVT, dt.SOLUONG, dt.MANCC, dt.DONGIA, dt.TINHTRANG };
                    dgvHang.DataSource = data1;
                }
                else
                {
                    var data1 = from dt in db.HANGs.Where(hang => hang.MANCC == db.NHACCs.Where(t => t.TENNCC == cboNCC.Text).Select(y => y.MANCC).FirstOrDefault() && hang.TINHTRANG == cboTT.Text)
                                select new { dt.MAHG, dt.TENHG, dt.DVT, dt.SOLUONG, dt.MANCC, dt.DONGIA, dt.TINHTRANG };
                    dgvHang.DataSource = data1;
                }
            }
        }

        private void dgvHang_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)dgvHang.SelectedRows[0].Clone();
            row.Cells[0].Value = dgvHang.SelectedRows[0].Cells[0].Value;
            row.Cells[1].Value = dgvHang.SelectedRows[0].Cells[1].Value;
            row.Cells[2].Value = dgvHang.SelectedRows[0].Cells[2].Value;
            row.Cells[3].Value = 1;
            row.Cells[4].Value = dgvHang.SelectedRows[0].Cells[4].Value;
            row.Cells[5].Value = dgvHang.SelectedRows[0].Cells[5].Value;
            row.Cells[6].Value = dgvHang.SelectedRows[0].Cells[6].Value;
            dgv_PhieuNhap.Rows.Add(row);
        }
    }
}