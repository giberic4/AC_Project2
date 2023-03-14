using DataAccess;
using Models;

namespace Services;
public class UserServices{
<<<<<<< HEAD
    private readonly IRepository _iRepo;
=======
    private IRepository _iRepo;
>>>>>>> 9ba8360d55189e9966d4423ebbdaeb6cf0e9004b
    public UserServices(IRepository iRepo){
        _iRepo = iRepo;
    }

<<<<<<< HEAD
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
=======
    public List<User>? GetUsers(){
        return _iRepo.GetAllUsers();
    }
    public bool UserLogin(User user){
        Console.WriteLine("@@");
        return _iRepo.UserLogin(user);
    }

    // public void CreateAccount(){
    //     try{
    //         Console.Write("Please Enter Your First Name: ");
    //         string fname = Console.ReadLine()!;

    //         Console.Write("Please Enter Your Last Name: ");
    //         string lname = Console.ReadLine()!;

    //         Console.Write("Please Create Your Username: ");
    //         string uName = Console.ReadLine()!;

    //         Console.Write("Please Create A Password: ");
    //         string pwd = Console.ReadLine()!;

    //         User newUser = new ();
    //         newUser.FirstName = fname;
    //         newUser.LastName = lname;
    //         newUser.Username = uName;
    //         newUser.Password = pwd;
    //         newUser.Wallet = 1000;
    //         _iRepo.AddUser(newUser);

    //     }
    //     catch(Exception e){
    //         Console.Write("Error When Creating Account: {0}", e);
    //     }
    // }
>>>>>>> 9ba8360d55189e9966d4423ebbdaeb6cf0e9004b
    
}
