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

namespace moss_AP
{
    public partial class Form_SiteDetail : Form
    {
        //変更前ステータス
        private string orgStatus;

        //ログイン情報
        public opeDS loginDS { get; set; }

        //ログ
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //拠点データ
        public siteDS sitedt { get; set; }
        
        //ユーザ情報一覧
        public List<userDS> userList { get; set; }

        //システム情報一覧
        public List<systemDS> systemList{ get; set; }

        //DBコネクション
        public NpgsqlConnection con { get; set; }

        //ListViewのソートの際に使用する
        private int sort_kind = 0;

        //拠点情報の一覧
        DataTable site_list;

        public Form_SiteDetail()
        {
            InitializeComponent();
        }
        
        //表示前処理
        //取得したデータを読み取り表示する
        private void Form_SystemDetail_Load(object sender, EventArgs e)
        {
            this.splitContainer1.SplitterDistance = 32;

            m_selectKoumoku.Items.Add("拠点通番");
            m_selectKoumoku.Items.Add("拠点名");
            m_selectKoumoku.Items.Add("郵便番号");
            m_selectKoumoku.Items.Add("住所");
            m_selectKoumoku.Items.Add("TEL/FAX");
            m_selectKoumoku.Items.Add("ステータス");
            m_selectKoumoku.Items.Add("備考");
            m_selectKoumoku.Items.Add("カスタマ通番");
            m_selectKoumoku.Items.Add("システム通番");
            m_selectKoumoku.Items.Add("更新日時");
            m_selectKoumoku.Items.Add("更新者");

            if(sitedt != null)
                getsite(sitedt);

        }
        //拠点一覧を取得する
        private void getsite(siteDS sitedt)
        {
            this.m_siteno.Text = sitedt.siteno;
            this.m_userno.Text = sitedt.userno;
            this.m_systemno.Text = sitedt.systemno;
            this.m_systemname.Text = sitedt.systemname;
            this.m_sitename.Text = sitedt.sitename;
            this.m_address1.Text = sitedt.address1;
            this.m_address2.Text = sitedt.address2;
            this.m_tel.Text = sitedt.telno;
            this.m_statusCombo.Text = sitedt.status;
            orgStatus = sitedt.status;
            this.m_biko.Text = sitedt.biko;
            this.m_update.Text = sitedt.chk_date;
            this.m_updateOpe.Text = sitedt.chk_name_id;

            Class_Detaget dg = new Class_Detaget();
            dg.con = con;
            if (sitedt.userno != "") {
                this.m_cutomername.Text = dg.getCustomername(sitedt.userno);
            }
            if (sitedt.systemno != "") {

                this.m_systemname.Text = dg.getSystemname(sitedt.systemno);

            }
        }


