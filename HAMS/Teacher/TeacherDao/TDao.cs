﻿
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
        public DataTable login(String account, String pw)
        {

            String sql = "select teacherSpecId,name,password from teacher where teacherSpecId=@id";
            //传入要填写的参数
            MySqlParameter parameter = new MySqlParameter("@id", account);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, parameter);
            //MessageBox.Show(table.Rows[0][0].ToString());->stuSpecId
            //MessageBox.Show(table.Rows[0][1].ToString());->name
            //MessageBox.Show(table.Rows[0][2].ToString());->password
            return table;
        }

        //查询教师主界面左侧信息
        public DataTable loadMainFormLeft(string teacherSpecId)
        {
            //sql语句
            String sql = "select * from class where teacherId=@id";   //查询对应老师id的class表中所有的数据，这里查不到
            String sql1 = "select teacherId from teacher where teacherSpecId=@spaceId";  //根据当前老师的spaceId查询teacherId
            MySqlParameter parameter1 = new MySqlParameter("@spaceId", teacherSpecId);   //tbTeacherInfo.Text,这里我修改了一下，传过来的只有老师工号
            DataTable table1 = DataUtil.DataOperation.dataQuery(sql1, parameter1);
            //MessageBox.Show(table1.Rows[0][0].ToString());
            //以上是正确的
            MySqlParameter parameter = new MySqlParameter("@id", table1.Rows[0][0]);  //查到当前老师的teacherID
            DataTable table = DataUtil.DataOperation.dataQuery(sql, parameter);       //在class表中查老师教的课程名
        
            return table;
        }
        //查询教师布置作业的截止时间
        public DataTable getTrueDeadLine(String notId)
        {
            //sql语句
            String sql = "select truDeadline from notice where notId=@id";   //根据noticeId查找truDeadline
            MySqlParameter parameter = new MySqlParameter("@id", notId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, parameter);       
            return table;
        }
        //查询作业公告发布时间
        public DataTable GetSubmitTime(string notId)
        {
            //sql语句
            String sql = "select submitTime from notice where notId=@id";   //根据noticeId查找truDeadline
            MySqlParameter parameter = new MySqlParameter("@id", notId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, parameter);
            return table;
        }
        //查找notId
        public DataTable getNotIdByClassIdAndNotTitle(String notTitle,int classId)    //根据名称和classId查notId
        {
            //sql语句
            String sql = "select notId from notice where notTitle=@id and classId=@cId";   //根据noticeId查找truDeadline
            MySqlParameter parameter = new MySqlParameter("@id", notTitle);
            MySqlParameter parameter1 = new MySqlParameter("@cid", classId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, parameter,parameter1);
            return table;
        }
        //根据公告标题和classId查询公告描述
        public DataTable getNotDespByClassIdAndNotTitle(String notTitle, String classId)    //根据名称和classId查content
        {
            //sql语句
            String sql = "select content from notice where notTitle=@id and classId=@cId";   //根据noticeId查找truDeadline
            MySqlParameter parameter = new MySqlParameter("@id", notTitle);
            MySqlParameter parameter1 = new MySqlParameter("@cid", classId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, parameter, parameter1);
            return table;
        }
        //从数据库查询目前已有的作业公告
        public DataTable getNotice(string classSpaceId)  //从数据库查询目前已有的作业
        {
            
            DataTable table0 = getClassId(classSpaceId);  //查询需要的classId,使用查询classId的函数
            String sql = "select * from notice where classId = @id;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@id", table0.Rows[0][0]);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, para);
            return table;

        }
        //新增作业公告
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
            return DataUtil.DataOperation.dataAdd(sql, para1, para2, para3, para4, para5, para6);//如果插入成功，则返回true
        }
        //更新作业公告
        public bool updateNotice(DateTime truDeadline,string content,string notURLName,int notId)
        {
            //根据notId更新truDeadline,content,notURLName
            String sql = "update notice set truDeadline=@truDdl,content=@cont,notURLName=@ntUrlName where notId=@nid";
            //传入要填写的参数
            MySqlParameter para1 = new MySqlParameter("@truDdl", truDeadline);
            MySqlParameter para2 = new MySqlParameter("@cont", content);
            MySqlParameter para3 = new MySqlParameter("@ntUrlName", notURLName);
            MySqlParameter para4 = new MySqlParameter("@nid", notId);
            return DataUtil.DataOperation.dataAdd(sql, para1, para2, para3, para4);//如果插入成功，则返回true
        }
        //根据课堂号获得课堂id
        public DataTable getClassId(string classSpecId)
        {
            //根据真实的课堂号获取课堂表里的自增主键课堂号classId
            String sql = "select classId from class where classSpecId = @id;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@id", classSpecId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, para);
            return table;

        }
        public DataTable getNoteTitle(int classId)
        {
            //从数据库表中
            String sql = "select notTitle from notice where classId = @cid;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@cid", classId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, para);
            return table;

        }
       //获得教师id
        public DataTable getTeacherId(String TeacherSpecId)
        {
            //根据教师工号获取课堂表里的自增主键教师号teacherId
            String sql = "select teacherId from teacher where teacherSpecId = @tid;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@tid", TeacherSpecId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, para);
            return table;
        }
        //查询发布公告数
        public int getNoticeNum(string classId)
        {
            //根据classId获取notice的数量
            String sql = "select * from notice where classId = @tid;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@tid", classId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, para);
            return table.Rows.Count;
        }
        //获得学生人数
        public int getStuNum(string classId)
        {
            //根据classId获取notice的数量
            String sql = "select * from takecourse where classId = @tid;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@tid", classId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, para);
            return table.Rows.Count;
        }
        //查询近期发布作业公告
        public DataTable getRecentNoticeByClassId(string classId)
        {
            //根据classId获取公告表里的全部内容
            String sql = "select * from notice where classId = @tid;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@tid", classId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, para);
            return table;
        }
        //根据教师id获得classId
        public DataTable getClassIdByTId(string teacherId)
        {
            //根据teacherId获得classId
            String sql = "select classId from class where teacherId = @tid;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@tid", teacherId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, para);
            return table;
        }
        //根据homeworkId获得附件地址
        public DataTable getHomURLByHomId(int homId)
        {
            //根据homId获得homURL
            String sql = "select homURL from homework where homId = @hid;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@hid", homId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, para);
            return table;
        }
        //查询作业评语
        public DataTable getPostilByHomId(int homId)
        {
            //根据homId获得postil
            String sql = "select postil from homework where homId = @hid;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@hid", homId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, para);
            return table;
        }
        //更新作业批改信息
        public bool updateHomeworkByCorrect(int homId,string score,string remark)
        {
            String sql = "update homework set score = @sc,remark = @rm where homId = @hid;";
            //传入要填写的参数
            MySqlParameter para1 = new MySqlParameter("@sc", score);
            MySqlParameter para2 = new MySqlParameter("@rm", remark);
            MySqlParameter para3 = new MySqlParameter("@hid", homId);
            return DataUtil.DataOperation.dataUpdate(sql, para1, para2, para3);//如果更新成功，则返回true
        }
        //查询作业批改信息
        public DataTable getScoreAndRemarkByHomId(int homId)    //需要用到的函数
        {
            //根据homId获取score和remark
            String sql = "select score,remark from homework where homId = @hid;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@hid", homId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, para);

            return table;
        }
        //删除作业公告
        public Boolean deleteNotice(string noticeId)
        {
            //根据notTitle删除公告，此功能有问题

            String sql = "delete from notice where notId=@nId;";
          
            //传入要填写的参数
            
            MySqlParameter para = new MySqlParameter("@nId", noticeId);
            return DataUtil.DataOperation.dataDelete(sql, para);//如果删除成功，则返回true
        }
        //删除作业
        public Boolean deleteHomework(String noticeId)
        {
            //因为需要级联删除，因此需要首先删除homework表中的数据,才能删除notice表中的数据
            String sql1 = "delete from homework where notId=@nId;";
            MySqlParameter para = new MySqlParameter("@nId", noticeId);
            return DataUtil.DataOperation.dataDelete(sql1, para);//如果删除成功，则返回true
        }
        //更新作业公告
        public Boolean updateNotice(int notId, string notTitle,string content)
        {
            //根据notId更新notTitle和ontent
            String sql = "update notice set content = @content,notTitle = @notTitle where notId  = @notId;";
            //传入要填写的参数
            MySqlParameter para1 = new MySqlParameter("@content", content);
            MySqlParameter para2 = new MySqlParameter("@notTitle", notTitle);
            MySqlParameter para3 = new MySqlParameter("@notId", notId);
            return DataUtil.DataOperation.dataUpdate(sql, para1, para2, para3);//如果更新成功，则返回true
        }
        //查询学生id
        public DataTable getStuIdFromClassId(int classId)
        {
            //根据classId获取公告表里的全部内容
            String sql = "select stuId from takecourse where classId = @classId;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@classId", classId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, para);
            return table;
        }
        //查询学生id（根据学号）
        public DataTable getStuIdFromStuSpecId(string stuSpecId)
        {
            //根据学生学号查询学生Id
            String sql = "select stuId from student where stuSpecId = @stuSpecId;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@stuSpecId", stuSpecId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, para);
            return table;
        }
        //插入作业
        public bool insertHomework(Homework homework)
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
            return DataUtil.DataOperation.dataAdd(sql, para1, para2, para3, para4);//如果插入成功，则返回true
        }
        //查询作业地址
        public DataTable getHomURLAndNameByHomId(int homId)
        {
            //根据homId获得homURL
            String sql = "select homURL,homURLName from homework where homId = @hid;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@hid", homId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, para);
            return table;
        }
        //查询公告附件名称
        public DataTable getNotURLNameByNotId(int notId)
        {
            //根据notId获得notURLName
            String sql = "select notURLName from notice where notId = @nid;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@nid", notId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, para);
            return table;

        }
        //根据notId查询学生作业路径，用来在作业批改界面下载学生的作业。
        public DataTable getNotURLByNotId(String notId)    
        {
            //根据notId查notURL
            String sql = "select notURL from notice where notId=@nid";   //根据noticeId查找truDeadline
            MySqlParameter parameter = new MySqlParameter("@nid", notId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, parameter);
            return table;
        }

        //获得已批改作业信息
        public DataTable selectHomeworkCheckedInfo(String notId)
        {
            //此类用来装返回的对象

            String sql = "select stuId from homework where score is not null and notId=@nid;";
            MySqlParameter parameter = new MySqlParameter("@nid", notId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, parameter);  //查到学生id,分数,作业路径
            return table;

        }
        //获得待批改作业信息
        public DataTable selectHomeworkNeedCorrectInfo(String notId)
        {
            //此类用来装返回的对象

            String sql = "select stuId from homework where score is null and homURLName is not null and notId=@nid;";   //null
            MySqlParameter parameter = new MySqlParameter("@nid", notId);
   
            DataTable table = DataUtil.DataOperation.dataQuery(sql, parameter);  //查到学生id,分数,作业路径
            return table;

        }
        //获得未完成作业信息
        public DataTable selectHomeworkUnfinishedInfo(String notId)
        {
            //此类用来装返回的对象

            String sql = "select stuId from homework where notId=@nid and homURLName is null;";
            MySqlParameter parameter = new MySqlParameter("@nid", notId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, parameter);  //查到学生id,分数,作业路径
            return table;

        }
        //根据学生Id查询学生学号和姓名
        public DataTable getStudentNameAndIdByStuID(String stuId)
        {
            //此类用来装返回的对象

            String sql = "select stuSpecId,name from student where stuId=@nid";
            MySqlParameter parameter = new MySqlParameter("@nid", stuId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, parameter);  //查到学生id,分数,作业路径
            return table;

        }
        //根据课堂id查到学生id,分数和作业路径
        public DataTable getClassInfoByClassID(String classId)
        {
            //此类用来装返回的对象

            String sql = "select * from class where classId=@nid";
            MySqlParameter parameter = new MySqlParameter("@nid", classId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, parameter);  //查到学生id,分数,作业路径
            return table;

        }

        //根据学生Id和作业公告id查到作业id
        public DataTable getHomIdByStuIdAndNotId(String stuId,string noteId)
        {
            String sql = "select homId from homework where stuId=@nid and notId=@ntId";
            MySqlParameter parameter = new MySqlParameter("@nid", stuId);
            MySqlParameter parameter1 = new MySqlParameter("@ntid", noteId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, parameter,parameter1);  //查到学生id,分数,作业路径
            return table;
        }

        //根据作业id查到学生id
        public DataTable getStuIdByHomId(int homId)
        {
            //根据homId查询stuId
            String sql = "select stuId from homework where homId=@hid";
            MySqlParameter parameter = new MySqlParameter("@hid", homId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, parameter);  //查到学生id,分数,作业路径
            return table;
        }

        //根据公告id查到评论信息
        public DataTable getComment(string notId)
        {
            //根据homId查询stuId
            String sql = "select * from comment where notId=@hid";
            MySqlParameter parameter = new MySqlParameter("@hid", notId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, parameter); 
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
            DataTable table1 = DataUtil.DataOperation.dataQuery(sql1, para);

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
        //更新答疑
        public bool updateComment(string teacherComment,string commentId)
        {

            //向comm表添加新作业记录
            String sql = "update comment set commTeacher=@ntUrlName where commStudent=@nid";

            //传入要填写的参数
           
            MySqlParameter para2 = new MySqlParameter("@ntUrlName", teacherComment);

            MySqlParameter para3 = new MySqlParameter("@nid", commentId);
            return DataUtil.DataOperation.dataUpdate(sql, para2,para3);//如果插入成功，则返回true
        }
        //根据具体教师工号返回教师性别
        public DataTable getSexByTeaSpecId(string teacherSpecId)
        {
            //根据teacherSpecId查询sex
            String sql = "select sex from teacher where teacherSpecId=@tSpecid";
            MySqlParameter parameter = new MySqlParameter("@tSpecid", teacherSpecId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, parameter);  //查到学生id,分数,作业路径
            return table;
        }
        //根据学号返回学生性别
        public DataTable getSexByStuSpecId(string stuSpecId)
        {
            //根据teacherSpecId查询sex
            String sql = "select sex from student where stuSpecId=@tSpecid";
            MySqlParameter parameter = new MySqlParameter("@tSpecid", stuSpecId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, parameter);  //查到学生id,分数,作业路径
            return table;
        }

        //获得该公告的答疑数量
        public int getCommentNumByNotId(String noticeId)
        {
            //根据notId查询答疑表中的行数
            String sql = "select * from comment where notId=@nid";
            MySqlParameter parameter = new MySqlParameter("@nid", noticeId);
            DataTable table = DataUtil.DataOperation.dataQuery(sql, parameter);  //查到学生id,分数,作业路径
            return table.Rows.Count;
        }

        //删除答疑评论
        public Boolean deleteComment(String noticeId)
        {
            //因为需要级联删除，因此需要首先删除comment表中的数据,才能删除notice表中的数据
            String sql1 = "delete from comment where notId=@nId;";
            
            MySqlParameter para = new MySqlParameter("@nId", noticeId);
            return DataUtil.DataOperation.dataDelete(sql1, para);//如果删除成功，则返回true
        }


    }


}

