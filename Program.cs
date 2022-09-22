using LoggingWithWatchDogUsingCredentialFromDatabase.Controllers;
using LoggingWithWatchDogUsingCredentialFromDatabase.DBContext;
using LoggingWithWatchDogUsingCredentialFromDatabase.Services;
using Microsoft.EntityFrameworkCore;
using WatchDog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<UserService>();
builder.Services.AddDbContext<UserDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myconn"));
});

builder.Services.AddWatchDogServices(opt =>
{
    opt.ClearTimeSchedule = WatchDog.src.Enums.WatchDogAutoClearScheduleEnum.Weekly;
    opt.IsAutoClear = true;
    opt.SqlDriverOption = WatchDog.src.Enums.WatchDogSqlDriverEnum.MSSQL;
    opt.SetExternalDbConnString = builder.Configuration.GetConnectionString("myconn");
});
var app = builder.Build();
app.UseWatchDogExceptionLogger();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


using (var scope = app.Services.CreateScope())
{
    var userService = scope.ServiceProvider.GetRequiredService<UserService>();
    var cred = userService.GetUserByRole("admin");


    app.UseWatchDog(opt =>
    {
        opt.WatchPageUsername = cred.UserName;
        opt.WatchPagePassword = cred.Password; ;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
