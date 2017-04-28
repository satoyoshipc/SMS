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
            this.splitter5 = new System.Windows.Forms.Splitter();
            this.panel6 = new System.Windows.Forms.Panel();
            this.sagyoList = new System.Windows.Forms.ListView();
            this.linkLabel7 = new System.Windows.Forms.LinkLabel();
            this.label12 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.linkLabel6 = new System.Windows.Forms.LinkLabel();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.label11 = new System.Windows.Forms.Label();
            this.kaisenList = new System.Windows.Forms.ListView();
            this.label10 = new System.Windows.Forms.Label();
            this.interfaceList = new System.Windows.Forms.ListView();
            this.splitter4 = new System.Windows.Forms.Splitter();
            this.panel4 = new System.Windows.Forms.Panel();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.label9 = new System.Windows.Forms.Label();
            this.hostList = new System.Windows.Forms.ListView();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.label8 = new System.Windows.Forms.Label();
            this.siteList = new System.Windows.Forms.ListView();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_system_label = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.systemList = new System.Windows.Forms.ListView();
            this.label7 = new System.Windows.Forms.Label();
            this.userList = new System.Windows.Forms.ListView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.m_tourokuBtn = new System.Windows.Forms.Button();
            this.m_clear_btn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
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
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.splitContainer1.Panel2.Controls.Add(this.splitter5);
            this.splitContainer1.Panel2.Controls.Add(this.panel6);
            this.splitContainer1.Panel2.Controls.Add(this.panel5);
            this.splitContainer1.Panel2.Controls.Add(this.splitter4);
            this.splitContainer1.Panel2.Controls.Add(this.panel4);
            this.splitContainer1.Panel2.Controls.Add(this.splitter3);
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Panel2.Controls.Add(this.splitter2);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.splitter1);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(1158, 687);
            this.splitContainer1.SplitterDistance = 206;
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
            this.splitContainer2.Panel1.Controls.Add(this.m_refresh_btn);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.treeView1);
            this.splitContainer2.Size = new System.Drawing.Size(202, 683);
            this.splitContainer2.SplitterDistance = 25;
            this.splitContainer2.TabIndex = 1;
            // 
            // m_refresh_btn
            // 
            this.m_refresh_btn.Location = new System.Drawing.Point(3, 3);
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
            this.treeView1.Size = new System.Drawing.Size(202, 654);
            this.treeView1.TabIndex = 1;
            this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // splitter5
            // 
            this.splitter5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter5.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter5.Location = new System.Drawing.Point(0, 548);
            this.splitter5.Name = "splitter5";
            this.splitter5.Size = new System.Drawing.Size(944, 10);
            this.splitter5.TabIndex = 193;
            this.splitter5.TabStop = false;
            // 
            // panel6
            // 
            this.panel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel6.Controls.Add(this.sagyoList);
            this.panel6.Controls.Add(this.linkLabel7);
            this.panel6.Controls.Add(this.label12);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 548);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(944, 135);
            this.panel6.TabIndex = 196;
            // 
            // sagyoList
            // 
            this.sagyoList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sagyoList.GridLines = true;
            this.sagyoList.Location = new System.Drawing.Point(20, 33);
            this.sagyoList.Name = "sagyoList";
            this.sagyoList.Size = new System.Drawing.Size(914, 87);
            this.sagyoList.TabIndex = 187;
            this.sagyoList.UseCompatibleStateImageBehavior = false;
            this.sagyoList.View = System.Windows.Forms.View.Details;
            // 
            // linkLabel7
            // 
            this.linkLabel7.AutoSize = true;
            this.linkLabel7.Location = new System.Drawing.Point(77, 18);
            this.linkLabel7.Name = "linkLabel7";
            this.linkLabel7.Size = new System.Drawing.Size(29, 12);
            this.linkLabel7.TabIndex = 186;
            this.linkLabel7.TabStop = true;
            this.linkLabel7.Text = "登録";
            this.linkLabel7.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel7_LinkClicked_1);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(18, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 185;
            this.label12.Text = "作業情報";
            // 
            // panel5
            // 
            this.panel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel5.Controls.Add(this.linkLabel6);
            this.panel5.Controls.Add(this.linkLabel5);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.kaisenList);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.interfaceList);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 440);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(944, 108);
            this.panel5.TabIndex = 192;
            // 
            // linkLabel6
            // 
            this.linkLabel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel6.AutoSize = true;
            this.linkLabel6.Location = new System.Drawing.Point(534, 4);
            this.linkLabel6.Name = "linkLabel6";
            this.linkLabel6.Size = new System.Drawing.Size(29, 12);
            this.linkLabel6.TabIndex = 188;
            this.linkLabel6.TabStop = true;
            this.linkLabel6.Text = "登録";
            this.linkLabel6.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel6_LinkClicked);
            // 
            // linkLabel5
            // 
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.Location = new System.Drawing.Point(150, 4);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(29, 12);
            this.linkLabel5.TabIndex = 187;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Text = "登録";
            this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel5_LinkClicked);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(475, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 186;
            this.label11.Text = "回線情報";
            // 
            // kaisenList
            // 
            this.kaisenList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kaisenList.GridLines = true;
            this.kaisenList.Location = new System.Drawing.Point(472, 19);
            this.kaisenList.Name = "kaisenList";
            this.kaisenList.Size = new System.Drawing.Size(462, 83);
            this.kaisenList.TabIndex = 185;
            this.kaisenList.UseCompatibleStateImageBehavior = false;
            this.kaisenList.View = System.Windows.Forms.View.Details;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(122, 12);
            this.label10.TabIndex = 184;
            this.label10.Text = "監視インターフェイス情報";
            // 
            // interfaceList
            // 
            this.interfaceList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.interfaceList.GridLines = true;
            this.interfaceList.Location = new System.Drawing.Point(20, 20);
            this.interfaceList.Name = "interfaceList";
            this.interfaceList.Size = new System.Drawing.Size(446, 83);
            this.interfaceList.TabIndex = 183;
            this.interfaceList.UseCompatibleStateImageBehavior = false;
            this.interfaceList.View = System.Windows.Forms.View.Details;
            // 
            // splitter4
            // 
            this.splitter4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter4.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter4.Location = new System.Drawing.Point(0, 430);
            this.splitter4.Name = "splitter4";
            this.splitter4.Size = new System.Drawing.Size(944, 10);
            this.splitter4.TabIndex = 191;
            this.splitter4.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel4.Controls.Add(this.linkLabel4);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.hostList);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 304);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(944, 126);
            this.panel4.TabIndex = 190;
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Location = new System.Drawing.Point(79, 6);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(29, 12);
            this.linkLabel4.TabIndex = 183;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "登録";
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 12);
            this.label9.TabIndex = 182;
            this.label9.Text = "ホスト情報";
            // 
            // hostList
            // 
            this.hostList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostList.GridLines = true;
            this.hostList.Location = new System.Drawing.Point(20, 22);
            this.hostList.Name = "hostList";
            this.hostList.Size = new System.Drawing.Size(914, 98);
            this.hostList.TabIndex = 181;
            this.hostList.UseCompatibleStateImageBehavior = false;
            this.hostList.View = System.Windows.Forms.View.Details;
            // 
            // splitter3
            // 
            this.splitter3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter3.Location = new System.Drawing.Point(0, 294);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(944, 10);
            this.splitter3.TabIndex = 189;
            this.splitter3.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel3.Controls.Add(this.linkLabel3);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.siteList);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 190);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(944, 104);
            this.panel3.TabIndex = 188;
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(79, 7);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(29, 12);
            this.linkLabel3.TabIndex = 182;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "登録";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 181;
            this.label8.Text = "拠点情報";
            // 
            // siteList
            // 
            this.siteList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.siteList.GridLines = true;
            this.siteList.Location = new System.Drawing.Point(20, 23);
            this.siteList.Name = "siteList";
            this.siteList.Size = new System.Drawing.Size(914, 79);
            this.siteList.TabIndex = 180;
            this.siteList.UseCompatibleStateImageBehavior = false;
            this.siteList.View = System.Windows.Forms.View.Details;
            // 
            // splitter2
            // 
            this.splitter2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter2.Location = new System.Drawing.Point(0, 182);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(944, 8);
            this.splitter2.TabIndex = 187;
            this.splitter2.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.m_system_label);
            this.panel2.Controls.Add(this.linkLabel2);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.systemList);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.userList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 62);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(944, 120);
            this.panel2.TabIndex = 186;
            // 
            // m_system_label
            // 
            this.m_system_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_system_label.AutoSize = true;
            this.m_system_label.Location = new System.Drawing.Point(555, 6);
            this.m_system_label.Name = "m_system_label";
            this.m_system_label.Size = new System.Drawing.Size(29, 12);
            this.m_system_label.TabIndex = 188;
            this.m_system_label.TabStop = true;
            this.m_system_label.Text = "登録";
            this.m_system_label.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(87, 6);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(29, 12);
            this.linkLabel2.TabIndex = 187;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "登録";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(475, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 12);
            this.label5.TabIndex = 186;
            this.label5.Text = "システム情報";
            // 
            // systemList
            // 
            this.systemList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.systemList.GridLines = true;
            this.systemList.Location = new System.Drawing.Point(472, 22);
            this.systemList.Name = "systemList";
            this.systemList.Size = new System.Drawing.Size(462, 92);
            this.systemList.TabIndex = 185;
            this.systemList.UseCompatibleStateImageBehavior = false;
            this.systemList.View = System.Windows.Forms.View.Details;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 184;
            this.label7.Text = "ユーザ情報";
            // 
            // userList
            // 
            this.userList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userList.GridLines = true;
            this.userList.Location = new System.Drawing.Point(20, 22);
            this.userList.Name = "userList";
            this.userList.Size = new System.Drawing.Size(446, 92);
            this.userList.TabIndex = 183;
            this.userList.UseCompatibleStateImageBehavior = false;
            this.userList.View = System.Windows.Forms.View.Details;
            this.userList.SelectedIndexChanged += new System.EventHandler(this.userList_SelectedIndexChanged);
            // 
            // splitter1
            // 
            this.splitter1.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 59);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(944, 3);
            this.splitter1.TabIndex = 185;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.m_tourokuBtn);
            this.panel1.Controls.Add(this.m_clear_btn);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.m_ipaddress);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.m_hostCombo);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.m_siteCombo);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.m_systemCombo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.m_usernameCombo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(944, 59);
            this.panel1.TabIndex = 184;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(775, 10);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(68, 40);
            this.button3.TabIndex = 7;
            this.button3.Text = "インシデント登録";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // m_tourokuBtn
            // 
            this.m_tourokuBtn.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.m_tourokuBtn.Location = new System.Drawing.Point(849, 10);
            this.m_tourokuBtn.Name = "m_tourokuBtn";
            this.m_tourokuBtn.Size = new System.Drawing.Size(87, 40);
            this.m_tourokuBtn.TabIndex = 8;
            this.m_tourokuBtn.Text = "管理情報登録";
            this.m_tourokuBtn.UseVisualStyleBackColor = true;
            this.m_tourokuBtn.Click += new System.EventHandler(this.m_tourokuBtn_Click);
            // 
            // m_clear_btn
            // 
            this.m_clear_btn.Location = new System.Drawing.Point(697, 10);
            this.m_clear_btn.Name = "m_clear_btn";
            this.m_clear_btn.Size = new System.Drawing.Size(72, 40);
            this.m_clear_btn.TabIndex = 6;
            this.m_clear_btn.Text = "条件クリア";
            this.m_clear_btn.UseVisualStyleBackColor = true;
            this.m_clear_btn.Click += new System.EventHandler(this.m_clear_btn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(623, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 40);
            this.button1.TabIndex = 5;
            this.button1.Text = "検索";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // m_ipaddress
            // 
            this.m_ipaddress.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.m_ipaddress.Location = new System.Drawing.Point(453, 33);
            this.m_ipaddress.Name = "m_ipaddress";
            this.m_ipaddress.Size = new System.Drawing.Size(147, 19);
            this.m_ipaddress.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(451, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 12);
            this.label6.TabIndex = 177;
            this.label6.Text = "IP or NATアドレス";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(237, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 12);
            this.label4.TabIndex = 176;
            this.label4.Text = "ホスト名";
            // 
            // m_hostCombo
            // 
            this.m_hostCombo.FormattingEnabled = true;
            this.m_hostCombo.Location = new System.Drawing.Point(287, 33);
            this.m_hostCombo.Name = "m_hostCombo";
            this.m_hostCombo.Size = new System.Drawing.Size(153, 20);
            this.m_hostCombo.TabIndex = 3;
            this.m_hostCombo.SelectionChangeCommitted += new System.EventHandler(this.comboBox4_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(237, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 174;
            this.label3.Text = "拠点";
            // 
            // m_siteCombo
            // 
            this.m_siteCombo.FormattingEnabled = true;
            this.m_siteCombo.Location = new System.Drawing.Point(287, 10);
            this.m_siteCombo.Name = "m_siteCombo";
            this.m_siteCombo.Size = new System.Drawing.Size(153, 20);
            this.m_siteCombo.TabIndex = 2;
            this.m_siteCombo.SelectionChangeCommitted += new System.EventHandler(this.comboBox3_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 12);
            this.label2.TabIndex = 172;
            this.label2.Text = "システム";
            // 
            // m_systemCombo
            // 
            this.m_systemCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.m_systemCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.m_systemCombo.FormattingEnabled = true;
            this.m_systemCombo.Location = new System.Drawing.Point(71, 33);
            this.m_systemCombo.Name = "m_systemCombo";
            this.m_systemCombo.Size = new System.Drawing.Size(153, 20);
            this.m_systemCombo.TabIndex = 1;
            this.m_systemCombo.SelectionChangeCommitted += new System.EventHandler(this.m_systemCombo_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 170;
            this.label1.Text = "ユーザ名";
            // 
            // m_usernameCombo
            // 
            this.m_usernameCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.m_usernameCombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.m_usernameCombo.FormattingEnabled = true;
            this.m_usernameCombo.Location = new System.Drawing.Point(71, 10);
            this.m_usernameCombo.Name = "m_usernameCombo";
            this.m_usernameCombo.Size = new System.Drawing.Size(153, 20);
            this.m_usernameCombo.TabIndex = 0;
            this.m_usernameCombo.SelectionChangeCommitted += new System.EventHandler(this.m_usernameCombo_SelectionChangeCommitted);
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
            // 
            // Form_MainList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1158, 687);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form_MainList";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form_MainList_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel m_system_label;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListView systemList;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView userList;
        private System.Windows.Forms.Button m_clear_btn;
        private System.Windows.Forms.Button button1;
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
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListView siteList;
        private System.Windows.Forms.Splitter splitter5;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.LinkLabel linkLabel6;
        private System.Windows.Forms.LinkLabel linkLabel5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListView kaisenList;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListView interfaceList;
        private System.Windows.Forms.Splitter splitter4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ListView hostList;
        private System.Windows.Forms.Button m_tourokuBtn;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.ListView sagyoList;
        private System.Windows.Forms.LinkLabel linkLabel7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button m_refresh_btn;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageList1;
    }
}

