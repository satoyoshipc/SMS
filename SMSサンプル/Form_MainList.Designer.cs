namespace SMSサンプル
{
    partial class Form_MainList
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_MainList));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.m_refresh_btn = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.linkLabel8 = new System.Windows.Forms.LinkLabel();
            this.label14 = new System.Windows.Forms.Label();
            this.m_teiki_List = new System.Windows.Forms.ListView();
            this.linkLabel7 = new System.Windows.Forms.LinkLabel();
            this.label13 = new System.Windows.Forms.Label();
            this.m_incident_List = new System.Windows.Forms.ListView();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label12 = new System.Windows.Forms.Label();
            this.m_keikaku_list = new System.Windows.Forms.ListView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.m_incidentTouroku = new System.Windows.Forms.Button();
            this.m_tourokuBtn = new System.Windows.Forms.Button();
            this.m_clear_btn = new System.Windows.Forms.Button();
            this.m_selectBtn = new System.Windows.Forms.Button();
            this.m_ipaddress = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_hostCombo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_siteCombo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_systemCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_usernameCombo = new System.Windows.Forms.ComboBox();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.m_system_label = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.systemList = new System.Windows.Forms.ListView();
            this.label7 = new System.Windows.Forms.Label();
            this.userList = new System.Windows.Forms.ListView();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.label8 = new System.Windows.Forms.Label();
            this.siteList = new System.Windows.Forms.ListView();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.label9 = new System.Windows.Forms.Label();
            this.hostList = new System.Windows.Forms.ListView();
            this.linkLabel6 = new System.Windows.Forms.LinkLabel();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.label11 = new System.Windows.Forms.Label();
            this.kaisenList = new System.Windows.Forms.ListView();
            this.label10 = new System.Windows.Forms.Label();
            this.interfaceList = new System.Windows.Forms.ListView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.m_opename = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Controls.Add(this.splitter1);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(1158, 687);
            this.splitContainer1.SplitterDistance = 168;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.m_opename);
            this.splitContainer2.Panel1.Controls.Add(this.m_refresh_btn);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.treeView1);
            this.splitContainer2.Size = new System.Drawing.Size(164, 683);
            this.splitContainer2.SplitterDistance = 48;
            this.splitContainer2.TabIndex = 1;
            // 
            // m_refresh_btn
            // 
            this.m_refresh_btn.Location = new System.Drawing.Point(7, 25);
            this.m_refresh_btn.Name = "m_refresh_btn";
            this.m_refresh_btn.Size = new System.Drawing.Size(75, 20);
            this.m_refresh_btn.TabIndex = 0;
            this.m_refresh_btn.Text = "更新";
            this.m_refresh_btn.UseVisualStyleBackColor = true;
            this.m_refresh_btn.Click += new System.EventHandler(this.m_refresh_btn_Click);
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(164, 631);
            this.treeView1.TabIndex = 1;
            this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(982, 680);
            this.tabControl1.TabIndex = 186;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.linkLabel8);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.m_teiki_List);
            this.tabPage1.Controls.Add(this.linkLabel7);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.m_incident_List);
            this.tabPage1.Controls.Add(this.linkLabel1);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.m_keikaku_list);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(974, 654);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "タイマー情報";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // linkLabel8
            // 
            this.linkLabel8.AutoSize = true;
            this.linkLabel8.Location = new System.Drawing.Point(101, 193);
            this.linkLabel8.Name = "linkLabel8";
            this.linkLabel8.Size = new System.Drawing.Size(29, 12);
            this.linkLabel8.TabIndex = 195;
            this.linkLabel8.TabStop = true;
            this.linkLabel8.Text = "登録";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 193);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 194;
            this.label14.Text = "定期作業";
            // 
            // m_teiki_List
            // 
            this.m_teiki_List.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_teiki_List.GridLines = true;
            this.m_teiki_List.Location = new System.Drawing.Point(8, 209);
            this.m_teiki_List.Name = "m_teiki_List";
            this.m_teiki_List.Size = new System.Drawing.Size(960, 150);
            this.m_teiki_List.TabIndex = 193;
            this.m_teiki_List.UseCompatibleStateImageBehavior = false;
            this.m_teiki_List.View = System.Windows.Forms.View.Details;
            // 
            // linkLabel7
            // 
            this.linkLabel7.AutoSize = true;
            this.linkLabel7.Location = new System.Drawing.Point(101, 14);
            this.linkLabel7.Name = "linkLabel7";
            this.linkLabel7.Size = new System.Drawing.Size(29, 12);
            this.linkLabel7.TabIndex = 192;
            this.linkLabel7.TabStop = true;
            this.linkLabel7.Text = "登録";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 14);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 12);
            this.label13.TabIndex = 191;
            this.label13.Text = "インシデント情報";
            // 
            // m_incident_List
            // 
            this.m_incident_List.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_incident_List.GridLines = true;
            this.m_incident_List.Location = new System.Drawing.Point(8, 30);
            this.m_incident_List.Name = "m_incident_List";
            this.m_incident_List.Size = new System.Drawing.Size(960, 150);
            this.m_incident_List.TabIndex = 190;
            this.m_incident_List.UseCompatibleStateImageBehavior = false;
            this.m_incident_List.View = System.Windows.Forms.View.Details;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(65, 412);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(29, 12);
            this.linkLabel1.TabIndex = 189;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "登録";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 412);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 188;
            this.label12.Text = "計画作業";
            // 
            // m_keikaku_list
            // 
            this.m_keikaku_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_keikaku_list.GridLines = true;
            this.m_keikaku_list.Location = new System.Drawing.Point(6, 428);
            this.m_keikaku_list.Name = "m_keikaku_list";
            this.m_keikaku_list.Size = new System.Drawing.Size(1037, 150);
            this.m_keikaku_list.TabIndex = 187;
            this.m_keikaku_list.UseCompatibleStateImageBehavior = false;
            this.m_keikaku_list.View = System.Windows.Forms.View.Details;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(974, 654);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "構成情報";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.m_incidentTouroku);
            this.splitContainer3.Panel1.Controls.Add(this.m_tourokuBtn);
            this.splitContainer3.Panel1.Controls.Add(this.m_clear_btn);
            this.splitContainer3.Panel1.Controls.Add(this.m_selectBtn);
            this.splitContainer3.Panel1.Controls.Add(this.m_ipaddress);
            this.splitContainer3.Panel1.Controls.Add(this.label6);
            this.splitContainer3.Panel1.Controls.Add(this.label4);
            this.splitContainer3.Panel1.Controls.Add(this.m_hostCombo);
            this.splitContainer3.Panel1.Controls.Add(this.label3);
            this.splitContainer3.Panel1.Controls.Add(this.m_siteCombo);
            this.splitContainer3.Panel1.Controls.Add(this.label2);
            this.splitContainer3.Panel1.Controls.Add(this.m_systemCombo);
            this.splitContainer3.Panel1.Controls.Add(this.label1);
            this.splitContainer3.Panel1.Controls.Add(this.m_usernameCombo);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer2_Panel2_Paint);
            this.splitContainer3.Size = new System.Drawing.Size(968, 648);
            this.splitContainer3.SplitterDistance = 57;
            this.splitContainer3.TabIndex = 1;
            // 
            // m_incidentTouroku
            // 
            this.m_incidentTouroku.Location = new System.Drawing.Point(764, 11);
            this.m_incidentTouroku.Name = "m_incidentTouroku";
            this.m_incidentTouroku.Size = new System.Drawing.Size(68, 40);
            this.m_incidentTouroku.TabIndex = 185;
            this.m_incidentTouroku.Text = "インシデント登録";
            this.m_incidentTouroku.UseVisualStyleBackColor = true;
            this.m_incidentTouroku.Click += new System.EventHandler(this.m_incidentTouroku_Click);
            // 
            // m_tourokuBtn
            // 
            this.m_tourokuBtn.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.m_tourokuBtn.Location = new System.Drawing.Point(838, 11);
            this.m_tourokuBtn.Name = "m_tourokuBtn";
            this.m_tourokuBtn.Size = new System.Drawing.Size(87, 40);
            this.m_tourokuBtn.TabIndex = 186;
            this.m_tourokuBtn.Text = "管理情報登録";
            this.m_tourokuBtn.UseVisualStyleBackColor = true;
            this.m_tourokuBtn.Click += new System.EventHandler(this.m_tourokuBtn_Click_1);
            // 
            // m_clear_btn
            // 
            this.m_clear_btn.Location = new System.Drawing.Point(686, 11);
            this.m_clear_btn.Name = "m_clear_btn";
            this.m_clear_btn.Size = new System.Drawing.Size(72, 40);
            this.m_clear_btn.TabIndex = 184;
            this.m_clear_btn.Text = "条件クリア";
            this.m_clear_btn.UseVisualStyleBackColor = true;
            this.m_clear_btn.Click += new System.EventHandler(this.m_clear_btn_Click_1);
            // 
            // m_selectBtn
            // 
            this.m_selectBtn.Location = new System.Drawing.Point(612, 11);
            this.m_selectBtn.Name = "m_selectBtn";
            this.m_selectBtn.Size = new System.Drawing.Size(68, 40);
            this.m_selectBtn.TabIndex = 183;
            this.m_selectBtn.Text = "検索";
            this.m_selectBtn.UseVisualStyleBackColor = true;
            this.m_selectBtn.Click += new System.EventHandler(this.m_selectBtn_Click);
            // 
            // m_ipaddress
            // 
            this.m_ipaddress.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.m_ipaddress.Location = new System.Drawing.Point(442, 34);
            this.m_ipaddress.Name = "m_ipaddress";
            this.m_ipaddress.Size = new System.Drawing.Size(147, 19);
            this.m_ipaddress.TabIndex = 182;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(440, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 12);
            this.label6.TabIndex = 191;
            this.label6.Text = "IP or NATアドレス";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(226, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 12);
            this.label4.TabIndex = 190;
            this.label4.Text = "ホスト名";
            // 
            // m_hostCombo
            // 
            this.m_hostCombo.FormattingEnabled = true;
            this.m_hostCombo.Location = new System.Drawing.Point(276, 34);
            this.m_hostCombo.Name = "m_hostCombo";
            this.m_hostCombo.Size = new System.Drawing.Size(153, 20);
            this.m_hostCombo.TabIndex = 181;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(226, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 189;
            this.label3.Text = "拠点";
            // 
            // m_siteCombo
            // 
            this.m_siteCombo.FormattingEnabled = true;
            this.m_siteCombo.Location = new System.Drawing.Point(276, 11);
            this.m_siteCombo.Name = "m_siteCombo";
            this.m_siteCombo.Size = new System.Drawing.Size(153, 20);
            this.m_siteCombo.TabIndex = 180;
            this.m_siteCombo.SelectionChangeCommitted += new System.EventHandler(this.m_siteCombo_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 12);
            this.label2.TabIndex = 188;
            this.label2.Text = "システム";
            // 
            // m_systemCombo
            // 
            this.m_systemCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.m_systemCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.m_systemCombo.FormattingEnabled = true;
            this.m_systemCombo.Location = new System.Drawing.Point(60, 34);
            this.m_systemCombo.Name = "m_systemCombo";
            this.m_systemCombo.Size = new System.Drawing.Size(153, 20);
            this.m_systemCombo.TabIndex = 179;
            this.m_systemCombo.SelectionChangeCommitted += new System.EventHandler(this.m_systemCombo_SelectionChangeCommitted_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 12);
            this.label1.TabIndex = 187;
            this.label1.Text = "カスタマ名";
            // 
            // m_usernameCombo
            // 
            this.m_usernameCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.m_usernameCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.m_usernameCombo.FormattingEnabled = true;
            this.m_usernameCombo.Location = new System.Drawing.Point(60, 11);
            this.m_usernameCombo.Name = "m_usernameCombo";
            this.m_usernameCombo.Size = new System.Drawing.Size(153, 20);
            this.m_usernameCombo.TabIndex = 178;
            this.m_usernameCombo.SelectionChangeCommitted += new System.EventHandler(this.m_usernameCombo_SelectionChangeCommitted_1);
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.m_system_label);
            this.splitContainer4.Panel1.Controls.Add(this.linkLabel2);
            this.splitContainer4.Panel1.Controls.Add(this.label5);
            this.splitContainer4.Panel1.Controls.Add(this.systemList);
            this.splitContainer4.Panel1.Controls.Add(this.label7);
            this.splitContainer4.Panel1.Controls.Add(this.userList);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.splitContainer5);
            this.splitContainer4.Size = new System.Drawing.Size(968, 587);
            this.splitContainer4.SplitterDistance = 118;
            this.splitContainer4.TabIndex = 0;
            // 
            // m_system_label
            // 
            this.m_system_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_system_label.AutoSize = true;
            this.m_system_label.Location = new System.Drawing.Point(547, 10);
            this.m_system_label.Name = "m_system_label";
            this.m_system_label.Size = new System.Drawing.Size(29, 12);
            this.m_system_label.TabIndex = 194;
            this.m_system_label.TabStop = true;
            this.m_system_label.Text = "登録";
            this.m_system_label.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_system_label_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(78, 10);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(29, 12);
            this.linkLabel2.TabIndex = 193;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "登録";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked_1);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(467, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 12);
            this.label5.TabIndex = 192;
            this.label5.Text = "システム情報";
            // 
            // systemList
            // 
            this.systemList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.systemList.GridLines = true;
            this.systemList.Location = new System.Drawing.Point(464, 26);
            this.systemList.Name = "systemList";
            this.systemList.Size = new System.Drawing.Size(494, 87);
            this.systemList.TabIndex = 191;
            this.systemList.UseCompatibleStateImageBehavior = false;
            this.systemList.View = System.Windows.Forms.View.Details;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 12);
            this.label7.TabIndex = 190;
            this.label7.Text = "カスタマ情報";
            // 
            // userList
            // 
            this.userList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userList.GridLines = true;
            this.userList.Location = new System.Drawing.Point(8, 26);
            this.userList.Name = "userList";
            this.userList.Size = new System.Drawing.Size(450, 87);
            this.userList.TabIndex = 189;
            this.userList.UseCompatibleStateImageBehavior = false;
            this.userList.View = System.Windows.Forms.View.Details;
            this.userList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.userList_MouseDoubleClick_1);
            // 
            // splitContainer5
            // 
            this.splitContainer5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.linkLabel3);
            this.splitContainer5.Panel1.Controls.Add(this.label8);
            this.splitContainer5.Panel1.Controls.Add(this.siteList);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.splitContainer6);
            this.splitContainer5.Size = new System.Drawing.Size(968, 465);
            this.splitContainer5.SplitterDistance = 130;
            this.splitContainer5.TabIndex = 0;
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(67, 7);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(29, 12);
            this.linkLabel3.TabIndex = 185;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "登録";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked_1);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 184;
            this.label8.Text = "拠点情報";
            // 
            // siteList
            // 
            this.siteList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.siteList.GridLines = true;
            this.siteList.Location = new System.Drawing.Point(8, 23);
            this.siteList.Name = "siteList";
            this.siteList.Size = new System.Drawing.Size(950, 102);
            this.siteList.TabIndex = 183;
            this.siteList.UseCompatibleStateImageBehavior = false;
            this.siteList.View = System.Windows.Forms.View.Details;
            // 
            // splitContainer6
            // 
            this.splitContainer6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.Location = new System.Drawing.Point(0, 0);
            this.splitContainer6.Name = "splitContainer6";
            this.splitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.linkLabel4);
            this.splitContainer6.Panel1.Controls.Add(this.label9);
            this.splitContainer6.Panel1.Controls.Add(this.hostList);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.linkLabel6);
            this.splitContainer6.Panel2.Controls.Add(this.linkLabel5);
            this.splitContainer6.Panel2.Controls.Add(this.label11);
            this.splitContainer6.Panel2.Controls.Add(this.kaisenList);
            this.splitContainer6.Panel2.Controls.Add(this.label10);
            this.splitContainer6.Panel2.Controls.Add(this.interfaceList);
            this.splitContainer6.Size = new System.Drawing.Size(968, 331);
            this.splitContainer6.SplitterDistance = 184;
            this.splitContainer6.TabIndex = 0;
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Location = new System.Drawing.Point(67, 6);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(29, 12);
            this.linkLabel4.TabIndex = 186;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "登録";
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked_1);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 12);
            this.label9.TabIndex = 185;
            this.label9.Text = "ホスト情報";
            // 
            // hostList
            // 
            this.hostList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostList.GridLines = true;
            this.hostList.Location = new System.Drawing.Point(8, 22);
            this.hostList.Name = "hostList";
            this.hostList.Size = new System.Drawing.Size(950, 150);
            this.hostList.TabIndex = 184;
            this.hostList.UseCompatibleStateImageBehavior = false;
            this.hostList.View = System.Windows.Forms.View.Details;
            // 
            // linkLabel6
            // 
            this.linkLabel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel6.AutoSize = true;
            this.linkLabel6.Location = new System.Drawing.Point(526, 8);
            this.linkLabel6.Name = "linkLabel6";
            this.linkLabel6.Size = new System.Drawing.Size(29, 12);
            this.linkLabel6.TabIndex = 200;
            this.linkLabel6.TabStop = true;
            this.linkLabel6.Text = "登録";
            this.linkLabel6.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel6_LinkClicked_1);
            // 
            // linkLabel5
            // 
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.Location = new System.Drawing.Point(138, 7);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(29, 12);
            this.linkLabel5.TabIndex = 199;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Text = "登録";
            this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel5_LinkClicked_1);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(467, 8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 198;
            this.label11.Text = "回線情報";
            // 
            // kaisenList
            // 
            this.kaisenList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kaisenList.GridLines = true;
            this.kaisenList.Location = new System.Drawing.Point(464, 23);
            this.kaisenList.Name = "kaisenList";
            this.kaisenList.Size = new System.Drawing.Size(494, 115);
            this.kaisenList.TabIndex = 197;
            this.kaisenList.UseCompatibleStateImageBehavior = false;
            this.kaisenList.View = System.Windows.Forms.View.Details;
            this.kaisenList.SelectedIndexChanged += new System.EventHandler(this.kaisenList_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(122, 12);
            this.label10.TabIndex = 196;
            this.label10.Text = "監視インターフェイス情報";
            // 
            // interfaceList
            // 
            this.interfaceList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.interfaceList.GridLines = true;
            this.interfaceList.Location = new System.Drawing.Point(8, 23);
            this.interfaceList.Name = "interfaceList";
            this.interfaceList.Size = new System.Drawing.Size(450, 115);
            this.interfaceList.TabIndex = 195;
            this.interfaceList.UseCompatibleStateImageBehavior = false;
            this.interfaceList.View = System.Windows.Forms.View.Details;
            // 
            // splitter1
            // 
            this.splitter1.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(982, 3);
            this.splitter1.TabIndex = 185;
            this.splitter1.TabStop = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Enterprise Building.png");
            this.imageList1.Images.SetKeyName(1, "Server rack.png");
            this.imageList1.Images.SetKeyName(2, "RunBooks.png");
            this.imageList1.Images.SetKeyName(3, "Device.png");
            this.imageList1.Images.SetKeyName(4, "Performance monitor.png");
            this.imageList1.Images.SetKeyName(5, "green__35x31.png");
            this.imageList1.Images.SetKeyName(6, "red__35x31.png");
            // 
            // timer1
            // 
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // m_opename
            // 
            this.m_opename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_opename.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.m_opename.Location = new System.Drawing.Point(3, 5);
            this.m_opename.Name = "m_opename";
            this.m_opename.ReadOnly = true;
            this.m_opename.Size = new System.Drawing.Size(152, 18);
            this.m_opename.TabIndex = 1;
            // 
            // Form_MainList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1158, 687);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form_MainList";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_MainList_FormClosed);
            this.Load += new System.EventHandler(this.Form_MainList_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel1.PerformLayout();
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel1.PerformLayout();
            this.splitContainer6.Panel2.ResumeLayout(false);
            this.splitContainer6.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
            this.splitContainer6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button m_refresh_btn;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button m_incidentTouroku;
        private System.Windows.Forms.Button m_tourokuBtn;
        private System.Windows.Forms.Button m_clear_btn;
        private System.Windows.Forms.Button m_selectBtn;
        private System.Windows.Forms.TextBox m_ipaddress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox m_hostCombo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox m_siteCombo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox m_systemCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox m_usernameCombo;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.LinkLabel m_system_label;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListView systemList;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView userList;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListView siteList;
        private System.Windows.Forms.SplitContainer splitContainer6;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ListView hostList;
        private System.Windows.Forms.LinkLabel linkLabel6;
        private System.Windows.Forms.LinkLabel linkLabel5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListView kaisenList;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListView interfaceList;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ListView m_keikaku_list;
        private System.Windows.Forms.LinkLabel linkLabel8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ListView m_teiki_List;
        private System.Windows.Forms.LinkLabel linkLabel7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ListView m_incident_List;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox m_opename;
    }
}

