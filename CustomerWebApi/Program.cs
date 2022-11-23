using CustomerWebApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//var dbHost = "DESKTOP-FTH\\SQLEXPRESS";    
//var dbName = "dms_customer";
//var dbPassword = ""; 

var dbHost = Environment.GetEnvironmentVariable("DB_HOST"); // "localhost,1500";
var dbName = Environment.GetEnvironmentVariable("DB_NAME"); // "dms_customer";
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD"); //  "P@ssw0rd123#";


var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};TrustServerCertificate=True;";

if (string.IsNullOrEmpty(dbPassword))
{
    connectionString += "Trusted_Connection=True";
}
else
{
    connectionString+=$"User ID=SA;Password ={ dbPassword} "; 
 }

builder.Services.AddDbContext<CustomerDbContext>(opt => opt.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
