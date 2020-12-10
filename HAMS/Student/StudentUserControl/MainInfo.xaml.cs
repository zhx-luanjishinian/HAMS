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
using HAMS.Student.StudentView;

namespace HAMS
{
    /// <summary>
    /// MainInfo.xaml 的交互逻辑
    /// </summary>
    public partial class MainInfo : UserControl
    {
        public MainInfo()
        {
            InitializeComponent();
        }

        private void class1_Click(object sender, RoutedEventArgs e)
        {
            StuMainHomework smh = new StuMainHomework();
                
        }
    }
}
