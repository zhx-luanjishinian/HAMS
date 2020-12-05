using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using HAMS.ToolClass;
using HAMS.Student.StudentDao;


namespace HAMS.Student.StudentService
{
    class SService
       
    {
        private SDao sd = new SDao();

        public BaseResult login(string account,string pw)
        {
            if(account =="" || pw == "")
            {
                return BaseResult.errorMsg("账号或者密码为空");
            }
            DataTable table = sd.Login(account, pw);
            if (table.Rows.Count == 0)
            {
                return BaseResult.errorMsg( "账号或者密码输入错误，请检查后再进行输入");
            }
            else if (table.Rows[0][2].ToString() != pw)
            {
                return BaseResult.errorMsg("账号正确，密码错误");
            }
            else
            {
                return BaseResult.ok(table.Rows[0][1].ToString());
            }
            
        }
    }
}
