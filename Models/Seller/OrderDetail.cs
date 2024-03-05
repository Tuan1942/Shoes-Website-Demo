using Microsoft.Data.SqlClient;
using ShoesMVC.Class;

namespace ShoesMVC.Models.Seller
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public string IDKH { get; set; }
        public string IDMH { get; set; }
        public string SoLuong { get; set; }
        public string DienThoai { get; set; }
        public string ThanhTien { get; set; }
        public Customer customer { get; set; }
        public Product product { get; set; }
        public OrderDetail(int id)
        {
            Id = id;
            SqlConnection connection = new SqlConnection(ConnectionURL.Products);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select * from DonHang where ID = " + id.ToString();
            try
            {
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        IDKH = reader.GetString(1);
                        IDMH = "" + reader.GetInt32(2);
                        SoLuong = "" + reader.GetInt32(3);
                        DienThoai = reader.GetString(4);
                        ThanhTien = (reader.GetDecimal(5)).ToString("N0");
                    }
                }
                connection.Close();
            }
            catch (Exception){
                throw;
            }
            GetCustomer(IDKH);
            GetProduct(IDMH);
        }
        public void GetCustomer(string IDKH)
        {
            customer = new Customer();
            try
            {
                string str = ConnectionURL.User;
                using (SqlConnection conn = new SqlConnection(str))
                {
                    conn.Open();
                    string sql = "select [firstName], [lastName], [PhoneNumber] " +
                        "from [AspNetUsers] " +
                        "where [Id] = '" + IDKH + "'";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customer.FirstName = reader.GetString(0);
                                customer.LastName = reader.GetString(1);
                                customer.PhoneNumber = reader.GetString(2);
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
        public void GetProduct(string IDMH)
        {
            product = new Product();
            try
            {
                string id = IDMH;
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
                                product.Gia = (reader.GetDecimal(5)).ToString("N0");
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
