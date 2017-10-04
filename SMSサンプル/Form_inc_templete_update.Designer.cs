namespace moss_AP
{
    partial class Form_inc_templete_update
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
            this.m_select_btn = new System.Windows.Forms.Button();
            this.m_selectCombo = new System.Windows.Forms.ComboBox();
            this.m_selecttext = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_templetelist = new System.Windows.Forms.ListView();
            this.m_userno = new System.Windows.Forms.TextBox();
            this.m_usernameCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_templete_type_combo = new System.Windows.Forms.ComboBox();
            this.m_tempno = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_content = new System.Windows.Forms.TextBox();
            this.m_title = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.m_deleteBtn = new System.Windows.Forms.Button();
            this.m_labelinputOpe = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.m_idlabel = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.m_cancelbtn = new System.Windows.Forms.Button();
            this.m_kousin_btn = new System.Windows.Forms.Button();
            this.m_templetename = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_select_btn
            // 
            this.m_select_btn.Location = new System.Drawing.Point(466, 3);
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
            this.m_selectCombo.Location = new System.Drawing.Point(13, 5);
            this.m_selectCombo.Name = "m_selectCombo";
            this.m_selectCombo.Size = new System.Drawing.Size(212, 20);
            this.m_selectCombo.TabIndex = 0;
            // 
            // m_selecttext
            // 
            this.m_selecttext.Location = new System.Drawing.Point(233, 6);
            this.m_selecttext.Name = "m_selecttext";
            this.m_selecttext.Size = new System.Drawing.Size(227, 19);
            this.m_selecttext.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(2, 31);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_templetelist);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_userno);
            this.splitContainer1.Panel2.Controls.Add(this.m_usernameCombo);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.m_templete_type_combo);
            this.splitContainer1.Panel2.Controls.Add(this.m_tempno);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.m_content);
            this.splitContainer1.Panel2.Controls.Add(this.m_title);
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.m_deleteBtn);
            this.splitContainer1.Panel2.Controls.Add(this.m_labelinputOpe);
            this.splitContainer1.Panel2.Controls.Add(this.label11);
            this.splitContainer1.Panel2.Controls.Add(this.m_idlabel);
            this.splitContainer1.Panel2.Controls.Add(this.label12);
            this.splitContainer1.Panel2.Controls.Add(this.m_cancelbtn);
            this.splitContainer1.Panel2.Controls.Add(this.m_kousin_btn);
            this.splitContainer1.Panel2.Controls.Add(this.m_templetename);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(777, 527);
            this.splitContainer1.SplitterDistance = 206;
            this.splitContainer1.TabIndex = 6;
            // 
            // m_templetelist
            // 
            this.m_templetelist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_templetelist.GridLines = true;
            this.m_templetelist.Location = new System.Drawing.Point(0, 0);
            this.m_templetelist.Name = "m_templetelist";
            this.m_templetelist.Size = new System.Drawing.Size(773, 202);
            this.m_templetelist.TabIndex = 0;
            this.m_templetelist.UseCompatibleStateImageBehavior = false;
            this.m_templetelist.View = System.Windows.Forms.View.Details;
            this.m_templetelist.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.m_templetelist_MouseDoubleClick);
            // 
            // m_userno
            // 
            this.m_userno.Location = new System.Drawing.Point(141, 79);
            this.m_userno.Name = "m_userno";
            this.m_userno.ReadOnly = true;
            this.m_userno.Size = new System.Drawing.Size(44, 19);
            this.m_userno.TabIndex = 152;
            this.m_userno.TabStop = false;
            // 
            // m_usernameCombo
            // 
            this.m_usernameCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_usernameCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_usernameCombo.FormattingEnabled = true;
            this.m_usernameCombo.Location = new System.Drawing.Point(191, 78);
            this.m_usernameCombo.Name = "m_usernameCombo";
            this.m_usernameCombo.Size = new System.Drawing.Size(320, 20);
            this.m_usernameCombo.TabIndex = 153;
            this.m_usernameCombo.SelectionChangeCommitted += new System.EventHandler(this.m_usernameCombo_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 151;
            this.label1.Text = "テンプレート種別";
            // 
            // m_templete_type_combo
            // 
            this.m_templete_type_combo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_templete_type_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_templete_type_combo.FormattingEnabled = true;
            this.m_templete_type_combo.Items.AddRange(new object[] {
            "インシデント",
            "タスク(インシデントタスク・計画作業)"});
            this.m_templete_type_combo.Location = new System.Drawing.Point(140, 30);
            this.m_templete_type_combo.Name = "m_templete_type_combo";
            this.m_templete_type_combo.Size = new System.Drawing.Size(371, 20);
            this.m_templete_type_combo.TabIndex = 150;
            // 
            // m_tempno
            // 
            this.m_tempno.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tempno.Location = new System.Drawing.Point(141, 8);
            this.m_tempno.Name = "m_tempno";
            this.m_tempno.ReadOnly = true;
            this.m_tempno.Size = new System.Drawing.Size(370, 19);
            this.m_tempno.TabIndex = 116;
            this.m_tempno.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 117;
            this.label4.Text = "テンプレート通番";
            // 
            // m_content
            // 
            this.m_content.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_content.Location = new System.Drawing.Point(8, 145);
            this.m_content.Multiline = true;
            this.m_content.Name = "m_content";
            this.m_content.Size = new System.Drawing.Size(503, 142);
            this.m_content.TabIndex = 4;
            // 
            // m_title
            // 
            this.m_title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_title.Location = new System.Drawing.Point(141, 102);
            this.m_title.Name = "m_title";
            this.m_title.Size = new System.Drawing.Size(370, 19);
            this.m_title.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 106);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 12);
            this.label7.TabIndex = 115;
            this.label7.Text = "タイトル(インシデントのみ)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 12);
            this.label6.TabIndex = 113;
            this.label6.Text = "カスタマ名";
            // 
            // m_deleteBtn
            // 
            this.m_deleteBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_deleteBtn.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_deleteBtn.ForeColor = System.Drawing.Color.Red;
            this.m_deleteBtn.Location = new System.Drawing.Point(687, 7);
            this.m_deleteBtn.Name = "m_deleteBtn";
            this.m_deleteBtn.Size = new System.Drawing.Size(76, 27);
            this.m_deleteBtn.TabIndex = 9;
            this.m_deleteBtn.Text = "削除";
            this.m_deleteBtn.UseVisualStyleBackColor = true;
            this.m_deleteBtn.Click += new System.EventHandler(this.m_deleteBtn_Click);
            // 
            // m_labelinputOpe
            // 
            this.m_labelinputOpe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_labelinputOpe.Location = new System.Drawing.Point(580, 223);
            this.m_labelinputOpe.Name = "m_labelinputOpe";
            this.m_labelinputOpe.ReadOnly = true;
            this.m_labelinputOpe.Size = new System.Drawing.Size(183, 19);
            this.m_labelinputOpe.TabIndex = 6;
            this.m_labelinputOpe.TabStop = false;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(521, 226);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 111;
            this.label11.Text = "更新者";
            // 
            // m_idlabel
            // 
            this.m_idlabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_idlabel.Location = new System.Drawing.Point(580, 195);
            this.m_idlabel.Name = "m_idlabel";
            this.m_idlabel.ReadOnly = true;
            this.m_idlabel.Size = new System.Drawing.Size(183, 19);
            this.m_idlabel.TabIndex = 5;
            this.m_idlabel.TabStop = false;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(521, 199);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 110;
            this.label12.Text = "更新日時";
            // 
            // m_cancelbtn
            // 
            this.m_cancelbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cancelbtn.Location = new System.Drawing.Point(685, 280);
            this.m_cancelbtn.Name = "m_cancelbtn";
            this.m_cancelbtn.Size = new System.Drawing.Size(78, 28);
            this.m_cancelbtn.TabIndex = 8;
            this.m_cancelbtn.Text = "キャンセル";
            this.m_cancelbtn.UseVisualStyleBackColor = true;
            this.m_cancelbtn.Click += new System.EventHandler(this.m_cancelbtn_Click);
            // 
            // m_kousin_btn
            // 
            this.m_kousin_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_kousin_btn.Location = new System.Drawing.Point(601, 280);
            this.m_kousin_btn.Name = "m_kousin_btn";
            this.m_kousin_btn.Size = new System.Drawing.Size(78, 28);
            this.m_kousin_btn.TabIndex = 7;
            this.m_kousin_btn.Text = "更新";
            this.m_kousin_btn.UseVisualStyleBackColor = true;
            this.m_kousin_btn.Click += new System.EventHandler(this.m_kousin_btn_Click);
            // 
            // m_templetename
            // 
            this.m_templetename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_templetename.Location = new System.Drawing.Point(141, 55);
            this.m_templetename.Name = "m_templetename";
            this.m_templetename.Size = new System.Drawing.Size(370, 19);
            this.m_templetename.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 100;
            this.label3.Text = "テンプレート内容";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 99;
            this.label2.Text = "テンプレート名";
            // 
            // Form_inc_templete_update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.m_select_btn);
            this.Controls.Add(this.m_selectCombo);
            this.Controls.Add(this.m_selecttext);
            this.Name = "Form_inc_templete_update";
            this.Text = "インシデント/タスクテンプレート編集";
            this.Load += new System.EventHandler(this.Form_inc_templete_update_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_select_btn;
        private System.Windows.Forms.ComboBox m_selectCombo;
        private System.Windows.Forms.TextBox m_selecttext;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView m_templetelist;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button m_deleteBtn;
        private System.Windows.Forms.TextBox m_labelinputOpe;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox m_idlabel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button m_cancelbtn;
        private System.Windows.Forms.Button m_kousin_btn;
        private System.Windows.Forms.TextBox m_templetename;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_title;
        private System.Windows.Forms.TextBox m_content;
        private System.Windows.Forms.TextBox m_tempno;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox m_templete_type_combo;
        private System.Windows.Forms.TextBox m_userno;
        private System.Windows.Forms.ComboBox m_usernameCombo;
    }
}