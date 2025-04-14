using HMS.API.Abstraction.Entities.User;
using HMS.DAL.Models.Models;

namespace HMS.DAL.DataAccess.Utilities
{
    public static class Converter
    {
        //return a UserEntity (store from database to UserEntity)
        public static UserEntity ToUserEntity(this User user)
        {
            return new UserEntity()
            {
                Id = user.Id,
                Email = user.Email,
                RoleId = user.RoleId,
            };
        }
    }
}