using Azure.Core;
using Azure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using ShoesMVC.Class;

namespace ShoesMVC.Models.Seller
{
    public class Edit : PageModel
    {
        public Product product = new Product();
        public string ProductID = "";
        public string? errorMessage = "";

        public string RoleID { get; set; }
        public Edit(string ProductID)
        {
            OnGet(ProductID);
        }
        public void OnGet(string ProductID)
        {
            errorMessage = "";
            try
            {
                string id = ProductID;
                string str = ConnectionURL.Products;
                using (SqlConnection conn = new SqlConnection(str))
                {
                    conn.Open();
                    string sql = "select * from SanPham where IdSP = @id";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                product.Id = "" + reader.GetInt32(0);
                                product.Ten = reader.GetString(1);
                                product.NhanHieu = reader.GetString(2);
                                product.TonKho = "" + reader.GetInt32(3);
                                product.MoTa = reader.GetString(4);
                                product.Gia = "" + reader.GetDecimal(5);
                                product.NgayThem = "" + reader.GetDateTime(6);
                                product.HinhAnh = "" + reader.GetString(7);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }

}
