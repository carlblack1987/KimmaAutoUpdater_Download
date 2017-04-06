using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Configuration;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Diagnostics;
using System.Net;
using AutoUpdater_Monitor;
using AutoUpdater_Client.UpdateServices;
using AutoUpdater_Client.Log;
using AutoUpdater_Client.DB;
using AutoUpdater_Client.Model;
using AutoUpdater_Client.JSON;
using AutoUpdater_Client.Schedule;
using AutoUpdater_Client.ZipUnzip;

namespace AutoUpdater_Client
{
    public class UpdateHelper
    {

        public UpdateHelper()
        {
            try
            {
                //读取app.config内的相关配置
                serverUrl = ConfigurationManager.AppSettings["UpdateServerUrl"];
                _StartupPath = ConfigurationManager.AppSettings["DownloadPath"];
                _BuffLength = 1024 * int.Parse(ConfigurationManager.AppSettings["PackageSize"]);
                dbFileName = ConfigurationManager.AppSettings["DBFileName"];
                iVendProcessName = ConfigurationManager.AppSettings["IVendProcessName"];
                _AutoUpdateFlag = ConfigurationManager.AppSettings["IsAutoUpdateActivated"];
                schedule = new ScheduleTask(ConfigurationManager.AppSettings["CheckUpdateCircle"]);
                checkUpdateInterval = int.Parse(ConfigurationManager.AppSettings["CheckUpdateInterval"]);

                dbOper = new DBOperator(dbFileName);
                zipHelper = new ZipHelper();

                //读取sqlite数据库的客户端相关数据
                VMID = dbOper.getSqlResult("SELECT CFGFACTVALUE FROM T_SYS_CONFIG WHERE UPPER(CFGNAME) = 'VMID'", "CFGFACTVALUE");
                UserKey = dbOper.getSqlResult("SELECT CFGFACTVALUE FROM T_SYS_CONFIG WHERE UPPER(CFGNAME) = 'VMSOFTAPIUSERKEY'", "CFGFACTVALUE");

                iVendRootPath = Directory.GetCurrentDirectory() + "/";
                updateRootPath = iVendRootPath + _StartupPath;

                //如果开启了自动检查更新则开始检查更新
                if (_AutoUpdateFlag.Equals("1"))
                {
                    //startUpdateCheck();
                    //启动定时监测更新的后台线程
                    startUpdateCheckThread();
                }
            }
            catch (Exception e)
            {
                Log.LogOperator.AddUpdateLog("ERROR", "Initiate update process failed");
                Log.LogOperator.AddUpdateLog("ERROR", "Reason: " + e.ToString());
            }
        }

        #region 私有属性

        /// <summary>
        /// 客户端的机器号
        /// </summary>
        public string VMID;
        /// <summary>
        /// 客户端的接入密匙
        /// </summary>
        public string UserKey;
        /// <summary>
        /// 客户端窗口对象
        /// </summary>
        public Client_Form form;
        /// <summary>
        /// 主窗口对象
        /// </summary>
        public AutoUpdater_Monitor_Form monitor_form;
        /// <summary>
        /// 服务端url
        /// </summary>
        string serverUrl {get; set;}
        /// <summary>
        /// 每次请求的字节数
        /// </summary>
        int _BuffLength = 1024 * 100;
        /// <summary>
        ///保存下载文件的根目录
        /// </summary>
        string _StartupPath;
        /// <summary>
        /// 当前客户端版本号
        /// </summary>
        public String _CurrentVersion { get; set; }
        /// <summary>
        /// 是否自动更新标识位
        /// </summary>
        string _AutoUpdateFlag { get; set; }
        /// <summary>
        /// sqlite数据库对象
        /// </summary>
        private DBOperator dbOper;
        /// <summary>
        /// sqlite数据库文件名
        /// </summary>
        private string dbFileName;
        /// <summary>
        /// 从服务端取得的更新信息
        /// </summary>
        private UpdateInfoJSON updateInfo;
        /// <summary>
        /// 压缩解压缩文件对象
        /// </summary>
        private ZipHelper zipHelper;
        /// <summary>
        /// 计划任务对象
        /// </summary>
        private ScheduleTask schedule;
        /// <summary>
        /// 当前是否处于更新状态
        /// </summary>
        public bool isInUpdate = false;
        /// <summary>
        /// iVend主进程名称
        /// </summary>
        private string iVendProcessName = "";
        /// <summary>
        /// 关闭iVend进程超时时间
        /// </summary>
        private int iVendShutDownTimeOutLimit = 60;
        /// <summary>
        /// 存放更新的根目录
        /// </summary>
        private string updateRootPath;
        /// <summary>
        /// iVend主程序目录
        /// </summary>
        private string iVendRootPath;
        /// <summary>
        /// 执行更新的随机时间范围
        /// </summary>
        private int checkUpdateInterval = 60;
        /// <summary>
        /// 下载时每秒种内读取到的字节数
        /// </summary>
        private int readByteSec = 0;
        /// <summary>
        /// 负责执行文件下载解压缩的线程
        /// </summary>
        public Thread _DownloadFileThread = null;
        /// <summary>
        /// 计算网络速度线程
        /// </summary>
        public Timer _DownLoadSpeedTimer = null;

