using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Entity
{
    class Admin
    {
        public int AdminId { private set; get; }
        public String PassWord { private set; get; }
        public String AdminName { set; get; }
        public Char Sex { set; get; }
        public int IfDelete { set; get; }
        public DateTime CreateTime { private set; get; }
        public DateTime ModifyTime { set; get; }
    }
}
