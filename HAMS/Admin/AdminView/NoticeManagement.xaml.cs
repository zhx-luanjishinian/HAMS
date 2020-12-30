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
using System.Windows.Navigation;
using System.Windows.Shapes;
using HAMS.Admin.AdminService;
using HAMS.Admin.AdminDao;
using HAMS.Admin.AdminUserControl;

namespace HAMS.Admin.AdminView
{
    /// <summary>
    /// NoticeManagement.xaml 的交互逻辑
    /// </summary>
    public partial class NoticeManagement : Page
    {
        public String id{ set; get; }
        public String als { set; get; }
        private AService s = new AService();
        private ADao ad = new ADao();
        public NoticeManagement(string adminId)
        {
            this.id = adminId;
            InitializeComponent();
            mainShow();
        }
        public void mainShow()
        {  
            List<List<String>> result = s.showNoticeInfo(id);
            for (int i = 0; i < result.Count; i++)
            {
                SysNoticeInfo hni = new SysNoticeInfo();
                ListViewItem ivi = new ListViewItem();
                //定义一个数组用来放东西
                String[] temp = new String[3];
                hni.labelNoticeId.Content = result[i][0];
                hni.labelNoticeName.Content = result[i][1];
                if(result[i][2].Length > 20)
                {
                    hni.labelNoticeDescription.Content = result[i][2].Substring(0,20)+"...";
                }
                else
                {
                    hni.labelNoticeDescription.Content = result[i][2];
                }
                temp[0] = result[i][1];
                temp[1] = result[i][2];
                temp[2]= result[i][0];
                hni.btnReviseNotice.Tag = temp;
                hni.btnDeleteNotice.Tag = result[i][0];
                hni.btnDeleteNotice.Click += new RoutedEventHandler(btnDeleteNotice_Click);
                hni.btnReviseNotice.Click += new RoutedEventHandler(btnReviseNotice_Click);
                ivi.Content = hni;
                listviewNotice.Items.Add(ivi);
            }
        }
        private void btnReviseNotice_Click(object sender, RoutedEventArgs e)
        {
            //记录生成的是哪个动态控件
            Button hnif = (Button)sender;
            String[] info = (String[])hnif.Tag;
            Notice sdh = new Notice(info[2], info[0],info[1]);
            sdh.Show();
            this.Visibility = Visibility.Hidden;
        }
        private void btnDeleteNotice_Click(object sender, RoutedEventArgs e)
        {
            //记录生成的是哪个动态控件
            Button hnif = (Button)sender;
            String info = (String)hnif.Tag;
            int id = int.Parse(info);
            if (MessageBox.Show("您确认删除该条信息吗？", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                ad.deleteSysNotice(id);
                MessageBox.Show("删除成功");
            }

        }

        private void BtnAddNotice_Click(object sender, RoutedEventArgs e)
        {
            Notice sdh = new Notice(id);
            sdh.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listviewNotice.Items.Clear();
            mainShow();
        }
    }

}
