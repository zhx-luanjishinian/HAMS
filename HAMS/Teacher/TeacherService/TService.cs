using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using HAMS.ToolClass;
using HAMS.Teacher.TeacherDao;
using HAMS.Entity;

namespace HAMS.Teacher.TeacherService
{
    class TService
    {
        private TDao td = new TDao();

        public BaseResult Login(string account, string pw)
        {
            if (account == "" || pw == "")
            {
                return BaseResult.errorMsg("账号或者密码为空");
            }
            DataTable table = td.Login(account, pw);
            if (table.Rows.Count == 0)
            {
                return BaseResult.errorMsg("账号或者密码输入错误，请检查后再进行输入");
            }
            else if (table.Rows[0][2].ToString() != pw)
            {
                return BaseResult.errorMsg("账号正确，密码错误");
            }
            else
            {
                return BaseResult.ok(table.Rows[0][1].ToString());//table.Rows[0][1].ToString()是传递的data
            }

        }

        public String GetHomURLByHomId(int homId)
        {
            //根据homId获取文件在服务器上的路径
            DataTable tbHomURL = td.getHomURLByHomId(homId);
            string homURL = tbHomURL.Rows[0][0].ToString();

            return homURL;
        }
        public String GetPostilByHomId(int homId)
        {
            //根据homId获取学生的作业备注
            DataTable tbHomURL = td.getPostilByHomId(homId);
            string homURL = tbHomURL.Rows[0][0].ToString();
            return homURL;
        }
        public bool CorrectHomework(int homId, string score, string remark)
        {
            //批改作业，往数据库中写入成绩和点评
            bool flag = td.updateHomeworkByCorrect(homId, score, remark);
            return flag;
        }

        //DateTime baseDate = new DateTime(1970, 1, 1);
        //DateTime result = temp.AddSeconds(timeStamp);
        //对truDeadline用datetime
        public String AnnounceNotice(DateTime truDeadline, String content, String notTitle, String classSpecId, String teacherSpecId, String localpath = "")
        {
            Notice notice = new Notice();
            notice.TruDeadLine = truDeadline;
            notice.Content = content;


            //查询该真实的课堂号在数据库中课堂表对应自增主键ClassId
            DataTable tbClassId = td.getClassId(classSpecId);

            int result;
            if (!int.TryParse(tbClassId.Rows[0][0].ToString(), out result))//table[0][0]就是查到的classId
            {
                return "classSpecId转换为classId失败";

            }
            notice.ClassId = result;

            //根据ClassId获取notice表中所有作业公告标题，比对是否重复
            DataTable tbNoteTitles = td.getNoteTitle(notice.ClassId);

            int count = tbNoteTitles.Rows.Count;//获得该课堂号所发布的所有作业公告的标题
            for (int x = 0; x < count; x++)//判断是否和已布置的作业公告标题存在重复
            {
                string tbNoteTitle = tbNoteTitles.Rows[x][0].ToString();//获取该课堂某次作业公告的标题
                if (notTitle == tbNoteTitle)
                {
                    return "此次布置的作业公告标题和该堂课之前的作业公告标题重复";
                }
            }
            notice.NoteTitle = notTitle;//说明没有重复，该作业公告标题合法

            bool flag;
            string errorinfo;

            //创建作业公告目录 
            string dirNotTitle = notTitle;//课堂真实号/作业公告标题/
            string orginPath = classSpecId;//原始目录或起始目录，即在哪个目录下创建
            flag = FtpUpDown.MakeDir(dirNotTitle, out errorinfo, orginPath);//创建目录的静态方法，可以直接通过类名访问
            if (flag == false)
            {
                return "在文件服务器中创建对应作业公告的目录失败";
            }


            //上传作业公告附件
            if (localpath != "")//存在作业公告附件，根据路径插入FTP服务器中
            {
                //创建作业附件目录
                string dirNotFile = "作业附件";
                orginPath += "/" + dirNotTitle;
                flag = FtpUpDown.MakeDir(dirNotFile, out errorinfo, orginPath);//创建目录的静态方法，可以直接通过类名访问
                if (flag == false)
                {
                    return "在文件服务器中创建存放作业附件的目录失败";
                }

                //上传作业附件
                string dirFullNotFile = orginPath + "/" + dirNotFile;
                flag = FtpUpDown.Upload(localpath, dirFullNotFile);
                if (!flag)
                {
                    return "在文件服务器中指定目录上传作业附件失败";
                }
                notice.NoteURL = dirFullNotFile;
            }
            else
            {
                notice.NoteURL = "";
            }
            //notice.NoteURL = notURL;
            //调用插入作业公告函数，将公告插入数据库notice表
            flag = td.insertNotice(notice);
            if (!flag)
            {
                return "无法将新增的作业公告插入到notice表";
            }

            //查询该教师工号在数据库中教师表对应的教师自增主键teacherId
            DataTable tbTeacherId = td.getTeacherId(teacherSpecId);//tbTeacherSpecId.Text是教师工号
            if (!int.TryParse(tbTeacherId.Rows[0][0].ToString(), out result))//table[0][0]就是查到的classId
            {
                return "classSpecId转换为classId失败";

            }
            int teacherId = result;
            //调用学生角色的业务层添加作业函数，该函数负责调用Dao层将作业插入数据库homework表
            //[studentDao文件夹下某Dao文件的一个对象].insertHomework(classId,teacherId,notId);
            //该函数还需要根据classId，获得每个选课学生的stuId，然后依次在作业表中根据(stuId,classId,teacherId,notId)进行插入
            return "发布公告成功";
        }
        public string[] GetScoreAndRemarkByHomId(int homId)
        {
            //根据作业Id获取成绩和点评
            DataTable tbScoreAndRemark = td.GetScoreAndRemarkByHomId(homId);
            string[] Scoreinfos = new string[2];
            Scoreinfos[0] = (string)tbScoreAndRemark.Rows[0][0];
            Scoreinfos[1] = (string)tbScoreAndRemark.Rows[0][1];
            return Scoreinfos;
        }
        public DateTime GetPreviousDateTime(string classSpaceId,string homeworkTitle)
        {
            DataTable table1 = td.getClassId(classSpaceId);
            DataTable table2 = td.getNoteId(homeworkTitle, table1.Rows[0][0].ToString());
            DataTable table3 = td.getTrueDeadLine(table2.Rows[0][0].ToString());
            return (DateTime)table3.Rows[0][0];
        }




    }
}
