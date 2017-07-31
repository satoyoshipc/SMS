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
    public partial class Form_incidentDetail : Form
    {

        //ログイン情報
        public opeDS loginDS { get; set; }

        public incidentDS incidentdt { get; set; }

        //ユーザ情報一覧
        public List<userDS> userList { get; set; }
        //システム情報一覧
        public List<systemDS> systemList { get; set; }
        
        //拠点
        public List<siteDS> siteList { get; set; }

        //ホスト情報一覧
        public List<hostDS> hostList { get; set; }

        //DBコネクション
        public NpgsqlConnection con { get; set; }

        //ListViewのソートの際に使用する
        private int sort_kind = 0;

        //インシデントの一覧
        DataTable incident_list;


        public Form_incidentDetail()
        {
            InitializeComponent();
        }

        //表示前処理
        private void Form_InterfaceDetail_Load(object sender, EventArgs e)
        {



            m_selectKoumoku.Items.Add("通番");
            m_selectKoumoku.Items.Add("ステータス");
            m_selectKoumoku.Items.Add("MPMSインシデント番号");
            m_selectKoumoku.Items.Add("S-cude事例ID");
            m_selectKoumoku.Items.Add("インシデント区分");
            m_selectKoumoku.Items.Add("インシデント内容(タイトル)");
            m_selectKoumoku.Items.Add("MAT対応");
            m_selectKoumoku.Items.Add("MAT対応コマンド");
            m_selectKoumoku.Items.Add("受付日時");
            m_selectKoumoku.Items.Add("手配日時");
            m_selectKoumoku.Items.Add("復旧日時");
            m_selectKoumoku.Items.Add("完了日時");
            m_selectKoumoku.Items.Add("タイマー");
            m_selectKoumoku.Items.Add("要確認メッセージ");
            m_selectKoumoku.Items.Add("カスタマ通番");
            m_selectKoumoku.Items.Add("システム通番");
            m_selectKoumoku.Items.Add("拠点通番");
            m_selectKoumoku.Items.Add("ホスト番号");
            m_selectKoumoku.Items.Add("更新日時");
            m_selectKoumoku.Items.Add("更新者");




            if (incidentdt != null) 
                getIncident(incidentdt);
            

        }

        //インシデント情報を表示する
        private void getIncident(incidentDS incidentdt)
        {

            this.m_incidentno.Text = incidentdt.incident_no;
            this.m_userno.Text = incidentdt.userno;
            this.m_systemno.Text = incidentdt.systemno;
            this.m_siteno.Text = incidentdt.siteno;
            this.m_hostno.Text = incidentdt.hostno;
            this.m_mpmsno.Text = incidentdt.mpms_incident;
            this.m_scubeno.Text = incidentdt.s_cube_id;
            this.m_incidentnaiyou.Text = incidentdt.content;

            this.m_statusCombo.Text = incidentdt.status;

            //アラーム検知 障害申告 問い合わせ
            int outputdata;
            if (int.TryParse(incidentdt.incident_type, out outputdata))
                this.m_incidentKBN.SelectedIndex = outputdata - 1;


            if (incidentdt.uketukedate != null || incidentdt.uketukedate != "")
            {
                m_uketukedate.Checked = true;
                this.m_uketukedate.Text = incidentdt.uketukedate;
            }
            else
                m_uketukedate.Checked = false;

            if (incidentdt.tehaidate != null || incidentdt.tehaidate != "")
            {
                m_tehaidate.Checked = true;
                this.m_tehaidate.Text = incidentdt.tehaidate;
            }
            else
                m_tehaidate.Checked = false;

            if (incidentdt.fukyudate != null || incidentdt.fukyudate != "")
            {
                m_fukkyudate.Checked = true;
                this.m_fukkyudate.Text = incidentdt.fukyudate;
            }
            else
                m_fukkyudate.Checked = false;

            if (incidentdt.enddate != null || incidentdt.enddate != "")
            {
                m_enddate.Checked = true;
                this.m_enddate.Text = incidentdt.enddate;
            }
            else
                m_enddate.Checked = false;


            if (incidentdt.matflg == "1")

                m_MATchkbox.Checked = true;
            else
                m_MATchkbox.Checked = false;

            this.m_MATCommannd.Text = incidentdt.matcommand;

            this.m_timer.Text = incidentdt.timer;


            if (incidentdt.timer != null || incidentdt.timer != "")
            {
                m_timer.Checked = true;
                this.m_timer.Text = incidentdt.timer;
            }
            else
                m_enddate.Checked = false;
            m_youkakunin.Text = incidentdt.kakuninmsg;


            this.m_updateOpe.Text = incidentdt.chk_name_id;
            this.m_update.Text = incidentdt.chk_date;



            //コンボボックスを読み込む


            Read_CustomerCombo();
            m_usernameCombo.SelectedValue = incidentdt.userno;
            if(m_usernameCombo.SelectedValue != null)
            { 
                Read_systemCombo();
                m_systemCombo.SelectedValue = incidentdt.systemno;
            }
            if (m_systemCombo.SelectedValue != null)
            {
                Read_siteCombo();
                m_siteCombo.SelectedValue = incidentdt.siteno;
            }
            if (m_siteCombo.SelectedValue != null && m_siteCombo.SelectedValue.ToString() != "")
            {
                Read_hostCombo();
                m_hostCombo.SelectedValue = incidentdt.hostno;
                if(m_hostCombo.Text != "")
                    m_hostno.Text = m_hostCombo.SelectedValue.ToString();
            }
        }
        //
        void Read_CustomerCombo()
        {
            m_userno.Text = "";
            m_usernameCombo.DataSource = null;
            m_systemno.Text = "";
            m_systemCombo.DataSource = null;
            m_siteno.Text = "";
            m_siteCombo.DataSource = null;
            m_hostno.Text = "";
            m_hostCombo.DataSource = null;

            //コンボボックス
            DataTable cutomerTable = new DataTable();
            cutomerTable.Columns.Add("ID", typeof(string));
            cutomerTable.Columns.Add("NAME", typeof(string));

            if (userList == null)
                return;

            //空行を挿入
            userDS tmp = new userDS();
            tmp.username = "";
            tmp.userno = "";
            cutomerTable.Rows.Add(tmp);

            //カスタマ情報を取得する
            foreach (userDS v in userList)
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

        }
        void Read_systemCombo()
        {

            m_siteno.Text = "";
            m_siteCombo.DataSource = null;
            m_hostno.Text = "";
            m_hostCombo.DataSource = null;

            m_systemno.Text = "";
            m_systemCombo.DataSource = null;

            //ラベルに反映
            if (m_usernameCombo.SelectedValue != null)
                m_userno.Text = m_usernameCombo.SelectedValue.ToString();

            //システムコンボの値を取得
            DataTable systemTable = new DataTable();
            systemTable.Columns.Add("ID", typeof(string));
            systemTable.Columns.Add("NAME", typeof(string));

            //システム情報を取得する
            if (systemList.Count <= 0)
                return;

            //空行を挿入
            DataRow row = systemTable.NewRow();
            row["ID"] = "";
            row["NAME"] = "";
            systemTable.Rows.Add(row);

            foreach (systemDS v in systemList)
            {
                //カスタマNOで区別する
                if (m_usernameCombo.SelectedValue != null)
                {

                    if (v.userno == m_usernameCombo.SelectedValue.ToString())
                    {
                        row = systemTable.NewRow();
                        row["ID"] = v.systemno;
                        row["NAME"] = v.systemname;
                        systemTable.Rows.Add(row);
                    }
                }
            }
            //データテーブルを割り当てる
            m_systemCombo.DataSource = systemTable;
            m_systemCombo.DisplayMember = "NAME";
            m_systemCombo.ValueMember = "ID";
            if (systemTable.Rows.Count > 0)
                m_systemno.Text = m_systemCombo.SelectedValue.ToString();
        }
        void Read_siteCombo()
        {
            m_siteCombo.DataSource = null;
            m_siteno.Text = "";
            m_hostno.Text = "";
            m_hostCombo.DataSource = null;


            //ラベルに反映
            if (m_systemCombo.SelectedValue != null)
                m_systemno.Text = m_systemCombo.SelectedValue.ToString();

            //コンボボックス
            DataTable siteTable = new DataTable();
            siteTable.Columns.Add("ID", typeof(string));
            siteTable.Columns.Add("NAME", typeof(string));

            string systemid = "";
            if (m_systemno.Text != "")
                systemid = m_systemno.Text;

            //拠点情報の取得
            Class_Detaget DGclass = new Class_Detaget();
            siteList = DGclass.getSiteList(systemid, con, true);

            //取れなかったらなにもしない
            if (siteList == null || siteList.Count <= 0)
                return;

            //空行の挿入
            DataRow row = siteTable.NewRow();
            row["ID"] = "";
            row["NAME"] = "";
            siteTable.Rows.Add(row);

            //拠点件数分ループを行う
            foreach (siteDS v in siteList)
            {
                if (m_systemCombo.SelectedValue != null)
                {
                    if (v.systemno == m_systemCombo.SelectedValue.ToString())
                    {
                        row = siteTable.NewRow();
                        row["ID"] = v.siteno;
                        row["NAME"] = v.sitename;
                        siteTable.Rows.Add(row);
                    }
                }
            }
            //データテーブルを割り当てる
            m_siteCombo.DataSource = siteTable;
            m_siteCombo.DisplayMember = "NAME";
            m_siteCombo.ValueMember = "ID";
            if (siteTable.Rows.Count > 0)
                if(m_siteCombo.Text != "")
                    m_siteno.Text = m_siteCombo.SelectedValue.ToString();
        }
        void Read_hostCombo()
        {
            try
            {
                //ラベルに反映
                if (m_siteCombo.SelectedValue != null)
                    m_siteno.Text = m_siteCombo.SelectedValue.ToString();

                m_hostCombo.DataSource = null;
                m_hostno.Text = "";

                Class_Detaget getuser = new Class_Detaget();

                //ホスト名を検索
                List<hostDS> hostDSList = getuser.getHostList(m_siteno.Text, con, true);

                //空白行を追加
                hostDS tmp = new hostDS();
                tmp.hostname = "";
                tmp.host_no = "";
                List<hostDS> tmphostDSList = new List<hostDS>();
                tmphostDSList.Add(tmp);

                //取得した行を空行についか
                if (hostDSList != null)
                    tmphostDSList.AddRange(hostDSList);

                m_hostCombo.DataSource = tmphostDSList;
                m_hostCombo.DisplayMember = "hostname";
                m_hostCombo.ValueMember = "host_no";
                //ホスト名ラベルを表示
                if (hostDSList.Count > 0)
                    m_hostno.Text = m_hostCombo.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ホストコンボボックスの一覧を取得することができませんでした。 " + ex.Message, "ホスト情報取得");
            }
        }

        //検索ボタン
        private void m_selectBtn_Click(object sender, EventArgs e)
        {
            m_incidentList.Clear();
            List<incidentDS> incidentdsList = new List<incidentDS>();
            Dictionary<string, string> param_dict = new Dictionary<string, string>();
            Class_Detaget dg = new Class_Detaget();

            if (m_selecttext.Text != "")
            {

                if (this.m_selectKoumoku.SelectedIndex.ToString() != "")
                {
                    switch (this.m_selectKoumoku.SelectedIndex)
                    {

                        case 0:
                            param_dict["incident_no"] = m_selecttext.Text;
                            break;

                        case 1:
                            if (m_selecttext.Text == "未完了")
                                param_dict["status"] = "1";
                            else if (m_selecttext.Text == "完了")
                                param_dict["status"] = "0";
                            break;

                        case 2:
                            param_dict["mpms_incident"] = m_selecttext.Text;
                            break;

                        case 3:
                            param_dict["s_cube_id"] = m_selecttext.Text;
                            break;

                        case 4:


                            if (m_selecttext.Text == "アラーム検知")
                                param_dict["incident_type"] = "1";
                            else if (m_selecttext.Text == "障害申告")
                                param_dict["incident_type"] = "2";

                            else if (m_selecttext.Text == "問い合わせ")
                                param_dict["incident_type"] = "3";
                            
                            break;

                        case 5:
                            param_dict["content"] = m_selecttext.Text;
                            break;

                        case 6:

                            if (m_selecttext.Text == "有")
                                param_dict["matflg"] =  "1";
                            else if (m_selecttext.Text == "無")
                                param_dict["matflg"] = "0";

                            break;

                        case 7:
                            param_dict["matcommand"] = m_selecttext.Text;
                            break;
                        case 8:
                            param_dict["uketukedate"] = m_selecttext.Text;
                            break;
                        case 9:
                            param_dict["tehaidate"] = m_selecttext.Text;

                            break;
                        case 10:
                            param_dict["fukyudate"] = m_selecttext.Text;
                            break;
                        case 11:
                            param_dict["enddate"] = m_selecttext.Text;
                            break;


                        case 12:
                            param_dict["timer"] = m_selecttext.Text;
                            break;
                        case 13:
                            param_dict["kakunin"] = m_selecttext.Text;
                            break;
                        case 14:
                            param_dict["userno"] = m_selecttext.Text;
                            break;

                        case 15:
                            param_dict["systemno"] = m_selecttext.Text;
                            break;
                        case 16:
                            param_dict["siteno"] = m_selecttext.Text;
                            break;
                        case 17:
                            param_dict["hostno"] = m_selecttext.Text;
                            break;

                        //更新日時
                        case 18:
                            param_dict["chk_date"] = m_selecttext.Text;
                            break;
                        //更新者
                        case 19:
                            param_dict["chk_name_id"] = m_selecttext.Text;
                            break;
                        default:
                            break;
                    }
                }
            }
            //まず件数を取得する
            Int64 count = dg.getIncidentListCount((Form_MainList)this.Owner, param_dict, con);
            if (MessageBox.Show(count.ToString() + "件ヒットしました。表示しますか？", "ホスト", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            //インシデント一覧を取得する
            incidentdsList = dg.getIncidentList((Form_MainList)this.Owner, param_dict, con);


            this.m_incidentList.VirtualMode = true;
            // １行全体選択
            this.m_incidentList.FullRowSelect = true;
            this.m_incidentList.HideSelection = false;
            this.m_incidentList.HeaderStyle = ColumnHeaderStyle.Clickable;
            //Hook up handlers for VirtualMode events.
            this.m_incidentList.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(incident_RetrieveVirtualItem);
            this.m_incidentList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.m_incidentList.Scrollable = true;


            this.m_incidentList.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(1, "ステータス", 50, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(2, "MPMSインシデント番号", 90, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(3, "S-cude事例ID", 90, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(4, "インシデント区分", 80, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(5, "インシデント内容(タイトル)", 300, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(6, "MAT対応", 50, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(7, "MAT対応コマンド", 120, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(8, "受付日時", 120, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(9, "手配日時", 120, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(10, "復旧日時", 120, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(11, "完了日時", 120, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(12, "タイマー", 120, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(13, "要確認メッセージ", 50, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(14, "カスタマ番号", 50, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(15, "システム通番番号", 50, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(16, "拠点通番", 50, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(17, "ホスト通番", 50, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(18, "更新日時", 110, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(19, "更新者", 50, HorizontalAlignment.Left);

            //リストビューを初期化する
            incident_list = new DataTable("table1");
            incident_list.Columns.Add("No", Type.GetType("System.Int32"));
            incident_list.Columns.Add("ステータス", Type.GetType("System.String"));
            incident_list.Columns.Add("MPMSインシデント番号", Type.GetType("System.String"));
            incident_list.Columns.Add("S-cude事例ID", Type.GetType("System.String"));
            incident_list.Columns.Add("インシデント区分", Type.GetType("System.String"));
            incident_list.Columns.Add("インシデント内容(タイトル)", Type.GetType("System.String"));
            incident_list.Columns.Add("MAT対応", Type.GetType("System.String"));
            incident_list.Columns.Add("MAT対応コマンド", Type.GetType("System.String"));
            incident_list.Columns.Add("受付日時", Type.GetType("System.String"));
            incident_list.Columns.Add("手配日時", Type.GetType("System.String"));
            incident_list.Columns.Add("復旧日時", Type.GetType("System.String"));
            incident_list.Columns.Add("完了日時", Type.GetType("System.String"));
            incident_list.Columns.Add("タイマー", Type.GetType("System.String"));
            incident_list.Columns.Add("要確認メッセージ", Type.GetType("System.String"));
            incident_list.Columns.Add("カスタマ番号", Type.GetType("System.String"));
            incident_list.Columns.Add("システム通番番号", Type.GetType("System.String"));
            incident_list.Columns.Add("拠点通番", Type.GetType("System.String"));
            incident_list.Columns.Add("ホスト通番", Type.GetType("System.String"));
            incident_list.Columns.Add("更新日時", Type.GetType("System.String"));
            incident_list.Columns.Add("更新者", Type.GetType("System.String"));

            //リストに表示
            if (incidentdsList != null)
            {
                m_incidentList.BeginUpdate();
                foreach (incidentDS s_ds in incidentdsList)
                {


                    DataRow urow = incident_list.NewRow();
                    urow["No"] = s_ds.incident_no;

                    urow["ステータス"] = s_ds.status;
                    urow["MPMSインシデント番号"] = s_ds.mpms_incident;
                    urow["S-cude事例ID"] = s_ds.s_cube_id;

                    //1:アラーム検知 2:障害申告 3:問い合わせ
                    string typestr = "";
                    if (s_ds.incident_type == "1")
                        typestr = "アラーム検知";
                    else if (s_ds.incident_type == "2")
                        typestr = "障害申告";
                    else if (s_ds.incident_type == "3")
                        typestr = "問い合わせ";


                    urow["インシデント区分"] = typestr;
                    urow["インシデント内容(タイトル)"] = s_ds.content;


                    //MAT対応
                    string matflg_str = "";
                    if (s_ds.matflg == "0")
                        matflg_str = "無";
                    else if (s_ds.matflg == "1")
                        matflg_str = "有";

                    urow["MAT対応"] = matflg_str;

                    urow["MAT対応コマンド"] = s_ds.matcommand;
                    urow["受付日時"] = s_ds.uketukedate;
                    urow["手配日時"] = s_ds.tehaidate ;
                    urow["復旧日時"] = s_ds.fukyudate;
                    urow["完了日時"] = s_ds.enddate;
                    urow["タイマー"] = s_ds.timer;
                    urow["要確認メッセージ"] = s_ds.kakuninmsg;
                    urow["カスタマ番号"] = s_ds.userno;
                    urow["システム通番番号"] = s_ds.systemno;
                    urow["拠点通番"] = s_ds.siteno;
                    urow["ホスト通番"] = s_ds.hostno;
                    urow["更新日時"] = s_ds.chk_date;
                    urow["更新者"] = s_ds.chk_name_id;

                    incident_list.Rows.Add(urow);
                }
                this.m_incidentList.VirtualListSize = incident_list.Rows.Count;
                this.m_incidentList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                m_incidentList.EndUpdate();
            }

        }
        void incident_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            //	e.Item = _item[e.ItemIndex];
            if (incident_list.Rows.Count > 0)
            {

                DataRow row = this.incident_list.Rows[e.ItemIndex];
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
        //キャンセルボタン
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //更新ボタン
        private void button1_Click(object sender, EventArgs e)
        {
            if (m_usernameCombo.Text == "")
            {
                MessageBox.Show("カスタマ名を選択して下さい。", "インシデント情報更新", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }
            if (m_systemCombo.Text == "")
            {
                MessageBox.Show("システム名を選択して下さい。", "インシデント情報更新", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }
            //20170728 コメントアウト
            //if (m_siteCombo.Text == "")
            //{
            //    MessageBox.Show("拠点名を選択して下さい。", "インシデント情報更新", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}

            if (m_incidentKBN.Text == "")
            {
                MessageBox.Show("インシデント区分を選択して下さい。", "インシデント情報更新", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //確認ダイアログ
            if (MessageBox.Show("インシデントデータの更新を行います。よろしいですか？", "インシデント情報更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string status = "";
            if (m_statusCombo.Text == "未完了")
                status = "1";
            else if (m_statusCombo.Text == "完了")
                status = "0";

            //インシデントタイプ
            string incident_type = "";
            if (m_incidentKBN.Text == "アラーム検知")
                incident_type = "1";
            else if (m_incidentKBN.Text == "障害申告")
                incident_type = "2";

            else if (m_incidentKBN.Text == "問い合わせ")
                incident_type = "3";

            string matflg = "";
            if (m_MATchkbox.Checked)
                matflg = "1";
            else
                matflg = "0";

            DateTime? uke_Date = null;
            if (m_uketukedate.Checked)
                uke_Date = m_uketukedate.Value;

            DateTime? teha_Date = null;
            if (m_tehaidate.Checked)
                teha_Date = m_tehaidate.Value;

            DateTime? fukyu_Date = null;
            if (m_fukkyudate.Checked)
                fukyu_Date = m_fukkyudate.Value;

            DateTime? end_Date = null;
            if (m_enddate.Checked)
                end_Date = m_enddate.Value;

            DateTime? timer_Date = null;
            if (m_timer.Checked)
                timer_Date = m_timer.Value;


            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "update incident set " +
               "status=:status," +
                "mpms_incident=:mpms_incident," +
                "s_cube_id=:s_cube_id," +
                "incident_type=:incident_type," +
                "content=:content," +
                "matflg=:matflg," +
                "matcommand=:matcommand," +
                "uketukedate=:uketukedate," +
                "tehaidate=:tehaidate," +
                "fukyudate=:fukyudate," +
                "enddate=:enddate," +
                "timer=:timer," +
                "kakunin=:kakunin," +
                "userno=:userno," +
                "systemno=:systemno," +
                "siteno=:siteno," +
                "hostno=:hostno," +
                "chk_name_id =:ope,chk_date=:chdate " +
                "where incident_no = :no";

            using (var transaction = con.BeginTransaction())
            {
                int userno;
                int systemno;
                int siteno;
                int hostno;
                int? userno2;
                int? systemno2;
                int? siteno2;
                int? hostno2;

                if (int.TryParse(m_userno.Text, out userno) )
                    userno2 = userno;
                else
                    userno2 = null;
                if (int.TryParse(m_systemno.Text, out systemno))
                    systemno2 = systemno;
                else
                    systemno2 = null;
                if (int.TryParse(m_siteno.Text, out siteno))
                    siteno2 = siteno;
                else
                    siteno2 = null;
                if (int.TryParse(m_hostno.Text, out hostno))
                    hostno2 = hostno;
                else
                    hostno2 = null;

                var command = new NpgsqlCommand(@sql, con);
                command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = m_incidentno.Text });
                command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                command.Parameters.Add(new NpgsqlParameter("mpms_incident", DbType.Int32) { Value = m_mpmsno.Text });
                command.Parameters.Add(new NpgsqlParameter("s_cube_id", DbType.String) { Value = m_scubeno.Text });
                command.Parameters.Add(new NpgsqlParameter("incident_type", DbType.String) { Value = incident_type });
                command.Parameters.Add(new NpgsqlParameter("content", DbType.String) { Value = m_incidentnaiyou.Text });
                command.Parameters.Add(new NpgsqlParameter("matflg", DbType.String) { Value = matflg });
                command.Parameters.Add(new NpgsqlParameter("matcommand", DbType.String) { Value = m_MATCommannd.Text });
                command.Parameters.Add(new NpgsqlParameter("uketukedate", DbType.DateTime) { Value = uke_Date });
                command.Parameters.Add(new NpgsqlParameter("tehaidate", DbType.DateTime) { Value = teha_Date });
                command.Parameters.Add(new NpgsqlParameter("fukyudate", DbType.DateTime) { Value = fukyu_Date });
                command.Parameters.Add(new NpgsqlParameter("enddate", DbType.DateTime) { Value = end_Date });
                command.Parameters.Add(new NpgsqlParameter("timer", DbType.DateTime) { Value = timer_Date });
                command.Parameters.Add(new NpgsqlParameter("kakunin", DbType.String) { Value = m_youkakunin.Text });
                command.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = userno2 });
                command.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = systemno2 });
                command.Parameters.Add(new NpgsqlParameter("siteno", DbType.Int32) { Value = siteno2 });
                command.Parameters.Add(new NpgsqlParameter("hostno", DbType.Int32) { Value = hostno2 });

                command.Parameters.Add(new NpgsqlParameter("ope", DbType.String) { Value = loginDS.opeid });
                command.Parameters.Add(new NpgsqlParameter("chdate", DbType.DateTime) { Value = DateTime.Now });
                Int32 rowsaffected;
                try
                {
                    //更新処理
                    rowsaffected = command.ExecuteNonQuery();
                    transaction.Commit();

                    if (rowsaffected != 1)
                        MessageBox.Show("更新できませんでした。", "インシデント情報更新");
                    else
                    {
                        //タイマーが設定されていた場合はインシデントタイマーを設定する
                        if (status == "1" && m_timer.Checked)
                        {
                            //スケジュール登録
                            if (updateSchedule(m_incidentno.Text, con) == 0)
                            {
                                MessageBox.Show("スケジュール情報の登録に失敗しました", "インシデント情報更新", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                transaction.Rollback();
                                return;
                            }
                            //タイマーアラーム登録


                            //一度未来のタイマーを削除
                            //DeleteTimer();

                            //新規にタイマーアラームを登録する
                            //InsertTimer();
                        }



                        MessageBox.Show("更新されました。", "インシデント情報更新");

                    }
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

        //スケジュール情報のアップデート
        private int updateSchedule(String schedule_no, NpgsqlConnection con)
        {
            int ret = 0;
            try
            {
                if (con.FullState != ConnectionState.Open) con.Open();

                //スケジュールテーブルの登録内容は削除する
                string sql = "DELETE FROM schedule WHERE incident_no=:no1;";
                //+
                 var deletecommand = new NpgsqlCommand(@sql, con);
                deletecommand.Parameters.Add(new NpgsqlParameter("no1", DbType.Int32) { Value = m_incidentno.Text });

                Int32 rowinsert;
                rowinsert = deletecommand.ExecuteNonQuery();
                if (rowinsert != 0)
                    MessageBox.Show("スケジュールテーブルの書き換えに失敗しました。", "スケジュール削除");

                InsertIncident(m_incidentno.Text, con);
                ret = 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show("タイマー登録する際にエラーが発生しました。 " + ex.Message, "インシデント登録");
                ret = -1;
            }


            return ret;
        }
        //スケジュールテーブルにタイマーをセットする
        private int InsertIncident(String schedule_no, NpgsqlConnection con)
        {
            int ret = 0;

            NpgsqlCommand cmd;
            try
            {
                DateTime timer_Date = new DateTime();
                if (m_timer.Checked)
                    timer_Date = m_timer.Value;


                //1時間出し続ける
                DateTime endTime = timer_Date.AddMinutes(60);

                cmd = new NpgsqlCommand(@"insert into schedule(userno,systemno,siteno,timer_name,schedule_type,repeat_type,start_date,end_date,alerm_message,status,sound,incident_no,chk_name_id) " +
                            "values ( :userno,:systemno,:siteno,:timer_name,:schedule_type,:repeat_type,:start_date,:end_date,:alerm_message,:status,:sound,:incident_no,:chk_name_id); " +
                            "select currval('schedule_schedule_no_seq') ;", con);

                cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = m_userno.Text });
                cmd.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = m_systemno.Text=="" ? null : m_systemno.Text });
                cmd.Parameters.Add(new NpgsqlParameter("siteno", DbType.Int32) { Value = m_siteno.Text == "" ? null : m_siteno.Text });
                cmd.Parameters.Add(new NpgsqlParameter("timer_name", DbType.String) { Value =  "インシデント管理タイマー" });
                cmd.Parameters.Add(new NpgsqlParameter("schedule_type", DbType.String) { Value = "1" });
                cmd.Parameters.Add(new NpgsqlParameter("repeat_type", DbType.String) { Value = "1" });
                cmd.Parameters.Add(new NpgsqlParameter("start_date", DbType.DateTime) { Value = DateTime.Now });
                cmd.Parameters.Add(new NpgsqlParameter("end_date", DbType.DateTime) { Value = endTime });
                cmd.Parameters.Add(new NpgsqlParameter("alerm_message", DbType.String) { Value = m_youkakunin.Text });
                //0:無効 1:有効
                cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = "1" });
                cmd.Parameters.Add(new NpgsqlParameter("sound", DbType.Binary) { Value = null });
                cmd.Parameters.Add(new NpgsqlParameter("incident_no", DbType.Int32) { Value = int.Parse(m_incidentno.Text) });
                cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });

                //OUTパラメータをセットする
                NpgsqlParameter firstColumn = new NpgsqlParameter("firstcolumn", DbType.Int32);
                firstColumn.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(firstColumn);
                Int32 rowsaffected;
                rowsaffected = cmd.ExecuteNonQuery();

                int currval = int.Parse(firstColumn.NpgsqlValue.ToString());

                if (rowsaffected != 1)
                {
                    MessageBox.Show("登録できませんでした。", "タイマー登録");
                }
                else
                {
                    if (currval > 0)
                    {

                        //引き続きアラートデータを作成し登録する
                        alerm_insert(currval, timer_Date);
                        //登録成功
                        MessageBox.Show("登録完了 " + "スケジュール番号" + currval, "タイマー登録");
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("スケジュールに登録する際にエラーが発生しました。 " + ex.Message, "インシデント登録");
                ret = -1;
                return ret;
            }
            return 1;
        }

        //タイマー対応テーブルに登録する
        int alerm_insert(int scheNO, DateTime alertdatetime)
        {
            //DB接続
            NpgsqlCommand cmd;
            try
            {

                Int32 rowsaffected;
                //データ登録
                cmd = new NpgsqlCommand(@"insert into timer_taiou(schedule_no,schedule_type,alertdatetime,chk_name_id) 
                    values ( :schedule_no,:schedule_type,:alertdatetime,:chk_name_id) ", con);

                cmd.Parameters.Add(new NpgsqlParameter("schedule_no", DbType.Int32) { Value = scheNO });
                //インシデントなので3固定
                cmd.Parameters.Add(new NpgsqlParameter("schedule_type", DbType.String) { Value = "1" });
                cmd.Parameters.Add(new NpgsqlParameter("alertdatetime", DbType.DateTime) { Value = alertdatetime });
                cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });
                rowsaffected = cmd.ExecuteNonQuery();

                if (rowsaffected != 1)
                {
                    MessageBox.Show("タイマー対応テーブルに登録できませんでした。", "タイマー登録");
                    return -1;
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("タイマー対応テーブル登録エラー " + ex.Message);
                return -1;
            }
            return 1;

        }



        //一覧をダブルクリック
        private void m_host_List_DoubleClick(object sender, EventArgs e)
        {

            ListView.SelectedIndexCollection item = m_incidentList.SelectedIndices;
            incidentDS incidentdt = new incidentDS();
            incidentdt.incident_no = this.m_incidentList.Items[item[0]].SubItems[0].Text;
            if (this.m_incidentList.Items[item[0]].SubItems[1].Text == "完了")
                incidentdt.status = "0";
            else if (this.m_incidentList.Items[item[0]].SubItems[1].Text == "未完了")
                incidentdt.status = "1";
            else
                incidentdt.status = "";

            incidentdt.mpms_incident = this.m_incidentList.Items[item[0]].SubItems[2].Text;
            incidentdt.s_cube_id = this.m_incidentList.Items[item[0]].SubItems[3].Text;

            //1:アラーム検知 2:障害申告 3:問い合わせ
            if (this.m_incidentList.Items[item[0]].SubItems[4].Text == "アラーム検知")
                incidentdt.incident_type = "1";
            else if (this.m_incidentList.Items[item[0]].SubItems[4].Text == "障害申告")
                incidentdt.incident_type = "2";
            else if (this.m_incidentList.Items[item[0]].SubItems[4].Text == "問い合わせ")
                incidentdt.incident_type = "3";
            else
                incidentdt.incident_type = "";


            incidentdt.content = this.m_incidentList.Items[item[0]].SubItems[5].Text;
            if (this.m_incidentList.Items[item[0]].SubItems[6].Text == "無")
                incidentdt.matflg = "0";
            else if (this.m_incidentList.Items[item[0]].SubItems[6].Text == "有")
                incidentdt.matflg = "1";
            else
                incidentdt.matflg = "";



            incidentdt.matcommand = this.m_incidentList.Items[item[0]].SubItems[7].Text;
            incidentdt.uketukedate = this.m_incidentList.Items[item[0]].SubItems[8].Text;
            incidentdt.tehaidate = this.m_incidentList.Items[item[0]].SubItems[9].Text;
            incidentdt.fukyudate = this.m_incidentList.Items[item[0]].SubItems[10].Text;
            incidentdt.enddate = this.m_incidentList.Items[item[0]].SubItems[11].Text;
            incidentdt.timer = this.m_incidentList.Items[item[0]].SubItems[12].Text;
            incidentdt.kakuninmsg = this.m_incidentList.Items[item[0]].SubItems[13].Text;
        
            incidentdt.userno = this.m_incidentList.Items[item[0]].SubItems[14].Text;
            incidentdt.systemno = this.m_incidentList.Items[item[0]].SubItems[15].Text;
            incidentdt.siteno = this.m_incidentList.Items[item[0]].SubItems[16].Text;
            incidentdt.hostno = this.m_incidentList.Items[item[0]].SubItems[17].Text;
            incidentdt.chk_date = this.m_incidentList.Items[item[0]].SubItems[18].Text;
            incidentdt.chk_name_id = this.m_incidentList.Items[item[0]].SubItems[19].Text;

            getIncident(incidentdt);

        }
        //メール出力画面
        private void m_mailout_Click(object sender, EventArgs e)
        {
            Form_mailTempleteList mailselectform = new Form_mailTempleteList();
            mailselectform.con = con;
            mailselectform.loginDS = loginDS;
            mailselectform.Show();
        }
        //インシデント情報一覧のカラムをクリックした時、ソート
        private void m_incidentList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.incident_list == null)
                return;
            if (this.incident_list.Rows.Count <= 0)
                return;
            //DataViewクラス ソートするためのクラス
            DataView dv = new DataView(incident_list);

            //一時クラス
            DataTable dttmp = new DataTable();

            String strSort = "";

            //0なら昇順にソート
            if (sort_kind == 0)
            {
                strSort = " ASC";
                sort_kind = 1;
            }
            else
            {
                //１の時は昇順にソート
                strSort = " DESC";
                sort_kind = 0;
            }

            //コピーを作成
            dttmp = incident_list.Clone();
            //ソートを実行
            dv.Sort = incident_list.Columns[e.Column].ColumnName + strSort;

            // ソートされたレコードのコピー
            foreach (DataRowView drv in dv)
            {
                // 一時テーブルに格納
                dttmp.ImportRow(drv.Row);
            }
            //格納したテーブルデータを上書く
            incident_list = dttmp.Copy();

            //行が存在するかチェックを行う。
            if (this.m_incidentList.TopItem != null)
            {
                //現在一番上の行に表示されている行を取得
                int start = m_incidentList.TopItem.Index;
                // ListView画面の再表示を行う
                m_incidentList.RedrawItems(start, m_incidentList.Items.Count - 1, true);
            }

        }
        //削除処理
        private void m_deleteBtn_Click(object sender, EventArgs e)
        {

            ListView.SelectedIndexCollection item = m_incidentList.SelectedIndices;
            int count = item.Count;

            //確認メッセージ
            if (MessageBox.Show("一覧に選択された行 " + count + "件 の削除を行います。" + Environment.NewLine +
                "よろしいですか？", "インシデント削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;


            int ret = deleteIncident(item);
            if (ret == -1)
            {
                return;
            }
            //リストの表示上からけす
            int i = 0;
            int[] indices = new int[item.Count];
            int cnt = m_incidentList.SelectedIndices.Count;


            m_incidentList.SelectedIndices.CopyTo(indices, 0);

            DataRowCollection items = incident_list.Rows;
            for (i = cnt - 1; i >= 0; --i)
                items.RemoveAt(indices[i]);

            //総件数を変更し再表示を行う
            this.m_incidentList.VirtualListSize = items.Count;


        }

        //削除
        private int deleteIncident(ListView.SelectedIndexCollection item)
        {

            string incidentno;

            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "DELETE FROM incident where incident_no = :no ";

            using (var transaction = con.BeginTransaction())
            {
                int ret = 0;
                int count = 0;
                int i=0;
                for (i = 0; i < item.Count; i++)
                {

                    incidentno = this.m_incidentList.Items[item[i]].SubItems[0].Text;


                    var command = new NpgsqlCommand(@sql, con);
                    command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = int.Parse(incidentno) });

                    Int32 rowsaffected;

                    try
                    {
                        //削除処理
                        rowsaffected = command.ExecuteNonQuery();

                        if (rowsaffected != 1) {
                            transaction.Rollback();
                            MessageBox.Show("削除できませんでした。ホストID:" + incidentno, "ホスト削除");

                            return -1;
                        }
                        else {
                            ret = 1;
                            count++;
                        }
                    }
                    catch (Exception ex)
                    {
                        //エラー時メッセージ表示
                        transaction.Rollback();
                        MessageBox.Show("インシデント削除時エラーが発生しました。 " + ex.Message);

                        return -1;
                    }
                }
                if (ret == 1) { 
                    transaction.Commit();
                    MessageBox.Show("削除完了しました。");
                }

            }
            return 1;
        }
        //カスタマコンボボックスが変更されたとき
        private void m_usernameCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (m_usernameCombo.Text == "")
            {
                m_userno.Text = "";
                return;
            }
            Read_systemCombo();


        }
        //システムコンボが変更されたとき
        private void m_systemCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (m_systemCombo.Text == "")
            {
                m_systemno.Text = "";
                return;
            }
            Read_siteCombo();

        }
        //拠点コンボボックスが変更されたとき
        private void m_siteCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (m_siteCombo.Text == "")
            {
                m_siteno.Text = "";
                return;
            }
            //ホスト名コンボボックスを取得する
            Read_hostCombo();
        }
        //ホストのコンボボックスが変更された時
        private void m_hostCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //ラベルに反映
            if (m_hostCombo.SelectedValue != null)
                m_hostno.Text = m_hostCombo.SelectedValue.ToString();

        }


    }
}
