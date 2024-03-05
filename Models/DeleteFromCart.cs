using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using ShoesMVC.Class;
using System;

namespace ShoesMVC.Models
{
    public class DeleteFromCart
    {
        public DeleteFromCart(string table_name, string id) 
        {
            try
            {
                string URL = ConnectionURL.Cart;
                using (SqlConnection connection = new SqlConnection(URL))
                {
                    connection.Open();
                    string sql = "DELETE FROM [" + table_name + "] where IdSP = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
