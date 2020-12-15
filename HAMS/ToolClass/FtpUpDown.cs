using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace HAMS.ToolClass
{
    //以后项目里面直接使用这个类就好了，这里的方法都是静态方法，可以直接调用
    class FtpUpDown
    {
        static string ftpServerIP = "114.215.202.254";
        static string ftpUserID = "ftptest";
        static string ftpPassword = "HAMS123456.";
        static FtpWebRequest reqFTP;//FTP请求
        public FtpUpDown(string ftpServerIP, string ftpUserID, string ftpPassword)
        {
            FtpUpDown.ftpServerIP = ftpServerIP;
            FtpUpDown.ftpUserID = ftpUserID;
            FtpUpDown.ftpPassword = ftpPassword;
        }

        //三个重载函数从ftp服务器上获得文件列表
        private string[] GetFileList(string path, string WRMethods)
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            try
            {
                Connect(path);
                reqFTP.Method = WRMethods;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);//中文文件名
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                //Console.Write(ex.Message);
                MessageBox.Show(ex.Message);
                downloadFiles = null;
                return downloadFiles;
            }
        }
        public string[] GetFileList(string path)
        {
            return GetFileList("ftp://" + ftpServerIP + "/" + path, WebRequestMethods.Ftp.ListDirectory);
        }
        public string[] GetFileList()
        {
            return GetFileList("ftp://" + ftpServerIP + "/", WebRequestMethods.Ftp.ListDirectory);
        }

        private static void Connect(String path)//连接ftp
        {
            // 根据uri创建FtpWebRequest对象
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(path));
            // 指定数据传输类型
            reqFTP.UseBinary = true;
            // ftp用户名和密码
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
        }
        //获得文件在远程服务器上面的路径,filepath里面存储的是数据库里面的文件夹名/文件名，如果没有文件夹就直接是文件名
        public static string getFilePath(string filepath)
        {
            return "ftp://" + ftpServerIP + "/" + filepath;

        }


        //上传文件,传入的路径是本地,目录名默认为空
        public static bool Upload(string localpath, string dirpath = "")
        {
            bool bol = false;
            string uri = "";
            try
            {
                FileInfo fileInf = new FileInfo(localpath);
                //此处需要在上传的时候需要找到服务器中的目录名,没有目录名的话可以不传
                if (dirpath == "")
                {
                    uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;

                }
                else
                {
                    uri = "ftp://" + ftpServerIP + "/" + dirpath + "/" + fileInf.Name;

                }

                Connect(uri);//连接 
                             // 默认为true，连接不会被关闭
                             // 在一个命令之后被执行
                reqFTP.KeepAlive = false;
                // 指定执行什么命令
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
                // 上传文件时通知服务器文件的大小
                reqFTP.ContentLength = fileInf.Length;

                // 缓冲大小设置为kb 
                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen;
                // 打开一个文件流(System.IO.FileStream) 去读上传的文件
                FileStream fs = fileInf.OpenRead();
                try
                {
                    // 把上传的文件写入流
                    Stream strm = reqFTP.GetRequestStream();
                    // 每次读文件流的kb
                    contentLen = fs.Read(buff, 0, buffLength);
                    if (contentLen == 0)
                    {
                        // 流内容没有结束
                        bol = true;
                    }
                    else
                    {
                        while (contentLen != 0)
                        {
                            // 把内容从file stream 写入upload stream 
                            strm.Write(buff, 0, contentLen);
                            contentLen = fs.Read(buff, 0, buffLength);
                            bol = true;

                        }
                    }
                    // 关闭两个流
                    strm.Close();
                    fs.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "上传失败");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "上传失败");
            }
            return bol;
        }
        //下载文件,localpath是文件要下载到本地的哪个路径，DbURL是存储在数据库中的路径
        public static bool Download(string localpath, string DbURL, out string errorinfo)
        {
            try
            {//由于存储在数据库中的是文件夹/文件名的形式，因此这里要截一下然后直接取文件名
                string[] DbURLs = DbURL.Split('/');
                String onlyFileName = Path.GetFileName(DbURLs[DbURLs.Length-1]);
                

                if (File.Exists(localpath))
                {
                    errorinfo = string.Format("本地文件{0}已存在,无法下载", localpath);
                    return false;
                }
                string url = "ftp://" + ftpServerIP + "/" + DbURL;
                
                Connect(url);//连接 
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);

                FileStream outputStream = new FileStream(localpath, FileMode.Create);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                ftpStream.Close();
                outputStream.Close();
                response.Close();
                errorinfo = "";
                return true;
            }
            catch (Exception ex)
            {
                errorinfo = string.Format("因{0},无法下载", ex.Message);
                return false;
            }
        }

        //创建目录，orginPath是已有目录，表示在已有目录下根据传入的newdirName进行创建（dirName可能是多层级的）
        public static bool MakeDir(string newdirName, out string errorinfo,string orginPath = "")
        {
            try
            {
                string[] newdirNames = newdirName.Split('/');//可能会一次创建多个层级的目录
                int dirLength = newdirNames.Length;//获取要创建的多级目录的长度
                
                string uri = "ftp://" + ftpServerIP;
                if(orginPath != "")
                {
                    uri += "/" + orginPath;
                }
                
                for (int i = 0;i< dirLength;i++)
                {
                    uri += "/" + newdirNames[i];
                    
                    Connect(uri);//连接 
                    reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                    FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                    response.Close();
                    
                }
                
                errorinfo = "";
                return true;
            }
            catch (Exception ex)
            {
                //Console.Write(ex.Message);
                MessageBox.Show(ex.Message);
                errorinfo = string.Format("因{0},无法创建", ex.Message);
                return false;
            }
        }


        //删除目录
        //注意，当目录下面有文件时，无法删除该目录
        public static bool delDir(string dirPath, out string errorinfo)
        {
            try
            {

                string uri = "ftp://" + ftpServerIP + "/" + dirPath;
                
                Connect(uri);//连接 
                
                reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;
                
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                
                response.Close();
                errorinfo = "";
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show()
                //Console.Write(ex.Message);
                MessageBox.Show(ex.Message);
                errorinfo = string.Format("因{0},无法删除", ex.Message);
                return false;
            }
        }

        //删除文件(覆盖提交时使用）
        public static bool delFile(string FileFullPath, out string errorinfo)
        {
            try
            {

                string uri = "ftp://" + ftpServerIP + "/" + FileFullPath;
                //MessageBox.Show(uri);
               
                Connect(uri);//连接 
                
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

                response.Close();
                errorinfo = "";
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show()
                //Console.Write(ex.Message);
                MessageBox.Show(ex.Message);
                errorinfo = string.Format("因{0},无法删除", ex.Message);
                return false;
            }
        }

        //目录改名（当作业公告在数据库中被删除后，将该作业公告文件夹进行更名，表示该作业公告文件夹已经无用）
        //currentDirFullPath是现有的完整路径, newDirName是想改的新目录名
        public static void Rename(string currentDirFullPath, string newDirName)
        {
            try
            {
                FileInfo fileInf = new FileInfo(currentDirFullPath);
                string uri = "ftp://" + ftpServerIP + "/" + fileInf;
                
                Connect(uri);//连接
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.RenameTo = newDirName;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
                //Console.Write(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }


    }
}
