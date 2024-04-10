using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Net
{
    /// <summary>
    /// http协议访问方法封装
    /// </summary>
    public class HttpUtil
    {

        //https请求规避验证
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受
        }

        private static HttpWebRequest createHttpRequest(string url)
        {
            HttpWebRequest request = null;
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version11;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }

            return request;
        }

        /// <summary>
        /// 指定Url地址使用Get 方式获取全部字符串
        /// </summary>
        /// <param name="url">请求链接地址</param>
        /// <returns></returns>
        public static string Get(string url)
        {
            string result = "";
            Stream stream = null;

            try
            {
                HttpWebRequest req = createHttpRequest(url);
                req.Timeout = 3000; // 设置超时时间（毫秒）
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                stream = resp.GetResponseStream();

                //获取内容
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (stream != null) { 
                    stream.Close();
                }
            }

            return result;
        }
        /// <summary>
        /// 发送Get请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="dic">请求参数定义</param>
        /// <returns></returns>
        public static string Get(string url, Dictionary<string, string> dic)
        {
            string result = "";
            Stream stream = null;

            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(url);
                if (dic.Count > 0)
                {
                    builder.Append("?");
                    int i = 0;
                    foreach (var item in dic)
                    {
                        if (i > 0)
                            builder.Append("&");
                        builder.AppendFormat("{0}={1}", item.Key, item.Value);
                        i++;
                    }
                }
                HttpWebRequest req = createHttpRequest(builder.ToString());
                //添加参数
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                stream = resp.GetResponseStream();
                
                //获取内容
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
                
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            return result;
        }


        #region Post提交
        /// <summary>
        /// 指定Post地址使用Get 方式获取全部字符串
        /// </summary>
        /// <param name="url">请求后台地址</param>
        /// <returns></returns>
        public static string Post(string url)
        {
            string result = "";
            Stream stream = null;

            try
            {
                HttpWebRequest req = createHttpRequest(url);
                req.Method = "POST";
                req.Timeout = 3000; // 设置超时时间（毫秒）
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                stream = resp.GetResponseStream();
                //获取内容
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            return result;
        }
        /// <summary>
        /// 指定Post地址使用Get 方式获取全部字符串
        /// </summary>
        /// <param name="url">请求后台地址</param>
        /// <param name="content">Post提交数据内容(utf-8编码的)</param>
        /// <returns></returns>
        public static string Post(string url, string content)
        {
            string result = "";
            Stream stream = null;

            try
            {
                HttpWebRequest req = createHttpRequest(url);
                req.Method = "POST";
                //req.ContentType = "application/x-www-form-urlencoded";
                req.ContentType = "application/json";

                #region 添加Post 参数
                byte[] data = Encoding.UTF8.GetBytes(content);
                req.ContentLength = data.Length;
                req.Timeout = 3000; // 设置超时时间（毫秒）
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }
                #endregion

                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                stream = resp.GetResponseStream();
                //获取响应内容
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            return result;
        }
        /// <summary>
        /// 指定Post地址使用Get 方式获取全部字符串
        /// </summary>
        /// <param name="url">请求后台地址</param>
        /// <returns></returns>
        public static string Post(string url, Dictionary<string, string> dic)
        {
            string result = "";
            Stream stream = null;

            try
            {
                HttpWebRequest req = createHttpRequest(url);
                req.Method = "POST";
                req.ContentType = "application/json";
                //req.ContentType = "application/x-www-form-urlencoded";

                #region 添加Post 参数
                StringBuilder builder = new StringBuilder();
                int i = 0;
                foreach (var item in dic)
                {
                    if (i > 0)
                        builder.Append("&");
                    builder.AppendFormat("{0}={1}", item.Key, item.Value);
                    i++;
                }
                byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
                req.ContentLength = data.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }
                #endregion

                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                stream = resp.GetResponseStream();
                //获取响应内容
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            return result;
        }
        #endregion

    }
}
