using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Security;
using System.IO;

namespace AutoUpdater_Client.Net
{
    class NetOperator
    {
        public static string getJSONFromNet(string url)
        {
            string result = string.Empty;
            //创建一个HTTP请求  
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //Post请求方式  
            request.Method = "GET";
            //内容类型
            request.ContentType = "application/x-www-form-urlencoded";
            //相应内容
            HttpWebResponse webResponse = null;

            try
            {
                //尝试获得要请求的URL的返回消息
                webResponse = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException)
            {
                //发生网络错误时,获取错误响应信息
                throw;
            }
            catch (Exception)
            {
                //发生异常时把错误信息当作错误信息返回
                throw;
            }
            finally
            {
                if (webResponse != null)
                {
                    //获得网络响应流
                    using (StreamReader responseReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                    {
                        result = responseReader.ReadToEnd();//获得返回流中的内容
                    }
                    webResponse.Close();//关闭web响应流
                }
            }
            return result;
        }


        public static string postJSONToNet(string url, string para)
        {
            string result = string.Empty;
            //创建一个HTTP请求  
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //Post请求方式  
            request.Method = "POST";
            //内容类型
            request.ContentType = "application/x-www-form-urlencoded";
            //相应内容
            HttpWebResponse webResponse = null;
            byte[] postBytes = Encoding.UTF8.GetBytes(para);
            request.ContentType = "application/x-www-form-urlencoded;";
            request.ContentLength = postBytes.Length;

            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(postBytes, 0, postBytes.Length);
            }
            try
            {
                //尝试获得要请求的URL的返回消息
                webResponse = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException)
            {
                //发生网络错误时,获取错误响应信息
                throw;
            }
            catch (Exception)
            {
                //出错后直接抛出
                throw;
            }
            finally
            {
                if (webResponse != null)
                {
                    //获得网络响应流
                    using (StreamReader responseReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                    {
                        result = responseReader.ReadToEnd();//获得返回流中的内容
                    }
                    webResponse.Close();//关闭web响应流
                }
            }
            return result;
        }
    }
}
