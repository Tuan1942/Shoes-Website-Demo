namespace ShoesMVC.Class
{
    public class ConnectionURL
    {
        static string DataSource = "LAPTOP-JFV3PC7D\\MSSQLSERVER01";
        static string URL(string Catalog)
        {
            return "Data Source=" + DataSource + ";Initial Catalog=" + Catalog + ";Integrated Security=True;Pooling=False;TrustServerCertificate=True";
        }
        public static readonly string Cart = URL("GioHang");
        public static readonly string User = URL("ShoesMVCUSer");
        public static readonly string Products = URL("SellerDB");
    }
}
