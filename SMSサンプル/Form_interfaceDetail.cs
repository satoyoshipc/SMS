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
    public partial class Form_interfaceDetail : Form
    {

        //ログイン情報
        public opeDS loginDS { get; set; }

        public watch_InterfaceDS interfacedt { get; set; }

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

        public Form_interfaceDetail()
        {
            InitializeComponent();
        }
        
        //表示前処理
        private void Form_InterfaceDetail_Load(object sender, EventArgs e)
        {
            //ソータを使用する
            _columnSorter = new Class_ListViewColumnSorter();
            m_InterfaceList.ListViewItemSorter = _columnSorter;

            m_selectKoumoku.Items.Add("監視インターフェイス通番");
            m_selectKoumoku.Items.Add("インターフェイス名");
            m_selectKoumoku.Items.Add("ステータス");
            m_selectKoumoku.Items.Add("監視タイプ");
            m_selectKoumoku.Items.Add("監視項目名");
            m_selectKoumoku.Items.Add("監視開始日時");
            m_selectKoumoku.Items.Add("監視終了日時");
            m_selectKoumoku.Items.Add("閾値");
            m_selectKoumoku.Items.Add("IPアドレス");
            m_selectKoumoku.Items.Add("IPアドレス(NAT)");
            m_selectKoumoku.Items.Add("カスタマ通番");
            m_selectKoumoku.Items.Add("システム通番");
            m_selectKoumoku.Items.Add("拠点通番");
            m_selectKoumoku.Items.Add("ホスト番号");
            m_selectKoumoku.Items.Add("更新日時");
            m_selectKoumoku.Items.Add("更新者");

            getInterface(interfacedt);

        }
        //インターフェイス情報を表示する
        private void getInterface(watch_InterfaceDS interfacedt)
        {
            this.m_interfaceno.Text = interfacedt.watch_Interfaceno;
            this.m_hostno.Text = interfacedt.host_no;
            this.m_userno.Text = interfacedt.userno;
            this.m_systemno.Text = interfacedt.systemno;
            this.m_siteno.Text = interfacedt.siteno;
            this.m_interfaceName.Text = interfacedt.interfacename;
            this.m_statusCombo.Text = interfacedt.status;
            this.m_watchtype.Text = interfacedt.type;

            this.m_koumoku.Text = interfacedt.kanshi;
            this.m_start_date.Text = interfacedt.start_date;
            this.m_end_date.Text = interfacedt.end_date;
            this.m_sikiiti.Text = interfacedt.border;
            this.m_addressIP.Text = interfacedt.IPaddress;
            this.m_addressNAT.Text = interfacedt.IPaddressNAT;

            this.m_updateOpe.Text = interfacedt.chk_name_id;
            this.m_update.Text = interfacedt.chk_date;

            Class_Detaget dg = new Class_Detaget();
            dg.con = con;
            if (interfacedt.userno != null && interfacedt.userno != "")
                this.m_cutomername.Text = dg.getCustomername(interfacedt.userno);
            //システム情報
            if (interfacedt.systemno != null && interfacedt.systemno != "")
                this.m_systemname.Text = dg.getSystemname(interfacedt.systemno);
            //拠点名取得
            if (interfacedt.siteno != null && interfacedt.siteno != "")
                this.m_sitename.Text = dg.getSitename(interfacedt.siteno);
            //ホスト名取得
            if (interfacedt.host_no != null && interfacedt.host_no != "")
                this.m_hostname.Text = dg.getHostname(interfacedt.host_no);

        }
        //検索ボタン
        private void m_selectBtn_Click(object sender, EventArgs e)
        {
            m_InterfaceList.Clear();
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
                            param_dict["kennshino"] = m_selecttext.Text;
                            break;
                        //ホスト名
                        case 1:
                            param_dict["interfacename"] = m_selecttext.Text;
                            break;
                        case 2:
                            if(m_selecttext.Text == "無効")
                                param_dict["status"] = "0";
                            else if (  m_selecttext.Text == "有効")
                                param_dict["status"] = "1";
                            break;

                        case 3:
                            param_dict["type"] = m_selecttext.Text;
                            break;

                        case 4:
                            param_dict["kanshi"] = m_selecttext.Text;
                            break;
    
                        case 5:
                            param_dict["start_date"] = m_selecttext.Text;
                            break;

                        case 6:
                            param_dict["end_date"] = m_selecttext.Text;
                            break;

                        case 7:
                            param_dict["border"] = m_selecttext.Text;
                            break;
                        case 8:
                            param_dict["IPaddress"] = m_selecttext.Text;
                            break;
                        case 9:
                            param_dict["IPaddressNAT"] = m_selecttext.Text;
                            break;
                        case 10:
                            param_dict["userno"] = m_selecttext.Text;
                            break;
                        case 11:
                            param_dict["systemno"] = m_selecttext.Text;
                            break;
                        case 12:
                            param_dict["siteno"] = m_selecttext.Text;
                            break;
                        case 13:
                            param_dict["host_no"] = m_selecttext.Text;
                            break;

                        //更新日時
                        case 14:
                            param_dict["chk_date"] = m_selecttext.Text;
                            break;
                        //更新者
                        case 15:
                            param_dict["chk_name_id"] = m_selecttext.Text;
                            break;
                        default:
                            break;

                    }
                }
            }

            //インターフェイス一覧を取得する
            dset = dg.getSelectInterface(param_dict, con, dset,true);

            this.m_InterfaceList.FullRowSelect = true;
            this.m_InterfaceList.HideSelection = false;
            this.m_InterfaceList.HeaderStyle = ColumnHeaderStyle.Clickable;

            this.m_InterfaceList.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_InterfaceList.Columns.Insert(1, "インターフェイス名", 120, HorizontalAlignment.Left);
            this.m_InterfaceList.Columns.Insert(2, "ステータス", 90, HorizontalAlignment.Left);
            this.m_InterfaceList.Columns.Insert(3, "監視タイプ", 90, HorizontalAlignment.Left);
            this.m_InterfaceList.Columns.Insert(4, "監視項目名", 80, HorizontalAlignment.Left);
            this.m_InterfaceList.Columns.Insert(5, "監視開始日時", 120, HorizontalAlignment.Left);
            this.m_InterfaceList.Columns.Insert(6, "監視終了日時", 120, HorizontalAlignment.Left);
            this.m_InterfaceList.Columns.Insert(7, "閾値", 50, HorizontalAlignment.Left);
            this.m_InterfaceList.Columns.Insert(8, "IPアドレス", 50, HorizontalAlignment.Left);
            this.m_InterfaceList.Columns.Insert(9, "IPアドレス(NAT)", 50, HorizontalAlignment.Left);
            this.m_InterfaceList.Columns.Insert(10, "カスタマ番号", 50, HorizontalAlignment.Left);
            this.m_InterfaceList.Columns.Insert(11, "システム通番番号", 50, HorizontalAlignment.Left);
            this.m_InterfaceList.Columns.Insert(12, "拠点通番", 50, HorizontalAlignment.Left);
            this.m_InterfaceList.Columns.Insert(13, "ホスト通番", 50, HorizontalAlignment.Left);
            this.m_InterfaceList.Columns.Insert(14, "更新日時", 50, HorizontalAlignment.Left);
            this.m_InterfaceList.Columns.Insert(15, "更新者", 50, HorizontalAlignment.Left);

            //リストに表示
            if (dset.watch_L != null)
            {
                foreach (watch_InterfaceDS s_ds in dset.watch_L)
                {

                    ListViewItem itemx1 = new ListViewItem();
                    itemx1.Text = s_ds.watch_Interfaceno;

                    itemx1.SubItems.Add(s_ds.interfacename);
                    itemx1.SubItems.Add(s_ds.status);
                    itemx1.SubItems.Add(s_ds.type);
                    itemx1.SubItems.Add(s_ds.kanshi);
                    itemx1.SubItems.Add(s_ds.start_date);
                    itemx1.SubItems.Add(s_ds.end_date);
                    itemx1.SubItems.Add(s_ds.border);
                    itemx1.SubItems.Add(s_ds.IPaddress);
                    itemx1.SubItems.Add(s_ds.IPaddressNAT);
                    itemx1.SubItems.Add(s_ds.userno);
                    itemx1.SubItems.Add(s_ds.systemno);
                    itemx1.SubItems.Add(s_ds.siteno);
                    itemx1.SubItems.Add(s_ds.host_no);
                    itemx1.SubItems.Add(s_ds.chk_date);
                    itemx1.SubItems.Add(s_ds.chk_name_id);

                    this.m_InterfaceList.Items.Add(itemx1);
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
            if (m_interfaceName.Text == "")
            {
                MessageBox.Show("インターフェース名を入力して下さい。", "インターフェース名修正", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //確認ダイアログ
            if (MessageBox.Show("監視インターフェースデータの更新を行います。よろしいですか？", "監視インターフェースデータ更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            string status = "";
            if (m_statusCombo.Text == "有効")
                status = "1";
            else if (m_statusCombo.Text == "無効")
                status = "0";
            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "update watch_Interface set kennshino=:kennshino,interfacename=:interfacename,status=:status,type=:type,kanshi=:kanshi,start_date=:start_date," +
                "end_date=:end_date,border=:border,IPaddress=:IPaddress,IPaddressNAT=:IPaddressNAT," +
                "chk_name_id =:ope,chk_date=:chdate where kennshino = :no";
            using (var transaction = con.BeginTransaction())
            {
                var command = new NpgsqlCommand(@sql, con);
                command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = m_interfaceno.Text });
                command.Parameters.Add(new NpgsqlParameter("kennshino", DbType.Int32) { Value = m_interfaceno.Text });
                command.Parameters.Add(new NpgsqlParameter("interfacename", DbType.String) { Value = m_interfaceName.Text });
                command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                command.Parameters.Add(new NpgsqlParameter("type", DbType.String) { Value = m_watchtype.Text });
                command.Parameters.Add(new NpgsqlParameter("kanshi", DbType.String) { Value = m_koumoku.Text });
                command.Parameters.Add(new NpgsqlParameter("start_date", DbType.DateTime) { Value = m_start_date.Value });
                command.Parameters.Add(new NpgsqlParameter("end_date", DbType.DateTime) { Value = m_end_date.Value });
                command.Parameters.Add(new NpgsqlParameter("border", DbType.String) { Value = m_sikiiti.Text});
                command.Parameters.Add(new NpgsqlParameter("IPaddress", DbType.String) { Value = m_addressIP.Text });
                command.Parameters.Add(new NpgsqlParameter("IPaddressNAT", DbType.String) { Value = m_addressNAT.Text });
                command.Parameters.Add(new NpgsqlParameter("ope", DbType.String) { Value = loginDS.opeid });
                command.Parameters.Add(new NpgsqlParameter("chdate", DbType.DateTime) { Value = DateTime.Now });
                Int32 rowsaffected;
                try
                {
                    //更新処理
                    rowsaffected = command.ExecuteNonQuery();
                    transaction.Commit();

                    if (rowsaffected != 1)
                        MessageBox.Show("更新できませんでした。", "監視インターフェイス更新");
                    else
                        MessageBox.Show("更新されました。", "監視インターフェイス更新");
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
            ListView.SelectedIndexCollection item = m_InterfaceList.SelectedIndices;
            watch_InterfaceDS interfacedt = new watch_InterfaceDS();
            interfacedt.watch_Interfaceno = this.m_InterfaceList.Items[item[0]].SubItems[0].Text;
            interfacedt.interfacename = this.m_InterfaceList.Items[item[0]].SubItems[1].Text;
            interfacedt.status = this.m_InterfaceList.Items[item[0]].SubItems[2].Text;
            interfacedt.type = this.m_InterfaceList.Items[item[0]].SubItems[3].Text;
            interfacedt.kanshi = this.m_InterfaceList.Items[item[0]].SubItems[4].Text;
            interfacedt.start_date = this.m_InterfaceList.Items[item[0]].SubItems[5].Text;
            interfacedt.end_date = this.m_InterfaceList.Items[item[0]].SubItems[6].Text;
            interfacedt.border = this.m_InterfaceList.Items[item[0]].SubItems[7].Text;
            interfacedt.IPaddress = this.m_InterfaceList.Items[item[0]].SubItems[8].Text;
            interfacedt.IPaddressNAT = this.m_InterfaceList.Items[item[0]].SubItems[9].Text;
            interfacedt.userno = this.m_InterfaceList.Items[item[0]].SubItems[10].Text;

            interfacedt.systemno = this.m_InterfaceList.Items[item[0]].SubItems[11].Text;
            interfacedt.siteno = this.m_InterfaceList.Items[item[0]].SubItems[12].Text;
            interfacedt.host_no = this.m_InterfaceList.Items[item[0]].SubItems[13].Text;
            interfacedt.chk_date = this.m_InterfaceList.Items[item[0]].SubItems[14].Text;
            interfacedt.chk_name_id = this.m_InterfaceList.Items[item[0]].SubItems[15].Text;

            getInterface(interfacedt);
        }

        //一覧リストのカラムをクリックしたとき ソート
        private void m_InterfaceList_ColumnClick(object sender, ColumnClickEventArgs e)
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
            m_InterfaceList.Sort();
        }
        //削除処理
        private void m_deleteBtn_Click(object sender, EventArgs e)
        {
            int count = m_InterfaceList.SelectedItems.Count;

            //確認メッセージ
            if (MessageBox.Show("一覧に選択された行 " + count + "件 の削除を行います。" + Environment.NewLine +
                "よろしいですか？", "監視インターフェイス削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;


            int ret = deleteInterface();
            if (ret == -1)
            {
                return;
            }
            //リストの表示上からけす
            foreach (ListViewItem item in m_InterfaceList.SelectedItems)
            {
                m_InterfaceList.Items.Remove(item);
            }
        }
        //削除
        private int deleteInterface()
        {

            string interfaceno;

            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "DELETE FROM watch_interface where kennshino = :no ";

            using (var transaction = con.BeginTransaction())
            {

                foreach (ListViewItem item in m_InterfaceList.SelectedItems)
                {
                    interfaceno = item.SubItems[0].Text;


                    var command = new NpgsqlCommand(@sql, con);
                    command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = int.Parse(interfaceno) });

                    Int32 rowsaffected;
                    try
                    {
                        //削除処理
                        rowsaffected = command.ExecuteNonQuery();
                        transaction.Commit();

                        if (rowsaffected != 1)
                        {
                            MessageBox.Show("削除できませんでした。監視インターフェイスID:" + interfaceno, "監視インターフェイス削除");
                            transaction.Rollback();
                            return -1;
                        }
                        else {
                            MessageBox.Show("削除完了しました。監視インターフェイスID:" + interfaceno, "監視インターフェイス削除");
                        }
                    }
                    catch (Exception ex)
                    {
                        //エラー時メッセージ表示
                        MessageBox.Show("監視インターフェイス削除時エラーが発生しました。 " + ex.Message);
                        transaction.Rollback();
                        return -1;
                    }
                }

            }
            return 1;
        }
    }
}
