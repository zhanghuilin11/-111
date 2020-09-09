using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 简单三层的学习;

namespace Excel读写应用
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Person> people = new List<Person> {
                new Person("zhang",12,"aaa"),
                new Person("zhang",12,"aaa"),new Person("zhang",12,"aaa"),new Person("zhang",12,"aaa"),new Person("zhang",12,"aaa")
            };
            //1,创建工作簿对象
            IWorkbook workbook = new HSSFWorkbook();
            //2,创建工作表对象
            ISheet sheet = workbook.CreateSheet("sheet1");
            
            for (int i = 0; i < people.Count; i++)
            {
                //3,向工作表中插入行
                IRow row = sheet.CreateRow(i);
                //4,在行中创建单元格并向行中的单元格插入值
                row.CreateCell(0).SetCellValue(people[i].name);
                row.CreateCell(1).SetCellValue(people[i].age);
                row.CreateCell(2).SetCellValue(people[i].addr);
            }
            //创建一个文件流
            using (FileStream fswrite = File.OpenWrite("测试.xls"))
            //workbook.Write(fswrite);只能创建.xls结尾的表格，xlsx格式的不支持
            {
                //把内存中的表格写入到磁盘中
                workbook.Write(fswrite);
            }
            MessageBox.Show("创建成功");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (FileStream fsRead = File.OpenRead("123.xls"))
            {
                //
                IWorkbook wk = new HSSFWorkbook(fsRead);
                //
                for (int i = 0; i < wk.NumberOfSheets; i++)
                {
                    ISheet sheet = wk.GetSheetAt(i);
                    for (int j = 0; j < sheet.LastRowNum; j++)
                    {
                        IRow row = sheet.GetRow(j);
                        for (int k = 0; k < row.LastCellNum; k++)
                        {
                            ICell cell = row.GetCell(k);
                            Console.Write("{0} | ", cell.ToString());
                        }
                        Console.WriteLine();
                    }
                    
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            MessageBox.Show(openFileDialog.FileName);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            IWorkbook workbook = new HSSFWorkbook();
            string sql = "select * from drugcodematch";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql,CommandType.Text))
            {
                if (reader.HasRows)
                {
                    ISheet sheet = workbook.CreateSheet("drugcodematch");
                    int rowindex = 0;
                    while (reader.Read())
                    {
                        IRow row = sheet.CreateRow(rowindex);
                        rowindex++;
                        //这种方法只适合用于同一数据类型的表格，不然的一列一列单独获取
                        for (int i = 1; i < 5; i++)
                        {
                            //判断这个格子是不是空，是空的话用0000代替
                           string  val =  reader[i] is DBNull ? null : reader.GetString(i);
                            //把获取的值赋值给表格对应的单元格
                            //判断是否为null，如果是，给单元格格式设为blank
                            if (val != null )
                            {
                                row.CreateCell(i).SetCellValue(val);
                                
                            }
                            else
                            {
                                row.CreateCell(i).SetCellType(CellType.Blank);
                                
                            }
                            
                        }  
                    }   
                }
            }
            //弹出保存文件的保存框
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "工作簿|*.xls";
            //设置默认的后缀为xls
            saveFileDialog.DefaultExt = ".xls";
            saveFileDialog.ShowDialog();
            //创建一个文件流用于存放表格在磁盘上
            //判断是否返回了一个正确的文件地址
            if (DialogResult == DialogResult.OK && saveFileDialog.FileName.Split('.')[saveFileDialog.FileName.Split('.').Length -1] == "xls")
            {
                using (FileStream fswrite = File.OpenWrite(saveFileDialog.FileName))
                {
                    workbook.Write(fswrite);
                }
                MessageBox.Show("导出成功");
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //设置只显示xls和xlsx后缀的文件
            openFileDialog.Filter = "工作簿|*.xls|工作簿|*.xlsx";
            //显示选择文件的框
            openFileDialog.ShowDialog();
            //要执行的sql语句
            //要防止插入重复的数据要么设置其中一列为主键，要么插入之前先判断一次
            string sql = "insert drugcodematch_bak (drugcode1 , drugcode2 , drugname1 , drugname2) values( @drugcode1, @drugcode2 , @drugname1, @drugname2 )";
            int num = 0;
            //根据选择的文件创建一个文件流
            //判断是否选择了一个正确的文件
            string[] filename = openFileDialog.FileName.Split('.');
            if ((DialogResult == DialogResult.OK) && (filename[filename.Length-1] == "xls" | filename[filename.Length - 1] == "xlsx"))
            {
                //判断完毕，解决掉数组
                filename = null;
                using (FileStream fsread = File.OpenRead(openFileDialog.FileName))
                {
                    //把文件流给一个工作簿对象
                    IWorkbook workbook = new HSSFWorkbook(fsread);
                    //获取第一个工作表对象
                    ISheet sheet = workbook.GetSheetAt(0);
                    //获取表中的行
                    for (int i = 0; i <= sheet.LastRowNum; i++)
                    {
                        //每一行给sql语句的参数创建对象
                        SqlParameter[] parameter = new SqlParameter[] {
                        new SqlParameter("@drugcode1",SqlDbType.NVarChar,50),
                        new SqlParameter("@drugcode2",SqlDbType.NVarChar,50),
                        new SqlParameter("@drugname1",SqlDbType.NVarChar,50),
                        new SqlParameter("@drugname2",SqlDbType.NVarChar,50),
                        };
                        //获取一行
                        IRow row = sheet.GetRow(i);
                        //先判断行是不是为null
                        if (row != null)
                        {
                            //获取一行的单元格
                            for (int j = 1; j < row.LastCellNum; j++)
                            {
                                //判断单元格是否为null，如果是就赋值Dbnull.value
                                if (row.GetCell(j) != null)
                                {
                                    //按顺序给sql语句的参数赋值
                                    parameter[j - 1].Value = row.GetCell(j).ToString();
                                }
                                else
                                {
                                    //为空是赋值dbnull.value给参数
                                    parameter[j - 1].Value = DBNull.Value;
                                }

                            }
                            //执行sql语句并记录影响的行数
                            num += SqlHelper.ExecuteNonQuery(sql, CommandType.Text, parameter);
                        }

                    }
                }
                //在文件流之外反馈，及时的释放文件
                MessageBox.Show("导入成功，导入了" + num + "行");
            }
            
            
        }
    }

    public class Person
    {
        public string name;
        public int age;
        public string addr;
        public Person(string name,int age,string addr ) {
            this.name = name;
            this.age = age;
            this.addr = addr;
        }
       
    }
}
