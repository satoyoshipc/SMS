namespace SMSサンプル
{
    partial class Form_MailTempleteInsert
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
            this.m_templete = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_subject = new System.Windows.Forms.TextBox();
            this.m_context = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.m_idlabel = new System.Windows.Forms.TextBox();
            this.m_labelinputOpe = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.m_attach1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.m_attach2 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.m_attach3 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.m_attach5 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.m_attach4 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_To_list = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_Cc_list = new System.Windows.Forms.ListView();
            this.m_Bcc_list = new System.Windows.Forms.ListView();
            this.To_add_Link = new System.Windows.Forms.LinkLabel();
            this.Cc_add_Link = new System.Windows.Forms.LinkLabel();
            this.Bcc_add_Link = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "テンプレート名";
            // 
            // m_templete
            // 
            this.m_templete.Location = new System.Drawing.Point(82, 14);
            this.m_templete.Name = "m_templete";
            this.m_templete.Size = new System.Drawing.Size(453, 19);
            this.m_templete.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "件名";
            // 
            // m_subject
            // 
            this.m_subject.Location = new System.Drawing.Point(82, 39);
            this.m_subject.Name = "m_subject";
            this.m_subject.Size = new System.Drawing.Size(453, 19);
            this.m_subject.TabIndex = 1;
            // 
            // m_context
            // 
            this.m_context.Location = new System.Drawing.Point(82, 68);
            this.m_context.Multiline = true;
            this.m_context.Name = "m_context";
            this.m_context.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.m_context.Size = new System.Drawing.Size(453, 509);
            this.m_context.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "本文";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(549, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "To:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(549, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "Cc:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(549, 229);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "Bcc:";
            // 
            // m_idlabel
            // 
            this.m_idlabel.Location = new System.Drawing.Point(613, 515);
            this.m_idlabel.Name = "m_idlabel";
            this.m_idlabel.ReadOnly = true;
            this.m_idlabel.Size = new System.Drawing.Size(44, 19);
            this.m_idlabel.TabIndex = 39;
            this.m_idlabel.TabStop = false;
            // 
            // m_labelinputOpe
            // 
            this.m_labelinputOpe.Location = new System.Drawing.Point(663, 515);
            this.m_labelinputOpe.Name = "m_labelinputOpe";
            this.m_labelinputOpe.ReadOnly = true;
            this.m_labelinputOpe.Size = new System.Drawing.Size(224, 19);
            this.m_labelinputOpe.TabIndex = 38;
            this.m_labelinputOpe.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(548, 518);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 12);
            this.label8.TabIndex = 37;
            this.label8.Text = "オペレータ";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(691, 554);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 23);
            this.button3.TabIndex = 19;
            this.button3.Text = "登録";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(792, 554);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "キャンセル";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // m_attach1
            // 
            this.m_attach1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_attach1.Location = new System.Drawing.Point(584, 322);
            this.m_attach1.Name = "m_attach1";
            this.m_attach1.Size = new System.Drawing.Size(303, 19);
            this.m_attach1.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(544, 325);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 12);
            this.label9.TabIndex = 41;
            this.label9.Text = "添付1";
            // 
            // m_attach2
            // 
            this.m_attach2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_attach2.Location = new System.Drawing.Point(584, 347);
            this.m_attach2.Name = "m_attach2";
            this.m_attach2.Size = new System.Drawing.Size(302, 19);
            this.m_attach2.TabIndex = 12;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(543, 350);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 12);
            this.label10.TabIndex = 43;
            this.label10.Text = "添付2";
            // 
            // m_attach3
            // 
            this.m_attach3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_attach3.Location = new System.Drawing.Point(584, 372);
            this.m_attach3.Name = "m_attach3";
            this.m_attach3.Size = new System.Drawing.Size(302, 19);
            this.m_attach3.TabIndex = 14;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(543, 375);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 12);
            this.label11.TabIndex = 45;
            this.label11.Text = "添付3";
            // 
            // m_attach5
            // 
            this.m_attach5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_attach5.Location = new System.Drawing.Point(585, 422);
            this.m_attach5.Name = "m_attach5";
            this.m_attach5.Size = new System.Drawing.Size(302, 19);
            this.m_attach5.TabIndex = 18;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(544, 427);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 12);
            this.label12.TabIndex = 49;
            this.label12.Text = "添付5";
            // 
            // m_attach4
            // 
            this.m_attach4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_attach4.Location = new System.Drawing.Point(585, 397);
            this.m_attach4.Name = "m_attach4";
            this.m_attach4.Size = new System.Drawing.Size(302, 19);
            this.m_attach4.TabIndex = 16;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(544, 400);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 12);
            this.label13.TabIndex = 47;
            this.label13.Text = "添付4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(546, 306);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(198, 12);
            this.label4.TabIndex = 55;
            this.label4.Text = "添付ファイル有無と添付ファイル名を入力";
            // 
            // m_To_list
            // 
            this.m_To_list.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_To_list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.m_To_list.FullRowSelect = true;
            this.m_To_list.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_To_list.Location = new System.Drawing.Point(585, 13);
            this.m_To_list.Name = "m_To_list";
            this.m_To_list.Size = new System.Drawing.Size(301, 106);
            this.m_To_list.TabIndex = 4;
            this.m_To_list.UseCompatibleStateImageBehavior = false;
            this.m_To_list.View = System.Windows.Forms.View.Details;
            this.m_To_list.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_To_list_KeyDown);
            // 
            // m_Cc_list
            // 
            this.m_Cc_list.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_Cc_list.FullRowSelect = true;
            this.m_Cc_list.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_Cc_list.Location = new System.Drawing.Point(585, 125);
            this.m_Cc_list.Name = "m_Cc_list";
            this.m_Cc_list.Size = new System.Drawing.Size(301, 89);
            this.m_Cc_list.TabIndex = 6;
            this.m_Cc_list.UseCompatibleStateImageBehavior = false;
            this.m_Cc_list.View = System.Windows.Forms.View.Details;
            this.m_Cc_list.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_Cc_list_KeyDown);
            // 
            // m_Bcc_list
            // 
            this.m_Bcc_list.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_Bcc_list.FullRowSelect = true;
            this.m_Bcc_list.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_Bcc_list.Location = new System.Drawing.Point(585, 227);
            this.m_Bcc_list.Name = "m_Bcc_list";
            this.m_Bcc_list.Size = new System.Drawing.Size(301, 61);
            this.m_Bcc_list.TabIndex = 8;
            this.m_Bcc_list.UseCompatibleStateImageBehavior = false;
            this.m_Bcc_list.View = System.Windows.Forms.View.Details;
            this.m_Bcc_list.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_Bcc_list_KeyDown);
            // 
            // To_add_Link
            // 
            this.To_add_Link.AutoSize = true;
            this.To_add_Link.Location = new System.Drawing.Point(551, 30);
            this.To_add_Link.Name = "To_add_Link";
            this.To_add_Link.Size = new System.Drawing.Size(29, 12);
            this.To_add_Link.TabIndex = 3;
            this.To_add_Link.TabStop = true;
            this.To_add_Link.Text = "追加";
            this.To_add_Link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.To_add_Link_LinkClicked);
            // 
            // Cc_add_Link
            // 
            this.Cc_add_Link.AutoSize = true;
            this.Cc_add_Link.Location = new System.Drawing.Point(551, 141);
            this.Cc_add_Link.Name = "Cc_add_Link";
            this.Cc_add_Link.Size = new System.Drawing.Size(29, 12);
            this.Cc_add_Link.TabIndex = 5;
            this.Cc_add_Link.TabStop = true;
            this.Cc_add_Link.Text = "追加";
            this.Cc_add_Link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Cc_add_Link_LinkClicked);
            // 
            // Bcc_add_Link
            // 
            this.Bcc_add_Link.AutoSize = true;
            this.Bcc_add_Link.Location = new System.Drawing.Point(551, 246);
            this.Bcc_add_Link.Name = "Bcc_add_Link";
            this.Bcc_add_Link.Size = new System.Drawing.Size(29, 12);
            this.Bcc_add_Link.TabIndex = 7;
            this.Bcc_add_Link.TabStop = true;
            this.Bcc_add_Link.Text = "追加";
            this.Bcc_add_Link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Bcc_add_Link_LinkClicked);
            // 
            // Form_MailTempleteInsert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 589);
            this.Controls.Add(this.Bcc_add_Link);
            this.Controls.Add(this.Cc_add_Link);
            this.Controls.Add(this.To_add_Link);
            this.Controls.Add(this.m_Bcc_list);
            this.Controls.Add(this.m_Cc_list);
            this.Controls.Add(this.m_To_list);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_attach5);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.m_attach4);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.m_attach3);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.m_attach2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.m_attach1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.m_idlabel);
            this.Controls.Add(this.m_labelinputOpe);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.m_context);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_subject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_templete);
            this.Controls.Add(this.label1);
            this.Name = "Form_MailTempleteInsert";
            this.Text = "メールテンプレート登録";
            this.Load += new System.EventHandler(this.Form_MailTempleteInsert_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_templete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_subject;
        private System.Windows.Forms.TextBox m_context;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox m_idlabel;
        private System.Windows.Forms.TextBox m_labelinputOpe;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox m_attach1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox m_attach2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox m_attach3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox m_attach5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox m_attach4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView m_To_list;
        private System.Windows.Forms.ListView m_Cc_list;
        private System.Windows.Forms.ListView m_Bcc_list;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.LinkLabel To_add_Link;
        private System.Windows.Forms.LinkLabel Cc_add_Link;
        private System.Windows.Forms.LinkLabel Bcc_add_Link;
    }
}