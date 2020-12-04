using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Entity
{
    class Notice
    {
        //作业公告的id
        public int NotId { private set; get; }
        public int IfDelete { set; get; }
        //作业的提交时间
        public DateTime SubmitTime { set; get; }
        //作业的真实截止时间
        public DateTime TruDeadLine { set; get; }
        //作业公告的具体内容
        public String Content { set; get; }
        //作业公告的路径地址
        public String NoteURL { set; get; }
        //作业公告附件的名字
        public String NoteName { set; get; }
        //作业公告的标题
        public String NoteTitle { set; get; }
        //班级的id
        public int ClassId { set; get; }
    }
}
