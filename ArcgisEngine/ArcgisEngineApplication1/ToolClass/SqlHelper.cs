using System.Configuration;
using System.Data;//DatSet..Table SqlDataAdapter
using MySql.Data.MySqlClient;//SqlConnection Command DataReader
namespace Util
{
    public class SqlHelper
    {
        public static string Constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        #region  建立MySql数据库连接
        /// <summary>
        /// 建立数据库连接.
        /// </summary>
        /// <returns>返回MySqlConnection对象</returns>
        public MySqlConnection getMySqlCon()
        {
            string M_str_sqlcon = Constr;// "server=localhost;port=3306;user id=root;password=root;database=car"; //根据自己的设置
            MySqlConnection myCon = new MySqlConnection(M_str_sqlcon);
            return myCon;
        }
        #endregion

        #region  执行MySqlCommand命令
        /// <summary>
        /// 执行MySqlCommand
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        public int getMySqlCom(string M_str_sqlstr, params MySqlParameter[] parameters)
        {
            MySqlConnection mysqlcon = this.getMySqlCon();
            mysqlcon.Open();
            MySqlCommand mysqlcom = new MySqlCommand(M_str_sqlstr, mysqlcon);
            mysqlcom.Parameters.AddRange(parameters);
            int count = mysqlcom.ExecuteNonQuery();
            mysqlcom.Dispose();
            mysqlcon.Close();
            mysqlcon.Dispose();
            return count;
        }
        #endregion

        #region  创建MySqlDataReader对象
        /// <summary>
        /// 创建一个MySqlDataReader对象
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        /// <returns>返回MySqlDataReader对象</returns>
        public DataTable getMySqlRead(string M_str_sqlstr, params MySqlParameter[] parameters)
        {
            MySqlConnection mysqlcon = this.getMySqlCon();
            mysqlcon.Open();
            MySqlCommand mysqlcom = new MySqlCommand(M_str_sqlstr, mysqlcon);
            mysqlcom.Parameters.AddRange(parameters);
            MySqlDataAdapter mda = new MySqlDataAdapter(mysqlcom);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mysqlcon.Close();
            return dt;
        }
        #endregion

    }
}