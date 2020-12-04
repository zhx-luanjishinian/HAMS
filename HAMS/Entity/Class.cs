using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Entity
{
    class Class
    {
        public int ClassId { private set; get; }
        public String ClassName { private set; get; }
        public int IfDelete { set; get; }
        public DateTime CreateTime { private set; get; }
        public DateTime ModifyTime { set; get; }
        //具体的课堂号
        public String ClassSpecificId { set; get; }
    }
}
