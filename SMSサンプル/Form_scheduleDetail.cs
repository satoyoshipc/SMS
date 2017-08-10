using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMSサンプル
{
    public partial class Form_scheduleDetail : Form
    {
        //ログイン情報
        public opeDS loginDS { get; set; }

        public scheduleDS keikakudt { get; set; }

        //ユーザ情報一覧
        public List<userDS> userList { get; set; }
        //システム情報一覧
        public List<systemDS> systemList { get; set; }
        //拠点
        public List<siteDS> siteList { get; set; }
        //DBコネクション
        public NpgsqlConnection con { get; set; }

        //初期表示のときのﾌﾗｸﾞ
        //private Boolean firstflg=false;
        //ListViewのソートの際に使用する
        private Class_ListViewColumnSorter _columnSorter;

        public Form_scheduleDetail()
        {
            InitializeComponent();
        }

        //表示前処理
        private void Form_KeikakuDetail_Load(object sender, EventArgs e)
        {
            this.splitContainer1.SplitterDistance = 32;

            _columnSorter = new Class_ListViewColumnSorter();
            m_scheduleList.ListViewItemSorter = _columnSorter;


            m_selectKoumoku.Items.Add("予定通番");
            m_selectKoumoku.Items.Add("タイマー名称");
            m_selectKoumoku.Items.Add("予定区分");
            m_selectKoumoku.Items.Add("繰り返し区分");
            m_selectKoumoku.Items.Add("開始日時");
            m_selectKoumoku.Items.Add("終了日時");
            m_selectKoumoku.Items.Add("ステータス");
            m_selectKoumoku.Items.Add("メッセージ");
            m_selectKoumoku.Items.Add("インシデント番号");
            m_selectKoumoku.Items.Add("要確認");
            m_selectKoumoku.Items.Add("カスタマ通番");
            m_selectKoumoku.Items.Add("カスタマ名");
            m_selectKoumoku.Items.Add("システム通番");
            m_selectKoumoku.Items.Add("拠点通番");
            m_selectKoumoku.Items.Add("更新日時");
            m_selectKoumoku.Items.Add("更新者");

            m_end_date.MaxDate = DateTime.Now.AddYears(3);

            //サウンドだけはDBから取得する
            if (keikakudt.schedule_no != null) {
                //firstflg = true;

                Class_Detaget dg = new Class_Detaget();
                keikakudt.sound = dg.getSound((Form_MainList)this.Owner, con, keikakudt.schedule_no);
                String alertdt = dg.getLatestAlerm(keikakudt.schedule_no, con);
                keikakudt.alertdate = alertdt;
                getKeikaku(keikakudt);
                koumokuDisable();
            }
        }

        //データ表示
        private void getKeikaku(scheduleDS keikakudt)
        {

            this.m_scheduleno.Text = keikakudt.schedule_no;
            this.m_userno.Text = keikakudt.userno;
            this.m_systemno.Text = keikakudt.systemno;
            this.m_siteno.Text = keikakudt.siteno;
            this.m_timername.Text = keikakudt.timer_name;
            if (keikakudt.schedule_type == null || keikakudt.schedule_type == "")
                keikakudt.schedule_type = "1";
            this.m_yoteikbn.SelectedIndex = int.Parse(keikakudt.schedule_type) - 1;
            this.m_statusCombo.Text = keikakudt.status;
            if (keikakudt.repeat_type == null || keikakudt.repeat_type == "")
                keikakudt.repeat_type = "1";
            this.m_repeatkbn.SelectedIndex = int.Parse(keikakudt.repeat_type) - 1;

            //開始日時の取得
            System.DateTime dd1;
            if (keikakudt.start_date != null && keikakudt.start_date != "")
            {     
                dd1 = DateTime.ParseExact(keikakudt.start_date, "yyyy/MM/dd HH:mm:ss", null);
                this.m_start_date.Value = dd1;
            }

            //終了日時
            System.DateTime dd2;
            if (keikakudt.end_date != null && keikakudt.end_date != "")
            {
            
                dd2 = DateTime.ParseExact(keikakudt.end_date, "yyyy/MM/dd HH:mm:ss", null);
                if (dd2 > m_end_date.MaxDate)
                    dd2 = m_end_date.MaxDate;
                this.m_end_date.Value = dd2;
            }

            System.DateTime alertdt;
            if (keikakudt.alertdate != null && keikakudt.alertdate != "")
            {
                this.m_alermDate.Enabled = true;
                alertdt = DateTime.ParseExact(keikakudt.alertdate, "yyyy/MM/dd HH:mm:ss", null);
                this.m_alermDate.Value = alertdt;
            }
            
            this.m_alermMessage.Text = keikakudt.alerm_message;

            if (m_sound.Text != "")
            {
                //相対パスの時はファイルを削除する
                if (System.IO.Path.IsPathRooted(@m_sound.Text) == false)
                {
                    
                    if (System.IO.File.Exists(@m_sound.Text))
                        System.IO.File.Delete(@m_sound.Text);

                }
            }

            this.m_sound.Text = keikakudt.sound;
            this.m_kakunin.Text = keikakudt.kakunin;

            this.m_incidentno.Text = keikakudt.incident_no;

            this.m_updateOpe.Text = keikakudt.chk_name_id;
            this.m_update.Text = keikakudt.chk_date;

            //コンボボックスを読み込む
            Read_CustomerCombo();
            m_usernameCombo.SelectedValue = keikakudt.userno;
            if (m_usernameCombo.SelectedValue != null)
            {
                Read_systemCombo();
                m_systemCombo.SelectedValue = keikakudt.systemno;
            }
            if (m_systemCombo.SelectedValue != null)
            {
                Read_siteCombo();
                m_siteCombo.SelectedValue = keikakudt.siteno;
                m_siteno.Text = keikakudt.siteno;
            }
 
        }
        void Read_CustomerCombo()
        {
            m_userno.Text = "";
            m_usernameCombo.DataSource = null;
            m_systemno.Text = "";
            m_systemCombo.DataSource = null;
            m_siteno.Text = "";
            m_siteCombo.DataSource = null;

            //コンボボックス
            DataTable cutomerTable = new DataTable();
            cutomerTable.Columns.Add("ID", typeof(string));
            cutomerTable.Columns.Add("NAME", typeof(string));

            if (userList == null)
                return;

            //空行を挿入
            userDS tmp = new userDS();
            tmp.username = "";
            tmp.userno = "";
            cutomerTable.Rows.Add(tmp);

            //カスタマ情報を取得する
            foreach (userDS v in userList)
            {
                DataRow row = cutomerTable.NewRow();
                row["ID"] = v.userno;
                row["NAME"] = v.username;
                cutomerTable.Rows.Add(row);
            }
            //データテーブルを割り当てる
            m_usernameCombo.DataSource = cutomerTable;
            m_usernameCombo.DisplayMember = "NAME";
            m_usernameCombo.ValueMember = "ID";

        }
        void Read_systemCombo()
        {
            m_systemno.Text = "";
            m_systemCombo.DataSource = null;

            m_siteno.Text = "";
            m_siteCombo.DataSource = null;

            //ラベルに反映
            if (m_usernameCombo.SelectedValue != null)
                m_userno.Text = m_usernameCombo.SelectedValue.ToString();

            //システムコンボの値を取得
            DataTable systemTable = new DataTable();
            systemTable.Columns.Add("ID", typeof(string));
            systemTable.Columns.Add("NAME", typeof(string));

            //システム情報を取得する
            if (systemList.Count <= 0)
                return;

            //空行を挿入
            DataRow row = systemTable.NewRow();
            row["ID"] = "";
            row["NAME"] = "";
            systemTable.Rows.Add(row);

            foreach (systemDS v in systemList)
            {
                //カスタマNOで区別する
                if (m_usernameCombo.SelectedValue != null)
                {

                    if (v.userno == m_usernameCombo.SelectedValue.ToString())
                    {
                        row = systemTable.NewRow();
                        row["ID"] = v.systemno;
                        row["NAME"] = v.systemname;
                        systemTable.Rows.Add(row);
                    }
                }
            }
            //データテーブルを割り当てる
            m_systemCombo.DataSource = systemTable;
            m_systemCombo.DisplayMember = "NAME";
            m_systemCombo.ValueMember = "ID";
            if (systemTable.Rows.Count > 0)
                m_systemno.Text = m_systemCombo.SelectedValue.ToString();
        }
        void Read_siteCombo()
        {
            m_siteCombo.DataSource = null;
            m_siteno.Text = "";



            //ラベルに反映
            if (m_systemCombo.SelectedValue != null)
                m_systemno.Text = m_systemCombo.SelectedValue.ToString();

            //コンボボックス
            DataTable siteTable = new DataTable();
            siteTable.Columns.Add("ID", typeof(string));
            siteTable.Columns.Add("NAME", typeof(string));

            string systemid = "";
            if (m_systemno.Text != "")
                systemid = m_systemno.Text;

            //拠点情報の取得
            Class_Detaget DGclass = new Class_Detaget();
            siteList = DGclass.getSiteList(systemid, con, true);

            //取れなかったらなにもしない
            if (siteList == null || siteList.Count <= 0)
                return;

            //空行の挿入
            DataRow row = siteTable.NewRow();
            row["ID"] = "";
            row["NAME"] = "";
            siteTable.Rows.Add(row);

            //拠点件数分ループを行う
            foreach (siteDS v in siteList)
            {
                if (m_systemCombo.SelectedValue != null)
                {
                    if (v.systemno == m_systemCombo.SelectedValue.ToString())
                    {
                        row = siteTable.NewRow();
                        row["ID"] = v.siteno;
                        row["NAME"] = v.sitename;
                        siteTable.Rows.Add(row);
                    }
                }
            }
            //データテーブルを割り当てる
            m_siteCombo.DataSource = siteTable;
            m_siteCombo.DisplayMember = "NAME";
            m_siteCombo.ValueMember = "ID";
            if (siteTable.Rows.Count > 0)
                if (m_siteCombo.Text != "")
                    m_siteno.Text = m_siteCombo.SelectedValue.ToString();
        }
        //検索ボタン
        private void m_selectBtn_Click(object sender, EventArgs e)
        {
            m_scheduleList.Clear();
            List<scheduleDS> scheduleset = new List<scheduleDS>();
            Dictionary<string, string> param_dict = new Dictionary<string, string>();
            Class_Detaget dg = new Class_Detaget();

            if (m_selecttext.Text != "")
            {

                if (this.m_selectKoumoku.SelectedIndex.ToString() != "")
                {
                    switch (this.m_selectKoumoku.SelectedIndex)
                    {
                        case 0:
                            param_dict["schedule_no"] = m_selecttext.Text;
                            break;
                        case 1:
                            param_dict["timer_name"] = m_selecttext.Text;
                            break;
                        //区分
                        case 2:
                            if(m_selecttext.Text != "")
                            { 
                                //スケジュール区分
                                if (0 <= m_selecttext.Text.IndexOf("インシデント"))
                                    param_dict["schedule_type"] = "1";
                                else if (0 <= m_selecttext.Text.IndexOf("定期"))
                                    param_dict["schedule_type"] = "2";
                                else if (0 <= m_selecttext.Text.IndexOf("計画"))
                                    param_dict["schedule_type"] = "3";
                                else if (0 <= m_selecttext.Text.IndexOf("特別"))
                                    param_dict["schedule_type"] = "4";
                            }
                            break;

                        case 3:
                            //1:1回、2:1時間毎、3:日毎、4:週毎、5:月毎
                            if (0 <= m_selecttext.Text.IndexOf("回"))
                                param_dict["repeat_type"] = "1";
                            else if (0 <= m_selecttext.Text.IndexOf("時"))
                                param_dict["repeat_type"] = "2";
                            else if (0 <= m_selecttext.Text.IndexOf("日"))
                                param_dict["repeat_type"] = "3";
                            else if (0 <= m_selecttext.Text.IndexOf("週"))
                                param_dict["repeat_type"] = "4";
                            else if (0 <= m_selecttext.Text.IndexOf("月"))
                                param_dict["repeat_type"] = "5";


                           // param_dict["repeat_type"] = m_selecttext.Text;
                            break;
                        case 4:
                            param_dict["start_date"] = m_selecttext.Text;
                            break;
                        case 5:
                            param_dict["end_date"] = m_selecttext.Text;
                            break;
                        case 6:
                            if (m_selecttext.Text == "未完了")
                                param_dict["status"] = "1";
                            else if (m_selecttext.Text == "完了")
                                param_dict["status"] = "0";
                            break;
                        case 7:
                            param_dict["alerm_message"] = m_selecttext.Text;
                            break;
                        case 8:
                            param_dict["incident_no"] = m_selecttext.Text;
                            break;
                        case 9:
                            param_dict["kakunin"] = m_selecttext.Text;
                            break;
                        case 10:
                            param_dict["userno"] = m_selecttext.Text;
                            break;
                        case 11:
                            param_dict["username"] = m_selecttext.Text;
                            break;
                        case 12:
                            param_dict["systemno"] = m_selecttext.Text;
                            break;
                        case 13:
                            param_dict["siteno"] = m_selecttext.Text;
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

            //計画作業
            scheduleset = dg.getSelectscheduleList((Form_MainList)this.Owner, param_dict, con);


            this.splitContainer1.SplitterDistance = 280;

            this.m_scheduleList.FullRowSelect = true;
            this.m_scheduleList.HideSelection = false;
            this.m_scheduleList.HeaderStyle = ColumnHeaderStyle.Clickable;


            this.m_scheduleList.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_scheduleList.Columns.Insert(1, "タイマー名称", 200, HorizontalAlignment.Left);
            this.m_scheduleList.Columns.Insert(2, "予定区分", 90, HorizontalAlignment.Left);
            this.m_scheduleList.Columns.Insert(3, "繰り返し区分", 40, HorizontalAlignment.Left);
            this.m_scheduleList.Columns.Insert(4, "開始日時", 120, HorizontalAlignment.Left);
            this.m_scheduleList.Columns.Insert(5, "終了日時", 120, HorizontalAlignment.Left);
            this.m_scheduleList.Columns.Insert(6, "ステータス", 60, HorizontalAlignment.Left);
            this.m_scheduleList.Columns.Insert(7, "メッセージ", 180, HorizontalAlignment.Left);
            this.m_scheduleList.Columns.Insert(8, "音", 50, HorizontalAlignment.Left);
            this.m_scheduleList.Columns.Insert(9, "インシデント番号(管理用)", 50, HorizontalAlignment.Left);
            this.m_scheduleList.Columns.Insert(10, "要確認", 50, HorizontalAlignment.Left);
            this.m_scheduleList.Columns.Insert(11, "カスタマ通番", 50, HorizontalAlignment.Left);
            this.m_scheduleList.Columns.Insert(12, "システム通番", 50, HorizontalAlignment.Left);
            this.m_scheduleList.Columns.Insert(13, "拠点通番", 50, HorizontalAlignment.Left);
            this.m_scheduleList.Columns.Insert(14, "更新日時", 50, HorizontalAlignment.Left);
            this.m_scheduleList.Columns.Insert(15, "更新者", 50, HorizontalAlignment.Left);

            //リストに表示
            if (scheduleset != null)
            {
                foreach (scheduleDS s_ds in scheduleset)
                {

                    ListViewItem itemx1 = new ListViewItem();
                    itemx1.Text = s_ds.schedule_no;

                    itemx1.SubItems.Add(s_ds.timer_name);
                    string schetype = "";
                    if (s_ds.schedule_type == "1")
                        schetype = "インシデント処理";
                    else if (s_ds.schedule_type == "2")
                        schetype = "定期作業";
                    else if (s_ds.schedule_type == "3")
                        schetype = "計画作業";
                    else if (s_ds.schedule_type == "4")
                        schetype = "特別対応";

                    itemx1.SubItems.Add(schetype);



                        //1:1回、2:1時間毎、3:日毎、4:週毎、5:月毎
                        string repType = "";
                        if (s_ds.repeat_type == "1")
                            repType = "1回";
                        else if (s_ds.repeat_type == "2")
                            repType = "1時間毎";
                        else if (s_ds.repeat_type == "3")
                            repType = "日毎";
                        else if (s_ds.repeat_type == "4")
                            repType = "週毎";
                        else if (s_ds.repeat_type == "5")
                            repType = "月毎";


                    itemx1.SubItems.Add(repType);
                    itemx1.SubItems.Add(s_ds.start_date);
                    itemx1.SubItems.Add(s_ds.end_date);
 

                    itemx1.SubItems.Add(s_ds.status);


                    itemx1.SubItems.Add(s_ds.alerm_message);
                    itemx1.SubItems.Add(s_ds.sound);



                    itemx1.SubItems.Add(s_ds.incident_no);
                    itemx1.SubItems.Add(s_ds.kakunin);
                    itemx1.SubItems.Add(s_ds.userno);
                    itemx1.SubItems.Add(s_ds.systemno);
                    itemx1.SubItems.Add(s_ds.siteno);
                    itemx1.SubItems.Add(s_ds.chk_date);
                    itemx1.SubItems.Add(s_ds.chk_name_id);

                    this.m_scheduleList.Items.Add(itemx1);
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
            if (m_timername.Text == "")
            {
                MessageBox.Show("タイマー名称を入力して下さい。", "スケジュール修正", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (m_sound.Text == "" && m_yoteikbn.SelectedIndex != 0 && m_yoteikbn.SelectedIndex != 3)
            {
                MessageBox.Show("音を入力して下さい。", "スケジュール修正", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //確認ダイアログ
            if (MessageBox.Show("スケジュールデータの更新を行います。よろしいですか？", "スケジュール情報更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            string status = "";
            if (m_statusCombo.Text == "未完了")
                status = "1";
            else if (m_statusCombo.Text == "完了")
                status = "0";

            string yoteikbn = "";
            if (m_yoteikbn.SelectedIndex == 0)
                yoteikbn = "1";
            else if (m_yoteikbn.SelectedIndex == 1)
                yoteikbn = "2";
            else if (m_yoteikbn.SelectedIndex == 2)
                yoteikbn = "3";
            else if (m_yoteikbn.SelectedIndex == 3)
                yoteikbn = "4";

            //1:1回、2:1時間毎、3:日毎、4:週毎、5:月毎
            string repType = "";
            if (m_repeatkbn.Text == "1回")
                repType = "1";
            else if (m_repeatkbn.Text== "1時間毎")
                repType = "2";
            else if (m_repeatkbn.Text == "日毎")
                repType = "3";
            else if (m_repeatkbn.Text == "週毎")
                repType = "4";
            else if (m_repeatkbn.Text == "月毎")
                repType = "5";


            byte[] bytes = null;
            if (m_sound.Text != "" && m_yoteikbn.SelectedIndex != 0 && m_yoteikbn.SelectedIndex != 3)
            {
                if (m_sound.Text != null)
                    bytes = File.ReadAllBytes(m_sound.Text);
            }
            if (this.con.FullState != ConnectionState.Open) this.con.Open();

            string sql = "update schedule set timer_name=:timer_name,schedule_type=:schedule_type,repeat_type=:repeat_type,start_date=:start_date,end_date=:end_date," +
                "status=:status,alerm_message=:alerm_message,sound=:sound,userno =:userno,systemno=:systemno,siteno=:siteno,incident_no =:incident_no,kakunin=:kakunin,chk_name_id =:ope,chk_date=:chdate where schedule_no = :no";

            using (var transaction = this.con.BeginTransaction())
            {

                var command = new NpgsqlCommand(@sql, this.con);
                int incino = 0;
                int.TryParse(m_incidentno.Text, out incino);

                int userno;
                int systemno;
                int siteno;

                int? userno2;
                int? systemno2;
                int? siteno2;


                if (int.TryParse(m_userno.Text, out userno))
                    userno2 = userno;
                else
                    userno2 = null;
                if (int.TryParse(m_systemno.Text, out systemno))
                    systemno2 = systemno;
                else
                    systemno2 = null;
                if (int.TryParse(m_siteno.Text, out siteno))
                    siteno2 = siteno;
                else
                    siteno2 = null;
                
                DateTime? startdate = null;
                DateTime? enddate = null;
                if (m_start_date.Enabled)
                    startdate = m_start_date.Value;
                if (m_end_date.Enabled)
                    enddate = m_end_date.Value;

                command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = m_scheduleno.Text });
                command.Parameters.Add(new NpgsqlParameter("timer_name", DbType.String) { Value = m_timername.Text });
                command.Parameters.Add(new NpgsqlParameter("schedule_type", DbType.String) { Value = yoteikbn });
                command.Parameters.Add(new NpgsqlParameter("repeat_type", DbType.String) { Value = repType });
                command.Parameters.Add(new NpgsqlParameter("start_date", DbType.DateTime) { Value = startdate });
                command.Parameters.Add(new NpgsqlParameter("end_date", DbType.DateTime) { Value = enddate });
                command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                command.Parameters.Add(new NpgsqlParameter("alerm_message", DbType.String) { Value = m_alermMessage.Text });
                command.Parameters.Add(new NpgsqlParameter("sound", DbType.Binary) { Value = bytes });
                command.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = userno });
                command.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = systemno });
                command.Parameters.Add(new NpgsqlParameter("siteno", DbType.Int32) { Value = siteno });

                command.Parameters.Add(new NpgsqlParameter("incident_no", DbType.Int32) { Value = incino });
                command.Parameters.Add(new NpgsqlParameter("kakunin", DbType.String) { Value = m_kakunin.Text });
                command.Parameters.Add(new NpgsqlParameter("ope", DbType.String) { Value = loginDS.opeid });
                command.Parameters.Add(new NpgsqlParameter("chdate", DbType.DateTime) { Value = DateTime.Now });
                Int32 rowsaffected;
                try
                {
                    //更新処理

                    //OUTパラメータをセットする
                    rowsaffected = command.ExecuteNonQuery();

                    if (rowsaffected != 1)
                        MessageBox.Show("更新できませんでした。", "スケジュール更新");

                    else
                    {
                        //タイマー情報も更新する
                        //まず登録されているタイマーは削除
                        int ret = deleteTimer(m_scheduleno.Text);
                        if(ret == 0)
                        //引き続きアラートデータを作成し登録する
                        make_alert(int.Parse(m_scheduleno.Text), yoteikbn);

                        MessageBox.Show("スケジュール情報を変更しました。","スケジュール更新",MessageBoxButtons.OK);
                        transaction.Commit();
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

        public int deleteTimer(String scheduleno)
        {
            //DB接続
            NpgsqlCommand cmd;
            try
            {
                if (con.FullState != ConnectionState.Open) con.Open();
                Int32 rowsaffected;
                //データ登録
                cmd = new NpgsqlCommand(@"DELETE FROM timer_taiou WHERE schedule_no = :schedule_no", con);

                cmd.Parameters.Add(new NpgsqlParameter("schedule_no", DbType.Int32) { Value = scheduleno });
                rowsaffected = cmd.ExecuteNonQuery();

                if (rowsaffected != 1)
                {
                    return 0;
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("削除エラー " + ex.Message,"タイマー削除",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return -1;
            }
            return 1;
            
        }

        // 引き続きアラートデータを作成し登録する
        // タイマースケジュールを登録する
        public void make_alert(int scheNO, string type)
        {
            //ステータス
            if (m_statusCombo.Text == "未完了")


                //アラーム周期            
                //1回の時
                if (m_repeatkbn.SelectedIndex == 0)
                {
                    //入力された日時を登録する
                    DateTime dd = m_alermDate.Value.Date;
                    alerm_insert(scheNO, type, dd);
                }
                //毎時の時
                else if (m_repeatkbn.SelectedIndex == 1)
                {
                    DateTime alertdate = m_alermDate.Value.Date;
                    TimeSpan dtt = m_alermDate.Value.TimeOfDay;
                    alertdate = alertdate + dtt;
                    //開始日
                    DateTime startdt = m_start_date.Value;

                    //終了日
                    DateTime enddt = m_end_date.Value;

                    while (true)
                    {

                        //開始日時以前なら登録しない
                        if (startdt > alertdate)
                        {
                            alertdate = alertdate.AddHours(1);
                            continue;
                        }

                        //終了日まで登録する。
                        if (enddt <= alertdate.AddMinutes(-1))
                            break;

                        //時間毎に登録
                        int ret = alerm_insert(scheNO, type, alertdate);
                        if (ret == -1)
                            break;

                        //1時間プラス
                        alertdate = alertdate.AddHours(1);
                        //i++;
                    }
                }
                //日
                else if (m_repeatkbn.SelectedIndex == 2)
                {

                    //開始日
                    DateTime alertdate = m_start_date.Value.Date;
                    TimeSpan dtt = m_alermDate.Value.TimeOfDay;
                    alertdate = alertdate + dtt;

                    //開始日
                    DateTime startdt = m_start_date.Value;

                    //終了日
                    DateTime enddt = m_end_date.Value;

                    while (true)
                    {

                        //開始日時以前なら登録しない
                        if (startdt > alertdate)
                        {
                            alertdate = alertdate.AddDays(1);
                            continue;
                        }

                        //終了日まで登録する。
                        if (enddt <= alertdate.AddMinutes(-1))
                            break;

                        //日付毎に登録
                        int ret = alerm_insert(scheNO, type, alertdate);
                        if (ret == -1)
                            break;

                        //1日プラス
                        alertdate = alertdate.AddDays(1);
                    }
                }
                //週
                else if (m_repeatkbn.SelectedIndex == 3)
                {
                    int weeknumber = (int)m_alermDate.Value.DayOfWeek;
                    TimeSpan dtt = m_alermDate.Value.TimeOfDay;
                    DateTime alertdate = m_start_date.Value.Date;
                    alertdate = alertdate + dtt;

                    //開始日
                    DateTime startdt = m_start_date.Value;
                    int startweekint = (int)m_start_date.Value.DayOfWeek;

                    //終了日
                    DateTime enddt = m_end_date.Value;
                    int endweekint = (int)m_end_date.Value.DayOfWeek;


                    int tmpweeknumber = startweekint;
                    while (true)
                    {

                        //開始日時以前なら登録しない
                        if (startdt > alertdate)
                        {
                            alertdate = alertdate.AddDays(1);
                            tmpweeknumber = (int)alertdate.DayOfWeek;
                            continue;
                        }
                        //終了日まで登録する。
                        if (enddt <= alertdate.AddMinutes(-1))
                            break;

                        //同じ曜日であれば登録
                        if (weeknumber == tmpweeknumber)
                        {
                            //日付毎に登録
                            int ret = alerm_insert(scheNO, type, alertdate);
                            if (ret == -1)
                                break;
                        }

                        //1日プラス
                        alertdate = alertdate.AddDays(1);
                        tmpweeknumber = (int)alertdate.DayOfWeek;
                    }
                }
                else if (m_repeatkbn.SelectedIndex == 4)
                {

                    //開始日
                    DateTime alertdate = m_start_date.Value.Date;
                    TimeSpan dtt = m_alermDate.Value.TimeOfDay;
                    alertdate = alertdate + dtt;

                    //開始日
                    DateTime startdt = m_start_date.Value;

                    //終了日
                    DateTime enddt = m_end_date.Value;

                    while (true)
                    {

                        //開始日時以前なら登録しない
                        if (startdt > alertdate)
                        {

                            alertdate = alertdate.AddMonths(1);
                            continue;
                        }
                        //終了日まで登録する。
                        if (enddt <= alertdate.AddMinutes(-1))
                            break;

                        //日付毎に登録
                        int ret = alerm_insert(scheNO, type, alertdate);
                        if (ret == -1)
                            break;

                        //1月プラス
                        alertdate = alertdate.AddMonths(1);
                    }
                }
        }
        //タイマー対応テーブルに登録する
        private int alerm_insert(int scheNO, string type, DateTime alertdatetime)
        {
            //DB接続
            NpgsqlCommand cmd;
            try
            {
                if (con.FullState != ConnectionState.Open) con.Open();
                Int32 rowsaffected;

                //データ登録
                cmd = new NpgsqlCommand(@"insert into timer_taiou(schedule_no,schedule_type,alertdatetime,chk_name_id) 
                    values ( :schedule_no,:schedule_type,:alertdatetime,:chk_name_id) ", con);

                cmd.Parameters.Add(new NpgsqlParameter("schedule_no", DbType.Int32) { Value = scheNO });
                cmd.Parameters.Add(new NpgsqlParameter("schedule_type", DbType.String) { Value = type });
                cmd.Parameters.Add(new NpgsqlParameter("alertdatetime", DbType.DateTime) { Value = alertdatetime });
                cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = m_updateOpe.Text });
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

        //ダブルクリック
        private void m_host_List_DoubleClick(object sender, EventArgs e)
        {
            //サウンドファイルを削除する
            deleteSoundFile();

            ListView.SelectedIndexCollection item = m_scheduleList.SelectedIndices;
            string schedule_type = "";
            string status = "";
            scheduleDS scheduledt = new scheduleDS();
            
            scheduledt.schedule_no = this.m_scheduleList.Items[item[0]].SubItems[0].Text;
            scheduledt.timer_name = this.m_scheduleList.Items[item[0]].SubItems[1].Text;

            //1:インシデント処理 2:定期作業業務促し 3:作業情報の警告 4:資料展開 5:サブタスク
            if (this.m_scheduleList.Items[item[0]].SubItems[2].Text == "インシデント処理")
                schedule_type = "1";
            else if (this.m_scheduleList.Items[item[0]].SubItems[2].Text == "定期作業")
                schedule_type = "2";
            else if (this.m_scheduleList.Items[item[0]].SubItems[2].Text == "計画作業")
                schedule_type = "3";
            else if (this.m_scheduleList.Items[item[0]].SubItems[2].Text == "特別対応")
                schedule_type = "4";

            scheduledt.schedule_type = schedule_type;
            
            //1:1回、2:1時間毎、3:日毎、4:週毎、5:月毎
            string repeat_type = "";
            if (this.m_scheduleList.Items[item[0]].SubItems[3].Text == "1回")
                repeat_type = "1";
            else if (this.m_scheduleList.Items[item[0]].SubItems[3].Text == "1時間毎")
                repeat_type = "2";
            else if (this.m_scheduleList.Items[item[0]].SubItems[3].Text == "日毎")
                repeat_type = "3";
            else if (this.m_scheduleList.Items[item[0]].SubItems[3].Text == "週毎")
                repeat_type = "4";
            else if (this.m_scheduleList.Items[item[0]].SubItems[3].Text == "月毎")
                repeat_type = "5";
            scheduledt.repeat_type = repeat_type;

            if(schedule_type != "4") { 
                scheduledt.start_date = this.m_scheduleList.Items[item[0]].SubItems[4].Text;
                scheduledt.end_date = this.m_scheduleList.Items[item[0]].SubItems[5].Text;
            }
            else
            {
                scheduledt.start_date = "";
                scheduledt.end_date = "";
            }

            if (this.m_scheduleList.Items[item[0]].SubItems[6].Text == "未完了")
                status = "1";
            else if (this.m_scheduleList.Items[item[0]].SubItems[6].Text == "完了")
                status = "0";

            scheduledt.status = status;

            if (schedule_type != "4")
            {
                scheduledt.alerm_message = this.m_scheduleList.Items[item[0]].SubItems[7].Text;
                scheduledt.sound = m_scheduleList.Items[item[0]].SubItems[8].Text;
                //音は改めて取得する
                Class_Detaget dg = new Class_Detaget();
                scheduledt.sound = dg.getSound((Form_MainList)this.Owner, con, scheduledt.schedule_no);
            }
            else
            {
                scheduledt.alerm_message = "";
                scheduledt.sound = "";
            }

            scheduledt.incident_no      = this.m_scheduleList.Items[item[0]].SubItems[9].Text;
            if (schedule_type != "4")
                scheduledt.kakunin          = this.m_scheduleList.Items[item[0]].SubItems[10].Text;
            else
            {
                scheduledt.kakunin = "";
            }

            scheduledt.userno           = this.m_scheduleList.Items[item[0]].SubItems[11].Text;
            scheduledt.systemno         = this.m_scheduleList.Items[item[0]].SubItems[12].Text;
            scheduledt.siteno           = this.m_scheduleList.Items[item[0]].SubItems[13].Text;
            scheduledt.chk_date         = this.m_scheduleList.Items[item[0]].SubItems[14].Text;
            scheduledt.chk_name_id      = this.m_scheduleList.Items[item[0]].SubItems[15].Text;

            //直近アラーム日時の取得を行う
            if (schedule_type != "4") { 
                Class_Detaget dg = new Class_Detaget();
                String alertdt = dg.getLatestAlerm(scheduledt.schedule_no,con);
                scheduledt.alertdate = alertdt;
            }

            getKeikaku(scheduledt);

            koumokuDisable();
            //firstflg = false;
        }
        
        //参照ボタン
        private void m_soundBtn_Click(object sender, EventArgs e)
        {
            string str = "";
            //
            Class_common common = new Class_common();
            str = common.Disp_FileSelectDlg("wav");
            if(str != "")
                m_sound.Text = str;
        }
        //♪
        private void button3_Click(object sender, EventArgs e)
        {
            if (m_sound.Text == "")
            {
                MessageBox.Show("wavファイルを入力してください。", "サウンドテスト", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //拡張子がwavファイル
            string stExtension = System.IO.Path.GetExtension(m_sound.Text);
            if (stExtension != ".wav")
            {
                MessageBox.Show("wavファイルを入力してください。", "サウンドテスト", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Form_testSound soundfm = new Form_testSound();
            soundfm.strParam = m_sound.Text;
            soundfm.ShowDialog(this);
        }

        //スケジュール区分を選択したとき
        private void m_yoteikbn_SelectionChangeCommitted(object sender, EventArgs e)
        {
            koumokuDisable();

        }
        public void koumokuDisable()
        {
            //インシデント処理 
            //定期作業
            //計画作業
            if (m_yoteikbn.SelectedIndex == 0)
            {
                //インシデント処理
                m_start_date.Enabled = true;
                m_alermMessage.Enabled = true;
                m_end_date.Enabled = true;
                m_alermDate.Enabled = true;
                m_sound.Enabled = false;
                m_testbtn.Enabled = false;
                m_sansyoBtn.Enabled = false;
            }
            else if (m_yoteikbn.SelectedIndex == 1)
            {
                //定期作業
                m_start_date.Enabled = true;
                m_end_date.Enabled = true;
                m_alermMessage.Enabled = true;
                m_alermDate.Enabled = false;
                m_sound.Enabled = true;
                m_testbtn.Enabled = true;
                m_sansyoBtn.Enabled = true;
            }
            else if (m_yoteikbn.SelectedIndex == 2)
            {
                //計画作業
                m_start_date.Enabled = true;
                m_end_date.Enabled = true;
                m_alermDate.Enabled = false;
                m_alermMessage.Enabled = true;
                m_sound.Enabled = true;
                m_testbtn.Enabled = true;
                m_sansyoBtn.Enabled = true;
            }
            else if (m_yoteikbn.SelectedIndex == 3)
            {
                //特別対応
                m_start_date.Enabled = false;
                m_end_date.Enabled = false;
                m_alermMessage.Enabled = false;

                m_alermDate.Enabled = false;
                m_sound.Enabled = false;
                m_testbtn.Enabled = false;
                m_sansyoBtn.Enabled = false;
            }
        }
        //カラムクリックでソートを行う
        private void m_scheduleList_ColumnClick(object sender, ColumnClickEventArgs e)
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
            m_scheduleList.Sort();
        }
        //削除
        private void m_deleteBtn_Click(object sender, EventArgs e)
        {
            int count = m_scheduleList.SelectedItems.Count;

            //確認メッセージ
            if (MessageBox.Show("一覧に選択された行 " + count + "件 の削除を行います。その際参照している他のテーブルデータも削除されます。" + Environment.NewLine +
                "よろしいですか？", "スケジュール削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)

                return;


            int ret = deleteschedule();
            if (ret == -1)
            {
                return;
            }

            //リストの表示上からけす
            foreach (ListViewItem item in m_scheduleList.SelectedItems)
            {
                m_scheduleList.Items.Remove(item);
            }
        }

        //削除
        private int deleteschedule()
        {

            string scheduleno;
            int ret = 0;

            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "WITH DELETED AS (DELETE FROM timer_taiou where schedule_no = :no " +
                "RETURNING schedule_no) " +
                "DELETE FROM schedule where schedule_no = :no";

            using (var transaction = con.BeginTransaction())
            {
                foreach (ListViewItem item in m_scheduleList.SelectedItems)
                {
                    scheduleno = item.SubItems[0].Text;

                    var command = new NpgsqlCommand(@sql, con);
                    command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = int.Parse(scheduleno) });

                    Int32 rowsaffected;
                    try
                    {
                        //削除処理
                        rowsaffected = command.ExecuteNonQuery();
                        
                        if (rowsaffected < 1)
                        {
                            MessageBox.Show("削除できませんでした。スケジュール通番:" + scheduleno, "スケジュール情報削除");
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
                        MessageBox.Show("スケジュール情報削除時エラーが発生しました。 " + ex.Message);
                        if(transaction.Connection != null ) transaction.Rollback();
                        return -1;
                    }
                }
                if(ret == 1) {
                    MessageBox.Show("削除完了しました。", "スケジュール情報削除");
                    transaction.Commit();
                }
            }
            return ret;
        }

        //フォームが閉じる際
        private void Form_KeikakuDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            //サウンドファイルを削除する
            deleteSoundFile();
            /*          
                        string strpath = "";  
                        foreach (ListViewItem item in m_scheduleList.Items)
                        {

                            strpath = item.SubItems[8].Text;
                            if(strpath != null && strpath != "" && strpath != "標準サウンド.wav")
                            {
                                System.IO.File.Delete(@strpath);
                            }
                        }
            */
        }

        //サウンドファイルを削除する
        private void deleteSoundFile()
        {

            if (m_sound.Text != "")
            {
                //相対パスの時に削除する
                if (System.IO.Path.IsPathRooted(@m_sound.Text)== false)
                {
                    if (System.IO.File.Exists(@m_sound.Text))
                        System.IO.File.Delete(@m_sound.Text);
                }
            }
        }
        //カスタマコンボボックスが変更されたとき
        private void m_usernameCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (m_usernameCombo.Text == "")
            {
                m_userno.Text = "";
                return;
            }
            Read_systemCombo();
        }
        //システムコンボが変更されたとき
        private void m_systemCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (m_systemCombo.Text == "")
            {
                m_systemno.Text = "";
                return;
            }
            Read_siteCombo();
        }
        //拠点コンボボックスが変更されたとき
        private void m_siteCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //ラベルに反映
            if (m_siteCombo.SelectedValue != null)
                m_siteno.Text = m_siteCombo.SelectedValue.ToString();

        }
    }
}
