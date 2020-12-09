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
        string[] className=new string[10];
        string[] classNum=new string[10];
        public TeacherMainForm(string session,string tname)
        {
            InitializeComponent();
            try
            {
                if (session != null)
                {
                    tbTeacherInfo.Text = session;
                    tbTeacherInfo1.Text = tname;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("界面间传值发生异常" + ex.Message);
            }
            TeacherDao.TDao td = new TeacherDao.TDao();
            DataTable table= td.LoadMainFormLeft(tbTeacherInfo.Text);
            TeachClass[] arrayTeachClass = new TeachClass[10];
            //给自定义控件的子控件加属性
            for (int i = 0; i < table.Rows.Count; i++)
            {
                arrayTeachClass[i] = new TeachClass();
                arrayTeachClass[i].Name = "array" + i.ToString();
                arrayTeachClass[i].labelClassId1.Content = table.Rows[i][5];
                
                arrayTeachClass[i].labelClassName1.Content = table.Rows[i][1].ToString();
                listViewTeacherClass.Items.Add(arrayTeachClass[i]);
                arrayTeachClass[i].MouseDown += new System.Windows.Input.MouseButtonEventHandler(mousedown);
                
    }  
    }

        private void mousedown(object sender, MouseButtonEventArgs e)
        {
            //记录点击的是哪个控件
            TeachClass clickTeachClass = (TeachClass)sender;
            //动态加载BreifView界面，需要知道当前课程名，课程id，老师id,老师姓名
            BreifView newBreif = new BreifView(clickTeachClass.labelClassId1.Content.ToString(), clickTeachClass.labelClassName1.Content.ToString(), tbTeacherInfo.Text, tbTeacherInfo1.Text);
            newBreif.Show();
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnEnterClass_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
                BreifView newBreifView = new BreifView("0","0","0","0");
                newBreifView.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }
       
    }
}
