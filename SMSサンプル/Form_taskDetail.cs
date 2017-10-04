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
    public partial class Form_taskDetail : Form
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        //DBコネクション
        public NpgsqlConnection con { get; set; }

        //ログイン情報
        public opeDS loginDS { get; set; }

        public  List<templeteDS> templist { get; set; }

        //カスタマ
        public List<userDS> userList { get; set; }

        //システム
        public List<systemDS> systemList { get; set; }

        //拠点
        public List<siteDS> siteList { get; set; }

        public taskDS taskds { get; set; }
        public timerDS timerds { get; set; }
        public List<timerDS> timerList { get; set; }

        //空欄許可のdatetimepicker
        DateTime? dateTime_alermDate;


        //ListViewのソートの際に使用する
        private Class_ListViewColumnSorter _columnSorter;

        public Form_taskDetail()
        {
            InitializeComponent();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
        //表示前
        private void Form_taskDetail_Load(object sender, EventArgs e)
        {
            this.splitContainer1.SplitterDistance = 32;
            _columnSorter = new Class_ListViewColumnSorter();
            m_taskList.ListViewItemSorter = _columnSorter;

            dateTime_alermDate = null;

            m_selectKoumoku.Items.Add("タスク通番");
            m_selectKoumoku.Items.Add("タスク区分");
            m_selectKoumoku.Items.Add("カスタマ名");
            m_selectKoumoku.Items.Add("テンプレート");
            m_selectKoumoku.Items.Add("開始日時");
            m_selectKoumoku.Items.Add("終了日時");
            m_selectKoumoku.Items.Add("ステータス");
            m_selectKoumoku.Items.Add("内容");
            m_selectKoumoku.Items.Add("備考");
            m_selectKoumoku.Items.Add("更新日時");
            m_selectKoumoku.Items.Add("更新者");
            m_selectKoumoku.Items.Add("タイマー名");
            m_selectKoumoku.Items.Add("アラート日時");
            m_selectKoumoku.Items.Add("タイマーステータス");
            m_selectKoumoku.Items.Add("繰り返し区分");
            m_selectKoumoku.Items.Add("タイマー開始日時");
            m_selectKoumoku.Items.Add("タイマー終了日時");
            m_selectKoumoku.Items.Add("作成日時");
            m_selectKoumoku.Items.Add("作成者");

            if (taskds != null)
            {
                Class_Detaget dg = new Class_Detaget();
                Dictionary<string, string> param_dict = new Dictionary<string, string>();
                
                param_dict["schedule_no"] = taskds.schedule_no;
                //param_dict["timerid"] = ;

                //タイマー情報を取得
                timerList = dg.getTimer(param_dict, con);

                //タスクを取得する
                gettask(taskds, timerList);

            }
        }

        //タスクを取得する
        void gettask (taskDS taskds,List<timerDS> timerList)
        {
            System.DateTime dd1;

            this.m_taskno.Text = taskds.schedule_no;
            this.m_schedule_combo.SelectedIndex = int.Parse(taskds.schedule_type)-1;
            this.m_userno.Text = taskds.userno;

            //            this.m_templeteCombo.Text = taskds.templeteno;
            this.m_statusCombo.Text = taskds.status;

            this.m_naiyou.Text = taskds.naiyou;
            this.m_startDate.Text = taskds.startdate;
            this.m_endDate.Text = taskds.enddate;
            this.m_naiyou.Text = taskds.naiyou;
            this.m_biko.Text = taskds.biko;

            //登録日時
            this.m_labelinputOpe.Text = taskds.ins_date;
            //登録者
            this.m_inputope.Text = taskds.ins_name_id;
            //更新日時
            this.m_update.Text = taskds.chk_date;
            //更新者
            this.m_idlabel.Text = taskds.chk_name_id;

            foreach (timerDS timerds in timerList)
            {
                if (timerds.timerid == "1")
                {
                    this.m_title1.Text = timerds.timername;
                    this.m_alermDate1.Text = timerds.alert_time;
                    this.m_statusCombo1.Text = timerds.status;
                    string radionum = timerds.repeat_type;

                    //ラジオボタンをチェックする
                    radio_check(timerds.timerid, radionum);

                    //アラーム日時
                    if (timerds.alert_time != null && timerds.alert_time != "")
                    {
                        this.m_alermDate1.Enabled = true;
                        //dd1 = DateTime.ParseExact(timerds.alert_time, "yyyy/MM/dd HH:mm:ss", null);

                        dd1 = DateTime.Parse(timerds.alert_time);
                        this.m_alermDate1.Value = dd1;
                    }


                    //開始日時の取得
                    if (timerds.start_date != null && timerds.start_date != "")
                    {
                        m_startDate1.Checked = true;

                        //dd1 = DateTime.ParseExact(timerds.start_date, "yyyy/MM/dd HH:mm:ss", null);
                        dd1 = DateTime.Parse(timerds.start_date);
                        this.m_startDate1.Value = dd1;
                    }
                    else
                    {
                        m_startDate1.Checked = false;

                    }
                    //終了日時
                    if (timerds.end_date != null && timerds.end_date != "")
                    {
                        m_endDate1.Checked = true;
                        //dd1 = DateTime.ParseExact(timerds.end_date, "yyyy/MM/dd HH:mm:ss", null);
                        dd1 = DateTime.Parse(timerds.end_date);
                        if (dd1 > m_endDate1.MaxDate)
                            dd1 = m_endDate1.MaxDate;
                        this.m_endDate1.Value = dd1;
                    }
                    else
                    {
                        m_endDate1.Checked = false;

                    }

                }

                //タイマー②
                if (timerds.timerid == "2")
                {
                    this.m_title2.Text = timerds.timername;
                    this.m_alermDate2.Text = timerds.alert_time;
                    this.m_statusCombo2.Text = timerds.status;
                    string radionum = timerds.repeat_type;

                    //ラジオボタンをチェックする
                    radio_check(timerds.timerid, radionum);


                    //アラーム日時
                    if (timerds.alert_time != null && timerds.alert_time != "")
                    {
                        this.m_alermDate2.Enabled = true;
                        dd1 = DateTime.Parse(timerds.alert_time);
                        //dd1 = DateTime.ParseExact(timerds.alert_time, "yyyy/MM/dd HH:mm:ss", null);
                        this.m_alermDate2.Value = dd1;
                    }


                    //開始日時の取得
                    if (timerds.start_date != null && timerds.start_date != "")
                    {
                        m_startDate2.Checked = true;
                        //dd1 = DateTime.ParseExact(timerds.start_date, "yyyy/MM/dd HH:mm:ss", null);
                        dd1 = DateTime.Parse(timerds.start_date);
                        this.m_startDate2.Value = dd1;
                    }
                    else

                        m_startDate2.Checked = false;


                    //終了日時
                    if (timerds.end_date != null && timerds.end_date != "")
                    {
                        m_endDate2.Checked = true;
                        //dd1 = DateTime.ParseExact(timerds.end_date, "yyyy/MM/dd HH:mm:ss", null);
                        dd1 = DateTime.Parse(timerds.end_date);
                        if (dd1 > m_endDate2.MaxDate)
                            dd1 = m_endDate2.MaxDate;
                        this.m_endDate2.Value = dd1;
                    }
                    else
                        m_endDate2.Checked = false;


                }
                //タイマー③
                if (timerds.timerid == "3")
                {
                    this.m_title3.Text = timerds.timername;
                    this.m_alermDate3.Text = timerds.alert_time;
                    this.m_statusCombo3.Text = timerds.status;
                    string radionum = timerds.repeat_type;

                    //ラジオボタンをチェックする
                    radio_check(timerds.timerid, radionum);


                    //アラーム日時
                    if (timerds.alert_time != null && timerds.alert_time != "")
                    {
                        this.m_alermDate3.Enabled = true;
                        //dd1 = DateTime.ParseExact(timerds.alert_time, "yyyy/MM/dd HH:mm:ss", null);
                        dd1 = DateTime.Parse(timerds.alert_time);
                        this.m_alermDate3.Value = dd1;
                    }


                    //開始日時の取得
                    if (timerds.start_date != null && timerds.start_date != "")
                    {
                        //dd1 = DateTime.ParseExact(timerds.start_date, "yyyy/MM/dd HH:mm:ss", null);
                        dd1 = DateTime.Parse(timerds.start_date);
                        this.m_startDate3.Value = dd1;
                    }
                    else
                    {
                        m_startDate3.Checked = false;
                    }
                    //終了日時
                    if (timerds.end_date != null && timerds.end_date != "")
                    {
                        m_endDate3.Checked = true;
                        //dd1 = DateTime.ParseExact(timerds.end_date, "yyyy/MM/dd HH:mm:ss", null);
                        dd1 = DateTime.Parse(timerds.end_date);
                        if (dd1 > m_endDate3.MaxDate)
                            dd1 = m_endDate3.MaxDate;
                        this.m_endDate3.Value = dd1;
                    }
                    else
                        m_endDate3.Checked = false;

                }
                //タイマー④
                if (timerds.timerid == "4")
                {
                    this.m_title4.Text = timerds.timername;
                    this.m_alermDate4.Text = timerds.alert_time;
                    this.m_statusCombo4.Text = timerds.status;
                    string radionum = timerds.repeat_type;

                    //ラジオボタンをチェックする
                    radio_check(timerds.timerid, radionum);


                    //アラーム日時
                    if (timerds.alert_time != null && timerds.alert_time != "")
                    {
                        this.m_alermDate4.Enabled = true;
                        //dd1 = DateTime.ParseExact(timerds.alert_time, "yyyy/MM/dd HH:mm:ss", null);
                        dd1 = DateTime.Parse(timerds.alert_time);
                        this.m_alermDate4.Value = dd1;
                    }


                    //開始日時の取得
                    if (timerds.start_date != null && timerds.start_date != "")
                    {
                        m_startDate4.Checked = true;
                        //dd1 = DateTime.ParseExact(timerds.start_date, "yyyy/MM/dd HH:mm:ss", null);
                        dd1 = DateTime.Parse(timerds.start_date);
                        this.m_startDate4.Value = dd1;
                    }
                    else
                    {
                        m_startDate4.Checked = false;
                    }
                    //終了日時
                    if (timerds.end_date != null && timerds.end_date != "")
                    {
                        m_endDate4.Checked = true;
                        //dd1 = DateTime.ParseExact(timerds.end_date, "yyyy/MM/dd HH:mm:ss", null);
                        dd1 = DateTime.Parse(timerds.end_date);
                        if (dd1 > m_endDate4.MaxDate)
                            dd1 = m_endDate4.MaxDate;
                        this.m_endDate4.Value = dd1;
                    }
                    else
                        m_endDate4.Checked = false;


                }

                //タイマー⑤
                if (timerds.timerid == "5")
                {
                    this.m_title5.Text = timerds.timername;
                    this.m_alermDate5.Text = timerds.alert_time;
                    this.m_statusCombo5.Text = timerds.status;
                    string radionum = timerds.repeat_type;

                    //ラジオボタンをチェックする
                    radio_check(timerds.timerid, radionum);


                    //アラーム日時
                    if (timerds.alert_time != null && timerds.alert_time != "")
                    {

                        //dd1 = DateTime.ParseExact(timerds.alert_time, "yyyy/MM/dd HH:mm:ss", null);
                        dd1 = DateTime.Parse(timerds.alert_time);

                        this.m_alermDate5.Value = dd1;
                    }


                    //開始日時の取得
                    if (timerds.start_date != null && timerds.start_date != "")
                    {
                        m_startDate5.Checked = true;
                        //dd1 = DateTime.ParseExact(timerds.start_date, "yyyy/MM/dd HH:mm:ss", null);
                        dd1 = DateTime.Parse(timerds.start_date);
                        this.m_startDate5.Value = dd1;
                    }
                    else
                    {
                        m_startDate5.Checked = false;
                    }
                    //終了日時
                    if (timerds.end_date != null && timerds.end_date != "")
                    {
                        m_endDate5.Checked = true;
                        //dd1 = DateTime.ParseExact(timerds.end_date, "yyyy/MM/dd HH:mm:ss", null);
                        dd1 = DateTime.Parse(timerds.end_date);
                        if (dd1 > m_endDate5.MaxDate)
                            dd1 = m_endDate5.MaxDate;
                        this.m_endDate5.Value = dd1;
                    }
                    else
                        m_endDate5.Checked = false;


                }

            }

            //コンボボックスを読み込む
            Read_CustomerCombo();
            m_userno.Text = taskds.userno;
            m_usernameCombo.SelectedValue = taskds.userno;

        }
        void Read_CustomerCombo()
        {
            m_userno.Text = "";
            m_usernameCombo.DataSource = null;


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
        //繰り返し区分のラジオボタンをチェックする
        private void radio_check(string timerid, string selectnum)
        {
            int index = int.Parse(timerid);
            RadioButton rb;

            Control[] RadioOnes = new Control[] { m_radio_one1, m_radio_one2, m_radio_one3, m_radio_one4, m_radio_one5 };
            Control[] RadioHours = new Control[] { m_radio_hour1, m_radio_hour2, m_radio_hour3, m_radio_hour4, m_radio_hour5 };
            Control[] RadioDays = new Control[] { m_radio_day1, m_radio_day2, m_radio_day3, m_radio_day4, m_radio_day5 };
            Control[] RadioWeeks = new Control[] { m_radio_week1, m_radio_week2, m_radio_week3, m_radio_week4, m_radio_week5 };
            Control[] RadioMonths = new Control[] { m_radio_month1, m_radio_month2, m_radio_month3, m_radio_month4, m_radio_month5 };


            //1:1回、2:1時間毎、3:日毎、4:週毎、5:月毎
            switch (selectnum)
            {
                case "1":
                    //１回のとき
                    rb = (RadioButton)RadioOnes[index];
                    rb.Checked = true;
                    break;
                case "2":
                    //毎時
                    rb = (RadioButton)RadioHours[index];
                    rb.Checked = true;
                    break;
                case "3":
                    //毎日
                    rb = (RadioButton)RadioDays[index];
                    rb.Checked = true;
                    break;

                case "4":
                    //毎週
                    rb = (RadioButton)RadioWeeks[index];
                    rb.Checked = true;
                    break;

                case "5":
                    //毎月
                    rb = (RadioButton)RadioMonths[index];
                    rb.Checked = true;
                    break;
            }


        }


        //カスタマのコンボボックスが変更されたとき
        private void m_usernameCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (m_usernameCombo.Text == "")
            {
                m_userno.Text = "";
                return;
            }
            //ラベルに反映
            if (m_usernameCombo.SelectedValue != null)
                m_userno.Text = m_usernameCombo.SelectedValue.ToString();
            taskchange();
        }
        
        //検索ボタン
        private void button1_Click(object sender, EventArgs e)
        {

        }

        //テンプレート選択
        private void m_templeteCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            templeteSelect();

        }
        private void templeteSelect()
        {
            //m_title.Text = "";
            m_naiyou.Text = "";

            //テンプレート件数分ループを行う
            foreach (templeteDS v in templist)
            {
                if (m_templeteCombo.SelectedValue != null)
                {
                    if (v.templeteno == m_templeteCombo.SelectedValue.ToString())
                    {
                        //m_title.Text = v.title;
                        m_naiyou.Text = v.text;
                    }
                }
            }
        }
        //タスク区分コンボ
        private void m_schedule_combo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            taskchange();
        }
        //タスク区分コンボが変更された時
        private void taskchange()
        {
            if (m_schedule_combo.SelectedItem == null)
                return;
            //1:インシデント
            //2:定期作業
            //3:計画作業
            //4:特別作業

            if (m_schedule_combo.SelectedItem.ToString() == "3:計画作業")
            {
                //計画作業が選択された場合は一覧を取得する
                m_templeteCombo.Enabled = true;
                m_templeteCombo.Enabled = true;

                string userno = m_userno.Text;
                m_templeteCombo.DataSource = null;
                Class_Detaget dg = new Class_Detaget();
                templist
                   = dg.getTempleteList(userno, "2", con,true);

                //コンボボックス
                DataTable templeteTable = new DataTable();
                templeteTable.Columns.Add("ID", typeof(string));
                templeteTable.Columns.Add("NAME", typeof(string));

                if (templist == null)
                    return;

                //空白行を追加
                templeteDS tmp = new templeteDS();
                tmp.templeteno = "";
                tmp.templetename = "";
                List<templeteDS> templeteDSList = new List<templeteDS>();
                templeteDSList.Add(tmp);

                //テンプレート情報を取得する
                foreach (templeteDS v in templist)
                {
                    DataRow row = templeteTable.NewRow();
                    row["ID"] = v.templeteno;
                    row["NAME"] = v.templetename;
                    templeteTable.Rows.Add(row);
                }

                //データテーブルを割り当てる
                m_templeteCombo.DataSource = templeteTable;
                m_templeteCombo.DisplayMember = "NAME";
                m_templeteCombo.ValueMember = "ID";
                //初期値を反映させる
                templeteSelect();
            }
            else
            {
                m_templeteCombo.Enabled = false;
                //m_title.Text = "";
                m_naiyou.Text = "";
                m_templeteCombo.DataSource = null;
            }

        }
        //開始日時が変更されたとき
        private void m_startDate_ValueChanged(object sender, EventArgs e)
        {
            //開始日時の5分前をタイマー①に設定
            DateTime dt = m_startDate.Value;
            m_alermDate1.Value = dt.AddMinutes(-5);
            m_radio_one1.Checked = true;
            m_startDate1.Enabled = false;
            m_endDate1.Enabled = false;
            m_statusCombo1.Text = "有効";

            m_title1.Text = "開始5分前タイマー";


        }

        //キャンセル
        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_endDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime dt = m_endDate.Value;
            m_alermDate2.Value = dt.AddMinutes(-5);
            m_radio_one2.Checked = true;
            m_startDate2.Enabled = false;
            m_endDate2.Enabled = false;
            m_statusCombo2.Text = "有効";
            m_title2.Text = "終了5分前タイマー";
        }
        //更新ボタン
        private void button9_Click(object sender, EventArgs e)
        {
            int ret = 0;

            //登録を行う
            //カスタマ名
            if (m_schedule_combo.Text == "")
            {
                MessageBox.Show("タスク区分が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //カスタマ名
            if (m_usernameCombo.Text == "")
            {
                MessageBox.Show("カスタマ名が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //ステータス
            if (m_statusCombo.Text == "")
            {
                MessageBox.Show("ステータスが入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //内容
            if (m_naiyou.Text == "")
            {
                MessageBox.Show("内容が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //確認画面
            if (MessageBox.Show("タスク情報を登録します。よろしいですか？", "登録確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string schedule_type = m_schedule_combo.Text;
            string stype = schedule_type.Split(':')[0]; ;
            string userno = m_userno.Text;

            string templeteno = null;
            if (m_templeteCombo.Text != null  && m_templeteCombo.Text != "")
                templeteno = m_templeteCombo.SelectedValue.ToString();
            DateTime startdate = m_startDate.Value;
            DateTime enddate = m_endDate.Value;
            string statusstr = m_statusCombo.Text;

            //ステータス
            // 0:無効 1:有効
            string status = "";
            if (statusstr == "無効")
                //無効
                status = "0";
            else
                //有効
                status = "1";
            using (var transaction = con.BeginTransaction())
            {
                //DB接続
                NpgsqlCommand cmd;
                try
                {
                    if (con.FullState != ConnectionState.Open) con.Open();
                    Int32 rowsaffected;
                    //データ登録
                    cmd = new NpgsqlCommand(@"insert into task(schedule_type,userno,status,templeteno,naiyou,startdate,enddate,biko,ins_date,ins_name_id,chk_name_id) 
                        values ( :schedule_type,:userno,:status,:templeteno,:naiyou,:startdate,:enddate,:biko,:ins_date,:ins_name_id,:chk_name_id); " +
                        "select currval('task_schedule_no_seq') ;", con);
                    int? result=null;
                    if(templeteno != null)

                    result = int.Parse(templeteno);

                    cmd.Parameters.Add(new NpgsqlParameter("schedule_type", DbType.String) { Value = stype });
                    cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = userno });
                    cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                    cmd.Parameters.Add(new NpgsqlParameter("templeteno", DbType.Int32) { Value = result });
                    cmd.Parameters.Add(new NpgsqlParameter("naiyou", DbType.String) { Value = m_naiyou.Text });
                    cmd.Parameters.Add(new NpgsqlParameter("startdate", DbType.DateTime) { Value = startdate });
                    cmd.Parameters.Add(new NpgsqlParameter("enddate", DbType.DateTime) { Value = enddate });
                    cmd.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = m_biko.Text });
                    cmd.Parameters.Add(new NpgsqlParameter("ins_date", DbType.DateTime) { Value = DateTime.Now });
                    cmd.Parameters.Add(new NpgsqlParameter("ins_name_id", DbType.String) { Value = loginDS.opeid });
                    cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });

                    //OUTパラメータをセットする
                    NpgsqlParameter firstColumn = new NpgsqlParameter("firstcolumn", DbType.Int32);
                    firstColumn.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(firstColumn);
                    rowsaffected = cmd.ExecuteNonQuery();

                    int currval = int.Parse(firstColumn.NpgsqlValue.ToString());

                    if (rowsaffected != 1)
                    {
                        MessageBox.Show("登録できませんでした。", "タスク登録");
                    }
                    else
                    {
                        if (currval > 0)
                        {
                            //引き続きタイマーデータを作成し登録する
                            ret = make_timer(currval, stype);
                            if(ret == -1)
                            {
                                transaction.Rollback();
                            }
                            else {
                                transaction.Commit();

                                //登録成功
                                MessageBox.Show("登録完了 " + "スケジュール番号" + currval, "タイマー登録");
                                //this.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("タイマー登録エラー " + ex.Message);
                    return;
                }
            }
        }
        //タイマーデータの登録
        private int make_timer(int currval,String taskkubun)
        {
            //コントロール群
            Control[] titles = new Control[] { m_title1, m_title2, m_title3, m_title4, m_title5 };
            Control[] alertDates = new Control[] { m_alermDate1, m_alermDate2, m_alermDate3, m_alermDate4, m_alermDate5 };
            Control[] statuses = new Control[] { m_statusCombo1, m_statusCombo2, m_statusCombo3, m_statusCombo4, m_statusCombo5 };
            Control[] start_dates = new Control[] { m_startDate1, m_startDate2, m_startDate3, m_startDate4, m_startDate5 };
            Control[] end_dates = new Control[] { m_endDate1, m_endDate2, m_endDate3, m_endDate4, m_endDate5 };

            int i = 0;

            for (i = 0; i < 5; i++)
            {
                if (titles[i].Text == "")
                    continue;

                String timerID = "";
                String title = "";
                String repeat_type = "";
                DateTime alert_time ;
                DateTime? start_date =null;
                DateTime? end_date = null;
                String status = "";
                String words = "";
                //DB接続
                NpgsqlCommand cmd;
                try
                {
                    timerID = (i+1).ToString();

                    title = titles[i].Text;

                    DateTimePicker  obj = (DateTimePicker)alertDates[i];
                    alert_time = obj.Value;

                    status = statuses[i ].Text;
                    if (statuses[i ].Text == "無効")
                        //無効
                        status = "0";
                    else
                        //有効
                        status = "1";


                    Control[] RadioHours = new Control[] { m_radio_hour1, m_radio_hour2, m_radio_hour3, m_radio_hour4, m_radio_hour5 };
                    Control[] RadioDays = new Control[] { m_radio_day1, m_radio_day2, m_radio_day3, m_radio_day4, m_radio_day5 };
                    Control[] RadioWeeks = new Control[] { m_radio_week1, m_radio_week2, m_radio_week3, m_radio_week4, m_radio_week5 };
                    Control[] RadioMonths = new Control[] { m_radio_month1, m_radio_month2, m_radio_month3, m_radio_month4, m_radio_month5 };

                    String rep_type = "";

                    GroupBox gb = null;
                    if (i+1 == 1)
                        gb = radioGroup1;
                    else if (i + 1 == 2)
                        gb = radioGroup2;
                    else if (i + 1 == 3)
                        gb = radioGroup3;
                    else if (i + 1 == 4)
                        gb = radioGroup4;
                    else if (i + 1 == 5)
                        gb = radioGroup5;

                    //ラジオボタンの値を取得
                    foreach (RadioButton rb1 in gb.Controls)
                    {
                        if (rb1.Checked) { 
                            words = rb1.Text;
                            break;
                        }
                    }

                    if (words == "1回")
                        rep_type = "1";
                    else if (words == "毎時")
                        rep_type = "2";
                    else if (words == "毎日")
                        rep_type = "3";
                    else if (words == "毎週")
                        rep_type = "4";

                    else if (words == "毎月")
                        rep_type = "5";

                    obj = (DateTimePicker)start_dates[i];

                    if(obj.Enabled)
                        start_date = obj.Value;

                    obj = (DateTimePicker)end_dates[i];

                    if (obj.Enabled)
                        end_date = obj.Value;

                    if (con.FullState != ConnectionState.Open) con.Open();
                    Int32 rowsaffected;

                    //データ登録
                    cmd = new NpgsqlCommand(@"insert into timer(schedule_no,timerID,timername,repeat_type,alert_time,start_date,end_date,status,chk_name_id) 
                        values ( :schedule_no,:timerID,:timername,:repeat_type,:alert_time,:start_date,:end_date,:status,:chk_name_id); " +
                        "select currval('task_schedule_no_seq') ;", con);

                    cmd.Parameters.Add(new NpgsqlParameter("schedule_no", DbType.Int32) { Value = currval });
                    cmd.Parameters.Add(new NpgsqlParameter("timerID", DbType.Int32) { Value = timerID });
                    cmd.Parameters.Add(new NpgsqlParameter("timername", DbType.String) { Value = title });
                    cmd.Parameters.Add(new NpgsqlParameter("repeat_type", DbType.String) { Value = repeat_type });
                    cmd.Parameters.Add(new NpgsqlParameter("alert_time", DbType.DateTime) { Value = alert_time });
                    cmd.Parameters.Add(new NpgsqlParameter("start_date", DbType.DateTime) { Value = start_date });
                    cmd.Parameters.Add(new NpgsqlParameter("end_date", DbType.DateTime) { Value = end_date });
                    cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                    cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });

                    //OUTパラメータをセットする
                    NpgsqlParameter firstColumn = new NpgsqlParameter("firstcolumn", DbType.Int32);
                    firstColumn.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(firstColumn);
                    rowsaffected = cmd.ExecuteNonQuery();

                    currval = int.Parse(firstColumn.NpgsqlValue.ToString());

                    if (rowsaffected != 1)
                    {
//                        MessageBox.Show("登録できませんでした。", "タスク登録");
                        return -1;
                    }
                    else
                    {
                        if (currval > 0)
                        {
                            //引き続きアラートデータを作成し登録する
                            make_alert(currval, taskkubun, i+1);

                            //登録成功
  //                          MessageBox.Show("登録完了 " + "タスク番号" + currval, "タイマー登録");
 //                           this.Close();
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("タイマーテーブル登録エラー " + ex.Message);
                    logger.Error("タイマーテーブル登録エラー");
                    return -1;
                }
            }
            return 1;
        }

        //アラートデータ
        public void make_alert(int scheNO, string taskkubun, int index)
        {

            Control[] titles = new Control[] { m_title1, m_title2, m_title3, m_title4, m_title5 };
            Control[] alertDates = new Control[] { m_alermDate1, m_alermDate2, m_alermDate3, m_alermDate4, m_alermDate5 };
            Control[] statuses = new Control[] { m_statusCombo1, m_statusCombo2, m_statusCombo3, m_statusCombo4, m_statusCombo5 };
            Control[] start_dates = new Control[] { m_startDate1, m_startDate2, m_startDate3, m_startDate4, m_startDate5 };
            Control[] end_dates = new Control[] { m_endDate1, m_endDate2, m_endDate3, m_endDate4, m_endDate5 };

            Control[] RadioHours = new Control[] { m_radio_hour1, m_radio_hour2, m_radio_hour3, m_radio_hour4, m_radio_hour5 };
            Control[] RadioDays = new Control[] { m_radio_day1, m_radio_day2, m_radio_day3, m_radio_day4, m_radio_day5 };
            Control[] RadioWeeks = new Control[] { m_radio_week1, m_radio_week2, m_radio_week3, m_radio_week4, m_radio_week5 };
            Control[] RadioMonths = new Control[] { m_radio_month1, m_radio_month2, m_radio_month3, m_radio_month4, m_radio_month5 };


            GroupBox gb = null;
            if(index == 1)
                gb = radioGroup1;
            else if(index == 2)
                gb = radioGroup2;
            else if (index == 3)
                gb = radioGroup3;
            else if (index == 4)
                gb = radioGroup4;
            else if (index == 5)
                gb = radioGroup5;

            foreach ( RadioButton rb1 in gb.Controls)
            {
                if (rb1.Checked)
                {
                    if (rb1.Text == "1回")
                    {
                        //入力された日時を登録する
                        DateTimePicker obj;
                        obj = (DateTimePicker)alertDates[index - 1];
                        DateTime dd = obj.Value;

                        alerm_insert(scheNO, taskkubun, dd);

                    }
                    else if (rb1.Text == "毎時")
                    {
                        DateTimePicker obj;
                        obj = (DateTimePicker)alertDates[index - 1];

                        DateTime alertdate = obj.Value.Date;
                        TimeSpan dtt = obj.Value.TimeOfDay;
                        alertdate = alertdate + dtt;

                        //開始日
                        DateTimePicker startdate = (DateTimePicker)start_dates[index - 1];
                        //終了日
                        DateTimePicker enddate = (DateTimePicker)end_dates[index - 1];

                        //開始日、終了日が入力されていなかったら登録しない
                        if (startdate == null || enddate == null)
                        {
                            MessageBox.Show("開始日、終了日が入力されていません。");
                            return;
                        }

                        DateTime startdt = startdate.Value;

                        DateTime enddt = enddate.Value;

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
                            int ret = alerm_insert(scheNO, taskkubun, alertdate);
                            if (ret == -1)
                                break;

                            //1時間プラス
                            alertdate = alertdate.AddHours(1);
                            //i++;
                        }

                    }
                    else if (rb1.Text == "毎日")
                    {
                        DateTimePicker obj;
                        obj = (DateTimePicker)alertDates[index - 1];

                        DateTime alertdate = obj.Value.Date;
                        TimeSpan dtt = obj.Value.TimeOfDay;
                        alertdate = alertdate + dtt;

                        //開始日
                        DateTimePicker startdate = (DateTimePicker)start_dates[index - 1];
                        //終了日
                        DateTimePicker enddate = (DateTimePicker)end_dates[index - 1];

                        //開始日、終了日が入力されていなかったら登録しない
                        if (startdate == null || enddate == null)
                        {
                            MessageBox.Show("開始日、終了日が入力されていません。");
                            return;
                        }

                        DateTime startdt = startdate.Value;

                        DateTime enddt = enddate.Value;


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
                            int ret = alerm_insert(scheNO, taskkubun, alertdate);
                            if (ret == -1)
                                break;

                            //1日プラス
                            alertdate = alertdate.AddDays(1);

                        }
                    }
                    else if (rb1.Text == "毎週")
                    {
                        DateTimePicker obj;
                        obj = (DateTimePicker)alertDates[index - 1];

                        DateTime alertdate = obj.Value.Date;
                        TimeSpan dtt = obj.Value.TimeOfDay;
                        alertdate = alertdate + dtt;
                        
                        
                        //曜日を番号で取得
                        int weeknumber = (int)obj.Value.DayOfWeek;

                        //開始日
                        DateTimePicker startdate = (DateTimePicker)start_dates[index - 1];
                        //終了日
                        DateTimePicker enddate = (DateTimePicker)end_dates[index - 1];



                        //開始日
                        DateTime startdt = startdate.Value;


                        int startweekint = (int)startdate.Value.DayOfWeek;

                        //終了日
                        DateTime enddt = enddate.Value;
                        int endweekint = (int)enddate.Value.DayOfWeek;


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
                                int ret = alerm_insert(scheNO, taskkubun, alertdate);
                                if (ret == -1)
                                    break;
                            }
                            //1日プラス
                            alertdate = alertdate.AddDays(1);
                            tmpweeknumber = (int)alertdate.DayOfWeek;
                        }
                    }
                    else if (rb1.Text == "毎月")
                    {
                        DateTimePicker obj;
                        obj = (DateTimePicker)alertDates[index - 1];

                        DateTime alertdate = obj.Value.Date;
                        TimeSpan dtt = obj.Value.TimeOfDay;
                        alertdate = alertdate + dtt;

                        //開始日
                        DateTimePicker startdate = (DateTimePicker)start_dates[index - 1];
                        //終了日
                        DateTimePicker enddate = (DateTimePicker)end_dates[index - 1];

                        //開始日、終了日が入力されていなかったら登録しない
                        if (startdate == null || enddate == null)
                        {
                            MessageBox.Show("開始日、終了日が入力されていません。");
                            return;
                        }

                        DateTime startdt = startdate.Value;

                        DateTime enddt = enddate.Value;

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
                            int ret = alerm_insert(scheNO, taskkubun, alertdate);
                            if (ret == -1)
                                break;

                            //1月プラス
                            alertdate = alertdate.AddMonths(1);
                        }

                        //IF文終わり
                    }

                }
                //ループ終わり
            }

        }
        //タイマー対応テーブルに登録する
        private int alerm_insert(int scheNO, string taskkubun, DateTime alertdatetime)
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
                cmd.Parameters.Add(new NpgsqlParameter("schedule_type", DbType.String) { Value = taskkubun });
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
        //
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        //対象の日時にデータタイムを取得する
        private void timer_datetime(int group_idx,int repeat_kubun)
        {
            Control[] start_dates = new Control[] { m_startDate1, m_startDate2, m_startDate3, m_startDate4, m_startDate5 };
            Control[] end_dates = new Control[] { m_endDate1, m_endDate2, m_endDate3, m_endDate4, m_endDate5 };


            //1回
            if (repeat_kubun == 1)
            { 
                start_dates[group_idx].Enabled = false;
                end_dates[group_idx].Enabled = false;
            }
            //毎時
            else if (repeat_kubun == 2)
            {
                start_dates[group_idx].Enabled = true;
                end_dates[group_idx].Enabled = true;
            }
            //毎日
            else if (repeat_kubun == 3)
            {
                start_dates[group_idx].Enabled = true;
                end_dates[group_idx].Enabled = true;
            }
            //毎週
            else if (repeat_kubun == 4)
            {
                start_dates[group_idx].Enabled = true;
                end_dates[group_idx].Enabled = true;
            }
            //毎月
            else if (repeat_kubun == 5)
            {
                start_dates[group_idx].Enabled = true;
                end_dates[group_idx].Enabled = true;
            }
        }

        //タイマー①
        private void m_radio_one1_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用不可にする
            if (m_radio_one1.Checked)
                timer_datetime(0, 1);
        }
        //タイマー①
        private void m_radio_hour1_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用不可にする
            if (m_radio_hour1.Checked)
                timer_datetime(0, 2);
        }
        //タイマー①
        private void m_radio_day1_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用可にする
            if (m_radio_day1.Checked)
                timer_datetime(0, 3);
        }
        //タイマー①
        private void m_radio_week1_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用可にする
            if (m_radio_week1.Checked)
                timer_datetime(0, 4);
        }
        //タイマー①
        private void m_radio_month1_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用可にする
            if (m_radio_month1.Checked)
                timer_datetime(0, 5);
        }
        //タイマー②
        private void m_radio_one2_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用不可にする
            if (m_radio_one2.Checked)
                timer_datetime(1, 1);
        }
        //タイマー②
        private void m_radio_hour2_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用可にする
            if (m_radio_hour2.Checked)
                timer_datetime(1, 2);
        }
        //タイマー②
        private void m_radio_day2_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用可にする
            if (m_radio_day2.Checked)
                timer_datetime(1, 3);
        }
        //タイマー②
        private void m_radio_week2_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用可にする
            if (m_radio_week2.Checked)
                timer_datetime(1, 4);
        }
        //タイマー②
        private void m_radio_month2_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用可にする
            if (m_radio_month2.Checked)
                timer_datetime(1, 5);
        }
        //タイマー③
        private void m_radio_one3_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用不可にする
            if (m_radio_one3.Checked)
                timer_datetime(2, 1);
        }
        //タイマー③
        private void m_radio_hour3_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用可にする
            if (m_radio_month2.Checked)
                timer_datetime(2, 2);
        }
        //タイマー③
        private void m_radio_day3_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用可にする
            if (m_radio_day3.Checked)
                timer_datetime(2, 3);
        }
        //タイマー③
        private void m_radio_week3_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用可にする
            if (m_radio_week3.Checked)
                timer_datetime(2, 4);
        }
        //タイマー③
        private void m_radio_month3_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用可にする
            if (m_radio_month3.Checked)
                timer_datetime(2, 5);
        }
        //タイマー④
        private void m_radio_one4_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用不可にする
            if (m_radio_one4.Checked)
                timer_datetime(3, 1);
        }
        //タイマー④
        private void m_radio_hour4_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用可にする
            if (m_radio_hour4.Checked)
                timer_datetime(3, 2);
        }
        //タイマー④
        private void m_radio_day4_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用可にする
            if (m_radio_day3.Checked)
                timer_datetime(3, 3);
        }
        //タイマー④
        private void m_radio_week4_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用可にする
            if (m_radio_week4.Checked)
                timer_datetime(3, 4);
        }
        //タイマー④
        private void m_radio_month4_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用可にする
            if (m_radio_month4.Checked)
                timer_datetime(3, 5);
        }
        //タイマー⑤
        private void m_radio_one5_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用不可にする
            if (m_radio_one5.Checked)
                timer_datetime(4, 1);
        }
        //タイマー⑤
        private void m_radio_hour5_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用可にする
            if (m_radio_hour5.Checked)
                timer_datetime(4, 2);
        }
        //タイマー⑤
        private void m_radio_day5_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用可にする
            if (m_radio_day5.Checked)
                timer_datetime(4, 3);
        }
        //タイマー⑤
        private void m_radio_week5_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用可にする
            if (m_radio_week5.Checked)
                timer_datetime(4, 4);
        }
        //タイマー⑤
        private void m_radio_month5_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用可にする
            if (m_radio_month5.Checked)
                timer_datetime(4, 5);
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        //検索ボタン
        private void m_selectBtn_Click(object sender, EventArgs e)
        {
            
        }
        //キャンセル
        private void button8_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        //登録ボタン
        private void button9_Click_1(object sender, EventArgs e)
        {
            if (m_taskno.Text == "")
            {
                MessageBox.Show("タスク通番が取得できませんでした。", "タスク情報更新", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (m_schedule_combo.Text == "")
            {
                MessageBox.Show("タスク区分が取得できませんでした。", "タスク情報更新", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (m_userno.Text == "")
            {
                MessageBox.Show("カスタマ通番が取得できませんでした。", "タスク情報更新", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (m_templeteCombo.Text == "")
            {
                MessageBox.Show("テンプレートが取得できませんでした。", "タスク情報更新", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (m_statusCombo.Text == "")
            {
                MessageBox.Show("ステータスが取得できませんでした。", "タスク情報更新", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //確認ダイアログ
            if (MessageBox.Show("タスク情報の更新を行います。よろしいですか？", "スケジュール情報更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            string status = "";
            if (m_statusCombo.Text == "有効")
                status = "1";
            else if (m_statusCombo.Text == "無効")
                status = "0";

            string yoteikbn = "";
            //インシデント
            if (m_schedule_combo.SelectedIndex == 0)
                yoteikbn = "1";

            //定期作業
            else if (m_schedule_combo.SelectedIndex == 1)
                yoteikbn = "2";
            //計画作業
            else if (m_schedule_combo.SelectedIndex == 2)
                yoteikbn = "3";
            //特別作業
            else if (m_schedule_combo.SelectedIndex == 3)
                yoteikbn = "4";


            if (this.con.FullState != ConnectionState.Open) this.con.Open();

            string sql = "update task set schedule_type=:schedule_type,status=:status,templeteno=:templeteno,naiyou=:naiyou,startdate=:startdate,enddate=:enddate," +
                "biko=:biko,userno=:userno,chk_name_id=:chk_name_id where schedule_no = :no";

            using (var transaction = this.con.BeginTransaction())
            {

                var command = new NpgsqlCommand(@sql, this.con);

                int scheduleno = int.Parse(m_taskno.Text);
                String schedule_type = yoteikbn;
                

                DateTime? startdate = null;
                DateTime? enddate = null;
                if (m_startDate.Enabled)
                    startdate = m_startDate.Value;
                if (m_endDate.Enabled)
                    enddate = m_endDate.Value;

                command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = m_taskno.Text });
                command.Parameters.Add(new NpgsqlParameter("schedule_type", DbType.String) { Value = yoteikbn });
                command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                command.Parameters.Add(new NpgsqlParameter("templeteno", DbType.Int32) { Value = enddate });
                command.Parameters.Add(new NpgsqlParameter("startdate", DbType.DateTime) { Value = startdate });
                command.Parameters.Add(new NpgsqlParameter("enddate", DbType.DateTime) { Value = enddate });
                command.Parameters.Add(new NpgsqlParameter("naiyou", DbType.String) { Value = m_naiyou.Text });
                command.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = m_biko.Text });
                command.Parameters.Add(new NpgsqlParameter("userno", DbType.Binary) { Value = m_userno.Text });
                command.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.Int32) { Value = loginDS.opeid });
                Int32 rowsaffected;
                try
                {
                    //更新処理
                    //OUTパラメータをセットする
                    rowsaffected = command.ExecuteNonQuery();

                    if (rowsaffected != 1)
                        MessageBox.Show("更新できませんでした。", "タスク情報更新");
                    else
                    {
                        //タイマー情報も更新する
                        int ret = updateTimertbl(schedule_type, yoteikbn);

                        if (ret == 1)
                        {

                            transaction.Commit();
                            MessageBox.Show("スケジュール情報を変更しました。", "スケジュール更新", MessageBoxButtons.OK);
                        }                    
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
        //アラートテーブルの更新
        private int make_alert(int timeridx, string str_taskno,string yoteikbn )
        {
            Control[] RadioHours = new Control[] { m_radio_hour1, m_radio_hour2, m_radio_hour3, m_radio_hour4, m_radio_hour5 };
            Control[] RadioDays = new Control[] { m_radio_day1, m_radio_day2, m_radio_day3, m_radio_day4, m_radio_day5 };
            Control[] RadioWeeks = new Control[] { m_radio_week1, m_radio_week2, m_radio_week3, m_radio_week4, m_radio_week5 };
            Control[] RadioMonths = new Control[] { m_radio_month1, m_radio_month2, m_radio_month3, m_radio_month4, m_radio_month5 };

            Control[] titles = new Control[] { m_title1, m_title2, m_title3, m_title4, m_title5 };
            Control[] alertDates = new Control[] { m_alermDate1, m_alermDate2, m_alermDate3, m_alermDate4, m_alermDate5 };
            Control[] statuses = new Control[] { m_statusCombo1, m_statusCombo2, m_statusCombo3, m_statusCombo4, m_statusCombo5 };
            Control[] start_dates = new Control[] { m_startDate1, m_startDate2, m_startDate3, m_startDate4, m_startDate5 };
            Control[] end_dates = new Control[] { m_endDate1, m_endDate2, m_endDate3, m_endDate4, m_endDate5 };


            GroupBox gb = null;
            if (timeridx + 1 == 1)
                gb = radioGroup1;
            else if (timeridx + 1 == 2)
                gb = radioGroup2;
            else if (timeridx + 1 == 3)
                gb = radioGroup3;
            else if (timeridx + 1 == 4)
                gb = radioGroup4;
            else if (timeridx + 1 == 5)
                gb = radioGroup5;
            
            string words="";
            string rep_type = "";

            //ラジオボタンの値を取得
            foreach (RadioButton rb1 in gb.Controls)
            {
                if (rb1.Checked)
                {
                    words = rb1.Text;
                    break;
                }
            }

            if (words == "1回")
                rep_type = "1";
            else if (words == "毎時")
                rep_type = "2";
            else if (words == "毎日")
                rep_type = "3";
            else if (words == "毎週")
                rep_type = "4";
            else if (words == "毎月")
                rep_type = "5";

            //ステータス
            if (m_statusCombo.Text == "有効")

                //アラーム周期            
                //1回の時
                if (rep_type == "1")
                {
                    //入力された日時を登録する
                    DateTimePicker dd = (DateTimePicker)alertDates[timeridx];
                    DateTime alertdt = dd.Value;

                    //アラートデータの挿入
                    alerm_insert(int.Parse(str_taskno), yoteikbn, alertdt);
                }
                //毎時の時
                else if (rep_type == "2")
                {
                    DateTimePicker dd = (DateTimePicker)alertDates[timeridx];
                    DateTime alertdate = dd.Value;
                    TimeSpan dtt = dd.Value.TimeOfDay;
                    alertdate = alertdate + dtt;


                    //開始日
                    DateTime startdt = ((DateTimePicker)start_dates[timeridx]).Value;

                    //終了日
                    DateTime enddt = ((DateTimePicker)end_dates[timeridx]).Value;

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
                        int ret = alerm_insert(int.Parse(str_taskno), yoteikbn, alertdate);
                        if (ret == -1)
                            break;

                        //1時間プラス
                        alertdate = alertdate.AddHours(1);
                        //i++;
                    }
                }
                //日
                else if (rep_type == "3")
                {
                    DateTimePicker dd = (DateTimePicker)alertDates[timeridx];
                    DateTime alertdate = dd.Value;
                    TimeSpan dtt = dd.Value.TimeOfDay;


                    alertdate = alertdate + dtt;


                    //開始日
                    DateTime startdt = ((DateTimePicker)start_dates[timeridx]).Value;

                    //終了日
                    DateTime enddt = ((DateTimePicker)end_dates[timeridx]).Value;

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
                        int ret = alerm_insert(int.Parse(str_taskno), yoteikbn, alertdate);
                        if (ret == -1)
                            break;

                        //1日プラス
                        alertdate = alertdate.AddDays(1);
                    }
                }
                //週
                else if (rep_type == "4")
                {
                    DateTimePicker dd = (DateTimePicker)alertDates[timeridx];

                    TimeSpan dtt = dd.Value.TimeOfDay;
                    //開始日
                    DateTime startdate = ((DateTimePicker)start_dates[timeridx]).Value;

                    //アラーム日時
                    DateTime alertdate;
                    alertdate = startdate + dtt;


                    int weeknumber = (int)dd.Value.DayOfWeek;





                    int startweekint = (int)startdate.DayOfWeek;

                    //終了日
                    DateTimePicker enddate = (DateTimePicker)end_dates[timeridx];
                    DateTime enddt = enddate.Value;
                    int endweekint = (int)enddate.Value.DayOfWeek;


                    int tmpweeknumber = startweekint;
                    while (true)
                    {

                        //開始日時以前なら登録しない
                        if (startdate > alertdate)
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
                            int ret = alerm_insert(int.Parse(str_taskno), yoteikbn, alertdate);
                            if (ret == -1)
                                break;
                        }

                        //1日プラス
                        alertdate = alertdate.AddDays(1);
                        tmpweeknumber = (int)alertdate.DayOfWeek;
                    }
                }
                else if (rep_type == "5")
                {
                    //開始日
                    DateTime startdt = ((DateTimePicker)start_dates[timeridx]).Value;

                    //終了日
                    DateTime enddt = ((DateTimePicker)end_dates[timeridx]).Value;

                    //アラーム日時
                    DateTimePicker alermdt = (DateTimePicker)alertDates[timeridx];

                    //開始日
                    DateTime alertdate = startdt.Date;

                    TimeSpan dtt = alermdt.Value.TimeOfDay;
                    alertdate = alertdate + dtt;



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
                        int ret = alerm_insert(int.Parse(str_taskno), yoteikbn, alertdate);
                        if (ret == -1)
                            break;

                        //1月プラス
                        alertdate = alertdate.AddMonths(1);
                    }
                }

            return 1;
        }


        //タイマーテーブルを更新する
        private int updateTimertbl(String schedule_type,string yoteikbn)
        {
            Control[] titles = new Control[] { m_title1, m_title2, m_title3, m_title4, m_title5 };
            Control[] alertDates = new Control[] { m_alermDate1, m_alermDate2, m_alermDate3, m_alermDate4, m_alermDate5 };
            Control[] statuses = new Control[] { m_statusCombo1, m_statusCombo2, m_statusCombo3, m_statusCombo4, m_statusCombo5 };
            Control[] start_dates = new Control[] { m_startDate1, m_startDate2, m_startDate3, m_startDate4, m_startDate5 };
            Control[] end_dates = new Control[] { m_endDate1, m_endDate2, m_endDate3, m_endDate4, m_endDate5 };

            int i = 0;

            for (i = 0; i < 5; i++)
            {
                if (titles[i].Text == "")
                    continue;

                String timerID = "";
                String title = "";

                DateTime alert_time;
                String status = "";
                String words = "";
                //DB接続
                NpgsqlCommand cmd;

                try
                {
                    timerID = (i + 1).ToString();

                    title = titles[i].Text;

                    DateTimePicker obj = (DateTimePicker)alertDates[i];
                    alert_time = obj.Value;

                    status = statuses[i].Text;
                    if (statuses[i].Text == "無効")
                        //無効
                        status = "0";
                    else
                        //有効
                        status = "1";

                    Control[] RadioHours = new Control[] { m_radio_hour1, m_radio_hour2, m_radio_hour3, m_radio_hour4, m_radio_hour5 };
                    Control[] RadioDays = new Control[] { m_radio_day1, m_radio_day2, m_radio_day3, m_radio_day4, m_radio_day5 };
                    Control[] RadioWeeks = new Control[] { m_radio_week1, m_radio_week2, m_radio_week3, m_radio_week4, m_radio_week5 };
                    Control[] RadioMonths = new Control[] { m_radio_month1, m_radio_month2, m_radio_month3, m_radio_month4, m_radio_month5 };

                    String rep_type = "";

                    GroupBox gb = null;
                    if (i + 1 == 1)
                        gb = radioGroup1;
                    else if (i + 1 == 2)
                        gb = radioGroup2;
                    else if (i + 1 == 3)
                        gb = radioGroup3;
                    else if (i + 1 == 4)
                        gb = radioGroup4;
                    else if (i + 1 == 5)
                        gb = radioGroup5;

                    //ラジオボタンの値を取得
                    foreach (RadioButton rb1 in gb.Controls)
                    {
                        if (rb1.Checked)
                        {
                            words = rb1.Text;
                            break;
                        }
                    }

                    if (words == "1回")
                        rep_type = "1";
                    else if (words == "毎時")
                        rep_type = "2";
                    else if (words == "毎日")
                        rep_type = "3";
                    else if (words == "毎週")
                        rep_type = "4";
                    else if (words == "毎月")
                        rep_type = "5";

                    obj = (DateTimePicker)start_dates[i];


                    DateTime startdate = new DateTime();
                    DateTime enddate = new DateTime();

                    if (obj.Enabled)
                        startdate = obj.Value;

                    obj = (DateTimePicker)end_dates[i];

                    if (obj.Enabled)
                        enddate = obj.Value;

                    if (con.FullState != ConnectionState.Open) con.Open();

                    string sql = "update timer set timername=:timername,repeat_type=:repeat_type,alert_time=:alert_time,start_date=:start_date,end_date=:end_date,status=:status," +
                         "chk_name_id=:chk_name_id where schedule_no = :no and timerid = :timerid";


                    var command = new NpgsqlCommand(@sql, this.con);

                    int scheduleno = int.Parse(m_taskno.Text);

                    command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = m_taskno.Text });
                    command.Parameters.Add(new NpgsqlParameter("timerid", DbType.Int32) { Value = (i + 1).ToString() });
                    command.Parameters.Add(new NpgsqlParameter("timername", DbType.String) { Value = title });
                    command.Parameters.Add(new NpgsqlParameter("repeat_type", DbType.String) { Value = rep_type });
                    command.Parameters.Add(new NpgsqlParameter("alert_time", DbType.DateTime) { Value = alert_time });
                    command.Parameters.Add(new NpgsqlParameter("start_date", DbType.DateTime) { Value = startdate });
                    command.Parameters.Add(new NpgsqlParameter("end_date", DbType.DateTime) { Value = enddate });
                    command.Parameters.Add(new NpgsqlParameter("naiyou", DbType.String) { Value = m_naiyou.Text });
                    command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                    command.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.Int32) { Value = loginDS.opeid });
                    Int32 rowsaffected;


                    //更新処理
                    //OUTパラメータをセットする
                    rowsaffected = command.ExecuteNonQuery();

                    if (rowsaffected != 1)
                    {

                        MessageBox.Show("タイマー情報を更新できませんでした。", "タスク更新" + "タイマー " + (i+1).ToString());
                        return -1;
                    }
                    else
                    {
                        //正常に更新できたとき
                        //まず登録されているアラートは削除
                        int ret = deleteTimer(m_taskno.Text);

                        //引き続きアラートデータを作成し登録する
                        make_alert(i + 1, m_taskno.Text, yoteikbn);
                        if (ret != 1)
                        {
                            return -1;
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("削除エラー " + ex.Message, "タイマー削除", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
            return 1;

        }
        //削除
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
                MessageBox.Show("削除エラー " + ex.Message, "タイマー削除", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            return 1;
        }
    }
}
