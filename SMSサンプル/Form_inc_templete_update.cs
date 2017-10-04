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
    public partial class Form_inc_templete_update : Form
    {
        public NpgsqlConnection con { get; set; }

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public opeDS loginDS { get; set; }

        //ListViewのソートの際に使用する
        private Class_ListViewColumnSorter _columnSorter;

        //カスタマ
        public List<userDS> userList { get; set; }

        //システム
        public templeteDS templetedt { get; set; }

        //テンプレート
        public List<templeteDS> templList { get; set; }

        //テンプレート一覧
        DataTable templ_list;

        public Form_inc_templete_update()
        {
            InitializeComponent();
        }

        //更新ボタン
        private void m_kousin_btn_Click(object sender, EventArgs e)
        {
            if (m_templete_type_combo.Text == "")
            {
                MessageBox.Show("テンプレート種別を選択して下さい。", "テンプレート更新", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }

            if (m_usernameCombo.Text == "")
            {
                MessageBox.Show("カスタマ名を選択して下さい。", "テンプレート更新", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }

            if (m_templetename.Text == "")
            {
                MessageBox.Show("テンプレート名を入力して下さい。", "テンプレート更新", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (m_content.Text == "")
            {
                MessageBox.Show("テンプレート内容を入力して下さい。", "テンプレート更新", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }



            //確認ダイアログ
            if (MessageBox.Show("テンプレートデータの更新を行います。よろしいですか？", "インシデント情報更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;


            //タイプ
            string templete_type = "";
            if (m_templete_type_combo.Text == "インシデント")
                templete_type = "1";
            else if (m_templete_type_combo.Text == "タスク(インシデントタスク・計画作業)")
                templete_type = "2";


            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "update templete set " +
               "templetetype=:templetetype," +
                "templetename=:templetename," +
                "title=:title," +
                "text=:text," +
                "userno=:userno," +
                "chk_name_id =:ope,chk_date=:chdate " +
                "where templeteno = :no";

            using (var transaction = con.BeginTransaction())
            {
                int userno;
                userno = int.Parse(m_userno.Text);

                var command = new NpgsqlCommand(@sql, con);
                command.Parameters.Add(new NpgsqlParameter("templetetype", DbType.String) { Value = templete_type});
                command.Parameters.Add(new NpgsqlParameter("templetename", DbType.String) { Value = m_templetename.Text });
                command.Parameters.Add(new NpgsqlParameter("title", DbType.String) { Value = m_title.Text });
                command.Parameters.Add(new NpgsqlParameter("text", DbType.String) { Value = m_content.Text });
                command.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = userno });
                command.Parameters.Add(new NpgsqlParameter("ope", DbType.String) { Value = loginDS.opeid });
                command.Parameters.Add(new NpgsqlParameter("chdate", DbType.DateTime) { Value = DateTime.Now });
                command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = int.Parse(m_tempno.Text) });
                Int32 rowsaffected;
                try
                {
                    //更新処理
                    rowsaffected = command.ExecuteNonQuery();
                    transaction.Commit();

                    if (rowsaffected != 1) 
                        MessageBox.Show("更新できませんでした。", "テンプレート更新");
                    else
                        MessageBox.Show("更新されました。", "テンプレート更新");

                }
                catch (Exception ex)
                {
                    //エラー時メッセージ表示
                    MessageBox.Show(ex.Message);
                    logger.ErrorFormat("テンプレート情報更新エラー メソッド名：{0}。テンプレート通番：{1} カスタマ通番：{2}", System.Reflection.MethodBase.GetCurrentMethod().Name, m_userno.Text);
                    transaction.Rollback();
                    return;
                }
            }
        }
        //キャンセルボタン
        private void m_cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        //検索ボタン
        private void m_select_btn_Click(object sender, EventArgs e)
        {
            m_templetelist.Clear();
            DISP_dataSet dset = new DISP_dataSet();
            Dictionary<string, string> param_dict = new Dictionary<string, string>();
            Class_Detaget dg = new Class_Detaget();

            if (m_selecttext.Text != "")
            {

                if (this.m_selectCombo.SelectedIndex.ToString() != "")
                {
                    switch (this.m_selectCombo.SelectedIndex)
                    {
                        //テンプレート通番
                        case 0:
                            param_dict["templeteno"] = m_selecttext.Text;
                            break;
                        //テンプレートタイプ
                        case 1:
                            if (m_selecttext.Text == "インシデント")
                                param_dict["templetetype"] = "1";
                            else if (m_selecttext.Text == "タスク(インシデントタスク・計画作業)")
                                param_dict["templetetype"] = "0";
                            break;
                        //カスタマ通番
                        case 2:
                            param_dict["templetename"] = m_selecttext.Text;
                            break;

                        //タイトル
                        case 3:
                            param_dict["username"] = m_selecttext.Text;
                            break;

                        //本文
                        case 4:
                            param_dict["text"] = m_selecttext.Text;
                            break;

                        
                        case 5:
                            param_dict["title"] = m_selecttext.Text;
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

            //まず件数を取得する
            Int64 count = dg.getTempleteListCount("", param_dict, con);
            if (MessageBox.Show(count.ToString() + "件ヒットしました。表示しますか？", "ホスト", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            //テンプレート一覧を取得する
            templList = dg.selectTempleteList("",param_dict, con);

            this.splitContainer1.SplitterDistance = 210;

            this.m_templetelist.VirtualMode = true;
            // １行全体選択
            this.m_templetelist.FullRowSelect = true;
            this.m_templetelist.HideSelection = false;
            this.m_templetelist.HeaderStyle = ColumnHeaderStyle.Clickable;
            //Hook up handlers for VirtualMode events.
            this.m_templetelist.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(incident_RetrieveVirtualItem);
            this.m_templetelist.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.m_templetelist.Scrollable = true;

            this.m_templetelist.Columns.Insert(0, "テンプレート通番", 30, HorizontalAlignment.Left);
            this.m_templetelist.Columns.Insert(1, "テンプレート種別", 50, HorizontalAlignment.Left);
            this.m_templetelist.Columns.Insert(2, "テンプレート名", 300, HorizontalAlignment.Left);
            this.m_templetelist.Columns.Insert(3, "カスタマ通番", 50, HorizontalAlignment.Left);
            this.m_templetelist.Columns.Insert(4, "タイトル(インシデントのみ)", 80, HorizontalAlignment.Left);
            this.m_templetelist.Columns.Insert(5, "テンプレート内容", 300, HorizontalAlignment.Left);
            this.m_templetelist.Columns.Insert(6, "更新日時", 110, HorizontalAlignment.Left);
            this.m_templetelist.Columns.Insert(7, "更新者", 50, HorizontalAlignment.Left);

            //リストビューを初期化する
            templ_list = new DataTable("table1");
            templ_list.Columns.Add("テンプレート通番", Type.GetType("System.Int32"));
            templ_list.Columns.Add("テンプレート種別", Type.GetType("System.String"));
            templ_list.Columns.Add("テンプレート名", Type.GetType("System.String"));
            templ_list.Columns.Add("カスタマ通番", Type.GetType("System.String"));
            templ_list.Columns.Add("タイトル(インシデントのみ)", Type.GetType("System.String"));
            templ_list.Columns.Add("テンプレート内容", Type.GetType("System.String"));
            templ_list.Columns.Add("更新日時", Type.GetType("System.String"));
            templ_list.Columns.Add("更新者", Type.GetType("System.String"));

            //リストに表示
            if (templList != null)
            {
                m_templetelist.BeginUpdate();
                foreach (templeteDS s_ds in templList)
                {


                    DataRow urow = templ_list.NewRow();
                    urow["テンプレート通番"] = s_ds.templeteno;

                    //1:インシデント 2:計画作業
                    string typestr = "";
                    if (s_ds.templetetype == "1")
                        typestr = "インシデント";
                    else if (s_ds.templetetype == "2")
                        typestr = "タスク(インシデントタスク・計画作業)";

                    urow["テンプレート種別"] = typestr;
                    urow["テンプレート名"] = s_ds.templetename;
                    urow["カスタマ通番"] = s_ds.userno;

                    urow["テンプレート内容"] = s_ds.text;
                    urow["タイトル(インシデントのみ)"] = s_ds.title;

                    urow["更新日時"] = s_ds.chk_date;
                    urow["更新者"] = s_ds.chk_name_id;

                    templ_list.Rows.Add(urow);
                }
                this.m_templetelist.VirtualListSize = templ_list.Rows.Count;
                this.m_templetelist.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                m_templetelist.EndUpdate();
            }
        }
        //　表示前
        private void Form_inc_templete_update_Load(object sender, EventArgs e)
        {
            m_idlabel.Text = loginDS.opeid;
            m_labelinputOpe.Text = loginDS.lastname + loginDS.fastname;

            _columnSorter = new Class_ListViewColumnSorter();
            m_templetelist.ListViewItemSorter = _columnSorter;
            this.splitContainer1.SplitterDistance = 22;

            m_selectCombo.Items.Add("テンプレート通番");
            m_selectCombo.Items.Add("テンプレート種別");
            m_selectCombo.Items.Add("テンプレート名");
            m_selectCombo.Items.Add("カスタマ名");
            m_selectCombo.Items.Add("タイトル(インシデントのみ)");
            m_selectCombo.Items.Add("テンプレート内容");
            
            m_selectCombo.Items.Add("更新日時");
            m_selectCombo.Items.Add("更新者");
            if (templetedt != null)
                getTemplete(templetedt);


            DataTable cutomerTable = new DataTable();
            cutomerTable.Columns.Add("ID", typeof(string));
            cutomerTable.Columns.Add("NAME", typeof(string));

            if (userList == null)
                return;

            //空行の挿入
            DataRow row = cutomerTable.NewRow();
            row["ID"] = "0";
            row["NAME"] = "全てのカスタマ";
            cutomerTable.Rows.Add(row);

            //カスタマ情報を取得する
            foreach (userDS v in userList)
            {
                row = cutomerTable.NewRow();
                row["ID"] = v.userno;
                row["NAME"] = v.username;
                cutomerTable.Rows.Add(row);
            }

            //データテーブルを割り当てる
            m_usernameCombo.DataSource = cutomerTable;
            m_usernameCombo.DisplayMember = "NAME";
            m_usernameCombo.ValueMember = "ID";
            if (cutomerTable.Rows.Count > 0)
                m_userno.Text = m_usernameCombo.SelectedValue.ToString();


        }
        void incident_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            //	e.Item = _item[e.ItemIndex];
            if (templ_list.Rows.Count > 0)
            {

                DataRow row = this.templ_list.Rows[e.ItemIndex];
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
                Convert.ToString(row[7])
                    });
            }

        }
        //テンプレート一覧を取得する
        private void getTemplete(templeteDS templetedt)
        {

            this.m_tempno.Text = templetedt.templeteno;
            if (templetedt.templetetype == "1")
                this.m_templete_type_combo.Text = "インシデント";

            else if (templetedt.templetetype == "2")
                this.m_templete_type_combo.Text = "タスク(インシデントタスク・計画作業)";

            this.m_templetename.Text = templetedt.templetename;
            this.m_userno.Text = templetedt.userno;
            this.m_title.Text = templetedt.title;
            this.m_content.Text = templetedt.text;
            this.m_idlabel.Text = templetedt.chk_date;
            this.m_labelinputOpe.Text = templetedt.chk_name_id;

            Read_CustomerCombo();
            m_usernameCombo.SelectedValue = templetedt.userno;
        }
        private void Read_CustomerCombo()
        {
            //m_userno.Text = "";
            m_usernameCombo.DataSource = null;

            //コンボボックス
            DataTable cutomerTable = new DataTable();
            cutomerTable.Columns.Add("ID", typeof(string));
            cutomerTable.Columns.Add("NAME", typeof(string));

            if (userList == null)
                return;

            //空行を挿入

            //userDS tmp = new userDS();
            //tmp.username = "全てのカスタマ";
            //tmp.userno = "0";
            //cutomerTable.Rows.Add(tmp);

            DataRow row = cutomerTable.NewRow();
            row["ID"] = "0";
            row["NAME"] = "全てのカスタマ";
            cutomerTable.Rows.Add(row);




            //カスタマ情報を取得する
            foreach (userDS v in userList)
            {
                row = cutomerTable.NewRow();
                row["ID"] = v.userno;
                row["NAME"] = v.username;
                cutomerTable.Rows.Add(row);
            }
            //データテーブルを割り当てる
            m_usernameCombo.DataSource = cutomerTable;
            m_usernameCombo.DisplayMember = "NAME";
            m_usernameCombo.ValueMember = "ID";

        }
        //一覧をダブルクリック
        private void m_templetelist_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            ListView.SelectedIndexCollection item = m_templetelist.SelectedIndices;
            templeteDS templetedt = new templeteDS();
            templetedt.templeteno = this.m_templetelist.Items[item[0]].SubItems[0].Text;

            if (this.m_templetelist.Items[item[0]].SubItems[1].Text == "インシデント")
                templetedt.templetetype = "1";
            else if (this.m_templetelist.Items[item[0]].SubItems[1].Text == "タスク(インシデントタスク・計画作業)")
                templetedt.templetetype = "2";

            //templetedt.templetetype = this.m_templetelist.Items[item[0]].SubItems[1].Text;

            templetedt.templetename = this.m_templetelist.Items[item[0]].SubItems[2].Text;
            templetedt.userno = this.m_templetelist.Items[item[0]].SubItems[3].Text;
            templetedt.title = this.m_templetelist.Items[item[0]].SubItems[4].Text;


            templetedt.text = this.m_templetelist.Items[item[0]].SubItems[5].Text;
            templetedt.chk_date = this.m_templetelist.Items[item[0]].SubItems[6].Text;
            templetedt.chk_name_id = this.m_templetelist.Items[item[0]].SubItems[7].Text;

            getTemplete(templetedt);

        }

        //カスタマ名コンボボックスの変更がされた場合
        private void m_usernameCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (m_usernameCombo.Text == "")
            {
                m_userno.Text = "";
                return;
            }
            //コンボボックス
            if (m_usernameCombo.SelectedValue != null)
                m_userno.Text = m_usernameCombo.SelectedValue.ToString();
        }
        //削除
        private void m_deleteBtn_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection item = m_templetelist.SelectedIndices;
            int count = item.Count;

            //確認メッセージ
            if (MessageBox.Show("一覧に選択された行 " + count + "件 の削除を行います。" + Environment.NewLine +
                "よろしいですか？", "インシデント/タスクテンプレート削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int ret = deletetemplete(item);
            if (ret == -1)
            {
                return;
            }
            //リストの表示上からけす
            int i = 0;
            //削除するテンプレート番号の取得
            int[] indices = new int[item.Count];
            int cnt = m_templetelist.SelectedIndices.Count;


            m_templetelist.SelectedIndices.CopyTo(indices, 0);

            DataRowCollection items = templ_list.Rows;
            for (i = cnt - 1; i >= 0; --i)
                items.RemoveAt(indices[i]);

            //総件数を変更し再表示を行う
            this.m_templetelist.VirtualListSize = items.Count;
        }

        //削除
        private int deletetemplete(ListView.SelectedIndexCollection item)
        {
            int ret = 0;
            string templeteno;

            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "DELETE FROM templete where templeteno = :no ";

            using (var transaction = con.BeginTransaction())
            {

                int i = 0;
                for (i = 0; i < item.Count; i++)
                {
                    templeteno = this.m_templetelist.Items[item[i]].SubItems[0].Text;

                    var command = new NpgsqlCommand(@sql, con);
                    command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = int.Parse(templeteno) });

                    Int32 rowsaffected;
                    try
                    {
                        //削除処理
                        rowsaffected = command.ExecuteNonQuery();

                        if (rowsaffected < 1)
                        {
                            transaction.Rollback();
                            MessageBox.Show("削除できませんでした。テンプレートNO:" + templeteno, "テンプレート削除");

                            ret = -1;
                        }
                        else
                        {
                            ret = 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        //エラー時メッセージ表示
                        MessageBox.Show("テンプレート削除時エラーが発生しました。 " + ex.Message);
                        ret = -1;
                    }
                }
                if (ret == 1)
                {
                    transaction.Commit();

                    MessageBox.Show("削除完了しました。", "テンプレート削除");
                    logger.InfoFormat("テンプレート削除完了。 テンプレート");
                }
            }
            return ret;
        }


    }
}