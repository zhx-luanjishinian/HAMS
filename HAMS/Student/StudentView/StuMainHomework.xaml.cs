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
    /// StuMainHomework.xaml 的交互逻辑
    /// </summary>
    public partial class StuMainHomework : Window
    {
        public StuMainHomework()
        {
            InitializeComponent();
        }

        private void homeworkManagement_Click(object sender, RoutedEventArgs e)
        {
            StudentMainForm smf = new StudentMainForm(tbuserNameAc.Text);
        }

        private void btnCheckRank_Click(object sender, RoutedEventArgs e)
        {
            StuHomeworkRank shr = new StuHomeworkRank();
        }

        private void btnHomeworkAnswer_Click(object sender, RoutedEventArgs e)
        {
            StudentAnswerQuestion saq = new StudentAnswerQuestion();
        }

        private void labelHomeworkName_Click(object sender, RoutedEventArgs e)
        {
            StuDoHomework sdh = new StuDoHomework();
        }

        private void homeworkName1_Click(object sender, RoutedEventArgs e)
        {
            StuDoHomework sdh = new StuDoHomework();
        }

        private void homeworkName2_Click(object sender, RoutedEventArgs e)
        {
            StuDoHomework sdh = new StuDoHomework();
        }

        private void homeworkName3_Click(object sender, RoutedEventArgs e)
        {
            StuDoHomework sdh = new StuDoHomework();
        }

        private void homeworkAnswer2_Click(object sender, RoutedEventArgs e)
        {
            StudentAnswerQuestion saq = new StudentAnswerQuestion();
        }

        private void checkRank2_Click(object sender, RoutedEventArgs e)
        {
            StuHomeworkRank shr = new StuHomeworkRank();
        }

        private void checkRank1_Click(object sender, RoutedEventArgs e)
        {
            StuHomeworkRank shr = new StuHomeworkRank();
        }

        private void checkRank3_Click(object sender, RoutedEventArgs e)
        {
            StuHomeworkRank shr = new StuHomeworkRank();
        }

        private void homeworkAnswer3_Click(object sender, RoutedEventArgs e)
        {
            StudentAnswerQuestion saq = new StudentAnswerQuestion();
        }

        private void homeworkAnswer1_Click(object sender, RoutedEventArgs e)
        {
            StudentAnswerQuestion saq = new StudentAnswerQuestion();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
