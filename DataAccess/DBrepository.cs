using Models;
using System.Data.SqlClient;

namespace DataAccess;
public class DBRepository : IRepository
{
    private readonly string _connectionString;
    public DBRepository(string connectionString) {
        _connectionString = connectionString;
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

    public User ViewPersonalInventory(User user) {
        
        using SqlConnection connection = new SqlConnection(_connectionString); 
        
        connection.Open();

        using SqlCommand cmd = new SqlCommand("SELECT USERS_ITEMS.id,ITEM.name, USERS_ITEMS.quantity FROM USERS_ITEMS join ITEM on ITEM.id=USERS_ITEMS.id where USERS_ITEMS.user_id=@id", connection);
        cmd.Parameters.AddWithValue("@id", user.Id);
        using SqlDataReader reader = cmd.ExecuteReader();
        
        while(reader.Read()) {
            int iid=(int) reader["id"];
            string iname = (string) reader["name"];
            int iquantity = (int) reader["quantity"];
            user.listOfItems.Add(new Item(iid,iname,iquantity));
            }            
        
        return user;
    }
}
