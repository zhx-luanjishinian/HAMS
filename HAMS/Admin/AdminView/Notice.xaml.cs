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
using System.Data;
using HAMS.Admin.AdminDao;
using System.Windows.Threading;

namespace HAMS.Admin.AdminView
{
    
    /// <summary>
    /// Notice.xaml 的交互逻辑
    /// </summary>
    public partial class Notice : Window
    {
        private ADao ad = new ADao();
        public String title { get; set; }
        public String content { get; set; }
        public int sysid { get; set; }
        public String ID { get; set; }
        DispatcherTimer timer = new DispatcherTimer();
        public Notice(string AdminId)
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromSeconds(0.1);   //设置刷新的间隔时间
            timer.Start();
            
            //txtboxReleaseTime.Text = DateTime.Now.ToString();
            this.sysid = -1;
            this.ID = AdminId;
        }
        
        public Notice(string id,string title, string content)
        {
            this.title = title;
            this.content = content;
            this.sysid = int.Parse(id);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromSeconds(0.1);   //设置刷新的间隔时间
            timer.Start();
            InitializeComponent();
            txtboxNoticeTitle.Text = title;
            txtboxNoticeContent.Text = content;
        }
        private void timer_Tick(object sender, EventArgs e)//计时执行的程序
        {

            txtboxReleaseTime.Text = string.Concat(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));

        }
        private void BtnAnnounce_Click(object sender, RoutedEventArgs e)
        {
            string sysTitle = txtboxNoticeTitle.Text;
            string sysContent=txtboxNoticeContent.Text;
            if (sysid == -1)
            {
                if (ad.insertSysNotice(sysTitle, sysContent,ID))
                {
                    MessageBox.Show("系统通知发布成功");
                    this.Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show("系统通知发布失败");
                    this.Visibility = Visibility.Hidden;
                }
            }
            else { 
            if(ad.updateSysNotice(sysid, sysTitle, sysContent))
            {
                MessageBox.Show("系统通知发布成功");
                    this.Visibility = Visibility.Hidden;
                }
            else
            {
                MessageBox.Show("系统通知发布失败");
                    this.Visibility = Visibility.Hidden;
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
