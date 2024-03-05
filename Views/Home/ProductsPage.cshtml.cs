using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using ShoesMVC.Class;

namespace ShoesMVC.Views.Home
{
    public class ProductsPageModel : PageModel
    {
        public List<Product> listProducts = new List<Product>();
        public int colCount = 4;
        public int rowCount = 0;
        public int lastColIndex = 0;
        [BindProperty]
        public string currentProduct { get; set; } = "";
        public ProductsPageModel(string NhanHieu)
        {
            OnGet(NhanHieu);
        }
        public void OnGet(string Query)
        {
            try
            {
                string str = ConnectionURL.Products;
                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();
                    string sql = "select * from SanPham where TenSP like N'%" + Query + "%'";
                    Console.WriteLine(sql);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Product product = new Product();
                                product.Id = "" + reader.GetInt32(0);
                                product.Ten = reader.GetString(1);
                                product.NhanHieu = reader.GetString(2);
                                product.TonKho = "" + reader.GetInt32(3);
                                product.MoTa = reader.GetString(4);
                                product.Gia = (reader.GetDecimal(5)).ToString("N0");
                                product.NgayThem = "" + reader.GetDateTime(6);
                                product.HinhAnh = "" + reader.GetString(7);
                                listProducts.Add(product);
                            }
                        }
                    }
                    rowCount = (int)(listProducts.Count / colCount);
                    lastColIndex = listProducts.Count % colCount;
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
    }
}
