using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MD5计算
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("输入要计算的字符串");
            string str = Console.ReadLine();
            string MD5String = GetMD5String(str);
            Console.WriteLine(MD5String);
            Console.ReadKey();
        }

        static string GetMD5String(string str)
        {
            //存放转换成的字符串
            StringBuilder builder = new StringBuilder();
            using (MD5 md5 = MD5.Create())
            {
                //将传进来的字符串转换成byte给MD5的方法使用
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
                //获取MD5计算后的字节数组
                byte[] md5bytes = md5.ComputeHash(bytes);
                for (int i = 0; i < md5bytes.Length; i++)
                {
                    //将转换的字节数组转换成字符串，且均为两位的形式，即1存为01
                    builder.Append(md5bytes[i].ToString("x2"));
                }

            }
            return builder.ToString();
        }
    }
}
