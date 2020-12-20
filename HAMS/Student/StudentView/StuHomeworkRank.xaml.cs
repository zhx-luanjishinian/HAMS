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
using Visifire.Charts;
using HAMS.Student.StudentService;


namespace HAMS.Student.StudentView
{
    /// <summary>
    /// StuHomeworkRank.xaml 的交互逻辑
    /// </summary>
    public partial class StuHomeworkRank : Window
    {
        public string pngfile;
        public String notId { set; get; }
        public String classSpecId { set; get; }
        public String account { set; get; }
        public String name { set; get; }
        private SService ss = new SService();
       

        public StuHomeworkRank(String account,String name,String notId,String classSpecId,string pgfile)
        {
            InitializeComponent();
            this.notId = notId;
            this.classSpecId = classSpecId;
            this.account = account;
            this.name = name;
            tbUserNameAc.Content = account + " "+ name;
            this.pngfile = pgfile;
            //设置该img控件的Source
            headImage.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath(System.IO.Path.Combine(System.Environment.CurrentDirectory, pngfile))));

            //Chart chart = new Chart();
            //创建一个标题的对象
            //Title title = new Title();

            //设置标题的名称
            //title.Text = "点击上方按钮绘制统计图";//这个没法居中
            
            //向图标添加标题
            //chart.Titles.Add(title);
           // ca.Children.Add(chart);

            //initPie();//不知道为什么无法显示
        }
        //绘制提交排行榜柱状图
        public void initColumn()
        {
            List<String> info = ss.getHomeworkDoneInfo(account, notId);
            Dictionary<String, int> result = ss.getTimeAndUsers(classSpecId, notId);
            String[] valueX = result.Keys.ToArray<String>();
            int[] valueY = result.Values.ToArray<int>();
            //创建一个图标
            Chart chart = new Chart();
            

            //设置图标的宽度和高度
            chart.Width = 600;
            chart.Height = 260;
            //chart.Margin = new Thickness(100, 5, 10, 5);
            //是否启用打印和保持图片
            chart.ToolBarEnabled = false;

            //设置图标的属性
            chart.ScrollingEnabled = false;//是否启用或禁用滚动
            chart.View3D = true;//3D效果显示

            //创建一个标题的对象
            Title title = new Title();

            //设置标题的名称
            title.Text = "该项作业每日完成人数柱状图";
            title.Padding = new Thickness(0, 10, 5, 0);
            //向图标添加标题
            chart.Titles.Add(title);

            Axis yAxis = new Axis();
            //设置图标中Y轴的最小值永远为0           
            yAxis.AxisMinimum = 0;
            //设置图表中Y轴的后缀          
            yAxis.Suffix = "人";
            chart.AxesY.Add(yAxis);
            // 创建一个新的数据线。               
            DataSeries dataSeries = new DataSeries();
            // 设置数据线的格式
            dataSeries.RenderAs = RenderAs.StackedColumn;//柱状Stacked
            // 设置数据点              
            DataPoint dataPoint;
            for (int i = 0; i < valueX.Length; i++)
            {
                // 创建一个数据点的实例。                   
                dataPoint = new DataPoint();
                //当学生提交了作业时就进行标记
                if(info[3]!=""&&info[4].Substring(0,10)==valueX[i])
                {
                    MessageBox.Show("你已经提交过作业了");
                    // Label lb = new Label();
                    //Title t = new Title();
                    //t.Text = "你在这里";
                   // //获取控件在chart中的坐标
                   //Point point = dataPoint.TransformToAncestor(window).Transform(new Point(0,0));
                   //dataPoint.TranslatePoint(point, t);
                  // chart.Titles.Add(t);
                    // lb.Content = "你在这里";5


                }
                // 设置X轴点  
      
                dataPoint.AxisXLabel = valueX[i];
                //设置Y轴点                   
                dataPoint.YValue = valueY[i];
                //添加数据点                   
                dataSeries.DataPoints.Add(dataPoint);
            }
            // 添加数据线到数据序列。                
            chart.Series.Add(dataSeries);
            //将图表添加到ca中
            ca.Children.Add(chart);
        }
        //绘制提交人数饼图
        public void initPie()
        {
            Dictionary<String, int> result = ss.getNums(classSpecId, notId);
            String[] valueX = result.Keys.ToArray<String>();
            int[] valueY = result.Values.ToArray<int>();

            //创建一个图标
            Chart chart = new Chart();

            //设置图标的宽度和高度
            chart.Width = 600;
            chart.Height = 260;
            //chart.Margin = new Thickness(100, 5, 10, 5);
            //是否启用打印和保持图片
            chart.ToolBarEnabled = false;

            //设置图标的属性
            chart.ScrollingEnabled = false;//是否启用或禁用滚动
            chart.View3D = true;//3D效果显示

            //创建一个标题的对象
            Title title = new Title();

            //设置标题的名称
            title.Text = "作业完成情况饼状图";
            title.Padding = new Thickness(0, 10, 5, 0);
            //向图标添加标题
            chart.Titles.Add(title);
            // 创建一个新的数据线。               
            DataSeries dataSeries = new DataSeries();

            // 设置数据线的格式
            dataSeries.RenderAs = RenderAs.Pie;//柱状Stacked


            // 设置数据点              
            DataPoint dataPoint;
            for (int i = 0; i < valueX.Length; i++)
            {
                // 创建一个数据点的实例。                   
                dataPoint = new DataPoint();
                // 设置X轴点                    
                dataPoint.AxisXLabel = valueX[i];

                dataPoint.LegendText = "##" + valueX[i];
                //设置Y轴点                   
                dataPoint.YValue = valueY[i];
                //添加数据点                   
                dataSeries.DataPoints.Add(dataPoint);
            }

            // 添加数据线到数据序列。                
            chart.Series.Add(dataSeries);
            //将图表添加到ca中
            ca.Children.Add(chart);
        }

        private void BtnShowChartData_Click(object sender, RoutedEventArgs e)
        {
            //设置按钮的背景颜色
            btnShowChartData.Background = new SolidColorBrush(Color.FromRgb(166, 221, 221));
            btnShowNumber.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            ca.Children.Clear();
            initColumn();
        }

        private void BtnShowNumber_Click(object sender, RoutedEventArgs e)
        {
            //设置按钮的背景颜色
            btnShowNumber.Background = new SolidColorBrush(Color.FromRgb(166, 221, 221));
            btnShowChartData.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            ca.Children.Clear();
            ca.Children.Clear();
            initPie();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                StuMainHomework smh = new StuMainHomework(account, name, classSpecId,pngfile);
                smh.pngfile = this.pngfile;
                smh.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void HomeworkManagement_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                StudentMainForm smf = new StudentMainForm(account, name,pngfile);
                smf.pngfile = this.pngfile;
                smf.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void HomeworkAlert_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                AlertForm af = new AlertForm(account, name,pngfile);
                af.pngfile = this.pngfile;
                af.Show();
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
