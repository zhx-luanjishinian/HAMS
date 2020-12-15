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
        public String content { set; get; }
        public String notId { set; get; }

        //public String message { set; get; }
        public MainHomeworkInfo(String content,String notId)
        {
            InitializeComponent();
            
            this.notId = notId;
            this.content = content;
            //this.message = message;
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
