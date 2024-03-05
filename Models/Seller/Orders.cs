using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using ShoesMVC.Class;

namespace ShoesMVC.Models.Seller
{
    public class Orders : PageModel
    {
        public List<Customer> CustomerList = new List<Customer>();
        public Orders(string UserID) 
        {
            OnGet();
        }
        public void GetCustomerInfo(Customer customer)
        {
            Customer c = new Customer();
            try
            {
                string str = ConnectionURL.User;
                using (SqlConnection conn = new SqlConnection(str))
                {
                    conn.Open();
                    string sql = "select [firstName], [lastName], [PhoneNumber] " +
                        "from [AspNetUsers] " +
                        "where [Id] = '" + customer.Id + "'";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                c.FirstName = reader.GetString(0);
                                c.LastName = reader.GetString(1);
                                c.PhoneNumber = reader.GetString(2);
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
            customer.PhoneNumber = c.PhoneNumber;
            customer.FirstName = c.FirstName;
            customer.LastName = c.LastName;
        }

        public void OnGet() 
        {
            try
            {
                string str = ConnectionURL.Products;
                using (SqlConnection conn = new SqlConnection(str))
                {
                    conn.Open();
                    string sql = "select DISTINCT IDKH from DonHang";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Customer customer = new Customer();
                                customer.Id = reader.GetString(0);
                                GetCustomerInfo(customer);
                                customer.CustomerOrders = GetCustomerOrders(customer.Id);
                                CustomerList.Add(customer);
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
        public List<string> GetCustomerOrders(string UserID)
        {
            List<string> orders = new List<string>();
            string URL = ConnectionURL.Products;
            SqlConnection sqlConnection = new SqlConnection(URL);
            SqlCommand command = sqlConnection.CreateCommand();
            command.CommandText = "select ID from DonHang where IDKH = '" + UserID + "'";
            try
            {
                sqlConnection.Open();
                using (SqlDataReader r = command.ExecuteReader())
                {
                    while (r.Read())
                    {
                        orders.Add("" + r.GetInt32(0));
                    }    
                }
                sqlConnection.Close();

            }
            catch (Exception) {
                throw;
            }
            return orders;
        }
    }
    public class Customer
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public List <string> CustomerOrders { get; set;}
    }
}
