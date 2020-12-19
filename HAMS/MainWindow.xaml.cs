using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using HAMS.Student.StudentService;
using HAMS.Teacher.TeacherService;
using HAMS.Admin.AdminService;
using HAMS.ToolClass;
using HAMS.Student.StudentView;
using HAMS.Teacher.TeacherView;
using HAMS.Admin.AdminView;
using System.Windows.Media.Animation;
using HAMS.Teacher.TeacherDao;
using HAMS.Student.StudentDao;

namespace HAMS
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //将业务逻辑层对象变为私有变量
        private SService sts = new SService();
        private TService tts = new TService();
        private AService ats = new AService();
        
      

        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            txtblockSysNotice.Text = ats.newSysNotice();
        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (radiobtnStudent.IsChecked == true)
            {
                BaseResult br = sts.login(txtUserName.Text, txtPassword.Password);
                if (br.code == 0)
                {
                    MessageBox.Show("恭喜你已登录成功");
                    int sex = sts.getSexByStuSpecId(txtUserName.Text.ToString());
                    string pngfile;
                    //headImage是image控件名
                    if (sex == 0)
                    {
                        pngfile = @"..\..\Resources\女生头像.png";

                    }
                    else
                    {
                        pngfile = @"..\..\Resources\男生头像.png";

                    }
                    StudentMainForm smf = new StudentMainForm(txtUserName.Text,(string)br.data, pngfile);
                    
                    smf.Show();
                    this.Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show(br.msg);
                }
            }
            else if (radiobtnTeacher.IsChecked == true)
            {
                BaseResult br = tts.Login(txtUserName.Text, txtPassword.Password);
                if (br.code == 0)
                {
                    MessageBox.Show("恭喜你已登录成功");
                   
                    int sex = tts.getSexByTeaSpecId(txtUserName.Text.ToString());

                    string pngfile;
                    //headImage是image控件名
                    if (sex == 0)
                    {
                        pngfile = @"..\..\Resources\女教师.png";
                       
                    }
                    else
                    {
                        pngfile = @"..\..\Resources\男教师.png";

                    }
                    TeacherMainForm tmf = new TeacherMainForm(txtUserName.Text, (string)br.data,pngfile);
                    //txtUserName.Text是教师工号Z0004520
                    //(string)br.data是刘树栋
                    tmf.Show();
                    this.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show(br.msg);
                }
            }
            else if (radiobtnAdmin.IsChecked == true)
            {
                BaseResult br = ats.login(txtUserName.Text, txtPassword.Password);
                if (br.code == 0)
                {
                    MessageBox.Show("恭喜你已登录成功");

                    AdminIndex sm = new AdminIndex(txtUserName.Text, (string)br.data);
                    sm.Show();
                }
                else
                {
                    MessageBox.Show(br.msg);
                }
            }
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            string constr = "server=182.92.220.26;Uid=HAMS;password=HAMS;Database=HAMS";
            MySqlConnection conn = new MySqlConnection(constr);
            try
            {
                conn.Open();
                MySqlCommand mycmd = new MySqlCommand("insert into admin(password,name,sex) values('dikd3939','紫梓','女')", conn);
                if (mycmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("插入成功");

                }
                Console.ReadLine();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }

        private void RadiobtnTeacher_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RadiobtnStudent_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RadiobtnAdmin_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtUserName.Text = "";
            txtPassword.Password = "";
        }

        private void TxtUserName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();    //注销，关闭系统
        }
    }
    
}
