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
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace HAMS.Student.StudentView
{
    /// <summary>
    /// StuSubmitHomework.xaml 的交互逻辑
    /// </summary>
    public partial class StuSubmitHomework : Window
    {
        public String account { set; get; }
        public String name { set; get; }
        public String notId { set; get; }
        public String classId { set; get; }
        public StuSubmitHomework(String account, String name,String notId,String classId)
        {
            InitializeComponent();
            this.account = account;
            this.name = name;
            this.notId = notId;
            this.classId = classId;
        }

        private void ListViewHomeworkNote_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                StuDoHomework sdh = new StuDoHomework(account,name,notId,classId);
                sdh.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void homeworkManagement_Click(object sender, RoutedEventArgs e)
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

        private void homeworkAlert_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                AlertForm af = new AlertForm(account,name);
                af.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void btnSubmitHomework_Click(object sender, RoutedEventArgs e)
        {
            ////调用业务层方法，往数据库里添加新的作业公告
            //String folderName = tbUserNameAc.Text;
            //String content = tbFileURL.Text;
            //DateTime currentTime = DateTime.Now;
            //String postil = listViewHomeworkNote.Text;
            //String homURL =  "Z004530B1300720" + "/" + "狗" + "/" + folderName +content;
            ////String notURL = 课堂真实号/作业公告名/作业附件/文件名
            
            ////该方法实现向notice表中新增一条作业公告，且返回具体的信息提示用户
            //string message = ans.announceNotice(truDeadline, content, notTitle, classSpecId, tbTeacherSpecId.Text, notURL);
            ////System.Windows.MessageBox.Show(message);
            //System.Windows.MessageBox.Show("提交成功！");
        }

        private void btnChooseFile_Click(object sender, RoutedEventArgs e)
        {
            //文件打开用到openFileDialog，文件保存是SaveFileDialog

            OpenFileDialog ofd = new OpenFileDialog();//选择文件的对话框
            ofd.Title = tbUserNameAc.Text + "同学，请选择要传的文件";
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
                tbFileURL.Text = System.IO.Path.GetFileName(ofd.FileName); ;//获取文件名
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
    }
}
