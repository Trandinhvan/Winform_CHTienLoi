using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace GUI
{
    public partial class frmDangNhap : Form
    {
        public static string tenDN = "";
        public static string matKhau = "";
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDN_Click(object sender, EventArgs e)

        {
            Program.sdt = txtDN.Text;
            QL_CHTLDataContext db = new QL_CHTLDataContext();
            try
            {
                tenDN = txtDN.Text;
                matKhau = txtMK.Text;
                NHANVIEN nv = db.NHANVIENs.Where(n => n.DIENTHOAI == txtDN.Text.Trim() && n.MK == txtMK.Text.Trim()).SingleOrDefault();
                if(nv!=null)
                {
                    Form1 frmMain = new Form1();
                    frmMain.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!");
                }
            }
            catch
            {
                MessageBox.Show("Đăng nhập thất bại! Kiểm tra lại chuỗi kết nối!");
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = XtraMessageBox.Show("Bạn có muốn thoát ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
