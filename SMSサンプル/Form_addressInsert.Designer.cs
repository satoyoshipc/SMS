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
            this.m_address = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_labelinputOpe = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.m_addressno = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_opeID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_addressname = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_Customerkbn_combo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_opename = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // m_idlabel
            // 
            this.m_idlabel.Location = new System.Drawing.Point(123, 164);
            this.m_idlabel.Name = "m_idlabel";
            this.m_idlabel.ReadOnly = true;
            this.m_idlabel.Size = new System.Drawing.Size(44, 19);
            this.m_idlabel.TabIndex = 6;
            this.m_idlabel.TabStop = false;
            // 
            // m_address
            // 
            this.m_address.Location = new System.Drawing.Point(123, 114);
            this.m_address.Name = "m_address";
            this.m_address.Size = new System.Drawing.Size(274, 19);
            this.m_address.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 12);
            this.label6.TabIndex = 50;
            this.label6.Text = "メールアドレス";
            // 
            // m_labelinputOpe
            // 
            this.m_labelinputOpe.Location = new System.Drawing.Point(173, 164);
            this.m_labelinputOpe.Name = "m_labelinputOpe";
            this.m_labelinputOpe.ReadOnly = true;
            this.m_labelinputOpe.Size = new System.Drawing.Size(224, 19);
            this.m_labelinputOpe.TabIndex = 7;
            this.m_labelinputOpe.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 12);
            this.label5.TabIndex = 48;
            this.label5.Text = "オペレータ";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(201, 196);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "登録";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(302, 196);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "キャンセル";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // m_addressno
            // 
            this.m_addressno.Location = new System.Drawing.Point(123, 86);
            this.m_addressno.Name = "m_addressno";
            this.m_addressno.Size = new System.Drawing.Size(59, 19);
            this.m_addressno.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 12);
            this.label3.TabIndex = 42;
            this.label3.Text = "アドレス番号(表示順)";
            // 
            // m_opeID
            // 
            this.m_opeID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_opeID.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.m_opeID.Location = new System.Drawing.Point(123, 35);
            this.m_opeID.Name = "m_opeID";
            this.m_opeID.Size = new System.Drawing.Size(274, 19);
            this.m_opeID.TabIndex = 1;
            this.m_opeID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_opeID_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 12);
            this.label1.TabIndex = 38;
            this.label1.Text = "ユーザーID(通番)";
            // 
            // m_addressname
            // 
            this.m_addressname.Location = new System.Drawing.Point(123, 139);
            this.m_addressname.Name = "m_addressname";
            this.m_addressname.Size = new System.Drawing.Size(274, 19);
            this.m_addressname.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 55;
            this.label4.Text = "アドレス名";
            // 
            // m_Customerkbn_combo
            // 
            this.m_Customerkbn_combo.FormattingEnabled = true;
            this.m_Customerkbn_combo.Items.AddRange(new object[] {
            "オペレータ ",
            "カスタマ担当者"});
            this.m_Customerkbn_combo.Location = new System.Drawing.Point(123, 9);
            this.m_Customerkbn_combo.Name = "m_Customerkbn_combo";
            this.m_Customerkbn_combo.Size = new System.Drawing.Size(121, 20);
            this.m_Customerkbn_combo.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 12);
            this.label7.TabIndex = 58;
            this.label7.Text = "ユーザー区分";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 12);
            this.label2.TabIndex = 40;
            this.label2.Text = "ユーザー名";
            // 
            // m_opename
            // 
            this.m_opename.Location = new System.Drawing.Point(123, 60);
            this.m_opename.Name = "m_opename";
            this.m_opename.Size = new System.Drawing.Size(274, 19);
            this.m_opename.TabIndex = 2;
            // 
            // Form_addressInsert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 227);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.m_Customerkbn_combo);
            this.Controls.Add(this.m_addressname);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_idlabel);
            this.Controls.Add(this.m_address);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.m_labelinputOpe);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.m_addressno);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_opename);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_opeID);
            this.Controls.Add(this.label1);
            this.Name = "Form_addressInsert";
            this.Text = "メールアドレス登録";
            this.Load += new System.EventHandler(this.Form_addressInsert_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_idlabel;
        private System.Windows.Forms.TextBox m_address;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox m_labelinputOpe;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox m_addressno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_opeID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_addressname;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox m_Customerkbn_combo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_opename;
    }
}