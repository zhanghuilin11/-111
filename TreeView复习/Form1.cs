using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 简单三层的学习;

namespace TreeView复习
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //清空treeview
            treeView1.Nodes.Clear();
            SetNods(treeView1.Nodes, 0);    
        }
        
        /// <summary>
        /// 给treeview添加节点
        /// </summary>
        /// <param name="nodes">节点</param>
        /// <param name="v">父节点id</param>
        private void SetNods(TreeNodeCollection nodes, int v)
        {
            //从数据库取到对应id的节点父节点和名称
            DataTable dt= GetNod(v);
            foreach (DataRow item in dt.Rows)
            {
                //获取节点id和名称
                int id = Convert.ToInt32(item[0]);
                string nod = item[1].ToString();
                //将节点加到treeview中
                TreeNode node =  nodes.Add(nod);
                node.Tag = id;
                //递归，获取改节点的子节点
                SetNods(node.Nodes, id);
            }
        }

        public static DataTable GetNod(int pid)
        {
            string sql = "select * from city where parid = @pid";
            SqlParameter[] pms = new SqlParameter[] {
                new SqlParameter("@pid", SqlDbType.NVarChar, 50) { Value = pid } 
            };
            using (DataTable tab = SqlHelper.ExecuteDataTable(sql, CommandType.Text, pms))
            {
                return tab;
            }
        }
    }
}
