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
        DataTable sagyo_list;

        //
        List<userDS> userDSList;
        List<systemDS> systemDSList;
        List<siteDS> siteDSList;
        List<hostDS> hostDSList;


        public Form_MainList()
        {
            InitializeComponent();
        }
        //表示前
        private void Form_MainList_Load(object sender, EventArgs e)
        {
            try
            {


                //ツリービューの再表示
                RefreshTreeView();

                //ユーザ名コンボボックスの設定
                combo_set();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("表示エラー" + ex.Message, "表示エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //検索コンボボックスを設定する
        private void combo_set()
        {

            //ユーザ名コンボボックスの設定
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

            //下から順に取りにいく
            Class_common common = new Class_common();
            var con = common.DB_connection();

            Class_Detaget getuser = new Class_Detaget();
            userDSList = getuser.getUserList();

            //ユーザ情報を読み込む
            foreach (userDS v in userDSList)
            {

                TreeNode NodeUser = new TreeNode(v.username,0,0);
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
                    TreeNode NodeSystem = new TreeNode(s.systemname,1,1);
                    NodeSystem.ToolTipText = s.systemno;

                    //拠点名
                    List<siteDS> siteDSListsub = getuser.getSiteList(s.systemno, true);
                    //リストに入れる
                    if (siteDSList != null)
                        siteDSList.AddRange(siteDSListsub);
                    else
                        siteDSList = siteDSListsub;

                    foreach (siteDS si in siteDSListsub)
                    {
                        TreeNode NodeSite = new TreeNode(si.sitename, 2, 2);
                        NodeSite.ToolTipText = si.siteno;

                        NodeSystem.Nodes.Add(NodeSite);
                    }

                    NodeUser.Nodes.Add(NodeSystem);
                }


                // 最上位階層に対してまとめて項目（ノード）を追加
                    treeView1.Nodes.Add(NodeUser);
                
            }

        /*            TreeNode treeNodeFruits = new TreeNode("システム1");
                    TreeNode treeNodeVegetables = new TreeNode("システム2");
                    TreeNode[] treeNodeSubFolder =
                        { treeNodeFruits, treeNodeVegetables};

                    // 下位階層に対してまとめて項目（ノード）を追加
                    TreeNode treeNodeFood =
                        new TreeNode("ユーザ1", treeNodeSubFolder);

                    TreeNode treeNodeDrink = new TreeNode("ユーザ2");
                    TreeNode[] treeNodeRoot = { treeNodeFood, treeNodeDrink };

                    // 最上位階層に対してまとめて項目（ノード）を追加
                    treeView1.Nodes.AddRange(treeNodeRoot);

                    treeView1.TopNode.Expand();
        */
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
            if(site_list.Rows.Count > 0) { 
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
            if (interface_list.Rows.Count > 0) { 
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
                    Convert.ToString(row[11])

                });
            }
        }
        
        //回線情報一覧
        void kaisenList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            //	e.Item = _item[e.ItemIndex];
            if (kasen_list.Rows.Count > 0) { 
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
                            Convert.ToString(row[9])

                });
    
            }
        }
        //作業予定
        void sagyoList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
        //	e.Item = _item[e.ItemIndex];
        if (sagyo_list.Rows.Count > 0)
        {
            DataRow row = this.sagyo_list.Rows[e.ItemIndex];
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

        //登録画面
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form_UserInsert useraddfrom = new Form_UserInsert();
            useraddfrom.Show();

        }
        //管理メニューの表示
        private void m_tourokuBtn_Click(object sender, EventArgs e)
        {

            Form_kanri_menu kanrimenu = new Form_kanri_menu();
            kanrimenu.ShowDialog();
        }

        
        //システム情報登録
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            Form_systemInsert systemdlg = new Form_systemInsert();
            if(userDSList != null)
                systemdlg.userList = userDSList;

            systemdlg.Show();
        }

        //拠点登録
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form_siteInsert sitefm = new Form_siteInsert();
            if (userDSList != null)
                sitefm.userList = userDSList;
            if (systemDSList != null)
                sitefm.systemList = systemDSList;

            sitefm.Show();

        }
        //ホスト登録
        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form_hostInsert hostfm = new Form_hostInsert();
            if (userDSList != null)
                hostfm.userList = userDSList;
            if (systemDSList != null)
                hostfm.systemList = systemDSList;
            if (siteDSList != null)
                hostfm.siteList = siteDSList;

            hostfm.Show();

        }
        //回線情報登録
        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form_kaisenInsert kaisenfm = new Form_kaisenInsert();
            if (userDSList != null)
                kaisenfm.userList = userDSList;
            if (systemDSList != null)
                kaisenfm.systemList = systemDSList;
            if (siteDSList != null)
                kaisenfm.siteList = siteDSList;
            kaisenfm.Show();
        }
        //監視インターフェイス
        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form_kanshiInterfaceInsert kanshiInt = new Form_kanshiInterfaceInsert();
            if (userDSList != null)
                kanshiInt.userList = userDSList;
            if (systemDSList != null)
                kanshiInt.systemList = systemDSList;
            if (siteDSList != null)
                kanshiInt.siteList = siteDSList;


            kanshiInt.Show();

        }
        //インシデント登録
        private void button3_Click(object sender, EventArgs e)
        {
            Form_IncidentInsert incidentfm = new Form_IncidentInsert();
            incidentfm.Show();
        }
        //作業情報登録
        private void linkLabel7_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form_sagyoInsert sagyofm = new Form_sagyoInsert();
            if (userDSList != null)
                sagyofm.userList = userDSList;

            sagyofm.Show();

        }

        //ユーザ一覧の表示
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
            this.userList.Columns.Insert(2, "ユーザ名", 180, HorizontalAlignment.Left);
            this.userList.Columns.Insert(3, "ユーザ名カナ", 30, HorizontalAlignment.Left);
            this.userList.Columns.Insert(4, "ユーザ名略称", 30, HorizontalAlignment.Left);
            this.userList.Columns.Insert(5, "レポート出力有無", 80, HorizontalAlignment.Left);
            this.userList.Columns.Insert(6, "備考", 180, HorizontalAlignment.Left);
            this.userList.Columns.Insert(7, "更新日時", 80, HorizontalAlignment.Left);
            this.userList.Columns.Insert(8, "更新者", 180, HorizontalAlignment.Left);

            //リストビューを初期化する
            user_list = new DataTable("table2");
            user_list.Columns.Add("No", Type.GetType("System.Int32"));
            user_list.Columns.Add("有効", Type.GetType("System.String"));
            user_list.Columns.Add("ユーザ名", Type.GetType("System.String"));
            user_list.Columns.Add("ユーザ名カナ", Type.GetType("System.String"));
            user_list.Columns.Add("ユーザ名略称", Type.GetType("System.String"));
            user_list.Columns.Add("レポート出力有無", Type.GetType("System.String"));
            user_list.Columns.Add("備考", Type.GetType("System.String"));
            user_list.Columns.Add("更新日時", Type.GetType("System.String"));
            user_list.Columns.Add("更新者", Type.GetType("System.String"));


            //データの挿入
            if (dsp_L.user_L != null)
            {
                foreach (userDS v in dsp_L.user_L)
                {
                    DataRow urow = user_list.NewRow();
                    urow["No"] = v.userno;
                    urow["有効"] = v.status;
                    urow["ユーザ名"] = v.username;
                    urow["ユーザ名カナ"] = v.username_kana;
                    urow["ユーザ名略称"] = v.username_sum;
                    urow["レポート出力有無"] = v.report_status;
                    urow["備考"] = v.biko;
                    urow["更新日時"] = v.chk_date;
                    urow["更新者"] = v.chk_name_id;
                    user_list.Rows.Add(urow);


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
                foreach (systemDS sys in dsp_L.system_L)
                {
                    DataRow row = system_list.NewRow();
                    row["No"] = sys.systemno;
                    row["システム名"] = sys.systemname;
                    row["システム名カナ"] = sys.systemkana;
                    row["備考"] = sys.biko;
                    row["更新日時"] = sys.chk_date;
                    row["更新者"] = sys.chk_name_id;
                    system_list.Rows.Add(row);

                }
 
                    this.systemList.VirtualListSize = system_list.Rows.Count;
                    this.systemList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            }
        }
        //拠点情報一覧の表示
        private void disp_site(DISP_dataSet dsp_L)
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
            site_list.Columns.Add("用途", Type.GetType("System.String"));
            site_list.Columns.Add("備考", Type.GetType("System.String"));
            site_list.Columns.Add("更新日時", Type.GetType("System.String"));
            site_list.Columns.Add("更新者", Type.GetType("System.String"));

            //拠点情報
            if (dsp_L.site_L != null)
            {
                foreach (siteDS s in dsp_L.site_L)
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

                    this.siteList.VirtualListSize = site_list.Rows.Count;
                    this.siteList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            }
            
        }


        //ホスト
        private void disp_host(DISP_dataSet dsp_L)
        {
            //機器
            this.hostList.VirtualMode = true;
            // １行全体選択
            this.hostList.FullRowSelect = true;
            this.hostList.HideSelection = false;
            this.hostList.HeaderStyle = ColumnHeaderStyle.Clickable;
            //Hook up handlers for VirtualMode events.
            this.hostList.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(hostList_RetrieveVirtualItem);
            this.hostList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            // Column追加
            this.hostList.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.hostList.Columns.Insert(1, "有効", 30, HorizontalAlignment.Left);
            this.hostList.Columns.Insert(2, "ホスト名(英数)", 180, HorizontalAlignment.Left);
            this.hostList.Columns.Insert(3, "ホスト名(日本語)", 180, HorizontalAlignment.Left);
            this.hostList.Columns.Insert(4, "機種", 30, HorizontalAlignment.Left);
            this.hostList.Columns.Insert(5, "設置場所", 80, HorizontalAlignment.Left);
            this.hostList.Columns.Insert(6, "用途", 180, HorizontalAlignment.Left);
            this.hostList.Columns.Insert(7, "監視開始日時", 180, HorizontalAlignment.Left);
            this.hostList.Columns.Insert(8, "監視終了日時", 180, HorizontalAlignment.Left);
            this.hostList.Columns.Insert(9, "保守管理番号", 180, HorizontalAlignment.Left);
            this.hostList.Columns.Insert(10, "保守情報", 180, HorizontalAlignment.Left);
            this.hostList.Columns.Insert(11, "備考", 180, HorizontalAlignment.Left);
            this.hostList.Columns.Insert(12, "更新日時", 80, HorizontalAlignment.Left);
            this.hostList.Columns.Insert(13, "更新者", 180, HorizontalAlignment.Left);

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

                foreach (hostDS h in dsp_L.host_L)
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


                    this.hostList.VirtualListSize = host_list.Rows.Count;
                    this.hostList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
       
            }

        }
        //インターフェイス
        private void disp_interface(DISP_dataSet dsp_L)
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
            this.interfaceList.Columns.Insert(10, "更新日時", 80, HorizontalAlignment.Left);
            this.interfaceList.Columns.Insert(11, "更新者", 80, HorizontalAlignment.Left);

            //リストビューを初期化する
            interface_list = new DataTable("table6");
            interface_list.Columns.Add("No", Type.GetType("System.Int32"));
            interface_list.Columns.Add("有効", Type.GetType("System.String"));
            interface_list.Columns.Add("インターフェイス名", Type.GetType("System.String"));
            interface_list.Columns.Add("監視タイプ", Type.GetType("System.String"));
            interface_list.Columns.Add("監視項目名", Type.GetType("System.String"));
            interface_list.Columns.Add("監視開始日時", Type.GetType("System.String"));
            interface_list.Columns.Add("監視終了日時", Type.GetType("System.String"));
            interface_list.Columns.Add("閾値", Type.GetType("System.String"));
            interface_list.Columns.Add("IPアドレス", Type.GetType("System.String"));
            interface_list.Columns.Add("IPアドレス(NAT)", Type.GetType("System.String"));
            interface_list.Columns.Add("更新日時", Type.GetType("System.String"));
            interface_list.Columns.Add("更新者", Type.GetType("System.String"));
            //インターフェイス情報
            if (dsp_L.watch_L != null)
            {
                foreach (watch_InterfaceDS w in dsp_L.watch_L)
                {
                    DataRow row = interface_list.NewRow();
                    row["No"] = w.kennshino;
                    row["有効"] = w.status;
                    row["インターフェイス名"] = w.interfacename;
                    row["監視タイプ"] = w.type;
                    row["監視項目名"] = w.kanshi;
                    row["監視開始日時"] = w.start_date;
                    row["監視終了日時"] = w.end_date;
                    row["閾値"] = w.end_date;
                    row["IPアドレス"] = w.IPaddress;
                    row["IPアドレス(NAT)"] = w.IPaddressNAT;
                    row["更新日時"] = w.chk_date;
                    row["更新者"] = w.chk_name_id;
                    interface_list.Rows.Add(row);
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
            this.kaisenList.Columns.Insert(8, "更新日時", 80, HorizontalAlignment.Left);
            this.kaisenList.Columns.Insert(9, "更新者", 80, HorizontalAlignment.Left);

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
            kasen_list.Columns.Add("更新日時", Type.GetType("System.String"));
            kasen_list.Columns.Add("更新者", Type.GetType("System.String"));
            //回線情報
            if (dsp_L.kaisen_L != null)
            {

                foreach (kaisenDS ka in dsp_L.kaisen_L)
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
                    row["更新日時"] = ka.chk_date;
                    row["更新者"] = ka.chk_name_id;
                    kasen_list.Rows.Add(row);
                }
                
                this.kaisenList.VirtualListSize = kasen_list.Rows.Count;
                this.kaisenList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                
            }
        }

        //作業情報
        private void disp_sagyo(DISP_dataSet dsp_L)
        {
            //作業情報
            this.sagyoList.VirtualMode = true;
            // １行全体選択
            this.sagyoList.FullRowSelect = true;
            this.sagyoList.HideSelection = false;
            this.sagyoList.HeaderStyle = ColumnHeaderStyle.Clickable;
            //Hook up handlers for VirtualMode events.
            this.sagyoList.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(sagyoList_RetrieveVirtualItem);
            this.sagyoList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            // Column追加
            this.sagyoList.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.sagyoList.Columns.Insert(1, "作業内容", 30, HorizontalAlignment.Left);
            this.sagyoList.Columns.Insert(2, "開始日時", 180, HorizontalAlignment.Left);
            this.sagyoList.Columns.Insert(3, "終了日時", 30, HorizontalAlignment.Left);
            this.sagyoList.Columns.Insert(4, "更新日時", 80, HorizontalAlignment.Left);
            this.sagyoList.Columns.Insert(5, "更新者", 80, HorizontalAlignment.Left);

            //リストビューを初期化する
            sagyo_list = new DataTable("table8");
            sagyo_list.Columns.Add("No", Type.GetType("System.Int32"));
            sagyo_list.Columns.Add("作業内容", Type.GetType("System.String"));
            sagyo_list.Columns.Add("開始日時", Type.GetType("System.String"));
            sagyo_list.Columns.Add("終了日時", Type.GetType("System.String"));
            sagyo_list.Columns.Add("更新日時", Type.GetType("System.String"));
            sagyo_list.Columns.Add("更新者", Type.GetType("System.String"));

            //作業情報
            if (dsp_L.sagyo_L != null)
            {
                foreach (sagyoDS sa in dsp_L.sagyo_L)
                {
                    DataRow row = sagyo_list.NewRow();
                    row["No"] = sa.taskno;
                    row["作業内容"] = sa.text;
                    row["開始日時"] = sa.start_date;
                    row["終了日時"] = sa.end_date;
                    row["更新日時"] = sa.chk_date;
                    row["更新者"] = sa.chk_name_id;
                    sagyo_list.Rows.Add(row);

                }

                //リストビューの更新
                this.sagyoList.VirtualListSize = sagyo_list.Rows.Count;
                //リストビューのカラムサイズの変更
                this.sagyoList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            }
        }

        //検索ボタンが押されたとき
        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = false;
            try { 
                userList.Clear();
                systemList.Clear();
                siteList.Clear();
                hostList.Clear();
                interfaceList.Clear();
                kaisenList.Clear();
                sagyoList.Clear();


                Dictionary<string, string> param_dict = new Dictionary<string, string>();
                Class_common common = new Class_common();
                var con = common.DB_connection();

                Class_Detaget getdata = new Class_Detaget();

                DISP_dataSet dsp_L = new DISP_dataSet();

                //IPアドレス
                if (m_ipaddress.Text != "")
                {
                    param_dict["IPaddress"] = m_ipaddress.Text;
                    dsp_L = getdata.getSelectInterface(param_dict, con);

                }

                //ホスト名コンボボックス
                if (m_hostCombo.Text != "")
                {
                    param_dict["hostname"] = m_hostCombo.Text;
                    dsp_L = getdata.getSelectHost(param_dict, con);
                }
                //拠点コンボボックス
                if (m_siteCombo.Text != "")
                {
                    param_dict["sitename"] = m_siteCombo.Text;
                    dsp_L = getdata.getSelectSite(param_dict, con);

                }
                //システム名コンボボックス
                if (m_systemCombo.Text != "")
                {
                    param_dict["systemname"] = m_systemCombo.Text;
                    dsp_L = getdata.getSelectSystem(param_dict, con);
                }
                //ユーザ名コンボボックス
                if (m_usernameCombo.Text != "")
                {
                    param_dict["username"] = m_usernameCombo.Text;
                    dsp_L = getdata.getSelectUser(param_dict, con);

                }

                //ユーザ情報の表示
                disp_User( dsp_L);

                disp_system(dsp_L);

                disp_site(dsp_L);
            
                disp_host(dsp_L);

                disp_interface(dsp_L);

                //回線情報を取得
                if(dsp_L.user_L != null)
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
                    dataget.getSelectSagyoInfo(user_list, dsp_L, con);        
                   disp_sagyo(dsp_L);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("構成情報の表示時にエラーが発生しました。" + ex.Message, "構成情報表示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            finally
            {
                this.button1.Enabled = true;
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
                String strsiteNo;
                //拠点の場合はホストの取得を行う
                if(e.Node.Level == 2)
                {
                    //拠点NOを取得 ツールチップに拠点Noを入れている
                    strsiteNo = e.Node.ToolTipText;

                    //拠点番号が取れないもしくは既に取得済みのときはとらない。
                    if(strsiteNo != "" && e.Node.Nodes.Count == 0)
                    {
                        Class_Detaget getuser = new Class_Detaget();
                        
                        //ホスト名を検索
                        List<hostDS> hostDSListsub = getuser.getHostList(strsiteNo, true);

                        //リストに入れる
                        if (hostDSList != null)
                            hostDSList.AddRange(hostDSListsub);
                        else
                            hostDSList = hostDSListsub;
                        
                        //ホスト名を設定する
                        foreach (hostDS ho in hostDSListsub)
                        {   
                            TreeNode NodeHost = new TreeNode(ho.hostname, 3, 3);
                            NodeHost.ToolTipText = ho.host_no;

                            e.Node.Nodes.Add(NodeHost);
                            e.Node.Expand();
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
            if(hostDSList != null) hostDSList.Clear();

            RefreshTreeView();
        }

        //ユーザ名コンボボックスが変更されたとき
        private void m_usernameCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //システム名コンボボックス情報を読み込み
            Read_systemCombo();

        }
        //ユーザ名のコンボボックスが変更されたときの処理
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

                    //ユーザNOで区別する
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
        //システム名コンボが変更されたとき
        private void m_systemCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //拠点情報を読み込む
            Read_siteCombo();

        }

        //拠点名が変更された時
        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string selectedIndex = null;

            if(m_siteCombo.SelectedValue != null)
            {
                selectedIndex  = m_siteCombo.SelectedValue.ToString();
            }

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
                List<hostDS> hostDSList1 = getuser.getHostList(siteno, true);
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
        private void Read_siteCombo()
        {
            try
            {
                m_siteCombo.DataSource = null;
                m_hostCombo.DataSource = null;

                //コンボボックス
                DataTable siteTable = new DataTable();
                siteTable.Columns.Add("ID", typeof(string));
                siteTable.Columns.Add("NAME", typeof(string));

                //システム情報を取得する
                if (siteDSList.Count <= 0)
                    return;

                int i = 0;

                //拠点情報を取得する
                foreach (siteDS v in siteDSList)
                {
                    //システム番号が空白のときはなにもしない
                    if (m_systemCombo.SelectedValue != null)
                    {

                        if (i == 0)
                            siteTable.Rows.Add("");

                        if (v.systemno == m_systemCombo.SelectedValue.ToString())
                        {
                            DataRow row = siteTable.NewRow();
                            row["ID"] = v.siteno;
                            row["NAME"] = v.sitename;
                            siteTable.Rows.Add(row);
                        }
                    }
                    i++;
                }
                //データテーブルを割り当てる
                m_siteCombo.DataSource = siteTable;
                m_siteCombo.DisplayMember = "NAME";     
                m_siteCombo.ValueMember = "ID";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "システム情報読込", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void comboBox4_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }
        //条件クリアボタン
        private void m_clear_btn_Click(object sender, EventArgs e)
        {
            m_usernameCombo.DataSource = null;
            m_systemCombo.DataSource = null;
            m_siteCombo.DataSource = null;
            m_hostCombo.DataSource = null;
            m_ipaddress.Text = "";


        }
    }
}
