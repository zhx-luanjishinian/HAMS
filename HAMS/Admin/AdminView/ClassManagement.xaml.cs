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
    /// ClassManagement.xaml 的交互逻辑
    /// </summary>
    public partial class ClassManagement : Page
    {
        private AService ser = new AService();
        private ADao ad = new ADao();
        public ClassManagement()
        {
            InitializeComponent();
            txtClassNum.ToolTip = "请输入完整课堂号";
            txtClassName.ToolTip = "请输入完整课堂名";
        }
    

        private void BtnQuery_Click_1(object sender, RoutedEventArgs e)
        {
            //非空判断
            if (txtClassNum.Text == null || txtClassName.Text == null)
            {
                MessageBox.Show("请输入查询条件");
            }
            else if (txtClassNum.Text != "" && txtClassName.Text == "")
            {
                string Num = txtClassNum.Text;
                DataTable data = ser.showClassInfo1(Num);
                data.Columns[0].ColumnName = "课堂号";
                data.Columns[1].ColumnName = "课堂名";
                data.Columns[2].ColumnName = "教师编号";
                datagridShowInfo.ItemsSource = data.DefaultView;
            }
            else if (txtClassNum.Text == "" && txtClassName.Text != "")
            {
                string Name = txtClassName.Text;
                DataTable data = ser.showClassInfo2(Name);
                data.Columns[0].ColumnName = "课堂号";
                data.Columns[1].ColumnName = "课堂名";
                data.Columns[2].ColumnName = "教师编号";
                datagridShowInfo.ItemsSource = data.DefaultView;
            }
            else if (txtClassNum.Text != "" && txtClassName.Text != "")
            {
                string Num = txtClassNum.Text;
                string Name = txtClassName.Text;
                DataTable data = ser.showClassInfo3(Num, Name);
                data.Columns[0].ColumnName = "课堂号";
                data.Columns[1].ColumnName = "课堂名";
                data.Columns[2].ColumnName = "教师编号";
                datagridShowInfo.ItemsSource = data.DefaultView;
            }
        }

        private void BtnRevise_Click(object sender, RoutedEventArgs e)
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
                string id =selectedElement.Row[2].ToString();
                ReviseClass rs = new ReviseClass(account, name,id);
                if (rs.ShowDialog() == true)
                {
                    rs.Show();
                }

            }
        }
    }
}
