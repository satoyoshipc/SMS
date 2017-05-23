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
            this.m_yakusyoku = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.m_tel1 = new System.Windows.Forms.TextBox();
            this.m_userno = new System.Windows.Forms.TextBox();
            this.m_usernameCombo = new System.Windows.Forms.ComboBox();
            this.m_idlabel = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_mukouRadio = new System.Windows.Forms.RadioButton();
            this.m_yukouRadio = new System.Windows.Forms.RadioButton();
            this.m_busyo = new System.Windows.Forms.TextBox();
            this.m_labelinputOpe = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.m_tantoukana = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.m_biko = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_tantou_name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.m_tel2 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_yakusyoku
            // 
            this.m_yakusyoku.Location = new System.Drawing.Point(100, 255);
            this.m_yakusyoku.Name = "m_yakusyoku";
            this.m_yakusyoku.Size = new System.Drawing.Size(298, 19);
            this.m_yakusyoku.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 284);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 117;
            this.label11.Text = "備考";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 258);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 115;
            this.label10.Text = "役職";
            // 
            // m_tel1
            // 
            this.m_tel1.Location = new System.Drawing.Point(100, 205);
            this.m_tel1.Name = "m_tel1";
            this.m_tel1.Size = new System.Drawing.Size(298, 19);
            this.m_tel1.TabIndex = 5;
            // 
            // m_userno
            // 
            this.m_userno.Location = new System.Drawing.Point(98, 41);
            this.m_userno.Name = "m_userno";
            this.m_userno.ReadOnly = true;
            this.m_userno.Size = new System.Drawing.Size(44, 19);
            this.m_userno.TabIndex = 110;
            this.m_userno.TabStop = false;
            // 
            // m_usernameCombo
            // 
            this.m_usernameCombo.FormattingEnabled = true;
            this.m_usernameCombo.Location = new System.Drawing.Point(148, 40);
            this.m_usernameCombo.Name = "m_usernameCombo";
            this.m_usernameCombo.Size = new System.Drawing.Size(250, 20);
            this.m_usernameCombo.TabIndex = 0;
            this.m_usernameCombo.SelectionChangeCommitted += new System.EventHandler(this.m_usernameCombo_SelectionChangeCommitted);
            // 
            // m_idlabel
            // 
            this.m_idlabel.Location = new System.Drawing.Point(98, 332);
            this.m_idlabel.Name = "m_idlabel";
            this.m_idlabel.ReadOnly = true;
            this.m_idlabel.Size = new System.Drawing.Size(44, 19);
            this.m_idlabel.TabIndex = 107;
            this.m_idlabel.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_mukouRadio);
            this.groupBox1.Controls.Add(this.m_yukouRadio);
            this.groupBox1.Location = new System.Drawing.Point(21, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 50);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "有効/無効";
            // 
            // m_mukouRadio
            // 
            this.m_mukouRadio.AutoSize = true;
            this.m_mukouRadio.Location = new System.Drawing.Point(172, 22);
            this.m_mukouRadio.Name = "m_mukouRadio";
            this.m_mukouRadio.Size = new System.Drawing.Size(47, 16);
            this.m_mukouRadio.TabIndex = 36;
            this.m_mukouRadio.Text = "無効";
            this.m_mukouRadio.UseVisualStyleBackColor = true;
            // 
            // m_yukouRadio
            // 
            this.m_yukouRadio.AutoSize = true;
            this.m_yukouRadio.Checked = true;
            this.m_yukouRadio.Location = new System.Drawing.Point(68, 22);
            this.m_yukouRadio.Name = "m_yukouRadio";
            this.m_yukouRadio.Size = new System.Drawing.Size(47, 16);
            this.m_yukouRadio.TabIndex = 35;
            this.m_yukouRadio.TabStop = true;
            this.m_yukouRadio.Text = "有効";
            this.m_yukouRadio.UseVisualStyleBackColor = true;
            // 
            // m_busyo
            // 
            this.m_busyo.Location = new System.Drawing.Point(100, 179);
            this.m_busyo.Name = "m_busyo";
            this.m_busyo.Size = new System.Drawing.Size(298, 19);
            this.m_busyo.TabIndex = 4;
            // 
            // m_labelinputOpe
            // 
            this.m_labelinputOpe.Location = new System.Drawing.Point(148, 332);
            this.m_labelinputOpe.Name = "m_labelinputOpe";
            this.m_labelinputOpe.ReadOnly = true;
            this.m_labelinputOpe.Size = new System.Drawing.Size(250, 19);
            this.m_labelinputOpe.TabIndex = 104;
            this.m_labelinputOpe.TabStop = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(203, 358);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "登録";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(304, 357);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "キャンセル";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // m_tantoukana
            // 
            this.m_tantoukana.Location = new System.Drawing.Point(100, 153);
            this.m_tantoukana.Name = "m_tantoukana";
            this.m_tantoukana.Size = new System.Drawing.Size(298, 19);
            this.m_tantoukana.TabIndex = 3;
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
            this.m_biko.Location = new System.Drawing.Point(100, 279);
            this.m_biko.Multiline = true;
            this.m_biko.Name = "m_biko";
            this.m_biko.Size = new System.Drawing.Size(298, 47);
            this.m_biko.TabIndex = 8;
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
            // m_tantou_name
            // 
            this.m_tantou_name.Location = new System.Drawing.Point(100, 127);
            this.m_tantou_name.Name = "m_tantou_name";
            this.m_tantou_name.Size = new System.Drawing.Size(298, 19);
            this.m_tantou_name.TabIndex = 2;
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
            this.label1.Size = new System.Drawing.Size(52, 12);
            this.label1.TabIndex = 122;
            this.label1.Text = "カスタマ名";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 123;
            this.label4.Text = "部署名";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 124;
            this.label5.Text = "電話番号1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 335);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 12);
            this.label6.TabIndex = 125;
            this.label6.Text = "オペレータ";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(15, 358);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(121, 23);
            this.button4.TabIndex = 9;
            this.button4.Text = "メールアドレス登録";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 235);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 128;
            this.label7.Text = "電話番号2";
            // 
            // m_tel2
            // 
            this.m_tel2.Location = new System.Drawing.Point(100, 231);
            this.m_tel2.Name = "m_tel2";
            this.m_tel2.Size = new System.Drawing.Size(298, 19);
            this.m_tel2.TabIndex = 6;
            // 
            // Form_UserTantouInsert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 393);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.m_tel2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_yakusyoku);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.m_tel1);
            this.Controls.Add(this.m_userno);
            this.Controls.Add(this.m_usernameCombo);
            this.Controls.Add(this.m_idlabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m_busyo);
            this.Controls.Add(this.m_labelinputOpe);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.m_tantoukana);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.m_biko);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_tantou_name);
            this.Controls.Add(this.label2);
            this.Name = "Form_UserTantouInsert";
            this.Text = "担当カスタマ登録";
            this.Load += new System.EventHandler(this.Form_UserTantouInsert_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox m_yakusyoku;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox m_tel1;
        private System.Windows.Forms.TextBox m_userno;
        private System.Windows.Forms.ComboBox m_usernameCombo;
        private System.Windows.Forms.TextBox m_idlabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton m_mukouRadio;
        private System.Windows.Forms.RadioButton m_yukouRadio;
        private System.Windows.Forms.TextBox m_busyo;
        private System.Windows.Forms.TextBox m_labelinputOpe;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox m_tantoukana;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox m_biko;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_tantou_name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox m_tel2;
    }
}