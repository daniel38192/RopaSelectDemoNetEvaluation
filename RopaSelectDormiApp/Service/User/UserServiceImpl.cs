using Microsoft.EntityFrameworkCore;
using RopaSelectDormiApp.Data;

namespace RopaSelectDormiApp.Service.User;

using RopaSelectDormiApp.Entities.User;

public class UserServiceImpl(ApplicationDbContext dbContext): IUserService
{
    public async Task<List<User>> FindAllAsync()
    {
        return await dbContext.Users
            .ToListAsync();
    }

    public Task<User> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User> CreateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}