using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Server
{
    public class HttpDO
    {
        public string api;

        public RspDO rspDo = new RspDO();
        public ReqDO reqDo = new ReqDO();
    }

    public class RspDO
    {
        public int code = 0; //0成功, 非0失败
        public string msg = "SUCCESS";

        public string content;

        public void fail() { 
            code = -1;
            msg = "FAIL";
        }
    }

    public class ReqDO
    {
        public string content;
        public NameValueCollection queryParams;
    }

   
}
