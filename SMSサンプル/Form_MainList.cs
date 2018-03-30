using log4net;
using Microsoft.VisualBasic.FileIO;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace moss_AP
{
    public partial class Form_MainList : Form
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

        //インシデント情報
        DataTable incident_sagyo_list;

        //作業情報
        DataTable teiki_sagyo_list;

        //作業情報
        DataTable keikaku_sagyo_list;

        //特別対応
        DataTable tokubetu_sagyo_list;

        //テンプレート
        public List<templeteDS> templList { get; set; }

        //システム
        public templeteDS templetedt { get; set; }

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

        List<taskDS> schDSList;
        List<taskDS> scheduleList_incident;
        List<taskDS> scheduleList_keikaku;
        List<taskDS> scheduleList_teiki;
        List<taskDS> scheduleList_tokubetu;



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
                logger.InfoFormat("ログイン オペレータ名：{0} ", loginDS.lastname + loginDS.fastname);

                Class_common common = new Class_common();
                con = common.DB_connection();

                //ツリービューの再表示
                RefreshTreeView();

                //インシデント一覧データを取得
                Class_Detaget dg_class = new Class_Detaget();
                //List<incidentDS> incidentList ;
 //               incidentDSList = dg_class.getOpenIncident(con);
                //インシデント一覧を表示
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
                logger.ErrorFormat("表示エラー：{0}", ex.Message);
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

        //全ノードをループし、展開されているノードを取得する
        private void PrintRecuesive(TreeNode treeNode,List<TreeNode> expand_array)
        {
            //System.Diagnostics.Debug.WriteLine(treeNode.Text);
            

            TreeNode nd;
            if (treeNode.IsExpanded)
            {

                nd = treeNode;

                expand_array.Add(nd);
            }
            foreach (TreeNode tn in treeNode.Nodes)
            {
                PrintRecuesive(tn, expand_array);
            }

        }
        private void CallRecursive(TreeView treeView, List<TreeNode> expand_array)
        {
            TreeNodeCollection nodes = treeView.Nodes;
            foreach ( TreeNode n in nodes)
            {
                PrintRecuesive(n, expand_array);
            }
        }

        private void PrintRecuesiveExpand(TreeNode treeNode, string nodeText)
        {
            TreeNode nd;

            if (nodeText == treeNode.Text)
            {
                nd = treeNode;
                nd.Expand();
            }
            foreach (TreeNode tn in treeNode.Nodes)
            {
                PrintRecuesiveExpand(tn, nodeText);
            }

        }
        //すべて展開
        private void CallRecursiveExpand(TreeView treeView, string nodeText)
        {
            TreeNodeCollection nodes = treeView.Nodes;
            foreach (TreeNode n in nodes)
            {
                PrintRecuesiveExpand(n, nodeText);
            }
        }


        // TreeViewコントロールのデータを更新します。
        private void RefreshTreeView()
        {

            treeView1.Nodes.Clear();
            treeView1.ImageList = this.imageList1;
            treeView1.ImageIndex = 0;
            treeView1.SelectedImageIndex = 0;
            if (schDSList != null) schDSList.Clear();

            Class_Detaget getuser = new Class_Detaget();
            getuser.con = con;
            userDSList = getuser.getUserList();
            //カスタマ情報を読み込む
            foreach (userDS v in userDSList)
            {
                if(v.status == "無効")
                    continue;               

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
//                foreach (systemDS s in systemDSListsub)
//                {
//                    TreeNode NodeSystem = new TreeNode(s.systemname, 1, 1);
//                    NodeSystem.ToolTipText = s.systemno;

                    TreeNode NodeTimer1 = new TreeNode("インシデント", 2, 2);
                    NodeTimer1.ToolTipText = "1";
                    NodeUser.Nodes.Add(NodeTimer1);
                    TreeNode NodeTimer2 = new TreeNode("定期作業", 2, 2);
                    NodeTimer2.ToolTipText = "2";
                    NodeUser.Nodes.Add(NodeTimer2);
                    TreeNode NodeTimer3 = new TreeNode("計画作業", 2, 2);
                    NodeTimer3.ToolTipText = "3";
                    NodeUser.Nodes.Add(NodeTimer3);

                    TreeNode NodeTimer4 = new TreeNode("特別対応", 2, 2);
                    NodeTimer4.ToolTipText = "4";
                    NodeUser.Nodes.Add(NodeTimer4);

                    Dictionary<string, string> param_dict = new Dictionary<string, string>();


                    param_dict["userno"] = v.userno;
                //                    param_dict["userno"] = s.userno;

                //スケジュール情報を取得する
                List<taskDS> taskDSListsub = getuser.getTimerList(param_dict, con);

                    //特別対応のみ別に取得する
                    List<taskDS> tokubetu_list = getuser.gettokubetulist(param_dict, con);

                    //後ろに特別対応を結合する
                    taskDSListsub.AddRange(tokubetu_list);

                    //リストに入れる
                    if (schDSList != null)
                        schDSList.AddRange(taskDSListsub);
                    else
                        schDSList = taskDSListsub;

                    // 現在時刻
                    DateTime nowdate = DateTime.Now;

                    foreach (taskDS si in taskDSListsub)
                    {
                        TreeNode NodeTimerDetail;
                        DateTime dt1= new DateTime();
                        if (si.schedule_type != "4")
                            dt1 = DateTime.Parse(si.enddate);

                        //有効でかつ期間内
                        if (si.status == "有効" && si.startdate != "" && dt1 > nowdate)
                        {
                            NodeTimerDetail = new TreeNode(si.naiyou, 6, 6);
                            NodeTimerDetail.ToolTipText = si.schedule_no;

                            NodeUser.ForeColor = Color.Red;
                            NodeTimerDetail.ForeColor = Color.Red;

                            //インシデントの時
                            if (si.schedule_type == "1")
                            {
                                NodeTimerDetail.Text =  NodeTimerDetail.Text;
                                NodeTimer1.Parent.ForeColor = Color.Red;
                                NodeTimer1.ForeColor = Color.Red;
                                NodeTimer1.Nodes.Add(NodeTimerDetail);
                                //scheduleDS ds = new scheduleDS();

                            }
                            //計画作業
                            else if (si.schedule_type == "2")
                            {
                                NodeTimer2.Parent.ForeColor = Color.Red;
                                NodeTimer2.ForeColor = Color.Red;
                                NodeTimer2.Nodes.Add(NodeTimerDetail);
                            }
                            //定期作業
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
                        else if(si.status == "有効")
                        {
                            NodeTimerDetail = new TreeNode(si.naiyou, 5, 5);
                            NodeTimerDetail.ToolTipText = si.schedule_no;

                            //インシデントの時
                            if (si.schedule_type == "1")
                            {
                                NodeTimerDetail.Text =  NodeTimerDetail.Text;
                                NodeTimer1.Nodes.Add(NodeTimerDetail);
                            }
                            //計画作業
                            else if (si.schedule_type == "2")
                            {
                                NodeTimer2.Nodes.Add(NodeTimerDetail);
                            }
                            //定期作業
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

                    //NodeUser.Nodes.Add(NodeSystem);
                //}

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
                    Convert.ToString(row[8]),
                    Convert.ToString(row[9])

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
                            Convert.ToString(row[13]),
                            Convert.ToString(row[14]),
                            Convert.ToString(row[15]),
                            Convert.ToString(row[16]),
                            Convert.ToString(row[17])

                });

            }
        }
        //インシデント一覧
        void m_incident_List_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            //	e.Item = _item[e.ItemIndex];
            if (this.incident_sagyo_list.Rows.Count > 0)
            {
                DataRow row = this.incident_sagyo_list.Rows[e.ItemIndex];
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
                Convert.ToString(row[11]),
                Convert.ToString(row[12])
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
                Convert.ToString(row[11]),
                Convert.ToString(row[12])
                    });
            }

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
            Form_scheduleInsert sagyofm = new Form_scheduleInsert();
            sagyofm.loginDS = loginDS;
            if (userDSList != null)
                sagyofm.userList = userDSList;

            sagyofm.Show();

        }

        //作業一覧を表示する
        private void disp_sagyoList(List<taskDS> scheduleList)
        {
            scheduleList_incident = new List<taskDS>();
            scheduleList_keikaku = new List<taskDS>();
            scheduleList_teiki = new List<taskDS>();
            scheduleList_tokubetu = new List<taskDS>();

            //
            if (scheduleList != null)
            { 
                foreach (taskDS schedata in scheduleList)
                {

                    //1:インシデントタスク 2:定期 3:作業 4:特別 5:サブタスク
                    if (schedata.schedule_type == "1")
                        scheduleList_incident.Add(schedata);
                    if (schedata.schedule_type == "2")
                        scheduleList_teiki.Add(schedata);
                    else if (schedata.schedule_type == "3")
                        scheduleList_keikaku.Add(schedata);
                    else if (schedata.schedule_type == "4")
                        scheduleList_tokubetu.Add(schedata);

                }
            }
            disp_incident();
            disp_teiki_list();
            disp_scheduleList_keikaku();
            disp_tokubetu_list();
            
        }
        


        //インシデントの表示
        private void disp_incident()
        {

            this.m_incident_List.VirtualMode = true;
            // １行全体選択
            this.m_incident_List.FullRowSelect = true;
            this.m_incident_List.HideSelection = false;
            this.m_incident_List.HeaderStyle = ColumnHeaderStyle.Clickable;
            //Hook up handlers for VirtualMode events.
            this.m_incident_List.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(m_incident_List_RetrieveVirtualItem);
            this.m_incident_List.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.m_incident_List.Scrollable = true;

            // Column追加
            this.m_incident_List.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(1, "ステータス", 30, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(2, "カスタマ通番", 50, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(3, "カスタマ名", 50, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(4, "内容", 50, HorizontalAlignment.Left);

            this.m_incident_List.Columns.Insert(5, "開始日時", 110, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(6, "終了日時", 110, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(7, "備考", 110, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(8, "登録日時", 180, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(9, "登録者", 80, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(10, "更新日時", 80, HorizontalAlignment.Left);
            this.m_incident_List.Columns.Insert(11, "更新者", 80, HorizontalAlignment.Left);

            //リストビューを初期化する
            incident_sagyo_list = new DataTable("table12");
            incident_sagyo_list.Columns.Add("No", Type.GetType("System.Int32"));
            incident_sagyo_list.Columns.Add("ステータス", Type.GetType("System.String"));
            incident_sagyo_list.Columns.Add("カスタマ通番", Type.GetType("System.String"));
            incident_sagyo_list.Columns.Add("カスタマ名", Type.GetType("System.String"));
            incident_sagyo_list.Columns.Add("内容", Type.GetType("System.String"));
            incident_sagyo_list.Columns.Add("開始日時", Type.GetType("System.String"));
            incident_sagyo_list.Columns.Add("終了日時", Type.GetType("System.String"));
            incident_sagyo_list.Columns.Add("備考", Type.GetType("System.String"));
            incident_sagyo_list.Columns.Add("登録日時", Type.GetType("System.String"));
            incident_sagyo_list.Columns.Add("登録者", Type.GetType("System.String"));
            incident_sagyo_list.Columns.Add("更新日時", Type.GetType("System.String"));
            incident_sagyo_list.Columns.Add("更新者", Type.GetType("System.String"));

            //データの挿入
            if (scheduleList_incident != null)
            {
                foreach (taskDS v in scheduleList_incident)
                {
                    //チェックボックスがOFFになっている場合は表示しない
                    if (this.m_incident_umu_check.Checked == false && v.status == "無効")
                        continue;


                    //if (fastline == v.incident_no)
                    //    continue;

                    DataRow urow = incident_sagyo_list.NewRow();

                    urow["No"] = v.schedule_no;
                    urow["ステータス"] = v.status;
                    urow["カスタマ通番"] = v.userno;
                    urow["カスタマ名"] = v.username;
                    urow["内容"] = v.naiyou;
                    //urow["テンプレートNO"] = v.templeteno;

                    //1:インシデント処理 2:定期作業業務促し 3:計画作業 4:特別対応
                    //if (v.schedule_type == "1")
                    //    urow["予定区分"] = "インシデント処理";
                    //else if (v.schedule_type == "2")
                    //    urow["予定区分"] = "定期作業";
                    //else if (v.schedule_type == "3")
                    //    urow["予定区分"] = "計画作業";
                    //else if (v.schedule_type == "4")
                    //    urow["予定区分"] = "特別対応";


                    urow["開始日時"] = v.startdate;
                    urow["終了日時"] = v.enddate;
                    urow["備考"] = v.biko;
                    urow["登録日時"] = v.ins_date;
                    urow["登録者"] = v.ins_name_id;
                    urow["更新日時"] = v.chk_date;
                    urow["更新者"] = v.chk_name_id;
                    incident_sagyo_list.Rows.Add(urow);
                }
                //件数を書き込む
                this.m_incident_count.Text = incident_sagyo_list.Rows.Count.ToString() + "件";

                this.m_incident_List.VirtualListSize = incident_sagyo_list.Rows.Count;
                this.m_incident_List.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            }

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
            this.m_teiki_List.Columns.Insert(1, "ステータス", 30, HorizontalAlignment.Left);
            this.m_teiki_List.Columns.Insert(2, "カスタマ通番", 50, HorizontalAlignment.Left);
            this.m_teiki_List.Columns.Insert(3, "カスタマ名", 50, HorizontalAlignment.Left);
            this.m_teiki_List.Columns.Insert(4, "内容", 50, HorizontalAlignment.Left);

            this.m_teiki_List.Columns.Insert(5, "開始日時", 110, HorizontalAlignment.Left);
            this.m_teiki_List.Columns.Insert(6, "終了日時", 110, HorizontalAlignment.Left);
            this.m_teiki_List.Columns.Insert(7, "備考", 110, HorizontalAlignment.Left);
            this.m_teiki_List.Columns.Insert(8, "登録日時", 180, HorizontalAlignment.Left);
            this.m_teiki_List.Columns.Insert(9, "登録者", 80, HorizontalAlignment.Left);
            this.m_teiki_List.Columns.Insert(10, "更新日時", 80, HorizontalAlignment.Left);
            this.m_teiki_List.Columns.Insert(11, "更新者", 80, HorizontalAlignment.Left);

            //リストビューを初期化する
            teiki_sagyo_list = new DataTable("table12");
            teiki_sagyo_list.Columns.Add("No", Type.GetType("System.Int32"));
            teiki_sagyo_list.Columns.Add("ステータス", Type.GetType("System.String"));
            teiki_sagyo_list.Columns.Add("カスタマ通番", Type.GetType("System.String"));
            teiki_sagyo_list.Columns.Add("カスタマ名", Type.GetType("System.String"));
            teiki_sagyo_list.Columns.Add("内容", Type.GetType("System.String"));
            teiki_sagyo_list.Columns.Add("開始日時", Type.GetType("System.String"));
            teiki_sagyo_list.Columns.Add("終了日時", Type.GetType("System.String"));
            teiki_sagyo_list.Columns.Add("備考", Type.GetType("System.String"));
            teiki_sagyo_list.Columns.Add("登録日時", Type.GetType("System.String"));
            teiki_sagyo_list.Columns.Add("登録者", Type.GetType("System.String"));
            teiki_sagyo_list.Columns.Add("更新日時", Type.GetType("System.String"));
            teiki_sagyo_list.Columns.Add("更新者", Type.GetType("System.String"));


            //データの挿入
            if (scheduleList_teiki != null)
            {
                foreach (taskDS v in scheduleList_teiki)
                {
                    //チェックボックスがOFFになっている場合は表示しない
                    if (this.m_teiki_umu_check.Checked == false && v.status == "無効")
                        continue;


                    //if (fastline == v.incident_no)
                    //    continue;

                    DataRow urow = teiki_sagyo_list.NewRow();

                    urow["No"] = v.schedule_no;
                    urow["ステータス"] = v.status;
                    urow["カスタマ通番"] = v.userno;
                    urow["カスタマ名"] = v.username;
                    urow["内容"] = v.naiyou;
                    //urow["テンプレートNO"] = v.templeteno;

                    //1:インシデント処理 2:定期作業業務促し 3:計画作業 4:特別対応
                    //if (v.schedule_type == "1")
                    //    urow["予定区分"] = "インシデント処理";
                    //else if (v.schedule_type == "2")
                    //    urow["予定区分"] = "定期作業";
                    //else if (v.schedule_type == "3")
                    //    urow["予定区分"] = "計画作業";
                    //else if (v.schedule_type == "4")
                    //    urow["予定区分"] = "特別対応";


                    urow["開始日時"] = v.startdate;
                    urow["終了日時"] = v.enddate;
                    urow["備考"] = v.biko;
                    urow["登録日時"] = v.ins_date;
                    urow["登録者"] = v.ins_name_id;
                    urow["更新日時"] = v.chk_date;
                    urow["更新者"] = v.chk_name_id;
                    teiki_sagyo_list.Rows.Add(urow);
                }
                //件数を書き込む
                this.m_teiki_count.Text = teiki_sagyo_list.Rows.Count.ToString() + "件";

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
            this.m_keikaku_list.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_keikaku_list.Columns.Insert(1, "ステータス", 30, HorizontalAlignment.Left);
            this.m_keikaku_list.Columns.Insert(2, "カスタマ通番", 50, HorizontalAlignment.Left);
            this.m_keikaku_list.Columns.Insert(3, "カスタマ名", 50, HorizontalAlignment.Left);
            this.m_keikaku_list.Columns.Insert(4, "内容", 50, HorizontalAlignment.Left);

            this.m_keikaku_list.Columns.Insert(5, "テンプレートNO", 180, HorizontalAlignment.Left);
            this.m_keikaku_list.Columns.Insert(6, "開始日時", 110, HorizontalAlignment.Left);
            this.m_keikaku_list.Columns.Insert(7, "終了日時", 110, HorizontalAlignment.Left);
            this.m_keikaku_list.Columns.Insert(8, "備考", 110, HorizontalAlignment.Left);
            this.m_keikaku_list.Columns.Insert(9, "登録日時", 180, HorizontalAlignment.Left);
            this.m_keikaku_list.Columns.Insert(10, "登録者", 80, HorizontalAlignment.Left);
            this.m_keikaku_list.Columns.Insert(11, "更新日時", 80, HorizontalAlignment.Left);
            this.m_keikaku_list.Columns.Insert(12, "更新者", 80, HorizontalAlignment.Left);

            //リストビューを初期化する
            keikaku_sagyo_list = new DataTable("table12");
            keikaku_sagyo_list.Columns.Add("No", Type.GetType("System.Int32"));
            keikaku_sagyo_list.Columns.Add("ステータス", Type.GetType("System.String"));
            keikaku_sagyo_list.Columns.Add("カスタマ通番", Type.GetType("System.String"));
            keikaku_sagyo_list.Columns.Add("カスタマ名", Type.GetType("System.String"));
            keikaku_sagyo_list.Columns.Add("内容", Type.GetType("System.String"));
            keikaku_sagyo_list.Columns.Add("テンプレートNO", Type.GetType("System.String"));
            keikaku_sagyo_list.Columns.Add("開始日時", Type.GetType("System.String"));
            keikaku_sagyo_list.Columns.Add("終了日時", Type.GetType("System.String"));
            keikaku_sagyo_list.Columns.Add("備考", Type.GetType("System.String"));
            keikaku_sagyo_list.Columns.Add("登録日時", Type.GetType("System.String"));
            keikaku_sagyo_list.Columns.Add("登録者", Type.GetType("System.String"));
            keikaku_sagyo_list.Columns.Add("更新日時", Type.GetType("System.String"));
            keikaku_sagyo_list.Columns.Add("更新者", Type.GetType("System.String"));


            //データの挿入
            if (scheduleList_keikaku != null)
            {
                foreach (taskDS v in scheduleList_keikaku)
                {
                    //チェックボックスがOFFになっている場合は表示しない
                    if (this.m_keikaku_umu_check.Checked == false && v.status == "無効")
                        continue;

                    DataRow urow = keikaku_sagyo_list.NewRow();

                    urow["No"] = v.schedule_no;
                    urow["ステータス"] = v.status;
                    urow["カスタマ通番"] = v.userno;
                    urow["カスタマ名"] = v.username;

                    //1:インシデント処理 2:定期作業業務促し 3:計画作業 4:特別対応
                    //if (v.schedule_type == "1")
                    //    urow["予定区分"] = "インシデント処理";
                    //else if (v.schedule_type == "2")
                    //    urow["予定区分"] = "定期作業";
                    //else if (v.schedule_type == "3")
                    //    urow["予定区分"] = "計画作業";
                    //else if (v.schedule_type == "4")
                    //    urow["予定区分"] = "特別対応";
                    urow["内容"] = v.naiyou;
                    urow["テンプレートNO"] = v.templeteno;
                    urow["開始日時"] = v.startdate;
                    urow["終了日時"] = v.enddate;
                    urow["登録日時"] = v.ins_date;
                    urow["登録者"] = v.ins_name_id;
                    urow["更新日時"] = v.chk_date;
                    urow["更新者"] = v.chk_name_id;
                    keikaku_sagyo_list.Rows.Add(urow);


                }

                //件数を書き込む
                this.m_keikaku_count.Text = keikaku_sagyo_list.Rows.Count.ToString() + "件";

                this.m_keikaku_list.VirtualListSize = keikaku_sagyo_list.Rows.Count;
                this.m_keikaku_list.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
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
            this.m_tokubetu_list.Columns.Insert(1, "ステータス", 30, HorizontalAlignment.Left);
            this.m_tokubetu_list.Columns.Insert(2, "カスタマ通番", 50, HorizontalAlignment.Left);
            this.m_tokubetu_list.Columns.Insert(3, "カスタマ名", 50, HorizontalAlignment.Left);
            this.m_tokubetu_list.Columns.Insert(4, "内容", 50, HorizontalAlignment.Left);

            this.m_tokubetu_list.Columns.Insert(5, "テンプレートNO", 180, HorizontalAlignment.Left);
            this.m_tokubetu_list.Columns.Insert(6, "開始日時", 110, HorizontalAlignment.Left);
            this.m_tokubetu_list.Columns.Insert(7, "終了日時", 110, HorizontalAlignment.Left);
            this.m_tokubetu_list.Columns.Insert(8, "備考", 110, HorizontalAlignment.Left);
            this.m_tokubetu_list.Columns.Insert(9, "登録日時", 180, HorizontalAlignment.Left);
            this.m_tokubetu_list.Columns.Insert(10, "登録者", 80, HorizontalAlignment.Left);
            this.m_tokubetu_list.Columns.Insert(11, "更新日時", 80, HorizontalAlignment.Left);
            this.m_tokubetu_list.Columns.Insert(12, "更新者", 80, HorizontalAlignment.Left);

            //リストビューを初期化する
            tokubetu_sagyo_list = new DataTable("table12");
            tokubetu_sagyo_list.Columns.Add("No", Type.GetType("System.Int32"));
            tokubetu_sagyo_list.Columns.Add("ステータス", Type.GetType("System.String"));
            tokubetu_sagyo_list.Columns.Add("カスタマ通番", Type.GetType("System.String"));
            tokubetu_sagyo_list.Columns.Add("カスタマ名", Type.GetType("System.String"));
            tokubetu_sagyo_list.Columns.Add("内容", Type.GetType("System.String"));
            tokubetu_sagyo_list.Columns.Add("テンプレートNO", Type.GetType("System.String"));
            tokubetu_sagyo_list.Columns.Add("開始日時", Type.GetType("System.String"));
            tokubetu_sagyo_list.Columns.Add("終了日時", Type.GetType("System.String"));
            tokubetu_sagyo_list.Columns.Add("備考", Type.GetType("System.String"));
            tokubetu_sagyo_list.Columns.Add("登録日時", Type.GetType("System.String"));
            tokubetu_sagyo_list.Columns.Add("登録者", Type.GetType("System.String"));
            tokubetu_sagyo_list.Columns.Add("更新日時", Type.GetType("System.String"));
            tokubetu_sagyo_list.Columns.Add("更新者", Type.GetType("System.String"));


            //データの挿入
            if (scheduleList_tokubetu != null)
            {
                foreach (taskDS v in scheduleList_tokubetu)
                {
                    //チェックボックスがOFFになっている場合は表示しない
                    if (this.m_tokubetu_umu_check.Checked == false && v.status == "無効")
                        continue;

                    //if (fastline == v.incident_no)
                    DataRow urow = tokubetu_sagyo_list.NewRow();
                    //    continue;

                    urow["No"] = v.schedule_no;
                    urow["ステータス"] = v.status;
                    urow["カスタマ通番"] = v.userno;
                    urow["カスタマ名"] = v.username;

                    //1:インシデント処理 2:定期作業業務促し 3:計画作業 4:特別対応
                    //if (v.schedule_type == "1")
                    //    urow["予定区分"] = "インシデント処理";
                    //else if (v.schedule_type == "2")
                    //    urow["予定区分"] = "定期作業";
                    //else if (v.schedule_type == "3")
                    //    urow["予定区分"] = "計画作業";
                    //else if (v.schedule_type == "4")
                    //    urow["予定区分"] = "特別対応";
                    urow["内容"] = v.naiyou;
                    urow["テンプレートNO"] = v.templeteno;
                    urow["開始日時"] = v.startdate;
                    urow["終了日時"] = v.enddate;
                    urow["備考"] = v.biko;
                    urow["登録日時"] = v.ins_date;
                    urow["登録者"] = v.ins_name_id;

                    urow["更新日時"] = v.chk_date;
                    urow["更新者"] = v.chk_name_id;
                    tokubetu_sagyo_list.Rows.Add(urow);
                }

                //件数を書き込む
                this.m_toubetu_count.Text = tokubetu_sagyo_list.Rows.Count.ToString() + "件";

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
            this.userList.Columns.Insert(2, "カスタマID", 80, HorizontalAlignment.Left);
            this.userList.Columns.Insert(3, "カスタマ名", 180, HorizontalAlignment.Left);
            this.userList.Columns.Insert(4, "カスタマ名カナ", 30, HorizontalAlignment.Left);
            this.userList.Columns.Insert(5, "カスタマ名略称", 30, HorizontalAlignment.Left);
            this.userList.Columns.Insert(6, "SLO対象", 80, HorizontalAlignment.Left);
            this.userList.Columns.Insert(7, "備考", 180, HorizontalAlignment.Left);
            this.userList.Columns.Insert(8, "更新日時", 80, HorizontalAlignment.Left);
            this.userList.Columns.Insert(9, "更新者", 80, HorizontalAlignment.Left);

            //リストビューを初期化する
            user_list = new DataTable("table2");
            user_list.Columns.Add("No", Type.GetType("System.Int32"));
            user_list.Columns.Add("有効", Type.GetType("System.String"));
            user_list.Columns.Add("カスタマID", Type.GetType("System.String"));
            user_list.Columns.Add("カスタマ名", Type.GetType("System.String"));
            user_list.Columns.Add("カスタマ名カナ", Type.GetType("System.String"));
            user_list.Columns.Add("カスタマ名略称", Type.GetType("System.String"));
            user_list.Columns.Add("SLO対象", Type.GetType("System.String"));
            user_list.Columns.Add("備考", Type.GetType("System.String"));
            user_list.Columns.Add("更新日時", Type.GetType("System.String"));
            user_list.Columns.Add("更新者", Type.GetType("System.String"));

            if (dsp_L == null)
                return;
            //データの挿入
            if (dsp_L.user_L != null)
            {
                string fastline = "";
                foreach (userDS v in dsp_L.user_L)
                {

                    if (fastline == v.userno)
                        continue;

                    //チェックボックスがOFFになっている場合は表示しない
                    if (this.m_customer_umu_check.Checked　==false  && v.status == "無効")
                        continue;

                    DataRow urow = user_list.NewRow();

                    urow["No"] = v.userno;
                    urow["有効"] = v.status;
                    urow["カスタマID"] = v.customerID;
                    urow["カスタマ名"] = v.username;
                    urow["カスタマ名カナ"] = v.username_kana;
                    urow["カスタマ名略称"] = v.username_sum;
                    urow["SLO対象"] = v.report_status;
                    urow["備考"] = v.biko;
                    urow["更新日時"] = v.chk_date;
                    urow["更新者"] = v.chk_name_id;
                    user_list.Rows.Add(urow);

                    fastline = v.userno;

                }
                //件数を書き込む
                this.m_user_count.Text = user_list.Rows.Count.ToString() + "件";
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
            this.systemList.Columns.Insert(1, "有効", 50, HorizontalAlignment.Left);
            this.systemList.Columns.Insert(2, "システム名", 180, HorizontalAlignment.Left);
            this.systemList.Columns.Insert(3, "システム名カナ", 50, HorizontalAlignment.Left);
            this.systemList.Columns.Insert(4, "備考", 30, HorizontalAlignment.Left);
            this.systemList.Columns.Insert(5, "更新日時", 80, HorizontalAlignment.Left);
            this.systemList.Columns.Insert(6, "更新者", 80, HorizontalAlignment.Left);

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
                HashSet<string> ary1 = new HashSet<string>();

                string fastline = "";
                foreach (systemDS sys in dsp_L.system_L)
                {
                    if (fastline == sys.systemno)
                        continue;
                    //チェックボックスがOFFになっている場合は表示しない
                    if (this.m_system_umu_check.Checked == false && sys.status == "無効")
                        continue;
                    //重複チェック
                    if (ary1.Add(sys.systemno))
                    {
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
                    }
                }
                //件数を書き込む
                this.m_system_count.Text = system_list.Rows.Count.ToString() + "件";

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
            this.siteList.Columns.Insert(3, "郵便番号", 50, HorizontalAlignment.Left);
            this.siteList.Columns.Insert(4, "住所", 50, HorizontalAlignment.Left);
            this.siteList.Columns.Insert(5, "TEL/FAX", 80, HorizontalAlignment.Left);
            this.siteList.Columns.Insert(6, "備考", 180, HorizontalAlignment.Left);
            this.siteList.Columns.Insert(7, "更新日時", 80, HorizontalAlignment.Left);
            this.siteList.Columns.Insert(8, "更新者", 80, HorizontalAlignment.Left);

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
                HashSet<string> ary1 = new HashSet<string>();

                foreach (siteDS s in dsp_L.site_L)
                {
                    //チェックボックスがOFFになっている場合は表示しない
                    if (this.m_site_umu_check.Checked == false && s.status == "無効")
                        continue;

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
                        }
                    }
                }
                //件数を書き込む
                this.m_site_count.Text = site_list.Rows.Count.ToString() + "件";

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

                HashSet<string> ary1 = new HashSet<string>();
                foreach (hostDS h in dsp_L.host_L)
                {
                    //チェックボックスがOFFになっている場合は表示しない
                    if (this.m_host_umu_check.Checked == false && h.status == "無効")
                        continue;

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
                        }
                    }
                }
                //件数を書き込む
                this.m_host_count.Text = host_list.Rows.Count.ToString() + "件";
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
            this.interfaceList.Columns.Insert(5, "閾値", 30, HorizontalAlignment.Left);
            this.interfaceList.Columns.Insert(6, "IPアドレス", 100, HorizontalAlignment.Left);
            this.interfaceList.Columns.Insert(7, "IPアドレス(NAT)", 100, HorizontalAlignment.Left);
            this.interfaceList.Columns.Insert(8, "備考", 100, HorizontalAlignment.Left);
            this.interfaceList.Columns.Insert(9, "ホスト通番", 80, HorizontalAlignment.Left);
            this.interfaceList.Columns.Insert(10, "カスタマ通番", 80, HorizontalAlignment.Left);
            this.interfaceList.Columns.Insert(11, "システム通番", 80, HorizontalAlignment.Left);
            this.interfaceList.Columns.Insert(12, "拠点通番", 80, HorizontalAlignment.Left);
            this.interfaceList.Columns.Insert(13, "更新日時", 80, HorizontalAlignment.Left);
            this.interfaceList.Columns.Insert(14, "更新者", 80, HorizontalAlignment.Left);

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
                foreach (watch_InterfaceDS w in dsp_L.watch_L)
                {
                    dispflg = 0;

                    //チェックボックスがOFFになっている場合は表示しない
                    if (this.m_interface_umu_check.Checked == false && w.status == "無効")
                        continue;

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

                        }
                    }
                }

                //件数を書き込む
                this.m_interface_count.Text = interface_list.Rows.Count.ToString() + "件";

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
            this.kaisenList.Columns.Insert(8, "電話番号1", 180, HorizontalAlignment.Left);
            this.kaisenList.Columns.Insert(9, "電話番号2", 180, HorizontalAlignment.Left);
            this.kaisenList.Columns.Insert(10, "電話番号3", 180, HorizontalAlignment.Left);
            this.kaisenList.Columns.Insert(11, "備考", 180, HorizontalAlignment.Left);
            this.kaisenList.Columns.Insert(12, "カスタマ通番", 30, HorizontalAlignment.Left);
            this.kaisenList.Columns.Insert(13, "システム通番", 30, HorizontalAlignment.Left);
            this.kaisenList.Columns.Insert(14, "拠点通番", 30, HorizontalAlignment.Left);
            this.kaisenList.Columns.Insert(15, "ホスト通番", 30, HorizontalAlignment.Left);
            this.kaisenList.Columns.Insert(16, "更新日時", 80, HorizontalAlignment.Left);
            this.kaisenList.Columns.Insert(17, "更新者", 80, HorizontalAlignment.Left);

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
            kasen_list.Columns.Add("電話番号1", Type.GetType("System.String"));
            kasen_list.Columns.Add("電話番号2", Type.GetType("System.String"));
            kasen_list.Columns.Add("電話番号3", Type.GetType("System.String"));
            kasen_list.Columns.Add("備考", Type.GetType("System.String"));
            kasen_list.Columns.Add("カスタマ通番", Type.GetType("System.String"));
            kasen_list.Columns.Add("システム通番", Type.GetType("System.String"));
            kasen_list.Columns.Add("拠点通番", Type.GetType("System.String"));
            kasen_list.Columns.Add("ホスト通番", Type.GetType("System.String"));
            kasen_list.Columns.Add("更新日時", Type.GetType("System.String"));
            kasen_list.Columns.Add("更新者", Type.GetType("System.String"));
            if (dsp_L == null)
                return;
            //回線情報
            if (dsp_L.kaisen_L != null)
            {
                HashSet<string> ary1 = new HashSet<string>();
                foreach (kaisenDS ka in dsp_L.kaisen_L)
                {
                    //チェックボックスがOFFになっている場合は表示しない
                    if (this.m_kaisen_umu_check.Checked == false && ka.status == "無効")
                        continue;

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

                        row["電話番号1"] = ka.telno1;
                        row["電話番号2"] = ka.telno2;
                        row["電話番号3"] = ka.telno3;
                        row["備考"] = ka.biko;

                        row["カスタマ通番"] = ka.userno;
                        row["システム通番"] = ka.systemno;
                        row["拠点通番"] = ka.siteno;
                        row["ホスト通番"] = ka.host_no;
                        row["更新日時"] = ka.chk_date;
                        row["更新者"] = ka.chk_name_id;

                        kasen_list.Rows.Add(row);
                    }
                }
                //件数を書き込む
                this.m_kaisen_count.Text = kasen_list.Rows.Count.ToString() + "件";

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
                if (dsp_L.user_L != null )
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
                logger.ErrorFormat("構成情報の表示時にエラーが発生しました。" + ex.Message );
            }
            finally
            {
                this.m_selectBtn.Enabled = true;
            }

        }



        //ダブルクリックしたとき
        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                String teskNO;
                if (e.Node.Level == 2)
                {

                    teskNO = e.Node.ToolTipText;

                    if (teskNO != "" && e.Node.Nodes.Count == 0)
                    {
                        //インシデントの時         
                        if (e.Node.Parent.ToolTipText == "1")
                        {
                            List<incidentDS> incidentList = new List<incidentDS>();
                            Dictionary<string, string> param_dict = new Dictionary<string, string>();
                            Class_Detaget dg = new Class_Detaget();

                            //Form_scheduleDetail formdetail = new Form_scheduleDetail();
                            Form_taskDetail formdetail = new Form_taskDetail();

                            string scheduleno = e.Node.ToolTipText;

                            formdetail.taskds = new taskDS();

                            string status = "0";
                            foreach (taskDS sch in schDSList)
                            {
                                if (scheduleno == sch.schedule_no)
                                {
                                    formdetail.taskds = sch;
                                    if (sch.status != null)
                                        if (sch.status == "未完了")
                                            status = "1";
                                        else if (sch.status == "完了")
                                            status = "0";

                                    formdetail.taskds.schedule_no = sch.schedule_no;
                                    formdetail.taskds.schedule_type = sch.schedule_type;
                                    formdetail.taskds.status = status;
                                    formdetail.taskds.templeteno = sch.templeteno;
                                    formdetail.taskds.naiyou = sch.naiyou;
                                    formdetail.taskds.startdate = sch.startdate;
                                    formdetail.taskds.enddate = sch.enddate;
                                    formdetail.taskds.biko = sch.biko;
                                    formdetail.taskds.userno= sch.userno;
                                    formdetail.taskds.username = sch.username;
                                    formdetail.taskds.ins_date = sch.ins_date;
                                    formdetail.taskds.ins_name_id = sch.ins_name_id;
                                    formdetail.taskds.chk_date = sch.chk_date;
                                    formdetail.taskds.chk_name_id = sch.chk_name_id;

                                    break;
                                }
                            }
                            if (userDSList != null)
                                formdetail.userList = userDSList;
                            formdetail.loginDS = loginDS;
                            formdetail.con = con;
                            formdetail.Owner = this;
                            formdetail.Show();
                        }
                        //定期作業のとき
                        else if (e.Node.Parent.ToolTipText == "2")
                        {
                            //Form_scheduleDetail formdetail = new Form_scheduleDetail();
                            Form_taskDetail formdetail = new Form_taskDetail();
                            formdetail.con = con;

                            string scheduleno = e.Node.ToolTipText;

                            formdetail.taskds = new taskDS();

                            string status = "0";
                            foreach (taskDS sch in scheduleList_teiki)
                            {
                                if (scheduleno == sch.schedule_no)
                                {
                                    formdetail.taskds = sch;
                                    if (sch.status != null)
                                        if (sch.status == "未完了")
                                            status = "1";
                                        else if (sch.status == "完了")
                                            status = "0";

                                    formdetail.taskds.schedule_no = sch.schedule_no;
                                    formdetail.taskds.status = status;
                                    formdetail.taskds.schedule_type = sch.schedule_type;
                                    formdetail.taskds.templeteno = sch.templeteno;
                                    formdetail.taskds.naiyou = sch.naiyou;
                                    formdetail.taskds.startdate = sch.startdate;
                                    formdetail.taskds.enddate = sch.enddate;
                                    formdetail.taskds.biko = sch.biko;
                                    formdetail.taskds.userno    = sch.userno;
                                    formdetail.taskds.username = sch.username;
                                    formdetail.taskds.ins_date = sch.ins_date;
                                    formdetail.taskds.ins_name_id = sch.ins_name_id;
                                    formdetail.taskds.chk_date = sch.chk_date;
                                    formdetail.taskds.chk_name_id = sch.chk_name_id;

                                    break;
                                }
                            }
                            if (userDSList != null)
                                formdetail.userList = userDSList;
                            if (systemDSList != null)
                                formdetail.systemList = systemDSList;
                            if (siteDSList != null)
                                formdetail.siteList = siteDSList;
                            formdetail.loginDS = loginDS;
                            formdetail.con = con;
                            formdetail.Owner = this;
                            formdetail.Show();
                        }

                        //計画作業のとき
                        else if (e.Node.Parent.ToolTipText == "3")
                        {
                            Form_taskDetail formdetail = new Form_taskDetail();
                            formdetail.con = con;

                            string scheduleno = e.Node.ToolTipText;

                            formdetail.taskds  = new taskDS();

                            string status = "0";
                            foreach (taskDS sch in scheduleList_keikaku)
                            {
                                if (scheduleno == sch.schedule_no)
                                {
                                    formdetail.taskds = sch;
                                    if (sch.status != null)
                                        if (sch.status == "有効")
                                            status = "1";
                                        else if (sch.status == "無効")
                                            status = "0";

                                    formdetail.taskds.schedule_no = sch.schedule_no;
                                    formdetail.taskds.status = status;
                                    formdetail.taskds.schedule_type = sch.schedule_type;
                                    formdetail.taskds.templeteno = sch.templeteno;
                                    formdetail.taskds.naiyou = sch.naiyou;
                                    formdetail.taskds.startdate = sch.startdate;
                                    formdetail.taskds.enddate = sch.enddate;
                                    formdetail.taskds.biko = sch.biko;
                                    formdetail.taskds.userno = sch.userno;
                                    formdetail.taskds.username = sch.username;
                                    formdetail.taskds.ins_date = sch.ins_date;
                                    formdetail.taskds.ins_name_id = sch.ins_name_id;
                                    formdetail.taskds.chk_date = sch.chk_date;
                                    formdetail.taskds.chk_name_id = sch.chk_name_id;

                                    break;
                                }
                            }
                            if (userDSList != null)
                                formdetail.userList = userDSList;
                            //if (systemDSList != null)
                            //    formdetail.systemList = systemDSList;
                            //if (siteDSList != null)
                            //    formdetail.siteList = siteDSList;

                            formdetail.loginDS = loginDS;
                            formdetail.con = con;
                            formdetail.Owner = this;
                            formdetail.Show();
                        }
                        //特別対応のとき
                        else if (e.Node.Parent.ToolTipText == "4")
                        {
                            Form_taskDetail formdetail = new Form_taskDetail();
                            formdetail.con = con;

                            string scheduleno = e.Node.ToolTipText;

                            formdetail.taskds = new taskDS();

                            string status = "0";
                            foreach (taskDS sch in scheduleList_tokubetu)
                            {
                                if (scheduleno == sch.schedule_no)
                                {
                                    formdetail.taskds = sch;
                                    if (sch.status != null)
                                        if (sch.status == "有効")
                                            status = "1";
                                        else if (sch.status == "無効")
                                            status = "0";

                                    formdetail.taskds.schedule_no = sch.schedule_no;
                                    formdetail.taskds.status = status;
                                    formdetail.taskds.schedule_type = sch.schedule_type;
                                    formdetail.taskds.templeteno = sch.templeteno;
                                    formdetail.taskds.naiyou = sch.naiyou;
                                    formdetail.taskds.startdate = sch.startdate;
                                    formdetail.taskds.enddate = sch.enddate;
                                    formdetail.taskds.biko = sch.biko;
                                    formdetail.taskds.userno = sch.userno;
                                    formdetail.taskds.username = sch.username;
                                    formdetail.taskds.ins_date = sch.ins_date;
                                    formdetail.taskds.ins_name_id = sch.ins_name_id;
                                    formdetail.taskds.chk_date = sch.chk_date;
                                    formdetail.taskds.chk_name_id = sch.chk_name_id;

                                    break;
                                }
                            }
                            if (userDSList != null)
                                formdetail.userList = userDSList;
                            //if (systemDSList != null)
                            //    formdetail.systemList = systemDSList;
                            //if (siteDSList != null)
                            //    formdetail.siteList = siteDSList;
                            formdetail.loginDS = loginDS;
                            formdetail.con = con;
                            formdetail.Owner = this;
                            formdetail.Show();
                        }


                    }
                }
            }
            // If the file is not found, handle the exception and inform the user.
            catch (Exception ex)
            {
                MessageBox.Show("タイマーの値の表示時にエラーが発生しました。MSG:" + ex.Message);
                logger.ErrorFormat("タイマー値表示エラー：{0}", ex.Message);

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

            List<TreeNode> expand_array = new List<TreeNode>();
            
            //選択されたノードを覚えておく
            TreeNode node = treeView1.SelectedNode;

            CallRecursive(treeView1, expand_array);

            treeView1.BeginUpdate();

            RefreshTreeView();


            int i = 0;
            for (i= 0;i< expand_array.Count;i++)
                CallRecursiveExpand(treeView1, expand_array[i].Text);


            treeView1.EndUpdate();

            treeView1.Refresh();

            if (node != null)
                node.EnsureVisible();
            treeView1.Focus();
            combo_set();
            //インシデント一覧データを取得
            Class_Detaget dg_class = new Class_Detaget();
            //List<incidentDS> incidentList;
            //incidentDSList = dg_class.getOpenIncident(con);

            //インシデント一覧を表示
           // disp_Incident(incidentDSList);
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
                logger.ErrorFormat("システム情報コンボボックス読込エラー：{0}", ex.Message);
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
                MessageBox.Show(ex.Message, "ホスト情報コンボボックスの読み込みに失敗", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                logger.ErrorFormat("ホスト情報の読み込みに失敗：{0}", ex.Message);
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
                logger.ErrorFormat("拠点情報コンボボックスデータ読み込み時エラー：{0}", ex.Message);
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
            OpenMente_customer();
        }

        //ENTERキーが押されたとき
        private void userList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenMente_customer();
            }
        }
        //カスタマメンテナンス画面を開く
        private void OpenMente_customer()
        {
            ListView.SelectedIndexCollection item = userList.SelectedIndices;
            Form_UserDetail formdetail = new Form_UserDetail();
            formdetail.con = con;

            if (item.Count > 0)
            {
                userDS userdt = new userDS();
                userdt.userno = this.userList.Items[item[0]].SubItems[0].Text;
                userdt.customerID = this.userList.Items[item[0]].SubItems[2].Text;
                userdt.username = this.userList.Items[item[0]].SubItems[3].Text;
                userdt.username_kana = this.userList.Items[item[0]].SubItems[4].Text;
                userdt.username_sum = this.userList.Items[item[0]].SubItems[5].Text;

                string statustxt = this.userList.Items[item[0]].SubItems[1].Text;
                if (statustxt == "有効")
                    userdt.status = "1";
                else
                    userdt.status = "0";

                string reportststxt = this.userList.Items[item[0]].SubItems[6].Text;
                if (reportststxt == "有効")
                    userdt.report_status = "1";
                else
                    userdt.report_status = "0";

                userdt.biko = this.userList.Items[item[0]].SubItems[7].Text;
                userdt.chk_date = this.userList.Items[item[0]].SubItems[8].Text;
                userdt.chk_name_id = this.userList.Items[item[0]].SubItems[9].Text;


                formdetail.userdt = userdt;
            }
            formdetail.loginDS = loginDS;
            formdetail.Show();
            formdetail.Owner = this;
        }
        //システム情報の表示
        private void systemList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenMente_system();

        }
        private void systemList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenMente_system();
            }
        }
        //システムメンテナンス画面を開く
        private void OpenMente_system()
        {
            ListView.SelectedIndexCollection item = systemList.SelectedIndices;
            Form_SystemDetail formdetail = new Form_SystemDetail();
            formdetail.con = con;
            systemDS systemdt = new systemDS();

            if (item.Count > 0)
            {
                string systemno = this.systemList.Items[item[0]].SubItems[0].Text;

                foreach (systemDS sl in systemDSList)
                {
                    if (systemno == sl.systemno)
                    {
                        formdetail.systemdt = sl;
                        break;
                    }
                }
            }
            formdetail.loginDS = loginDS;

            formdetail.Show();
            formdetail.Owner = this;
        }


        //拠点をダブルクリック
        private void siteList_DoubleClick(object sender, EventArgs e)
        {
            OpenMente_site();
        }
        private void siteList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenMente_system();
            }
        }
        //拠点情報の表示
        private void OpenMente_site()
        {
            ListView.SelectedIndexCollection item = siteList.SelectedIndices;
            Form_SiteDetail formdetail = new Form_SiteDetail();
            formdetail.con = con;


            if (item.Count > 0)
            {

                //拠点番号
                string siteno = this.siteList.Items[item[0]].SubItems[0].Text;

                if (siteDSList == null)
                {
                    formdetail.sitedt = new siteDS();
                    formdetail.sitedt.siteno = this.siteList.Items[item[0]].SubItems[0].Text;
                    formdetail.sitedt.status = this.siteList.Items[item[0]].SubItems[1].Text;
                    formdetail.sitedt.sitename = this.siteList.Items[item[0]].SubItems[2].Text;


                    formdetail.sitedt.address1 = this.siteList.Items[item[0]].SubItems[3].Text;
                    formdetail.sitedt.address2 = this.siteList.Items[item[0]].SubItems[4].Text;
                    formdetail.sitedt.telno = this.siteList.Items[item[0]].SubItems[5].Text;
                    formdetail.sitedt.biko = this.siteList.Items[item[0]].SubItems[6].Text;
                    formdetail.sitedt.chk_date = this.siteList.Items[item[0]].SubItems[7].Text;
                    formdetail.sitedt.chk_name_id = this.siteList.Items[item[0]].SubItems[8].Text;
                }
                else
                {
                    foreach (siteDS sl in siteDSList)
                    {
                        if (siteno == sl.siteno)
                        {
                            formdetail.sitedt = sl;
                            break;
                        }
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
            OpenMente_host();
        }
        private void m_host_list_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenMente_host();
            }
        }
        private void OpenMente_host()
        {
            ListView.SelectedIndexCollection item = m_host_list.SelectedIndices;
            Form_HostDetail formdetail = new Form_HostDetail();
            formdetail.con = con;

            if (item.Count > 0)
            {
                //ホスト番号
                string hostno = this.m_host_list.Items[item[0]].SubItems[0].Text;

                if (hostDSList == null)
                {
                    formdetail.hostdt = new hostDS();
                    formdetail.hostdt.hostname = this.siteList.Items[item[0]].SubItems[0].Text;
                    formdetail.hostdt.status = this.siteList.Items[item[0]].SubItems[1].Text;
                    formdetail.hostdt.device = this.siteList.Items[item[0]].SubItems[2].Text;
                    formdetail.hostdt.location = this.siteList.Items[item[0]].SubItems[3].Text;
                    formdetail.hostdt.usefor = this.siteList.Items[item[0]].SubItems[4].Text;
                    formdetail.hostdt.settikikiid = this.siteList.Items[item[0]].SubItems[5].Text;
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
                else
                {
                    foreach (hostDS sl in hostDSList)
                    {
                        if (hostno == sl.host_no)
                        {
                            formdetail.hostdt = sl;
                            break;
                        }
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
            OpenMente_interface();
        }
        private void interfaceList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenMente_interface();
            }
        }
        //インターフェイス一覧表示
        private void OpenMente_interface()
        {
            ListView.SelectedIndexCollection item = interfaceList.SelectedIndices;
            Form_interfaceDetail formdetail = new Form_interfaceDetail();
            formdetail.con = con;
            if (item.Count > 0)
            {
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
                    formdetail.interfacedt.border = this.interfaceList.Items[item[0]].SubItems[5].Text;
                    formdetail.interfacedt.IPaddress = this.interfaceList.Items[item[0]].SubItems[6].Text;
                    formdetail.interfacedt.IPaddressNAT = this.interfaceList.Items[item[0]].SubItems[7].Text;
                    formdetail.interfacedt.biko = this.interfaceList.Items[item[0]].SubItems[8].Text;
                    formdetail.interfacedt.host_no = this.interfaceList.Items[item[0]].SubItems[9].Text;
                    formdetail.interfacedt.userno = this.interfaceList.Items[item[0]].SubItems[10].Text;
                    formdetail.interfacedt.systemno = this.interfaceList.Items[item[0]].SubItems[11].Text;
                    formdetail.interfacedt.siteno = this.interfaceList.Items[item[0]].SubItems[12].Text;
                    formdetail.interfacedt.chk_date = this.interfaceList.Items[item[0]].SubItems[13].Text;
                    formdetail.interfacedt.chk_name_id = this.interfaceList.Items[item[0]].SubItems[14].Text;
                }
                else
                {
                    foreach (watch_InterfaceDS il in interfaceDSList)
                    {
                        if (interfaceno == il.watch_Interfaceno)
                        {
                            formdetail.interfacedt = il;
                            break;
                        }
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
            OpenMente_kaisen();

        }
        private void kaisenList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenMente_kaisen();
            }
        }
        private void OpenMente_kaisen()
        {
            ListView.SelectedIndexCollection item = kaisenList.SelectedIndices;
            Form_KaisenDetail formdetail = new Form_KaisenDetail();
            formdetail.con = con;
            if (item.Count > 0)
            {
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
                formdetail.kaisendt.telno1 = this.kaisenList.Items[item[0]].SubItems[8].Text;
                formdetail.kaisendt.telno2 = this.kaisenList.Items[item[0]].SubItems[9].Text;
                formdetail.kaisendt.telno3 = this.kaisenList.Items[item[0]].SubItems[10].Text;
                formdetail.kaisendt.biko = this.kaisenList.Items[item[0]].SubItems[11].Text;
                formdetail.kaisendt.userno = this.kaisenList.Items[item[0]].SubItems[12].Text;
                formdetail.kaisendt.systemno = this.kaisenList.Items[item[0]].SubItems[13].Text;
                formdetail.kaisendt.siteno = this.kaisenList.Items[item[0]].SubItems[14].Text;
                formdetail.kaisendt.host_no = this.kaisenList.Items[item[0]].SubItems[15].Text;
                formdetail.kaisendt.chk_date = this.kaisenList.Items[item[0]].SubItems[16].Text;
                formdetail.kaisendt.chk_name_id = this.kaisenList.Items[item[0]].SubItems[17].Text;
            }
            if (userDSList != null)
                formdetail.userList = userDSList;
            if (systemDSList != null)
                formdetail.systemList = systemDSList;
            if (siteDSList != null)
                formdetail.siteList = siteDSList;

            formdetail.loginDS = loginDS;

            formdetail.Show();
            formdetail.Owner = this;
        }

        //タイマー1分毎にサーバを見に行く
        private void timer1_Tick(object sender, EventArgs e)
        {
            logger.InfoFormat("Timer1_Tick()_Begin");
            try
            {
                //時間になったタイマーの問い合わせ
                Class_Detaget dataget = new Class_Detaget();
                dataget.con = con;
                List<alermDS> alermlist;
                alermlist = dataget.getAlert(this, con);

                logger.InfoFormat("getAlert " + alermlist.Count.ToString() + "件");

                
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
                        alermdlg.Close();
                        alermdlg = null;
                        alermdlg = new Form_alermlist();
                        alermdlg.almList = alermlist;
                        alermdlg.con = con;
                        alermdlg.Show();


                        alermdlg.almList = alermlist;
                        alermdlg.Refresh();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("タイマー表示 " + ex.Message);
                return ;
            }

            logger.InfoFormat("Timer1_Tick()_END");

        }

        //フォームが閉じるとき
        private void Form_MainList_FormClosed(object sender, FormClosedEventArgs e)
        {
            //DBコネクションのクローズ
            if (con != null) {
                logger.Info("コネクションクローズ");
                con.Close();
                logger.InfoFormat("ログアウト：{0} ", loginDS.lastname + loginDS.fastname);
            }

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


        //計画作業列をダブルクリック
        private void m_keikaku_list_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            disp_schedule();

        }
        private void m_incident_List_KeyDown(object sender, KeyEventArgs e)
        {
            disp_schedule();
        }

        private void disp_schedule()
        {
            ListView.SelectedIndexCollection item = m_keikaku_list.SelectedIndices;
            Form_taskDetail formdetail = new Form_taskDetail();
            formdetail.con = con;

            string scheduleno = this.m_keikaku_list.Items[item[0]].SubItems[0].Text;

            formdetail.taskds = new taskDS();

            string status = "0";
            foreach (taskDS sch in scheduleList_keikaku)
            {
                if (scheduleno == sch.schedule_no)
                {
                    formdetail.taskds = sch;
                    if (sch.status != null)
                        if (sch.status == "有効")
                            status = "1";
                        else if (sch.status == "無効")
                            status = "0";

                    formdetail.taskds.schedule_no = sch.schedule_no;
                    formdetail.taskds.status = status;
                    formdetail.taskds.schedule_type = sch.schedule_type;
                    formdetail.taskds.templeteno = sch.templeteno;

                    formdetail.taskds.naiyou = sch.naiyou;
                    formdetail.taskds.startdate = sch.startdate;
                    formdetail.taskds.enddate = sch.enddate;
                    formdetail.taskds.biko = sch.biko;
                    formdetail.taskds.userno = sch.userno;
                    formdetail.taskds.username = sch.username;
                    formdetail.taskds.ins_date = sch.ins_date;
                    formdetail.taskds.ins_name_id = sch.ins_name_id;
                    formdetail.taskds.chk_date = sch.chk_date;
                    formdetail.taskds.chk_name_id = sch.chk_name_id;

                    break;
                }
            }
            if (userDSList != null)
                formdetail.userList = userDSList;
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
            if (status == "有効")
                menustring = "無効";
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
            if (status == "有効")
                menustring = "無効";
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
            if(MessageBox.Show(count.ToString() + "件　更新します。よろしいですか?", "定期作業ステータス更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            return;
            foreach (int i in collection)
            {
                // iに選択されているリストのインデックスが順次入る（当然0からの値）
                string scheduleid = this.m_teiki_List.Items[i].SubItems[0].Text;
                string status = this.m_teiki_List.Items[i].SubItems[1].Text;
                if (status == "有効")
                {
                    if (con.FullState != ConnectionState.Open) con.Open();

                    string sql = "update task set status =:state,chk_name_id =:id, chk_date=:date where schedule_no = :a";
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

                            if (rowsaffected != 1) {
                                MessageBox.Show("定期作業ステータス更新時エラーが発生しました。","");
                                logger.ErrorFormat("定期作業ステータス読込エラー：{0}", sql);
                            }
                            MessageBox.Show("定期作業ステータス更新しました。", "定期作業ステータス更新");

                        }
                        catch (Exception ex)
                        {
                            //エラー時メッセージ表示
                            MessageBox.Show(ex.Message);
                            logger.ErrorFormat("定期作業ステータス更新エラー：{0} sql:{1}", ex.Message,sql);
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
            if (MessageBox.Show(count.ToString() + "件　更新します。よろしいですか?", "計画作業ステータス更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            foreach (int i in collection)
            {
                // iに選択されているリストのインデックスが順次入る（当然0からの値）
                string scheduleid = this.m_keikaku_list.Items[i].SubItems[0].Text;
                string status = this.m_keikaku_list.Items[i].SubItems[1].Text;
                if (status == "有効")
                {
                    if (con.FullState != ConnectionState.Open) con.Open();

                    //string sql = "update schedule set status =:state,chk_name_id =:id, chk_date=:date where schedule_no = :a";
                    string sql = "update task set status =:state,chk_name_id =:id, chk_date=:date where schedule_no = :a";

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
                            {
                                MessageBox.Show("更新できませんでした。", "計画作業ステータス更新");
                                logger.ErrorFormat("計画作業ステータス更新エラー：sql:{0}", sql);
                            }
                            MessageBox.Show("計画作業ステータス更新しました。", "計画作業ステータス更新");

                        }
                        catch (Exception ex)
                        {
                            //エラー時メッセージ表示
                            MessageBox.Show(ex.Message);
                            logger.ErrorFormat("計画作業ステータス更新エラー：{0}", ex.Message);
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

            Form_taskDetail formdetail = new Form_taskDetail();

            formdetail.con = con;

            string scheduleno = this.m_teiki_List.Items[item[0]].SubItems[0].Text;

            formdetail.taskds = new taskDS();

            string status = "0";
            foreach (taskDS sch in scheduleList_teiki)
            {
                if (scheduleno == sch.schedule_no)
                {
                    formdetail.taskds = sch;
                    if (sch.status != null)
                        if (sch.status == "有効")
                            status = "1";
                        else if (sch.status == "無効")
                            status = "0";

                    formdetail.taskds.schedule_no = sch.schedule_no;
                    formdetail.taskds.schedule_type = sch.schedule_type;
                    formdetail.taskds.status = status;

                    formdetail.taskds.schedule_type = sch.schedule_type;
                    formdetail.taskds.templeteno = sch.templeteno;
                    formdetail.taskds.naiyou = sch.naiyou;
                    formdetail.taskds.startdate = sch.startdate;
                    formdetail.taskds.enddate = sch.enddate;
                    formdetail.taskds.biko = sch.biko;
                    formdetail.taskds.userno = sch.userno;
                    formdetail.taskds.username = sch.username;
                    formdetail.taskds.ins_date = sch.ins_date;
                    formdetail.taskds.ins_name_id = sch.ins_name_id;
                    formdetail.taskds.chk_date = sch.chk_date;
                    formdetail.taskds.chk_name_id = sch.chk_name_id;

                    break;
                }
            }
            //
            if (userDSList != null)
                formdetail.userList = userDSList;
            formdetail.loginDS = loginDS;
            formdetail.con = con;
            formdetail.Owner = this;
            formdetail.Show();
        }

        //インシデント情報をダブルクリック
        private void m_incident_List_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            ListView.SelectedIndexCollection item = m_incident_List.SelectedIndices;
            Form_taskDetail formdetail = new Form_taskDetail();
            formdetail.con = con;

            string scheduleno = this.m_incident_List.Items[item[0]].SubItems[0].Text;

            formdetail.taskds = new taskDS();

            string status = "0";
            foreach (taskDS sch in scheduleList_incident)
            {
                if (scheduleno == sch.schedule_no)
                {
                    formdetail.taskds = sch;
                    if (sch.status != null)
                        if (sch.status == "有効")
                            status = "1";
                        else if (sch.status == "無効")
                            status = "0";

                    formdetail.taskds.schedule_no = sch.schedule_no;
                    formdetail.taskds.schedule_type = sch.schedule_type;
                    formdetail.taskds.status = status;

                    formdetail.taskds.schedule_type = sch.schedule_type;
                    formdetail.taskds.templeteno = sch.templeteno;
                    formdetail.taskds.naiyou = sch.naiyou;
                    formdetail.taskds.startdate = sch.startdate;
                    formdetail.taskds.enddate = sch.enddate;
                    formdetail.taskds.biko = sch.biko;
                    formdetail.taskds.userno = sch.userno;
                    formdetail.taskds.username = sch.username;
                    formdetail.taskds.ins_date = sch.ins_date;
                    formdetail.taskds.ins_name_id = sch.ins_name_id;
                    formdetail.taskds.chk_date = sch.chk_date;
                    formdetail.taskds.chk_name_id = sch.chk_name_id;

                    break;
                }
            }
            //
            if (userDSList != null)
                formdetail.userList = userDSList;
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
        private DISP_dataSet kaisenFilter(int kousei)
        {
            kaisenList.Clear();
            DISP_dataSet dset = new DISP_dataSet();
            Dictionary<string, string> param_dict = new Dictionary<string, string>();
            Class_Detaget dg = new Class_Detaget();


            switch (kousei)
            {


                case 1:

                    ListView.SelectedIndexCollection useritem = userList.SelectedIndices;

                    //選択項目を取得
                    if (this.userList.Items[useritem[0]].SubItems[0].Text != "")
                        param_dict["userno"] = this.userList.Items[useritem[0]].SubItems[0].Text;

                    break;
                case 2:
                    ListView.SelectedIndexCollection sysitem = systemList.SelectedIndices;

                    //選択項目を取得
                    if (this.systemList.Items[sysitem[0]].SubItems[0].Text != "")
                        param_dict["systemno"] = this.systemList.Items[sysitem[0]].SubItems[0].Text;
                    break;
                case 3:
                    ListView.SelectedIndexCollection siteitem = siteList.SelectedIndices;

                    //選択項目を取得
                    if (this.siteList.Items[siteitem[0]].SubItems[0].Text != "")
                        param_dict["siteno"] = this.siteList.Items[siteitem[0]].SubItems[0].Text;
                    break;
                case 4:
                    ListView.SelectedIndexCollection hostitem = this.m_host_list.SelectedIndices;

                    //選択項目を取得
                    if (this.m_host_list.Items[hostitem[0]].SubItems[0].Text != "")
                        param_dict["host_no"] = this.m_host_list.Items[hostitem[0]].SubItems[0].Text;
                    break;
            }

            //回線一覧を取得する
            dset = dg.getSelectKaisenList(param_dict, con, dset);

            disp_kaisen(dset);

            return dset;

        }


        //カスタマ情報クリック
        private void userList_MouseClick(object sender, MouseEventArgs e)
        {
            DISP_dataSet dset = kaisenFilter(1);

        }
        //システム情報をクリック
        private void systemList_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection item = systemList.SelectedIndices;


            if (this.systemList.Items[item[0]].SubItems[0].Text != "")
            {
                //拠点情報一覧
                //                siteList.Clear();
                //                systemno = this.systemList.Items[item[0]].SubItems[0].Text;
                //拠点情報を絞り込む
                //                disp_site(dsp_L, systemno);

                //ホスト情報一覧
                //                m_host_list.Clear();
                //ホスト情報を絞り込む
                //                disp_host(dsp_L, systemno);

                //インターフェイス一覧
                //                interfaceList.Clear();
                //ホスト情報を絞り込む
                //                disp_interface(dsp_L, systemno);
                DISP_dataSet dset = kaisenFilter(2);
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

                DISP_dataSet dset = kaisenFilter(3);
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

                DISP_dataSet dset = kaisenFilter(4);
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

        private void m_incident_List_ColumnClick_1(object sender, ColumnClickEventArgs e)
        {
           if (this.incident_sagyo_list == null)
                return;
            if (this.incident_sagyo_list.Rows.Count <= 0)
                return;
            //DataViewクラス ソートするためのクラス
            DataView dv = new DataView(incident_sagyo_list);

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
            dttmp = incident_sagyo_list.Clone();
            //ソートを実行
            dv.Sort = incident_sagyo_list.Columns[e.Column].ColumnName + strSort;

            // ソートされたレコードのコピー
            foreach (DataRowView drv in dv)
            {
                // 一時テーブルに格納
                dttmp.ImportRow(drv.Row);
            }
            //格納したテーブルデータを上書く
            incident_sagyo_list = dttmp.Copy();

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
        //対応履歴を表示する
        private void button2_Click(object sender, EventArgs e)
        {
            Form_taiou_list taioufm = new Form_taiou_list();
            taioufm.con = con;
            taioufm.Show();

        }
        //特別対応をダブルクリック
        private void m_tokubetu_list_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView.SelectedIndexCollection item = m_tokubetu_list.SelectedIndices;
            Form_taskDetail formdetail = new Form_taskDetail();
            formdetail.con = con;

            string scheduleno = this.m_tokubetu_list.Items[item[0]].SubItems[0].Text;

            formdetail.taskds = new taskDS();

            string status = "0";
            foreach (taskDS sch in scheduleList_tokubetu)
            {
                if (scheduleno == sch.schedule_no)
                {
                    formdetail.taskds = sch;
                    if (sch.status != null)
                        if (sch.status == "有効")
                            status = "1";
                        else if (sch.status == "無効")
                            status = "0";

                    formdetail.taskds.schedule_no = sch.schedule_no;
                    formdetail.taskds.status = status;
                    formdetail.taskds.templeteno = sch.templeteno;
                    formdetail.taskds.schedule_type = sch.schedule_type;
                    formdetail.taskds.naiyou = sch.naiyou;
                    formdetail.taskds.startdate = sch.startdate;
                    formdetail.taskds.enddate = sch.enddate;
                    formdetail.taskds.biko = sch.biko;
                    formdetail.taskds.userno = sch.userno;
                    formdetail.taskds.username = sch.username;
                    formdetail.taskds.ins_date = sch.ins_date;
                    formdetail.taskds.ins_name_id = sch.ins_name_id;
                    formdetail.taskds.chk_date = sch.chk_date;
                    formdetail.taskds.chk_name_id = sch.chk_name_id;

                    break;
                }
            }
            if (userDSList != null)
                formdetail.userList = userDSList;
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


        //全ノード展開
        private void button3_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            if (button3.Text == "全ノード展開") { 
                treeView1.ExpandAll();
                button3.Text = "閉じる";
            }
            else
            {
                treeView1.CollapseAll();
                button3.Text = "全ノード展開";
            }

            if (node != null)
            {
                treeView1.SelectedNode = node;
                node.EnsureVisible();
                treeView1.Focus();
            }
        }

        //インシデント印刷
        private void print_incident_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //印刷の表示
            Form_print form = new Form_print();

            form.con = con;

            //form.incidentDSList = incidentDSList;
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
        //詳細画面
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            string strSelectedname;
            //カスタマ名のみ取得
            if (treeView1.SelectedNode.Level == 0)
            {
                //選択されたノード
                strSelectedname = treeView1.SelectedNode.Text;

                Dictionary<string, string> param_dict = new Dictionary<string, string>() ;
                DISP_dataSet displist = new DISP_dataSet() ;
                Class_Detaget dg = new Class_Detaget();

                param_dict["username"] = strSelectedname;
                displist = dg.getSelectUser(param_dict, con, displist);

                ListView.SelectedIndexCollection item = userList.SelectedIndices;
                Form_UserDetail formdetail = new Form_UserDetail();

                formdetail.con = con;

                formdetail.loginDS = loginDS;

                //ユーザ情報を取得する
                if(displist.user_L == null )
                {
                    MessageBox.Show("カスタマ情報を取得できませんでした。","");
                    logger.ErrorFormat("カスタマ詳細画面の表示時、カスタマ情報を取得できませんでした");
                    formdetail.Show();
                    formdetail.Owner = this;
                    return;
                }
                formdetail.userdt = displist.user_L[0];
                formdetail.Show();
                formdetail.Owner = this;

            }
          
        }
        //ツリービューでダブルクリックされたとき
        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //構成情報検索画面が選択されているときのみ
            if (this.tabControl1.SelectedIndex != 1)
                return;

            //カスタマ名のみ取得
            if (treeView1.SelectedNode.Level == 0)
            {
                m_usernameCombo.Text = "";
                //m_systemCombo.Text = "";
                //選択されたノード
                m_usernameCombo.Text = treeView1.SelectedNode.Text;
            }
            else if (treeView1.SelectedNode.Level == 1)
            {
                m_usernameCombo.Text = "";
                //m_systemCombo.Text = "";
                //がダブルクリック
                m_usernameCombo.Text = treeView1.SelectedNode.Parent.Text;                
                //m_systemCombo.Text = treeView1.SelectedNode.Text;
            }
        }
        //カスタマメンテ
        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenMente_customer();
        }
        //システムメンテ
        private void m_systemMente_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenMente_system();
        }
        //拠点メンテ
        private void m_siteMente_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenMente_site();
        }
        //ホストメンテ
        private void m_hostMente_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenMente_host();
        }
        //インターフェイスメンテ
        private void linkLabel13_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenMente_interface();
        }
        //回線情報メンテ
        private void linkLabel14_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenMente_kaisen();
        }
        //キーダウン
        private void Form_MainList_KeyDown(object sender, KeyEventArgs e)
        {
            //F5キーが押されたか調べる
            if (e.KeyData == Keys.F5)
                m_refresh_btn.PerformClick();
        }
        //オペレータ登録
        private void button8_Click(object sender, EventArgs e)
        {
            Form_opeInsert ope = new Form_opeInsert();
            ope.con = con;
            ope.loginDS = loginDS;
            ope.ShowDialog(this);
        }
        //オペレータ編集
        private void m_opeUpdateBtn_Click(object sender, EventArgs e)
        {
            Form_opeDetail opeDetail = new Form_opeDetail();
            opeDetail.con = con;
            opeDetail.loginDS = loginDS;
            opeDetail.ShowDialog(this);
        }



        private void m_SummaryBtn_Click(object sender, EventArgs e)
        {
            //インシデントの受付日時から手配日時の時間が15分以上のインシデントを取得する
            Form_incidentSummary icdForm = new Form_incidentSummary();
            icdForm.con = con;
            icdForm.loginDS = loginDS;
            icdForm.Show(this);
        }

        private void m_mailTemplateInsert_Click(object sender, EventArgs e)
        {
            Form_MailTempleteInsert mailInsert = new Form_MailTempleteInsert();
            mailInsert.con = con;
            mailInsert.loginDS = loginDS;
            mailInsert.ShowDialog(this);
        }

        private void m_orbcomm_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                String str = System.Configuration.ConfigurationManager.AppSettings["orbcomm_path"];
                // Excel操作用オブジェクト
                Microsoft.Office.Interop.Excel.Application xlApp = null;
                Microsoft.Office.Interop.Excel.Workbooks xlBooks = null;
                Microsoft.Office.Interop.Excel.Workbook xlBook = null;
                //                Microsoft.Office.Interop.Excel.Sheets xlSheets = null;
                //                Microsoft.Office.Interop.Excel.Worksheet xlSheet = null;

                // Excelアプリケーション生成
                xlApp = new Microsoft.Office.Interop.Excel.Application();

                // ◆操作対象のExcelブックを開く◆
                // Openメソッド
                xlBooks = xlApp.Workbooks;
                xlBook = xlBooks.Open(System.IO.Path.GetFullPath(@str));
                xlApp.Visible = true;

                // Book解放
                //xlBook.Close();
                // System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
                // System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBooks);

                // Excelアプリケーションを解放
                //xlApp.Quit();
                // System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excelファイルの表示時に障害が発生しました。" + ex.Message);
                logger.ErrorFormat("Excelファイルの表示時に障害が発生しました" + ex.Message);


            }
        }




        private void カスタマ情報UToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = Disp_FileSelectDlg();
            if (filePath == "")
            {
                return;
            }

            userDS uds;
            List<userDS> list_userDS = new List<userDS>();

            try
            {

                StreamReader reader = new StreamReader(filePath, Encoding.GetEncoding("Shift_JIS"));

                TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(","); // 区切り文字はコンマ


                while (!parser.EndOfData)
                {
                    uds = new userDS();
                    string[] cols = parser.ReadFields(); // 1行読み込み

                    //1文字目#はコメント
                    string str = "";
                    if (str.StartsWith("#"))
                        continue;
                    uds.customerID = cols[6];
                    //カスタマ名
                    uds.username = cols[0];
                    //カスタマメイカナ
                    uds.username_kana = cols[1];
                    //カスタマ名略
                    uds.username_sum = cols[2];

                    //レポート有無
                    if (cols[6] == "○")
                        uds.report_status = "1";
                    else
                        uds.report_status = "0";

                    //備考
                    uds.biko = cols[3];

                    //最終更新日時
                    uds.chk_date = cols[7];


                    list_userDS.Add(uds);


                }
                reader.Close();

                if (MessageBox.Show(list_userDS.Count + "件登録します。よろしいですか？", "カスタマインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

            }
            catch (Exception ex)
            {
                MessageBox.Show("CSVファイルの読み込み時にエラーが発生しました。" + ex.Message);
                logger.ErrorFormat("カスタマのインポートエラー。CSVファイルの読み込み時にエラーが発生しました。 ");

                return;

            }
            //DB接続
            if (con.FullState != ConnectionState.Open) con.Open();

            NpgsqlCommand cmd;
            string rep_status;


            using (var transaction = con.BeginTransaction())
            {

                //インサート文
                int i = 0;
                foreach (userDS us in list_userDS)
                {
                    cmd = new NpgsqlCommand(@"insert into user_tbl(username, username_kana,username_sum,status,report_status,biko,chk_date,chk_name_id) 
                    values ( :username,:username_kana,:username_sum,:status,:report_status,:biko,:chk_date,:chk_name_id)", con);
                    try
                    {
                        //ステータス
                        if (us.report_status == "有効")
                            rep_status = "1";
                        else
                            rep_status = "0";

                        Int32 rowsaffected;
                        //データ登録
                        cmd.Parameters.Add(new NpgsqlParameter("username", DbType.String) { Value = us.username });
                        cmd.Parameters.Add(new NpgsqlParameter("username_kana", DbType.String) { Value = us.username_kana });
                        cmd.Parameters.Add(new NpgsqlParameter("username_sum", DbType.String) { Value = us.username_sum });
                        cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = "1" });
                        cmd.Parameters.Add(new NpgsqlParameter("report_status", DbType.String) { Value = rep_status });
                        cmd.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = us.biko });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = DateTime.Parse(us.chk_date) });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });

                        rowsaffected = cmd.ExecuteNonQuery();

                        if (rowsaffected != 1)
                        {

                            if (MessageBox.Show("登録できませんでした。カスタマ名:" + us.username + Environment.NewLine + " 継続しますか？", "カスタマ登録", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                logger.ErrorFormat("カスタマ登録に失敗しました。カスタマ名:{0}", us.username);
                                transaction.Rollback();
                                return;
                            }
                        }
                        else
                        {
                            //登録成功
                            i++;
                            //MessageBox.Show("登録完了", "カスタマ登録");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("カスタマ登録時エラー " + ex.Message);
                        logger.ErrorFormat("カスタマ登録に失敗しました。カスタマ名:{0}", us.username);

                        return;
                    }


                }
                //終わったらコミットする
                transaction.Commit();
                MessageBox.Show("カスタマインポート" + i + "件 ");

            }
        }
        //ファイル選択ダイアログを表示
        private string Disp_FileSelectDlg()
        {

            string retStr = "";

            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();

            //はじめのファイル名を指定する
            //はじめに「ファイル名」で表示される文字列を指定する
            ofd.FileName = "";
            //はじめに表示されるフォルダを指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            ofd.InitialDirectory = "";
            //[ファイルの種類]に表示される選択肢を指定する
            //指定しないとすべてのファイルが表示される
            ofd.Filter = "CSVファイル(*.csv)|*.csv";
            //[ファイルの種類]ではじめに
            //「すべてのファイル」が選択されているようにする
            ofd.FilterIndex = 2;
            //タイトルを設定する
            ofd.Title = "CSVファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            ofd.RestoreDirectory = true;
            //存在しないファイルの名前が指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckFileExists = true;
            //存在しないパスが指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckPathExists = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき
                //選択されたファイル名を表示する
                retStr = ofd.FileName;
            }

            return retStr;
        }
        private String getCustomerNo(String Customername, NpgsqlConnection con)
        {
            String userno = "";
            NpgsqlCommand cmd;
            try
            {
                //DB接続
                if (con.FullState != ConnectionState.Open) con.Open();


                cmd = new NpgsqlCommand(@"SELECT userno,username FROM user_tbl WHERE username = :username", con);

                cmd.Parameters.Add(new NpgsqlParameter("username", DbType.String) { Value = Customername });

                var dataReader = cmd.ExecuteReader();

                //データを読み取る
                while (dataReader.Read())
                {
                    userno = dataReader["userno"].ToString();

                }
                //
                if (userno == null || userno == "")
                {
                    // MessageBox.Show("カスタマNOが取得できませんでした。カスタマ名：" + Customername, "システムインポート", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("カスタマNOが取得できませんでした エラー : " + ex.Message);
                logger.ErrorFormat("カスタマNOが取得できませんでした。カスタマ名:{0}", Customername);

                return "";

            }
            return userno;

        }

        //システムNOの取得
        private String getSystemNo(String userno, String Systemname, NpgsqlConnection con)
        {
            String systemno = "";
            NpgsqlCommand cmd;
            try
            {
                cmd = new NpgsqlCommand(@"SELECT systemno,systemname FROM system WHERE userno = :userno AND systemname = :systemname", con);

                cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = int.Parse(userno) });
                cmd.Parameters.Add(new NpgsqlParameter("systemname", DbType.String) { Value = Systemname });

                var dataReader = cmd.ExecuteReader();

                //データを読み取る
                while (dataReader.Read())
                {
                    systemno = dataReader["systemno"].ToString();

                }
                //
                if (systemno == null || systemno == "")
                {
                    // MessageBox.Show("システムNOが取得できませんでした。システム名：" + Systemname, "拠点インポート", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    cmd = new NpgsqlCommand(@"SELECT systemno,systemname FROM system WHERE userno = :userno ", con);

                    cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = int.Parse(userno) });

                    dataReader = cmd.ExecuteReader();

                    //データを読み取る
                    while (dataReader.Read())
                    {
                        systemno = dataReader["systemno"].ToString();
                        if (systemno == null || systemno == "")
                        {
                            logger.Warn("システムNOが取得できませんでした。システム名:" + Systemname);
                            return "";

                        }
                        else
                        {
                            logger.Warn("システムNOが取得できませんでした。存在するシステムIDを返します。　システム名:" + Systemname);

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー : " + ex.Message);
                logger.ErrorFormat("システムNOが取得できませんでした。システム名:{0}", Systemname);
                return "";

            }
            return systemno;

        }

        //拠点NOの取得
        private String getSiteNo(String userno, String systemno, String sitename, NpgsqlConnection con)
        {
            String siteno = "";
            NpgsqlCommand cmd;
            try
            {
                cmd = new NpgsqlCommand(@"SELECT siteno,sitename FROM site WHERE userno = :userno AND systemno = :systemno AND sitename = :sitename", con);

                cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = int.Parse(userno) });
                cmd.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = int.Parse(systemno) });
                cmd.Parameters.Add(new NpgsqlParameter("sitename", DbType.String) { Value = sitename });

                var dataReader = cmd.ExecuteReader();

                //データを読み取る
                while (dataReader.Read())
                {
                    siteno = dataReader["siteno"].ToString();

                }
                //
                if (siteno == null || siteno == "")
                {
                    // MessageBox.Show("拠点NOが取得できませんでした。拠点名：" + Sitename, "ホストインポート", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー : " + ex.Message);
                logger.ErrorFormat("拠点NOが取得できませんでした。拠点名:{0}", sitename);
                return "";

            }
            return siteno;

        }

        //ホスト名
        private String getHostNo(String userno, String systemno, String siteno, String hostname, NpgsqlConnection con)
        {
            String hostno = "";
            NpgsqlCommand cmd;
            try
            {
                cmd = new NpgsqlCommand(@"SELECT host_no,hostname FROM host WHERE userno = :userno AND systemno = :systemno AND siteno = :siteno AND hostname =:hostname", con);

                cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = int.Parse(userno) });
                cmd.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = int.Parse(systemno) });
                cmd.Parameters.Add(new NpgsqlParameter("siteno", DbType.Int32) { Value = int.Parse(siteno) });
                cmd.Parameters.Add(new NpgsqlParameter("hostname", DbType.String) { Value = hostname });

                var dataReader = cmd.ExecuteReader();

                //データを読み取る
                while (dataReader.Read())
                {
                    hostno = dataReader["host_no"].ToString();

                }
                //
                if (hostno == null || hostno == "")
                {
                    // MessageBox.Show("ホストNOが取得できませんでした。拠点名：" + hostname, "ホストインポート", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー : " + ex.Message);
                logger.ErrorFormat("ホストNOが取得できませんでした。ホスト名:{0}", hostname);

                return "";

            }
            return hostno;
        }

        //ユーザ担当者番号の取得
        private String getuserTantouNo(String userno, String UsertanntouName, NpgsqlConnection con)
        {
            String tantouno = "";
            NpgsqlCommand cmd;
            try
            {
                cmd = new NpgsqlCommand(@"SELECT user_tantou_no,user_tantou_name FROM user_tanntou WHERE userno = :userno AND user_tantou_name = :user_tantou_name", con);

                cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = int.Parse(userno) });
                cmd.Parameters.Add(new NpgsqlParameter("user_tantou_name", DbType.String) { Value = UsertanntouName });

                var dataReader = cmd.ExecuteReader();

                //データを読み取る
                while (dataReader.Read())
                {
                    tantouno = dataReader["user_tantou_no"].ToString();
                }
                //
                if (tantouno == null || tantouno == "")
                {
                    // MessageBox.Show("ユーザ担当者NOが取得できませんでした。ユーザ担当者番号：" + tantouno, "ホストインポート", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー : " + ex.Message);
                logger.ErrorFormat("ユーザ担当者NOが取得できませんでした。ユーザ担当者:{0}", UsertanntouName);
                return "";
            }
            return tantouno;
        }
        //システムデータのインポート
        private void システムSToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //ファイル選択ダイアログ
            string filePath = Disp_FileSelectDlg();
            if (filePath == "")
            {
                return;
            }
            systemDS sds;
            List<systemDS> list_systemDS = new List<systemDS>();

            try
            {

                TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(","); // 区切り文字はコンマ


                while (!parser.EndOfData)
                {
                    sds = new systemDS();
                    string[] cols = parser.ReadFields(); // 1行読み込み

                    //1行目#はコメント
                    string str = "";
                    str = cols[0];
                    if (str.StartsWith("#"))
                        continue;

                    //会社名
                    sds.username = cols[0];
                    //システム名
                    sds.systemname = cols[1];
                    //備考(CSC管理番号)
                    sds.biko = cols[3] + Environment.NewLine + "NESIC-CSC管理番号:" + cols[4] + Environment.NewLine;

                    //更新日時
                    sds.chk_date = cols[5];

                    list_systemDS.Add(sds);

                }
                parser.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSVファイルの読み込み時にエラーが発生しました。" + ex.Message);
                logger.ErrorFormat("システム情報のインポートエラー。CSVファイルの読み込み時にエラーが発生しました。");

                return;
            }
            if (MessageBox.Show(list_systemDS.Count + "件登録します。よろしいですか？", "システムインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            //DB接続

            if (con.FullState != ConnectionState.Open) con.Open();

            NpgsqlCommand cmd;

            using (var transaction = con.BeginTransaction())
            {

                //インサート文
                int i = 0;
                foreach (systemDS us in list_systemDS)
                {
                    //カスタマNOの取得
                    us.userno = getCustomerNo(us.username, con);
                    if (us.userno == "")
                        //継続
                        continue;


                    cmd = new NpgsqlCommand(@"insert into system(systemname,biko,userno,chk_date,chk_name_id) 
                    values ( :systemname,:biko,:userno,:chk_date,:chk_name_id)", con);

                    try
                    {

                        Int32 rowsaffected;
                        //データ登録
                        cmd.Parameters.Add(new NpgsqlParameter("systemname", DbType.String) { Value = us.systemname });
                        cmd.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = us.biko });
                        cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = int.Parse(us.userno) });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = DateTime.Parse(us.chk_date) });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });

                        rowsaffected = cmd.ExecuteNonQuery();

                        if (rowsaffected != 1)
                        {

                            if (MessageBox.Show("登録できませんでした。システム名:" + us.username + Environment.NewLine + " 継続しますか？", "システム情報のインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                transaction.Rollback();
                                return;
                            }
                        }
                        else
                        {
                            //登録成功
                            i++;
                            //MessageBox.Show("登録完了", "システム名");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("システム情報のインポート" + ex.Message);
                        logger.ErrorFormat("システム情報のインポートエラー。{0} システム:{1}", ex.Message, us.systemname);

                        return;
                    }


                }
                //終わったらコミットする
                transaction.Commit();

                //システム名がないカスタマをチェックする
                Int64 nosysCnt = check_noSystem();
                if (nosysCnt >= 1)
                {
                    if (MessageBox.Show("システムが存在しないカスタマが" + nosysCnt.ToString() + "件存在します。「NW監視」で固定登録します。" + Environment.NewLine
                        + "よろしいですか?", "システムインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //システムを固定で挿入する
                        systemInsert();

                }


                MessageBox.Show("システム情報のインポート" + i + "件 ");

            }

        }
        //システムが存在しないカスタマがあるか確認する 件数を返す
        private Int64 check_noSystem()
        {
            String sql = "select count(userno) from user_tbl where not exists (select systemno from system where user_tbl.userno = system.userno)";

            Int64 Count = 0;

            NpgsqlCommand cmd;


            try
            {
                //DB接続
                if (con.FullState != ConnectionState.Open) con.Open();

                //SELECT実行
                cmd = new NpgsqlCommand(@sql, con);
                Count = (Int64)cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                MessageBox.Show("システムテーブルチェック時にエラー " + ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.ErrorFormat("システムテーブルチェック時にエラー メソッド名：{0}。MSG：{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return -1;
            }

            return Count;
        }
        //システムを固定でインサートする
        private void systemInsert()
        {
            //DB接続
            if (con.FullState != ConnectionState.Open) con.Open();

            NpgsqlCommand cmd;

            using (var transaction = con.BeginTransaction())
            {

                //インサート文

                cmd = new NpgsqlCommand(@"  insert into system (systemname,systemkana,userno,chk_date,chk_name_id) " +
                    "select 'NM監視','ネットワーク監視',userno,:chk_date,:chk_name_id from user_tbl where not exists (select systemno from system where user_tbl.userno = system.userno)", con);

                try
                {
                    Int32 rowsaffected;
                    //データ登録
                    cmd.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = DateTime.Now });
                    cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });

                    rowsaffected = cmd.ExecuteNonQuery();

                    if (rowsaffected < 1)
                    {

                        transaction.Rollback();
                        MessageBox.Show("固定のシステム「NW監視」が登録できませんでした。", "システムインサート", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        logger.ErrorFormat("固定のシステムが登録できませんでした。");
                        return;

                    }
                    else
                    {
                        //終わったらコミットする
                        transaction.Commit();
                        MessageBox.Show("固定のシステム「NW監視システム」を登録しました " + rowsaffected + "件 ");
                    }
                }
                catch (Exception ex)
                {
                    logger.ErrorFormat("固定のシステム「NW監視システム」を登録時にエラーが発生しました。{0}", ex.Message);
                    MessageBox.Show("固定のシステム「NW監視システム」を登録時にエラーが発生しました。{0}" + ex.Message);
                    //transaction.Rollback();
                    return;
                }


            }

        }
        private void カスタマ担当者TToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ファイル選択ダイアログ
            string filePath = Disp_FileSelectDlg();
            if (filePath == "")
            {
                return;
            }
            tantouDS sds;
            List<tantouDS> list_tantouDS = new List<tantouDS>();
            try
            {
                TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(","); // 区切り文字はコンマ


                while (!parser.EndOfData)
                {
                    sds = new tantouDS();
                    string[] cols = parser.ReadFields(); // 1行読み込み

                    //1行目#はコメント
                    string str = "";
                    str = cols[0];
                    if (str.StartsWith("#"))
                        continue;



                    //ステータス
                    if (cols[0] == "○")
                        sds.status = "1";
                    if (cols[0] == "×")
                        sds.status = "2";


                    //社員名
                    sds.user_tantou_name = cols[1];
                    sds.user_tantou_name_kana = cols[2];

                    //会社名
                    sds.username = cols[7];

                    //役職
                    sds.yakusyoku = cols[5];

                    //部署名
                    sds.busho_name = cols[8];


                    //備考
                    sds.biko = cols[11];

                    //更新日時
                    sds.chk_date = cols[12];


                    list_tantouDS.Add(sds);


                }
                parser.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSVファイルの読み込み時にエラーが発生しました。" + ex.Message);
                logger.ErrorFormat("カスタマ担当者のインポートエラー。CSVファイルの読み込み時にエラーが発生しました。");

                return;
            }
            if (MessageBox.Show(list_tantouDS.Count + "件登録します。よろしいですか？", "担当者インポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            //DB接続

            if (con.FullState != ConnectionState.Open) con.Open();

            NpgsqlCommand cmd;

            using (var transaction = con.BeginTransaction())
            {

                //インサート文
                int i = 0;
                int cnt = 0;
                foreach (tantouDS si in list_tantouDS)
                {
                    //カスタマNOの取得
                    si.userno = getCustomerNo(si.username, con);
                    if (si.userno == "")
                        //継続
                        continue;

                    //ステータス
                    string stat = "";
                    if (si.status == "有効")
                        stat = "1";
                    else if (si.status == "無効")
                        stat = "2";

                    DateTime datet;
                    DateTime.TryParse(si.chk_date, out datet);


                    cmd = new NpgsqlCommand(@"insert into user_tanntou(user_tantou_name,user_tantou_name_kana,busho_name,yakusyoku,status,biko,userno,chk_date,chk_name_id) 
                    values (:user_tantou_name,:user_tantou_name_kana,:busho_name,:yakusyoku,:status,:biko,:userno,:chk_date,:chk_name_id)", con);

                    try
                    {
                        Int32 rowsaffected;
                        //データ登録
                        cmd.Parameters.Add(new NpgsqlParameter("user_tantou_name", DbType.String) { Value = si.user_tantou_name });
                        cmd.Parameters.Add(new NpgsqlParameter("user_tantou_name_kana", DbType.String) { Value = si.user_tantou_name_kana });
                        cmd.Parameters.Add(new NpgsqlParameter("busho_name", DbType.String) { Value = si.busho_name });
                        cmd.Parameters.Add(new NpgsqlParameter("yakusyoku", DbType.String) { Value = si.yakusyoku });
                        cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = stat });
                        cmd.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = si.biko });
                        cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = int.Parse(si.userno) });

                        cmd.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = datet });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });

                        rowsaffected = cmd.ExecuteNonQuery();

                        if (rowsaffected != 1)
                        {

                            if (MessageBox.Show("登録できませんでした。担当者名:" + si.user_tantou_name + Environment.NewLine + " 継続しますか？", "担当者情報のインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                transaction.Rollback();
                                return;
                            }
                        }
                        else
                        {
                            //登録成功
                            i++;
                            //MessageBox.Show("登録完了", "システム名");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("担当者名情報のインポート時" + cnt + "件目でエラーが発生しました。" + ex.Message);
                        //transaction.Rollback();
                        return;
                    }


                }
                //終わったらコミットする
                transaction.Commit();
                MessageBox.Show("担当者名情報のインポート" + i + "件 ");

            }
        }

        private void カスタマ担当者電話番号DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ファイル選択ダイアログ
            string filePath = Disp_FileSelectDlg();
            if (filePath == "")
            {
                return;
            }
            tantouDS sds;
            List<tantouDS> list_tantouDS = new List<tantouDS>();
            try
            {
                TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(","); // 区切り文字はコンマ


                while (!parser.EndOfData)
                {
                    sds = new tantouDS();
                    string[] cols = parser.ReadFields(); // 1行読み込み

                    //1行目#はコメント
                    string str = "";
                    str = cols[0];
                    if (str.StartsWith("#"))
                        continue;



                    //ステータス
                    if (cols[0] == "○")
                        sds.status = "1";
                    if (cols[0] == "×")
                        sds.status = "2";

                    if (cols[1] == "TEL")
                        sds.telno1 = cols[2];
                    if (cols[1] == "携帯")
                        sds.telno2 = cols[2];

                    //社員名
                    sds.user_tantou_name = cols[5];

                    //更新日時
                    sds.chk_date = cols[13];

                    list_tantouDS.Add(sds);

                }
                parser.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSVファイルの読み込み時にエラーが発生しました。" + ex.Message);
                logger.ErrorFormat("カスタマ担当者のインポートエラー。CSVファイルの読み込み時にエラーが発生しました。");

                return;
            }

            if (MessageBox.Show(list_tantouDS.Count + "件の電話番号を登録します。よろしいですか？", "電話番号インポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //DB接続
            if (con.FullState != ConnectionState.Open) con.Open();

            NpgsqlCommand cmd;

            using (var transaction = con.BeginTransaction())
            {

                //インサート文
                int i = 0;
                int cnt = 0;
                foreach (tantouDS si in list_tantouDS)
                {

                    cmd = new NpgsqlCommand(@"update user_tanntou SET telno1=:telno1 ,telno2=:telno2,chk_date=:chk_date WHERE user_tantou_name=:user_tantou_name", con);

                    try
                    {
                        DateTime datet;
                        DateTime.TryParse(si.chk_date, out datet);

                        Int32 rowsaffected;
                        //データ登録
                        cmd.Parameters.Add(new NpgsqlParameter("user_tantou_name", DbType.String) { Value = si.user_tantou_name });
                        cmd.Parameters.Add(new NpgsqlParameter("telno1", DbType.String) { Value = si.telno1 });
                        cmd.Parameters.Add(new NpgsqlParameter("telno2", DbType.String) { Value = si.telno2 });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = datet });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });

                        rowsaffected = cmd.ExecuteNonQuery();

                        if (rowsaffected != 1)
                        {

                            //if (MessageBox.Show("電話番号を登録できませんでした。担当者名:" + si.user_tantou_name + Environment.NewLine + " 継続しますか？", "担当者情報(電話)のインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            //{
                            //    transaction.Rollback();
                            //    return;
                            //}
                        }
                        else
                        {
                            //登録成功
                            i++;
                            //MessageBox.Show("登録完了", "システム名");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("電話番号のインポート時" + cnt + "件目でエラーが発生しました。" + ex.Message);
                        //transaction.Rollback();
                        return;
                    }

                }
                //終わったらコミットする
                transaction.Commit();
                MessageBox.Show("電話番号のインポート" + i + "件 ");

            }
        }

        private void メールアドレスMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ファイル選択ダイアログ
            string filePath = Disp_FileSelectDlg();
            if (filePath == "")
                return;

            MailaddressDS sds;
            List<MailaddressDS> list_mailaddressDS = new List<MailaddressDS>();

            try
            {

                TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(","); // 区切り文字はコンマ
                String u_tantou = "";
                int idx = 1;
                while (!parser.EndOfData)
                {
                    sds = new MailaddressDS();
                    string[] cols = parser.ReadFields(); // 1行読み込み

                    //1行目#はコメント
                    string str = "";
                    str = cols[0];
                    if (str.StartsWith("#"))
                        continue;



                    //担当名が重複する場合
                    if (u_tantou != cols[4])
                    {
                        u_tantou = cols[4];
                        idx = 1;
                    }
                    //if (0 <= cols[1].IndexOf("Mail")) {
                    //    if (0 <= cols[1].IndexOf("会社"))
                    //sds.addressNo = idx.ToString() ;
                    //else if (0 <= cols[1].IndexOf("携帯"))
                    //sds.addressNo = idx.ToString();
                    //else
                    //sds.addressNo = idx.ToString();
                    //}
                    sds.addressNo = idx.ToString();
                    idx++;


                    //会社名
                    sds.username = cols[8];

                    //カスタマNOの取得
                    sds.userno = getCustomerNo(sds.username, con);
                    if (sds.userno == "")
                        //継続
                        continue;

                    //社員名
                    sds.user_tantou_name = cols[4];
                    //社員NOの取得
                    sds.opetantouno = getuserTantouNo(sds.userno, sds.user_tantou_name, con);
                    if (sds.opetantouno == "")
                        //継続
                        continue;


                    //メールアドレス
                    sds.mailAddress = cols[2];
                    //メール名
                    sds.addressname = cols[17];


                    //更新日時
                    sds.chk_date = cols[13];


                    list_mailaddressDS.Add(sds);

                }
                parser.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSVファイルの読み込み時にエラーが発生しました。" + ex.Message);
                logger.ErrorFormat("メールアドレスのインポートエラー。CSVファイルの読み込み時にエラーが発生しました。");

                return;
            }
            if (MessageBox.Show(list_mailaddressDS.Count + "件のメールアドレスを登録します。よろしいですか？", "メールアドレスインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //DB接続
            if (con.FullState != ConnectionState.Open) con.Open();

            NpgsqlCommand cmd;

            using (var transaction = con.BeginTransaction())
            {

                //インサート文
                int i = 0;
                int cnt = 0;
                foreach (MailaddressDS si in list_mailaddressDS)
                {

                    cmd = new NpgsqlCommand(@"insert into mailaddress(kubun,opetantouno,addressno,mailaddress,addressname,chk_date,chk_name_id) 
                    values (:kubun,:opetantouno,:addressno,:mailaddress,:addressname,:chk_date,:chk_name_id)", con);
                    try
                    {
                        DateTime datet;
                        DateTime.TryParse(si.chk_date, out datet);

                        Int32 rowsaffected;
                        //データ登録
                        //カスタマ担当者
                        cmd.Parameters.Add(new NpgsqlParameter("kubun", DbType.String) { Value = "2" });
                        cmd.Parameters.Add(new NpgsqlParameter("opetantouno", DbType.Int32) { Value = int.Parse(si.opetantouno) });
                        cmd.Parameters.Add(new NpgsqlParameter("addressno", DbType.Int32) { Value = int.Parse(si.addressNo) });
                        cmd.Parameters.Add(new NpgsqlParameter("mailaddress", DbType.String) { Value = si.mailAddress });
                        cmd.Parameters.Add(new NpgsqlParameter("addressname", DbType.String) { Value = si.addressname });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = datet });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });

                        rowsaffected = cmd.ExecuteNonQuery();

                        if (rowsaffected != 1)
                        {

                            //if (MessageBox.Show("メールアドレスを登録できませんでした。担当者名:" + si.user_tantou_name + Environment.NewLine + " 継続しますか？", "担当者情報(電話)のインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            //{
                            //    transaction.Rollback();
                            //    return;
                            //}
                        }
                        else
                        {
                            //登録成功
                            i++;
                            //MessageBox.Show("登録完了", "システム名");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("メールアドレスのインポート時" + cnt + "件目でエラーが発生しました。" + ex.Message);
                        logger.ErrorFormat("メールアドレスのインポート時" + cnt + "件目でエラーが発生しました。MSG:{0}", ex.Message);

                        //transaction.Rollback();
                        return;
                    }

                }
                //終わったらコミットする
                transaction.Commit();
                MessageBox.Show("メールアドレスのインポート" + i + "件 ");

            }
        }

        private void システムSToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            //ファイル選択ダイアログ
            string filePath = Disp_FileSelectDlg();
            if (filePath == "")
            {
                return;
            }
            systemDS sds;
            List<systemDS> list_systemDS = new List<systemDS>();

            try
            {

                TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(","); // 区切り文字はコンマ


                while (!parser.EndOfData)
                {
                    sds = new systemDS();
                    string[] cols = parser.ReadFields(); // 1行読み込み

                    //1行目#はコメント
                    string str = "";
                    str = cols[0];
                    if (str.StartsWith("#"))
                        continue;

                    //会社名
                    sds.username = cols[0];
                    //システム名
                    sds.systemname = cols[1];
                    //備考(CSC管理番号)
                    sds.biko = cols[3] + Environment.NewLine + "NESIC-CSC管理番号:" + cols[4] + Environment.NewLine;

                    //更新日時
                    sds.chk_date = cols[5];

                    list_systemDS.Add(sds);

                }
                parser.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSVファイルの読み込み時にエラーが発生しました。" + ex.Message);
                logger.ErrorFormat("システム情報のインポートエラー。CSVファイルの読み込み時にエラーが発生しました。");

                return;
            }
            if (MessageBox.Show(list_systemDS.Count + "件登録します。よろしいですか？", "システムインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            //DB接続

            if (con.FullState != ConnectionState.Open) con.Open();

            NpgsqlCommand cmd;

            using (var transaction = con.BeginTransaction())
            {

                //インサート文
                int i = 0;
                foreach (systemDS us in list_systemDS)
                {
                    //カスタマNOの取得
                    us.userno = getCustomerNo(us.username, con);
                    if (us.userno == "")
                        //継続
                        continue;


                    cmd = new NpgsqlCommand(@"insert into system(systemname,biko,userno,chk_date,chk_name_id) 
                    values ( :systemname,:biko,:userno,:chk_date,:chk_name_id)", con);

                    try
                    {

                        Int32 rowsaffected;
                        //データ登録
                        cmd.Parameters.Add(new NpgsqlParameter("systemname", DbType.String) { Value = us.systemname });
                        cmd.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = us.biko });
                        cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = int.Parse(us.userno) });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = DateTime.Parse(us.chk_date) });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });

                        rowsaffected = cmd.ExecuteNonQuery();

                        if (rowsaffected != 1)
                        {

                            if (MessageBox.Show("登録できませんでした。システム名:" + us.username + Environment.NewLine + " 継続しますか？", "システム情報のインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                transaction.Rollback();
                                return;
                            }
                        }
                        else
                        {
                            //登録成功
                            i++;
                            //MessageBox.Show("登録完了", "システム名");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("システム情報のインポート" + ex.Message);
                        logger.ErrorFormat("システム情報のインポートエラー。{0} システム:{1}", ex.Message, us.systemname);

                        return;
                    }


                }
                //終わったらコミットする
                transaction.Commit();

                //システム名がないカスタマをチェックする
                Int64 nosysCnt = check_noSystem();
                if (nosysCnt >= 1)
                {
                    if (MessageBox.Show("システムが存在しないカスタマが" + nosysCnt.ToString() + "件存在します。「NW監視」で固定登録します。" + Environment.NewLine
                        + "よろしいですか?", "システムインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //システムを固定で挿入する
                        systemInsert();

                }


                MessageBox.Show("システム情報のインポート" + i + "件 ");

            }
        }
        private void 拠点SToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //ファイル選択ダイアログ
            string filePath = Disp_FileSelectDlg();
            if (filePath == "")
            {
                return;
            }

            siteDS sds;
            List<siteDS> list_siteDS = new List<siteDS>();

            try
            {
                TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(","); // 区切り文字はコンマ

                while (!parser.EndOfData)
                {
                    sds = new siteDS();
                    string[] cols = parser.ReadFields(); // 1行読み込み

                    //1行目#はコメント
                    string str = "";
                    str = cols[0];
                    if (str.StartsWith("#"))
                        continue;

                    //会社名
                    sds.username = cols[0];

                    //システム名
                    sds.systemname = cols[16];

                    //ステータス
                    if (cols[1] == "○")
                        sds.status = "1";
                    if (cols[1] == "×")
                        sds.status = "2";
                    //拠点名
                    sds.sitename = cols[3];

                    //電話番号1
                    sds.telno = cols[4] + " :" + cols[5] + " :" + cols[6] + " :" + cols[7] + cols[8] + " :" + cols[9];
                    //郵便番号
                    sds.address1 = cols[10];
                    //住所
                    sds.address2 = cols[11];

                    //備考
                    sds.biko = cols[12] + Environment.NewLine + ":" + cols[16];

                    //更新日時
                    sds.chk_date = cols[13];


                    list_siteDS.Add(sds);


                }
                parser.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSVファイルの読み込み時にエラーが発生しました。" + ex.Message);
                logger.ErrorFormat("拠点情報のインポートエラー。CSVファイルの読み込み時にエラーが発生しました。");

                return;

            }
            if (MessageBox.Show(list_siteDS.Count + "件登録します。よろしいですか？", "拠点インポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            //DB接続
            if (con.FullState != ConnectionState.Open) con.Open();

            NpgsqlCommand cmd;

            using (var transaction = con.BeginTransaction())
            {

                //インサート文
                int i = 0;
                int cnt = 0;
                foreach (siteDS si in list_siteDS)
                {
                    //カスタマNOの取得
                    si.userno = getCustomerNo(si.username, con);
                    if (si.userno == "")
                        //継続
                        continue;

                    //システム番号の取得
                    si.systemno = getSystemNo(si.userno, si.systemname, con);
                    //                    if (si.systemno == "")
                    //継続
                    //   continue;
                    //ステータス
                    string stat = "";
                    if (si.status == "有効")
                        stat = "1";
                    else if (si.status == "無効")
                        stat = "2";
                    cmd = new NpgsqlCommand(@"insert into site(sitename,address1,address2,telno,status,biko,userno,systemno,chk_date,chk_name_id) 
                    values ( :sitename,:address1,:address2,:telno,:status,:biko,:userno,:systemno,:chk_date,:chk_name_id)", con);

                    try
                    {
                        int systemno = 0;
                        int.TryParse(si.systemno, out systemno);

                        Int32 rowsaffected;
                        //データ登録
                        cmd.Parameters.Add(new NpgsqlParameter("sitename", DbType.String) { Value = si.sitename });
                        cmd.Parameters.Add(new NpgsqlParameter("address1", DbType.String) { Value = si.address1 });
                        cmd.Parameters.Add(new NpgsqlParameter("address2", DbType.String) { Value = si.address2 });
                        cmd.Parameters.Add(new NpgsqlParameter("telno", DbType.String) { Value = si.telno });
                        cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = stat });
                        cmd.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = si.biko });
                        cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = int.Parse(si.userno) });
                        cmd.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = systemno });

                        DateTime datet;
                        if (si.chk_date == "")
                            datet = DateTime.Now;
                        DateTime.TryParse(si.chk_date, out datet);
                        cmd.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = datet });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });

                        rowsaffected = cmd.ExecuteNonQuery();

                        if (rowsaffected != 1)
                        {

                            if (MessageBox.Show("登録できませんでした。拠点名:" + si.sitename + Environment.NewLine + " 継続しますか？", "拠点情報のインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                logger.ErrorFormat("拠点名を登録できませんでした。{0} ", si.sitename);

                                transaction.Rollback();
                                return;
                            }
                        }
                        else
                        {
                            //登録成功
                            i++;
                            //MessageBox.Show("登録完了", "システム名");
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.ErrorFormat("拠点情報のインポート時" + cnt + "件目でエラーが発生しました。{0}", ex.Message);
                        MessageBox.Show("拠点情報のインポート時" + cnt + "件目でエラーが発生しました。" + ex.Message);
                        //transaction.Rollback();
                        return;
                    }


                }
                //終わったらコミットする
                transaction.Commit();
                MessageBox.Show("拠点情報のインポート" + i + "件 ");

            }
        }

        private void ホストHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ファイル選択ダイアログ
            string filePath = Disp_FileSelectDlg();
            if (filePath == "")
            {
                return;
            }
            hostDS hds;
            List<hostDS> list_hostDS = new List<hostDS>();
            try
            {
                TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(","); // 区切り文字はコンマ

                while (!parser.EndOfData)
                {
                    hds = new hostDS();
                    string[] cols = parser.ReadFields(); // 1行読み込み

                    //1行目#はコメント
                    string str = "";
                    str = cols[0];
                    if (str.StartsWith("#"))
                        continue;


                    //会社名
                    hds.username = cols[0];
                    //システム名
                    hds.systemname = cols[3];

                    //ステータス
                    if (cols[1] == "○")
                        hds.status = "1";
                    if (cols[1] == "×")
                        hds.status = "2";
                    //拠点名
                    hds.sitename = cols[4];
                    //ホスト名
                    hds.hostname = cols[6];
                    //機種
                    hds.device = cols[7];
                    //設置場所
                    hds.location = cols[8];
                    //用途
                    hds.usefor = "";

                    //設置機器ID
                    hds.settikikiid = cols[9];

                    //開始日時
                    hds.kansiStartdate = cols[10];
                    //終了日時
                    hds.kansiEndsdate = cols[11];

                    //機種保守情報を入れる
                    hds.hosyuinfo = cols[12];
                    //機種管理番号を入れる
                    hds.hosyukanri = cols[13];

                    hds.biko = cols[14] + " :" + cols[15] + " :" + cols[16];

                    //更新日時
                    hds.chk_date = cols[23];

                    list_hostDS.Add(hds);

                }
                parser.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSVファイルの読み込み時にエラーが発生しました。" + ex.Message);
                logger.ErrorFormat("ホストのインポートエラー。CSVファイルの読み込み時にエラーが発生しました。");

                return;
            }
            if (MessageBox.Show(list_hostDS.Count + "件登録します。よろしいですか？", "ホストインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            //DB接続
            if (con.FullState != ConnectionState.Open) con.Open();

            NpgsqlCommand cmd;

            using (var transaction = con.BeginTransaction())
            {

                //インサート文
                int i = 0;
                int cnt = 0;
                foreach (hostDS si in list_hostDS)
                {
                    //カスタマNOの取得
                    si.userno = getCustomerNo(si.username, con);
                    if (si.userno == "")
                        //継続
                        continue;

                    //システム番号の取得
                    si.systemno = getSystemNo(si.userno, si.systemname, con);

                    //if (si.systemno == "")
                    //継続
                    //   continue;
                    //拠点番号の取得
                    si.siteno = getSiteNo(si.userno, si.systemno, si.sitename, con);

                    //ステータス
                    string stat = "";
                    if (si.status == "有効")
                        stat = "1";
                    else if (si.status == "無効")
                        stat = "2";
                    cmd = new NpgsqlCommand(@"insert into host(hostname,settikikiid,status,device,location,usefor,kansistartdate,kansiendsdate,hosyukanri,hosyuinfo,biko,userno,systemno,siteno,chk_date,chk_name_id) 
                                       values ( :hostname,:settikikiid,:status,:device,:location,:usefor,:kansistartdate,:kansiendsdate,:hosyukanri,:hosyuinfo,:biko,:userno,:systemno,:siteno,:chk_date,:chk_name_id)", con);

                    try
                    {
                        int systemno = 0;
                        int.TryParse(si.systemno, out systemno);

                        Int32 rowsaffected;
                        //データ登録
                        cmd.Parameters.Add(new NpgsqlParameter("hostname", DbType.String) { Value = si.hostname });
                        cmd.Parameters.Add(new NpgsqlParameter("settikikiid", DbType.String) { Value = si.settikikiid });
                        cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = stat });
                        cmd.Parameters.Add(new NpgsqlParameter("device", DbType.String) { Value = si.device });
                        cmd.Parameters.Add(new NpgsqlParameter("location", DbType.String) { Value = si.location });
                        cmd.Parameters.Add(new NpgsqlParameter("usefor", DbType.String) { Value = si.usefor });


                        //監視開始日時
                        if (si.kansiStartdate == "" || si.kansiStartdate == null)
                        {
                            cmd.Parameters.Add(new NpgsqlParameter("kansistartdate", DbType.DateTime) { Value = null });
                        }
                        else
                        {
                            DateTime startdate;
                            DateTime.TryParse(si.kansiStartdate, out startdate);

                            cmd.Parameters.Add(new NpgsqlParameter("kansistartdate", DbType.DateTime) { Value = startdate });
                        }

                        //監視終了日時
                        if (si.kansiEndsdate == "" || si.kansiEndsdate == null)
                        {

                            cmd.Parameters.Add(new NpgsqlParameter("kansiendsdate", DbType.DateTime) { Value = null });
                        }
                        else
                        {
                            DateTime enddate;
                            DateTime.TryParse(si.kansiEndsdate, out enddate);

                            cmd.Parameters.Add(new NpgsqlParameter("kansiendsdate", DbType.DateTime) { Value = enddate });
                        }
                        cmd.Parameters.Add(new NpgsqlParameter("hosyukanri", DbType.String) { Value = si.hosyukanri });
                        cmd.Parameters.Add(new NpgsqlParameter("hosyuinfo", DbType.String) { Value = si.hosyuinfo });
                        cmd.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = si.biko });
                        cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = int.Parse(si.userno) });
                        cmd.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = systemno });
                        cmd.Parameters.Add(new NpgsqlParameter("siteno", DbType.Int32) { Value = si.siteno });

                        DateTime datet;
                        if (si.chk_date == "")
                            datet = DateTime.Now;
                        DateTime.TryParse(si.chk_date, out datet);
                        cmd.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = datet });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });

                        rowsaffected = cmd.ExecuteNonQuery();

                        if (rowsaffected != 1)
                        {

                            if (MessageBox.Show("ホスト情報を登録できませんでした。ホスト名:" + si.hostname + Environment.NewLine + " 継続しますか？", "ホスト情報のインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                transaction.Rollback();
                                logger.ErrorFormat("ホスト情報を登録できませんでした。{0} ", si.hostname);
                                cnt++;
                                return;
                            }
                        }
                        else
                        {
                            //登録成功
                            i++;
                            //MessageBox.Show("登録完了", "システム名");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ホスト情報のインポート時" + cnt + "行目にエラーが発生しました。  ホスト名：" + si.hostname + "  " + ex.Message);
                        //transaction.Rollback();
                        cnt++;
                        return;
                    }

                    cnt++;
                }
                //終わったらコミットする
                transaction.Commit();
                MessageBox.Show("ホスト情報のインポート" + i + "件 ");
            }
        }

        private void インターフェイスToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ファイル選択ダイアログ
            string filePath = Disp_FileSelectDlg();
            if (filePath == "")
                return;

            watch_InterfaceDS kds;
            List<watch_InterfaceDS> list_interface = new List<watch_InterfaceDS>();
            try
            {
                TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(","); // 区切り文字はコンマ

                String username = "";
                String systemname = "";
                String sitename = "";
                String hostname = "";

                while (!parser.EndOfData)
                {
                    kds = new watch_InterfaceDS();
                    string[] cols = parser.ReadFields(); // 1行読み込み

                    //1行目#はコメント
                    string str = "";
                    str = cols[0];
                    if (str.StartsWith("#"))
                        continue;

                    username = cols[13];
                    systemname = cols[11];
                    sitename = cols[9];
                    hostname = cols[7];

                    //カスタマNOの取得
                    kds.userno = getCustomerNo(username, con);
                    if (kds.userno == "")
                    {
                        logger.ErrorFormat("カスタマ通番が取得できませんでした。カスタマ名：" + username);
                        //継続
                        continue;
                    }
                    //システムNOの取得
                    kds.systemno = getSystemNo(kds.userno, systemname, con);
                    if (kds.systemno == "")
                    {
                        logger.ErrorFormat("システム通番が取得できませんでした。");
                        //継続
                        continue;
                    }

                    //拠点NOの取得
                    kds.siteno = getSiteNo(kds.userno, kds.systemno, sitename, con);
                    if (kds.siteno == "")
                    {
                        logger.ErrorFormat("拠点通番が取得できませんでした。");
                        //継続
                        continue;
                    }
                    //ホストNOの取得
                    kds.host_no = getHostNo(kds.userno, kds.systemno, kds.siteno, hostname, con);
                    if (kds.siteno == "")
                    {
                        logger.ErrorFormat("ホスト通番が取得できませんでした。");
                        //継続
                        continue;
                    }

                    kds.interfacename = cols[0];
                    kds.status = "1";
                    kds.type = cols[2];
                    kds.kanshi = cols[3];

                    kds.IPaddress = cols[4];
                    kds.border = cols[5];
                    kds.IPaddressNAT = cols[6];


                    //更新日時
                    kds.chk_date = cols[14];

                    list_interface.Add(kds);
                }
                parser.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSVファイルの読み込み時にエラーが発生しました。" + ex.Message);
                logger.ErrorFormat("監視インターフェイスのインポートエラー。CSVファイルの読み込み時にエラーが発生しました。");

                return;
            }

            if (MessageBox.Show(list_interface.Count + "件のインターフェイス情報を登録します。よろしいですか？", "インターフェイス情報インポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //DB接続
            if (con.FullState != ConnectionState.Open) con.Open();

            NpgsqlCommand cmd;

            using (var transaction = con.BeginTransaction())
            {

                //インサート文
                int i = 0;
                string status = "";
                foreach (watch_InterfaceDS sikds in list_interface)
                {
                    if (sikds.status == "有効")
                    {

                        status = "1";
                    }
                    if (sikds.status == "無効")
                    {
                        status = "0";

                    }

                    cmd = new NpgsqlCommand(@"insert into watch_Interface(interfacename,status,type,kanshi,border,ipaddress,ipaddressnat,biko,host_no,userno,systemno,siteno,chk_date,chk_name_id) 
                    values (:interfacename,:status,:type,:kanshi,:border,:ipaddress,:ipaddressnat,:biko,:host_no,:userno,:systemno,:siteno,:chk_date,:chk_name_id)", con);
                    try
                    {
                        DateTime datet;
                        DateTime.TryParse(sikds.chk_date, out datet);
                        if (sikds.chk_date == null || sikds.chk_date == "")
                            datet = DateTime.Now;



                        Console.WriteLine(datet.ToString());


                        Int32 rowsaffected;

                        //データ登録
                        int ret_userno;
                        int ret_systemno;
                        int ret_siteno;
                        int ret_host_no;

                        int.TryParse(sikds.userno, out ret_userno);
                        int.TryParse(sikds.systemno, out ret_systemno);
                        int.TryParse(sikds.siteno, out ret_siteno);
                        int.TryParse(sikds.host_no, out ret_host_no);
                        cmd.Parameters.Add(new NpgsqlParameter("interfacename", DbType.String) { Value = sikds.interfacename });
                        cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                        cmd.Parameters.Add(new NpgsqlParameter("type", DbType.String) { Value = sikds.type });
                        cmd.Parameters.Add(new NpgsqlParameter("kanshi", DbType.String) { Value = sikds.kanshi });

                        cmd.Parameters.Add(new NpgsqlParameter("border", DbType.String) { Value = sikds.border });
                        cmd.Parameters.Add(new NpgsqlParameter("ipaddress", DbType.String) { Value = sikds.IPaddress });
                        cmd.Parameters.Add(new NpgsqlParameter("ipaddressnat", DbType.String) { Value = sikds.IPaddressNAT });
                        cmd.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = sikds.biko });
                        cmd.Parameters.Add(new NpgsqlParameter("host_no", DbType.Int32) { Value = ret_host_no });
                        cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = ret_userno });
                        cmd.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = ret_systemno });
                        cmd.Parameters.Add(new NpgsqlParameter("siteno", DbType.Int32) { Value = ret_siteno });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = datet });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });

                        rowsaffected = cmd.ExecuteNonQuery();

                        if (rowsaffected != 1)
                        {

                            //if (MessageBox.Show("メールアドレスを登録できませんでした。担当者名:" + si.user_tantou_name + Environment.NewLine + " 継続しますか？", "担当者情報(電話)のインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            //{
                            //    transaction.Rollback();
                            //    return;
                            //}
                        }
                        else
                        {
                            //登録成功
                            i++;
                            //MessageBox.Show("登録完了", "システム名");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("監視インターフェイス情報のインポート時" + i + "件目でエラーが発生しました。" + ex.Message);
                        logger.ErrorFormat("監視インターフェイス情報のインポート時{0}件目でエラーが発生しました。:{1}", i, ex.Message);

                        //transaction.Rollback();
                        return;
                    }

                }
                //終わったらコミットする
                transaction.Commit();

                MessageBox.Show("監視インターフェイス情報" + i + "件 ");
            }
        }

        private void 回線情報LToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ファイル選択ダイアログ
            string filePath = Disp_FileSelectDlg();
            if (filePath == "")
                return;

            kaisenDS kds;
            List<kaisenDS> list_kaisen = new List<kaisenDS>();

            try
            {
                TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(","); // 区切り文字はコンマ

                String username = "";
                String systemname = "";
                String sitename = "";
                String hostname = "";


                while (!parser.EndOfData)
                {
                    kds = new kaisenDS();
                    string[] cols = parser.ReadFields(); // 1行読み込み                         //ステータス

                    //1行目#はコメント
                    string str = "";
                    str = cols[0];
                    if (str.StartsWith("#"))
                        continue;



                    kds.career = cols[2] + " " + cols[3];
                    kds.type = cols[4];
                    kds.kaisenid = cols[5];
                    kds.telno1 = cols[6];
                    kds.telno2 = cols[7];
                    kds.telno3 = cols[8];

                    //isp
                    kds.isp = cols[9] + " " + cols[10];
                    kds.servicetype = cols[11];
                    kds.serviceid = cols[12];

                    hostname = cols[15];
                    systemname = cols[17];
                    sitename = cols[20];
                    username = cols[22];
                    kds.status = "1";

                    //カスタマNOの取得
                    kds.userno = getCustomerNo(username, con);
                    if (kds.userno == "")
                        //継続
                        continue;

                    //システムNOの取得
                    kds.systemno = getSystemNo(kds.userno, systemname, con);
                    if (kds.systemno != "")
                    {

                        //拠点NOの取得
                        kds.siteno = getSiteNo(kds.userno, kds.systemno, sitename, con);
                        if (kds.siteno != "")
                        {
                            //ホストNOの取得
                            kds.host_no = getHostNo(kds.userno, kds.systemno, kds.siteno, hostname, con);
                        }
                    }


                    //更新日時
                    kds.chk_date = cols[24];

                    list_kaisen.Add(kds);
                }
                parser.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("CSVファイルの読み込み時にエラーが発生しました。" + ex.Message);
                logger.ErrorFormat("回線情報のインポートエラー。CSVファイルの読み込み時にエラーが発生しました。");

                return;
            }

            if (MessageBox.Show(list_kaisen.Count + "件の回線情報を登録します。よろしいですか？", "回線情報インポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //DB接続
            if (con.FullState != ConnectionState.Open) con.Open();

            NpgsqlCommand cmd;

            using (var transaction = con.BeginTransaction())
            {

                //インサート文
                int i = 0;
                int cnt = 0;
                string status = "";
                foreach (kaisenDS sikds in list_kaisen)
                {
                    if (sikds.status == "有効")
                    {
                        status = "1";
                    }
                    if (sikds.status == "無効")
                    {
                        status = "0";

                    }


                    cmd = new NpgsqlCommand(@"insert into kaisen(status,userno,systemno,siteno,host_no,career,type,kaisenid,telno1,telno2,telno3,isp,servicetype,serviceid,chk_date,chk_name_id) 
                    values (:status,:userno,:systemno,:siteno,:host_no,:career,:type,:kaisenid,:telno1,:telno2,:telno3,:isp,:servicetype,:serviceid,:chk_date,:chk_name_id)", con);
                    try
                    {
                        DateTime datet;
                        DateTime.TryParse(sikds.chk_date, out datet);

                        Int32 rowsaffected;

                        //データ登録
                        int ret_userno;
                        int ret_systemno;
                        int ret_siteno;
                        int ret_host_no;

                        int.TryParse(sikds.userno, out ret_userno);
                        int.TryParse(sikds.systemno, out ret_systemno);
                        int.TryParse(sikds.siteno, out ret_siteno);
                        int.TryParse(sikds.host_no, out ret_host_no);
                        cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                        cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = ret_userno });
                        cmd.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = ret_systemno });
                        cmd.Parameters.Add(new NpgsqlParameter("siteno", DbType.Int32) { Value = ret_siteno });
                        cmd.Parameters.Add(new NpgsqlParameter("host_no", DbType.Int32) { Value = ret_host_no });
                        cmd.Parameters.Add(new NpgsqlParameter("career", DbType.String) { Value = sikds.career });
                        cmd.Parameters.Add(new NpgsqlParameter("type", DbType.String) { Value = sikds.type });
                        cmd.Parameters.Add(new NpgsqlParameter("kaisenid", DbType.String) { Value = sikds.kaisenid });
                        cmd.Parameters.Add(new NpgsqlParameter("telno1", DbType.String) { Value = sikds.telno1 });
                        cmd.Parameters.Add(new NpgsqlParameter("telno2", DbType.String) { Value = sikds.telno2 });
                        cmd.Parameters.Add(new NpgsqlParameter("telno3", DbType.String) { Value = sikds.telno3 });
                        cmd.Parameters.Add(new NpgsqlParameter("isp", DbType.String) { Value = sikds.isp });
                        cmd.Parameters.Add(new NpgsqlParameter("servicetype", DbType.String) { Value = sikds.servicetype });
                        cmd.Parameters.Add(new NpgsqlParameter("serviceid", DbType.String) { Value = sikds.serviceid });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = datet });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });

                        rowsaffected = cmd.ExecuteNonQuery();

                        if (rowsaffected != 1)
                        {

                            //if (MessageBox.Show("メールアドレスを登録できませんでした。担当者名:" + si.user_tantou_name + Environment.NewLine + " 継続しますか？", "担当者情報(電話)のインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            //{
                            //    transaction.Rollback();
                            //    return;
                            //}
                        }
                        else
                        {
                            //登録成功
                            i++;
                            //MessageBox.Show("登録完了", "システム名");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("回線情報のインポート時" + cnt + "件目でエラーが発生しました。" + ex.Message);
                        logger.ErrorFormat("回線情報のインポート時{0}件目でエラーが発生しました。:{1}", i, ex.Message);

                        //transaction.Rollback();
                        return;
                    }

                }
                //終わったらコミットする
                transaction.Commit();
                MessageBox.Show("回線情報のインポート" + i + "件 ");

            }
        }
        //カスタマチェックボックスの有効無効を表示する
        private void m_umu_check_CheckedChanged(object sender, EventArgs e)
        {
            //
        }
        //カスタマのチェックを変えたとき
        private void m_customer_umu_check_CheckedChanged(object sender, EventArgs e)
        {
            userList.Clear();
            disp_User(dsp_L);
        }
        private void m_system_umu_check_CheckedChanged(object sender, EventArgs e)
        {
            systemList.Clear();
            disp_system(dsp_L);
        }
        //拠点のチェックを変えたとき
        private void m_site_umu_check_CheckedChanged(object sender, EventArgs e)
        {
            siteList.Clear();
            this.disp_site(dsp_L);
        }
        //ホストのチェックを変えたとき
        private void m_host_umu_check_CheckedChanged(object sender, EventArgs e)
        {
            m_host_list.Clear();
            this.disp_host(dsp_L);
        }

        private void m_interface_umu_check_CheckedChanged(object sender, EventArgs e)
        {
            interfaceList.Clear();
            this.disp_interface(dsp_L);
        }

        private void m_kaisen_umu_check_CheckedChanged(object sender, EventArgs e)
        {
            kaisenList.Clear();
            DISP_dataSet dset = new DISP_dataSet();
            Dictionary<string, string> param_dict = new Dictionary<string, string>();
            Class_Detaget dg = new Class_Detaget();

            dset = dg.getSelectKaisenList(param_dict, con, dset);

            disp_kaisen(dset);
        }
        private void m_incident_umu_check_CheckedChanged_1(object sender, EventArgs e)
        {
            m_incident_List.Clear();
            this.disp_incident();
        }

        private void m_teiki_umu_check_CheckedChanged(object sender, EventArgs e)
        {
            m_teiki_List.Clear();
            this.disp_teiki_list();
        }


        private void m_keikaku_umu_check_CheckedChanged(object sender, EventArgs e)
        {
            m_keikaku_list.Clear();
            this.disp_scheduleList_keikaku();
        }

        private void m_tokubetu_umu_check_CheckedChanged(object sender, EventArgs e)
        {
            m_tokubetu_list.Clear();
            this.disp_tokubetu_list();
        }
        //テンプレート登録
        private void m_inc_templete_insert_btn_Click(object sender, EventArgs e)
        {
            Form_inc_templete_insert templeteInsert = new Form_inc_templete_insert();
            templeteInsert.con = con;
            if (userList != null)
                templeteInsert.userList = userDSList;
            templeteInsert.loginDS = loginDS;
            templeteInsert.ShowDialog(this);
        }
        //テンプレート編集
        private void m_inc_templete_update_btn_Click(object sender, EventArgs e)
        {
            Form_inc_templete_update templeteUpdate = new Form_inc_templete_update();
            templeteUpdate.con = con;
            if (userList != null)
                templeteUpdate.userList = userDSList;
            templeteUpdate.loginDS = loginDS;
            templeteUpdate.ShowDialog(this);
        }

        //タスク登録ボタン
        private void m_taskInsert_lnk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }


        private void テンプレートインポートToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //1:インシデントテンプレート　2:作業テンプレート
        private void ImportTemplete(string incidentflg)
        {
            //ファイル選択ダイアログ
            string filePath = Disp_FileSelectDlg();
            if (filePath == "")
                return;

            List<templeteDS> templetedt_list = new List<templeteDS>();
            String context = "";
            try
            {

                TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(","); // 区切り文字はコンマ

                String username = "";



                while (!parser.EndOfData)
                {
                    templetedt = new templeteDS();
                    string[] cols = parser.ReadFields(); // 1行読み込み                         //ステータス

                    //1行目#はコメント
                    string str = "";
                    str = cols[0];
                    if (str.StartsWith("#"))
                        continue;

                    //インシデントテンプレート
                    if (incidentflg == "1") {
                        context = "インシデントメール";


                        templetedt.title = cols[2];
                    }
                    else
                    {
                        context = "計画作業メール";
                    }
                    templetedt.templetename = cols[2];
                    username = cols[2];
                    string tat = cols[4];
                    templetedt.text  = tat.Replace("\n", "\r\n");
                    //更新日時
                    templetedt.chk_date = cols[5];
                    //更新ID
                    templetedt.chk_name_id = cols[6];

                    

                    //カスタマNOの取得
                    //ALLだったら0:全てのカスタマ
                    if (username == "ALL")
                    {
                        templetedt.userno = "0";
                        username = "全てのカスタマ";
                    }
                    else
                    {
                        templetedt.userno = getCustomerNo(username, con);
                    }

                    //カスタマ通番が取得できなかったら登録しない
                    if (templetedt.userno == "")
                        //継続
                        continue;


                    templetedt_list.Add(templetedt);
                }
                parser.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("CSVファイルの読み込み時にエラーが発生しました。" + ex.Message);
                logger.ErrorFormat("テンプレートのインポートエラー。CSVファイルの読み込み時にエラーが発生しました。");

                return;
            }

            if (MessageBox.Show(templetedt_list.Count + "件のテンプレートを登録します。よろしいですか？", context + "テンプレートインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //DB接続
            if (con.FullState != ConnectionState.Open) con.Open();

            NpgsqlCommand cmd;

            using (var transaction = con.BeginTransaction())
            {

                //インサート文
                int i = 0;
                int cnt = 0;
                foreach (templeteDS tempDS in templetedt_list)
                {

                    cmd = new NpgsqlCommand(@"insert into templete(templetetype,templetename,title,text,userno,chk_date,chk_name_id) 
                    values (:templetetype,:templetename,:title,:text,:userno,:chk_date,:chk_name_id)", con);
                    try
                    {
                        DateTime datet;
                        DateTime.TryParse(tempDS.chk_date, out datet);

                        Int32 rowsaffected;

                        //データ登録
                        int ret_userno;
                        int.TryParse(tempDS.userno, out ret_userno);

                        cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = ret_userno });
                        cmd.Parameters.Add(new NpgsqlParameter("templetetype", DbType.String) { Value = incidentflg });
                        cmd.Parameters.Add(new NpgsqlParameter("templetename", DbType.String) { Value = tempDS.templetename });
                        cmd.Parameters.Add(new NpgsqlParameter("title", DbType.String) { Value = tempDS.title });
                        cmd.Parameters.Add(new NpgsqlParameter("text", DbType.String) { Value = tempDS.text });
                        cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.String) { Value = tempDS.userno });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = datet });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });

                        rowsaffected = cmd.ExecuteNonQuery();

                        if (rowsaffected != 1)
                        {

                            //if (MessageBox.Show("メールアドレスを登録できませんでした。担当者名:" + si.user_tantou_name + Environment.NewLine + " 継続しますか？", "担当者情報(電話)のインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            //{
                            //    transaction.Rollback();
                            //    return;
                            //}
                        }
                        else
                        {
                            //登録成功
                            i++;
                            //MessageBox.Show("登録完了", "システム名");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(context + "テンプレートインポート時" + cnt + "件目でエラーが発生しました。" + ex.Message);
                        logger.ErrorFormat(context + "テンプレートインポート時{0}件目でエラーが発生しました。:{1}", i, ex.Message);

                        //transaction.Rollback();
                        return;
                    }

                }
                //終わったらコミットする
                transaction.Commit();
                MessageBox.Show("テンプレートのインポート" + i + "件 ");

            }



        }
        private void インシデントテンプレートToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //インポートを行う
            ImportTemplete("1");

        }

        private void 作業テンプレートToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //インポートを行う
            ImportTemplete("2");
        }
        //インシデントメールボタン
        private void button2_Click_1(object sender, EventArgs e)
        {
            Form_IncidentMailSend formdetail = new Form_IncidentMailSend();
            formdetail.con = con;

            if (userDSList != null)
                formdetail.userDSList = userDSList;
            if (systemDSList != null)
                formdetail.systemDSList = systemDSList;

            if (siteDSList != null)
                formdetail.siteDSList = siteDSList;

            formdetail.con = con;
            formdetail.loginDS = loginDS;

            formdetail.Show();
            //formdetail.Owner = this;
        }
        //インシデントタスク登録
        private void linkLabel7_LinkClicked_2(object sender, LinkLabelLinkClickedEventArgs e)
        {


            Form_taskInsert taskinsert = new Form_taskInsert();
            taskinsert.schedule_type = 1-1;
            taskinsert.con = con;
            if (userList != null)
                taskinsert.userList = userDSList;
            taskinsert.loginDS = loginDS;
            taskinsert.Show();
        }

        //定期作業登録
        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            Form_taskInsert taskinsert = new Form_taskInsert();
            taskinsert.schedule_type = 2 - 1;

            taskinsert.con = con;
            if (userList != null)
                taskinsert.userList = userDSList;
            taskinsert.systemList = systemDSList;
            taskinsert.loginDS = loginDS;
            taskinsert.Show();
        }
        //計画作業登録
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            Form_taskInsert taskinsert = new Form_taskInsert();
            taskinsert.schedule_type = 3 - 1;

            taskinsert.con = con;
            if (userList != null)
                taskinsert.userList = userDSList;
            if (systemDSList != null)
                taskinsert.systemList = systemDSList;
            if (siteDSList != null)
                taskinsert.siteList = siteDSList;

            taskinsert.loginDS = loginDS;
            taskinsert.Show();
        }

        //特別対応登録ボタン
        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form_taskInsert timerfm = new Form_taskInsert();
            if (userDSList != null)
                timerfm.userList = userDSList;
            if (systemDSList != null)
                timerfm.systemList = systemDSList;
            if (siteDSList != null)
                timerfm.siteList = siteDSList;
            timerfm.schedule_type = 4 - 1;
            timerfm.con = con;
            timerfm.loginDS = loginDS;
            timerfm.ShowDialog(this);
        }

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            //F5キーが押されたか調べる
            if (e.KeyData == Keys.F5)
                m_refresh_btn.PerformClick();
        }
    }


}
