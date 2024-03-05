using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using ShoesMVC.Class;

namespace ShoesMVC.Models
{
    public class Pay : PageModel
    {
        public string IDKH { get; set; }
        public string IDMH { get; set; }
        public string SoLuong { get; set; }
        public string ThanhTien {  get; set; }
        public string DienThoai { get; set; }
        public string GetPhoneNumber(string UserID)
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
                        string result = reader.GetString(0);
                        conn.Close();
                        return result;
                    }
                    else
                    {
                        conn.Close();
                        return "";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Pay(string iDKH, string iDMH, string soLuong, string thanhTien)
        {
            Redirect("Identity/Account/Manage");
            IDKH = iDKH;
            IDMH = iDMH;
            SoLuong = soLuong;
            DienThoai = GetPhoneNumber(iDKH);
            ThanhTien = thanhTien;
            string URL = ConnectionURL.Products;
            SqlConnection conn = new SqlConnection(URL);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Insert into DonHang " +
                "values (" +
                "'" + IDKH + "'," +
                "'" + IDMH + "'," +
                "'" + SoLuong + "'," +
                "'" + DienThoai + "'," +
                "'" + ThanhTien + "'" +
                ")";
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch(Exception) {
                throw;
            }
        }
    }
}
