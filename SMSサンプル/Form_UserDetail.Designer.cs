﻿namespace moss_AP
{
    partial class Form_UserDetail
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.m_selectBtn = new System.Windows.Forms.Button();
            this.m_selecttext = new System.Windows.Forms.TextBox();
            this.m_selectKoumoku = new System.Windows.Forms.ComboBox();
            this.m_Customer_List = new System.Windows.Forms.ListView();
            this.m_umu_check = new System.Windows.Forms.CheckBox();
            this.m_tantouMente_link = new System.Windows.Forms.LinkLabel();
            this.m_customerID = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.m_deleteBtn = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.m_tantouList = new System.Windows.Forms.ListView();
            this.m_updateOpe = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_update = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.m_reportCombo = new System.Windows.Forms.ComboBox();
            this.m_statusCombo = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.m_biko = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.m_username_Ryaku = new System.Windows.Forms.TextBox();
            this.m_username_kana = new System.Windows.Forms.TextBox();
            this.m_username = new System.Windows.Forms.TextBox();
            this.m_userno = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_tantou_count = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_tantou_count);
            this.splitContainer1.Panel2.Controls.Add(this.m_umu_check);
            this.splitContainer1.Panel2.Controls.Add(this.m_tantouMente_link);
            this.splitContainer1.Panel2.Controls.Add(this.m_customerID);
            this.splitContainer1.Panel2.Controls.Add(this.label11);
            this.splitContainer1.Panel2.Controls.Add(this.m_deleteBtn);
            this.splitContainer1.Panel2.Controls.Add(this.label10);
            this.splitContainer1.Panel2.Controls.Add(this.m_tantouList);
            this.splitContainer1.Panel2.Controls.Add(this.m_updateOpe);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.m_update);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.m_reportCombo);
            this.splitContainer1.Panel2.Controls.Add(this.m_statusCombo);
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.Controls.Add(this.m_biko);
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            this.splitContainer1.Panel2.Controls.Add(this.m_username_Ryaku);
            this.splitContainer1.Panel2.Controls.Add(this.m_username_kana);
            this.splitContainer1.Panel2.Controls.Add(this.m_username);
            this.splitContainer1.Panel2.Controls.Add(this.m_userno);
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(846, 625);
            this.splitContainer1.SplitterDistance = 225;
            this.splitContainer1.TabIndex = 0;
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
            this.splitContainer2.Panel2.Controls.Add(this.m_Customer_List);
            this.splitContainer2.Size = new System.Drawing.Size(844, 223);
            this.splitContainer2.SplitterDistance = 33;
            this.splitContainer2.TabIndex = 1;
            this.splitContainer2.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer2_SplitterMoved);
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
            // m_Customer_List
            // 
            this.m_Customer_List.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_Customer_List.FullRowSelect = true;
            this.m_Customer_List.GridLines = true;
            this.m_Customer_List.Location = new System.Drawing.Point(0, 0);
            this.m_Customer_List.Name = "m_Customer_List";
            this.m_Customer_List.Size = new System.Drawing.Size(844, 186);
            this.m_Customer_List.TabIndex = 0;
            this.m_Customer_List.UseCompatibleStateImageBehavior = false;
            this.m_Customer_List.View = System.Windows.Forms.View.Details;
            this.m_Customer_List.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_Customer_List_ColumnClick);
            this.m_Customer_List.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.m_Customer_List_MouseDoubleClick);
            // 
            // m_umu_check
            // 
            this.m_umu_check.AutoSize = true;
            this.m_umu_check.Location = new System.Drawing.Point(752, 198);
            this.m_umu_check.Name = "m_umu_check";
            this.m_umu_check.Size = new System.Drawing.Size(81, 16);
            this.m_umu_check.TabIndex = 210;
            this.m_umu_check.Text = "完了を表示";
            this.m_umu_check.UseVisualStyleBackColor = true;
            this.m_umu_check.CheckedChanged += new System.EventHandler(this.m_umu_check_CheckedChanged);
            // 
            // m_tantouMente_link
            // 
            this.m_tantouMente_link.AutoSize = true;
            this.m_tantouMente_link.Location = new System.Drawing.Point(58, 198);
            this.m_tantouMente_link.Name = "m_tantouMente_link";
            this.m_tantouMente_link.Size = new System.Drawing.Size(31, 12);
            this.m_tantouMente_link.TabIndex = 79;
            this.m_tantouMente_link.TabStop = true;
            this.m_tantouMente_link.Text = "メンテ";
            this.m_tantouMente_link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_tantouMente_link_LinkClicked);
            // 
            // m_customerID
            // 
            this.m_customerID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_customerID.Location = new System.Drawing.Point(114, 29);
            this.m_customerID.Name = "m_customerID";
            this.m_customerID.Size = new System.Drawing.Size(208, 19);
            this.m_customerID.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 33);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 12);
            this.label11.TabIndex = 78;
            this.label11.Text = "カスタマID";
            // 
            // m_deleteBtn
            // 
            this.m_deleteBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_deleteBtn.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_deleteBtn.ForeColor = System.Drawing.Color.Red;
            this.m_deleteBtn.Location = new System.Drawing.Point(741, 3);
            this.m_deleteBtn.Name = "m_deleteBtn";
            this.m_deleteBtn.Size = new System.Drawing.Size(92, 33);
            this.m_deleteBtn.TabIndex = 11;
            this.m_deleteBtn.Text = "削除";
            this.m_deleteBtn.UseVisualStyleBackColor = true;
            this.m_deleteBtn.Click += new System.EventHandler(this.m_deleteBtn_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 198);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 76;
            this.label10.Text = "担当者";
            // 
            // m_tantouList
            // 
            this.m_tantouList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tantouList.GridLines = true;
            this.m_tantouList.Location = new System.Drawing.Point(11, 215);
            this.m_tantouList.Name = "m_tantouList";
            this.m_tantouList.Size = new System.Drawing.Size(822, 137);
            this.m_tantouList.TabIndex = 8;
            this.m_tantouList.UseCompatibleStateImageBehavior = false;
            this.m_tantouList.View = System.Windows.Forms.View.Details;
            this.m_tantouList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.m_tantouList_MouseDoubleClick);
            // 
            // m_updateOpe
            // 
            this.m_updateOpe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_updateOpe.Location = new System.Drawing.Point(632, 174);
            this.m_updateOpe.Name = "m_updateOpe";
            this.m_updateOpe.ReadOnly = true;
            this.m_updateOpe.Size = new System.Drawing.Size(166, 19);
            this.m_updateOpe.TabIndex = 10;
            this.m_updateOpe.TabStop = false;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(573, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 73;
            this.label6.Text = "更新者";
            // 
            // m_update
            // 
            this.m_update.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_update.Location = new System.Drawing.Point(632, 146);
            this.m_update.Name = "m_update";
            this.m_update.ReadOnly = true;
            this.m_update.Size = new System.Drawing.Size(166, 19);
            this.m_update.TabIndex = 9;
            this.m_update.TabStop = false;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(573, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 71;
            this.label5.Text = "更新日時";
            // 
            // m_reportCombo
            // 
            this.m_reportCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_reportCombo.FormattingEnabled = true;
            this.m_reportCombo.Items.AddRange(new object[] {
            "有効",
            "無効"});
            this.m_reportCombo.Location = new System.Drawing.Point(355, 128);
            this.m_reportCombo.Name = "m_reportCombo";
            this.m_reportCombo.Size = new System.Drawing.Size(79, 20);
            this.m_reportCombo.TabIndex = 6;
            // 
            // m_statusCombo
            // 
            this.m_statusCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_statusCombo.FormattingEnabled = true;
            this.m_statusCombo.Items.AddRange(new object[] {
            "有効",
            "無効"});
            this.m_statusCombo.Location = new System.Drawing.Point(114, 128);
            this.m_statusCombo.Name = "m_statusCombo";
            this.m_statusCombo.Size = new System.Drawing.Size(79, 20);
            this.m_statusCombo.TabIndex = 5;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(757, 358);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 33);
            this.button2.TabIndex = 10;
            this.button2.Text = "戻る";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(675, 358);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 33);
            this.button1.TabIndex = 12;
            this.button1.Text = "更新";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // m_biko
            // 
            this.m_biko.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_biko.Location = new System.Drawing.Point(114, 154);
            this.m_biko.Multiline = true;
            this.m_biko.Name = "m_biko";
            this.m_biko.Size = new System.Drawing.Size(436, 39);
            this.m_biko.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 131);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 64;
            this.label9.Text = "有効/無効";
            // 
            // m_username_Ryaku
            // 
            this.m_username_Ryaku.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_username_Ryaku.Location = new System.Drawing.Point(114, 103);
            this.m_username_Ryaku.Name = "m_username_Ryaku";
            this.m_username_Ryaku.Size = new System.Drawing.Size(436, 19);
            this.m_username_Ryaku.TabIndex = 4;
            // 
            // m_username_kana
            // 
            this.m_username_kana.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_username_kana.Location = new System.Drawing.Point(114, 79);
            this.m_username_kana.Name = "m_username_kana";
            this.m_username_kana.Size = new System.Drawing.Size(436, 19);
            this.m_username_kana.TabIndex = 3;
            // 
            // m_username
            // 
            this.m_username.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_username.Location = new System.Drawing.Point(114, 54);
            this.m_username.Name = "m_username";
            this.m_username.Size = new System.Drawing.Size(436, 19);
            this.m_username.TabIndex = 2;
            // 
            // m_userno
            // 
            this.m_userno.Location = new System.Drawing.Point(114, 5);
            this.m_userno.Name = "m_userno";
            this.m_userno.ReadOnly = true;
            this.m_userno.Size = new System.Drawing.Size(122, 19);
            this.m_userno.TabIndex = 0;
            this.m_userno.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 153);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "備考";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(299, 131);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 12);
            this.label8.TabIndex = 5;
            this.label8.Text = "SLO対象";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "カスタマ名略称";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "カスタマ名カナ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "カスタマ名";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "カスタマ通番";
            // 
            // m_tantou_count
            // 
            this.m_tantou_count.AutoSize = true;
            this.m_tantou_count.Location = new System.Drawing.Point(112, 199);
            this.m_tantou_count.Name = "m_tantou_count";
            this.m_tantou_count.Size = new System.Drawing.Size(17, 12);
            this.m_tantou_count.TabIndex = 211;
            this.m_tantou_count.Text = "件";
            // 
            // Form_UserDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 625);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form_UserDetail";
            this.Text = "カスタマ情報";
            this.Load += new System.EventHandler(this.Form_UserDetail_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox m_username_Ryaku;
        private System.Windows.Forms.TextBox m_username_kana;
        private System.Windows.Forms.TextBox m_username;
        private System.Windows.Forms.TextBox m_userno;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_biko;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ComboBox m_reportCombo;
        private System.Windows.Forms.ComboBox m_statusCombo;
        private System.Windows.Forms.ListView m_Customer_List;
        private System.Windows.Forms.TextBox m_updateOpe;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox m_update;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button m_selectBtn;
        private System.Windows.Forms.TextBox m_selecttext;
        private System.Windows.Forms.ComboBox m_selectKoumoku;
        private System.Windows.Forms.ListView m_tantouList;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button m_deleteBtn;
        private System.Windows.Forms.TextBox m_customerID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.LinkLabel m_tantouMente_link;
        private System.Windows.Forms.CheckBox m_umu_check;
        private System.Windows.Forms.Label m_tantou_count;
    }
}