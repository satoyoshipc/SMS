namespace moss_AP
{
    partial class Form_taiou_list
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
            this.m_alertdatetime_Before = new System.Windows.Forms.DateTimePicker();
            this.m_alertdatetime_After = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.m_taioulist = new System.Windows.Forms.ListView();
            this.m_kensaku = new System.Windows.Forms.Button();
            this.m_taiou = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_taiouchk = new System.Windows.Forms.CheckBox();
            this.m_deleteBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_alertdatetime_Before
            // 
            this.m_alertdatetime_Before.CustomFormat = "yyyy年M月d日(dddd) HH:mm";
            this.m_alertdatetime_Before.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_alertdatetime_Before.Location = new System.Drawing.Point(77, 9);
            this.m_alertdatetime_Before.Name = "m_alertdatetime_Before";
            this.m_alertdatetime_Before.Size = new System.Drawing.Size(202, 19);
            this.m_alertdatetime_Before.TabIndex = 0;
            // 
            // m_alertdatetime_After
            // 
            this.m_alertdatetime_After.CustomFormat = "yyyy年M月d日(dddd) HH:mm";
            this.m_alertdatetime_After.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_alertdatetime_After.Location = new System.Drawing.Point(305, 9);
            this.m_alertdatetime_After.Name = "m_alertdatetime_After";
            this.m_alertdatetime_After.Size = new System.Drawing.Size(202, 19);
            this.m_alertdatetime_After.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(285, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "～";
            // 
            // m_taioulist
            // 
            this.m_taioulist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_taioulist.FullRowSelect = true;
            this.m_taioulist.GridLines = true;
            this.m_taioulist.Location = new System.Drawing.Point(2, 83);
            this.m_taioulist.Name = "m_taioulist";
            this.m_taioulist.Size = new System.Drawing.Size(607, 287);
            this.m_taioulist.TabIndex = 6;
            this.m_taioulist.UseCompatibleStateImageBehavior = false;
            this.m_taioulist.View = System.Windows.Forms.View.Details;
            this.m_taioulist.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_taioulist_ColumnClick);
            // 
            // m_kensaku
            // 
            this.m_kensaku.Location = new System.Drawing.Point(446, 36);
            this.m_kensaku.Name = "m_kensaku";
            this.m_kensaku.Size = new System.Drawing.Size(61, 41);
            this.m_kensaku.TabIndex = 4;
            this.m_kensaku.Text = "検索";
            this.m_kensaku.UseVisualStyleBackColor = true;
            this.m_kensaku.Click += new System.EventHandler(this.m_kensaku_Click);
            // 
            // m_taiou
            // 
            this.m_taiou.Location = new System.Drawing.Point(77, 58);
            this.m_taiou.Name = "m_taiou";
            this.m_taiou.Size = new System.Drawing.Size(140, 19);
            this.m_taiou.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "対応者ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "アラーム日時";
            // 
            // m_taiouchk
            // 
            this.m_taiouchk.AutoSize = true;
            this.m_taiouchk.Location = new System.Drawing.Point(77, 36);
            this.m_taiouchk.Name = "m_taiouchk";
            this.m_taiouchk.Size = new System.Drawing.Size(71, 16);
            this.m_taiouchk.TabIndex = 2;
            this.m_taiouchk.Text = "対応済み";
            this.m_taiouchk.UseVisualStyleBackColor = true;
            // 
            // m_deleteBtn
            // 
            this.m_deleteBtn.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_deleteBtn.ForeColor = System.Drawing.Color.Red;
            this.m_deleteBtn.Location = new System.Drawing.Point(539, 36);
            this.m_deleteBtn.Name = "m_deleteBtn";
            this.m_deleteBtn.Size = new System.Drawing.Size(61, 41);
            this.m_deleteBtn.TabIndex = 5;
            this.m_deleteBtn.Text = "削除";
            this.m_deleteBtn.UseVisualStyleBackColor = true;
            this.m_deleteBtn.Click += new System.EventHandler(this.m_deleteBtn_Click);
            // 
            // Form_taiou_list
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 374);
            this.Controls.Add(this.m_deleteBtn);
            this.Controls.Add(this.m_taiouchk);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_taiou);
            this.Controls.Add(this.m_kensaku);
            this.Controls.Add(this.m_taioulist);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_alertdatetime_After);
            this.Controls.Add(this.m_alertdatetime_Before);
            this.Name = "Form_taiou_list";
            this.Text = "対応リスト";
            this.Load += new System.EventHandler(this.Form_taiou_list_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker m_alertdatetime_Before;
        private System.Windows.Forms.DateTimePicker m_alertdatetime_After;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView m_taioulist;
        private System.Windows.Forms.Button m_kensaku;
        private System.Windows.Forms.TextBox m_taiou;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox m_taiouchk;
        private System.Windows.Forms.Button m_deleteBtn;
    }
}