using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Entity
{
    class Teacher
    {
        public int TeacherId { private set; get; }
        public String Password { set; get; }
        public String TeacherName { set; get; }
        public Char Sex { set; get; }
        public String Department { set; get; }
        public int IfDelete { set; get; }
        public DateTime CreateTime { set; get; }
        public String TeacherSpecificId { set; get; }
        public DateTime ModifyTime { set; get; }
        public int ClassId { set; get; }
    }
}
