using Models;

namespace DataAccess;
public interface IRepository{
        List<User> GetAllUsers();
        List<Item> GetAllItems();
        User AddUser(User user);
         User AddUser(User user);
        bool UserLogin(User user);
        User ViewPersonalInventory(User user);
        void sellItem(int[] sellinfo);
        void buyItem(int[] buyinfo);
}