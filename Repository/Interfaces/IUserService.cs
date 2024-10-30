using GraphQlApi.Controllers;

namespace GraphQlApi.Repository.Interfaces;

public interface IUserService
{
    public List<UserDetail> GetUsers();

    public UserDetail GetUser(int userId);

    public Task<UserDetail> Create(UserDetail user);
}
