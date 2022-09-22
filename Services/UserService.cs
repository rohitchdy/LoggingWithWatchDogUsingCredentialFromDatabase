using LoggingWithWatchDogUsingCredentialFromDatabase.DBContext;
using LoggingWithWatchDogUsingCredentialFromDatabase.Entities;

namespace LoggingWithWatchDogUsingCredentialFromDatabase.Services;

public class UserService
{
    private readonly UserDbContext _userDbContext;
    public UserService(UserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }

    public User GetUserByRole(string role)
    {
        var userCred = _userDbContext.Users.FirstOrDefault(x => x.Role == role);
        return userCred;
    }
}
