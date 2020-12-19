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
        public StuChooseCalender(String account,String notId)
        {
            InitializeComponent();
            this.account = account;
            this.notId = notId;
       
        }
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

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            //如果自定义截止时间设置成功
            if (insertDefDeadLine(account, notId))
            {
                //将该窗体隐藏
                this.Visibility = Visibility.Hidden;
            }
           
        }

        private void BtnCancl_Click(object sender, RoutedEventArgs e)
        {
            tpStartDate.Text = "";
            tpStartTime.Text = "";
            this.Visibility = Visibility.Hidden;
        }
    }
}
