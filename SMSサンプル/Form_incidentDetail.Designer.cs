﻿namespace moss_AP
{
    partial class Form_incidentDetail
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
            this.m_statusCombo = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.m_scubeno = new System.Windows.Forms.TextBox();
            this.m_mpmsno = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_updateOpe = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_update = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_selectBtn = new System.Windows.Forms.Button();
            this.m_selecttext = new System.Windows.Forms.TextBox();
            this.m_selectKoumoku = new System.Windows.Forms.ComboBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.m_incidentList = new System.Windows.Forms.ListView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_systemno = new System.Windows.Forms.TextBox();
            this.m_systemCombo = new System.Windows.Forms.ComboBox();
            this.m_hostno = new System.Windows.Forms.TextBox();
            this.m_hostCombo = new System.Windows.Forms.ComboBox();
            this.m_siteno = new System.Windows.Forms.TextBox();
            this.m_siteCombo = new System.Windows.Forms.ComboBox();
            this.m_userno = new System.Windows.Forms.TextBox();
            this.m_usernameCombo = new System.Windows.Forms.ComboBox();
            this.m_deleteBtn = new System.Windows.Forms.Button();
            this.m_mailout = new System.Windows.Forms.Button();
            this.m_incidentKBN = new System.Windows.Forms.ComboBox();
            this.m_timer = new System.Windows.Forms.DateTimePicker();
            this.label18 = new System.Windows.Forms.Label();
            this.m_youkakunin = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.m_enddate = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.m_fukkyudate = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.m_MATchkbox = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_incidentno = new System.Windows.Forms.TextBox();
            this.m_tehaidate = new System.Windows.Forms.DateTimePicker();
            this.label16 = new System.Windows.Forms.Label();
            this.m_uketukedate = new System.Windows.Forms.DateTimePicker();
            this.label17 = new System.Windows.Forms.Label();
            this.m_MATCommannd = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.m_incidentnaiyou = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_statusCombo
            // 
            this.m_statusCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_statusCombo.FormattingEnabled = true;
            this.m_statusCombo.Items.AddRange(new object[] {
            "未完了",
            "完了"});
            this.m_statusCombo.Location = new System.Drawing.Point(140, 97);
            this.m_statusCombo.Name = "m_statusCombo";
            this.m_statusCombo.Size = new System.Drawing.Size(68, 20);
            this.m_statusCombo.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(470, 35);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 12);
            this.label12.TabIndex = 116;
            this.label12.Text = "システム";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 36);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 12);
            this.label10.TabIndex = 112;
            this.label10.Text = "カスタマ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 100);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 12);
            this.label9.TabIndex = 111;
            this.label9.Text = "ステータス";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 144);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 12);
            this.label8.TabIndex = 110;
            this.label8.Text = "S-CUBE事例ID";
            // 
            // m_scubeno
            // 
            this.m_scubeno.Location = new System.Drawing.Point(140, 141);
            this.m_scubeno.Name = "m_scubeno";
            this.m_scubeno.Size = new System.Drawing.Size(325, 19);
            this.m_scubeno.TabIndex = 11;
            // 
            // m_mpmsno
            // 
            this.m_mpmsno.Location = new System.Drawing.Point(140, 119);
            this.m_mpmsno.Name = "m_mpmsno";
            this.m_mpmsno.Size = new System.Drawing.Size(325, 19);
            this.m_mpmsno.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 103;
            this.label4.Text = "拠点名";
            // 
            // m_updateOpe
            // 
            this.m_updateOpe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_updateOpe.Location = new System.Drawing.Point(560, 354);
            this.m_updateOpe.Name = "m_updateOpe";
            this.m_updateOpe.ReadOnly = true;
            this.m_updateOpe.Size = new System.Drawing.Size(233, 19);
            this.m_updateOpe.TabIndex = 22;
            this.m_updateOpe.TabStop = false;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(502, 356);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 101;
            this.label6.Text = "更新者";
            // 
            // m_update
            // 
            this.m_update.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_update.Location = new System.Drawing.Point(560, 331);
            this.m_update.Name = "m_update";
            this.m_update.ReadOnly = true;
            this.m_update.Size = new System.Drawing.Size(233, 19);
            this.m_update.TabIndex = 21;
            this.m_update.TabStop = false;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(502, 334);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 99;
            this.label5.Text = "更新日時";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(717, 388);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 33);
            this.button2.TabIndex = 23;
            this.button2.Text = "戻る";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(635, 388);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 33);
            this.button1.TabIndex = 22;
            this.button1.Text = "更新";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 12);
            this.label3.TabIndex = 91;
            this.label3.Text = "MPMSインシデント番号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 12);
            this.label2.TabIndex = 90;
            this.label2.Text = "ホスト";
            // 
            // m_selectBtn
            // 
            this.m_selectBtn.Location = new System.Drawing.Point(419, 4);
            this.m_selectBtn.Name = "m_selectBtn";
            this.m_selectBtn.Size = new System.Drawing.Size(75, 23);
            this.m_selectBtn.TabIndex = 2;
            this.m_selectBtn.Text = "検索";
            this.m_selectBtn.UseVisualStyleBackColor = true;
            this.m_selectBtn.Click += new System.EventHandler(this.m_selectBtn_Click);
            // 
            // m_selecttext
            // 
            this.m_selecttext.Location = new System.Drawing.Point(159, 6);
            this.m_selecttext.Name = "m_selecttext";
            this.m_selecttext.Size = new System.Drawing.Size(244, 19);
            this.m_selecttext.TabIndex = 1;
            // 
            // m_selectKoumoku
            // 
            this.m_selectKoumoku.FormattingEnabled = true;
            this.m_selectKoumoku.Location = new System.Drawing.Point(13, 6);
            this.m_selectKoumoku.Name = "m_selectKoumoku";
            this.m_selectKoumoku.Size = new System.Drawing.Size(140, 20);
            this.m_selectKoumoku.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.m_selectBtn);
            this.splitContainer2.Panel1.Controls.Add(this.m_selecttext);
            this.splitContainer2.Panel1.Controls.Add(this.m_selectKoumoku);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.m_incidentList);
            this.splitContainer2.Size = new System.Drawing.Size(804, 208);
            this.splitContainer2.SplitterDistance = 33;
            this.splitContainer2.TabIndex = 0;
            // 
            // m_incidentList
            // 
            this.m_incidentList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_incidentList.FullRowSelect = true;
            this.m_incidentList.GridLines = true;
            this.m_incidentList.Location = new System.Drawing.Point(0, 0);
            this.m_incidentList.Name = "m_incidentList";
            this.m_incidentList.Size = new System.Drawing.Size(804, 171);
            this.m_incidentList.TabIndex = 0;
            this.m_incidentList.UseCompatibleStateImageBehavior = false;
            this.m_incidentList.View = System.Windows.Forms.View.Details;
            this.m_incidentList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_incidentList_ColumnClick);
            this.m_incidentList.DoubleClick += new System.EventHandler(this.m_host_List_DoubleClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_systemno);
            this.splitContainer1.Panel2.Controls.Add(this.m_systemCombo);
            this.splitContainer1.Panel2.Controls.Add(this.m_hostno);
            this.splitContainer1.Panel2.Controls.Add(this.m_hostCombo);
            this.splitContainer1.Panel2.Controls.Add(this.m_siteno);
            this.splitContainer1.Panel2.Controls.Add(this.m_siteCombo);
            this.splitContainer1.Panel2.Controls.Add(this.m_userno);
            this.splitContainer1.Panel2.Controls.Add(this.m_usernameCombo);
            this.splitContainer1.Panel2.Controls.Add(this.m_deleteBtn);
            this.splitContainer1.Panel2.Controls.Add(this.m_mailout);
            this.splitContainer1.Panel2.Controls.Add(this.m_incidentKBN);
            this.splitContainer1.Panel2.Controls.Add(this.m_timer);
            this.splitContainer1.Panel2.Controls.Add(this.label18);
            this.splitContainer1.Panel2.Controls.Add(this.m_youkakunin);
            this.splitContainer1.Panel2.Controls.Add(this.label19);
            this.splitContainer1.Panel2.Controls.Add(this.m_enddate);
            this.splitContainer1.Panel2.Controls.Add(this.label13);
            this.splitContainer1.Panel2.Controls.Add(this.m_fukkyudate);
            this.splitContainer1.Panel2.Controls.Add(this.label15);
            this.splitContainer1.Panel2.Controls.Add(this.m_MATchkbox);
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.m_incidentno);
            this.splitContainer1.Panel2.Controls.Add(this.m_tehaidate);
            this.splitContainer1.Panel2.Controls.Add(this.label16);
            this.splitContainer1.Panel2.Controls.Add(this.m_uketukedate);
            this.splitContainer1.Panel2.Controls.Add(this.label17);
            this.splitContainer1.Panel2.Controls.Add(this.m_MATCommannd);
            this.splitContainer1.Panel2.Controls.Add(this.label14);
            this.splitContainer1.Panel2.Controls.Add(this.m_incidentnaiyou);
            this.splitContainer1.Panel2.Controls.Add(this.label11);
            this.splitContainer1.Panel2.Controls.Add(this.m_statusCombo);
            this.splitContainer1.Panel2.Controls.Add(this.label12);
            this.splitContainer1.Panel2.Controls.Add(this.label10);
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Panel2.Controls.Add(this.m_scubeno);
            this.splitContainer1.Panel2.Controls.Add(this.m_mpmsno);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.m_updateOpe);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.m_update);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(806, 642);
            this.splitContainer1.SplitterDistance = 210;
            this.splitContainer1.TabIndex = 1;
            // 
            // m_systemno
            // 
            this.m_systemno.Location = new System.Drawing.Point(518, 33);
            this.m_systemno.Name = "m_systemno";
            this.m_systemno.ReadOnly = true;
            this.m_systemno.Size = new System.Drawing.Size(44, 19);
            this.m_systemno.TabIndex = 3;
            this.m_systemno.TabStop = false;
            // 
            // m_systemCombo
            // 
            this.m_systemCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_systemCombo.FormattingEnabled = true;
            this.m_systemCombo.Location = new System.Drawing.Point(568, 33);
            this.m_systemCombo.Name = "m_systemCombo";
            this.m_systemCombo.Size = new System.Drawing.Size(225, 20);
            this.m_systemCombo.TabIndex = 4;
            this.m_systemCombo.SelectionChangeCommitted += new System.EventHandler(this.m_systemCombo_SelectionChangeCommitted);
            // 
            // m_hostno
            // 
            this.m_hostno.Location = new System.Drawing.Point(140, 76);
            this.m_hostno.Name = "m_hostno";
            this.m_hostno.ReadOnly = true;
            this.m_hostno.Size = new System.Drawing.Size(44, 19);
            this.m_hostno.TabIndex = 7;
            this.m_hostno.TabStop = false;
            // 
            // m_hostCombo
            // 
            this.m_hostCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_hostCombo.FormattingEnabled = true;
            this.m_hostCombo.Location = new System.Drawing.Point(188, 75);
            this.m_hostCombo.Name = "m_hostCombo";
            this.m_hostCombo.Size = new System.Drawing.Size(277, 20);
            this.m_hostCombo.TabIndex = 8;
            this.m_hostCombo.SelectionChangeCommitted += new System.EventHandler(this.m_hostCombo_SelectionChangeCommitted);
            // 
            // m_siteno
            // 
            this.m_siteno.Location = new System.Drawing.Point(140, 55);
            this.m_siteno.Name = "m_siteno";
            this.m_siteno.ReadOnly = true;
            this.m_siteno.Size = new System.Drawing.Size(44, 19);
            this.m_siteno.TabIndex = 5;
            this.m_siteno.TabStop = false;
            // 
            // m_siteCombo
            // 
            this.m_siteCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_siteCombo.FormattingEnabled = true;
            this.m_siteCombo.Location = new System.Drawing.Point(188, 54);
            this.m_siteCombo.Name = "m_siteCombo";
            this.m_siteCombo.Size = new System.Drawing.Size(277, 20);
            this.m_siteCombo.TabIndex = 6;
            this.m_siteCombo.SelectionChangeCommitted += new System.EventHandler(this.m_siteCombo_SelectionChangeCommitted);
            // 
            // m_userno
            // 
            this.m_userno.Location = new System.Drawing.Point(140, 33);
            this.m_userno.Name = "m_userno";
            this.m_userno.ReadOnly = true;
            this.m_userno.Size = new System.Drawing.Size(44, 19);
            this.m_userno.TabIndex = 1;
            this.m_userno.TabStop = false;
            // 
            // m_usernameCombo
            // 
            this.m_usernameCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_usernameCombo.FormattingEnabled = true;
            this.m_usernameCombo.Location = new System.Drawing.Point(188, 32);
            this.m_usernameCombo.Name = "m_usernameCombo";
            this.m_usernameCombo.Size = new System.Drawing.Size(277, 20);
            this.m_usernameCombo.TabIndex = 2;
            this.m_usernameCombo.SelectionChangeCommitted += new System.EventHandler(this.m_usernameCombo_SelectionChangeCommitted);
            // 
            // m_deleteBtn
            // 
            this.m_deleteBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_deleteBtn.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_deleteBtn.ForeColor = System.Drawing.Color.Red;
            this.m_deleteBtn.Location = new System.Drawing.Point(701, 4);
            this.m_deleteBtn.Name = "m_deleteBtn";
            this.m_deleteBtn.Size = new System.Drawing.Size(92, 27);
            this.m_deleteBtn.TabIndex = 24;
            this.m_deleteBtn.Text = "削除";
            this.m_deleteBtn.UseVisualStyleBackColor = true;
            this.m_deleteBtn.Click += new System.EventHandler(this.m_deleteBtn_Click);
            // 
            // m_mailout
            // 
            this.m_mailout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_mailout.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.m_mailout.Location = new System.Drawing.Point(433, 388);
            this.m_mailout.Name = "m_mailout";
            this.m_mailout.Size = new System.Drawing.Size(99, 33);
            this.m_mailout.TabIndex = 21;
            this.m_mailout.Text = "メール出力";
            this.m_mailout.UseVisualStyleBackColor = true;
            this.m_mailout.Click += new System.EventHandler(this.m_mailout_Click);
            // 
            // m_incidentKBN
            // 
            this.m_incidentKBN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_incidentKBN.FormattingEnabled = true;
            this.m_incidentKBN.Items.AddRange(new object[] {
            "アラーム検知",
            "障害申告",
            "問い合わせ"});
            this.m_incidentKBN.Location = new System.Drawing.Point(140, 164);
            this.m_incidentKBN.Name = "m_incidentKBN";
            this.m_incidentKBN.Size = new System.Drawing.Size(325, 20);
            this.m_incidentKBN.TabIndex = 12;
            // 
            // m_timer
            // 
            this.m_timer.CustomFormat = "yyyy年 M月 d日(ddd) HH:mm";
            this.m_timer.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_timer.Location = new System.Drawing.Point(537, 138);
            this.m_timer.Name = "m_timer";
            this.m_timer.ShowCheckBox = true;
            this.m_timer.Size = new System.Drawing.Size(212, 19);
            this.m_timer.TabIndex = 19;
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(481, 173);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(86, 12);
            this.label18.TabIndex = 176;
            this.label18.Text = "要確認メッセージ";
            // 
            // m_youkakunin
            // 
            this.m_youkakunin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_youkakunin.Location = new System.Drawing.Point(483, 189);
            this.m_youkakunin.Multiline = true;
            this.m_youkakunin.Name = "m_youkakunin";
            this.m_youkakunin.Size = new System.Drawing.Size(310, 98);
            this.m_youkakunin.TabIndex = 20;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(479, 143);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(41, 12);
            this.label19.TabIndex = 175;
            this.label19.Text = "タイマー";
            // 
            // m_enddate
            // 
            this.m_enddate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_enddate.CustomFormat = "yyyy年 M月 d日(ddd) HH:mm";
            this.m_enddate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_enddate.Location = new System.Drawing.Point(140, 402);
            this.m_enddate.Name = "m_enddate";
            this.m_enddate.ShowCheckBox = true;
            this.m_enddate.Size = new System.Drawing.Size(212, 19);
            this.m_enddate.TabIndex = 18;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(17, 407);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 172;
            this.label13.Text = "完了日時";
            // 
            // m_fukkyudate
            // 
            this.m_fukkyudate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_fukkyudate.CustomFormat = "yyyy年 M月 d日(ddd) HH:mm";
            this.m_fukkyudate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_fukkyudate.Location = new System.Drawing.Point(140, 379);
            this.m_fukkyudate.Name = "m_fukkyudate";
            this.m_fukkyudate.ShowCheckBox = true;
            this.m_fukkyudate.Size = new System.Drawing.Size(212, 19);
            this.m_fukkyudate.TabIndex = 17;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(17, 384);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 171;
            this.label15.Text = "復旧日時";
            // 
            // m_MATchkbox
            // 
            this.m_MATchkbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_MATchkbox.AutoSize = true;
            this.m_MATchkbox.Location = new System.Drawing.Point(19, 287);
            this.m_MATchkbox.Name = "m_MATchkbox";
            this.m_MATchkbox.Size = new System.Drawing.Size(91, 16);
            this.m_MATchkbox.TabIndex = 168;
            this.m_MATchkbox.Text = "MAT対応する";
            this.m_MATchkbox.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 311);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 12);
            this.label7.TabIndex = 167;
            this.label7.Text = "MAT対応コマンド";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 12);
            this.label1.TabIndex = 165;
            this.label1.Text = "No";
            // 
            // m_incidentno
            // 
            this.m_incidentno.Location = new System.Drawing.Point(140, 8);
            this.m_incidentno.Name = "m_incidentno";
            this.m_incidentno.ReadOnly = true;
            this.m_incidentno.Size = new System.Drawing.Size(99, 19);
            this.m_incidentno.TabIndex = 0;
            // 
            // m_tehaidate
            // 
            this.m_tehaidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_tehaidate.CustomFormat = "yyyy年 M月 d日(ddd) HH:mm";
            this.m_tehaidate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_tehaidate.Location = new System.Drawing.Point(140, 356);
            this.m_tehaidate.Name = "m_tehaidate";
            this.m_tehaidate.ShowCheckBox = true;
            this.m_tehaidate.Size = new System.Drawing.Size(212, 19);
            this.m_tehaidate.TabIndex = 16;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(17, 361);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 163;
            this.label16.Text = "手配日時";
            // 
            // m_uketukedate
            // 
            this.m_uketukedate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_uketukedate.CustomFormat = "yyyy年 M月 d日(ddd) HH:mm";
            this.m_uketukedate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_uketukedate.Location = new System.Drawing.Point(140, 333);
            this.m_uketukedate.Name = "m_uketukedate";
            this.m_uketukedate.ShowCheckBox = true;
            this.m_uketukedate.Size = new System.Drawing.Size(212, 19);
            this.m_uketukedate.TabIndex = 15;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(17, 338);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 12);
            this.label17.TabIndex = 162;
            this.label17.Text = "受付日時";
            // 
            // m_MATCommannd
            // 
            this.m_MATCommannd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_MATCommannd.Location = new System.Drawing.Point(140, 306);
            this.m_MATCommannd.Name = "m_MATCommannd";
            this.m_MATCommannd.Size = new System.Drawing.Size(325, 19);
            this.m_MATCommannd.TabIndex = 14;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(16, 193);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(84, 12);
            this.label14.TabIndex = 122;
            this.label14.Text = "インシデント内容";
            // 
            // m_incidentnaiyou
            // 
            this.m_incidentnaiyou.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_incidentnaiyou.Location = new System.Drawing.Point(140, 189);
            this.m_incidentnaiyou.Multiline = true;
            this.m_incidentnaiyou.Name = "m_incidentnaiyou";
            this.m_incidentnaiyou.Size = new System.Drawing.Size(325, 97);
            this.m_incidentnaiyou.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 167);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 12);
            this.label11.TabIndex = 120;
            this.label11.Text = "インシデント区分";
            // 
            // Form_incidentDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 642);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form_incidentDetail";
            this.Text = "インシデント情報";
            this.Load += new System.EventHandler(this.Form_InterfaceDetail_Load);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox m_statusCombo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox m_scubeno;
        private System.Windows.Forms.TextBox m_mpmsno;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox m_updateOpe;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox m_update;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button m_selectBtn;
        private System.Windows.Forms.TextBox m_selecttext;
        private System.Windows.Forms.ComboBox m_selectKoumoku;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListView m_incidentList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox m_MATCommannd;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox m_incidentnaiyou;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker m_tehaidate;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker m_uketukedate;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_incidentno;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox m_MATchkbox;
        private System.Windows.Forms.DateTimePicker m_enddate;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker m_fukkyudate;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox m_youkakunin;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DateTimePicker m_timer;
        private System.Windows.Forms.ComboBox m_incidentKBN;
        private System.Windows.Forms.Button m_mailout;
        private System.Windows.Forms.Button m_deleteBtn;
        private System.Windows.Forms.TextBox m_systemno;
        private System.Windows.Forms.ComboBox m_systemCombo;
        private System.Windows.Forms.TextBox m_hostno;
        private System.Windows.Forms.ComboBox m_hostCombo;
        private System.Windows.Forms.TextBox m_siteno;
        private System.Windows.Forms.ComboBox m_siteCombo;
        private System.Windows.Forms.TextBox m_userno;
        private System.Windows.Forms.ComboBox m_usernameCombo;
    }
}