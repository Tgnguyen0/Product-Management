using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLySanPham.Object;

namespace QuanLySanPham.dao
{
    public interface ISanPhamDAO
    {
        List<SanPham> GetAllProducts();
        //SanPham GetProductById(int id);
        void AddProduct(SanPham product);
        void UpdateProduct(SanPham product);
        void DeleteProduct(int id);
        void SaveProduct(List<SanPham> lsp);
    }
}