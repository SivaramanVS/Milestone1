using System.Collections.Generic;
using BusinessService.Data.DBModel;


namespace BusinessService.Domain.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
