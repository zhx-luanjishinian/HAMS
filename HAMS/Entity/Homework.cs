using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Entity
{
    class Homework
    {
        public int HomeId { private set; get; }
        public DateTime SubmitTime { set; get; }
        //设置截止时间
        public DateTime DefDeadLine { set; get; }
        //设置作业的复杂度
        public String DefComplexity { set; get; }
        //作业备注
        public String Postil { set; get; }
        //作业的具体级别
        public String Score { set; get; }
        //remark是老师的评语
        public String Remark { set; get; }
        //作业文件的具体存放地址
        public String HomeURL { set; get; }
        //作业的名字
        public String HomeName { set; get; }
        //是否删除
        public int IfDelete { set; get; }
        //学生学号
        public int StuId { set;  get; }

        public int ClassId { set; get; }

        public int TeacherId { set; get; }

        public int NotId { set; get; }


    }
}
