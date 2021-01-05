using HAMS.Student.StudentService;
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
using System.Windows.Shapes;

namespace HAMS.Student.StudentView
{
    /// <summary>
    /// HomeworkSubmit.xaml 的交互逻辑
    /// </summary>
    public partial class HomeworkSubmit : Window
    {
        public string pngfile;
        private SService ss = new SService();
        public String name { set; get; }
        public String account { set; get; }
        public String notId { set; get; }
        public String classSpecId { set; get; }

        //构造函数
        public HomeworkSubmit(String account,String name,String notId,String classSpecId,string pgfile)
        {
            InitializeComponent();
            this.name = name;
            this.account = account;
            this.notId = notId;
            this.classSpecId = classSpecId;
            this.pngfile = pgfile;
            //设置该img控件的Source
            headImage.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath(System.IO.Path.Combine(System.Environment.CurrentDirectory, pngfile))));


            textBlockUserId.Text = account +" "+ name;
            List<String> result = ss.showHomeworkInfo(account,notId);
            textBlockGrade.Text = result[0];
            textBoxComment.Text = result[1];
        }

        //注销，退出系统
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        //返回上一界面
        private void btnComeback_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                //String account, String name, String notId, String classId
                StuDoHomework sdh = new StuDoHomework(account, name, notId,classSpecId,pngfile);
                sdh.pngfile = this.pngfile;
                sdh.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        //跳转至作业管理主界面
        private void btnHomeworkMana_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                StudentMainForm smf = new StudentMainForm(account,name,pngfile);
                smf.pngfile = this.pngfile;
                smf.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        //跳转至作业预警主界面
        private void btnHomeworkAlert_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                AlertForm af = new AlertForm(account,name,pngfile);
                af.pngfile = this.pngfile;
                af.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }
    }
}
