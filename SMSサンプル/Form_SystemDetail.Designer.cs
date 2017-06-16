namespace SMSサンプル
{
    partial class Form_SystemDetail
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.m_selectBtn = new System.Windows.Forms.Button();
            this.m_selecttext = new System.Windows.Forms.TextBox();
            this.m_selectKoumoku = new System.Windows.Forms.ComboBox();
            this.m_System_List = new System.Windows.Forms.ListView();
            this.m_userno = new System.Windows.Forms.TextBox();
            this.m_cutomername = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_updateOpe = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_update = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.m_biko = new System.Windows.Forms.TextBox();
            this.m_systemname_kana = new System.Windows.Forms.TextBox();
            this.m_systemname = new System.Windows.Forms.TextBox();
            this.m_systemno = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_deleteBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_deleteBtn);
            this.splitContainer1.Panel2.Controls.Add(this.m_userno);
            this.splitContainer1.Panel2.Controls.Add(this.m_cutomername);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.m_updateOpe);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.m_update);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.Controls.Add(this.m_biko);
            this.splitContainer1.Panel2.Controls.Add(this.m_systemname_kana);
            this.splitContainer1.Panel2.Controls.Add(this.m_systemname);
            this.splitContainer1.Panel2.Controls.Add(this.m_systemno);
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(846, 415);
            this.splitContainer1.SplitterDistance = 186;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.m_selectBtn);
            this.splitContainer2.Panel1.Controls.Add(this.m_selecttext);
            this.splitContainer2.Panel1.Controls.Add(this.m_selectKoumoku);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.m_System_List);
            this.splitContainer2.Size = new System.Drawing.Size(844, 184);
            this.splitContainer2.SplitterDistance = 33;
            this.splitContainer2.TabIndex = 1;
            this.splitContainer2.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer2_SplitterMoved);
            // 
            // m_selectBtn
            // 
            this.m_selectBtn.Location = new System.Drawing.Point(419, 4);
            this.m_selectBtn.Name = "m_selectBtn";
            this.m_selectBtn.Size = new System.Drawing.Size(75, 23);
            this.m_selectBtn.TabIndex = 2;
            this.m_selectBtn.Text = "検索";
            this.m_selectBtn.UseVisualStyleBackColor = true;
            this.m_selectBtn.Click += new System.EventHandler(this.m_selectBtn_Click);
            // 
            // m_selecttext
            // 
            this.m_selecttext.Location = new System.Drawing.Point(159, 6);
            this.m_selecttext.Name = "m_selecttext";
            this.m_selecttext.Size = new System.Drawing.Size(244, 19);
            this.m_selecttext.TabIndex = 1;
            // 
            // m_selectKoumoku
            // 
            this.m_selectKoumoku.FormattingEnabled = true;
            this.m_selectKoumoku.Location = new System.Drawing.Point(13, 6);
            this.m_selectKoumoku.Name = "m_selectKoumoku";
            this.m_selectKoumoku.Size = new System.Drawing.Size(140, 20);
            this.m_selectKoumoku.TabIndex = 0;
            // 
            // m_System_List
            // 
            this.m_System_List.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_System_List.FullRowSelect = true;
            this.m_System_List.GridLines = true;
            this.m_System_List.Location = new System.Drawing.Point(0, 0);
            this.m_System_List.Name = "m_System_List";
            this.m_System_List.Size = new System.Drawing.Size(844, 147);
            this.m_System_List.TabIndex = 1;
            this.m_System_List.UseCompatibleStateImageBehavior = false;
            this.m_System_List.View = System.Windows.Forms.View.Details;
            this.m_System_List.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_System_List_ColumnClick);
            this.m_System_List.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.m_Ststem_List_MouseDoubleClick);
            // 
            // m_userno
            // 
            this.m_userno.Location = new System.Drawing.Point(121, 39);
            this.m_userno.Name = "m_userno";
            this.m_userno.ReadOnly = true;
            this.m_userno.Size = new System.Drawing.Size(45, 19);
            this.m_userno.TabIndex = 105;
            // 
            // m_cutomername
            // 
            this.m_cutomername.Location = new System.Drawing.Point(172, 39);
            this.m_cutomername.Name = "m_cutomername";
            this.m_cutomername.ReadOnly = true;
            this.m_cutomername.Size = new System.Drawing.Size(385, 19);
            this.m_cutomername.TabIndex = 104;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 12);
            this.label4.TabIndex = 103;
            this.label4.Text = "カスタマ名";
            // 
            // m_updateOpe
            // 
            this.m_updateOpe.Location = new System.Drawing.Point(641, 123);
            this.m_updateOpe.Name = "m_updateOpe";
            this.m_updateOpe.ReadOnly = true;
            this.m_updateOpe.Size = new System.Drawing.Size(166, 19);
            this.m_updateOpe.TabIndex = 102;
            this.m_updateOpe.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(582, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 101;
            this.label6.Text = "更新者";
            // 
            // m_update
            // 
            this.m_update.Location = new System.Drawing.Point(641, 95);
            this.m_update.Name = "m_update";
            this.m_update.ReadOnly = true;
            this.m_update.Size = new System.Drawing.Size(166, 19);
            this.m_update.TabIndex = 100;
            this.m_update.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(582, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 99;
            this.label5.Text = "更新日時";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(748, 169);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 33);
            this.button2.TabIndex = 98;
            this.button2.Text = "戻る";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(666, 169);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 33);
            this.button1.TabIndex = 97;
            this.button1.Text = "更新";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // m_biko
            // 
            this.m_biko.Location = new System.Drawing.Point(121, 117);
            this.m_biko.Multiline = true;
            this.m_biko.Name = "m_biko";
            this.m_biko.Size = new System.Drawing.Size(436, 39);
            this.m_biko.TabIndex = 96;
            // 
            // m_systemname_kana
            // 
            this.m_systemname_kana.Location = new System.Drawing.Point(121, 92);
            this.m_systemname_kana.Name = "m_systemname_kana";
            this.m_systemname_kana.Size = new System.Drawing.Size(436, 19);
            this.m_systemname_kana.TabIndex = 95;
            // 
            // m_systemname
            // 
            this.m_systemname.Location = new System.Drawing.Point(121, 67);
            this.m_systemname.Name = "m_systemname";
            this.m_systemname.Size = new System.Drawing.Size(436, 19);
            this.m_systemname.TabIndex = 94;
            // 
            // m_systemno
            // 
            this.m_systemno.Location = new System.Drawing.Point(121, 11);
            this.m_systemno.Name = "m_systemno";
            this.m_systemno.ReadOnly = true;
            this.m_systemno.Size = new System.Drawing.Size(122, 19);
            this.m_systemno.TabIndex = 93;
            this.m_systemno.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 92;
            this.label7.Text = "備考";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 12);
            this.label3.TabIndex = 91;
            this.label3.Text = "システム名カナ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 12);
            this.label2.TabIndex = 90;
            this.label2.Text = "システム名";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 12);
            this.label1.TabIndex = 89;
            this.label1.Text = "システム通番";
            // 
            // m_deleteBtn
            // 
            this.m_deleteBtn.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_deleteBtn.ForeColor = System.Drawing.Color.Red;
            this.m_deleteBtn.Location = new System.Drawing.Point(748, 11);
            this.m_deleteBtn.Name = "m_deleteBtn";
            this.m_deleteBtn.Size = new System.Drawing.Size(76, 27);
            this.m_deleteBtn.TabIndex = 182;
            this.m_deleteBtn.Text = "削除";
            this.m_deleteBtn.UseVisualStyleBackColor = true;
            this.m_deleteBtn.Click += new System.EventHandler(this.m_deleteBtn_Click);
            // 
            // Form_SystemDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 415);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form_SystemDetail";
            this.Text = "システム情報";
            this.Load += new System.EventHandler(this.Form_SystemDetail_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListView m_System_List;
        private System.Windows.Forms.Button m_selectBtn;
        private System.Windows.Forms.TextBox m_selecttext;
        private System.Windows.Forms.ComboBox m_selectKoumoku;
        private System.Windows.Forms.TextBox m_updateOpe;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox m_update;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox m_biko;
        private System.Windows.Forms.TextBox m_systemname_kana;
        private System.Windows.Forms.TextBox m_systemname;
        private System.Windows.Forms.TextBox m_systemno;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_cutomername;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox m_userno;
        private System.Windows.Forms.Button m_deleteBtn;
    }
}