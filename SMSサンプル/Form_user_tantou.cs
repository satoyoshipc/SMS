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
    public partial class Form_user_tantou : Form
    {
        //ログイン情報
        public opeDS loginDS { get; set; }

        public tantouDS tantoudt { get; set; }

        public List<tantouDS> tantouList { get; set; }

        //DBコネクション
        public NpgsqlConnection con { get; set; }

        //ListViewのソートの際に使用する
        private Class_ListViewColumnSorter _columnSorter;


        public Form_user_tantou()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //更新ボタン
        private void m_kousin_btn_Click(object sender, EventArgs e)
        {
            if (m_tantouname.Text == "")
            {
                MessageBox.Show("担当者名を入力して下さい。", "カスタマ担当者修正", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (m_statusCombo.Text == "")
            {
                MessageBox.Show("有効/無効を入力して下さい。", "カスタマ担当者修正", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //確認ダイアログ
            if (MessageBox.Show("カスタマ担当者の更新を行います。よろしいですか？", "カスタマ担当者更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string status = "";
            if (m_statusCombo.Text == "有効")
                //有効の時
                status = "1";
            else if (m_statusCombo.Text == "無効")
                //無効の時
                status = "0";


            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "update user_tanntou set user_tantou_name =:a , user_tantou_name_kana=:b,busho_name=:c,telno1=:d,telno2 =:e,yakusyoku=:f,status=:g,biko=:h,chk_name_id =:i,chk_date=:j " +
                "WHERE user_tantou_no=:k"  ;
            using (var transaction = con.BeginTransaction())
            {

                var command = new NpgsqlCommand(@sql, con);
                command.Parameters.Add(new NpgsqlParameter("a", DbType.String) { Value = m_tantouname.Text });
                command.Parameters.Add(new NpgsqlParameter("b", DbType.String) { Value = m_tantoukana.Text });
                command.Parameters.Add(new NpgsqlParameter("c", DbType.String) { Value = m_busyoname.Text });
                command.Parameters.Add(new NpgsqlParameter("d", DbType.String) { Value = m_tel1.Text });
                command.Parameters.Add(new NpgsqlParameter("e", DbType.String) { Value = m_tel2.Text });
                command.Parameters.Add(new NpgsqlParameter("f", DbType.String) { Value = m_yakusyoku.Text });
                command.Parameters.Add(new NpgsqlParameter("g", DbType.String) { Value = status });
                command.Parameters.Add(new NpgsqlParameter("h", DbType.String) { Value = m_biko.Text });
                command.Parameters.Add(new NpgsqlParameter("i", DbType.String) { Value = loginDS.opeid });
                command.Parameters.Add(new NpgsqlParameter("j", DbType.DateTime) { Value = DateTime.Now });
                command.Parameters.Add(new NpgsqlParameter("k", DbType.Int32) { Value = m_tantouno.Text });
                Int32 rowsaffected;
                try
                {
                    //更新処理
                    rowsaffected = command.ExecuteNonQuery();
                    transaction.Commit();

                    if (rowsaffected != 1)
                        MessageBox.Show("更新できませんでした。", "カスタマ担当者");
                    else
                        MessageBox.Show("更新されました。", "カスタマ担当者");
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

        //表示前処理
        private void Form_user_tantou_Load(object sender, EventArgs e)
        {
            _columnSorter = new Class_ListViewColumnSorter();
            m_customertantouList.ListViewItemSorter = _columnSorter;

            m_selectCombo.Items.Add("担当者通番");
            m_selectCombo.Items.Add("担当者名");
            m_selectCombo.Items.Add("担当者名カナ");
            m_selectCombo.Items.Add("部署名");
            m_selectCombo.Items.Add("電話番号1");
            m_selectCombo.Items.Add("電話番号2");
            m_selectCombo.Items.Add("役職");
            m_selectCombo.Items.Add("ステータス");
            m_selectCombo.Items.Add("備考");
            m_selectCombo.Items.Add("カスタマ通番");
            m_selectCombo.Items.Add("更新日時");
            m_selectCombo.Items.Add("更新者ID");
            if (tantoudt != null)
            { 
                user_tanntou_Disp(tantoudt);
                
                //アドレスを表示
                opeAddress();
            }

        }
        //カスタマ担当者の表示
        private void user_tanntou_Disp(tantouDS tantoudt)
        {
            m_userno.Text = tantoudt.userno;
            Class_Detaget dg = new Class_Detaget();
            dg.con = con;
            string cusname = dg.getCustomername(tantoudt.userno);
            m_username.Text = cusname;
            m_tantouno.Text = tantoudt.user_tantou_no;
            m_tantouname.Text = tantoudt.user_tantou_name;
            m_tantoukana.Text = tantoudt.user_tantou_name_kana;
            m_busyoname.Text = tantoudt.busho_name;
            m_tel1.Text = tantoudt.telno1;
            m_tel2.Text = tantoudt.telno2;
            m_statusCombo.Text = tantoudt.status;
            m_yakusyoku.Text = tantoudt.yakusyoku;
            m_biko.Text = tantoudt.biko;
            m_update.Text = tantoudt.chk_date;
            m_updateOpe.Text = tantoudt.chk_name_id;
        }
        private void m_cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_customertantouList_DoubleClick(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection item = m_customertantouList.SelectedIndices;

            tantouDS tantoudt = new tantouDS();

            tantoudt.user_tantou_no         = this.m_customertantouList.Items[item[0]].SubItems[0].Text;
            tantoudt.user_tantou_name       = this.m_customertantouList.Items[item[0]].SubItems[1].Text;
            tantoudt.user_tantou_name_kana  = this.m_customertantouList.Items[item[0]].SubItems[2].Text;
            tantoudt.busho_name             = this.m_customertantouList.Items[item[0]].SubItems[3].Text;
            tantoudt.telno1                 = this.m_customertantouList.Items[item[0]].SubItems[4].Text;
            tantoudt.telno2                 = this.m_customertantouList.Items[item[0]].SubItems[5].Text;
            tantoudt.yakusyoku              = this.m_customertantouList.Items[item[0]].SubItems[6].Text;
            tantoudt.biko                   = this.m_customertantouList.Items[item[0]].SubItems[8].Text;
            tantoudt.userno                = this.m_customertantouList.Items[item[0]].SubItems[9].Text;
            tantoudt.chk_date               = this.m_customertantouList.Items[item[0]].SubItems[11].Text;
            tantoudt.chk_name_id            = this.m_customertantouList.Items[item[0]].SubItems[12].Text;


            string statustxt = this.m_customertantouList.Items[item[0]].SubItems[7].Text;
            if (statustxt == "有効")
                tantoudt.status = "1";
            else
                tantoudt.status = "0";
            
            //担当者番号
            user_tanntou_Disp(tantoudt);

            //アドレスを表示
            opeAddress();


        }

        //アドレス
        private void opeAddress()
        {
            m_addressslist.Clear();

            Class_Detaget dg = new Class_Detaget();
            List<MailaddressDS> addressList = new List<MailaddressDS>();
            Dictionary<string, string> param_dict = new Dictionary<string, string>();
            param_dict["opetantouno"] = m_tantouno.Text;

            //アドレスを取得
            addressList = dg.selectMailList_Tantou(param_dict, con);

            this.m_addressslist.FullRowSelect = true;
            this.m_addressslist.HideSelection = false;
            this.m_addressslist.HeaderStyle = ColumnHeaderStyle.Clickable;

            this.m_addressslist.Columns.Insert(0, "アドレス番号", 50, HorizontalAlignment.Left);
            this.m_addressslist.Columns.Insert(1, "メールアドレス", 150, HorizontalAlignment.Left);
            this.m_addressslist.Columns.Insert(2, "アドレス名", 150, HorizontalAlignment.Left);
            this.m_addressslist.Columns.Insert(3, "更新日時", 120, HorizontalAlignment.Left);
            this.m_addressslist.Columns.Insert(4, "更新者", 120, HorizontalAlignment.Left);

            //リストに表示
            if (addressList != null)
            {
                foreach (MailaddressDS ad_ds in addressList)
                {

                    ListViewItem itemx1 = new ListViewItem();
                    itemx1.Text = ad_ds.addressNo;

                    itemx1.SubItems.Add(ad_ds.mailAddress);
                    itemx1.SubItems.Add(ad_ds.addressname);
                    itemx1.SubItems.Add(ad_ds.chk_date);
                    itemx1.SubItems.Add(ad_ds.chk_name_id);

                    this.m_addressslist.Items.Add(itemx1);
                }

                //件数表示
                this.m_mail_count.Text = addressList.Count.ToString() + "件";
            }

        }


        //検索ボタン
        private void m_select_btn_Click(object sender, EventArgs e)
        {
            m_customertantouList.Clear();
            DISP_dataSet dset = new DISP_dataSet();
            Dictionary<string, string> param_dict = new Dictionary<string, string>();
            Class_Detaget dg = new Class_Detaget();
            if(m_selecttext.Text != "") {
                if (this.m_selectCombo.SelectedIndex.ToString() != "")
                {


                    switch (this.m_selectCombo.SelectedIndex)
                    {
                        //担当者番号
                        case 0:
                            param_dict["user_tantou_no"] = m_selecttext.Text;
                            break;

                        case 1:
                            param_dict["user_tantou_name"] = m_selecttext.Text;
                            break;
                        case 2:
                            param_dict["user_tantou_name_kana"] = m_selecttext.Text;
                            break;

                        case 3:
                            param_dict["busho_name"] = m_selecttext.Text;
                            break;

                        case 4:
                            param_dict["telno1"] = m_selecttext.Text;
                            break;

                        case 5:
                            param_dict["telno2"] = m_selecttext.Text;
                            break;
                        case 6:
                            param_dict["yakusyoku"] = m_selecttext.Text;
                            break;

                        case 7:
                            if (m_selecttext.Text == "無効")
                                param_dict["status"] = "0";
                            else if (m_selecttext.Text == "有効")
                                param_dict["status"] = "1";

                            break;
                        case 8:
                            param_dict["biko"] = m_selecttext.Text;
                            break;
                        case 9:
                            param_dict["userno"] = m_selecttext.Text;
                            break;

                        //更新日時
                        case 10:

                            DateTime dt;
                            String str = m_selecttext.Text;

                            //入力された日付の形式の確認
                            if (DateTime.TryParse(str, out dt))
                            {
                                param_dict["chk_date"] = str;
                            }
                            else
                            {

                                MessageBox.Show("日付の形式が正しくありません。", "カスタマ担当者");
                                return;
                            }
                            break;
                        //更新者
                        case 11:
                            param_dict["chk_name_id"] = m_selecttext.Text;
                            break;

                        default:
                            break;
                    }
                }
            }

            List < tantouDS > tantouList = dg.get_tantouName(param_dict, con);
            
            this.m_customertantouList.FullRowSelect = true;
            this.m_customertantouList.HideSelection = false;
            this.m_customertantouList.HeaderStyle = ColumnHeaderStyle.Clickable;

            this.m_customertantouList.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_customertantouList.Columns.Insert(1, "担当者名", 120, HorizontalAlignment.Left);
            this.m_customertantouList.Columns.Insert(2, "担当者名カナ", 120, HorizontalAlignment.Left);
            this.m_customertantouList.Columns.Insert(3, "部署名", 90, HorizontalAlignment.Left);
            this.m_customertantouList.Columns.Insert(4, "電話番号1", 80, HorizontalAlignment.Left);
            this.m_customertantouList.Columns.Insert(5, "電話番号2", 80, HorizontalAlignment.Left);
            this.m_customertantouList.Columns.Insert(6, "役職", 50, HorizontalAlignment.Left);
            this.m_customertantouList.Columns.Insert(7, "ステータス", 50, HorizontalAlignment.Left);
            this.m_customertantouList.Columns.Insert(8, "備考", 50, HorizontalAlignment.Left);
            this.m_customertantouList.Columns.Insert(9, "カスタマID", 50, HorizontalAlignment.Left);
            this.m_customertantouList.Columns.Insert(10, "カスタマ名", 50, HorizontalAlignment.Left);

            this.m_customertantouList.Columns.Insert(11, "更新日時", 50, HorizontalAlignment.Left);
            this.m_customertantouList.Columns.Insert(12, "更新者", 50, HorizontalAlignment.Left);

            //リストに表示
            if (tantouList != null)
            { 
                foreach (tantouDS t_ds in tantouList) { 
            
                    ListViewItem itemx1 = new ListViewItem();
                    itemx1.Text = t_ds.user_tantou_no;

                    itemx1.SubItems.Add(t_ds.user_tantou_name);
                    itemx1.SubItems.Add(t_ds.user_tantou_name_kana);
                    itemx1.SubItems.Add(t_ds.busho_name);
                    itemx1.SubItems.Add(t_ds.telno1);
                    itemx1.SubItems.Add(t_ds.telno2);
                    itemx1.SubItems.Add(t_ds.yakusyoku);
                    itemx1.SubItems.Add(t_ds.status);
                    itemx1.SubItems.Add(t_ds.biko);
                    itemx1.SubItems.Add(t_ds.userno);
                    itemx1.SubItems.Add(t_ds.username);

                    itemx1.SubItems.Add(t_ds.chk_date);
                    itemx1.SubItems.Add(t_ds.chk_name_id);

                    this.m_customertantouList.Items.Add(itemx1);
                }
            }
        }

        //削除ボタン
        private void m_deleteBtn_Click(object sender, EventArgs e)
        {
            int count = m_customertantouList.SelectedItems.Count;

            //確認メッセージ
            if (MessageBox.Show("一覧に選択された行 " + count + "件 の削除を行います。" + Environment.NewLine +
                "よろしいですか？", "カスタマ担当者削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)

                return;


            int ret = deleteCustomer();
            if (ret == -1)
            {
                return;
            }

            //リストの表示上からけす
            foreach (ListViewItem item in m_customertantouList.SelectedItems)
            {
                m_customertantouList.Items.Remove(item);
            }
        }
        //削除
        private int deleteCustomer()
        {

            string user_tantou_no;
            int ret = 0;
            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "DELETE FROM user_tanntou where user_tantou_no = :no";

            using (var transaction = con.BeginTransaction())
            {
                foreach (ListViewItem item in m_customertantouList.SelectedItems)
                {
                    user_tantou_no = item.SubItems[0].Text;


                    var command = new NpgsqlCommand(@sql, con);
                    command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = int.Parse(user_tantou_no) });

                    Int32 rowsaffected;
                    try
                    {
                        //削除処理
                        rowsaffected = command.ExecuteNonQuery();
                        

                        if (rowsaffected < 1)
                        {
                            MessageBox.Show("削除できませんでした。カスタマ担当者通番:" + user_tantou_no, "カスタマ担当者削除");
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
                if (ret==1)
                {
                    MessageBox.Show("削除完了しました。", "カスタマ担当者削除");
                    transaction.Commit();
                }
            }
            return 1;

        }
        //メールアドレス追加ボタン
        private void button1_Click(object sender, EventArgs e)
        {
            Form_addressInsert addressIns = new Form_addressInsert();
            addressIns.con = con;
            addressIns.kbn = 2;
            addressIns.userid = m_tantouno.Text;
            addressIns.username = m_tantouname.Text;
            addressIns.loginDS = loginDS;
            addressIns.Show();
        }

        private void m_addressslist_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView.SelectedIndexCollection item = m_addressslist.SelectedIndices;

            Form_mailDetail addressDetail = new Form_mailDetail();
            addressDetail.con = con;


            MailaddressDS mailDt = new MailaddressDS();

            mailDt.kubun = "2";
            mailDt.opetantouno = this.m_tantouno.Text;
            mailDt.addressNo = this.m_addressslist.Items[item[0]].SubItems[0].Text;
            //mailDt.userno = this.m_addressslist.Items[item[0]].SubItems[1].Text;
            mailDt.mailAddress = this.m_addressslist.Items[item[0]].SubItems[1].Text;
            mailDt.addressname = this.m_addressslist.Items[item[0]].SubItems[2].Text;
            mailDt.chk_date = this.m_addressslist.Items[item[0]].SubItems[3].Text;
            mailDt.chk_name_id = this.m_addressslist.Items[item[0]].SubItems[4].Text;

            addressDetail.addresssDS = mailDt;
            addressDetail.loginDS = loginDS;
            addressDetail.Show();
        }

        private void m_customertantouList_ColumnClick(object sender, ColumnClickEventArgs e)
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
            m_customertantouList.Sort();

        }
        //メールアドレスメンテ画面を開く
        private void m_mailMente_link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            Form_mailDetail addressDetail = new Form_mailDetail();
            addressDetail.con = con;
            addressDetail.loginDS = loginDS;
            addressDetail.Show();
        }
    }
}
