﻿using Models;
using System.Data.SqlClient;
using System.Data;
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
            using SqlConnection connect = new SqlConnection(_connectionString);
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
            //Log.Warning("Error! Couldn't add user because {0}", ex);
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

    public User GetUserByID(int userID){
        using SqlConnection connection = new SqlConnection(_connectionString); 
        
        connection.Open();

        using SqlCommand cmd = new SqlCommand("SELECT * FROM USERS where id=@id", connection);
         cmd.Parameters.AddWithValue("@id", userID);
        using SqlDataReader reader = cmd.ExecuteReader();
        
        while(reader.Read()) {
            // string uCreatedAt=(string) reader["created_at"];
            string uFName = (string) reader["first_name"];
            string uLName = (string) reader["last_name"];
            string uName = (string) reader["username"];
            string uPassword = (string) reader["password"];
            int uWallet = (int) reader["wallet"];
            return new User(userID,uFName,uLName,uName,uPassword,uWallet);         
        }
        return new User();
    }

    public User GetUserByUsername(string username){
        using SqlConnection connection = new SqlConnection(_connectionString); 
        
        connection.Open();

        using SqlCommand cmd = new SqlCommand("SELECT * FROM USERS where username=@username", connection);
         cmd.Parameters.AddWithValue("@username", username);
        using SqlDataReader reader = cmd.ExecuteReader();
        
        while(reader.Read()) {
            int uId=(int) reader["id"];
            // string uCreatedAt=(string) reader["created_at"];
            string uFName = (string) reader["first_name"];
            string uLName = (string) reader["last_name"];
            string uPassword = (string) reader["password"];
            int uWallet = (int) reader["wallet"];
            return new User(uId,uFName,uLName,username,uPassword,uWallet);         
        }
        return new User();
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

        using SqlCommand cmd = new SqlCommand("SELECT USERS_ITEMS.id,ITEM.name, USERS_ITEMS.quantity,ITEM.url FROM USERS_ITEMS join ITEM on ITEM.id=USERS_ITEMS.id where USERS_ITEMS.user_id=@id", connection);
        cmd.Parameters.AddWithValue("@id", user.Id);
        using SqlDataReader reader = cmd.ExecuteReader();
        
        while(reader.Read()) {
            int iid=(int) reader["id"];
            string iname = (string) reader["name"];
            int iquantity = (int) reader["quantity"];
            string iurl=(string) reader["url"];
            user.listOfItems.Add(new Item(iid,iname,iquantity,url: iurl));
            }            
        
        return user;
    }

    public List<Item> GetMarketplaceItems() {

        List<Item> itemList = new List<Item>();
        using SqlConnection connection = new SqlConnection(_connectionString); 
        
        connection.Open();

        using SqlCommand cmd = new SqlCommand("SELECT STORE_INVENTORY.listing_id,ITEM.name, STORE_INVENTORY.quantity, STORE_INVENTORY.unit_price, ITEM.url FROM STORE_INVENTORY join ITEM on ITEM.id=STORE_INVENTORY.item_id", connection);
        using SqlDataReader reader = cmd.ExecuteReader();
        int i =0;
        while(reader.Read()) {
            int iid=(int) reader["listing_id"];
            string iname = (string) reader["name"];
            int iquantity = (int) reader["quantity"];
            int iprice = (int) reader["unit_price"];
            string iurl=(string) reader["url"];
            Console.WriteLine(iname);
            itemList.Add(new Item(iid,iname,iquantity,iprice,iurl));
            }            
        
        return itemList;
    }

    public List<int> GetSellerAndItemIdByListingId(int listing_id) {
        using SqlConnection connection = new SqlConnection(_connectionString); 
        
        connection.Open();
        List<int> list= new List<int>();
        using SqlCommand cmd = new SqlCommand("SELECT * FROM STORE_INVENTORY WHERE listing_id=@id", connection);
        cmd.Parameters.AddWithValue("@id", listing_id);
        using SqlDataReader reader = cmd.ExecuteReader();
        
        while(reader.Read()) {
            int sid=(int) reader["seller_id"];
            int iid=(int) reader["item_id"];
            list.Add(sid);
            list.Add(iid);

        }            
        
        return list;
    }

    public List<Item> getMarketplaceItemsByName(string searchitem) {

        List<Item> itemList = new List<Item>();
        using SqlConnection connection = new SqlConnection(_connectionString); 
        
        connection.Open();

        using SqlCommand cmd = new SqlCommand($"SELECT STORE_INVENTORY.listing_id,ITEM.name, STORE_INVENTORY.quantity, STORE_INVENTORY.unit_price, ITEM.url FROM STORE_INVENTORY join ITEM on ITEM.id=STORE_INVENTORY.item_id where ITEM.name LIKE '%{searchitem}%'", connection);
        using SqlDataReader reader = cmd.ExecuteReader();
        // cmd.Parameters.AddWithValue("@search", $"'% + {searchitem} +%'");
        int i =0;
        while(reader.Read()) {
            int iid=(int) reader["listing_id"];
            string iname = (string) reader["name"];
            int iquantity = (int) reader["quantity"];
            int iprice = (int) reader["unit_price"];
            string iurl=(string) reader["url"];
            Console.WriteLine(iname);
            itemList.Add(new Item(iid,iname,iquantity,iprice,iurl));
            }            
        
        return itemList;
    }




    /// <summary>
    /// This will take in a string list that we pass in
    /// inclded should be the values for the indexes
    ///  [0] id int
    ///  [1] quantity int
    ///  [2] user_id int
    ///  [3] price int 
    /// Once we have that we will call the stored procedure and add those to values to the command.
    ///  </summary>
    /// <param name=""></param>
    /// <returns>This will return nothing at this time, but I would like it to return a bool or return the listing_id </returns>
