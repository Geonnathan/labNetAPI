using MySql.Data.MySqlClient;
using System.Data;

namespace UserWebAPI.Models
{
    public class UserWebs : ResultSetRowBuilder<UserWebs>
    {
        public int idWeb { get; set; }
        public int idUser { get; set; }
        public string webName { get; set; }
        public string url { get; set; }

        public UserWebs() { }

        public UserWebs(string webName, string url)
        {
            this.webName = webName;
            this.url = url;
        }

        public UserWebs(int idWeb, int idUser, string webName, string url)
        {
            this.idWeb = idWeb;
            this.idUser = idUser;
            this.webName = webName;
            this.url = url;
        }

        public override UserWebs parseArrayRow(DataRow dtc)
        {
            throw new NotImplementedException();
        }

        public override UserWebs parseRow(MySqlDataReader rdr)
        {
            UserWebs webs = new UserWebs();
            webs.idWeb = rdr.GetInt32(rdr.GetOrdinal("idWeb"));
            webs.idUser = rdr.GetInt32(rdr.GetOrdinal("idUser"));
            webs.webName = rdr.GetString(rdr.GetOrdinal("webName"));
            webs.url = rdr.GetString(rdr.GetOrdinal("url"));

            return webs;
        }
    }
}
