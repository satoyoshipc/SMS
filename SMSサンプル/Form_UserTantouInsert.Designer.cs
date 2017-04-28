namespace SMSサンプル
{
    partial class Form_UserTantouInsert
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
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.m_idlabel = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_riyouRadio = new System.Windows.Forms.RadioButton();
            this.m_kanriRadio = new System.Windows.Forms.RadioButton();
            this.m_pass = new System.Windows.Forms.TextBox();
            this.m_labelinputOpe = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.m_fastname = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.m_biko = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_lastname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(100, 231);
            this.textBox5.Name = "textBox5";
            this.textBox5.PasswordChar = '*';
            this.textBox5.Size = new System.Drawing.Size(298, 19);
            this.textBox5.TabIndex = 120;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 262);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 117;
            this.label11.Text = "備考";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 234);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 115;
            this.label10.Text = "役職";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(100, 205);
            this.textBox4.Name = "textBox4";
            this.textBox4.PasswordChar = '*';
            this.textBox4.Size = new System.Drawing.Size(298, 19);
            this.textBox4.TabIndex = 114;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(98, 41);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(44, 19);
            this.textBox1.TabIndex = 110;
            this.textBox1.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(148, 40);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(250, 20);
            this.comboBox1.TabIndex = 108;
            // 
            // m_idlabel
            // 
            this.m_idlabel.Location = new System.Drawing.Point(98, 322);
            this.m_idlabel.Name = "m_idlabel";
            this.m_idlabel.ReadOnly = true;
            this.m_idlabel.Size = new System.Drawing.Size(44, 19);
            this.m_idlabel.TabIndex = 107;
            this.m_idlabel.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_riyouRadio);
            this.groupBox1.Controls.Add(this.m_kanriRadio);
            this.groupBox1.Location = new System.Drawing.Point(21, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 50);
            this.groupBox1.TabIndex = 106;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "有効/無効*";
            // 
            // m_riyouRadio
            // 
            this.m_riyouRadio.AutoSize = true;
            this.m_riyouRadio.Location = new System.Drawing.Point(172, 22);
            this.m_riyouRadio.Name = "m_riyouRadio";
            this.m_riyouRadio.Size = new System.Drawing.Size(47, 16);
            this.m_riyouRadio.TabIndex = 36;
            this.m_riyouRadio.Text = "無効";
            this.m_riyouRadio.UseVisualStyleBackColor = true;
            // 
            // m_kanriRadio
            // 
            this.m_kanriRadio.AutoSize = true;
            this.m_kanriRadio.Checked = true;
            this.m_kanriRadio.Location = new System.Drawing.Point(68, 22);
            this.m_kanriRadio.Name = "m_kanriRadio";
            this.m_kanriRadio.Size = new System.Drawing.Size(47, 16);
            this.m_kanriRadio.TabIndex = 35;
            this.m_kanriRadio.TabStop = true;
            this.m_kanriRadio.Text = "有効";
            this.m_kanriRadio.UseVisualStyleBackColor = true;
            // 
            // m_pass
            // 
            this.m_pass.Location = new System.Drawing.Point(100, 179);
            this.m_pass.Name = "m_pass";
            this.m_pass.PasswordChar = '*';
            this.m_pass.Size = new System.Drawing.Size(298, 19);
            this.m_pass.TabIndex = 105;
            // 
            // m_labelinputOpe
            // 
            this.m_labelinputOpe.Location = new System.Drawing.Point(148, 322);
            this.m_labelinputOpe.Name = "m_labelinputOpe";
            this.m_labelinputOpe.ReadOnly = true;
            this.m_labelinputOpe.Size = new System.Drawing.Size(250, 19);
            this.m_labelinputOpe.TabIndex = 104;
            this.m_labelinputOpe.TabStop = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(203, 348);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 23);
            this.button3.TabIndex = 103;
            this.button3.Text = "登録";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(304, 347);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 23);
            this.button2.TabIndex = 102;
            this.button2.Text = "キャンセル";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // m_fastname
            // 
            this.m_fastname.Location = new System.Drawing.Point(100, 153);
            this.m_fastname.Name = "m_fastname";
            this.m_fastname.Size = new System.Drawing.Size(298, 19);
            this.m_fastname.TabIndex = 101;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(18, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 100;
            this.button1.Text = "CSV登録";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // m_biko
            // 
            this.m_biko.Location = new System.Drawing.Point(100, 257);
            this.m_biko.Multiline = true;
            this.m_biko.Name = "m_biko";
            this.m_biko.Size = new System.Drawing.Size(298, 59);
            this.m_biko.TabIndex = 99;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 12);
            this.label3.TabIndex = 98;
            this.label3.Text = "担当者名(カナ)";
            // 
            // m_lastname
            // 
            this.m_lastname.Location = new System.Drawing.Point(100, 127);
            this.m_lastname.Name = "m_lastname";
            this.m_lastname.Size = new System.Drawing.Size(298, 19);
            this.m_lastname.TabIndex = 97;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 96;
            this.label2.Text = "担当者名";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 122;
            this.label1.Text = "ユーザ名";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 123;
            this.label4.Text = "部署名";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 209);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 124;
            this.label5.Text = "電話番号";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 325);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 12);
            this.label6.TabIndex = 125;
            this.label6.Text = "オペレータ";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(15, 348);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(121, 23);
            this.button4.TabIndex = 126;
            this.button4.Text = "メールアドレス登録";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form_UserTantouInsert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 383);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.m_idlabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m_pass);
            this.Controls.Add(this.m_labelinputOpe);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.m_fastname);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.m_biko);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_lastname);
            this.Controls.Add(this.label2);
            this.Name = "Form_UserTantouInsert";
            this.Text = "担当ユーザ登録";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox m_idlabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton m_riyouRadio;
        private System.Windows.Forms.RadioButton m_kanriRadio;
        private System.Windows.Forms.TextBox m_pass;
        private System.Windows.Forms.TextBox m_labelinputOpe;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox m_fastname;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox m_biko;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_lastname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button4;
    }
}