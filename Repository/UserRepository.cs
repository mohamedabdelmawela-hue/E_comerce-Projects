using Microsoft.EntityFrameworkCore;
using myStore.Models;
using myStore.DataBase;
//using myStore.Models;

namespace myStore.Repository
{
    public interface IUserRepository
    {
        List<UsersModel> getAllUsers();
        UsersModel? getUserById(int id);
        void AddUser(UsersModel user);
        void updateUser(UsersModel user);
        void deleteUser(UsersModel user);
        UsersModel? Chick(string Email);
    }
    public class UserRepository : IUserRepository
    {
        private readonly E_comerceContext _dB;
        public UserRepository(E_comerceContext dB)
        {
            //_userRepository = userRepository;
            this._dB = dB;
        }
        public List<UsersModel> getAllUsers()
        {
            return _dB.Users.ToList();

        }
        public UsersModel? getUserById(int id)
        {
            return _dB.Users.FirstOrDefault(x => x.userId == id);
        }
        public UsersModel? Chick(string Email) {
        return _dB.Users.FirstOrDefault(x=>x.userEmail == Email);
        }
        public void AddUser(UsersModel user)
        {
            _dB.Users.Add(user);
            _dB.SaveChanges();
        }
        public void updateUser(UsersModel user)
        {
            _dB.Users.Update(user);
            _dB.SaveChanges();
        }
        public void deleteUser(UsersModel user) {
            _dB.Users.Remove(user);
            _dB.SaveChanges();
        }

    }
}
