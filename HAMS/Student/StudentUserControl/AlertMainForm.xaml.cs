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
using System.Windows.Threading;

namespace HAMS.Student.StudentUserControl
{
    /// <summary>
    /// AlertMainForm.xaml 的交互逻辑
    /// </summary>
    public partial class AlertMainForm : UserControl
    {

        //定义真实时间，截止时间以及天数，时分秒
        public String truD { set; get; }
        public String defD { set; get; }
        public int days { set; get; }
        public int hours { set; get; }
        public int minutes { set; get; }
        public int seconds { set; get; }
        private DispatcherTimer disTimer = new DispatcherTimer();

        public AlertMainForm(String truDl, String defDl="")
        {
            InitializeComponent();
            this.truD = truDl;
            this.defD = defDl;
       
            
            //showTime();
        }
        //计时器函数
        private TimeSpan timer()
        {
            //获取系统当前的时间
            TimeSpan time = new TimeSpan();
            DateTime now = DateTime.Now;
            DateTime trueDl = Convert.ToDateTime(truD);
            
            //用户没有设置自定义的截止时间，直接按照老师定义的截至时间进行计时
            if (defD == "")
            {
                //再判断当前时间和老师设置的截止时间的大小关系
                if (DateTime.Compare(trueDl, now) > 0)
                {
                    //计算两个时间的差值，天数，时分秒
                    int days = (trueDl.Date - now.Date).Days;
                    int hours = (trueDl.Date - now.Date).Hours;
                    int minutes = (trueDl.Date - now.Date).Minutes;
                    int seconds = (trueDl.Date - now.Date).Seconds;
                    time = new TimeSpan(days, hours, minutes, seconds);
                }
            }
            else
            {
                DateTime defDl = Convert.ToDateTime(defD);
                int days = (defDl.Date - now.Date).Days;
                int hours = (defDl.Date - now.Date).Hours;
                int minutes = (defDl.Date - now.Date).Minutes;
                int seconds = (defDl.Date - now.Date).Seconds;
                time = new TimeSpan(days, hours, minutes, seconds);
            }

            return time;
        }
        
        //进行计时操作
        private void showTime()
        {
            DateTime now = DateTime.Now;
            TimeSpan ts = new TimeSpan(0, 0, 0, 1);
            //只有大于0的时候才执行这个操作
            if (DateTime.Compare(Convert.ToDateTime(truD), now) > 0) {
                
                disTimer.Interval = ts;
                disTimer.Tick += new EventHandler(func);
                disTimer.Start();       
            }
        }
        //给天数，时分秒赋值
        private void func(object sender, EventArgs e)
        {
            TimeSpan ts = timer();
            days = ts.Days;
            hours = ts.Hours;
            minutes = ts.Minutes;
            seconds = ts.Seconds;
            if (days == 0 && hours == 0 && minutes == 0 && seconds == 0)
            {
                disTimer.Stop();
            }
            else { 
            while (days > 0)
            {
               
                    if (seconds < 0)
                    {
                        seconds = 59;
                        minutes--;
                    }
                    if (minutes < 0)
                    {
                        minutes = 59;
                        hours--;
                    }
                    if (hours < 0)
                    {
                        hours = 24;
                        days--;
                    }
                    
                    textBlockTimeLeft1.Text = String.Format("{0:00}:{1:00}:{2:00}:{3:00}", days, hours, minutes, seconds);
                };
                seconds--;

            }
        }

        private void btnLimitedTime1_Click(object sender, RoutedEventArgs e)
        {

        }
    }

   
    
}