using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLySanPham.Object
{
    public class NhomSP
    {
        private int maNhom;
        private string tenNhom;

        public NhomSP(string tenNhom) {
            this.tenNhom = tenNhom;
        }

        public NhomSP(int maNhom) {
            this.maNhom = maNhom;
            
            switch (maNhom) {
                case 0:
                    this.tenNhom = "Nhóm Giá Thấp";
                    break;
                case 1:
                    this.tenNhom = "Nhóm Giá Trung Bình";
                    break;
                case 2:
                    this.tenNhom = "Nhóm Giá Cao";
                    break;
                default:
                    this.tenNhom = null;
                    break;
            }
        }

        public NhomSP(int maNhom, string tenNhom) {
            this.maNhom = maNhom;
            this.tenNhom = tenNhom;
        }

        public string MaNhom 
        {
            get { return MaNhom; }
            set { MaNhom = value; }
        }

        public string TenNhom 
        {
            get { return tenNhom; }
            set { tenNhom = value; }
        }
    }
}