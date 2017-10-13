using System;
using Npgsql;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace moss_AP
{
    public partial class Form_IncidentMailSend : Form
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        //DBコネクション
        public NpgsqlConnection con { get; set; }

        //カスタマ
        public List<userDS> userDSList { get; set; }

        //システム
        public List<systemDS> systemDSList { get; set; }

        //拠点
        public List<siteDS> siteDSList { get; set; }
        //ホスト
        public List<hostDS> hostDSList { get; set; }
        //インターフェイス
        public List<watch_InterfaceDS> interfaceDSList { get; set; }


        public taskDS taskds { get; set; }
        public timerDS timerds { get; set; }
        public List<timerDS> timerDSList { get; set; }

        //システム情報表示用
        DataTable system_list;

        //拠点情報表示用   
        DataTable site_list;
        //機器情報表示用
        DataTable host_list;
        //インターフェイス情報表示用
        DataTable interface_list;


        //テンプレート
        public templeteDS templetedt { get; set; }

        //テンプレート一覧
        public List<templeteDS> templist { get; set; }

        DISP_dataSet dsp_L;

        //ListViewのソートの際に使用する
        private int sort_kind_site = 0;
        private int sort_kind_system = 0;
        private int sort_kind_host = 0;
        private int sort_kind_interface = 0;

        public Form_IncidentMailSend()
        {
            InitializeComponent();
        }

        //コンテキストメニューが開く前処理
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            // コンテキストメニューをクリア
            this.contextMenuStrip1.Items.Clear();

            ToolStripMenuItem tsi1 = new ToolStripMenuItem();
            tsi1.Text = "構成管理";
            tsi1.ToolTipText = "選択されている構成管理情報を表示";

            //カスタマ名
            ToolStripMenuItem tsi2 = new ToolStripMenuItem();
            tsi2.Text = m_usernameCombo.Text;
            tsi2.ToolTipText = "選択されているカスタマ名";

            // クリックイベントを追加する
            // フォームで設定した ItemClicked イベントは第1階層の項目のみ発生する
            tsi2.Click += new EventHandler(contextMenuStrip_SubMenuClick);

            // 第1階層のメニューの最後尾に追加
            tsi1.DropDownItems.Add(tsi2);

            string sysname = "";
            ListView.SelectedIndexCollection sysitem = m_system_List.SelectedIndices;

            //選択項目を取得
            if (sysitem.Count > 0 && this.m_system_List.Items[sysitem[0]].SubItems[2].Text != "")
            {
                sysname = this.m_system_List.Items[sysitem[0]].SubItems[2].Text;

                ToolStripMenuItem tsi3 = new ToolStripMenuItem();


                tsi3.Size = new Size(400,80);
                //

                tsi3.Text = sysname;
                tsi3.ToolTipText = "選択されているシステム名";
                tsi3.Click += new EventHandler(contextMenuStrip_SubMenuClick);


                // 第1階層のメニューの最後尾に追加する
                tsi1.DropDownItems.Add(tsi3);
            }
            else
            {

                if (systemDSList == null || systemDSList.Count == 0)
                    return;

                ToolStripComboBox sub_system = new ToolStripComboBox();
                sub_system.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

                sub_system.Size = new Size(400, 80);

                //選択されているシステムが存在しない場合は全件表示する
                foreach (systemDS v in systemDSList) { 
                    sub_system.ComboBox.Items.Add(v.systemname);
                }

                sub_system.ComboBox.SelectedIndex = 0;

                sub_system.SelectedIndexChanged += new EventHandler(contextMenuStripCombo_SubMenuClick);


                tsi1.DropDownItems.Add(sub_system);
            }

            //拠点情報
            string sitename = "";
            ListView.SelectedIndexCollection sitelist = m_site_List.SelectedIndices;
            if (sitelist.Count > 0 && this.m_site_List.Items[sitelist[0]].SubItems[2].Text != "")
            {
                sitename = this.m_site_List.Items[sitelist[0]].SubItems[2].Text;

                ToolStripMenuItem tsi4 = new ToolStripMenuItem();
                tsi4.Size = new Size(400, 80);

                tsi4.Text = sitename;
                tsi4.ToolTipText = "選択されている拠点名";
                //tsi3.Click += contextMenuStrip_SubMenuClick;

                tsi4.Click += new EventHandler(contextMenuStrip_SubMenuClick);

                // 第1階層のメニューの最後尾に追加する
                tsi1.DropDownItems.Add(tsi4);
            }
            else
            {

                if (siteDSList != null && siteDSList.Count > 0) { 

                    ToolStripComboBox sub_site = new ToolStripComboBox();
                    sub_site.Size = new Size(400, 80);
                    sub_site.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

                    //選択されている拠点が存在しない場合は全件表示する
                    foreach (siteDS v in siteDSList)
                        sub_site.ComboBox.Items.Add(v.sitename);

                    //初期値0以外を選択する
                    sub_site.ComboBox.SelectedIndex = 0;

                    sub_site.SelectedIndexChanged += new EventHandler(contextMenuStripCombo_SubMenuClick);

                    tsi1.DropDownItems.Add(sub_site);
                }
            }

            //ホスト情報
            string hostname = "";
            ListView.SelectedIndexCollection hostlist = m_host_list.SelectedIndices;
            if (hostlist.Count > 0 && this.m_host_list.Items[hostlist[0]].SubItems[2].Text != "")
            {
                hostname = this.m_host_list.Items[hostlist[0]].SubItems[2].Text;

                ToolStripMenuItem sub_host = new ToolStripMenuItem();

                sub_host.Size = new Size(400, 80);


                sub_host.Text = hostname;
                sub_host.ToolTipText = "選択されている拠点名";
                //tsi3.Click += contextMenuStrip_SubMenuClick;

                sub_host.Click += new EventHandler(contextMenuStrip_SubMenuClick);

                // 第1階層のメニューの最後尾に追加する
                tsi1.DropDownItems.Add(sub_host);
            }
            else
            {

                if (hostDSList != null && hostDSList.Count > 0)
                {


                    //コンボボックスにする
                    ToolStripComboBox sub_host = new ToolStripComboBox();
                    sub_host.Size = new Size(400, 80);
                    sub_host.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

                    //選択されているホストが存在しない場合は全件表示する
                    foreach (hostDS v in hostDSList)
                        sub_host.ComboBox.Items.Add(v.hostname);

                    sub_host.ComboBox.SelectedIndex = 0;
                    sub_host.SelectedIndexChanged += new EventHandler(contextMenuStripCombo_SubMenuClick);

                    tsi1.DropDownItems.Add(sub_host);
                }
            }

            //インターフェイス情報
            string interfacename = "";
            ListView.SelectedIndexCollection interfacelist = m_host_list.SelectedIndices;
            if (interfacelist.Count > 0 && this.m_interface_List.Items[interfacelist[0]].SubItems[2].Text != "")
            {
                interfacename = this.m_interface_List.Items[interfacelist[0]].SubItems[2].Text;

                ToolStripMenuItem sub_interface = new ToolStripMenuItem();
                sub_interface.Size = new Size(400, 80);


                sub_interface.Text = interfacename;
                sub_interface.ToolTipText = "選択されている拠点名";
                //tsi3.Click += contextMenuStrip_SubMenuClick;

                sub_interface.Click += new EventHandler(contextMenuStrip_SubMenuClick);

                // 第1階層のメニューの最後尾に追加する
                tsi1.DropDownItems.Add(sub_interface);
            }
            else
            {
                if (interfaceDSList != null && interfaceDSList.Count > 0)
                {
                    ToolStripMenuItem sub_interface = new ToolStripMenuItem();
                    sub_interface.Size = new Size(400, 80);
                    sub_interface.Text = "監視インターフェイス";

                    ToolStripComboBox sub_interfacename = new ToolStripComboBox();
                    ToolStripComboBox sub_ipaddress = new ToolStripComboBox();
                    ToolStripComboBox sub_ipaddressNAT = new ToolStripComboBox();
                    ToolStripComboBox sub_kanshiKoumoku = new ToolStripComboBox();

                    sub_interfacename.Size = new Size(400, 80);
                    sub_interfacename.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    sub_ipaddress.Size = new Size(400, 80);
                    sub_ipaddress.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    sub_ipaddressNAT.Size = new Size(400, 80);
                    sub_ipaddressNAT.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    sub_kanshiKoumoku.Size = new Size(400, 80);
                    sub_kanshiKoumoku.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

                    //選択されているホストが存在しない場合は全件表示する
                    foreach (watch_InterfaceDS v in interfaceDSList)
                    {
                        sub_interfacename.ComboBox.Items.Add(v.interfacename);

                        sub_ipaddress.ComboBox.Items.Add(v.IPaddress);
                        sub_ipaddressNAT.ComboBox.Items.Add(v.IPaddressNAT);
                        sub_kanshiKoumoku.ComboBox.Items.Add(v.kanshi);
                    }


                    sub_interfacename.ComboBox.SelectedIndex = 0;
                    sub_ipaddress.ComboBox.SelectedIndex = 0;
                    sub_ipaddressNAT.ComboBox.SelectedIndex = 0;
                    sub_kanshiKoumoku.ComboBox.SelectedIndex = 0;

                    sub_interfacename.SelectedIndexChanged += new EventHandler(contextMenuStripCombo_SubMenuClick);

                    sub_interface.DropDownItems.Add(sub_interfacename);
                    sub_interface.DropDownItems.Add(sub_ipaddress);
                    sub_interface.DropDownItems.Add(sub_ipaddressNAT);
                    sub_interface.DropDownItems.Add(sub_kanshiKoumoku);


                    tsi1.DropDownItems.Add(sub_interface);
                }
            }

            // コンテキストメニューに第1階層のメニューを追加する
            this.contextMenuStrip1.Items.Add(tsi1);

            ToolStripMenuItem closemenu = new ToolStripMenuItem();
            closemenu.Size = new Size(400, 80);


            closemenu.Text = "閉じる";
            closemenu.ToolTipText = "メニューを閉じる";

            closemenu.Click += new EventHandler(contextMenuStrip_closeClick);

            this.contextMenuStrip1.Items.Add(closemenu);
        }
        private void contextMenuStrip_closeClick(object sender, EventArgs e)
        {
            contextMenuStrip1.Close();
        }
        private void contextMenuStrip_SubMenuClick(object sender, EventArgs e)
        {
            // sender にはクリックされたメニューの ToolStripMenuItem が入ってきますので、
            // 必要に応じて処理を行います
            string str = "";

            ToolStripMenuItem contextmenu = (ToolStripMenuItem)sender;
            str = contextmenu.Text;
            insertChangeWords(str);
#if DEBUG
            Console.WriteLine(str);
#endif

            contextMenuStrip1.Close();

        }

        // 第2階層のメニュー項目のクリックイベント
        private void contextMenuStripCombo_SubMenuClick(object sender, EventArgs e)
        {
            // sender にはクリックされたメニューの ToolStripMenuItem が入ってきますので、
            // 必要に応じて処理を行います
            string str = "";

            ToolStripComboBox combo = (ToolStripComboBox)sender;
            str = combo.Text;
            insertChangeWords(str);
#if DEBUG
            Console.WriteLine(str);
#endif

            contextMenuStrip1.Close();

        }
        //パラメータの文字列を挿入して色を変える
        private void insertChangeWords(String words)
        {
            //選択状態を解除しておく
            m_body.SelectedText = "";
            m_body.SelectionLength = 0;

            //赤にする
            m_body.SelectionColor = Color.Red;
            //BoidをFontStyleに追加したFontを作成する
            Font baseFont = m_body.SelectionFont;
            Font fnt = new Font(baseFont.FontFamily,
                baseFont.Size,
                baseFont.Style | FontStyle.Bold);
            //Fontを変更する
            m_body.SelectionFont = fnt;
            //文字列を挿入する
            m_body.SelectedText = words;

            baseFont.Dispose();
            fnt.Dispose();


        }

        //表示前処理
        private void Form_IncidentMailSend_Load(object sender, EventArgs e)
        {
            //カスタマ名コンボボックスに表示する値を取得する

            DataTable cutomerTable = new DataTable();
            cutomerTable.Columns.Add("ID", typeof(string));
            cutomerTable.Columns.Add("NAME", typeof(string));

            Class_Detaget getuser = new Class_Detaget();
            getuser.con = con;

            //カスタマ名を取得
            userDSList = getuser.getUserList();

            if (userDSList == null)
                return;

            //カスタマ情報を取得する
            foreach (userDS v in userDSList)
            {
                DataRow row = cutomerTable.NewRow();
                row["ID"] = v.userno;
                row["NAME"] = v.username;
                cutomerTable.Rows.Add(row);
            }

            //データテーブルを割り当てる
            m_usernameCombo.DataSource = cutomerTable;
            m_usernameCombo.DisplayMember = "NAME";
            m_usernameCombo.ValueMember = "ID";

            //Read_systemCombo();
            //ラベルに反映
            if (m_usernameCombo.SelectedValue != null)
                m_userno.Text = m_usernameCombo.SelectedValue.ToString();
        }

        //カスタマ名コンボボックスが変更されたとき
        private void m_usernameCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (m_usernameCombo.Text == "")
            {
                m_userno.Text = "";
                return;
            }
            //ラベルに反映
            if (m_usernameCombo.SelectedValue != null)
                m_userno.Text = m_usernameCombo.SelectedValue.ToString();
            taskchange();
        }
        //タスク区分コンボが変更された時
        private void taskchange()
        {

            //インシデントタスクテンプレートを取得する
            m_templeteCombo.Enabled = true;

            string userno = m_userno.Text;
            m_templeteCombo.DataSource = null;
            Class_Detaget dg = new Class_Detaget();
            templist
                = dg.getTempleteList(userno, "1", con, true);

            //コンボボックス
            DataTable templeteTable = new DataTable();
            templeteTable.Columns.Add("ID", typeof(string));
            templeteTable.Columns.Add("NAME", typeof(string));

            if (templist == null)
                return;

            //空白行を追加
            templeteDS tmp = new templeteDS();
            tmp.templeteno = "";
            tmp.templetename = "";

            List<templeteDS> temptemplist = new List<templeteDS>();
            temptemplist.Add(tmp);

            //テンプレート情報を取得する
            foreach (templeteDS v in templist)
            {
                DataRow row = templeteTable.NewRow();
                row["ID"] = v.templeteno;
                row["NAME"] = v.templetename;
                templeteTable.Rows.Add(row);
            }

            //データテーブルを割り当てる
            m_templeteCombo.DataSource = templeteTable;
            m_templeteCombo.DisplayMember = "NAME";
            m_templeteCombo.ValueMember = "ID";
            //初期値を反映させる
            templeteSelect();

        }
        private void templeteSelect()
        {
            //m_title.Text = "";
            //mailsendaddressDS?te.Text = "";

            //テンプレート件数分ループを行う
            foreach (templeteDS v in templist)
            {
                if (m_templeteCombo.SelectedValue != null)
                {
                    if (v.templeteno == m_templeteCombo.SelectedValue.ToString())
                    {
                        m_templetename.Text = v.title;
                        m_body.Text = v.text;
                    }

                }
            }
        }
        //テンプレート名を選択
        private void m_templeteCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            templeteSelect();


        }
        //検索ボタン
        private void m_selectBtn_Click(object sender, EventArgs e)
        {
            this.m_selectBtn.Enabled = false;
            try
            {

                m_system_List.Clear();
                m_site_List.Clear();
                m_host_list.Clear();
                m_interface_List.Clear();


                Dictionary<string, string> param_dict = new Dictionary<string, string>();

                Class_Detaget getdata = new Class_Detaget();

                dsp_L = new DISP_dataSet();

                //カスタマ名コンボボックス
                if (m_usernameCombo.Text != "")
                {
                    param_dict["username"] = m_usernameCombo.Text;
                }

                //構成データの取得
                getdata.con = con;
                dsp_L = getdata.getSelectDataFor_Interface(param_dict, con, dsp_L);


                disp_system(dsp_L);

                disp_site(dsp_L);

                disp_host(dsp_L);

                disp_interface(dsp_L);

                //テンプレートを取得
                taskchange();


            }
            catch (Exception ex)
            {
                MessageBox.Show("構成情報の表示時にエラーが発生しました。" + ex.Message, "構成情報表示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                logger.ErrorFormat("構成情報の表示時にエラーが発生しました。" + ex.Message );
            }
            finally
            {
                this.m_selectBtn.Enabled = true;
            }

            this.m_selectBtn.Enabled = true;
        }

        //システム情報の表示
        private void disp_system(DISP_dataSet dsp_L)
        {
            //システムリスト
            this.m_system_List.VirtualMode = true;

            // １行全体選択
            //this.m_system_List.FullRowSelect = false;
            this.m_system_List.HideSelection = false;
            this.m_system_List.HeaderStyle = ColumnHeaderStyle.Clickable;
            //Hook up handlers for VirtualMode events.
            this.m_system_List.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(systemList_RetrieveVirtualItem);
            this.m_system_List.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            // Column追加
            this.m_system_List.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_system_List.Columns.Insert(1, "有効", 50, HorizontalAlignment.Left);
            this.m_system_List.Columns.Insert(2, "システム名", 180, HorizontalAlignment.Left);
            this.m_system_List.Columns.Insert(3, "システム名カナ", 50, HorizontalAlignment.Left);
            this.m_system_List.Columns.Insert(4, "備考", 30, HorizontalAlignment.Left);
            this.m_system_List.Columns.Insert(5, "更新日時", 80, HorizontalAlignment.Left);
            this.m_system_List.Columns.Insert(6, "更新者", 80, HorizontalAlignment.Left);

            //リストビューを初期化する
            system_list = new DataTable("table3");
            system_list.Columns.Add("No", Type.GetType("System.Int32"));
            system_list.Columns.Add("有効", Type.GetType("System.String"));
            system_list.Columns.Add("システム名", Type.GetType("System.String"));
            system_list.Columns.Add("システム名カナ", Type.GetType("System.String"));

            system_list.Columns.Add("備考", Type.GetType("System.String"));
            system_list.Columns.Add("更新日時", Type.GetType("System.String"));
            system_list.Columns.Add("更新者", Type.GetType("System.String"));

            //システム情報
            if (dsp_L.system_L != null)
            {
                string fastline = "";

                systemDSList = new List<systemDS>();

                foreach (systemDS sys in dsp_L.system_L)
                {

                    if (fastline == sys.systemno)
                        continue;
                    //チェックボックスがOFFになっている場合は表示しない
    //                if (this.m_system_umu_check.Checked == false && sys.status == "無効")
    //                    continue;

                    DataRow row = system_list.NewRow();
                    row["No"] = sys.systemno;
                    row["有効"] = sys.status;
                    row["システム名"] = sys.systemname;
                    row["システム名カナ"] = sys.systemkana;
                    row["備考"] = sys.biko;
                    row["更新日時"] = sys.chk_date;
                    row["更新者"] = sys.chk_name_id;
                    system_list.Rows.Add(row);

                    fastline = sys.systemno;

                    systemDSList.Add(sys);
                }
                //件数を書き込む
                //this.m_system_count.Text = system_list.Rows.Count.ToString() + "件";

                this.m_system_List.VirtualListSize = system_list.Rows.Count;
                this.m_system_List.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            }
        }
        //拠点情報一覧の表示
        private void disp_site(DISP_dataSet dsp_L, String systemno = null)
        {
            //拠点
            this.m_site_List.VirtualMode = true;
            // １行全体選択
            //this.m_site_List.FullRowSelect = true;
            this.m_site_List.HideSelection = false;
            this.m_site_List.HeaderStyle = ColumnHeaderStyle.Clickable;

            //Hook up handlers for VirtualMode events.
            this.m_site_List.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(siteList_RetrieveVirtualItem);
            this.m_site_List.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            // Column追加
            this.m_site_List.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_site_List.Columns.Insert(1, "有効", 30, HorizontalAlignment.Left);
            this.m_site_List.Columns.Insert(2, "拠点名", 180, HorizontalAlignment.Left);
            this.m_site_List.Columns.Insert(3, "郵便番号", 50, HorizontalAlignment.Left);
            this.m_site_List.Columns.Insert(4, "住所", 50, HorizontalAlignment.Left);
            this.m_site_List.Columns.Insert(5, "TEL/FAX", 80, HorizontalAlignment.Left);
            this.m_site_List.Columns.Insert(6, "備考", 180, HorizontalAlignment.Left);
            this.m_site_List.Columns.Insert(7, "更新日時", 80, HorizontalAlignment.Left);
            this.m_site_List.Columns.Insert(8, "更新者", 80, HorizontalAlignment.Left);

            //リストビューを初期化する
            site_list = new DataTable("table4");
            site_list.Columns.Add("No", Type.GetType("System.Int32"));
            site_list.Columns.Add("有効", Type.GetType("System.String"));
            site_list.Columns.Add("拠点名", Type.GetType("System.String"));
            site_list.Columns.Add("郵便番号", Type.GetType("System.String"));
            site_list.Columns.Add("住所", Type.GetType("System.String"));
            site_list.Columns.Add("TEL/FAX", Type.GetType("System.String"));
            site_list.Columns.Add("備考", Type.GetType("System.String"));
            site_list.Columns.Add("更新日時", Type.GetType("System.String"));
            site_list.Columns.Add("更新者", Type.GetType("System.String"));


            if (dsp_L == null)
                return;
            //拠点情報
            if (dsp_L.site_L != null)
            {
                siteDSList = new List<siteDS>();

                HashSet<string> ary1 = new HashSet<string>();

                foreach (siteDS s in dsp_L.site_L)
                {
                    //チェックボックスがOFFになっている場合は表示しない
    //                if (this.m_site_umu_check.Checked == false && s.status == "無効")
    //                    continue;

                    //絞込みの時
                    if (systemno != null)
                    {
                        if (systemno == s.systemno)
                        {
                            //重複チェック
                            if (ary1.Add(s.siteno))
                            {
                                DataRow row = site_list.NewRow();
                                row["No"] = s.siteno;
                                row["有効"] = s.status;
                                row["拠点名"] = s.sitename;
                                row["郵便番号"] = s.address1;
                                row["住所"] = s.address2;
                                row["TEL/FAX"] = s.telno;
                                row["備考"] = s.biko;
                                row["更新日時"] = s.chk_date;
                                row["更新者"] = s.chk_name_id;
                                site_list.Rows.Add(row);
                                siteDSList.Add(s);
                            }
                        }
                    }
                    else
                    {
                        //重複チェック
                        if (ary1.Add(s.siteno))
                        {
                            DataRow row = site_list.NewRow();
                            row["No"] = s.siteno;
                            row["有効"] = s.status;
                            row["拠点名"] = s.sitename;
                            row["郵便番号"] = s.address1;
                            row["住所"] = s.address2;
                            row["TEL/FAX"] = s.telno;
                            row["備考"] = s.biko;
                            row["更新日時"] = s.chk_date;
                            row["更新者"] = s.chk_name_id;
                            site_list.Rows.Add(row);
                            siteDSList.Add(s);
                        }
                    }
                }
                //件数を書き込む
    //            this.m_site_count.Text = site_list.Rows.Count.ToString() + "件";

                this.m_site_List.VirtualListSize = site_list.Rows.Count;
                this.m_site_List.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            }

        }


        //ホスト
        private void disp_host(DISP_dataSet dsp_L, String siteno = null)
        {
            //機器
            this.m_host_list.VirtualMode = true;
            // １行全体選択
            //this.m_host_list.FullRowSelect = true;
            this.m_host_list.HideSelection = false;
            this.m_host_list.HeaderStyle = ColumnHeaderStyle.Clickable;
            //Hook up handlers for VirtualMode events.
            this.m_host_list.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(hostList_RetrieveVirtualItem);
            this.m_host_list.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            // Column追加
            this.m_host_list.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_host_list.Columns.Insert(1, "有効", 30, HorizontalAlignment.Left);
            this.m_host_list.Columns.Insert(2, "ホスト名", 180, HorizontalAlignment.Left);
            this.m_host_list.Columns.Insert(3, "機種", 30, HorizontalAlignment.Left);
            this.m_host_list.Columns.Insert(4, "設置場所", 80, HorizontalAlignment.Left);
            this.m_host_list.Columns.Insert(5, "用途", 180, HorizontalAlignment.Left);
            this.m_host_list.Columns.Insert(6, "装置機器ID", 180, HorizontalAlignment.Left);
            this.m_host_list.Columns.Insert(7, "監視開始日時", 180, HorizontalAlignment.Left);
            this.m_host_list.Columns.Insert(8, "監視終了日時", 180, HorizontalAlignment.Left);
            this.m_host_list.Columns.Insert(9, "保守管理番号", 180, HorizontalAlignment.Left);
            this.m_host_list.Columns.Insert(10, "保守情報", 180, HorizontalAlignment.Left);
            this.m_host_list.Columns.Insert(11, "備考", 180, HorizontalAlignment.Left);
            this.m_host_list.Columns.Insert(12, "更新日時", 80, HorizontalAlignment.Left);
            this.m_host_list.Columns.Insert(13, "更新者", 180, HorizontalAlignment.Left);

            //リストビューを初期化する
            host_list = new DataTable("table5");
            host_list.Columns.Add("No", Type.GetType("System.Int32"));
            host_list.Columns.Add("有効", Type.GetType("System.String"));
            host_list.Columns.Add("ホスト名", Type.GetType("System.String"));
            host_list.Columns.Add("機種", Type.GetType("System.String"));
            host_list.Columns.Add("設置場所", Type.GetType("System.String"));
            host_list.Columns.Add("用途", Type.GetType("System.String"));
            host_list.Columns.Add("装置機器ID", Type.GetType("System.String"));
            host_list.Columns.Add("監視開始日時", Type.GetType("System.String"));
            host_list.Columns.Add("監視終了日時", Type.GetType("System.String"));
            host_list.Columns.Add("保守管理番号", Type.GetType("System.String"));
            host_list.Columns.Add("保守情報", Type.GetType("System.String"));
            host_list.Columns.Add("備考", Type.GetType("System.String"));
            host_list.Columns.Add("更新日時", Type.GetType("System.String"));
            host_list.Columns.Add("更新者", Type.GetType("System.String"));


                if (dsp_L == null)

                    return;
            //機器情報
            if (dsp_L.host_L != null)
            {
                if (hostDSList != null) hostDSList.Clear();
                hostDSList = new List<hostDS>();

                HashSet<string> ary1 = new HashSet<string>();
                foreach (hostDS h in dsp_L.host_L)
                {
                    //チェックボックスがOFFになっている場合は表示しない
    //                if (this.m_host_umu_check.Checked == false && h.status == "無効")
    //                    continue;

                    //絞込みの時
                    if (siteno != null)
                    {
                        if (siteno == h.siteno)
                        {
                            //重複チェック
                            if (ary1.Add(h.host_no))
                            {
                                DataRow row = host_list.NewRow();
                                row["No"] = h.host_no;
                                row["有効"] = h.status;
                                row["ホスト名"] = h.hostname;
                                row["機種"] = h.device;
                                row["設置場所"] = h.location;
                                row["用途"] = h.usefor;
                                row["装置機器ID"] = h.settikikiid;
                                row["監視開始日時"] = h.kansiStartdate;
                                row["監視終了日時"] = h.kansiEndsdate;
                                row["保守管理番号"] = h.hosyukanri;
                                row["保守情報"] = h.hosyuinfo;
                                row["備考"] = h.biko;
                                row["更新日時"] = h.chk_date;
                                row["更新者"] = h.chk_name_id;
                                host_list.Rows.Add(row);
                                hostDSList.Add(h);
                            }

                        }
                    }
                    else
                    {
                        //重複チェック
                        if (ary1.Add(h.host_no))
                        {
                            DataRow row = host_list.NewRow();
                            row["No"] = h.host_no;
                            row["有効"] = h.status;
                            row["ホスト名"] = h.hostname;
                            row["機種"] = h.device;
                            row["設置場所"] = h.location;
                            row["用途"] = h.usefor;
                            row["装置機器ID"] = h.settikikiid;
                            row["監視開始日時"] = h.kansiStartdate;
                            row["監視終了日時"] = h.kansiEndsdate;
                            row["保守管理番号"] = h.hosyukanri;
                            row["保守情報"] = h.hosyuinfo;
                            row["備考"] = h.biko;
                            row["更新日時"] = h.chk_date;
                            row["更新者"] = h.chk_name_id;
                                host_list.Rows.Add(row);
                            hostDSList.Add(h);
                        }
                    }
                }
                //件数を書き込む
    //            this.m_host_count.Text = m_host_list.Rows.Count.ToString() + "件";
                this.m_host_list.VirtualListSize = host_list.Rows.Count;
                this.m_host_list.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }

        }
        //インターフェイス
        private void disp_interface(DISP_dataSet dsp_L, String siteno = null, String hostno = null)
        {
            //インターフェイス
            this.m_interface_List.VirtualMode = true;
            // １行全体選択
            //this.m_interface_List.FullRowSelect = true;
            this.m_interface_List.HideSelection = false;
            this.m_interface_List.HeaderStyle = ColumnHeaderStyle.Clickable;
            //Hook up handlers for VirtualMode events.
            this.m_interface_List.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(interfaceList_RetrieveVirtualItem);
            this.m_interface_List.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            // Column追加
            this.m_interface_List.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_interface_List.Columns.Insert(1, "有効", 30, HorizontalAlignment.Left);
            this.m_interface_List.Columns.Insert(2, "インターフェイス名", 180, HorizontalAlignment.Left);
            this.m_interface_List.Columns.Insert(3, "監視タイプ", 30, HorizontalAlignment.Left);
            this.m_interface_List.Columns.Insert(4, "監視項目名", 180, HorizontalAlignment.Left);
            this.m_interface_List.Columns.Insert(5, "閾値", 30, HorizontalAlignment.Left);
            this.m_interface_List.Columns.Insert(6, "IPアドレス", 100, HorizontalAlignment.Left);
            this.m_interface_List.Columns.Insert(7, "IPアドレス(NAT)", 100, HorizontalAlignment.Left);
            this.m_interface_List.Columns.Insert(8, "備考", 100, HorizontalAlignment.Left);
            this.m_interface_List.Columns.Insert(9, "ホスト通番", 80, HorizontalAlignment.Left);
            this.m_interface_List.Columns.Insert(10, "カスタマ通番", 80, HorizontalAlignment.Left);
            this.m_interface_List.Columns.Insert(11, "システム通番", 80, HorizontalAlignment.Left);
            this.m_interface_List.Columns.Insert(12, "拠点通番", 80, HorizontalAlignment.Left);
            this.m_interface_List.Columns.Insert(13, "更新日時", 80, HorizontalAlignment.Left);
            this.m_interface_List.Columns.Insert(14, "更新者", 80, HorizontalAlignment.Left);

            //リストビューを初期化する
            interface_list = new DataTable("table6");
            interface_list.Columns.Add("No", Type.GetType("System.String"));
            interface_list.Columns.Add("有効", Type.GetType("System.String"));
            interface_list.Columns.Add("インターフェイス名", Type.GetType("System.String"));
            interface_list.Columns.Add("監視タイプ", Type.GetType("System.String"));
            interface_list.Columns.Add("監視項目名", Type.GetType("System.String"));
            interface_list.Columns.Add("閾値", Type.GetType("System.String"));
            interface_list.Columns.Add("IPアドレス", Type.GetType("System.String"));
            interface_list.Columns.Add("IPアドレス(NAT)", Type.GetType("System.String"));
            interface_list.Columns.Add("備考", Type.GetType("System.String"));
            interface_list.Columns.Add("ホスト通番", Type.GetType("System.String"));
            interface_list.Columns.Add("カスタマ通番", Type.GetType("System.String"));
            interface_list.Columns.Add("システム通番", Type.GetType("System.String"));
            interface_list.Columns.Add("拠点通番", Type.GetType("System.String"));

            interface_list.Columns.Add("更新日時", Type.GetType("System.String"));
            interface_list.Columns.Add("更新者", Type.GetType("System.String"));
            if (dsp_L == null)
                return;
            //インターフェイス情報
            if (dsp_L.watch_L != null)
            {
                HashSet<string> ary1 = new HashSet<string>();
                int dispflg = 0;
                if (interfaceDSList != null) interfaceDSList.Clear();
                interfaceDSList = new List<watch_InterfaceDS>();

                foreach (watch_InterfaceDS w in dsp_L.watch_L)
                {
                    dispflg = 0;

                    //チェックボックスがOFFになっている場合は表示しない
    //                if (this.m_interface_umu_check.Checked == false && w.status == "無効")
    //                    continue;

                    //絞込みの時
                    if (siteno != null)
                    {

                        //拠点番号が同じなら表示
                        if (siteno == w.siteno)
                            dispflg = 1;
                    }
                    //拠点ごとではなくホストごとのとき
                    else if (hostno != null)
                    {
                        if (hostno == w.host_no)
                            dispflg = 1;
                    }
                    //普通の表示
                    else
                        dispflg = 1;


                    //表示対象
                    if (dispflg == 1)
                    {
                        //重複チェック
                        if (ary1.Add(w.watch_Interfaceno))
                        {
                            DataRow row = interface_list.NewRow();
                            row["No"] = w.watch_Interfaceno;
                            row["有効"] = w.status;
                            row["インターフェイス名"] = w.interfacename;
                            row["監視タイプ"] = w.type;
                            row["監視項目名"] = w.kanshi;
                            row["閾値"] = w.border;
                            row["IPアドレス"] = w.IPaddress;
                            row["IPアドレス(NAT)"] = w.IPaddressNAT;
                            row["備考"] = w.biko;
                            row["ホスト通番"] = w.host_no;
                            row["カスタマ通番"] = w.userno;
                            row["システム通番"] = w.systemno;
                            row["拠点通番"] = w.siteno;
                            row["更新日時"] = w.chk_date;
                            row["更新者"] = w.chk_name_id;

                            interface_list.Rows.Add(row);
                            interfaceDSList.Add(w);

                        }
                    }
                }

                //件数を書き込む
    //            this.m_interface_count.Text = interface_list.Rows.Count.ToString() + "件";

                this.m_interface_List.VirtualListSize = interface_list.Rows.Count;
                this.m_interface_List.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }
        //システム
        void systemList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (system_list.Rows.Count > 0)
            {
                //	e.Item = _item[e.ItemIndex];
                DataRow row = system_list.Rows[e.ItemIndex];
                e.Item = new ListViewItem(
                new String[]
                {
                        Convert.ToString(row[0]),
                        Convert.ToString(row[1]),
                        Convert.ToString(row[2]),
                        Convert.ToString(row[3]),
                        Convert.ToString(row[4]),
                        Convert.ToString(row[5]),
                        Convert.ToString(row[6])

                });
            }


        }
        //拠点情報
        void siteList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (site_list.Rows.Count > 0)
            {
                //	e.Item = _item[e.ItemIndex];
                DataRow row = site_list.Rows[e.ItemIndex];
                e.Item = new ListViewItem(
                new String[]
                {
                    Convert.ToString(row[0]),
                    Convert.ToString(row[1]),
                    Convert.ToString(row[2]),
                    Convert.ToString(row[3]),
                    Convert.ToString(row[4]),
                    Convert.ToString(row[5]),
                    Convert.ToString(row[6]),
                    Convert.ToString(row[7]),
                    Convert.ToString(row[8])

                });
            }
        }
        //機器情報一覧更新
        void hostList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {

            if (host_list.Rows.Count > 0)
            {
                //	e.Item = _item[e.ItemIndex];
                DataRow row = host_list.Rows[e.ItemIndex];
                e.Item = new ListViewItem(
                    new String[]
                    {
                        Convert.ToString(row[0]),
                        Convert.ToString(row[1]),
                        Convert.ToString(row[2]),
                        Convert.ToString(row[3]),
                        Convert.ToString(row[4]),
                        Convert.ToString(row[5]),
                        Convert.ToString(row[6]),
                        Convert.ToString(row[7]),
                        Convert.ToString(row[8]),
                        Convert.ToString(row[9]),
                        Convert.ToString(row[10]),
                        Convert.ToString(row[11]),
                        Convert.ToString(row[12]),
                        Convert.ToString(row[13])

                    });
            }
        }
        //インターフェイス監視一覧
        void interfaceList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (interface_list.Rows.Count > 0)
            {
                //	e.Item = _item[e.ItemIndex];
                DataRow row = this.interface_list.Rows[e.ItemIndex];
                e.Item = new ListViewItem(
                    new String[]
                    {
                    Convert.ToString(row[0]),
                    Convert.ToString(row[1]),
                    Convert.ToString(row[2]),
                    Convert.ToString(row[3]),
                    Convert.ToString(row[4]),
                    Convert.ToString(row[5]),
                    Convert.ToString(row[6]),
                    Convert.ToString(row[7]),
                    Convert.ToString(row[8]),
                    Convert.ToString(row[9]),
                    Convert.ToString(row[10]),
                    Convert.ToString(row[11]),
                    Convert.ToString(row[12]),
                    Convert.ToString(row[13]),
                    Convert.ToString(row[14])

                    });
            }
        }
        //システムリストのカラムクリック
        private void m_system_List_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.system_list == null)
                return;
            if (this.system_list.Rows.Count <= 0)
                return;
            //DataViewクラス ソートするためのクラス
            DataView dv = new DataView(system_list);

            //一時クラス
            DataTable dttmp = new DataTable();

            String strSort = "";

            //0なら昇順にソート
            if (sort_kind_system == 0)
            {
                strSort = " ASC";
                sort_kind_system = 1;
            }
            else
            {
                //１の時は昇順にソート
                strSort = " DESC";
                sort_kind_system = 0;
            }

            //コピーを作成
            dttmp = system_list.Clone();
            //ソートを実行
            dv.Sort = system_list.Columns[e.Column].ColumnName + strSort;

            // ソートされたレコードのコピー
            foreach (DataRowView drv in dv)
            {
                // 一時テーブルに格納
                dttmp.ImportRow(drv.Row);
            }
            //格納したテーブルデータを上書く
            system_list = dttmp.Copy();

            //行が存在するかチェックを行う。
            if (m_system_List.TopItem != null)
            {
                //現在一番上の行に表示されている行を取得
                int start = m_system_List.TopItem.Index;
                // ListView画面の再表示を行う
                m_system_List.RedrawItems(start, m_system_List.Items.Count - 1, true);
            }
        }
        //拠点リストのカラムクリック
        private void m_site_List_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.site_list == null)
                return;
            if (this.site_list.Rows.Count <= 0)
                return;
            //DataViewクラス ソートするためのクラス
            DataView dv = new DataView(site_list);

            //一時クラス
            DataTable dttmp = new DataTable();

            String strSort = "";

            //0なら昇順にソート
            if (sort_kind_site == 0)
            {
                strSort = " ASC";
                sort_kind_site = 1;
            }
            else
            {
                //１の時は昇順にソート
                strSort = " DESC";
                sort_kind_site = 0;
            }

            //コピーを作成
            dttmp = site_list.Clone();
            //ソートを実行
            dv.Sort = site_list.Columns[e.Column].ColumnName + strSort;

            // ソートされたレコードのコピー
            foreach (DataRowView drv in dv)
            {
                // 一時テーブルに格納
                dttmp.ImportRow(drv.Row);
            }
            //格納したテーブルデータを上書く
            site_list = dttmp.Copy();

            //行が存在するかチェックを行う。
            if (m_site_List.TopItem != null)
            {
                //現在一番上の行に表示されている行を取得
                int start = m_site_List.TopItem.Index;
                // ListView画面の再表示を行う
                m_site_List.RedrawItems(start, m_site_List.Items.Count - 1, true);
            }
        }
        //ホストリストのカラムクリック
        private void m_host_list_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.host_list == null)
                return;
            if (this.host_list.Rows.Count <= 0)
                return;
            //DataViewクラス ソートするためのクラス
            DataView dv = new DataView(host_list);

            //一時クラス
            DataTable dttmp = new DataTable();

            String strSort = "";

            //0なら昇順にソート
            if (sort_kind_host == 0)
            {
                strSort = " ASC";
                sort_kind_host = 1;
            }
            else
            {
                //１の時は昇順にソート
                strSort = " DESC";
                sort_kind_host = 0;
            }

            //コピーを作成
            dttmp = host_list.Clone();
            //ソートを実行
            dv.Sort = host_list.Columns[e.Column].ColumnName + strSort;

            // ソートされたレコードのコピー
            foreach (DataRowView drv in dv)
            {
                // 一時テーブルに格納
                dttmp.ImportRow(drv.Row);
            }
            //格納したテーブルデータを上書く
            host_list = dttmp.Copy();

            //行が存在するかチェックを行う。
            if (this.m_host_list.TopItem != null)
            {
                //現在一番上の行に表示されている行を取得
                int start = m_host_list.TopItem.Index;
                // ListView画面の再表示を行う
                m_host_list.RedrawItems(start, m_host_list.Items.Count - 1, true);
            }
        }
        //インターフェイスリストのカラムクリック
        private void m_interface_List_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.interface_list == null)
                return;
            if (this.interface_list.Rows.Count <= 0)
                return;
            //DataViewクラス ソートするためのクラス
            DataView dv = new DataView(interface_list);

            //一時クラス
            DataTable dttmp = new DataTable();

            String strSort = "";

            //0なら昇順にソート
            if (sort_kind_interface == 0)
            {
                strSort = " ASC";
                sort_kind_interface = 1;
            }
            else
            {
                //１の時は昇順にソート
                strSort = " DESC";
                sort_kind_interface = 0;
            }

            //コピーを作成
            dttmp = interface_list.Clone();
            //ソートを実行
            dv.Sort = interface_list.Columns[e.Column].ColumnName + strSort;

            // ソートされたレコードのコピー
            foreach (DataRowView drv in dv)
            {
                // 一時テーブルに格納
                dttmp.ImportRow(drv.Row);
            }
            //格納したテーブルデータを上書く
            interface_list = dttmp.Copy();

            //行が存在するかチェックを行う。
            if (this.m_interface_List.TopItem != null)
            {
                //現在一番上の行に表示されている行を取得
                int start = m_interface_List.TopItem.Index;
                // ListView画面の再表示を行う
                m_interface_List.RedrawItems(start, m_interface_List.Items.Count - 1, true);
            }
        }
        //キャンセルボタン
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
