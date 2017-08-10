namespace SMSサンプル
{
    partial class Form_hostInsert
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
            this.m_systemno = new System.Windows.Forms.TextBox();
            this.m_userno = new System.Windows.Forms.TextBox();
            this.m_systemCombo = new System.Windows.Forms.ComboBox();
            this.m_usernameCombo = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.m_idlabel = new System.Windows.Forms.TextBox();
            this.m_location = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_labelinputOpe = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.m_usefor = new System.Windows.Forms.TextBox();
            this.m_biko = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_hostname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_siteno = new System.Windows.Forms.TextBox();
            this.m_siteCombo = new System.Windows.Forms.ComboBox();
            this.m_settikikiID = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.m_startdate = new System.Windows.Forms.DateTimePicker();
            this.m_enddate = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.m_hosyuno = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.m_hosyuinfo = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.m_statusCombo = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.m_device = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_systemno
            // 
            this.m_systemno.Location = new System.Drawing.Point(118, 37);
            this.m_systemno.Name = "m_systemno";
            this.m_systemno.ReadOnly = true;
            this.m_systemno.Size = new System.Drawing.Size(44, 19);
            this.m_systemno.TabIndex = 2;
            this.m_systemno.TabStop = false;
            // 
            // m_userno
            // 
            this.m_userno.Location = new System.Drawing.Point(118, 13);
            this.m_userno.Name = "m_userno";
            this.m_userno.ReadOnly = true;
            this.m_userno.Size = new System.Drawing.Size(44, 19);
            this.m_userno.TabIndex = 0;
            this.m_userno.TabStop = false;
            // 
            // m_systemCombo
            // 
            this.m_systemCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_systemCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_systemCombo.FormattingEnabled = true;
            this.m_systemCombo.Location = new System.Drawing.Point(168, 37);
            this.m_systemCombo.Name = "m_systemCombo";
            this.m_systemCombo.Size = new System.Drawing.Size(249, 20);
            this.m_systemCombo.TabIndex = 3;
            this.m_systemCombo.SelectionChangeCommitted += new System.EventHandler(this.m_systemCombo_SelectionChangeCommitted);
            // 
            // m_usernameCombo
            // 
            this.m_usernameCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_usernameCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_usernameCombo.FormattingEnabled = true;
            this.m_usernameCombo.Location = new System.Drawing.Point(168, 12);
            this.m_usernameCombo.Name = "m_usernameCombo";
            this.m_usernameCombo.Size = new System.Drawing.Size(249, 20);
            this.m_usernameCombo.TabIndex = 1;
            this.m_usernameCombo.SelectionChangeCommitted += new System.EventHandler(this.m_usernameCombo_SelectionChangeCommitted);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 12);
            this.label8.TabIndex = 79;
            this.label8.Text = "システム名";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 12);
            this.label7.TabIndex = 78;
            this.label7.Text = "カスタマ名";
            // 
            // m_idlabel
            // 
            this.m_idlabel.Location = new System.Drawing.Point(118, 405);
            this.m_idlabel.Name = "m_idlabel";
            this.m_idlabel.ReadOnly = true;
            this.m_idlabel.Size = new System.Drawing.Size(44, 19);
            this.m_idlabel.TabIndex = 17;
            this.m_idlabel.TabStop = false;
            // 
            // m_location
            // 
            this.m_location.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_location.Location = new System.Drawing.Point(118, 163);
            this.m_location.Name = "m_location";
            this.m_location.Size = new System.Drawing.Size(297, 19);
            this.m_location.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 74;
            this.label6.Text = "設置場所";
            // 
            // m_labelinputOpe
            // 
            this.m_labelinputOpe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_labelinputOpe.Location = new System.Drawing.Point(168, 405);
            this.m_labelinputOpe.Name = "m_labelinputOpe";
            this.m_labelinputOpe.ReadOnly = true;
            this.m_labelinputOpe.Size = new System.Drawing.Size(249, 19);
            this.m_labelinputOpe.TabIndex = 18;
            this.m_labelinputOpe.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 408);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 12);
            this.label5.TabIndex = 72;
            this.label5.Text = "オペレータ";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(221, 431);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 23);
            this.button3.TabIndex = 19;
            this.button3.Text = "登録";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(322, 430);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "キャンセル";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // m_usefor
            // 
            this.m_usefor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_usefor.Location = new System.Drawing.Point(118, 185);
            this.m_usefor.Name = "m_usefor";
            this.m_usefor.Size = new System.Drawing.Size(297, 19);
            this.m_usefor.TabIndex = 10;
            // 
            // m_biko
            // 
            this.m_biko.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_biko.Location = new System.Drawing.Point(118, 342);
            this.m_biko.Multiline = true;
            this.m_biko.Name = "m_biko";
            this.m_biko.Size = new System.Drawing.Size(297, 59);
            this.m_biko.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 345);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 66;
            this.label4.Text = "備考";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 65;
            this.label3.Text = "用途";
            // 
            // m_hostname
            // 
            this.m_hostname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_hostname.Location = new System.Drawing.Point(118, 115);
            this.m_hostname.Name = "m_hostname";
            this.m_hostname.Size = new System.Drawing.Size(297, 19);
            this.m_hostname.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 12);
            this.label2.TabIndex = 63;
            this.label2.Text = "ホスト名";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 61;
            this.label1.Text = "拠点名";
            // 
            // m_siteno
            // 
            this.m_siteno.Location = new System.Drawing.Point(118, 63);
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
            this.m_siteCombo.Location = new System.Drawing.Point(168, 63);
            this.m_siteCombo.Name = "m_siteCombo";
            this.m_siteCombo.Size = new System.Drawing.Size(249, 20);
            this.m_siteCombo.TabIndex = 5;
            this.m_siteCombo.SelectionChangeCommitted += new System.EventHandler(this.m_siteCombo_SelectionChangeCommitted);
            // 
            // m_settikikiID
            // 
            this.m_settikikiID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_settikikiID.Location = new System.Drawing.Point(118, 208);
            this.m_settikikiID.Name = "m_settikikiID";
            this.m_settikikiID.Size = new System.Drawing.Size(297, 19);
            this.m_settikikiID.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 212);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 12);
            this.label9.TabIndex = 86;
            this.label9.Text = "設置機器ID";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 236);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 12);
            this.label10.TabIndex = 88;
            this.label10.Text = "監視開始日時";
            // 
            // m_startdate
            // 
            this.m_startdate.CustomFormat = "yyyy年 M月 d日(ddd) HH:mm";
            this.m_startdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_startdate.Location = new System.Drawing.Point(118, 231);
            this.m_startdate.Name = "m_startdate";
            this.m_startdate.Size = new System.Drawing.Size(191, 19);
            this.m_startdate.TabIndex = 12;
            this.m_startdate.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // m_enddate
            // 
            this.m_enddate.CustomFormat = "yyyy年 M月 d日(ddd) HH:mm";
            this.m_enddate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_enddate.Location = new System.Drawing.Point(118, 255);
            this.m_enddate.Name = "m_enddate";
            this.m_enddate.Size = new System.Drawing.Size(191, 19);
            this.m_enddate.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(24, 259);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 12);
            this.label11.TabIndex = 90;
            this.label11.Text = "監視終了日時";
            // 
            // m_hosyuno
            // 
            this.m_hosyuno.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_hosyuno.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.m_hosyuno.Location = new System.Drawing.Point(118, 281);
            this.m_hosyuno.Name = "m_hosyuno";
            this.m_hosyuno.Size = new System.Drawing.Size(297, 19);
            this.m_hosyuno.TabIndex = 14;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(24, 284);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 12);
            this.label12.TabIndex = 92;
            this.label12.Text = "保守管理番号";
            // 
            // m_hosyuinfo
            // 
            this.m_hosyuinfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_hosyuinfo.Location = new System.Drawing.Point(118, 308);
            this.m_hosyuinfo.Name = "m_hosyuinfo";
            this.m_hosyuinfo.Size = new System.Drawing.Size(297, 19);
            this.m_hosyuinfo.TabIndex = 15;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(24, 311);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 94;
            this.label13.Text = "保守情報";
            // 
            // m_statusCombo
            // 
            this.m_statusCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_statusCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_statusCombo.FormattingEnabled = true;
            this.m_statusCombo.Items.AddRange(new object[] {
            "有効",
            "無効"});
            this.m_statusCombo.Location = new System.Drawing.Point(118, 89);
            this.m_statusCombo.Name = "m_statusCombo";
            this.m_statusCombo.Size = new System.Drawing.Size(57, 20);
            this.m_statusCombo.TabIndex = 6;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(24, 92);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 12);
            this.label14.TabIndex = 97;
            this.label14.Text = "有効/無効";
            // 
            // m_device
            // 
            this.m_device.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_device.Location = new System.Drawing.Point(118, 139);
            this.m_device.Name = "m_device";
            this.m_device.Size = new System.Drawing.Size(297, 19);
            this.m_device.TabIndex = 8;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(24, 143);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 12);
            this.label15.TabIndex = 99;
            this.label15.Text = "機種";
            // 
            // Form_hostInsert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 460);
            this.Controls.Add(this.m_device);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.m_statusCombo);
            this.Controls.Add(this.m_hosyuinfo);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.m_hosyuno);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.m_enddate);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.m_startdate);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.m_settikikiID);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.m_siteno);
            this.Controls.Add(this.m_siteCombo);
            this.Controls.Add(this.m_systemno);
            this.Controls.Add(this.m_userno);
            this.Controls.Add(this.m_systemCombo);
            this.Controls.Add(this.m_usernameCombo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.m_idlabel);
            this.Controls.Add(this.m_location);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.m_labelinputOpe);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.m_usefor);
            this.Controls.Add(this.m_biko);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_hostname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form_hostInsert";
            this.Text = "ホスト情報登録";
            this.Load += new System.EventHandler(this.Form_hostInsert_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_systemno;
        private System.Windows.Forms.TextBox m_userno;
        private System.Windows.Forms.ComboBox m_systemCombo;
        private System.Windows.Forms.ComboBox m_usernameCombo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox m_idlabel;
        private System.Windows.Forms.TextBox m_location;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox m_labelinputOpe;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox m_usefor;
        private System.Windows.Forms.TextBox m_biko;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_hostname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_siteno;
        private System.Windows.Forms.ComboBox m_siteCombo;
        private System.Windows.Forms.TextBox m_settikikiID;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker m_startdate;
        private System.Windows.Forms.DateTimePicker m_enddate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox m_hosyuno;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox m_hosyuinfo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox m_statusCombo;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox m_device;
        private System.Windows.Forms.Label label15;
    }
}