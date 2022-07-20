using Api_Fabrica.Context;
using Api_Fabrica.Dto;
using Api_Fabrica.Model;
using Api_Fabrica.Services.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BCrypt.Net;
using Api_Fabrica.Authorization;

namespace Api_Fabrica.Services.implemaentation
{
    public class UserService : IUserService
    {
        private readonly MyDbContext _myDbContext;
        private IJwtUtils _jwtUtils;

        public UserService(MyDbContext myDbContext, IConfiguration configuration, IJwtUtils jwtUtils)
        {
            this._myDbContext = myDbContext;
            this._jwtUtils = jwtUtils;
        }

        public UserEntity AddUser(UserEntity userItem)
        {
            userItem.Password = BCrypt.Net.BCrypt.HashPassword(userItem.Password);

            var entity = _myDbContext.Users.Where(u => u.Login.Equals(userItem.Login)
           ).FirstOrDefault();


            if (entity!=null)
                throw new Exception("Username '" + entity.Login + "' is already taken");


            var x = _myDbContext.Users.Add(userItem);
            _myDbContext.SaveChanges();
            return userItem;
        }

        public bool DeleteUser(int id)
        {
            var entity = _myDbContext.Users.Find(id);
            if (entity != null)
            {
                var x = _myDbContext.Users.Remove(entity);
                _myDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public UserEntity GetUserByiD(int id)
        {
            var entity = _myDbContext.Users.Find(id);
            return entity;

        }

        public List<UserEntity> GetUsers()
        {
            var all = _myDbContext.Users.ToList();
            return all;
        }

        public AuthenticateResponse Login(AuthDTO authDTO)
        {
            var entity = _myDbContext.Users.Where(u => u.Login.Equals(authDTO.UserName)
             ).FirstOrDefault();

            if (entity == null || !BCrypt.Net.BCrypt.Verify(authDTO.Password, entity.Password))
                throw new Exception("Username or password is incorrect");

            AuthenticateResponse user = new AuthenticateResponse() {
                UserName = entity.Name,
                Id = entity.Id,
                Token = _jwtUtils.GenerateToken(entity)
            };
            return user;

        }


        public UserEntity UpdateUser(int id, UserEntity userItem)
        {
            var original = _myDbContext.Users.Find(id);

            if (original != null)
            {
                userItem.Password = BCrypt.Net.BCrypt.HashPassword(userItem.Password);
                _myDbContext.Entry(original).CurrentValues.SetValues(userItem);
                _myDbContext.SaveChanges();
                return userItem;
            }
            return null;

        }
    }
}
