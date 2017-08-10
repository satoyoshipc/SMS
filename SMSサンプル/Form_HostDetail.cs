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
    public partial class Form_HostDetail : Form
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //変更前ステータス
        private string orgStatus;

        //ログイン情報
        public opeDS loginDS { get; set; }

        public hostDS hostdt { get; set; }

        //ユーザ情報一覧
        public List<userDS> userList { get; set; }

        //システム情報一覧
        public List<systemDS> systemList { get; set; }

        //ホスト情報一覧
        public List<hostDS> hostList { get; set; }

        //DBコネクション
        public NpgsqlConnection con { get; set; }

        //ListViewのソートの際に使用する
        private int sort_kind = 0;

        //ホストの一覧
        DataTable host_list;

        public Form_HostDetail()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        //表示前処理
        private void Form_HostDetail_Load(object sender, EventArgs e)
        {

            this.splitContainer1.SplitterDistance = 32;

            m_selectKoumoku.Items.Add("ホスト番号");
            m_selectKoumoku.Items.Add("ホスト名");
            m_selectKoumoku.Items.Add("カスタマ通番");
            m_selectKoumoku.Items.Add("システム通番");
            m_selectKoumoku.Items.Add("拠点通番");
            m_selectKoumoku.Items.Add("ステータス");
            m_selectKoumoku.Items.Add("機種");
            m_selectKoumoku.Items.Add("設置場所");
            m_selectKoumoku.Items.Add("用途");
            m_selectKoumoku.Items.Add("設置機器ID");
            m_selectKoumoku.Items.Add("監視開始日時");
            m_selectKoumoku.Items.Add("監視終了日時");
            m_selectKoumoku.Items.Add("保守管理番号");
            m_selectKoumoku.Items.Add("保守情報");
            m_selectKoumoku.Items.Add("備考");
            m_selectKoumoku.Items.Add("更新日時");
            m_selectKoumoku.Items.Add("更新者");
            if(hostdt != null)
                gethost(hostdt);

        }
        void Host_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            //	e.Item = _item[e.ItemIndex];
            if (host_list.Rows.Count > 0)
            {

                DataRow row = this.host_list.Rows[e.ItemIndex];
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
        //ホスト情報を表示する
        private void gethost(hostDS hostdt)
        {
            this.m_hostno.Text = hostdt.host_no;
            this.m_userno.Text = hostdt.userno;
            this.m_systemno.Text = hostdt.systemno;
            this.m_siteno.Text = hostdt.siteno;
            this.m_hostname.Text = hostdt.hostname;
            this.m_settikikiid.Text = hostdt.settikikiid;
            this.m_statusCombo.Text = hostdt.status;

            //元のステータスを保存しておく
            orgStatus = hostdt.status;

            this.m_kisyu.Text = hostdt.device;
            this.m_locate.Text = hostdt.location;
            this.m_usefor.Text = hostdt.usefor;
            if (hostdt.kansiStartdate == "")
                m_start_date.Checked =false;
            else
                this.m_start_date.Text = hostdt.kansiStartdate;

            if (hostdt.kansiEndsdate == "")
                m_end_date.Checked = false;
            else
                this.m_end_date.Text = hostdt.kansiEndsdate;
            this.m_kanrino.Text = hostdt.hosyukanri;
            this.m_hosyu.Text = hostdt.hosyuinfo;
            this.m_biko.Text = hostdt.biko;
            this.m_update.Text = hostdt.chk_date;
            this.m_updateOpe.Text = hostdt.chk_name_id;

            Class_Detaget dg = new Class_Detaget();
            dg.con = con;
            if (hostdt.userno != "")
                this.m_cutomername.Text = dg.getCustomername(hostdt.userno);
            //システム情報
            if (hostdt.systemno != "")
                this.m_systemname.Text = dg.getSystemname(hostdt.systemno);
            //拠点名取得
            if (hostdt.siteno != "")
                this.m_sitename.Text = dg.getSitename(hostdt.siteno);

        }
        //検索ボタン
        private void m_selectBtn_Click(object sender, EventArgs e)
        {
            m_host_List.Clear();
            DISP_dataSet dset = new DISP_dataSet();
            Dictionary<string, string> param_dict = new Dictionary<string, string>();
            Class_Detaget dg = new Class_Detaget();

            if (m_selecttext.Text != "")
            {

                if (this.m_selectKoumoku.SelectedIndex.ToString() != "")
                {
                    switch (this.m_selectKoumoku.SelectedIndex)
                    {                                      
                        //ホスト通番
                        case 0:
                            param_dict["host_no"] = m_selecttext.Text;
                            break;
                        //ホスト名
                        case 1:
                            param_dict["hostname"] = m_selecttext.Text;
                            break;
                        //カスタマ通番
                        case 2:
                            param_dict["userno"] = m_selecttext.Text;
                            break;
                        case 3:
                            param_dict["systemno"] = m_selecttext.Text;
                            break;
                        case 4:
                            param_dict["siteno"] = m_selecttext.Text;
                            break;

                        case 5:
                            if (m_selecttext.Text == "有効")
                                param_dict["status"] = "1";
                            else if (m_selecttext.Text == "無効")
                                param_dict["status"] = "0";
                            break;


                        case 6:
                            param_dict["device"] = m_selecttext.Text;
                            break;

                        case 7:
                            param_dict["location"] = m_selecttext.Text;
                            break;

                        case 8:
                            param_dict["usefor"] = m_selecttext.Text;
                            break;
                        //設置機器ID
                        case 9:
                            param_dict["settikikiid"] = m_selecttext.Text;
                            break;

                        case 10:
                            DateTime dt;
                            String str = m_selecttext.Text;

                            //入力された日付の形式の確認
                            if (DateTime.TryParse(str, out dt))
                            {
                                param_dict["kansiStartdate"] = str;
                            }
                            else
                            {

                                MessageBox.Show("日付の形式が正しくありません。", "拠点検索");
                                return;
                            }
                            break;

                        case 11:
                            
                            str = m_selecttext.Text;

                            //入力された日付の形式の確認
                            if (DateTime.TryParse(str, out dt))
                            {
                                param_dict["kansiEndsdate"] = str;
                            }
                            else
                            {
                                MessageBox.Show("日付の形式が正しくありません。", "拠点検索");
                                return;
                            }

                            break;
                        case 12:
                            param_dict["hosyukanri"] = m_selecttext.Text;
                            break;
                        case 13:
                            param_dict["hosyuinfo"] = m_selecttext.Text;
                            break;
                        case 14:
                            param_dict["biko"] = m_selecttext.Text;
                            break;

                        //更新日時
                        case 15:
                            
                            str = m_selecttext.Text;

                            //入力された日付の形式の確認
                            if (DateTime.TryParse(str, out dt))
                            {
                                param_dict["chk_date"] = str;
                            }
                            else
                            {

                                MessageBox.Show("日付の形式が正しくありません。", "拠点検索");
                                return;
                            }
                            break;
                        //更新者
                        case 16:
                            param_dict["chk_name_id"] = m_selecttext.Text;
                            break;
                        default:
                            break;

                    }
                }
            }
            //まず件数を取得する
            Int64 count = dg.getSelectHostCount(param_dict, con, dset, true);
            if (MessageBox.Show(count.ToString() + "件ヒットしました。表示しますか？", "ホスト", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            //ホスト一覧を取得する
            dset = dg.getSelectHost(param_dict, con, dset);

            this.splitContainer1.SplitterDistance = 280;

            this.m_host_List.VirtualMode = true;
            // １行全体選択
            this.m_host_List.FullRowSelect = true;
            this.m_host_List.HideSelection = false;
            this.m_host_List.HeaderStyle = ColumnHeaderStyle.Clickable;
            //Hook up handlers for VirtualMode events.
            this.m_host_List.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(Host_RetrieveVirtualItem);
            this.m_host_List.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.m_host_List.Scrollable = true;

            this.m_host_List.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(1, "ホスト名", 120, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(2, "ステータス", 90, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(3, "機種", 80, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(4, "設置場所", 50, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(5, "用途", 50, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(6, "設置機器ID", 120, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(7, "監視開始日時", 120, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(8, "監視終了日時", 120, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(9, "保守管理番号", 50, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(10, "保守情報", 50, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(11, "備考", 50, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(12, "カスタマ番号", 50, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(13, "システム通番番号", 50, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(14, "拠点通番", 50, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(15, "更新日時", 50, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(16, "更新者", 50, HorizontalAlignment.Left);
 
            //リストビューを初期化する
            host_list = new DataTable("table1");
            host_list.Columns.Add("No", Type.GetType("System.Int32"));
            host_list.Columns.Add("ホスト名", Type.GetType("System.String"));
            host_list.Columns.Add("ステータス", Type.GetType("System.String"));
            host_list.Columns.Add("機種", Type.GetType("System.String"));
            host_list.Columns.Add("設置場所", Type.GetType("System.String"));
            host_list.Columns.Add("用途", Type.GetType("System.String"));
            host_list.Columns.Add("設置機器ID", Type.GetType("System.String"));
            host_list.Columns.Add("監視開始日時", Type.GetType("System.String"));
            host_list.Columns.Add("監視終了日時", Type.GetType("System.String"));
            host_list.Columns.Add("保守管理番号", Type.GetType("System.String"));
            host_list.Columns.Add("保守情報", Type.GetType("System.String"));
            host_list.Columns.Add("備考", Type.GetType("System.String"));
            host_list.Columns.Add("カスタマ番号", Type.GetType("System.String"));
            host_list.Columns.Add("システム通番番号", Type.GetType("System.String"));
            host_list.Columns.Add("拠点通番", Type.GetType("System.String"));
            host_list.Columns.Add("更新日時", Type.GetType("System.String"));
            host_list.Columns.Add("更新者", Type.GetType("System.String"));

            //リストに表示
            if (dset.host_L != null)
            {
                m_host_List.BeginUpdate();

                foreach (hostDS s_ds in dset.host_L)
                {

                    DataRow urow = host_list.NewRow(); 

                    urow["No"] = s_ds.host_no;

                    urow["ホスト名"] = s_ds.hostname;
                    urow["ステータス"] = s_ds.status;
                    urow["機種"] = s_ds.device;
                    urow["設置場所"] = s_ds.location;
                    urow["用途"] = s_ds.usefor;
                    urow["設置機器ID"] = s_ds.settikikiid;
                    urow["監視開始日時"] = s_ds.kansiStartdate;
                    urow["監視終了日時"] = s_ds.kansiEndsdate;
                    urow["保守管理番号"] = s_ds.hosyukanri;
                    urow["保守情報"] = s_ds.hosyuinfo;
                    urow["備考"] = s_ds.biko;
                    urow["カスタマ番号"] = s_ds.userno;
                    urow["システム通番番号"] = s_ds.systemno;
                    urow["拠点通番"] = s_ds.siteno;
                    urow["更新日時"] = s_ds.chk_date;
                    urow["更新者"] = s_ds.chk_name_id;

                    host_list.Rows.Add(urow);
                }
                this.m_host_List.VirtualListSize = host_list.Rows.Count;
                this.m_host_List.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);


                m_host_List.EndUpdate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //更新ボタン
        private void button1_Click(object sender, EventArgs e)
        {
            if (m_hostname.Text == "")
            {
                MessageBox.Show("ホスト名を入力して下さい。", "ホスト名修正", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //確認ダイアログ
            if (MessageBox.Show("ホスト名データの更新を行います。よろしいですか？", "ホスト名データ更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            string status = "";
            if (m_statusCombo.Text == "有効")
                status = "1";
            else if (m_statusCombo.Text == "無効")
                status = "0";

            DateTime? startdate = null;
            DateTime? enddate = null;
            if (m_start_date.Checked)
                startdate = m_start_date.Value;

            if (m_end_date.Checked)
                enddate = m_end_date.Value;

            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "update host set hostname=:hostname,settikikiid=:settikikiid,status=:status,device=:device,location=:location,usefor=:usefor," +
                "kansiStartdate=:kansiStartdate,kansiEndsdate=:kansiEndsdate,hosyukanri=:hosyukanri,hosyuinfo=:hosyuinfo,biko=:biko," +
                "chk_name_id =:ope,chk_date=:chdate where host_no = :no";
            using (var transaction = con.BeginTransaction())
            {
                var command = new NpgsqlCommand(@sql, con);
                command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = m_hostno.Text });
                command.Parameters.Add(new NpgsqlParameter("hostname", DbType.String) { Value = m_hostname.Text });
                command.Parameters.Add(new NpgsqlParameter("settikikiid", DbType.String) { Value = m_settikikiid.Text });
                command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                command.Parameters.Add(new NpgsqlParameter("device", DbType.String) { Value = m_kisyu.Text });
                command.Parameters.Add(new NpgsqlParameter("location", DbType.String) { Value = m_locate.Text });
                command.Parameters.Add(new NpgsqlParameter("usefor", DbType.String) { Value = m_usefor.Text });
                command.Parameters.Add(new NpgsqlParameter("kansiStartdate", DbType.DateTime) { Value = startdate });
                command.Parameters.Add(new NpgsqlParameter("kansiEndsdate", DbType.DateTime) { Value = enddate });
                command.Parameters.Add(new NpgsqlParameter("hosyukanri", DbType.String) { Value = m_kanrino.Text });
                command.Parameters.Add(new NpgsqlParameter("hosyuinfo", DbType.String) { Value = m_hosyu.Text });
                command.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = m_biko.Text });

                command.Parameters.Add(new NpgsqlParameter("ope", DbType.String) { Value = loginDS.opeid });
                command.Parameters.Add(new NpgsqlParameter("chdate", DbType.DateTime) { Value = DateTime.Now });
                Int32 rowsaffected;
                try
                {
                    //更新処理
                    rowsaffected = command.ExecuteNonQuery();

                    if (rowsaffected != 1)
                    {
                        transaction.Rollback();
                        MessageBox.Show("更新できませんでした。", "ホスト更新");
                        logger.ErrorFormat("ホスト情報更新エラー メソッド名：{0}。ホスト名：{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, m_hostname.Text);

                    }
                    else
                    {
                        //ステータスが変わっている場合は下位伝播する
                        if (orgStatus != m_statusCombo.Text.Trim())
                        {
                            //下位伝播
                            int ret = statusCascade(m_hostno.Text, status);
                            if (ret == -1)
                            {
                                transaction.Rollback();
                                MessageBox.Show("下位伝播時にエラーが発生しました。ログを確認してください。", "ホスト更新");

                                return;
                            }

                        }
                        transaction.Commit();
                        MessageBox.Show("更新されました。", "ホスト更新");
                    }
                }
                catch (Exception ex)
                {
                    //エラー時メッセージ表示
                    if (transaction.Connection != null) transaction.Rollback();
                    MessageBox.Show("ホスト情報更新エラー " + ex.Message);
                    logger.ErrorFormat("ホスト情報更新エラー メソッド名：{0}。MSG：{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);

                    return;
                }
            }
        }
        //下位伝播ステータスのみ
        private int statusCascade(String hostno, String status)
        {
            int ret = 0;


            //監視インターフェイス
            ret = interface_update(hostno, status);
            if (ret == -1)
                return ret;
            //回線情報
            ret = kasen_update(hostno, status);
            if (ret == -1)
                return ret;

            return ret;
        }

        //監視インターフェイスステータス更新
        private int interface_update(String hostno, String status)
        {
            int ret = 0;

            string sql = "update watch_Interface set status=:status,chk_name_id =:chk_name_id,chk_date=:chk_date " +
                "WHERE host_no=:hostno";

            var command = new NpgsqlCommand(@sql, con);
            command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
            command.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });
            command.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = DateTime.Now });
            command.Parameters.Add(new NpgsqlParameter("hostno", DbType.Int32) { Value = hostno });
            Int32 rowsaffected;
            try
            {
                //更新処理
                rowsaffected = command.ExecuteNonQuery();


                if (rowsaffected < 1)
                {
                    logger.Warn("監視インターフェイスのステータスを更新できませんでした。配下の監視インターフェイス数が0件のときはこのメッセージが出ることがあります。" + " ホスト:" + m_hostname.Text);
                    ret = 0;
                }
                else
                {
                    logger.Info("監視インターフェイスのステータスを更新しました。" + " ホスト:" + m_hostname.Text);
                    ret = 1;
                }
            }
            catch (Exception ex)
            {
                //エラー時メッセージ表示
                MessageBox.Show("監視インターフェイスステータス更新エラー " + ex.Message);
                return -1;
            }

            return ret;
        }

        //回線情報
        private int kasen_update(String hostno, String status)
        {
            int ret = 0;

            string sql = "update Kaisen set status=:status,chk_name_id =:chk_name_id,chk_date=:chk_date " +
                "WHERE host_no=:hostno";

            var command = new NpgsqlCommand(@sql, con);
            command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
            command.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });
            command.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = DateTime.Now });
            command.Parameters.Add(new NpgsqlParameter("hostno", DbType.Int32) { Value = hostno });
            Int32 rowsaffected;
            try
            {
                //更新処理
                rowsaffected = command.ExecuteNonQuery();


                if (rowsaffected < 1)
                {
                    logger.Warn("回線情報のステータスを更新できませんでした。配下の回線情報が0件のときはこのメッセージが出ることがあります。" + " ホスト:" + m_hostname.Text);
                    ret = 0;
                }
                else
                {
                    logger.Info("回線情報のステータスを更新しました。" + " ホスト:" + m_hostname.Text);
                    ret = 1;
                }
            }
            catch (Exception ex)
            {
                //エラー時メッセージ表示
                MessageBox.Show("回線情報更新エラー " + ex.Message);
                return -1;
            }

            return ret;
        }


        //ダブルクリック
        private void m_host_List_DoubleClick(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection item = m_host_List.SelectedIndices;
            hostDS hostdt = new hostDS();
            hostdt.host_no = this.m_host_List.Items[item[0]].SubItems[0].Text;
            hostdt.hostname = this.m_host_List.Items[item[0]].SubItems[1].Text;
            hostdt.status = this.m_host_List.Items[item[0]].SubItems[2].Text;
            hostdt.device = this.m_host_List.Items[item[0]].SubItems[3].Text;
            hostdt.location = this.m_host_List.Items[item[0]].SubItems[4].Text;
            hostdt.usefor = this.m_host_List.Items[item[0]].SubItems[5].Text;
            hostdt.settikikiid = this.m_host_List.Items[item[0]].SubItems[6].Text;
            hostdt.kansiStartdate = this.m_host_List.Items[item[0]].SubItems[7].Text;
            hostdt.kansiEndsdate = this.m_host_List.Items[item[0]].SubItems[8].Text;
            hostdt.hosyukanri = this.m_host_List.Items[item[0]].SubItems[9].Text;
            hostdt.hosyuinfo = this.m_host_List.Items[item[0]].SubItems[10].Text;
            hostdt.biko = this.m_host_List.Items[item[0]].SubItems[11].Text;
            hostdt.userno = this.m_host_List.Items[item[0]].SubItems[12].Text;
            hostdt.systemno = this.m_host_List.Items[item[0]].SubItems[13].Text;
            hostdt.siteno = this.m_host_List.Items[item[0]].SubItems[14].Text;

            hostdt.chk_date = this.m_host_List.Items[item[0]].SubItems[15].Text;
            hostdt.chk_name_id = this.m_host_List.Items[item[0]].SubItems[16].Text;
            gethost(hostdt);

            gethost(hostdt);
        }
        //ListViewのカラムをクリックした場合
        private void m_host_List_ColumnClick(object sender, ColumnClickEventArgs e)
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
            if (this.m_host_List.TopItem != null)
            {
                //現在一番上の行に表示されている行を取得
                int start = m_host_List.TopItem.Index;
                // ListView画面の再表示を行う
                m_host_List.RedrawItems(start, m_host_List.Items.Count - 1, true);
            }
        }
        //削除ボタン
        private void m_deleteBtn_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection item = m_host_List.SelectedIndices;
            int count = item.Count;


            if (MessageBox.Show("一覧に選択された行 "+ count + "件 の削除を行います。その際参照している他のテーブルデータも削除されます。"+ Environment.NewLine +
                "よろしいですか？", "ホスト名削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int ret = deleteHost(item);
            if(ret == -1)
            {
                return;
            }
            //リストの表示上からけす
            int i = 0;
            int[] indices = new int[item.Count];
            int cnt = m_host_List.SelectedIndices.Count;


            m_host_List.SelectedIndices.CopyTo(indices, 0);

            DataRowCollection items = host_list.Rows;
            for (i = cnt - 1; i >= 0; --i)
                items.RemoveAt(indices[i]);

            //総件数を変更し再表示を行う
            this.m_host_List.VirtualListSize = items.Count;

        }
        //削除
        private int deleteHost(ListView.SelectedIndexCollection item)
        {

            string hostno;
            int ret = 0;

            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "WITH DELETED AS (DELETE FROM host where host_no = :no " +
                "RETURNING host_no) " +
                "DELETE FROM watch_Interface WHERE host_no IN (SELECT host_no FROM DELETED)";

            using (var transaction = con.BeginTransaction())
            {
                int i;
                for (i = 0; i < item.Count; i++)
                {
                    hostno = this.m_host_List.Items[item[i]].SubItems[0].Text;

                    var command = new NpgsqlCommand(@sql, con);
                    command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = int.Parse(hostno)});

                    Int32 rowsaffected;
                    try
                    {
                        //削除処理
                        rowsaffected = command.ExecuteNonQuery();
                        //transaction.Commit();

                        if (rowsaffected < 1) { 
                            //MessageBox.Show("削除できませんでした。ホストID:" + hostno, "ホスト削除");
                            logger.WarnFormat("ホスト情報削除エラー　(監視インターフェイスが登録されていないホストに出る場合があります)。{0} ホスト番号:{1}", sql, hostno);
                            //                            transaction.Rollback();
                            //return -1;
                            ret = 1;
                        }
                        else
                        {
                            ret = 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        //エラー時メッセージ表示
                        transaction.Rollback();
                        MessageBox.Show(ex.Message);

                        return -1;
                    }
                }
                if(ret == 1)
                {
                    transaction.Commit();
                    MessageBox.Show("削除完了しました。", "ホスト情報削除");
                    logger.InfoFormat("ホスト情報削完了。 ホスト番号");


                }

            }
            return 1;
        }

    }
}
