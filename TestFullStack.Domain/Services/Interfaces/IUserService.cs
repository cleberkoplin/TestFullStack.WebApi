using TestFullStack.Domain.Entities;


namespace TestFullStack.Domain.Services.Interfaces
{
    public interface IUserService
    {
        User Get(long id);
        User Get(string username);
        void Save(User user);
    }
}
