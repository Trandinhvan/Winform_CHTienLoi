using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL_BAL
{
    public class NhaCungCap_DAL_BAL
    {
        QL_CHTLDataContext db;
        public NhaCungCap_DAL_BAL()
        {
            db = new QL_CHTLDataContext();
        }
            public List<NHACC> loadNCC(){
                List<NHACC> result=new List<NHACC>();
                var ncc=from nc in db.NHACCs select new{nc.MANCC,nc.TENNCC,nc.DCHI,nc.DTHOAI};
                foreach(var nc in ncc){
                    result.Add(new NHACC{
                        MANCC=nc.MANCC,
                        TENNCC=nc.TENNCC,
                        DCHI=nc.DCHI,
                        DTHOAI=nc.DTHOAI
                    });
                }
                return result;
            }
        public bool themNCC(string tenNCC,string diachi, string sdt){

            try
            {
                NHACC ncc = new NHACC();
                ncc.TENNCC = tenNCC;
                ncc.DCHI = diachi;
                ncc.DTHOAI = sdt;
                db.NHACCs.InsertOnSubmit(ncc);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool xoa(int mancc)
        {

            string khoaNgoai = KiemTraKhoaNgoai(mancc);
            NHACC kh = db.NHACCs.Where(t => t.MANCC == mancc).FirstOrDefault();
            
            DialogResult kt = MessageBox.Show("Bạn có chắc muốn xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kh == null)
                MessageBox.Show("Không tồn tại mã nhà cung cấp này trong bảng!");
            else if(!String.IsNullOrEmpty(khoaNgoai))
            {
                MessageBox.Show(khoaNgoai);
            }
            else
            {
                if (kt == DialogResult.Yes)
                {
                    try
                    {
                        db.NHACCs.DeleteOnSubmit(kh);
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
        public bool sua(int mancc, string tenNCC, string diachi, string sdt)
        {

            NHACC kh = db.NHACCs.Where(t => t.MANCC == mancc).FirstOrDefault();

            DialogResult kt = MessageBox.Show("Bạn có chắc muốn sửa địa chỉ khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kt == DialogResult.Yes)
            {
                try
                {
                    kh.TENNCC = tenNCC;
                    kh.DTHOAI = sdt;

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
        
        public string KiemTraKhoaNgoai(int pMaNCC)
        {
            if(db.HANGs.Any(h => h.MANCC == pMaNCC) || db.PHIEUNHAPs.Any(pn => pn.MANCC == pMaNCC))
            {
                return "Có sản phẩm hoặc phiếu nhập sử dụng mã này! Không thể xóa!";
            }
            return null;
        }
    }
}
