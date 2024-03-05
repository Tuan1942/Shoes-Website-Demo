using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoesMVC.Class;
using Microsoft.Data.SqlClient;

namespace ShoesMVC.Models.Seller
{
    public class Create : PageModel
    {
        public Product product = new Product();
        public string errorMessage = string.Empty;
        public Create()
        {
            OnGet();
        }
        void OnGet()
        {
            product = new Product();
        }
    }
}