        #endregion

        #region 公共方法

        //执行文件下载
        void DownloadFIle()
        {
            if(updateInfo == null)
                return;
            form.progressBarTotal.Value = 0;
            form.progressBarTotal.Maximum = updateInfo.Filelist.Length;
            string info = "/" + this.form.progressBarTotal.Maximum + " 已完成";
            //创建下载目录
            Directory.CreateDirectory(updateRootPath);
            Directory.SetCurrentDirectory(updateRootPath);
            string curPath = Directory.GetCurrentDirectory();
            foreach(FileList file in updateInfo.Filelist) {
                this.form.label5.Text = this.form.progressBarTotal.Value.ToString() + info;
                this.form.label7.Text = file.Name;

                //获取文件的次数
                long rFileCount = file.Size / _BuffLength;
                if (file.Size % _BuffLength != 0)
                {
                    rFileCount++;
                }
                form.progressBarSingle.Value = 0;
                form.progressBarSingle.Maximum = (int)rFileCount;
                
                //开始下载文件
                if (DownloadFileByStream(file.Name, file.Size, file.url, this._BuffLength, rFileCount, file.Localpath)) {
                    this.form.progressBarTotal.Value++;
                    Directory.SetCurrentDirectory(updateRootPath);
                }
                else
                {
                    form.label2.Text = "下载更新内容失败";

                    //关闭更新窗口
                    Thread.Sleep(3000);
                    form.Close();
                    form = null;

                    return;
                }
            }

            //发送关闭iVend客户端的消息
            form.label2.Text = "下载成功，正在关闭iVend客户端";
            form.label9.Text = "";
            if (!this.closeiVendProcessJudge())
            {
                form.label2.Text = "关闭iVend客户端失败，请查看后台日志";

                //关闭更新窗口
                Thread.Sleep(3000);
                form.Close();
                form = null;

                return;
            }

            //开始对文件进行移动以及解压缩
            form.label2.Text = "开始复制更新内容";
            if (CopyAndUncompressFiles())
            {
                form.label2.Text = "更新成功，重启iVend客户端";
                Log.LogOperator.AddUpdateLog("UPDATE", "Version " + updateInfo.Newver + ": update completed");
                Log.LogOperator.AddUpdateLog("UPDATE", "===========================================");
                //更新数据库记录
                dbOper.executeSql("UPDATE T_VERSION_INFO SET ISINUSE = 0");
                dbOper.executeSql("INSERT INTO T_VERSION_INFO VALUES((SELECT MAX(ID) FROM T_VERSION_INFO) + 1, '" 
                + updateInfo.Newver + "', '"
                + _CurrentVersion + "', 100, DATETIME('NOW'), 'SUCCESS', 1)");
            }
            else
            {
                form.label2.Text = "更新失败，请查看后台日志";
                Log.LogOperator.AddUpdateLog("UPDATE", "Version " + updateInfo.Newver + ": update uncompleted");
                Log.LogOperator.AddUpdateLog("UPDATE", "===========================================");
                //更新数据库记录
                dbOper.executeSql("INSERT INTO T_VERSION_INFO VALUES((SELECT MAX(ID) FROM T_VERSION_INFO) + 1, '" 
                + updateInfo.Newver + "', '"
                + _CurrentVersion + "', 100, DATETIME('NOW'), 'FAIL', 0)");

                //对更新内容进行回滚
            }

            //重新启动iVend进程
            iVend_Message.iVend_Message.startProcessByName("iVend");

            //关闭更新窗口
            Thread.Sleep(8000);
            form.Close();
            form = null;
        }

