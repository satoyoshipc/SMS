namespace moss_AP
{
    partial class Form_login
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
            this.m_opeid = new System.Windows.Forms.TextBox();
            this.m_pass = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_versioninfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_opeid
            // 
            this.m_opeid.Location = new System.Drawing.Point(81, 13);
            this.m_opeid.Name = "m_opeid";
            this.m_opeid.Size = new System.Drawing.Size(194, 19);
            this.m_opeid.TabIndex = 0;
            this.m_opeid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_opeid_KeyDown);
            // 
            // m_pass
            // 
            this.m_pass.Location = new System.Drawing.Point(81, 38);
            this.m_pass.Name = "m_pass";
            this.m_pass.PasswordChar = '*';
            this.m_pass.Size = new System.Drawing.Size(194, 19);
            this.m_pass.TabIndex = 1;
            this.m_pass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_pass_KeyDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(119, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(200, 63);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "キャンセル";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "オペレータ名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "パスワード";
            // 
            // m_versioninfo
            // 
            this.m_versioninfo.AutoSize = true;
            this.m_versioninfo.Location = new System.Drawing.Point(4, 92);
            this.m_versioninfo.Name = "m_versioninfo";
            this.m_versioninfo.Size = new System.Drawing.Size(35, 12);
            this.m_versioninfo.TabIndex = 7;
            this.m_versioninfo.Text = "label3";
            // 
            // Form_login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 108);
            this.ControlBox = false;
            this.Controls.Add(this.m_versioninfo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.m_pass);
            this.Controls.Add(this.m_opeid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form_login";
            this.Text = "ログイン";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_login_FormClosed);
            this.Load += new System.EventHandler(this.Form_login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_opeid;
        private System.Windows.Forms.TextBox m_pass;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label m_versioninfo;
    }
}