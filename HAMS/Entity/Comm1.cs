﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Entity
{
    class Comm1
    {
        //一级评论的id
        public int Comm1Id { private set; get; }
        //作业公告的id
        public int NotId { private set; get; }
        //一级评论的具体内容
        public String Comment1 { set; get; }
    }
}
