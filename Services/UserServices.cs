using DataAccess;
using Models;

namespace Services;
public class UserServices{

    private readonly IRepository _iRepo;


    public UserServices(IRepository iRepo){
        _iRepo = iRepo;
    }



    public User CreateAccount(User user){
        try{

            _iRepo.AddUser(user);
            user.Wallet = 1000;
            return (user);

        }
        catch(ArgumentNullException e){
            Console.Write("Error When Creating Account: {0}", e);
            return null;
        }
    }

    public List<User>? GetUsers(){
        return _iRepo.GetAllUsers();
    }
    public bool UserLogin(User user){
        return _iRepo.UserLogin(user);
    }

    public User ViewPersonalInventory(User user) {
        return _iRepo.ViewPersonalInventory(user);
    }

    public User GetUserByID(int userID) {
        return _iRepo.GetUserByID(userID);
    }

    public User GetUserByUsername(string username) {
        return _iRepo.GetUserByUsername(username);
    }

    public List<Item> GetMarketplaceItems() {
        return _iRepo.GetMarketplaceItems();
    }

    public List<Item> getMarketplaceItemsByName(string searchitem) {
        return _iRepo.getMarketplaceItemsByName(searchitem);
    }

}
