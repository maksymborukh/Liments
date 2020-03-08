namespace Liments.MVC.Core.Database
{
    public class DbSettings : IDbSettings
    {
        public string connectionString { get; set; }
        public string databaseName { get; set; }
    }
}
