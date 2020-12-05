using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.ToolClass
{
    class BaseResult
    {
        // 响应业务状态,为0表示正常，为1表示错误
        public int code { set; get; }
        // 响应消息
        public String msg { set; get; }
      


        // 响应中的数据
        public Object data { set; get; }



        public static BaseResult build(int code, String msg, Object data)
        {

            return new BaseResult(code, msg, data);
        }


        public static BaseResult ok(Object data)
        {
            return new BaseResult(data);
        }
        public static BaseResult ok()
        {
            return new BaseResult(0, null, null);
        }
        public static BaseResult errorMsg(String msg)
        {
            return new BaseResult(1, msg, null);
        }
        public static BaseResult errorMsg(String msg, Object data)
        {
            return new BaseResult(1, msg, data);
        }





        public BaseResult()
        {

        }
        public BaseResult(int code, String msg, Object data)
        {
            
            this.code = code;
            this.msg = msg;
            this.data = data;
           

        }
        public BaseResult(String msg)
        {
           
            this.code = 1;
            this.msg = msg;
           
        }



        public BaseResult(Object data)
        {
           
            this.code = 0;
            this.msg = "OK";
            this.data = data;
          
        }





       

        //public void setCode(int code)
        //{
        //    this.code = code;
        //}
        //public String getMsg()
        //{
        //    return msg;
        //}

        //public void setMsg(String msg)
        //{
        //    this.msg = msg;
        //}

        //public Object getData()
        //{
        //    return data;
        //}

        //public void setData(Object data)
        //{
        //    this.data = data;
        //}

       
    }
}
