
using HAMS.Entity;
using HAMS.Teacher.TeacherView;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace HAMS.Teacher.TeacherDao
{
    class TDao
    {
        private MySqlConnection conn = DataUtil.DBUtil.getConnection();
        public DataTable Login(String account, String pw)
        {

            String sql = "select teacherSpecId,name,password from teacher where teacherSpecId=@id";
            //传入要填写的参数
            MySqlParameter parameter = new MySqlParameter("@id", account);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, parameter);
            //MessageBox.Show(table.Rows[0][0].ToString());->stuSpecId
            //MessageBox.Show(table.Rows[0][1].ToString());->name
            //MessageBox.Show(table.Rows[0][2].ToString());->password
            return table;
        }
        public DataTable LoadMainFormLeft(string teacherSpecId)
        {
            //sql语句
            String sql = "select * from class where teacherId=@id";   //查询对应老师id的class表中所有的数据，这里查不到
            String sql1 = "select teacherId from teacher where teacherSpecId=@spaceId";  //根据当前老师的spaceId查询teacherId
            MySqlParameter parameter1 = new MySqlParameter("@spaceId", teacherSpecId);   //tbTeacherInfo.Text,这里我修改了一下，传过来的只有老师工号
            DataTable table1 = DataUtil.DataOperation.DataQuery(sql1, parameter1);
            //MessageBox.Show(table1.Rows[0][0].ToString());
            //以上是正确的
            MySqlParameter parameter = new MySqlParameter("@id", table1.Rows[0][0]);  //查到当前老师的teacherID
            DataTable table = DataUtil.DataOperation.DataQuery(sql, parameter);       //在class表中查老师教的课程名
        
            return table;
        }
        public DataTable getTrueDeadLine(String notId)
        {
            //sql语句
            String sql = "select truDeadline from notice where notId=@id";   //根据noticeId查找truDeadline
            MySqlParameter parameter = new MySqlParameter("@id", notId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, parameter);       
            return table;
        }
        public DataTable GetSubmitTime(string notId)
        {
            //sql语句
            String sql = "select submitTime from notice where notId=@id";   //根据noticeId查找truDeadline
            MySqlParameter parameter = new MySqlParameter("@id", notId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, parameter);
            return table;
        }
        public DataTable getNotIdByClassIdAndNotTitle(String notTitle,int classId)    //根据名称和classId查notId
        {
            //sql语句
            String sql = "select notId from notice where notTitle=@id and classId=@cId";   //根据noticeId查找truDeadline
            MySqlParameter parameter = new MySqlParameter("@id", notTitle);
            MySqlParameter parameter1 = new MySqlParameter("@cid", classId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, parameter,parameter1);
            return table;
        }
        public DataTable getNotice(string classSpaceId)  //从数据库查询目前已有的作业
        {
            
            DataTable table0 = getClassId(classSpaceId);  //查询需要的classId,使用查询classId的函数
            String sql = "select * from notice where classId = @id;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@id", table0.Rows[0][0]);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table;

        }
        public bool insertNotice(Notice notice)
        {
            String sql = "insert into notice (truDeadline,content,notURL,notURLName,notTitle,classId) values (@truDdl,@cont,@ntUrl,@ntUrlName,@ntTitle,@cid);";
            //传入要填写的参数
            MySqlParameter para1 = new MySqlParameter("@truDdl", notice.TruDeadLine);
            MySqlParameter para2 = new MySqlParameter("@cont", notice.Content);
            MySqlParameter para3 = new MySqlParameter("@ntUrl", notice.NoteURL);
            MySqlParameter para4 = new MySqlParameter("@ntUrlName", notice.NoteURLName);
            MySqlParameter para5 = new MySqlParameter("@ntTitle", notice.NoteTitle);
            MySqlParameter para6 = new MySqlParameter("@cid", notice.ClassId);
            return DataUtil.DataOperation.DataAdd(sql, para1, para2, para3, para4, para5, para6);//如果插入成功，则返回true
        }

        public bool updateNotice(DateTime truDeadline,string content,string notURLName,int notId)
        {
            //根据notId更新truDeadline,content,notURLName
            String sql = "update notice set truDeadline=@truDdl,content=@cont,notURLName=@ntUrlName where notId=@nid";
            //传入要填写的参数
            MySqlParameter para1 = new MySqlParameter("@truDdl", truDeadline);
            MySqlParameter para2 = new MySqlParameter("@cont", content);
            MySqlParameter para3 = new MySqlParameter("@ntUrlName", notURLName);
            MySqlParameter para4 = new MySqlParameter("@nid", notId);
            return DataUtil.DataOperation.DataAdd(sql, para1, para2, para3, para4);//如果插入成功，则返回true
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
       
        public DataTable getTeacherId(String TeacherSpecId)
        {
            //根据教师工号获取课堂表里的自增主键教师号teacherId
            String sql = "select teacherId from teacher where teacherSpecId = @tid;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@tid", TeacherSpecId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table;
        }

        public int getNoticeNum(string classId)
        {
            //根据classId获取notice的数量
            String sql = "select * from notice where classId = @tid;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@tid", classId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table.Rows.Count;
        }

        public int getStuNum(string classId)
        {
            //根据classId获取notice的数量
            String sql = "select * from takecourse where classId = @tid;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@tid", classId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table.Rows.Count;
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

        public DataTable getClassIdByTId(string teacherId)
        {
            //根据teacherId获得classId
            String sql = "select classId from class where teacherId = @tid;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@tid", teacherId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table;
        }

        public DataTable getHomURLByHomId(int homId)
        {
            //根据homId获得homURL
            String sql = "select homURL from homework where homId = @hid;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@hid", homId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table;
        }

        public DataTable getPostilByHomId(int homId)
        {
            //根据homId获得postil
            String sql = "select postil from homework where homId = @hid;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@hid", homId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table;
        }

        public bool updateHomeworkByCorrect(int homId,string score,string remark)
        {
            String sql = "update homework set score = @sc,remark = @rm where homId = @hid;";
            //传入要填写的参数
            MySqlParameter para1 = new MySqlParameter("@sc", score);
            MySqlParameter para2 = new MySqlParameter("@rm", remark);
            MySqlParameter para3 = new MySqlParameter("@hid", homId);
            return DataUtil.DataOperation.DataUpdate(sql, para1, para2, para3);//如果更新成功，则返回true
        }

        public DataTable GetScoreAndRemarkByHomId(int homId)    //需要用到的函数
        {
            //根据homId获取score和remark
            String sql = "select score,remark from homework where homId = @hid;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@hid", homId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);

            return table;
        }
        public Boolean deleteNotice(string noticeId)
        {
            //根据notTitle删除公告，此功能有问题

            String sql = "delete from notice where notId=@nId;";
          
            //传入要填写的参数
            
            MySqlParameter para = new MySqlParameter("@nId", noticeId);
            return DataUtil.DataOperation.DataDelete(sql, para);//如果删除成功，则返回true
        }
        public Boolean deleteHomework(String noticeId)
        {
            //因为需要级联删除，因此需要首先删除homework表中的数据,才能删除notice表中的数据
            String sql1 = "delete from homework where notId=@nId;";
            MySqlParameter para = new MySqlParameter("@nId", noticeId);
            return DataUtil.DataOperation.DataDelete(sql1, para);//如果删除成功，则返回true
        }
        public Boolean updateNotice(int notId, string notTitle,string content)
        {
            //根据notId更新notTitle和ontent
            String sql = "update notice set content = @content,notTitle = @notTitle where notId  = @notId;";
            //传入要填写的参数
            MySqlParameter para1 = new MySqlParameter("@content", content);
            MySqlParameter para2 = new MySqlParameter("@notTitle", notTitle);
            MySqlParameter para3 = new MySqlParameter("@notId", notId);
            return DataUtil.DataOperation.DataUpdate(sql, para1, para2, para3);//如果更新成功，则返回true
        }
        public DataTable GetStuIdFromClassId(int classId)
        {
            //根据classId获取公告表里的全部内容
            String sql = "select stuId from takecourse where classId = @classId;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@classId", classId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table;
        }
        public DataTable GetStuIdFromStuSpecId(string stuSpecId)
        {
            //根据学生学号查询学生Id
            String sql = "select stuId from student where stuSpecId = @stuSpecId;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@stuSpecId", stuSpecId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table;
        }
        public bool InsertHomework(Homework homework)
        {
            //发布作业公告后，需要在Homework表添加作业记录
            //向Homework表添加新作业记录
            String sql = "insert into homework (stuId, classId, teacherId, notId) values (@stuid,@classid,@teaid,@notid);";

            //传入要填写的参数
            MySqlParameter para1 = new MySqlParameter("@stuid", homework.StuId);
            MySqlParameter para2 = new MySqlParameter("@classid", homework.ClassId);
            MySqlParameter para3 = new MySqlParameter("@teaid", homework.TeacherId);
            //MessageBox.Show(homework.TeacherId.ToString());
            MySqlParameter para4 = new MySqlParameter("@notId", homework.NotId);
            return DataUtil.DataOperation.DataAdd(sql, para1, para2, para3, para4);//如果插入成功，则返回true
        }
        public DataTable getHomURLAndNameByHomId(int homId)
        {
            //根据homId获得homURL
            String sql = "select homURL,homURLName from homework where homId = @hid;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@hid", homId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table;
        }
        public DataTable getNotURLNameByNotId(int notId)
        {
            //根据notId获得notURLName
            String sql = "select notURLName from notice where notId = @nid;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@nid", notId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table;

        }
        public DataTable getNotURLByNotId(String notId)    
        {
            //根据notId查notURL
            String sql = "select notURL from notice where notId=@nid";   //根据noticeId查找truDeadline
            MySqlParameter parameter = new MySqlParameter("@nid", notId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, parameter);
            return table;
        }

        //获得已批改作业信息
        public DataTable SelectHomeworkCheckedInfo(String notId)
        {
            //此类用来装返回的对象

            String sql = "select stuId from homework where score is not null and notId=@nid;";
            MySqlParameter parameter = new MySqlParameter("@nid", notId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, parameter);  //查到学生id,分数,作业路径
            return table;

        }
        //获得待批改作业信息
        public DataTable SelectHomeworkNeedCorrectInfo(String notId)
        {
            //此类用来装返回的对象

            String sql = "select stuId from homework where score is null and homURLName is not null and notId=@nid;";   //null
            MySqlParameter parameter = new MySqlParameter("@nid", notId);
   
            DataTable table = DataUtil.DataOperation.DataQuery(sql, parameter);  //查到学生id,分数,作业路径
            return table;

        }
        //获得未完成作业信息
        public DataTable SelectHomeworkUnfinishedInfo(String notId)
        {
            //此类用来装返回的对象

            String sql = "select stuId from homework where notId=@nid and homURLName is null;";
            MySqlParameter parameter = new MySqlParameter("@nid", notId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, parameter);  //查到学生id,分数,作业路径
            return table;

        }
        //根据学生Id查询学生学号和姓名
        public DataTable GetStudentNameAndIdByStuID(String stuId)
        {
            //此类用来装返回的对象

            String sql = "select stuSpecId,name from student where stuId=@nid";
            MySqlParameter parameter = new MySqlParameter("@nid", stuId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, parameter);  //查到学生id,分数,作业路径
            return table;

        }
        public DataTable GetClassInfoByClassID(String classId)
        {
            //此类用来装返回的对象

            String sql = "select * from class where classId=@nid";
            MySqlParameter parameter = new MySqlParameter("@nid", classId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, parameter);  //查到学生id,分数,作业路径
            return table;

        }

        public DataTable GetHomIdByStuIdAndNotId(String stuId,string noteId)
        {
            String sql = "select homId from homework where stuId=@nid and notId=@ntId";
            MySqlParameter parameter = new MySqlParameter("@nid", stuId);
            MySqlParameter parameter1 = new MySqlParameter("@ntid", noteId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, parameter,parameter1);  //查到学生id,分数,作业路径
            return table;
        }


        public DataTable getStuIdByHomId(int homId)
        {
            //根据homId查询stuId
            String sql = "select stuId from homework where homId=@hid";
            MySqlParameter parameter = new MySqlParameter("@hid", homId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, parameter);  //查到学生id,分数,作业路径
            return table;
        }

        //获得已批改作业的等级和该等级下的作业数 和 获得该作业公告未完成和已完成的作业人数
        public List<Dictionary<String, int>> getHomeNumAndScore(String notId)
        {
            List<Dictionary<String, int>> result = new List<Dictionary<String, int>>();
            
            //查询该notId下的作业等级
            String sql1 = "select score,homURL from homework where notId=@nid";
            MySqlParameter para = new MySqlParameter("nid", notId);
            //统计已批改作业的等级和该等级下的作业数
            Dictionary<String, int> dic = new Dictionary<string, int>();
            String[] scores = { "优秀", "良好", "及格", "不及格" };
            int[] scoreNum = { 0, 0, 0, 0 };//定义这四个成绩等级的初始人数默认值为0
            
            //将初始成绩等级和人数添加进dic中
            for (int i = 0; i < scores.Length; i++)
            {
                dic.Add(scores[i], scoreNum[i]);
            }

            //统计已完成作业的人数
            int count = 0;
            //查询作业以及作业的地址
            DataTable table1 = DataUtil.DataOperation.DataQuery(sql1, para);

            //统计已完成该作业的人数（有homURL说明已完成作业）
            for (int i = 0; i < table1.Rows.Count; i++)
            {
                if (table1.Rows[i][1] != DBNull.Value)//说明已完成作业
                {
                    string score = table1.Rows[i][0].ToString();
                    if (score != "")//说明作业已被批改
                    {
                        dic[score] += 1;//该成绩等级的人数加一
                    }
                    count++;//已完成作业的人数加一
                }
            }
            Dictionary<String, int> countNum = new Dictionary<string, int>();
            countNum.Add("已完成", count);
            countNum.Add("未完成", table1.Rows.Count - count);
            //先添加该作业公告已完成和未完成的作业数量
            result.Add(countNum);
            //再添加已批改作业的等级和该等级下的作业数
            result.Add(dic);
            return result;

        }

        public bool InsertComment(Comment comm)
        {
            
            //向comm表添加新作业记录
            String sql = "insert into comment (commId, notId, commStudent, commTeacher) values (@comid,@notid,@comStuid,@comTeaid);";

            //传入要填写的参数
            MySqlParameter para1 = new MySqlParameter("@comid", comm.CommId);
            MySqlParameter para2 = new MySqlParameter("@notid", comm.NotId);
            MySqlParameter para3 = new MySqlParameter("@comStuid", comm.CommStudent);
            MySqlParameter para4 = new MySqlParameter("@comTeaid", comm.CommTeacher);
            return DataUtil.DataOperation.DataAdd(sql, para1, para2, para3, para4);//如果插入成功，则返回true
        }

        public DataTable getSexByTeaSpecId(string teacherSpecId)
        {
            //根据teacherSpecId查询sex
            String sql = "select sex from teacher where teacherSpecId=@tSpecid";
            MySqlParameter parameter = new MySqlParameter("@tSpecid", teacherSpecId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, parameter);  //查到学生id,分数,作业路径
            return table;
        }

    }


}

