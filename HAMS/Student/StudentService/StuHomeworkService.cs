﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Student.StudentService
{
    //class StuHomeworkService
    //{
    //    private AnnounceNoticeDao annNotDao = new AnnounceNoticeDao();

    //    public String announceNotice(DateTime truDeadline, String content, String notTitle, String classSpecId, String teacherSpecId, String notURL = "")
    //    {
    //        Notice notice = new Notice();
    //        notice.TruDeadLine = truDeadline;
    //        notice.Content = content;
    //        notice.NoteURL = notURL;

    //        //查询该真实的课堂号在数据库中课堂表对应自增主键ClassId
    //        DataTable tbClassId = annNotDao.getClassId(classSpecId);

    //        int result;
    //        if (!int.TryParse(tbClassId.Rows[0][0].ToString(), out result))//table[0][0]就是查到的classId
    //        {
    //            return "classSpecId转换为classId失败";

    //        }
    //        notice.ClassId = result;

    //        //根据ClassId获取notice表中所有作业公告标题，比对是否重复
    //        DataTable tbNoteTitles = annNotDao.getNoteTitle(notice.ClassId);

    //        int count = tbNoteTitles.Rows.Count;//获得该课堂号所发布的所有作业公告的标题
    //        for (int x = 0; x < count; x++)//判断是否和已布置的作业公告标题存在重复
    //        {
    //            string tbNoteTitle = tbNoteTitles.Rows[x][0].ToString();//获取该课堂某次作业公告的标题
    //            if (notTitle == tbNoteTitle)
    //            {
    //                return "此次布置的作业公告标题和该堂课之前的作业公告标题重复";
    //            }
    //        }
    //        notice.NoteTitle = notTitle;//说明没有重复，该作业公告标题合法

    //        /*
    //        //上传作业公告附件
    //        if (notURL != "")//存在作业公告附件，根据路径插入FTP服务器中
    //        {
                
    //        }
    //        */

    //        //调用插入作业公告函数，将公告插入数据库notice表
    //        bool flag = annNotDao.insertNotice(notice);
    //        if (!flag)
    //        {
    //            return "无法将新增的作业公告插入到notice表";
    //        }

    //        //查询该教师工号在数据库中教师表对应的教师自增主键teacherId
    //        DataTable tbTeacherId = annNotDao.getTeacherId(teacherSpecId);//tbTeacherSpecId.Text是教师工号
    //        if (!int.TryParse(tbTeacherId.Rows[0][0].ToString(), out result))//table[0][0]就是查到的classId
    //        {
    //            return "classSpecId转换为classId失败";

    //        }
    //        int teacherId = result;
    //        //调用学生角色的业务层添加作业函数，该函数负责调用Dao层将作业插入数据库homework表
    //        //[studentDao文件夹下某Dao文件的一个对象].insertHomework(classId,teacherId,notId);
    //        //该函数还需要根据classId，获得每个选课学生的stuId，然后依次在作业表中根据(stuId,classId,teacherId,notId)进行插入
    //        return "发布公告成功";
    //    }
    //}
}