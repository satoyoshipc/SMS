namespace SMSサンプル
{
    partial class Form_kaisenInsert
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
            this.m_serviceid = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.m_servicetype = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.m_isp = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.m_hostno = new System.Windows.Forms.TextBox();
            this.m_hostCombo = new System.Windows.Forms.ComboBox();
            this.m_systemno = new System.Windows.Forms.TextBox();
            this.m_userno = new System.Windows.Forms.TextBox();
            this.m_systemCombo = new System.Windows.Forms.ComboBox();
            this.m_usernameCombo = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.m_idlabel = new System.Windows.Forms.TextBox();
            this.m_kaisenid = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_labelinputOpe = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.m_kaisensyubetu = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.m_career = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_siteno = new System.Windows.Forms.TextBox();
            this.m_siteCombo = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.m_statusCombo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_serviceid
            // 
            this.m_serviceid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_serviceid.Location = new System.Drawing.Point(111, 273);
            this.m_serviceid.Name = "m_serviceid";
            this.m_serviceid.Size = new System.Drawing.Size(298, 19);
            this.m_serviceid.TabIndex = 10;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(11, 275);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 128;
            this.label13.Text = "サービスID";
            // 
            // m_servicetype
            // 
            this.m_servicetype.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_servicetype.Location = new System.Drawing.Point(111, 250);
            this.m_servicetype.Name = "m_servicetype";
            this.m_servicetype.Size = new System.Drawing.Size(298, 19);
            this.m_servicetype.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 252);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 12);
            this.label12.TabIndex = 126;
            this.label12.Text = "サービス種別";
            // 
            // m_isp
            // 
            this.m_isp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_isp.Location = new System.Drawing.Point(111, 227);
            this.m_isp.Name = "m_isp";
            this.m_isp.Size = new System.Drawing.Size(298, 19);
            this.m_isp.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 231);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(22, 12);
            this.label9.TabIndex = 120;
            this.label9.Text = "ISP";
            // 
            // m_hostno
            // 
            this.m_hostno.Location = new System.Drawing.Point(111, 111);
            this.m_hostno.Name = "m_hostno";
            this.m_hostno.ReadOnly = true;
            this.m_hostno.Size = new System.Drawing.Size(44, 19);
            this.m_hostno.TabIndex = 119;
            this.m_hostno.TabStop = false;
            // 
            // m_hostCombo
            // 
            this.m_hostCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_hostCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_hostCombo.FormattingEnabled = true;
            this.m_hostCombo.Location = new System.Drawing.Point(161, 111);
            this.m_hostCombo.Name = "m_hostCombo";
            this.m_hostCombo.Size = new System.Drawing.Size(250, 20);
            this.m_hostCombo.TabIndex = 3;
            this.m_hostCombo.SelectionChangeCommitted += new System.EventHandler(this.m_hostCombo_SelectionChangeCommitted);
            // 
            // m_systemno
            // 
            this.m_systemno.Location = new System.Drawing.Point(111, 67);
            this.m_systemno.Name = "m_systemno";
            this.m_systemno.ReadOnly = true;
            this.m_systemno.Size = new System.Drawing.Size(44, 19);
            this.m_systemno.TabIndex = 117;
            this.m_systemno.TabStop = false;
            // 
            // m_userno
            // 
            this.m_userno.Location = new System.Drawing.Point(111, 46);
            this.m_userno.Name = "m_userno";
            this.m_userno.ReadOnly = true;
            this.m_userno.Size = new System.Drawing.Size(44, 19);
            this.m_userno.TabIndex = 116;
            this.m_userno.TabStop = false;
            // 
            // m_systemCombo
            // 
            this.m_systemCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_systemCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_systemCombo.FormattingEnabled = true;
            this.m_systemCombo.Location = new System.Drawing.Point(161, 67);
            this.m_systemCombo.Name = "m_systemCombo";
            this.m_systemCombo.Size = new System.Drawing.Size(250, 20);
            this.m_systemCombo.TabIndex = 1;
            this.m_systemCombo.SelectionChangeCommitted += new System.EventHandler(this.m_systemCombo_SelectionChangeCommitted);
            // 
            // m_usernameCombo
            // 
            this.m_usernameCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_usernameCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_usernameCombo.FormattingEnabled = true;
            this.m_usernameCombo.Location = new System.Drawing.Point(161, 45);
            this.m_usernameCombo.Name = "m_usernameCombo";
            this.m_usernameCombo.Size = new System.Drawing.Size(250, 20);
            this.m_usernameCombo.TabIndex = 0;
            this.m_usernameCombo.SelectionChangeCommitted += new System.EventHandler(this.m_usernameCombo_SelectionChangeCommitted);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 12);
            this.label8.TabIndex = 113;
            this.label8.Text = "システム名";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 112;
            this.label7.Text = "ユーザ名";
            // 
            // m_idlabel
            // 
            this.m_idlabel.Location = new System.Drawing.Point(111, 296);
            this.m_idlabel.Name = "m_idlabel";
            this.m_idlabel.ReadOnly = true;
            this.m_idlabel.Size = new System.Drawing.Size(44, 19);
            this.m_idlabel.TabIndex = 111;
            this.m_idlabel.TabStop = false;
            // 
            // m_kaisenid
            // 
            this.m_kaisenid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_kaisenid.Location = new System.Drawing.Point(111, 205);
            this.m_kaisenid.Name = "m_kaisenid";
            this.m_kaisenid.Size = new System.Drawing.Size(298, 19);
            this.m_kaisenid.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 210);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 12);
            this.label6.TabIndex = 108;
            this.label6.Text = "回線ID";
            // 
            // m_labelinputOpe
            // 
            this.m_labelinputOpe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_labelinputOpe.Location = new System.Drawing.Point(161, 296);
            this.m_labelinputOpe.Name = "m_labelinputOpe";
            this.m_labelinputOpe.ReadOnly = true;
            this.m_labelinputOpe.Size = new System.Drawing.Size(250, 19);
            this.m_labelinputOpe.TabIndex = 107;
            this.m_labelinputOpe.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 298);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 12);
            this.label5.TabIndex = 106;
            this.label5.Text = "オペレータ";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(210, 319);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "登録";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(314, 319);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "キャンセル";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // m_kaisensyubetu
            // 
            this.m_kaisensyubetu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_kaisensyubetu.Location = new System.Drawing.Point(111, 183);
            this.m_kaisensyubetu.Name = "m_kaisensyubetu";
            this.m_kaisensyubetu.Size = new System.Drawing.Size(298, 19);
            this.m_kaisensyubetu.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "CSV登録";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 99;
            this.label3.Text = "回線種別";
            // 
            // m_career
            // 
            this.m_career.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_career.Location = new System.Drawing.Point(111, 160);
            this.m_career.Name = "m_career";
            this.m_career.Size = new System.Drawing.Size(298, 19);
            this.m_career.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 12);
            this.label2.TabIndex = 97;
            this.label2.Text = "キャリア";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 12);
            this.label1.TabIndex = 96;
            this.label1.Text = "ホスト名";
            // 
            // m_siteno
            // 
            this.m_siteno.Location = new System.Drawing.Point(111, 89);
            this.m_siteno.Name = "m_siteno";
            this.m_siteno.ReadOnly = true;
            this.m_siteno.Size = new System.Drawing.Size(44, 19);
            this.m_siteno.TabIndex = 132;
            this.m_siteno.TabStop = false;
            // 
            // m_siteCombo
            // 
            this.m_siteCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_siteCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_siteCombo.FormattingEnabled = true;
            this.m_siteCombo.Location = new System.Drawing.Point(161, 89);
            this.m_siteCombo.Name = "m_siteCombo";
            this.m_siteCombo.Size = new System.Drawing.Size(250, 20);
            this.m_siteCombo.TabIndex = 2;
            this.m_siteCombo.SelectionChangeCommitted += new System.EventHandler(this.m_siteCombo_SelectionChangeCommitted);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 92);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 130;
            this.label14.Text = "拠点名";
            // 
            // m_statusCombo
            // 
            this.m_statusCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_statusCombo.FormattingEnabled = true;
            this.m_statusCombo.Items.AddRange(new object[] {
            "有効",
            "無効"});
            this.m_statusCombo.Location = new System.Drawing.Point(111, 135);
            this.m_statusCombo.Name = "m_statusCombo";
            this.m_statusCombo.Size = new System.Drawing.Size(68, 20);
            this.m_statusCombo.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 136;
            this.label4.Text = "有効/無効";
            // 
            // Form_kaisenInsert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 347);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_statusCombo);
            this.Controls.Add(this.m_siteno);
            this.Controls.Add(this.m_siteCombo);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.m_serviceid);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.m_servicetype);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.m_isp);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.m_hostno);
            this.Controls.Add(this.m_hostCombo);
            this.Controls.Add(this.m_systemno);
            this.Controls.Add(this.m_userno);
            this.Controls.Add(this.m_systemCombo);
            this.Controls.Add(this.m_usernameCombo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.m_idlabel);
            this.Controls.Add(this.m_kaisenid);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.m_labelinputOpe);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.m_kaisensyubetu);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_career);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form_kaisenInsert";
            this.Text = "回線情報登録";
            this.Load += new System.EventHandler(this.Form_kaisenInsert_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_serviceid;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox m_servicetype;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox m_isp;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox m_hostno;
        private System.Windows.Forms.ComboBox m_hostCombo;
        private System.Windows.Forms.TextBox m_systemno;
        private System.Windows.Forms.TextBox m_userno;
        private System.Windows.Forms.ComboBox m_systemCombo;
        private System.Windows.Forms.ComboBox m_usernameCombo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox m_idlabel;
        private System.Windows.Forms.TextBox m_kaisenid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox m_labelinputOpe;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox m_kaisensyubetu;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_career;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_siteno;
        private System.Windows.Forms.ComboBox m_siteCombo;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox m_statusCombo;
        private System.Windows.Forms.Label label4;
    }
}