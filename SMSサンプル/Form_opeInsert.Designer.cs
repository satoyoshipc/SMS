namespace moss_AP
{
    partial class Form_opeInsert
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
            this.m_labelinputOpe = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.m_fastname = new System.Windows.Forms.TextBox();
            this.m_biko = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_lastname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_opeID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_pass = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_riyouRadio = new System.Windows.Forms.RadioButton();
            this.m_kanriRadio = new System.Windows.Forms.RadioButton();
            this.m_idlabel = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.m_duplicationResult = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_labelinputOpe
            // 
            this.m_labelinputOpe.Location = new System.Drawing.Point(165, 252);
            this.m_labelinputOpe.Name = "m_labelinputOpe";
            this.m_labelinputOpe.ReadOnly = true;
            this.m_labelinputOpe.Size = new System.Drawing.Size(224, 19);
            this.m_labelinputOpe.TabIndex = 8;
            this.m_labelinputOpe.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 255);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 12);
            this.label5.TabIndex = 28;
            this.label5.Text = "オペレータ";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(310, 275);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "登録";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(411, 275);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "キャンセル";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // m_fastname
            // 
            this.m_fastname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_fastname.Location = new System.Drawing.Point(115, 88);
            this.m_fastname.Name = "m_fastname";
            this.m_fastname.Size = new System.Drawing.Size(391, 19);
            this.m_fastname.TabIndex = 3;
            // 
            // m_biko
            // 
            this.m_biko.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_biko.Location = new System.Drawing.Point(115, 189);
            this.m_biko.Multiline = true;
            this.m_biko.Name = "m_biko";
            this.m_biko.Size = new System.Drawing.Size(391, 59);
            this.m_biko.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 199);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "備考";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "オペレータ名(名)";
            // 
            // m_lastname
            // 
            this.m_lastname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lastname.Location = new System.Drawing.Point(115, 63);
            this.m_lastname.Name = "m_lastname";
            this.m_lastname.Size = new System.Drawing.Size(391, 19);
            this.m_lastname.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "オペレータ名(姓)";
            // 
            // m_opeID
            // 
            this.m_opeID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_opeID.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.m_opeID.Location = new System.Drawing.Point(115, 11);
            this.m_opeID.Name = "m_opeID";
            this.m_opeID.Size = new System.Drawing.Size(339, 19);
            this.m_opeID.TabIndex = 0;
            this.m_opeID.TextChanged += new System.EventHandler(this.m_opeID_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "オペレータアカウント";
            // 
            // m_pass
            // 
            this.m_pass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pass.Location = new System.Drawing.Point(115, 113);
            this.m_pass.Name = "m_pass";
            this.m_pass.PasswordChar = '*';
            this.m_pass.Size = new System.Drawing.Size(391, 19);
            this.m_pass.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 12);
            this.label6.TabIndex = 30;
            this.label6.Text = "パスワード";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_riyouRadio);
            this.groupBox1.Controls.Add(this.m_kanriRadio);
            this.groupBox1.Location = new System.Drawing.Point(115, 135);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 50);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "権限";
            // 
            // m_riyouRadio
            // 
            this.m_riyouRadio.AutoSize = true;
            this.m_riyouRadio.Location = new System.Drawing.Point(147, 22);
            this.m_riyouRadio.Name = "m_riyouRadio";
            this.m_riyouRadio.Size = new System.Drawing.Size(59, 16);
            this.m_riyouRadio.TabIndex = 1;
            this.m_riyouRadio.Text = "利用者";
            this.m_riyouRadio.UseVisualStyleBackColor = true;
            // 
            // m_kanriRadio
            // 
            this.m_kanriRadio.AutoSize = true;
            this.m_kanriRadio.Checked = true;
            this.m_kanriRadio.Location = new System.Drawing.Point(52, 22);
            this.m_kanriRadio.Name = "m_kanriRadio";
            this.m_kanriRadio.Size = new System.Drawing.Size(59, 16);
            this.m_kanriRadio.TabIndex = 0;
            this.m_kanriRadio.TabStop = true;
            this.m_kanriRadio.Text = "管理者";
            this.m_kanriRadio.UseVisualStyleBackColor = true;
            // 
            // m_idlabel
            // 
            this.m_idlabel.Location = new System.Drawing.Point(115, 252);
            this.m_idlabel.Name = "m_idlabel";
            this.m_idlabel.ReadOnly = true;
            this.m_idlabel.Size = new System.Drawing.Size(44, 19);
            this.m_idlabel.TabIndex = 7;
            this.m_idlabel.TabStop = false;
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button5.Location = new System.Drawing.Point(460, 7);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(49, 23);
            this.button5.TabIndex = 1;
            this.button5.Text = "チェック";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // m_duplicationResult
            // 
            this.m_duplicationResult.AutoSize = true;
            this.m_duplicationResult.Location = new System.Drawing.Point(119, 39);
            this.m_duplicationResult.Name = "m_duplicationResult";
            this.m_duplicationResult.Size = new System.Drawing.Size(0, 12);
            this.m_duplicationResult.TabIndex = 32;
            // 
            // Form_opeInsert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 306);
            this.Controls.Add(this.m_duplicationResult);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.m_idlabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m_pass);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.m_labelinputOpe);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.m_fastname);
            this.Controls.Add(this.m_biko);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_lastname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_opeID);
            this.Controls.Add(this.label1);
            this.Name = "Form_opeInsert";
            this.Text = "オペレータ登録";
            this.Load += new System.EventHandler(this.Form_opeInsert_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_labelinputOpe;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox m_fastname;
        private System.Windows.Forms.TextBox m_biko;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_lastname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_opeID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_pass;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton m_riyouRadio;
        private System.Windows.Forms.RadioButton m_kanriRadio;
        private System.Windows.Forms.TextBox m_idlabel;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label m_duplicationResult;
    }
}