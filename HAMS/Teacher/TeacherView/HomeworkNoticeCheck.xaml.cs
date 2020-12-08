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

namespace HAMS.Teacher.TeacherView
{
    /// <summary>
    /// HomeworkNoticeCheck.xaml 的交互逻辑
    /// </summary>
    public partial class HomeworkNoticeCheck : Window
    {
        public HomeworkNoticeCheck()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            // 打开子窗体
            //BreifView newBriefView = new BreifView();
            //newBriefView.Show();
            //// 隐藏自己(父窗体)
            //this.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnAnswerQuestion_Click(object sender, RoutedEventArgs e)
        {
            // 打开子窗体
            AnswerQuestion newAnswerQuestion = new AnswerQuestion();
            newAnswerQuestion.Show();
            // 隐藏自己(父窗体)
            this.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
