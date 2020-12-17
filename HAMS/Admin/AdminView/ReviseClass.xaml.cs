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
    /// ReviseClass.xaml 的交互逻辑
    /// </summary>
    public partial class ReviseClass : Window
    {
        public String cnum { get; set; }
        public String cna { get; set; }
        public int id { get; set; }
        private ADao ad = new ADao();
        public ReviseClass(String cnum, String cna, int id)
        {
            InitializeComponent();
            this.cnum = cnum;
            this.cna = cna;
            this.id = id;
            txtClassNum.Text = cnum;
            txtClassName.Text = cna;
            txtTeaNum.Text = id.ToString();
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
            if (txtClassNum.Text == "" || txtClassName.Text == "" || txtTeaNum.Text == "")
            {
                MessageBox.Show("修改信息不能为空");
            }
            else
            {

                if (a == true)
                {
                    string classNum = txtClassNum.Text;
                    string className = txtClassName.Text;
                    int teaId = int.Parse(txtTeaNum.Text);

                    if (ad.updateClassInfo(classNum, className, teaId) == true)
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
