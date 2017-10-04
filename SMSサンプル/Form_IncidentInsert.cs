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
    public partial class Form_IncidentInsert : Form
    {
        //DBコネクション
        public NpgsqlConnection con { get; set; }
        public opeDS loginDS { get; set; }

        //カスタマ
        public List<userDS> userList { get; set; }
        //システム
        public List<systemDS> systemList { get; set; }
        //拠点
        public List<siteDS> siteList { get; set; }
        //ホスト
        public List<hostDS> hostList { get; set; }

        //?があるとnull許容型
        DateTime? dateTime_uketukedate;
        DateTime? dateTime_tehaidate;
        DateTime? dateTime_fukyudate;
        DateTime? dateTime_enddate;

        DateTime? dateTime_Timer;


        public Form_IncidentInsert()
        {
            InitializeComponent();
        }


        //表示前処理
        private void Form_IncidentInsert_Load(object sender, EventArgs e)
        {
            m_idlabel.Text = loginDS.opeid;
            m_labelinputOpe.Text = loginDS.lastname + loginDS.fastname;


            dateTime_uketukedate = null;
            dateTime_tehaidate = null;
            dateTime_fukyudate = null;
            dateTime_enddate = null;
            dateTime_Timer = null;

            //日付コントロールを空白にする
            setDateTimePicker(dateTime_uketukedate, m_uketukedate);
            setDateTimePicker(dateTime_tehaidate, m_tehaidate);
            setDateTimePicker(dateTime_fukyudate, m_fukyudate);
            setDateTimePicker(dateTime_enddate, m_enddate);
            setDateTimePicker(dateTime_Timer, m_timerpicker);

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
        //日付コントロールを空白にする
        private void setDateTimePicker(DateTime? datetime,DateTimePicker m_datetimepickercontrol)
        {
            if (datetime == null)
            {
                //DateTimePickerFormat.Custom　にして、CostomFormatは半角の空白を入れておくと、日時が非表示になる。
                m_datetimepickercontrol.Format = DateTimePickerFormat.Custom;
                m_datetimepickercontrol.CustomFormat = " ";
            }
            else
            {
                //フォーマットを元に戻して、値をセットする。
                m_datetimepickercontrol.CustomFormat = "yyyy年M月d日(ddd) HH:mm";
                m_datetimepickercontrol.Value = (DateTime)datetime;
            }
        }

        //システム名のコンボボックスが変更されたときの処理
        private void Read_siteCombo()
        {
            m_siteCombo.DataSource = null;
            m_siteno.Text = "";
            m_hostno.Text = "";
            m_hostCombo.DataSource = null;

            //ラベルに反映
            if (m_systemCombo.SelectedValue != null)
               m_systemno.Text = m_systemCombo.SelectedValue.ToString();

            //コンボボックス
            DataTable siteTable = new DataTable();
            siteTable.Columns.Add("ID", typeof(string));
            siteTable.Columns.Add("NAME", typeof(string));
            
            string systemid="";
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
                m_siteno.Text = m_siteCombo.SelectedValue.ToString();

        }
        //カスタマ名のコンボボックスが変更されたときの処理
        private void Read_systemCombo()
        {
            
            m_systemno.Text = "";
            m_systemCombo.DataSource = null;
            m_siteno.Text = "";
            m_siteCombo.DataSource = null;
            m_hostno.Text = "";
            m_hostCombo.DataSource = null;

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
        //ホスト名を読み込む
        private void Read_hostCombo()
        {
            try { 
                //ラベルに反映
                if (m_siteCombo.SelectedValue != null)
                    m_siteno.Text = m_siteCombo.SelectedValue.ToString();

                m_hostCombo.DataSource = null;
                m_hostno.Text = "";

                Class_Detaget getuser = new Class_Detaget();

                //ホスト名を検索
                List<hostDS> hostDSList = getuser.getHostList(m_siteno.Text, con, true);

                //空白行を追加
                hostDS tmp = new hostDS();
                tmp.hostname = "";
                tmp.host_no = "";
                List<hostDS> tmphostDSList = new List<hostDS>();
                tmphostDSList.Add(tmp);

                //取得した行を空行についか
                if (hostDSList != null)
                    tmphostDSList.AddRange(hostDSList);
            
                m_hostCombo.DataSource = tmphostDSList;
                m_hostCombo.DisplayMember = "hostname";
                m_hostCombo.ValueMember = "host_no";
                //ホスト名ラベルを表示
                if (hostDSList.Count > 0)
                    m_hostno.Text = m_hostCombo.SelectedValue.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show("ホストコンボボックスの一覧を取得することができませんでした。 " + ex.Message,"ホスト情報取得" );
            }
        }
        //カスタマコンボボックスが変更されたとき
        private void m_usernameCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if(m_usernameCombo.Text == "")
            {
                m_userno.Text = "";
                m_systemno.Text = "";
                m_systemCombo.DataSource = null;
                m_siteno.Text = "";
                m_siteCombo.DataSource = null;
                m_hostno.Text = "";
                m_hostCombo.DataSource = null;
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
                m_siteno.Text = "";
                m_siteCombo.DataSource = null;
                m_hostno.Text = "";
                m_hostCombo.DataSource = null;
                return;
            }
            Read_siteCombo();
        }
        //キャンセルボタン
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //拠点コンボボックスが変更されたとき
        private void m_siteCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (m_siteCombo.Text == "")
            {
                m_siteno.Text = "";
                m_hostno.Text = "";
                m_hostCombo.DataSource = null;

                return;
            }
            //ホスト名コンボボックスを取得する
            Read_hostCombo();
        }
        //ホストのコンボボックスが変更された時
        private void m_hostCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //ラベルに反映
            if (m_hostCombo.SelectedValue != null)
                m_hostno.Text = m_hostCombo.SelectedValue.ToString();
        }
        //登録ボタン
        private void button3_Click(object sender, EventArgs e)
        {
            //確認
            if (MessageBox.Show("インシデントデータを登録します。よろしいですか？", "登録確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int? userno = null;
            if (m_userno.Text != null && m_userno.Text != "")
                userno = int.Parse(m_userno.Text);
            int? systemno = null;
            if (m_systemno.Text != null && m_systemno.Text != "")
                systemno = int.Parse(m_systemno.Text);

            int? siteno = null;
            if (m_siteno.Text != null && m_siteno.Text != "")
                siteno = int.Parse(m_siteno.Text);

            int? hostno = null;
            if (m_hostno.Text != null && m_hostno.Text != "")
                hostno = int.Parse(m_hostno.Text);

            //ステータス 1:未完了 0:完了
            string status = "";
            if (m_statuscheck.Checked)
                //完了
                status = "0";
            else
                //未完了
                status = "1";

            int MPMSSno = 0;
            if (m_MSMSno.Text != "")
                int.TryParse(m_MSMSno.Text,out MPMSSno);

            string ScubeID = m_ScubeID.Text;

            //1:アラーム検知 2:障害申告 3:問い合わせ
            int kubunint= m_incident_kubun_combo.SelectedIndex + 1;
            string incident_kubun_combo = kubunint.ToString();

            string content = m_content.Text;
            int matstatus = m_matflgCombo.SelectedIndex;
            string matflg;
            if (matstatus == 0)
                matflg = "1";
            else
                matflg = "0";

            string matcommand = m_matcommand.Text;

            DateTime? timer = null;
            if (m_timerpicker.Text.Trim() != "")
                timer = m_timerpicker.Value;


            string kakunin = m_kakunin.Text;

            DateTime? uketukedate = null;
            if (m_uketukedate.Text.Trim() != "")
                uketukedate = m_uketukedate.Value;

            DateTime? tehaidate =null;
            if (m_tehaidate.Text.Trim() != "")
                tehaidate = m_tehaidate.Value;


            DateTime? fukyudate = null;
            if (m_fukyudate.Text.Trim() != "")
                fukyudate = m_fukyudate.Value;

            DateTime? enddate = null;
            if (m_enddate.Text.Trim() != "")
                enddate = m_enddate.Value;

            NpgsqlCommand cmd;
            try
            {
                if (con.FullState != ConnectionState.Open) con.Open();

                Int32 rowsaffected;

                //データ登録
                cmd = new NpgsqlCommand(@"insert into incident(status,mpms_incident,s_cube_id,incident_type,content,matflg,matcommand,uketukedate,tehaidate,fukyudate,enddate,timer,kakunin,userno,systemno,siteno,hostno,chk_name_id) 
                    values ( :status,:mpms_incident,:s_cube_id,:incident_type,:content,:matflg,:matcommand,:uketukedate,:tehaidate,:fukyudate,:enddate,:timer,:kakunin,:userno,:systemno,:siteno,:hostno,:chk_name_id);" +
                    "select currval('incident_incident_no_seq')", con);

                cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                cmd.Parameters.Add(new NpgsqlParameter("mpms_incident", DbType.Int32) { Value = MPMSSno });
                cmd.Parameters.Add(new NpgsqlParameter("s_cube_id", DbType.String) { Value = ScubeID });
                cmd.Parameters.Add(new NpgsqlParameter("incident_type", DbType.String) { Value = incident_kubun_combo });
                cmd.Parameters.Add(new NpgsqlParameter("content", DbType.String) { Value = content });
                cmd.Parameters.Add(new NpgsqlParameter("matflg", DbType.String) { Value = matflg });
                cmd.Parameters.Add(new NpgsqlParameter("matcommand", DbType.String) { Value = matcommand });
                cmd.Parameters.Add(new NpgsqlParameter("uketukedate", DbType.DateTime) { Value = uketukedate });
                cmd.Parameters.Add(new NpgsqlParameter("tehaidate", DbType.DateTime) { Value = tehaidate });
                cmd.Parameters.Add(new NpgsqlParameter("fukyudate", DbType.DateTime) { Value = fukyudate });
                cmd.Parameters.Add(new NpgsqlParameter("enddate", DbType.DateTime) { Value = enddate });
                cmd.Parameters.Add(new NpgsqlParameter("timer", DbType.DateTime) { Value = timer });
                cmd.Parameters.Add(new NpgsqlParameter("kakunin", DbType.String) { Value = kakunin });
                cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = userno });
                cmd.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = systemno });
                cmd.Parameters.Add(new NpgsqlParameter("siteno", DbType.Int32) { Value = siteno });
                cmd.Parameters.Add(new NpgsqlParameter("hostno", DbType.Int32) { Value = hostno });

                cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = m_idlabel.Text });

                NpgsqlParameter firstColumn = new NpgsqlParameter("firstcolumn", DbType.Int32);
                firstColumn.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(firstColumn);

                rowsaffected = cmd.ExecuteNonQuery();

                if (rowsaffected != 1)
                {
                    MessageBox.Show("登録できませんでした。", "インシデント登録");
                }
                else
                {
                    int currval = int.Parse(firstColumn.NpgsqlValue.ToString());

                    //スケジュールを登録
                    if(timer != null) {
                        DateTime alertTime = (DateTime)timer;
                    
                        //1時間出し続ける
                        DateTime endTime = alertTime.AddMinutes(60);
                        //スケジュールを登録
                        try
                        {
                            if (con.FullState != ConnectionState.Open) con.Open();
                            //データ登録
                            cmd = new NpgsqlCommand(@"insert into schedule(userno,systemno,timer_name,schedule_type,repeat_type,start_date,end_date,alerm_message,status,sound,chk_name_id) " +
                                "values ( :userno,:systemno,:timer_name,:schedule_type,:repeat_type,:start_date,:end_date,:alerm_message,:status,:sound,:chk_name_id); " +
                                "select currval('schedule_schedule_no_seq') ;", con);

                            cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = userno });
                            cmd.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = systemno });
                            cmd.Parameters.Add(new NpgsqlParameter("timer_name", DbType.String) { Value = "インシデント管理タイマー" });
                            cmd.Parameters.Add(new NpgsqlParameter("schedule_type", DbType.String) { Value = "1" });
                            cmd.Parameters.Add(new NpgsqlParameter("repeat_type", DbType.String) { Value = "1" });
                            cmd.Parameters.Add(new NpgsqlParameter("start_date", DbType.DateTime) { Value = DateTime.Now });
                            cmd.Parameters.Add(new NpgsqlParameter("end_date", DbType.DateTime) { Value = endTime });
                            cmd.Parameters.Add(new NpgsqlParameter("alerm_message", DbType.String) { Value = kakunin });
                            //0:無効 1:有効
                            cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = 1 });
                            cmd.Parameters.Add(new NpgsqlParameter("sound", DbType.Binary) { Value = null });
                            cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = m_idlabel.Text });

                            //OUTパラメータをセットする
                            Int32 rowsaffectedsch = 0;
                            NpgsqlParameter scheduleColumn = new NpgsqlParameter("scheduleColumn", DbType.Int32);
                            scheduleColumn.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(scheduleColumn);
                            rowsaffectedsch = cmd.ExecuteNonQuery();

                            int currvalsch = int.Parse(scheduleColumn.NpgsqlValue.ToString());

                            if (rowsaffected != 1)
                            {
                                MessageBox.Show("登録できませんでした。", "タイマー登録");
                            }
                            else
                            {
                                if (currvalsch > 0)
                                {
                                    //引き続きアラートデータを作成し登録する
                                    alerm_insert(currvalsch, alertTime);
                                    //登録成功
                                    //MessageBox.Show("登録完了 " + "スケジュール番号" + currval, "タイマー登録");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("スケジュールに登録する際にエラーが発生しました。 " + ex.Message, "インシデント登録");
                            return;
                        }
                    }
                    //登録成功
                    MessageBox.Show("登録完了", "インシデント登録");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("インシデント登録時エラー " + ex.Message);
                return;
            }

        }

        //タイマー対応テーブルに登録する
        int alerm_insert(int scheNO,  DateTime alertdatetime)
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
                //インシデントなので1固定
                cmd.Parameters.Add(new NpgsqlParameter("schedule_type", DbType.String) { Value = "1" });
                cmd.Parameters.Add(new NpgsqlParameter("alertdatetime", DbType.DateTime) { Value = alertdatetime });
                cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = m_idlabel.Text });
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

        //手配日付の値が変更されたとき
        private void m_tehaidate_ValueChanged(object sender, EventArgs e)
        {
            //dateTimePicker1の値が変更されたら表示する
            dateTime_tehaidate = m_tehaidate.Value;
            setDateTimePicker(dateTime_tehaidate, m_tehaidate);
        }
        //手配日付
        private void m_tehaidate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                //Deleteキーが押されたら、dateTimeにnullを設定してdateTimePicker1を非表示に
                dateTime_tehaidate = null;
                setDateTimePicker(dateTime_tehaidate, m_tehaidate);
            }
        }
        //受付日付の値が変更されたとき
        private void m_uketukedate_ValueChanged(object sender, EventArgs e)
        {
            //dateTimePicker1の値が変更されたら表示する
            dateTime_uketukedate = m_uketukedate.Value;
            setDateTimePicker(dateTime_uketukedate, m_uketukedate);
        }
        //受付日付
        private void m_uketukedate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                //Deleteキーが押されたら、dateTimeにnullを設定してdateTimePicker1を非表示に
                dateTime_uketukedate = null;
                setDateTimePicker(dateTime_uketukedate, m_uketukedate);
            }
        }
        //復旧日時
        private void m_fukyudate_ValueChanged(object sender, EventArgs e)
        {
            //dateTimePicker1の値が変更されたら表示する
            dateTime_fukyudate = m_fukyudate.Value;
            setDateTimePicker(dateTime_fukyudate, m_fukyudate);
        }

        //復旧日時
        private void m_fukyudate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                //Deleteキーが押されたら、dateTimeにnullを設定してdateTimePicker1を非表示に
                dateTime_fukyudate = null;
                setDateTimePicker(dateTime_fukyudate, m_fukyudate);
            }
        }

        //完了日付
        private void m_enddate_ValueChanged(object sender, EventArgs e)
        {
            //dateTimePicker1の値が変更されたら表示する
            dateTime_enddate = m_enddate.Value;
            setDateTimePicker(dateTime_enddate, m_enddate);
        }
        //完了日付
        private void m_enddate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                //Deleteキーが押されたら、dateTimeにnullを設定してdateTimePicker1を非表示に
                dateTime_enddate = null;
                setDateTimePicker(dateTime_enddate, m_enddate);
            }
        }
        //タイマー
        private void m_timerpicker_ValueChanged(object sender, EventArgs e)
        {
            //dateTimePicker1の値が変更されたら表示する
            dateTime_Timer = m_timerpicker.Value;
            setDateTimePicker(dateTime_Timer, m_timerpicker);
        }
        //タイマー
        private void m_timerpicker_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                //Deleteキーが押されたら、dateTimeにnullを設定してdateTimePicker1を非表示に
                dateTime_Timer = null;
                setDateTimePicker(dateTime_Timer, m_timerpicker);
            }
        }
        //メール出力ボタン
        private void button4_Click(object sender, EventArgs e)
        {
            Form_mailTempleteList mailselectform = new Form_mailTempleteList();
            mailselectform.con = con;
            mailselectform.loginDS = loginDS;
            mailselectform.Show();
        }

        private void m_incident_kubun_combo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //テンプレート
        private void m_templeteCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //インシデントテンプレート
            
        }
        //テンプレートの登録
        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
