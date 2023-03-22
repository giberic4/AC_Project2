using Models;

namespace DataAccess;
public interface IRepository{
        List<User> GetAllUsers();
        List<Item> GetAllItems();
        User AddUser(User user);
        User GetUserByID(int userID);
        User GetUserByUsername(string username);
        bool UserLogin(User user);
        User ViewPersonalInventory(User user);
        void sellItem(int[] sellinfo);
        void buyItem(int[] buyinfo);
        List<Item> GetMarketplaceItems();
        List<int> GetSellerAndItemIdByListingId(int listing_id);
        List<Item> getMarketplaceItemsByName(string searchitem);
}