using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMSサンプル
{
    public partial class Form_MainList : Form
    {
        NpgsqlConnection con;

        //ログイン情報
        opeDS loginDS;

        //カスタマ表示用のデータテーブル
        DataTable user_list;

        //システム情報表示用
        DataTable system_list;

        //拠点情報表示用   
        DataTable site_list;
        //機器情報表示用
        DataTable host_list;
        //インターフェイス情報表示用
        DataTable interface_list;
        //回線情報表示用
        DataTable kasen_list;

        //作業情報
        DataTable teiki_sagyo_list;

        //作業情報
        DataTable keikaku_sagyo_list;

        //特別対応
        DataTable tokubetu_sagyo_list;

        //インシデント情報
        DataTable incident_List;

        private Class_ListViewColumnSorter _columnSorter;

        //
        private int sort_kind = 0;
        private int sort_kind_site = 0;
        private int sort_kind_system = 0;
        private int sort_kind_host = 0;
        private int sort_kind_interface = 0;
        private int sort_kind_kaisen = 0;
        private int sort_kind_incident = 0;
        private int sort_kind_teiki = 0;
        private int sort_kind_keikaku = 0;
        private int sort_kind_tokubetu = 0;

        DISP_dataSet dsp_L;
        List<userDS> userDSList;
        List<systemDS> systemDSList;
        List<siteDS> siteDSList;
        List<hostDS> hostDSList;
        List<watch_InterfaceDS> interfaceDSList;

        List<scheduleDS> schDSList;
        List<scheduleDS> scheduleList_keikaku;
        List<scheduleDS> scheduleList_teiki;
        List<scheduleDS> scheduleList_tokubetu;

        List<incidentDS> incidentDSList;

        //タイマーフォーム
        Form_alermlist alermdlg;

        public int soundidx = 0;


        public Form_MainList()
        {
            InitializeComponent();
        }
        //表示前処理
        private void Form_MainList_Load(object sender, EventArgs e)
        {

            //製造元
            System.Reflection.AssemblyCopyrightAttribute asmcpy =
                (System.Reflection.AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(System.Reflection.Assembly.GetExecutingAssembly(),
                typeof(System.Reflection.AssemblyCopyrightAttribute));
            toolStripStatusLabel1.Text = asmcpy.Copyright;


            Form_login login = new Form_login();

            login.ShowDialog();
            //ログイン失敗時
            if (login.ret_value == -1)
                this.Close();

            _columnSorter = new Class_ListViewColumnSorter();
            userList.ListViewItemSorter = _columnSorter;
            loginDS = login.opeData;
            try
            {
                //ログイン
                m_opename.Text = loginDS.lastname + loginDS.fastname;

                Class_common common = new Class_common();
                con = common.DB_connection();

                //ツリービューの再表示
                RefreshTreeView();

                //インシデント一覧データを取得
                Class_Detaget dg_class = new Class_Detaget();
                //List<incidentDS> incidentList ;
                incidentDSList = dg_class.getOpenIncident(con);
                //インシデント一覧を表示
                disp_Incident(incidentDSList);
                disp_sagyoList(schDSList);
                timer1.Start();


                //カスタマ名コンボボックスの設定
                combo_set();

                //いったんDBをクローズ
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("表示エラー" + ex.Message, "表示エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //検索コンボボックスを設定する
        private void combo_set()
        {

            //カスタマ名コンボボックスの設定
            List<userDS> userDSListdisp = new List<userDS>();

            userDS aa = new userDS();
            aa.username = "";
            aa.userno = "";
            userDSListdisp.Add(aa);
            userDSListdisp.AddRange(userDSList);

            //データテーブルを割り当てる
            m_usernameCombo.DataSource = userDSListdisp;
            m_usernameCombo.DisplayMember = "username";
            m_usernameCombo.ValueMember = "userno";
        }

        // TreeViewコントロールのデータを更新します。
        private void RefreshTreeView()
        {
            treeView1.Nodes.Clear();
            treeView1.ImageList = this.imageList1;
            treeView1.ImageIndex = 0;
            treeView1.SelectedImageIndex = 0;
            if (schDSList != null) schDSList.Clear();

            //下から順に取りにいく
            Class_Detaget getuser = new Class_Detaget();
            getuser.con = con;
            userDSList = getuser.getUserList();
            //カスタマ情報を読み込む
            foreach (userDS v in userDSList)
            {
                TreeNode NodeUser = new TreeNode(v.username, 0, 0);
                NodeUser.ToolTipText = v.userno;

                //システム名を取得
                List<systemDS> systemDSListsub = getuser.getSystemList(v.userno, true);

                //リストに入れる
                if (systemDSList != null)
                    systemDSList.AddRange(systemDSListsub);
                else
                    systemDSList = systemDSListsub;

                //システム名を表示
                foreach (systemDS s in systemDSListsub)
                {
                    TreeNode NodeSystem = new TreeNode(s.systemname, 1, 1);
                    NodeSystem.ToolTipText = s.systemno;

                    TreeNode NodeTimer1 = new TreeNode("インシデント", 2, 2);
                    NodeTimer1.ToolTipText = "1";
                    NodeSystem.Nodes.Add(NodeTimer1);
                    TreeNode NodeTimer2 = new TreeNode("定期業務", 2, 2);
                    NodeTimer2.ToolTipText = "2";
                    NodeSystem.Nodes.Add(NodeTimer2);
                    TreeNode NodeTimer3 = new TreeNode("計画作業", 2, 2);
                    NodeTimer3.ToolTipText = "3";
                    NodeSystem.Nodes.Add(NodeTimer3);

                    TreeNode NodeTimer4 = new TreeNode("特別対応", 2, 2);
                    NodeTimer4.ToolTipText = "4";
                    NodeSystem.Nodes.Add(NodeTimer4);

                    Dictionary<string, string> param_dict = new Dictionary<string, string>();


                    param_dict["userno"] = s.userno;
                    param_dict["systemno"] = s.systemno;

                    //タイマー名
                    List<scheduleDS> timerDSListsub = getuser.getTimerList(param_dict, con);

                    //リストに入れる
                    if (schDSList != null)
                        schDSList.AddRange(timerDSListsub);
                    else
                        schDSList = timerDSListsub;

                    // 現在時刻
                    DateTime nowdate = DateTime.Now;

                    foreach (scheduleDS si in timerDSListsub)
                    {
                        TreeNode NodeTimerDetail;
                        DateTime dt1 = DateTime.Parse(si.end_date);

                        //有効でかつ期間内
                        if (si.status == "未完了" && si.start_date != "" && dt1 > nowdate)
                        {
                            NodeTimerDetail = new TreeNode(si.timer_name, 6, 6);
                            NodeTimerDetail.ToolTipText = si.schedule_no;

                            NodeUser.ForeColor = Color.Red;
                            NodeTimerDetail.ForeColor = Color.Red;

                            //インシデントの時
                            if (si.schedule_type == "1")
                            {
                                NodeTimerDetail.Text += "  " + si.incident_no;
                                NodeTimer1.Parent.ForeColor = Color.Red;
                                NodeTimer1.ForeColor = Color.Red;
                                NodeTimer1.Nodes.Add(NodeTimerDetail);
                                scheduleDS ds = new scheduleDS();

                            }
                            //計画作業
                            else if (si.schedule_type == "2")
                            {
                                NodeTimer2.Parent.ForeColor = Color.Red;
                                NodeTimer2.ForeColor = Color.Red;
                                NodeTimer2.Nodes.Add(NodeTimerDetail);
                            }
                            //定期業務
                            else if (si.schedule_type == "3")
                            {

                                NodeTimer3.Parent.ForeColor = Color.Red;
                                NodeTimer3.ForeColor = Color.Red;
                                NodeTimer3.Nodes.Add(NodeTimerDetail);
                            }
                            //特別対応
                            else if (si.schedule_type == "4")
                            {

                                NodeTimer4.Parent.ForeColor = Color.Red;
                                NodeTimer4.ForeColor = Color.Red;
                                NodeTimer4.Nodes.Add(NodeTimerDetail);
                            }
                        }
                        else
                        {
                            NodeTimerDetail = new TreeNode(si.timer_name, 5, 5);
                            NodeTimerDetail.ToolTipText = si.schedule_no;

                            //インシデントの時
                            if (si.schedule_type == "1")
                            {
                                NodeTimer1.Nodes.Add(NodeTimerDetail);
                            }
                            //計画作業
                            else if (si.schedule_type == "2")
                            {
                                NodeTimer2.Nodes.Add(NodeTimerDetail);
                            }
                            //定期業務
                            else if (si.schedule_type == "3")
                            {
                                NodeTimer3.Nodes.Add(NodeTimerDetail);
                            }
                            //特別対応
                            else if (si.schedule_type == "4")
                            {
                                NodeTimer4.Nodes.Add(NodeTimerDetail);
                            }
                        }
                    }

                    NodeUser.Nodes.Add(NodeSystem);
                }

                // 最上位階層に対してまとめて項目（ノード）を追加
                treeView1.Nodes.Add(NodeUser);

                //一覧表のリストに表示

                /*            TreeNode treeNodeFruits = new TreeNode("システム1");
                            TreeNode treeNodeVegetables = new TreeNode("システム2");
                            TreeNode[] treeNodeSubFolder =
                                { treeNodeFruits, treeNodeVegetables};

                            // 下位階層に対してまとめて項目（ノード）を追加
                            TreeNode treeNodeFood =
                                new TreeNode("カスタマ1", treeNodeSubFolder);

                            TreeNode treeNodeDrink = new TreeNode("カスタマ2");
                            TreeNode[] treeNodeRoot = { treeNodeFood, treeNodeDrink };

                            // 最上位階層に対してまとめて項目（ノード）を追加
                            treeView1.Nodes.AddRange(treeNodeRoot);

                            treeView1.TopNode.Expand();
                */
            }


        }
        void userList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (user_list.Rows.Count > 0)
            {
                //	e.Item = _item[e.ItemIndex];
                DataRow row = user_list.Rows[e.ItemIndex];
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
                        Convert.ToString(row[5])

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
                    Convert.ToString(row[14]),
                    Convert.ToString(row[15])

                    });
            }
        }

        //回線情報一覧
        void kaisenList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            //	e.Item = _item[e.ItemIndex];
            if (kasen_list.Rows.Count > 0)
            {
                DataRow row = this.kasen_list.Rows[e.ItemIndex];
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
        //インシデント一覧
        void incidentList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            //	e.Item = _item[e.ItemIndex];
            if (this.incident_List.Rows.Count > 0)
            {
                DataRow row = this.incident_List.Rows[e.ItemIndex];
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
                Convert.ToString(row[14]),
                Convert.ToString(row[15]),
                Convert.ToString(row[16]),
                Convert.ToString(row[17]),
                Convert.ToString(row[18]),
                Convert.ToString(row[19])
                    });
            }

        }

        //定期予定
        void teiki_sagyoList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            //	e.Item = _item[e.ItemIndex];
            if (teiki_sagyo_list.Rows.Count > 0)
            {

                DataRow row = this.teiki_sagyo_list.Rows[e.ItemIndex];
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
                Convert.ToString(row[11])
                    });
            }

        }
        //計画作業
        void keikaku_sagyoList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            //	e.Item = _item[e.ItemIndex];
            if (keikaku_sagyo_list.Rows.Count > 0)
            {

                DataRow row = this.keikaku_sagyo_list.Rows[e.ItemIndex];
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
                Convert.ToString(row[11])
                    });
            }

        }
        //特別対応
        void tokubetu_sagyoList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            //	e.Item = _item[e.ItemIndex];
            if (tokubetu_sagyo_list.Rows.Count > 0)
            {

                DataRow row = this.tokubetu_sagyo_list.Rows[e.ItemIndex];
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
                Convert.ToString(row[11])
                    });
            }

        }
        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void userList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //カスタマ登録画面
        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form_UserInsert useraddfrom = new Form_UserInsert();
            if (userDSList != null)
                useraddfrom.userList = userDSList;
            useraddfrom.con = con;
            useraddfrom.loginDS = loginDS;
            useraddfrom.Show();

        }
        //管理メニューの表示
        private void m_tourokuBtn_Click_1(object sender, EventArgs e)
        {

            Form_kanri_menu kanrimenu = new Form_kanri_menu();
            kanrimenu.con = con;
            if (userDSList != null)
                kanrimenu.userList = userDSList;
            if (systemDSList != null)
                kanrimenu.systemList = systemDSList;
            if (siteDSList != null)
                kanrimenu.siteList = siteDSList;
            kanrimenu.loginDS = loginDS;
            kanrimenu.Show();
        }


        //システム情報登録
        private void m_system_label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form_systemInsert systemdlg = new Form_systemInsert();
            systemdlg.loginDS = loginDS;
            systemdlg.con = con;
            if (userDSList != null)
                systemdlg.userList = userDSList;

            systemdlg.Show();
        }

        //拠点登録
        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {

            Form_siteInsert sitefm = new Form_siteInsert();
            sitefm.loginDS = loginDS;
            sitefm.con = con;
            if (userDSList != null)
                sitefm.userList = userDSList;
            if (systemDSList != null)
                sitefm.systemList = systemDSList;

            sitefm.Show();
        }
        //ホスト登録
        private void linkLabel4_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form_hostInsert hostfm = new Form_hostInsert();
            hostfm.loginDS = loginDS;
            hostfm.con = con;
            if (userDSList != null)
                hostfm.userList = userDSList;
            if (systemDSList != null)
                hostfm.systemList = systemDSList;

            hostfm.Show();
        }

        //回線情報登録
        private void linkLabel6_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form_kaisenInsert kaisenfm = new Form_kaisenInsert();
            kaisenfm.loginDS = loginDS;
            kaisenfm.con = con;
            if (userDSList != null)
                kaisenfm.userList = userDSList;
            if (systemDSList != null)
                kaisenfm.systemList = systemDSList;

            kaisenfm.Show();
        }
        //監視インターフェイス
        private void linkLabel5_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {

            Form_kanshiInterfaceInsert kanshiInt = new Form_kanshiInterfaceInsert();
            kanshiInt.loginDS = loginDS;
            kanshiInt.con = con;
            if (userDSList != null)
                kanshiInt.userList = userDSList;
            if (systemDSList != null)
                kanshiInt.systemList = systemDSList;


            kanshiInt.Show();

        }
        //インシデント登録
        private void m_incidentTouroku_Click(object sender, EventArgs e)
        {


            Form_IncidentInsert incidentfm = new Form_IncidentInsert();
            incidentfm.loginDS = loginDS;

            if (userDSList != null)
                incidentfm.userList = userDSList;
            if (systemDSList != null)
                incidentfm.systemList = systemDSList;
            if (siteDSList != null)
                incidentfm.siteList = siteDSList;
            incidentfm.con = con;
            incidentfm.Show();
        }
        //作業情報登録
        private void linkLabel7_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form_sagyoInsert sagyofm = new Form_sagyoInsert();
            sagyofm.loginDS = loginDS;
            if (userDSList != null)
                sagyofm.userList = userDSList;

            sagyofm.Show();

        }
        private void disp_sagyoList(List<scheduleDS> scheduleList)
        {
            scheduleList_keikaku = new List<scheduleDS>();
            scheduleList_teiki = new List<scheduleDS>();
            scheduleList_tokubetu = new List<scheduleDS>();

            //
            foreach (scheduleDS schedata in scheduleList)
            {
                //1:インシデント処理 2:定期 3:作業 4:特別 5:サブタスク
                if (schedata.schedule_type == "2")
                    scheduleList_teiki.Add(schedata);
                else if (schedata.schedule_type == "3")
                    scheduleList_keikaku.Add(schedata);
                else if (schedata.schedule_type == "4")
                    scheduleList_tokubetu.Add(schedata);

            }
            disp_teiki_list();
            disp_scheduleList_keikaku();
            disp_tokubetu_list();
        }
        //定期作業の表示
        private void disp_teiki_list()
        {

            this.m_teiki_List.VirtualMode = true;
            // １行全体選択
            this.m_teiki_List.FullRowSelect = true;
            this.m_teiki_List.HideSelection = false;
            this.m_teiki_List.HeaderStyle = ColumnHeaderStyle.Clickable;
            //Hook up handlers for VirtualMode events.
            this.m_teiki_List.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(teiki_sagyoList_RetrieveVirtualItem);
            this.m_teiki_List.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.m_teiki_List.Scrollable = true;

            // Column追加
            this.m_teiki_List.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_teiki_List.Columns.Insert(1, "完了", 30, HorizontalAlignment.Left);
            this.m_teiki_List.Columns.Insert(2, "カスタマ", 50, HorizontalAlignment.Left);
            this.m_teiki_List.Columns.Insert(3, "システム", 50, HorizontalAlignment.Left);
            this.m_teiki_List.Columns.Insert(4, "拠点", 50, HorizontalAlignment.Left);

            this.m_teiki_List.Columns.Insert(5, "タイマー名", 180, HorizontalAlignment.Left);
            this.m_teiki_List.Columns.Insert(6, "予定区分", 110, HorizontalAlignment.Left);
            this.m_teiki_List.Columns.Insert(7, "開始日時", 110, HorizontalAlignment.Left);
            this.m_teiki_List.Columns.Insert(8, "終了日時", 110, HorizontalAlignment.Left);
            this.m_teiki_List.Columns.Insert(9, "メッセージ", 180, HorizontalAlignment.Left);
            this.m_teiki_List.Columns.Insert(10, "更新日時", 80, HorizontalAlignment.Left);
            this.m_teiki_List.Columns.Insert(11, "更新者", 80, HorizontalAlignment.Left);
            //リストビューを初期化する
            teiki_sagyo_list = new DataTable("table12");
            teiki_sagyo_list.Columns.Add("No", Type.GetType("System.Int32"));
            teiki_sagyo_list.Columns.Add("完了", Type.GetType("System.String"));
            teiki_sagyo_list.Columns.Add("カスタマ", Type.GetType("System.String"));
            teiki_sagyo_list.Columns.Add("システム", Type.GetType("System.String"));
            teiki_sagyo_list.Columns.Add("拠点", Type.GetType("System.String"));
            teiki_sagyo_list.Columns.Add("タイマー名", Type.GetType("System.String"));
            teiki_sagyo_list.Columns.Add("予定区分", Type.GetType("System.String"));
            teiki_sagyo_list.Columns.Add("開始日時", Type.GetType("System.String"));
            teiki_sagyo_list.Columns.Add("終了日時", Type.GetType("System.String"));
            teiki_sagyo_list.Columns.Add("メッセージ", Type.GetType("System.String"));
            teiki_sagyo_list.Columns.Add("更新日時", Type.GetType("System.String"));
            teiki_sagyo_list.Columns.Add("更新者", Type.GetType("System.String"));


            //データの挿入
            if (scheduleList_teiki != null)
            {
                string fastline = "";
                foreach (scheduleDS v in scheduleList_teiki)
                {

                    //if (fastline == v.incident_no)
                    //    continue;

                    DataRow urow = teiki_sagyo_list.NewRow();

                    urow["No"] = v.schedule_no;
                    urow["完了"] = v.status;
                    urow["カスタマ"] = v.username;
                    urow["システム"] = v.systemname;
                    urow["拠点"] = v.sitename;
                    urow["タイマー名"] = v.timer_name;
                    //1:インシデント処理 2:定期作業業務促し 3:計画作業 4:特別作業
                    if (v.schedule_type == "1")
                        urow["予定区分"] = "インシデント処理";
                    else if (v.schedule_type == "2")
                        urow["予定区分"] = "定期作業";
                    else if (v.schedule_type == "3")
                        urow["予定区分"] = "計画作業";
                    else if (v.schedule_type == "4")
                        urow["予定区分"] = "特別対応";


                    urow["開始日時"] = v.start_date;
                    urow["終了日時"] = v.end_date;
                    urow["メッセージ"] = v.alerm_message;
                    urow["更新日時"] = v.chk_date;
                    urow["更新者"] = v.chk_name_id;
                    teiki_sagyo_list.Rows.Add(urow);

                    fastline = v.incident_no;
                }

                this.m_teiki_List.VirtualListSize = teiki_sagyo_list.Rows.Count;
                this.m_teiki_List.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            }

        }
        //計画作業の表示
        private void disp_scheduleList_keikaku()
        {

            this.m_keikaku_list.VirtualMode = true;
            // １行全体選択
            this.m_keikaku_list.FullRowSelect = true;
            this.m_keikaku_list.HideSelection = false;
            this.m_keikaku_list.HeaderStyle = ColumnHeaderStyle.Clickable;
            this.m_keikaku_list.CheckBoxes = true;
            //Hook up handlers for VirtualMode events.
            this.m_keikaku_list.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(keikaku_sagyoList_RetrieveVirtualItem);
            this.m_keikaku_list.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.m_keikaku_list.Scrollable = true;

            // Column追加
            this.m_keikaku_list.Columns.Insert(0, "No", 80, HorizontalAlignment.Right);
            this.m_keikaku_list.Columns.Insert(1, "完了", 30, HorizontalAlignment.Left);
            this.m_keikaku_list.Columns.Insert(2, "カスタマ", 50, HorizontalAlignment.Left);
            this.m_keikaku_list.Columns.Insert(3, "システム", 50, HorizontalAlignment.Left);
            this.m_keikaku_list.Columns.Insert(4, "拠点", 50, HorizontalAlignment.Left);

            this.m_keikaku_list.Columns.Insert(5, "タイマー名", 180, HorizontalAlignment.Left);
            this.m_keikaku_list.Columns.Insert(6, "予定区分", 110, HorizontalAlignment.Left);
            this.m_keikaku_list.Columns.Insert(7, "開始日時", 110, HorizontalAlignment.Left);
            this.m_keikaku_list.Columns.Insert(8, "終了日時", 110, HorizontalAlignment.Left);
            this.m_keikaku_list.Columns.Insert(9, "メッセージ", 180, HorizontalAlignment.Left);
            this.m_keikaku_list.Columns.Insert(10, "更新日時", 80, HorizontalAlignment.Left);
            this.m_keikaku_list.Columns.Insert(11, "更新者", 80, HorizontalAlignment.Left);

            //リストビューを初期化する
            keikaku_sagyo_list = new DataTable("table12");
            keikaku_sagyo_list.Columns.Add("No", Type.GetType("System.Int32"));
            keikaku_sagyo_list.Columns.Add("完了", Type.GetType("System.String"));
            keikaku_sagyo_list.Columns.Add("カスタマ", Type.GetType("System.String"));
            keikaku_sagyo_list.Columns.Add("システム", Type.GetType("System.String"));
            keikaku_sagyo_list.Columns.Add("拠点", Type.GetType("System.String"));
            keikaku_sagyo_list.Columns.Add("タイマー名", Type.GetType("System.String"));
            keikaku_sagyo_list.Columns.Add("予定区分", Type.GetType("System.String"));
            keikaku_sagyo_list.Columns.Add("開始日時", Type.GetType("System.String"));
            keikaku_sagyo_list.Columns.Add("終了日時", Type.GetType("System.String"));
            keikaku_sagyo_list.Columns.Add("メッセージ", Type.GetType("System.String"));
            keikaku_sagyo_list.Columns.Add("更新日時", Type.GetType("System.String"));
            keikaku_sagyo_list.Columns.Add("更新者", Type.GetType("System.String"));


            //データの挿入
            if (scheduleList_keikaku != null)
            {
                string fastline = "";
                foreach (scheduleDS v in scheduleList_keikaku)
                {

                    //if (fastline == v.incident_no)
                    //    continue;

                    DataRow urow = keikaku_sagyo_list.NewRow();

                    urow["No"] = v.schedule_no;
                    urow["完了"] = v.status;
                    urow["カスタマ"] = v.username;
                    urow["システム"] = v.systemname;
                    urow["拠点"] = v.sitename;
                    urow["タイマー名"] = v.timer_name;
                    //1:インシデント処理 2:定期作業業務促し 3:計画作業 4:特別作業
                    if (v.schedule_type == "1")
                        urow["予定区分"] = "インシデント処理";
                    else if (v.schedule_type == "2")
                        urow["予定区分"] = "定期作業";
                    else if (v.schedule_type == "3")
                        urow["予定区分"] = "計画作業";
                    else if (v.schedule_type == "4")
                        urow["予定区分"] = "特別対応";


                    urow["開始日時"] = v.start_date;
                    urow["終了日時"] = v.end_date;
                    urow["メッセージ"] = v.alerm_message;
                    urow["更新日時"] = v.chk_date;
                    urow["更新者"] = v.chk_name_id;
                    keikaku_sagyo_list.Rows.Add(urow);

                    fastline = v.incident_no;

                }

                this.m_keikaku_list.VirtualListSize = keikaku_sagyo_list.Rows.Count;
                this.m_keikaku_list.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }

        }
        //インシデント情報の表示
        private void disp_Incident(List<incidentDS> incidentList)
        {
            this.m_incident_List.VirtualMode = true;
            // １行全体選択
            this.m_incident_List.FullRowSelect = true;
            this.m_incident_List.HideSelection = false;
            this.m_incident_List.HeaderStyle = ColumnHeaderStyle.Clickable;
            //Hook up handlers for VirtualMode events.
            this.m_incident_List.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(incidentList_RetrieveVirtualItem);
            this.m_incident_List.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.m_incident_List.Scrollable = true;

            // Column追加
            this.m_incident_List.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(1, "完了", 30, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(2, "カスタマ", 180, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(3, "システム", 180, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(4, "拠点", 180, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(5, "ホスト", 180, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(6, "MPMSインシデント番号", 180, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(7, "S-cude事例ID", 30, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(8, "インシデント区分", 30, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(9, "インシデント内容(タイトル)", 80, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(10, "MAT対応", 180, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(11, "MAT対応コマンド", 180, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(12, "受付日時", 30, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(13, "手配日時", 30, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(14, "復旧日時", 80, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(15, "完了日時", 180, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(16, "担当者番号", 180, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(17, "オペレータID", 180, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(18, "更新日時", 80, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(19, "更新者", 80, HorizontalAlignment.Left);

            //リストビューを初期化する
            incident_List = new DataTable("table11");
            incident_List.Columns.Add("No", Type.GetType("System.Int32"));
            incident_List.Columns.Add("完了", Type.GetType("System.String"));
            incident_List.Columns.Add("カスタマ", Type.GetType("System.String"));
            incident_List.Columns.Add("システム", Type.GetType("System.String"));
            incident_List.Columns.Add("拠点", Type.GetType("System.String"));
            incident_List.Columns.Add("ホスト", Type.GetType("System.String"));
            incident_List.Columns.Add("MPMSインシデント番号", Type.GetType("System.String"));
            incident_List.Columns.Add("S-cude事例ID", Type.GetType("System.String"));
            incident_List.Columns.Add("インシデント区分", Type.GetType("System.String"));
            incident_List.Columns.Add("インシデント内容(タイトル)", Type.GetType("System.String"));
            incident_List.Columns.Add("MAT対応", Type.GetType("System.String"));
            incident_List.Columns.Add("MAT対応コマンド", Type.GetType("System.String"));
            incident_List.Columns.Add("受付日時", Type.GetType("System.String"));
            incident_List.Columns.Add("手配日時", Type.GetType("System.String"));
            incident_List.Columns.Add("復旧日時", Type.GetType("System.String"));
            incident_List.Columns.Add("完了日時", Type.GetType("System.String"));
            incident_List.Columns.Add("担当者番号", Type.GetType("System.String"));
            incident_List.Columns.Add("オペレータID", Type.GetType("System.String"));
            incident_List.Columns.Add("更新日時", Type.GetType("System.String"));
            incident_List.Columns.Add("更新者", Type.GetType("System.String"));


            //データの挿入
            if (incidentList != null)
            {
                string fastline = "";
                foreach (incidentDS v in incidentList)
                {

                    if (fastline == v.incident_no)
                        continue;

                    DataRow urow = incident_List.NewRow();

                    urow["No"] = v.incident_no;
                    urow["完了"] = v.status;
                    urow["カスタマ"] = v.username;
                    urow["システム"] = v.systemname;
                    urow["拠点"] = v.sitename;
                    urow["ホスト"] = v.hostname;
                    urow["MPMSインシデント番号"] = v.mpms_incident;
                    urow["S-cude事例ID"] = v.s_cube_id;
                    //1:アラーム検知 2:障害申告 3:問い合わせ
                    if (v.incident_type == "1")
                    {
                        urow["インシデント区分"] = "アラーム検知";
                    }
                    else if (v.incident_type == "2")
                    {
                        urow["インシデント区分"] = "障害申告";
                    }
                    else if (v.incident_type == "3")
                    {
                        urow["インシデント区分"] = "問い合わせ";
                    }

                    urow["インシデント内容(タイトル)"] = v.content;

                    if (v.matflg == "1")
                        urow["MAT対応"] = "あり";
                    if (v.matflg == "0")
                        urow["MAT対応"] = "なし";

                    urow["MAT対応コマンド"] = v.matcommand;
                    urow["受付日時"] = v.uketukedate;
                    urow["手配日時"] = v.tehaidate;
                    urow["復旧日時"] = v.fukyudate;
                    urow["完了日時"] = v.enddate;
                    urow["担当者番号"] = v.user_tantou_no;
                    urow["オペレータID"] = v.opename;
                    urow["更新日時"] = v.chk_date;
                    urow["更新者"] = v.chk_name_id;
                    incident_List.Rows.Add(urow);

                    fastline = v.incident_no;

                }
                this.m_incident_List.VirtualListSize = incident_List.Rows.Count;
                this.m_incident_List.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        //特別対応の表示
        private void disp_tokubetu_list()
        {

            this.m_tokubetu_list.VirtualMode = true;
            // １行全体選択
            this.m_tokubetu_list.FullRowSelect = true;
            this.m_tokubetu_list.HideSelection = false;
            this.m_tokubetu_list.HeaderStyle = ColumnHeaderStyle.Clickable;
            //Hook up handlers for VirtualMode events.
            this.m_tokubetu_list.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(tokubetu_sagyoList_RetrieveVirtualItem);
            this.m_tokubetu_list.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.m_tokubetu_list.Scrollable = true;

            // Column追加
            this.m_tokubetu_list.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_tokubetu_list.Columns.Insert(1, "完了", 30, HorizontalAlignment.Left);
            this.m_tokubetu_list.Columns.Insert(2, "カスタマ", 50, HorizontalAlignment.Left);
            this.m_tokubetu_list.Columns.Insert(3, "システム", 50, HorizontalAlignment.Left);
            this.m_tokubetu_list.Columns.Insert(4, "拠点", 50, HorizontalAlignment.Left);
            this.m_tokubetu_list.Columns.Insert(5, "タイマー名", 180, HorizontalAlignment.Left);
            this.m_tokubetu_list.Columns.Insert(6, "予定区分", 110, HorizontalAlignment.Left);
            this.m_tokubetu_list.Columns.Insert(7, "開始日時", 110, HorizontalAlignment.Left);
            this.m_tokubetu_list.Columns.Insert(8, "終了日時", 110, HorizontalAlignment.Left);
            this.m_tokubetu_list.Columns.Insert(9, "メッセージ", 180, HorizontalAlignment.Left);
            this.m_tokubetu_list.Columns.Insert(10, "更新日時", 80, HorizontalAlignment.Left);
            this.m_tokubetu_list.Columns.Insert(11, "更新者", 80, HorizontalAlignment.Left);
            //リストビューを初期化する
            tokubetu_sagyo_list = new DataTable("table20");
            tokubetu_sagyo_list.Columns.Add("No", Type.GetType("System.Int32"));
            tokubetu_sagyo_list.Columns.Add("完了", Type.GetType("System.String"));
            tokubetu_sagyo_list.Columns.Add("カスタマ", Type.GetType("System.String"));
            tokubetu_sagyo_list.Columns.Add("システム", Type.GetType("System.String"));
            tokubetu_sagyo_list.Columns.Add("拠点", Type.GetType("System.String"));
            tokubetu_sagyo_list.Columns.Add("タイマー名", Type.GetType("System.String"));
            tokubetu_sagyo_list.Columns.Add("予定区分", Type.GetType("System.String"));
            tokubetu_sagyo_list.Columns.Add("開始日時", Type.GetType("System.String"));
            tokubetu_sagyo_list.Columns.Add("終了日時", Type.GetType("System.String"));
            tokubetu_sagyo_list.Columns.Add("メッセージ", Type.GetType("System.String"));
            tokubetu_sagyo_list.Columns.Add("更新日時", Type.GetType("System.String"));
            tokubetu_sagyo_list.Columns.Add("更新者", Type.GetType("System.String"));


            //データの挿入
            if (scheduleList_tokubetu != null)
            {
                string fastline = "";
                foreach (scheduleDS v in scheduleList_tokubetu)
                {

                    //if (fastline == v.incident_no)
                    DataRow urow = tokubetu_sagyo_list.NewRow();
                    //    continue;


                    urow["No"] = v.schedule_no;
                    urow["完了"] = v.status;
                    urow["カスタマ"] = v.username;
                    urow["システム"] = v.systemname;
                    urow["拠点"] = v.systemname;
                    urow["タイマー名"] = v.timer_name;
                    //1:インシデント処理 2:定期作業業務促し 3:計画作業 4:特別作業
                    if (v.schedule_type == "1")
                        urow["予定区分"] = "インシデント処理";
                    else if (v.schedule_type == "2")
                        urow["予定区分"] = "定期作業";
                    else if (v.schedule_type == "3")
                        urow["予定区分"] = "計画作業";
                    else if (v.schedule_type == "4")
                        urow["予定区分"] = "特別対応";
                    urow["開始日時"] = v.start_date;
                    urow["終了日時"] = v.end_date;
                    urow["メッセージ"] = v.alerm_message;
                    urow["更新日時"] = v.chk_date;
                    urow["更新者"] = v.chk_name_id;
                    tokubetu_sagyo_list.Rows.Add(urow);

                    fastline = v.incident_no;
                }

                this.m_tokubetu_list.VirtualListSize = tokubetu_sagyo_list.Rows.Count;
                this.m_tokubetu_list.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            }
        }

        //カスタマ一覧の表示
        private void disp_User(DISP_dataSet dsp_L)
        {
            this.userList.VirtualMode = true;
            // １行全体選択
            this.userList.FullRowSelect = true;
            this.userList.HideSelection = false;
            this.userList.HeaderStyle = ColumnHeaderStyle.Clickable;
            //Hook up handlers for VirtualMode events.
            this.userList.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(userList_RetrieveVirtualItem);
            this.userList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.userList.Scrollable = true;

            // Column追加
            this.userList.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.userList.Columns.Insert(1, "有効", 30, HorizontalAlignment.Left);
            this.userList.Columns.Insert(2, "カスタマ名", 180, HorizontalAlignment.Left);
            this.userList.Columns.Insert(3, "カスタマ名カナ", 30, HorizontalAlignment.Left);
            this.userList.Columns.Insert(4, "カスタマ名略称", 30, HorizontalAlignment.Left);
            this.userList.Columns.Insert(5, "レポート出力有無", 80, HorizontalAlignment.Left);
            this.userList.Columns.Insert(6, "備考", 180, HorizontalAlignment.Left);
            this.userList.Columns.Insert(7, "更新日時", 80, HorizontalAlignment.Left);
            this.userList.Columns.Insert(8, "更新者", 80, HorizontalAlignment.Left);

            //リストビューを初期化する
            user_list = new DataTable("table2");
            user_list.Columns.Add("No", Type.GetType("System.Int32"));
            user_list.Columns.Add("有効", Type.GetType("System.String"));
            user_list.Columns.Add("カスタマ名", Type.GetType("System.String"));
            user_list.Columns.Add("カスタマ名カナ", Type.GetType("System.String"));
            user_list.Columns.Add("カスタマ名略称", Type.GetType("System.String"));
            user_list.Columns.Add("レポート出力有無", Type.GetType("System.String"));
            user_list.Columns.Add("備考", Type.GetType("System.String"));
            user_list.Columns.Add("更新日時", Type.GetType("System.String"));
            user_list.Columns.Add("更新者", Type.GetType("System.String"));


            //データの挿入
            if (dsp_L.user_L != null)
            {
                string fastline = "";
                foreach (userDS v in dsp_L.user_L)
                {

                    if (fastline == v.userno)
                        continue;

                    DataRow urow = user_list.NewRow();

                    urow["No"] = v.userno;
                    urow["有効"] = v.status;
                    urow["カスタマ名"] = v.username;
                    urow["カスタマ名カナ"] = v.username_kana;
                    urow["カスタマ名略称"] = v.username_sum;
                    urow["レポート出力有無"] = v.report_status;
                    urow["備考"] = v.biko;
                    urow["更新日時"] = v.chk_date;
                    urow["更新者"] = v.chk_name_id;
                    user_list.Rows.Add(urow);

                    fastline = v.userno;

                }

                this.userList.VirtualListSize = user_list.Rows.Count;
                this.userList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            }

        }
        //システム情報の表示
        private void disp_system(DISP_dataSet dsp_L)
        {
            //システムリスト
            this.systemList.VirtualMode = true;

            // １行全体選択
            this.systemList.FullRowSelect = true;
            this.systemList.HideSelection = false;
            this.systemList.HeaderStyle = ColumnHeaderStyle.Clickable;
            //Hook up handlers for VirtualMode events.
            this.systemList.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(systemList_RetrieveVirtualItem);
            this.systemList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            // Column追加
            this.systemList.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.systemList.Columns.Insert(1, "システム名", 180, HorizontalAlignment.Left);
            this.systemList.Columns.Insert(2, "システム名カナ", 50, HorizontalAlignment.Left);
            this.systemList.Columns.Insert(3, "備考", 30, HorizontalAlignment.Left);
            this.systemList.Columns.Insert(4, "更新日時", 80, HorizontalAlignment.Left);
            this.systemList.Columns.Insert(5, "更新者", 80, HorizontalAlignment.Left);

            //リストビューを初期化する
            system_list = new DataTable("table3");
            system_list.Columns.Add("No", Type.GetType("System.Int32"));
            system_list.Columns.Add("システム名", Type.GetType("System.String"));
            system_list.Columns.Add("システム名カナ", Type.GetType("System.String"));
            system_list.Columns.Add("備考", Type.GetType("System.String"));
            system_list.Columns.Add("更新日時", Type.GetType("System.String"));
            system_list.Columns.Add("更新者", Type.GetType("System.String"));

            //システム情報
            if (dsp_L.system_L != null)
            {
                string fastline = "";
                foreach (systemDS sys in dsp_L.system_L)
                {

                    if (fastline == sys.systemno)
                        continue;

                    DataRow row = system_list.NewRow();
                    row["No"] = sys.systemno;
                    row["システム名"] = sys.systemname;
                    row["システム名カナ"] = sys.systemkana;
                    row["備考"] = sys.biko;
                    row["更新日時"] = sys.chk_date;
                    row["更新者"] = sys.chk_name_id;
                    system_list.Rows.Add(row);

                    fastline = sys.systemno;
                }

                this.systemList.VirtualListSize = system_list.Rows.Count;
                this.systemList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            }
        }
        //拠点情報一覧の表示
        private void disp_site(DISP_dataSet dsp_L, String systemno = null)
        {
            //拠点
            this.siteList.VirtualMode = true;
            // １行全体選択
            this.siteList.FullRowSelect = true;
            this.siteList.HideSelection = false;
            this.siteList.HeaderStyle = ColumnHeaderStyle.Clickable;

            //Hook up handlers for VirtualMode events.
            this.siteList.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(siteList_RetrieveVirtualItem);
            this.siteList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            // Column追加
            this.siteList.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.siteList.Columns.Insert(1, "有効", 30, HorizontalAlignment.Left);
            this.siteList.Columns.Insert(2, "拠点名", 180, HorizontalAlignment.Left);
            this.siteList.Columns.Insert(3, "住所1", 50, HorizontalAlignment.Left);
            this.siteList.Columns.Insert(4, "住所2", 50, HorizontalAlignment.Left);
            this.siteList.Columns.Insert(5, "TEL/FAX", 80, HorizontalAlignment.Left);
            this.siteList.Columns.Insert(6, "備考", 180, HorizontalAlignment.Left);
            this.siteList.Columns.Insert(7, "更新日時", 80, HorizontalAlignment.Left);
            this.siteList.Columns.Insert(8, "更新者", 80, HorizontalAlignment.Left);

            //リストビューを初期化する
            site_list = new DataTable("table4");
            site_list.Columns.Add("No", Type.GetType("System.Int32"));
            site_list.Columns.Add("有効", Type.GetType("System.String"));
            site_list.Columns.Add("拠点名", Type.GetType("System.String"));
            site_list.Columns.Add("住所1", Type.GetType("System.String"));
            site_list.Columns.Add("住所2", Type.GetType("System.String"));
            site_list.Columns.Add("TEL/FAX", Type.GetType("System.String"));
            site_list.Columns.Add("備考", Type.GetType("System.String"));
            site_list.Columns.Add("更新日時", Type.GetType("System.String"));
            site_list.Columns.Add("更新者", Type.GetType("System.String"));

            //拠点情報
            if (dsp_L.site_L != null)
            {
                HashSet<string> ary1 = new HashSet<string>();

                foreach (siteDS s in dsp_L.site_L)
                {
                    //絞込みの時
                    if (systemno != null)
                    {
                        if (systemno == s.systemno)
                        {
                            //重複チェック
                            if (ary1.Add(s.systemno))
                            {
                                DataRow row = site_list.NewRow();
                                row["No"] = s.siteno;
                                row["有効"] = s.status;
                                row["拠点名"] = s.sitename;
                                row["住所1"] = s.address1;
                                row["住所2"] = s.address2;
                                row["TEL/FAX"] = s.telno;
                                row["備考"] = s.biko;
                                row["更新日時"] = s.chk_date;
                                row["更新者"] = s.chk_name_id;
                                site_list.Rows.Add(row);
                            }
                        }
                    }
                    else
                    {
                        //重複チェック
                        if (ary1.Add(s.systemno))
                        {
                            DataRow row = site_list.NewRow();
                            row["No"] = s.siteno;
                            row["有効"] = s.status;
                            row["拠点名"] = s.sitename;
                            row["住所1"] = s.address1;
                            row["住所2"] = s.address2;
                            row["TEL/FAX"] = s.telno;
                            row["備考"] = s.biko;
                            row["更新日時"] = s.chk_date;
                            row["更新者"] = s.chk_name_id;
                            site_list.Rows.Add(row);
                        }
                    }
                }

                this.siteList.VirtualListSize = site_list.Rows.Count;
                this.siteList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            }

        }


        //ホスト
        private void disp_host(DISP_dataSet dsp_L, String siteno = null)
        {
            //機器
            this.m_host_list.VirtualMode = true;
            // １行全体選択
            this.m_host_list.FullRowSelect = true;
            this.m_host_list.HideSelection = false;
            this.m_host_list.HeaderStyle = ColumnHeaderStyle.Clickable;
            //Hook up handlers for VirtualMode events.
            this.m_host_list.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(hostList_RetrieveVirtualItem);
            this.m_host_list.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            // Column追加
            this.m_host_list.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_host_list.Columns.Insert(1, "有効", 30, HorizontalAlignment.Left);
            this.m_host_list.Columns.Insert(2, "ホスト名(英数)", 180, HorizontalAlignment.Left);
            this.m_host_list.Columns.Insert(3, "ホスト名(日本語)", 180, HorizontalAlignment.Left);
            this.m_host_list.Columns.Insert(4, "機種", 30, HorizontalAlignment.Left);
            this.m_host_list.Columns.Insert(5, "設置場所", 80, HorizontalAlignment.Left);
            this.m_host_list.Columns.Insert(6, "用途", 180, HorizontalAlignment.Left);
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
            host_list.Columns.Add("ホスト名(英数)", Type.GetType("System.String"));
            host_list.Columns.Add("ホスト名(日本語)", Type.GetType("System.String"));
            host_list.Columns.Add("機種", Type.GetType("System.String"));
            host_list.Columns.Add("設置場所", Type.GetType("System.String"));
            host_list.Columns.Add("用途", Type.GetType("System.String"));
            host_list.Columns.Add("監視開始日時", Type.GetType("System.String"));
            host_list.Columns.Add("監視終了日時", Type.GetType("System.String"));
            host_list.Columns.Add("保守管理番号", Type.GetType("System.String"));
            host_list.Columns.Add("保守情報", Type.GetType("System.String"));
            host_list.Columns.Add("備考", Type.GetType("System.String"));
            host_list.Columns.Add("更新日時", Type.GetType("System.String"));
            host_list.Columns.Add("更新者", Type.GetType("System.String"));

            //機器情報
            if (dsp_L.host_L != null)
            {

                HashSet<string> ary1 = new HashSet<string>();
                foreach (hostDS h in dsp_L.host_L)
                {
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
                                row["ホスト名(英数)"] = h.hostname;
                                row["ホスト名(日本語)"] = h.hostname_ja;
                                row["機種"] = h.device;
                                row["設置場所"] = h.location;
                                row["用途"] = h.usefor;
                                row["監視開始日時"] = h.kansiStartdate;
                                row["監視終了日時"] = h.kansiEndsdate;
                                row["保守管理番号"] = h.hosyukanri;
                                row["保守情報"] = h.hosyuinfo;
                                row["備考"] = h.biko;
                                row["更新日時"] = h.chk_date;
                                row["更新者"] = h.chk_name_id;
                                host_list.Rows.Add(row);
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
                            row["ホスト名(英数)"] = h.hostname;
                            row["ホスト名(日本語)"] = h.hostname_ja;
                            row["機種"] = h.device;
                            row["設置場所"] = h.location;
                            row["用途"] = h.usefor;
                            row["監視開始日時"] = h.kansiStartdate;
                            row["監視終了日時"] = h.kansiEndsdate;
                            row["保守管理番号"] = h.hosyukanri;
                            row["保守情報"] = h.hosyuinfo;
                            row["備考"] = h.biko;
                            row["更新日時"] = h.chk_date;
                            row["更新者"] = h.chk_name_id;
                            host_list.Rows.Add(row);
                        }
                    }
                }

                this.m_host_list.VirtualListSize = host_list.Rows.Count;
                this.m_host_list.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }

        }
        //インターフェイス
        private void disp_interface(DISP_dataSet dsp_L, String siteno = null, String hostno = null)
        {
            //インターフェイス
            this.interfaceList.VirtualMode = true;
            // １行全体選択
            this.interfaceList.FullRowSelect = true;
            this.interfaceList.HideSelection = false;
            this.interfaceList.HeaderStyle = ColumnHeaderStyle.Clickable;
            //Hook up handlers for VirtualMode events.
            this.interfaceList.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(interfaceList_RetrieveVirtualItem);
            this.interfaceList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            // Column追加
            this.interfaceList.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.interfaceList.Columns.Insert(1, "有効", 30, HorizontalAlignment.Left);
            this.interfaceList.Columns.Insert(2, "インターフェイス名", 180, HorizontalAlignment.Left);
            this.interfaceList.Columns.Insert(3, "監視タイプ", 30, HorizontalAlignment.Left);
            this.interfaceList.Columns.Insert(4, "監視項目名", 180, HorizontalAlignment.Left);
            this.interfaceList.Columns.Insert(5, "監視開始日時", 100, HorizontalAlignment.Left);
            this.interfaceList.Columns.Insert(6, "監視終了日時", 100, HorizontalAlignment.Left);
            this.interfaceList.Columns.Insert(7, "閾値", 30, HorizontalAlignment.Left);
            this.interfaceList.Columns.Insert(8, "IPアドレス", 100, HorizontalAlignment.Left);
            this.interfaceList.Columns.Insert(9, "IPアドレス(NAT)", 100, HorizontalAlignment.Left);
            this.interfaceList.Columns.Insert(10, "ホスト通番", 80, HorizontalAlignment.Left);
            this.interfaceList.Columns.Insert(11, "カスタマ通番", 80, HorizontalAlignment.Left);
            this.interfaceList.Columns.Insert(12, "システム通番", 80, HorizontalAlignment.Left);
            this.interfaceList.Columns.Insert(13, "拠点通番", 80, HorizontalAlignment.Left);
            this.interfaceList.Columns.Insert(14, "更新日時", 80, HorizontalAlignment.Left);
            this.interfaceList.Columns.Insert(15, "更新者", 80, HorizontalAlignment.Left);

            //リストビューを初期化する
            interface_list = new DataTable("table6");
            interface_list.Columns.Add("No", Type.GetType("System.String"));
            interface_list.Columns.Add("有効", Type.GetType("System.String"));
            interface_list.Columns.Add("インターフェイス名", Type.GetType("System.String"));
            interface_list.Columns.Add("監視タイプ", Type.GetType("System.String"));
            interface_list.Columns.Add("監視項目名", Type.GetType("System.String"));
            interface_list.Columns.Add("監視開始日時", Type.GetType("System.String"));
            interface_list.Columns.Add("監視終了日時", Type.GetType("System.String"));
            interface_list.Columns.Add("閾値", Type.GetType("System.String"));
            interface_list.Columns.Add("IPアドレス", Type.GetType("System.String"));
            interface_list.Columns.Add("IPアドレス(NAT)", Type.GetType("System.String"));
            interface_list.Columns.Add("ホスト通番", Type.GetType("System.String"));
            interface_list.Columns.Add("カスタマ通番", Type.GetType("System.String"));
            interface_list.Columns.Add("システム通番", Type.GetType("System.String"));
            interface_list.Columns.Add("拠点通番", Type.GetType("System.String"));

            interface_list.Columns.Add("更新日時", Type.GetType("System.String"));
            interface_list.Columns.Add("更新者", Type.GetType("System.String"));
            //インターフェイス情報
            if (dsp_L.watch_L != null)
            {
                HashSet<string> ary1 = new HashSet<string>();
                int dispflg = 0;
                foreach (watch_InterfaceDS w in dsp_L.watch_L)
                {
                    dispflg = 0;
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
                            row["監視開始日時"] = w.start_date;
                            row["監視終了日時"] = w.end_date;
                            row["閾値"] = w.border;
                            row["IPアドレス"] = w.IPaddress;
                            row["IPアドレス(NAT)"] = w.IPaddressNAT;
                            row["ホスト通番"] = w.host_no;
                            row["カスタマ通番"] = w.userno;
                            row["システム通番"] = w.systemno;
                            row["拠点通番"] = w.siteno;
                            row["更新日時"] = w.chk_date;
                            row["更新者"] = w.chk_name_id;

                            interface_list.Rows.Add(row);

                        }
                    }
                }
                this.interfaceList.VirtualListSize = interface_list.Rows.Count;
                this.interfaceList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }
        //回線情報
        private void disp_kaisen(DISP_dataSet dsp_L)
        {
            //回線情報
            this.kaisenList.VirtualMode = true;

            // １行全体選択
            this.kaisenList.FullRowSelect = true;
            this.kaisenList.HideSelection = false;
            this.kaisenList.HeaderStyle = ColumnHeaderStyle.Clickable;
            //Hook up handlers for VirtualMode events.
            this.kaisenList.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(kaisenList_RetrieveVirtualItem);
            this.kaisenList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            // Column追加
            this.kaisenList.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.kaisenList.Columns.Insert(1, "有効", 30, HorizontalAlignment.Left);
            this.kaisenList.Columns.Insert(2, "キャリア", 180, HorizontalAlignment.Left);
            this.kaisenList.Columns.Insert(3, "回線種別", 30, HorizontalAlignment.Left);
            this.kaisenList.Columns.Insert(4, "回線ID", 180, HorizontalAlignment.Left);
            this.kaisenList.Columns.Insert(5, "ISP", 100, HorizontalAlignment.Left);
            this.kaisenList.Columns.Insert(6, "サービス種別", 100, HorizontalAlignment.Left);
            this.kaisenList.Columns.Insert(7, "サービスID", 30, HorizontalAlignment.Left);
            this.kaisenList.Columns.Insert(8, "カスタマ通番", 30, HorizontalAlignment.Left);
            this.kaisenList.Columns.Insert(9, "システム通番", 30, HorizontalAlignment.Left);
            this.kaisenList.Columns.Insert(10, "拠点通番", 30, HorizontalAlignment.Left);
            this.kaisenList.Columns.Insert(11, "ホスト通番", 30, HorizontalAlignment.Left);
            this.kaisenList.Columns.Insert(12, "更新日時", 80, HorizontalAlignment.Left);
            this.kaisenList.Columns.Insert(13, "更新者", 80, HorizontalAlignment.Left);

            //リストビューを初期化する
            kasen_list = new DataTable("table7");
            kasen_list.Columns.Add("No", Type.GetType("System.Int32"));
            kasen_list.Columns.Add("有効", Type.GetType("System.String"));
            kasen_list.Columns.Add("キャリア", Type.GetType("System.String"));
            kasen_list.Columns.Add("回線種別", Type.GetType("System.String"));
            kasen_list.Columns.Add("回線ID", Type.GetType("System.String"));
            kasen_list.Columns.Add("ISP", Type.GetType("System.String"));
            kasen_list.Columns.Add("サービス種別", Type.GetType("System.String"));
            kasen_list.Columns.Add("サービスID", Type.GetType("System.String"));
            kasen_list.Columns.Add("カスタマ通番", Type.GetType("System.String"));
            kasen_list.Columns.Add("システム通番", Type.GetType("System.String"));
            kasen_list.Columns.Add("拠点通番", Type.GetType("System.String"));
            kasen_list.Columns.Add("ホスト通番", Type.GetType("System.String"));
            kasen_list.Columns.Add("更新日時", Type.GetType("System.String"));
            kasen_list.Columns.Add("更新者", Type.GetType("System.String"));
            //回線情報
            if (dsp_L.kaisen_L != null)
            {
                HashSet<string> ary1 = new HashSet<string>();
                foreach (kaisenDS ka in dsp_L.kaisen_L)
                {
                    //重複チェック
                    if (ary1.Add(ka.kaisenno))
                    {

                        DataRow row = kasen_list.NewRow();
                        row["No"] = ka.kaisenno;
                        row["有効"] = ka.status;
                        row["キャリア"] = ka.career;
                        row["回線種別"] = ka.type;
                        row["回線ID"] = ka.kaisenid;
                        row["ISP"] = ka.isp;
                        row["サービス種別"] = ka.servicetype;
                        row["サービスID"] = ka.serviceid;
                        row["カスタマ通番"] = ka.userno;
                        row["システム通番"] = ka.systemno;
                        row["拠点通番"] = ka.siteno;
                        row["ホスト通番"] = ka.host_no;
                        row["更新日時"] = ka.chk_date;
                        row["更新者"] = ka.chk_name_id;

                        kasen_list.Rows.Add(row);
                    }
                }

                this.kaisenList.VirtualListSize = kasen_list.Rows.Count;
                this.kaisenList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            }
        }



        //検索ボタンが押されたとき
        private void m_selectBtn_Click(object sender, EventArgs e)
        {

            this.m_selectBtn.Enabled = false;
            try
            {

                userList.Clear();
                systemList.Clear();
                siteList.Clear();
                m_host_list.Clear();
                interfaceList.Clear();
                kaisenList.Clear();
                m_keikaku_list.Clear();

                Dictionary<string, string> param_dict = new Dictionary<string, string>();

                Class_Detaget getdata = new Class_Detaget();

                dsp_L = new DISP_dataSet();

                //IPアドレス
                if (m_ipaddress.Text != "")
                {
                    param_dict["IPaddress"] = m_ipaddress.Text;
                    //dsp_L = getdata.getSelectInterface(param_dict, con, dsp_L);
                }

                //ホスト名コンボボックス
                if (m_hostCombo.Text != "")
                {

                    param_dict["hostname"] = m_hostCombo.Text;
                    //                    dsp_L = getdata.getSelectHost(param_dict, con, dsp_L);
                }

                //拠点コンボボックス
                if (m_siteCombo.Text != "")
                {
                    param_dict["sitename"] = m_siteCombo.Text;
                    //                    dsp_L = getdata.getSelectSite(param_dict, con, dsp_L);
                }
                //システム名コンボボックス
                if (m_systemCombo.Text != "")
                {
                    param_dict["systemname"] = m_systemCombo.Text;
                    //                    dsp_L = getdata.getSelectSystem(param_dict, con, dsp_L);
                }
                //カスタマ名コンボボックス
                if (m_usernameCombo.Text != "")
                {
                    param_dict["username"] = m_usernameCombo.Text;
                    //                    dsp_L = getdata.getSelectUser(param_dict, con, dsp_L);
                }

                //構成データの取得
                getdata.con = con;
                dsp_L = getdata.getSelectDataFor_Interface(param_dict, con, dsp_L);

                disp_User(dsp_L);

                disp_system(dsp_L);

                disp_site(dsp_L);

                disp_host(dsp_L);

                disp_interface(dsp_L);


                siteDSList = dsp_L.site_L;
                hostDSList = dsp_L.host_L;

                //回線情報を取得
                if (dsp_L.user_L != null)
                {
                    Class_Detaget dataget = new Class_Detaget();
                    dataget.getSelectKaisenInfo(user_list, dsp_L, con);
                    disp_kaisen(dsp_L);
                }
                //作業情報を取得
                if (dsp_L.user_L != null)
                {
                    Class_Detaget dataget = new Class_Detaget();
                    //SQL文の実行
                    //dataget.getSelectSagyoInfo(user_list, dsp_L, con);
                    //disp_sagyo(dsp_L);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("構成情報の表示時にエラーが発生しました。" + ex.Message, "構成情報表示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                this.m_selectBtn.Enabled = true;
            }

        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {

        }

        //ダブルクリックしたとき
        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                String scheduleNo;
                if (e.Node.Level == 3)
                {

                    scheduleNo = e.Node.ToolTipText;

                    if (scheduleNo != "" && e.Node.Nodes.Count == 0)
                    {
                        //インシデントの時         
                        if (e.Node.Parent.ToolTipText == "1")
                        {
                            List<incidentDS> incidentList = new List<incidentDS>();
                            Dictionary<string, string> param_dict = new Dictionary<string, string>();
                            Class_Detaget dg = new Class_Detaget();

                            Form_KeikakuDetail formdetail = new Form_KeikakuDetail();

                            string scheduleno = e.Node.ToolTipText;

                            formdetail.keikakudt = new scheduleDS();

                            string status = "0";
                            foreach (scheduleDS sch in schDSList)
                            {
                                if (scheduleno == sch.schedule_no)
                                {
                                    formdetail.keikakudt = sch;
                                    if (sch.status != null)
                                        if (sch.status == "未完了")
                                            status = "1";
                                        else if (sch.status == "完了")
                                            status = "0";

                                    formdetail.keikakudt.schedule_no = sch.schedule_no;
                                    formdetail.keikakudt.status = status;
                                    formdetail.keikakudt.timer_name = sch.timer_name;
                                    formdetail.keikakudt.schedule_type = sch.schedule_type;
                                    formdetail.keikakudt.repeat_type = sch.repeat_type;
                                    formdetail.keikakudt.start_date = sch.start_date;
                                    formdetail.keikakudt.end_date = sch.end_date;
                                    formdetail.keikakudt.alerm_message = sch.alerm_message;
                                    formdetail.keikakudt.sound = sch.sound;
                                    formdetail.keikakudt.incident_no = sch.incident_no;
                                    formdetail.keikakudt.kakunin = sch.kakunin;
                                    formdetail.keikakudt.userno = sch.userno;
                                    formdetail.keikakudt.systemno = sch.systemno;
                                    formdetail.keikakudt.siteno = sch.siteno;
                                    formdetail.keikakudt.chk_date = sch.chk_date;
                                    formdetail.keikakudt.chk_name_id = sch.chk_name_id;

                                    break;
                                }
                            }
                            formdetail.loginDS = loginDS;
                            formdetail.con = con;
                            formdetail.Owner = this;
                            formdetail.Show();
                        }
                        //定期作業のとき
                        else if (e.Node.Parent.ToolTipText == "2")
                        {
                            Form_KeikakuDetail formdetail = new Form_KeikakuDetail();
                            formdetail.con = con;

                            string scheduleno = e.Node.ToolTipText;

                            formdetail.keikakudt = new scheduleDS();

                            string status = "0";
                            foreach (scheduleDS sch in scheduleList_teiki)
                            {
                                if (scheduleno == sch.schedule_no)
                                {
                                    formdetail.keikakudt = sch;
                                    if (sch.status != null)
                                        if (sch.status == "未完了")
                                            status = "1";
                                        else if (sch.status == "完了")
                                            status = "0";

                                    formdetail.keikakudt.schedule_no = sch.schedule_no;
                                    formdetail.keikakudt.status = status;
                                    formdetail.keikakudt.timer_name = sch.timer_name;
                                    formdetail.keikakudt.schedule_type = sch.schedule_type;
                                    formdetail.keikakudt.repeat_type = sch.repeat_type;
                                    formdetail.keikakudt.start_date = sch.start_date;
                                    formdetail.keikakudt.end_date = sch.end_date;
                                    formdetail.keikakudt.alerm_message = sch.alerm_message;
                                    formdetail.keikakudt.sound = sch.sound;
                                    formdetail.keikakudt.incident_no = sch.incident_no;
                                    formdetail.keikakudt.kakunin = sch.kakunin;
                                    formdetail.keikakudt.userno = sch.userno;
                                    formdetail.keikakudt.systemno = sch.systemno;
                                    formdetail.keikakudt.siteno = sch.siteno;
                                    formdetail.keikakudt.chk_date = sch.chk_date;
                                    formdetail.keikakudt.chk_name_id = sch.chk_name_id;

                                    break;
                                }
                            }
                            formdetail.loginDS = loginDS;
                            formdetail.con = con;
                            formdetail.Owner = this;
                            formdetail.Show();
                        }

                        //計画作業のとき
                        else if (e.Node.Parent.ToolTipText == "3")
                        {
                            Form_KeikakuDetail formdetail = new Form_KeikakuDetail();
                            formdetail.con = con;

                            string scheduleno = e.Node.ToolTipText;

                            formdetail.keikakudt = new scheduleDS();

                            string status = "0";
                            foreach (scheduleDS sch in scheduleList_keikaku)
                            {
                                if (scheduleno == sch.schedule_no)
                                {
                                    formdetail.keikakudt = sch;
                                    if (sch.status != null)
                                        if (sch.status == "未完了")
                                            status = "1";
                                        else if (sch.status == "完了")
                                            status = "0";

                                    formdetail.keikakudt.schedule_no = sch.schedule_no;
                                    formdetail.keikakudt.status = status;
                                    formdetail.keikakudt.timer_name = sch.timer_name;
                                    formdetail.keikakudt.schedule_type = sch.schedule_type;
                                    formdetail.keikakudt.repeat_type = sch.repeat_type;
                                    formdetail.keikakudt.start_date = sch.start_date;
                                    formdetail.keikakudt.end_date = sch.end_date;
                                    formdetail.keikakudt.alerm_message = sch.alerm_message;
                                    formdetail.keikakudt.sound = sch.sound;
                                    formdetail.keikakudt.incident_no = sch.incident_no;
                                    formdetail.keikakudt.kakunin = sch.kakunin;
                                    formdetail.keikakudt.userno = sch.userno;
                                    formdetail.keikakudt.systemno = sch.systemno;
                                    formdetail.keikakudt.siteno = sch.siteno;
                                    formdetail.keikakudt.chk_date = sch.chk_date;
                                    formdetail.keikakudt.chk_name_id = sch.chk_name_id;

                                    break;
                                }
                            }
                            formdetail.loginDS = loginDS;
                            formdetail.con = con;
                            formdetail.Owner = this;
                            formdetail.Show();
                        }
                        //特別対応のとき
                        else if (e.Node.Parent.ToolTipText == "4")
                        {
                            Form_KeikakuDetail formdetail = new Form_KeikakuDetail();
                            formdetail.con = con;

                            string scheduleno = e.Node.ToolTipText;

                            formdetail.keikakudt = new scheduleDS();

                            string status = "0";
                            foreach (scheduleDS sch in scheduleList_tokubetu)
                            {
                                if (scheduleno == sch.schedule_no)
                                {
                                    formdetail.keikakudt = sch;
                                    if (sch.status != null)
                                        if (sch.status == "未完了")
                                            status = "1";
                                        else if (sch.status == "完了")
                                            status = "0";

                                    formdetail.keikakudt.schedule_no = sch.schedule_no;
                                    formdetail.keikakudt.status = status;
                                    formdetail.keikakudt.timer_name = sch.timer_name;
                                    formdetail.keikakudt.schedule_type = sch.schedule_type;
                                    formdetail.keikakudt.repeat_type = sch.repeat_type;
                                    formdetail.keikakudt.start_date = sch.start_date;
                                    formdetail.keikakudt.end_date = sch.end_date;
                                    formdetail.keikakudt.alerm_message = sch.alerm_message;
                                    formdetail.keikakudt.sound = sch.sound;
                                    formdetail.keikakudt.incident_no = sch.incident_no;
                                    formdetail.keikakudt.kakunin = sch.kakunin;
                                    formdetail.keikakudt.userno = sch.userno;
                                    formdetail.keikakudt.systemno = sch.systemno;
                                    formdetail.keikakudt.siteno = sch.siteno;
                                    formdetail.keikakudt.chk_date = sch.chk_date;
                                    formdetail.keikakudt.chk_name_id = sch.chk_name_id;

                                    break;
                                }
                            }
                            formdetail.loginDS = loginDS;
                            formdetail.con = con;
                            formdetail.Owner = this;
                            formdetail.Show();
                        }


                    }
                }
            }
            // If the file is not found, handle the exception and inform the user.
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("File not found.");
            }
        }

        //更新ボタン
        private void m_refresh_btn_Click(object sender, EventArgs e)
        {

            if (userDSList != null) userDSList.Clear();
            if (systemDSList != null) systemDSList.Clear();
            if (siteDSList != null) siteDSList.Clear();
            if (hostDSList != null) hostDSList.Clear();

            if (m_incident_List != null) m_incident_List.Clear();
            if (m_teiki_List != null) m_teiki_List.Clear();
            if (m_keikaku_list != null) m_keikaku_list.Clear();
            if (m_tokubetu_list != null) m_tokubetu_list.Clear();

            RefreshTreeView();

            combo_set();
            //インシデント一覧データを取得
            Class_Detaget dg_class = new Class_Detaget();
            //List<incidentDS> incidentList;
            incidentDSList = dg_class.getOpenIncident(con);

            //インシデント一覧を表示
            disp_Incident(incidentDSList);
            disp_sagyoList(schDSList);
        }

        //カスタマ名コンボボックスが変更されたとき
        private void m_usernameCombo_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            //システム名コンボボックス情報を読み込み
            Read_systemCombo();
        }

        //カスタマ名のコンボボックスが変更されたときの処理
        private void Read_systemCombo()
        {
            try
            {
                m_systemCombo.DataSource = null;
                m_siteCombo.DataSource = null;
                m_hostCombo.DataSource = null;

                //システムコンボの値を取得
                DataTable systemTable = new DataTable();
                systemTable.Columns.Add("ID", typeof(string));
                systemTable.Columns.Add("NAME", typeof(string));

                //システム情報を取得する
                if (systemDSList.Count <= 0)
                    return;
                int i = 0;
                foreach (systemDS v in systemDSList)
                {
                    //
                    if (i == 0)
                        systemTable.Rows.Add("");

                    //カスタマNOで区別する
                    if (m_usernameCombo.SelectedValue != null)
                    {
                        if (v.userno == m_usernameCombo.SelectedValue.ToString())
                        {
                            DataRow row = systemTable.NewRow();
                            row["ID"] = v.systemno;
                            row["NAME"] = v.systemname;
                            systemTable.Rows.Add(row);
                        }
                    }
                    i++;
                }
                //データテーブルを割り当てる
                m_systemCombo.DataSource = systemTable;
                m_systemCombo.DisplayMember = "NAME";
                m_systemCombo.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "システム情報読込", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        //システム名コンボが変更されたとき (システムIDでDBより拠点名を持ってくる
        private void m_systemCombo_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            string selectedIndex = null;
            if (m_systemCombo.SelectedValue == null || m_systemCombo.SelectedIndex == 0)
            {
                m_siteCombo.DataSource = null;
                m_hostCombo.DataSource = null;
                return;
            }

            selectedIndex = m_systemCombo.SelectedValue.ToString();

            //拠点情報を読み込む
            Read_siteCombo(selectedIndex);
        }

        //拠点名が変更された時
        private void m_siteCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string selectedIndex = null;
            if (m_siteCombo.SelectedValue == null || m_siteCombo.SelectedIndex == 0)
            {
                m_hostCombo.DataSource = null;
                return;
            }
            selectedIndex = m_siteCombo.SelectedValue.ToString();

            //ホスト名を読み込む
            Read_hostCombo(selectedIndex);
        }

        //ホスト名を読み込む
        private void Read_hostCombo(string siteno)
        {
            try
            {
                m_hostCombo.DataSource = null;

                Class_Detaget getuser = new Class_Detaget();

                //ホスト名を検索

                //空白行を追加
                hostDS tmp = new hostDS();
                tmp.hostname = "";
                tmp.host_no = "";
                List<hostDS> hostDSList = new List<hostDS>();
                hostDSList.Add(tmp);

                //リストの取得
                Boolean flg;
                if (siteno == "")
                    flg = false;

                else
                    flg = true;
                getuser.con = con;
                List<hostDS> hostDSList1 = getuser.getHostList(siteno, con, flg);

                if (hostDSList1 != null)
                    hostDSList.AddRange(hostDSList1);

                m_hostCombo.DataSource = hostDSList;
                m_hostCombo.DisplayMember = "hostname";
                m_hostCombo.ValueMember = "host_no";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ホスト情報の読み込みに失敗", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //システム名のコンボボックスが変更されたときの処理
        private void Read_siteCombo(string selectedIndex)
        {
            try
            {
                m_siteCombo.DataSource = null;
                m_hostCombo.DataSource = null;

                siteDS tmp = new siteDS();
                tmp.sitename = "";
                tmp.siteno = "";
                List<siteDS> siteDSList = new List<siteDS>();
                siteDSList.Add(tmp);

                //コンボボックス
                Class_Detaget getuser = new Class_Detaget();

                //拠点情報を取得する
                List<siteDS> siteDSList1 = getuser.getSiteList(selectedIndex, con, true);

                //拠点情報を取得する
                if (siteDSList1 != null)
                {
                    siteDSList.AddRange(siteDSList1);
                }
                m_siteCombo.DataSource = siteDSList;
                m_siteCombo.DisplayMember = "sitename";
                m_siteCombo.ValueMember = "siteno";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "拠点情報コンボボックスデータ読み込み時エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void comboBox4_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }
        //条件クリアボタン
        private void m_clear_btn_Click_1(object sender, EventArgs e)
        {
            m_usernameCombo.SelectedIndex = 0;
            m_systemCombo.DataSource = null;
            m_siteCombo.DataSource = null;
            m_hostCombo.DataSource = null;
            m_ipaddress.Text = "";
        }

        //カスタマ名をダブルクリック
        private void userList_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {

            ListView.SelectedIndexCollection item = userList.SelectedIndices;
            Form_UserDetail formdetail = new Form_UserDetail();
            formdetail.con = con;
            userDS userdt = new userDS();
            userdt.userno = this.userList.Items[item[0]].SubItems[0].Text;
            userdt.username = this.userList.Items[item[0]].SubItems[2].Text;
            userdt.username_kana = this.userList.Items[item[0]].SubItems[3].Text;
            userdt.username_sum = this.userList.Items[item[0]].SubItems[4].Text;

            string statustxt = this.userList.Items[item[0]].SubItems[1].Text;
            if (statustxt == "有効")
                userdt.status = "1";
            else
                userdt.status = "0";

            string reportststxt = this.userList.Items[item[0]].SubItems[5].Text;
            if (reportststxt == "有効")
                userdt.report_status = "1";
            else
                userdt.report_status = "0";

            userdt.biko = this.userList.Items[item[0]].SubItems[6].Text;
            userdt.chk_date = this.userList.Items[item[0]].SubItems[7].Text;
            userdt.chk_name_id = this.userList.Items[item[0]].SubItems[8].Text;

            formdetail.loginDS = loginDS;

            formdetail.userdt = userdt;
            formdetail.Show();
            formdetail.Owner = this;
        }
        //システム情報の表示
        private void systemList_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            ListView.SelectedIndexCollection item = systemList.SelectedIndices;
            Form_SystemDetail formdetail = new Form_SystemDetail();
            formdetail.con = con;
            systemDS systemdt = new systemDS();

            string systemno = this.systemList.Items[item[0]].SubItems[0].Text;

            foreach (systemDS sl in systemDSList)
            {
                if (systemno == sl.systemno)
                {
                    formdetail.systemdt = sl;
                    break;
                }
            }

            formdetail.loginDS = loginDS;

            formdetail.Show();
            formdetail.Owner = this;
        }
        //拠点をダブルクリック
        private void siteList_DoubleClick(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection item = siteList.SelectedIndices;
            Form_SiteDetail formdetail = new Form_SiteDetail();
            formdetail.con = con;

            //拠点番号
            string siteno = this.siteList.Items[item[0]].SubItems[0].Text;

            if (siteDSList == null)
            {
                formdetail.sitedt = new siteDS();
                formdetail.sitedt.siteno =      this.siteList.Items[item[0]].SubItems[0].Text;
                formdetail.sitedt.status =      this.siteList.Items[item[0]].SubItems[1].Text;
                formdetail.sitedt.sitename =    this.siteList.Items[item[0]].SubItems[2].Text;
                formdetail.sitedt.address1 =    this.siteList.Items[item[0]].SubItems[3].Text;
                formdetail.sitedt.address2 =    this.siteList.Items[item[0]].SubItems[4].Text;
                formdetail.sitedt.telno =       this.siteList.Items[item[0]].SubItems[5].Text;
                formdetail.sitedt.biko =        this.siteList.Items[item[0]].SubItems[6].Text;
                formdetail.sitedt.chk_date =    this.siteList.Items[item[0]].SubItems[7].Text;
                formdetail.sitedt.chk_name_id = this.siteList.Items[item[0]].SubItems[8].Text;
            }
            else {
                foreach (siteDS sl in siteDSList)
                {
                    if (siteno == sl.siteno)
                    {
                        formdetail.sitedt = sl;
                        break;
                    }
                }
            }
            formdetail.loginDS = loginDS;

            formdetail.Show();
            formdetail.Owner = this;
        }
        //ホストリストのダブルクリック
        private void hostList_DoubleClick(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection item = m_host_list.SelectedIndices;
            Form_HostDetail formdetail = new Form_HostDetail();
            formdetail.con = con;

            //ホスト番号
            string hostno = this.m_host_list.Items[item[0]].SubItems[0].Text;

            if (hostDSList == null)
            {
                formdetail.hostdt = new hostDS();
                formdetail.hostdt.hostname = this.siteList.Items[item[0]].SubItems[0].Text;
                formdetail.hostdt.hostname_ja = this.siteList.Items[item[0]].SubItems[1].Text;
                formdetail.hostdt.status = this.siteList.Items[item[0]].SubItems[2].Text;
                formdetail.hostdt.device = this.siteList.Items[item[0]].SubItems[3].Text;
                formdetail.hostdt.location = this.siteList.Items[item[0]].SubItems[4].Text;
                formdetail.hostdt.usefor = this.siteList.Items[item[0]].SubItems[5].Text;
                formdetail.hostdt.kansiStartdate = this.siteList.Items[item[0]].SubItems[6].Text;
                formdetail.hostdt.kansiEndsdate = this.siteList.Items[item[0]].SubItems[7].Text;
                formdetail.hostdt.hosyukanri = this.siteList.Items[item[0]].SubItems[8].Text;
                formdetail.hostdt.hosyuinfo = this.siteList.Items[item[0]].SubItems[9].Text;
                formdetail.hostdt.biko = this.siteList.Items[item[0]].SubItems[10].Text;
                formdetail.hostdt.userno = this.siteList.Items[item[0]].SubItems[11].Text;
                formdetail.hostdt.systemno = this.siteList.Items[item[0]].SubItems[12].Text;
                formdetail.hostdt.siteno = this.siteList.Items[item[0]].SubItems[13].Text;
                formdetail.hostdt.chk_date = this.siteList.Items[item[0]].SubItems[14].Text;
                formdetail.hostdt.chk_name_id = this.siteList.Items[item[0]].SubItems[15].Text;
            }
            else {
                foreach (hostDS sl in hostDSList)
                {
                    if (hostno == sl.host_no)
                    {
                        formdetail.hostdt = sl;
                        break;
                    }
                }
            }

            formdetail.loginDS = loginDS;

            formdetail.Show();
            formdetail.Owner = this;
        }

        //インターフェイス一覧
        private void interfaceList_DoubleClick(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection item = interfaceList.SelectedIndices;
            Form_interfaceDetail formdetail = new Form_interfaceDetail();
            formdetail.con = con;

            string interfaceno = this.interfaceList.Items[item[0]].SubItems[0].Text;

            if (interfaceDSList == null)
            {
                formdetail.interfacedt = new watch_InterfaceDS();
                string status = "0";
                if (this.interfaceList.Items[item[0]].SubItems[1].Text != null)
                    if (this.interfaceList.Items[item[0]].SubItems[1].Text == "有効")
                        status = "1";
                    else if (this.interfaceList.Items[item[0]].SubItems[1].Text == "無効")
                        status = "0";


                formdetail.interfacedt.watch_Interfaceno = this.interfaceList.Items[item[0]].SubItems[0].Text;
                formdetail.interfacedt.interfacename = this.interfaceList.Items[item[0]].SubItems[2].Text;
                formdetail.interfacedt.status = status;
                formdetail.interfacedt.type = this.interfaceList.Items[item[0]].SubItems[3].Text;
                formdetail.interfacedt.kanshi = this.interfaceList.Items[item[0]].SubItems[4].Text;
                formdetail.interfacedt.start_date = this.interfaceList.Items[item[0]].SubItems[5].Text;
                formdetail.interfacedt.end_date = this.interfaceList.Items[item[0]].SubItems[6].Text;
                formdetail.interfacedt.border = this.interfaceList.Items[item[0]].SubItems[7].Text;
                formdetail.interfacedt.IPaddress = this.interfaceList.Items[item[0]].SubItems[8].Text;
                formdetail.interfacedt.IPaddressNAT = this.interfaceList.Items[item[0]].SubItems[9].Text;
                formdetail.interfacedt.host_no = this.interfaceList.Items[item[0]].SubItems[10].Text;
                formdetail.interfacedt.userno = this.interfaceList.Items[item[0]].SubItems[11].Text;
                formdetail.interfacedt.systemno = this.interfaceList.Items[item[0]].SubItems[12].Text;
                formdetail.interfacedt.siteno = this.interfaceList.Items[item[0]].SubItems[13].Text;
                formdetail.interfacedt.chk_date = this.interfaceList.Items[item[0]].SubItems[14].Text;
                formdetail.interfacedt.chk_name_id = this.interfaceList.Items[item[0]].SubItems[15].Text;
            }
            else {
                foreach (watch_InterfaceDS il in interfaceDSList)
                {
                    if (interfaceno == il.watch_Interfaceno)
                    {
                        formdetail.interfacedt = il;
                        break;
                    }
                }
            }

            formdetail.loginDS = loginDS;

            formdetail.Show();
            formdetail.Owner = this;
        }

        //回線情報ダブルクリック
        private void kaisenList_DoubleClick(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection item = kaisenList.SelectedIndices;
            Form_KaisenDetail formdetail = new Form_KaisenDetail();
            formdetail.con = con;

            string kaisenno = this.kaisenList.Items[item[0]].SubItems[0].Text;

            formdetail.kaisendt = new kaisenDS();
            string status = "0";
            if (this.kaisenList.Items[item[0]].SubItems[1].Text != null)
                if (this.kaisenList.Items[item[0]].SubItems[1].Text == "有効")
                    status = "1";
                else if (this.kaisenList.Items[item[0]].SubItems[1].Text == "無効")
                    status = "0";

            formdetail.kaisendt.kaisenno = this.kaisenList.Items[item[0]].SubItems[0].Text;
            formdetail.kaisendt.status = status;
            formdetail.kaisendt.career = this.kaisenList.Items[item[0]].SubItems[2].Text;
            formdetail.kaisendt.type = this.kaisenList.Items[item[0]].SubItems[3].Text;
            formdetail.kaisendt.kaisenid = this.kaisenList.Items[item[0]].SubItems[4].Text;
            formdetail.kaisendt.isp = this.kaisenList.Items[item[0]].SubItems[5].Text;
            formdetail.kaisendt.servicetype = this.kaisenList.Items[item[0]].SubItems[6].Text;
            formdetail.kaisendt.serviceid = this.kaisenList.Items[item[0]].SubItems[7].Text;
            formdetail.kaisendt.userno = this.kaisenList.Items[item[0]].SubItems[8].Text;
            formdetail.kaisendt.systemno = this.kaisenList.Items[item[0]].SubItems[9].Text;
            formdetail.kaisendt.siteno = this.kaisenList.Items[item[0]].SubItems[10].Text;
            formdetail.kaisendt.host_no = this.kaisenList.Items[item[0]].SubItems[11].Text;
            formdetail.kaisendt.chk_date = this.kaisenList.Items[item[0]].SubItems[12].Text;
            formdetail.kaisendt.chk_name_id = this.kaisenList.Items[item[0]].SubItems[13].Text;

            formdetail.loginDS = loginDS;

            formdetail.Show();
            formdetail.Owner = this;
        }


        //タイマー1分毎にサーバを見に行く
        private void timer1_Tick(object sender, EventArgs e)
        {

            //            System.Console.WriteLine("Timer1_Tick()_Begin");

            //時間になったタイマーの問い合わせ
            Class_Detaget dataget = new Class_Detaget();
            dataget.con = con;
            List<alermDS> alermlist;
            alermlist = dataget.getAlert(this, con);

            //1件以上あればメッセージ表示
            if (alermlist != null && alermlist.Count > 0)
            {
                //既に表示されているかチェックする
                if (alermdlg == null || alermdlg.IsDisposed)
                {
                    alermdlg = new Form_alermlist();
                    alermdlg.almList = alermlist;
                    alermdlg.con = con;
                    alermdlg.Show();
                }
                else
                {
                    alermdlg.almList = alermlist;
                    alermdlg.Refresh();
                }

            }
        }

        //フォームが閉じるとき
        private void Form_MainList_FormClosed(object sender, FormClosedEventArgs e)
        {
            //DBコネクションのクローズ
            if (con != null)
                con.Close();
        }
        //インシデント登録
        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form_IncidentInsert incidentfm = new Form_IncidentInsert();
            incidentfm.loginDS = loginDS;

            if (userDSList != null)
                incidentfm.userList = userDSList;
            if (systemDSList != null)
                incidentfm.systemList = systemDSList;
            if (siteDSList != null)
                incidentfm.siteList = siteDSList;
            incidentfm.con = con;
            incidentfm.Show();

        }
        //定期作業登録
        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            Form_TimerInsert timerfm = new Form_TimerInsert();
            if (userDSList != null)
                timerfm.userList = userDSList;
            if (systemDSList != null)
                timerfm.systemList = systemDSList;
            if (siteDSList != null)
                timerfm.siteList = siteDSList;
            timerfm.con = con;
            timerfm.loginDS = loginDS;
            timerfm.Show(this);
        }
        //計画作業登録
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form_TimerInsert timerfm = new Form_TimerInsert();
            if (userDSList != null)
                timerfm.userList = userDSList;
            if (systemDSList != null)
                timerfm.systemList = systemDSList;
            if (siteDSList != null)
                timerfm.siteList = siteDSList;

            timerfm.con = con;
            timerfm.loginDS = loginDS;
            timerfm.ShowDialog(this);
        }

        //計画作業列をダブルクリック
        private void m_keikaku_list_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView.SelectedIndexCollection item = m_keikaku_list.SelectedIndices;
            Form_KeikakuDetail formdetail = new Form_KeikakuDetail();
            formdetail.con = con;

            string scheduleno = this.m_keikaku_list.Items[item[0]].SubItems[0].Text;

            formdetail.keikakudt = new scheduleDS();

            string status = "0";
            foreach (scheduleDS sch in scheduleList_keikaku)
            {
                if (scheduleno == sch.schedule_no)
                {
                    formdetail.keikakudt = sch;
                    if (sch.status != null)
                        if (sch.status == "未完了")
                            status = "1";
                        else if (sch.status == "完了")
                            status = "0";

                    formdetail.keikakudt.schedule_no = sch.schedule_no;
                    formdetail.keikakudt.status = status;
                    formdetail.keikakudt.timer_name = sch.timer_name;
                    formdetail.keikakudt.schedule_type = sch.schedule_type;
                    formdetail.keikakudt.repeat_type = sch.repeat_type;
                    formdetail.keikakudt.start_date = sch.start_date;
                    formdetail.keikakudt.end_date = sch.end_date;
                    formdetail.keikakudt.alerm_message = sch.alerm_message;
                    formdetail.keikakudt.sound = sch.sound;
                    formdetail.keikakudt.incident_no = sch.incident_no;
                    formdetail.keikakudt.kakunin = sch.kakunin;
                    formdetail.keikakudt.userno = sch.userno;
                    formdetail.keikakudt.systemno = sch.systemno;
                    formdetail.keikakudt.chk_date = sch.chk_date;
                    formdetail.keikakudt.chk_name_id = sch.chk_name_id;

                    break;
                }
            }
            formdetail.loginDS = loginDS;
            formdetail.con = con;
            formdetail.Owner = this;
            formdetail.Show();

        }
        //計画作業でマウス右クリック
        private void m_keikaku_list_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                return;

            System.Drawing.Point p = System.Windows.Forms.Cursor.Position;

            ListView.SelectedIndexCollection item = m_keikaku_list.SelectedIndices;
            string status = this.m_keikaku_list.Items[item[0]].SubItems[1].Text;

            string menustring = "";
            if (status == "未完了")
                menustring = "完了";
            else
                return;

            this.keikakuContext.Items.Clear();

            ToolStripMenuItem it = new ToolStripMenuItem();
            it.Text = menustring;
            keikakuContext.Items.Add(it);
            keikakuContext.Show(p);

        }
        //定期作業を右クリック
        private void m_teiki_List_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                return;

            System.Drawing.Point p = System.Windows.Forms.Cursor.Position;

            ListView.SelectedIndexCollection item = m_teiki_List.SelectedIndices;
            string status = this.m_teiki_List.Items[item[0]].SubItems[1].Text;

            string menustring = "";
            if (status == "未完了")
                menustring = "完了";
            else
                return;

            this.keikakuContext.Items.Clear();

            ToolStripMenuItem it = new ToolStripMenuItem();
            it.Text = menustring;
            keikakuContext.Items.Add(it);
            keikakuContext.Show(p);
        }


        //変更がクリックされたとき  
        private void StatusChenge_Click(object sender, EventArgs e)
        {

        }
        //変更がクリックされたとき
        private void keikakuContext_MouseUp(object sender, MouseEventArgs e)
        {

        }
        //ステータス未完了を完了にする
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection collection = m_teiki_List.SelectedIndices;
            int count = collection.Count; // 選択されている個数がcountに
            MessageBox.Show(count.ToString() + "件　更新します。よろしいですか?", "定期作業ステータス更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            foreach (int i in collection)
            {
                // iに選択されているリストのインデックスが順次入る（当然0からの値）
                string scheduleid = this.m_teiki_List.Items[i].SubItems[0].Text;
                string status = this.m_teiki_List.Items[i].SubItems[1].Text;
                if (status == "未完了")
                {
                    if (con.FullState != ConnectionState.Open) con.Open();

                    string sql = "update schedule set status =:state,chk_name_id =:id, chk_date=:date where schedule_no = :a";
                    using (var transaction = con.BeginTransaction())
                    {
                        var command = new NpgsqlCommand(@sql, con);
                        command.Parameters.Add(new NpgsqlParameter("a", DbType.Int32) { Value = scheduleid });
                        command.Parameters.Add(new NpgsqlParameter("state", DbType.String) { Value = "0" });
                        command.Parameters.Add(new NpgsqlParameter("id", DbType.String) { Value = loginDS.opeid });
                        command.Parameters.Add(new NpgsqlParameter("date", DbType.DateTime) { Value = DateTime.Now });
                        Int32 rowsaffected;
                        try
                        {
                            //更新処理
                            rowsaffected = command.ExecuteNonQuery();
                            transaction.Commit();

                            if (rowsaffected != 1)
                                MessageBox.Show("更新できませんでした。", "定期作業ステータス更新");
                        }
                        catch (Exception ex)
                        {
                            //エラー時メッセージ表示
                            MessageBox.Show(ex.Message);
                            transaction.Rollback();
                            return;
                        }
                    }
                }
            }

        }
        //ステータス未完了を完了にする
        private void keikakuContext_Click(object sender, EventArgs e)
        {
            
            ListView.SelectedIndexCollection collection = m_keikaku_list.SelectedIndices;
            int count = collection.Count; // 選択されている個数がcountに
            MessageBox.Show(count.ToString() + "件　更新します。よろしいですか?", "計画作業ステータス更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            foreach (int i in collection)
            {
                // iに選択されているリストのインデックスが順次入る（当然0からの値）
                string scheduleid = this.m_keikaku_list.Items[i].SubItems[0].Text;
                string status = this.m_keikaku_list.Items[i].SubItems[1].Text;
                if (status == "未完了")
                {
                    if (con.FullState != ConnectionState.Open) con.Open();

                    string sql = "update schedule set status =:state,chk_name_id =:id, chk_date=:date where schedule_no = :a";
                    using (var transaction = con.BeginTransaction())
                    {
                        var command = new NpgsqlCommand(@sql, con);
                        command.Parameters.Add(new NpgsqlParameter("a", DbType.Int32) { Value = scheduleid });
                        command.Parameters.Add(new NpgsqlParameter("state", DbType.String) { Value = "0" });
                        command.Parameters.Add(new NpgsqlParameter("id", DbType.String) { Value = loginDS.opeid });
                        command.Parameters.Add(new NpgsqlParameter("date", DbType.DateTime) { Value = DateTime.Now });
                        Int32 rowsaffected;
                        try
                        {
                            //更新処理
                            rowsaffected = command.ExecuteNonQuery();
                            transaction.Commit();

                            if (rowsaffected != 1)
                                MessageBox.Show("更新できませんでした。", "計画作業ステータス更新");
                        }
                        catch (Exception ex)
                        {
                            //エラー時メッセージ表示
                            MessageBox.Show(ex.Message);
                            transaction.Rollback();
                            return;
                        }
                    }
                }
            }
        }
        //定期作業をダブルクリック
        private void m_teiki_List_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            ListView.SelectedIndexCollection item = m_teiki_List.SelectedIndices;
            Form_KeikakuDetail formdetail = new Form_KeikakuDetail();
            formdetail.con = con;

            string scheduleno = this.m_teiki_List.Items[item[0]].SubItems[0].Text;

            formdetail.keikakudt = new scheduleDS();

            string status = "0";
            foreach (scheduleDS sch in scheduleList_teiki)
            {
                if (scheduleno == sch.schedule_no)
                {
                    formdetail.keikakudt = sch;
                    if (sch.status != null)
                        if (sch.status == "未完了")
                            status = "1";
                        else if (sch.status == "完了")
                            status = "0";

                    formdetail.keikakudt.schedule_no = sch.schedule_no;
                    formdetail.keikakudt.status = status;
                    formdetail.keikakudt.timer_name = sch.timer_name;
                    formdetail.keikakudt.schedule_type = sch.schedule_type;
                    formdetail.keikakudt.repeat_type = sch.repeat_type;
                    formdetail.keikakudt.start_date = sch.start_date;
                    formdetail.keikakudt.end_date = sch.end_date;
                    formdetail.keikakudt.alerm_message = sch.alerm_message;
                    formdetail.keikakudt.sound = sch.sound;
                    formdetail.keikakudt.incident_no = sch.incident_no;
                    formdetail.keikakudt.kakunin = sch.kakunin;
                    formdetail.keikakudt.userno = sch.userno;
                    formdetail.keikakudt.systemno = sch.systemno;
                    formdetail.keikakudt.chk_date = sch.chk_date;
                    formdetail.keikakudt.chk_name_id = sch.chk_name_id;

                    break;
                }
            }
            formdetail.loginDS = loginDS;
            formdetail.con = con;
            formdetail.Owner = this;
            formdetail.Show();
        }
        //インシデント情報をダブルクリック
        private void m_incident_List_DoubleClick(object sender, EventArgs e)
        {

            ListView.SelectedIndexCollection item = m_incident_List.SelectedIndices;
            Form_incidentDetail formdetail = new Form_incidentDetail();
            formdetail.con = con;

            string incidentno = this.m_incident_List.Items[item[0]].SubItems[0].Text;

            formdetail.incidentdt = new incidentDS();

            string status = "0";
            foreach (incidentDS sch in incidentDSList)
            {
                if (incidentno == sch.incident_no)
                {
                    formdetail.incidentdt = sch;
                    if (sch.status != null)
                        if (sch.status == "未完了")
                            status = "1";
                        else if (sch.status == "完了")
                            status = "0";

                    formdetail.incidentdt.incident_no = sch.incident_no;
                    formdetail.incidentdt.status = status;
                    formdetail.incidentdt.mpms_incident = sch.mpms_incident;
                    formdetail.incidentdt.s_cube_id = sch.s_cube_id;
                    formdetail.incidentdt.incident_type = sch.incident_type;
                    formdetail.incidentdt.content = sch.content;
                    formdetail.incidentdt.matflg = sch.matflg;
                    formdetail.incidentdt.matcommand = sch.matcommand;
                    formdetail.incidentdt.uketukedate = sch.uketukedate;
                    formdetail.incidentdt.tehaidate = sch.tehaidate;
                    formdetail.incidentdt.fukyudate = sch.fukyudate;
                    formdetail.incidentdt.enddate = sch.enddate;
                    formdetail.incidentdt.userno = sch.userno;
                    formdetail.incidentdt.systemno = sch.systemno;
                    formdetail.incidentdt.chk_date = sch.chk_date;
                    formdetail.incidentdt.chk_name_id = sch.chk_name_id;

                    break;
                }
            }
            formdetail.loginDS = loginDS;
            formdetail.con = con;
            formdetail.Owner = this;
            formdetail.Show();
        }

        //メールの作成
        private void button1_Click(object sender, EventArgs e)
        {
            Form_mailTempleteList mailtmp = new Form_mailTempleteList();
            mailtmp.con = con;
            mailtmp.loginDS = loginDS;
            mailtmp.Show();
        }
        //カスタマ情報クリック
        private void userList_MouseClick(object sender, MouseEventArgs e)
        {

        }
        //システム情報をクリック
        private void systemList_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection item = systemList.SelectedIndices;

            //選択項目を取得
            string systemno;

            if (this.systemList.Items[item[0]].SubItems[0].Text != "")
            {
                //拠点情報一覧
                siteList.Clear();
                systemno = this.systemList.Items[item[0]].SubItems[0].Text;
                //拠点情報を絞り込む
                disp_site(dsp_L, systemno);

                //ホスト情報一覧
                m_host_list.Clear();
                //ホスト情報を絞り込む
                disp_host(dsp_L, systemno);

                //インターフェイス一覧
                interfaceList.Clear();
                //ホスト情報を絞り込む
                disp_interface(dsp_L, systemno);
            }


        }
        //拠点情報クリック
        private void siteList_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection item = siteList.SelectedIndices;

            //選択項目を取得
            string siteno;

            if (this.siteList.Items[item[0]].SubItems[0].Text != "")
            {
                //ホスト情報一覧
                m_host_list.Clear();
                siteno = this.siteList.Items[item[0]].SubItems[0].Text;
                //ホスト情報を絞り込む
                disp_host(dsp_L, siteno);

                //インターフェイス一覧
                interfaceList.Clear();
                //ホスト情報を絞り込む
                disp_interface(dsp_L, siteno);
            }


        }
        //ホストをクリック
        private void m_host_list_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection item = m_host_list.SelectedIndices;

            //選択項目を取得
            string host_no;

            if (this.m_host_list.Items[item[0]].SubItems[0].Text != "")
            {
                host_no = this.m_host_list.Items[item[0]].SubItems[0].Text;

                //インターフェイス一覧
                interfaceList.Clear();

                //ホスト情報を絞り込む
                disp_interface(dsp_L, null, host_no);
            }

        }


        //カスタマデータをクリック
        private void userList_ColumnClick(object sender, ColumnClickEventArgs e)
        {

            if (this.user_list == null)
                return;
            if (this.user_list.Rows.Count <= 0)
                return;
            //DataViewクラス ソートするためのクラス
            DataView dv = new DataView(user_list);

            //一時クラス
            DataTable dttmp = new DataTable();

            String strSort = "";

            //0なら昇順にソート
            if (sort_kind == 0)
            {
                strSort = " ASC";
                sort_kind = 1;
            }
            else {
                //１の時は昇順にソート
                strSort = " DESC";
                sort_kind = 0;
            }

            //コピーを作成
            dttmp = user_list.Clone();
            //ソートを実行
            dv.Sort = user_list.Columns[e.Column].ColumnName + strSort;

            // ソートされたレコードのコピー
            foreach (DataRowView drv in dv)
            {
                // 一時テーブルに格納
                dttmp.ImportRow(drv.Row);
            }
            //格納したテーブルデータを上書く
            user_list = dttmp.Copy();

            //行が存在するかチェックを行う。
            if (userList.TopItem != null)
            {
                //現在一番上の行に表示されている行を取得
                int start = userList.TopItem.Index;
                // ListView画面の再表示を行う
                userList.RedrawItems(start, userList.Items.Count - 1, true);
            }
        }



        //拠点
        private void siteList_ColumnClick(object sender, ColumnClickEventArgs e)
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
            else {
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
            if (siteList.TopItem != null)
            {
                //現在一番上の行に表示されている行を取得
                int start = siteList.TopItem.Index;
                // ListView画面の再表示を行う
                siteList.RedrawItems(start, siteList.Items.Count - 1, true);
            }
        }

        //システムリストカラムをクリック
        private void systemList_ColumnClick(object sender, ColumnClickEventArgs e)
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
            else {
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
            if (systemList.TopItem != null)
            {
                //現在一番上の行に表示されている行を取得
                int start = systemList.TopItem.Index;
                // ListView画面の再表示を行う
                systemList.RedrawItems(start, systemList.Items.Count - 1, true);
            }
        }

        //ホスト情報一覧のカラムクリック
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
            else {
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
        //インターフェイス一覧のカラムをクリックしたとき
        private void interfaceList_ColumnClick(object sender, ColumnClickEventArgs e)
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
            else {
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
            if (this.interfaceList.TopItem != null)
            {
                //現在一番上の行に表示されている行を取得
                int start = interfaceList.TopItem.Index;
                // ListView画面の再表示を行う
                interfaceList.RedrawItems(start, interfaceList.Items.Count - 1, true);
            }
        }
        //回線業者情報のカラムをクリックしたとき
        private void kaisenList_ColumnClick(object sender, ColumnClickEventArgs e)
        {

            if (this.kasen_list == null)
                return;
            if (this.kasen_list.Rows.Count <= 0)
                return;
            //DataViewクラス ソートするためのクラス
            DataView dv = new DataView(kasen_list);

            //一時クラス
            DataTable dttmp = new DataTable();

            String strSort = "";

            //0なら昇順にソート
            if (sort_kind_kaisen == 0)
            {
                strSort = " ASC";
                sort_kind_kaisen = 1;
            }
            else {
                //１の時は昇順にソート
                strSort = " DESC";
                sort_kind_kaisen = 0;
            }

            //コピーを作成
            dttmp = kasen_list.Clone();
            //ソートを実行
            dv.Sort = kasen_list.Columns[e.Column].ColumnName + strSort;

            // ソートされたレコードのコピー
            foreach (DataRowView drv in dv)
            {
                // 一時テーブルに格納
                dttmp.ImportRow(drv.Row);
            }
            //格納したテーブルデータを上書く
            kasen_list = dttmp.Copy();

            //行が存在するかチェックを行う。
            if (this.kaisenList.TopItem != null)
            {
                //現在一番上の行に表示されている行を取得
                int start = kaisenList.TopItem.Index;
                // ListView画面の再表示を行う
                kaisenList.RedrawItems(start, kaisenList.Items.Count - 1, true);
            }

        }
        //インシデント情報をクリックしたとき
        private void m_incident_List_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.incident_List == null)
                return;
            if (this.incident_List.Rows.Count <= 0)
                return;
            //DataViewクラス ソートするためのクラス
            DataView dv = new DataView(incident_List);

            //一時クラス
            DataTable dttmp = new DataTable();

            String strSort = "";

            //0なら昇順にソート
            if (sort_kind_incident == 0)
            {
                strSort = " ASC";
                sort_kind_incident = 1;
            }
            else {
                //１の時は昇順にソート
                strSort = " DESC";
                sort_kind_incident = 0;
            }

            //コピーを作成
            dttmp = incident_List.Clone();
            //ソートを実行
            dv.Sort = incident_List.Columns[e.Column].ColumnName + strSort;

            // ソートされたレコードのコピー
            foreach (DataRowView drv in dv)
            {
                // 一時テーブルに格納
                dttmp.ImportRow(drv.Row);
            }
            //格納したテーブルデータを上書く
            incident_List = dttmp.Copy();

            //行が存在するかチェックを行う。
            if (this.m_incident_List.TopItem != null)
            {
                //現在一番上の行に表示されている行を取得
                int start = m_incident_List.TopItem.Index;
                // ListView画面の再表示を行う
                m_incident_List.RedrawItems(start, m_incident_List.Items.Count - 1, true);
            }
        }
        //計画作業情報のカラムをクリックした時
        private void m_teiki_List_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.teiki_sagyo_list == null)
                return;
            if (this.teiki_sagyo_list.Rows.Count <= 0)
                return;
            //DataViewクラス ソートするためのクラス
            DataView dv = new DataView(teiki_sagyo_list);

            //一時クラス
            DataTable dttmp = new DataTable();

            String strSort = "";

            //0なら昇順にソート
            if (sort_kind_teiki == 0)
            {
                strSort = " ASC";
                sort_kind_teiki = 1;
            }
            else {
                //１の時は昇順にソート
                strSort = " DESC";
                sort_kind_teiki = 0;
            }

            //コピーを作成
            dttmp = teiki_sagyo_list.Clone();
            //ソートを実行
            dv.Sort = teiki_sagyo_list.Columns[e.Column].ColumnName + strSort;

            // ソートされたレコードのコピー
            foreach (DataRowView drv in dv)
            {
                // 一時テーブルに格納
                dttmp.ImportRow(drv.Row);
            }
            //格納したテーブルデータを上書く
            teiki_sagyo_list = dttmp.Copy();

            //行が存在するかチェックを行う。
            if (this.m_teiki_List.TopItem != null)
            {
                //現在一番上の行に表示されている行を取得
                int start = m_teiki_List.TopItem.Index;
                // ListView画面の再表示を行う
                m_teiki_List.RedrawItems(start, m_teiki_List.Items.Count - 1, true);
            }
        }

        private void m_keikaku_list_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.keikaku_sagyo_list == null)
                return;
            if (this.keikaku_sagyo_list.Rows.Count <= 0)
                return;
            //DataViewクラス ソートするためのクラス
            DataView dv = new DataView(keikaku_sagyo_list);

            //一時クラス
            DataTable dttmp = new DataTable();

            String strSort = "";

            //0なら昇順にソート
            if (sort_kind_keikaku == 0)
            {
                strSort = " ASC";
                sort_kind_keikaku = 1;
            }
            else {
                //１の時は昇順にソート
                strSort = " DESC";
                sort_kind_keikaku = 0;
            }

            //コピーを作成
            dttmp = keikaku_sagyo_list.Clone();
            //ソートを実行
            dv.Sort = keikaku_sagyo_list.Columns[e.Column].ColumnName + strSort;

            // ソートされたレコードのコピー
            foreach (DataRowView drv in dv)
            {
                // 一時テーブルに格納
                dttmp.ImportRow(drv.Row);
            }
            //格納したテーブルデータを上書く
            keikaku_sagyo_list = dttmp.Copy();

            //行が存在するかチェックを行う。
            if (this.m_keikaku_list.TopItem != null)
            {
                //現在一番上の行に表示されている行を取得
                int start = m_keikaku_list.TopItem.Index;
                // ListView画面の再表示を行う
                m_keikaku_list.RedrawItems(start, m_keikaku_list.Items.Count - 1, true);
            }
        }
        //印刷
        private void button2_Click(object sender, EventArgs e)
        {
            Form_DispMail mailform = new Form_DispMail();
            mailTempleteDS mailds = new mailTempleteDS();
            mailds.body = "あああいいいうう";
            mailform.mailtempDS = mailds;
            mailform.Show();
        }
        //特別対応をダブルクリック
        private void m_tokubetu_list_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView.SelectedIndexCollection item = m_tokubetu_list.SelectedIndices;
            Form_KeikakuDetail formdetail = new Form_KeikakuDetail();
            formdetail.con = con;

            string scheduleno = this.m_tokubetu_list.Items[item[0]].SubItems[0].Text;

            formdetail.keikakudt = new scheduleDS();

            string status = "0";
            foreach (scheduleDS sch in scheduleList_tokubetu)
            {
                if (scheduleno == sch.schedule_no)
                {
                    formdetail.keikakudt = sch;
                    if (sch.status != null)
                        if (sch.status == "未完了")
                            status = "1";
                        else if (sch.status == "完了")
                            status = "0";

                    formdetail.keikakudt.schedule_no = sch.schedule_no;
                    formdetail.keikakudt.status = status;
                    formdetail.keikakudt.timer_name = sch.timer_name;
                    formdetail.keikakudt.schedule_type = sch.schedule_type;
                    formdetail.keikakudt.repeat_type = sch.repeat_type;
                    formdetail.keikakudt.start_date = sch.start_date;
                    formdetail.keikakudt.end_date = sch.end_date;
                    formdetail.keikakudt.alerm_message = sch.alerm_message;
                    formdetail.keikakudt.sound = sch.sound;
                    formdetail.keikakudt.incident_no = sch.incident_no;
                    formdetail.keikakudt.kakunin = sch.kakunin;
                    formdetail.keikakudt.userno = sch.userno;
                    formdetail.keikakudt.systemno = sch.systemno;
                    formdetail.keikakudt.chk_date = sch.chk_date;
                    formdetail.keikakudt.chk_name_id = sch.chk_name_id;

                    break;
                }
            }
            formdetail.loginDS = loginDS;
            formdetail.con = con;
            formdetail.Owner = this;
            formdetail.Show();
        }
        //右クリック
        private void m_tokubetu_list_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                return;

            System.Drawing.Point p = System.Windows.Forms.Cursor.Position;

            ListView.SelectedIndexCollection item = m_tokubetu_list.SelectedIndices;
            string status = this.m_tokubetu_list.Items[item[0]].SubItems[1].Text;

            string menustring = "";
            if (status == "未完了")
                menustring = "完了";
            else
                return;

            this.tokubetuContext.Items.Clear();

            ToolStripMenuItem it = new ToolStripMenuItem();
            it.Text = menustring;
            tokubetuContext.Items.Add(it);
            tokubetuContext.Show(p);
        }
        //特別対応カラムクリック
        private void m_tokubetu_list_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.tokubetu_sagyo_list == null)
                return;
            if (this.tokubetu_sagyo_list.Rows.Count <= 0)
                return;
            //DataViewクラス ソートするためのクラス
            DataView dv = new DataView(tokubetu_sagyo_list);

            //一時クラス
            DataTable dttmp = new DataTable();

            String strSort = "";

            //0なら昇順にソート
            if (sort_kind_tokubetu == 0)
            {
                strSort = " ASC";
                sort_kind_tokubetu = 1;
            }
            else {
                //１の時は昇順にソート
                strSort = " DESC";
                sort_kind_tokubetu = 0;
            }

            //コピーを作成
            dttmp = tokubetu_sagyo_list.Clone();
            //ソートを実行
            dv.Sort = tokubetu_sagyo_list.Columns[e.Column].ColumnName + strSort;

            // ソートされたレコードのコピー
            foreach (DataRowView drv in dv)
            {
                // 一時テーブルに格納
                dttmp.ImportRow(drv.Row);
            }
            //格納したテーブルデータを上書く
            tokubetu_sagyo_list = dttmp.Copy();

            //行が存在するかチェックを行う。
            if (this.m_tokubetu_list.TopItem != null)
            {
                //現在一番上の行に表示されている行を取得
                int start = m_tokubetu_list.TopItem.Index;
                // ListView画面の再表示を行う
                m_tokubetu_list.RedrawItems(start, m_tokubetu_list.Items.Count - 1, true);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        //特別対応登録ボタン
        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form_TimerInsert timerfm = new Form_TimerInsert();
            if (userDSList != null)
                timerfm.userList = userDSList;
            if (systemDSList != null)
                timerfm.systemList = systemDSList;
            if (siteDSList != null)
                timerfm.siteList = siteDSList;

            timerfm.con = con;
            timerfm.loginDS = loginDS;
            timerfm.ShowDialog(this);
        }
        //全ノード展開
        private void button3_Click(object sender, EventArgs e)
        {
            if(button3.Text == "全ノード展開") { 
                treeView1.ExpandAll();
                button3.Text = "閉じる";
            }
            else
            {
                treeView1.CollapseAll();
                button3.Text = "全ノード展開";
            }
        }

        //インシデント印刷
        private void print_incident_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //印刷の表示
            Form_print form = new Form_print();

            form.con = con;

            form.incidentDSList = incidentDSList;
            form.kubunstr = "インシデント対応";
            form.Show();

        }

        //計画作業
        private void print_keikaku_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //印刷の表示
            Form_print form = new Form_print();

            form.con = con;

            form.scheduleList = scheduleList_keikaku;
            form.kubunstr = "計画作業";
            form.Show();

        }
        //特別対応
        private void print_tokubetu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //印刷の表示
            Form_print form = new Form_print();

            form.con = con;

            form.scheduleList = scheduleList_tokubetu;
            form.kubunstr = "特別対応";
            form.Show();

        }
    }

}
