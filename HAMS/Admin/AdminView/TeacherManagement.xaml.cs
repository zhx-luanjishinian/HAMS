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
using System.Windows.Navigation;
using HAMS.Admin.AdminService;
using System.Data;
using HAMS.Admin.AdminDao;

namespace HAMS.Admin.AdminView
{
    /// <summary>
    /// TeacherManagement.xaml 的交互逻辑
    /// </summary>
    public partial class TeacherManagement : Page
    {
        private AService ser = new AService();
        private ADao ad = new ADao();
        public TeacherManagement()
        {
            InitializeComponent();
            txtNum.ToolTip = "请输入完整工号";
            txtName.ToolTip = "请输入完整姓名";
        }

        private void BtnQuery_Click_1(object sender, RoutedEventArgs e)
        {
            //非空判断
            if (txtNum.Text == null || txtName.Text == null)
            {
                MessageBox.Show("请输入查询条件");
            }
            else if (txtNum.Text != "" && txtName.Text == "")
            {
                string Num = txtNum.Text;
                DataTable data = ser.showTeacherInfo1(Num);
                data.Columns[0].ColumnName = "工号";
                data.Columns[1].ColumnName = "姓名";
                data.Columns[2].ColumnName = "性别";
                
                data.Columns[3].ColumnName = "院系";
                data.Columns[4].ColumnName = "密码";
                datagridShowInfo.ItemsSource = data.DefaultView;
            }
            else if (txtNum.Text == "" && txtName.Text != "")
            {
                string Name = txtName.Text;
                DataTable data = ser.showTeacherInfo2(Name);
                data.Columns[0].ColumnName = "工号";
                data.Columns[1].ColumnName = "姓名";
                data.Columns[2].ColumnName = "性别";
                data.Columns[3].ColumnName = "院系";
                data.Columns[4].ColumnName = "密码";
                datagridShowInfo.ItemsSource = data.DefaultView;
            }
            else if (txtNum.Text != "" && txtName.Text != "")
            {
                string Num = txtNum.Text;
                string Name = txtName.Text;
                DataTable data = ser.showTeacherInfo3(Num, Name);
                data.Columns[0].ColumnName = "工号";
                data.Columns[1].ColumnName = "姓名";
                data.Columns[2].ColumnName = "性别";
                data.Columns[3].ColumnName = "院系";
                data.Columns[4].ColumnName = "密码";
                datagridShowInfo.ItemsSource = data.DefaultView;
            }
        }

        private void BtnRevise_Click_1(object sender, RoutedEventArgs e)
        {
            DataRowView selectedElement = (DataRowView)datagridShowInfo.SelectedItem;
            if (selectedElement == null)
            {
                MessageBox.Show("请选择需要修改的信息");
            }
            else
            {
                string account = selectedElement.Row[0].ToString();
                string name = selectedElement.Row[1].ToString();
                string sex = selectedElement.Row[2].ToString();
                string dep = selectedElement.Row[3].ToString();
                string pwd = selectedElement.Row[4].ToString();
                ReviseTeacher rs = new ReviseTeacher(account, name, sex, dep, pwd);
                if (rs.ShowDialog() == true)
                {
                    rs.Show();
                }
            }
        }
    }
}
