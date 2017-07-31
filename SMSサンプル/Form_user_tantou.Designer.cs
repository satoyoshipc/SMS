namespace SMSサンプル
{
    partial class Form_user_tantou
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
            this.m_select_btn = new System.Windows.Forms.Button();
            this.m_selectCombo = new System.Windows.Forms.ComboBox();
            this.m_selecttext = new System.Windows.Forms.TextBox();
            this.m_customertantouList = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.m_tantouno = new System.Windows.Forms.TextBox();
            this.m_tantouname = new System.Windows.Forms.TextBox();
            this.m_tantoukana = new System.Windows.Forms.TextBox();
            this.m_busyoname = new System.Windows.Forms.TextBox();
            this.m_tel1 = new System.Windows.Forms.TextBox();
            this.m_tel2 = new System.Windows.Forms.TextBox();
            this.m_yakusyoku = new System.Windows.Forms.TextBox();
            this.m_statusCombo = new System.Windows.Forms.ComboBox();
            this.m_biko = new System.Windows.Forms.TextBox();
            this.m_kousin_btn = new System.Windows.Forms.Button();
            this.m_cancelbtn = new System.Windows.Forms.Button();
            this.m_updateOpe = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.m_update = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.m_deleteBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.m_addressslist = new System.Windows.Forms.ListView();
            this.m_username = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.m_userno = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(1, 1);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_select_btn);
            this.splitContainer1.Panel1.Controls.Add(this.m_selectCombo);
            this.splitContainer1.Panel1.Controls.Add(this.m_selecttext);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_customertantouList);
            this.splitContainer1.Size = new System.Drawing.Size(777, 248);
            this.splitContainer1.SplitterDistance = 29;
            this.splitContainer1.TabIndex = 0;
            // 
            // m_select_btn
            // 
            this.m_select_btn.Location = new System.Drawing.Point(446, 3);
            this.m_select_btn.Name = "m_select_btn";
            this.m_select_btn.Size = new System.Drawing.Size(75, 23);
            this.m_select_btn.TabIndex = 2;
            this.m_select_btn.Text = "検索";
            this.m_select_btn.UseVisualStyleBackColor = true;
            this.m_select_btn.Click += new System.EventHandler(this.m_select_btn_Click);
            // 
            // m_selectCombo
            // 
            this.m_selectCombo.FormattingEnabled = true;
            this.m_selectCombo.Location = new System.Drawing.Point(4, 2);
            this.m_selectCombo.Name = "m_selectCombo";
            this.m_selectCombo.Size = new System.Drawing.Size(212, 20);
            this.m_selectCombo.TabIndex = 0;
            // 
            // m_selecttext
            // 
            this.m_selecttext.Location = new System.Drawing.Point(224, 3);
            this.m_selecttext.Name = "m_selecttext";
            this.m_selecttext.Size = new System.Drawing.Size(200, 19);
            this.m_selecttext.TabIndex = 1;
            // 
            // m_customertantouList
            // 
            this.m_customertantouList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_customertantouList.GridLines = true;
            this.m_customertantouList.Location = new System.Drawing.Point(0, 0);
            this.m_customertantouList.Name = "m_customertantouList";
            this.m_customertantouList.Size = new System.Drawing.Size(775, 213);
            this.m_customertantouList.TabIndex = 0;
            this.m_customertantouList.UseCompatibleStateImageBehavior = false;
            this.m_customertantouList.View = System.Windows.Forms.View.Details;
            this.m_customertantouList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_customertantouList_ColumnClick);
            this.m_customertantouList.DoubleClick += new System.EventHandler(this.m_customertantouList_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 288);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "担当者通番";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 311);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "担当者";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 336);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "担当者名カナ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 362);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "部署名";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 383);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "電話番号(TEL等)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 407);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "電話番号(携帯等)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 433);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "役職";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 455);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "ステータス";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(367, 269);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 9;
            this.label9.Text = "備考";
            // 
            // m_tantouno
            // 
            this.m_tantouno.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tantouno.Location = new System.Drawing.Point(110, 285);
            this.m_tantouno.Name = "m_tantouno";
            this.m_tantouno.ReadOnly = true;
            this.m_tantouno.Size = new System.Drawing.Size(222, 19);
            this.m_tantouno.TabIndex = 2;
            // 
            // m_tantouname
            // 
            this.m_tantouname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tantouname.Location = new System.Drawing.Point(110, 309);
            this.m_tantouname.Name = "m_tantouname";
            this.m_tantouname.Size = new System.Drawing.Size(222, 19);
            this.m_tantouname.TabIndex = 3;
            // 
            // m_tantoukana
            // 
            this.m_tantoukana.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tantoukana.Location = new System.Drawing.Point(110, 333);
            this.m_tantoukana.Name = "m_tantoukana";
            this.m_tantoukana.Size = new System.Drawing.Size(222, 19);
            this.m_tantoukana.TabIndex = 4;
            // 
            // m_busyoname
            // 
            this.m_busyoname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_busyoname.Location = new System.Drawing.Point(110, 356);
            this.m_busyoname.Name = "m_busyoname";
            this.m_busyoname.Size = new System.Drawing.Size(222, 19);
            this.m_busyoname.TabIndex = 5;
            // 
            // m_tel1
            // 
            this.m_tel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tel1.Location = new System.Drawing.Point(110, 380);
            this.m_tel1.Name = "m_tel1";
            this.m_tel1.Size = new System.Drawing.Size(222, 19);
            this.m_tel1.TabIndex = 6;
            // 
            // m_tel2
            // 
            this.m_tel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tel2.Location = new System.Drawing.Point(110, 403);
            this.m_tel2.Name = "m_tel2";
            this.m_tel2.Size = new System.Drawing.Size(222, 19);
            this.m_tel2.TabIndex = 7;
            // 
            // m_yakusyoku
            // 
            this.m_yakusyoku.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_yakusyoku.Location = new System.Drawing.Point(110, 427);
            this.m_yakusyoku.Name = "m_yakusyoku";
            this.m_yakusyoku.Size = new System.Drawing.Size(222, 19);
            this.m_yakusyoku.TabIndex = 8;
            // 
            // m_statusCombo
            // 
            this.m_statusCombo.FormattingEnabled = true;
            this.m_statusCombo.Items.AddRange(new object[] {
            "無効",
            "有効"});
            this.m_statusCombo.Location = new System.Drawing.Point(110, 453);
            this.m_statusCombo.Name = "m_statusCombo";
            this.m_statusCombo.Size = new System.Drawing.Size(75, 20);
            this.m_statusCombo.TabIndex = 9;
            // 
            // m_biko
            // 
            this.m_biko.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_biko.Location = new System.Drawing.Point(426, 266);
            this.m_biko.Multiline = true;
            this.m_biko.Name = "m_biko";
            this.m_biko.Size = new System.Drawing.Size(264, 128);
            this.m_biko.TabIndex = 10;
            // 
            // m_kousin_btn
            // 
            this.m_kousin_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_kousin_btn.Location = new System.Drawing.Point(612, 476);
            this.m_kousin_btn.Name = "m_kousin_btn";
            this.m_kousin_btn.Size = new System.Drawing.Size(78, 28);
            this.m_kousin_btn.TabIndex = 14;
            this.m_kousin_btn.Text = "更新";
            this.m_kousin_btn.UseVisualStyleBackColor = true;
            this.m_kousin_btn.Click += new System.EventHandler(this.m_kousin_btn_Click);
            // 
            // m_cancelbtn
            // 
            this.m_cancelbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cancelbtn.Location = new System.Drawing.Point(696, 476);
            this.m_cancelbtn.Name = "m_cancelbtn";
            this.m_cancelbtn.Size = new System.Drawing.Size(78, 28);
            this.m_cancelbtn.TabIndex = 15;
            this.m_cancelbtn.Text = "キャンセル";
            this.m_cancelbtn.UseVisualStyleBackColor = true;
            this.m_cancelbtn.Click += new System.EventHandler(this.m_cancelbtn_Click);
            // 
            // m_updateOpe
            // 
            this.m_updateOpe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_updateOpe.Location = new System.Drawing.Point(426, 428);
            this.m_updateOpe.Name = "m_updateOpe";
            this.m_updateOpe.ReadOnly = true;
            this.m_updateOpe.Size = new System.Drawing.Size(166, 19);
            this.m_updateOpe.TabIndex = 12;
            this.m_updateOpe.TabStop = false;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(367, 431);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 77;
            this.label11.Text = "更新者";
            // 
            // m_update
            // 
            this.m_update.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_update.Location = new System.Drawing.Point(426, 400);
            this.m_update.Name = "m_update";
            this.m_update.ReadOnly = true;
            this.m_update.Size = new System.Drawing.Size(166, 19);
            this.m_update.TabIndex = 11;
            this.m_update.TabStop = false;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(367, 404);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 75;
            this.label12.Text = "更新日時";
            // 
            // m_deleteBtn
            // 
            this.m_deleteBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_deleteBtn.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_deleteBtn.ForeColor = System.Drawing.Color.Red;
            this.m_deleteBtn.Location = new System.Drawing.Point(698, 255);
            this.m_deleteBtn.Name = "m_deleteBtn";
            this.m_deleteBtn.Size = new System.Drawing.Size(76, 27);
            this.m_deleteBtn.TabIndex = 16;
            this.m_deleteBtn.Text = "削除";
            this.m_deleteBtn.UseVisualStyleBackColor = true;
            this.m_deleteBtn.Click += new System.EventHandler(this.m_deleteBtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1, 477);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 28);
            this.button1.TabIndex = 13;
            this.button1.Text = "メールアドレス追加";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // m_addressslist
            // 
            this.m_addressslist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_addressslist.GridLines = true;
            this.m_addressslist.Location = new System.Drawing.Point(2, 510);
            this.m_addressslist.Name = "m_addressslist";
            this.m_addressslist.Size = new System.Drawing.Size(775, 105);
            this.m_addressslist.TabIndex = 17;
            this.m_addressslist.UseCompatibleStateImageBehavior = false;
            this.m_addressslist.View = System.Windows.Forms.View.Details;
            this.m_addressslist.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.m_addressslist_MouseDoubleClick);
            // 
            // m_username
            // 
            this.m_username.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_username.Location = new System.Drawing.Point(149, 259);
            this.m_username.Name = "m_username";
            this.m_username.ReadOnly = true;
            this.m_username.Size = new System.Drawing.Size(183, 19);
            this.m_username.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 262);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 12);
            this.label10.TabIndex = 79;
            this.label10.Text = "カスタマ";
            // 
            // m_userno
            // 
            this.m_userno.Location = new System.Drawing.Point(110, 259);
            this.m_userno.Name = "m_userno";
            this.m_userno.ReadOnly = true;
            this.m_userno.Size = new System.Drawing.Size(33, 19);
            this.m_userno.TabIndex = 0;
            // 
            // Form_user_tantou
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 617);
            this.Controls.Add(this.m_userno);
            this.Controls.Add(this.m_username);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.m_addressslist);
            this.Controls.Add(this.m_deleteBtn);
            this.Controls.Add(this.m_updateOpe);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.m_update);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.m_cancelbtn);
            this.Controls.Add(this.m_kousin_btn);
            this.Controls.Add(this.m_biko);
            this.Controls.Add(this.m_statusCombo);
            this.Controls.Add(this.m_yakusyoku);
            this.Controls.Add(this.m_tel2);
            this.Controls.Add(this.m_tel1);
            this.Controls.Add(this.m_busyoname);
            this.Controls.Add(this.m_tantoukana);
            this.Controls.Add(this.m_tantouname);
            this.Controls.Add(this.m_tantouno);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form_user_tantou";
            this.Text = "カスタマ担当者";
            this.Load += new System.EventHandler(this.Form_user_tantou_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView m_customertantouList;
        private System.Windows.Forms.Button m_select_btn;
        private System.Windows.Forms.ComboBox m_selectCombo;
        private System.Windows.Forms.TextBox m_selecttext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox m_tantouno;
        private System.Windows.Forms.TextBox m_tantouname;
        private System.Windows.Forms.TextBox m_tantoukana;
        private System.Windows.Forms.TextBox m_busyoname;
        private System.Windows.Forms.TextBox m_tel1;
        private System.Windows.Forms.TextBox m_tel2;
        private System.Windows.Forms.TextBox m_yakusyoku;
        private System.Windows.Forms.ComboBox m_statusCombo;
        private System.Windows.Forms.TextBox m_biko;
        private System.Windows.Forms.Button m_kousin_btn;
        private System.Windows.Forms.Button m_cancelbtn;
        private System.Windows.Forms.TextBox m_updateOpe;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox m_update;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button m_deleteBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView m_addressslist;
        private System.Windows.Forms.TextBox m_username;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox m_userno;
    }
}