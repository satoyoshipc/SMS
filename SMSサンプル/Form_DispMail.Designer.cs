namespace moss_AP
{
    partial class Form_DispMail
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
            this.m_body = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.m_subject = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_body
            // 
            this.m_body.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_body.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.m_body.Location = new System.Drawing.Point(6, 53);
            this.m_body.Multiline = true;
            this.m_body.Name = "m_body";
            this.m_body.ReadOnly = true;
            this.m_body.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.m_body.Size = new System.Drawing.Size(529, 420);
            this.m_body.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(462, 479);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 22);
            this.button1.TabIndex = 10;
            this.button1.Text = "戻る";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 31);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 19;
            this.label10.Text = "件名";
            // 
            // m_subject
            // 
            this.m_subject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_subject.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.m_subject.Location = new System.Drawing.Point(39, 28);
            this.m_subject.Name = "m_subject";
            this.m_subject.ReadOnly = true;
            this.m_subject.Size = new System.Drawing.Size(496, 19);
            this.m_subject.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(383, 479);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(73, 22);
            this.button2.TabIndex = 20;
            this.button2.Text = "メール出力";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(40, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(288, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "件名本文を確認し、メール出力ボタンを押してください。";
            // 
            // Form_DispMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 505);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.m_subject);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.m_body);
            this.Name = "Form_DispMail";
            this.Text = "メール出力";
            this.Load += new System.EventHandler(this.Form_DispMail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox m_body;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox m_subject;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
    }
}