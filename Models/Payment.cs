using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using ShoesMVC.Class;

namespace ShoesMVC.Models
{
    public class Payment : PageModel
    {
        public Product product = new Product();
        public string UserID { get; set; }
        public string PhoneNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public bool GetPhoneNumber(string UserID)
        {
            string URL = ConnectionURL.User;
            SqlConnection conn = new SqlConnection(URL);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select [PhoneNumber] from [AspNetUsers] " +
                "where [Id] = '" + UserID + "'";
            Console.WriteLine(cmd.CommandText);
            try
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (reader.IsDBNull(0))
                        {
                            conn.Close();
                            return false;
                        }
                        conn.Close();
                        return true;
                    }
                    else
                    {
                        conn.Close();
                        return false;
                    }
                }
            }
            catch (Exception) {
                throw;
            }
        }
        public Payment(string UserID, string ProductID)
        {
            this.UserID = UserID;
            OnGet(UserID, ProductID);
        }
        public void OnGet(string table_name, string ProductID)
        {
            try
            {
                string id = ProductID;
                string str = ConnectionURL.Cart;
                using (SqlConnection conn = new SqlConnection(str))
                {
                    conn.Open();
                    string sql = "select * from [" + table_name + "] where IdSP = @id";
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
                                product.Gia = (reader.GetDecimal(5)).ToString("N0");
                                product.NgayThem = "" + reader.GetDateTime(6);
                                product.HinhAnh = "" + reader.GetString(7);
                                product.SoLuong = "" + reader.GetInt32(8);
                                TotalPrice = decimal.Parse(product.Gia) * decimal.Parse(product.SoLuong);
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

        }
    }
}
