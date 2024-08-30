using System;

namespace QuanLySanPham.Object
{
    public class SanPham
    {
        private string ma;
        private string ten;
        private string noiNhap;
        private double giaNhap;
        private DateTime ngayNhap;
        private NhomSP loai;

        public SanPham() {
            this.ma = "";
            this.ten = "";
            this.noiNhap = "";
            this.giaNhap = 0.0;
            this.ngayNhap = DateTime.Now;
            this.loai = new NhomSP("Nhóm giá thấp");
        }

        public SanPham(string ma, string ten, string noiNhap, double giaNhap, DateTime ngayNhap, NhomSP loai) {
            this.ma = ma;
            this.ten = ten;
            this.noiNhap = noiNhap;
            this.giaNhap = giaNhap;
            this.ngayNhap = ngayNhap;
            this.loai = loai;
        }

        public string Ma   // property
        {
            get { return ma; }   // get method
            set { ma = value; }  // set method
        }

        public string Ten   // property
        {
            get { return ten; }   // get method
            set { ten = value; }  // set method
        }

        public string NoiNhap   // property
        {
            get { return noiNhap; }   // get method
            set { noiNhap = value; }  // set method
        }

        public double GiaNhap   // property
        {
            get { return giaNhap; }   // get method
            set { giaNhap = value; }  // set method
        }

        public DateTime NgayNhap   // property
        {
            get { return ngayNhap; }   // get method
            set { ngayNhap = value; }  // set method
        }

        public NhomSP Loai   // property
        {
            get { return loai; }   // get method
            set { loai = value; }  // set method
        }
    }
}
