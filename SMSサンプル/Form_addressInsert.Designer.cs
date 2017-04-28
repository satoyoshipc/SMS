namespace SMSサンプル
{
    partial class Form_addressInsert
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
            this.m_idlabel = new System.Windows.Forms.TextBox();
            this.m_pass = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_labelinputOpe = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.m_fastname = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.m_lastname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_opeID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_idlabel
            // 
            this.m_idlabel.Location = new System.Drawing.Point(120, 202);
            this.m_idlabel.Name = "m_idlabel";
            this.m_idlabel.ReadOnly = true;
            this.m_idlabel.Size = new System.Drawing.Size(44, 19);
            this.m_idlabel.TabIndex = 53;
            this.m_idlabel.TabStop = false;
            // 
            // m_pass
            // 
            this.m_pass.Location = new System.Drawing.Point(120, 152);
            this.m_pass.Name = "m_pass";
            this.m_pass.PasswordChar = '*';
            this.m_pass.Size = new System.Drawing.Size(274, 19);
            this.m_pass.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 12);
            this.label6.TabIndex = 50;
            this.label6.Text = "メールアドレス";
            // 
            // m_labelinputOpe
            // 
            this.m_labelinputOpe.Location = new System.Drawing.Point(170, 202);
            this.m_labelinputOpe.Name = "m_labelinputOpe";
            this.m_labelinputOpe.ReadOnly = true;
            this.m_labelinputOpe.Size = new System.Drawing.Size(224, 19);
            this.m_labelinputOpe.TabIndex = 49;
            this.m_labelinputOpe.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 12);
            this.label5.TabIndex = 48;
            this.label5.Text = "オペレータ";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(198, 234);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "登録";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(299, 234);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "キャンセル";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // m_fastname
            // 
            this.m_fastname.Location = new System.Drawing.Point(120, 98);
            this.m_fastname.Name = "m_fastname";
            this.m_fastname.Size = new System.Drawing.Size(59, 19);
            this.m_fastname.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(18, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "CSV登録";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 42;
            this.label3.Text = "アドレス番号";
            // 
            // m_lastname
            // 
            this.m_lastname.Location = new System.Drawing.Point(120, 72);
            this.m_lastname.Name = "m_lastname";
            this.m_lastname.Size = new System.Drawing.Size(274, 19);
            this.m_lastname.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 12);
            this.label2.TabIndex = 40;
            this.label2.Text = "ユーザオペレータ名";
            // 
            // m_opeID
            // 
            this.m_opeID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_opeID.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.m_opeID.Location = new System.Drawing.Point(120, 47);
            this.m_opeID.Name = "m_opeID";
            this.m_opeID.Size = new System.Drawing.Size(274, 19);
            this.m_opeID.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 12);
            this.label1.TabIndex = 38;
            this.label1.Text = "ユーザオペレータID";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(120, 177);
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '*';
            this.textBox1.Size = new System.Drawing.Size(274, 19);
            this.textBox1.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 181);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 55;
            this.label4.Text = "アドレス名";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(120, 123);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 126);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 58;
            this.label7.Text = "ユーザ区分";
            // 
            // Form_addressInsert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 269);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_idlabel);
            this.Controls.Add(this.m_pass);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.m_labelinputOpe);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.m_fastname);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_lastname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_opeID);
            this.Controls.Add(this.label1);
            this.Name = "Form_addressInsert";
            this.Text = "メールアドレス登録";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_idlabel;
        private System.Windows.Forms.TextBox m_pass;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox m_labelinputOpe;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox m_fastname;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_lastname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_opeID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label7;
    }
}