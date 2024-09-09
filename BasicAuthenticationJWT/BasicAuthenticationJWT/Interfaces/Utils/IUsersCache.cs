using BasicAuthenticationJWT.Models;

namespace BasicAuthenticationJWT.Interfaces.Utils;

public interface IUsersCache
{
    User Add(User user);
    List<User> Get();
    User? Get(string username);
}