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
        public HomeworkSubmit()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnComeback_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                StuCheckHomework sch = new StuCheckHomework();
                sch.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void btnHomeworkMana_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
               // StudentMainForm smf = new StudentMainForm(textBlockUserId.Text);
                //smf.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void btnHomeworkAlert_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                AlertForm af = new AlertForm();
                af.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }
    }
}
