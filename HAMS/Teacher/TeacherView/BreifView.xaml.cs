using HAMS.Teacher.TeacherDao;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace HAMS.Teacher.TeacherView
{
    /// <summary>
    /// BreifView.xaml 的交互逻辑
    /// </summary>
    public partial class BreifView : Window
    {
        //教师业务类对象
        TeacherService.TService ts = new TeacherService.TService();
        //头像路径
        public string pngfile;
        //构造函数
        public BreifView(string courseNum,string courseName,string tId,string tName, string pgfile)
        {
            //生成基本信息
            InitializeComponent();
            this.pngfile = pgfile;
            
            //设置该img控件的Source
            headImage.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath(System.IO.Path.Combine(System.Environment.CurrentDirectory, @pngfile))));

            labelCourseName.Content = courseName;
            labelCourseNumber.Content = courseNum;
            lbTeacherInfo.Text = tId;
            lbTeacherInfo1.Text = tName;

            //从数据库中查找目前该课堂已经的布置作业
            DataTable table = ts.getNotice(courseNum);  //有问题
            //MessageBox.Show(table.Rows[3][0].ToString());
            ////动态生成控件
            BreifHomework[] arrayBreifHomework = new BreifHomework[20];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                arrayBreifHomework[i] = new BreifHomework();

                arrayBreifHomework[i].Name = "arrayHk" + i.ToString();
                //加载作业标题
                arrayBreifHomework[i].title.Content = table.Rows[i][7].ToString();
                //加载作业描述
                
                //如果作业描述很长的话只显示10个
                if (table.Rows[i][4].ToString().Length > 30)
                {
                    arrayBreifHomework[i].description.Content = table.Rows[i][4].ToString().Substring(0, 30) + "...";//将作业内容content值显示在对应的控件中
                }
                else
                {
                    arrayBreifHomework[i].description.Content = table.Rows[i][4].ToString();//不超过10条就全部展示
                }

                //加在canvas里面
                //arrayHomk.Children.Add(arrayBreifHomework[i]);
                homeworkListView.Items.Add(arrayBreifHomework[i]);
                //定义点击删除按钮时的事件
                arrayBreifHomework[i].btnDelete.Click += new RoutedEventHandler(btnDelete_Click);
                //定义修改公告按钮的操作
                arrayBreifHomework[i].btnModify.Click += new RoutedEventHandler(btnModify_Click);
                //定义点击查看作业公告详情按钮
                arrayBreifHomework[i].btnCheckDetail.Click += new RoutedEventHandler(btnCheckDetail_Click);

            }
        }
        //修改作业公告按钮点击事件
        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
           
            TeacherService.TService ts = new TeacherService.TService();
            System.Windows.Controls.Button sonBtn = (System.Windows.Controls.Button)sender;  //获取当前点击的那个
            Grid sonGrid = (Grid)sonBtn.Parent;
            BreifHomework clickTeachClass = (BreifHomework)sonGrid.Parent;

         
            //获取父级元素
            DateTime dt = ts.getPreviousDateTime(labelCourseNumber.Content.ToString(), clickTeachClass.title.Content.ToString());
            //获取当前作业的作业截止时间
            // 打开子窗体
            AnnounceNotice newAnnounceNotice = new AnnounceNotice(lbTeacherInfo.Text.ToString(), lbTeacherInfo1.Text.ToString(), labelCourseNumber.Content.ToString()
                , labelCourseName.Content.ToString(), clickTeachClass.title.Content.ToString(), clickTeachClass.description.Content.ToString(),dt,this.pngfile);
            newAnnounceNotice.Show();
            this.Visibility = System.Windows.Visibility.Hidden;
        }
        //删除作业公告按钮点击事件
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //删除服务器上的作业附件
            //需要把所有学生表里的作业都删掉
            //首先根据课堂具体工号找到classId
            //然后找到该classId下title对应的notId
            //然后执行删除公告操作（业务层），在该业务层需要先删除作业附件，再调用删除作业表上的所有作业，再删除所有作业公告


            MessageBoxResult dr = System.Windows.MessageBox.Show("此操作将会导致该公告所有已交作业被删除，是否确定删除该作业？", "", MessageBoxButton.OKCancel, MessageBoxImage.Question);
           if(dr== MessageBoxResult.OK)
            {
                System.Windows.Controls.Button sonBtn = (System.Windows.Controls.Button)sender;  //获取当前点击的那个具体作业 
                //获取父级元素，找到要删除的公告
                Grid sonGrid = (Grid)sonBtn.Parent;
                BreifHomework clickTeachClass = (BreifHomework)sonGrid.Parent;
                DataTable tableClassId = ts.getClassId(labelCourseNumber.Content.ToString());
                DataTable tableNotId = ts.getNotIdByClassIdAndNotTitle(clickTeachClass.title.Content.ToString(),Convert.ToInt32(tableClassId.Rows[0][0]) );
                int stuNum = int.Parse(ts.getStuNum(tableClassId.Rows[0][0].ToString()));
                if(stuNum==0)
                {
                    bool flag0 =ts.deleteNotice(tableNotId.Rows[0][0].ToString());
                    if(flag0 ==true)
                    {
                        System.Windows.MessageBox.Show("删除成功");
                        homeworkListView.Items.Remove(clickTeachClass);
                    } 
                    else
                    {
                        System.Windows.MessageBox.Show("删除失败");
                    }
                }
                else
                {
                    String ifDelete = ts.deleteHomeworkNotice(labelCourseNumber.Content.ToString(), clickTeachClass.title.Content.ToString());  //删除时要考虑到与作业表级联删除的情况

                    if (ifDelete == "删除该作业公告成功")
                    {
                        System.Windows.MessageBox.Show("删除该作业公告成功");
                        homeworkListView.Items.Remove(clickTeachClass);
                    }
                    else
                    {
                        System.Windows.MessageBox.Show(ifDelete);
                    }
                }

                

                

               
            }
           
        }

        // public BreifV
        //注销按钮点击事件
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
        //返回按钮点击事件
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                TeacherMainForm newTeacherMainForm = new TeacherMainForm(lbTeacherInfo.Text.ToString(),lbTeacherInfo1.Text.ToString(),this.pngfile);
                newTeacherMainForm.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }
        //发布作业按钮点击跳转事件
        private void btnDeliverHomework_Click(object sender, RoutedEventArgs e)
        {
            // 打开子窗体
            AnnounceNotice newAnnounceNotice = new AnnounceNotice(lbTeacherInfo.Text.ToString(),lbTeacherInfo1.Text.ToString(),labelCourseNumber.Content.ToString()
                ,labelCourseName.Content.ToString(), this.pngfile);
            newAnnounceNotice.pngfile = this.pngfile;
            newAnnounceNotice.Show();
            // 隐藏自己(父窗体)
            this.Visibility = System.Windows.Visibility.Hidden;
        }
       // 查看作业公告详情点击跳转事件
        private void btnCheckDetail_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button sonBtn = (System.Windows.Controls.Button)sender;  //获取当前点击的那个按钮
                                                                                             //获取父级元素，找到要进入的公告
            Grid sonGrid = (Grid)sonBtn.Parent;
         
            BreifHomework clickTeachClass = (BreifHomework)sonGrid.Parent;
            //获得当前点击按钮对应的作业标题和作业描述
            string homeworkTitle =clickTeachClass.title.Content.ToString();
            //string homeworkDescription = clickTeachClass.description.Content.ToString();
            string teacherSpecId = lbTeacherInfo.Text.ToString();
            string teacherName = lbTeacherInfo1.Text.ToString();
            string classSpecId = labelCourseNumber.Content.ToString();
            string className = labelCourseName.Content.ToString();
            //从数据库中查找作业描述
            DataTable tbClassId = ts.getClassId(classSpecId);
            String NotDesp = ts.getNotDespByClassIdAndNotTitle(tbClassId.Rows[0][0].ToString(), homeworkTitle);
            string homeworkDescription = NotDesp;
            //生成新界面
            CheckingClassHomework newCheckingClassHomework = new CheckingClassHomework(homeworkTitle, homeworkDescription,teacherSpecId,teacherName,classSpecId,className,this.pngfile);
            newCheckingClassHomework.pngfile = this.pngfile;
            newCheckingClassHomework.Show();
            this.Visibility = System.Windows.Visibility.Hidden;

        }

     
    }
}
