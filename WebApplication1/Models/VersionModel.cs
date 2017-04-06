using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class VersionModel
    {
        /// <summary>
        /// 版本编号
        /// </summary>
        public int versionID { get; set; }
        /// <summary>
        /// 版本名称
        /// </summary>
        public string versionName { get; set; }
        /// <summary>
        /// 版本序号
        /// </summary>
        public string versionSeq { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime releaseTime { get; set; }
        /// <summary>
        /// 版本状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 上一版本名称
        /// </summary>
        public string previousVersionName { get; set; }
    }
}