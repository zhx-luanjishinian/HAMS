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
using HAMS.Admin.AdminDao;

namespace HAMS.Admin.AdminView
{
    /// <summary>
    /// ReviseTeacher.xaml 的交互逻辑
    /// </summary>
    public partial class ReviseTeacher : Window
    {
        public String tnum { get; set; }
        public String tna { get; set; }
        public String tsex { get; set; }
        public String teadep { get; set; }
        public String pwd { get; set; }
        private ADao ad = new ADao();
        public ReviseTeacher(String tnum, String tna, String tsex, String teadep, String pwd)
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.tnum = tnum;
            this.tna = tna;
            this.tsex = tsex;
            this.teadep = teadep;
            this.pwd = pwd;
            txtTeaNum.Text = tnum;
            txtTeaName.Text = tna;
            if (tsex == "男")
            {
                radiobtnTeaMale.IsChecked = true;
            }
            else if (tsex == "女")
            {
                radiobtnTeaFemale.IsChecked = true;
            }

            txtTeaCollege.Text = teadep;
            lableTeaPassword.Text = pwd;
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;  // cancels the window close    
            this.Hide();      // Programmatically hides the window
        }
     

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void BtnRevise_Click(object sender, RoutedEventArgs e)
        {
            bool a = true;
            if (txtTeaNum.Text == "" || txtTeaName.Text == "" || txtTeaCollege.Text == "" || lableTeaPassword.Text == "")
            {
                MessageBox.Show("修改信息不能为空");
            }
            else
            {

                if (a == true)
                {
                    string teaNum = txtTeaNum.Text;
                    string teaName = txtTeaName.Text;
                    int teaSex;
                    if (radiobtnTeaMale.IsChecked == true)
                    {
                        teaSex = 1;
                    }
                    else
                    {
                        teaSex = 0;
                    }
                    string teaDep = txtTeaCollege.Text;
                    string teaPwd = lableTeaPassword.Text;

                    if (ad.updateTeacherInfo(teaNum, teaName, teaSex, teaDep, teaPwd) == true)
                    {
                        this.DialogResult = true;
                        MessageBox.Show("修改成功");
                        this.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        this.DialogResult = false;
                        MessageBox.Show("修改失败");
                        this.Visibility = Visibility.Hidden;
                    }
                }
            }
        }
    }
}
