using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Entity
{
    class Student
    {
        //学生id
        public int StudentId { private set; get; }
        //学生密码
        public String StudentPassword { private set; get; }
        //学生性别
        public String StudentSex { private set; get; }
        //学生姓名
        public String StudentName { private set; get; }
        //学生班级
        public String StudentClassName { private set; get; }
       
        public int IsDeleted { set; get; }
        //创建时间
        public DateTime CreatedTime { private set; get; }
        //修改时间
        public DateTime ModifiedTime { private set; get; }
        //自定义预警作业数量
        public int DefHomeNum { set; get; }
        //学生具体学号
        public String StudentSpecificId { set; get; }
        public Student()
        {
        }
        //重写ToString方法
        public override String ToString()
        {
            return StudentId.ToString() + "," + StudentPassword + "," + StudentSex + "," + StudentName + "," + StudentClassName;
        }
    }
}
