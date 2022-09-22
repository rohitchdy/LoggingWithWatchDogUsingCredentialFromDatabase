using LoggingWithWatchDogUsingCredentialFromDatabase.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoggingWithWatchDogUsingCredentialFromDatabase.DBContext;

public class UserDbContext: DbContext
{
	public UserDbContext(DbContextOptions<UserDbContext> options): base(options)
	{

	}
	public DbSet<User> Users { get; set; }
}
