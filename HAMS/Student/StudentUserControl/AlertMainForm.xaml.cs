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
        public String className { set; get; }
        public String notName { set; get; }
        private DispatcherTimer disTimer = new DispatcherTimer();
        private DispatcherTimer disTimer1 = new DispatcherTimer();

        public AlertMainForm(String truDl,String className,String notName, String defDl="")
        {
            InitializeComponent();
            this.truD = truDl;
            this.defD = defDl;
            this.className = className;
            this.notName = notName;
            //显示定时时间
            showTime();
            //进行时间预警操作
            timeAlert();
        }
       //进行预警操作
       private void timeAlert()
        {
            DateTime trueDl = Convert.ToDateTime(truD);
            if (defD == "")
            {
                if (trueDl < DateTime.Now)
                {
                
                    MessageBox.Show("亲，你的" + className + "课堂的作业" + notName + "到达老师设置的截止时间了");

                }

            }
            
            disTimer1.Tick += new EventHandler(alertTime);
            //每隔一分钟进行一次报警
            disTimer1.Interval = new TimeSpan(0, 0, 60);
            disTimer1.Start();
        }
        
        //进行计时操作
        private void showTime()
        {
            disTimer.Tick += new EventHandler(func);
            //每个一秒钟进行计时
            disTimer.Interval = new TimeSpan(0, 0, 0,1);
            disTimer.Start();


        }
        //给天数，时分秒赋值
        private void func(object sender, EventArgs e)
        {
            DateTime trueDl = Convert.ToDateTime(truD);
            
            TimeSpan ts = new TimeSpan();
            //用户没有设置自定义截止时间
            if (defD == "")
            {
                if (trueDl < DateTime.Now)
                {
                    disTimer.Stop();
                }
                else
                {
                    ts = trueDl - DateTime.Now;
                    textBlockTimeLeft1.Text = String.Format("{0:00}:{1:00}:{2:00}:{3:00}", ts.Days.ToString(), ts.Hours.ToString(), ts.Minutes.ToString(), ts.Seconds.ToString());
                }
               
            }
            else
            {
                DateTime defD1 = Convert.ToDateTime(defD);
                if (defD1 < DateTime.Now)
                {
                    disTimer.Stop();
                }
                else
                {
                    ts = defD1 - DateTime.Now;
                    textBlockTimeLeft1.Text = String.Format("{0:00}:{1:00}:{2:00}:{3:00}", ts.Days.ToString(), ts.Hours.ToString(), ts.Minutes.ToString(), ts.Seconds.ToString());
                }
            }
            

        }

        private void btnLimitedTime1_Click(object sender, RoutedEventArgs e)
        {

        }
        //进行每个作业超过设置的预警时间或者老师设置的截至时间的预警操作
        private void alertTime(object sender,EventArgs e)
        {
            DateTime trueDl = Convert.ToDateTime(truD);

            
            //用户没有设置自定义截止时间
            if (defD == "")
            {
                if (trueDl > DateTime.Now)
                {
                    disTimer.Stop();
                }
                else
                {
                    MessageBox.Show("亲，你的" + className + "课堂的作业" + notName + "到达老师设置的截止时间了");
                    
                }

            }
            else
            {
                DateTime defD1 = Convert.ToDateTime(defD);
                if (defD1 > DateTime.Now)
                {
                    disTimer.Stop();
                }
                else
                {
                    MessageBox.Show("亲，你的" + className + "课堂的作业" + notName + "到达自己设置的截止时间了");
                }
            }

        }

    }

   
    
}