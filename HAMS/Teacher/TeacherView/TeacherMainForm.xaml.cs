using HAMS.Teacher.TeacherDao;
using HAMS.Teacher.TeacherService;
using MySql.Data.MySqlClient;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace HAMS.Teacher.TeacherView
{
    /// <summary>
    /// TeacherMainForm.xaml 的交互逻辑
    /// </summary>
    public partial class TeacherMainForm : Window
    {
        
        private TService ts = new TService();
        public string pngfile;
        public TeacherMainForm(string session,string tname,string pgfile)
        {
            InitializeComponent();
            this.pngfile = pgfile;
            //设置该img控件的Source
            headImage.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath(System.IO.Path.Combine(System.Environment.CurrentDirectory, pngfile))));
            try
            {
                if (session != null)
                {
                    tbTeacherInfo.Text = session;
                    tbTeacherInfo1.Text = tname;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("界面间传值发生异常" + ex.Message);
            }
             
            //AnnounceNoticeDao temp = new AnnounceNoticeDao();
            //两个方法类
            DataTable tableTeacherId = ts.getTeacherId(session);
            DataTable table= ts.loadMainFormLeft(tbTeacherInfo.Text);

            TeachClass[] arrayTeachClass = new TeachClass[10];
            //给自定义控件的子控件加属性
            for (int i = 0; i < table.Rows.Count; i++)
            {
                arrayTeachClass[i] = new TeachClass();
                arrayTeachClass[i].Name = "array" + i.ToString();
                arrayTeachClass[i].labelClassId1.Content = table.Rows[i][5];
                arrayTeachClass[i].labelNoticeNumber.Content = "已发布公告数："+ts.getNoticeNum(table.Rows[i][0].ToString());
                arrayTeachClass[i].labelStudentNumber.Content = "当前课堂人数：" + ts.getStuNum(table.Rows[i][0].ToString());
                arrayTeachClass[i].labelClassName1.Content = table.Rows[i][1].ToString();
                listViewTeacherClass.Items.Add(arrayTeachClass[i]);
                //arrayBreifHomework[i].btnModify.Click += new RoutedEventHandler(btnModify_Click);
                arrayTeachClass[i].MouseDown += new System.Windows.Input.MouseButtonEventHandler(mousedown);
                //arrayTeachClass[i].btnEnterClass.Click += new RoutedEventHandler(btnModify_Click);

            }
            //查到老师当前教的课程的id
            
         
            DataTable tableclassId = ts.getClassIdByTId(tableTeacherId.Rows[0][0].ToString());
            
            RecentNoticeControll[] arrayRecentNotice = new RecentNoticeControll[20];
            //动态生成控件
            DataTable tableRecentNotice;

    
            for (int j = 0; j < tableclassId.Rows.Count; j++)
            {
                tableRecentNotice = ts.getRecentNoticeByClassId(tableclassId.Rows[j][0].ToString());    //获得对应classId在notice表中的内容
                DataTable tableclassInfo = ts.getClassInfoByClassID(tableclassId.Rows[j][0].ToString()); //获得对应classId在class表中的其他内容
                int noticeNum = tableRecentNotice.Rows.Count;
                    for (int k = 0; k < noticeNum; k++)
                    {
                        arrayRecentNotice[k] = new RecentNoticeControll();
                        arrayRecentNotice[k].labelNotName.Content = tableRecentNotice.Rows[noticeNum - 1 - k][7];
                        //为UserControl的属性赋值
                        arrayRecentNotice[k].desrciption = tableRecentNotice.Rows[noticeNum - 1 - k][4].ToString();
                    // MessageBox.Show(tableclassInfo.Rows[1][1].ToString());
                    arrayRecentNotice[k].className = tableclassInfo.Rows[0][1].ToString();   //有问题
                    arrayRecentNotice[k].classSpecId = tableclassInfo.Rows[0][5].ToString();

                    listViewRecentNotice.Items.Add(arrayRecentNotice[k]);
                        //定义点击查看作业公告详情按钮
                        arrayRecentNotice[k].btnRecntNo1.Click += new RoutedEventHandler(btnRecntNo1_Click);
                    }
                }
               
            }

        //private void btnModify_Click(object sender, RoutedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        private void mousedown(object sender, MouseButtonEventArgs e)
        {
            //记录点击的是哪个控件
            TeachClass clickTeachClass = (TeachClass)sender;
            //动态加载BreifView界面，需要知道当前课程名，课程id，老师id,老师姓名
            BreifView newBreif = new BreifView(clickTeachClass.labelClassId1.Content.ToString(), clickTeachClass.labelClassName1.Content.ToString(), tbTeacherInfo.Text, tbTeacherInfo1.Text, this.pngfile);
            newBreif.pngfile = this.pngfile;
            newBreif.Show();
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnEnterClass_Click(object sender, EventArgs e)
        {
            //记录点击的是哪个控件
            TeachClass clickTeachClass = (TeachClass)sender;
            //动态加载BreifView界面，需要知道当前课程名，课程id，老师id,老师姓名
            BreifView newBreif = new BreifView(clickTeachClass.labelClassId1.Content.ToString(), clickTeachClass.labelClassName1.Content.ToString(), tbTeacherInfo.Text, tbTeacherInfo1.Text, this.pngfile);
            newBreif.pngfile = this.pngfile;
            newBreif.Show();
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();    //注销，关闭系统
        }

        private void btnRecntNo1_Click(object sender, RoutedEventArgs e)
        {
            //有一个按钮还是有问题，就一个
            Button sonBtn = (Button)sender;  //获取当前点击的那个button
            RecentNoticeControll clickRectNotice = (RecentNoticeControll)sonBtn.Parent;
            string homeworkTitle = clickRectNotice.labelNotName.Content.ToString();
            string description = clickRectNotice.desrciption;
            string teacherSpecId = tbTeacherInfo.Text;
            string teacherName = tbTeacherInfo1.Text;
            string classSpecId = clickRectNotice.classSpecId;
            string className = clickRectNotice.className;
            CheckingClassHomework newCheckingClassHomework =new CheckingClassHomework(homeworkTitle, description, teacherSpecId, teacherName, classSpecId, className, this.pngfile);
            newCheckingClassHomework.pngfile = this.pngfile;
            newCheckingClassHomework.Show();
            this.Visibility = System.Windows.Visibility.Hidden;
        }
       
    }
}
