namespace AutoUpdater_Monitor
{
    partial class AutoUpdater_Monitor_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoUpdater_Monitor_Form));
            this.label_ProgramVersion = new System.Windows.Forms.Label();
            this.label_ProgramVersionContent = new System.Windows.Forms.Label();
            this.label_ClientVersionContent = new System.Windows.Forms.Label();
            this.label_ClientVersion = new System.Windows.Forms.Label();
            this.updateIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.btn_CheckUpdate = new System.Windows.Forms.Button();
            this.btn_StartIVend = new System.Windows.Forms.Button();
            this.btn_StopIVend = new System.Windows.Forms.Button();
            this.label_ClientStatusContent = new System.Windows.Forms.Label();
            this.label_ClientStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_ProgramVersion
            // 
            this.label_ProgramVersion.AutoSize = true;
            this.label_ProgramVersion.Location = new System.Drawing.Point(12, 9);
            this.label_ProgramVersion.Name = "label_ProgramVersion";
            this.label_ProgramVersion.Size = new System.Drawing.Size(97, 15);
            this.label_ProgramVersion.TabIndex = 0;
            this.label_ProgramVersion.Text = "本程序版本：";
            // 
            // label_ProgramVersionContent
            // 
            this.label_ProgramVersionContent.AutoSize = true;
            this.label_ProgramVersionContent.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_ProgramVersionContent.Location = new System.Drawing.Point(115, 9);
            this.label_ProgramVersionContent.Name = "label_ProgramVersionContent";
            this.label_ProgramVersionContent.Size = new System.Drawing.Size(43, 15);
            this.label_ProgramVersionContent.TabIndex = 1;
            this.label_ProgramVersionContent.Text = "V1.0";
            // 
            // label_ClientVersionContent
            // 
            this.label_ClientVersionContent.AutoSize = true;
            this.label_ClientVersionContent.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_ClientVersionContent.Location = new System.Drawing.Point(115, 34);
            this.label_ClientVersionContent.Name = "label_ClientVersionContent";
            this.label_ClientVersionContent.Size = new System.Drawing.Size(39, 15);
            this.label_ClientVersionContent.TabIndex = 3;
            this.label_ClientVersionContent.Text = "未知";
            // 
            // label_ClientVersion
            // 
            this.label_ClientVersion.AutoSize = true;
            this.label_ClientVersion.Location = new System.Drawing.Point(12, 34);
            this.label_ClientVersion.Name = "label_ClientVersion";
            this.label_ClientVersion.Size = new System.Drawing.Size(97, 15);
            this.label_ClientVersion.TabIndex = 2;
            this.label_ClientVersion.Text = "客户端版本：";
            // 
            // updateIcon1
            // 
            this.updateIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("updateIcon1.Icon")));
            this.updateIcon1.Text = "更新监听";
            this.updateIcon1.Visible = true;
            this.updateIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.updateIcon1_MouseDoubleClick);
            // 
            // btn_CheckUpdate
            // 
            this.btn_CheckUpdate.Location = new System.Drawing.Point(99, 204);
            this.btn_CheckUpdate.Name = "btn_CheckUpdate";
            this.btn_CheckUpdate.Size = new System.Drawing.Size(138, 40);
            this.btn_CheckUpdate.TabIndex = 4;
            this.btn_CheckUpdate.Text = "检查更新";
            this.btn_CheckUpdate.UseVisualStyleBackColor = true;
            this.btn_CheckUpdate.Click += new System.EventHandler(this.btn_CheckUpdate_Click);
            // 
            // btn_StartIVend
            // 
            this.btn_StartIVend.Location = new System.Drawing.Point(260, 204);
            this.btn_StartIVend.Name = "btn_StartIVend";
            this.btn_StartIVend.Size = new System.Drawing.Size(138, 40);
            this.btn_StartIVend.TabIndex = 5;
            this.btn_StartIVend.Text = "开启iVend";
            this.btn_StartIVend.UseVisualStyleBackColor = true;
            this.btn_StartIVend.Click += new System.EventHandler(this.btn_StartIVend_Click);
            // 
            // btn_StopIVend
            // 
            this.btn_StopIVend.Location = new System.Drawing.Point(417, 204);
            this.btn_StopIVend.Name = "btn_StopIVend";
            this.btn_StopIVend.Size = new System.Drawing.Size(138, 40);
            this.btn_StopIVend.TabIndex = 6;
            this.btn_StopIVend.Text = "关闭iVend";
            this.btn_StopIVend.UseVisualStyleBackColor = true;
            this.btn_StopIVend.Click += new System.EventHandler(this.btn_StopIVend_Click);
            // 
            // label_ClientStatusContent
            // 
            this.label_ClientStatusContent.AutoSize = true;
            this.label_ClientStatusContent.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_ClientStatusContent.Location = new System.Drawing.Point(115, 59);
            this.label_ClientStatusContent.Name = "label_ClientStatusContent";
            this.label_ClientStatusContent.Size = new System.Drawing.Size(39, 15);
            this.label_ClientStatusContent.TabIndex = 8;
            this.label_ClientStatusContent.Text = "未知";
            // 
            // label_ClientStatus
            // 
            this.label_ClientStatus.AutoSize = true;
            this.label_ClientStatus.Location = new System.Drawing.Point(12, 59);
            this.label_ClientStatus.Name = "label_ClientStatus";
            this.label_ClientStatus.Size = new System.Drawing.Size(97, 15);
            this.label_ClientStatus.TabIndex = 7;
            this.label_ClientStatus.Text = "客户端状态：";
            this.label_ClientStatus.Click += new System.EventHandler(this.label2_Click);
            // 
            // AutoUpdater_Monitor_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 256);
            this.Controls.Add(this.label_ClientStatusContent);
            this.Controls.Add(this.label_ClientStatus);
            this.Controls.Add(this.btn_StopIVend);
            this.Controls.Add(this.btn_StartIVend);
            this.Controls.Add(this.btn_CheckUpdate);
            this.Controls.Add(this.label_ClientVersionContent);
            this.Controls.Add(this.label_ClientVersion);
            this.Controls.Add(this.label_ProgramVersionContent);
            this.Controls.Add(this.label_ProgramVersion);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AutoUpdater_Monitor_Form";
            this.Text = "金马自动更新客户端监听程序";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AutoUpdater_Monitor_Form_FormClosing);
            this.Load += new System.EventHandler(this.AutoUpdater_Monitor_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_ProgramVersion;
        private System.Windows.Forms.Label label_ProgramVersionContent;
        private System.Windows.Forms.Label label_ClientVersionContent;
        private System.Windows.Forms.Label label_ClientVersion;
        private System.Windows.Forms.NotifyIcon updateIcon1;
        private System.Windows.Forms.Button btn_CheckUpdate;
        private System.Windows.Forms.Button btn_StartIVend;
        private System.Windows.Forms.Button btn_StopIVend;
        private System.Windows.Forms.Label label_ClientStatus;
        public System.Windows.Forms.Label label_ClientStatusContent;

    }
}

