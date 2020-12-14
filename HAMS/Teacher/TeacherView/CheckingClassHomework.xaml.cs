using HAMS.Teacher.TeacherDao;
using HAMS.Teacher.TeacherService;
using HAMS.Teacher.TeacherUserControl;
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

namespace HAMS.Teacher.TeacherView
{
    /// <summary>
    /// CheckingClassHomework.xaml 的交互逻辑
    /// </summary>
    public partial class CheckingClassHomework : Window
    {
		private TDao td = new TDao();
		private TService ts = new TService();
		public string account { set; get; }
		public CheckingClassHomework()
        {
            InitializeComponent();
			this.account = account;
			//HomeworkCheckedShow();
			HomeworkCheckingShow();
			//HomeworkUnfinishedShow();
			
		}
		//展示已批改作业信息
		public void HomeworkCheckedShow()
		{
			String notId="6";
			Dictionary<int, List<string>> info = td.showHomeworkCheckedInfo(notId);
			for (int i = 0; i < info.Count; i++)
			{
				ListViewItem ivi = new ListViewItem();
				HomeworkChecked hckd = new HomeworkChecked();
				hckd.Name = "hckd_" + i;
				hckd.lbStudentInfo.Content = info[i][0];
				hckd.lbHomeworkState.Content = "已批改";
				hckd.btnHomeworkCheck.Content = "检查作业";
				ivi.Content = hckd;
				listViewChecked.Items.Add(ivi);
			 }

		}
		//展示待批改作业信息
		public void HomeworkCheckingShow()
		{
			
			string[] str = tbClassInfo.Text.Split(' ');
			String classSpecId = str[0];
			MessageBox.Show(classSpecId.ToString());

			string notId = null;
			//= ts.GetNotId("Z004530B1300720", "测试2");
			Dictionary<int, List<string>> info = td.showHomeworkCheckingInfo(notId);
			
			for (int i = 0; i < info.Count; i++)
			{
				ListViewItem ivi = new ListViewItem();
				HomeworkChecking hckg = new HomeworkChecking();
				hckg.Name = "hckg_" + i;
				hckg.lbStudentInfo.Content = info[i][0];
				hckg.lbHomeworkState.Content = "未批改";
				hckg.btnHomeworkCorrect.Content = "批改作业";
				ivi.Content = hckg;
				listViewUnCheck.Items.Add(ivi);
			}

		}

		//展示未完成作业信息
		public void HomeworkUnfinishedShow()
		{
			string id = "7";
			Dictionary<int, List<string>> info = td.showHomeworkUnfinishedInfo(id);

			for (int i = 0; i < info.Count; i++)
			{
				ListViewItem ivi = new ListViewItem();
				HomeworkUnfinished hufd = new HomeworkUnfinished();
				hufd.Name = "hufd_" + i;
				hufd.lbStudentInfo.Content = info[i][0];
				ivi.Content = hufd;
				listViewUnFinish.Items.Add(ivi);
			}
		}


		private void btnHomeworkCorrect_Click(object sender, RoutedEventArgs e)
        {
            //!!定义homIds的操作应该属于业务层的方法，然后是在ListViewUnCheck加载的时候就对homIds进行了赋值操作
            List<int> homIds = new List<int>();
            //对homIds进行赋值操作


            //获取当前点击的作业再homIds中的下标
            //int index = getHomIdsIndex();
            int index = 0;//这里设置一个假值

            TeacherHomeworkCheck thc = new TeacherHomeworkCheck(homIds,index,(string)lbNotTitle.Content,(string)lbStudentInfo.Content);
            //这里的homId是要带给下一个界面的值,表示待的homId数组，其中index是正要批改的homId在homId数组中的索引
            thc.ShowDialog();
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnHomeworkCorrect1_Click(object sender, RoutedEventArgs e)
        {
            //!!定义homIds的操作应该属于业务层的方法，然后是在ListViewUnCheck加载的时候就对homIds进行了赋值操作
            List<int> homIds = new List<int>();
            //对homIds进行赋值操作


            //获取当前点击的作业再homIds中的下标
            //int index = getHomIdsIndex();
            int index = 0;//这里设置一个假值

            TeacherHomeworkCheck thc = new TeacherHomeworkCheck(homIds, index, (string)lbNotTitle.Content, (string)lbStudentInfo.Content,true);//最后的true表示这属于已经批改的界面
            //这里的homId是要带给下一个界面的值,表示待的homId数组，其中index是正要批改的homId在homId数组中的索引
            thc.ShowDialog();
            this.Visibility = System.Windows.Visibility.Hidden;
        }

		private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}
	}
}
