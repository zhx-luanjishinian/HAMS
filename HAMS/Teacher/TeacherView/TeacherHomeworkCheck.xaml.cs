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

namespace HAMS.Teacher.TeacherView
{
    /// <summary>
    /// TeacherHomeworkCheck.xaml 的交互逻辑
    /// </summary>
    public partial class TeacherHomeworkCheck : Window
    {
        public TeacherHomeworkCheck(List<int> homIds,int index, string NotTitle,string StudentInfo)
        {
            InitializeComponent();
            //通过上一个界面传递过来的值，进行此界面控件信息的赋值操作

            //给作业公告标题赋值
            lbNotTitle.Content = NotTitle;

            //获取当前待批改作业的作业Id
            int homId = homIds[index];//获得当前待批改作业的作业Id,此处还是一个假值
            homId = 4;

            //给学生信息赋值
            lbStudentInfo.Content = StudentInfo;
            //StudentInfo存储的是学号+1空格+姓名，按空格切分即可获得学号与姓名
            string[] StudentInfos = StudentInfo.Split(' ');
            string StuId = StudentInfos[0];//获取当前待批改作业的学号
            string StuName = StudentInfos[1];//获取当前待批改作业的姓名

            //对下载附件按钮进行初始化：能够实现鼠标放上去之后学生附件的名称，将该按钮和服务器上的某个路径建立关系（这里应该得到数据库中存储的文件路径）
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {

            //1、从数据库中查出homURL,然后获取文件名并能够鼠标放上去时在旁边显示文件名
            //从上一界面获取notId,根据notId+stuId访问到homId

            SaveFileDialog sfd = new SaveFileDialog();
            //要保存的文件的文件名（"文件名和文件类型"）
            string homURL = "we.txt";//数据库中的路径
            sfd.FileName = "we.txt";//这个名字应该是从数据库中取到的文件名
            //初始化提示保存文件的路径地址
            sfd.InitialDirectory = "C:\\Users"; 

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string errorinfo;
                //获取要保存文件名的完整路径
                string localpath = sfd.FileName;
                
                
                bool flag = FtpUpDown.Download(localpath, homURL, out errorinfo);
                //localpath是本地路径,homURL是数据库中存放的文件路径（文件在服务器上的路径）
                if (flag == true)
                    System.Windows.MessageBox.Show("下载成功");
                else
                    System.Windows.MessageBox.Show("下载失败：" + errorinfo + "");
            }
        }
    }
}
