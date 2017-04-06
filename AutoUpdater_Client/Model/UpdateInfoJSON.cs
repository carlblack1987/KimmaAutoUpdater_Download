using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AutoUpdater_Client.Model
{
    //从更新服务器返回的更新信息
    [DataContract]
    class UpdateInfoJSON
    {
        //返回值，为0表示获取信息成功
        [DataMember(Order = 0)]
        public int ret { get; set; }
        //是否需要更新，0表示不需要，1表示需要
        [DataMember(Order = 1)]
        public int Isupdate { get; set; }
        //最新版本号
        [DataMember(Order = 2)]
        public string Newver { get; set; }
        //升级内容说明
        [DataMember(Order = 3)]
        public string Content { get; set; }
        //升级文件总大小，以bps为单位
        [DataMember(Order = 4)]
        public long Totalsize { get; set; }
        //需要下载的文件总数量
        [DataMember(Order = 5)]
        public int Filecount { get; set; }
        //需要下载的文件列表
        [DataMember(Order = 6)]
        public FileList [] Filelist { get; set; }
    }

    //Filelist所指向的文件信息
    [DataContract]
    class FileList
    {
        //文件名称
        [DataMember(Order = 0)]
        public string Name { get; set; }
        //文件大小
        [DataMember(Order = 1)]
        public long Size { get; set; }
        //文件下载URL地址
        [DataMember(Order = 2)]
        public string url { get; set; }
        //文件存储路径【在终端上的存储路径】
        [DataMember(Order = 3)]
        public string Localpath { get; set; }
    }
}
