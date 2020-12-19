using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using HAMS.ToolClass;
using HAMS.Student.StudentDao;
using HAMS.Entity;
using System.Windows;

namespace HAMS.Student.StudentService
{
    class SService

    {
        private SDao sd = new SDao();

        public BaseResult login(string account, string pw)
        {
            if (account == "" || pw == "")
            {
                return BaseResult.errorMsg("账号或者密码为空");
            }
            DataTable table = sd.Login(account, pw);
            //table[0][0]表示第0个从数据库查出来数据的第0条信息-》stuSpecId
            //table[0][1]->name
            //table[0][1]表示第0个从数据库查出来数据的第1条信息-》password
            if (table.Rows.Count == 0)
            {
                return BaseResult.errorMsg("账号或者密码输入错误，请检查后再进行输入");
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
        public Dictionary<int, List<String>> showCourseInfo(String account)
        {
            return sd.showCourseInfo(account);
        }
        //获得主界面的作业信息
        public List<List<String>> showHomeNoticeInfo(String account)
        {
            return sd.showHomeNoticeInfo(account);
        }
        //获取作业主界面每门课程所有的作业信息
        public Dictionary<int, List<String>> showAllHomeworkInfo(String classSpecId)
        {
            return sd.showAllHomeworkInfo(classSpecId);
        }
        public String judgeHomeworkStatus(String account,String notId)
        {
            List<String> table = sd.defineHomeworkStatus(account, notId);
            DateTime dl = Convert.ToDateTime(table[4]);
            DateTime now = DateTime.Now;
            String message = "";
            //查到作业名说明已经交了作业


            if (table[3].ToString() != "")
            {
                object score = table[2];
                //MessageBox.Show((score!=DBNull.Value).ToString());
                //MessageBox.Show();
                //然后判断老师是否已经批改过作业了,作业评分那一部分不为空，说明作业已经批改
                if (score.ToString() != "") //如果有分数的话
                {
                    message = "已批改";
                }
                else
                {
                    message = "待批改";
                }
            }
            //没有提交作业的话，进行截止时间的判断
            else
            {
                //作业为空说明学生还未提交作业，此时再比较系统当前时间和老师设置的截止时间
                //截止时间大于当前时间，说明截止时间还没有到
                if (DateTime.Compare(dl, now) > 0)
                {
                    message = "未完成";
                }
                else
                {
                    message = "已逾期";
                }
            }
            return message;
        }
        //判断学生的作业状态，根据作业状态呈现出不同的值,传入学号，作业公告id,还有该项作业的截止时间
        public String judgeHomeworkStatus(String account, String notId, String deadline)
        {
            //account, notId, info[i][3].ToString()真实截止时间
            //table表的列名homId,submitTime,score,homURLName
            List<String> table = sd.defineHomeworkStatus(account, notId);
            //将timestamp时间戳转换为DateTime类型
            DateTime dl = Convert.ToDateTime(deadline);
            DateTime now = DateTime.Now;
            String message = "";
            //查到作业名说明已经交了作业


            if (table[3].ToString() != "")
            {
                object score = table[2];
                //然后判断老师是否已经批改过作业了,作业评分那一部分不为空，说明作业已经批改
                if (score.ToString() != "") //如果有分数的话
                {
                    message = "已批改";
                }
                else
                {
                    message = "待批改";
                }
            }
            //没有提交作业的话，进行截止时间的判断
            else
            {
                //作业为空说明学生还未提交作业，此时再比较系统当前时间和老师设置的截止时间
                //截止时间大于当前时间，说明截止时间还没有到
                if (DateTime.Compare(dl, now) > 0)
                {
                    message = "未完成";
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
            return sd.alertFormInfo(account);
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
            List<List<List<String>>> results = sd.alertFormInfo(account);
            List<List<String>> result = results[0];
            result.Sort(delegate (List<String> l1, List<String> l2)
            {
                if (l1[4] != "" && l2[4] != "")
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
        public List<List<List<String>>> downRank(String account)
        {
            List<List<List<String>>> results = sd.alertFormInfo(account);
            List<List<String>> result = results[0];
            result.Sort(delegate (List<String> l1, List<String> l2)
            {
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
        public bool updateComplexity(String account, String notId, String complexity)
        {
            return sd.updateComplexity(account, notId, complexity);
        }
        //更新自定义截止时间的信息
        public BaseResult updateDefDeadLine(String account, String notId, String defDead)
        {
            return sd.updateDefDeadLine(account, notId, defDead);
        }
        //进行作业复杂度的升序排序
        public List<List<List<String>>> upComplexity(String account)
        {
            List<List<List<String>>> results = sd.alertFormInfo(account);
            List<List<String>> result = results[0];
            //按第五项进行升序排序
            result.Sort(delegate (List<String> l1, List<String> l2)
            {
                if (l1.Count > 5 && l2.Count > 5)
                {
                    return l1[5].CompareTo(l2[5]);
                }
                return 0;
            });
            return results;
        }
        //进行作业复杂度的降序排序
        public List<List<List<String>>> downComplexity(String account)
        {
            List<List<List<String>>> results = sd.alertFormInfo(account);
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
        //学生提交作业时需要调用该函数对作业表中的字段进行修改
        public String SubmitHomework(string name, string classId, string account, string postil, string homUrlName, string notId, string localpath)
        {
            //可以直接从前端界面取值，就不需要查库了
            //通过账号来获取到姓名
            //DataTable sdName = sd.GetStuName(account);
            //string name = sdName.Rows[0][0].ToString();
            //System.Windows.MessageBox.Show(name);

            //通过作业公告Id获取作业公告名
            DataTable sdNotName = sd.GetNotName(notId);
            string notName = sdNotName.Rows[0][0].ToString();


            //进行服务器文件夹和文件的上传操作
            //数据库中数据的更新
            //update homework  set postil = @postil, homUrl = @homUrl, homUrlName = @homUrlName , submitTime = @submitTime where notId  = @notId  and  stuId = @stuId;";
            //学生提交作业的时间
            DateTime submitTime = DateTime.Now;
            //查询该真实的学号在数据库中课堂表对应自增主键stuId
            DataTable sdStuId = sd.GetStuIdByAccount(account);
            
            int result;
            if (!int.TryParse(sdStuId.Rows[0][0].ToString(), out result))//table[0][0]就是查到的stuId
            {
                return "account转换为stuId失败";
            }
            string stuId = result.ToString();
            bool flag;
            string errorinfo;

            //根据stuId和notId获取homework表中该作业的作业名，判断是否交过作业
            DataTable sdHomName = sd.GetHomeUrlNameByStuIdAndNotId(stuId, notId);

            String homName = sdHomName.Rows[0][0].ToString();//获得该作业的作业名
            //作业名为空，表示是第一次交作业，需要创建目录并添加文件到服务器
            if (homName == "")
            {
                string dirNameAc = account + name;//课堂真实号/作业公告标题/学生学号+姓名
                string orginPath = classId + "/" + notName;//原始目录或起始目录，即在哪个目录下创建
                flag = FtpUpDown.MakeDir(dirNameAc, out errorinfo, orginPath);//创建目录的静态方法，可以直接通过类名访问
                //创建作业目录 
                if (flag == false)
                {
                    return "在文件服务器中创建对应作业目录失败";
                }
                //上传作业
                string dirFullNotFile = orginPath + "/" + dirNameAc;
                if (localpath != "")
                {
                    flag = FtpUpDown.Upload(localpath, dirFullNotFile);
                    if (!flag)
                    {
                        return "在文件服务器中指定目录上传作业失败";
                    }
                }
            }
            //作业名不为空，表示已经交过一次作业了，不需要创建文件夹，只需要更改文件名并上传至服务器即可
            else
            {
                string dirNameAc = account + name;//课堂真实号/作业公告标题/学生学号+姓名
                string orginPath = classId + "/" + notName;//原始目录或起始目录，即在哪个目录下创建
                string dirFullNotFile = orginPath + "/" + dirNameAc;
                if (localpath != "")
                {
                    DataTable normalName = sd.GetHomeUrlNameByStuIdAndNotId(stuId, notId);  //寻找到当前文件服务器和数据库中存储的作业文件名
                    string Name = normalName.Rows[0][0].ToString(); 
                    string fileFullPath = dirFullNotFile + "/" + Name;  //寻找到在文件服务器中当前作业的路径
                    flag =  FtpUpDown.delFile(fileFullPath, out errorinfo);  //将文件服务器中已有的作业删除
                    if (!flag)
                    {
                        return "修改时，源文件删除失败，请重试";  //表示修改失败
                    }
                    flag = FtpUpDown.Upload(localpath, dirFullNotFile);  //删除原文件之后，将新的文件添加到文件服务器中
                    if (!flag)
                    {
                        return "在文件服务器中指定目录修改作业失败";
                    }
                }
            }
            string homUrl = classId + "/" + notName + "/" + account + name;
            //进行数据库中表的更新
            //调用函数，更新数据库homework表
            flag = sd.UpdateHomework(submitTime, notId, stuId, postil, homUrl, homUrlName);
            if (!flag)
            {
                return "无法将更新homework表";
            }
            return "上传作业成功";
        }

        public string downloadLink(string notId)
        {
            DataTable sdNotName = sd.GetNotName(notId);
            string notName = sdNotName.Rows[0][0].ToString();
            return notName;
        }
        //获得提交作业时间和当天提交的人数
        public Dictionary<String, int> getTimeAndUsers(String classSpecId, String notId)
        {
            List<Dictionary<String, int>> results = sd.getHomeNumAndDate(classSpecId, notId);
            return results[1];
        }
        //获得未完成和已完成的作业人数
        public Dictionary<String, int> getNums(String classSpecId, String notId)
        {
            List<Dictionary<String, int>> results = sd.getHomeNumAndDate(classSpecId, notId);
            return results[0];
        }
        //显示作业信息
        public List<String> showHomeworkInfo(String account, String notId)
        {
            List<String> result = new List<string>();
            DataTable stuIdTable = sd.GetStuIdByAccount(account);
            string stuId = stuIdTable.Rows[0][0].ToString();
            DataTable homInfo = sd.getScoreByNotIdStuId(stuId, notId);
            result.Add(homInfo.Rows[0][0].ToString());//将成绩加入列表中
            result.Add(homInfo.Rows[0][1].ToString());//将评语加入列表中
            return result;

        }
        //显示作业答疑区的信息
        public List<List<String>> showAskQuestion(String notId)
        {
            return sd.showAskQuestion(notId);
        }
        //进行学生答疑的插入
        public bool insertStudentQuestion(String notId,String comment)
        {
            return sd.insertStudentComment(notId, comment);
        }

        public int getSexByStuSpecId(string StuSpecId)
        {
            return int.Parse(sd.getSexByStuSpecId(StuSpecId).Rows[0][0].ToString());
        }

    }
}
