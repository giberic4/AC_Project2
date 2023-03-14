using Models;
using UI;

// using Serilog;


// Log.Logger = new LoggerConfiguration()
//     .WriteTo.Console()
//     .WriteTo.File("../logs/logs.txt", rollingInterval: RollingInterval.Day)
//     .CreateLogger();


MainMenu menu = new MainMenu();
await menu.StartAsync();
