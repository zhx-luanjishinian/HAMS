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

namespace HAMS.Teacher.TeacherUserControl
{
    /// <summary>
    /// StudentCheck.xaml 的交互逻辑
    /// </summary>
    public partial class StudentCheck : UserControl
    {
        //这是用来记录当前控件在所有动态生成控件中的下标，方便根据这个下标找到该homId在homId数组中的索引，用于实现TeacherHomeworkCheck界面的下一个功能
        public int index { get; set; }
        public StudentCheck(int index)
        {
            InitializeComponent();
            this.index = index;
        }
    }
}
