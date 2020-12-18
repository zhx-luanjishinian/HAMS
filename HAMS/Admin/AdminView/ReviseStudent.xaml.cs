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
using HAMS.Admin.AdminService;
using System.Data;
using HAMS.Admin.AdminDao;



namespace HAMS.Admin.AdminView
{
    /// <summary>
    /// ReviseStudent.xaml 的交互逻辑
    /// </summary>
    public partial class ReviseStudent : Window
    {

        public String snum { get; set; }
        public String sna { get; set; }
        public String sex { get; set; }
        public String stuclass { get; set; }
        public String pwd { get; set; }
        private ADao ad = new ADao();
        public ReviseStudent(String snum,String sna,String sex,String stuclass, String pwd,StudentManagement s)
        {
            InitializeComponent();
            this.snum = snum;
            this.sna = sna;
            this.sex = sex;
            this.stuclass = stuclass;
            this.pwd = pwd;
            this.
            txtStuNum.Text = snum;
            txtStuName.Text = sna;
            if (sex == "1") {
                radiobtnStuMale.IsChecked = true; 
            }
            else if(sex == "0")
            {
                radiobtnStuMale1.IsChecked = true;
            }
            
            txtStuClass.Text = stuclass;
            txtStuPassword.Text = pwd;
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;  // cancels the window close    
            this.Hide();      // Programmatically hides the window
        }

        private void BtnRevise_Click(object sender, RoutedEventArgs e)
        {
            bool a = true;
            if (txtStuNum.Text == "" || txtStuName.Text == "" || txtStuClass.Text == "" || txtStuPassword.Text=="")
            {
                MessageBox.Show("修改信息不能为空");
            }
            else
            {
               
                if (a == true)
                {
                    string studentNum = txtStuNum.Text;
                    string studentName = txtStuName.Text;
                    int studentSex;
                    if (radiobtnStuMale.IsChecked == true) {
                        studentSex = 1;
                    }
                    else
                    {
                        studentSex = 0;
                    }
                    string studentClass = txtStuClass.Text;
                    string studentPwd = txtStuPassword.Text;

                    if (ad.updateStudentInfo(studentNum, studentName, studentSex, studentClass, studentPwd) == true)
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

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
