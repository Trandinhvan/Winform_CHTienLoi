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
    public partial class frmNhacc : DevExpress.XtraEditors.XtraForm
    {
        public frmNhacc()
        {
            InitializeComponent();
        }
        NhaCungCap_DAL_BAL ncc = new NhaCungCap_DAL_BAL();
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dgv_nhacc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void frmNhacc_Load(object sender, EventArgs e)
        {
            dgv_nhacc.DataSource = ncc.loadNCC();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (ncc.themNCC(txtten.Text, txtdiachi.Text, txtdiem.Text))
            {
                MessageBox.Show("Thêm thành công");
                dgv_nhacc.DataSource = ncc.loadNCC();
            }
            else
            {
                MessageBox.Show("Thêm thất bại");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int a = int.Parse(txtsdt.Text);
            if(ncc.xoa(a)){
                MessageBox.Show("Xóa thành công");
                dgv_nhacc.DataSource = ncc.loadNCC();
            }
            else{
                MessageBox.Show("Xóa thất bại");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ncc.sua(int.Parse(txtsdt.Text), txtten.Text, txtdiachi.Text, txtdiem.Text))
            {
                MessageBox.Show("Sửa thông tin thành công");
                dgv_nhacc.DataSource = ncc.loadNCC();
            }
            else
            {
                MessageBox.Show("Sủa thông tin thất bại");
            }
        }

        private void dgv_nhacc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtsdt.Text = dgv_nhacc.SelectedRows[0].Cells[0].Value.ToString();
            txtten.Text = dgv_nhacc.SelectedRows[0].Cells[1].Value.ToString();
            txtdiachi.Text = dgv_nhacc.SelectedRows[0].Cells[2].Value.ToString();
            txtdiem.Text = dgv_nhacc.SelectedRows[0].Cells[3].Value.ToString();
            
        }

    }
}