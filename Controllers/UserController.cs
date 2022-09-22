using LoggingWithWatchDogUsingCredentialFromDatabase.DBContext;
using LoggingWithWatchDogUsingCredentialFromDatabase.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LoggingWithWatchDogUsingCredentialFromDatabase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    public readonly UserDbContext _userDbContext;
    public UserController(UserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }
    [HttpPost]
    public IActionResult AddUser([FromBody] User user)
    {
        _userDbContext.Users.Add(user);
        _userDbContext.SaveChanges();
        return Ok(user);
    }
}
