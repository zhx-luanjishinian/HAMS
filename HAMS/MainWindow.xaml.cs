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
using HAMS.ToolClass;
using HAMS.Student.StudentView;

namespace HAMS
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //将业务逻辑层对象变为私有变量
        private SService sts = new SService();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (radiobtnStudent.IsChecked==true) { 
            BaseResult br = sts.login(txtUserName.Text, txtPassword.Text);
            if (br.code == 0)
            {
                MessageBox.Show("恭喜您登录成功");
                StudentMainForm smf = new StudentMainForm(txtUserName.Text+ (string)br.data);
                    smf.ShowDialog();
            }
            else
            {
                MessageBox.Show(br.msg);
            }
            }
        }

        private void RadiobtnTeacher_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RadiobtnStudent_Checked(object sender, RoutedEventArgs e)
        {
            
        }
    }
    
}
