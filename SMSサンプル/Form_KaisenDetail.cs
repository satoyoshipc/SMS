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

        //ログイン情報
        public opeDS loginDS { get; set; }

        public kaisenDS kaisendt { get; set; }

        //ユーザ情報一覧
        public List<userDS> userList { get; set; }
        //システム情報一覧
        public List<systemDS> systemList { get; set; }

        //DBコネクション
        public NpgsqlConnection con { get; set; }

        //ListViewのソートの際に使用する
        private Class_ListViewColumnSorter _columnSorter;

        public Form_KaisenDetail()
        {
            InitializeComponent();
        }
        
        //表示前処理
        private void Form_InterfaceDetail_Load(object sender, EventArgs e)
        {
            _columnSorter = new Class_ListViewColumnSorter();
            m_kaisenList.ListViewItemSorter = _columnSorter;

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
            m_selectKoumoku.Items.Add("更新日時");
            m_selectKoumoku.Items.Add("更新者");

            getkaisen(kaisendt);

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

            this.m_kaisenID.Text = kaisendt.kaisenid;
            this.m_isp.Text = kaisendt.isp;
            this.m_serviceType.Text = kaisendt.servicetype;
            this.m_serviceID.Text = kaisendt.serviceid;

            this.m_updateOpe.Text = kaisendt.chk_name_id;
            this.m_update.Text = kaisendt.chk_date;

            Class_Detaget dg = new Class_Detaget();
            dg.con = con;
            if (kaisendt.userno != null && kaisendt.userno != "" )
                this.m_cutomername.Text = dg.getCustomername(kaisendt.userno);
            //システム情報
            if (kaisendt.systemno != null && kaisendt.systemno != "" )
                this.m_systemname.Text = dg.getSystemname(kaisendt.systemno);
            //拠点名取得
            if (kaisendt.siteno != null && kaisendt.siteno != "" )
                this.m_sitename.Text = dg.getSitename(kaisendt.siteno);
            //ホスト名取得
            if (kaisendt.host_no != null && kaisendt.host_no != "" )
                this.m_hostname.Text = dg.getHostname(kaisendt.host_no);

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

                        //更新日時
                        case 12:
                            param_dict["chk_date"] = m_selecttext.Text;
                            break;
                        //更新者
                        case 13:
                            param_dict["chk_name_id"] = m_selecttext.Text;
                            break;
                        default:
                            break;

                    }
                }
            }

            //回線一覧を取得する
            dset = dg.getSelectKaisenList(param_dict, con, dset);

            this.m_kaisenList.FullRowSelect = true;
            this.m_kaisenList.HideSelection = false;
            this.m_kaisenList.HeaderStyle = ColumnHeaderStyle.Clickable;


            this.m_kaisenList.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(1, "ステータス", 120, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(2, "キャリア", 90, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(3, "回線種別", 90, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(4, "回線ID", 80, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(5, "ISP", 120, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(6, "サービス種別", 120, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(7, "サービスID", 50, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(8, "カスタマ通番", 50, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(9, "システム通番", 50, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(10, "拠点通番", 50, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(11, "ホスト通番", 50, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(12, "更新日時", 50, HorizontalAlignment.Left);
            this.m_kaisenList.Columns.Insert(13, "更新者", 50, HorizontalAlignment.Left);

            //リストに表示
            if (dset.kaisen_L != null)
            {
                foreach (kaisenDS s_ds in dset.kaisen_L)
                {

                    ListViewItem itemx1 = new ListViewItem();
                    itemx1.Text = s_ds.kaisenno;

                    itemx1.SubItems.Add(s_ds.status);
                    itemx1.SubItems.Add(s_ds.career);
                    itemx1.SubItems.Add(s_ds.type);
                    itemx1.SubItems.Add(s_ds.kaisenid);
                    itemx1.SubItems.Add(s_ds.isp);
                    itemx1.SubItems.Add(s_ds.servicetype);
                    itemx1.SubItems.Add(s_ds.serviceid);
                    itemx1.SubItems.Add(s_ds.userno);
                    itemx1.SubItems.Add(s_ds.systemno);
                    itemx1.SubItems.Add(s_ds.siteno);
                    itemx1.SubItems.Add(s_ds.host_no);
                    itemx1.SubItems.Add(s_ds.chk_date);
                    itemx1.SubItems.Add(s_ds.chk_name_id);

                    this.m_kaisenList.Items.Add(itemx1);
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
            if (m_career.Text == "")
            {
                MessageBox.Show("キャリアを入力して下さい。", "インターフェース名修正", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //確認ダイアログ
            if (MessageBox.Show("回線情報データの更新を行います。よろしいですか？", "回線情報更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            string status = "";
            if (m_statusCombo.Text == "有効")
                status = "1";
            else if (m_statusCombo.Text == "無効")
                status = "0";
            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "update kaisen set status=:status,career=:career,type=:type,kaisenid=:kaisenid,isp=:isp," +
                "servicetype=:servicetype,serviceid=:serviceid,chk_name_id =:ope,chk_date=:chdate where kaisenno = :no";
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
                command.Parameters.Add(new NpgsqlParameter("ope", DbType.String) { Value = loginDS.opeid });
                command.Parameters.Add(new NpgsqlParameter("chdate", DbType.DateTime) { Value = DateTime.Now });
                Int32 rowsaffected;
                try
                {
                    //更新処理
                    rowsaffected = command.ExecuteNonQuery();
                    transaction.Commit();

                    if (rowsaffected != 1)
                        MessageBox.Show("更新できませんでした。", "回線種別");
                    else
                        MessageBox.Show("更新されました。", "回線種別");
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
            kaisendt.userno = this.m_kaisenList.Items[item[0]].SubItems[8].Text;
            kaisendt.systemno = this.m_kaisenList.Items[item[0]].SubItems[9].Text;
            kaisendt.siteno = this.m_kaisenList.Items[item[0]].SubItems[10].Text;
            kaisendt.host_no = this.m_kaisenList.Items[item[0]].SubItems[11].Text;
            kaisendt.chk_date= this.m_kaisenList.Items[item[0]].SubItems[12].Text;
            kaisendt.chk_name_id = this.m_kaisenList.Items[item[0]].SubItems[13].Text;
            getkaisen(kaisendt);
        }
        //一覧のカラムをクリックした時
        private void m_kaisenList_ColumnClick(object sender, ColumnClickEventArgs e)
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
            m_kaisenList.Sort();
        }
        //削除
        private void m_deleteBtn_Click(object sender, EventArgs e)
        {
            int count = m_kaisenList.SelectedItems.Count;

            //確認メッセージ
            if (MessageBox.Show("一覧に選択された行 " + count + "件 の削除を行います。" + Environment.NewLine +
                "よろしいですか？", "回線情報削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;


            int ret = deleteKaisen();
            if (ret == -1)
            {
                return;
            }

            //リストの表示上からけす
            foreach (ListViewItem item in m_kaisenList.SelectedItems)
            {
                m_kaisenList.Items.Remove(item);
            }
        }

        //削除
        private int deleteKaisen()
        {

            string kaisenno;

            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "DELETE FROM kaisen where kaisenno = :no ";

            using (var transaction = con.BeginTransaction())
            {

                foreach (ListViewItem item in m_kaisenList.SelectedItems)
                {
                    kaisenno = item.SubItems[0].Text;


                    var command = new NpgsqlCommand(@sql, con);
                    command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = int.Parse(kaisenno) });

                    Int32 rowsaffected;
                    try
                    {
                        //削除処理
                        rowsaffected = command.ExecuteNonQuery();
                        transaction.Commit();

                        if (rowsaffected != 1)
                        {
                            MessageBox.Show("削除できませんでした。回線ID:" + kaisenno, "回線情報削除");
                            transaction.Rollback();
                            return -1;
                        }
                        else {
                            MessageBox.Show("削除完了しました。回線ID:" + kaisenno, "回線情報削除");
                        }
                    }
                    catch (Exception ex)
                    {
                        //エラー時メッセージ表示
                        MessageBox.Show("回線情報削除時エラーが発生しました。 " + ex.Message);
                        transaction.Rollback();
                        return -1;
                    }
                }

            }
            return 1;
        }
    }
}
