using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ShoesMVC.Class;
using ShoesMVC.Models;
using ShoesMVC.Models.Seller;
using System.Security.Claims;

namespace ShoesMVC.Controllers
{
    public class SellerController : Controller
    {
        public string GetUserID()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if (claimsIdentity.FindFirst(ClaimTypes.NameIdentifier) != null)
            {
                string claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).ToString();
                string[] parts = claims.Split("nameidentifier:"); // Tách chuỗi
                string result = parts[1].Trim(); // Lấy phần tử thứ hai và xóa khoảng trắng
                return result;
            }
            else
            {
                return "";
            }
        }
        public void ChangeProduct(Product product)
        {
            ViewBag.UserID = GetUserID();
            ViewBag.UserRole = "Seller";
            if (product.Id == null ||
                product.Ten == null ||
                product.NhanHieu == null ||
                product.TonKho == null ||
                product.MoTa == null ||
                product.Gia == null ||
                product.HinhAnh == null)
            {
                return;
            }
            try
            {
                string connectionURL = ConnectionURL.Products;
                using (SqlConnection conn = new SqlConnection(connectionURL))
                {
                    conn.Open();
                    string sql = "update SanPham" +
                        " set TenSP=N'" + product.Ten + "', " +
                        " NhanHieu=N'" + product.NhanHieu + "', " +
                        " TonKho='" + product.TonKho + "', " +
                        " MoTa=N'" + product.MoTa + "', " +
                        " Gia='" + product.Gia + "', " +
                        " NgayThem=getdate(), " +
                        " HinhAnh='" + product.HinhAnh + "' " +
                        " where IdSP='" + product.Id + "'";
                    Console.WriteLine(sql);
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            Response.Redirect("/Home/Detail?id=" + product.Id);
        }
        public void AddProduct(Product product)
        {
            ViewBag.UserID = GetUserID();
            ViewBag.UserRole = "Seller";
            if (product.Ten == null ||
                product.NhanHieu == null ||
                product.TonKho == null ||
                product.MoTa == null ||
                product.Gia == null ||
                product.HinhAnh == null)
            {
                return;
            }
            try
            {
                string connectionURL = ConnectionURL.Products;
                using (SqlConnection conn = new SqlConnection(connectionURL))
                {
                    conn.Open();
                    string sql = "insert into SanPham " + "values(" +
                        "N'" + product.Ten + "', " +
                        "N'" + product.NhanHieu + "', " +
                        "N'" + product.TonKho + "', " +
                        "N'" + product.MoTa + "', " +
                        "N'" + product.Gia + "', " +
                        "getdate(), " +
                        "'" + product.HinhAnh + "')";
                    Console.WriteLine(sql);
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            Response.Redirect("/Home/Detail?id=" + product.Id);
        }
        public void DeleteProduct(string ID)
        {
            try
            {
                string connectionURL = ConnectionURL.Products;
                using (SqlConnection conn = new SqlConnection(connectionURL))
                {
                    conn.Open();
                    string sql = "delete from SanPham " + 
                        "where IdSP = " + ID + "";
                    Console.WriteLine(sql);
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            Response.Redirect("/Home/Index");
        }
        public IActionResult Edit()
        {
            Edit edit = new Edit(Request.Query["Id"]);
            return View(edit);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            ChangeProduct(product);
            return Redirect("/Home/Detail?id=" + product.Id);
        }
        public IActionResult Create()
        {
            ViewBag.UserID = GetUserID();
            ViewBag.UserRole = "Seller";
            return View(new Create());
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            AddProduct(product);
            return Redirect("/Home/");
        }
        public IActionResult Delete()
        {
            string ID = Request.Query["Id"];
            DeleteProduct(ID);
            return View(new Index());
        }
        public IActionResult Orders()
        {
            ViewBag.UserID = GetUserID();
            ViewBag.UserRole = "Seller";
            Orders orders = new Orders(GetUserID());
            return View(orders);
        }
        public IActionResult OrderDetail()
        {
            ViewBag.UserID = GetUserID();
            ViewBag.UserRole = "Seller";
            OrderDetail orderDetail = new OrderDetail(int.Parse(Request.Query["id"]));
            return View(orderDetail);
        }
    }
}
