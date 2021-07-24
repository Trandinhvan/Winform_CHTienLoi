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
    public partial class frmNhanvien : DevExpress.XtraEditors.XtraForm
    {
        public frmNhanvien()
        {
            InitializeComponent();
        }
        Nhanvien_DAL_BAL nv = new Nhanvien_DAL_BAL();
        private void btnThem_Click(object sender, EventArgs e)
        {
            if(txtten.Text!="" && txtma.Text != "" && txtdiachi.Text != "" && txtLuongCoBan.Text != "" && txtmk.Text != ""){
            if(nv.themNV(txtten.Text, txtma.Text,txtdiachi.Text,int.Parse(txtLuongCoBan.Text), txtmk.Text, dateTimePicker2.Value, dateTimePicker1.Value)){
                MessageBox.Show("Thêm thành viên thành công");
                dgv_nhanvien.DataSource = nv.loadNhanVien();
                dgv_nhanvien.Columns[6].Visible = false;
                dgv_nhanvien.Columns[7].Visible = false;
            }
            else{MessageBox.Show("Thêm thành viên thất bại");}
            }
            else{MessageBox.Show("Vui lòng nhập đầy đủ thông tin");}
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (nv.xoaNV(txtma.Text))
            {
                MessageBox.Show("Xóa thành công");
                dgv_nhanvien.DataSource = nv.loadNhanVien();
                dgv_nhanvien.Columns[6].Visible = false;
                dgv_nhanvien.Columns[7].Visible = false;
            }
            else { MessageBox.Show("Xóa thất bại"); }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if(txtten.Text!="" && txtma.Text != "" && txtdiachi.Text != "" && txtLuongCoBan.Text != "" && txtmk.Text != ""){
            if(nv.suaNV(txtma.Text,txtdiachi.Text,int.Parse(txtLuongCoBan.Text), dateTimePicker1.Value,dateTimePicker2.Value)){
                MessageBox.Show("Sửa thành viên thành công");
                dgv_nhanvien.DataSource = nv.loadNhanVien();
                dgv_nhanvien.Columns[6].Visible = false;
                dgv_nhanvien.Columns[7].Visible = false;
            }
            else{MessageBox.Show("Sửa thành viên thất bại");}
            }
            else{MessageBox.Show("Vui lòng nhập đầy đủ thông tin");}
        }

        private void frmNhanvien_Load(object sender, EventArgs e)
        {
            dgv_nhanvien.DataSource= nv.loadNhanVien();
            dgv_nhanvien.Columns[6].Visible = false;
            dgv_nhanvien.Columns[7].Visible = false;
        }

        private void dgv_nhanvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                txtma.Text = dgv_nhanvien.SelectedRows[0].Cells[4].Value.ToString();
                txtten.Text = dgv_nhanvien.SelectedRows[0].Cells[0].Value.ToString();
                dateTimePicker1.Value = DateTime.Parse(dgv_nhanvien.SelectedRows[0].Cells[2].Value.ToString());
                txtdiachi.Text = dgv_nhanvien.SelectedRows[0].Cells[3].Value.ToString();
                txtLuongCoBan.Text = dgv_nhanvien.SelectedRows[0].Cells[5].Value.ToString();
                dateTimePicker2.Value = DateTime.Parse(dgv_nhanvien.SelectedRows[0].Cells[1].Value.ToString());
        }


    }
}