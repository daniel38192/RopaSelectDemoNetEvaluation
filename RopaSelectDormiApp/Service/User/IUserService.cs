namespace RopaSelectDormiApp.Service.User;

using RopaSelectDormiApp.Entities.User;

public interface IUserService
{
    Task<List<User>> FindAllAsync();
    Task<User> FindByIdAsync(int id);
    Task<User> CreateAsync(User user);
    Task<User> UpdateAsync(User user);
    Task DeleteAsync(int id);
}