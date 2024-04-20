using personapi_dotnet;

var builder = WebApplication.CreateBuilder(args);

// Create a new instance of the Startup class and configure the services
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline using the Startup class
startup.Configure(app, app.Environment);

// Run the app
app.Run();
