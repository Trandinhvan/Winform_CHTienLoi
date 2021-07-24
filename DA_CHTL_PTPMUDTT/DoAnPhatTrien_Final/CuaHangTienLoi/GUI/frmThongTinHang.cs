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
    public partial class frmThongTinHang : DevExpress.XtraEditors.XtraForm
    {
        public frmThongTinHang()
        {
            InitializeComponent();
        }
        NhaCungCap_DAL_BAL ncc = new NhaCungCap_DAL_BAL();
        HangHoa_DAL_BAL hanghoa = new HangHoa_DAL_BAL();
        private void frmThongTinHang_Load(object sender, EventArgs e)
        {
            cb_nhacc.DataSource = ncc.loadNCC();
            cb_nhacc.DisplayMember = "TENNCC";
            cb_nhacc.ValueMember = "MANCC";
            dataGridView1.DataSource = hanghoa.LoadSP();
            dataGridView1.Columns[8].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFIle = new OpenFileDialog();
            openFIle.Filter = openFIle.Filter = "JPG files (*.jpg)|*.jpg|All files (*.*)|*.*";
            openFIle.FilterIndex = 1;
            openFIle.RestoreDirectory = true;
            if (openFIle.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFIle.FileName;
                txtlinkanh.Text = openFIle.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtdongia.Text == "" || txt_dvt.Text == "" || txtlinkanh.Text == "" || txtMaHag.Text == "" || txt_soluong.Text == "" || txtTenHang.Text == "")
            {
                if (hanghoa.ThemHang(txtTenHang.Text, int.Parse(cb_nhacc.SelectedValue.ToString()), int.Parse(txt_soluong.Text), double.Parse(txtdongia.Text), txt_dvt.Text, txtlinkanh.Text))
                {
                    MessageBox.Show("Thêm Hàng Thành Công");
                    dataGridView1.DataSource = hanghoa.LoadSP();
                }
                else
                {
                    MessageBox.Show("Thêm hàng thất bại");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầu đủ thông tin");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int a = int.Parse(txtMaHag.Text.Trim());
            if (hanghoa.xoaHang(a))
            {
                MessageBox.Show("Xóa thành công");
                dataGridView1.DataSource = hanghoa.LoadSP();
            }
            else
            {
                MessageBox.Show("Xóa thất bại");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtdongia.Text == "" || txt_dvt.Text == "" || txtlinkanh.Text == "" || txtMaHag.Text == "" || txt_soluong.Text == "" || txtTenHang.Text == "")
            {
                if (hanghoa.SuaHang(int.Parse(txtMaHag.Text),txtTenHang.Text, int.Parse(cb_nhacc.SelectedValue.ToString()), int.Parse(txt_soluong.Text), double.Parse(txtdongia.Text), txt_dvt.Text, txtlinkanh.Text))
                {
                    MessageBox.Show("Sửa thông tin Hàng Thành Công");
                    dataGridView1.DataSource = hanghoa.LoadSP();
                }
                else
                {
                    MessageBox.Show("Sửa thông tin hàng thất bại");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầu đủ thông tin");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            using (QL_CHTLDataContext db = new QL_CHTLDataContext())
            {
                txtMaHag.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                txtTenHang.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txtlinkanh.Text = System.IO.Directory.GetCurrentDirectory() + "\\Images\\" + db.HANGs.Where(t => t.MAHG == (int)dataGridView1.SelectedRows[0].Cells[0].Value).Select(t => t.ANH).FirstOrDefault();
                pictureBox1.Image = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + "\\Images\\" + db.HANGs.Where(t => t.MAHG == (int)dataGridView1.SelectedRows[0].Cells[0].Value).Select(t => t.ANH).FirstOrDefault());
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                txt_dvt.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                txt_soluong.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                txtdongia.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                cb_nhacc.Text = db.NHACCs.Where(y => y.MANCC == (int)dataGridView1.SelectedRows[0].Cells[4].Value).Select(t => t.TENNCC).FirstOrDefault();
            }
        }
    }
}