using Api_Fabrica.Dto;
using Api_Fabrica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Fabrica.Services.interfaces
{
    public interface IUserService
    {
        public List<UserEntity> GetUsers();

        public UserEntity GetUserByiD(int id);

        public UserEntity AddUser(UserEntity UserItem);

        public UserEntity UpdateUser(int id, UserEntity UserItem);

        public Boolean DeleteUser(int id);
        public AuthenticateResponse Login(AuthDTO authDTO);



    }
}
