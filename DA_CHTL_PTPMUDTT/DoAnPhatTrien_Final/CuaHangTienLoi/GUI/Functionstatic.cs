using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;

namespace GUI
{
    public class Functionstatic
    {
        public static void hienThiFormMoi(Form frmCha, Form frmDich)
        {
            foreach (Form f in frmCha.MdiChildren)
                if (f.GetType() == frmDich.GetType())
                    f.Close();

            if (frmDich.Name.Equals("frmBanHang") || frmDich.Name.Equals("frmThongTin"))
            {
                RibbonControl rid = frmCha.Controls["ribbonMain"] as RibbonControl;
                rid.Minimized = true;
            }
            else
            {
                RibbonControl rid = frmCha.Controls["ribbonMain"] as RibbonControl;
                //rid.Minimized = true;
            }
            frmDich.MdiParent = frmCha;
            frmDich.Show();
        }
    }
}