public void sellItem(int[] sellinfo)
{     

        int saleItemId = sellinfo[0];
        int saleItemQuanity = sellinfo[1];
        int saleItemUser_id = sellinfo[2];
        int saleItemPrice = sellinfo[3];
        
            
            try
            {

            using SqlConnection connection = new SqlConnection(_connectionString);
                {



                    using SqlCommand command = new SqlCommand("Sell_Item", connection);
                    command.Connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id" , saleItemId);
                    command.Parameters.AddWithValue("@quantity" , saleItemQuanity);
                    command.Parameters.AddWithValue("@user_id" , saleItemUser_id);
                    command.Parameters.AddWithValue("@price" , saleItemPrice);

                    int i = command.ExecuteNonQuery();
                    if(i>0)
                        {
                        Console.WriteLine("Records Inserted Successfully.");
                        }
            
                }

                    
            
     }
     catch(Exception ex)
        {
            throw;
        }     
}

/// <summary>
    /// This will take in a string list that we pass in
    /// inclded should be the values for the indexes
    // [0] listing_id int
    // [1] quantity int
    // [2] buyer_id int
    // [3] price int
    // [4] item_id int
    // [5] seller_id int
    /// Once we have that we will call the stored procedure and add those to values to the command.
    ///  </summary>
    /// <param name=""></param>
    /// <returns>This will return nothing at this time, but I would like it to return a bool </returns>
public void buyItem(int[] buyinfo)
{           Console.WriteLine("DB.cs");
            try
            {

            using SqlConnection connection = new SqlConnection(_connectionString);
                {



                    using SqlCommand command = new SqlCommand("buy_item", connection);
                    command.Connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@listing_id" , buyinfo[0]);
                    command.Parameters.AddWithValue("@quantity" , buyinfo[1]);
                    command.Parameters.AddWithValue("@buyer_id" , buyinfo[2]);
                    command.Parameters.AddWithValue("@price" , buyinfo[3]);
                    command.Parameters.AddWithValue("@item_id" , buyinfo[4]);
                    command.Parameters.AddWithValue("@seller_id" , buyinfo[5]);

                    int i = command.ExecuteNonQuery();
                    if(i>0)
                        {
                        Console.WriteLine("That transaction is complete.");
                        }
            
                }

                    
            
     }
     catch(Exception ex)
        {
            throw;
        }     
}


}
