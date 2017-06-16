namespace SMSサンプル
{
    partial class Form_opeDetail
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
            this.m_oper_List = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.m_openo = new System.Windows.Forms.TextBox();
            this.m_opeid = new System.Windows.Forms.TextBox();
            this.m_lastname = new System.Windows.Forms.TextBox();
            this.m_firstname = new System.Windows.Forms.TextBox();
            this.m_password = new System.Windows.Forms.TextBox();
            this.m_biko = new System.Windows.Forms.TextBox();
            this.m_kousin_btn = new System.Windows.Forms.Button();
            this.m_cancelbtn = new System.Windows.Forms.Button();
            this.m_updateOpe = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.m_update = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.m_deleteBtn = new System.Windows.Forms.Button();
            this.m_hirabunBtn = new System.Windows.Forms.Button();
            this.m_addressslist = new System.Windows.Forms.ListView();
            this.m_kengenCombo = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
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
            this.splitContainer1.Panel2.Controls.Add(this.m_oper_List);
            this.splitContainer1.Size = new System.Drawing.Size(721, 265);
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
            // m_oper_List
            // 
            this.m_oper_List.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_oper_List.GridLines = true;
            this.m_oper_List.Location = new System.Drawing.Point(0, 0);
            this.m_oper_List.Name = "m_oper_List";
            this.m_oper_List.Size = new System.Drawing.Size(719, 228);
            this.m_oper_List.TabIndex = 0;
            this.m_oper_List.UseCompatibleStateImageBehavior = false;
            this.m_oper_List.View = System.Windows.Forms.View.Details;
            this.m_oper_List.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_oper_List_ColumnClick);
            this.m_oper_List.DoubleClick += new System.EventHandler(this.m_customertantouList_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 288);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "オペレータ通番";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 311);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "オペレータID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 336);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "姓";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 362);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "名";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 383);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "パスワード";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 407);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "権限";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(355, 291);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 9;
            this.label9.Text = "備考";
            // 
            // m_openo
            // 
            this.m_openo.Location = new System.Drawing.Point(95, 285);
            this.m_openo.Name = "m_openo";
            this.m_openo.ReadOnly = true;
            this.m_openo.Size = new System.Drawing.Size(222, 19);
            this.m_openo.TabIndex = 0;
            // 
            // m_opeid
            // 
            this.m_opeid.Location = new System.Drawing.Point(95, 309);
            this.m_opeid.Name = "m_opeid";
            this.m_opeid.Size = new System.Drawing.Size(222, 19);
            this.m_opeid.TabIndex = 1;
            // 
            // m_lastname
            // 
            this.m_lastname.Location = new System.Drawing.Point(95, 333);
            this.m_lastname.Name = "m_lastname";
            this.m_lastname.Size = new System.Drawing.Size(222, 19);
            this.m_lastname.TabIndex = 2;
            // 
            // m_firstname
            // 
            this.m_firstname.Location = new System.Drawing.Point(95, 356);
            this.m_firstname.Name = "m_firstname";
            this.m_firstname.Size = new System.Drawing.Size(222, 19);
            this.m_firstname.TabIndex = 3;
            // 
            // m_password
            // 
            this.m_password.Location = new System.Drawing.Point(95, 380);
            this.m_password.Name = "m_password";
            this.m_password.Size = new System.Drawing.Size(222, 19);
            this.m_password.TabIndex = 4;
            // 
            // m_biko
            // 
            this.m_biko.Location = new System.Drawing.Point(384, 288);
            this.m_biko.Multiline = true;
            this.m_biko.Name = "m_biko";
            this.m_biko.Size = new System.Drawing.Size(254, 38);
            this.m_biko.TabIndex = 7;
            // 
            // m_kousin_btn
            // 
            this.m_kousin_btn.Location = new System.Drawing.Point(476, 399);
            this.m_kousin_btn.Name = "m_kousin_btn";
            this.m_kousin_btn.Size = new System.Drawing.Size(78, 28);
            this.m_kousin_btn.TabIndex = 10;
            this.m_kousin_btn.Text = "更新";
            this.m_kousin_btn.UseVisualStyleBackColor = true;
            this.m_kousin_btn.Click += new System.EventHandler(this.m_kousin_btn_Click);
            // 
            // m_cancelbtn
            // 
            this.m_cancelbtn.Location = new System.Drawing.Point(560, 399);
            this.m_cancelbtn.Name = "m_cancelbtn";
            this.m_cancelbtn.Size = new System.Drawing.Size(78, 28);
            this.m_cancelbtn.TabIndex = 11;
            this.m_cancelbtn.Text = "キャンセル";
            this.m_cancelbtn.UseVisualStyleBackColor = true;
            this.m_cancelbtn.Click += new System.EventHandler(this.m_cancelbtn_Click);
            // 
            // m_updateOpe
            // 
            this.m_updateOpe.Location = new System.Drawing.Point(472, 364);
            this.m_updateOpe.Name = "m_updateOpe";
            this.m_updateOpe.ReadOnly = true;
            this.m_updateOpe.Size = new System.Drawing.Size(166, 19);
            this.m_updateOpe.TabIndex = 9;
            this.m_updateOpe.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(413, 367);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 77;
            this.label11.Text = "更新者";
            // 
            // m_update
            // 
            this.m_update.Location = new System.Drawing.Point(472, 336);
            this.m_update.Name = "m_update";
            this.m_update.ReadOnly = true;
            this.m_update.Size = new System.Drawing.Size(166, 19);
            this.m_update.TabIndex = 8;
            this.m_update.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(413, 340);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 75;
            this.label12.Text = "更新日時";
            // 
            // m_deleteBtn
            // 
            this.m_deleteBtn.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_deleteBtn.ForeColor = System.Drawing.Color.Red;
            this.m_deleteBtn.Location = new System.Drawing.Point(644, 273);
            this.m_deleteBtn.Name = "m_deleteBtn";
            this.m_deleteBtn.Size = new System.Drawing.Size(76, 27);
            this.m_deleteBtn.TabIndex = 13;
            this.m_deleteBtn.Text = "削除";
            this.m_deleteBtn.UseVisualStyleBackColor = true;
            this.m_deleteBtn.Click += new System.EventHandler(this.m_deleteBtn_Click);
            // 
            // m_hirabunBtn
            // 
            this.m_hirabunBtn.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_hirabunBtn.ForeColor = System.Drawing.Color.Black;
            this.m_hirabunBtn.Location = new System.Drawing.Point(320, 380);
            this.m_hirabunBtn.Name = "m_hirabunBtn";
            this.m_hirabunBtn.Size = new System.Drawing.Size(38, 20);
            this.m_hirabunBtn.TabIndex = 5;
            this.m_hirabunBtn.Text = "平文";
            this.m_hirabunBtn.UseVisualStyleBackColor = true;
            this.m_hirabunBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // m_addressslist
            // 
            this.m_addressslist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_addressslist.GridLines = true;
            this.m_addressslist.Location = new System.Drawing.Point(2, 469);
            this.m_addressslist.Name = "m_addressslist";
            this.m_addressslist.Size = new System.Drawing.Size(718, 105);
            this.m_addressslist.TabIndex = 185;
            this.m_addressslist.UseCompatibleStateImageBehavior = false;
            this.m_addressslist.View = System.Windows.Forms.View.Details;
            this.m_addressslist.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_addressslist_ColumnClick);
            this.m_addressslist.DoubleClick += new System.EventHandler(this.m_addressslist_DoubleClick);
            // 
            // m_kengenCombo
            // 
            this.m_kengenCombo.FormattingEnabled = true;
            this.m_kengenCombo.Items.AddRange(new object[] {
            "管理者",
            "利用者"});
            this.m_kengenCombo.Location = new System.Drawing.Point(95, 404);
            this.m_kengenCombo.Name = "m_kengenCombo";
            this.m_kengenCombo.Size = new System.Drawing.Size(121, 20);
            this.m_kengenCombo.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1, 435);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 28);
            this.button1.TabIndex = 12;
            this.button1.Text = "メールアドレス追加";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form_opeDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 576);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.m_kengenCombo);
            this.Controls.Add(this.m_addressslist);
            this.Controls.Add(this.m_hirabunBtn);
            this.Controls.Add(this.m_deleteBtn);
            this.Controls.Add(this.m_updateOpe);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.m_update);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.m_cancelbtn);
            this.Controls.Add(this.m_kousin_btn);
            this.Controls.Add(this.m_biko);
            this.Controls.Add(this.m_password);
            this.Controls.Add(this.m_firstname);
            this.Controls.Add(this.m_lastname);
            this.Controls.Add(this.m_opeid);
            this.Controls.Add(this.m_openo);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form_opeDetail";
            this.Text = "オペレータ編集";
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
        private System.Windows.Forms.ListView m_oper_List;
        private System.Windows.Forms.Button m_select_btn;
        private System.Windows.Forms.ComboBox m_selectCombo;
        private System.Windows.Forms.TextBox m_selecttext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox m_openo;
        private System.Windows.Forms.TextBox m_opeid;
        private System.Windows.Forms.TextBox m_lastname;
        private System.Windows.Forms.TextBox m_firstname;
        private System.Windows.Forms.TextBox m_password;
        private System.Windows.Forms.TextBox m_biko;
        private System.Windows.Forms.Button m_kousin_btn;
        private System.Windows.Forms.Button m_cancelbtn;
        private System.Windows.Forms.TextBox m_updateOpe;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox m_update;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button m_deleteBtn;
        private System.Windows.Forms.Button m_hirabunBtn;
        private System.Windows.Forms.ListView m_addressslist;
        private System.Windows.Forms.ComboBox m_kengenCombo;
        private System.Windows.Forms.Button button1;
    }
}