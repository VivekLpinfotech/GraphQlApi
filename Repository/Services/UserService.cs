using GraphQlApi.Controllers;
using GraphQlApi.Data;
using GraphQlApi.Models;
using GraphQlApi.Repository.Interfaces;

namespace GraphQlApi.Repository.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<UserDetail> GetUsers()
    {
        return _context.Users.Select(u => new UserDetail()
        {
            Id = u.Id,
            Email = u.Email,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Phone = u.Phone
        }).ToList();
    }

    public UserDetail GetUser(int userId)
    {
        var user = _context.Users.Where(x => x.Id == userId).FirstOrDefault();

        return new UserDetail()
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Phone = user.Phone
        };
    }

    public async Task<UserDetail> Create(UserDetail user)
    {
        var newUser = new User()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone
        };

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();

        return new UserDetail()
        {
            Id = newUser.Id,
            Email = newUser.Email,
            FirstName = newUser.FirstName,
            LastName = newUser.LastName,
            Phone = newUser.Phone
        };
    }
}
