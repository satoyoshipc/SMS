namespace moss_AP
{
    partial class Form_inc_templete_insert
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
            this.label1 = new System.Windows.Forms.Label();
            this.m_userno = new System.Windows.Forms.TextBox();
            this.m_usernameCombo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_templete_type_combo = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.m_content = new System.Windows.Forms.TextBox();
            this.m_title = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_templete_name = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_idlabel = new System.Windows.Forms.TextBox();
            this.m_labelinputOpe = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 12);
            this.label1.TabIndex = 146;
            this.label1.Text = "カスタマ名";
            // 
            // m_userno
            // 
            this.m_userno.Location = new System.Drawing.Point(99, 75);
            this.m_userno.Name = "m_userno";
            this.m_userno.ReadOnly = true;
            this.m_userno.Size = new System.Drawing.Size(44, 19);
            this.m_userno.TabIndex = 2;
            this.m_userno.TabStop = false;
            // 
            // m_usernameCombo
            // 
            this.m_usernameCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_usernameCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_usernameCombo.FormattingEnabled = true;
            this.m_usernameCombo.Location = new System.Drawing.Point(149, 74);
            this.m_usernameCombo.Name = "m_usernameCombo";
            this.m_usernameCombo.Size = new System.Drawing.Size(408, 20);
            this.m_usernameCombo.TabIndex = 3;
            this.m_usernameCombo.SelectionChangeCommitted += new System.EventHandler(this.m_usernameCombo_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 149;
            this.label2.Text = "テンプレート種別";
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
            this.m_templete_type_combo.Location = new System.Drawing.Point(99, 12);
            this.m_templete_type_combo.Name = "m_templete_type_combo";
            this.m_templete_type_combo.Size = new System.Drawing.Size(458, 20);
            this.m_templete_type_combo.TabIndex = 0;
            this.m_templete_type_combo.SelectionChangeCommitted += new System.EventHandler(this.m_templete_type_combo_SelectionChangeCommitted);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(361, 502);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 27);
            this.button3.TabIndex = 6;
            this.button3.Text = "登録";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(462, 502);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 27);
            this.button2.TabIndex = 7;
            this.button2.Text = "キャンセル";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 155;
            this.label5.Text = "テンプレート内容";
            // 
            // m_content
            // 
            this.m_content.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_content.Location = new System.Drawing.Point(10, 173);
            this.m_content.Multiline = true;
            this.m_content.Name = "m_content";
            this.m_content.Size = new System.Drawing.Size(547, 301);
            this.m_content.TabIndex = 5;
            // 
            // m_title
            // 
            this.m_title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_title.Location = new System.Drawing.Point(9, 126);
            this.m_title.Name = "m_title";
            this.m_title.Size = new System.Drawing.Size(548, 19);
            this.m_title.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 12);
            this.label3.TabIndex = 154;
            this.label3.Text = "タイトル(インシデントのみ)";
            // 
            // m_templete_name
            // 
            this.m_templete_name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_templete_name.Location = new System.Drawing.Point(99, 49);
            this.m_templete_name.Name = "m_templete_name";
            this.m_templete_name.Size = new System.Drawing.Size(459, 19);
            this.m_templete_name.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 157;
            this.label4.Text = "テンプレート名";
            // 
            // m_idlabel
            // 
            this.m_idlabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_idlabel.Location = new System.Drawing.Point(99, 478);
            this.m_idlabel.Name = "m_idlabel";
            this.m_idlabel.ReadOnly = true;
            this.m_idlabel.Size = new System.Drawing.Size(44, 19);
            this.m_idlabel.TabIndex = 158;
            this.m_idlabel.TabStop = false;
            // 
            // m_labelinputOpe
            // 
            this.m_labelinputOpe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_labelinputOpe.Location = new System.Drawing.Point(149, 479);
            this.m_labelinputOpe.Name = "m_labelinputOpe";
            this.m_labelinputOpe.ReadOnly = true;
            this.m_labelinputOpe.Size = new System.Drawing.Size(409, 19);
            this.m_labelinputOpe.TabIndex = 159;
            this.m_labelinputOpe.TabStop = false;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 482);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 12);
            this.label6.TabIndex = 160;
            this.label6.Text = "オペレータ";
            // 
            // Form_inc_templete_insert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 534);
            this.Controls.Add(this.m_idlabel);
            this.Controls.Add(this.m_labelinputOpe);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.m_templete_name);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.m_content);
            this.Controls.Add(this.m_title);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_templete_type_combo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_userno);
            this.Controls.Add(this.m_usernameCombo);
            this.Name = "Form_inc_templete_insert";
            this.Text = "インシデント/タスクテンプレート登録";
            this.Load += new System.EventHandler(this.Form_inc_templete_insert_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_userno;
        private System.Windows.Forms.ComboBox m_usernameCombo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox m_templete_type_combo;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox m_content;
        private System.Windows.Forms.TextBox m_title;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_templete_name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox m_idlabel;
        private System.Windows.Forms.TextBox m_labelinputOpe;
        private System.Windows.Forms.Label label6;
    }
}