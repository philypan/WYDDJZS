
using System.Net;
using System.Text;

namespace Net.Server
{

    public class HttpServer
    {
        private const string CONTENTTYPE = "application/json";

        private HttpListener listener;
        private Dictionary<string, Action<HttpDO>> actionDict = new Dictionary<string, Action<HttpDO>>();

        //private LogUtil log = LogUtil.Creator(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Logs"), "http_server");
        public void AddPrefixes(string url, Action<HttpDO> action)
        {
            if (!url.StartsWith("/"))
            {
                url = "/" + url;
            }
            actionDict.Add(url, action);
        }

        //开始监听
        public void Start(int port)
        {
            if (listener != null)
            {
                destory();
            }

            if (!HttpListener.IsSupported)
            {
               // log.log("无法在当前系统上运行服务(Windows XP SP2 or Server 2003)。" + DateTime.Now.ToString());
                return;
            }

            if (actionDict.Count <= 0)
            {
               // log.log("没有监听端口");
            }

            listener = new HttpListener();

            foreach (var item in actionDict)
            {
                string path = item.Key;
                if (!path.EndsWith("/"))
                {
                    path = path + "/";
                }
                var url = string.Format("http://+:{0}{1}", port, path);//http：//*：8080/ 和 http://+:8080
                //log.log(url);
                listener.Prefixes.Add(url);  //监听的是以item.Key + "/"+XXX接口

            }

            listener.Start();
            listener.BeginGetContext(Result, null);

           // log.log("开始监听");
            //System.Console.Read();
        }

        public void destory()
        {
            try { 
                if (listener != null)
                {
                    /*listener.Abort();
                    listener.Close();*/
                    listener.Stop();

                    listener = null;
                }
            }
            catch {
            }
        }

        private void Result(IAsyncResult asy)
        {
            if (listener == null)
            {
                return;
            }

            //abort， close后还是会被回调， 会出现各种错误， 不知道咋处理， try catch下
            try
            {
                var context = listener.EndGetContext(asy);
                var req = context.Request;
                var rsp = context.Response;

                // 
                rsp.StatusCode = 200;
                rsp.ContentType = $"{CONTENTTYPE};charset=UTF-8";
                rsp.AddHeader("Content-type", CONTENTTYPE);
                rsp.ContentEncoding = Encoding.UTF8;

               // log.log($"处理请求:{req.RawUrl}");

                HttpDO httpDo = new HttpDO();
                httpDo.api = req.RawUrl;
                //对接口所传数据处理
                HandleHttpMethod(context, httpDo);
                //对接口处理
                HandlerReq(httpDo);

            
                try
                {
                    using (var stream = rsp.OutputStream)
                    {
                        ///获取数据，要返回给接口的
                        string data = httpDo.rspDo.content;//utils.JsonUtil.toJson(httpDo.rspDo);
                        if (string.IsNullOrEmpty(httpDo.rspDo.content))
                        {
                            data = "100 100 100 100 100 100 99";
                        }
                        byte[] dataByte = Encoding.UTF8.GetBytes(data);
                        stream.Write(dataByte, 0, data.Length);
                    }
                }
                catch (Exception e)
                {
                    //log.log(e.Message);

                }finally { 
                    rsp.Close(); 
                }

                listener.BeginGetContext(Result, null);
            }
            catch (Exception e)
            {

               // log.log(e.Message);
            }
        }

        /// <summary>
        /// 对客户端来的url处理
        /// </summary>
        /// <param name="url"></param>
        private void HandlerReq( HttpDO httpDo)
        {
            try
            {
                string path = httpDo.api;
                if (path.EndsWith("/"))
                {
                    path = httpDo.api.Substring(0, httpDo.api.Length - 1);
                }
                var action = actionDict[path];
                action(httpDo);
            }
            catch (Exception e)
            {
                //log.log(e.Message);
            }
        }
        //处理接口所传数据 Post和Get
        private void HandleHttpMethod(HttpListenerContext context, HttpDO httpDo)
        {
            //if (context.Request.ContentType != CONTENTTYPE)
            //{
            //    log.log("参数格式错误");
            //    return;
            //}
            switch (context.Request.HttpMethod)
            {
                case "GET":
                case "POST":
                    using (Stream stream = context.Request.InputStream)
                    {
                        StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                        httpDo.reqDo.content = reader.ReadToEnd();
                    }

                    httpDo.reqDo.queryParams = context.Request.QueryString;
                    break;

                    default: 
                    break;
            }
        }

    }

}