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
using System.Windows.Forms;
using HAMS.ToolClass;

namespace HAMS.Student.StudentView
{
    /// <summary>
    /// StuDoHomework.xaml 的交互逻辑
    /// </summary>
    public partial class StuDoHomework : Window
    {
        private SService ss = new SService();
        public String account { set; get; }
        public String name { set; get; }
        public String notId { set; get; }
        public String classId { set; get; }
        public StuDoHomework(String account,String name)
        {
            InitializeComponent();
            this.account = account;
            this.name = name;
            tbUserNameAc.Text = account + name;
            doHomeworkInfoShow();
        }
        public StuDoHomework(String account, String name,String notId,String classId)
        {
            InitializeComponent();
            this.account = account;
            this.name = name;
            this.notId = notId;
            this.classId = classId;
            tbUserNameAc.Text = account + name;
            doHomeworkInfoShow();
        }
        public void doHomeworkInfoShow()
        {
            List<String> result = ss.showDohomeworkInfo(notId);
            labelHomeworkName.Content = result[0];
            ListBoxItem lbi = new ListBoxItem();
            lbi.Content = result[1];
            listBoxRequest.Items.Add(lbi);
            tbDeadLineTime.Text = result[2];
            if (result[3] != null)
            {
                tbAccessoryName.Content = result[3];
            }
            else
            {
                //如果不存在作业附件则不进行显示
                tbAccessoryName.Content = "";
                imgAccessory.Source = null;
            }

        }
        //点击作答按钮跳转到作业提交界面
       
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                StuMainHomework smh = new StuMainHomework(account,name,classId);
                smh.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void homeworkManagement_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                StudentMainForm smf= new StudentMainForm(account,name);
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

        private void btnDoHomework_Click(object sender, RoutedEventArgs e)
        {
            string content = btnDoHomework.Content.ToString();
            if (content == "待批改" || content== "未完成")//里面是验证函数
            {
                // 打开子窗体
                StuSubmitHomework ssh = new StuSubmitHomework(account,name,notId,classId);
                ssh.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }


        private void tbAccessoryName_Click(object sender, RoutedEventArgs e)
        {
            string notTitle = ss.downloadLink(notId);
            

            SaveFileDialog sfd = new SaveFileDialog();
            //设置默认要保存文件的文件名（文件名.扩展名）
            sfd.FileName = tbAccessoryName.Content.ToString();//这个名字也是原本服务器上保存文件的文件名
            //初始化提示保存文件的路径地址,默认保存在D盘中
            sfd.InitialDirectory = @"D:\";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string errorinfo;
                //获取要保存文件名的本地完整路径
                string localpath = sfd.FileName;// System.IO.Path.GetFullPath(sfd.FileName);
                                                //调用下载文件函数，将教师作业公告附件从服务器上下载下来，其中localpath是本地路径,NotURL是数据库中存放的文件路径（文件在服务器上的路径）
                string NotURL = classId + "/" +notTitle +"/作业附件/"+ tbAccessoryName.Content.ToString();
                bool flag = FtpUpDown.Download(localpath, NotURL, out errorinfo);

                if (flag == true)
                    System.Windows.MessageBox.Show("下载成功");
                else
                    System.Windows.MessageBox.Show("下载失败：" + errorinfo + "");
            }
        }
    }
}
