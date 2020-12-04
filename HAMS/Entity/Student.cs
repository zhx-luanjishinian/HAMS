using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Entity
{
    class Student
    {
        public int StudentId { private set; get; }
        public String StudentPassword { private set; get; }
        public String StudentSex { private set; get; }
        public String StudentName { private set; get; }
        public String StudentClassName { private set; get; }
        public int IsDeleted { set; get; }
        public DateTime CreatedTime { private set; get; }
        public DateTime ModifiedTime { private set; get; }
        public int DefHomeNum { set; get; }
        public String StudentSpecificId { set; get; }
        public Student()
        {
        }
        public override String ToString()
        {
            return StudentId.ToString() + "," + StudentPassword + "," + StudentSex + "," + StudentName + "," + StudentClassName;
        }
    }
}
