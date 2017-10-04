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
    public partial class Form_SystemDetail : Form
    {
        //ログイン情報
        public opeDS loginDS { get; set; }

        //変更前ステータス
        private string orgStatus;

        //システム
        public systemDS systemdt { get; set; }

        //システム情報一覧
        public List<systemDS> systemList{ get; set; }

        //DBコネクション
        public NpgsqlConnection con { get; set; }

        //ログ
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



        //ListViewのソートの際に使用する
        private Class_ListViewColumnSorter _columnSorter;


        public Form_SystemDetail()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        
        //表示前処理
        //取得したデータを読み取り表示する
        private void Form_SystemDetail_Load(object sender, EventArgs e)
        {
            _columnSorter = new Class_ListViewColumnSorter();
            m_System_List.ListViewItemSorter = _columnSorter;
            this.splitContainer1.SplitterDistance = 32;

            m_selectKoumoku.Items.Add("システム通番");
            m_selectKoumoku.Items.Add("システム名");
            m_selectKoumoku.Items.Add("システム名カナ");
            m_selectKoumoku.Items.Add("ステータス");
            m_selectKoumoku.Items.Add("備考");
            m_selectKoumoku.Items.Add("カスタマ通番");
            m_selectKoumoku.Items.Add("更新日時");
            m_selectKoumoku.Items.Add("更新者");
            if(systemdt != null)
                getsystem(systemdt);
        }
        //システム一覧を取得する
        private void getsystem(systemDS systemdt)
        {

            this.m_systemno.Text = systemdt.systemno;
            this.m_userno.Text = systemdt.userno;
            this.m_cutomername.Text = systemdt.username;
            this.m_systemname.Text = systemdt.systemname;
            this.m_systemname_kana.Text = systemdt.systemkana;
            this.m_statusCombo.Text = systemdt.status;
            this.m_biko.Text = systemdt.biko;
            this.m_statusCombo.Text = systemdt.status;
            this.m_update.Text = systemdt.chk_date;
            this.m_updateOpe.Text = systemdt.chk_name_id;

        }
        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        //検索ボタン
        private void m_selectBtn_Click(object sender, EventArgs e)
        {
            m_System_List.Clear();
            DISP_dataSet dset = new DISP_dataSet();
            Dictionary<string, string> param_dict = new Dictionary<string, string>();
            Class_Detaget dg = new Class_Detaget();

            if (m_selecttext.Text != "")
            {

                if (this.m_selectKoumoku.SelectedIndex.ToString() != "")
                {
                    switch (this.m_selectKoumoku.SelectedIndex)
                    {
                        //システム通番
                        case 0:
                            param_dict["systemno"] = m_selecttext.Text;
                            break;
                        //システム名
                        case 1:
                            param_dict["systemname"] = m_selecttext.Text;
                            break;
                        //システム名カナ
                        case 2:
                            param_dict["systemkana"] = m_selecttext.Text;
                            break;

                        //ステータス
                        case 3:
                            param_dict["status"] = m_selecttext.Text;
                            break;

                        //備考
                        case 4:
                            param_dict["biko"] = m_selecttext.Text;
                            break;

                        //カスタマ通番
                        case 5:
                            param_dict["userno"] = m_selecttext.Text;
                            break;

                        //更新日時
                        case 6:
                            DateTime dt;
                            String str = m_selecttext.Text;

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
                        case 7:
                            param_dict["chk_name_id"] = m_selecttext.Text;
                            break;

                        default:
                            break;

                    }
                }
            }

            //システム一覧を取得する
            dset = dg.getSelectSystem(param_dict, con, dset,true);

            this.splitContainer1.SplitterDistance = 180;

            this.m_System_List.FullRowSelect = true;
            this.m_System_List.HideSelection = false;
            this.m_System_List.HeaderStyle = ColumnHeaderStyle.Clickable;

            this.m_System_List.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_System_List.Columns.Insert(1, "システム名", 200, HorizontalAlignment.Left);
            this.m_System_List.Columns.Insert(2, "システム名カナ", 200, HorizontalAlignment.Left);
            this.m_System_List.Columns.Insert(3, "カスタマ通番", 90, HorizontalAlignment.Left);
            this.m_System_List.Columns.Insert(4, "カスタマ名", 200, HorizontalAlignment.Left);
            this.m_System_List.Columns.Insert(5, "ステータス", 50, HorizontalAlignment.Left);
            this.m_System_List.Columns.Insert(6, "備考", 200, HorizontalAlignment.Left);
            this.m_System_List.Columns.Insert(7, "更新日時", 120, HorizontalAlignment.Left);
            this.m_System_List.Columns.Insert(8, "更新者", 50, HorizontalAlignment.Left);

            //リストに表示
            if(dset.system_L != null)
            { 
                foreach (systemDS s_ds in dset.system_L) { 
            
                    ListViewItem itemx1 = new ListViewItem();
                    itemx1.Text = s_ds.systemno;

                    itemx1.SubItems.Add(s_ds.systemname);
                    itemx1.SubItems.Add(s_ds.systemkana);
                    itemx1.SubItems.Add(s_ds.userno);
                    itemx1.SubItems.Add(s_ds.username);
                    itemx1.SubItems.Add(s_ds.status);
                    itemx1.SubItems.Add(s_ds.biko);
                    itemx1.SubItems.Add(s_ds.chk_date);
                    itemx1.SubItems.Add(s_ds.chk_name_id);

                    this.m_System_List.Items.Add(itemx1);
                }
            }
        }

        //システム一覧がダブルクリックされたとき
        private void m_Ststem_List_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView.SelectedIndexCollection item = m_System_List.SelectedIndices;
            systemDS systemdt = new systemDS();
            systemdt.systemno = this.m_System_List.Items[item[0]].SubItems[0].Text;
            systemdt.systemname = this.m_System_List.Items[item[0]].SubItems[1].Text;
            systemdt.systemkana = this.m_System_List.Items[item[0]].SubItems[2].Text;
            systemdt.userno = this.m_System_List.Items[item[0]].SubItems[3].Text;
            systemdt.username = this.m_System_List.Items[item[0]].SubItems[4].Text;
            systemdt.status = this.m_System_List.Items[item[0]].SubItems[5].Text;
            systemdt.biko =           this.m_System_List.Items[item[0]].SubItems[6].Text;
            systemdt.chk_date =       this.m_System_List.Items[item[0]].SubItems[7].Text;
            systemdt.chk_name_id =    this.m_System_List.Items[item[0]].SubItems[8].Text;

            getsystem(systemdt);
        }
        //戻るボタン
        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        //更新ボタン
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (m_systemname.Text == "")
            {
                MessageBox.Show("システム名を入力して下さい。", "システム修正", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            //確認ダイアログ
            if (MessageBox.Show("システムデータの更新を行います。よろしいですか？", "システムデータ更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //ステータス
            string status = "";
            if (m_statusCombo.Text == "有効")
                status = "1";
            else if (m_statusCombo.Text == "無効")
                status = "0";

            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "update system set systemname =:name,systemkana =:kana,status=:status,biko=:biko,chk_name_id =:ope,chk_date=:chdate where systemno = :no";
            using (var transaction = con.BeginTransaction())
            {
                var command = new NpgsqlCommand(@sql, con);
                command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = m_systemno.Text });
                command.Parameters.Add(new NpgsqlParameter("name", DbType.String) { Value = m_systemname.Text });
                command.Parameters.Add(new NpgsqlParameter("kana", DbType.String) { Value = m_systemname_kana.Text });
                command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
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
                        MessageBox.Show("更新できませんでした。", "システム更新");
                        transaction.Rollback();
                    }
                    else
                    {
                        //ステータスが変わっている場合は下位伝播する
                        if (orgStatus != m_statusCombo.Text.Trim())
                        {
                            //下位伝播
                            int ret = statusCascade(m_systemno.Text, status);
                            if (ret == -1)
                            {
                                MessageBox.Show("下位伝播時にエラーが発生しました。ログを確認してください。", "拠点更新");
                                transaction.Rollback();
                                return;
                            }

                        }
                        transaction.Commit();
                        MessageBox.Show("更新されました。", "システム更新");
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
        //下位伝播ステータスのみ
        private int statusCascade(String systemno, String status)
        {
            int ret = 0;

            //拠点
            ret = site_update(systemno, status);
            if (ret == -1)
                return ret;
            //ホスト
            ret = host_update(systemno, status);
            if (ret == -1)
                return ret;
            //監視インターフェイス
            ret = interface_update(systemno, status);
            if (ret == -1)
                return ret;
            //回線情報
            ret = kasen_update(systemno, status);
            if (ret == -1)
                return ret;


            return ret;
        }

        //拠点のステータス更新
        private int site_update(String systemno, String status)
        {
            int ret = 0;

            string sql = "update site set status=:status,chk_name_id =:chk_name_id,chk_date=:chk_date " +
                "WHERE systemno=:systemno";

            var command = new NpgsqlCommand(@sql, con);
            command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
            command.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });
            command.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = DateTime.Now });
            command.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = systemno });
            Int32 rowsaffected;
            try
            {
                //更新処理
                rowsaffected = command.ExecuteNonQuery();


                if (rowsaffected < 1)
                {
                    logger.Warn("拠点のステータスを更新できませんでした。配下の拠点数が0件のときはこのメッセージが出ることがあります。" + " システム:" + m_systemname.Text);
                    ret = 0;
                }
                else
                {
                    logger.Info("拠点のステータスを更新しました。" + " システム:" + m_systemname.Text);
                    ret = 1;
                }
            }
            catch (Exception ex)
            {
                //エラー時メッセージ表示
                MessageBox.Show("拠点ステータス更新時エラー " + ex.Message);
                return -1;
            }

            return ret;
        }
        //ホストのステータス更新
        private int host_update(String systemno, String status)
        {
            int ret = 0;

            string sql = "update host set status=:status,chk_name_id =:chk_name_id,chk_date=:chk_date " +
                "WHERE systemno=:systemno";

            var command = new NpgsqlCommand(@sql, con);
            command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
            command.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });
            command.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = DateTime.Now });
            command.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = systemno });
            Int32 rowsaffected;
            try
            {
                //更新処理
                rowsaffected = command.ExecuteNonQuery();


                if (rowsaffected < 1)
                {
                    logger.Warn("ホストのステータスを更新できませんでした。配下のホスト数が0件のときはこのメッセージが出ることがあります。" + " システム:" + m_systemno.Text);
                    ret = 0;
                }
                else
                {
                    logger.Info("ホストのステータスを更新しました。" + " システム:" + m_systemname.Text);
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
        private int interface_update(String systemno, String status)
        {
            int ret = 0;

            string sql = "update watch_Interface set status=:status,chk_name_id =:chk_name_id,chk_date=:chk_date " +
                "WHERE systemno=:systemno";

            var command = new NpgsqlCommand(@sql, con);
            command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
            command.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });
            command.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = DateTime.Now });
            command.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = systemno });
            Int32 rowsaffected;
            try
            {
                //更新処理
                rowsaffected = command.ExecuteNonQuery();


                if (rowsaffected < 1)
                {
                    logger.Warn("監視インターフェイスのステータスを更新できませんでした。配下の監視インターフェイス数が0件のときはこのメッセージが出ることがあります。" +" システム:" + m_systemname.Text);
                    ret = 0;
                }
                else
                {
                    logger.Info("監視インターフェイスのステータスを更新しました。" + " システム:" + m_systemname.Text);
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
        private int kasen_update(String systemno, String status)
        {
            int ret = 0;

            string sql = "update Kaisen set status=:status,chk_name_id =:chk_name_id,chk_date=:chk_date " +
                "WHERE systemno=:systemno";

            var command = new NpgsqlCommand(@sql, con);
            command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
            command.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });
            command.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = DateTime.Now });
            command.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = systemno });
            Int32 rowsaffected;
            try
            {
                //更新処理
                rowsaffected = command.ExecuteNonQuery();


                if (rowsaffected < 1)
                {
                    logger.Warn("回線情報のステータスを更新できませんでした。配下の回線情報が0件のときはこのメッセージが出ることがあります。" + " システム:" + m_systemname.Text);
                    ret = 0;
                }
                else
                {
                    logger.Info("回線情報のステータスを更新しました。" + " システム:" + m_systemname.Text);
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

        //カラムクリックからのソート
        private void m_System_List_ColumnClick(object sender, ColumnClickEventArgs e)
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
            m_System_List.Sort();
        }

        //削除ボタン
        private void m_deleteBtn_Click(object sender, EventArgs e)
        {
            int count = m_System_List.SelectedItems.Count;

            //確認メッセージ
            if (MessageBox.Show("一覧に選択された行 " + count + "件 の削除を行います。その際参照している他のテーブルデータも削除されます。" + Environment.NewLine +
                "よろしいですか？", "システム削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)

                return;

            int ret = deletesystem();
            if (ret == -1)
            {
                return;
            }

            //リストの表示上からけす
            foreach (ListViewItem item in m_System_List.SelectedItems)
            {
                m_System_List.Items.Remove(item);
            }
        }
        //削除
        private int deletesystem()
        {

            string systemno;

            if (con.FullState != ConnectionState.Open) con.Open();

            String sql = "WITH DELETED1 AS (DELETE FROM watch_interface WHERE systemno = :no " +
                "RETURNING systemno), " +
                "DELETED2 AS (DELETE FROM host WHERE systemno = :no " +
                "RETURNING systemno), " +
                "DELETED3 AS (DELETE FROM site WHERE systemno = :no " +
                "RETURNING systemno) " +
                "DELETE FROM system WHERE systemno = :no  ";

            using (var transaction = con.BeginTransaction())
            {
                int ret = 0;
                foreach (ListViewItem item in m_System_List.SelectedItems)
                {
                    systemno = item.SubItems[0].Text;


                    var command = new NpgsqlCommand(@sql, con);
                    command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = int.Parse(systemno) });

                    Int32 rowsaffected;
                    try
                    {
                        //削除処理
                        rowsaffected = command.ExecuteNonQuery();

                        if (rowsaffected < 1)
                        {
                            MessageBox.Show("削除できませんでした。システム通番:" + systemno, "システム情報削除");
                            transaction.Rollback();
                            return -1;
                        }
                        else {
                            ret = 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        //エラー時メッセージ表示
                        MessageBox.Show("システム情報削除時エラーが発生しました。 " + ex.Message);
                        if (transaction.Connection != null) transaction.Rollback();
                        return -1;
                    }
                }
                if (ret == 1)
                {
                    MessageBox.Show("削除完了しました。", "システム情報削除");
                    transaction.Commit();

                }
            }
            return 1;
        }
    }
}
