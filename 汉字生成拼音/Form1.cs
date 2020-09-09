using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 汉字生成拼音
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = textBox1.Text.Trim();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if (ChineseChar.IsValidChar(str[i]))
                {
                    ChineseChar chineseChar = new ChineseChar(str[i]);
                    if (chineseChar.Pinyins.Count >0)
                    {
                        builder.Append(chineseChar.Pinyins[0].Substring(0, chineseChar.Pinyins[0].Length - 1));
                    }
                }
                else
                {
                    builder.Append(str[i]);
                }
            }
            textBox2.Text = builder.ToString();
        }
    }
}
