using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class FileModel
    {
        /// <summary>
        /// 更新的文件Id,请求文件时用该Id
        /// </summary>
        public long FileId { set; get; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { set; get; }
        /// <summary>
        /// 相对文件路径
        /// </summary>
        public string RelativePath { set; get; }
        /// <summary>
        /// 文件版本(例如:1.1.0)
        /// </summary>
        public string FileVersion { set; get; }
        /// <summary>
        /// 版本序号，1.1.0对应序号为110
        /// </summary>
        public string FileVersionSeq { set; get; }
        /// <summary>
        /// 文件字节长度
        /// </summary>
        public long FileLength { set; get; }
        /// <summary>
        /// 文件最后更新时间
        /// </summary>
        public DateTime FileLastTime { set; get; }
    }
}