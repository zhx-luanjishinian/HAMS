using HAMS.Teacher.TeacherView;
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

namespace HAMS.Teacher.TeacherUserControl
{
	/// <summary>
	/// HomeworkChecking.xaml 的交互逻辑
	/// </summary>
	public partial class HomeworkChecking : UserControl
	{
		public HomeworkChecking()
		{
			InitializeComponent();
		}

		private void btnHomeworkCorrect_Click(object sender, RoutedEventArgs e)
		{
			if (true)
			{
				TeacherHomeworkCheck thk = new TeacherHomeworkCheck();
				thk.Show();
				this.Visibility = System.Windows.Visibility.Hidden;
			}
		}
	}
}
