using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class RequestFileModel
    {
        /// <summary>
        /// 文件id
        /// </summary>
        public long FileId { set; get; }
        /// <summary>
        /// 开始该文件位置
        /// </summary>
        public int StartPosition { set; get; }
        /// <summary>
        /// 读取文件流的长度(一次读取多少字节)
        /// </summary>
        public int ReadFileLength { set; get; }
        /// <summary>
        /// 相对路径
        /// </summary>
        public string RelativePath { set; get; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { set; get; }
    }
}