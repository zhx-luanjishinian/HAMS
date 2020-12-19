using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;
using HAMS.DataUtil;
using System.Collections;
using System.Collections.Generic;
using HAMS.ToolClass;

namespace HAMS.Student.StudentDao
{
    class SDao
    {
        private MySqlConnection conn = DataUtil.DBUtil.getConnection();
        public DataTable Login(String account, String pw)
        {
            
            String sql = "select stuSpecId,name,password from student where stuSpecId=@id";
            //传入要填写的参数
            MySqlParameter parameter = new MySqlParameter("@id",account);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, parameter);
            return table;
           
        }
        public Dictionary<int,List<String>> showCourseInfo(String account)
        {
            //此类用来装返回的对象
            
            Dictionary<int,List<String>> dictionaryEntry = new Dictionary<int, System.Collections.Generic.List<String>>();
            String sql = "select stuId from student where stuSpecId=@id";
            MySqlParameter parameter = new MySqlParameter("@id", account);
            DataTable table = DataOperation.DataQuery(sql, parameter);
            String sql1 = "select classId from takecourse where stuId =@sid";
            MySqlParameter parameter1 = new MySqlParameter("@sid", table.Rows[0][0].ToString());
            DataTable table1 = DataOperation.DataQuery(sql1, parameter1);
            String sql2 = "select teacherId,classSpecId,className from class where classId=@cid";
            String sql3 = "select name,department from teacher where teacherId=@tid";
            for(int i =0;i<table1.Rows.Count; i++)
            {
                //所有的信息都装在列表里面
                List<String> arrayList = new List<string>();
                //取每一条记录中的classid进行查询
                MySqlParameter para1 = new MySqlParameter("@cid", table1.Rows[i][0].ToString());
                DataTable table2 = DataOperation.DataQuery(sql2, para1);
                MySqlParameter para2 = new MySqlParameter("@tid", table2.Rows[0][0].ToString());
                DataTable table3 = DataOperation.DataQuery(sql3, para2);
                //添加老师的名字
                arrayList.Add(table3.Rows[0][0].ToString());
                //添加老师的学院
                arrayList.Add(table3.Rows[0][1].ToString());
                //添加课堂号
                arrayList.Add(table2.Rows[0][1].ToString());
                //添加课堂名字
                arrayList.Add(table2.Rows[0][2].ToString());
                dictionaryEntry.Add(i, arrayList);
            }
            return dictionaryEntry;
      

        }
        //查询作业公告的信息
        public List<List<String>> showHomeNoticeInfo(String account)
        {
           
            List<List<String>> info = new List<List<String>>();
            //先查学生id(自增)
            String sql = "select stuId from student where stuSpecId=@id";
            MySqlParameter parameter = new MySqlParameter("@id", account);
            DataTable table = DataOperation.DataQuery(sql, parameter);
            //再到选课表里面查询课程id
            String sql1 = "select classId from takecourse where stuId =@sid";
            //查询每门课程具体的课堂号
            String sql3 = "select classSpecId from class where classId=@cid";
            MySqlParameter parameter1 = new MySqlParameter("@sid", table.Rows[0][0].ToString());
            DataTable table1 = DataOperation.DataQuery(sql1, parameter1);
            //查询作业的标题
            String sql2 = "select notTitle,notId from notice where classId=@cid";
            for(int i = 0; i < table1.Rows.Count; i++)
            {
                MySqlParameter para = new MySqlParameter("@cid", table1.Rows[i][0].ToString());
                DataTable table2 = DataOperation.DataQuery(sql2, para);
                DataTable table3 = DataOperation.DataQuery(sql3, para);
                //一门课程不只有一个作业
                for(int j = 0; j < table2.Rows.Count; j++)
                {
                    List<String> result = new List<String>();
                    //获得作业公告的标题
                    result.Add(table2.Rows[j][0].ToString());
                    //获得作业公告的id
                    result.Add(table2.Rows[j][1].ToString());
                    //获得每门课程的课堂号
                    result.Add(table3.Rows[0][0].ToString());

                    info.Add(result);
                }
            }
            
            return info;

        }
        /*查询每门课程的所有作业信息,根据上一个界面传递的课堂号来查找该门课堂的作业,同时查找该门
         课程设计的作业的作业id以及该名学生的stuId
         */
        public Dictionary<int,List<String>> showAllHomeworkInfo(String classSpecId)
        {
            Dictionary<int, List<String>> result = new Dictionary<int, List<string>>();   //定义返回值，类型是字典类型
            String sql = "select classId,className from class where classSpecId=@cid"; //通过真实课堂号来查询课堂号、课堂名
            MySqlParameter para = new MySqlParameter("@cid", classSpecId); //赋值操作
            DataTable table = DataOperation.DataQuery(sql, para);  //执行SQL语句进行查询
            String sql1 = "select notTitle,content,notId,truDeadline from notice where classId =@cd";   //通过课堂号来查找notTitle,content,notId,truDeadline
            MySqlParameter para1 = new MySqlParameter("@cd", table.Rows[0][0].ToString());  //进行赋值操作
            DataTable table1 = DataOperation.DataQuery(sql1, para1);  //执行SQL语句进行查询
           //在table1中进行遍历。table1中内容是notTitle,content,notId,truDeadline
            for(int i = 0; i < table1.Rows.Count; i++)
            {
                //新建info字符串列表
                List<String> info = new List<string>();  
                //先添加作业的标题
                info.Add(table1.Rows[i][0].ToString());  
                //然后添加作业的具体内容
                info.Add(table1.Rows[i][1].ToString());
                //添加作业公告的id
                info.Add(table1.Rows[i][2].ToString());
                //添加作业的具体截止时间
                info.Add(table1.Rows[i][3].ToString());
                //添加课堂名
                //result的value每一个值都是一个列表，列表里是作业的信息，key是0，1，2，3，4，5，6
                result.Add(i, info);
            }
            //添加课堂名字，只有一个就不需要循环了
            List<String> className = new List<string>();
            className.Add(table.Rows[0][1].ToString());
            result.Add(table1.Rows.Count, className); //key是作业数量的统计，value是课堂名
            return result;
        }
        //判断学生的作业状态,直接返回一张表,传入学生的学号以及具体的课堂号
        public List<String> defineHomeworkStatus(String account,String notId)
        {
            List<String> rtn = new List<String>();
            //查询stuId
            String sql = "select stuId from student where stuSpecId=@sid";
            MySqlParameter para = new MySqlParameter("@sid", account);
            DataTable table = DataOperation.DataQuery(sql, para);
            //查询作业表中的信息，包括homId,submitTime,score,homURLName
            String sql1 = "select homId,submitTime,score,homURLName from homework where notId =@nid and stuId=@sd";
            MySqlParameter para1 = new MySqlParameter("@nid", notId);//notId的赋值
            MySqlParameter para2 = new MySqlParameter("@sd", table.Rows[0][0].ToString());//stuId的赋值
            //此处有两个查询条件
            MySqlParameter[] paras = new MySqlParameter[2];
            paras[0] = para1;
            paras[1] = para2;
            DataTable table1 = DataOperation.DataQuery(sql1, paras);
            //通过notId查truDeadline
            String sql2 = "select truDeadline from notice where notId = @nid";
            DataTable table2 = DataOperation.DataQuery(sql2, para1);
            
            for (int i=0;i<table1.Columns.Count;i++)
            {
                //MessageBox.Show(table1.Rows[0][i].ToString());
                rtn.Add(table1.Rows[0][i].ToString());
            }
            rtn.Add(table2.Rows[0][0].ToString());
            return rtn;
        }
        //查询dohomework主界面的信息，传入的直接就是作业的id
        public DataTable showDoHomeworkInfo(String notId)
        {
            String sql = "select notTitle,content,truDeadline,notURLName from notice where notId=@nid";
            MySqlParameter para = new MySqlParameter("@nid", notId);
            DataTable table = DataOperation.DataQuery(sql, para);
            return table;
        }
        //查询作业预警主界面的信息,直接根据学生的学号进行查询
        public List<List<List<String>>> alertFormInfo(String account)
        {
            //此处返回超过老师设置的截止时间的作业和没有超过老师设置的截止时间的作业
            List<List<String>> notBeyondHomework = new List<List<string>>();
            List<List<String>> BeyondHomework = new List<List<string>>();
            List<List<List<String>>> result = new List<List<List<string>>>();     
            String sql = "select stuId from student where stuSpecId=@id";
            MySqlParameter parameter = new MySqlParameter("@id", account);
            DataTable table = DataOperation.DataQuery(sql, parameter);
            //再到选课表里面查询课程id
            String sql1 = "select classId from takecourse where stuId =@sid";
            MySqlParameter parameter1 = new MySqlParameter("@sid", table.Rows[0][0].ToString());
            DataTable table1 = DataOperation.DataQuery(sql1, parameter1);
            //查询作业公告的id
            String sql2 = "select notId,notTitle,truDeadline from notice where classId=@cid";
            //查询每门课程的具体名字
            String sql3 = "select className from class where classId=@cid";
            //定位到每个具体的作业，方便进行作业时间的统计
            String sql4 = "select defDeadline,defComplexity,homURL from homework where notId=@nid and stuId=@sid";
            //查询具体的作业情况
            for(int i = 0; i < table1.Rows.Count; i++)
            {
                MySqlParameter para = new MySqlParameter("@cid", table1.Rows[i][0].ToString());
                //作业公告信息
                DataTable table2 = DataOperation.DataQuery(sql2, para);
                //课堂名信息
                DataTable table3 = DataOperation.DataQuery(sql3, para);
                
                //由于一门课的作业公告又有多个，所以还要进行一次循环操作
                for(int j = 0; j < table2.Rows.Count; j++)
                {
                    List<String> ls = new List<string>();
                    //1添加课堂名信息
                    ls.Add(table3.Rows[0][0].ToString());
                    //2添加该项作业的具体截止时间
                    ls.Add(table2.Rows[j][2].ToString());
                    //3添加该项作业的作业标题
                    ls.Add(table2.Rows[j][1].ToString());
                    //4添加该项作业的作业公告id
                    ls.Add(table2.Rows[j][0].ToString());
                    MySqlParameter para1 = new MySqlParameter("@nid", table2.Rows[j][0]);
                    MySqlParameter[] paras = new MySqlParameter[2];
                    //将作业公告id和学生的id添加到参数中去
                    paras[0] = para1;
                    paras[1] = parameter1;
                    DataTable table4 = DataOperation.DataQuery(sql4, paras);
                    //5,6添加每门作业的自定义截止时间，自定义复杂度
                    //此处需要判断用户有没有自定义截止时间和自定义复杂度,其实if判断语句可以不需要
                    if (table4.Rows.Count > 0)
                    {
                        ls.Add(table4.Rows[0][0].ToString());
                        ls.Add(table4.Rows[0][1].ToString());
                        ls.Add(table4.Rows[0][2].ToString());
                    }
                    //如果还没有达到老师设置的截止时间就放在没有超过截止时间的里面
                    if (Convert.ToDateTime(table2.Rows[j][2].ToString()) > DateTime.Now) {  
                    notBeyondHomework.Add(ls);
                    }
                    //否则就将其放在超过了截止时间的列表里面
                    else
                    {
                        BeyondHomework.Add(ls);
                    }
                }
                //首先放的是没有超过截止时间的数据
                result.Add(notBeyondHomework);
                //然后放超过了截止时间的数据
                result.Add(BeyondHomework);
            }
            return result;
        }
        //统计学生已经完成的作业数量
        public int countHomeworkNumber(String account)
        {
            String sql = "select stuId from student where stuSpecId=@sid";
            MySqlParameter para = new MySqlParameter("@sid", account);
            DataTable table = DataOperation.DataQuery(sql, para);
            String sql1 = "select homURL from homework where stuId=@sd";
            MySqlParameter para1 = new MySqlParameter("@sd", table.Rows[0][0].ToString());
            DataTable table1 = DataOperation.DataQuery(sql1, para1);
            int count = 0;
            for(int i = 0; i < table1.Rows.Count; i++)
            {
                if (table1.Rows[i][0] != DBNull.Value)
                {
                    count++;
                }
            }
            return count;
        }
        //设置预警数量的值
        public String showAlertNumber(String account)
        {
            String sql = "select defHomNum from student where stuSpecId = @sid";
            MySqlParameter para = new MySqlParameter("@sid", account);
            DataTable table = DataOperation.DataQuery(sql, para);
            return table.Rows[0][0].ToString();
        }
        //插入用户设置的预警数量
        public bool insertAlertNum(String num)
        {
            String sql = "update student set defHomNum=@dfn";
            MySqlParameter para = new MySqlParameter("@dfn", long.Parse(num));
            return DataOperation.DataUpdate(sql, para);
        }
        //进行作业复杂度信息的更新
        public bool updateComplexity(String account,String notId,String complexity)
        {
            String sql = "select stuId from student where stuSpecId=@sid";
            MySqlParameter para = new MySqlParameter("@sid", account);
            DataTable table = DataOperation.DataQuery(sql, para);
            String sql1 = "update homework set defComplexity=@cl where stuId=@sd and notId=@nid";
            MySqlParameter[] paras = new MySqlParameter[3];
            MySqlParameter para1 = new MySqlParameter("@cl", complexity);
            paras[0] = para1;
            MySqlParameter para2 = new MySqlParameter("@sd", table.Rows[0][0].ToString());
            paras[1] = para2;
            MySqlParameter para3 = new MySqlParameter("@nid", notId);
            paras[2] = para3;
            return DataOperation.DataUpdate(sql1, paras);
           
        }
        //进行自定义截至时间的更新
        public BaseResult updateDefDeadLine(String account,String notId,String defTime)
        {
            //首先根据notId查询真实的截止时间，必须保证用户设置的截止时间小于老师的截止时间
            String sql2 = "select truDeadline from notice where notId=@nid";
            MySqlParameter para4 = new MySqlParameter("@nid", int.Parse(notId));
            DataTable table1 = DataOperation.DataQuery(sql2, para4);
            if (Convert.ToDateTime(table1.Rows[0][0]) < Convert.ToDateTime(defTime))
            {
                return BaseResult.errorMsg("自定义的截止时间不能大于老师的截止时间");
            }
            String sql = "select stuId from student where stuSpecId=@sid";
            MySqlParameter para = new MySqlParameter("@sid", account);
            DataTable table = DataOperation.DataQuery(sql, para);
            String sql1 = "update homework set defDeadline=@dl where stuId=@sd and notId=@nid";
            MySqlParameter[] paras = new MySqlParameter[3];
            MySqlParameter para1 = new MySqlParameter("@dl", defTime);
            paras[0] = para1;
            MySqlParameter para2 = new MySqlParameter("@sd", table.Rows[0][0].ToString());
            paras[1] = para2;
            MySqlParameter para3 = new MySqlParameter("@nid", notId);
            paras[2] = para3;
            if(DataOperation.DataUpdate(sql1, paras))
            {
                return BaseResult.ok();
            }
            else
            {
                return BaseResult.errorMsg("自定义时间设置失败");
            }
        }
        //通过真实学号来获取学生Id号
        public DataTable GetStuIdByAccount(String account)
        {
            //在学生表中，通过真实学号来获取学生的Id号
            String sql = "select stuId from student where stuSpecId=@id";
            //传入要填写的参数
            MySqlParameter parameter = new MySqlParameter("@id", account);
            //执行查询语句，以table类型返回
            DataTable table = DataUtil.DataOperation.DataQuery(sql, parameter);
            return table;
        }
        //获取学生成绩和老师评语，通过stuId和notId
        public DataTable getScoreByNotIdStuId(String stuId,String notId)
        {
            //在学生表中，通过真实学号来获取学生的Id号
            String sql = "select score,remark from homework where stuId=@sid  and notId = @nid";
            //传入要填写的参数
            MySqlParameter para1 = new MySqlParameter("@sid", stuId);
            MySqlParameter para2 = new MySqlParameter("@nid", notId);
            //执行查询语句，以table类型返回
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para1,para2);
            return table;
        }

        //学生提交作业，其实是对homework表的一个修改操作，修改与作业相关的字段即可
        public Boolean UpdateHomework(DateTime submitTime, string notId, string stuId, string postil, string homUrl, string homUrlName)
        {
            //sql语句，对homework表的更新操作
            String sql = "update homework  set postil = @postil, homUrl = @homUrl, homUrlName = @homUrlName , submitTime = @submitTime where notId  = @notId  and  stuId = @stuId;";
            //传入要填写的参数
            MySqlParameter para1 = new MySqlParameter("@postil",postil);
            MySqlParameter para2 = new MySqlParameter("@homUrl", homUrl);
            MySqlParameter para3 = new MySqlParameter("@homUrlName", homUrlName);
            MySqlParameter para4 = new MySqlParameter("@submitTime",submitTime);
            MySqlParameter para5 = new MySqlParameter("@notId", notId);
            MySqlParameter para6 = new MySqlParameter("@stuId",stuId);
            return DataUtil.DataOperation.DataUpdate(sql, para1, para2, para3,para4,para5,para6);//如果更新成功，则返回true
        }

        //通过作业名是否为空来判断它是否交过作业
        public DataTable GetHomeUrlNameByStuIdAndNotId(String stuId, String notId)
        {
            //在作业表中，通过学号和作业公告号来获取作业名 
            String sql = "select homURLName from homework where stuId=@stuid and notId = @notId";
            //传入要填写的参数
            MySqlParameter para1 = new MySqlParameter("@stuId", stuId);
            MySqlParameter para2 = new MySqlParameter("@notId", notId);
            //执行查询语句，以table类型返回
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para1,para2);
            return table;
        }

        //通过classId来获取classSpecld
        //public DataTable GetClassSpecld(string classId)
        //{
        //    //根据真实的课堂号获取课堂表里的自增主键课堂号classId
        //    String sql = "select classSpecId from class where classId = @id;";
        //    //传入要填写的参数
        //    MySqlParameter para = new MySqlParameter("@id", classId);
        //    DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
        //    MessageBox.Show(table.Rows[0][0].ToString());
        //    return table;

        //}
        //通过学生真实学号寻找学生姓名
        //public DataTable GetStuName(string account)
        //{
        //    //根据真实的课堂号获取课堂表里的自增主键课堂号classId
        //    String sql = "select name from student where stuSpecId = @id;";
        //    //传入要填写的参数
        //    MySqlParameter para = new MySqlParameter("@id", account);
        //    DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
        //    return table;

        //}

        //通过作业公告Id来获取作业公告标题
        public DataTable GetNotName(string notId)
        {
            //根据作业公告号来获取作业公告标题
            String sql = "select notTitle from notice where notId = @id;";
            //传入要填写的参数
            MySqlParameter para = new MySqlParameter("@id", notId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, para);
            return table;
        }
        //进行提问操作
        public String insertComm(String notId,String commStudent)
        {
            String result;
            String sql = "insert comm set notId=@notId and commStudent = @comm";
            MySqlParameter para1 = new MySqlParameter("@notId", notId);
            MySqlParameter para2 = new MySqlParameter("@comm", commStudent);
            bool flag=DataUtil.DataOperation.DataAdd(sql, para1, para2);
            if(flag)
            {
                String sql1 = "select commId from comm where notId = @notId and commStudent = @comm";
                DataTable table = DataUtil.DataOperation.DataQuery(sql1, para1, para2);
                result = table.Rows[0][0].ToString();
            }
            else
            {
                return "提问失败，请重试";
            }
            return result;
        }
        //获得每一天提交的作业人数的数量和日期数以及已提交和未提交的人数
        public List<Dictionary<String,int>> getHomeNumAndDate(String classSpecId,String notId)
        {
            List<Dictionary<String,int>> result = new List<Dictionary<String,int>>();
            String sql = "select classId from class where classSpecId =@cid";
            MySqlParameter para = new MySqlParameter("@cid", classSpecId);
            DataTable table = DataOperation.DataQuery(sql,para);
            String sql1 = "select submitTime,homURL  from homework where notId=@nid and classId=@cd";
            MySqlParameter[] paras = new MySqlParameter[2];
            MySqlParameter para1 = new MySqlParameter("nid", notId);
            paras[0] = para1;
            MySqlParameter para2 = new MySqlParameter("@cd", table.Rows[0][0].ToString());
            paras[1] = para2;
            Dictionary<String, int> dic = new Dictionary<string, int>();
            //统计已完成作业的人数
            int count = 0;
            //查询提交时间以及作业的地址
            DataTable table1 = DataOperation.DataQuery(sql1, paras);
            for(int i = 0; i < table1.Rows.Count; i++)
            {
                if (table1.Rows[i][1] != DBNull.Value) { 
                String time = table1.Rows[i][0].ToString().Substring(0,10);
                    if (dic.ContainsKey(time))
                    {
                        //当有这个时间时就进行添加
                        dic[time] += 1;
                    }
                    else
                    {
                        //否则就认为是第一次进行添加
                        dic.Add(time, 1);
                    }
                    count++;
                }
            }
            Dictionary<String, int> countNum = new Dictionary<string, int>();
            countNum.Add("已完成", count);
            countNum.Add("未完成", table1.Rows.Count - count);
            //先添加已完成和未完成的数量
            result.Add(countNum);
            //再添加提交时间和当天提交的人数
            result.Add(dic);
            return result;

        }
        //进行答疑区界面的展示
        public List< List<String>> showAskQuestion(String notId)
        {
            String sql = "select commStudent,commTeacher from comment where notId=@nid";
            MySqlParameter para = new MySqlParameter("@nid", notId);
            DataTable table = DataOperation.DataQuery(sql, para);
            List< List<String>> result = new List<List<String>>();
            for(int i = 0; i < table.Rows.Count; i++)
            {
                List<String> info = new List<String>();
                info.Add(table.Rows[i][0].ToString());
                info.Add(table.Rows[i][1].ToString());
                result.Add(info);
            }
            return result;
        }
        //进行学生答疑信息的插入
        public bool insertStudentComment(String notId,String comment)
        {
            String sql = "insert into comment(notId,commStudent) values(@notId,@comment)";
            MySqlParameter para = new MySqlParameter("@notId", long.Parse(notId));
            MySqlParameter para1 = new MySqlParameter("@comment", comment);
            MySqlParameter[] paras = new MySqlParameter[2];
            paras[0] = para;
            paras[1] = para1;
            return DataOperation.DataAdd(sql, paras);
        }
        //通过学生真实学号来获取头像信息
        public DataTable getSexByStuSpecId(string StuSpecId)
        {
            //根据teacherSpecId查询sex
            String sql = "select sex from student where StuSpecId=@ssid";
            MySqlParameter parameter = new MySqlParameter("@ssid", StuSpecId);
            DataTable table = DataUtil.DataOperation.DataQuery(sql, parameter);  //查到学生id,分数,作业路径
            return table;
        }

    }
}
