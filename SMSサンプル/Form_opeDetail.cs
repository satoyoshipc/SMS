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
    public partial class Form_opeDetail : Form
    {
        //ログイン情報
        public opeDS loginDS { get; set; }

        public opeDS opedt { get; set; }

        public List<opeDS> tantouList { get; set; }

        //DBコネクション
        public NpgsqlConnection con { get; set; }

        //ListViewのソートの際に使用する
        private Class_ListViewColumnSorter _columnSorter, _columnSorteraddress;

        public Form_opeDetail()
        {
            InitializeComponent();
        }
        
        //更新ボタン
        private void m_kousin_btn_Click(object sender, EventArgs e)
        {
            if (m_opeid.Text == "")
            {
                MessageBox.Show("オペレータ名を入力して下さい。", "オペレータ名修正", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (m_lastname.Text == "")
            {
                MessageBox.Show("姓を入力して下さい。", "オペレータ名修正", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (m_password.Text == "")
            {
                MessageBox.Show("パスワードを入力して下さい。", "オペレータ名修正", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //確認ダイアログ
            if (MessageBox.Show("オペレータ名の更新を行います。よろしいですか？", "オペレータ名更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string status = "";
            if (m_kengenCombo.Text == "管理者")
                status = "1";
            else if (m_kengenCombo.Text == "利用者")
                status = "2";


            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "update ope set openo=:openo,opeid=:opeid,lastname=:lastname,fastname=:fastname,password=:password,type=:type,biko=:biko,chk_name_id =:chk_name_id ,chk_date=:chk_date " +
                "WHERE openo=:openo";
            using (var transaction = con.BeginTransaction())
            {

                var command = new NpgsqlCommand(@sql, con);
                command.Parameters.Add(new NpgsqlParameter("openo", DbType.Int32) { Value = int.Parse(m_openo.Text) });
                command.Parameters.Add(new NpgsqlParameter("opeid", DbType.String) { Value = m_opeid.Text });
                command.Parameters.Add(new NpgsqlParameter("lastname", DbType.String) { Value = m_firstname.Text });
                command.Parameters.Add(new NpgsqlParameter("fastname", DbType.String) { Value = m_lastname.Text });
                command.Parameters.Add(new NpgsqlParameter("password", DbType.String) { Value = m_password.Text });
                command.Parameters.Add(new NpgsqlParameter("type", DbType.String) { Value = status });
                command.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = m_biko.Text });
                command.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = DateTime.Now });
                command.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });
                Int32 rowsaffected;
                try
                {
                    //更新処理
                    rowsaffected = command.ExecuteNonQuery();
                    transaction.Commit();

                    if (rowsaffected != 1)
                        MessageBox.Show("更新できませんでした。", "オペレータ");
                    else
                        MessageBox.Show("更新されました。", "オペレータ");
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
            m_oper_List.ListViewItemSorter = _columnSorter;

            _columnSorteraddress = new Class_ListViewColumnSorter();
            m_addressslist.ListViewItemSorter = _columnSorteraddress;


            m_selectCombo.Items.Add("オペレータ通番");
            m_selectCombo.Items.Add("オペレータID");
            m_selectCombo.Items.Add("姓");
            m_selectCombo.Items.Add("名");
            m_selectCombo.Items.Add("パスワード");
            m_selectCombo.Items.Add("権限");
            m_selectCombo.Items.Add("備考");
            m_selectCombo.Items.Add("更新日時");
            m_selectCombo.Items.Add("更新者ID");




        }
        //オペレータの表示
        private void ope_Disp(opeDS operDt)
        {
            m_openo.Text = operDt.openo;
            m_opeid.Text = operDt.opeid;
            m_lastname.Text = operDt.lastname;
            m_firstname.Text = operDt.fastname;
            m_password.Text = operDt.password;
            if (operDt.type == "1")
                m_kengenCombo.Text = "管理者";
            else if (operDt.type == "2")
                m_kengenCombo.Text = "利用者";

            m_biko.Text = operDt.biko;
            m_update.Text = operDt.chk_date;
            m_updateOpe.Text = operDt.chk_name_id;
        }
        private void m_cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //リストダブルクリック
        private void m_customertantouList_DoubleClick(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection item = m_oper_List.SelectedIndices;

            opeDS opeDt = new opeDS();

            opeDt.openo     = this.m_oper_List.Items[item[0]].SubItems[0].Text;
            opeDt.opeid     = this.m_oper_List.Items[item[0]].SubItems[1].Text;
            opeDt.lastname  = this.m_oper_List.Items[item[0]].SubItems[2].Text;
            opeDt.fastname  = this.m_oper_List.Items[item[0]].SubItems[3].Text;
            opeDt.password  = this.m_oper_List.Items[item[0]].SubItems[4].Text;

            opeDt.biko             = this.m_oper_List.Items[item[0]].SubItems[6].Text;
            opeDt.chk_date               = this.m_oper_List.Items[item[0]].SubItems[7].Text;
            opeDt.chk_name_id            = this.m_oper_List.Items[item[0]].SubItems[8].Text;

            string typetxt = this.m_oper_List.Items[item[0]].SubItems[5].Text;
            if (typetxt == "管理者")
                opeDt.type= "1";
            else
                opeDt.type = "2";

            ope_Disp(opeDt);

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
            param_dict["opetantouno"] = m_openo.Text;

            //アドレスを取得
            addressList = dg.selectMailList_ope(param_dict, con);

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
            }

        }
        //検索ボタン
        private void m_select_btn_Click(object sender, EventArgs e)
        {
            m_oper_List.Clear();
            DISP_dataSet dset = new DISP_dataSet();
            Dictionary<string, string> param_dict = new Dictionary<string, string>();
            Class_Detaget dg = new Class_Detaget();
            if(m_selecttext.Text != "") {
                if (this.m_selectCombo.SelectedIndex.ToString() != "")
                {


                    switch (this.m_selectCombo.SelectedIndex)
                    {
                        
                        case 0:
                            param_dict["openo"] = m_selecttext.Text;
                            break;

                        case 1:
                            param_dict["opeid"] = m_selecttext.Text;
                            break;
                        case 2:
                            param_dict["lastname"] = m_selecttext.Text;
                            break;

                        case 3:
                            param_dict["fastname"] = m_selecttext.Text;
                            break;

                        case 4:
                            param_dict["password"] = m_selecttext.Text;
                            break;

                        case 5:
                            param_dict["type"] = m_selecttext.Text;
                            break;
                        case 6:
                            param_dict["biko"] = m_selecttext.Text;
                            break;

                        //更新日時
                        case 10:
                            param_dict["chk_date"] = m_selecttext.Text;
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

            List < opeDS > opeList = dg.getSelectOper(param_dict, con);
            
            this.m_oper_List.FullRowSelect = true;
            this.m_oper_List.HideSelection = false;
            this.m_oper_List.HeaderStyle = ColumnHeaderStyle.Clickable;

            this.m_oper_List.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_oper_List.Columns.Insert(1, "オペレータID", 120, HorizontalAlignment.Left);
            this.m_oper_List.Columns.Insert(2, "姓", 120, HorizontalAlignment.Left);
            this.m_oper_List.Columns.Insert(3, "名", 90, HorizontalAlignment.Left);
            this.m_oper_List.Columns.Insert(4, "パスワード", 80, HorizontalAlignment.Left);
            this.m_oper_List.Columns.Insert(5, "権限", 80, HorizontalAlignment.Left);
            this.m_oper_List.Columns.Insert(6, "備考", 50, HorizontalAlignment.Left);
            this.m_oper_List.Columns.Insert(7, "更新日時", 50, HorizontalAlignment.Left);
            this.m_oper_List.Columns.Insert(8, "更新者", 50, HorizontalAlignment.Left);

            //リストに表示
            if (opeList != null)
            { 
                foreach (opeDS t_ds in opeList) { 
            
                    ListViewItem itemx1 = new ListViewItem();
                    itemx1.Text = t_ds.openo;

                    itemx1.SubItems.Add(t_ds.opeid);
                    itemx1.SubItems.Add(t_ds.lastname);
                    itemx1.SubItems.Add(t_ds.fastname);
                    itemx1.SubItems.Add(t_ds.password);
                    String str = "";
                    if (t_ds.type == "1")
                        str = "管理者";
                    else
                        str = "利用者";

                    itemx1.SubItems.Add(str);
                    itemx1.SubItems.Add(t_ds.biko);
                    itemx1.SubItems.Add(t_ds.chk_date);
                    itemx1.SubItems.Add(t_ds.chk_name_id);

                    this.m_oper_List.Items.Add(itemx1);
                }
            }
        }

        //削除ボタン
        private void m_deleteBtn_Click(object sender, EventArgs e)
        {
            int count = m_oper_List.SelectedItems.Count;

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
            foreach (ListViewItem item in m_oper_List.SelectedItems)
            {
                m_oper_List.Items.Remove(item);
            }
        }
        //削除
        private int deleteCustomer()
        {

            string operno;
            int ret = 0;
            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "DELETE FROM ope where openo = :no";

            using (var transaction = con.BeginTransaction())
            {
                foreach (ListViewItem item in m_oper_List.SelectedItems)
                {
                    operno = item.SubItems[0].Text;


                    var command = new NpgsqlCommand(@sql, con);
                    command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = int.Parse(operno) });

                    Int32 rowsaffected;
                    try
                    {
                        //削除処理
                        rowsaffected = command.ExecuteNonQuery();
                        

                        if (rowsaffected < 1)
                        {
                            MessageBox.Show("削除できませんでした。オペレータ通番:" + operno, "オペレータ削除");
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
                        MessageBox.Show("オペレータ削除時エラーが発生しました。 " + ex.Message);
                        if (transaction.Connection != null) transaction.Rollback();
                        return -1;
                    }
                }
                if (ret==1)
                {
                    MessageBox.Show("削除完了しました。", "オペレータ削除");
                    transaction.Commit();
                }
            }
            return 1;
        }
        //平文ボタン
        private void button1_Click(object sender, EventArgs e)
        {
            if(m_password.PasswordChar == '*')
                m_password.PasswordChar = '\0';
            else
                m_password.PasswordChar = '*';
        }

        private void m_addressslist_ColumnClick(object sender, ColumnClickEventArgs e)
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
            addressIns.userid = m_openo.Text;
            addressIns.username = m_lastname.Text + m_firstname.Text;
            addressIns.loginDS = loginDS;
            addressIns.Show();
        }

        //メールアドレスのダブルクリック
        private void m_addressslist_DoubleClick(object sender, EventArgs e)
        {

            ListView.SelectedIndexCollection item = m_addressslist.SelectedIndices;

            Form_mailDetail addressDetail = new Form_mailDetail();
            addressDetail.con = con;


            MailaddressDS mailDt = new MailaddressDS();

            mailDt.kubun = "1";
            mailDt.opetantouno = this.m_openo.Text;
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

        //オペレータリストのカラムクリック
        private void m_oper_List_ColumnClick(object sender, ColumnClickEventArgs e)
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
            m_oper_List.Sort();
        }

    }
}
