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
        public String classSpecId { set; get; }
       
        public MainInfo(String classSpecId)
        {
            InitializeComponent();
            this.classSpecId = classSpecId;//只有真实课堂号一个参数
            
        }

        private void class1_Click(object sender, RoutedEventArgs e)
        {
            

        }
    }
}
