using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Entity
{
    class Comm2
    {
        //二级评论和一级评论的id
        public int Comm2Id { set; get; }
        public int Comm1Id { set; get; }
        //评论的具体内容
        public String Comment2 { set; get; }
        //判断是否是老师
        public int IfTeacher { set; get; }
    }
}
