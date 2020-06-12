using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Web;
using System.Collections;
using ToolClass.PublicClass;
using Util;


namespace ToolClass.Operation
{
    class OperateDatabase
    {
        public OperateDatabase()
        {
        
        }
        /// <summary>
        /// 普通查询
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="str">参数字符串</param>
        /// <returns></returns>

        public static DataTable select(string TableName, ArrayList arr)
        {
            string sql_str = "select * from " + TableName;
            string sql_TJ = " where";
            if (arr.Count != 0)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    string str = sqlDataFormat.dataFormat(arr[i].ToString());
                    string ColumnName = str.Split(':')[0];
                    string value = str.Split(':')[1];
                    if (i == arr.Count - 1)
                    {
                        sql_TJ += " " + ColumnName + "=" + value;
                    }
                    else
                    {
                        sql_TJ += " " + ColumnName + "=" + value + " and ";
                    }

                }
                sql_str += sql_TJ;
            }
            SqlHelper sqlhelper = new SqlHelper();
            DataTable dt = sqlhelper.getMySqlRead(sql_str);
            return dt;
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="arr">参数集合</param>
        /// <returns></returns>
        public static int Insert(string TableName,ArrayList arr)
        {
            int result = 0;
            string sql_str1 = "insert into " + TableName+"(";
            string sql_str2 = " values(";
            if (arr.Count != 0)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    string str = sqlDataFormat.dataFormat(arr[i].ToString());
                    string ColumnName = str.Split(':')[0];
                    string value = str.Split(':')[1];
                    if (i == arr.Count - 1)
                    {
                        sql_str1 += ColumnName;
                        sql_str2 += value;
                    }
                    else
                    {
                        sql_str1 += ColumnName + ",";
                        sql_str2 += value+",";
                    }

                }
                sql_str1 += ") ";
                sql_str2 += ")";
                SqlHelper sqlhelper = new SqlHelper();
                string sql = sql_str1 + sql_str2;
                result = sqlhelper.getMySqlCom(sql);
                
            }
            return result;
        }


        /// <summary>
        /// 更新数据库
        /// </summary>
        /// <param name="TableName">数据表名</param>
        /// <param name="arr">更新的字段</param>
        /// <param name="arr_where">条件字段</param>
        /// <returns></returns>
        public static int Update(string TableName, ArrayList arr,ArrayList arr_where)
        {
            int result = 0;
            string sql_str1 = "update "+TableName+" set ";
            string sql_where = "";
            if (arr.Count != 0)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    string str = sqlDataFormat.dataFormat(arr[i].ToString());
                    string ColumnName = str.Split(':')[0];
                    string value = str.Split(':')[1];
                    if (i == arr.Count - 1)
                    {
                        sql_str1 += ColumnName+"="+value;
                    }
                    else
                    {
                        sql_str1 += ColumnName + "=" + value+",";
                    }

                }
            }

            if (arr_where.Count != 0)
            {
                sql_where = " where ";
                for (int i = 0; i < arr_where.Count; i++)
                {
                    string str = sqlDataFormat.dataFormat(arr_where[i].ToString());
                    string ColumnName = str.Split(':')[0];
                    string value = str.Split(':')[1];
                    if (i == arr_where.Count - 1)
                    {
                        sql_where += ColumnName + "=" + value;
                    }
                    else
                    {
                        sql_where += ColumnName + "=" + value + " and ";
                    }

                }

            }
           SqlHelper sqlhelper = new SqlHelper();
            string sql = sql_str1 + sql_where;
            result = sqlhelper.getMySqlCom(sql);
            return result;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="arr_where">条件集合</param>
        /// <returns></returns>
        public static int Delete(string TableName, ArrayList arr_where)
        {
            int result = 0;
            string sql_str1 = "delete from " + TableName;
            string sql_where = "";
            if (arr_where.Count != 0)
            {
                sql_where = " where ";
                for (int i = 0; i < arr_where.Count; i++)
                {
                    string str = sqlDataFormat.dataFormat(arr_where[i].ToString());
                    string ColumnName = str.Split(':')[0];
                    string value = str.Split(':')[1];
                    if (value == "")
                    {
                        Log.WriteLog("删除时条件值不能为空");
                        return 0;
                    }
                    if (i == arr_where.Count - 1)
                    {
                        sql_where += ColumnName + "=" + value;
                    }
                    else
                    {
                        sql_where += ColumnName + "=" + value + ",";
                    }

                }
            }
            else {
                return 0;
            }

            SqlHelper sqlhelper = new SqlHelper();
            string sql = sql_str1 + sql_where;
            result = sqlhelper.getMySqlCom(sql);
            return result;
        }

    }
}
