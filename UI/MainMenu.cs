using Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using DataAccess;

namespace UI;

public class MainMenu
{
    private HttpClient _http;
    public MainMenu() {
       _http = new HttpClient();
       _http.BaseAddress = new Uri("http://localhost:5144/");
    }
    

    public async Task StartAsync()
    {
        
        Console.WriteLine("-----------------------------------------------------------------");
        Console.WriteLine("Welcome to Animal Crossing");
        Console.WriteLine("-----------------------------------------------------------------");       

        while (true) {
            Console.WriteLine("\nPLEASE ENTER THE NUMBER OF YOUR CHOICE\n");
            Console.WriteLine("0 -> Exit Application");
            Console.WriteLine("1 -> Login to My Account");
            Console.WriteLine("2 -> Register to New Account");
            Console.WriteLine("3 -> Show Users List");
            Console.WriteLine("4 -> Test sellItem");
            Console.WriteLine("5 -> Test buyItem");

            string input= Console.ReadLine()!;

            switch (input) {
                case "0":
                Console.WriteLine("Goodbye");
                Environment.Exit(0);
                break;
                case "1":
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("Welcome to the Employee portal login");
                Console.WriteLine("-----------------------------------------------------------------\n");
                await UserLogin();
                break;
                case "2":
                await CreateNewUser();
                break;
                case "3":
                // ShowUsersList();
                // HttpResponseMessage msg = await _http.GetAsync("users");
                // Console.WriteLine(await msg.Content.ReadAsStringAsync());
                string content = await _http.GetStringAsync("users");
                Console.WriteLine(content);

                List<User> users = JsonSerializer.Deserialize<List<User>>(content);
                foreach (User u in users)
                    Console.WriteLine(u);
                break;
                case "4": 
                int[] intlist = new int[4];
                Console.WriteLine("id: ");
                intlist[0] = Int32.Parse(Console.ReadLine());
                Console.WriteLine("quantity: ");
                intlist[1] = Int32.Parse(Console.ReadLine());
                Console.WriteLine("user_id: ");
                intlist[2] = Int32.Parse(Console.ReadLine());
                Console.WriteLine("price: ");
                intlist[3] = Int32.Parse(Console.ReadLine());
                new DBRepository(Secrets.getConnectionString()).sellItem(intlist);
                Console.Read();
                break;
                case "5": 
                int[] bintlist = new int[6];
                Console.WriteLine("listing_id: ");
                bintlist[0] = Int32.Parse(Console.ReadLine());
                Console.WriteLine("quantity: ");
                bintlist[1] = Int32.Parse(Console.ReadLine());
                Console.WriteLine("buyer_id: ");
                bintlist[2] = Int32.Parse(Console.ReadLine());
                Console.WriteLine("price: ");
                bintlist[3] = Int32.Parse(Console.ReadLine());
                Console.WriteLine("item_id: ");
                bintlist[4] = Int32.Parse(Console.ReadLine());
                Console.WriteLine("sell_id: ");
                bintlist[5] = Int32.Parse(Console.ReadLine());
                new DBRepository(Secrets.getConnectionString()).buyItem(bintlist);
                break;
                default:
                Console.WriteLine("Invalid entry");
                break;
            }
        }       
    }

    private async Task CreateNewUser() {
    }

    public async Task UserLogin() {
        User user = new User();
        Console.Write("Please enter your Username: ");
        string? UserName = Console.ReadLine()!;
        user.Username=UserName;
        Console.Write("Please enter your Password: ");
        string? Password = PasswordEntryMasking();
        user.Password=Password;

        JsonContent jsonContent = JsonContent.Create<User>(user);
        Console.Write("@@@@@@@@@@@");
        HttpResponseMessage msg = await _http.PostAsync("login", jsonContent);
        Console.Write("!!!!!!!!!!!!!");
        //await Task.Delay(2000);
        // if ((await msg.Content.ReadAsStringAsync())=="0"){
        //         Console.WriteLine("\nLogged in successfully!\n");
        //         UserMenu userMenu = new UserMenu();
        //         userMenu.Start(user);
        //     }
        //     else if ((await msg.Content.ReadAsStringAsync())=="1"){
        //         Console.WriteLine("\nLogged in successfully as a Manager!\n");
        //         ManagerMenu managerMenu = new ManagerMenu();
        //         managerMenu.Start(user);
        //     }   
    }

    public string PasswordEntryMasking() {
        string? Password = string.Empty;
        ConsoleKey key;
        do
        {
            var keyInfo = Console.ReadKey(intercept: true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && Password.Length > 0)
            {
                Console.Write("\b \b");
                Password = Password[0..^1];
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                Console.Write("*");
                Password += keyInfo.KeyChar;
            }
        } while (key != ConsoleKey.Enter);
        return Password;
    }
}