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
    public partial class Form_mailDetail : Form
    {
        //ログイン情報
        public opeDS loginDS { get; set; }

        //ログ
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MailaddressDS addresssDS { get; set; }
        //DBコネクション
        public NpgsqlConnection con { get; set; }

        //ListViewのソートの際に使用する
        private Class_ListViewColumnSorter _columnSorteraddress;

        public Form_mailDetail()
        {
            InitializeComponent();
        }

        //更新ボタン
        private void m_kousin_btn_Click_1(object sender, EventArgs e)
        {
            if (m_kubun.Text == "")
            {
                MessageBox.Show("ユーザ区分が入力されていません。", "メールアドレス修正", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (m_userid.Text == "")
            {
                MessageBox.Show("ユーザIDが入力されていません。", "メールアドレス修正", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (m_addressno.Text == "")
            {
                MessageBox.Show("アドレス番号を入力して下さい。", "メールアドレス修正", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (m_address.Text == "")
            {
                MessageBox.Show("メールアドレスを入力して下さい。", "メールアドレス修正", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //確認ダイアログ
            if (MessageBox.Show("メールアドレスの更新を行います。よろしいですか？", "メールアドレス修正", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "update mailaddress set kubun=:kubun,opetantouno=:opetantouno,addressno=:addressno,mailaddress=:mailaddress,addressname=:addressname,chk_name_id =:chk_name_id ,chk_date=:chk_date " +
                "WHERE kubun=:kubun AND opetantouno=:opetantouno AND addressno=:addressno ";
            using (var transaction = con.BeginTransaction())
            {
                string kubun = m_kubun.Text;
                if (0 <= kubun.IndexOf("オペレータ"))
                {
                    kubun = "1";
                }
                else if(0 <= kubun.IndexOf("オペレータ"))
                {
                    kubun = "2";
                }
                var command = new NpgsqlCommand(@sql, con);
                command.Parameters.Add(new NpgsqlParameter("kubun", DbType.String) { Value = kubun });
                command.Parameters.Add(new NpgsqlParameter("opetantouno", DbType.Int32) { Value = int.Parse(m_userid.Text) });
                command.Parameters.Add(new NpgsqlParameter("addressno", DbType.Int32) { Value = int.Parse(m_addressno.Text) });
                command.Parameters.Add(new NpgsqlParameter("mailaddress", DbType.String) { Value = m_address.Text });
                command.Parameters.Add(new NpgsqlParameter("addressname", DbType.String) { Value = m_addressname.Text });
                command.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = DateTime.Now });
                command.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });
                Int32 rowsaffected;
                try
                {
                    //更新処理
                    rowsaffected = command.ExecuteNonQuery();


                    if (rowsaffected != 1)
                    {
                        MessageBox.Show("メールアドレスを更新できませんでした。SQL:" + sql + "ユーザID:"+ m_userid.Text, "メールアドレス");
                        logger.ErrorFormat("メールアドレスを更新できませんでした。 SQL:{0} ユーザID:{1}",sql, m_userid.Text);
                    }
                    else
                    {
                        transaction.Commit();
                        MessageBox.Show("更新されました。", "メールアドレス");


                    }
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

        //表示前処理
        private void Form_user_tantou_Load(object sender, EventArgs e)
        {
            _columnSorteraddress = new Class_ListViewColumnSorter();
            m_addressslist.ListViewItemSorter = _columnSorteraddress;

            m_selectCombo.Items.Add("ユーザ区分");
            m_selectCombo.Items.Add("ユーザID");
            m_selectCombo.Items.Add("アドレス番号");
            m_selectCombo.Items.Add("メールアドレス");
            m_selectCombo.Items.Add("アドレス名");
            m_selectCombo.Items.Add("更新日時");
            m_selectCombo.Items.Add("更新者ID");
            if(addresssDS != null)
                add_Disp(addresssDS);

        }
        //オペレータの表示
        private void add_Disp(MailaddressDS addressDt)
        {
            string kubunstr="";
            if (addressDt.kubun == "1")
                kubunstr = "1:オペレータ";
            else if (addressDt.kubun == "2")
                kubunstr = "2:カスタマ担当者";

            m_kubun.Text = kubunstr;
            m_userid.Text = addressDt.opetantouno;
            m_addressno.Text = addressDt.addressNo;
            m_username.Text = addressDt.user_tantou_name;
            m_Customername.Text = addressDt.username;

            m_address.Text = addressDt.mailAddress;
            m_addressname.Text = addressDt.addressname;

            m_update.Text = addressDt.chk_date;
            m_updateOpe.Text = addressDt.chk_name_id;
        }
        //キャンセルボタン
        private void m_cancelbtn_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        //リストダブルクリック
        private void m_addressslist_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            ListView.SelectedIndexCollection item = m_addressslist.SelectedIndices;

            MailaddressDS mailDt = new MailaddressDS();
            string kubun = this.m_addressslist.Items[item[0]].SubItems[0].Text;
            if(0 <= kubun.IndexOf("オペレータ"))
                mailDt.kubun     = "1";
            else
                mailDt.kubun = "2";

            mailDt.opetantouno = this.m_addressslist.Items[item[0]].SubItems[1].Text;
            mailDt.addressNo  = this.m_addressslist.Items[item[0]].SubItems[2].Text;

            mailDt.user_tantou_name= this.m_addressslist.Items[item[0]].SubItems[3].Text;
            mailDt.username = this.m_addressslist.Items[item[0]].SubItems[4].Text;
            
            mailDt.mailAddress  = this.m_addressslist.Items[item[0]].SubItems[5].Text;
            mailDt.addressname  = this.m_addressslist.Items[item[0]].SubItems[6].Text;
            mailDt.chk_date               = this.m_addressslist.Items[item[0]].SubItems[7].Text;
            mailDt.chk_name_id            = this.m_addressslist.Items[item[0]].SubItems[8].Text;

            add_Disp(mailDt);
        }
        //アドレス
        private void getmailaddress(Dictionary<string, string> param_dict,NpgsqlConnection con)
        {
            m_addressslist.Clear();

            Class_Detaget dg = new Class_Detaget();
            List<MailaddressDS> addressList = new List<MailaddressDS>();

            //アドレスを取得3は両方
            addressList = dg.selectMailList(param_dict, con,"3");

            this.m_addressslist.FullRowSelect = true;
            this.m_addressslist.HideSelection = false;
            this.m_addressslist.HeaderStyle = ColumnHeaderStyle.Clickable;
            this.m_addressslist.Columns.Insert(0, "ユーザ区分", 100, HorizontalAlignment.Left);
            this.m_addressslist.Columns.Insert(1, "ユーザ通番", 50, HorizontalAlignment.Left);
            this.m_addressslist.Columns.Insert(2, "アドレス番号", 50, HorizontalAlignment.Left);
            this.m_addressslist.Columns.Insert(3, "ユーザ名", 150, HorizontalAlignment.Left);
            this.m_addressslist.Columns.Insert(4, "カスタマ名", 150, HorizontalAlignment.Left);
            this.m_addressslist.Columns.Insert(5, "メールアドレス", 250, HorizontalAlignment.Left);
            this.m_addressslist.Columns.Insert(6, "アドレス名", 250, HorizontalAlignment.Left);
            this.m_addressslist.Columns.Insert(7, "更新日時", 120, HorizontalAlignment.Left);
            this.m_addressslist.Columns.Insert(8, "更新者", 120, HorizontalAlignment.Left);

            //リストに表示
            if (addressList != null)
            {
                foreach (MailaddressDS ad_ds in addressList)
                {

                    ListViewItem itemx1 = new ListViewItem();
                    string kubunstr = "";
                    if (ad_ds.kubun == "1")
                        kubunstr = "1:オペレータ";
                    else if (ad_ds.kubun == "2")
                        kubunstr = "2:カスタマ担当者";

                    itemx1.Text = kubunstr;
                    itemx1.SubItems.Add(ad_ds.opetantouno);

                    itemx1.SubItems.Add(ad_ds.addressNo);
                    itemx1.SubItems.Add(ad_ds.user_tantou_name);
                    itemx1.SubItems.Add(ad_ds.username);
                    itemx1.SubItems.Add(ad_ds.mailAddress);

                    itemx1.SubItems.Add(ad_ds.addressname);
                    itemx1.SubItems.Add(ad_ds.chk_date);
                    itemx1.SubItems.Add(ad_ds.chk_name_id);

                    this.m_addressslist.Items.Add(itemx1);
                }
            }

        }
        //検索ボタン
        private void m_select_btn_Click_1(object sender, EventArgs e)
        {
            m_addressslist.Clear();
            DISP_dataSet dset = new DISP_dataSet();
            Dictionary<string, string> param_dict = new Dictionary<string, string>();
            Class_Detaget dg = new Class_Detaget();
            if(m_selecttext.Text != "") {
                if (this.m_selectCombo.SelectedIndex.ToString() != "")
                {

                    switch (this.m_selectCombo.SelectedIndex)
                    {
                        
                        case 0:
                            param_dict["kubun"] = m_selecttext.Text;
                            break;

                        case 1:
                            param_dict["opetantouno"] = m_selecttext.Text;
                            break;
                        case 2:
                            param_dict["addressNo"] = m_selecttext.Text;
                            break;

                        case 3:
                            param_dict["mailAddress"] = m_selecttext.Text;
                            break;

                        case 4:
                            param_dict["addressname"] = m_selecttext.Text;
                            break;
                        //更新日時
                        case 5:
                            DateTime dt;
                            String str = m_selecttext.Text;

                            //入力された日付の形式の確認
                            if (DateTime.TryParse(str, out dt))
                            {
                                param_dict["chk_date"] = str;
                            }
                            else
                            {

                                MessageBox.Show("日付の形式が正しくありません。", "メール検索");
                                return;
                            }
                            break;
                        //更新者
                        case 6:
                            param_dict["chk_name_id"] = m_selecttext.Text;
                            break;

                        default:
                            break;
                    }
                }
            }

            //メール一覧をリストに表示する
            getmailaddress(param_dict, con);
            
        }

        //削除ボタン
        private void m_deleteBtn_Click_1(object sender, EventArgs e)
        {

            int count = m_addressslist.SelectedItems.Count;

            //確認メッセージ
            if (MessageBox.Show("一覧に選択された行 " + count + "件 の削除を行います。" + Environment.NewLine +
                "よろしいですか？", "オペレータ削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)

                return;

            int ret = deleteCustomer();
            if (ret == -1)
            {
                return;
            }

            //リストの表示上からけす
            foreach (ListViewItem item in m_addressslist.SelectedItems)
            {
                m_addressslist.Items.Remove(item);
            }
        }
        //削除
        private int deleteCustomer()
        {

            string operno;
            string opetantouno;
            string addressno;
            int ret = 0;
            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "DELETE FROM mailaddress where kubun = :kubun AND opetantouno = :opetantouno AND addressNo = :addressNo ";

            using (var transaction = con.BeginTransaction())
            {
                foreach (ListViewItem item in m_addressslist.SelectedItems)
                {
                    operno = item.SubItems[0].Text;
                    opetantouno = item.SubItems[1].Text;
                    addressno = item.SubItems[2].Text;

                    if (0 <= operno.IndexOf("オペレータ"))
                        operno = "1";
                    else
                        operno = "2";

                    var command = new NpgsqlCommand(@sql, con);
                    command.Parameters.Add(new NpgsqlParameter("kubun", DbType.String) { Value = operno });
                    command.Parameters.Add(new NpgsqlParameter("opetantouno", DbType.Int32) { Value = int.Parse(opetantouno) });
                    command.Parameters.Add(new NpgsqlParameter("addressNo", DbType.Int32) { Value = int.Parse(addressno) });
                    Int32 rowsaffected;
                    try
                    {
                        //削除処理
                        rowsaffected = command.ExecuteNonQuery();
                        

                        if (rowsaffected < 1)
                        {
                            MessageBox.Show("削除できませんでした。メールアドレス通番:" + operno, "メールアドレス削除");
                            logger.ErrorFormat("メールアドレス削除エラー。 SQL:{0} ユーザID:{1}", sql, m_userid.Text);

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
                        MessageBox.Show("メールアドレス削除時エラーが発生しました。 " + ex.Message);
                        logger.ErrorFormat("メールアドレス削除エラー。 SQL:{0} ユーザID:{1}", sql, m_userid.Text);
                        if (transaction.Connection != null) transaction.Rollback();
                        return -1;
                    }
                }
                if (ret==1)
                {
                    MessageBox.Show("削除完了しました。", "メールアドレス削除");
                    transaction.Commit();
                }
            }
            return 1;
        }

        private void m_addressslist_ColumnClick_1(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == _columnSorteraddress.SortColumn)
            {
                if (_columnSorteraddress.Order == SortOrder.Ascending)
                {
                    _columnSorteraddress.Order = SortOrder.Descending;
                }
                else
                {
                    _columnSorteraddress.Order = SortOrder.Ascending;
                }
            }
            else
            {
                _columnSorteraddress.SortColumn = e.Column;
                _columnSorteraddress.Order = SortOrder.Ascending;
            }
            m_addressslist.Sort();

        }


        
        //メールアドレスの追加
        private void button1_Click_1(object sender, EventArgs e)
        {
            Form_addressInsert addressIns = new Form_addressInsert();
            addressIns.con = con;
            addressIns.kbn = 1;
            addressIns.userid = m_kubun.Text;
            addressIns.username = m_addressno.Text + m_address.Text;
            addressIns.loginDS = loginDS;
            addressIns.Show();
        }


    }
}
