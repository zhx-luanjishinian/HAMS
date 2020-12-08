﻿using System;
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

namespace HAMS.Teacher.TeacherView
{
    /// <summary>
    /// TeachClass.xaml 的交互逻辑
    /// </summary>
    public partial class TeachClass : UserControl
    {
        public TeachClass()
        {
            InitializeComponent();
        }

        public void btnEnterClass_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("test");
            string courseName = labelClassName1.Content.ToString();
            string courseNumber = labelClassId1.Content.ToString();
            BreifView newBreifView = new BreifView(courseName, courseNumber);
            newBreifView.Show();
            this.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
