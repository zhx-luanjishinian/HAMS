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
        public AlertForm(String account,String name)
        {
            InitializeComponent();
            this.account = account;
            this.name = name;
            this.textBlockUserId.Text = account + name;
            showAertInfo();
            
        }
        //显示预警主界面信息的函数
        public void showAertInfo()
        {
            List< List<String>> result = ss.showAlertFormInfo(account);
            for(int i = 0; i < result.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                //首先默认用户没有设置自定义的时间
                AlertMainForm amf = new AlertMainForm(result[i][1]);
                if (result[i].Count > 4) {
                 amf = new AlertMainForm(result[i][1],result[i][4]);
                }
              
                if (DateTime.Compare(Convert.ToDateTime(result[i][1]), DateTime.Now) < 0)
                {
                    //如果超过了截止时间就变红
                    var bc = new BrushConverter();

                    amf.bor.Background = (Brush)bc.ConvertFrom("#FF0000");
                }
                amf.textBlockHomeworkOrder1.Text = (i+1).ToString();
                amf.textBlockHomeworkName1.Text = result[i][2];
                amf.textBlockClassName1.Text = result[i][0];
                //设置作业难度选项
                String[] rank = new String[5] {"最难","偏难","中等","偏易","最易" };
                for (int j = 0; j < 5; j++)
                {
                    ComboBoxItem cbi = new ComboBoxItem();       
                    amf.comboBoxDegree1.Items.Add(rank[j]);

                }
                amf.btnLimitedTime1.Click += new RoutedEventHandler(btnDeadline_Click);
                lvi.Content = amf;
                listView2.Items.Add(lvi);
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

       
    }
}
