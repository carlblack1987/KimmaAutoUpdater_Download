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
            this.label_ProgramVersion = new System.Windows.Forms.Label();
            this.label_ProgramVersionContent = new System.Windows.Forms.Label();
            this.label_ClientVersionContent = new System.Windows.Forms.Label();
            this.label_ClientVersion = new System.Windows.Forms.Label();
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
            this.label_ProgramVersionContent.Size = new System.Drawing.Size(39, 15);
            this.label_ProgramVersionContent.TabIndex = 1;
            this.label_ProgramVersionContent.Text = "未知";
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
            // AutoUpdater_Monitor_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 256);
            this.Controls.Add(this.label_ClientVersionContent);
            this.Controls.Add(this.label_ClientVersion);
            this.Controls.Add(this.label_ProgramVersionContent);
            this.Controls.Add(this.label_ProgramVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AutoUpdater_Monitor_Form";
            this.Text = "金马自动更新客户端监听程序";
            this.Load += new System.EventHandler(this.AutoUpdater_Monitor_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_ProgramVersion;
        private System.Windows.Forms.Label label_ProgramVersionContent;
        private System.Windows.Forms.Label label_ClientVersionContent;
        private System.Windows.Forms.Label label_ClientVersion;

    }
}

