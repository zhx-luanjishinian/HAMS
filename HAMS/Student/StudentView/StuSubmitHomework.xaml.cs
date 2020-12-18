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
using HAMS.Student.StudentService;

namespace HAMS.Student.StudentView
{
    /// <summary>
    /// StuSubmitHomework.xaml 的交互逻辑
    /// </summary>
    public partial class StuSubmitHomework : Window
    {
        public string pngfile;
        public String account { set; get; }
        public String name { set; get; }
        public String notId { set; get; }
        public String classId { set; get; }
        public String message { set; get; }
       
        public StuSubmitHomework(String account, String name,String notId,String classId,string pgfile,String message)//真实课堂号
        {
            InitializeComponent();
            this.account = account;
            this.name = name;
            this.notId = notId;
            this.classId = classId;
            this.pngfile = pgfile;
            this.message = message;
            
            //设置该img控件的Source
            headImage.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath(System.IO.Path.Combine(System.Environment.CurrentDirectory, pngfile))));

            tbUserNameAc.Text = account + name;
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
                StuDoHomework sdh = new StuDoHomework(account,name,notId,classId,pngfile,message);
                sdh.pngfile = this.pngfile;
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
                StudentMainForm smf = new StudentMainForm(account,name,pngfile);
                smf.pngfile = this.pngfile;
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
                AlertForm af = new AlertForm(account,name,this.pngfile);
                af.pngfile = this.pngfile;
                af.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }


        OpenFileDialog ofd = new OpenFileDialog();//选择文件的对话框
        private void btnChooseFile_Click(object sender, RoutedEventArgs e)
        {
            //文件打开用到openFileDialog，文件保存是SaveFileDialog

            
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
        
        private SService ss = new SService();
        private void btnSubmitHomework_Click(object sender, RoutedEventArgs e)
        {
            //public String SubmitHomework(string classId, string account, string postil, string homUrlName, string notId, string localpath)
            //调用业务层方法，修改数据库Homework表的相应字段
            String homUrlName = tbFileURL.Text;  //获取到文件名，从tbFileURL这个界面控件中获取
            String postil = listViewHomeworkNote.Text; //从listViewHomeworkNote中获取备注的信息
            String localpath = ofd.FileName; //获取作业内容在本地的存储路径
            //System.Windows.MessageBox.Show(localpath);
            //System.Windows.MessageBox.Show(classId);
            //该方法实现向homework表中修改一条作业记录，且返回具体的信息提示用户
            string message = ss.SubmitHomework(name,classId, account, postil, homUrlName, notId, localpath);
            System.Windows.MessageBox.Show(message);
            //System.Windows.MessageBox.Show("提交成功！");//只要一个提示就OK，所以就注释了一个
        }
    }
}
