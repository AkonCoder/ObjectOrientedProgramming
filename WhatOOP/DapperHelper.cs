using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace WhatOOP
{
    /// <summary>
    /// 主站信息
    /// </summary>
    public class DapperHelper
    {
        public static string Sqlconnection = ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString;

        /// <summary>
        /// 得到一组信息
        /// </summary>
        /// <typeparam name="T">用于初始化或者转化的对像</typeparam>
        /// <param name="sqlString">查询SQL语句</param>
        /// <param name="cmdParms">包含条件或参数值的对像</param>
        /// <returns>返回 T 的集合</returns>
        public static IEnumerable<T> Query<T>(string sqlString, object cmdParms = null)
        {
            using (IDbConnection conn = new SqlConnection(Sqlconnection))
            {
                conn.Open();
                return conn.Query<T>(sqlString, cmdParms);
            }
        }
        /// <summary>
        /// 得到一组信息
        /// </summary>
        /// <typeparam name="T">用于初始化或者转化的对像</typeparam>
        /// <param name="sqlString">查询SQL语句</param>
        /// <param name="cmdParms">包含条件或参数值的对像</param>
        /// <returns>返回 T 的集合</returns>
        public static IEnumerable<dynamic> Query(string sqlString, object cmdParms = null)
        {
            using (IDbConnection conn = new SqlConnection(Sqlconnection))
            {
                conn.Open();
                IEnumerable<dynamic> list = conn.Query(sqlString, cmdParms);
                return list;
            }
        }
        /// <summary>
        /// 得到一个对像
        /// <para>此方法只能返回一行数据时使用。</para>
        /// </summary>
        /// <typeparam name="T">用于初始化或者转化的对像</typeparam>
        /// <param name="sqlString">查询SQL语句</param>
        /// <param name="cmdParms">包含条件或参数值的对像</param>
        /// <returns>返回一个对像</returns>
        public static T GetModel<T>(string sqlString, object cmdParms = null)
        {
            using (IDbConnection conn = new SqlConnection(Sqlconnection))
            {
                conn.Open();
                return conn.Query<T>(sqlString, cmdParms).SingleOrDefault<T>();
            }
        }

        /// <summary>
        /// 执行SQL 并返回影响的行数
        /// </summary>
        /// <param name="sqlString">用于执行的SQL</param>
        /// <param name="cmdParms">包含条件或参数值的对像</param>
        /// <returns>影响的行数</returns>
        public static int Execute(string sqlString, object cmdParms = null)
        {
            using (IDbConnection conn = new SqlConnection(Sqlconnection))
            {
                conn.Open();
                int row = conn.Execute(sqlString, cmdParms);
                return row;
            }
        }
        /// <summary>
        /// 执行SQL 并返回第一行第一列的值
        /// </summary>
        /// <param name="sqlString">用于执行的SQL</param>
        /// <param name="cmdParms">包含条件或参数值的对像</param>
        /// <returns>第一行第一列的值</returns>
        public static object ExecuteScalar(string sqlString, object cmdParms = null)
        {

            using (IDbConnection conn = new SqlConnection(Sqlconnection))
            {
                conn.Open();
                object firstSelected = conn.ExecuteScalar(sqlString, cmdParms);
                return firstSelected;
            }
        }
        /// <summary>
        /// 执行SQL 并返回一个对像
        /// </summary>
        /// <typeparam name="T">用于初始化或者转化的对像</typeparam>
        /// <param name="sqlString">用于执行的SQL</param>
        /// <param name="cmdParms">包含条件或参数值的对像</param>
        /// <returns>一个对像</returns>
        public static T ExecuteScalar<T>(string sqlString, object cmdParms = null)
        {

            using (IDbConnection conn = new SqlConnection(Sqlconnection))
            {
                conn.Open();
                T firstSelected = conn.ExecuteScalar<T>(sqlString, cmdParms);
                return firstSelected;
            }
        }
        /// <summary>
        /// 运行事务，执行SQL 并返回影响的行数
        /// </summary>
        /// <param name="sqlString">用于执行的SQL</param>
        /// <param name="cmdParms">包含条件或参数值的对像</param>
        /// <returns>影响的行数</returns>
        public static int ExecuteTransaction(string sqlString, object cmdParms = null)
        {
            using (IDbConnection conn = new SqlConnection(Sqlconnection))
            {
                conn.Open();
                IDbTransaction transaction = conn.BeginTransaction();
                int row = conn.Execute(sqlString, cmdParms, transaction, null, null);
                transaction.Commit();
                return row;
            }
        }
        /// <summary>
        /// 运行事务，执行多条SQL 并返回每条影响的行数
        /// </summary>
        /// <param name="execteParameter">多条运行的变量</param>
        /// <returns>按入口参数顺序返回每条SQL的影响行数</returns>
        public static int[] ExecuteTransaction(List<ExecuteParameter> execteParameter)
        {
            using (IDbConnection conn = new SqlConnection(Sqlconnection))
            {
                conn.Open();
                IDbTransaction transaction = conn.BeginTransaction();
                List<int> rows = new List<int>();

                foreach (ExecuteParameter item in execteParameter)
                {
                    rows.Add(conn.Execute(item.SqlString, item.CmdParms, transaction, null, null));
                }
                transaction.Commit();
                return rows.ToArray();
            }
        }

        /// <summary>
        /// 查询多数据集，返回一个针对数据集的Reader
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static SqlMapper.GridReader QueryMultiple(string sqlString, object cmdParms = null)
        {
            using (IDbConnection conn = new SqlConnection(Sqlconnection))
            {
                conn.Open();
                SqlMapper.GridReader gridReader = conn.QueryMultiple(sqlString, cmdParms);
                return gridReader;
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int ExecuteProc(string procName, DynamicParameters param)
        {
            using (IDbConnection conn = new SqlConnection(Sqlconnection))
            {
                conn.Open();
                return conn.Execute(procName, param, null, null, CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// sql执行的变量
        /// </summary>
        public class ExecuteParameter
        {
            public ExecuteParameter()
            {
                CmdParms = null;
            }
            /// <summary>
            /// SQL语句
            /// </summary>
            public string SqlString { get; set; }
            /// <summary>
            /// 包含条件或参数值的对像
            /// </summary>
            public object CmdParms { get; set; }
        }
    }


}