using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Entity
{
    class SysNotice
    {
        public int SysId { private set; get; }
        public String SysTitle { set; get; }
        public String SysContent { set; get; }
        public int IfDelete { set; get; }
        public DateTime CreateTime { private set; get; }
        public DateTime ModifyTime { set; get; }
        public int AdminId { set; get; }
    }
}
