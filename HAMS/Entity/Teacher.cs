using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Entity
{
    class Teacher
    {
        //教师id
        public int TeacherId { private set; get; }
        //教师密码
        public String Password { set; get; }
        //教师姓名
        public String TeacherName { set; get; }
        //教师性别
        public Char Sex { set; get; }
        //教师系别
        public String Department { set; get; }
        public int IfDelete { set; get; }
        //创建时间
        public DateTime CreateTime { set; get; }
        //教师具体工号
        public String TeacherSpecificId { set; get; }
        //修改时间
        public DateTime ModifyTime { set; get; }
        //课堂id
        public int ClassId { set; get; }
    }
}
