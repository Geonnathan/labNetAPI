using MySql.Data.MySqlClient;
using System.Data;

namespace UserWebAPI.Models
{
    public class User : ResultSetRowBuilder<User>
    {
        public int idUser { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public User() { }

        public User(int idUser, string name, string email, string password)
        {
            this.idUser = idUser;
            this.name = name;
            this.email = email;
            this.password = password;
        }

        public User(string name, string email, string password)
        {
            this.name = name;
            this.email = email;
            this.password = password;
        }

        public User(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

        public override User parseArrayRow(DataRow dtc)
        {
            throw new NotImplementedException();
        }

        public override User parseRow(MySqlDataReader rdr)
        {
            User user = new User();
            user.idUser = rdr.GetInt32(rdr.GetOrdinal("idUser"));
            user.name = rdr.GetString(rdr.GetOrdinal("name"));
            user.email = rdr.GetString(rdr.GetOrdinal("email"));
            user.password = rdr.GetString(rdr.GetOrdinal("password"));

            return user;
        }
    }
}
