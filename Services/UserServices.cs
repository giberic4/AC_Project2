using DataAccess;
using Models;

namespace Services;
public class UserServices{
    private readonly IRepository _iRepo;
    public UserServices(IRepository iRepo){
        _iRepo = iRepo;
    }

    // public List<User>? GetUsers(){
    //     return _iRepo.GetAllUsers();
    // }
    // public void Login(){

    // }


    public User CreateAccount(User user){
        try{
            // Console.Write("Please Enter Your First Name: ");
            // string fname = Console.ReadLine()!;

            // Console.Write("Please Enter Your Last Name: ");
            // string lname = Console.ReadLine()!;

            // Console.Write("Please Create Your Username: ");
            // string uName = Console.ReadLine()!;

            // Console.Write("Please Create A Password: ");
            // string pwd = Console.ReadLine()!;

            // user = new ();
            // user.FirstName = fname;
            // user.LastName = lname;
            // user.Username = uName;
            // user.Password = pwd;
            _iRepo.AddUser(user);
            user.Wallet = 1000;
            return (user);

        }
        catch(ArgumentNullException e){
            Console.Write("Error When Creating Account: {0}", e);
            return null;
        }
    }
    
}
