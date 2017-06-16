namespace SMSサンプル
{
    partial class Form_alermlist
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
            this.m_alerm_list = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_taiouOpe = new System.Windows.Forms.TextBox();
            this.m_alermtitle = new System.Windows.Forms.TextBox();
            this.m_alerm_message = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.m_syoriDate = new System.Windows.Forms.DateTimePicker();
            this.m_alermno = new System.Windows.Forms.TextBox();
            this.m_customer_name = new System.Windows.Forms.TextBox();
            this.m_system_name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_alerm_list
            // 
            this.m_alerm_list.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_alerm_list.Location = new System.Drawing.Point(12, 160);
            this.m_alerm_list.Name = "m_alerm_list";
            this.m_alerm_list.Size = new System.Drawing.Size(516, 127);
            this.m_alerm_list.TabIndex = 0;
            this.m_alerm_list.UseCompatibleStateImageBehavior = false;
            this.m_alerm_list.View = System.Windows.Forms.View.Details;
            this.m_alerm_list.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_alerm_list_ColumnClick);
            this.m_alerm_list.MouseClick += new System.Windows.Forms.MouseEventHandler(this.m_alerm_list_MouseClick);
            this.m_alerm_list.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.m_alerm_list_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "未処理タイマー";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(379, 359);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 31);
            this.button1.TabIndex = 2;
            this.button1.Text = "登録";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(459, 359);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(69, 31);
            this.button2.TabIndex = 3;
            this.button2.Text = "終了";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 311);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "対応者(ID)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 337);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "対応日時";
            // 
            // m_taiouOpe
            // 
            this.m_taiouOpe.Location = new System.Drawing.Point(80, 308);
            this.m_taiouOpe.Name = "m_taiouOpe";
            this.m_taiouOpe.Size = new System.Drawing.Size(200, 19);
            this.m_taiouOpe.TabIndex = 8;
            this.m_taiouOpe.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // m_alermtitle
            // 
            this.m_alermtitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_alermtitle.Location = new System.Drawing.Point(65, 31);
            this.m_alermtitle.Name = "m_alermtitle";
            this.m_alermtitle.ReadOnly = true;
            this.m_alermtitle.Size = new System.Drawing.Size(463, 19);
            this.m_alermtitle.TabIndex = 11;
            // 
            // m_alerm_message
            // 
            this.m_alerm_message.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_alerm_message.Location = new System.Drawing.Point(12, 102);
            this.m_alerm_message.Multiline = true;
            this.m_alerm_message.Name = "m_alerm_message";
            this.m_alerm_message.ReadOnly = true;
            this.m_alerm_message.Size = new System.Drawing.Size(516, 39);
            this.m_alerm_message.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(14, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "タイマー情報";
            // 
            // m_syoriDate
            // 
            this.m_syoriDate.CustomFormat = "yyyy年 M月 d日(ddd) HH:mm";
            this.m_syoriDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_syoriDate.Location = new System.Drawing.Point(80, 333);
            this.m_syoriDate.Name = "m_syoriDate";
            this.m_syoriDate.Size = new System.Drawing.Size(200, 19);
            this.m_syoriDate.TabIndex = 1;
            // 
            // m_alermno
            // 
            this.m_alermno.Location = new System.Drawing.Point(12, 31);
            this.m_alermno.Name = "m_alermno";
            this.m_alermno.ReadOnly = true;
            this.m_alermno.Size = new System.Drawing.Size(47, 19);
            this.m_alermno.TabIndex = 15;
            // 
            // m_customer_name
            // 
            this.m_customer_name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_customer_name.Location = new System.Drawing.Point(64, 56);
            this.m_customer_name.Name = "m_customer_name";
            this.m_customer_name.ReadOnly = true;
            this.m_customer_name.Size = new System.Drawing.Size(463, 19);
            this.m_customer_name.TabIndex = 16;
            // 
            // m_system_name
            // 
            this.m_system_name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_system_name.Location = new System.Drawing.Point(65, 79);
            this.m_system_name.Name = "m_system_name";
            this.m_system_name.ReadOnly = true;
            this.m_system_name.Size = new System.Drawing.Size(463, 19);
            this.m_system_name.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "カスタマ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 12);
            this.label6.TabIndex = 19;
            this.label6.Text = "システム";
            // 
            // Form_alermlist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 397);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_system_name);
            this.Controls.Add(this.m_customer_name);
            this.Controls.Add(this.m_alermno);
            this.Controls.Add(this.m_syoriDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.m_alerm_message);
            this.Controls.Add(this.m_alermtitle);
            this.Controls.Add(this.m_taiouOpe);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_alerm_list);
            this.Name = "Form_alermlist";
            this.Text = "タイマー";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_alermlist_FormClosed);
            this.Load += new System.EventHandler(this.Form_alermlist_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView m_alerm_list;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox m_taiouOpe;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox m_alerm_message;
        private System.Windows.Forms.TextBox m_alermtitle;
        private System.Windows.Forms.DateTimePicker m_syoriDate;
        private System.Windows.Forms.TextBox m_alermno;
        private System.Windows.Forms.TextBox m_system_name;
        private System.Windows.Forms.TextBox m_customer_name;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
    }
}