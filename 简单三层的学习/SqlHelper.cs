using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 简单三层的学习
{
    public static class SqlHelper
    {
        private static readonly string cnstr = ConfigurationManager.ConnectionStrings["mssql"].ConnectionString;

        //ExecuteNonQuery  增删改用这个，返回影响的行数
        public static int ExecuteNonQuery(string sql, CommandType cmdtype, params SqlParameter[] pms)
        {
            using (SqlConnection con = new SqlConnection(cnstr))//创建连接对象
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))//出啊关键cmd对象
                {
                    cmd.CommandType = cmdtype;//设置语句执行的方式
                    if (pms != null)//设置语句执行的参数
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    con.Open();
                    return cmd.ExecuteNonQuery();//返回影响的行数
                }
            }
        }

        //ExecuteScalar  返回单个值，第一行的第一列，查count可以用
        public static object ExecuteScalar(string sql, CommandType cmdtype, params SqlParameter[] pms)
        {
            using (SqlConnection conn = new SqlConnection(cnstr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = cmdtype;
                    if (pms != null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        //ExecuteReader  查询列表时使用
        public static SqlDataReader ExecuteReader(string sql, CommandType cmdtype, params SqlParameter[] pms)
        {
            SqlConnection conn = new SqlConnection(cnstr);//不能使用using
            //因为在reader没用使用完时，reader会一直存在，conn也会一直存在，占用连接
            //反之，如果reader还没使用完就将conn关闭，会导致reader无法继续读取后续的数据
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.CommandType = cmdtype;
                if (pms != null)
                {
                    cmd.Parameters.AddRange(pms);
                }
                try
                {
                    conn.Open();
                    return cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    //CommandBehavior.CloseConnection 作用是在使用完reader时顺便关闭conn这个连接
                }
                catch (Exception)
                {
                    conn.Close();//当try中语句出现错误时应当立即关闭conn
                    conn.Dispose();
                    throw;
                }
            }
        }
    }
}
