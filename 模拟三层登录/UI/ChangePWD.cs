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
    public partial class ChangePWD : Form
    {
        public ChangePWD()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string oldpwd = textBox1.Text;
            string newpwd1 = textBox2.Text;
            string newpwd = textBox3.Text;
            if (newpwd == newpwd1)
            {
                if (oldpwd == CommonTool.LogUser.pwd)
                {
                    userinfoBLL bLL = new userinfoBLL();
                    if (bLL.ChangePWD(CommonTool.LogUser.pkid, newpwd))
                    {
                        MessageBox.Show("密码修改成功");
                        //修改成功，关闭修改密码窗口，修改密码enable=false，清空loguser，重新登录
                    } 
                }
                else
                {
                    MessageBox.Show("旧密码输入错误");
                }
            }
            else
            {
                MessageBox.Show("新密码不一致");
            }

        }
    }
}
