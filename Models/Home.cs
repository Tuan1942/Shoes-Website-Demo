using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ShoesMVC.Class;
using ShoesMVC.Controllers;
using System.Collections.Generic;
using System.Collections;

namespace ShoesMVC.Models
{
    public class Home : PageModel
    {
        public List<Product> listNew = new List<Product>();
        public List<Product> list1 = new List<Product>();
        public List<Product> list2 = new List<Product>();
        public List<Product> list3 = new List<Product>();
        public List<Product> cart = new List<Product>();
        public int colCount = 5;
        public int rowCount = 1;
        public int lastColIndex = 0;
        public Home()
        {
            OnGet();
        }
        public void OnGet()
        {
            listNew = getShoes("");
            list1 = getShoes("Adidas");
            list2 = getShoes("Nike");
            list3 = getShoes("Ari Jordan");
        }
        public List<Product> getShoes(string NhanHieu)
        {
            List<Product> list = new List<Product>(); 
            try
            {
                string str = ConnectionURL.Products;
                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();
                    string sql = "select top(4) * from SanPham where NhanHieu like '%" + NhanHieu + "' order by NgayThem desc";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
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
                                list.Add(product);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            return list;
        }
    }
}
