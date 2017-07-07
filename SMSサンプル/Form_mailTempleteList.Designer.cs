namespace SMSサンプル
{
    partial class Form_mailTempleteList
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
            this.m_mailTempleteList = new System.Windows.Forms.ListView();
            this.label4 = new System.Windows.Forms.Label();
            this.m_account = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.m_deleteBtn = new System.Windows.Forms.Button();
            this.m_updateOpe = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_update = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Bcc = new System.Windows.Forms.LinkLabel();
            this.Cc = new System.Windows.Forms.LinkLabel();
            this.To = new System.Windows.Forms.LinkLabel();
            this.m_Bcc_list = new System.Windows.Forms.ListView();
            this.m_Cc_list = new System.Windows.Forms.ListView();
            this.m_To_list = new System.Windows.Forms.ListView();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.m_temp5 = new System.Windows.Forms.TextBox();
            this.m_temp4 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.m_temp3 = new System.Windows.Forms.TextBox();
            this.m_temp2 = new System.Windows.Forms.TextBox();
            this.m_temp1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.m_templetename = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.m_tempno = new System.Windows.Forms.TextBox();
            this.m_mailDispBtn = new System.Windows.Forms.Button();
            this.m_cancel = new System.Windows.Forms.Button();
            this.m_OK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.m_subject = new System.Windows.Forms.TextBox();
            this.m_body = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_mailTempleteList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.m_account);
            this.splitContainer1.Panel2.Controls.Add(this.label12);
            this.splitContainer1.Panel2.Controls.Add(this.m_deleteBtn);
            this.splitContainer1.Panel2.Controls.Add(this.m_updateOpe);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.m_update);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.Bcc);
            this.splitContainer1.Panel2.Controls.Add(this.Cc);
            this.splitContainer1.Panel2.Controls.Add(this.To);
            this.splitContainer1.Panel2.Controls.Add(this.m_Bcc_list);
            this.splitContainer1.Panel2.Controls.Add(this.m_Cc_list);
            this.splitContainer1.Panel2.Controls.Add(this.m_To_list);
            this.splitContainer1.Panel2.Controls.Add(this.label10);
            this.splitContainer1.Panel2.Controls.Add(this.label11);
            this.splitContainer1.Panel2.Controls.Add(this.m_temp5);
            this.splitContainer1.Panel2.Controls.Add(this.m_temp4);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Panel2.Controls.Add(this.m_temp3);
            this.splitContainer1.Panel2.Controls.Add(this.m_temp2);
            this.splitContainer1.Panel2.Controls.Add(this.m_temp1);
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            this.splitContainer1.Panel2.Controls.Add(this.m_templetename);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.m_tempno);
            this.splitContainer1.Panel2.Controls.Add(this.m_mailDispBtn);
            this.splitContainer1.Panel2.Controls.Add(this.m_cancel);
            this.splitContainer1.Panel2.Controls.Add(this.m_OK);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.m_subject);
            this.splitContainer1.Panel2.Controls.Add(this.m_body);
            this.splitContainer1.Size = new System.Drawing.Size(974, 590);
            this.splitContainer1.SplitterDistance = 139;
            this.splitContainer1.TabIndex = 0;
            // 
            // m_mailTempleteList
            // 
            this.m_mailTempleteList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_mailTempleteList.GridLines = true;
            this.m_mailTempleteList.Location = new System.Drawing.Point(0, 0);
            this.m_mailTempleteList.Name = "m_mailTempleteList";
            this.m_mailTempleteList.Size = new System.Drawing.Size(970, 135);
            this.m_mailTempleteList.TabIndex = 0;
            this.m_mailTempleteList.UseCompatibleStateImageBehavior = false;
            this.m_mailTempleteList.View = System.Windows.Forms.View.Details;
            this.m_mailTempleteList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_mailTempleteList_ColumnClick);
            this.m_mailTempleteList.SelectedIndexChanged += new System.EventHandler(this.m_mailTempleteList_SelectedIndexChanged);
            this.m_mailTempleteList.DoubleClick += new System.EventHandler(this.m_mailTempleteList_DoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 12);
            this.label4.TabIndex = 184;
            this.label4.Text = "送信アカウント(メールアドレス)";
            // 
            // m_account
            // 
            this.m_account.Location = new System.Drawing.Point(162, 57);
            this.m_account.Name = "m_account";
            this.m_account.Size = new System.Drawing.Size(261, 19);
            this.m_account.TabIndex = 3;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(-3, -79);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 183;
            this.label12.Text = "表題";
            // 
            // m_deleteBtn
            // 
            this.m_deleteBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_deleteBtn.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_deleteBtn.ForeColor = System.Drawing.Color.Red;
            this.m_deleteBtn.Location = new System.Drawing.Point(856, 6);
            this.m_deleteBtn.Name = "m_deleteBtn";
            this.m_deleteBtn.Size = new System.Drawing.Size(92, 24);
            this.m_deleteBtn.TabIndex = 21;
            this.m_deleteBtn.Text = "削除";
            this.m_deleteBtn.UseVisualStyleBackColor = true;
            this.m_deleteBtn.Click += new System.EventHandler(this.m_deleteBtn_Click);
            // 
            // m_updateOpe
            // 
            this.m_updateOpe.Location = new System.Drawing.Point(782, 208);
            this.m_updateOpe.Name = "m_updateOpe";
            this.m_updateOpe.ReadOnly = true;
            this.m_updateOpe.Size = new System.Drawing.Size(166, 19);
            this.m_updateOpe.TabIndex = 17;
            this.m_updateOpe.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(723, 211);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 105;
            this.label2.Text = "更新者";
            // 
            // m_update
            // 
            this.m_update.Location = new System.Drawing.Point(782, 183);
            this.m_update.Name = "m_update";
            this.m_update.ReadOnly = true;
            this.m_update.Size = new System.Drawing.Size(166, 19);
            this.m_update.TabIndex = 16;
            this.m_update.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(723, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 103;
            this.label3.Text = "更新日時";
            // 
            // Bcc
            // 
            this.Bcc.AutoSize = true;
            this.Bcc.Location = new System.Drawing.Point(636, 85);
            this.Bcc.Name = "Bcc";
            this.Bcc.Size = new System.Drawing.Size(27, 12);
            this.Bcc.TabIndex = 8;
            this.Bcc.TabStop = true;
            this.Bcc.Text = "Bcc:";
            this.Bcc.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Bcc_LinkClicked);
            // 
            // Cc
            // 
            this.Cc.AutoSize = true;
            this.Cc.Location = new System.Drawing.Point(334, 85);
            this.Cc.Name = "Cc";
            this.Cc.Size = new System.Drawing.Size(21, 12);
            this.Cc.TabIndex = 6;
            this.Cc.TabStop = true;
            this.Cc.Text = "Cc:";
            this.Cc.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Cc_LinkClicked);
            // 
            // To
            // 
            this.To.AutoSize = true;
            this.To.Location = new System.Drawing.Point(13, 85);
            this.To.Name = "To";
            this.To.Size = new System.Drawing.Size(20, 12);
            this.To.TabIndex = 4;
            this.To.TabStop = true;
            this.To.Text = "To:";
            this.To.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.To_LinkClicked);
            // 
            // m_Bcc_list
            // 
            this.m_Bcc_list.FullRowSelect = true;
            this.m_Bcc_list.Location = new System.Drawing.Point(666, 80);
            this.m_Bcc_list.Name = "m_Bcc_list";
            this.m_Bcc_list.Size = new System.Drawing.Size(282, 84);
            this.m_Bcc_list.TabIndex = 9;
            this.m_Bcc_list.UseCompatibleStateImageBehavior = false;
            this.m_Bcc_list.View = System.Windows.Forms.View.Details;
            this.m_Bcc_list.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_Bcc_list_KeyDown);
            // 
            // m_Cc_list
            // 
            this.m_Cc_list.FullRowSelect = true;
            this.m_Cc_list.Location = new System.Drawing.Point(360, 80);
            this.m_Cc_list.Name = "m_Cc_list";
            this.m_Cc_list.Size = new System.Drawing.Size(267, 84);
            this.m_Cc_list.TabIndex = 7;
            this.m_Cc_list.UseCompatibleStateImageBehavior = false;
            this.m_Cc_list.View = System.Windows.Forms.View.Details;
            this.m_Cc_list.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_Cc_list_KeyDown);
            // 
            // m_To_list
            // 
            this.m_To_list.FullRowSelect = true;
            this.m_To_list.Location = new System.Drawing.Point(39, 80);
            this.m_To_list.Name = "m_To_list";
            this.m_To_list.Size = new System.Drawing.Size(280, 84);
            this.m_To_list.TabIndex = 5;
            this.m_To_list.UseCompatibleStateImageBehavior = false;
            this.m_To_list.View = System.Windows.Forms.View.Details;
            this.m_To_list.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_To_list_KeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(326, 195);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 12);
            this.label10.TabIndex = 68;
            this.label10.Text = "添付5";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(326, 173);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 12);
            this.label11.TabIndex = 67;
            this.label11.Text = "添付4";
            // 
            // m_temp5
            // 
            this.m_temp5.Location = new System.Drawing.Point(374, 192);
            this.m_temp5.Name = "m_temp5";
            this.m_temp5.Size = new System.Drawing.Size(261, 19);
            this.m_temp5.TabIndex = 14;
            // 
            // m_temp4
            // 
            this.m_temp4.Location = new System.Drawing.Point(374, 170);
            this.m_temp4.Name = "m_temp4";
            this.m_temp4.Size = new System.Drawing.Size(261, 19);
            this.m_temp4.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 216);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 64;
            this.label6.Text = "添付3";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 195);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 63;
            this.label7.Text = "添付2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 173);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 62;
            this.label8.Text = "添付1";
            // 
            // m_temp3
            // 
            this.m_temp3.Location = new System.Drawing.Point(58, 213);
            this.m_temp3.Name = "m_temp3";
            this.m_temp3.Size = new System.Drawing.Size(261, 19);
            this.m_temp3.TabIndex = 12;
            // 
            // m_temp2
            // 
            this.m_temp2.Location = new System.Drawing.Point(58, 192);
            this.m_temp2.Name = "m_temp2";
            this.m_temp2.Size = new System.Drawing.Size(261, 19);
            this.m_temp2.TabIndex = 11;
            // 
            // m_temp1
            // 
            this.m_temp1.Location = new System.Drawing.Point(58, 170);
            this.m_temp1.Name = "m_temp1";
            this.m_temp1.Size = new System.Drawing.Size(261, 19);
            this.m_temp1.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 246);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 52;
            this.label9.Text = "本文";
            // 
            // m_templetename
            // 
            this.m_templetename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_templetename.Location = new System.Drawing.Point(150, 8);
            this.m_templetename.Name = "m_templetename";
            this.m_templetename.Size = new System.Drawing.Size(627, 19);
            this.m_templetename.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 39;
            this.label5.Text = "テンプレート名";
            // 
            // m_tempno
            // 
            this.m_tempno.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tempno.Location = new System.Drawing.Point(87, 8);
            this.m_tempno.Name = "m_tempno";
            this.m_tempno.ReadOnly = true;
            this.m_tempno.Size = new System.Drawing.Size(57, 19);
            this.m_tempno.TabIndex = 0;
            // 
            // m_mailDispBtn
            // 
            this.m_mailDispBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_mailDispBtn.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.m_mailDispBtn.Location = new System.Drawing.Point(12, 419);
            this.m_mailDispBtn.Name = "m_mailDispBtn";
            this.m_mailDispBtn.Size = new System.Drawing.Size(101, 23);
            this.m_mailDispBtn.TabIndex = 20;
            this.m_mailDispBtn.Text = "メール出力";
            this.m_mailDispBtn.UseVisualStyleBackColor = true;
            this.m_mailDispBtn.Click += new System.EventHandler(this.m_mailDispBtn_Click);
            // 
            // m_cancel
            // 
            this.m_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cancel.Location = new System.Drawing.Point(873, 419);
            this.m_cancel.Name = "m_cancel";
            this.m_cancel.Size = new System.Drawing.Size(75, 23);
            this.m_cancel.TabIndex = 19;
            this.m_cancel.Text = "キャンセル";
            this.m_cancel.UseVisualStyleBackColor = true;
            this.m_cancel.Click += new System.EventHandler(this.m_cancel_Click);
            // 
            // m_OK
            // 
            this.m_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_OK.Location = new System.Drawing.Point(792, 419);
            this.m_OK.Name = "m_OK";
            this.m_OK.Size = new System.Drawing.Size(75, 23);
            this.m_OK.TabIndex = 18;
            this.m_OK.Text = "変更";
            this.m_OK.UseVisualStyleBackColor = true;
            this.m_OK.Click += new System.EventHandler(this.m_OK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 31;
            this.label1.Text = "表題";
            // 
            // m_subject
            // 
            this.m_subject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_subject.Location = new System.Drawing.Point(45, 33);
            this.m_subject.Name = "m_subject";
            this.m_subject.Size = new System.Drawing.Size(903, 19);
            this.m_subject.TabIndex = 2;
            // 
            // m_body
            // 
            this.m_body.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_body.Location = new System.Drawing.Point(39, 243);
            this.m_body.Multiline = true;
            this.m_body.Name = "m_body";
            this.m_body.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.m_body.Size = new System.Drawing.Size(909, 172);
            this.m_body.TabIndex = 15;
            // 
            // Form_mailTempleteList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 590);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form_mailTempleteList";
            this.Text = "メールテンプレートリスト";
            this.Load += new System.EventHandler(this.Form_mailTempleteList_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView m_mailTempleteList;
        private System.Windows.Forms.Button m_cancel;
        private System.Windows.Forms.Button m_OK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_subject;
        private System.Windows.Forms.TextBox m_body;
        private System.Windows.Forms.Button m_mailDispBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox m_tempno;
        private System.Windows.Forms.TextBox m_templetename;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox m_temp5;
        private System.Windows.Forms.TextBox m_temp4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox m_temp3;
        private System.Windows.Forms.TextBox m_temp2;
        private System.Windows.Forms.TextBox m_temp1;
        private System.Windows.Forms.ListView m_Bcc_list;
        private System.Windows.Forms.ListView m_Cc_list;
        private System.Windows.Forms.ListView m_To_list;
        private System.Windows.Forms.LinkLabel Bcc;
        private System.Windows.Forms.LinkLabel Cc;
        private System.Windows.Forms.LinkLabel To;
        private System.Windows.Forms.TextBox m_updateOpe;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_update;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button m_deleteBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox m_account;
        private System.Windows.Forms.Label label12;
    }
}