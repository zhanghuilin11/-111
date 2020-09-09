using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace 简单三层的学习
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string uid = textBox1.Text;
            string pwd = textBox2.Text;
            string sql = "select count( * ) from userinfo where userid = @uid and password = @pwd";
            SqlParameter[] pms = new SqlParameter[]{
            new SqlParameter("@uid",SqlDbType.NVarChar,50){ Value = uid},
            new SqlParameter("@pwd",SqlDbType.NVarChar,50){ Value = pwd}
            };
            int a = (int)SqlHelper.ExecuteScalar(sql, CommandType.Text, pms);
            if (a > 0)
            {
                MessageBox.Show("登录成功");
            }
            else
            {
                MessageBox.Show("登录不是很成功");
            }
        }
    }
}
