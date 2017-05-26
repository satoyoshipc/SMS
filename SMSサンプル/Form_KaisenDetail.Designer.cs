namespace SMSサンプル
{
    partial class Form_KaisenDetail
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
            this.m_systemno = new System.Windows.Forms.TextBox();
            this.m_systemname = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.m_kaisentype = new System.Windows.Forms.TextBox();
            this.m_career = new System.Windows.Forms.TextBox();
            this.m_hostname = new System.Windows.Forms.TextBox();
            this.m_userno = new System.Windows.Forms.TextBox();
            this.m_cutomername = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_updateOpe = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_update = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.m_sitename = new System.Windows.Forms.TextBox();
            this.m_hostno = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_selectBtn = new System.Windows.Forms.Button();
            this.m_selecttext = new System.Windows.Forms.TextBox();
            this.m_selectKoumoku = new System.Windows.Forms.ComboBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.m_kaisenList = new System.Windows.Forms.ListView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.m_kaisenno = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.m_serviceID = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.m_kaisenID = new System.Windows.Forms.TextBox();
            this.m_siteno = new System.Windows.Forms.TextBox();
            this.m_serviceType = new System.Windows.Forms.TextBox();
            this.m_isp = new System.Windows.Forms.TextBox();
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
            "有効",
            "無効"});
            this.m_statusCombo.Location = new System.Drawing.Point(140, 116);
            this.m_statusCombo.Name = "m_statusCombo";
            this.m_statusCombo.Size = new System.Drawing.Size(68, 20);
            this.m_statusCombo.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(481, 38);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 12);
            this.label12.TabIndex = 116;
            this.label12.Text = "システム";
            // 
            // m_systemno
            // 
            this.m_systemno.Location = new System.Drawing.Point(537, 35);
            this.m_systemno.Name = "m_systemno";
            this.m_systemno.ReadOnly = true;
            this.m_systemno.Size = new System.Drawing.Size(45, 19);
            this.m_systemno.TabIndex = 115;
            // 
            // m_systemname
            // 
            this.m_systemname.Location = new System.Drawing.Point(588, 35);
            this.m_systemname.Name = "m_systemname";
            this.m_systemname.ReadOnly = true;
            this.m_systemname.Size = new System.Drawing.Size(182, 19);
            this.m_systemname.TabIndex = 114;
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
            this.label9.Location = new System.Drawing.Point(16, 119);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 12);
            this.label9.TabIndex = 111;
            this.label9.Text = "ステータス";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 141);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 110;
            this.label8.Text = "回線種別";
            // 
            // m_kaisentype
            // 
            this.m_kaisentype.Location = new System.Drawing.Point(140, 138);
            this.m_kaisentype.Name = "m_kaisentype";
            this.m_kaisentype.Size = new System.Drawing.Size(325, 19);
            this.m_kaisentype.TabIndex = 2;
            // 
            // m_career
            // 
            this.m_career.Location = new System.Drawing.Point(140, 95);
            this.m_career.Name = "m_career";
            this.m_career.Size = new System.Drawing.Size(325, 19);
            this.m_career.TabIndex = 0;
            // 
            // m_hostname
            // 
            this.m_hostname.Location = new System.Drawing.Point(191, 74);
            this.m_hostname.Name = "m_hostname";
            this.m_hostname.ReadOnly = true;
            this.m_hostname.Size = new System.Drawing.Size(274, 19);
            this.m_hostname.TabIndex = 3;
            // 
            // m_userno
            // 
            this.m_userno.Location = new System.Drawing.Point(140, 32);
            this.m_userno.Name = "m_userno";
            this.m_userno.ReadOnly = true;
            this.m_userno.Size = new System.Drawing.Size(47, 19);
            this.m_userno.TabIndex = 105;
            // 
            // m_cutomername
            // 
            this.m_cutomername.Location = new System.Drawing.Point(191, 33);
            this.m_cutomername.Name = "m_cutomername";
            this.m_cutomername.ReadOnly = true;
            this.m_cutomername.Size = new System.Drawing.Size(274, 19);
            this.m_cutomername.TabIndex = 1;
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
            this.m_updateOpe.Location = new System.Drawing.Point(537, 233);
            this.m_updateOpe.Name = "m_updateOpe";
            this.m_updateOpe.ReadOnly = true;
            this.m_updateOpe.Size = new System.Drawing.Size(233, 19);
            this.m_updateOpe.TabIndex = 102;
            this.m_updateOpe.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(479, 235);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 101;
            this.label6.Text = "更新者";
            // 
            // m_update
            // 
            this.m_update.Location = new System.Drawing.Point(537, 210);
            this.m_update.Name = "m_update";
            this.m_update.ReadOnly = true;
            this.m_update.Size = new System.Drawing.Size(233, 19);
            this.m_update.TabIndex = 100;
            this.m_update.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(479, 213);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 99;
            this.label5.Text = "更新日時";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(694, 258);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 33);
            this.button2.TabIndex = 8;
            this.button2.Text = "戻る";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(612, 258);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 33);
            this.button1.TabIndex = 7;
            this.button1.Text = "更新";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // m_sitename
            // 
            this.m_sitename.Location = new System.Drawing.Point(191, 53);
            this.m_sitename.Name = "m_sitename";
            this.m_sitename.ReadOnly = true;
            this.m_sitename.Size = new System.Drawing.Size(274, 19);
            this.m_sitename.TabIndex = 2;
            // 
            // m_hostno
            // 
            this.m_hostno.Location = new System.Drawing.Point(140, 74);
            this.m_hostno.Name = "m_hostno";
            this.m_hostno.ReadOnly = true;
            this.m_hostno.Size = new System.Drawing.Size(47, 19);
            this.m_hostno.TabIndex = 0;
            this.m_hostno.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 12);
            this.label3.TabIndex = 91;
            this.label3.Text = "キャリア";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 12);
            this.label2.TabIndex = 90;
            this.label2.Text = "ホスト名";
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
            this.splitContainer2.Panel2.Controls.Add(this.m_kaisenList);
            this.splitContainer2.Size = new System.Drawing.Size(797, 244);
            this.splitContainer2.SplitterDistance = 33;
            this.splitContainer2.TabIndex = 1;
            // 
            // m_kaisenList
            // 
            this.m_kaisenList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_kaisenList.FullRowSelect = true;
            this.m_kaisenList.GridLines = true;
            this.m_kaisenList.Location = new System.Drawing.Point(0, 0);
            this.m_kaisenList.Name = "m_kaisenList";
            this.m_kaisenList.Size = new System.Drawing.Size(797, 207);
            this.m_kaisenList.TabIndex = 0;
            this.m_kaisenList.UseCompatibleStateImageBehavior = false;
            this.m_kaisenList.View = System.Windows.Forms.View.Details;
            this.m_kaisenList.DoubleClick += new System.EventHandler(this.m_host_List_DoubleClick);
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
            this.splitContainer1.Panel2.Controls.Add(this.m_serviceType);
            this.splitContainer1.Panel2.Controls.Add(this.m_isp);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.m_kaisenno);
            this.splitContainer1.Panel2.Controls.Add(this.label16);
            this.splitContainer1.Panel2.Controls.Add(this.label17);
            this.splitContainer1.Panel2.Controls.Add(this.label14);
            this.splitContainer1.Panel2.Controls.Add(this.m_serviceID);
            this.splitContainer1.Panel2.Controls.Add(this.label11);
            this.splitContainer1.Panel2.Controls.Add(this.m_kaisenID);
            this.splitContainer1.Panel2.Controls.Add(this.m_siteno);
            this.splitContainer1.Panel2.Controls.Add(this.m_statusCombo);
            this.splitContainer1.Panel2.Controls.Add(this.label12);
            this.splitContainer1.Panel2.Controls.Add(this.m_systemno);
            this.splitContainer1.Panel2.Controls.Add(this.m_systemname);
            this.splitContainer1.Panel2.Controls.Add(this.label10);
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Panel2.Controls.Add(this.m_kaisentype);
            this.splitContainer1.Panel2.Controls.Add(this.m_career);
            this.splitContainer1.Panel2.Controls.Add(this.m_hostname);
            this.splitContainer1.Panel2.Controls.Add(this.m_userno);
            this.splitContainer1.Panel2.Controls.Add(this.m_cutomername);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.m_updateOpe);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.m_update);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.Controls.Add(this.m_sitename);
            this.splitContainer1.Panel2.Controls.Add(this.m_hostno);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(799, 554);
            this.splitContainer1.SplitterDistance = 246;
            this.splitContainer1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 165;
            this.label1.Text = "回線通番";
            // 
            // m_kaisenno
            // 
            this.m_kaisenno.Location = new System.Drawing.Point(140, 8);
            this.m_kaisenno.Name = "m_kaisenno";
            this.m_kaisenno.ReadOnly = true;
            this.m_kaisenno.Size = new System.Drawing.Size(99, 19);
            this.m_kaisenno.TabIndex = 164;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(16, 209);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(66, 12);
            this.label16.TabIndex = 163;
            this.label16.Text = "サービス種別";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(16, 186);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(22, 12);
            this.label17.TabIndex = 162;
            this.label17.Text = "ISP";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(16, 233);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 122;
            this.label14.Text = "サービスID";
            // 
            // m_serviceID
            // 
            this.m_serviceID.Location = new System.Drawing.Point(140, 229);
            this.m_serviceID.Name = "m_serviceID";
            this.m_serviceID.Size = new System.Drawing.Size(325, 19);
            this.m_serviceID.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 164);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 12);
            this.label11.TabIndex = 120;
            this.label11.Text = "回線ID";
            // 
            // m_kaisenID
            // 
            this.m_kaisenID.Location = new System.Drawing.Point(140, 161);
            this.m_kaisenID.Name = "m_kaisenID";
            this.m_kaisenID.Size = new System.Drawing.Size(325, 19);
            this.m_kaisenID.TabIndex = 3;
            // 
            // m_siteno
            // 
            this.m_siteno.Location = new System.Drawing.Point(140, 53);
            this.m_siteno.Name = "m_siteno";
            this.m_siteno.ReadOnly = true;
            this.m_siteno.Size = new System.Drawing.Size(47, 19);
            this.m_siteno.TabIndex = 118;
            // 
            // m_serviceType
            // 
            this.m_serviceType.Location = new System.Drawing.Point(140, 207);
            this.m_serviceType.Name = "m_serviceType";
            this.m_serviceType.Size = new System.Drawing.Size(325, 19);
            this.m_serviceType.TabIndex = 5;
            // 
            // m_isp
            // 
            this.m_isp.Location = new System.Drawing.Point(140, 184);
            this.m_isp.Name = "m_isp";
            this.m_isp.Size = new System.Drawing.Size(325, 19);
            this.m_isp.TabIndex = 4;
            // 
            // Form_KaisenDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 554);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form_KaisenDetail";
            this.Text = "回線情報";
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
        private System.Windows.Forms.TextBox m_systemno;
        private System.Windows.Forms.TextBox m_systemname;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox m_kaisentype;
        private System.Windows.Forms.TextBox m_career;
        private System.Windows.Forms.TextBox m_hostname;
        private System.Windows.Forms.TextBox m_userno;
        private System.Windows.Forms.TextBox m_cutomername;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox m_updateOpe;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox m_update;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox m_sitename;
        private System.Windows.Forms.TextBox m_hostno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button m_selectBtn;
        private System.Windows.Forms.TextBox m_selecttext;
        private System.Windows.Forms.ComboBox m_selectKoumoku;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListView m_kaisenList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox m_siteno;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox m_serviceID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox m_kaisenID;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_kaisenno;
        private System.Windows.Forms.TextBox m_serviceType;
        private System.Windows.Forms.TextBox m_isp;
    }
}