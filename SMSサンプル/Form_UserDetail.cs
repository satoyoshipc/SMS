﻿using Npgsql;
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
    public partial class Form_UserDetail : Form
    {
        //変更前ステータス
        private string orgStatus;

        //ログ
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        //ログイン情報
        public opeDS loginDS { get; set; }
         

        //カスタマ
        public userDS userdt { get; set; }

        //担当者リスト
        public List<tantouDS> slist { get; set; }


        public List<userDS> userList{ get; set; }
        //DBコネクション
        public NpgsqlConnection con { get; set; }

        //ListViewのソートの際に使用する
        private Class_ListViewColumnSorter _columnSorter;

        public Form_UserDetail()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        //戻るボタン
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //更新ボタン
        private void button1_Click(object sender, EventArgs e)
        {
            if(m_username.Text == "")
            {
                MessageBox.Show("カスタマ名を入力して下さい。", "カスタマ修正", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (m_statusCombo.Text == "")

            {
                MessageBox.Show("有効/無効を入力して下さい。", "カスタマ修正", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }
            if (m_reportCombo.Text == "")
            {
                MessageBox.Show("レポート出力有無を入力して下さい。", "カスタマ修正", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }


            //確認ダイアログ
            if (MessageBox.Show("カスタマデータの更新を行います。よろしいですか？", "カスタマデータ更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string status = "";
            if(m_statusCombo.Text == "有効")
                //有効の時
                status = "1";
            else if (m_statusCombo.Text == "無効")
                //無効の時
                status = "0";

            string report_status = "";
            if (m_reportCombo.Text == "有効")
                //有効の時
                report_status = "1";
            else if (m_reportCombo.Text == "無効")
                //無効の時
                report_status = "0";

            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "update user_tbl set customerID =:j,username =:b ,username_kana =:c,username_sum =:d,status =:e,report_status =:f,biko =:g,chk_name_id =:h, chk_date=:i where userno = :a";
            using (var transaction = con.BeginTransaction())
            {
                var command = new NpgsqlCommand(@sql, con);
                command.Parameters.Add(new NpgsqlParameter("a", DbType.Int32) { Value = m_userno.Text });
                command.Parameters.Add(new NpgsqlParameter("b", DbType.String) { Value = m_username.Text });
                command.Parameters.Add(new NpgsqlParameter("c", DbType.String) { Value = m_username_kana.Text });
                command.Parameters.Add(new NpgsqlParameter("d", DbType.String) { Value = m_username_Ryaku.Text });
                command.Parameters.Add(new NpgsqlParameter("e", DbType.String) { Value = status });
                command.Parameters.Add(new NpgsqlParameter("f", DbType.String) { Value = report_status });
                command.Parameters.Add(new NpgsqlParameter("g", DbType.String) { Value = m_biko.Text});
                command.Parameters.Add(new NpgsqlParameter("h", DbType.String) { Value = loginDS.opeid });
                command.Parameters.Add(new NpgsqlParameter("i", DbType.DateTime) { Value = DateTime.Now });
                command.Parameters.Add(new NpgsqlParameter("j", DbType.String) { Value = m_customerID.Text });
                Int32 rowsaffected;
                try
                {
                    //更新処理
                    rowsaffected = command.ExecuteNonQuery();


                    if (rowsaffected != 1) { 
                        MessageBox.Show("更新できませんでした。", "カスタマ更新");
                        transaction.Rollback();
                    }
                    else
                    {
                        //ステータスが変わっている場合は下位伝播する
                        if (orgStatus != m_statusCombo.Text.Trim()) { 
                            //下位伝播
                            int ret = statusCascade(m_userno.Text, status);
                            if(ret == -1)
                            {
                                MessageBox.Show("下位伝播時にエラーが発生しました。ログを確認してください。", "カスタマ更新");
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
                    MessageBox.Show("カスタマ情報更新エラー " + ex.Message);
                    logger.ErrorFormat("カスタマ情報更新エラー メソッド名：{0}。MSG：{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);

                    return;
                }
            }

        }
        //下位伝播ステータスのみ
        private int statusCascade(String userno,String status)
        {
            int ret = 0;
            //カスタマ担当者
            ret = user_tantou_update(userno,status);
            if(ret == -1)
                return ret;
            
            //システム
            ret = system_update(userno, status);
            if (ret == -1)
                return ret;

            //拠点
            ret = site_update(userno, status);
            if (ret == -1)
                return ret;

            //ホスト
            ret = host_update(userno, status);
            if (ret == -1)
                return ret;
            //監視インターフェイス
            ret = interface_update(userno, status);
            if (ret == -1)
                return ret;
            //回線情報
            ret = kasen_update(userno, status);
            if (ret == -1)
                return ret;


            return ret;
        }

        //カスタマ担当者のステータス更新
        private int user_tantou_update(String userno, String status)
        {
            int ret = 0;

            string sql = "update user_tanntou set status=:status,chk_name_id =:chk_name_id,chk_date=:chk_date " +
                "WHERE userno=:userno";

            var command = new NpgsqlCommand(@sql, con);
            command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
            command.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });
            command.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = DateTime.Now });
            command.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = userno });
            Int32 rowsaffected;
            try
            {
                //更新処理
                rowsaffected = command.ExecuteNonQuery();


                if (rowsaffected < 1) {
                    logger.Warn("カスタマ担当者のステータスを更新できませんでした。カスタマ担当者が0人のときはこのメッセージが出ることがあります。" + " カスタマ:" + m_username.Text);
                    ret = 0;
                }
                else {
                    logger.Info("カスタマ担当者のステータスを更新しました。"+ " カスタマ:" + m_username.Text);
                    ret = 1;
                }
            }
            catch (Exception ex)
            {
                //エラー時メッセージ表示
                MessageBox.Show(ex.Message);
                return -1;
            }

            return ret;
        }
        //システムのステータス更新
        private int system_update(String userno, String status)
        {
            int ret = 0;

            string sql = "update system set status=:status,chk_name_id =:chk_name_id,chk_date=:chk_date " +
                "WHERE userno=:userno";

            var command = new NpgsqlCommand(@sql, con);
            command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
            command.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });
            command.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = DateTime.Now });
            command.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = userno });
            Int32 rowsaffected;
            try
            {
                //更新処理
                rowsaffected = command.ExecuteNonQuery();


                if (rowsaffected < 1)
                {
                    logger.Warn("システムのステータスを更新できませんでした。配下の拠点数が0件のときはこのメッセージが出ることがあります。" + " カスタマ:" + m_username.Text);
                    ret = 0;
                }
                else
                {
                    logger.Info("システムのステータスを更新しました。" + " カスタマ:" + m_username.Text);
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
        //拠点のステータス更新
        private int site_update(String userno, String status)
        {
            int ret = 0;

            string sql = "update site set status=:status,chk_name_id =:chk_name_id,chk_date=:chk_date " +
                "WHERE userno=:userno";

            var command = new NpgsqlCommand(@sql, con);
            command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
            command.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });
            command.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = DateTime.Now });
            command.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = userno });
            Int32 rowsaffected;
            try
            {
                //更新処理
                rowsaffected = command.ExecuteNonQuery();


                if (rowsaffected < 1)
                {
                    logger.Warn("拠点のステータスを更新できませんでした。配下の拠点数が0件のときはこのメッセージが出ることがあります。" + " カスタマ:" + m_username.Text);
                    ret = 0;
                }
                else
                {
                    logger.Info("拠点のステータスを更新しました。" + " カスタマ:" + m_username.Text);
                    ret = 1;
                }
            }
            catch (Exception ex)
            {
                //エラー時メッセージ表示
                MessageBox.Show("拠点ステータス更新時エラー "+ ex.Message);
                return -1;
            }

            return ret;
        }
        //ホストのステータス更新
        private int host_update(String userno, String status)
        {
            int ret = 0;

            string sql = "update host set status=:status,chk_name_id =:chk_name_id,chk_date=:chk_date " +
                "WHERE userno=:userno";

            var command = new NpgsqlCommand(@sql, con);
            command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
            command.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });
            command.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = DateTime.Now });
            command.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = userno });
            Int32 rowsaffected;
            try
            {
                //更新処理
                rowsaffected = command.ExecuteNonQuery();


                if (rowsaffected < 1)
                {
                    logger.Warn("ホストのステータスを更新できませんでした。配下のホスト数が0件のときはこのメッセージが出ることがあります。" + " カスタマ:" + m_username.Text);
                    ret = 0;
                }
                else
                {
                    logger.Info("ホストのステータスを更新しました。" + " カスタマ:" + m_username.Text);
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
        private int interface_update(String userno, String status)
        {
            int ret = 0;

            string sql = "update watch_Interface set status=:status,chk_name_id =:chk_name_id,chk_date=:chk_date " +
                "WHERE userno=:userno";

            var command = new NpgsqlCommand(@sql, con);
            command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
            command.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });
            command.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = DateTime.Now });
            command.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = userno });
            Int32 rowsaffected;
            try
            {
                //更新処理
                rowsaffected = command.ExecuteNonQuery();


                if (rowsaffected < 1)
                {
                    logger.Warn("監視インターフェイスのステータスを更新できませんでした。配下の監視インターフェイス数が0件のときはこのメッセージが出ることがあります。" + " カスタマ:" + m_username.Text);
                    ret = 0;
                }
                else
                {
                    logger.Info("監視インターフェイスのステータスを更新しました。" + " カスタマ:" + m_username.Text);
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
        private int kasen_update(String userno, String status)
        {
            int ret = 0;

            string sql = "update Kaisen set status=:status,chk_name_id =:chk_name_id,chk_date=:chk_date " +
                "WHERE userno=:userno";

            var command = new NpgsqlCommand(@sql, con);
            command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
            command.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });
            command.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = DateTime.Now });
            command.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = userno });
            Int32 rowsaffected;
            try
            {
                //更新処理
                rowsaffected = command.ExecuteNonQuery();


                if (rowsaffected < 1)
                {
                    logger.Warn("回線情報のステータスを更新できませんでした。配下の回線情報が0件のときはこのメッセージが出ることがあります。" + " カスタマ:" + m_username.Text);
                    ret = 0;
                }
                else
                {
                    logger.Info("回線情報のステータスを更新しました。" + " カスタマ:" + m_username.Text);
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

        //表示前処理
        //取得したデータを読み取り表示する
        private void Form_UserDetail_Load(object sender, EventArgs e)
        {
            this.splitContainer1.SplitterDistance = 32;

            _columnSorter = new Class_ListViewColumnSorter();
            m_Customer_List.ListViewItemSorter = _columnSorter;

            m_selectKoumoku.Items.Add("カスタマ通番");
            m_selectKoumoku.Items.Add("カスタマID");
            m_selectKoumoku.Items.Add("カスタマ名");
            m_selectKoumoku.Items.Add("カスタマ名カナ");
            m_selectKoumoku.Items.Add("カスタマ名略称");
            m_selectKoumoku.Items.Add("有効/無効");
            m_selectKoumoku.Items.Add("SLO対象");
            m_selectKoumoku.Items.Add("備考");
            m_selectKoumoku.Items.Add("更新日時");
            m_selectKoumoku.Items.Add("更新者");
            if(userdt != null  ) {             
                getcustomer(userdt);
            }
        }


        //カスタマ一覧を取得する
        private void getcustomer(userDS userdt)
        {
            this.m_userno.Text = userdt.userno;
            this.m_customerID.Text = userdt.customerID;
            this.m_username.Text = userdt.username;
            this.m_username_kana.Text = userdt.username_kana;
            this.m_username_Ryaku.Text = userdt.username_sum;
            this.m_statusCombo.Text = userdt.status;

            //元のステータスを保存しておく
            orgStatus = userdt.status;

            this.m_reportCombo.Text = userdt.report_status;
            this.m_biko.Text = userdt.biko;
            this.m_update.Text = userdt.chk_date;
            this.m_updateOpe.Text = userdt.chk_name_id;

            //担当者を取得する
            m_tantouList.Clear();

            Class_Detaget dataget = new Class_Detaget();
            Dictionary<string, string> param_dict = new Dictionary<string, string>();
            param_dict["userno"] = userdt.userno;

            slist = dataget.get_tantouName(param_dict, con);

            //担当者リストを表示
            disp_tantouList();
        }
        //担当者リストを表示
        private void disp_tantouList()
        {



            this.m_tantouList.FullRowSelect = true;
            this.m_tantouList.HideSelection = false;
            this.m_tantouList.HeaderStyle = ColumnHeaderStyle.Clickable;

            this.m_tantouList.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_tantouList.Columns.Insert(1, "担当者名", 120, HorizontalAlignment.Left);
            this.m_tantouList.Columns.Insert(2, "担当者名カナ", 120, HorizontalAlignment.Left);
            this.m_tantouList.Columns.Insert(3, "部署名", 90, HorizontalAlignment.Left);
            this.m_tantouList.Columns.Insert(4, "電話番号1", 80, HorizontalAlignment.Left);
            this.m_tantouList.Columns.Insert(5, "電話番号2", 80, HorizontalAlignment.Left);
            this.m_tantouList.Columns.Insert(6, "役職", 50, HorizontalAlignment.Left);
            this.m_tantouList.Columns.Insert(7, "備考", 50, HorizontalAlignment.Left);
            this.m_tantouList.Columns.Insert(8, "ステータス", 50, HorizontalAlignment.Left);
            this.m_tantouList.Columns.Insert(9, "カスタマ通番", 50, HorizontalAlignment.Left);
            this.m_tantouList.Columns.Insert(10, "更新日時", 80, HorizontalAlignment.Left);
            this.m_tantouList.Columns.Insert(11, "更新者", 80, HorizontalAlignment.Left);

            //リストに表示
            foreach (tantouDS t_ds in slist)
            {
                //チェックボックスがOFFになっている場合は表示しない
                if (this.m_umu_check.Checked == false && t_ds.status == "無効")
                    continue;

                ListViewItem itemx1 = new ListViewItem();
                itemx1.Text = t_ds.user_tantou_no;

                //表示しない項目
                itemx1.Name = t_ds.user_tantou_no;

                itemx1.SubItems.Add(t_ds.user_tantou_name);
                itemx1.SubItems.Add(t_ds.user_tantou_name_kana);
                itemx1.SubItems.Add(t_ds.busho_name);
                itemx1.SubItems.Add(t_ds.telno1);
                itemx1.SubItems.Add(t_ds.telno2);
                itemx1.SubItems.Add(t_ds.yakusyoku);
                itemx1.SubItems.Add(t_ds.biko);
                itemx1.SubItems.Add(t_ds.status);
                itemx1.SubItems.Add(t_ds.userno);
                itemx1.SubItems.Add(t_ds.chk_date);
                itemx1.SubItems.Add(t_ds.chk_name_id);


                this.m_tantouList.Items.Add(itemx1);
            }
            //件数表示
            this.m_tantou_count.Text = slist.Count.ToString() + "件";

        }
        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        //検索ボタン
        private void m_selectBtn_Click(object sender, EventArgs e)
        {
            m_Customer_List.Clear();
            DISP_dataSet dset = new DISP_dataSet();
            Dictionary<string, string> param_dict = new Dictionary<string, string>();
            Class_Detaget dg = new Class_Detaget();

            if (m_selecttext.Text != "")
            {

                if (this.m_selectKoumoku.SelectedIndex.ToString() != "")

                {
                    switch (this.m_selectKoumoku.SelectedIndex)
                    {
                        //カスタマ通番
                        case 0:
                            param_dict["userno"] = m_selecttext.Text;
                            break;
                        //カスタマID
                        case 1:
                            param_dict["customerID"] = m_selecttext.Text;
                            break;
                        //カスタマ名
                        case 2:
                            param_dict["username"] = m_selecttext.Text;
                            break;
                        //カスタマ名カナ
                        case 3:
                            param_dict["username_kana"] = m_selecttext.Text;
                            break;

                        //カスタマ名略称
                        case 4:
                            param_dict["username_sum"] = m_selecttext.Text;
                            break;

                        //有効/無効
                        case 5:
                            if (m_selecttext.Text == "無効")
                                param_dict["status"] = "0";
                            else if (m_selecttext.Text == "有効")
                                param_dict["status"] = "1";
                            
                            break;

                        //レポート出力有無
                        case 6:
                            if (m_selecttext.Text == "無効")
                                param_dict["report_status"] = "0";
                            else if (m_selecttext.Text == "有効")
                                param_dict["report_status"] = "1";

                            break;
                        //備考
                        case 7:
                            param_dict["biko"] = m_selecttext.Text;
                            break;
                        //更新日時
                        case 8:
                            DateTime dt;
                            String str = m_selecttext.Text;

                            //入力された日付の形式の確認
                            if (DateTime.TryParse(str, out dt))
                            {
                                param_dict["chk_date"] = str;
                            }
                            else
                            {

                                MessageBox.Show("日付の形式が正しくありません。", "カスタマ検索");
                                return;
                            }
                            break;
                        //更新者
                        case 9:
                            param_dict["chk_name_id"] = m_selecttext.Text;
                            break;

                        default:
                            break;

                    }
                }
            }

            dset = dg.getSelectUser(param_dict, con, dset,true);

            this.splitContainer1.SplitterDistance = 220;

            this.m_Customer_List.FullRowSelect = true;
            this.m_Customer_List.HideSelection = false;
            this.m_Customer_List.HeaderStyle = ColumnHeaderStyle.Clickable;

            this.m_Customer_List.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_Customer_List.Columns.Insert(1, "カスタマID", 50, HorizontalAlignment.Left);
            this.m_Customer_List.Columns.Insert(2, "カスタマ名", 200, HorizontalAlignment.Left);
            this.m_Customer_List.Columns.Insert(3, "カスタマ名カナ", 200, HorizontalAlignment.Left);
            this.m_Customer_List.Columns.Insert(4, "カスタマ名略称", 100, HorizontalAlignment.Left);
            this.m_Customer_List.Columns.Insert(5, "有効/無効", 40, HorizontalAlignment.Left);
            this.m_Customer_List.Columns.Insert(6, "SLO対象", 40, HorizontalAlignment.Left);
            this.m_Customer_List.Columns.Insert(7, "備考", 300, HorizontalAlignment.Left);
            this.m_Customer_List.Columns.Insert(8, "更新日時", 120, HorizontalAlignment.Left);
            this.m_Customer_List.Columns.Insert(9, "更新者", 120, HorizontalAlignment.Left);

            //リストに表示
            if(dset.user_L != null)
            { 
                foreach (userDS t_ds in dset.user_L) { 
            

                    ListViewItem itemx1 = new ListViewItem();
                    itemx1.Text = t_ds.userno;

                    itemx1.SubItems.Add(t_ds.customerID);
                    itemx1.SubItems.Add(t_ds.username);
                    itemx1.SubItems.Add(t_ds.username_kana);
                    itemx1.SubItems.Add(t_ds.username_sum);
                    itemx1.SubItems.Add(t_ds.status);
                    itemx1.SubItems.Add(t_ds.report_status);
                    itemx1.SubItems.Add(t_ds.biko);
                    itemx1.SubItems.Add(t_ds.chk_date);
                    itemx1.SubItems.Add(t_ds.chk_name_id);

                    this.m_Customer_List.Items.Add(itemx1);
                }
            }
        }
        //カスタマ名をダブルクリックしたとき
        private void m_Customer_List_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            ListView.SelectedIndexCollection item = m_Customer_List.SelectedIndices;
            userDS userdt = new userDS();
            userdt.userno = this.m_Customer_List.Items[item[0]].SubItems[0].Text;
            userdt.customerID = this.m_Customer_List.Items[item[0]].SubItems[1].Text;
            userdt.username = this.m_Customer_List.Items[item[0]].SubItems[2].Text;
            userdt.username_kana = this.m_Customer_List.Items[item[0]].SubItems[3].Text;
            userdt.username_sum = this.m_Customer_List.Items[item[0]].SubItems[4].Text;

            string statustxt = this.m_Customer_List.Items[item[0]].SubItems[5].Text;
            if (statustxt == "有効")
                userdt.status = "1";
            else
                userdt.status = "0";

            string reportststxt = this.m_Customer_List.Items[item[0]].SubItems[6].Text;
            if (reportststxt == "有効")
                userdt.report_status = "1";
            else
                userdt.report_status = "0";

            userdt.biko = this.m_Customer_List.Items[item[0]].SubItems[7].Text;
            userdt.chk_date = this.m_Customer_List.Items[item[0]].SubItems[8].Text;
            userdt.chk_name_id = this.m_Customer_List.Items[item[0]].SubItems[9].Text;

            getcustomer(userdt);
        }
        //担当者一覧がダブルクリックされたとき
        private void m_tantouList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView.SelectedIndexCollection item = m_tantouList.SelectedIndices;
            tantouDS tantoudt = new tantouDS();
            tantoudt.user_tantou_no= this.m_tantouList.Items[item[0]].SubItems[0].Text;

            tantoudt.user_tantou_name = this.m_tantouList.Items[item[0]].SubItems[1].Text;
            tantoudt.user_tantou_name_kana = this.m_tantouList.Items[item[0]].SubItems[2].Text;
            tantoudt.busho_name = this.m_tantouList.Items[item[0]].SubItems[3].Text;
            tantoudt.telno1 = this.m_tantouList.Items[item[0]].SubItems[4].Text;
            tantoudt.telno2 = this.m_tantouList.Items[item[0]].SubItems[5].Text;
            tantoudt.yakusyoku = this.m_tantouList.Items[item[0]].SubItems[6].Text;
            tantoudt.biko = this.m_tantouList.Items[item[0]].SubItems[7].Text;
            string status = "0";
            if (this.m_tantouList.Items[item[0]].SubItems[8].Text == "有効")
            {
                status = "1";
            }
            else
            {
                status = "0";
            }
            tantoudt.status = status;
            tantoudt.userno = this.m_tantouList.Items[item[0]].SubItems[9].Text;

            tantoudt.chk_date = this.m_tantouList.Items[item[0]].SubItems[10].Text;
            tantoudt.chk_name_id= this.m_tantouList.Items[item[0]].SubItems[11].Text;

            Form_user_tantou usertantou = new Form_user_tantou();
            usertantou.tantoudt = tantoudt;

            usertantou.loginDS = loginDS;
            usertantou.con = con;
            // カスタマ担当者を取得する
            usertantou.Show();

        }
        //メンテリンク
        private void m_tantouMente_link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form_user_tantou usertantou = new Form_user_tantou();
            usertantou.loginDS = loginDS;
            usertantou.con = con;
            // カスタマ担当者を取得する
            usertantou.Show();
        }
        //一覧情報のカラムをクリックをクリックしたとき
        private void m_Customer_List_ColumnClick(object sender, ColumnClickEventArgs e)
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
            m_Customer_List.Sort();
        }

        private void m_deleteBtn_Click(object sender, EventArgs e)
        {
            int count = m_Customer_List.SelectedItems.Count;
            if (count == 0)
                return;
            //確認メッセージ
            if (MessageBox.Show("一覧に選択された行 " + count + "件 の削除を行います。他のテーブルのデータも削除します。" + Environment.NewLine +
                "よろしいですか？", "カスタマ削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            int ret = deleteCustomer();
            if (ret == -1)
            {
                return;
            }
            
            //リストの表示上からけす
            foreach (ListViewItem item in m_Customer_List.SelectedItems)
            {
                m_Customer_List.Items.Remove(item);
            }
        }
        //削除
        private int deleteCustomer()
        {

            string userno;
            int ret = 0;
            if (con.FullState != ConnectionState.Open) con.Open();

            String sql = "WITH DELETED1 AS (DELETE FROM watch_interface WHERE userno = :no " +
                "RETURNING userno), " +
                "DELETED2 AS (DELETE FROM host WHERE userno = :no " +
                "RETURNING userno), " +
                "DELETED3 AS (DELETE FROM site WHERE userno = :no " +
                "RETURNING userno), " +
                "DELETED4 AS (DELETE FROM system WHERE userno = :no " +
                "RETURNING userno), " +
                "DELETED5 AS (DELETE FROM user_tanntou WHERE userno = :no " +
                "RETURNING user_tantou_no,'2'), " +
                "DELETED6 AS (DELETE FROM mailaddress WHERE (opetantouno,kubun) IN (SELECT user_tantou_no,kubun FROM DELETED5)" +
                "RETURNING opetantouno) " +
                "DELETE FROM user_tbl WHERE userno = :no  ";

            using (var transaction = con.BeginTransaction())
            {
                foreach (ListViewItem item in m_Customer_List.SelectedItems)
                {
                    userno = item.SubItems[0].Text;


                    var command = new NpgsqlCommand(@sql, con);
                    command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = int.Parse(userno) });

                    Int32 rowsaffected;
                    try
                    {
                        //削除処理
                        rowsaffected = command.ExecuteNonQuery();


                        if (rowsaffected < 1)
                        {
                            MessageBox.Show("削除できませんでした。カスタマ担当者通番:" + userno, "カスタマ削除");
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
                        MessageBox.Show("カスタマ担当者削除時エラーが発生しました。 " + ex.Message);
                        if (transaction.Connection != null) transaction.Rollback();
                        return -1;
                    }
                }
                if (ret == 1)
                {
                    MessageBox.Show("削除完了しました。", "カスタマ削除");
                    transaction.Commit();
                }
            }
            return 1;
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        //完了の有無チェックをする
        private void m_umu_check_CheckedChanged(object sender, EventArgs e)
        {
            //担当者リストを再表示
            m_tantouList.Clear();
            disp_tantouList();
        }
    }
}
