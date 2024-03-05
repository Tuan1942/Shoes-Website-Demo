using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ShoesMVC.Areas.Identity.Pages.Account;
using ShoesMVC.Class;
using ShoesMVC.Models;
using ShoesMVC.Views.Home;
using System.Diagnostics;
using System.Security.Claims;

namespace ShoesMVC.Controllers
{
    public class HomeController : Controller
    {
        public int colCount = 5;
        public int rowCount = 1;
        public int lastColIndex = 0;
        public string ID;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        private readonly ILogger<HomeController> _logger;

        public List<Product> listProducts = new List<Product>();

        public IActionResult Index()
        {
            ViewBag.UserID = GetUserID();
            ViewBag.UserRole = GetRoleName(GetRoleID());
            Home home = new Home();
            return View(new Home());
        }
        [HttpPost]
        public string GetUserID() 
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if (claimsIdentity.FindFirst(ClaimTypes.NameIdentifier) != null )
            {
                ID = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).ToString();
                string[] parts = ID.Split("nameidentifier:"); // Tách chuỗi
                string result = parts[1].Trim(); // Lấy phần tử thứ hai và xóa khoảng trắng
                return result;
            }
            else
            {
                return "";
            }
        }
        public string GetRoleID()
        {
            string URL = ConnectionURL.User;
            SqlConnection connection = new SqlConnection(URL);
            connection.Open();
            string query = "Select RoleId from AspNetUserRoles where UserId = '" + GetUserID() + "'";
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = query;
            SqlDataReader reader = cmd.ExecuteReader();
            string result;
            if (reader.Read())
            {
                result = reader.GetString(0);
            }
            else result = "0";
            connection.Close();
            return result;
        }
        public string GetRoleName(string roleID)
        {
            string URL = ConnectionURL.User;
            SqlConnection connection = new SqlConnection(URL);
            connection.Open();
            string query = "Select Name from AspNetRoles where Id = '" + GetRoleID() + "'";
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = query;
            SqlDataReader reader = cmd.ExecuteReader();
            string result;
            if (reader.Read())
            {
                result = reader.GetString(0);
            }
            else result = "";
            connection.Close();
            return result;
        }
        public IActionResult Detail()
        {
            ViewBag.UserID = GetUserID();
            ViewBag.UserRole = GetRoleName(GetRoleID());
            string ProductID = Request.Query["id"];
            DetailPageModel detailPageModel = new DetailPageModel(ProductID)
            {
                UserID = GetUserID(),
                RoleID = GetRoleID()
            };
            return View(detailPageModel);
        }
        public IActionResult Cart()
        {
            ViewBag.UserID = GetUserID();
            ViewBag.UserRole = GetRoleName(GetRoleID());
            CartModel cartModel = new CartModel(GetUserID());
            return View(cartModel);
        }
        public IActionResult Login()
        {
            ViewBag.UserID = GetUserID();
            ViewBag.UserRole = GetRoleName(GetRoleID());
            return View();
        }
        public IActionResult ProductsPage()
        {
            ViewBag.UserID = GetUserID();
            ViewBag.UserRole = GetRoleName(GetRoleID());
            string Query = Request.Query["query"];
            ProductsPageModel productsPageModel = new ProductsPageModel(Query);
            return View(productsPageModel);
        }
        public IActionResult AddToCart()
        {
            AddToCart addToCart = new AddToCart(Request.Query["Id"], Request.Query["num"], GetUserID());
            return View(addToCart);
        }
        public IActionResult DeleteFromCart()
        {
            DeleteFromCart deleteFromCart = new DeleteFromCart(Request.Query["User"], Request.Query["id"]);
            return View();
        }
        public IActionResult Payment()
        {
            ViewBag.UserID = GetUserID();
            ViewBag.UserRole = GetRoleName(GetRoleID());
            Payment payment = new Payment(Request.Query["User"], Request.Query["id"]);
            return View(payment);
        }
        public IActionResult Pay()
        {
            Pay pay = new Pay(Request.Query["IDKH"], Request.Query["IDMH"], Request.Query["SoLuong"], Request.Query["ThanhTien"]);
            return View(pay);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
