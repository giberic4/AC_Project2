using Models;
using System.Data.SqlClient;

namespace DataAccess;
public class DBRepository : IRepository
{
    private readonly string _connectionString;
    public DBRepository(string connectionString) {
        _connectionString = connectionString;
    }

    public User AddUser(User user)
    {
        try{
            using SqlConnection connect = new SqlConnection(_connectString);
            connect.Open();


            using SqlCommand command = new SqlCommand("INSERT INTO Users(first_name, last_name, username, password, wallet) OUTPUT INSERTED.id VALUES (@fName, @lName, @uName, @uPwd, @uWallet)", connect);
            command.Parameters.AddWithValue("@fName", user.FirstName);
            command.Parameters.AddWithValue("@lName", user.LastName);
            command.Parameters.AddWithValue("@uName", user.Username);
            command.Parameters.AddWithValue("@uPwd", user.Password);
            command.Parameters.AddWithValue("@uWallet", user.Wallet);
            
            int createdId = (int) command.ExecuteScalar();
            user.Id = createdId;


            return user;
        }
        catch(SqlException ex) {
            Log.Warning("Error! Couldn't add user because {0}", ex);
            throw;
        }
    }

    public List<Item> GetAllItems()
    {
        throw new NotImplementedException();
    }

    public List<User> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public bool UserLogin(User user) {
        using SqlConnection connection = new SqlConnection(_connectionString); 
        
        connection.Open();

        using SqlCommand cmd = new SqlCommand("SELECT * FROM USERS", connection);
        using SqlDataReader reader = cmd.ExecuteReader();
        
        while(reader.Read()) {
            string uName = (string) reader["username"];
            string uPassword = (string) reader["password"];
            if(uName==user.Username && uPassword==user.Password) {
                return true;
            }            
        }
        return false;
    }
}
