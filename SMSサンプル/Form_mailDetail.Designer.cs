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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_select_btn = new System.Windows.Forms.Button();
            this.m_selectCombo = new System.Windows.Forms.ComboBox();
            this.m_selecttext = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.m_kubun = new System.Windows.Forms.TextBox();
            this.m_userid = new System.Windows.Forms.TextBox();
            this.m_addressno = new System.Windows.Forms.TextBox();
            this.m_address = new System.Windows.Forms.TextBox();
            this.m_addressname = new System.Windows.Forms.TextBox();
            this.m_kousin_btn = new System.Windows.Forms.Button();
            this.m_cancelbtn = new System.Windows.Forms.Button();
            this.m_updateOpe = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.m_update = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.m_deleteBtn = new System.Windows.Forms.Button();
            this.m_addressslist = new System.Windows.Forms.ListView();
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
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_addressslist);
            this.splitContainer1.Size = new System.Drawing.Size(571, 265);
            this.splitContainer1.SplitterDistance = 31;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 288);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "ユーザ区分";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 311);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "ユーザID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 337);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "アドレス番号";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 360);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "メールアドレス";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 383);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "アドレス名";
            // 
            // m_kubun
            // 
            this.m_kubun.Location = new System.Drawing.Point(95, 285);
            this.m_kubun.Name = "m_kubun";
            this.m_kubun.ReadOnly = true;
            this.m_kubun.Size = new System.Drawing.Size(222, 19);
            this.m_kubun.TabIndex = 0;
            // 
            // m_userid
            // 
            this.m_userid.Location = new System.Drawing.Point(95, 309);
            this.m_userid.Name = "m_userid";
            this.m_userid.ReadOnly = true;
            this.m_userid.Size = new System.Drawing.Size(222, 19);
            this.m_userid.TabIndex = 1;
            // 
            // m_addressno
            // 
            this.m_addressno.Location = new System.Drawing.Point(95, 333);
            this.m_addressno.Name = "m_addressno";
            this.m_addressno.Size = new System.Drawing.Size(222, 19);
            this.m_addressno.TabIndex = 2;
            // 
            // m_address
            // 
            this.m_address.Location = new System.Drawing.Point(95, 356);
            this.m_address.Name = "m_address";
            this.m_address.Size = new System.Drawing.Size(222, 19);
            this.m_address.TabIndex = 3;
            // 
            // m_addressname
            // 
            this.m_addressname.Location = new System.Drawing.Point(95, 380);
            this.m_addressname.Name = "m_addressname";
            this.m_addressname.Size = new System.Drawing.Size(222, 19);
            this.m_addressname.TabIndex = 4;
            // 
            // m_kousin_btn
            // 
            this.m_kousin_btn.Location = new System.Drawing.Point(404, 399);
            this.m_kousin_btn.Name = "m_kousin_btn";
            this.m_kousin_btn.Size = new System.Drawing.Size(78, 28);
            this.m_kousin_btn.TabIndex = 10;
            this.m_kousin_btn.Text = "更新";
            this.m_kousin_btn.UseVisualStyleBackColor = true;
            this.m_kousin_btn.Click += new System.EventHandler(this.m_kousin_btn_Click);
            // 
            // m_cancelbtn
            // 
            this.m_cancelbtn.Location = new System.Drawing.Point(488, 399);
            this.m_cancelbtn.Name = "m_cancelbtn";
            this.m_cancelbtn.Size = new System.Drawing.Size(78, 28);
            this.m_cancelbtn.TabIndex = 11;
            this.m_cancelbtn.Text = "キャンセル";
            this.m_cancelbtn.UseVisualStyleBackColor = true;
            this.m_cancelbtn.Click += new System.EventHandler(this.m_cancelbtn_Click);
            // 
            // m_updateOpe
            // 
            this.m_updateOpe.Location = new System.Drawing.Point(400, 364);
            this.m_updateOpe.Name = "m_updateOpe";
            this.m_updateOpe.ReadOnly = true;
            this.m_updateOpe.Size = new System.Drawing.Size(166, 19);
            this.m_updateOpe.TabIndex = 9;
            this.m_updateOpe.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(341, 367);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 77;
            this.label11.Text = "更新者";
            // 
            // m_update
            // 
            this.m_update.Location = new System.Drawing.Point(400, 336);
            this.m_update.Name = "m_update";
            this.m_update.ReadOnly = true;
            this.m_update.Size = new System.Drawing.Size(166, 19);
            this.m_update.TabIndex = 8;
            this.m_update.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(341, 340);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 75;
            this.label12.Text = "更新日時";
            // 
            // m_deleteBtn
            // 
            this.m_deleteBtn.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_deleteBtn.ForeColor = System.Drawing.Color.Red;
            this.m_deleteBtn.Location = new System.Drawing.Point(490, 277);
            this.m_deleteBtn.Name = "m_deleteBtn";
            this.m_deleteBtn.Size = new System.Drawing.Size(76, 27);
            this.m_deleteBtn.TabIndex = 13;
            this.m_deleteBtn.Text = "削除";
            this.m_deleteBtn.UseVisualStyleBackColor = true;
            this.m_deleteBtn.Click += new System.EventHandler(this.m_deleteBtn_Click);
            // 
            // m_addressslist
            // 
            this.m_addressslist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_addressslist.GridLines = true;
            this.m_addressslist.Location = new System.Drawing.Point(2, 3);
            this.m_addressslist.Name = "m_addressslist";
            this.m_addressslist.Size = new System.Drawing.Size(568, 222);
            this.m_addressslist.TabIndex = 185;
            this.m_addressslist.UseCompatibleStateImageBehavior = false;
            this.m_addressslist.View = System.Windows.Forms.View.Details;
            this.m_addressslist.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_addressslist_ColumnClick);
            this.m_addressslist.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.m_addressslist_MouseDoubleClick);
            // 
            // Form_mailDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 439);
            this.Controls.Add(this.m_deleteBtn);
            this.Controls.Add(this.m_updateOpe);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.m_update);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.m_cancelbtn);
            this.Controls.Add(this.m_kousin_btn);
            this.Controls.Add(this.m_addressname);
            this.Controls.Add(this.m_address);
            this.Controls.Add(this.m_addressno);
            this.Controls.Add(this.m_userid);
            this.Controls.Add(this.m_kubun);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form_mailDetail";
            this.Text = "メールアドレス一覧";
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
        private System.Windows.Forms.Button m_select_btn;
        private System.Windows.Forms.ComboBox m_selectCombo;
        private System.Windows.Forms.TextBox m_selecttext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox m_kubun;
        private System.Windows.Forms.TextBox m_userid;
        private System.Windows.Forms.TextBox m_addressno;
        private System.Windows.Forms.TextBox m_address;
        private System.Windows.Forms.TextBox m_addressname;
        private System.Windows.Forms.Button m_kousin_btn;
        private System.Windows.Forms.Button m_cancelbtn;
        private System.Windows.Forms.TextBox m_updateOpe;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox m_update;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button m_deleteBtn;
        private System.Windows.Forms.ListView m_addressslist;
    }
}