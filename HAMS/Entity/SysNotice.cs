using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Entity
{
    class SysNotice
    {
        //系统公告编号
        public int SysId { private set; get; }
        //系统公告名
        public String SysTitle { set; get; }
        //系统公告内容
        public String SysContent { set; get; }
        public int IfDelete { set; get; }
        //创建时间
        public DateTime CreateTime { private set; get; }
        //修改时间
        public DateTime ModifyTime { set; get; }
        //管理员id
        public int AdminId { set; get; }
    }
}
