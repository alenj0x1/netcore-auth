using BasicAuthenticationJWT.Enums;
using BasicAuthenticationJWT.Interfaces.Utils;
using BasicAuthenticationJWT.Models;

namespace BasicAuthenticationJWT.Utils;

public class UsersCache : IUsersCache
{
    private readonly List<User> _users = [new User()
    {
        Id = 1, // Default user
        Username = "alenj0x1",
        Password = "pass",
        Role = UserRole.Admin
    }];

    public User Add(User user)
    {
        user.Id = _users.Count + 1;
        
        _users.Add(user);
        return user;
    }

    public List<User> Get()
    {
        return _users;
    }

    public User? Get(string username)
    {
        return _users.FirstOrDefault(usr => usr.Username == username);
    }
}