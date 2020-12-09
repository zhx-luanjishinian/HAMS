using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace HAMS.Teacher.TeacherView
{
    /// <summary>
    /// TeacherMainForm.xaml 的交互逻辑
    /// </summary>
    public partial class TeacherMainForm : Window
    {
        public TeacherMainForm(string session)
        {
            InitializeComponent();
            try { if (session != null) { tbTeacherInfo.Text = session; } }
            catch (Exception ex)
            {
                throw new Exception("界面间传值发生异常" + ex.Message);
            }
            //成功动态加载控件
            String sql = "select className from class where classId=@id";
            //传入要填写的参数
            MySqlParameter parameter = new MySqlParameter("@id", 1);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, parameter);
            TeachClass test1 = new TeachClass();
            TeachClass test2 = new TeachClass();
            test1.labelStudentNumber.Content = table.Rows[0][0];
            listViewTeacherClass.Items.Add(test1);
            listViewTeacherClass.Items.Add(test2);
          
        }
        

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();    //注销，关闭系统
        }

        private void btnRecntNo1_Click(object sender, RoutedEventArgs e)
        {

            if (true)//里面是验证函数
            {
                // 打开子窗体
                BreifView newBreifView = new BreifView();
                newBreifView.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }
        private void createClassButton()
        {
            //String sql = "select teacherSpecId,name,password from  where teacherSpecId=@id";
            ////传入要填写的参数
            //MySqlParameter parameter = new MySqlParameter("@id", account);
            //DataTable table = DataUtil.DataOperation.DataQuery(sql, parameter);
            ////MessageBox.Show(table.Rows[0][0].ToString());->stuSpecId
            ////MessageBox.Show(table.Rows[0][1].ToString());->name
            ////MessageBox.Show(table.Rows[0][2].ToString());->password
            //return table;
            //DataContext q = new DataContext
            //for (int i = 0; i <)
        }
    }
}
