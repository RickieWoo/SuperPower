using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SyncChatServer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MainForm());
            //数据库操作
            // 声明连接对象
            SqlConnection conn = new SqlConnection(SyncChatServer.Properties.Settings.Default.User);
             //打开链接
             try
            {
                //打开链接
                conn.Open();
                //访问数据库并进行相关操作
                System.Threading.Thread.Sleep(3000);
                MessageBox.Show("数据库连接已打开:)");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //关闭连接
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                MessageBox.Show("数据库连接已关闭:(");
            }
           //好像要添加什么操作？？？
        }
    }
    
    public static class DBHeloer
    {
        ///<summary>
        ///获取连接的字符串
        /// </summary>
        public static string GetConnectionString()
        {
            return "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\USERS\\USER\\DESKTOP\\课设\\WINDOWSFORMSAPPLICATION1\\WINDOWSFORMSAPPLICATION1\\BIN\\DEBUG\\CONTACKDB.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
        /// <summary>
        /// 获取连接对象
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(GetConnectionString());
        }
    }
    

    

}
