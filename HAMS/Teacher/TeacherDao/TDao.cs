using HAMS.DataUtil;
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
			String sql = "insert into notice (truDeadline,content,notURL,notTitle,classId) values (@truDdl,@cont,@ntUrl,@ntTitle,@cid);";
			//传入要填写的参数
			MySqlParameter para1 = new MySqlParameter("@truDdl", notice.TruDeadLine);
			MySqlParameter para2 = new MySqlParameter("@cont", notice.Content);
			MySqlParameter para3 = new MySqlParameter("@ntUrl", notice.NoteURL);
			MySqlParameter para4 = new MySqlParameter("@ntTitle", notice.NoteTitle);
			MySqlParameter para5 = new MySqlParameter("@cid", notice.ClassId);
			return DataUtil.DataOperation.DataAdd(sql, para1, para2, para3, para4, para5);//如果插入成功，则返回true
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
		public DataTable getNotIdByClassIdAndNotTitle(string notTitle, int classId)    //根据名称和classId查notId
		{
			//sql语句
			String sql = "select notId from notice where notTitle=@id and classId=@cId";   //根据noticeId查找truDeadline
			MySqlParameter parameter = new MySqlParameter("@id", notTitle);
			MySqlParameter parameter1 = new MySqlParameter("@cid", classId);
			DataTable table = DataUtil.DataOperation.DataQuery(sql, parameter, parameter1);
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

		public bool UpdateHomeworkByCorrect(int homId, string score, string remark)
		{
			String sql = "update homework set score = @sc,remark = @rm where homId = @hid;";
			//传入要填写的参数
			MySqlParameter para1 = new MySqlParameter("@sc", score);
			MySqlParameter para2 = new MySqlParameter("@rm", remark);
			MySqlParameter para3 = new MySqlParameter("@hid", homId);
			return DataUtil.DataOperation.DataUpdate(sql, para1, para2, para3);//如果更新成功，则返回true
		}

		public DataTable GetScoreAndRemarkByHomId(int homId)
		{
			//根据homId获取score和remark
			String sql = "select score,remark from homework where homId = @hid;";
			//传入要填写的参数
			MySqlParameter para = new MySqlParameter("@hid", homId);
			DataTable table = DataUtil.DataOperation.DataQuery(sql, para);

			return table;
		}
		public Boolean deleteNotice(string noticeTitle)
		{
			String sql = "delete from notice where notTitle=@ntitle;";
			//传入要填写的参数
			MySqlParameter para = new MySqlParameter("@ntitle", noticeTitle);
			return DataUtil.DataOperation.DataDelete(sql, para);//如果删除成功，则返回true
		}

		//获得已批改作业信息
		public Dictionary<int, List<String>> showHomeworkCheckedInfo(String account)
		{
			//此类用来装返回的对象
			string str;
			Dictionary<int, List<String>> dictionaryEntry = new Dictionary<int, System.Collections.Generic.List<String>>();

			String sql = "select stuId,score,homURL from homework where notId=@nid";
			MySqlParameter parameter = new MySqlParameter("@nid", account);
			DataTable table = DataOperation.DataQuery(sql, parameter);  //查到学生id,分数,作业路径

			String sql1 = "select stuSpecId,name from student where stuId =@sid";
			for (int i = 0; i < table.Rows.Count; i++)
			{
				
				if (table.Rows[i][1].ToString() != "" && table.Rows[i][2].ToString() != "")
				{
					List<String> arrayList = new List<string>();
					MySqlParameter parameter1 = new MySqlParameter("@sid", table.Rows[i][0].ToString());
					DataTable table1 = DataOperation.DataQuery(sql1, parameter1);  //查到学生学号，姓名
					
					str = table1.Rows[0][0].ToString() + table1.Rows[0][1];  //添加学生学号,姓名
					MessageBox.Show(str);
					arrayList.Add(str);
					dictionaryEntry.Add(i, arrayList);
				}
			}

			return dictionaryEntry;

		}

		//获得待批改作业信息
		public Dictionary<int, List<String>> showHomeworkCheckingInfo(String account)
		{
			//此类用来装返回的对象
			string str;
			Dictionary<int, List<String>> dictionaryEntry = new Dictionary<int, System.Collections.Generic.List<String>>();

			String sql = "select stuId,score,homURL from homework where notId=@nid";
			MySqlParameter parameter = new MySqlParameter("@nid", account);
			DataTable table = DataOperation.DataQuery(sql, parameter);  //查到学生id,分数,作业路径

			String sql1 = "select stuSpecId,name from student where stuId =@sid";
			for (int i = 0; i < table.Rows.Count; i++)
			{
				
				if (table.Rows[i][1].ToString()== "" && table.Rows[i][2].ToString() != "")
				{
					List<String> arrayList = new List<string>();
					MySqlParameter parameter1 = new MySqlParameter("@sid", table.Rows[i][0].ToString());
					DataTable table1 = DataOperation.DataQuery(sql1, parameter1);  //查到学生学号，姓名

					str = table1.Rows[0][0].ToString() + table1.Rows[0][1];  //添加学生学号,姓名
					MessageBox.Show(str);
					arrayList.Add(str);
					dictionaryEntry.Add(i, arrayList);
				}
			}
			return dictionaryEntry;
		}
		//获得未完成作业信息
		public Dictionary<int, List<String>> showHomeworkUnfinishedInfo(String account)
		{
			//此类用来装返回的对象
			string str;
			Dictionary<int, List<String>> dictionaryEntry = new Dictionary<int, System.Collections.Generic.List<String>>();

			String sql = "select stuId,score,homURL from homework where notId=@nid";
			MySqlParameter parameter = new MySqlParameter("@nid", account);
			DataTable table = DataOperation.DataQuery(sql, parameter);  //查到学生id,分数,作业路径

			String sql1 = "select stuSpecId,name from student where stuId =@sid";
			for (int i = 0; i < table.Rows.Count; i++)
			{
				if(table.Rows[i][1].ToString() == "" && table.Rows[i][2].ToString() == "")
				{
					List<String> arrayList = new List<string>();
					MySqlParameter parameter1 = new MySqlParameter("@sid", table.Rows[i][0].ToString());
					DataTable table1 = DataOperation.DataQuery(sql1, parameter1);  //查到学生学号，姓名
					str = table1.Rows[0][0].ToString() + table1.Rows[0][1];  //添加学生学号,姓名
					arrayList.Add(str);
					MessageBox.Show(str);
					dictionaryEntry.Add(i, arrayList);
				}
			}
			return dictionaryEntry;
		}
	}


}

