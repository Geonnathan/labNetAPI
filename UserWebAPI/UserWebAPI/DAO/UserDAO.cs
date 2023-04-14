using MySql.Data.MySqlClient;
using System.Data;
using UserWebAPI.Models;

namespace UserWebAPI.DAO
{
    public interface IUserDAO
    {
        public ReturnMessage<List<User>> GetUser(string dataSource, APICredentials credentials, User user);
        public ReturnMessage<List<User>> UpsertUser(string dataSourse, APICredentials credentials, User user);
    }
    public class UserDAO : IUserDAO
    {
        private readonly IConnectionHandler _connectionHandler;
        public UserDAO(IConnectionHandler connectionHandler, IConfiguration configuration)
        {
            _connectionHandler = connectionHandler;
        }
        public ReturnMessage<List<User>> GetUser(string dataSource, APICredentials credentials, User user)
        {
            ReturnMessage<List<User>> res = new ReturnMessage<List<User>>();
            var connection = _connectionHandler.GetMySqlConnection(dataSource);
            if (connection != null)
            {
                res = get_loginUser(connection, credentials, user);
                _connectionHandler.Close(connection);
            }
            else
            {
                res.Code = 0;
                res.Message = "No se pudo encontrar el dataSource";
            }
            return res;
        }

        public ReturnMessage<List<User>> UpsertUser(string dataSource, APICredentials credentials, User user)
        {
            ReturnMessage<List<User>> res = new ReturnMessage<List<User>>();
            var connection = _connectionHandler.GetMySqlConnection(dataSource);
            if (connection != null)
            {
                res = upsert_user(connection, credentials, user);
                _connectionHandler.Close(connection);
            }
            else
            {
                res.Code = 0;
                res.Message = "No se pudo encontrar el dataSource";
            }
            return res;
        }

        private ReturnMessage<List<User>> get_loginUser(MySqlConnection connection, APICredentials credentials, User user)
        {
            ReturnMessage<List<User>> res = new ReturnMessage<List<User>>();
            MySqlDataReader rdr = null;
            MySqlCommand command = new MySqlCommand();
            try
            {
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_login";
                command.Parameters.Add(new MySqlParameter("@p_key", credentials.ApiKey));
                command.Parameters.Add(new MySqlParameter("@p_secret", credentials.Secret));
                command.Parameters.Add(new MySqlParameter("@p_email", user.email));
                command.Parameters.Add(new MySqlParameter("@p_password", user.password));
                rdr = command.ExecuteReader();
                res.Value = new User().parseRows(rdr);
                res.Code = res.Value.Count;
                res.Message = "OK";
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Code = -1;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
                if (rdr != null)
                {
                    rdr.Close();
                }
            }
            return res;
        }
        private ReturnMessage<List<User>> upsert_user(MySqlConnection connection, APICredentials credentials, User user)
        {
            ReturnMessage<List<User>> res = new ReturnMessage<List<User>>();
            MySqlDataReader rdr = null;
            MySqlCommand command = new MySqlCommand();
            try
            {
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_upsertUser";
                command.Parameters.Add(new MySqlParameter("@p_key", credentials.ApiKey));
                command.Parameters.Add(new MySqlParameter("@p_secret", credentials.Secret));
                command.Parameters.Add(new MySqlParameter("@p_idUser", user.idUser));
                command.Parameters.Add(new MySqlParameter("@p_name", user.name));
                command.Parameters.Add(new MySqlParameter("@p_email", user.email));
                command.Parameters.Add(new MySqlParameter("@p_password", user.password));
                rdr = command.ExecuteReader();
                res.Value = new User().parseRows(rdr);
                res.Code = res.Value.Count;
                res.Message = "OK";
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Code = -1;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
                if (rdr != null)
                {
                    rdr.Close();
                }
            }
            return res;
        }
    }
}
