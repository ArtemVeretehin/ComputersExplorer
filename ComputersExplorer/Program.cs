using Microsoft.EntityFrameworkCore;
using ComputersExplorer;
using ComputersExplorer.CustomAuthenticationSchemes.GUID;
var builder = WebApplication.CreateBuilder(args);

string ComputersExplorerConnection = builder.Configuration.GetConnectionString("ComputersExplorerConnection");


// Add services to the container.
builder.Services.AddAuthentication("GUID").AddScheme<GUIDAuthenticationOptions, GUIDAuthenticationHandler>("GUID", null);
builder.Services.AddDbContext<ComputersExplorerContext>(options => options.UseSqlServer(ComputersExplorerConnection));
builder.Services.AddSingleton<IGUIDAuthenticationManager, GUIDAuthenticationManager>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   // app.UseSwagger();
   // app.UseSwaggerUI();
}
//app.UseWelcomePage();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
