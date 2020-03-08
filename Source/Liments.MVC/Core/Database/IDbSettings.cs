namespace Liments.MVC.Core.Database
{
    public interface IDbSettings
    {
        string connectionString { get; set; }
        string databaseName { get; set; }
    }
}
