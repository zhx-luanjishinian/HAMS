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
using System.Windows.Forms;//要在依赖中引入Window.Forms,并添加这个命名空间
using System.Text.RegularExpressions;
using HAMS.Teacher.TeacherService;
using HAMS.ToolClass;

namespace HAMS.Teacher.TeacherView
{
    /// <summary>
    /// AnnounceNotice.xaml 的交互逻辑
    /// </summary>
    public partial class AnnounceNotice : Window
    {
        public AnnounceNotice(string tNum,string tName,string cId, string cName)
        {
            InitializeComponent();
            tbName.Text = tName;
            tbTeacherSpecId.Text = tNum;
            labelcClassName.Content = cName;
            lbClassSpecId.Text = cId;
            textBoxContent.Text = "请输入作业描述";
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            BreifView newBreifView = new BreifView(lbClassSpecId.Text,labelcClassName.Content.ToString(),tbTeacherSpecId.Text,tbName.Text);
            newBreifView.Show();
            // 隐藏自己(父窗体)
            this.Visibility = System.Windows.Visibility.Hidden;
        }
        OpenFileDialog ofd = new OpenFileDialog();//选择文件的对话框
        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            //文件打开用到openFileDialog，文件保存是SaveFileDialog
            ofd.Title = tbName.Text + "老师，请选择要传的文件";
            ofd.InitialDirectory = @"C:\Users";//打开本地文件框的起始路径
            string filter = @"文本文档|*.txt;*.pdf;*.doc;*.html;*.wps;*.rtf";
            /*
            string filter = @"所有文件|*.*|
                           压缩文件|*.zip;*.rar;*.arj|
                           文本文档|*.txt;*.pdf;*.doc;*.html;*.wps;*.rtf|
                           图片文件|*.jpg;*.png;*.gif;*.jpeg;*.bmp|
                           视频文件|*.avi;*.mp3;*.swf;*.mpg;*.mov|
                           系统文件|*.int;*.sys;*.dll;*.adt|
                           可执行文件|*.exe;*.com;*.bat;*.vbs";
            */

            ofd.Filter = Regex.Replace(filter, @"\s", ""); //筛选文件

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                upload.Text = System.IO.Path.GetFileName(ofd.FileName); ;//获取文件名
                                                                         //System.IO.Path.GetFullPath(ofd.FileName);//获取文件路径
                                                                         //ofd.FileName//获取文件路径
                                                                         // string fileName = System.IO.Path.GetFileName(ofd.FileName);//获取文件名
                                                                         //string extension = System.IO.Path.GetExtension(fileName);//获取文件扩展名

                /*
                //令选择的文件可以以图片形式显示在image控件上
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();  //给BitmapImage对象赋予数据的时候，需要用BeginInit()开始，用EndInit()结束
                bitmapImage.UriSource = new Uri(@pngfile);//@+引用这个图片pngfile
                //bitmapImage.DecodePixelWidth = 320;   //对大图片，可以节省内存。尽可能不要同时设置DecodePixelWidth和DecodePixelHeight，否则宽高比可能改变
                bitmapImage.EndInit();
                image1.Source = bitmapImage;
                */
            }
        }

        private AnnounceNoticeService ans = new AnnounceNoticeService();
        
        private void btnAnnounce_Click(object sender, RoutedEventArgs e)
        {
            //调用业务层方法，往数据库里添加新的作业公告
            String notTitle = textBoxHomeworkTitle.Text;
            String content = textBoxContent.Text;
           
            if(calTruDeadline.SelectedDate == null)//如果没有选择截止日期，默认两天后
            {
                calTruDeadline.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day+2); ;//给日历控件截止时间设置默认为当前时间两天之后
            }
            DateTime truDeadline = calTruDeadline.SelectedDate.Value;
            String localpath = ofd.FileName;
            String classSpecId = lbClassSpecId.Text;
            //String notURL = 课堂真实号/作业公告名/作业附件/文件名
            //String notURL = classSpecId + "/" + notTitle + "/" + "作业附件" + fileName;
            //该方法实现向notice表中新增一条作业公告，且返回具体的信息提示用户
            string message = ans.announceNotice(truDeadline, content, notTitle, classSpecId,tbTeacherSpecId.Text, localpath);
            System.Windows.MessageBox.Show(message);
        }


      

        private void textBoxHomeworkTitle_GotFocus(object sender, RoutedEventArgs e)
        {
            textBoxHomeworkTitle.Text = "";  //实现鼠标点击时提示文字消失
        }

        private void textBoxContent_GotFocus(object sender, RoutedEventArgs e)
        {
            textBoxContent.Text = "";
        }
    }
}


    

