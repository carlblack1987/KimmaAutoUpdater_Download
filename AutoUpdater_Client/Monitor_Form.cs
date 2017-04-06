using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Security;
using System.Diagnostics;
using System.IO;
using System.Threading;
using AutoUpdater_Client.Log;
using AutoUpdater_Client.Net;

namespace AutoUpdater_Monitor
{
    public partial class AutoUpdater_Monitor_Form : Form
    {
        /// <summary>
        /// 后台对象
        /// </summary>
        AutoUpdater_Client.UpdateHelper updateHelper;
        /// <summary>
        /// 下载更新窗口对象
        /// </summary>
        AutoUpdater_Client.Client_Form client_From;

        public AutoUpdater_Monitor_Form()
        {
            InitializeComponent();
            this.TopMost = true;
            Control.CheckForIllegalCrossThreadCalls = false;
            try
            {
                client_From = null;
                updateHelper = new AutoUpdater_Client.UpdateHelper();
                updateHelper._CurrentVersion = updateHelper.getClientVersion();
                updateHelper.monitor_form = this;
                label_ClientVersionContent.Text = updateHelper._CurrentVersion;
            }
            catch (WebException ex)
            {
                AutoUpdater_Client.Log.LogOperator.AddUpdateLog("ERROR", "Network failure: " + ex.ToString());
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                AutoUpdater_Client.Log.LogOperator.AddUpdateLog("ERROR", "Initiate Monitor Program failure, reason: " + ex.ToString());
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void AutoUpdater_Monitor_Form_Load(object sender, EventArgs e)
        {
            //为图标添加右键菜单
            MenuItem menuItem1 = new MenuItem("显示窗口");
            MenuItem menuItem2 = new MenuItem("退出程序");
            menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            updateIcon1.ContextMenu = new ContextMenu(new MenuItem[] { menuItem1, menuItem2});
        }

        private void menuItem1_Click(object sender, System.EventArgs e)
        {
            this.Show();
        }

        private void menuItem2_Click(object sender, System.EventArgs e)
        {
            Application.ExitThread();
        } 

        private void updateIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void AutoUpdater_Monitor_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void btn_CheckUpdate_Click(object sender, EventArgs e)
        {
            //发送获取更新的请求
            string updateInfo = updateHelper.getUpdateInfo();
            //MessageBox.Show(updateInfo);
            //判断是否需要更新
            if (updateHelper.isUpdateRequired())
            {
                if (MessageBox.Show("发现新版本，是否更新?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    client_From = new AutoUpdater_Client.Client_Form(updateHelper);
                    client_From.TopMost = true;
                    client_From.Show();
                }
            }
            else if (updateInfo.Equals("-1"))
            {
                MessageBox.Show("连接更新服务器失败，请检查网络连接。", "提示");
            }
            else
            {
                MessageBox.Show("当前版本已经是最新版本。", "提示");
            }
        }

        private void btn_StartIVend_Click(object sender, EventArgs e)
        {
            iVend_Message.iVend_Message.startProcessByName("iVend");
        }

        private void btn_StopIVend_Click(object sender, EventArgs e)
        {
            updateHelper.closeiVendProcessTrd();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // 设置隐藏
            this.Visible = false;
        }

        public void showMessage(string str)
        {
            MessageBox.Show(str);
        }

    }
}
