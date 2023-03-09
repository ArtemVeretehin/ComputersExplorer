using Microsoft.EntityFrameworkCore;
using ComputersExplorer;
using ComputersExplorer.CustomAuthenticationSchemes.GUID;
using System.Text.Json.Serialization;
using ComputersExplorer.Repositories;
using ComputersExplorer.Logic;

var builder = WebApplication.CreateBuilder(args);

string ComputersExplorerConnection = builder.Configuration.GetConnectionString("ComputersExplorerConnection");


// Add services to the container.



builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

builder.Services.AddAuthentication("GUID").AddScheme<GUIDAuthenticationOptions, GUIDAuthenticationHandler>("GUID", null);
builder.Services.AddDbContext<ComputersExplorerContext>(options => options.UseSqlServer(ComputersExplorerConnection));
builder.Services.AddSingleton<IGUIDAuthenticationManager, GUIDAuthenticationManager>();

//New Services
builder.Services.AddTransient(typeof(IRepository<>), typeof(MsSqlRepo<>));
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<ComputerRepository>();
builder.Services.AddTransient<RoleRepository>();
builder.Services.AddTransient<UserLogicProvider>();
builder.Services.AddTransient<ComputerRepository>();
builder.Services.AddTransient<RoleLogicProvider>();
//

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();
