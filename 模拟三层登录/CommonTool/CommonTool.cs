using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace 模拟三层登录.CommonTool
{
    public static class CommonTool
    {
        public static string GetMD5Str(string str) 
        {
            StringBuilder builder = new StringBuilder();
            using (MD5 mD5 = MD5.Create())
            {
                byte[] bytes =  System.Text.Encoding.Default.GetBytes(str);
                byte[] vs = mD5.ComputeHash(bytes);
                for (int i = 0; i < vs.Length; i++)
                {
                    builder.Append(vs[i].ToString("x2"));
                }
            }
            return builder.ToString();
        }
    }
}
