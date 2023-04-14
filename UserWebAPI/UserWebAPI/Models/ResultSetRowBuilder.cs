using MySql.Data.MySqlClient;
using System.Data;

namespace UserWebAPI.Models
{
    public abstract class ResultSetRowBuilder<T>
    {
        public abstract T parseRow(MySqlDataReader rdr);
        public abstract T parseArrayRow(DataRow dtc);
        public List<T> parseRows(MySqlDataReader rdr)
        {
            List<T> rows = new List<T>();
            while (rdr.Read())
            {
                rows.Add(parseRow(rdr));
            }
            return rows;
        }
        public List<T> parseArrayRows(DataTable dtc)
        {
            List<T> rows = new List<T>();
            foreach (DataRow row in dtc.Rows)
            {
                rows.Add(parseArrayRow(row));
            }
            return rows;
        }
    }
}
