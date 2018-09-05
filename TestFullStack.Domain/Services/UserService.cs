using TestFullStack.Domain.Entities;
using TestFullStack.Domain.Repositories.Interfaces;
using TestFullStack.Domain.Services.Interfaces;
using System.Linq;


namespace TestFullStack.Domain.Services
{
    public class UserService : IUserService
    {
        IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public User Get(long id)
        {
            return _userRepository.Get(id);
        }

        public User Get(string username)
        {
            var user = _userRepository.Get(x => x.Username.ToUpper() == username.ToUpper()).FirstOrDefault();
            return user;
        }

        public void Save(User user)
        {
            _userRepository.Insert(user);
            _userRepository.Save();
        }

    }
}
