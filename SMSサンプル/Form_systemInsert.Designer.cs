namespace SMSサンプル
{
    partial class Form_systemInsert
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
            this.m_labelinputOpe = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.m_biko = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_systemnamekana = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_systemname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.m_usernameCombo = new System.Windows.Forms.ComboBox();
            this.m_userID = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // m_idlabel
            // 
            this.m_idlabel.Location = new System.Drawing.Point(113, 187);
            this.m_idlabel.Name = "m_idlabel";
            this.m_idlabel.ReadOnly = true;
            this.m_idlabel.Size = new System.Drawing.Size(44, 19);
            this.m_idlabel.TabIndex = 53;
            this.m_idlabel.TabStop = false;
            // 
            // m_labelinputOpe
            // 
            this.m_labelinputOpe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_labelinputOpe.Location = new System.Drawing.Point(163, 187);
            this.m_labelinputOpe.Name = "m_labelinputOpe";
            this.m_labelinputOpe.ReadOnly = true;
            this.m_labelinputOpe.Size = new System.Drawing.Size(289, 19);
            this.m_labelinputOpe.TabIndex = 49;
            this.m_labelinputOpe.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 12);
            this.label5.TabIndex = 48;
            this.label5.Text = "オペレータ";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(256, 212);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "登録";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(357, 212);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "キャンセル";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "CSV登録";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // m_biko
            // 
            this.m_biko.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_biko.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_biko.Location = new System.Drawing.Point(113, 124);
            this.m_biko.Multiline = true;
            this.m_biko.Name = "m_biko";
            this.m_biko.Size = new System.Drawing.Size(339, 59);
            this.m_biko.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 42;
            this.label4.Text = "備考";
            // 
            // m_systemnamekana
            // 
            this.m_systemnamekana.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_systemnamekana.ImeMode = System.Windows.Forms.ImeMode.Katakana;
            this.m_systemnamekana.Location = new System.Drawing.Point(114, 99);
            this.m_systemnamekana.Name = "m_systemnamekana";
            this.m_systemnamekana.Size = new System.Drawing.Size(339, 19);
            this.m_systemnamekana.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 12);
            this.label2.TabIndex = 39;
            this.label2.Text = "システム名カナ";
            // 
            // m_systemname
            // 
            this.m_systemname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_systemname.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_systemname.Location = new System.Drawing.Point(114, 74);
            this.m_systemname.Name = "m_systemname";
            this.m_systemname.Size = new System.Drawing.Size(337, 19);
            this.m_systemname.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 12);
            this.label1.TabIndex = 37;
            this.label1.Text = "システム名";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 54;
            this.label6.Text = "ユーザ名";
            // 
            // m_usernameCombo
            // 
            this.m_usernameCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_usernameCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.m_usernameCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.m_usernameCombo.FormattingEnabled = true;
            this.m_usernameCombo.Location = new System.Drawing.Point(163, 49);
            this.m_usernameCombo.Name = "m_usernameCombo";
            this.m_usernameCombo.Size = new System.Drawing.Size(288, 20);
            this.m_usernameCombo.TabIndex = 0;
            this.m_usernameCombo.SelectedValueChanged += new System.EventHandler(this.m_usernameCombo_SelectedValueChanged);
            // 
            // m_userID
            // 
            this.m_userID.Location = new System.Drawing.Point(114, 50);
            this.m_userID.Name = "m_userID";
            this.m_userID.ReadOnly = true;
            this.m_userID.Size = new System.Drawing.Size(44, 19);
            this.m_userID.TabIndex = 56;
            this.m_userID.TabStop = false;
            // 
            // Form_systemInsert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 245);
            this.Controls.Add(this.m_userID);
            this.Controls.Add(this.m_usernameCombo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.m_idlabel);
            this.Controls.Add(this.m_labelinputOpe);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.m_biko);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_systemnamekana);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_systemname);
            this.Controls.Add(this.label1);
            this.Name = "Form_systemInsert";
            this.Text = "システム情報登録";
            this.Load += new System.EventHandler(this.Form_systemInsert_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_idlabel;
        private System.Windows.Forms.TextBox m_labelinputOpe;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox m_biko;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox m_systemnamekana;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_systemname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox m_usernameCombo;
        private System.Windows.Forms.TextBox m_userID;
    }
}