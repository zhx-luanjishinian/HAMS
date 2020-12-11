using HAMS.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Teacher.TeacherDao
{
    class AnnounceNoticeDao
    {
        public Boolean insertNotice(Notice notice)
        {
            String sql = "insert into notice (truDeadline,content,notURL,notTitle,classId) values (@truDdl,@cont,@ntUrl,@ntTitle,@cid);";
            //传入要填写的参数
            MySqlParameter para1 = new MySqlParameter("@truDdl", notice.TruDeadLine);
            MySqlParameter para2 = new MySqlParameter("@cont", notice.Content);
            MySqlParameter para3 = new MySqlParameter("@ntUrl", notice.NoteURL);
            MySqlParameter para4 = new MySqlParameter("@ntTitle", notice.NoteTitle);
            MySqlParameter para5 = new MySqlParameter("@cid", notice.ClassId);
            return DataUtil.DataOperation.DataAdd(sql, para1, para2, para3, para4, para5);//如果插入成功，则返回true
        }
        public Boolean deleteNotice(string noticeTitle)
        {
            String sql = "delete from notice where notTitle=@ntitle;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@ntitle", noticeTitle);
            return DataUtil.DataOperation.DataDelete(sql, para);//如果删除成功，则返回true
        }
        public DataTable getClassId(string classSpecId)
        {
            //根据真实的课堂号获取课堂表里的自增主键课堂号classId
            String sql = "select classId from class where classSpecId = @id;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@id", classSpecId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table;

        }
        public DataTable getNoteTitle(int classId)
        {
            //从数据库表中
            String sql = "select notTitle from notice where classId = @cid;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@cid", classId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table;

        }

        public DataTable getTeacherId(string TeacherSpecId)
        {
            //根据教师工号获取课堂表里的自增主键教师号teacherId
            String sql = "select teacherId from teacher where teacherSpecId = @tid;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@tid", TeacherSpecId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table;
        }
        public DataTable getClassIdByTId(string teacherId)
        {
            //根据teacherId获得classId
            String sql = "select classId from class where teacherId = @tid;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@tid", teacherId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table;
        }
        public DataTable getRecentNoticeByClassId(string classId)
        {
            //根据classId获取公告表里的全部内容
            String sql = "select * from notice where classId = @tid;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@tid", classId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table;
        }
    }
}
