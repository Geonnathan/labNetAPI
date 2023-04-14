using UserWebAPI.DAO;
using UserWebAPI.Models;

namespace UserWebAPI.Services
{
    public interface IUsersWebsService
    {
        public ReturnMessage<List<UserWebs>> GetWebsUser(string dataSource, APICredentials credentials, int idUser);
        public ReturnMessage<List<UserWebs>> UpsertWeb(string dataSource, APICredentials credentials, UserWebs webs);
    }
    public class UserWebService : IUsersWebsService
    {
        private readonly IWebDAO _webDAO;

        public UserWebService(IWebDAO webDAO) 
        { 
            _webDAO = webDAO; 
        }

        public ReturnMessage<List<UserWebs>> GetWebsUser(string dataSource, APICredentials credentials, int idUser)
        {
            return _webDAO.GetWebsUser(dataSource, credentials, idUser);
        }

        public ReturnMessage<List<UserWebs>> UpsertWeb(string dataSource, APICredentials credentials, UserWebs webs)
        {
            return _webDAO.UpsertWeb(dataSource, credentials, webs);
        }
    }
}
