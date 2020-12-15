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
using HAMS.Student.StudentView;
using HAMS.Student.StudentUserControl;
using System.Windows.Threading;

namespace HAMS.Student.StudentView
{
    /// <summary>
    /// AlertForm.xaml 的交互逻辑
    /// </summary>
    public partial class AlertForm : Window
    {
        
        public String account{set;get;}
        public String name { set; get; }
        private SService ss = new SService();
        DispatcherTimer disTimer = new DispatcherTimer();
        public AlertForm(String account,String name)
        {
            InitializeComponent();
            this.account = account;
            this.name = name;
            this.textBlockUserId.Text = account + name;
            //展示预警信息
            showAertInfo(ss.showAlertFormInfo(account));
            //初始化操作
            initComboxRank();
            homeNumberAlert();


        }
       
        //显示预警主界面信息的函数
        public void showAertInfo( List<List<List<String>>> info)
        {
            List<List< List<String>>> result = info;
            int count = result[0].Count;
            for(int i = 0; i < result[0].Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                //首先默认用户没有设置自定义的时间,传入具体截止时间，课堂名，作业标题
                AlertMainForm amf = new AlertMainForm(result[0][i][1],result[0][i][0],result[0][i][2]);
                if (result[0][i].Count > 4) {
                 
                    if (result[0][i][4] != "") {
                        //此处相比于先前加入了自定义的截止时间
                        amf = new AlertMainForm(result[0][i][1], result[0][i][0], result[0][i][2], result[0][i][4]);
                        amf.btnLimitedTime1.Content = result[0][i][4];
                    }
                    else
                    {
                        amf.btnLimitedTime1.Content = "截止时间设置";
                    }
                }
                if (result[0][i].Count > 5) {
                if(result[0][i][5]!="")
                {
                    //默认减1获得当前设置的值
                    amf.comboBoxDegree1.SelectedIndex = int.Parse(result[0][i][5])-1;
                    }
                }
                
                amf.textBlockHomeworkOrder1.Text = (i+1).ToString();
                amf.textBlockHomeworkName1.Text = result[0][i][2];
                amf.textBlockClassName1.Text = result[0][i][0];             
                for (int j = 0; j < 5; j++)
                { 
                    amf.comboBoxDegree1.Items.Add(j+1);

                }
                amf.comboBoxDegree1.Tag = result[0][i][3];           
                amf.comboBoxDegree1.SelectionChanged += new SelectionChangedEventHandler(defcomplexity);
                amf.btnLimitedTime1.Click += new RoutedEventHandler(btnDeadline_Click);
                lvi.Content = amf;
                listView2.Items.Add(lvi);
            }
            for(int i = 0; i < result[1].Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                //首先默认用户没有设置自定义的时间,传入具体截止时间，课堂名，作业标题
                AlertMainForm amf = new AlertMainForm(result[1][i][1], result[1][i][0], result[1][i][2]);
                if (result[1][i].Count > 4)
                {

                    if (result[1][i][4] != "")
                    {
                        //此处相比于先前加入了自定义的截止时间
                        amf = new AlertMainForm(result[1][i][1], result[1][i][0], result[1][i][2], result[1][i][4]);
                        amf.btnLimitedTime1.Content = result[1][i][4];
                    }
                    else
                    {
                        amf.btnLimitedTime1.Content = "截止时间设置";
                    }
                }
                if (result[1][i].Count > 5)
                {
                    if (result[1][i][5] != "")
                    {
                        //默认减1获得当前设置的值
                        amf.comboBoxDegree1.SelectedIndex = int.Parse(result[1][i][5]) - 1;
                    }
                }

                amf.textBlockHomeworkOrder1.Text = (i + 1+result[0].Count).ToString();
                amf.textBlockHomeworkName1.Text = result[1][i][2];
                amf.textBlockClassName1.Text = result[1][i][0];
                for (int j = 0; j < 5; j++)
                {
                    amf.comboBoxDegree1.Items.Add(j + 1);

                }
                //超过了截止时间的不计入到里面
                var bc = new BrushConverter();
                amf.bor.Background = (Brush)bc.ConvertFrom("#FF0000");
                amf.comboBoxDegree1.Tag = result[1][i][3];
                amf.comboBoxDegree1.SelectionChanged += new SelectionChangedEventHandler(defcomplexity);
                amf.btnLimitedTime1.Click += new RoutedEventHandler(btnDeadline_Click);
                lvi.Content = amf;
                listView2.Items.Add(lvi);

                //如果超过了截止时间就变红            
                
            }
            //总的作业公告数量-已完成的作业数量=未完成的作业数量
            textBlockUnfinishedNumber.Text = (count-ss.countHomeworkNumber(account)).ToString();
            //直接进行预警数量的设置,库里面没有的话说明数据是为空的
            textBlockAlertNumber.Text = ss.setAlertNum(account);
            
            
            
        }
        //处理每一个控件的选择部分
        private void defcomplexity(object sender,SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if(ss.updateComplexity(account, (String)cb.Tag, (cb.SelectedIndex+1).ToString()))
            {
                MessageBox.Show("作业复杂度设置成功");
            }
            else
            {
                MessageBox.Show("作业复杂度设置失败");
            }
        }
        private void initComboxRank()
        {
            String[] ranks = new string[2] { "降序", "升序" };
            for(int i = 0; i < ranks.Length; i++)
            {
                comBoxByTime.Items.Add(ranks[i]);
                comBoxByDegree.Items.Add(ranks[i]);
            }
        }

