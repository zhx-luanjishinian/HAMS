using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using HAMS.ToolClass;
using HAMS.Student.StudentDao;
using HAMS.Entity;

namespace HAMS.Student.StudentService
{
    class SService
       
    {
        private SDao sd = new SDao();

        public BaseResult login(string account,string pw)
        {
            if(account =="" || pw == "")
            {
                return BaseResult.errorMsg("账号或者密码为空");
            }
            DataTable table = sd.Login(account, pw);
            //table[0][0]表示第0个从数据库查出来数据的第0条信息-》stuSpecId
            //table[0][1]->name
            //table[0][1]表示第0个从数据库查出来数据的第1条信息-》password
            if (table.Rows.Count == 0)
            {
                return BaseResult.errorMsg( "账号或者密码输入错误，请检查后再进行输入");
            }
            else if (table.Rows[0][2].ToString() != pw)
            {
                return BaseResult.errorMsg("账号正确，密码错误");
            }
            else
            {
                return BaseResult.ok(table.Rows[0][1].ToString());
            }
            
        }
        //获得主界面的课程信息
        public Dictionary<int,List<String>> showCourseInfo(String account)
        {
            return sd.showCourseInfo(account);
        }
        
        //获得主界面的作业信息
        public List<String> showHomeNoticeInfo(String account)
        {
            return sd.showHomeNoticeInfo(account);
        }

        //获取作业公告标题
        public String GetNotTitleByHomId(int NotId)
        {
            //根据homId获取文件在服务器上的路径
            DataTable tbNotTitle = sd.GetNotTitle(NotId);
            string notTitle = tbNotTitle.Rows[0][0].ToString();
            return notTitle;
        }

        public String SubmitHomework(DateTime submitTime, String postil, String homName, String stuName, int stuId, int teaId, String classSpecId , int notid)
        {
            //insert into homework (submitTime,postil,homURL,homName,stuId,teaId,classId,notId) values (@subTime,@postil,@homUrl,@homName,@stuid,@teaid,@cid,@notid);
            Homework homework = new Homework();
            homework.SubmitTime = submitTime;
            homework.Postil = postil;
            homework.HomeName = homName;
            //stuId、teaId、classSpecId、notId可以从界面中获取到值
            //作业URL是：课堂真实号/作业公告标题/学号+姓名/homName
            string notTitle = GetNotTitleByHomId(notid);


            bool flag;
            string errorinfo;

            //创建作业公告目录 
            string dirNotTitle = stuId+stuName;//学号+姓名/
            string orginPath = classSpecId+"/"+notTitle;//原始目录或起始目录，即在哪个目录下创建，也就是：课堂真实号/作业公告标题/
            flag = FtpUpDown.MakeDir(dirNotTitle, out errorinfo, orginPath);//创建目录的静态方法，可以直接通过类名访问
            if (flag == false)
            {
                return "在文件服务器中创建对应学生作业的目录失败";
            }
             //上传作业
             string dirFullNotFile = orginPath + "/" + dirNotTitle;
             flag = FtpUpDown.Upload(homName, dirFullNotFile);
             if (!flag)
             {
                return "在文件服务器中指定目录上传作业失败";
            }
            homework.HomeURL = dirFullNotFile;
            //notice.NoteURL = notURL;
            //调用插入作业公告函数，将公告插入数据库notice表
            flag = sd.UpdateHomework(homework);
            if (!flag)
            {
                return "无法将新增的作业插入到homework表";
            }
            return "提交作业成功";
        }
    }
}
