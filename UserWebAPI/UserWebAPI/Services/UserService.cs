using UserWebAPI.DAO;
using UserWebAPI.Models;

namespace UserWebAPI.Services
{
    public interface IUserService
    {
        public ReturnMessage<List<User>> GetUser(string dataSource, APICredentials credentials, User user);
        public ReturnMessage<List<User>> UpsertUser(string dataSource, APICredentials credentials, User user);
    }
    public class UserService : IUserService
    {
        private readonly IUserDAO _userDAO;

        public UserService(IUserDAO userDAO) 
        {
            _userDAO = userDAO;
        }
        public ReturnMessage<List<User>> GetUser(string dataSource, APICredentials credentials, User user)
        {
            return _userDAO.GetUser(dataSource, credentials, user);
        }

        public ReturnMessage<List<User>> UpsertUser(string dataSource, APICredentials credentials, User user)
        {
            return _userDAO.UpsertUser(dataSource, credentials, user);
        }
    }
}
