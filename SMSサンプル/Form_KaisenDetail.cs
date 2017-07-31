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
    public partial class Form_KaisenDetail : Form
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        //ログイン情報
        public opeDS loginDS { get; set; }

        public kaisenDS kaisendt { get; set; }

        //ユーザ情報一覧
        public List<userDS> userList { get; set; }
        //システム情報一覧
        public List<systemDS> systemList { get; set; }
        //拠点
        public List<siteDS> siteList { get; set; }
        //DBコネクション
        public NpgsqlConnection con { get; set; }

        //ListViewのソートの際に使用する
        private int sort_kind = 0;

        //回線の一覧
        DataTable kaisen_list;


        public Form_KaisenDetail()
        {
            InitializeComponent();
        }
        
        //表示前処理
        private void Form_InterfaceDetail_Load(object sender, EventArgs e)
        {

            m_selectKoumoku.Items.Add("回線通番");
            m_selectKoumoku.Items.Add("ステータス");
            m_selectKoumoku.Items.Add("キャリア");
            m_selectKoumoku.Items.Add("回線種別");
            m_selectKoumoku.Items.Add("回線ID");
            m_selectKoumoku.Items.Add("ISP");
            m_selectKoumoku.Items.Add("サービス種別");
            m_selectKoumoku.Items.Add("サービスID");
            m_selectKoumoku.Items.Add("カスタマ通番");
            m_selectKoumoku.Items.Add("システム通番");
            m_selectKoumoku.Items.Add("拠点通番");
            m_selectKoumoku.Items.Add("ホスト番号");
            m_selectKoumoku.Items.Add("電話番号");

            m_selectKoumoku.Items.Add("更新日時");
            m_selectKoumoku.Items.Add("更新者");
            if(kaisendt!= null)
                getkaisen(kaisendt);
            else
                Read_CustomerCombo();
        }
        
        private void getkaisen(kaisenDS kaisendt)
        {
            this.m_kaisenno.Text = kaisendt.kaisenno;
            this.m_hostno.Text = kaisendt.host_no;
            this.m_userno.Text = kaisendt.userno;
            this.m_systemno.Text = kaisendt.systemno;
            this.m_siteno.Text = kaisendt.siteno;
            this.m_career.Text = kaisendt.career;
            this.m_statusCombo.Text = kaisendt.status;
            this.m_kaisentype.Text = kaisendt.type;

            this.m_tel1.Text = kaisendt.telno1;
            this.m_tel2.Text = kaisendt.telno2;
            this.m_tel3.Text = kaisendt.telno3;

            this.m_kaisenID.Text = kaisendt.kaisenid;
            this.m_isp.Text = kaisendt.isp;
            this.m_serviceType.Text = kaisendt.servicetype;
            this.m_serviceID.Text = kaisendt.serviceid;
            this.m_updateOpe.Text = kaisendt.chk_name_id;
            this.m_update.Text = kaisendt.chk_date;


            //コンボボックスを読み込む

            Read_CustomerCombo();
            m_usernameCombo.SelectedValue = kaisendt.userno;
            if (m_usernameCombo.SelectedValue != null)
            {
                Read_systemCombo();
                m_systemCombo.SelectedValue = kaisendt.systemno;
            }
            if (m_systemCombo.SelectedValue != null && m_systemCombo.SelectedValue.ToString() != "")
            {
                Read_siteCombo();
                m_siteCombo.SelectedValue = kaisendt.siteno;
            }
            if (m_siteCombo.SelectedValue != null && m_siteCombo.SelectedValue.ToString() != "")
            {
                Read_hostCombo();
                m_hostCombo.SelectedValue = kaisendt.host_no;
                if (m_hostCombo.Text != "")
                    m_hostno.Text = m_hostCombo.SelectedValue.ToString();
            }
        }
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
                if (m_siteCombo.Text != "")
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
                    if (m_hostCombo.Text != "")
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
            m_kaisenList.Clear();
            DISP_dataSet dset = new DISP_dataSet();
            Dictionary<string, string> param_dict = new Dictionary<string, string>();
            Class_Detaget dg = new Class_Detaget();

            if (m_selecttext.Text != "")
            {

                if (this.m_selectKoumoku.SelectedIndex.ToString() != "")
                {
                    switch (this.m_selectKoumoku.SelectedIndex)
                    {

                        case 0:
                            param_dict["kaisenno"] = m_selecttext.Text;
                            break;
                        case 1:
                            if (m_selecttext.Text == "無効")
                                param_dict["status"] = "0";
                            else if (m_selecttext.Text == "有効")
                                param_dict["status"] = "1";
                            break;
                        //ホスト名日本
                        case 2:
                            param_dict["career"] = m_selecttext.Text;
                            break;

                        case 3:
                            param_dict["type"] = m_selecttext.Text;
                            break;

                        case 4:
                            param_dict["kaisenid"] = m_selecttext.Text;
                            break;
    
                        case 5:
                            param_dict["isp"] = m_selecttext.Text;
                            break;

                        case 6:
                            param_dict["servicetype"] = m_selecttext.Text;
                            break;

                        case 7:
                            param_dict["serviceid"] = m_selecttext.Text;
                            break;
                        case 8:
                            param_dict["userno"] = m_selecttext.Text;
                            break;
                        case 9:
                            param_dict["systemno"] = m_selecttext.Text;
                            break;
                        case 10:
                            param_dict["siteno"] = m_selecttext.Text;
                            break;
                        case 11:
                            param_dict["host_no"] = m_selecttext.Text;
                            break;
                        case 12:
                            param_dict["telno"] = m_selecttext.Text;
                            break;

                        //更新日時
                        case 13:
                            DateTime dt;
                            String str = m_selecttext.Text;

                            //入力された日付の形式の確認
                            if (DateTime.TryParse(str, out dt))
                            {
                                param_dict["start_date"] = str;
                            }
                            else
                            {
                                MessageBox.Show("日付の形式が正しくありません。", "監視インターフェイス検索");
                                return;
                            }

                            break;
                        //更新者
                        case 14:
                            param_dict["chk_name_id"] = m_selecttext.Text;
                            break;
                        default:
                            break;

                    }
                }
            }

            //回線一覧を取得する
            dset = dg.getSelectKaisenList(param_dict, con, dset);

            this.m_kaisenList.VirtualMode = true;
            // １行全体選択
            this.m_kaisenList.FullRowSelect = true;
            this.m_kaisenList.HideSelection = false;
            this.m_kaisenList.HeaderStyle = ColumnHeaderStyle.Clickable;
            //Hook up handlers for VirtualMode events.
            this.m_kaisenList.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(Kaisen_RetrieveVirtualItem);
            this.m_kaisenList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.m_kaisenList.Scrollable = true;


            this.m_kaisenList.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(1, "ステータス", 50, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(2, "キャリア", 180, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(3, "回線種別", 180, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(4, "回線ID", 150, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(5, "ISP", 180, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(6, "サービス種別", 180, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(7, "サービスID", 180, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(8, "電話番号1", 70, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(9, "電話番号2", 70, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(10, "電話番号3", 70, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(11, "カスタマ通番", 50, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(12, "システム通番", 50, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(13, "拠点通番", 50, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(14, "ホスト通番", 50, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(15, "更新日時", 120, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(16, "更新者", 50, HorizontalAlignment.Left);

            //リストビューを初期化する
            kaisen_list = new DataTable("table1");
            kaisen_list.Columns.Add("No", Type.GetType("System.Int32"));
            kaisen_list.Columns.Add("ステータス", Type.GetType("System.String"));
            kaisen_list.Columns.Add("キャリア", Type.GetType("System.String"));
            kaisen_list.Columns.Add("回線種別", Type.GetType("System.String"));
            kaisen_list.Columns.Add("回線ID", Type.GetType("System.String"));
            kaisen_list.Columns.Add("ISP", Type.GetType("System.String"));
            kaisen_list.Columns.Add("サービス種別", Type.GetType("System.String"));
            kaisen_list.Columns.Add("サービスID", Type.GetType("System.String"));
            kaisen_list.Columns.Add("電話番号1", Type.GetType("System.String"));
            kaisen_list.Columns.Add("電話番号2", Type.GetType("System.String"));
            kaisen_list.Columns.Add("電話番号3", Type.GetType("System.String"));
            kaisen_list.Columns.Add("カスタマ通番", Type.GetType("System.String"));
            kaisen_list.Columns.Add("システム通番", Type.GetType("System.String"));
            kaisen_list.Columns.Add("拠点通番", Type.GetType("System.String"));
            kaisen_list.Columns.Add("ホスト通番", Type.GetType("System.String"));
            kaisen_list.Columns.Add("更新日時", Type.GetType("System.String"));
            kaisen_list.Columns.Add("更新者", Type.GetType("System.String"));

            //リストに表示
            if (dset.kaisen_L != null)
            {
                m_kaisenList.BeginUpdate();

                foreach (kaisenDS s_ds in dset.kaisen_L)
                {
                    DataRow urow = kaisen_list.NewRow();

                    urow["No"] = s_ds.kaisenno;
                    urow["ステータス"] = s_ds.status;
                    urow["キャリア"] = s_ds.career;
                    urow["回線種別"] = s_ds.type;
                    urow["回線ID"] = s_ds.kaisenid;
                    urow["ISP"] = s_ds.isp;
                    urow["サービス種別"] = s_ds.servicetype;
                    urow["サービスID"] = s_ds.serviceid;
                    urow["電話番号1"] = s_ds.telno1;
                    urow["電話番号2"] = s_ds.telno2;
                    urow["電話番号3"] = s_ds.telno3;
                    urow["カスタマ通番"] = s_ds.userno;
                    urow["システム通番"] = s_ds.systemno;
                    urow["拠点通番"] = s_ds.siteno;
                    urow["ホスト通番"] = s_ds.host_no;
                    urow["更新日時"] = s_ds.chk_date;
                    urow["更新者"] = s_ds.chk_name_id;
                    kaisen_list.Rows.Add(urow);
                }
                this.m_kaisenList.VirtualListSize = kaisen_list.Rows.Count;
                this.m_kaisenList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                m_kaisenList.EndUpdate();
            }
        }
        void Kaisen_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            //	e.Item = _item[e.ItemIndex];
            if (kaisen_list.Rows.Count > 0)
            {

                DataRow row = this.kaisen_list.Rows[e.ItemIndex];
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
                Convert.ToString(row[16])
                    });
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //更新ボタン
        private void button1_Click(object sender, EventArgs e)
        {
            if (m_career.Text == "")
            {
                MessageBox.Show("キャリアを入力して下さい。", "回線情報修正", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //確認ダイアログ
            if (MessageBox.Show("回線情報の更新を行います。よろしいですか？", "回線情報更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            string status = "";
            if (m_statusCombo.Text == "有効")
                status = "1";
            else if (m_statusCombo.Text == "無効")
                status = "0";

            int userno;
            int systemno;
            int siteno;
            int hostno;
            int? userno2;
            int? systemno2;
            int? siteno2;
            int? hostno2;

            if (int.TryParse(m_userno.Text, out userno))
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

            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "update kaisen set status=:status,career=:career,type=:type,kaisenid=:kaisenid,isp=:isp," +
                "servicetype=:servicetype,serviceid=:serviceid,telno1=:telno1,telno2=:telno2,telno3=:telno3,userno=:userno,systemno=:systemno,siteno=:siteno,host_no=:hostno,chk_name_id =:ope,chk_date=:chdate where kaisenno = :no";
            using (var transaction = con.BeginTransaction())
            {
                var command = new NpgsqlCommand(@sql, con);
                command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = m_kaisenno.Text });
                command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                command.Parameters.Add(new NpgsqlParameter("career", DbType.String) { Value = m_career.Text });
                command.Parameters.Add(new NpgsqlParameter("type", DbType.String) { Value = m_kaisentype.Text });
                command.Parameters.Add(new NpgsqlParameter("kaisenid", DbType.String) { Value = m_kaisenID.Text });
                command.Parameters.Add(new NpgsqlParameter("isp", DbType.String) { Value = m_isp.Text });
                command.Parameters.Add(new NpgsqlParameter("servicetype", DbType.String) { Value = m_serviceType.Text});
                command.Parameters.Add(new NpgsqlParameter("serviceid", DbType.String) { Value = m_serviceID.Text });
                command.Parameters.Add(new NpgsqlParameter("telno1", DbType.String) { Value = m_tel1.Text });
                command.Parameters.Add(new NpgsqlParameter("telno2", DbType.String) { Value = m_tel2.Text });
                command.Parameters.Add(new NpgsqlParameter("telno3", DbType.String) { Value = m_tel3.Text });
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

                    if (rowsaffected < 1)
                        MessageBox.Show("更新できませんでした。", "回線種別");
                    else
                        MessageBox.Show("更新されました。", "回線種別");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    //エラー時メッセージ表示
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }

        //ダブルクリック
        private void m_host_List_DoubleClick(object sender, EventArgs e)
        {

            ListView.SelectedIndexCollection item = m_kaisenList.SelectedIndices;
            string status = "";

            kaisenDS kaisendt = new kaisenDS();

            kaisendt.kaisenno = this.m_kaisenList.Items[item[0]].SubItems[0].Text;
            if (this.m_kaisenList.Items[item[0]].SubItems[1].Text == "有効")
                status = "1";
            else if (this.m_kaisenList.Items[item[0]].SubItems[1].Text == "無効")
                status = "0";
            kaisendt.status = status;

            kaisendt.career = this.m_kaisenList.Items[item[0]].SubItems[2].Text;
            kaisendt.type = this.m_kaisenList.Items[item[0]].SubItems[3].Text;
            kaisendt.kaisenid = this.m_kaisenList.Items[item[0]].SubItems[4].Text;
            kaisendt.isp = this.m_kaisenList.Items[item[0]].SubItems[5].Text;
            kaisendt.servicetype = this.m_kaisenList.Items[item[0]].SubItems[6].Text;
            kaisendt.serviceid = this.m_kaisenList.Items[item[0]].SubItems[7].Text;
            kaisendt.telno1 = this.m_kaisenList.Items[item[0]].SubItems[8].Text;
            kaisendt.telno2 = this.m_kaisenList.Items[item[0]].SubItems[9].Text;
            kaisendt.telno3 = this.m_kaisenList.Items[item[0]].SubItems[10].Text;
            kaisendt.userno = this.m_kaisenList.Items[item[0]].SubItems[11].Text;
            kaisendt.systemno = this.m_kaisenList.Items[item[0]].SubItems[12].Text;
            kaisendt.siteno = this.m_kaisenList.Items[item[0]].SubItems[13].Text;
            kaisendt.host_no = this.m_kaisenList.Items[item[0]].SubItems[14].Text;
            kaisendt.chk_date= this.m_kaisenList.Items[item[0]].SubItems[15].Text;
            kaisendt.chk_name_id = this.m_kaisenList.Items[item[0]].SubItems[16].Text;
            getkaisen(kaisendt);
        }
        //一覧のカラムをクリックした時
        private void m_kaisenList_ColumnClick(object sender, ColumnClickEventArgs e)
        {

            if (this.kaisen_list == null)
                return;
            if (this.kaisen_list.Rows.Count <= 0)
                return;
            //DataViewクラス ソートするためのクラス
            DataView dv = new DataView(kaisen_list);

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
            dttmp = kaisen_list.Clone();
            //ソートを実行
            dv.Sort = kaisen_list.Columns[e.Column].ColumnName + strSort;

            // ソートされたレコードのコピー
            foreach (DataRowView drv in dv)
            {
                // 一時テーブルに格納
                dttmp.ImportRow(drv.Row);
            }
            //格納したテーブルデータを上書く
            kaisen_list = dttmp.Copy();

            //行が存在するかチェックを行う。
            if (this.m_kaisenList.TopItem != null)
            {
                //現在一番上の行に表示されている行を取得
                int start = m_kaisenList.TopItem.Index;
                // ListView画面の再表示を行う
                m_kaisenList.RedrawItems(start, m_kaisenList.Items.Count - 1, true);
            }
        }
        //削除
        private void m_deleteBtn_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection item = m_kaisenList.SelectedIndices;
            int count = item.Count;


            //確認メッセージ
            if (MessageBox.Show("一覧に選択された行 " + count + "件 の削除を行います。" + Environment.NewLine +
                "よろしいですか？", "回線情報削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int ret = deleteKaisen(item);
            if (ret == -1)
                return;

            //リストの表示上からけす
            int i = 0;
            //削除する回線の取得
            int[] indices = new int[item.Count];
            int cnt = m_kaisenList.SelectedIndices.Count;

            m_kaisenList.SelectedIndices.CopyTo(indices, 0);

            DataRowCollection items = kaisen_list.Rows;
            for (i = cnt - 1; i >= 0; --i)
                items.RemoveAt(indices[i]);

            //総件数を変更し再表示を行う
            this.m_kaisenList.VirtualListSize = items.Count;
        }

        //削除
        private int deleteKaisen(ListView.SelectedIndexCollection item)
        {

            string kaisenno;
            int ret = 0;

            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "DELETE FROM kaisen where kaisenno = :no ";

            using (var transaction = con.BeginTransaction())
            {
                int i = 0;
                for (i = 0; i < item.Count; i++)
                {
                    kaisenno = this.m_kaisenList.Items[item[i]].SubItems[0].Text;

                    var command = new NpgsqlCommand(@sql, con);
                    command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = int.Parse(kaisenno) });

                    Int32 rowsaffected;
                    try
                    {
                        //削除処理
                        rowsaffected = command.ExecuteNonQuery();

                        if (rowsaffected < 1)
                        {
                            MessageBox.Show("削除できませんでした。回線ID:" + kaisenno, "回線情報削除");
                            logger.WarnFormat("回線情報削除エラー。SQL:{0}", sql);
                            ret = -1;
                        }
                        else
                            ret = 1;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        //エラー時メッセージ表示
                        MessageBox.Show("回線情報削除時エラーが発生しました。 " + ex.Message);
                        ret = -1;
                    }
                }
                if (ret == 1)
                {
                    transaction.Commit();
                    MessageBox.Show("削除完了しました。", "回線情報削除");
                    logger.InfoFormat("回線情報削除完了。 回線情報");
                }
            }
            return ret;
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
