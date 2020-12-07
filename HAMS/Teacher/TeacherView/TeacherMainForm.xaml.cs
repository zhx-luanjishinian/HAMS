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
using System.Windows.Shapes;

namespace HAMS.Teacher.TeacherView
{
    /// <summary>
    /// TeacherMainForm.xaml 的交互逻辑
    /// </summary>
    public partial class TeacherMainForm : Window
    {
        public TeacherMainForm(string session)
        {
            InitializeComponent();
            try { if (session != null) { tbTeacherInfo.Text = session; } }
            catch (Exception ex)
            {
                throw new Exception("界面间传值发生异常" + ex.Message);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();    //注销，关闭系统
        }

        private void btnRecntNo1_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                BreifView newBreifView = new BreifView();
                newBreifView.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }
    }
}