        //移动与复制当前的更新文件
        public bool CopyAndUncompressFiles()
        {
            foreach (FileList file in updateInfo.Filelist)
            {
                if (File.Exists(file.Name))
                {
                    try
                    {
                        Log.LogOperator.AddUpdateLog("UPDATE", "File " + file.Name + ": Move and uncompress initiated");
                        string path = file.Localpath;
                        if (path != "" && !path.EndsWith("/"))
                            path += "/";
                        File.Copy(path + file.Name, "../" + file.Name, true);
                        if (Path.GetExtension(file.Name).ToUpper() == ".ZIP" || Path.GetExtension(file.Name).ToUpper() == ".RAR")
                        {
                            Directory.SetCurrentDirectory(iVendRootPath);
                            zipHelper.UnZip(file.Name, "");
                            File.Delete(file.Name);
                        }
                    }
                    catch (Exception e)
                    {
                        Log.LogOperator.AddUpdateLog("ERROR", "Error occurs during move and copy process");
                        Log.LogOperator.AddUpdateLog("ERROR", e.ToString());
                        return false;
                    }
                }
            }
            return true;
        }

        //下载文件流
        public bool DownloadFileByStream(string strFileName, long fileSize, string url, long buffLength, long buffSize, string path)
        {
            bool flag = false;
            //打开上次下载的文件
            long SPosition = 0;
            //实例化流对象
            FileStream FStream;
            //创建本地文件写入流
            if (path != "" && path != null)
            {
                Directory.CreateDirectory(path);
                Directory.SetCurrentDirectory(path);
            }
            //判断要下载的文件夹是否存在
            if (File.Exists(strFileName))
            {
                //打开要下载的文件
                FStream = File.OpenWrite(strFileName);
                //获取已经下载的长度
                SPosition = FStream.Length;
                //form.showMessage(SPosition.ToString() + " " + fileSize);
                if (SPosition >= fileSize - 1)
                {
                    FStream.Close();
                    return true;
                }
                FStream.Seek(SPosition, SeekOrigin.Current);
                form.progressBarSingle.Value = (int)(SPosition / buffLength);
                form.progressBarSingle.Refresh();
            }
            else
            {
                //文件不保存创建一个文件
                FStream = new FileStream(strFileName, FileMode.Create);
                SPosition = 0;
            }
            try
            {
                //打开网络连接
                HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                if (SPosition == myRequest.ContentLength)
                    return true;
                if (SPosition > 0)
                    myRequest.AddRange(SPosition);             //设置Range值
                //向服务器请求,获得服务器的回应数据流
                Stream myStream = myRequest.GetResponse().GetResponseStream();
                //定义一个字节数据
                byte[] btContent = new byte[buffLength];
                int intSize = 0;
                intSize = myStream.Read(btContent, 0, (int)buffLength);
                //设定一个用来计算网络速度的定时器
                _DownLoadSpeedTimer = new System.Threading.Timer(getNetworkSpeed, null, 0, 1000);
                while (intSize > 0)
                {
                    FStream.Write(btContent, 0, intSize);
                    if (form.progressBarSingle.Value < form.progressBarSingle.Maximum)
                        form.progressBarSingle.Value++;
                    intSize = myStream.Read(btContent, 0, (int)buffLength);
                    readByteSec += intSize;
                }
                _DownLoadSpeedTimer.Dispose();
                //关闭流
                FStream.Close();
                myStream.Close();
                flag = true;
            }
            catch (ThreadAbortException e)
            {
                FStream.Close();
                Log.LogOperator.AddUpdateLog("UPDATE", "Download thread is manually terminated by user");
                flag = false;       //返回false下载失败
            }
            catch (Exception e)
            {
                FStream.Close();
                Log.LogOperator.AddUpdateLog("ERROR", "FILE " + url + ": Error occurs during downloading process");
                Log.LogOperator.AddUpdateLog("ERROR", e.ToString());
                flag = false;       //返回false下载失败
            }
            return flag;
        }

        //计算网络速度
        public void getNetworkSpeed(object readByte)
        {
            double FileSpeed = Math.Round((double)readByteSec / 1024, 2);
            if(FileSpeed > 1024.00)
                form.label9.Text = Math.Round(FileSpeed / 1024, 2) + " MB/s";
            else
                form.label9.Text = FileSpeed + " KB/s";
            readByteSec = 0;
        }

