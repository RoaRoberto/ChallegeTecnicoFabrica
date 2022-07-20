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



namespace Api_Fabrica.Services.implemaentation
{
    public class UserService : IUserService
    {
        private readonly MyDbContext _myDbContext;

        public UserService(MyDbContext myDbContext, IConfiguration configuration)
        {
            this._myDbContext = myDbContext;
        }

        public UserEntity AddUser(UserEntity userItem)
        {
            userItem.Password = BCrypt.Net.BCrypt.HashPassword(userItem.Password);
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

        public bool Login(AuthDTO authDTO)
        {
            var entity = _myDbContext.Users.Where(u => u.Login.Equals(authDTO.UserName)
             ).FirstOrDefault();

            if (entity == null || !BCrypt.Net.BCrypt.Verify(authDTO.Password, entity.Password))
                throw new Exception("Username or password is incorrect");


            return true;

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
