using System.Net.Http.Headers;
using System.Text;
using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplicationServices(builder.Configuration);
//advantage of using an interface if you want to test

builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod()
  .WithOrigins ("http://localhost:4200"));

app.UseAuthentication(); // do you have a valid token?
app.UseAuthorization(); // what you are allowed to do?

app.MapControllers();

using var scope = app.Services.CreateAsyncScope();
var services = scope.ServiceProvider;
try{

    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
}
 
 catch (Exception ex)
 {

  var logger = services.GetService<ILogger<Program>>();
  logger.LogError(ex, "An error occured during migrations");
 }


app.Run();
