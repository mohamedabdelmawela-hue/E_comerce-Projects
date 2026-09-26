using Microsoft.AspNetCore.Http.HttpResults;
using myStore.DTO;
using myStore.Models;
using myStore.Repository;
using myStore.Services;
namespace myStore.ServiceLayer
{
    public interface IUserServiceLayer
    {
        
            List<UserDTo> GetAll();

            UserDTo? GetById(int id);

            void Add(UserDTo dto);

            void Update(int id, UserDTo dto);

            void Delete(int id);
        string? Login(LoginDTO loginDTO);
        void Register(RegisterDTO registerDTO);
        

    }
    public class UserServiceLayer:IUserServiceLayer
    {
        private readonly IJwtServices _jwtServices;
        private readonly IUserRepository userRepository;
        public UserServiceLayer(IUserRepository userRepository,IJwtServices jwtServices)
        {
            this.userRepository = userRepository;
            _jwtServices = jwtServices;
        }
        public string? Login(LoginDTO loginDTO)
        {
            var userEmail=userRepository.Chick(loginDTO.Email);
            if (userEmail==null)
            {
                return null;
            }
            if (userEmail.userPassword!=loginDTO.Password)
            {
                return null;
            }

            return _jwtServices.GeneratToken(userEmail);

        }
        public void Register(RegisterDTO registerDTO)
        {
            var getEmail = userRepository.Chick(registerDTO.userEmail);
            if (getEmail!=null)
            {
                throw new Exception("Email is exist");
            }
            UsersModel usersModel= new UsersModel();
            usersModel.userName = registerDTO.userName;
            usersModel.userEmail = registerDTO.userEmail;
            usersModel.userPassword = registerDTO.userPassword;
            usersModel.userAddress = registerDTO.userAddress;
            userRepository.AddUser(usersModel);
         }




        public List<UserDTo> GetAll() {
            var users = userRepository.getAllUsers();

            return users.Select(u => new UserDTo
            {
                userName = u.userName,
                userEmail = u.userEmail,
                userPassword = u.userPassword,
                userAddress= u.userAddress
            }).ToList();
        }
        public UserDTo? GetById(int id)
        {
            var user = userRepository.getUserById(id);

            if (user == null)
                return null;

            return new UserDTo
            {
                userName = user.userName,
                userEmail = user.userEmail,
                userPassword = user.userPassword,
                userAddress = user.userAddress
            };
        }

        public void Add(UserDTo dto)
        {
            var user = new UsersModel
            {
                userName = dto.userName,
                userEmail = dto.userEmail,
                userPassword = dto.userPassword,
                userAddress = dto.userAddress
            };

            userRepository.AddUser(user);
        }

        public void Update(int id, UserDTo dto)
        {
            var user = userRepository.getUserById(id);

            if (user == null)
                throw new Exception("User Not Found");

            user.userName = dto.userName;
            user.userEmail = dto.userEmail
                ;
            user.userPassword = dto.userPassword;
            user.userAddress = dto.userAddress;

            userRepository.updateUser(user);
        }

        public void Delete(int id)
        {
            var result4 = userRepository.getUserById(id);
            if (result4 == null)
            {
                throw new Exception("Not found");
            }
            else
            {
                userRepository.deleteUser(result4);
            }
        }
    }
}
