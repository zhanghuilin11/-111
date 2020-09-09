using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 模拟三层登录.DAL;

namespace 模拟三层登录.BLL
{
    class userinfoBLL
    {
        /// <summary>
        /// 确认登录信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public Boolean UserLogin(string user,string pwd) 
        {
            userinfoDAL dAL = new userinfoDAL();
            return dAL.CheckUserLogin(user, CommonTool.CommonTool.GetMD5Str(pwd)) > 0;
        }
        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public Boolean AddUser(string user, string pwd)
        {
            userinfoDAL dAL = new userinfoDAL();
            if (dAL.SelectUser(user) < 1)
            {
                return dAL.UserAdd(user, CommonTool.CommonTool.GetMD5Str(pwd)) > 0;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据pkid修改密码
        /// </summary>
        /// <param name="pkid"></param>
        /// <param name="newpwd"></param>
        /// <returns></returns>
        public Boolean ChangePWD(int pkid,string newpwd)
        {
            userinfoDAL dAL = new userinfoDAL();
            return dAL.ChangePWD(pkid, CommonTool.CommonTool.GetMD5Str(newpwd)) >0 ;
        }
        /// <summary>
        /// 获取对应userid的pkid
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int SelectPKid(string user)
        {
            userinfoDAL dAL = new userinfoDAL();
            return dAL.SelectPKid(user);
        }

    }
}
