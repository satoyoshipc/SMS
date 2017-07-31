namespace SMSサンプル
{
    partial class Form_mailDetail
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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.m_addressslist = new System.Windows.Forms.ListView();
            this.m_deleteBtn = new System.Windows.Forms.Button();
            this.m_updateOpe = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.m_update = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.m_cancelbtn = new System.Windows.Forms.Button();
            this.m_kousin_btn = new System.Windows.Forms.Button();
            this.m_addressname = new System.Windows.Forms.TextBox();
            this.m_address = new System.Windows.Forms.TextBox();
            this.m_addressno = new System.Windows.Forms.TextBox();
            this.m_userid = new System.Windows.Forms.TextBox();
            this.m_kubun = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_select_btn = new System.Windows.Forms.Button();
            this.m_selectCombo = new System.Windows.Forms.ComboBox();
            this.m_selecttext = new System.Windows.Forms.TextBox();
            this.m_Customername = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_username = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Location = new System.Drawing.Point(1, 29);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.m_addressslist);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.m_username);
            this.splitContainer2.Panel2.Controls.Add(this.label7);
            this.splitContainer2.Panel2.Controls.Add(this.m_Customername);
            this.splitContainer2.Panel2.Controls.Add(this.label6);
            this.splitContainer2.Panel2.Controls.Add(this.m_deleteBtn);
            this.splitContainer2.Panel2.Controls.Add(this.m_updateOpe);
            this.splitContainer2.Panel2.Controls.Add(this.label11);
            this.splitContainer2.Panel2.Controls.Add(this.m_update);
            this.splitContainer2.Panel2.Controls.Add(this.label12);
            this.splitContainer2.Panel2.Controls.Add(this.m_cancelbtn);
            this.splitContainer2.Panel2.Controls.Add(this.m_kousin_btn);
            this.splitContainer2.Panel2.Controls.Add(this.m_addressname);
            this.splitContainer2.Panel2.Controls.Add(this.m_address);
            this.splitContainer2.Panel2.Controls.Add(this.m_addressno);
            this.splitContainer2.Panel2.Controls.Add(this.m_userid);
            this.splitContainer2.Panel2.Controls.Add(this.m_kubun);
            this.splitContainer2.Panel2.Controls.Add(this.label5);
            this.splitContainer2.Panel2.Controls.Add(this.label4);
            this.splitContainer2.Panel2.Controls.Add(this.label3);
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Panel2.Controls.Add(this.label1);
            this.splitContainer2.Size = new System.Drawing.Size(575, 477);
            this.splitContainer2.SplitterDistance = 268;
            this.splitContainer2.TabIndex = 1;
            // 
            // m_addressslist
            // 
            this.m_addressslist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_addressslist.GridLines = true;
            this.m_addressslist.Location = new System.Drawing.Point(0, 0);
            this.m_addressslist.Name = "m_addressslist";
            this.m_addressslist.Size = new System.Drawing.Size(571, 264);
            this.m_addressslist.TabIndex = 1;
            this.m_addressslist.UseCompatibleStateImageBehavior = false;
            this.m_addressslist.View = System.Windows.Forms.View.Details;
            this.m_addressslist.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_addressslist_ColumnClick_1);
            this.m_addressslist.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.m_addressslist_MouseDoubleClick_1);
            // 
            // m_deleteBtn
            // 
            this.m_deleteBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_deleteBtn.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_deleteBtn.ForeColor = System.Drawing.Color.Red;
            this.m_deleteBtn.Location = new System.Drawing.Point(485, 7);
            this.m_deleteBtn.Name = "m_deleteBtn";
            this.m_deleteBtn.Size = new System.Drawing.Size(76, 27);
            this.m_deleteBtn.TabIndex = 109;
            this.m_deleteBtn.Text = "削除";
            this.m_deleteBtn.UseVisualStyleBackColor = true;
            this.m_deleteBtn.Click += new System.EventHandler(this.m_deleteBtn_Click_1);
            // 
            // m_updateOpe
            // 
            this.m_updateOpe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_updateOpe.Location = new System.Drawing.Point(397, 138);
            this.m_updateOpe.Name = "m_updateOpe";
            this.m_updateOpe.ReadOnly = true;
            this.m_updateOpe.Size = new System.Drawing.Size(166, 19);
            this.m_updateOpe.TabIndex = 106;
            this.m_updateOpe.TabStop = false;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(338, 141);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 111;
            this.label11.Text = "更新者";
            // 
            // m_update
            // 
            this.m_update.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_update.Location = new System.Drawing.Point(397, 110);
            this.m_update.Name = "m_update";
            this.m_update.ReadOnly = true;
            this.m_update.Size = new System.Drawing.Size(166, 19);
            this.m_update.TabIndex = 104;
            this.m_update.TabStop = false;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(338, 114);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 110;
            this.label12.Text = "更新日時";
            // 
            // m_cancelbtn
            // 
            this.m_cancelbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cancelbtn.Location = new System.Drawing.Point(486, 170);
            this.m_cancelbtn.Name = "m_cancelbtn";
            this.m_cancelbtn.Size = new System.Drawing.Size(78, 28);
            this.m_cancelbtn.TabIndex = 108;
            this.m_cancelbtn.Text = "キャンセル";
            this.m_cancelbtn.UseVisualStyleBackColor = true;
            this.m_cancelbtn.Click += new System.EventHandler(this.m_cancelbtn_Click_1);
            // 
            // m_kousin_btn
            // 
            this.m_kousin_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_kousin_btn.Location = new System.Drawing.Point(402, 170);
            this.m_kousin_btn.Name = "m_kousin_btn";
            this.m_kousin_btn.Size = new System.Drawing.Size(78, 28);
            this.m_kousin_btn.TabIndex = 107;
            this.m_kousin_btn.Text = "更新";
            this.m_kousin_btn.UseVisualStyleBackColor = true;
            this.m_kousin_btn.Click += new System.EventHandler(this.m_kousin_btn_Click_1);
            // 
            // m_addressname
            // 
            this.m_addressname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_addressname.Location = new System.Drawing.Point(92, 154);
            this.m_addressname.Name = "m_addressname";
            this.m_addressname.Size = new System.Drawing.Size(222, 19);
            this.m_addressname.TabIndex = 102;
            // 
            // m_address
            // 
            this.m_address.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_address.Location = new System.Drawing.Point(92, 130);
            this.m_address.Name = "m_address";
            this.m_address.Size = new System.Drawing.Size(222, 19);
            this.m_address.TabIndex = 101;
            // 
            // m_addressno
            // 
            this.m_addressno.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_addressno.Location = new System.Drawing.Point(92, 107);
            this.m_addressno.Name = "m_addressno";
            this.m_addressno.Size = new System.Drawing.Size(222, 19);
            this.m_addressno.TabIndex = 98;
            // 
            // m_userid
            // 
            this.m_userid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_userid.Location = new System.Drawing.Point(92, 35);
            this.m_userid.Name = "m_userid";
            this.m_userid.ReadOnly = true;
            this.m_userid.Size = new System.Drawing.Size(222, 19);
            this.m_userid.TabIndex = 97;
            // 
            // m_kubun
            // 
            this.m_kubun.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_kubun.Location = new System.Drawing.Point(92, 11);
            this.m_kubun.Name = "m_kubun";
            this.m_kubun.ReadOnly = true;
            this.m_kubun.Size = new System.Drawing.Size(222, 19);
            this.m_kubun.TabIndex = 95;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 105;
            this.label5.Text = "アドレス名";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 12);
            this.label4.TabIndex = 103;
            this.label4.Text = "メールアドレス";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 100;
            this.label3.Text = "アドレス番号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 12);
            this.label2.TabIndex = 99;
            this.label2.Text = "ユーザID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 96;
            this.label1.Text = "ユーザ区分";
            // 
            // m_select_btn
            // 
            this.m_select_btn.Location = new System.Drawing.Point(454, 4);
            this.m_select_btn.Name = "m_select_btn";
            this.m_select_btn.Size = new System.Drawing.Size(75, 23);
            this.m_select_btn.TabIndex = 5;
            this.m_select_btn.Text = "検索";
            this.m_select_btn.UseVisualStyleBackColor = true;
            this.m_select_btn.Click += new System.EventHandler(this.m_select_btn_Click_1);
            // 
            // m_selectCombo
            // 
            this.m_selectCombo.FormattingEnabled = true;
            this.m_selectCombo.Location = new System.Drawing.Point(12, 3);
            this.m_selectCombo.Name = "m_selectCombo";
            this.m_selectCombo.Size = new System.Drawing.Size(212, 20);
            this.m_selectCombo.TabIndex = 3;
            // 
            // m_selecttext
            // 
            this.m_selecttext.Location = new System.Drawing.Point(232, 4);
            this.m_selecttext.Name = "m_selecttext";
            this.m_selecttext.Size = new System.Drawing.Size(200, 19);
            this.m_selecttext.TabIndex = 4;
            // 
            // m_Customername
            // 
            this.m_Customername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_Customername.Location = new System.Drawing.Point(92, 60);
            this.m_Customername.Name = "m_Customername";
            this.m_Customername.ReadOnly = true;
            this.m_Customername.Size = new System.Drawing.Size(222, 19);
            this.m_Customername.TabIndex = 112;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 12);
            this.label6.TabIndex = 113;
            this.label6.Text = "カスタマ名";
            // 
            // m_username
            // 
            this.m_username.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_username.Location = new System.Drawing.Point(92, 84);
            this.m_username.Name = "m_username";
            this.m_username.ReadOnly = true;
            this.m_username.Size = new System.Drawing.Size(222, 19);
            this.m_username.TabIndex = 114;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 115;
            this.label7.Text = "ユーザ名";
            // 
            // Form_mailDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 505);
            this.Controls.Add(this.m_select_btn);
            this.Controls.Add(this.m_selectCombo);
            this.Controls.Add(this.m_selecttext);
            this.Controls.Add(this.splitContainer2);
            this.Name = "Form_mailDetail";
            this.Text = "メールアドレス一覧";
            this.Load += new System.EventHandler(this.Form_user_tantou_Load);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListView m_addressslist;
        private System.Windows.Forms.Button m_deleteBtn;
        private System.Windows.Forms.TextBox m_updateOpe;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox m_update;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button m_cancelbtn;
        private System.Windows.Forms.Button m_kousin_btn;
        private System.Windows.Forms.TextBox m_addressname;
        private System.Windows.Forms.TextBox m_address;
        private System.Windows.Forms.TextBox m_addressno;
        private System.Windows.Forms.TextBox m_userid;
        private System.Windows.Forms.TextBox m_kubun;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button m_select_btn;
        private System.Windows.Forms.ComboBox m_selectCombo;
        private System.Windows.Forms.TextBox m_selecttext;
        private System.Windows.Forms.TextBox m_Customername;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox m_username;
        private System.Windows.Forms.Label label7;
    }
}