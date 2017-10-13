namespace moss_AP
{
    partial class Form_IncidentMailSend
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
            this.components = new System.ComponentModel.Container();
            this.m_userno = new System.Windows.Forms.TextBox();
            this.m_usernameCombo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.m_selectBtn = new System.Windows.Forms.Button();
            this.m_site_List = new System.Windows.Forms.ListView();
            this.m_host_list = new System.Windows.Forms.ListView();
            this.m_interface_List = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_system_List = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.kousei_menu = new System.Windows.Forms.ToolStripMenuItem();
            this.customer_name = new System.Windows.Forms.ToolStripTextBox();
            this.system_name = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripTextBox3 = new System.Windows.Forms.ToolStripTextBox();
            this.site_name = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.host_name = new System.Windows.Forms.ToolStripTextBox();
            this.location_name = new System.Windows.Forms.ToolStripTextBox();
            this.youto = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.kansi_type = new System.Windows.Forms.ToolStripTextBox();
            this.koumoku_name = new System.Windows.Forms.ToolStripTextBox();
            this.ipaddress = new System.Windows.Forms.ToolStripTextBox();
            this.NAT_Ipaddress = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.m_templeteCombo = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_templetename = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.m_body = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_userno
            // 
            this.m_userno.Location = new System.Drawing.Point(67, 13);
            this.m_userno.Name = "m_userno";
            this.m_userno.ReadOnly = true;
            this.m_userno.Size = new System.Drawing.Size(44, 19);
            this.m_userno.TabIndex = 113;
            this.m_userno.TabStop = false;
            // 
            // m_usernameCombo
            // 
            this.m_usernameCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_usernameCombo.FormattingEnabled = true;
            this.m_usernameCombo.Location = new System.Drawing.Point(117, 12);
            this.m_usernameCombo.Name = "m_usernameCombo";
            this.m_usernameCombo.Size = new System.Drawing.Size(325, 20);
            this.m_usernameCombo.TabIndex = 114;
            this.m_usernameCombo.SelectionChangeCommitted += new System.EventHandler(this.m_usernameCombo_SelectionChangeCommitted);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 12);
            this.label7.TabIndex = 115;
            this.label7.Text = "カスタマ名";
            // 
            // m_selectBtn
            // 
            this.m_selectBtn.Location = new System.Drawing.Point(448, 11);
            this.m_selectBtn.Name = "m_selectBtn";
            this.m_selectBtn.Size = new System.Drawing.Size(75, 23);
            this.m_selectBtn.TabIndex = 116;
            this.m_selectBtn.Text = "検索";
            this.m_selectBtn.UseVisualStyleBackColor = true;
            this.m_selectBtn.Click += new System.EventHandler(this.m_selectBtn_Click);
            // 
            // m_site_List
            // 
            this.m_site_List.GridLines = true;
            this.m_site_List.Location = new System.Drawing.Point(11, 182);
            this.m_site_List.Name = "m_site_List";
            this.m_site_List.Size = new System.Drawing.Size(512, 106);
            this.m_site_List.TabIndex = 191;
            this.m_site_List.UseCompatibleStateImageBehavior = false;
            this.m_site_List.View = System.Windows.Forms.View.Details;
            this.m_site_List.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_site_List_ColumnClick);
            // 
            // m_host_list
            // 
            this.m_host_list.GridLines = true;
            this.m_host_list.Location = new System.Drawing.Point(11, 313);
            this.m_host_list.Name = "m_host_list";
            this.m_host_list.Size = new System.Drawing.Size(512, 123);
            this.m_host_list.TabIndex = 192;
            this.m_host_list.UseCompatibleStateImageBehavior = false;
            this.m_host_list.View = System.Windows.Forms.View.Details;
            this.m_host_list.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_host_list_ColumnClick);
            // 
            // m_interface_List
            // 
            this.m_interface_List.GridLines = true;
            this.m_interface_List.Location = new System.Drawing.Point(11, 460);
            this.m_interface_List.Name = "m_interface_List";
            this.m_interface_List.Size = new System.Drawing.Size(512, 115);
            this.m_interface_List.TabIndex = 196;
            this.m_interface_List.UseCompatibleStateImageBehavior = false;
            this.m_interface_List.View = System.Windows.Forms.View.Details;
            this.m_interface_List.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_interface_List_ColumnClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 12);
            this.label1.TabIndex = 197;
            this.label1.Text = "システム";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 198;
            this.label2.Text = "拠点";
            // 
            // m_system_List
            // 
            this.m_system_List.GridLines = true;
            this.m_system_List.Location = new System.Drawing.Point(11, 70);
            this.m_system_List.Name = "m_system_List";
            this.m_system_List.Size = new System.Drawing.Size(512, 91);
            this.m_system_List.TabIndex = 199;
            this.m_system_List.UseCompatibleStateImageBehavior = false;
            this.m_system_List.View = System.Windows.Forms.View.Details;
            this.m_system_List.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_system_List_ColumnClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kousei_menu,
            this.toolStripComboBox1});
            this.contextMenuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.ShowCheckMargin = true;
            this.contextMenuStrip1.Size = new System.Drawing.Size(204, 78);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // kousei_menu
            // 
            this.kousei_menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customer_name,
            this.system_name,
            this.toolStripTextBox3,
            this.site_name,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.kousei_menu.Name = "kousei_menu";
            this.kousei_menu.Size = new System.Drawing.Size(203, 22);
            this.kousei_menu.Text = "構成情報";
            // 
            // customer_name
            // 
            this.customer_name.Name = "customer_name";
            this.customer_name.Size = new System.Drawing.Size(100, 25);
            // 
            // system_name
            // 
            this.system_name.Name = "system_name";
            this.system_name.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripTextBox3
            // 
            this.toolStripTextBox3.Name = "toolStripTextBox3";
            this.toolStripTextBox3.Size = new System.Drawing.Size(100, 25);
            // 
            // site_name
            // 
            this.site_name.Name = "site_name";
            this.site_name.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.host_name,
            this.location_name,
            this.youto});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(196, 22);
            this.toolStripMenuItem1.Text = "ホスト情報";
            // 
            // host_name
            // 
            this.host_name.Name = "host_name";
            this.host_name.Size = new System.Drawing.Size(100, 25);
            // 
            // location_name
            // 
            this.location_name.Name = "location_name";
            this.location_name.Size = new System.Drawing.Size(100, 25);
            // 
            // youto
            // 
            this.youto.Name = "youto";
            this.youto.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kansi_type,
            this.koumoku_name,
            this.ipaddress,
            this.NAT_Ipaddress});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(196, 22);
            this.toolStripMenuItem2.Text = "監視インターフェイス";
            // 
            // kansi_type
            // 
            this.kansi_type.Name = "kansi_type";
            this.kansi_type.Size = new System.Drawing.Size(100, 25);
            // 
            // koumoku_name
            // 
            this.koumoku_name.Name = "koumoku_name";
            this.koumoku_name.Size = new System.Drawing.Size(100, 25);
            // 
            // ipaddress
            // 
            this.ipaddress.Name = "ipaddress";
            this.ipaddress.Size = new System.Drawing.Size(100, 25);
            // 
            // NAT_Ipaddress
            // 
            this.NAT_Ipaddress.Name = "NAT_Ipaddress";
            this.NAT_Ipaddress.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 26);
            // 
            // m_templeteCombo
            // 
            this.m_templeteCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_templeteCombo.Enabled = false;
            this.m_templeteCombo.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_templeteCombo.FormattingEnabled = true;
            this.m_templeteCombo.Location = new System.Drawing.Point(602, 10);
            this.m_templeteCombo.Name = "m_templeteCombo";
            this.m_templeteCombo.Size = new System.Drawing.Size(546, 21);
            this.m_templeteCombo.TabIndex = 240;
            this.m_templeteCombo.SelectionChangeCommitted += new System.EventHandler(this.m_templeteCombo_SelectionChangeCommitted);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(538, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 241;
            this.label9.Text = "テンプレート";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(538, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 12);
            this.label3.TabIndex = 243;
            this.label3.Text = "タイトル";
            // 
            // m_templetename
            // 
            this.m_templetename.Location = new System.Drawing.Point(602, 38);
            this.m_templetename.Name = "m_templetename";
            this.m_templetename.Size = new System.Drawing.Size(546, 19);
            this.m_templetename.TabIndex = 244;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 298);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 12);
            this.label4.TabIndex = 245;
            this.label4.Text = "ホスト";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 445);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 12);
            this.label5.TabIndex = 246;
            this.label5.Text = "監視インターフェイス";
            // 
            // m_body
            // 
            this.m_body.ContextMenuStrip = this.contextMenuStrip1;
            this.m_body.Location = new System.Drawing.Point(540, 70);
            this.m_body.Name = "m_body";
            this.m_body.Size = new System.Drawing.Size(608, 505);
            this.m_body.TabIndex = 247;
            this.m_body.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(983, 581);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 31);
            this.button1.TabIndex = 248;
            this.button1.Text = "送信";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1073, 581);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 31);
            this.button2.TabIndex = 249;
            this.button2.Text = "キャンセル";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form_IncidentMailSend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 623);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.m_body);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_templetename);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_templeteCombo);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.m_system_List);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_interface_List);
            this.Controls.Add(this.m_host_list);
            this.Controls.Add(this.m_site_List);
            this.Controls.Add(this.m_selectBtn);
            this.Controls.Add(this.m_userno);
            this.Controls.Add(this.m_usernameCombo);
            this.Controls.Add(this.label7);
            this.Name = "Form_IncidentMailSend";
            this.Text = "インシデントメール";
            this.Load += new System.EventHandler(this.Form_IncidentMailSend_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_userno;
        private System.Windows.Forms.ComboBox m_usernameCombo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button m_selectBtn;
        private System.Windows.Forms.ListView m_site_List;
        private System.Windows.Forms.ListView m_host_list;
        private System.Windows.Forms.ListView m_interface_List;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView m_system_List;
        private System.Windows.Forms.ComboBox m_templeteCombo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_templetename;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem kousei_menu;
        private System.Windows.Forms.ToolStripTextBox customer_name;
        private System.Windows.Forms.ToolStripTextBox system_name;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox3;
        private System.Windows.Forms.ToolStripTextBox site_name;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripTextBox host_name;
        private System.Windows.Forms.ToolStripTextBox location_name;
        private System.Windows.Forms.ToolStripTextBox youto;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripTextBox kansi_type;
        private System.Windows.Forms.ToolStripTextBox koumoku_name;
        private System.Windows.Forms.ToolStripTextBox ipaddress;
        private System.Windows.Forms.ToolStripTextBox NAT_Ipaddress;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.RichTextBox m_body;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}