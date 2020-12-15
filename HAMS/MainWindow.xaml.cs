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
        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (radiobtnStudent.IsChecked == true)
            {
                BaseResult br = sts.login(txtUserName.Text, txtPassword.Text);
                if (br.code == 0)
                {
                    MessageBox.Show("恭喜你已登录成功");

                    StudentMainForm smf = new StudentMainForm(txtUserName.Text,(string)br.data);
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
                BaseResult br = tts.Login(txtUserName.Text, txtPassword.Text);
                if (br.code == 0)
                {
                    MessageBox.Show("恭喜你已登录成功");
                    
                    //TeacherMainForm tmf = new TeacherMainForm(txtUserName.Text + (string)br.data);
                    TeacherMainForm tmf = new TeacherMainForm(txtUserName.Text, (string)br.data);
                    //txtUserName.Text是教师工号Z0004520
                    //(string)br.data是刘树栋
                    tmf.ShowDialog();
                    this.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show(br.msg);
                }
            }
            else if (radiobtnAdmin.IsChecked == true)
            {
                BaseResult br = ats.login(txtUserName.Text, txtPassword.Text);
                if (br.code == 0)
                {
                    MessageBox.Show("恭喜你已登录成功");

                    AdminIndex sm = new AdminIndex(txtUserName.Text + (string)br.data);
                    sm.ShowDialog();
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
            
        }

        private void TxtUserName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
    
}
