using MySql.Data.MySqlClient;

namespace UserWebAPI.DAO
{
    public interface IConnectionHandler
    {
        MySqlConnection GetMySqlConnection(string dataSource);
        void Close(object o);
    }
    public class ConnectionHandler : IConnectionHandler
    {
        readonly IConfiguration _configuration;
        public ConnectionHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public MySqlConnection GetMySqlConnection(string dataSource)
        {
            string company = _configuration[$"Datasources:{dataSource}:Connection"];
            if (company != null)
            {
                string connectionString = _configuration.GetConnectionString(company);
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                return connection;
            }
            else
            {
                return null;
            }
        }
        public void Close(object o)
        {
            if (o is MySqlConnection)
            {
                ((MySqlConnection)o).Close();
            }
            else if (o is MySqlDataReader)
            {
                ((MySqlDataReader)o).Close();
            }
        }
    }
}

