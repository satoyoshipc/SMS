namespace SMSサンプル
{
    partial class Form_UserInsert
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
            this.m_username = new System.Windows.Forms.TextBox();
            this.m_userkana = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_reportchk = new System.Windows.Forms.CheckBox();
            this.m_status = new System.Windows.Forms.CheckBox();
            this.m_biko = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.m_userryaku = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.m_dispOpename = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.m_dispOpeNo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "カスタマ名";
            // 
            // m_username
            // 
            this.m_username.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_username.Location = new System.Drawing.Point(99, 50);
            this.m_username.Name = "m_username";
            this.m_username.Size = new System.Drawing.Size(288, 19);
            this.m_username.TabIndex = 1;
            // 
            // m_userkana
            // 
            this.m_userkana.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_userkana.Location = new System.Drawing.Point(99, 71);
            this.m_userkana.Name = "m_userkana";
            this.m_userkana.Size = new System.Drawing.Size(288, 19);
            this.m_userkana.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "カスタマ名カナ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "カスタマ名略称";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "備考";
            // 
            // m_reportchk
            // 
            this.m_reportchk.AutoSize = true;
            this.m_reportchk.Location = new System.Drawing.Point(99, 124);
            this.m_reportchk.Name = "m_reportchk";
            this.m_reportchk.Size = new System.Drawing.Size(85, 16);
            this.m_reportchk.TabIndex = 4;
            this.m_reportchk.Text = "レポート出力";
            this.m_reportchk.UseVisualStyleBackColor = true;
            // 
            // m_status
            // 
            this.m_status.AutoSize = true;
            this.m_status.Location = new System.Drawing.Point(99, 146);
            this.m_status.Name = "m_status";
            this.m_status.Size = new System.Drawing.Size(48, 16);
            this.m_status.TabIndex = 5;
            this.m_status.Text = "有効";
            this.m_status.UseVisualStyleBackColor = true;
            // 
            // m_biko
            // 
            this.m_biko.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_biko.Location = new System.Drawing.Point(99, 172);
            this.m_biko.Multiline = true;
            this.m_biko.Name = "m_biko";
            this.m_biko.Size = new System.Drawing.Size(288, 59);
            this.m_biko.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "CSV登録";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // m_userryaku
            // 
            this.m_userryaku.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_userryaku.Location = new System.Drawing.Point(99, 96);
            this.m_userryaku.Name = "m_userryaku";
            this.m_userryaku.Size = new System.Drawing.Size(288, 19);
            this.m_userryaku.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(292, 261);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "キャンセル";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(191, 261);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "登録";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 238);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "オペレータ";
            // 
            // m_dispOpename
            // 
            this.m_dispOpename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dispOpename.Location = new System.Drawing.Point(138, 235);
            this.m_dispOpename.Name = "m_dispOpename";
            this.m_dispOpename.ReadOnly = true;
            this.m_dispOpename.Size = new System.Drawing.Size(249, 19);
            this.m_dispOpename.TabIndex = 14;
            this.m_dispOpename.TabStop = false;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(14, 261);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(95, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "担当者登録";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // m_dispOpeNo
            // 
            this.m_dispOpeNo.Location = new System.Drawing.Point(99, 235);
            this.m_dispOpeNo.Name = "m_dispOpeNo";
            this.m_dispOpeNo.ReadOnly = true;
            this.m_dispOpeNo.Size = new System.Drawing.Size(35, 19);
            this.m_dispOpeNo.TabIndex = 16;
            this.m_dispOpeNo.TabStop = false;
            // 
            // Form_UserInsert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 291);
            this.Controls.Add(this.m_dispOpeNo);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.m_dispOpename);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.m_userryaku);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.m_biko);
            this.Controls.Add(this.m_status);
            this.Controls.Add(this.m_reportchk);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_userkana);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_username);
            this.Controls.Add(this.label1);
            this.Name = "Form_UserInsert";
            this.Text = "カスタマ登録";
            this.Load += new System.EventHandler(this.Form_UserInsert_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_username;
        private System.Windows.Forms.TextBox m_userkana;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox m_reportchk;
        private System.Windows.Forms.CheckBox m_status;
        private System.Windows.Forms.TextBox m_biko;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox m_userryaku;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox m_dispOpename;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox m_dispOpeNo;
    }
}