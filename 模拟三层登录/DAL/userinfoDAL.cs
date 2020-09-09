using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 简单三层的学习;

namespace 模拟三层登录.DAL
{
    public class userinfoDAL
    {
        /// <summary>
        /// 新增用户名和密码
        /// </summary>
        /// <param name="user">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public int UserAdd(string user,string pwd)
        {
            string sql = "insert userinfo values( @user , @pwd )";
            SqlParameter[] pms = new SqlParameter[] { 
                new SqlParameter("@user",SqlDbType.NVarChar,50){ Value = user},
                new SqlParameter("@pwd",SqlDbType.NVarChar,50){ Value = pwd}
            };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pms);
        }
        /// <summary>
        /// 查询表中指定用户名的个数
        /// </summary>
        /// <param name="user">用户名</param>
        /// <returns>查询结果的个数</returns>
        public int SelectUser(string user) 
        {
            string sql = "select count(*) from userinfo where userid = @user";
            SqlParameter[] pms = new SqlParameter[] { new SqlParameter("@user", SqlDbType.NVarChar, 50) { Value = user } };
            return (int)SqlHelper.ExecuteScalar(sql, CommandType.Text, pms);
        }
        /// <summary>
        /// 查询用户名密码是否一致
        /// </summary>
        /// <param name="user">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns>返回查到的个数，为1则一致，0不一致</returns>
        public int CheckUserLogin(string user, string pwd)
        {
            string sql = "select count(*) from userinfo where userid = @user and password = @pwd ";
            SqlParameter[] pms = new SqlParameter[] { 
                new SqlParameter("@user", SqlDbType.NVarChar, 50) { Value = user },
                new SqlParameter("@pwd", SqlDbType.NVarChar, 50) { Value = pwd }
            };
            return (int)SqlHelper.ExecuteScalar(sql, CommandType.Text, pms);
        }
        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="pkid">用户id</param>
        /// <param name="newpwd">新密码</param>
        /// <returns></returns>
        public int ChangePWD(int pkid ,string newpwd)
        {
            string sql = " update userinfo set password = @newpwd where pkid = @pkid";
            SqlParameter[] pms = new SqlParameter[] { 
                new SqlParameter("@newpwd",SqlDbType.NVarChar,50){ Value = newpwd},
                new SqlParameter("@pkid",SqlDbType.Int){ Value = pkid}
            };
            return SqlHelper.ExecuteNonQuery(sql,CommandType.Text,pms);
        }
        /// <summary>
        /// 根据用户名查询用户的pkid号
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public  int SelectPKid(string user)
        {
            string sql = " select pkid from userinfo where userid = @user";
            return (int)SqlHelper.ExecuteScalar(sql, CommandType.Text, new SqlParameter("@user", SqlDbType.NVarChar, 50) { Value = user });
        }

       
    }
}
