using DataAccess;
using Models;

namespace Services;
public class UserServices{
    private IRepository _iRepo;
    public UserServices(IRepository iRepo){
        _iRepo = iRepo;
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
}
