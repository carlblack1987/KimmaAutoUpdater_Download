using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace AutoUpdater_Client.JSON
{
    public static class JSONOperator
    {
        //将JSON数据转化为对应的类型
        public static T parase<T>(string JSONStr)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(JSONStr)))
            {
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
            }
        }

        //将对应的类型转化为JSON字符串
        public static string stringify(object jsonObject)
        {
            using (var ms = new MemoryStream())
            {
                new DataContractJsonSerializer(jsonObject.GetType()).WriteObject(ms, jsonObject);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}
