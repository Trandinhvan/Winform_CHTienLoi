using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL_BAL
{
    public class Khach_DAL_BAL
    {
        QL_CHTLDataContext db;
        public Khach_DAL_BAL()
        {
            db = new QL_CHTLDataContext();
        }
        public List<KHACH> loadKhach()
        {
            List<KHACH> result = new List<KHACH>();
            var data = from kh in db.KHACHes
                       select new { kh.TENKH, kh.DIENTHOAI, kh.DCHI, kh.DIEMTHANHVIEN };
            foreach (var kh in data)
            {
                result.Add(new KHACH { 
                    TENKH=kh.TENKH,
                    DIENTHOAI=kh.DIENTHOAI,
                    DCHI=kh.DCHI,
                    DIEMTHANHVIEN=kh.DIEMTHANHVIEN
                });
            }
            return result;
        }
        public bool themKH(string tenkh, string dienthoai, string diachi){

            try{
                KHACH kh = new KHACH();
                        kh.DIENTHOAI = dienthoai;
                        kh.TENKH = tenkh;
                        kh.DCHI = diachi;
                        kh.DIEMTHANHVIEN = 0;
                        db.KHACHes.InsertOnSubmit(kh);
                        db.SubmitChanges();
                        return true;
            }catch{
                return false;
            }
        }

        public bool xoaKH(string dienthoai)
        {
            string khoaNgoai = KiemTraKhoaNgoai(dienthoai);
            KHACH kh = db.KHACHes.Where(t => t.DIENTHOAI == dienthoai).FirstOrDefault();

            DialogResult kt = MessageBox.Show("Bạn có chắc muốn xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kh == null)
                MessageBox.Show("Không tồn tại khách hàng này trong bảng!");
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
                        db.KHACHes.DeleteOnSubmit(kh);
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

        public bool suaKH(string dienthoai, string diachi)
        {

            KHACH kh = db.KHACHes.Where(t => t.DIENTHOAI == dienthoai).FirstOrDefault();
            if (kh == null)
            {
                MessageBox.Show("Không tồn tại khách hàng này");
                
            }
            DialogResult kt = MessageBox.Show("Bạn có chắc muốn sửa địa chỉ khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kt == DialogResult.Yes)
            {
                try
                {
                    kh.DCHI = diachi;
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

        public string timkhach(string dienthoai)
        {
            string giamgia=string.Empty;
            foreach (KHACH kh in db.KHACHes.ToList())
            {
                if (kh.DIENTHOAI.Trim() == dienthoai.Trim())
                {
                    giamgia= kh.DIEMTHANHVIEN.ToString();
                    
                }
                else
                {
                    MessageBox.Show("Khách hàng không tồn tại");
                    break;
                }
            }
            return giamgia;
            
        }

        public string KiemTraKhoaNgoai(string psdt)
        {
            if (db.HOADONs.Any(h => h.MAKH == psdt))
            {
                return "Mã khách này được sử dụng ở bảng hóa đơn! Không thể xóa!";
            }
            return null;
        }

    }
}