        //検索ボタン
        private void m_selectBtn_Click(object sender, EventArgs e)
        {
            m_Site_List.Clear();
            DISP_dataSet dset = new DISP_dataSet();
            Dictionary<string, string> param_dict = new Dictionary<string, string>();
            Class_Detaget dg = new Class_Detaget();

            if (m_selecttext.Text != "")
            {

                if (this.m_selectKoumoku.SelectedIndex.ToString() != "")
                {
                    switch (this.m_selectKoumoku.SelectedIndex)
                    {

                        //拠点通番
                        case 0:
                            param_dict["siteno"] = m_selecttext.Text;
                            break;
                        //拠点名
                        case 1:
                            param_dict["sitename"] = m_selecttext.Text;
                            break;
                        //郵便番号
                        case 2:
                            param_dict["address1"] = m_selecttext.Text;
                            break;

                        //住所
                        case 3:
                            param_dict["address2"] = m_selecttext.Text;
                            break;

                        //TEL/FAX
                        case 4:
                            param_dict["telno"] = m_selecttext.Text;
                            break;
                        //ステータス
                        case 5:
                            if (m_selecttext.Text == "無効")
                                param_dict["status"] = "0";
                            else if (m_selecttext.Text == "有効")
                                param_dict["status"] = "1";

                            break;
                        //備考
                        case 6:
                            param_dict["biko"] = m_selecttext.Text;
                            break;
                        //カスタマ通番
                        case 7:
                            param_dict["userno"] = m_selecttext.Text;
                            break;

                        //システム通番
                        case 8:
                            param_dict["systemno"] = m_selecttext.Text;
                            break;

                        //更新日時
                        case 9:

                            DateTime dt;
                            String str = m_selecttext.Text;

                            //入力された日付の形式の確認
                            if (DateTime.TryParse(str, out dt))
                            {
                                param_dict["chk_date"] = str;
                            }
                            else {
                                MessageBox.Show("日付の形式が正しくありません。","拠点検索");
                                return;
                            }
                            break;
                        //更新者
                        case 10:
                            param_dict["chk_name_id"] = m_selecttext.Text;
                            break;

                        default:
                            break;
                    }
                }
            }

            //まず件数を取得する
            Int64 count = dg.getSelectSiteCount(param_dict, con, dset, true);
            if (MessageBox.Show(count.ToString() + "件ヒットしました。表示しますか？", "拠点", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            //拠点一覧を取得する
            dset = dg.getSelectSite(param_dict, con, dset,true);

            this.splitContainer1.SplitterDistance = 227;
            this.m_Site_List.VirtualMode = true;
            // １行全体選択
            this.m_Site_List.FullRowSelect = true;
            this.m_Site_List.HideSelection = false;
            this.m_Site_List.HeaderStyle = ColumnHeaderStyle.Clickable;
            //Hook up handlers for VirtualMode events.
            this.m_Site_List.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(Site_RetrieveVirtualItem);
            this.m_Site_List.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.m_Site_List.Scrollable = true;


            this.m_Site_List.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_Site_List.Columns.Insert(1, "拠点名", 120, HorizontalAlignment.Left);
            this.m_Site_List.Columns.Insert(2, "郵便番号", 120, HorizontalAlignment.Left);
            this.m_Site_List.Columns.Insert(3, "住所", 90, HorizontalAlignment.Left);
            this.m_Site_List.Columns.Insert(4, "TEL/FAX", 80, HorizontalAlignment.Left);
            this.m_Site_List.Columns.Insert(5, "ステータス", 50, HorizontalAlignment.Left);
            this.m_Site_List.Columns.Insert(6, "カスタマ番号", 50, HorizontalAlignment.Left);
            this.m_Site_List.Columns.Insert(7, "カスタマ名", 120, HorizontalAlignment.Left);
            this.m_Site_List.Columns.Insert(8, "システム番号", 120, HorizontalAlignment.Left);
            this.m_Site_List.Columns.Insert(9, "システム名", 120, HorizontalAlignment.Left);
            this.m_Site_List.Columns.Insert(10, "更新日時", 50, HorizontalAlignment.Left);
            this.m_Site_List.Columns.Insert(11, "更新者", 50, HorizontalAlignment.Left);

            //リストビューを初期化する
            site_list = new DataTable("table1");
            site_list.Columns.Add("No", Type.GetType("System.Int32"));
            site_list.Columns.Add("拠点名", Type.GetType("System.String"));
            site_list.Columns.Add("郵便番号", Type.GetType("System.String"));
            site_list.Columns.Add("住所", Type.GetType("System.String"));
            site_list.Columns.Add("TEL/FAX", Type.GetType("System.String"));
            site_list.Columns.Add("ステータス", Type.GetType("System.String"));
            site_list.Columns.Add("カスタマ番号", Type.GetType("System.String"));
            site_list.Columns.Add("カスタマ名", Type.GetType("System.String"));
            site_list.Columns.Add("システム番号", Type.GetType("System.String"));
            site_list.Columns.Add("システム名", Type.GetType("System.String"));
            site_list.Columns.Add("更新日時", Type.GetType("System.String"));
            site_list.Columns.Add("更新者", Type.GetType("System.String"));

            //リストに表示
            if (dset.site_L != null)
            {
                m_Site_List.BeginUpdate();

                foreach (siteDS s_ds in dset.site_L) {

                    DataRow urow = site_list.NewRow();

                    urow["No"] = s_ds.siteno;
                    urow["拠点名"] = s_ds.sitename;
                    urow["郵便番号"] = s_ds.address1;
                    urow["住所"] = s_ds.address2;
                    urow["TEL/FAX"] = s_ds.telno;
                    urow["ステータス"] = s_ds.status;
                    urow["カスタマ番号"] = s_ds.userno;
                    urow["カスタマ名"] = s_ds.username;
                    urow["システム番号"] = s_ds.systemno;
                    urow["システム名"] = s_ds.systemname;
                    urow["更新日時"] = s_ds.chk_date;
                    urow["更新者"] = s_ds.chk_name_id;
                    site_list.Rows.Add(urow);
                }
                this.m_Site_List.VirtualListSize = site_list.Rows.Count;
                this.m_Site_List.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                m_Site_List.EndUpdate();
            }
        }
        void Site_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            //	e.Item = _item[e.ItemIndex];
            if (site_list.Rows.Count > 0)
            {

                DataRow row = this.site_list.Rows[e.ItemIndex];
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
        //戻るボタン
        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        //更新ボタン
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (m_sitename.Text == "")
            {
                MessageBox.Show("拠点名を入力して下さい。", "拠点修正", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            //確認ダイアログ
            if (MessageBox.Show("拠点データの更新を行います。よろしいですか？", "拠点データ更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string status = "";
            if (m_statusCombo.Text == "有効")
                status = "1";
            else if (m_statusCombo.Text == "無効" )
                status = "0";

            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "update site set sitename=:name,address1=:ad1,address2=:ad2,telno=:tel,status=:status,biko=:biko,chk_name_id =:ope,chk_date=:chdate where siteno = :no";
            using (var transaction = con.BeginTransaction())
            {
                var command = new NpgsqlCommand(@sql, con);
                command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = m_siteno.Text });
                command.Parameters.Add(new NpgsqlParameter("name", DbType.String) { Value = m_sitename.Text });
                command.Parameters.Add(new NpgsqlParameter("ad1", DbType.String) { Value = m_address1.Text });
                command.Parameters.Add(new NpgsqlParameter("ad2", DbType.String) { Value = m_address2.Text });
                command.Parameters.Add(new NpgsqlParameter("tel", DbType.String) { Value = m_tel.Text });
                command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                command.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = m_biko.Text });
                command.Parameters.Add(new NpgsqlParameter("ope", DbType.String) { Value = loginDS.opeid });
                command.Parameters.Add(new NpgsqlParameter("chdate", DbType.DateTime) { Value = DateTime.Now });
                Int32 rowsaffected;
                try
                {
                    //更新処理
                    rowsaffected = command.ExecuteNonQuery();
                    

                    if (rowsaffected != 1) { 
                        MessageBox.Show("更新できませんでした。", "拠点更新");
                        transaction.Rollback();
                    }
                    else {

                        //ステータスが変わっている場合は下位伝播する
                        if (orgStatus != m_statusCombo.Text.Trim())
                        {
                            //下位伝播
                            int ret = statusCascade(m_siteno.Text, status);
                            if (ret == -1)
                            {
                                MessageBox.Show("下位伝播時にエラーが発生しました。ログを確認してください。", "拠点更新");
                                transaction.Rollback();
                                return;
                            }

                        }
                        transaction.Commit();
                        MessageBox.Show("更新されました。", "カスタマ更新");

                    }
                }
                catch (Exception ex)
                {
                    //エラー時メッセージ表示
                    if (transaction.Connection != null) transaction.Rollback();
                    MessageBox.Show(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logger.ErrorFormat("拠点更新エラー メソッド名：{0}。MSG：{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);


                    return;
                }
            }
        }
        //下位伝播ステータスのみ
        private int statusCascade(String siteno, String status)
        {
            int ret = 0;

            //ホスト
            ret = host_update(siteno, status);
            if (ret == -1)
                return ret;
            //監視インターフェイス
            ret = interface_update(siteno, status);
            if (ret == -1)
                return ret;
            //回線情報
            ret = kasen_update(siteno, status);
            if (ret == -1)
                return ret;


            return ret;
        }

        //ホストのステータス更新
        private int host_update(String siteno, String status)
        {
            int ret = 0;

            string sql = "update host set status=:status,chk_name_id =:chk_name_id,chk_date=:chk_date " +
                "WHERE siteno=:siteno";

            var command = new NpgsqlCommand(@sql, con);
            command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
            command.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });
            command.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = DateTime.Now });
            command.Parameters.Add(new NpgsqlParameter("siteno", DbType.Int32) { Value = siteno });
            Int32 rowsaffected;
            try
            {
                //更新処理
                rowsaffected = command.ExecuteNonQuery();


                if (rowsaffected < 1)
                {
                    logger.Warn("ホストのステータスを更新できませんでした。配下のホスト数が0件のときはこのメッセージが出ることがあります。" + " 拠点名:" + m_sitename.Text);
                    ret = 0;
                }
                else
                {
                    logger.Info("ホストのステータスを更新しました。" + " 拠点:" + m_sitename.Text);
                    ret = 1;
                }
            }
            catch (Exception ex)
            {
                //エラー時メッセージ表示
                MessageBox.Show("ホストステータス更新エラー " + ex.Message);
                return -1;
            }

            return ret;
        }
        //監視インターフェイスステータス更新
        private int interface_update(String siteno, String status)
        {
            int ret = 0;

            string sql = "update watch_Interface set status=:status,chk_name_id =:chk_name_id,chk_date=:chk_date " +
                "WHERE siteno=:siteno";

            var command = new NpgsqlCommand(@sql, con);
            command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
            command.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });
            command.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = DateTime.Now });
            command.Parameters.Add(new NpgsqlParameter("siteno", DbType.Int32) { Value = siteno });
            Int32 rowsaffected;
            try
            {
                //更新処理
                rowsaffected = command.ExecuteNonQuery();


                if (rowsaffected < 1)
                {
                    logger.Warn("監視インターフェイスのステータスを更新できませんでした。配下の監視インターフェイス数が0件のときはこのメッセージが出ることがあります。" + " 拠点:" + m_sitename.Text);
                    ret = 0;
                }
                else
                {
                    logger.Info("監視インターフェイスのステータスを更新しました。" + " 拠点:" + m_sitename.Text);
                    ret = 1;
                }
            }
            catch (Exception ex)
            {
                //エラー時メッセージ表示
                MessageBox.Show("監視インターフェイスステータス更新エラー " + ex.Message + " " + " 拠点: " + m_sitename.Text);
                return -1;
            }

            return ret;
        }

        //回線情報
        private int kasen_update(String siteno, String status)
        {
            int ret = 0;

            string sql = "update Kaisen set status=:status,chk_name_id =:chk_name_id,chk_date=:chk_date " +
                "WHERE siteno=:siteno";

            var command = new NpgsqlCommand(@sql, con);
            command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
            command.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });
            command.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = DateTime.Now });
            command.Parameters.Add(new NpgsqlParameter("siteno", DbType.Int32) { Value = siteno });
            Int32 rowsaffected;
            try
            {
                //更新処理
                rowsaffected = command.ExecuteNonQuery();


                if (rowsaffected < 1)
                {
                    logger.Warn("回線情報のステータスを更新できませんでした。配下の回線情報が0件のときはこのメッセージが出ることがあります。" + " 拠点:" + m_sitename.Text);
                    ret = 0;
                }
                else
                {
                    logger.Info("回線情報のステータスを更新しました。" + " 拠点:" + m_sitename.Text);
                    ret = 1;
                }
            }
            catch (Exception ex)
            {
                //エラー時メッセージ表示
                MessageBox.Show("回線情報更新エラー " + " 拠点:" + m_sitename.Text + " " + ex.Message );
                return -1;
            }

            return ret;
        }
        //ダブルクリックのとき
        private void m_Site_List_DoubleClick(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection item = m_Site_List.SelectedIndices;
            siteDS sitedt = new siteDS();

            sitedt.siteno = this.m_Site_List.Items[item[0]].SubItems[0].Text;
            sitedt.sitename = this.m_Site_List.Items[item[0]].SubItems[1].Text;
            sitedt.address1 = this.m_Site_List.Items[item[0]].SubItems[2].Text;
            sitedt.address2 = this.m_Site_List.Items[item[0]].SubItems[3].Text;
            sitedt.telno = this.m_Site_List.Items[item[0]].SubItems[4].Text;
            if (this.m_Site_List.Items[item[0]].SubItems[5].Text == "無効")
                sitedt.status = "0";
            else if (this.m_Site_List.Items[item[0]].SubItems[5].Text == "有効")
                sitedt.status = "1";

            sitedt.userno = this.m_Site_List.Items[item[0]].SubItems[6].Text;
            sitedt.username = this.m_Site_List.Items[item[0]].SubItems[7].Text;
            sitedt.systemno = this.m_Site_List.Items[item[0]].SubItems[8].Text;
            sitedt.systemname = this.m_Site_List.Items[item[0]].SubItems[9].Text;
            sitedt.chk_date = this.m_Site_List.Items[item[0]].SubItems[10].Text;
            sitedt.chk_name_id = this.m_Site_List.Items[item[0]].SubItems[11].Text;

            getsite(sitedt);
        }

        private void m_Site_List_ColumnClick(object sender, ColumnClickEventArgs e)
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
            if (this.m_Site_List.TopItem != null)
            {
                //現在一番上の行に表示されている行を取得
                int start = m_Site_List.TopItem.Index;
                // ListView画面の再表示を行う
                m_Site_List.RedrawItems(start, m_Site_List.Items.Count - 1, true);
            }

        }
        //削除ボタンがクリックされたとき
        private void m_deleteBtn_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection item = m_Site_List.SelectedIndices;
            int count = item.Count;

            //確認メッセージ
            if (MessageBox.Show("一覧に選択された行 " + count + "件 の削除を行います。その際参照している他のテーブルデータも削除されます。" + Environment.NewLine +
                "よろしいですか？", "拠点削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int ret = deletesite(item);
            if (ret == -1)
            {
                return;
            }

            //リストの表示上からけす
            int i =0;
            //削除する拠点番号の取得
            int[] indices = new int[item.Count];
            int cnt = m_Site_List.SelectedIndices.Count;

            m_Site_List.SelectedIndices.CopyTo(indices, 0);

            DataRowCollection items = site_list.Rows;
            for (i = cnt - 1; i >= 0; --i)
                items.RemoveAt(indices[i]);

            //総件数を変更し再表示を行う
            this.m_Site_List.VirtualListSize = items.Count;

        }
        //削除
        private int deletesite(ListView.SelectedIndexCollection item)
        {

            string siteno;

            int ret = 0;
            if (con.FullState != ConnectionState.Open) con.Open();

            String sql = "WITH DELETED1 AS (DELETE FROM watch_interface where siteno = :no " +
                "RETURNING siteno), "+
                "DELETED2 AS (DELETE FROM host where siteno = :no " +
                "RETURNING siteno) " +
                "DELETE FROM site where siteno = :no ";



            using (var transaction = con.BeginTransaction())
            {

                int i = 0;
                for (i = 0; i < item.Count; i++)
                {
                    siteno = this.m_Site_List.Items[item[i]].SubItems[0].Text;

                    var command = new NpgsqlCommand(@sql, con);
                    command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = int.Parse(siteno) });

                    Int32 rowsaffected;
                    try
                    {
                        //削除処理
                        rowsaffected = command.ExecuteNonQuery();

                        if (rowsaffected < 1)
                        {
                            
                            transaction.Rollback();
                            MessageBox.Show("削除できませんでした。拠点通番:" + siteno, "拠点情報削除");

                            ret = 1;
                        }
                        else {
                            ret = 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (transaction.Connection != null) transaction.Rollback();
                        //エラー時メッセージ表示
                        MessageBox.Show("拠点情報削除時エラーが発生しました。 " + ex.Message);
                        return -1;
                    }
                }
                if(ret == 1)
                {
                    transaction.Commit();
                    MessageBox.Show("削除完了しました。", "拠点情報削除");
                    logger.InfoFormat("削除完了。 拠点情報削除");

                }

            }
            return ret;
        }
    }
}
