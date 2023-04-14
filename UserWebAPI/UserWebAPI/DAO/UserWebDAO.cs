using MySql.Data.MySqlClient;
using System.Data;
using UserWebAPI.Models;

namespace UserWebAPI.DAO
{
    public interface IWebDAO
    {
        public ReturnMessage<List<UserWebs>> GetWebsUser(string dataSource, APICredentials credentials, int idUser);
        public ReturnMessage<List<UserWebs>> UpsertWeb(string dataSourse, APICredentials credentials, UserWebs webs);
    }
    public class UserWebDAO : IWebDAO
    {
        private readonly IConnectionHandler _connectionHandler;

        public UserWebDAO(IConnectionHandler connectionHandler)
        {
            _connectionHandler = connectionHandler;
        }

        public ReturnMessage<List<UserWebs>> GetWebsUser(string dataSource, APICredentials credentials, int idUser)
        {
            ReturnMessage<List<UserWebs>> res = new ReturnMessage<List<UserWebs>>();
            var connection = _connectionHandler.GetMySqlConnection(dataSource);
            if (connection != null)
            {
                res = get_allWebs(connection, credentials, idUser);
                _connectionHandler.Close(connection);
            }
            else
            {
                res.Code = 0;
                res.Message = "No se pudo encontrar el dataSource";
            }
            return res;
        }

        public ReturnMessage<List<UserWebs>> UpsertWeb(string dataSource, APICredentials credentials, UserWebs webs)
        {
            ReturnMessage<List<UserWebs>> res = new ReturnMessage<List<UserWebs>>();
            var connection = _connectionHandler.GetMySqlConnection(dataSource);
            if (connection != null)
            {
                res = upsert_web(connection, credentials, webs);
                _connectionHandler.Close(connection);
            }
            else
            {
                res.Code = 0;
                res.Message = "No se pudo encontrar el dataSource";
            }
            return res;
        }

        private ReturnMessage<List<UserWebs>> get_allWebs(MySqlConnection connection, APICredentials credentials, int idUser)
        {
            ReturnMessage<List<UserWebs>> res = new ReturnMessage<List<UserWebs>>();
            MySqlDataReader rdr = null;
            MySqlCommand command = new MySqlCommand();
            try
            {
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_selectUserWebs";
                command.Parameters.Add(new MySqlParameter("@p_key", credentials.ApiKey));
                command.Parameters.Add(new MySqlParameter("@p_secret", credentials.Secret));
                command.Parameters.Add(new MySqlParameter("@p_idUser", idUser));
                rdr = command.ExecuteReader();
                res.Value = new UserWebs().parseRows(rdr);
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

        private ReturnMessage<List<UserWebs>> upsert_web(MySqlConnection connection, APICredentials credentials, UserWebs webs)
        {
            ReturnMessage<List<UserWebs>> res = new ReturnMessage<List<UserWebs>>();
            MySqlDataReader rdr = null;
            MySqlCommand command = new MySqlCommand();
            try
            {
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_upsertUserWeb";
                command.Parameters.Add(new MySqlParameter("@p_key", credentials.ApiKey));
                command.Parameters.Add(new MySqlParameter("@p_secret", credentials.Secret));
                command.Parameters.Add(new MySqlParameter("@p_idWeb", webs.idWeb));
                command.Parameters.Add(new MySqlParameter("@p_idUser", webs.idUser));
                command.Parameters.Add(new MySqlParameter("@p_webName", webs.webName));
                command.Parameters.Add(new MySqlParameter("@p_url", webs.url));
                rdr = command.ExecuteReader();
                res.Value = new UserWebs().parseRows(rdr);
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