        //获取客户端版本号
        public string getClientVersion()
        {
            string result = "";
            try
            {
                result = dbOper.getSqlResult("SELECT VERSION FROM T_VERSION_INFO WHERE ISINUSE = 1", "VERSION");
                return result;
            }
            catch (Exception e)
            {
                if (e.ToString().Contains("no such table"))
                {
                    dbOper.initiateClientDB();
                    result = dbOper.getSqlResult("SELECT VERSION FROM T_VERSION_INFO WHERE ISINUSE = 1", "VERSION");
                }
            }
            return result;
        }

        //从服务端获取更新信息，格式为JSON
        public string getUpdateInfo()
        {
            string result = string.Empty;
            try
            {
                string postUrl = serverUrl + "?"
                    + "userkey=" + UserKey
                    + "&vmid=" + VMID
                    + "&ver=" + _CurrentVersion;
                //monitor_form.showMessage(postUrl);
                result = Net.NetOperator.getJSONFromNet(postUrl);
                //模拟测试数据
                result = "{ \"ret\": \"0\", \"Isupdate\": \"1\", \"Newver\": \"V1.1.0\", "
                    + "\"Filelist\": [ "
                    //+ "{ \"Name\": \"Debug.zip\", \"Size\": \"56693419\", \"url\": \"http://localhost/AutoUpdateService/UpdateFiles/V1.0.1/Debug.zip\", \"Localpath\": \"\"} ] }";
                    //+ "{ \"Name\": \"Debug.zip\", \"Size\": \"533768959\", \"url\": \"http://192.168.221.11/AutoUpdateService/UpdateFiles/Debug.zip\", \"Localpath\": \"\"} ] }";
                    + "{ \"Name\": \"Debug.zip\", \"Size\": \"119366105\", \"url\": \"http://www.kivend.net/termfile/termupdate.zip\", \"Localpath\": \"\"} ] }";
                //解析从服务端返回的JSON结果
                this.updateInfo = JSONOperator.parase<UpdateInfoJSON>(result);
                result = JSONOperator.stringify(updateInfo);
            }
            catch (WebException e)
            {
                monitor_form.label_ClientStatusContent.Text = "无法连接到指定更新服务器，请查看配置或检查网络";
                Log.LogOperator.AddUpdateLog("ERROR", "Fail to connect to update server");
                Log.LogOperator.AddUpdateLog("ERROR", e.ToString());
                result = "-1";
            }
            catch (Exception e)
            {
                monitor_form.label_ClientStatusContent.Text = "网络连接失败，请查看后台日志";
                Log.LogOperator.AddUpdateLog("ERROR", "Network failure");
                Log.LogOperator.AddUpdateLog("ERROR", e.ToString());
                result = "-1";
            }

            return result;
        }

        //判断是否需要升级
        public bool isUpdateRequired()
        {
            if (this.updateInfo == null)
                return false;
            else if (this.updateInfo.Isupdate == 1)
                return true;
            else
                return false;
        }
                
        //启动检测更新的线程
        public void startUpdateCheckThread()
        {
            Thread t = new Thread(updateCheckBySchedule);
            t.IsBackground = true;
            t.Start();
        }

        //更新计划任务线程
        public void updateCheckBySchedule()
        {
            while (true)
            {
                Thread.Sleep(2000);
                if (schedule.isRuleMatches() && !isInUpdate) {
                    Console.WriteLine("!!!!!!!Matches!!!!!!");
                    //选择一个随机时间进行更新
                    startUpdateRandom();
                }
                else {
                    Console.WriteLine("Not match");
                }
            }
        }

        //选择随机时间进行更新
        public void startUpdateRandom()
        {
            if(checkUpdateInterval <= 0)
                startUpdateCheck();
            else
            {
                Random ran = new Random();
                int interval = ran.Next(1, checkUpdateInterval);
                Console.WriteLine("Random minute: " + interval);
                DateTime dt = DateTime.Now.AddMinutes(interval);
                DateTime dtMax = DateTime.Now.AddMinutes(checkUpdateInterval);
                Log.LogOperator.AddUpdateLog("UPDATE", "Update random time: " + dt.ToString());
                while (true)
                {
                    Thread.Sleep(10000);
                    if (dt.ToShortTimeString() == DateTime.Now.ToShortTimeString())
                    {
                        Log.LogOperator.AddUpdateLog("UPDATE", "Time's up, initiate update");
                        startUpdateCheck();
                        break;
                    }
                    else if (DateTime.Now > dtMax)
                        break;
                }
            }
        }

