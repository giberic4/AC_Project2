using DataAccess;
using Models;
using Services;
using Moq;
namespace Tests;

public class UnitTest1{
    


    [Fact]
    public void CreateAccountTest()
    {  
        User newUser = new();
        newUser.FirstName = "Test";
        newUser.LastName = "User";
        newUser.Username = "testUsername";
        newUser.Password = "testPwd";

        var mockRepo = new Mock<IRepository>();
        var _service = new UserServices(mockRepo.Object);
        

        _service.CreateAccount(newUser);


        Assert.Equal(newUser.FirstName, "Test");
        Assert.Equal(newUser.LastName, "User");
        Assert.Equal(newUser.Username, "testUsername");
        Assert.Equal(newUser.Password, "testPwd");
        Assert.Equal(newUser.Wallet, 1000);
    }

    [Fact]
    public void AddUser()
{
    // Arrange
        User newUser = new();
        newUser.FirstName = "Test";
        newUser.LastName = "User";
        newUser.Username = "testUsername";
        newUser.Password = "testPwd";

        var mockRepo = new Mock<IRepository>();
        var _service = new UserServices(mockRepo.Object);
        

        _service.CreateAccount(newUser);

        var currentUser = _service.CreateAccount(newUser);
    
        var dBRepo = new DBRepository();

        dBRepo.AddUser(currentUser);

        Assert.Same(currentUser, newUser);

    //Assert
        Assert.Equal(newUser.FirstName, "Test");
        Assert.Equal(newUser.LastName, "User");
        Assert.Equal(newUser.Username, "testUsername");
        Assert.Equal(newUser.Password, "testPwd");
        Assert.Equal(newUser.Wallet, 1000);

}
}