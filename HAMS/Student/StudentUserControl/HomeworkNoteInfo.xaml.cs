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

namespace HAMS.Student.StudentUserControl
{
    /// <summary>
    /// HomeworkNoteInfo.xaml 的交互逻辑
    /// </summary>
    public partial class HomeworkNoteInfo : UserControl
    {
        public String notId { set; get; }
        public String classSpecId { set; get; }
        public HomeworkNoteInfo(String notId,String classSpecId)
        {
            InitializeComponent();
            this.notId = notId;//作业公告Id
            this.classSpecId = classSpecId;//真实课堂号
           
        }

        private void BtnRecntNo1_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
