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
    /// AnnounceNotice.xaml 的交互逻辑
    /// </summary>
    public partial class AnnounceNotice : Window
    {
        public AnnounceNotice()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            BreifView newBreifView = new BreifView();
            newBreifView.Show();
            // 隐藏自己(父窗体)
            this.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
