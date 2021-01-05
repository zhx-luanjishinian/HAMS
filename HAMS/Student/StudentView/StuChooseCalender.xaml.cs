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
using HAMS.Student.StudentService;
using HAMS.ToolClass;

namespace HAMS.Student.StudentView
{
    /// <summary>
    /// StuChooseCalender.xaml 的交互逻辑
    /// </summary>
    public partial class StuChooseCalender : Window
    {
        public String account { set; get; }
        public String notId { set; get; }
        private SService ss = new SService();

        //构造函数
        public StuChooseCalender(String account,String notId)
        {
            InitializeComponent();
            this.account = account;
            this.notId = notId;
       
        }

        //设置自定义截止时间
        public bool insertDefDeadLine(String account,String notId)
        {
            String time = tpStartDate.Text + " " + tpStartTime.Text;
            BaseResult br = ss.updateDefDeadLine(account, notId, time);
            
            if (br.code==0)
            {
                MessageBox.Show("自定义截止时间设置成功");
                return true;
            }
            else
            {
                MessageBox.Show(br.msg);
                return false;
            }
        }

        //点击确定设置成功之后界面跳转事件
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            //如果自定义截止时间设置成功
            if (insertDefDeadLine(account, notId))
            {
                //将该窗体隐藏
                this.Visibility = Visibility.Hidden;
            }
           
        }

        //点击确定设置取消之后的事件
        private void BtnCancl_Click(object sender, RoutedEventArgs e)
        {
            tpStartDate.Text = "";
            tpStartTime.Text = "";
            this.Visibility = Visibility.Hidden;
        }
    }
}
