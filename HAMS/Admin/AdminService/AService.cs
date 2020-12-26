using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using HAMS.ToolClass;
using HAMS.Admin.AdminDao;

namespace HAMS.Admin.AdminService
{
    class AService
    {
        private ADao ad = new ADao();

        public BaseResult login(string account, string pw)
        {
            if (account == "" || pw == "")
            {
                return BaseResult.errorMsg("账号或者密码为空");
            }
            DataTable table = ad.login(account, pw);
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
                return BaseResult.ok(table.Rows[0][1].ToString());
            }

        }

        //根据学号获得studentManagment界面的信息
        public DataTable  showStudentInfo1(String stuId)
        {
            DataTable result = new DataTable();
            DataTable table = ad.numberStudentInfo(stuId);
            result.Columns.Add(new DataColumn("stuSpecId", typeof(string)));
            result.Columns.Add(new DataColumn("name", typeof(string)));
            result.Columns.Add(new DataColumn("sex", typeof(string)));
            result.Columns.Add(new DataColumn("classroom", typeof(string)));
            result.Columns.Add(new DataColumn("password", typeof(string)));
            //将传入的原始的表dtBefore中每一行中的每个数据复制给dr2这新行，再加入到新表dt中
            DataRow dr2 = null;
            foreach (DataRow row in table.Rows)
            {
                dr2 = result.NewRow();
                dr2[0] = row["stuSpecId"];
                dr2[1] = row["name"];
                if (row["sex"].ToString() == "0")
                {
                    dr2[2] = "女";
                }
                else
                {
                    dr2[2] = "男";
                }
                dr2[3] = row["classroom"];
                dr2[4] = row["password"];
                result.Rows.Add(dr2);
            }

            return result;
        }
        //根据姓名获得studentManagment界面的信息
        public DataTable showStudentInfo2(String stuName)
        {
            DataTable result = new DataTable();
            DataTable table = ad.nameShowStudent(stuName);
            result.Columns.Add(new DataColumn("stuSpecId", typeof(string)));
            result.Columns.Add(new DataColumn("name", typeof(string)));
            result.Columns.Add(new DataColumn("sex", typeof(string)));
            result.Columns.Add(new DataColumn("classroom", typeof(string)));
            result.Columns.Add(new DataColumn("password", typeof(string)));
            //将传入的原始的表dtBefore中每一行中的每个数据复制给dr2这新行，再加入到新表dt中
            DataRow dr2 = null;
            foreach (DataRow row in table.Rows)
            {
                dr2 = result.NewRow();
                dr2[0] = row["stuSpecId"];
                dr2[1] = row["name"];
                if(row["sex"].ToString()=="0")
                {
                    dr2[2] = "女";
                }
                else
                {
                    dr2[2] = "男";
                }
                //dr2[2] = row["sex"];
                dr2[3] = row["classroom"];
                dr2[4] = row["password"];
                result.Rows.Add(dr2);
            }

            return result;
        }
        //根据学号和姓名获得studentManagment界面的信息
        public DataTable showStudentInfo3(String stuNumber, String stuName)
        {
            DataTable result = new DataTable();
            DataTable table = ad.showStudent(stuNumber,stuName);
            result.Columns.Add(new DataColumn("stuSpecId", typeof(string)));
            result.Columns.Add(new DataColumn("name", typeof(string)));
            result.Columns.Add(new DataColumn("sex", typeof(string)));
            result.Columns.Add(new DataColumn("classroom", typeof(string)));
            result.Columns.Add(new DataColumn("password", typeof(string)));
            //将传入的原始的表dtBefore中每一行中的每个数据复制给dr2这新行，再加入到新表dt中
            DataRow dr2 = null;
            foreach (DataRow row in table.Rows)
            {
                dr2 = result.NewRow();
                dr2[0] = row["stuSpecId"];
                dr2[1] = row["name"];
                if (row["sex"].ToString() == "0")
                {
                    dr2[2] = "女";
                }
                else
                {
                    dr2[2] = "男";
                }
                dr2[3] = row["classroom"];
                dr2[4] = row["password"];
                result.Rows.Add(dr2);
            }
            return result;
        }
        //修改学生信息
        public bool reviseStudent(string studentSpecId, string studentName, int studentSex, string studentClass, string studentPass)
        {
            bool flag = ad.updateStudentInfo(studentSpecId, studentName, studentSex, studentClass, studentPass);
            return flag;
        }
        //根据工号获得teacherManagment界面的信息
        public DataTable showTeacherInfo1(String teaId)
        {
            DataTable result = new DataTable();
            DataTable table = ad.numberShowTeacher(teaId);
            result.Columns.Add(new DataColumn("teacherSpecId", typeof(string)));
            result.Columns.Add(new DataColumn("name", typeof(string)));
            result.Columns.Add(new DataColumn("sex", typeof(string)));
            result.Columns.Add(new DataColumn("department", typeof(string)));
            result.Columns.Add(new DataColumn("password", typeof(string)));
            //将传入的原始的表dtBefore中每一行中的每个数据复制给dr2这新行，再加入到新表dt中
            DataRow dr2 = null;
            foreach (DataRow row in table.Rows)
            {
                dr2 = result.NewRow();
                dr2[0] = row["teacherSpecId"];
                dr2[1] = row["name"];
                if (row["sex"].ToString() == "0")
                {
                    dr2[2] = "女";
                }
                else
                {
                    dr2[2] = "男";
                }
                dr2[3] = row["department"];
                dr2[4] = row["password"];
                result.Rows.Add(dr2);
            }
            return result;
        }
        //根据姓名获得teacherManagment界面的信息
        public DataTable showTeacherInfo2(String teaName)
        {
            DataTable result = new DataTable(); ;
            DataTable table = ad.nameShowTeacher(teaName);
            result.Columns.Add(new DataColumn("teacherSpecId", typeof(string)));
            result.Columns.Add(new DataColumn("name", typeof(string)));
            result.Columns.Add(new DataColumn("sex", typeof(string)));
            result.Columns.Add(new DataColumn("department", typeof(string)));
            result.Columns.Add(new DataColumn("password", typeof(string)));
            //将传入的原始的表dtBefore中每一行中的每个数据复制给dr2这新行，再加入到新表dt中
            DataRow dr2 = null;
            foreach (DataRow row in table.Rows)
            {
                dr2 = result.NewRow();
                dr2[0] = row["teacherSpecId"];
                dr2[1] = row["name"];
                if (row["sex"].ToString() == "0")
                {
                    dr2[2] = "女";
                }
                else
                {
                    dr2[2] = "男";
                }
                dr2[3] = row["department"];
                dr2[4] = row["password"];
                result.Rows.Add(dr2);
            }
            return result;
        }
        //根据工号和姓名获得teacherManagment界面的信息
        public DataTable showTeacherInfo3(String teaNum,String teaName)
        {
            DataTable result = new DataTable(); ;
            DataTable table = ad.showTeacher(teaNum,teaName);
            result.Columns.Add(new DataColumn("teacherSpecId", typeof(string)));
            result.Columns.Add(new DataColumn("name", typeof(string)));
            result.Columns.Add(new DataColumn("sex", typeof(string)));
            result.Columns.Add(new DataColumn("department", typeof(string)));
            result.Columns.Add(new DataColumn("password", typeof(string)));
            //将传入的原始的表dtBefore中每一行中的每个数据复制给dr2这新行，再加入到新表dt中
            DataRow dr2 = null;
            foreach (DataRow row in table.Rows)
            {
                dr2 = result.NewRow();
                dr2[0] = row["teacherSpecId"];
                dr2[1] = row["name"];
                if (row["sex"].ToString() == "0")
                {
                    dr2[2] = "女";
                }
                else
                {
                    dr2[2] = "男";
                }
                dr2[3] = row["department"];
                dr2[4] = row["password"];
                result.Rows.Add(dr2);
            }
            return result;
        }
        //修改教师信息
        public bool reviseTeacher(string teacherSpecId, string teacherName, int teacherSex, string teacherDep, string teacherPass)
        {
            bool flag = ad.updateTeacherInfo(teacherSpecId, teacherName, teacherSex, teacherDep , teacherPass);
            return flag;
        }
        //根据课堂号获得classManagment界面的信息
        public DataTable showClassInfo1(String classId)
        {
            DataTable result = new DataTable();
            DataTable table = ad.numberShowClass(classId);
            result.Columns.Add(new DataColumn("classSpecId", typeof(string)));
            result.Columns.Add(new DataColumn("className", typeof(string)));
            result.Columns.Add(new DataColumn("teacherId", typeof(string)));
            //将传入的原始的表dtBefore中每一行中的每个数据复制给dr2这新行，再加入到新表dt中
            DataRow dr2 = null;
            foreach (DataRow row in table.Rows)
            {
                dr2 = result.NewRow();
                dr2[0] = row["classSpecId"];
                dr2[1] = row["className"];
                dr2[2] = row["teacherId"];
                int teaId = int.Parse(dr2[2].ToString());
                DataTable table2 = ad.showTeacherSpecId(teaId);
                dr2[2] = table2.Rows[0][0];
                result.Rows.Add(dr2);
            }
            return result;
        }
        //根据课堂名获得classManagment界面的信息
        public DataTable showClassInfo2(String className)
        {
            DataTable result = new DataTable();
            DataTable table = ad.nameShowClass(className);
            result.Columns.Add(new DataColumn("classSpecId", typeof(string)));
            result.Columns.Add(new DataColumn("className", typeof(string)));
            result.Columns.Add(new DataColumn("teacherId", typeof(string)));
            //将传入的原始的表dtBefore中每一行中的每个数据复制给dr2这新行，再加入到新表dt中
            DataRow dr2 = null;
            foreach (DataRow row in table.Rows)
            {
                dr2 = result.NewRow();
                dr2[0] = row["classSpecId"];
                dr2[1] = row["className"];
                dr2[2] = row["teacherId"];
                int teaId = int.Parse(dr2[2].ToString());
                DataTable table2 = ad.showTeacherSpecId(teaId);
                dr2[2] = table2.Rows[0][0];
                result.Rows.Add(dr2);
            }
            return result;
        }
        //根据课堂号和课堂名获得classManagment界面的信息
        public DataTable showClassInfo3(String classId,String className)
        {
            DataTable result = new DataTable();
            DataTable table = ad.showClass(classId,className);
            result.Columns.Add(new DataColumn("classSpecId", typeof(string)));
            result.Columns.Add(new DataColumn("className", typeof(string)));
            result.Columns.Add(new DataColumn("teacherId", typeof(string)));
            //将传入的原始的表dtBefore中每一行中的每个数据复制给dr2这新行，再加入到新表dt中
            DataRow dr2 = null;
            foreach (DataRow row in table.Rows)
            {
                dr2 = result.NewRow();
                dr2[0] = row["classSpecId"];
                dr2[1] = row["className"];
                dr2[2] = row["teacherId"];
                int teaId = int.Parse(dr2[2].ToString());
                DataTable table2 = ad.showTeacherSpecId(teaId);
                dr2[2] = table2.Rows[0][0];
                result.Rows.Add(dr2);
            }
            return result;
        }
        //修改课堂信息
        public bool reviseClass(string classSpecId, string className, int teacherId)
        {
            bool flag = ad.updateClassInfo(classSpecId, className, teacherId);
            return flag;
        }
        //根据公告号获得noticeManagment界面的信息
        public List<List<String>> showNoticeInfo(String adminId)
        {
            List<List<String>> result = new List<List<string>>();
           
            DataTable table = ad.getSysNotice(adminId);
            //添加公告标题
            for (int i = 0; i<table.Rows.Count ; i++)
            {
                List<String> info = new List<string>();
                info.Add(table.Rows[i][0].ToString());
                info.Add(table.Rows[i][1].ToString());
                info.Add(table.Rows[i][2].ToString());
                result.Add(info);             
              
            }
            return result;
        }
        public String newSysNotice()
        {
            DataTable table = ad.newSysNotice();
            List<String> info = new List<string>();
            info.Add(table.Rows[0][2].ToString());
            return info[0];
        }

    }
}
