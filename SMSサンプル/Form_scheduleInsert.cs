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
    public partial class Form_scheduleInsert : Form
    {
        //DBコネクション
        public NpgsqlConnection con { get; set; }

        //ログイン情報
        public opeDS loginDS { get; set; }

        //カスタマ
        public List<userDS> userList { get; set; }

        //システム
        public List<systemDS> systemList { get; set; }

        //拠点
        public List<siteDS> siteList { get; set; }
        public Form_scheduleInsert()
        {
            InitializeComponent();
        }
        //テストサウンド
        private void button4_Click(object sender, EventArgs e)
        {
            if (m_soudpath.Text == "" )
            {
                MessageBox.Show("wavファイルを入力してください。", "サウンドテスト", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //拡張子がwavファイル
            string stExtension = System.IO.Path.GetExtension(m_soudpath.Text);
            if(stExtension != ".wav")
            {
                MessageBox.Show("wavファイルを入力してください。", "サウンドテスト", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Form_testSound soundfm = new Form_testSound();
            soundfm.strParam = m_soudpath.Text;
            soundfm.ShowDialog(this);

        }
        //参照
        private void button1_Click(object sender, EventArgs e)
        {
            string str = "";
            //
            Class_common common = new Class_common();
            str = common.Disp_FileSelectDlg("wav");

            m_soudpath.Text = str;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //登録ボタン
        private void button3_Click(object sender, EventArgs e)
        {
            //カスタマ名
            if (m_usernameCombo.Text == "")
            {
                MessageBox.Show("カスタマ名が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //システム名
            if (m_systemCombo.Text == "")
            {
                MessageBox.Show("システム名が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //拠点名は必須
            if (m_siteCombo.Text == "")
            {
                MessageBox.Show("拠点名が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            //タイマー名
            if (m_timer_name.Text == "")
            {
                MessageBox.Show("タイマー名が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            //予定区分が入力されていません
            if (m_schedule_combo.Text == "")
            {
                MessageBox.Show("予定区分が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //特別対応以外のとき
            if (m_schedule_combo.SelectedIndex != 3)
            {
                //アラーム日時
                if (m_alermDate.Text == "")
                {
                    MessageBox.Show("アラーム日時が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //メッセージ
                if (m_message.Text == "")
                {
                    MessageBox.Show("メッセージが入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //音が入力されていません
                if ((m_soudpath.Text == "" || System.IO.File.Exists(m_soudpath.Text) == false) && m_schedule_combo.SelectedIndex != 0)
                {

                    MessageBox.Show("アラーム音が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            //確認画面
            if(m_radio_hour.Checked )
            {
                if (MessageBox.Show("1時間毎のアラームの場合、終了時間によりタイマーの登録に時間を要する場合があります。よろしいですか？", "登録確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }
            else { 
                if (MessageBox.Show("アラーム情報を登録します。よろしいですか？", "登録確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }
            string userno = m_userno.Text;
            string systemno = m_systemno.Text;
            string siteno = m_siteno.Text;
            string timername = m_timer_name.Text;
            string schedule_type = m_schedule_combo.Text;
            string type = schedule_type.Split(':')[0];
            string message = m_message.Text;

            //ステータス
            // 0:無効 1:有効
            string status = "";
            if (m_radio_enable.Checked)
                //有効
                status = "1";

            else
                //無効
                status = "0";

            //繰り返し区分
            string repeat_type = "0"; 
            if(m_radio_one.Checked)
                repeat_type = "1";
            else if (m_radio_hour.Checked)
                    repeat_type = "2";
            else if (m_radio_day.Checked)
                repeat_type = "3";
            else if (m_radio_week.Checked )
                repeat_type = "4";
            else if (m_radio_month.Checked)
                repeat_type = "5";


            byte[] bytes= null;
            //開始日時
            DateTime ? startdate = null;
            DateTime? enddate = null;
            if (m_schedule_combo.SelectedIndex != 3 )
            {
                startdate = m_startDate.Value;
                //終了日時
                enddate = m_endDate.Value;

                //インシデントのときは音は登録しない
                if ( m_schedule_combo.SelectedIndex != 0) {
                    bytes = File.ReadAllBytes(m_soudpath.Text);
                }
            }

            //DB接続
            NpgsqlCommand cmd;
            try
            {
                if (con.FullState != ConnectionState.Open) con.Open();
                Int32 rowsaffected;
                //データ登録
                cmd = new NpgsqlCommand(@"insert into schedule(userno,systemno,siteno,timer_name,schedule_type,repeat_type,start_date,end_date,alerm_message,status,sound,chk_name_id) 
                    values ( :userno,:systemno,:siteno,:timer_name,:schedule_type,:repeat_type,:start_date,:end_date,:alerm_message,:status,:sound,:chk_name_id); " +
                    "select currval('schedule_schedule_no_seq') ;", con);

                cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = userno });
                cmd.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = systemno });
                cmd.Parameters.Add(new NpgsqlParameter("siteno", DbType.Int32) { Value = siteno });
                cmd.Parameters.Add(new NpgsqlParameter("timer_name", DbType.String) { Value = timername });
                cmd.Parameters.Add(new NpgsqlParameter("schedule_type", DbType.String) { Value = type });
                cmd.Parameters.Add(new NpgsqlParameter("repeat_type", DbType.String) { Value = repeat_type });
                cmd.Parameters.Add(new NpgsqlParameter("start_date", DbType.DateTime) { Value = startdate });
                cmd.Parameters.Add(new NpgsqlParameter("end_date", DbType.DateTime) { Value = enddate });
                cmd.Parameters.Add(new NpgsqlParameter("alerm_message", DbType.String) { Value = message });
                cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                cmd.Parameters.Add(new NpgsqlParameter("sound", DbType.Binary) { Value = bytes });
                cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = m_idlabel.Text });

                //OUTパラメータをセットする
                NpgsqlParameter firstColumn = new NpgsqlParameter("firstcolumn", DbType.Int32);
                firstColumn.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(firstColumn);
                rowsaffected = cmd.ExecuteNonQuery();

                int currval = int.Parse(firstColumn.NpgsqlValue.ToString());

                if (rowsaffected != 1)
                {
                    MessageBox.Show("登録できませんでした。", "タイマー登録");
                }
                else
                {
                    if(currval > 0) {
                        if (m_schedule_combo.SelectedIndex != 3) { 
                            //引き続きアラートデータを作成し登録する
                            make_alert(currval,type);
                        }
                        //登録成功
                        MessageBox.Show("登録完了 " + "スケジュール番号" + currval, "タイマー登録");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("タイマー登録エラー " + ex.Message);
                return;
            }

        }
        // 引き続きアラートデータを作成し登録する
        // タイマースケジュールを登録する
        public void make_alert(int scheNO, string type)
        {
            //ステータス
            if (m_radio_mukou.Checked)
                return;
            //アラーム周期            
            //1回の時
            if (m_radio_one.Checked)
            {
                //入力された日時を登録する
                DateTime dd = m_alermDate.Value;
                alerm_insert(scheNO, type, dd);
            }
            //毎時の時
            else if (m_radio_hour.Checked) {
                DateTime alertdate = m_alermDate.Value.Date;
                TimeSpan dtt = m_alermDate.Value.TimeOfDay;
                alertdate = alertdate + dtt;
                //開始日
                DateTime startdt = m_startDate.Value;

                //終了日
                DateTime enddt = m_endDate.Value;

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
                    int ret =alerm_insert(scheNO, type, alertdate);
                    if (ret == -1)
                        break;

                    //1時間プラス
                    alertdate = alertdate.AddHours(1);
                    //i++;
                }
            }
            //日
            else if (m_radio_day.Checked) {
                
                //開始日
                DateTime alertdate = m_startDate.Value.Date;
                TimeSpan dtt = m_alermDate.Value.TimeOfDay;
                alertdate = alertdate + dtt;
                
                //開始日
                DateTime startdt = m_startDate.Value;

                //終了日
                DateTime enddt = m_endDate.Value;

                while (true)
                {

                    //開始日時以前なら登録しない
                    if (startdt > alertdate) { 
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
            else if (m_radio_week.Checked) {
                int weeknumber  = (int)m_alermDate.Value.DayOfWeek;
                TimeSpan dtt = m_alermDate.Value.TimeOfDay;
                DateTime alertdate = m_startDate.Value.Date;
                alertdate = alertdate + dtt;

                //開始日
                DateTime startdt = m_startDate.Value;
                int startweekint = (int)m_startDate.Value.DayOfWeek;

                //終了日
                DateTime enddt = m_endDate.Value;
                int endweekint = (int)m_endDate.Value.DayOfWeek;


                int tmpweeknumber = startweekint;
                while (true)
                {

                    //開始日時以前なら登録しない
                    if (startdt > alertdate) {
                        alertdate = alertdate.AddDays(1);
                        tmpweeknumber = (int)alertdate.DayOfWeek;
                        continue;
                    }
                    //終了日まで登録する。
                    if (enddt <= alertdate.AddMinutes(-1))
                        break;

                    //同じ曜日であれば登録
                    if(weeknumber == tmpweeknumber) { 

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
            else if (m_radio_month.Checked) {

                //開始日
                DateTime alertdate = m_startDate.Value.Date;
                TimeSpan dtt = m_alermDate.Value.TimeOfDay;
                alertdate = alertdate + dtt;

                //開始日
                DateTime startdt = m_startDate.Value;

                //終了日
                DateTime enddt = m_endDate.Value;

                while (true)
                {

                    //開始日時以前なら登録しない
                    if (startdt > alertdate) {

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
        int  alerm_insert(int scheNO, string type,DateTime alertdatetime)
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
        
        //表示前処理
        private void Form_TimerInsert_Load(object sender, EventArgs e)
        {
            m_idlabel.Text = loginDS.opeid;
            m_labelinputOpe.Text = loginDS.lastname + loginDS.fastname;


            //コンボボックス
            DataTable cutomerTable = new DataTable();
            cutomerTable.Columns.Add("ID", typeof(string));
            cutomerTable.Columns.Add("NAME", typeof(string));
            
            Class_Detaget getuser = new Class_Detaget();
            getuser.con = con;

            //カスタマ名を取得
            userList = getuser.getUserList();
            
            if (userList == null)
                return;

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

            Read_systemCombo();

            //3年以上の期間は入力できない
            m_endDate.MaxDate = DateTime.Now.AddYears(3);

            //利用不可にする
            m_startDate.Enabled = false;
            m_endDate.Enabled = false;
        }
        //システム名のコンボボックスが変更されたときの処理
        private void Read_siteCombo()
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
                m_siteno.Text = m_siteCombo.SelectedValue.ToString();

        }

        //システム名一覧を取得してコンボボックスの値に設定する
        private void Read_systemCombo()
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




        //システムコンボボックスが変更されたときにデータ取得されているかの確認
        private void m_usernameCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (m_usernameCombo.Text == "")
            {
                m_userno.Text = "";
                return;
            }
            Read_systemCombo();
        }


        
        //1回が選択されたとき
        private void m_radio_one_CheckedChanged(object sender, EventArgs e)
        {
            //利用不可にする
            m_startDate.Enabled = false;
            m_endDate.Enabled = false;
            m_alermDate.CustomFormat = "yyyy年M月d日(dddd) HH:mm";
        }
        //時間が選択されたとき
        private void m_radio_hour_CheckedChanged(object sender, EventArgs e)
        {
            //利用可能にする
            m_startDate.Enabled = true;
            m_endDate.Enabled = true;
            m_alermDate.CustomFormat = "mm分";


        }
        //日が選択されたとき
        private void m_radio_day_CheckedChanged(object sender, EventArgs e)
        {
            //利用可能にする
            m_startDate.Enabled = true;
            m_endDate.Enabled = true;
            m_alermDate.CustomFormat = "HH:mm";
        }
        //週が選択されたとき
        private void m_radio_week_CheckedChanged(object sender, EventArgs e)
        {
            //利用可能にする
            m_startDate.Enabled = true;
            m_endDate.Enabled = true;
            m_alermDate.CustomFormat = "(dddd) HH:mm";

        }
        //つきが選択されたとき
        private void m_radio_month_CheckedChanged(object sender, EventArgs e)
        {
            //利用可能にする
            m_startDate.Enabled = true;
            m_endDate.Enabled = true;
            m_alermDate.CustomFormat = "d日  HH:mm";

        }
        //予定区分が変更されたとき
        private void m_schedule_combo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (m_schedule_combo.SelectedIndex == 0) {
                m_alerm_group.Enabled = true;
                //インシデントは音は選択できない
                m_soudpath.Enabled = false;
                button1.Enabled = false;
                button4.Enabled = false;

                m_startDate.Enabled = true;
                m_endDate.Enabled = true;

            }
            //特別作業の場合開始終了は不要
            else if (m_schedule_combo.SelectedIndex == 3)
            {
                m_alerm_group.Enabled = false;
                m_startDate.Enabled = false;
                m_endDate.Enabled = false;
                
            }
            else
            {
                m_soudpath.Enabled = true;
                button1.Enabled = true;
                button4.Enabled = true;

                m_alerm_group.Enabled = true;
                m_startDate.Enabled = true;
                m_endDate.Enabled = true;

            }
        }
        //拠点コンボボックスが変更されたとき
        private void m_siteCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (m_siteCombo.Text == "")
            {
                m_siteno.Text = "";
                return;
            }
            //ラベルに反映
            if (m_siteCombo.SelectedValue != null)
                m_siteno.Text = m_siteCombo.SelectedValue.ToString();

        }
        //システムコンボボックスが変更されたとき
        private void m_systemCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (m_systemCombo.Text == "")
            {
                m_systemno.Text = "";
                return;
            }
            Read_siteCombo();
        }
    }
}
