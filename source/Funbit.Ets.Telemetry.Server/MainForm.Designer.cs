﻿namespace Funbit.Ets.Telemetry.Server
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ipAddressLabel = new System.Windows.Forms.Label();
            this.interfacesDropDown = new System.Windows.Forms.ComboBox();
            this.networkInterfaceTitleLabel = new System.Windows.Forms.Label();
            this.serverIpTitleLabel = new System.Windows.Forms.Label();
            this.appUrlLabel = new System.Windows.Forms.LinkLabel();
            this.appUrlTitleLabel = new System.Windows.Forms.Label();
            this.apiUrlLabel = new System.Windows.Forms.LinkLabel();
            this.statusLabel = new System.Windows.Forms.Label();
            this.apiEndpointUrlTitleLabel = new System.Windows.Forms.Label();
            this.statusTitleLabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.broadcastTimer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.serverToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AddShortCutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.uninstallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.donateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.TranslateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Lang_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Lang_ko_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Lang_en_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // trayIcon
            // 
            this.trayIcon.BalloonTipTitle = "ETS2 Telemetry Server is running...";
            this.trayIcon.ContextMenuStrip = this.contextMenuStrip;
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "ETS2 Telemetry Server is running...";
            this.trayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.trayIcon_MouseDoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(129, 34);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Image = global::Funbit.Ets.Telemetry.Server.Properties.Resources.CloseIcon;
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(128, 30);
            this.closeToolStripMenuItem.Text = "종료";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // statusUpdateTimer
            // 
            this.statusUpdateTimer.Interval = 1000;
            this.statusUpdateTimer.Tick += new System.EventHandler(this.statusUpdateTimer_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ipAddressLabel);
            this.groupBox1.Controls.Add(this.interfacesDropDown);
            this.groupBox1.Controls.Add(this.networkInterfaceTitleLabel);
            this.groupBox1.Controls.Add(this.serverIpTitleLabel);
            this.groupBox1.Controls.Add(this.appUrlLabel);
            this.groupBox1.Controls.Add(this.appUrlTitleLabel);
            this.groupBox1.Controls.Add(this.apiUrlLabel);
            this.groupBox1.Controls.Add(this.statusLabel);
            this.groupBox1.Controls.Add(this.apiEndpointUrlTitleLabel);
            this.groupBox1.Controls.Add(this.statusTitleLabel);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(20, 45);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(879, 322);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "서버 상태";
            // 
            // ipAddressLabel
            // 
            this.ipAddressLabel.AutoSize = true;
            this.ipAddressLabel.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipAddressLabel.ForeColor = System.Drawing.Color.Purple;
            this.ipAddressLabel.Location = new System.Drawing.Point(236, 182);
            this.ipAddressLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ipAddressLabel.Name = "ipAddressLabel";
            this.ipAddressLabel.Size = new System.Drawing.Size(138, 28);
            this.ipAddressLabel.TabIndex = 21;
            this.ipAddressLabel.Text = "111.222.333.444";
            this.toolTip.SetToolTip(this.ipAddressLabel, "Use this IP address for mobile application (Android)");
            // 
            // interfacesDropDown
            // 
            this.interfacesDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.interfacesDropDown.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.interfacesDropDown.FormattingEnabled = true;
            this.interfacesDropDown.Location = new System.Drawing.Point(240, 126);
            this.interfacesDropDown.Margin = new System.Windows.Forms.Padding(4);
            this.interfacesDropDown.Name = "interfacesDropDown";
            this.interfacesDropDown.Size = new System.Drawing.Size(613, 36);
            this.interfacesDropDown.TabIndex = 20;
            this.interfacesDropDown.TabStop = false;
            this.interfacesDropDown.SelectedIndexChanged += new System.EventHandler(this.interfaceDropDown_SelectedIndexChanged);
            // 
            // networkInterfaceTitleLabel
            // 
            this.networkInterfaceTitleLabel.AutoSize = true;
            this.networkInterfaceTitleLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.networkInterfaceTitleLabel.Location = new System.Drawing.Point(21, 130);
            this.networkInterfaceTitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.networkInterfaceTitleLabel.Name = "networkInterfaceTitleLabel";
            this.networkInterfaceTitleLabel.Size = new System.Drawing.Size(206, 28);
            this.networkInterfaceTitleLabel.TabIndex = 19;
            this.networkInterfaceTitleLabel.Text = "네트워크 인터페이스 :";
            // 
            // serverIpTitleLabel
            // 
            this.serverIpTitleLabel.AutoSize = true;
            this.serverIpTitleLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serverIpTitleLabel.Location = new System.Drawing.Point(114, 182);
            this.serverIpTitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.serverIpTitleLabel.Name = "serverIpTitleLabel";
            this.serverIpTitleLabel.Size = new System.Drawing.Size(106, 28);
            this.serverIpTitleLabel.TabIndex = 17;
            this.serverIpTitleLabel.Text = "서버 주소 :";
            // 
            // appUrlLabel
            // 
            this.appUrlLabel.AutoSize = true;
            this.appUrlLabel.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appUrlLabel.Location = new System.Drawing.Point(236, 225);
            this.appUrlLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.appUrlLabel.Name = "appUrlLabel";
            this.appUrlLabel.Size = new System.Drawing.Size(112, 28);
            this.appUrlLabel.TabIndex = 16;
            this.appUrlLabel.TabStop = true;
            this.appUrlLabel.Text = "appUrlLabel";
            this.toolTip.SetToolTip(this.appUrlLabel, "이 URL은 HTML5 대시보드를 열기 위해 데스크탑이나 모바일 브라우저에서 열때 사용합니다. (클릭하여 열기)");
            this.appUrlLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.appUrlLabel_LinkClicked);
            // 
            // appUrlTitleLabel
            // 
            this.appUrlTitleLabel.AutoSize = true;
            this.appUrlTitleLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appUrlTitleLabel.Location = new System.Drawing.Point(69, 225);
            this.appUrlTitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.appUrlTitleLabel.Name = "appUrlTitleLabel";
            this.appUrlTitleLabel.Size = new System.Drawing.Size(153, 28);
            this.appUrlTitleLabel.TabIndex = 15;
            this.appUrlTitleLabel.Text = "HTML5 앱 주소 :";
            // 
            // apiUrlLabel
            // 
            this.apiUrlLabel.AutoSize = true;
            this.apiUrlLabel.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.apiUrlLabel.Location = new System.Drawing.Point(236, 268);
            this.apiUrlLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.apiUrlLabel.Name = "apiUrlLabel";
            this.apiUrlLabel.Size = new System.Drawing.Size(105, 28);
            this.apiUrlLabel.TabIndex = 14;
            this.apiUrlLabel.TabStop = true;
            this.apiUrlLabel.Text = "apiUrlLabel";
            this.toolTip.SetToolTip(this.apiUrlLabel, "이 URL은 REST API를 사용하여 직접 어플리케이션을 만들때 사용 합니다. (클릭하여 열기)");
            this.apiUrlLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.apiUrlLabel_LinkClicked);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.statusLabel.Location = new System.Drawing.Point(236, 57);
            this.statusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(84, 28);
            this.statusLabel.TabIndex = 13;
            this.statusLabel.Text = "확인중...";
            // 
            // apiEndpointUrlTitleLabel
            // 
            this.apiEndpointUrlTitleLabel.AutoSize = true;
            this.apiEndpointUrlTitleLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.apiEndpointUrlTitleLabel.Location = new System.Drawing.Point(40, 268);
            this.apiEndpointUrlTitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.apiEndpointUrlTitleLabel.Name = "apiEndpointUrlTitleLabel";
            this.apiEndpointUrlTitleLabel.Size = new System.Drawing.Size(184, 28);
            this.apiEndpointUrlTitleLabel.TabIndex = 12;
            this.apiEndpointUrlTitleLabel.Text = "Telemetry API 주소 :";
            // 
            // statusTitleLabel
            // 
            this.statusTitleLabel.AutoSize = true;
            this.statusTitleLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusTitleLabel.Location = new System.Drawing.Point(153, 57);
            this.statusTitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.statusTitleLabel.Name = "statusTitleLabel";
            this.statusTitleLabel.Size = new System.Drawing.Size(61, 28);
            this.statusTitleLabel.TabIndex = 11;
            this.statusTitleLabel.Text = "상태 :";
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 250;
            this.toolTip.AutoPopDelay = 6000;
            this.toolTip.InitialDelay = 250;
            this.toolTip.ReshowDelay = 50;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // broadcastTimer
            // 
            this.broadcastTimer.Interval = 1000;
            this.broadcastTimer.Tick += new System.EventHandler(this.broadcastTimer_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverToolStripMenu,
            this.Lang_ToolStripMenuItem,
            this.helpToolStripMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(10, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(920, 35);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // serverToolStripMenu
            // 
            this.serverToolStripMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddShortCutToolStripMenuItem,
            this.toolStripMenuItem2,
            this.uninstallToolStripMenuItem});
            this.serverToolStripMenu.Name = "serverToolStripMenu";
            this.serverToolStripMenu.Size = new System.Drawing.Size(60, 29);
            this.serverToolStripMenu.Text = "서버";
            // 
            // AddShortCutToolStripMenuItem
            // 
            this.AddShortCutToolStripMenuItem.Name = "AddShortCutToolStripMenuItem";
            this.AddShortCutToolStripMenuItem.Size = new System.Drawing.Size(288, 30);
            this.AddShortCutToolStripMenuItem.Text = "바탕화면 바로가기 추가";
            this.AddShortCutToolStripMenuItem.Click += new System.EventHandler(this.AddShortCutToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(285, 6);
            // 
            // uninstallToolStripMenuItem
            // 
            this.uninstallToolStripMenuItem.Name = "uninstallToolStripMenuItem";
            this.uninstallToolStripMenuItem.Size = new System.Drawing.Size(288, 30);
            this.uninstallToolStripMenuItem.Text = "설치 제거";
            this.uninstallToolStripMenuItem.Click += new System.EventHandler(this.uninstallToolStripMenuItem_Click);
            // 
            // helpToolStripMenu
            // 
            this.helpToolStripMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem,
            this.donateToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.toolStripMenuItem1,
            this.TranslateToolStripMenuItem});
            this.helpToolStripMenu.Name = "helpToolStripMenu";
            this.helpToolStripMenu.Size = new System.Drawing.Size(78, 29);
            this.helpToolStripMenu.Text = "도움말";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(252, 30);
            this.helpToolStripMenuItem.Text = "도움말";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // donateToolStripMenuItem
            // 
            this.donateToolStripMenuItem.Name = "donateToolStripMenuItem";
            this.donateToolStripMenuItem.Size = new System.Drawing.Size(252, 30);
            this.donateToolStripMenuItem.Text = "원 제작자에게 기부";
            this.donateToolStripMenuItem.Click += new System.EventHandler(this.donateToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(252, 30);
            this.aboutToolStripMenuItem.Text = "이 프로그램은...";
            this.aboutToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.aboutToolStripMenuItem.Visible = false;
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(249, 6);
            // 
            // TranslateToolStripMenuItem
            // 
            this.TranslateToolStripMenuItem.Name = "TranslateToolStripMenuItem";
            this.TranslateToolStripMenuItem.Size = new System.Drawing.Size(252, 30);
            this.TranslateToolStripMenuItem.Text = "한글화";
            this.TranslateToolStripMenuItem.Click += new System.EventHandler(this.TranslateToolStripMenuItem_Click);
            // 
            // Lang_ToolStripMenuItem
            // 
            this.Lang_ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Lang_ko_ToolStripMenuItem,
            this.Lang_en_ToolStripMenuItem});
            this.Lang_ToolStripMenuItem.Name = "Lang_ToolStripMenuItem";
            this.Lang_ToolStripMenuItem.Size = new System.Drawing.Size(155, 29);
            this.Lang_ToolStripMenuItem.Text = "언어 (Language)";
            // 
            // Lang_ko_ToolStripMenuItem
            // 
            this.Lang_ko_ToolStripMenuItem.Name = "Lang_ko_ToolStripMenuItem";
            this.Lang_ko_ToolStripMenuItem.Size = new System.Drawing.Size(252, 30);
            this.Lang_ko_ToolStripMenuItem.Text = "한국어";
            this.Lang_ko_ToolStripMenuItem.Click += new System.EventHandler(this.Lang_ko_ToolStripMenuItem_Click);
            // 
            // Lang_en_ToolStripMenuItem
            // 
            this.Lang_en_ToolStripMenuItem.Name = "Lang_en_ToolStripMenuItem";
            this.Lang_en_ToolStripMenuItem.Size = new System.Drawing.Size(252, 30);
            this.Lang_en_ToolStripMenuItem.Text = "English";
            this.Lang_en_ToolStripMenuItem.Click += new System.EventHandler(this.Lang_en_ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 388);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ETS2/ATS Telemetry 서버 한글판";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.contextMenuStrip.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.Timer statusUpdateTimer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label serverIpTitleLabel;
        private System.Windows.Forms.LinkLabel appUrlLabel;
        private System.Windows.Forms.Label appUrlTitleLabel;
        private System.Windows.Forms.LinkLabel apiUrlLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label apiEndpointUrlTitleLabel;
        private System.Windows.Forms.Label statusTitleLabel;
        private System.Windows.Forms.Label ipAddressLabel;
        private System.Windows.Forms.ComboBox interfacesDropDown;
        private System.Windows.Forms.Label networkInterfaceTitleLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Timer broadcastTimer;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem serverToolStripMenu;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenu;
        private System.Windows.Forms.ToolStripMenuItem uninstallToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem donateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem AddShortCutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TranslateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Lang_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Lang_ko_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Lang_en_ToolStripMenuItem;
    }
}

