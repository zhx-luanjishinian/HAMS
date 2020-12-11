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
using HAMS.Student.StudentService;
using HAMS.ToolClass;
using HAMS.Student.StudentView;

namespace HAMS.Student.StudentView
{
    /// <summary>
    /// AlertForm.xaml 的交互逻辑
    /// </summary>
    public partial class AlertForm : Window
    { 
        public AlertForm()
        {
            InitializeComponent();
        }

        private void btnHomeworkMana_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                //StudentMainForm smf = new StudentMainForm(textBlockUserId.Text);
                //smf.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnLimitedTime1_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                StuChooseCalender smf = new StuChooseCalender();
                smf.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void btnLimitedTime2_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                StuChooseCalender smf = new StuChooseCalender();
                smf.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void btnLimitedTime3_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                StuChooseCalender smf = new StuChooseCalender();
                smf.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

    }
}
