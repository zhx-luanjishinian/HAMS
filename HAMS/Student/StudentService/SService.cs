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
            //table[0][0]表示第0个从数据库查出来数据的第0条信息-》stuSpecId
            //table[0][1]->name
            //table[0][1]表示第0个从数据库查出来数据的第1条信息-》password
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
        //获得主界面的课程信息
        public Dictionary<int,List<String>> showCourseInfo(String account)
        {
            return sd.showCourseInfo(account);
        }
        //获得主界面的作业信息
        public List<List<String>> showHomeNoticeInfo(String account)
        {
            return sd.showHomeNoticeInfo(account);
        }
        //获取作业主界面每门课程所有的作业信息
        public Dictionary<int,List<String>> showAllHomeworkInfo(String classSpecId)
        {
            return sd.showAllHomeworkInfo(classSpecId);
        }
        //判断学生的作业状态，根据作业状态呈现出不同的值,传入学号，作业公告id,还有该项作业的截止时间
        public String judgeHomeworkStatus(String account,String notId,String deadline)
        {
            DataTable table = sd.defineHomeworkStatus(account, notId);
            //将timestamp时间戳转换为DateTime类型
            DateTime dl = Convert.ToDateTime(deadline);
            DateTime now = DateTime.Now;
            String message = "";
            //查到数据了说明已经交了作业
            if (table.Rows.Count > 0)
            {


                //然后判断老师是否已经批改过作业了,作业评分那一部分不为空，说明作业已经批改
                if (table.Rows[0][2] != DBNull.Value)
                {
                    message = "已批改";
                }
                else
                {
                    message = "待批改";
                }
            }
            else
            {
                //作业为空说明学生还未提交作业，此时再比较系统当前时间和老师设置的截止时间
                //截止时间大于当前时间，说明截止时间还没有到
                if (DateTime.Compare(dl, now) > 0)
                {
                    message = "去完成";
                }
                else
                {
                    message = "已逾期";
                }
            }
            

            return message;
        }
        //获得doHomework界面的信息
        public List<String> showDohomeworkInfo(String notId)
        {
            List<String> result = new List<string>();
            DataTable table = sd.showDoHomeworkInfo(notId);
            //添加作业标题
            result.Add(table.Rows[0][0].ToString());
            //添加作业内容
            result.Add(table.Rows[0][1].ToString());
            //添加作业的截止时间
            result.Add(table.Rows[0][2].ToString());
            //添加作业附件的名字
            if (table.Rows[0][3] != null)
            {
                result.Add(table.Rows[0][3].ToString());
            }
            return result;
        }
        public List<List<List<String>>> showAlertFormInfo(String account)
        {
            return sd.alertFomrInfo(account);
        }
        //统计已完成作业的数量
        public int countHomeworkNumber(String account)
        {
            return sd.countHomeworkNumber(account);
        }
        //进行预警数量的修改
        public bool updateAlertNum(String num)
        {
            return sd.insertAlertNum(num);
        }
        //设置预警数量的值
        public String setAlertNum(String account)
        {
            return sd.showAlertNumber(account);
        }
        //进行作业的升序排序
        public List<List<List<String>>> upRank(String account)
        {
            List<List<List<String>>> results = sd.alertFomrInfo(account);
            List < List < String >> result = results[0];
            result.Sort(delegate (List<String> l1, List<String> l2) {
                if (l1[4] !=""  && l2[4] !="")
                {
                    return l1[4].CompareTo(l2[4]);
                }
                else if (l1[4] != "" && l2[4] == "")
                {
                    return l1[4].CompareTo(l2[1]);
                }
                else if (l1[4] == "" && l2[4] != "")
                {
                    return l1[1].CompareTo(l2[4]);
                }
                return l1[1].CompareTo(l2[1]);
            });
            return results;

        }

        //进行作业的降序排序
        public List<List<List<String >>>downRank(String account)
        {
            List<List<List<String>>> results = sd.alertFomrInfo(account);
            List<List<String>> result = results[0];
            result.Sort(delegate (List<String> l1, List<String> l2) {
                if (l1[4] != "" && l2[4] != "")
                {
                    return l2[4].CompareTo(l1[4]);
                }
                else if (l1[4] != "" && l2[4] == "")
                {
                    return l2[4].CompareTo(l1[1]);
                }
                else if (l1[4] == "" && l2[4] != "")
                {
                    return l2[1].CompareTo(l1[4]);
                }
                return l2[1].CompareTo(l1[1]);
            });
            return results;
        }
        //更新作业复杂度的信息
        public bool updateComplexity(String account,String notId,String complexity)
        {
            return sd.updateComplexity(account, notId, complexity);
        }
        //进行作业复杂度的升序排序
        public List<List<List<String>>> upComplexity(String account)
        {
            List<List<List<String>>> results = sd.alertFomrInfo(account);
            List<List<String>> result = results[0];
            //按第五项进行升序排序
            result.Sort(delegate (List<String> l1, List<String> l2)
            {
                if (l1.Count > 5 && l2.Count > 5) {
                return l1[5].CompareTo(l2[5]);
                }
                return 0;
            });
            return results;
        }
        //进行作业复杂度的降序排序
        public List<List<List<String>>> downComplexity(String account)
        {
            List<List<List<String>>> results = sd.alertFomrInfo(account);
            List<List<String>> result = results[0];
            //进行降序排序
            result.Sort(delegate (List<String> l1, List<String> l2)
            {
                if (l1.Count > 5 && l2.Count > 5)
                {
                    return l2[5].CompareTo(l1[5]);
                }
                return 0;
            });
            return results;
        }
    }
}
