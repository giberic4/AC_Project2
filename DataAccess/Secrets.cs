namespace DataAccess;
internal class Secrets{
    private const string _connectionString = "Server=tcp:230206net-p2-server.database.windows.net,1433;Initial Catalog=TeamB;Persist Security Info=False;User ID=teamB;Password=@n!ma1Cr0$$ing;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    public static string getConnectionString() => _connectionString;
}