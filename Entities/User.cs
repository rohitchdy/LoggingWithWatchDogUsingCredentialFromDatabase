using System.ComponentModel.DataAnnotations;

namespace LoggingWithWatchDogUsingCredentialFromDatabase.Entities;

public class User
{
    [Key]
    public int UserId { get; set; }
    public string UserName { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public string Role { get; set; } = String.Empty;
}
