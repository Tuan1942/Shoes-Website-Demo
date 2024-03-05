using Microsoft.EntityFrameworkCore;

namespace ShoesMVC.Class
{
    public class Product
    {
        public string Id { get; set; }
        public string Ten { get; set; }
        public string NhanHieu { get; set; }
        public string TonKho { get; set; }
        public string MoTa { get; set; }
        public string Gia { get; set; }
        public string NgayThem { get; set; }
        public string HinhAnh { get; set; }
        public string SoLuong { get; set; }
    }
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
