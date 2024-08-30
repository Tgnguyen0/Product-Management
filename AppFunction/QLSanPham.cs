using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLySanPham.Object;

namespace QuanLySanPham.AppFunction
{
    public class QLSanPham
    {
        private List<SanPham> listSP;

        public QLSanPham() {
            listSP = new List<SanPham>();
        }

        public Boolean themSanPham(SanPham moi) {
            for (int i = 0 ; i < listSP.Count ; i++) {
                if (moi.Ma == listSP[i].Ma) {
                    return false;
                }
            }

            listSP.Add(moi);
            return true;
        }

        public Boolean themNhieuSanPham(List<SanPham> ds) {
            listSP.AddRange(ds);
            return true;
        }

        public Boolean xoaSanPham(String ma) {
            for (int i = 0 ; i < listSP.Count ; i++) {
                if (ma == listSP[i].Ma) {
                    listSP.RemoveAt(i);

                    return false;
                }
            }

            return false;
        }

        public SanPham timSanPhamTheoMa(String ma) {
            for (int i = 0 ; i < listSP.Count ; i++) {
                if (ma == listSP[i].Ma) {

                    return listSP[i];
                }
            }

            return null;
        }

        public SanPham timSanPhamTheoTT(int index) {
            if (index >= listSP.Count || index < 0) {
                return null;
            }

            return listSP[index];
        }

        public int soLuong() {
            return listSP.Count;
        }
        
        public List<SanPham> getListSP() {
            return listSP;
        }
    }
}