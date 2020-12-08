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
    /// AnswerQuestion.xaml 的交互逻辑
    /// </summary>
    public partial class AnswerQuestion : Window
    {
        public AnswerQuestion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            CheckingClassHomework newHomeworkNoticeCheck = new CheckingClassHomework();
            newHomeworkNoticeCheck.Show();
            // 隐藏自己(父窗体)
            this.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
