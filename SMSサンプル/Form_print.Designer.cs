namespace SMSサンプル
{
    partial class Form_print
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
            this.m_start_date = new System.Windows.Forms.DateTimePicker();
            this.m_end_date = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.m_schedule_Type = new System.Windows.Forms.TextBox();
            this.m_todaychk = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // m_start_date
            // 
            this.m_start_date.CustomFormat = "yyyy年 M月 d日(ddd) HH:mm";
            this.m_start_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_start_date.Location = new System.Drawing.Point(12, 53);
            this.m_start_date.Name = "m_start_date";
            this.m_start_date.Size = new System.Drawing.Size(194, 19);
            this.m_start_date.TabIndex = 0;
            // 
            // m_end_date
            // 
            this.m_end_date.CustomFormat = "yyyy年 M月 d日(ddd) HH:mm";
            this.m_end_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_end_date.Location = new System.Drawing.Point(224, 53);
            this.m_end_date.Name = "m_end_date";
            this.m_end_date.Size = new System.Drawing.Size(192, 19);
            this.m_end_date.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(209, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "印刷対象日時";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(218, 123);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(96, 23);
            this.button3.TabIndex = 147;
            this.button3.Text = "印刷";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(320, 123);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 23);
            this.button2.TabIndex = 148;
            this.button2.Text = "キャンセル";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // m_schedule_Type
            // 
            this.m_schedule_Type.Location = new System.Drawing.Point(12, 88);
            this.m_schedule_Type.Name = "m_schedule_Type";
            this.m_schedule_Type.ReadOnly = true;
            this.m_schedule_Type.Size = new System.Drawing.Size(404, 19);
            this.m_schedule_Type.TabIndex = 149;
            // 
            // m_todaychk
            // 
            this.m_todaychk.AutoSize = true;
            this.m_todaychk.Location = new System.Drawing.Point(12, 31);
            this.m_todaychk.Name = "m_todaychk";
            this.m_todaychk.Size = new System.Drawing.Size(48, 16);
            this.m_todaychk.TabIndex = 150;
            this.m_todaychk.Text = "本日";
            this.m_todaychk.UseVisualStyleBackColor = true;
            this.m_todaychk.CheckedChanged += new System.EventHandler(this.m_todaychk_CheckedChanged);
            // 
            // Form_print
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 158);
            this.Controls.Add(this.m_todaychk);
            this.Controls.Add(this.m_schedule_Type);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_end_date);
            this.Controls.Add(this.m_start_date);
            this.Name = "Form_print";
            this.Text = "印刷";
            this.Load += new System.EventHandler(this.Form_print_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker m_start_date;
        private System.Windows.Forms.DateTimePicker m_end_date;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox m_schedule_Type;
        private System.Windows.Forms.CheckBox m_todaychk;
    }
}