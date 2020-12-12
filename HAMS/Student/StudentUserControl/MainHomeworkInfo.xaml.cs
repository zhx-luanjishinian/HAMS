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

namespace HAMS.Student.StudentUserControl
{
    /// <summary>
    /// MainHomeworkInfo.xaml 的交互逻辑
    /// </summary>
    public partial class MainHomeworkInfo : UserControl
    {
        public String notId { set; get; }
        public String classSpecId { set; get; }
        public MainHomeworkInfo(String notId,String classSpecId)
        {
            InitializeComponent();
            this.notId = notId;
            this.classSpecId = classSpecId;
        }

        private void btnHomeworkAnswer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCheckRank_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnHomRe1_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
