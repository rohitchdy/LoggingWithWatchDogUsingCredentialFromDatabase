# LoggingWithWatchDogUsingCredentialFromDatabase

This is a sample code to implement the logger to log all the request to API and also exception occur during API Request and Response.


Here we need 5 nuget packages:
* Microsoft.EntityFrameworkCore
* Microsoft.EntityFrameworkCore.Design
* Microsoft.EntityFrameworkCore.SqlServer
* Microsoft.EntityFrameworkCore.Tools
* WatchDog.NET

## WatchDog.NET
WatchDog is a Realtime HTTP (Request & Response) and Exception logger and viewer for ASP.Net Core Web Apps and APIs. It allows developers log and view http requests made to their web application and also exception caught during runtime in their web applications in Realtime.

## How to WatchDog.NET
Add below code in Program.cs
```
builder.Services.AddWatchDogServices(opt =>
{
    opt.ClearTimeSchedule = WatchDog.src.Enums.WatchDogAutoClearScheduleEnum.Weekly;
    opt.IsAutoClear = true;
    opt.SqlDriverOption = WatchDog.src.Enums.WatchDogSqlDriverEnum.MSSQL;
    opt.SetExternalDbConnString = builder.Configuration.GetConnectionString("myconn");
});
```
Above code is use to attach AddWatchDogService in application.

Then Add bellow code to use the service
```
    app.UseWatchDog(opt =>
    {
        opt.WatchPageUsername = "username";
        opt.WatchPagePassword = "password"; ;
    });
```

Here we are using credential from database so we modify above code as

```
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
```

To see the log details

https://localhost:your-application-hosted-port/watchdog

Then enter your credential you will see all the logging details. Or you can also see the logging details into the SQL Database Table 'WatchDog_WatchLog'


## You can also see the official documentation here https://www.nuget.org/packages/WatchDog.NET/1.2.0
