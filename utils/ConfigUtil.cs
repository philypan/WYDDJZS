using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace utils
{
    public class ConfigUtil
    {
        /*
        * 1)private 不是必需的,根据设计了,public也可以.
        * 2)extern 关键字表示该方法是要调用非托管代码.如果使用extern关键字来引入非托管代码,则必须也同时使用static.为什么要用static,是因为你调用非托管代码,总得有个入口点吧,那么你声明的这个GetPrivateProfileString方法就是你要调用的非托管代码的入口.想想Main函数,是不是也必须是static呢.
        * 3) 为什么要用long，我看也有小伙伴也有用int的，估计是long支持的更多位数
        */

        [DllImport("kernel32")]// 读配置文件方法的6个参数：所在的分区（section）、 键值、     初始缺省值、   StringBuilder、  参数长度上限 、配置文件路径
        public static extern long GetPrivateProfileString(string section, string key, string defaultValue, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]//写入配置文件方法的4个参数：  所在的分区（section）、  键值、     参数值、       配置文件路径
        private static extern long WritePrivateProfileString(string section, string key, string value, string filePath);


        // ▼ 获取当前程序启动目录
        public static string strPath = AppDomain.CurrentDomain.BaseDirectory + @"/config.ini";

        /*读配置文件*/
        public static string GetValue(string section, string key)
        {
            if (File.Exists(strPath))  //检查是否有配置文件，并且配置文件内是否有相关数据。
            {
                StringBuilder sb = new StringBuilder(255);
                GetPrivateProfileString(section, key, null, sb, 255, strPath);

                return sb.ToString();
            }
            else
            {
                return null;
            }
        }
        // 获取的字符长度最大1000
        public static string GetValue1000(string section, string key)
        {
            if (File.Exists(strPath))  //检查是否有配置文件，并且配置文件内是否有相关数据。
            {
                StringBuilder sb = new StringBuilder(1000);
                GetPrivateProfileString(section, key, null, sb, 1000, strPath);

                return sb.ToString();
            }
            else
            {
                return null;
            }
        }

        /*写配置文件*/
        public static void SetValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, strPath);
        }

        public const string DEFAULT_SECTION = "config";
        public static string GetString(string key, string section = DEFAULT_SECTION)
        {
            return GetValue(section, key);
        }
        
        public static void SetString(string key, string value, string section = DEFAULT_SECTION)
        {
            SetValue(section, key, value);
        }

        public static int GetInt(string key, int defaultValue = 0, string section = DEFAULT_SECTION)
        {
            string value = GetValue(section, key);
            try
            {
                return int.Parse(value);
            }
            catch (Exception)
            {

                return defaultValue;
            }

        }

        public static void SetInt(string key, int value, string section = DEFAULT_SECTION)
        {
            SetValue(section, key, value.ToString());
        }

        public static long GetLong(string key, long defaultValue = 0, string section = DEFAULT_SECTION)
        {
            string value = GetValue(section, key);
            try
            {
                return long.Parse(value);
            }
            catch (Exception)
            {

                return defaultValue;
            }

        }

        public static void SetLong(string key, long value, string section = DEFAULT_SECTION)
        {
            SetValue(section, key, value.ToString());
        }

        public static bool GetBool(string key, bool defaultValue = false, string section = DEFAULT_SECTION)
        {
            string value = GetValue(section, key);
            try
            {
                return bool.Parse(value);
            }
            catch (Exception)
            {

                return defaultValue;
            }

        }

        public static void SetBool(string key, bool value, string section = DEFAULT_SECTION)
        {
            SetValue(section, key, value.ToString());
        }

        /// <summary>
        /// string 数组不知道怎么存好, base64编码下再存吧
        /// </summary>
        /// <param name="key"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public static List<string> GetStrings(string key, string section = DEFAULT_SECTION)
        {
            string value = GetValue(section, key);

            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    //先转base64, 再转json arry, 返回list数组
                    byte[] bytes = Convert.FromBase64String(value);
                    string jsoStr = Encoding.UTF8.GetString(bytes);
                    return JsonUtil.fromJson<List<string>>(jsoStr);
                }
                catch (Exception e)
                {
                    string s = e.ToString();
                }
            }

            return null;
        }

        public static List<string> GetBase64Strings(string key, string section = DEFAULT_SECTION)
        {
            string value = GetValue1000(section, key);

            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    //先转base64, 再转json arry, 返回list数组
                    byte[] bytes = Convert.FromBase64String(value);
                    string jsoStr = Encoding.UTF8.GetString(bytes);
                    return JsonUtil.fromJson<List<string>>(jsoStr);
                }
                catch (Exception e)
                {
                    string s = e.ToString();
                }
            }

            return null;
        }

        public static void SetStrings(string key, List<string> value, string section = DEFAULT_SECTION)
        {
            if (value == null)
            {
                //如果是空, 就是需要清除设置, 放个空list, 不测试null情况了
                value = new List<string>();
            }

            try
            {
                //先转json string, 再base64保存
                string jsoArr = JsonUtil.toJson(value);
                byte[] bytes = Encoding.UTF8.GetBytes(jsoArr);
                string saveString = Convert.ToBase64String(bytes);
                SetValue(section, key, saveString);
            }
            catch (Exception)
            {
            }
        }
    }
}
