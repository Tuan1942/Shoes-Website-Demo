using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using ShoesMVC.Class;
using System;

namespace ShoesMVC.Models
{
    public class AddToCart : PageModel
    {
        public Product product = new Product();
        public Product getProduct(string id, string num)
        {
            Product product = new Product();
            try
            {
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
                                product.SoLuong = num;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return product;
        }
        public bool HadOnCart(string id, string table_name)
        {
            try
            {
                string str = ConnectionURL.Cart;
                using (SqlConnection conn = new SqlConnection(str))
                {
                    conn.Open();
                    string sql = "select * from [" + table_name + "] where IdSP = @id";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.Parameters.AddWithValue("table_name", table_name);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                conn.Close();
                                return false;
                            }
                            else
                            {
                                conn.Close();
                                return true;
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
        public void Add(Product product, string table_name)
        {
            try
            {
                string str = ConnectionURL.Cart;
                using (SqlConnection conn = new SqlConnection(str))
                {
                    conn.Open();
                    string sql = "Insert into [" + table_name + "] " + 
                        "values (@IdSP, @TenSP, @NhanHieu, @TonKho, @MoTa, @Gia, GETDATE(), @HinhAnh, @SoLuong)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("IdSP", product.Id);
                        cmd.Parameters.AddWithValue("TenSP", product.Ten);
                        cmd.Parameters.AddWithValue("NhanHieu", product.NhanHieu);
                        cmd.Parameters.AddWithValue("TonKho", product.TonKho);
                        cmd.Parameters.AddWithValue("MoTa", product.MoTa);
                        cmd.Parameters.AddWithValue("Gia", product.Gia);
                        cmd.Parameters.AddWithValue("HinhAnh", product.HinhAnh);
                        cmd.Parameters.AddWithValue("SoLuong", product.SoLuong.ToString());
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void IncreaseNum(Product product, string table_name)
        {
            try
            {
                string str = ConnectionURL.Cart;
                using (SqlConnection conn = new SqlConnection(str))
                {
                    conn.Open();
                    string sql = "Update [" + table_name + "] " +
                        "set SoLuong+= " + product.SoLuong + " " +
                        "where IdSP=@IdSP";
                    Console.WriteLine(sql);
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("IdSP", product.Id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public AddToCart(string id, string num, string UserID)
        {
            product = getProduct(id, num);
            if (!HadOnCart(id, UserID))
            {
                Add(product, UserID);
            }
            else
            {
                IncreaseNum(product, UserID);
            }
            Redirect("/Home/Detail?id=" + id);
        }
    }
}
