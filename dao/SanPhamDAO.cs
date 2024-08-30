using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using QuanLySanPham.Object;
using QuanLySanPham.util;

namespace QuanLySanPham.dao
{
    public class SanPhamDAO : ISanPhamDAO
    {
        private string maSP;
        private string tenSP;
        private string noiNhap;
        private string giaNhap;
        private string ngayNhap;
        private string loai;
        private ConnectDB connectDB;

        public SanPhamDAO()
        {
            connectDB = new ConnectDB();
        }

        public List<SanPham> GetAllProducts() {
            List<SanPham> lsp = new List<SanPham>();

            try {
                
                using (SqlConnection con = connectDB.GetConnection()) {
                    string query = "SELECT * FROM SanPham";
                    SqlCommand command = new SqlCommand(query, con);

                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while(reader.Read()) {
                        SanPham sp = new SanPham {
                            Ma = Convert.ToString(reader["MaSP"]),
                            Ten = Convert.ToString(reader["TenSP"]),
                            NoiNhap = Convert.ToString(reader["NoiNhap"]),
                            GiaNhap = Convert.ToDouble(reader["GiaNhap"]),
                            NgayNhap = Convert.ToDateTime(reader["NgayNhap"]),
                            Loai = new NhomSP(Convert.ToString(reader["MaNhom"]))
                        };

                        lsp.Add(sp);   
                    }

                    reader.Close();
                }
                
            } catch (Exception ex) {
                Console.WriteLine("Lỗi khi lấy dữ liệu từ CSDL: " + ex.Message);
            }

            return lsp;
        }

        public void AddProduct(SanPham sp)
        {
            // Implement code to add a product
        }

        public void UpdateProduct(SanPham sp)
        {
            // Implement code to update a product
        }

        public void DeleteProduct(int id)
        {
            // Implement code to delete a product
        }

        public void SaveProduct(List<SanPham> lsp)
        {
            using (SqlConnection con = connectDB.GetConnection())
            {
                try
                {
                    // Mở kết nối
                    con.Open();

                    foreach (SanPham sp in lsp)
                    {
                        // Chuỗi SQL chèn dữ liệu
                        string sql = "INSERT INTO SanPham (MaSP, TenSP, NoiNhap, GiaNhap, NgayNhap, MaNhom) VALUES ('" + sp.Ma + "','" + sp.Ten + "','" + sp.NoiNhap + "','" + sp.GiaNhap + "','" + sp.NgayNhap + "','" + sp.Loai.MaNhom + " ')";

                        // Tạo đối tượng SqlCommand
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            // Thực thi câu lệnh SQL
                            int rowsAffected = cmd.ExecuteNonQuery();

                            // Kiểm tra và xử lý kết quả
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Dữ liệu đã được chèn thành công vào cơ sở dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Chèn dữ liệu vào cơ sở dữ liệu thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            cmd.Parameters.Clear();
                        }
                    }

                    con.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi: " + ex.Message);
                }
            }
        }
    }
}