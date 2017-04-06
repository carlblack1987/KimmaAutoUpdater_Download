using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Configuration;
using WebApplication1.Models;

namespace WebApplication1
{
    /// <summary>
    /// Summary description for UpdateServer
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [Serializable]
    public class UpdateServer : System.Web.Services.WebService
    {
        public UpdateServer()
        {
            _StartupPath = ConfigurationManager.AppSettings["DownloadPath"];
            Directory.SetCurrentDirectory(_StartupPath);
        }

        #region 私有属性

        /// <summary>
        ///保存下载文件的根目录
        /// </summary>
        string _StartupPath = "";

        #endregion

        /// <summary>
        /// 获得更新目标版本的序号
        /// </summary>
        [WebMethod]
        public VersionModel GetTargetVersionSeq(int versionSeq)
        {
            VersionModel vm = new VersionModel();
            vm.versionID = 1;
            int targetSeq = 110;
            return vm;
        }

        /// <summary>
        /// 获得给定版本的文件列表
        /// </summary>
        [WebMethod]
        public List<FileModel> GetFileList(int versionSeq)
        {
            List<FileModel> list = new List<FileModel>();
            string dirName = "V1.0.1";
            DirectoryInfo dir = new DirectoryInfo(dirName);
            int fileid = 1;
            foreach (FileInfo file in dir.GetFiles("*", SearchOption.AllDirectories))
            {
                Console.WriteLine(file.FullName);
                list.Add(new FileModel()
                {
                    FileId = fileid++,
                    FileName = file.Name,
                    FileVersion = "V1.0.1",
                    FileLength = file.Length,
                    FileLastTime = file.LastWriteTime,
                    RelativePath = file.DirectoryName.Substring(file.DirectoryName.IndexOf(dirName))
                });
            }
            return list;
        }

        /// <summary>
        /// 获得文件流
        /// </summary>
        [WebMethod]
        public byte[] GetFile(RequestFileModel rfm)
        {
            byte[] fileArr = null;
            //DBHelper daObj = new DBHelper();
            //TFileInfo r = daObj.GetUpdateFileById(rfm.FileId); // FileList.Find(s => s.FileId == rfm.FileId);
            string path = rfm.RelativePath;
            if (!string.IsNullOrEmpty(path) && path.Substring(path.Length - 1) != "\\")
            {
                path += "\\";
            }
            fileArr = GetFileStream(path + rfm.FileName, rfm.StartPosition, rfm.ReadFileLength);
            return fileArr;
        }

        /// <summary>
        /// 获得文件流
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="startPosition">文件的开始位置</param>
        /// <param name="readLength">读取文件的字节数</param>
        /// <returns>返回文件流</returns>
        byte[] GetFileStream(string fileName, int startPosition, int readLength)
        {
            byte[] fileStream = null;
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                if (stream.Length < startPosition + readLength)
                {
                    readLength = (int)(stream.Length - startPosition);
                }
                fileStream = new byte[readLength];
                stream.Position = startPosition;
                stream.Read(fileStream, 0, readLength);
                return fileStream;
            }
        }

        /// <summary>
        /// 判断服务器上当前路径是否存在
        /// </summary>
        [WebMethod]
        public bool isDirectoryExists(string dir)
        {
            return Directory.Exists(dir);
        }

        /// <summary>
        /// 判断本次更新所需要更新的文件数量
        /// </summary>
        [WebMethod]
        public int getUpdateFileNum(int startSeq, int endSeq)
        {
            string ver = "V1.0.1";
            int result = 0;
            DirectoryInfo dir = new DirectoryInfo(ver);
            result += dir.GetFiles("*", SearchOption.AllDirectories).Length;
            return result;
        }

        /// <summary>
        /// 获取版本号对应的路径对象
        /// </summary>
        //[WebMethod]
        //public DirectoryInfo getDirectoryInfo(string dir = "")
        //{
        //    return new DirectoryInfo(dir);
        //}
    }
}
