using HAMS.Teacher.TeacherService;
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

namespace HAMS.Teacher.TeacherView
{
    /// <summary>
    /// HomeworkStatistic.xaml 的交互逻辑
    /// </summary>
    public partial class HomeworkStatistic : Window
    {
        //已经在上一个界面生成此界面时，将该赋的值都附好了
        private TService ts = new TService();
        public String notId { set; get; }//作业公告id
        public String notTitle { set; get; }
        public String tSpecId { set; get; }
        public String tName { set; get; }
        public string description;
        public string classSpecId;
        public string className;
        public string pngfile;//头像路径


        public HomeworkStatistic(string pgfile)
        {
           
            InitializeComponent();
            this.pngfile = pgfile;
            //设置该img控件的Source
            headImage.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath(System.IO.Path.Combine(System.Environment.CurrentDirectory, pngfile))));

            Chart chart = new Chart();
            //创建一个标题的对象
            Title title = new Title();

            //设置标题的名称
            title.Text = "点击上方按钮绘制统计图";
            title.Padding = new Thickness(0, 10, 5, 0);
            //向图标添加标题
            chart.Titles.Add(title);
            ca.Children.Add(chart);
            
            //initPie();//不知道为什么无法显示
            


        }

        //绘制成绩等级统计柱状图
        public void initColumn()
        {
            Dictionary<String, int> result = ts.getScoreAndNums(notId);
            String[] valueX = result.Keys.ToArray<String>();
            int[] valueY = result.Values.ToArray<int>();
            //创建一个图标
            Chart chart = new Chart();

            //设置图标的宽度和高度
            //chart.Width = 520;
            //chart.Height = 230;

            //chart.Margin = new Thickness(100, 5, 10, 5);
            //是否启用打印和保持图片
            chart.ToolBarEnabled = false;

            //设置图标的属性
            chart.ScrollingEnabled = false;//是否启用或禁用滚动
            chart.View3D = true;//3D效果显示

            //创建一个标题的对象
            Title title = new Title();

            //设置标题的名称
            title.Text = "成绩等级统计柱状图";
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

        //绘制完成情况统计饼图
        public void initPie()
        {
            Dictionary<String, int> result = ts.getNums(notId);
            String[] valueX = result.Keys.ToArray<String>();
            int[] valueY = result.Values.ToArray<int>();

            //创建一个图标
            Chart chart = new Chart();

            //设置图标的宽度和高度
            //chart.Width = 520;
            //chart.Height = 230;

            //chart.Margin = new Thickness(100, 5, 10, 5);
            //是否启用打印和保持图片
            chart.ToolBarEnabled = false;

            //设置图标的属性
            chart.ScrollingEnabled = false;//是否启用或禁用滚动
            chart.View3D = true;//3D效果显示

            //创建一个标题的对象
            Title title = new Title();

            //设置标题的名称
            title.Text = "完成情况统计饼图";
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

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            CheckingClassHomework cch = new CheckingClassHomework(tbNotTitle.Text, description, tSpecId, tName, classSpecId, className, this.pngfile);
            cch.pngfile = this.pngfile;
            cch.Show();
            this.Visibility = System.Windows.Visibility.Hidden;

            
        }

        private void btnScoreStatistic_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnFinishStatistic_Click(object sender, RoutedEventArgs e)
        {
            //设置按钮的背景颜色
            btnScoreStatistic.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            btnFinishStatistic.Background = new SolidColorBrush(Color.FromRgb(166, 221, 221));
            ca.Children.Clear();

            //绘制完成统计饼图
            initPie();
        }

        private void btnScoreStatistic_Click_1(object sender, RoutedEventArgs e)
        {
            
            //设置按钮的背景颜色
            btnScoreStatistic.Background = new SolidColorBrush(Color.FromRgb(166,221,221));
            btnFinishStatistic.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            ca.Children.Clear();

            //绘制得分统计柱状图
            initColumn();
        }
    }
}
