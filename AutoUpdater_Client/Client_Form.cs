using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using AutoUpdater_Client.DB;

namespace AutoUpdater_Client
{
    public partial class Client_Form : Form
    {
        /// <summary>
        /// 当前客户端版本号
        /// </summary>
        private string currentVersion;
        /// <summary>
        /// 接口层对象
        /// </summary>
        UpdateHelper updateHelper;

        #region 控件缩放

        double formWidth;//窗体原始宽度
         
        double formHeight;//窗体原始高度

        double scaleX;//水平缩放比例

        double scaleY;//垂直缩放比例

        Dictionary<string, string> ControlsInfo = new Dictionary<string, string>();//控件中心Left,Top,控件Width,控件Height,控件字体Size

        #endregion

        public Client_Form(UpdateHelper updateHelper)
        {
            InitializeComponent();

            //自适应调整窗口尺寸以遮挡住整个桌面
            //MessageBox.Show(Screen.PrimaryScreen.Bounds.Width + " " + Screen.PrimaryScreen.Bounds.Height);
            GetAllInitInfo();
            autoAdjustFormSize();

            Control.CheckForIllegalCrossThreadCalls = false;
            this.updateHelper = updateHelper;
            this.updateHelper.form = this;
            this.label3.Text = this.updateHelper._CurrentVersion;

            //启动下载线程
            updateHelper.startDownloadContent();
            updateHelper.isInUpdate = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public void showMessage(string str)
        {
            MessageBox.Show(str);
        }

        private void Client_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (updateHelper.isInUpdate == true)
            {
                e.Cancel = true;
                updateHelper._DownloadFileThread.Suspend();
                if (MessageBox.Show("目前正在进行更新，是否退出?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    if (updateHelper._DownloadFileThread != null)
                    {
                        updateHelper._DownloadFileThread.Resume();
                        try
                        {
                            updateHelper._DownloadFileThread.Abort();
                            updateHelper._DownLoadSpeedTimer.Dispose();
                            updateHelper.isInUpdate = false;
                            Dispose();
                        }
                        catch (Exception ex)
                        {
                            updateHelper.isInUpdate = false;
                        }
                    }
                }
                else
                {
                    updateHelper._DownloadFileThread.Resume();
                }
            }
        }

        //自动调整控件使得自适应屏幕大小
        private void autoAdjustFormSize()
        {
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            scaleX = (double) screenWidth / formWidth;
            scaleY = (double) screenHeight / formHeight;
            double[] pos = new double[5];//pos数组保存当前控件中心Left,Top,控件Width,控件Height,控件字体Size
            foreach (Control item in this.Controls)
            {
                string[] strs = ControlsInfo[item.Name].Split(',');//从字典中查出的数据，以‘，’分割成字符串组

                for (int i = 0; i < 5; i++)
                {
                    pos[i] = Convert.ToDouble(strs[i]);//添加到临时数组
                }
                double itemWidth = pos[2] * scaleX;     //计算控件宽度，double类型
                double itemHeight = pos[3] * scaleY;    //计算控件高度
                item.Left = Convert.ToInt32(pos[0] * scaleX - itemWidth / 2);//计算控件距离左边距离
                item.Top = Convert.ToInt32(pos[1] * scaleY - itemHeight / 2);//计算控件距离顶部距离
                item.Width = Convert.ToInt32(itemWidth);//控件宽度，int类型
                item.Height = Convert.ToInt32(itemHeight);//控件高度
                item.Font = new Font(item.Font.Name, float.Parse((pos[4] * Math.Min(scaleX, scaleY)).ToString()));//字体
            }
        }

        //将控件信息加入到字典
        protected void GetAllInitInfo()
        {
            formWidth = this.Width;
            formHeight = this.Height;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            foreach (Control item in this.Controls)
            {
                if (item.Name.Trim() != "")
                {
                    //添加信息：键值：控件名，内容：据左边距离，距顶部距离，控件宽度，控件高度，控件字体。
                    ControlsInfo.Add(item.Name, (item.Left + item.Width / 2) + "," + (item.Top + item.Height / 2) + "," + item.Width + "," + item.Height + "," + item.Font.Size);
                }
            }

        }
    }
}
