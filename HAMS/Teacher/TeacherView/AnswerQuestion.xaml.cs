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
    /// AnswerQuestion.xaml 的交互逻辑
    /// </summary>
    public partial class AnswerQuestion : Window
    {
        public String notId { set; get; }
        public String classSpecId { set; get; }
        public AnswerQuestion(String className)
        {
            InitializeComponent();
            labelClassName.Content = className;
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //CheckingClassHomework newHomeworkNoticeCheck = new CheckingClassHomework();
            //newHomeworkNoticeCheck.Show();
            //// 隐藏自己(父窗体)
            //this.Visibility = System.Windows.Visibility.Hidden;
            this.Close();
        }

        private void btnSubmitQuestion_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void btnSubmitQuestion_Click(object sender, RoutedEventArgs e)
        //{
        //    Student.StudentUserControl.StudentAskQuestion newStudentAskQuestion = new Student.StudentUserControl.StudentAskQuestion();

        //}
    }
}
