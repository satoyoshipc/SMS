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
        private Class_ListViewColumnSorter _columnSorter;

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
            _columnSorter = new Class_ListViewColumnSorter();
            m_host_List.ListViewItemSorter = _columnSorter;

            m_selectKoumoku.Items.Add("ホスト番号");
            m_selectKoumoku.Items.Add("ホスト名(英数)");
            m_selectKoumoku.Items.Add("ホスト名(日本語)");
            m_selectKoumoku.Items.Add("カスタマ通番");
            m_selectKoumoku.Items.Add("システム通番");
            m_selectKoumoku.Items.Add("拠点通番");
            m_selectKoumoku.Items.Add("ステータス");
            m_selectKoumoku.Items.Add("機種");
            m_selectKoumoku.Items.Add("設置場所");
            m_selectKoumoku.Items.Add("用途");
            m_selectKoumoku.Items.Add("監視開始日時");
            m_selectKoumoku.Items.Add("監視終了日時");
            m_selectKoumoku.Items.Add("保守管理番号");
            m_selectKoumoku.Items.Add("保守情報");
            m_selectKoumoku.Items.Add("備考");
            m_selectKoumoku.Items.Add("更新日時");
            m_selectKoumoku.Items.Add("更新者");

            gethost(hostdt);

        }
        //ホスト情報を表示する
        private void gethost(hostDS hostdt)
        {
            this.m_hostno.Text = hostdt.host_no;
            this.m_userno.Text = hostdt.userno;
            this.m_systemno.Text = hostdt.systemno;
            this.m_siteno.Text = hostdt.siteno;
            this.m_hostname.Text = hostdt.hostname;
            this.m_hostjpn.Text = hostdt.hostname_ja;
            this.m_statusCombo.Text = hostdt.status;
            this.m_kisyu.Text = hostdt.device;

            this.m_locate.Text = hostdt.location;
            this.m_usefor.Text = hostdt.usefor;
            this.m_start_date.Text = hostdt.kansiStartdate;
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
                        //ホスト名日本
                        case 2:
                            param_dict["hostname_ja"] = m_selecttext.Text;
                            break;
                        //カスタマ通番
                        case 3:
                            param_dict["userno"] = m_selecttext.Text;
                            break;
                        case 4:
                            param_dict["systemno"] = m_selecttext.Text;
                            break;
                        case 5:
                            param_dict["siteno"] = m_selecttext.Text;
                            break;

                        case 6:
                            if (m_selecttext.Text == "有効")
                                param_dict["status"] = "1";
                            else if (m_selecttext.Text == "無効")
                                param_dict["status"] = "0";
                            break;


                        case 7:
                            param_dict["device"] = m_selecttext.Text;
                            break;

                        case 8:
                            param_dict["location"] = m_selecttext.Text;
                            break;

                        case 9:
                            param_dict["usefor"] = m_selecttext.Text;
                            break;

                        case 10:
                            param_dict["kansiStartdate"] = m_selecttext.Text;
                            break;

                        case 11:
                            param_dict["kansiEndsdate"] = m_selecttext.Text;
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
                            param_dict["chk_date"] = m_selecttext.Text;
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

            //ホスト一覧を取得する
            dset = dg.getSelectHost(param_dict, con, dset);

            this.m_host_List.FullRowSelect = true;
            this.m_host_List.HideSelection = false;
            this.m_host_List.HeaderStyle = ColumnHeaderStyle.Clickable;

            this.m_host_List.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(1, "ホスト名(英数)", 120, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(2, "ホスト名(日本語)", 120, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(3, "ステータス", 90, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(4, "機種", 80, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(5, "設置場所", 50, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(6, "用途", 50, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(7, "監視開始番号", 120, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(8, "監視終了番号", 120, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(9, "保守管理番号", 50, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(10, "保守情報", 50, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(11, "備考", 50, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(12, "カスタマ番号", 50, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(13, "システム通番番号", 50, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(14, "拠点通番", 50, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(15, "更新日時", 50, HorizontalAlignment.Left);
            this.m_host_List.Columns.Insert(16, "更新者", 50, HorizontalAlignment.Left);

            //リストに表示
            if (dset.host_L != null)
            {
                foreach (hostDS s_ds in dset.host_L)
                {

                    ListViewItem itemx1 = new ListViewItem();
                    itemx1.Text = s_ds.host_no;

                    itemx1.SubItems.Add(s_ds.hostname);
                    itemx1.SubItems.Add(s_ds.hostname_ja);
                    itemx1.SubItems.Add(s_ds.status);
                    itemx1.SubItems.Add(s_ds.device);
                    itemx1.SubItems.Add(s_ds.location);
                    itemx1.SubItems.Add(s_ds.usefor);
                    itemx1.SubItems.Add(s_ds.kansiStartdate);
                    itemx1.SubItems.Add(s_ds.kansiEndsdate);
                    itemx1.SubItems.Add(s_ds.hosyukanri);
                    itemx1.SubItems.Add(s_ds.hosyuinfo);
                    itemx1.SubItems.Add(s_ds.biko);
                    itemx1.SubItems.Add(s_ds.userno);
                    itemx1.SubItems.Add(s_ds.systemno);
                    itemx1.SubItems.Add(s_ds.siteno);
                    itemx1.SubItems.Add(s_ds.chk_date);
                    itemx1.SubItems.Add(s_ds.chk_name_id);

                    this.m_host_List.Items.Add(itemx1);
                }
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
            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "update host set hostname=:hostname,hostname_ja=:hostname_ja,status=:status,device=:device,location=:location,usefor=:usefor," +
                "kansiStartdate=:kansiStartdate,kansiEndsdate=:kansiEndsdate,hosyukanri=:hosyukanri,hosyuinfo=:hosyuinfo,biko=:biko," +
                "chk_name_id =:ope,chk_date=:chdate where host_no = :no";
            using (var transaction = con.BeginTransaction())
            {
                var command = new NpgsqlCommand(@sql, con);
                command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = m_hostno.Text });
                command.Parameters.Add(new NpgsqlParameter("hostname", DbType.String) { Value = m_hostname.Text });
                command.Parameters.Add(new NpgsqlParameter("hostname_ja", DbType.String) { Value = m_hostjpn.Text });
                command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                command.Parameters.Add(new NpgsqlParameter("device", DbType.String) { Value = m_kisyu.Text });
                command.Parameters.Add(new NpgsqlParameter("location", DbType.String) { Value = m_locate.Text });
                command.Parameters.Add(new NpgsqlParameter("usefor", DbType.String) { Value = m_usefor.Text });
                command.Parameters.Add(new NpgsqlParameter("kansiStartdate", DbType.DateTime) { Value = m_start_date.Value });
                command.Parameters.Add(new NpgsqlParameter("kansiEndsdate", DbType.DateTime) { Value = m_end_date.Value });
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
                    transaction.Commit();

                    if (rowsaffected != 1)
                        MessageBox.Show("更新できませんでした。", "ホスト更新");
                    else
                        MessageBox.Show("更新されました。", "ホスト更新");
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

        //ダブルリック
        private void m_host_List_DoubleClick(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection item = m_host_List.SelectedIndices;
            hostDS hostdt = new hostDS();
            hostdt.host_no = this.m_host_List.Items[item[0]].SubItems[0].Text;
            hostdt.hostname = this.m_host_List.Items[item[0]].SubItems[1].Text;
            hostdt.hostname_ja = this.m_host_List.Items[item[0]].SubItems[2].Text;
            hostdt.status = this.m_host_List.Items[item[0]].SubItems[3].Text;
            hostdt.device = this.m_host_List.Items[item[0]].SubItems[4].Text;
            hostdt.location = this.m_host_List.Items[item[0]].SubItems[5].Text;
            hostdt.usefor = this.m_host_List.Items[item[0]].SubItems[6].Text;
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
            if (e.Column == _columnSorter.SortColumn)
            {
                if (_columnSorter.Order == SortOrder.Ascending)
                {
                    _columnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    _columnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                _columnSorter.SortColumn = e.Column;
                _columnSorter.Order = SortOrder.Ascending;
            }
            m_host_List.Sort();
        }
        //削除ボタン
        private void m_deleteBtn_Click(object sender, EventArgs e)
        {
            int count = m_host_List.SelectedItems.Count;


            if (MessageBox.Show("一覧に選択された行 "+ count + "件 の削除を行います。その際参照している他のテーブルデータも削除されます。"+ Environment.NewLine +
                "よろしいですか？", "ホスト名削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int ret = deleteHost();
            if(ret == -1)
            {
                return;
            }
            //リストの表示上からけす
            foreach (ListViewItem item in m_host_List.SelectedItems)
            {
                m_host_List.Items.Remove(item);
            }
           
        }
        //削除
        private int deleteHost()
        {

            string hostno;

            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "WITH DELETED AS (DELETE FROM host where host_no = :no " +
                "RETURNING host_no) " +
                "DELETE FROM watch_Interface WHERE host_no IN (SELECT host_no FROM DELETED)";

            using (var transaction = con.BeginTransaction())
            {

                foreach (ListViewItem item in m_host_List.SelectedItems)
                {
                    hostno = item.SubItems[0].Text;


                    var command = new NpgsqlCommand(@sql, con);
                    command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = int.Parse(hostno)});

                    Int32 rowsaffected;
                    try
                    {
                        //削除処理
                        rowsaffected = command.ExecuteNonQuery();
                        transaction.Commit();

                        if (rowsaffected != 1)
                            MessageBox.Show("削除できませんでした。ホストID:" + hostno, "ホスト削除");
                        else
                            MessageBox.Show("削除完了しました。ホストID:" + hostno, "ホスト削除");
                    }
                    catch (Exception ex)
                    {
                        //エラー時メッセージ表示
                        MessageBox.Show(ex.Message);
                        transaction.Rollback();
                        return -1;
                    }
                }

            }
            return 1;
        }

        private void m_statusCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
