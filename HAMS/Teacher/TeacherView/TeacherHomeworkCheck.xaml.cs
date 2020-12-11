using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HAMS.ToolClass;
using HAMS.Teacher.TeacherService;

namespace HAMS.Teacher.TeacherView
{
    /// <summary>
    /// TeacherHomeworkCheck.xaml 的交互逻辑
    /// </summary>
    public partial class TeacherHomeworkCheck : Window
    {
        private TeacherHomeworkCheckService thc = new TeacherHomeworkCheckService();
        private string homURL; //存储待下载作业文件的路径
        private string homName; //存储待下载作业文件的文件名
        int homId;//存储作业Id
        //无参构造函数，由于前一个界面没有实现，而为了测试下载控件能否正常使用，并不是真正需要用的
        public TeacherHomeworkCheck()
        {
            InitializeComponent();
            //通过上一个界面传递过来的值，进行此界面控件信息的赋值操作

            //给作业公告标题赋值
            //!!lbNotTitle.Content = NotTitle;需要最后取消注释

            //假值，由于前一个界面还没有实现
            lbNotTitle.Content = "猫狗大战期末作业";



            //获取当前待批改作业的作业Id
            //!!int homId = homIds[index];需要最后取消注释

            //获得当前待批改作业的作业Id,此处还是一个假值
            homId = 4;

            //假值，由于前一个界面还没有实现
            string StudentInfo = "2018211XXXXX 小红帽";

            //给学生信息赋值
            lbStudentInfo.Content = StudentInfo;

            //StudentInfo存储的是学号+1空格+姓名，按空格切分即可获得学号与姓名
            string[] StudentInfos = StudentInfo.Split(' ');
            string StuId = StudentInfos[0];//获取当前待批改作业的学号
            string StuName = StudentInfos[1];//获取当前待批改作业的姓名

            //根据homId获取该学生的作业备注并显示在控件上
            lbHomPostil.Content = thc.getPostilByHomId(homId);

            //对下载附件按钮进行初始化：能够实现鼠标放上去之后学生附件的名称，将该按钮和服务器上的某个路径建立关系（这里应该得到数据库中存储的文件路
            //根据homId获得要下载文件在服务器上的路径
            homURL = thc.getHomURLByHomId(homId);

            //对路径按照"/"进行切分，路径为课堂号/作业标题/学生信息文件夹/学生文件名
            string[] homURLs = homURL.Split('/');
            homName = homURLs[homURLs.Length - 1];//获取学生文件名，然后进行显示
            lbUpload.ToolTip = homName;

        }

        //下方函数才是真正需要的
        public TeacherHomeworkCheck(List<int> homIds,int index, string NotTitle,string StudentInfo)
        {
            InitializeComponent();
            //通过上一个界面传递过来的值，进行此界面控件信息的赋值操作

            //给作业公告标题赋值
            lbNotTitle.Content = NotTitle;

            //假值，由于前一个界面还没有实现
            lbNotTitle.Content = "猫狗大战期末作业";



            //获取当前待批改作业的作业Id
            homId = homIds[index];
            

            //给学生信息赋值
            lbStudentInfo.Content = StudentInfo;



            ////StudentInfo存储的是学号+1空格+姓名，按空格切分即可获得学号与姓名
            //string[] StudentInfos = StudentInfo.Split(' ');
            //string StuId = StudentInfos[0];//获取当前待批改作业的学号
            //string StuName = StudentInfos[1];//获取当前待批改作业的姓名

            //根据homId获取该学生的作业备注并显示在控件上
            lbHomPostil.Content = thc.getPostilByHomId(homId);


            //对下载附件按钮进行初始化：能够实现鼠标放上去之后学生附件的名称，将该按钮和服务器上的某个路径建立关系（这里应该得到数据库中存储的文件路
            //根据homId获得要下载文件在服务器上的路径
            homURL = thc.getHomURLByHomId(homId);

            //对路径按照"/"进行切分，路径为课堂号/作业标题/学生信息文件夹/学生文件名
            string[] homURLs = homURL.Split('/');
            
            homName = homURLs[homURLs.Length - 1];//获取学生文件名，然后进行显示
            lbUpload.ToolTip = homName;





        }

        

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {

            //1、从数据库中查出homURL,然后获取文件名并能够鼠标放上去时在旁边显示文件名
            //从上一界面获取notId,根据notId+stuId访问到homId

            SaveFileDialog sfd = new SaveFileDialog();
            
            //设置默认要保存文件的文件名（文件名.扩展名）
            sfd.FileName = homName;//这个名字也是原本服务器上保存文件的文件名
            //初始化提示保存文件的路径地址
            sfd.InitialDirectory = @"C:\Users\郑昊欣\Documents"; 

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string errorinfo;
                //获取要保存文件名的本地完整路径
                string localpath = sfd.FileName;// System.IO.Path.GetFullPath(sfd.FileName);
                //调用下载文件函数，将学生作业从服务器上下载下来，其中localpath是本地路径,homURL是数据库中存放的文件路径（文件在服务器上的路径）
                bool flag = FtpUpDown.Download(localpath, homURL, out errorinfo);
                
                if (flag == true)
                    System.Windows.MessageBox.Show("下载成功");
                else
                    System.Windows.MessageBox.Show("下载失败：" + errorinfo + "");
            }
        }

        private void btnCorrect_Click(object sender, RoutedEventArgs e)
        {
            string score = "";//点评的成绩
            string remark;//点评的分数
            if (rBtnA.IsChecked == true)
            {
                score = (string)rBtnA.Content;//获取该单选框对应的文本值，如“优秀”
                
            }
            else if(rBtnB.IsChecked == true)
            {
                score = (string)rBtnB.Content;
            }
            else if(rBtnC.IsChecked == true)
            {
                score = (string)rBtnC.Content;
            }
            else if (rBtnD.IsChecked == true)
            {
                score = (string)rBtnD.Content;
            }
            remark = tbRemark.Text;

            bool flag = thc.correctHomework(homId,score,remark);
            if (flag)
            {
                System.Windows.MessageBox.Show("作业批改成功:)");
            }
            else
            {
                System.Windows.MessageBox.Show("作业批改失败:(");
            }

        }

        
    }
}
