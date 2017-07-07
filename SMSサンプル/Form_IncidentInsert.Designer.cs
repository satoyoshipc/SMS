namespace SMSサンプル
{
    partial class Form_IncidentInsert
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
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_content = new System.Windows.Forms.TextBox();
            this.m_userno = new System.Windows.Forms.TextBox();
            this.m_usernameCombo = new System.Windows.Forms.ComboBox();
            this.m_idlabel = new System.Windows.Forms.TextBox();
            this.m_labelinputOpe = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.m_ScubeID = new System.Windows.Forms.TextBox();
            this.m_matcommand = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_MSMSno = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.m_siteno = new System.Windows.Forms.TextBox();
            this.m_siteCombo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.m_hostno = new System.Windows.Forms.TextBox();
            this.m_hostCombo = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.m_incident_kubun_combo = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.m_uketukedate = new System.Windows.Forms.DateTimePicker();
            this.m_tehaidate = new System.Windows.Forms.DateTimePicker();
            this.m_fukyudate = new System.Windows.Forms.DateTimePicker();
            this.m_enddate = new System.Windows.Forms.DateTimePicker();
            this.button4 = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.m_matflgCombo = new System.Windows.Forms.ComboBox();
            this.m_statuscheck = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.m_systemno = new System.Windows.Forms.TextBox();
            this.m_systemCombo = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_timerpicker = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.m_kakunin = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 249);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 12);
            this.label5.TabIndex = 145;
            this.label5.Text = "インシデント内容";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 224);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 12);
            this.label4.TabIndex = 144;
            this.label4.Text = "インシデント区分";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 12);
            this.label1.TabIndex = 143;
            this.label1.Text = "カスタマ名";
            // 
            // m_content
            // 
            this.m_content.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_content.Location = new System.Drawing.Point(143, 244);
            this.m_content.Multiline = true;
            this.m_content.Name = "m_content";
            this.m_content.Size = new System.Drawing.Size(445, 55);
            this.m_content.TabIndex = 13;
            // 
            // m_userno
            // 
            this.m_userno.Location = new System.Drawing.Point(144, 41);
            this.m_userno.Name = "m_userno";
            this.m_userno.ReadOnly = true;
            this.m_userno.Size = new System.Drawing.Size(44, 19);
            this.m_userno.TabIndex = 1;
            this.m_userno.TabStop = false;
            // 
            // m_usernameCombo
            // 
            this.m_usernameCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_usernameCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_usernameCombo.FormattingEnabled = true;
            this.m_usernameCombo.Location = new System.Drawing.Point(194, 40);
            this.m_usernameCombo.Name = "m_usernameCombo";
            this.m_usernameCombo.Size = new System.Drawing.Size(397, 20);
            this.m_usernameCombo.TabIndex = 2;
            this.m_usernameCombo.SelectionChangeCommitted += new System.EventHandler(this.m_usernameCombo_SelectionChangeCommitted);
            // 
            // m_idlabel
            // 
            this.m_idlabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_idlabel.Location = new System.Drawing.Point(143, 454);
            this.m_idlabel.Name = "m_idlabel";
            this.m_idlabel.ReadOnly = true;
            this.m_idlabel.Size = new System.Drawing.Size(44, 19);
            this.m_idlabel.TabIndex = 20;
            this.m_idlabel.TabStop = false;
            // 
            // m_labelinputOpe
            // 
            this.m_labelinputOpe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_labelinputOpe.Location = new System.Drawing.Point(193, 454);
            this.m_labelinputOpe.Name = "m_labelinputOpe";
            this.m_labelinputOpe.ReadOnly = true;
            this.m_labelinputOpe.Size = new System.Drawing.Size(372, 19);
            this.m_labelinputOpe.TabIndex = 21;
            this.m_labelinputOpe.TabStop = false;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(389, 595);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 23);
            this.button3.TabIndex = 22;
            this.button3.Text = "登録";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(490, 595);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 23);
            this.button2.TabIndex = 23;
            this.button2.Text = "キャンセル";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // m_ScubeID
            // 
            this.m_ScubeID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ScubeID.Location = new System.Drawing.Point(144, 192);
            this.m_ScubeID.Name = "m_ScubeID";
            this.m_ScubeID.Size = new System.Drawing.Size(445, 19);
            this.m_ScubeID.TabIndex = 11;
            // 
            // m_matcommand
            // 
            this.m_matcommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_matcommand.Location = new System.Drawing.Point(143, 334);
            this.m_matcommand.Multiline = true;
            this.m_matcommand.Name = "m_matcommand";
            this.m_matcommand.Size = new System.Drawing.Size(445, 59);
            this.m_matcommand.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 195);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 127;
            this.label3.Text = "S-cube事例ID";
            // 
            // m_MSMSno
            // 
            this.m_MSMSno.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_MSMSno.Location = new System.Drawing.Point(143, 167);
            this.m_MSMSno.Name = "m_MSMSno";
            this.m_MSMSno.Size = new System.Drawing.Size(445, 19);
            this.m_MSMSno.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 171);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 12);
            this.label2.TabIndex = 125;
            this.label2.Text = "MPMSインシデント番号";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 148;
            this.label6.Text = "拠点名";
            // 
            // m_siteno
            // 
            this.m_siteno.Location = new System.Drawing.Point(144, 89);
            this.m_siteno.Name = "m_siteno";
            this.m_siteno.ReadOnly = true;
            this.m_siteno.Size = new System.Drawing.Size(44, 19);
            this.m_siteno.TabIndex = 5;
            this.m_siteno.TabStop = false;
            // 
            // m_siteCombo
            // 
            this.m_siteCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_siteCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_siteCombo.FormattingEnabled = true;
            this.m_siteCombo.Location = new System.Drawing.Point(194, 88);
            this.m_siteCombo.Name = "m_siteCombo";
            this.m_siteCombo.Size = new System.Drawing.Size(397, 20);
            this.m_siteCombo.TabIndex = 6;
            this.m_siteCombo.SelectionChangeCommitted += new System.EventHandler(this.m_siteCombo_SelectionChangeCommitted);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 12);
            this.label7.TabIndex = 151;
            this.label7.Text = "ホスト名";
            // 
            // m_hostno
            // 
            this.m_hostno.Location = new System.Drawing.Point(144, 115);
            this.m_hostno.Name = "m_hostno";
            this.m_hostno.ReadOnly = true;
            this.m_hostno.Size = new System.Drawing.Size(44, 19);
            this.m_hostno.TabIndex = 7;
            this.m_hostno.TabStop = false;
            // 
            // m_hostCombo
            // 
            this.m_hostCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_hostCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_hostCombo.FormattingEnabled = true;
            this.m_hostCombo.Location = new System.Drawing.Point(194, 114);
            this.m_hostCombo.Name = "m_hostCombo";
            this.m_hostCombo.Size = new System.Drawing.Size(397, 20);
            this.m_hostCombo.TabIndex = 8;
            this.m_hostCombo.SelectionChangeCommitted += new System.EventHandler(this.m_hostCombo_SelectionChangeCommitted);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 340);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 12);
            this.label8.TabIndex = 153;
            this.label8.Text = "MAT対応コマンド";
            // 
            // m_incident_kubun_combo
            // 
            this.m_incident_kubun_combo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_incident_kubun_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_incident_kubun_combo.FormattingEnabled = true;
            this.m_incident_kubun_combo.Items.AddRange(new object[] {
            "アラーム検知",
            "障害申告",
            "問い合わせ"});
            this.m_incident_kubun_combo.Location = new System.Drawing.Point(144, 218);
            this.m_incident_kubun_combo.Name = "m_incident_kubun_combo";
            this.m_incident_kubun_combo.Size = new System.Drawing.Size(444, 20);
            this.m_incident_kubun_combo.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(87, 404);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 155;
            this.label9.Text = "受付日時";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(87, 432);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 156;
            this.label11.Text = "手配日時";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(348, 403);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 157;
            this.label12.Text = "復旧日時";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(348, 430);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 158;
            this.label13.Text = "完了日時";
            // 
            // m_uketukedate
            // 
            this.m_uketukedate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_uketukedate.CustomFormat = "yyyy年M月d日(ddd) HH:mm";
            this.m_uketukedate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_uketukedate.Location = new System.Drawing.Point(146, 400);
            this.m_uketukedate.Name = "m_uketukedate";
            this.m_uketukedate.Size = new System.Drawing.Size(173, 19);
            this.m_uketukedate.TabIndex = 16;
            this.m_uketukedate.ValueChanged += new System.EventHandler(this.m_uketukedate_ValueChanged);
            this.m_uketukedate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_uketukedate_KeyDown);
            // 
            // m_tehaidate
            // 
            this.m_tehaidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_tehaidate.CustomFormat = "yyyy年M月d日(ddd) HH:mm";
            this.m_tehaidate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_tehaidate.Location = new System.Drawing.Point(146, 428);
            this.m_tehaidate.Name = "m_tehaidate";
            this.m_tehaidate.Size = new System.Drawing.Size(173, 19);
            this.m_tehaidate.TabIndex = 17;
            this.m_tehaidate.ValueChanged += new System.EventHandler(this.m_tehaidate_ValueChanged);
            this.m_tehaidate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_tehaidate_KeyDown);
            // 
            // m_fukyudate
            // 
            this.m_fukyudate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_fukyudate.CustomFormat = "yyyy年M月d日(ddd) HH:mm";
            this.m_fukyudate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_fukyudate.Location = new System.Drawing.Point(405, 399);
            this.m_fukyudate.Name = "m_fukyudate";
            this.m_fukyudate.Size = new System.Drawing.Size(182, 19);
            this.m_fukyudate.TabIndex = 18;
            this.m_fukyudate.ValueChanged += new System.EventHandler(this.m_fukyudate_ValueChanged);
            this.m_fukyudate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_fukyudate_KeyDown);
            // 
            // m_enddate
            // 
            this.m_enddate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_enddate.CustomFormat = "yyyy年M月d日(ddd) HH:mm";
            this.m_enddate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_enddate.Location = new System.Drawing.Point(405, 425);
            this.m_enddate.Name = "m_enddate";
            this.m_enddate.Size = new System.Drawing.Size(182, 19);
            this.m_enddate.TabIndex = 19;
            this.m_enddate.ValueChanged += new System.EventHandler(this.m_enddate_ValueChanged);
            this.m_enddate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_enddate_KeyDown);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(5, 7);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(95, 23);
            this.button4.TabIndex = 0;
            this.button4.Text = "メール出力";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(12, 461);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(51, 12);
            this.label18.TabIndex = 173;
            this.label18.Text = "オペレータ";
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(12, 310);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 12);
            this.label19.TabIndex = 174;
            this.label19.Text = "MAT対応";
            // 
            // m_matflgCombo
            // 
            this.m_matflgCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_matflgCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_matflgCombo.FormattingEnabled = true;
            this.m_matflgCombo.Items.AddRange(new object[] {
            "あり",
            "なし"});
            this.m_matflgCombo.Location = new System.Drawing.Point(144, 305);
            this.m_matflgCombo.Name = "m_matflgCombo";
            this.m_matflgCombo.Size = new System.Drawing.Size(68, 20);
            this.m_matflgCombo.TabIndex = 14;
            // 
            // m_statuscheck
            // 
            this.m_statuscheck.AutoSize = true;
            this.m_statuscheck.Location = new System.Drawing.Point(146, 143);
            this.m_statuscheck.Name = "m_statuscheck";
            this.m_statuscheck.Size = new System.Drawing.Size(48, 16);
            this.m_statuscheck.TabIndex = 9;
            this.m_statuscheck.Text = "完了";
            this.m_statuscheck.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 68);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 12);
            this.label10.TabIndex = 178;
            this.label10.Text = "システム名";
            // 
            // m_systemno
            // 
            this.m_systemno.Location = new System.Drawing.Point(144, 65);
            this.m_systemno.Name = "m_systemno";
            this.m_systemno.ReadOnly = true;
            this.m_systemno.Size = new System.Drawing.Size(44, 19);
            this.m_systemno.TabIndex = 3;
            this.m_systemno.TabStop = false;
            // 
            // m_systemCombo
            // 
            this.m_systemCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_systemCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_systemCombo.FormattingEnabled = true;
            this.m_systemCombo.Location = new System.Drawing.Point(194, 64);
            this.m_systemCombo.Name = "m_systemCombo";
            this.m_systemCombo.Size = new System.Drawing.Size(397, 20);
            this.m_systemCombo.TabIndex = 4;
            this.m_systemCombo.SelectionChangeCommitted += new System.EventHandler(this.m_systemCombo_SelectionChangeCommitted);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.m_timerpicker);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.m_kakunin);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Location = new System.Drawing.Point(12, 483);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(575, 103);
            this.groupBox1.TabIndex = 181;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "タイマー登録";
            // 
            // m_timerpicker
            // 
            this.m_timerpicker.CustomFormat = "yyyy年M月d日(ddd) HH:mm";
            this.m_timerpicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_timerpicker.Location = new System.Drawing.Point(131, 18);
            this.m_timerpicker.Name = "m_timerpicker";
            this.m_timerpicker.Size = new System.Drawing.Size(176, 19);
            this.m_timerpicker.TabIndex = 0;
            this.m_timerpicker.ValueChanged += new System.EventHandler(this.m_timerpicker_ValueChanged);
            this.m_timerpicker.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_timerpicker_KeyDown);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(313, 21);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(0, 12);
            this.label14.TabIndex = 1;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(9, 46);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(86, 12);
            this.label16.TabIndex = 183;
            this.label16.Text = "要確認メッセージ";
            // 
            // m_kakunin
            // 
            this.m_kakunin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_kakunin.Location = new System.Drawing.Point(131, 43);
            this.m_kakunin.Multiline = true;
            this.m_kakunin.Name = "m_kakunin";
            this.m_kakunin.Size = new System.Drawing.Size(406, 51);
            this.m_kakunin.TabIndex = 2;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 23);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 12);
            this.label15.TabIndex = 182;
            this.label15.Text = "タイマー";
            // 
            // Form_IncidentInsert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 620);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.m_systemno);
            this.Controls.Add(this.m_systemCombo);
            this.Controls.Add(this.m_statuscheck);
            this.Controls.Add(this.m_matflgCombo);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.m_enddate);
            this.Controls.Add(this.m_fukyudate);
            this.Controls.Add(this.m_tehaidate);
            this.Controls.Add(this.m_uketukedate);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.m_incident_kubun_combo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.m_hostno);
            this.Controls.Add(this.m_hostCombo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.m_siteno);
            this.Controls.Add(this.m_siteCombo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_content);
            this.Controls.Add(this.m_userno);
            this.Controls.Add(this.m_usernameCombo);
            this.Controls.Add(this.m_idlabel);
            this.Controls.Add(this.m_labelinputOpe);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.m_ScubeID);
            this.Controls.Add(this.m_matcommand);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_MSMSno);
            this.Controls.Add(this.label2);
            this.Name = "Form_IncidentInsert";
            this.Text = "インシデント登録";
            this.Load += new System.EventHandler(this.Form_IncidentInsert_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_content;
        private System.Windows.Forms.TextBox m_userno;
        private System.Windows.Forms.ComboBox m_usernameCombo;
        private System.Windows.Forms.TextBox m_idlabel;
        private System.Windows.Forms.TextBox m_labelinputOpe;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox m_ScubeID;
        private System.Windows.Forms.TextBox m_matcommand;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_MSMSno;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox m_siteno;
        private System.Windows.Forms.ComboBox m_siteCombo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox m_hostno;
        private System.Windows.Forms.ComboBox m_hostCombo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox m_incident_kubun_combo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker m_uketukedate;
        private System.Windows.Forms.DateTimePicker m_tehaidate;
        private System.Windows.Forms.DateTimePicker m_fukyudate;
        private System.Windows.Forms.DateTimePicker m_enddate;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox m_matflgCombo;
        private System.Windows.Forms.CheckBox m_statuscheck;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox m_systemno;
        private System.Windows.Forms.ComboBox m_systemCombo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox m_kakunin;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DateTimePicker m_timerpicker;
    }
}