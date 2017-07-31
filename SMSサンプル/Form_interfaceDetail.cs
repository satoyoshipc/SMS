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
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
        private int sort_kind = 0;

        //監視インターフェイスの一覧
        DataTable interface_list;

        public Form_interfaceDetail()
        {
            InitializeComponent();
        }


        void InterfaceList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            //	e.Item = _item[e.ItemIndex];
            if (interface_list.Rows.Count > 0)
            {

                DataRow row = this.interface_list.Rows[e.ItemIndex];
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
                Convert.ToString(row[15])
                    });
            }

        }
        //表示前処理
        private void Form_InterfaceDetail_Load(object sender, EventArgs e)
        {


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
            if(interfacedt != null)
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
                            DateTime dt;
                            String str = m_selecttext.Text;

                            //入力された日付の形式の確認
                            if (DateTime.TryParse(str, out dt))
                            {
                                param_dict["start_date"] = str;
                            }
                            else
                            {
                                MessageBox.Show("日付の形式が正しくありません。", "監視インターフェイス検索");
                                return;
                            }

                            break;

                        case 6:

                            str = m_selecttext.Text;

                            //入力された日付の形式の確認
                            if (DateTime.TryParse(str, out dt))
                            {
                                param_dict["end_date"] = str;
                            }
                            else
                            {
                                MessageBox.Show("日付の形式が正しくありません。", "監視インターフェイス検索");
                                return;
                            }
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

                            str = m_selecttext.Text;

                            //入力された日付の形式の確認
                            if (DateTime.TryParse(str, out dt))
                                param_dict["chk_date"] = str;
                            else
                            {
                                MessageBox.Show("日付の形式が正しくありません。", "監視インターフェイス検索");
                                return;
                            }
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
            //まず件数を取得する
            Int64 count = dg.getSelectInterfaceCount(param_dict, con, dset, true);
            if(MessageBox.Show(count.ToString() + "件ヒットしました。表示しますか？","監視インターフェイス",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            //インターフェイス一覧を取得する
            dset = dg.getSelectInterface(param_dict, con, dset,true);

            this.m_InterfaceList.VirtualMode = true;
            // １行全体選択
            this.m_InterfaceList.FullRowSelect = true;
            this.m_InterfaceList.HideSelection = false;
            this.m_InterfaceList.HeaderStyle = ColumnHeaderStyle.Clickable;
            //Hook up handlers for VirtualMode events.
            this.m_InterfaceList.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(InterfaceList_RetrieveVirtualItem);
            this.m_InterfaceList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.m_InterfaceList.Scrollable = true;


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

            //リストビューを初期化する
            interface_list = new DataTable("table1");
            interface_list.Columns.Add("No", Type.GetType("System.Int32"));
            interface_list.Columns.Add("インターフェイス名", Type.GetType("System.String"));
            interface_list.Columns.Add("ステータス", Type.GetType("System.String"));
            interface_list.Columns.Add("監視タイプ", Type.GetType("System.String"));
            interface_list.Columns.Add("監視項目名", Type.GetType("System.String"));
            interface_list.Columns.Add("監視開始日時", Type.GetType("System.String"));
            interface_list.Columns.Add("監視終了日時", Type.GetType("System.String"));
            interface_list.Columns.Add("閾値", Type.GetType("System.String"));
            interface_list.Columns.Add("IPアドレス", Type.GetType("System.String"));
            interface_list.Columns.Add("IPアドレス(NAT)", Type.GetType("System.String"));
            interface_list.Columns.Add("カスタマ番号", Type.GetType("System.String"));
            interface_list.Columns.Add("システム通番番号", Type.GetType("System.String"));
            interface_list.Columns.Add("拠点通番", Type.GetType("System.String"));
            interface_list.Columns.Add("ホスト通番", Type.GetType("System.String"));
            interface_list.Columns.Add("更新日時", Type.GetType("System.String"));
            interface_list.Columns.Add("更新者", Type.GetType("System.String"));



            //リストに表示
            if (dset.watch_L != null)
            {
                m_InterfaceList.BeginUpdate();

                foreach (watch_InterfaceDS s_ds in dset.watch_L)
                {
                    DataRow urow = interface_list.NewRow();

                    urow["No"] = s_ds.watch_Interfaceno;
                    urow["インターフェイス名"] = s_ds.interfacename;
                    urow["ステータス"] = s_ds.status;
                    urow["監視タイプ"] = s_ds.type;
                    urow["監視項目名"] = s_ds.kanshi;
                    urow["監視開始日時"] = s_ds.start_date;
                    urow["監視終了日時"] = s_ds.end_date;
                    urow["閾値"] = s_ds.border;
                    urow["IPアドレス"] =s_ds.IPaddress; 
                    urow["IPアドレス(NAT)"] = s_ds.IPaddressNAT;
                    urow["カスタマ番号"] = s_ds.userno;
                    urow["システム通番番号"] = s_ds.systemno;
                    urow["拠点通番"] = s_ds.siteno;
                    urow["ホスト通番"] = s_ds.host_no;
                    urow["更新日時"] = s_ds.chk_date;
                    urow["更新者"] = s_ds.chk_name_id;
                    interface_list.Rows.Add(urow);

                }
                this.m_InterfaceList.VirtualListSize = interface_list.Rows.Count;
                this.m_InterfaceList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                m_InterfaceList.EndUpdate();
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

            if (this.interface_list == null)
                return;
            if (this.interface_list.Rows.Count <= 0)
                return;
            //DataViewクラス ソートするためのクラス
            DataView dv = new DataView(interface_list);

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
            dttmp = interface_list.Clone();
            //ソートを実行
            dv.Sort = interface_list.Columns[e.Column].ColumnName + strSort;

            // ソートされたレコードのコピー
            foreach (DataRowView drv in dv)
            {
                // 一時テーブルに格納
                dttmp.ImportRow(drv.Row);
            }
            //格納したテーブルデータを上書く
            interface_list = dttmp.Copy();

            //行が存在するかチェックを行う。
            if (this.m_InterfaceList.TopItem != null)
            {
                //現在一番上の行に表示されている行を取得
                int start = m_InterfaceList.TopItem.Index;
                // ListView画面の再表示を行う
                m_InterfaceList.RedrawItems(start, m_InterfaceList.Items.Count - 1, true);
            }
        }
        //削除処理
        private void m_deleteBtn_Click(object sender, EventArgs e)
        {

                ListView.SelectedIndexCollection item = m_InterfaceList.SelectedIndices;
                int count = item.Count;

            //確認メッセージ
            if (MessageBox.Show("一覧に選択された行 " + count + "件 の削除を行います。" + Environment.NewLine +
                "よろしいですか？", "監視インターフェイス削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            
            int ret = deleteInterface( item);
            if (ret == -1)
            {
                return;
            }
            //リストの表示上からけす
            int i = 0;
            //削除するインターフェイス番号の取得
            int[] indices = new int[item.Count];
            int cnt = m_InterfaceList.SelectedIndices.Count;


            m_InterfaceList.SelectedIndices.CopyTo(indices, 0);
            
            DataRowCollection items = interface_list.Rows;
            for (i = cnt - 1; i >=0;-- i)
                items.RemoveAt(indices[i]);

            //総件数を変更し再表示を行う
            this.m_InterfaceList.VirtualListSize = items.Count;
        }
        //削除
        private int deleteInterface(ListView.SelectedIndexCollection item)
        {
            int ret = 0;
            string interfaceno;

            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "DELETE FROM watch_interface where kennshino = :no ";

            using (var transaction = con.BeginTransaction())
            {

                int i = 0;
                for (i = 0; i < item.Count; i++)
                {
                    interfaceno = this.m_InterfaceList.Items[item[i]].SubItems[0].Text; 

                    var command = new NpgsqlCommand(@sql, con);
                    command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = int.Parse(interfaceno) });

                    Int32 rowsaffected;
                    try
                    {
                        //削除処理
                        rowsaffected = command.ExecuteNonQuery();

                        if (rowsaffected < 1)
                        {
                            transaction.Rollback();
                            MessageBox.Show("削除できませんでした。監視インターフェイスID:" + interfaceno, "監視インターフェイス削除");

                            ret = -1;
                        }
                        else {
                            ret = 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        //エラー時メッセージ表示
                        MessageBox.Show("監視インターフェイス削除時エラーが発生しました。 " + ex.Message);
                        ret = -1;
                    }
                }
                if (ret == 1)
                {
                    transaction.Commit();

                    MessageBox.Show("削除完了しました。", "監視インターフェイス削除");
                    logger.InfoFormat("監視インターフェイス削除完了。 監視インターフェイス");
                }
            }
            return ret;
        }
    }
}
