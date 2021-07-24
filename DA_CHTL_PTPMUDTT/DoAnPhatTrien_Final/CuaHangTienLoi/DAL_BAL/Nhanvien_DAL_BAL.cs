using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL_BAL
{
    public class Nhanvien_DAL_BAL
    {
        QL_CHTLDataContext db;
        public Nhanvien_DAL_BAL()
        {
            db = new QL_CHTLDataContext();
        }
        public List<NHANVIEN> loadNhanVien()
        {
            List<NHANVIEN> result = new List<NHANVIEN>();
            var data = from nv in db.NHANVIENs
                       where (nv.LOAINV.Equals("NHÂN VIÊN"))
                       select new { nv.TENNV, nv.DIENTHOAI, nv.NGAYLAMVIEC, nv.DCHI, nv.NGAYSINH, nv.LUONGCOBAN };
            foreach (var nv in data)
            {
                result.Add(new NHANVIEN{
                    TENNV=nv.TENNV,
                    DIENTHOAI=nv.DIENTHOAI,
                    NGAYLAMVIEC=nv.NGAYLAMVIEC,
                    DCHI=nv.DCHI,
                    NGAYSINH=nv.NGAYSINH,
                    LUONGCOBAN=nv.LUONGCOBAN
                });
            }
            return result;
        }
        public bool themNV(string tennv, string dienthoai, string diachi,int luongcb, string mk, DateTime ngaylv, DateTime ngaysinh)
        {
            try {
                NHANVIEN nv = new NHANVIEN();
                nv.TENNV = tennv;
                nv.DIENTHOAI = dienthoai;
                nv.DCHI =diachi ;
                nv.LOAINV = "NHÂN VIÊN";
                nv.LUONGCOBAN = luongcb;
                nv.MK = mk;
                nv.NGAYLAMVIEC = ngaylv;
                nv.NGAYSINH = ngaysinh;
                db.NHANVIENs.InsertOnSubmit(nv);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool xoaNV(string dienthoai)
        {
            string khoaNgoai = KiemTraKhoaNgoai(dienthoai);
            NHANVIEN kh = db.NHANVIENs.Where(t => t.DIENTHOAI == dienthoai).FirstOrDefault();

            DialogResult kt = MessageBox.Show("Bạn có chắc muốn xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kh == null)
                MessageBox.Show("Không tồn tại nhân viên này trong bảng!");
            else if (!String.IsNullOrEmpty(khoaNgoai))
            {
                MessageBox.Show(khoaNgoai);
            }
            else
            {
                if (kt == DialogResult.Yes)
                {
                    try
                    {
                        db.NHANVIENs.DeleteOnSubmit(kh);
                        db.SubmitChanges();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }

            return false;
        }
        public bool suaNV(string dienthoai, string diachi, int luongcb, DateTime ngaylv, DateTime ngaysinh)
        {
            NHANVIEN nv = db.NHANVIENs.Where(t => t.DIENTHOAI.Equals(dienthoai)).FirstOrDefault();
            if (nv == null)
            {
                MessageBox.Show("Nhân viên không tồn tại");
            }
            DialogResult kt = MessageBox.Show("Bạn có chắc muốn sửa thông tin nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (kt == DialogResult.Yes)
            {
                try
                {
                    nv.DCHI = diachi;
                    nv.LUONGCOBAN = luongcb;
                    nv.NGAYSINH = ngaysinh;
                    nv.NGAYLAMVIEC = ngaylv;
                    db.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public string KiemTraKhoaNgoai(string pdt)
        {
            if (db.HOADONs.Any(h => h.MANV == pdt))
            {
                return "Nhân viên đang ở trong bảng Hóa Đơn!Không thể xóa!!";
            }
            return null;
        }
    }
}
