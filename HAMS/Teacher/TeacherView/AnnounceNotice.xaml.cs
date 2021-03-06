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
using System.Windows.Forms;//要在依赖中引入Window.Forms,并添加这个命名空间
using System.Text.RegularExpressions;
using HAMS.Teacher.TeacherService;
using HAMS.ToolClass;

namespace HAMS.Teacher.TeacherView
{
    /// <summary>
    /// AnnounceNotice.xaml 的交互逻辑
    /// </summary>
    public partial class AnnounceNotice : Window
    {
        public string pngfile;//头像路径
        private bool ifAnnounce;//是否是修改作业公告
        private bool upNotFile = false;//是否上传作业附件
        //构造函数
        public AnnounceNotice(string tNum,string tName,string cId, string cName,string pgfile)
        {
            InitializeComponent();
            this.pngfile = pgfile;
            //设置该img控件的Source
            headImage.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath(System.IO.Path.Combine(System.Environment.CurrentDirectory, pngfile))));

            tbName.Text = tName;
            tbTeacherSpecId.Text = tNum;
            labelcClassName.Content = cName;
            lbClassSpecId.Text = cId;
            
            ifAnnounce = false;//表示此公告是新公告，未经发布
        }
        //构造函数（修改作业公告时）
        public AnnounceNotice(string tNum, string tName, string cSpecId, string cName,string nTitle,string nContent,DateTime nSubTime, string pgfile)
        {
            InitializeComponent();
            this.pngfile = pgfile;
            //设置该img控件的Source
            headImage.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath(System.IO.Path.Combine(System.Environment.CurrentDirectory, pngfile))));

            tbName.Text = tName;
            tbTeacherSpecId.Text = tNum;
            labelcClassName.Content = cName;
            lbClassSpecId.Text = cSpecId.ToString();
            //修改作业公告时，作业标题不可更改
            textBoxHomeworkTitle.Text = nTitle;
            textBoxHomeworkTitle.IsEnabled = false;
            textBoxContent.Text = nContent;
            calTruDeadline.SelectedDate = nSubTime;
            ifAnnounce = true;//表示此公告是已发布公告，但要重新经过编辑

            //查找附件URLName并显示名称（不需要下载）
            //调用业务层的方法获取URLName并直接显示在控件上
            upload.Content = ts.getNotURLName(nTitle, cSpecId);
            if(upload.Content.ToString() == "")//如果之前没有上传附件
            {
                upload.Content = "点击上传";//则该空间显示点击上传附件
            }
            //System.Windows.MessageBox.Show("nTitle" + nTitle + "cSpecId" + cSpecId);
            btnAnnounce.Content = "修改";



        }
        //退出按钮事件
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
        //返回按钮事件
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            BreifView newBreifView = new BreifView(lbClassSpecId.Text,labelcClassName.Content.ToString(),tbTeacherSpecId.Text,tbName.Text,this.pngfile);
            newBreifView.pngfile = this.pngfile;
            newBreifView.Show();
            // 隐藏自己(父窗体)
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        OpenFileDialog ofd = new OpenFileDialog();//选择文件的对话框
        //上传作业附件按钮点击事件
        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            // 实现覆盖上传新的作业附件
            if ((ifAnnounce == true) && (ts.getNotURLName(textBoxHomeworkTitle.Text, lbClassSpecId.Text) != ""))//如果属于修改已发布作业公告且之前上传过作业附件,需要删除远程的作业附件文件
            {
                
                MessageBoxResult dr = System.Windows.MessageBox.Show("此操作将导致之前上传的作业附件被删除，是否确定要重新上传作业附件？", "", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (dr == MessageBoxResult.OK)
                {
                    
                    upNotFile = true;
                    //作业附件文件夹->课堂真实号/作业名/作业附件->lbClassSpecId.Text/textBoxHomeworkTitle.Text/作业附件
                    //调用业务层函数，根据指定路径，删除原有作业附件
                    bool flag = ts.deleteNotURL(lbClassSpecId.Text.ToString(), textBoxHomeworkTitle.Text, upload.Content.ToString());
                    if (!flag)
                    {
                        System.Windows.MessageBox.Show("删除原有作业附件失败！");
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("删除原有作业附件成功！");
                    }

                    //调用原本的添加函数实现上传新的作业URL
                    //文件打开用到openFileDialog，文件保存是SaveFileDialog
                    ofd.Title = tbName.Text + "老师，请选择您要重新上传的文件";
                    ofd.InitialDirectory = @"C:\Users";//打开本地文件框的起始路径
                    string filter = @"文本文档|*.txt;*.pdf;*.doc;*.docx;*.html;*.wps;*.rtf;*.zip";
                    /*
                        string filter = @"所有文件|*.*|
                                       压缩文件|*.zip;*.rar;*.arj|
                                       文本文档|*.txt;*.pdf;*.doc;*.html;*.wps;*.rtf|
                                       图片文件|*.jpg;*.png;*.gif;*.jpeg;*.bmp|
                                       视频文件|*.avi;*.mp3;*.swf;*.mpg;*.mov|
                                       系统文件|*.int;*.sys;*.dll;*.adt|
                                       可执行文件|*.exe;*.com;*.bat;*.vbs";
                    */

                    ofd.Filter = Regex.Replace(filter, @"\s", ""); //筛选文件
                    if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        upload.Content = System.IO.Path.GetFileName(ofd.FileName); //获取文件名
                                                                                //System.IO.Path.GetFullPath(ofd.FileName);//获取文件路径
                                                                                //ofd.FileName//获取文件路径
                                                                                // string fileName = System.IO.Path.GetFileName(ofd.FileName);//获取文件名
                                                                                //string extension = System.IO.Path.GetExtension(fileName);//获取文件扩展名

                    }

                }
            }
            else //如果之前没有上传作业附件或属于新发布作业公告
            {
                upNotFile = true;
                //文件打开用到openFileDialog，文件保存是SaveFileDialog
                ofd.Title = tbName.Text + "老师，请选择您要上传的文件";
                ofd.InitialDirectory = @"C:\Users";//打开本地文件框的起始路径
                string filter = @"文本文档|*.txt;*.pdf;*.doc;*.docx;*.html;*.wps;*.rtf;*.zip";
                /*
                string filter = @"所有文件|*.*|
                               压缩文件|*.zip;*.rar;*.arj|
                               文本文档|*.txt;*.pdf;*.doc;*.html;*.wps;*.rtf|
                               图片文件|*.jpg;*.png;*.gif;*.jpeg;*.bmp|
                               视频文件|*.avi;*.mp3;*.swf;*.mpg;*.mov|
                               系统文件|*.int;*.sys;*.dll;*.adt|
                               可执行文件|*.exe;*.com;*.bat;*.vbs";
                */

                ofd.Filter = Regex.Replace(filter, @"\s", ""); //筛选文件
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    upload.Content = System.IO.Path.GetFileName(ofd.FileName); //获取文件名
                                                                            //System.IO.Path.GetFullPath(ofd.FileName);//获取文件路径
                                                                            //ofd.FileName//获取文件路径
                                                                            // string fileName = System.IO.Path.GetFileName(ofd.FileName);//获取文件名
                                                                            //string extension = System.IO.Path.GetExtension(fileName);//获取文件扩展名

                }
            
            }

           
           
        }

        private TService ts = new TService();
        //发布作业按钮点击事件
        private void btnAnnounce_Click(object sender, RoutedEventArgs e)
        {
            if(textBoxHomeworkTitle.Text == "")
            {
                System.Windows.MessageBox.Show("您必须输入作业标题！");
            }
            else
            { 
            if (ifAnnounce)//如果是已发布
            {
                string notTitle, content, localpath, notURLName, classSpecId;
                DateTime truDeadline;
                if (upNotFile == true)//表示覆盖上传了作业附件或者重新上传了作业附件
                {
                    //调用业务层方法，修改作业公告
                    notTitle = textBoxHomeworkTitle.Text;
                    content = textBoxContent.Text;

                    truDeadline = calTruDeadline.SelectedDate.Value;
                    localpath = ofd.FileName;
                    notURLName = System.IO.Path.GetFileName(localpath);//存储作业附件名（本地上传文件名）
                    classSpecId = lbClassSpecId.Text;

                }
                else//表示并未覆盖上传作业附件，notURLName还是原来的数据库中的名称
                {
                    notTitle = textBoxHomeworkTitle.Text;
                    content = textBoxContent.Text;
                    truDeadline = calTruDeadline.SelectedDate.Value;
                    localpath = ofd.FileName;
                    classSpecId = lbClassSpecId.Text;
                    notURLName = ts.getNotURLName(notTitle, classSpecId);//注意这里
                }
                //String notURL = 课堂真实号/作业公告名/作业附件/文件名
                //String notURL = classSpecId + "/" + notTitle + "/" + "作业附件" + fileName;
                //该方法实现修改notice表中原有的一条作业公告，且返回具体的信息提示用户
                string message = ts.modifyNotice(truDeadline, content, notTitle, classSpecId, tbTeacherSpecId.Text, localpath, notURLName);
                System.Windows.MessageBox.Show(message);

            }
            else//如果是未发布
            {
                //调用业务层方法，往数据库里添加新的作业公告
                String notTitle = textBoxHomeworkTitle.Text;
                String content = textBoxContent.Text;

                if (calTruDeadline.SelectedDate == null)//如果没有选择截止日期，默认两天后
                {
                    calTruDeadline.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 2); ;//给日历控件截止时间设置默认为当前时间两天之后
                }
                DateTime truDeadline = calTruDeadline.SelectedDate.Value;
                String localpath = ofd.FileName;
                string notURLName = "";//存储作业附件名（本地上传文件名）
                if (localpath != "")
                {
                    notURLName = upload.Content.ToString();
                }
                String classSpecId = lbClassSpecId.Text;
                //String notURL = 课堂真实号/作业公告名/作业附件/文件名
                //String notURL = classSpecId + "/" + notTitle + "/" + "作业附件" + fileName;
                //该方法实现向notice表中新增一条作业公告，且返回具体的信息提示用户
                string message = ts.announceNotice(truDeadline, content, notTitle, classSpecId, tbTeacherSpecId.Text, localpath, notURLName);
                System.Windows.MessageBox.Show(message);
            }
        }
        }
        //鼠标点击时，文字消失
        private void textBoxHomeworkTitle_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!ifAnnounce)//当这个公告是没有发布的公告时，才需要实现此功能；如果是修改已发布公告，不需要实现文字消失
            { 
            textBoxHomeworkTitle.Text = "";  //实现鼠标点击时提示文字消失
            }
        }

        private void textBoxContent_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!ifAnnounce)//当这个公告是没有发布的公告时，才需要实现此功能；如果是修改已发布公告，不需要实现文字消失
            {
                textBoxContent.Text = ""; //实现鼠标点击时提示文字消失
            }
            
        }

       
    }
}


    

