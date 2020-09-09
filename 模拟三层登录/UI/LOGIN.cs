using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 模拟三层登录.BLL;

namespace 模拟三层登录.UI
{
    public partial class LOGIN : Form
    {
        public LOGIN()
        {
            InitializeComponent();
        }

        private void LOGIN_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text.Trim();
            string pwd = textBox2.Text;
            userinfoBLL bLL = new userinfoBLL();
            if (bLL.UserLogin(user,pwd))
            {
                MessageBox.Show("登录成功！");
            }
            else
            {
                MessageBox.Show("登录失败");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text.Trim();
            string pwd = textBox2.Text;
            userinfoBLL bLL = new userinfoBLL();
            if (bLL.UserLogin(user, pwd))
            {
                MessageBox.Show("登录成功！");
                CommonTool.LogUser.pkid = bLL.SelectPKid(user);
                CommonTool.LogUser.userid = user;
                CommonTool.LogUser.pwd = pwd;
                button3.Enabled = true;
            }
            else
            {
                MessageBox.Show("登录失败");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text.Trim();
            string pwd = textBox2.Text;
            userinfoBLL bLL = new userinfoBLL();
            if (bLL.AddUser(user,pwd))
            {
                MessageBox.Show("注册成功");
            }
            else
            {
                MessageBox.Show("注册失败，用户名已存在");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChangePWD changePWD = new ChangePWD();
            changePWD.ShowDialog();
        }
    }
}