        //截止时间设置，打开截止时间设置窗口
        private void btnDeadline_Click(object sender,RoutedEventArgs e)
        {
            StuChooseCalender scc = new StuChooseCalender();
            scc.Show();
        }
      
        private void btnHomeworkMana_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                StudentMainForm smf = new StudentMainForm(account,name);
                smf.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
        //当用户有进行修改时，直接读取用户的修改值插入数据库
        private void TextBlockAlertNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(textBlockAlertNumber.Text!= ss.setAlertNum(account)&&textBlockAlertNumber.Text!="") {
            if (ss.updateAlertNum(textBlockAlertNumber.Text))
            {
                MessageBox.Show("预警数量设置成功");
            }
            else
            {
                MessageBox.Show("预警数量设置失败");
            }
            }
        }

        private void ComBoxByTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comBoxByTime.SelectedValue.ToString() == "升序")
            {
                //先清空listView2里面的东西
                listView2.Items.Clear();
                showAertInfo(ss.upRank(account));      
            }
            else if(comBoxByTime.SelectedValue.ToString() == "降序")
            {
                listView2.Items.Clear();
                showAertInfo(ss.downRank(account));
            }
        }

        private void ComBoxByDegree_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comBoxByDegree.SelectedValue.ToString() == "降序")
            {
                listView2.Items.Clear();
                //进行复杂度的降序排序
                showAertInfo(ss.downComplexity(account));
            }
            else if (comBoxByDegree.SelectedValue.ToString() == "升序")
            {
                listView2.Items.Clear();
                //进行复杂度的升序排序
                showAertInfo(ss.upComplexity(account));
            }
        }
        //进行作业预警数量是否到达的预警
        public void homeNumberAlert()
        {
            if (int.Parse(this.textBlockUnfinishedNumber.Text) > int.Parse(this.textBlockAlertNumber.Text))
            {
                int count = int.Parse(this.textBlockUnfinishedNumber.Text) - int.Parse(this.textBlockAlertNumber.Text);
                MessageBox.Show("亲，你已有" + count.ToString() + "份作业超出了预警范围，请及时完成");
                disTimer.Tick += new EventHandler(funcAlert);
                disTimer.Interval = new TimeSpan(0, 0, 60);
                disTimer.Start();
            }
        }
        //设置每隔1分钟进行一次弹窗提醒
        private void funcAlert(object sender,EventArgs e)
        {
            if (int.Parse(this.textBlockUnfinishedNumber.Text) > int.Parse(this.textBlockAlertNumber.Text))
            {
                int count = int.Parse(this.textBlockUnfinishedNumber.Text) - int.Parse(this.textBlockAlertNumber.Text);
                MessageBox.Show("亲，你已有" + count.ToString() + "份作业超出了预警范围，请及时完成");
            }
        }
        public void homeTimeAlert()
        {

        }


    }
}
