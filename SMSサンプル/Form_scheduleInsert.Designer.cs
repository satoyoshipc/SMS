namespace moss_AP
{
    partial class Form_scheduleInsert
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
            this.label1 = new System.Windows.Forms.Label();
            this.m_schedule_combo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_startDate = new System.Windows.Forms.DateTimePicker();
            this.m_endDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_radio_mukou = new System.Windows.Forms.RadioButton();
            this.m_radio_enable = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.m_timer_name = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.m_cencelBtn = new System.Windows.Forms.Button();
            this.m_alerm_group = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.m_soudpath = new System.Windows.Forms.TextBox();
            this.m_message = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_alermDate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.m_radio_month = new System.Windows.Forms.RadioButton();
            this.m_radio_week = new System.Windows.Forms.RadioButton();
            this.m_radio_day = new System.Windows.Forms.RadioButton();
            this.m_radio_hour = new System.Windows.Forms.RadioButton();
            this.m_radio_one = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.m_systemno = new System.Windows.Forms.TextBox();
            this.m_systemCombo = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.m_userno = new System.Windows.Forms.TextBox();
            this.m_usernameCombo = new System.Windows.Forms.ComboBox();
            this.m_idlabel = new System.Windows.Forms.TextBox();
            this.m_labelinputOpe = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.m_siteno = new System.Windows.Forms.TextBox();
            this.m_siteCombo = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.m_alerm_group.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "予定区分";
            // 
            // m_schedule_combo
            // 
            this.m_schedule_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_schedule_combo.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_schedule_combo.FormattingEnabled = true;
            this.m_schedule_combo.Items.AddRange(new object[] {
            "",
            "2:定期作業",
            "3:計画作業",
            "4:特別作業"});
            this.m_schedule_combo.Location = new System.Drawing.Point(88, 115);
            this.m_schedule_combo.Name = "m_schedule_combo";
            this.m_schedule_combo.Size = new System.Drawing.Size(447, 21);
            this.m_schedule_combo.TabIndex = 7;
            this.m_schedule_combo.SelectionChangeCommitted += new System.EventHandler(this.m_schedule_combo_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 437);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "開始日時";
            // 
            // m_startDate
            // 
            this.m_startDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_startDate.CustomFormat = "yyyy年M月d日(dddd) HH:mm";
            this.m_startDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_startDate.Location = new System.Drawing.Point(102, 433);
            this.m_startDate.Name = "m_startDate";
            this.m_startDate.Size = new System.Drawing.Size(206, 19);
            this.m_startDate.TabIndex = 11;
            // 
            // m_endDate
            // 
            this.m_endDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_endDate.CustomFormat = "yyyy年M月d日(dddd) HH:mm";
            this.m_endDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_endDate.Location = new System.Drawing.Point(102, 458);
            this.m_endDate.Name = "m_endDate";
            this.m_endDate.Size = new System.Drawing.Size(206, 19);
            this.m_endDate.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 461);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "終了日時";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_radio_mukou);
            this.groupBox1.Controls.Add(this.m_radio_enable);
            this.groupBox1.Location = new System.Drawing.Point(14, 143);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 50);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "有効/無効";
            // 
            // m_radio_mukou
            // 
            this.m_radio_mukou.AutoSize = true;
            this.m_radio_mukou.Location = new System.Drawing.Point(114, 22);
            this.m_radio_mukou.Name = "m_radio_mukou";
            this.m_radio_mukou.Size = new System.Drawing.Size(47, 16);
            this.m_radio_mukou.TabIndex = 1;
            this.m_radio_mukou.Text = "無効";
            this.m_radio_mukou.UseVisualStyleBackColor = true;
            // 
            // m_radio_enable
            // 
            this.m_radio_enable.AutoSize = true;
            this.m_radio_enable.Checked = true;
            this.m_radio_enable.Location = new System.Drawing.Point(40, 22);
            this.m_radio_enable.Name = "m_radio_enable";
            this.m_radio_enable.Size = new System.Drawing.Size(47, 16);
            this.m_radio_enable.TabIndex = 0;
            this.m_radio_enable.TabStop = true;
            this.m_radio_enable.Text = "有効";
            this.m_radio_enable.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 12);
            this.label5.TabIndex = 139;
            this.label5.Text = "タイトル";
            // 
            // m_timer_name
            // 
            this.m_timer_name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_timer_name.Location = new System.Drawing.Point(88, 90);
            this.m_timer_name.Multiline = true;
            this.m_timer_name.Name = "m_timer_name";
            this.m_timer_name.Size = new System.Drawing.Size(447, 19);
            this.m_timer_name.TabIndex = 6;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.Location = new System.Drawing.Point(370, 517);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 23);
            this.button3.TabIndex = 17;
            this.button3.Text = "登録";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // m_cencelBtn
            // 
            this.m_cencelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_cencelBtn.Location = new System.Drawing.Point(473, 517);
            this.m_cencelBtn.Name = "m_cencelBtn";
            this.m_cencelBtn.Size = new System.Drawing.Size(95, 23);
            this.m_cencelBtn.TabIndex = 18;
            this.m_cencelBtn.Text = "キャンセル";
            this.m_cencelBtn.UseVisualStyleBackColor = true;
            this.m_cencelBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // m_alerm_group
            // 
            this.m_alerm_group.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_alerm_group.Controls.Add(this.button4);
            this.m_alerm_group.Controls.Add(this.button1);
            this.m_alerm_group.Controls.Add(this.label4);
            this.m_alerm_group.Controls.Add(this.m_soudpath);
            this.m_alerm_group.Controls.Add(this.m_message);
            this.m_alerm_group.Controls.Add(this.label6);
            this.m_alerm_group.Controls.Add(this.m_alermDate);
            this.m_alerm_group.Controls.Add(this.label7);
            this.m_alerm_group.Controls.Add(this.m_radio_month);
            this.m_alerm_group.Controls.Add(this.m_radio_week);
            this.m_alerm_group.Controls.Add(this.m_radio_day);
            this.m_alerm_group.Controls.Add(this.m_radio_hour);
            this.m_alerm_group.Controls.Add(this.m_radio_one);
            this.m_alerm_group.Location = new System.Drawing.Point(14, 207);
            this.m_alerm_group.Name = "m_alerm_group";
            this.m_alerm_group.Size = new System.Drawing.Size(554, 220);
            this.m_alerm_group.TabIndex = 9;
            this.m_alerm_group.TabStop = false;
            this.m_alerm_group.Text = "タイマー";
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button4.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button4.Location = new System.Drawing.Point(460, 188);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(69, 23);
            this.button4.TabIndex = 12;
            this.button4.Text = "♪テスト";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(408, 188);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(47, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "参照";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "サウンド(wav)";
            // 
            // m_soudpath
            // 
            this.m_soudpath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_soudpath.Location = new System.Drawing.Point(96, 190);
            this.m_soudpath.Name = "m_soudpath";
            this.m_soudpath.Size = new System.Drawing.Size(306, 19);
            this.m_soudpath.TabIndex = 10;
            // 
            // m_message
            // 
            this.m_message.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_message.Location = new System.Drawing.Point(96, 78);
            this.m_message.Multiline = true;
            this.m_message.Name = "m_message";
            this.m_message.Size = new System.Drawing.Size(433, 106);
            this.m_message.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "作業内容";
            // 
            // m_alermDate
            // 
            this.m_alermDate.CustomFormat = "yyyy年M月d日(dddd) HH:mm";
            this.m_alermDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_alermDate.Location = new System.Drawing.Point(96, 49);
            this.m_alermDate.Name = "m_alermDate";
            this.m_alermDate.Size = new System.Drawing.Size(206, 19);
            this.m_alermDate.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "アラート日時♪";
            // 
            // m_radio_month
            // 
            this.m_radio_month.AutoSize = true;
            this.m_radio_month.Location = new System.Drawing.Point(292, 21);
            this.m_radio_month.Name = "m_radio_month";
            this.m_radio_month.Size = new System.Drawing.Size(47, 16);
            this.m_radio_month.TabIndex = 4;
            this.m_radio_month.Text = "毎月";
            this.m_radio_month.UseVisualStyleBackColor = true;
            this.m_radio_month.CheckedChanged += new System.EventHandler(this.m_radio_month_CheckedChanged);
            // 
            // m_radio_week
            // 
            this.m_radio_week.AutoSize = true;
            this.m_radio_week.Location = new System.Drawing.Point(230, 21);
            this.m_radio_week.Name = "m_radio_week";
            this.m_radio_week.Size = new System.Drawing.Size(47, 16);
            this.m_radio_week.TabIndex = 3;
            this.m_radio_week.Text = "毎週";
            this.m_radio_week.UseVisualStyleBackColor = true;
            this.m_radio_week.CheckedChanged += new System.EventHandler(this.m_radio_week_CheckedChanged);
            // 
            // m_radio_day
            // 
            this.m_radio_day.AutoSize = true;
            this.m_radio_day.Location = new System.Drawing.Point(168, 21);
            this.m_radio_day.Name = "m_radio_day";
            this.m_radio_day.Size = new System.Drawing.Size(47, 16);
            this.m_radio_day.TabIndex = 2;
            this.m_radio_day.Text = "毎日";
            this.m_radio_day.UseVisualStyleBackColor = true;
            this.m_radio_day.CheckedChanged += new System.EventHandler(this.m_radio_day_CheckedChanged);
            // 
            // m_radio_hour
            // 
            this.m_radio_hour.AutoSize = true;
            this.m_radio_hour.Location = new System.Drawing.Point(103, 21);
            this.m_radio_hour.Name = "m_radio_hour";
            this.m_radio_hour.Size = new System.Drawing.Size(47, 16);
            this.m_radio_hour.TabIndex = 1;
            this.m_radio_hour.Text = "毎時";
            this.m_radio_hour.UseVisualStyleBackColor = true;
            this.m_radio_hour.CheckedChanged += new System.EventHandler(this.m_radio_hour_CheckedChanged);
            // 
            // m_radio_one
            // 
            this.m_radio_one.AutoSize = true;
            this.m_radio_one.Checked = true;
            this.m_radio_one.Location = new System.Drawing.Point(40, 22);
            this.m_radio_one.Name = "m_radio_one";
            this.m_radio_one.Size = new System.Drawing.Size(41, 16);
            this.m_radio_one.TabIndex = 0;
            this.m_radio_one.TabStop = true;
            this.m_radio_one.Text = "1回";
            this.m_radio_one.UseVisualStyleBackColor = true;
            this.m_radio_one.CheckedChanged += new System.EventHandler(this.m_radio_one_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 40);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 12);
            this.label10.TabIndex = 184;
            this.label10.Text = "システム名";
            // 
            // m_systemno
            // 
            this.m_systemno.Location = new System.Drawing.Point(88, 37);
            this.m_systemno.Name = "m_systemno";
            this.m_systemno.ReadOnly = true;
            this.m_systemno.Size = new System.Drawing.Size(44, 19);
            this.m_systemno.TabIndex = 2;
            this.m_systemno.TabStop = false;
            // 
            // m_systemCombo
            // 
            this.m_systemCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_systemCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_systemCombo.FormattingEnabled = true;
            this.m_systemCombo.Location = new System.Drawing.Point(138, 36);
            this.m_systemCombo.Name = "m_systemCombo";
            this.m_systemCombo.Size = new System.Drawing.Size(397, 20);
            this.m_systemCombo.TabIndex = 3;
            this.m_systemCombo.SelectionChangeCommitted += new System.EventHandler(this.m_systemCombo_SelectionChangeCommitted);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 12);
            this.label8.TabIndex = 181;
            this.label8.Text = "カスタマ名";
            // 
            // m_userno
            // 
            this.m_userno.Location = new System.Drawing.Point(88, 13);
            this.m_userno.Name = "m_userno";
            this.m_userno.ReadOnly = true;
            this.m_userno.Size = new System.Drawing.Size(44, 19);
            this.m_userno.TabIndex = 0;
            this.m_userno.TabStop = false;
            // 
            // m_usernameCombo
            // 
            this.m_usernameCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_usernameCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_usernameCombo.FormattingEnabled = true;
            this.m_usernameCombo.Location = new System.Drawing.Point(138, 12);
            this.m_usernameCombo.Name = "m_usernameCombo";
            this.m_usernameCombo.Size = new System.Drawing.Size(397, 20);
            this.m_usernameCombo.TabIndex = 1;
            this.m_usernameCombo.SelectionChangeCommitted += new System.EventHandler(this.m_usernameCombo_SelectionChangeCommitted);
            // 
            // m_idlabel
            // 
            this.m_idlabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_idlabel.Location = new System.Drawing.Point(102, 491);
            this.m_idlabel.Name = "m_idlabel";
            this.m_idlabel.ReadOnly = true;
            this.m_idlabel.Size = new System.Drawing.Size(44, 19);
            this.m_idlabel.TabIndex = 15;
            this.m_idlabel.TabStop = false;
            // 
            // m_labelinputOpe
            // 
            this.m_labelinputOpe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_labelinputOpe.Location = new System.Drawing.Point(152, 491);
            this.m_labelinputOpe.Name = "m_labelinputOpe";
            this.m_labelinputOpe.ReadOnly = true;
            this.m_labelinputOpe.Size = new System.Drawing.Size(264, 19);
            this.m_labelinputOpe.TabIndex = 16;
            this.m_labelinputOpe.TabStop = false;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 494);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 12);
            this.label9.TabIndex = 14;
            this.label9.Text = "オペレータ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 64);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 190;
            this.label11.Text = "拠点名";
            // 
            // m_siteno
            // 
            this.m_siteno.Location = new System.Drawing.Point(88, 63);
            this.m_siteno.Name = "m_siteno";
            this.m_siteno.ReadOnly = true;
            this.m_siteno.Size = new System.Drawing.Size(44, 19);
            this.m_siteno.TabIndex = 4;
            this.m_siteno.TabStop = false;
            // 
            // m_siteCombo
            // 
            this.m_siteCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_siteCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_siteCombo.FormattingEnabled = true;
            this.m_siteCombo.Location = new System.Drawing.Point(138, 62);
            this.m_siteCombo.Name = "m_siteCombo";
            this.m_siteCombo.Size = new System.Drawing.Size(397, 20);
            this.m_siteCombo.TabIndex = 5;
            this.m_siteCombo.SelectionChangeCommitted += new System.EventHandler(this.m_siteCombo_SelectionChangeCommitted);
            // 
            // Form_scheduleInsert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 543);
            this.Controls.Add(this.m_siteno);
            this.Controls.Add(this.m_siteCombo);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.m_idlabel);
            this.Controls.Add(this.m_labelinputOpe);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.m_systemno);
            this.Controls.Add(this.m_systemCombo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.m_userno);
            this.Controls.Add(this.m_usernameCombo);
            this.Controls.Add(this.m_alerm_group);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.m_cencelBtn);
            this.Controls.Add(this.m_timer_name);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_endDate);
            this.Controls.Add(this.m_startDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_schedule_combo);
            this.Controls.Add(this.label1);
            this.Name = "Form_scheduleInsert";
            this.Text = "タイマー登録";
            this.Load += new System.EventHandler(this.Form_TimerInsert_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.m_alerm_group.ResumeLayout(false);
            this.m_alerm_group.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox m_schedule_combo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker m_startDate;
        private System.Windows.Forms.DateTimePicker m_endDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton m_radio_mukou;
        private System.Windows.Forms.RadioButton m_radio_enable;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox m_timer_name;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button m_cencelBtn;
        private System.Windows.Forms.GroupBox m_alerm_group;
        private System.Windows.Forms.RadioButton m_radio_month;
        private System.Windows.Forms.RadioButton m_radio_week;
        private System.Windows.Forms.RadioButton m_radio_day;
        private System.Windows.Forms.RadioButton m_radio_hour;
        private System.Windows.Forms.RadioButton m_radio_one;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox m_soudpath;
        private System.Windows.Forms.TextBox m_message;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker m_alermDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox m_systemno;
        private System.Windows.Forms.ComboBox m_systemCombo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox m_userno;
        private System.Windows.Forms.ComboBox m_usernameCombo;
        private System.Windows.Forms.TextBox m_idlabel;
        private System.Windows.Forms.TextBox m_labelinputOpe;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox m_siteno;
        private System.Windows.Forms.ComboBox m_siteCombo;
    }
}