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
    public partial class Form_incidentDetail : Form
    {

        //ログイン情報
        public opeDS loginDS { get; set; }

        public incidentDS incidentdt { get; set; }

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

        public Form_incidentDetail()
        {
            InitializeComponent();
        }

        //表示前処理
        private void Form_InterfaceDetail_Load(object sender, EventArgs e)
        {

            //インシデントソータを使用する
            _columnSorter = new Class_ListViewColumnSorter();
            m_incidentList.ListViewItemSorter = _columnSorter;

            m_selectKoumoku.Items.Add("インシデント番号");
            m_selectKoumoku.Items.Add("ステータス");
            m_selectKoumoku.Items.Add("MPMSインシデント番号");
            m_selectKoumoku.Items.Add("S-cude事例ID");
            m_selectKoumoku.Items.Add("インシデント区分");
            m_selectKoumoku.Items.Add("インシデント内容(タイトル)");
            m_selectKoumoku.Items.Add("MAT対応");
            m_selectKoumoku.Items.Add("MAT対応コマンド");
            m_selectKoumoku.Items.Add("受付日時");
            m_selectKoumoku.Items.Add("手配日時");
            m_selectKoumoku.Items.Add("復旧日時");
            m_selectKoumoku.Items.Add("完了日時");
            m_selectKoumoku.Items.Add("タイマー");
            m_selectKoumoku.Items.Add("要確認メッセージ");
            m_selectKoumoku.Items.Add("カスタマ通番");
            m_selectKoumoku.Items.Add("システム通番");
            m_selectKoumoku.Items.Add("拠点通番");
            m_selectKoumoku.Items.Add("ホスト番号");
            m_selectKoumoku.Items.Add("更新日時");
            m_selectKoumoku.Items.Add("更新者");
            if(incidentdt != null)
                getIncident(incidentdt);

        }

        //インシデント情報を表示する
        private void getIncident(incidentDS incidentdt)
        {
            this.m_incidentno.Text = incidentdt.incident_no;
            this.m_userno.Text = incidentdt.userno;
            this.m_systemno.Text = incidentdt.systemno;
            this.m_siteno.Text = incidentdt.siteno;
            this.m_hostno.Text = incidentdt.hostno;
            this.m_mpmsno.Text = incidentdt.mpms_incident;
            this.m_scubeno.Text = incidentdt.s_cube_id;
            this.m_incidentnaiyou.Text = incidentdt.content;

            this.m_statusCombo.Text = incidentdt.status;

            //アラーム検知 障害申告 問い合わせ
            int outputdata;
            if (int.TryParse(incidentdt.incident_type, out outputdata))
                this.m_incidentKBN.SelectedIndex = outputdata - 1;


            if (incidentdt.uketukedate != null || incidentdt.uketukedate != "")
            {
                m_uketukedate.Checked = true;
                this.m_uketukedate.Text = incidentdt.uketukedate;
            }
            else
                m_uketukedate.Checked = false;

            if (incidentdt.tehaidate != null || incidentdt.tehaidate != "")
            {
                m_tehaidate.Checked = true;
                this.m_tehaidate.Text = incidentdt.tehaidate;
            }
            else
                m_tehaidate.Checked = false;

            if (incidentdt.fukyudate != null || incidentdt.fukyudate != "")
            {
                m_fukkyudate.Checked = true;
                this.m_fukkyudate.Text = incidentdt.fukyudate;
            }
            else
                m_fukkyudate.Checked = false;

            if (incidentdt.enddate != null || incidentdt.enddate != "")
            {
                m_enddate.Checked = true;
                this.m_enddate.Text = incidentdt.enddate;
            }
            else
                m_enddate.Checked = false;


            if (incidentdt.matflg == "1")

                m_MATchkbox.Checked = true;
            else
                m_MATchkbox.Checked = false;

            this.m_MATCommannd.Text = incidentdt.matcommand;

            this.m_timer.Text = incidentdt.timer;


            if (incidentdt.timer != null || incidentdt.timer != "")
            {
                m_timer.Checked = true;
                this.m_timer.Text = incidentdt.timer;
            }
            else
                m_enddate.Checked = false;
            m_youkakunin.Text = incidentdt.kakuninmsg;


            this.m_updateOpe.Text = incidentdt.chk_name_id;
            this.m_update.Text = incidentdt.chk_date;

            Class_Detaget dg = new Class_Detaget();
            dg.con = con;
            if (incidentdt.userno != null && incidentdt.userno != "")
                this.m_cutomername.Text = dg.getCustomername(incidentdt.userno);
            //システム情報
            if (incidentdt.systemno != null && incidentdt.systemno != "")
                this.m_systemname.Text = dg.getSystemname(incidentdt.systemno);
            //拠点名取得
            if (incidentdt.siteno != null && incidentdt.siteno != "")
                this.m_sitename.Text = dg.getSitename(incidentdt.siteno);
            //ホスト名取得
            if (incidentdt.hostno != null && incidentdt.hostno != "")
                this.m_hostname.Text = dg.getHostname(incidentdt.hostno);

        }
        //検索ボタン
        private void m_selectBtn_Click(object sender, EventArgs e)
        {
            m_incidentList.Clear();
            List<incidentDS> incidentdsList = new List<incidentDS>();
            Dictionary<string, string> param_dict = new Dictionary<string, string>();
            Class_Detaget dg = new Class_Detaget();

            if (m_selecttext.Text != "")
            {

                if (this.m_selectKoumoku.SelectedIndex.ToString() != "")
                {
                    switch (this.m_selectKoumoku.SelectedIndex)
                    {

                        case 0:
                            param_dict["incident_no"] = m_selecttext.Text;
                            break;

                        case 1:
                            if (m_selecttext.Text == "未完了")
                                param_dict["status"] = "1";
                            else if (m_selecttext.Text == "完了")
                                param_dict["status"] = "0";
                            break;

                        case 2:
                            param_dict["mpms_incident"] = m_selecttext.Text;
                            break;

                        case 3:
                            param_dict["s_cube_id"] = m_selecttext.Text;
                            break;

                        case 4:


                            if (m_selecttext.Text == "アラーム検知")
                                param_dict["incident_type"] = "1";
                            else if (m_selecttext.Text == "障害申告")
                                param_dict["incident_type"] = "2";

                            else if (m_selecttext.Text == "問い合わせ")
                                param_dict["incident_type"] = "3";
                            
                            break;

                        case 5:
                            param_dict["content"] = m_selecttext.Text;
                            break;

                        case 6:

                            if (m_selecttext.Text == "有")
                                param_dict["matflg"] =  "1";
                            else if (m_selecttext.Text == "無")
                                param_dict["matflg"] = "0";

                            break;

                        case 7:
                            param_dict["matcommand"] = m_selecttext.Text;
                            break;
                        case 8:
                            param_dict["uketukedate"] = m_selecttext.Text;
                            break;
                        case 9:
                            param_dict["tehaidate"] = m_selecttext.Text;

                            break;
                        case 10:
                            param_dict["fukyudate"] = m_selecttext.Text;
                            break;
                        case 11:
                            param_dict["enddate"] = m_selecttext.Text;
                            break;


                        case 12:
                            param_dict["timer"] = m_selecttext.Text;
                            break;
                        case 13:
                            param_dict["kakunin"] = m_selecttext.Text;
                            break;
                        case 14:
                            param_dict["userno"] = m_selecttext.Text;
                            break;

                        case 15:
                            param_dict["systemno"] = m_selecttext.Text;
                            break;
                        case 16:
                            param_dict["siteno"] = m_selecttext.Text;
                            break;
                        case 17:
                            param_dict["hostno"] = m_selecttext.Text;
                            break;

                        //更新日時
                        case 18:
                            param_dict["chk_date"] = m_selecttext.Text;
                            break;
                        //更新者
                        case 19:
                            param_dict["chk_name_id"] = m_selecttext.Text;
                            break;
                        default:
                            break;
                    }
                }
            }

            //インシデント一覧を取得する
            incidentdsList = dg.getIncidentList((Form_MainList)this.Owner, param_dict, con);

            this.m_incidentList.FullRowSelect = true;
            this.m_incidentList.HideSelection = false;
            this.m_incidentList.HeaderStyle = ColumnHeaderStyle.Clickable;

            this.m_incidentList.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(1, "ステータス", 50, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(2, "MPMSインシデント番号", 90, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(3, "S-cude事例ID", 90, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(4, "インシデント区分", 80, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(5, "インシデント内容(タイトル)", 120, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(6, "MAT対応", 50, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(7, "MAT対応コマンド", 120, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(8, "受付日時", 50, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(9, "手配日時", 50, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(10, "復旧日時", 50, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(11, "完了日時", 50, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(12, "タイマー", 50, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(13, "要確認メッセージ", 50, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(14, "カスタマ番号", 50, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(15, "システム通番番号", 50, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(16, "拠点通番", 50, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(17, "ホスト通番", 50, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(18, "更新日時", 80, HorizontalAlignment.Left);
            this.m_incidentList.Columns.Insert(19, "更新者", 50, HorizontalAlignment.Left);

            //リストに表示
            if (incidentdsList != null)
            {
                foreach (incidentDS s_ds in incidentdsList)
                {

                    ListViewItem itemx1 = new ListViewItem();
                    itemx1.Text = s_ds.incident_no;

                    itemx1.SubItems.Add(s_ds.status);
                    itemx1.SubItems.Add(s_ds.mpms_incident);
                    itemx1.SubItems.Add(s_ds.s_cube_id);

                    //インシデントのタイプの取得
                    if (s_ds.incident_type == "1")

                        //1:アラーム検知 2:障害申告 3:問い合わせ
                        itemx1.SubItems.Add("アラーム検知");

                    else if (s_ds.incident_type == "2")

                        itemx1.SubItems.Add("障害申告");

                    else if (s_ds.incident_type == "3")

                        itemx1.SubItems.Add("問い合わせ");
                    else
                        itemx1.SubItems.Add("");


                    itemx1.SubItems.Add(s_ds.content);

                    if (s_ds.matflg == "1")

                        itemx1.SubItems.Add("有");
                    else
                        itemx1.SubItems.Add("無");

                    itemx1.SubItems.Add(s_ds.matcommand);
                    itemx1.SubItems.Add(s_ds.uketukedate);
                    itemx1.SubItems.Add(s_ds.tehaidate);
                    itemx1.SubItems.Add(s_ds.fukyudate);
                    itemx1.SubItems.Add(s_ds.enddate);
                    itemx1.SubItems.Add(s_ds.timer);
                    itemx1.SubItems.Add(s_ds.kakuninmsg);
                    itemx1.SubItems.Add(s_ds.userno);
                    itemx1.SubItems.Add(s_ds.systemno);
                    itemx1.SubItems.Add(s_ds.siteno);
                    itemx1.SubItems.Add(s_ds.hostno);
                    itemx1.SubItems.Add(s_ds.chk_date);
                    itemx1.SubItems.Add(s_ds.chk_name_id);

                    this.m_incidentList.Items.Add(itemx1);
                }
            }
        }

        //キャンセルボタン
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //更新ボタン
        private void button1_Click(object sender, EventArgs e)
        {
            if (m_incidentKBN.Text == "")
            {
                MessageBox.Show("インシデント区分を選択して下さい。", "インシデント情報更新", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //確認ダイアログ
            if (MessageBox.Show("インシデントデータの更新を行います。よろしいですか？", "インシデント情報更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string status = "";
            if (m_statusCombo.Text == "未完了")
                status = "1";
            else if (m_statusCombo.Text == "完了")
                status = "0";

            //インシデントタイプ
            string incident_type = "";
            if (m_incidentKBN.Text == "アラーム検知")
                incident_type = "1";
            else if (m_incidentKBN.Text == "障害申告")
                incident_type = "2";

            else if (m_incidentKBN.Text == "問い合わせ")
                incident_type = "3";

            string matflg = "";
            if (m_MATchkbox.Checked)
                matflg = "1";
            else
                matflg = "0";

            DateTime? uke_Date = null;
            if (m_uketukedate.Checked)
                uke_Date = m_uketukedate.Value;

            DateTime? teha_Date = null;
            if (m_tehaidate.Checked)
                teha_Date = m_tehaidate.Value;

            DateTime? fukyu_Date = null;
            if (m_fukkyudate.Checked)
                fukyu_Date = m_fukkyudate.Value;

            DateTime? end_Date = null;
            if (m_enddate.Checked)
                end_Date = m_enddate.Value;

            DateTime? timer_Date = null;
            if (m_timer.Checked)
                timer_Date = m_timer.Value;


            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "update incident set " +
               "status=:status," +
                "mpms_incident=:mpms_incident," +
                "s_cube_id=:s_cube_id," +
                "incident_type=:incident_type," +
                "content=:content," +
                "matflg=:matflg," +
                "matcommand=:matcommand," +
                "uketukedate=:uketukedate," +
                "tehaidate=:tehaidate," +
                "fukyudate=:fukyudate," +
                "enddate=:enddate," +
                "timer=:timer," +
                "kakunin=:kakunin," +
                "chk_name_id =:ope,chk_date=:chdate " +
                "where incident_no = :no";

            using (var transaction = con.BeginTransaction())
            {
                var command = new NpgsqlCommand(@sql, con);
                command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = m_incidentno.Text });
                command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                command.Parameters.Add(new NpgsqlParameter("mpms_incident", DbType.Int32) { Value = m_mpmsno.Text });
                command.Parameters.Add(new NpgsqlParameter("s_cube_id", DbType.String) { Value = m_scubeno.Text });
                command.Parameters.Add(new NpgsqlParameter("incident_type", DbType.String) { Value = incident_type });
                command.Parameters.Add(new NpgsqlParameter("content", DbType.String) { Value = m_incidentKBN.Text });
                command.Parameters.Add(new NpgsqlParameter("matflg", DbType.String) { Value = matflg });
                command.Parameters.Add(new NpgsqlParameter("matcommand", DbType.String) { Value = m_MATCommannd.Text });
                command.Parameters.Add(new NpgsqlParameter("uketukedate", DbType.DateTime) { Value = uke_Date });
                command.Parameters.Add(new NpgsqlParameter("tehaidate", DbType.DateTime) { Value = teha_Date });
                command.Parameters.Add(new NpgsqlParameter("fukyudate", DbType.DateTime) { Value = fukyu_Date });
                command.Parameters.Add(new NpgsqlParameter("enddate", DbType.DateTime) { Value = end_Date });
                command.Parameters.Add(new NpgsqlParameter("timer", DbType.DateTime) { Value = timer_Date });
                command.Parameters.Add(new NpgsqlParameter("kakunin", DbType.String) { Value = m_youkakunin.Text });
                command.Parameters.Add(new NpgsqlParameter("ope", DbType.String) { Value = loginDS.opeid });
                command.Parameters.Add(new NpgsqlParameter("chdate", DbType.DateTime) { Value = DateTime.Now });
                Int32 rowsaffected;
                try
                {
                    //更新処理
                    rowsaffected = command.ExecuteNonQuery();
                    transaction.Commit();

                    if (rowsaffected != 1)
                        MessageBox.Show("更新できませんでした。", "インシデント情報更新");
                    else
                    {
                        //タイマーが設定されていた場合はインシデントタイマーを設定する
                        if (status == "1" && m_timer.Checked)
                        {
                            //スケジュール登録
                            if (updateSchedule(m_incidentno.Text, con) == 0)
                            {
                                MessageBox.Show("スケジュール情報の登録に失敗しました", "インシデント情報更新", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                transaction.Rollback();
                                return;
                            }
                            //タイマーアラーム登録


                            //一度未来のタイマーを削除
                            //DeleteTimer();

                            //新規にタイマーアラームを登録する
                            //InsertTimer();
                        }



                        MessageBox.Show("更新されました。", "インシデント情報更新");

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

        //スケジュール情報のアップデート
        private int updateSchedule(String schedule_no, NpgsqlConnection con)
        {
            int ret = 0;
            try
            {
                if (con.FullState != ConnectionState.Open) con.Open();

                //スケジュールテーブルの登録内容は削除する
                string sql = "DELETE FROM schedule WHERE incident_no=:no1;";
                //+
                 var deletecommand = new NpgsqlCommand(@sql, con);
                deletecommand.Parameters.Add(new NpgsqlParameter("no1", DbType.Int32) { Value = m_incidentno.Text });

                Int32 rowinsert;
                rowinsert = deletecommand.ExecuteNonQuery();
                if (rowinsert != 0)
                    MessageBox.Show("スケジュールテーブルの書き換えに失敗しました。", "スケジュール削除");

                InsertIncident(m_incidentno.Text, con);
                ret = 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show("タイマー登録する際にエラーが発生しました。 " + ex.Message, "インシデント登録");
                ret = -1;
            }


            return ret;
        }
        //スケジュールテーブルにタイマーをセットする
        private int InsertIncident(String schedule_no, NpgsqlConnection con)
        {
            int ret = 0;

            NpgsqlCommand cmd;
            try
            {
                DateTime timer_Date = new DateTime();
                if (m_timer.Checked)
                    timer_Date = m_timer.Value;


                //1時間出し続ける
                DateTime endTime = timer_Date.AddMinutes(60);

                cmd = new NpgsqlCommand(@"insert into schedule(userno,systemno,timer_name,schedule_type,repeat_type,start_date,end_date,alerm_message,status,sound,incident_no,chk_name_id) " +
                            "values ( :userno,:systemno,:timer_name,:schedule_type,:repeat_type,:start_date,:end_date,:alerm_message,:status,:sound,:incident_no,:chk_name_id); " +
                            "select currval('schedule_schedule_no_seq') ;", con);

                cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = m_userno.Text });
                cmd.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = m_systemno.Text });
                cmd.Parameters.Add(new NpgsqlParameter("timer_name", DbType.String) { Value =  "インシデント管理タイマー" });
                cmd.Parameters.Add(new NpgsqlParameter("schedule_type", DbType.String) { Value = "1" });
                cmd.Parameters.Add(new NpgsqlParameter("repeat_type", DbType.String) { Value = "1" });
                cmd.Parameters.Add(new NpgsqlParameter("start_date", DbType.DateTime) { Value = DateTime.Now });
                cmd.Parameters.Add(new NpgsqlParameter("end_date", DbType.DateTime) { Value = endTime });
                cmd.Parameters.Add(new NpgsqlParameter("alerm_message", DbType.String) { Value = m_youkakunin.Text });
                //0:無効 1:有効
                cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = "1" });
                cmd.Parameters.Add(new NpgsqlParameter("sound", DbType.Binary) { Value = null });
                cmd.Parameters.Add(new NpgsqlParameter("incident_no", DbType.Int32) { Value = int.Parse(m_incidentno.Text) });
                cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });

                //OUTパラメータをセットする
                NpgsqlParameter firstColumn = new NpgsqlParameter("firstcolumn", DbType.Int32);
                firstColumn.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(firstColumn);
                Int32 rowsaffected;
                rowsaffected = cmd.ExecuteNonQuery();

                int currval = int.Parse(firstColumn.NpgsqlValue.ToString());

                if (rowsaffected != 1)
                {
                    MessageBox.Show("登録できませんでした。", "タイマー登録");
                }
                else
                {
                    if (currval > 0)
                    {

                        //引き続きアラートデータを作成し登録する
                        alerm_insert(currval, timer_Date);
                        //登録成功
                        MessageBox.Show("登録完了 " + "スケジュール番号" + currval, "タイマー登録");
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("スケジュールに登録する際にエラーが発生しました。 " + ex.Message, "インシデント登録");
                ret = -1;
                return ret;
            }
            return 1;
        }

        //タイマー対応テーブルに登録する
        int alerm_insert(int scheNO, DateTime alertdatetime)
        {
            //DB接続
            NpgsqlCommand cmd;
            try
            {

                Int32 rowsaffected;
                //データ登録
                cmd = new NpgsqlCommand(@"insert into timer_taiou(schedule_no,schedule_type,alertdatetime,chk_name_id) 
                    values ( :schedule_no,:schedule_type,:alertdatetime,:chk_name_id) ", con);

                cmd.Parameters.Add(new NpgsqlParameter("schedule_no", DbType.Int32) { Value = scheNO });
                //インシデントなので3固定
                cmd.Parameters.Add(new NpgsqlParameter("schedule_type", DbType.String) { Value = "1" });
                cmd.Parameters.Add(new NpgsqlParameter("alertdatetime", DbType.DateTime) { Value = alertdatetime });
                cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });
                rowsaffected = cmd.ExecuteNonQuery();

                if (rowsaffected != 1)
                {
                    MessageBox.Show("タイマー対応テーブルに登録できませんでした。", "タイマー登録");
                    return -1;
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("タイマー対応テーブル登録エラー " + ex.Message);
                return -1;
            }
            return 1;

        }



        //一覧をダブルクリック
        private void m_host_List_DoubleClick(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection item = m_incidentList.SelectedIndices;
            incidentDS incidentdt = new incidentDS();
            incidentdt.incident_no = this.m_incidentList.Items[item[0]].SubItems[0].Text;
            if (this.m_incidentList.Items[item[0]].SubItems[1].Text == "完了")
                incidentdt.status = "0";
            else if (this.m_incidentList.Items[item[0]].SubItems[1].Text == "未完了")
                incidentdt.status = "1";
            else
                incidentdt.status = "";

            incidentdt.mpms_incident = this.m_incidentList.Items[item[0]].SubItems[2].Text;
            incidentdt.s_cube_id = this.m_incidentList.Items[item[0]].SubItems[3].Text;

            //1:アラーム検知 2:障害申告 3:問い合わせ
            if (this.m_incidentList.Items[item[0]].SubItems[4].Text == "アラーム検知")
                incidentdt.incident_type = "1";
            else if (this.m_incidentList.Items[item[0]].SubItems[4].Text == "障害申告")
                incidentdt.incident_type = "2";
            else if (this.m_incidentList.Items[item[0]].SubItems[4].Text == "問い合わせ")
                incidentdt.incident_type = "3";
            else
                incidentdt.incident_type = "";


            incidentdt.content = this.m_incidentList.Items[item[0]].SubItems[5].Text;
            if (this.m_incidentList.Items[item[0]].SubItems[6].Text == "無")
                incidentdt.matflg = "0";
            else if (this.m_incidentList.Items[item[0]].SubItems[6].Text == "有")
                incidentdt.matflg = "1";
            else
                incidentdt.matflg = "";



            incidentdt.matcommand = this.m_incidentList.Items[item[0]].SubItems[7].Text;
            incidentdt.uketukedate = this.m_incidentList.Items[item[0]].SubItems[8].Text;
            incidentdt.tehaidate = this.m_incidentList.Items[item[0]].SubItems[9].Text;
            incidentdt.fukyudate = this.m_incidentList.Items[item[0]].SubItems[10].Text;
            incidentdt.enddate = this.m_incidentList.Items[item[0]].SubItems[11].Text;
            incidentdt.timer = this.m_incidentList.Items[item[0]].SubItems[12].Text;
            incidentdt.kakuninmsg = this.m_incidentList.Items[item[0]].SubItems[13].Text;
        
            incidentdt.userno = this.m_incidentList.Items[item[0]].SubItems[14].Text;
            incidentdt.systemno = this.m_incidentList.Items[item[0]].SubItems[15].Text;
            incidentdt.siteno = this.m_incidentList.Items[item[0]].SubItems[16].Text;
            incidentdt.hostno = this.m_incidentList.Items[item[0]].SubItems[17].Text;
            incidentdt.chk_date = this.m_incidentList.Items[item[0]].SubItems[18].Text;
            incidentdt.chk_name_id = this.m_incidentList.Items[item[0]].SubItems[19].Text;

            getIncident(incidentdt);
        }
        //メール出力画面
        private void m_mailout_Click(object sender, EventArgs e)
        {
            Form_mailTempleteList mailselectform = new Form_mailTempleteList();
            mailselectform.con = con;
            mailselectform.loginDS = loginDS;
            mailselectform.Show();
        }
        //インシデント情報一覧のカラムをクリックした時、ソート
        private void m_incidentList_ColumnClick(object sender, ColumnClickEventArgs e)
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
            m_incidentList.Sort();

        }
        //削除処理
        private void m_deleteBtn_Click(object sender, EventArgs e)
        {
            int count = m_incidentList.SelectedItems.Count;

            //確認メッセージ
            if (MessageBox.Show("一覧に選択された行 " + count + "件 の削除を行います。" + Environment.NewLine +
                "よろしいですか？", "インシデント削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;


            int ret = deleteIncident();
            if (ret == -1)
            {
                return;
            }

            //リストの表示上からけす
            foreach (ListViewItem item in m_incidentList.SelectedItems)
            {
                m_incidentList.Items.Remove(item);
            }
        }

        //削除
        private int deleteIncident()
        {

            string incidentno;

            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "DELETE FROM incident where incident_no = :no ";

            using (var transaction = con.BeginTransaction())
            {

                foreach (ListViewItem item in m_incidentList.SelectedItems)
                {
                    incidentno = item.SubItems[0].Text;


                    var command = new NpgsqlCommand(@sql, con);
                    command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = int.Parse(incidentno) });

                    Int32 rowsaffected;
                    try
                    {
                        //削除処理
                        rowsaffected = command.ExecuteNonQuery();
                        transaction.Commit();

                        if (rowsaffected != 1) { 
                            MessageBox.Show("削除できませんでした。ホストID:" + incidentno, "ホスト削除");
                            transaction.Rollback();
                            return -1;
                        }
                        else { 
                            MessageBox.Show("削除完了しました。ホストID:" + incidentno, "ホスト削除");
                        }
                    }
                    catch (Exception ex)
                    {
                        //エラー時メッセージ表示
                        MessageBox.Show("インシデント削除時エラーが発生しました。 " + ex.Message);
                        transaction.Rollback();
                        return -1;
                    }
                }

            }
            return 1;
        }
    }
}
