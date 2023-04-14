using MySql.Data.MySqlClient;
using System.Data;

namespace UserWebAPI.Models
{
    public class UserLogin : ResultSetRowBuilder<UserLogin>
    {
        public string email { get; set; }
        public string password { get; set; }

        public UserLogin() { }

        public UserLogin(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

        public override UserLogin parseArrayRow(DataRow dtc)
        {
            throw new NotImplementedException();
        }

        public override UserLogin parseRow(MySqlDataReader rdr)
        {
            UserLogin UserLogin = new UserLogin();
            UserLogin.email = rdr.GetString(rdr.GetOrdinal("email"));
            UserLogin.password = rdr.GetString(rdr.GetOrdinal("password"));

            return UserLogin;
        }
    }
}