        //开始检查是否有更新
        public void startUpdateCheck()
        {
            try
            {
                //发送获取更新的请求
                string updateInfo = getUpdateInfo();
                //MessageBox.Show(updateInfo);
                //判断是否需要更新
                if (isUpdateRequired())
                {
                    //MessageBox.Show("Update Required");
                    form = new AutoUpdater_Client.Client_Form(this);
                    form.TopMost = true;
                    form.Show();
                    isInUpdate = true;
                }
                else
                {
                    //MessageBox.Show("Update Not");
                }
            }
            catch (Exception e)
            {
                Log.LogOperator.AddUpdateLog("ERROR", "Check update failed");
                Log.LogOperator.AddUpdateLog("ERROR", "Reason: " + e.ToString());
            }
        }

        //关闭iVend进程
        public void closeiVendProcessTrd()
        {
            Thread t = new Thread(closeiVendProcess);
            t.IsBackground = true;
            t.Start();
        }

        //发送关闭售货机主进程消息函数
        public void closeiVendProcess()
        {
            DateTime startTime = DateTime.Now;
            while (true)
            {
                if (isOperationTimeOut(startTime, iVendShutDownTimeOutLimit))
                {
                    Log.LogOperator.AddUpdateLog("UPDATE", "The shuting down of iVend process exceeds time limit");
                    break;
                }
                Process[] p = Process.GetProcessesByName(iVendProcessName);
                int hWnd = p.Length;
                if (hWnd == 0)
                {
                    Console.WriteLine("Can not find iVend process");
                    Log.LogOperator.AddUpdateLog("UPDATE", "No iVend process found");
                    break;
                }
                else
                {
                    Console.WriteLine("iVend process found");
                    Log.LogOperator.AddUpdateLog("UPDATE", "iVend process found, sending shuting down command");
                    //向iVend进程发送关闭指令消息
                    Thread t = new Thread(new ParameterizedThreadStart(iVend_Message.iVend_Message.SendData));
                    t.IsBackground = true;
                    t.Start(iVend_Message.iVend_Message.WM_CLOSE.ToString());
                }
                Thread.Sleep(5000);
            }
        }

        //发送关闭售货机主进程消息函数
        public bool closeiVendProcessJudge()
        {
            DateTime startTime = DateTime.Now;
            while (true)
            {
                if (isOperationTimeOut(startTime, iVendShutDownTimeOutLimit))
                {
                    Log.LogOperator.AddUpdateLog("UPDATE", "The shuting down of iVend process exceeds time limit");
                    return false;
                }
                Process[] p = Process.GetProcessesByName(iVendProcessName);
                int hWnd = p.Length;
                if (hWnd == 0)
                {
                    Console.WriteLine("Can not find iVend process");
                    Log.LogOperator.AddUpdateLog("UPDATE", "No iVend process found");
                    return true;
                }
                else
                {
                    Console.WriteLine("iVend process found");
                    Log.LogOperator.AddUpdateLog("UPDATE", "iVend process found, sending shuting down command");
                    //向iVend进程发送关闭指令消息
                    Thread t = new Thread(new ParameterizedThreadStart(iVend_Message.iVend_Message.SendData));
                    t.IsBackground = true;
                    t.Start(iVend_Message.iVend_Message.WM_CLOSE.ToString());
                    //iVend_Message.iVend_Message.SendData(iVend_Message.iVend_Message.WM_CLOSE.ToString());
                }
                Thread.Sleep(5000);
            }
        }

        //判断相关操作是否已经超时，单位为秒
        public bool isOperationTimeOut(DateTime startTime, int secondLimit)
        {
            return (int)(DateTime.Now - startTime).TotalSeconds > secondLimit ? true : false;
        }

        //开始下载更新内容
        public void startDownloadContent()
        {
            form.label2.Text = "正在下载更新包";
            _DownloadFileThread = new Thread(this.DownloadFIle);
            _DownloadFileThread.Start();
            Log.LogOperator.AddUpdateLog("UPDATE", "===========================================");
            Log.LogOperator.AddUpdateLog("UPDATE", "Update initiated");
            Log.LogOperator.AddUpdateLog("UPDATE", "Download thread initiated");
        }

        //检查指定进程是否存在
        public bool isProcessExists(string processName)
        {
            try
            {
                Process[] p = Process.GetProcessesByName(processName);
                int hWnd = p.Length;
                if (hWnd == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        } 

        #endregion
    }

}
