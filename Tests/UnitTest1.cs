<<<<<<< HEAD
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
=======
// dotnet test --collect:"XPlat Code Coverage"
// dotnet tool install -g dotnet-reportgenerator-globaltool
// reportgenerator -reports:"./TestResults/23249cf1-781a-4bc5-ad73-7179a1fa90d0/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttype:Html
using Models;

namespace Tests;

public class UnitTest1
{
    // User class members test
    [Fact]
    public void CheckUserConstructor()
    {
        User user1 = new User() {
            Id=1, 
            FirstName="Sam", 
            LastName="Barton", 
            Username="Sambarton", 
            Password="R3v@tur3", 
            Wallet=85
        };

        Assert.NotNull(user1);

        User user2 = new User(2,"Ben","Kembel","Kemben","P@55w0rd",47);

        Assert.NotNull(user2);
    }

    [Fact]
    public void CheckUserGetters()
    {
        User user = new User(2,"Ben","Kembel","Kemben","P@55w0rd",47);

        Assert.Equal(user.Id,2);
        Assert.Equal(user.FirstName,"Ben");
        Assert.Equal(user.LastName,"Kembel");
        Assert.Equal(user.Username,"Kemben");
        Assert.Equal(user.Password,"P@55w0rd");
        Assert.Equal(user.Wallet,47);
    }
    [Fact]
    public void CheckUserToString()
    {
        User user = new User(2,"Ben","Kembel","Kemben","P@55w0rd",47);

        Assert.NotNull(user.ToString());
    }

    // Item class members test
    [Fact]
    public void CheckItemConstructor()
    {
        Item item = new Item(1,"Chair");

        Assert.NotNull(item);
    }
    [Fact]
    public void CheckItemGetters()
    {
        Item item = new Item(1,"Chair");

        Assert.Equal(1,item.Id);
        Assert.Equal("Chair",item.Name);
    }
    [Fact]
    public void CheckItemSetters()
    {
        Item item = new Item(1,"Chair");
        item.Id=2;
        item.Name="Table";
        Assert.Equal(2,item.Id);
        Assert.Equal("Table",item.Name);
    }
    [Fact]
    public void CheckItemToString()
    {
        Item item = new Item(1,"Chair");

        Assert.NotNull(item.ToString());
    }

    // Marketplace class members test
    [Fact]
    public void CheckMarketplaceConstructor()
    {
        Marketplace marketplace = new Marketplace(2,1,10,1200,3);

        Assert.NotNull(marketplace);
    }
    [Fact]
    public void CheckMarketplaceGetters()
    {
        Marketplace marketplace = new Marketplace(2,1,10,1200,3);

        Assert.Equal(2,marketplace.ListingId);
        Assert.Equal(1,marketplace.ItemId);
        Assert.Equal(10,marketplace.Quantity);
        Assert.Equal(1200,marketplace.UnitPrice);
        Assert.Equal(3,marketplace.SellerId);
    }
    [Fact]
    public void CheckMarketplaceSetters()
    {
        Marketplace marketplace = new Marketplace(2,1,10,1200,3);
        marketplace.ListingId=2;
        marketplace.ItemId=6;
        marketplace.Quantity=6;
        marketplace.UnitPrice=800;
        marketplace.SellerId=8;

        Assert.Equal(2,marketplace.ListingId);
        Assert.Equal(6,marketplace.ItemId);
        Assert.Equal(6,marketplace.Quantity);
        Assert.Equal(800,marketplace.UnitPrice);
        Assert.Equal(8,marketplace.SellerId);
    }
    [Fact]
    public void CheckMarketplaceToString()
    {
        Marketplace marketplace = new Marketplace(2,1,10,1200,3);

        Assert.NotNull(marketplace.ToString());
    }

}

>>>>>>> 9ba8360d55189e9966d4423ebbdaeb6cf0e9004b
