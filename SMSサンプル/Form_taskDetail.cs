﻿using Npgsql;
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

        //タスク一覧
        public List<taskDS> taskDSlist { get; set; }

        public  List<templeteDS> templist { get; set; }

        //タスク一覧
        DataTable task_list;

        //カスタマ
        public List<userDS> userList { get; set; }

        //システム
        public List<systemDS> systemList { get; set; }

        //拠点
        public List<siteDS> siteList { get; set; }

        public taskDS taskds { get; set; }
        public timerDS timerds { get; set; }
        public List<timerDS> timerList { get; set; }

        //テンプレート
        public templeteDS templetedt { get; set; }

        //テンプレート一覧
        public List<templeteDS> templList { get; set; }

        //テンプレート一覧
        DataTable templ_list;

        //ListViewのソートの際に使用する
        private int sort_kind = 0;

        public Form_taskDetail()
        {
            InitializeComponent();
        }

        //表示前
        private void Form_taskDetail_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);


            this.splitContainer1.SplitterDistance = 32;

            m_selectKoumoku.Items.Add("タスク通番");
            m_selectKoumoku.Items.Add("タスク区分");
            m_selectKoumoku.Items.Add("カスタマ名");
            m_selectKoumoku.Items.Add("テンプレート");
            m_selectKoumoku.Items.Add("開始日時");
            m_selectKoumoku.Items.Add("終了日時");
            m_selectKoumoku.Items.Add("ステータス");
            m_selectKoumoku.Items.Add("内容");
            m_selectKoumoku.Items.Add("備考");
            m_selectKoumoku.Items.Add("タイマー名");
            m_selectKoumoku.Items.Add("アラート日時");
            m_selectKoumoku.Items.Add("タイマーステータス");
            m_selectKoumoku.Items.Add("繰り返し区分");
            m_selectKoumoku.Items.Add("アラーム日時");
            m_selectKoumoku.Items.Add("タイマー開始日時");
            m_selectKoumoku.Items.Add("タイマー終了日時");
            m_selectKoumoku.Items.Add("作成日時");
            m_selectKoumoku.Items.Add("作成者");
            m_selectKoumoku.Items.Add("更新日時");
            m_selectKoumoku.Items.Add("更新者");

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
            System.DateTime? dd1;

            this.m_taskno.Text = taskds.schedule_no;
            string temptype = (int.Parse(taskds.schedule_type) - 1).ToString();
            this.m_schedule_combo.SelectedIndex = int.Parse(taskds.schedule_type)-1;
            this.m_userno.Text = taskds.userno;


            //テンプレートコンボボックス変更
            dispTempleteCombo();

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
                        this.m_alermDate1.Value = (DateTime)dd1;
                    }


                    //開始日時の取得
                    if (timerds.start_date != null && timerds.start_date != "")
                    {
                        m_startDate1.Checked = true;

                        //dd1 = DateTime.ParseExact(timerds.start_date, "yyyy/MM/dd HH:mm:ss", null);
                        dd1 = DateTime.Parse(timerds.start_date);
                        this.m_startDate1.Value = (DateTime)dd1;
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
                        this.m_endDate1.Value = (DateTime)dd1;
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
                        this.m_alermDate2.Value = (DateTime)dd1;
                    }


                    //開始日時の取得
                    if (timerds.start_date != null && timerds.start_date != "")
                    {
                        m_startDate2.Checked = true;
                        //dd1 = DateTime.ParseExact(timerds.start_date, "yyyy/MM/dd HH:mm:ss", null);
                        dd1 = DateTime.Parse(timerds.start_date);
                        this.m_startDate2.Value = (DateTime)dd1;
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
                        this.m_endDate2.Value = (DateTime)dd1;
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
                        this.m_alermDate3.Value = (DateTime)dd1;
                    }


                    //開始日時の取得
                    if (timerds.start_date != null && timerds.start_date != "")
                    {
                        //dd1 = DateTime.ParseExact(timerds.start_date, "yyyy/MM/dd HH:mm:ss", null);
                        dd1 = DateTime.Parse(timerds.start_date);
                        this.m_startDate3.Value = (DateTime)dd1;
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
                        this.m_endDate3.Value = (DateTime)dd1;
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
                        this.m_alermDate4.Value = (DateTime)dd1;
                    }


                    //開始日時の取得
                    if (timerds.start_date != null && timerds.start_date != "")
                    {
                        m_startDate4.Checked = true;
                        //dd1 = DateTime.ParseExact(timerds.start_date, "yyyy/MM/dd HH:mm:ss", null);
                        dd1 = DateTime.Parse(timerds.start_date);
                        this.m_startDate4.Value = (DateTime)dd1;
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
                        this.m_endDate4.Value = (DateTime)dd1;
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

                        this.m_alermDate5.Value = (DateTime)dd1;
                    }


                    //開始日時の取得
                    if (timerds.start_date != null && timerds.start_date != "")
                    {
                        m_startDate5.Checked = true;
                        //dd1 = DateTime.ParseExact(timerds.start_date, "yyyy/MM/dd HH:mm:ss", null);
                        dd1 = DateTime.Parse(timerds.start_date);
                        this.m_startDate5.Value = (DateTime)dd1;
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
                        this.m_endDate5.Value = (DateTime)dd1;
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
        //テンプレートコンボボックスが変更されたとき
        private void dispTempleteCombo()
        {
            //テンプレート
            m_templeteCombo.Enabled = true;

            string userno = m_userno.Text;
            //計画作業もしくはインシデントのときは一覧を取得する
            if (m_schedule_combo.SelectedItem.ToString() == "1:インシデント" ||
                m_schedule_combo.SelectedItem.ToString() == "3:計画作業")
            {
                string temlete_type = "";
                if (m_schedule_combo.SelectedItem.ToString() == "1:インシデント")
                    temlete_type = "1";

                else if (m_schedule_combo.SelectedItem.ToString() == "3:計画作業")
                    temlete_type = "2";

                //計画作業が選択された場合は一覧を取得する
                m_templeteCombo.Enabled = true;

                m_templeteCombo.DataSource = null;
                Class_Detaget dg = new Class_Detaget();

                //1.インシデント、2:計画作業
                templist
                   = dg.getTempleteList(userno, temlete_type, con, true);

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
                templeteComboSelect(taskds.templeteno);
            }
            else
            {
                m_templeteCombo.Enabled = false;
                //m_title.Text = "";
                m_naiyou.Text = "";
                m_templeteCombo.DataSource = null;
            }

        }
        //選択表示時
        private void templeteComboSelect(String templete_no)
        {

            //テンプレート件数分ループを行う
            foreach (templeteDS v in templist)
            {
                if (templete_no != null && templete_no != "")
                {

                    if (v.templeteno == templete_no)
                    {
                        m_templeteCombo.SelectedValue = v.templeteno;
                    }
                }
            }
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
                    rb = (RadioButton)RadioOnes[index - 1];
                    rb.Checked = true;
                    break;
                case "2":
                    //毎時
                    rb = (RadioButton)RadioHours[index - 1];
                    rb.Checked = true;
                    break;
                case "3":
                    //毎日
                    rb = (RadioButton)RadioDays[index - 1];
                    rb.Checked = true;
                    break;

                case "4":
                    //毎週
                    rb = (RadioButton)RadioWeeks[index - 1];
                    rb.Checked = true;
                    break;

                case "5":
                    //毎月
                    rb = (RadioButton)RadioMonths[index-1];
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
        private void m_templeteCombo_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            templeteSelect();

        }
        //テンプレート選択した後
        private void templeteSelect()
        {

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
            //4:特別対応
            if (m_schedule_combo.SelectedItem.ToString() == "3:計画作業")
            {
                //計画作業が選択された場合は一覧を取得する
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
            if (MessageBox.Show("タスク情報を更新します。よろしいですか？", "更新確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
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
                        MessageBox.Show("更新できませんでした。", "タスク更新");
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
                                MessageBox.Show("更新完了 " + "スケジュール番号" + currval, "タイマー更新");
                                //this.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("タイマー更新エラー " + ex.Message);
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
                            make_alert(i + 1,currval.ToString(), taskkubun );

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
        void task_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            //	e.Item = _item[e.ItemIndex];
            if (task_list.Rows.Count > 0)
            {

                DataRow row = this.task_list.Rows[e.ItemIndex];
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
                Convert.ToString(row[15]),
                Convert.ToString(row[16]),
                Convert.ToString(row[17]),
                Convert.ToString(row[18]),
                Convert.ToString(row[19]),
                Convert.ToString(row[20]),
                Convert.ToString(row[21]),
                Convert.ToString(row[22]),
                Convert.ToString(row[23]),
                Convert.ToString(row[24]),
                Convert.ToString(row[25]),
                Convert.ToString(row[26]),
                Convert.ToString(row[27]),
                Convert.ToString(row[28]),
                Convert.ToString(row[29]),
                Convert.ToString(row[30]),
                Convert.ToString(row[31]),
                Convert.ToString(row[32]),
                Convert.ToString(row[33]),
                Convert.ToString(row[34]),
                Convert.ToString(row[35]),
                Convert.ToString(row[36]),
                Convert.ToString(row[37]),
                Convert.ToString(row[38]),
                Convert.ToString(row[39]),
                Convert.ToString(row[40]),
                Convert.ToString(row[41]),
                Convert.ToString(row[42]),
                Convert.ToString(row[43])
                    });
            }

        }

        //タイマー対応テーブルに登録する
        private int alerm_insert(int scheNO, string taskkubun, DateTime alertdatetime,int index)
        {
            //DB接続
            NpgsqlCommand cmd;
            try
            {
                if (con.FullState != ConnectionState.Open) con.Open();
                Int32 rowsaffected;
                //データ登録
                cmd = new NpgsqlCommand(@"insert into timer_taiou(schedule_no,schedule_type,TimerID,alertdatetime,chk_name_id) 
                    values ( :schedule_no,:schedule_type,:TimerID,:alertdatetime,:chk_name_id) ", con);

                cmd.Parameters.Add(new NpgsqlParameter("schedule_no", DbType.Int32) { Value = scheNO });
                cmd.Parameters.Add(new NpgsqlParameter("schedule_type", DbType.String) { Value = taskkubun });
                cmd.Parameters.Add(new NpgsqlParameter("TimerID", DbType.Int32) { Value = index });

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


        //対象の日時にデータタイムを取得する
        private void timer_datetime(int group_idx,int repeat_kubun)
        {
            Control[] alert_dates = new Control[] { m_alermDate1, m_alermDate2, m_alermDate3, m_alermDate4, m_alermDate5 };

            Control[] start_dates = new Control[] { m_startDate1, m_startDate2, m_startDate3, m_startDate4, m_startDate5 };
            Control[] end_dates = new Control[] { m_endDate1, m_endDate2, m_endDate3, m_endDate4, m_endDate5 };


            //1回
            if (repeat_kubun == 1)
            { 
                start_dates[group_idx].Enabled = false;
                end_dates[group_idx].Enabled = false;

                //DatetimeFormatを変更する
                DateTimePicker dtp = (DateTimePicker)alert_dates[group_idx];
                dtp.CustomFormat = "yyyy年M月d日(dddd) HH:mm";
            }
            //毎時
            else if (repeat_kubun == 2)
            {
                start_dates[group_idx].Enabled = true;
                end_dates[group_idx].Enabled = true;

                //DatetimeFormatを変更する
                DateTimePicker dtp = (DateTimePicker)alert_dates[group_idx];
                dtp.CustomFormat = "mm分";

            }
            //毎日
            else if (repeat_kubun == 3)
            {
                start_dates[group_idx].Enabled = true;
                end_dates[group_idx].Enabled = true;

                //DatetimeFormatを変更する
                DateTimePicker dtp = (DateTimePicker)alert_dates[group_idx];
                dtp.CustomFormat = "HH:mm";

            }
            //毎週
            else if (repeat_kubun == 4)
            {
                start_dates[group_idx].Enabled = true;
                end_dates[group_idx].Enabled = true;

                //DatetimeFormatを変更する
                DateTimePicker dtp = (DateTimePicker)alert_dates[group_idx];
                dtp.CustomFormat = "(dddd) HH:mm";
            }
            //毎月
            else if (repeat_kubun == 5)
            {
                start_dates[group_idx].Enabled = true;
                end_dates[group_idx].Enabled = true;

                //DatetimeFormatを変更する
                DateTimePicker dtp = (DateTimePicker)alert_dates[group_idx];
                dtp.CustomFormat = "d日  HH:mm";
            }
        }

        //タイマー①
        private void m_radio_one1_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用可にする
            if (m_radio_one1.Checked)
                timer_datetime(0, 1);


        }
        //タイマー①
        private void m_radio_hour1_CheckedChanged(object sender, EventArgs e)
        {
            //チェックされていたら日時を利用可にする
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
            if (m_radio_hour3.Checked)
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
            if (m_radio_day4.Checked)
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
            m_taskList.Clear();
            List<taskDS> taskdsList = new List<taskDS>();
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
                            
                            if (0 <= m_selecttext.Text.IndexOf("インシデント"))
                                param_dict["schedule_type"] = "1";
                            else if (0 <= m_selecttext.Text.IndexOf("定期作業"))
                                param_dict["schedule_type"] = "2";
                            else if (0 <= m_selecttext.Text.IndexOf("計画作業"))
                                param_dict["schedule_type"] = "3";
                            else if (0 <= m_selecttext.Text.IndexOf("特別対応"))
                                param_dict["schedule_type"] = "4";
                            break;
                        case 2:// カスタマ名
                            param_dict["username"] = m_selecttext.Text;
                            break;

                        case 3://テンプレート
                            param_dict["templeteno"] = m_selecttext.Text;
                            break;

                        case 4: //開始日時

                            param_dict["startdate"] = m_selecttext.Text;
                            break;

                        case 5:
                            param_dict["enddate"] = m_selecttext.Text;
                            break;

                        case 6: // ステータス
                            if (m_selecttext.Text == "有効")
                                param_dict["status"] = "1";
                            else if (m_selecttext.Text == "無効")
                                param_dict["status"] = "0";
                            break;

                        case 7: // 内容
                            param_dict["naiyou"] = m_selecttext.Text;
                            break;
                        case 8: // 備考
                            param_dict["biko"] = m_selecttext.Text;
                            break;

                        case 9: //タイマー名
                            param_dict["timername_timer"] = m_selecttext.Text;
                            break;
                        case 10: //アラート日時
                            param_dict["alert_time_timer"] = m_selecttext.Text;
                            break;
                        case 11: //繰り返し区分

                            string words = m_selecttext.Text;
                            if (words == "1回")
                                param_dict["repeat_type_timer"] = "1";
                            else if (words == "毎時")
                                param_dict["repeat_type_timer"] = "2";
                            else if (words == "毎日")
                                param_dict["repeat_type_timer"] = "3";
                            else if (words == "毎週")
                                param_dict["repeat_type_timer"] = "4";
                            else if (words == "毎月")
                                param_dict["repeat_type_timer"] = "5";

                            break;
                        case 12: //タイマー開始日時
                            param_dict["start_date_timer"] = m_selecttext.Text;
                            break;
                        case 13: //タイマー終了日時
                            param_dict["end_date_timer"] = m_selecttext.Text;
                            break;

                        case 14: //作成日時
                            param_dict["ins_date"] = m_selecttext.Text;
                            break;
                        case 15: //作成者
                            param_dict["ins_name_id"] = m_selecttext.Text;
                            break;
                        case 16:  //更新日時
                            param_dict["chk_date"] = m_selecttext.Text;
                            break;

                        case 17: //更新者
                            param_dict["chk_name_id"] = m_selecttext.Text;
                            break;

                        default:
                            break;
                    }
                }
            }
            //まず件数を取得する
            Int64 count = dg.getTaskListCount((Form_MainList)this.Owner, param_dict, con);
            if (MessageBox.Show(count.ToString() + "件ヒットしました。表示しますか？", "ホスト", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            //タスク一覧を取得する
            taskDSlist = dg.getTaskList((Form_MainList)this.Owner, param_dict, con);

            this.splitContainer1.SplitterDistance = 170;

            this.m_taskList.VirtualMode = true;
            // １行全体選択
            this.m_taskList.FullRowSelect = true;
            this.m_taskList.HideSelection = false;
            this.m_taskList.HeaderStyle = ColumnHeaderStyle.Clickable;
            //Hook up handlers for VirtualMode events.
            this.m_taskList.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(task_RetrieveVirtualItem);
            this.m_taskList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.m_taskList.Scrollable = true;

            this.m_taskList.Columns.Insert(0, "タスク通番", 30, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(1, "タスク区分", 50, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(2, "カスタマ通番", 90, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(3, "カスタマ名", 90, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(4, "テンプレート", 90, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(5, "開始日時", 80, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(6, "終了日時", 300, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(7, "ステータス", 50, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(8, "内容", 120, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(9, "備考", 120, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(10, "作成日時", 50, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(11, "作成者", 50, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(12, "更新日時", 110, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(13, "更新者", 50, HorizontalAlignment.Left);

            this.m_taskList.Columns.Insert(14, "タイマー名1", 120, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(15, "アラート日時1", 120, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(16, "繰り返し区分1", 120, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(17, "タイマーステータス1", 120, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(18, "タイマー開始日時1", 50, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(19, "タイマー終了日時1", 50, HorizontalAlignment.Left);

            this.m_taskList.Columns.Insert(20, "タイマー名2", 120, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(21, "アラート日時2", 120, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(22, "繰り返し区分2", 120, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(23, "タイマーステータス2", 120, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(24, "タイマー開始日時2", 50, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(25, "タイマー終了日時2", 50, HorizontalAlignment.Left);

            this.m_taskList.Columns.Insert(26, "タイマー名3", 120, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(27, "アラート日時3", 120, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(28, "繰り返し区分3", 120, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(29, "タイマーステータス3", 120, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(30, "タイマー開始日時3", 50, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(31, "タイマー終了日時3", 50, HorizontalAlignment.Left);
        
            this.m_taskList.Columns.Insert(32, "タイマー名4", 120, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(33, "アラート日時4", 120, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(34, "繰り返し区分4", 120, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(35, "タイマーステータス4", 120, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(36, "タイマー開始日時4", 50, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(37, "タイマー終了日時4", 50, HorizontalAlignment.Left);

            this.m_taskList.Columns.Insert(38, "タイマー名5", 120, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(39, "アラート日時5", 120, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(40, "繰り返し区分5", 120, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(41, "タイマーステータス5", 120, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(42, "タイマー開始日時5", 50, HorizontalAlignment.Left);
            this.m_taskList.Columns.Insert(43, "タイマー終了日時5", 50, HorizontalAlignment.Left);

            //リストビューを初期化する
            task_list = new DataTable("table1");
            task_list.Columns.Add("タスク通番", Type.GetType("System.Int32"));
            task_list.Columns.Add("タスク区分", Type.GetType("System.String"));
            task_list.Columns.Add("カスタマ番号", Type.GetType("System.String"));
            task_list.Columns.Add("カスタマ名", Type.GetType("System.String"));
            task_list.Columns.Add("テンプレート", Type.GetType("System.String"));
            task_list.Columns.Add("開始日時", Type.GetType("System.String"));
            task_list.Columns.Add("終了日時", Type.GetType("System.String"));
            task_list.Columns.Add("ステータス", Type.GetType("System.String"));
            task_list.Columns.Add("内容", Type.GetType("System.String"));
            task_list.Columns.Add("備考", Type.GetType("System.String"));
            task_list.Columns.Add("作成日時", Type.GetType("System.String"));
            task_list.Columns.Add("作成者", Type.GetType("System.String"));
            task_list.Columns.Add("更新日時", Type.GetType("System.String"));
            task_list.Columns.Add("更新者", Type.GetType("System.String"));

            task_list.Columns.Add("タイマー名1", Type.GetType("System.String"));
            task_list.Columns.Add("アラート日時1", Type.GetType("System.String"));
            task_list.Columns.Add("繰り返し区分1", Type.GetType("System.String"));
            task_list.Columns.Add("タイマーステータス1", Type.GetType("System.String"));
            task_list.Columns.Add("タイマー開始日時1", Type.GetType("System.String"));
            task_list.Columns.Add("タイマー終了日時1", Type.GetType("System.String"));

            task_list.Columns.Add("タイマー名2", Type.GetType("System.String"));
            task_list.Columns.Add("アラート日時2", Type.GetType("System.String"));
            task_list.Columns.Add("繰り返し区分2", Type.GetType("System.String"));
            task_list.Columns.Add("タイマーステータス2", Type.GetType("System.String"));
            task_list.Columns.Add("タイマー開始日時2", Type.GetType("System.String"));
            task_list.Columns.Add("タイマー終了日時2", Type.GetType("System.String"));

            task_list.Columns.Add("タイマー名3", Type.GetType("System.String"));
            task_list.Columns.Add("アラート日時3", Type.GetType("System.String"));
            task_list.Columns.Add("繰り返し区分3", Type.GetType("System.String"));
            task_list.Columns.Add("タイマーステータス3", Type.GetType("System.String"));
            task_list.Columns.Add("タイマー開始日時3", Type.GetType("System.String"));
            task_list.Columns.Add("タイマー終了日時3", Type.GetType("System.String"));

            task_list.Columns.Add("タイマー名4", Type.GetType("System.String"));
            task_list.Columns.Add("アラート日時4", Type.GetType("System.String"));
            task_list.Columns.Add("繰り返し区分4", Type.GetType("System.String"));
            task_list.Columns.Add("タイマーステータス4", Type.GetType("System.String"));
            task_list.Columns.Add("タイマー開始日時4", Type.GetType("System.String"));
            task_list.Columns.Add("タイマー終了日時4", Type.GetType("System.String"));

            task_list.Columns.Add("タイマー名5", Type.GetType("System.String"));
            task_list.Columns.Add("アラート日時5", Type.GetType("System.String"));
            task_list.Columns.Add("繰り返し区分5", Type.GetType("System.String"));
            task_list.Columns.Add("タイマーステータス5", Type.GetType("System.String"));
            task_list.Columns.Add("タイマー開始日時5", Type.GetType("System.String"));
            task_list.Columns.Add("タイマー終了日時5", Type.GetType("System.String"));




            //リストに表示
            if (taskDSlist != null)
            {
                m_taskList.BeginUpdate();
                foreach (taskDS s_ds in taskDSlist)
                {


                    DataRow urow = task_list.NewRow();
                    urow["タスク通番"] = s_ds.schedule_no;

                    urow["ステータス"] = s_ds.status;
                    urow["カスタマ番号"] = s_ds.userno;
                    urow["カスタマ名"] = s_ds.username;

                    //1:インシデント
                    //2:定期作業
                    //3:計画作業
                    //4:特別対応
                    string typestr = "";
                    if (s_ds.schedule_type == "1")
                        typestr = "インシデント";
                    else if (s_ds.schedule_type == "2")
                        typestr = "定期作業";
                    else if (s_ds.schedule_type == "3")
                        typestr = "計画作業";
                    else if (s_ds.schedule_type == "4")
                        typestr = "特別対応";

                    urow["タスク区分"] = typestr;

                    urow["テンプレート"] = s_ds.templeteno;


                    urow["開始日時"] = s_ds.startdate;

                    urow["終了日時"] = s_ds.enddate;
                    urow["内容"] = s_ds.naiyou;
                    urow["備考"] = s_ds.biko;
                    urow["作成日時"] = s_ds.ins_date;
                    urow["作成者"] = s_ds.ins_name_id;
                    urow["更新日時"] = s_ds.chk_date;
                    urow["更新者"] = s_ds.chk_name_id;

                    if (s_ds.timerDS_Dict.Count > 0)
                    {

                        if (s_ds.timerDS_Dict.ContainsKey("1"))
                        {
                            if (s_ds.timerDS_Dict["1"].timername != "")
                            {
                                urow["タイマー名1"] = s_ds.timerDS_Dict["1"].timername;
                                urow["アラート日時1"] = s_ds.timerDS_Dict["1"].alert_time;
                                string words = "";
                                switch(s_ds.timerDS_Dict["1"].repeat_type)
                                {
                                    case "1":
                                        words = "1回";
                                        break;

                                    case "2":
                                        words = "毎時";
                                        break;
                                    case "3":
                                        words = "毎日";
                                        break;
                                    case "4":
                                        words = "毎週";
                                        break;
                                    case "5":
                                        words = "毎月";
                                        break;
                                    default:
                                        break;
                                }

                                urow["繰り返し区分1"] = words;
                                urow["タイマーステータス1"] = s_ds.timerDS_Dict["1"].status;
                                urow["タイマー開始日時1"] = s_ds.timerDS_Dict["1"].start_date;
                                urow["タイマー終了日時1"] = s_ds.timerDS_Dict["1"].end_date;
                            }
                        }
                        if (s_ds.timerDS_Dict.ContainsKey("2") )
                        {
                            if (s_ds.timerDS_Dict["2"].timername != "")
                            {
                                urow["タイマー名2"] = s_ds.timerDS_Dict["2"].timername;
                                urow["アラート日時2"] = s_ds.timerDS_Dict["2"].alert_time;
                                string words = "";
                                switch (s_ds.timerDS_Dict["2"].repeat_type)
                                {
                                    case "1":
                                        words = "1回";
                                        break;

                                    case "2":
                                        words = "毎時";
                                        break;
                                    case "3":
                                        words = "毎日";
                                        break;
                                    case "4":
                                        words = "毎週";
                                        break;
                                    case "5":
                                        words = "毎月";
                                        break;
                                    default:
                                        break;
                                }


                                urow["繰り返し区分2"] = words;
                                urow["タイマーステータス2"] = s_ds.timerDS_Dict["2"].status;
                                urow["タイマー開始日時2"] = s_ds.timerDS_Dict["2"].start_date;
                                urow["タイマー終了日時2"] = s_ds.timerDS_Dict["2"].end_date;
                            }
                        }
                        if (s_ds.timerDS_Dict.ContainsKey("3"))
                        {
                            if (s_ds.timerDS_Dict["3"].timername != "")
                            {

                                urow["タイマー名3"] = s_ds.timerDS_Dict["3"].timername;
                                urow["アラート日時3"] = s_ds.timerDS_Dict["3"].alert_time;
                                string words = "";
                                switch (s_ds.timerDS_Dict["3"].repeat_type)
                                {
                                    case "1":
                                        words = "1回";
                                        break;

                                    case "2":
                                        words = "毎時";
                                        break;
                                    case "3":
                                        words = "毎日";
                                        break;
                                    case "4":
                                        words = "毎週";
                                        break;
                                    case "5":
                                        words = "毎月";
                                        break;
                                    default:
                                        break;
                                }


                                urow["繰り返し区分3"] = words;

                                urow["タイマーステータス3"] = s_ds.timerDS_Dict["3"].status;
                                urow["タイマー開始日時3"] = s_ds.timerDS_Dict["3"].start_date;
                                urow["タイマー終了日時3"] = s_ds.timerDS_Dict["3"].end_date;
                            }
                        }
                        if (s_ds.timerDS_Dict.ContainsKey("4"))
                        {
                            if (s_ds.timerDS_Dict["4"].timername != "")
                            {

                                urow["タイマー名4"] = s_ds.timerDS_Dict["4"].timername;
                                urow["アラート日時4"] = s_ds.timerDS_Dict["4"].alert_time;
                                string words = "";
                                switch (s_ds.timerDS_Dict["4"].repeat_type)
                                {
                                    case "1":
                                        words = "1回";
                                        break;

                                    case "2":
                                        words = "毎時";
                                        break;
                                    case "3":
                                        words = "毎日";
                                        break;
                                    case "4":
                                        words = "毎週";
                                        break;
                                    case "5":
                                        words = "毎月";
                                        break;
                                    default:
                                        break;
                                }


                                urow["繰り返し区分4"] = words;
                                urow["タイマーステータス4"] = s_ds.timerDS_Dict["4"].status;
                                urow["タイマー開始日時4"] = s_ds.timerDS_Dict["4"].start_date;
                                urow["タイマー終了日時4"] = s_ds.timerDS_Dict["4"].end_date;
                            }
                        }
                        if (s_ds.timerDS_Dict.ContainsKey("5"))
                        {
                            if (s_ds.timerDS_Dict["5"].timername != "")
                            {

                                urow["タイマー名5"] = s_ds.timerDS_Dict["5"].timername;
                                urow["アラート日時5"] = s_ds.timerDS_Dict["5"].alert_time;

                                string words = "";
                                switch (s_ds.timerDS_Dict["5"].repeat_type)
                                {
                                    case "1":
                                        words = "1回";
                                        break;

                                    case "2":
                                        words = "毎時";
                                        break;
                                    case "3":
                                        words = "毎日";
                                        break;
                                    case "4":
                                        words = "毎週";
                                        break;
                                    case "5":
                                        words = "毎月";
                                        break;
                                    default:
                                        break;
                                }


                                urow["繰り返し区分5"] = words;
                                urow["タイマーステータス5"] = s_ds.timerDS_Dict["5"].status;
                                urow["タイマー開始日時5"] = s_ds.timerDS_Dict["5"].start_date;
                                urow["タイマー終了日時5"] = s_ds.timerDS_Dict["5"].end_date;
                            }
                        }
                    }

                    task_list.Rows.Add(urow);
                }
                this.m_taskList.VirtualListSize = task_list.Rows.Count;
                this.m_taskList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                m_taskList.EndUpdate();
            }
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
            //if (m_templeteCombo.Text == "")
            //{
            //MessageBox.Show("テンプレートが取得できませんでした。", "タスク情報更新", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //return;
            //}
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
            //特別対応
            else if (m_schedule_combo.SelectedIndex == 3)
                yoteikbn = "4";



            int? tempint = null;
            if(m_templeteCombo.Text != "")
            {
                tempint = int.Parse(m_templeteCombo.SelectedValue.ToString());
            }


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
                command.Parameters.Add(new NpgsqlParameter("templeteno", DbType.Int32) { Value = tempint });
                command.Parameters.Add(new NpgsqlParameter("startdate", DbType.DateTime) { Value = startdate });
                command.Parameters.Add(new NpgsqlParameter("enddate", DbType.DateTime) { Value = enddate });
                command.Parameters.Add(new NpgsqlParameter("naiyou", DbType.String) { Value = m_naiyou.Text });
                command.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = m_biko.Text });
                command.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = int.Parse(m_userno.Text) });
                command.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });
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
                        int ret = deleteTimertbl(m_taskno.Text);
                        if (ret != 1)
                        {
                            //return -1;
                        }
                        //タイマー情報も更新する
                        ret = updateTimertbl(m_taskno.Text, yoteikbn);


                        if (ret == 1)
                        {

                            transaction.Commit();
                            MessageBox.Show("タスク情報を変更しました。", "タスク更新", MessageBoxButtons.OK);
                        }
                        else if (ret == -1)
                        {

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
            int ret = 0;
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
                    ret = alerm_insert(int.Parse(str_taskno), yoteikbn, alertdt, timeridx);
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
                        ret = alerm_insert(int.Parse(str_taskno), yoteikbn, alertdate, timeridx);
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
                        ret = alerm_insert(int.Parse(str_taskno), yoteikbn, alertdate, timeridx);
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
                            ret = alerm_insert(int.Parse(str_taskno), yoteikbn, alertdate, timeridx);
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
                        ret = alerm_insert(int.Parse(str_taskno), yoteikbn, alertdate, timeridx);
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


                    DateTime? startdate = null;
                    DateTime? enddate = null;

                    if (obj.Enabled && obj.Checked == true)
                        startdate = obj.Value;

                    obj = (DateTimePicker)end_dates[i];

                    if (obj.Enabled && obj.Checked == true)
                        enddate = obj.Value;

                    if (con.FullState != ConnectionState.Open) con.Open();

                    //                    string sql = "update timer set timername=:timername,repeat_type=:repeat_type,alert_time=:alert_time,start_date=:start_date,end_date=:end_date,status=:status," +
                    //                         "chk_name_id=:chk_name_id where schedule_no = :no and timerid = :timerid";


                    string sql = "insert into timer ( schedule_no,timerid,timername,repeat_type,alert_time,start_date,end_date,status," +
                        "chk_name_id) values ( :schedule_no,:timerid,:timername,:repeat_type,:alert_time,:start_date,:end_date,:status,:chk_name_id ) ";

                    var command = new NpgsqlCommand(@sql, this.con);

                    int scheduleno = int.Parse(m_taskno.Text);

                    command.Parameters.Add(new NpgsqlParameter("schedule_no", DbType.Int32) { Value = m_taskno.Text });
                    command.Parameters.Add(new NpgsqlParameter("timerid", DbType.Int32) { Value = (i + 1) });
                    command.Parameters.Add(new NpgsqlParameter("timername", DbType.String) { Value = title });
                    command.Parameters.Add(new NpgsqlParameter("repeat_type", DbType.String) { Value = rep_type });
                    command.Parameters.Add(new NpgsqlParameter("alert_time", DbType.DateTime) { Value = alert_time });
                    command.Parameters.Add(new NpgsqlParameter("start_date", DbType.DateTime) { Value = startdate });
                    command.Parameters.Add(new NpgsqlParameter("end_date", DbType.DateTime) { Value = enddate });
                    command.Parameters.Add(new NpgsqlParameter("naiyou", DbType.String) { Value = m_naiyou.Text });
                    command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                    command.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });
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
                        if (ret != 1)
                        {
                            //return -1;
                        }
                        //引き続きアラートデータを作成し登録する
                        ret = make_alert(i + 1, m_taskno.Text, yoteikbn);
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
        //タイマーテーブルの削除
        public int deleteTimertbl(String scheduleno)
        {
            
            //DB接続
            NpgsqlCommand cmd;
            try
            {
                if (con.FullState != ConnectionState.Open) con.Open();
                Int32 rowsaffected;
                //データ登録
                cmd = new NpgsqlCommand(@"DELETE FROM timer WHERE schedule_no = :schedule_no", con);

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

        //アラート削除
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
                    //return 0;
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

        //カスタマコンボボックスが変更された時
        private void m_usernameCombo_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            if (m_usernameCombo.Text == "")
            {
                m_userno.Text = "";
                return;
            }
            m_userno.Text = m_usernameCombo.SelectedValue.ToString();

            //テンプレートも変わる
            dispTempleteCombo();

        }
        //キャンセルボタン
        private void button8_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_taskList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.task_list == null)
                return;
            if (this.task_list.Rows.Count <= 0)
                return;
            //DataViewクラス ソートするためのクラス
            DataView dv = new DataView(task_list);

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
            dttmp = task_list.Clone();
            //ソートを実行
            dv.Sort = task_list.Columns[e.Column].ColumnName + strSort;

            // ソートされたレコードのコピー
            foreach (DataRowView drv in dv)
            {
                // 一時テーブルに格納
                dttmp.ImportRow(drv.Row);
            }
            //格納したテーブルデータを上書く
            task_list = dttmp.Copy();

            //行が存在するかチェックを行う。
            if (this.m_taskList.TopItem != null)
            {
                //現在一番上の行に表示されている行を取得
                int start = m_taskList.TopItem.Index;
                // ListView画面の再表示を行う
                m_taskList.RedrawItems(start, m_taskList.Items.Count - 1, true);
            }
        }
        //ダブルクリック
        private void m_taskList_DoubleClick(object sender, EventArgs e)
        {
           
            ListView.SelectedIndexCollection item = m_taskList.SelectedIndices;
            string schedule_type = "";
            taskDS taskdt = new taskDS();
            List<timerDS> timerList = new List<timerDS>();

            //タスク通番
            taskdt.schedule_no = this.m_taskList.Items[item[0]].SubItems[0].Text;

            //タスク区分
            if (this.m_taskList.Items[item[0]].SubItems[1].Text == "インシデント")
                schedule_type = "1";
            else if (this.m_taskList.Items[item[0]].SubItems[1].Text == "定期作業")
                schedule_type = "2";
            else if (this.m_taskList.Items[item[0]].SubItems[1].Text == "計画作業")
                schedule_type = "3";
            else if (this.m_taskList.Items[item[0]].SubItems[1].Text == "特別対応")
                schedule_type = "4";

            taskdt.schedule_type = schedule_type;

            //カスタマ通番
            taskdt.userno = this.m_taskList.Items[item[0]].SubItems[2].Text;
            //カスタマ名
            taskdt.username = this.m_taskList.Items[item[0]].SubItems[3].Text;

            //テンプレート
            taskdt.templeteno = this.m_taskList.Items[item[0]].SubItems[4].Text;

            //開始日時
            taskdt.enddate = this.m_taskList.Items[item[0]].SubItems[5].Text;
            //終了日時
            taskdt.startdate = this.m_taskList.Items[item[0]].SubItems[6].Text;

            //ステータス
            string stat = this.m_taskList.Items[item[0]].SubItems[7].Text;
            if (stat == "有効")
                taskdt.status = "1";
            else if (stat == "無効")
                taskdt.status = "0";
            //内容
            taskdt.naiyou = this.m_taskList.Items[item[0]].SubItems[8].Text;
            //備考
            taskdt.biko = this.m_taskList.Items[item[0]].SubItems[9].Text;

            //作成日時
            taskdt.ins_date = this.m_taskList.Items[item[0]].SubItems[10].Text;

            //作成者
            taskdt.ins_name_id = this.m_taskList.Items[item[0]].SubItems[11].Text;

            //更新日時
            taskdt.chk_date = this.m_taskList.Items[item[0]].SubItems[12].Text;

            //更新者
            taskdt.chk_name_id = this.m_taskList.Items[item[0]].SubItems[13].Text;

            //タイマー①
            timerDS timeDS = new timerDS();
            if(this.m_taskList.Items[item[0]].SubItems[14].Text != "")
            {
                timeDS.timerid = "1";
                timeDS.timername= this.m_taskList.Items[item[0]].SubItems[14].Text;
                timeDS.alert_time = this.m_taskList.Items[item[0]].SubItems[15].Text;
                string repeat_type = "";
                //1:1回、2:1時間毎、3:日毎、4:週毎、5:月毎
                switch (this.m_taskList.Items[item[0]].SubItems[16].Text)
                {
                    case "1回":
                        repeat_type = "1";
                        break;
                    case "1時間毎":
                        repeat_type = "2";
                        break;
                    case "日毎":
                        repeat_type = "3";
                        break;
                    case "週毎":
                        repeat_type = "4";
                        break;
                    case "月毎":
                        repeat_type = "5";
                        break;
                    default:
                        repeat_type = "";
                        break;
                }

                timeDS.repeat_type = repeat_type;

                timeDS.status = this.m_taskList.Items[item[0]].SubItems[17].Text;
                timeDS.start_date = this.m_taskList.Items[item[0]].SubItems[18].Text;
                timeDS.end_date= this.m_taskList.Items[item[0]].SubItems[19].Text;
                timerList.Add(timeDS);
            }
            //タイマー②
            timeDS = new timerDS();
            if (this.m_taskList.Items[item[0]].SubItems[20].Text != "")
            {
                timeDS.timerid = "2";
                timeDS.timername = this.m_taskList.Items[item[0]].SubItems[20].Text;
                timeDS.alert_time = this.m_taskList.Items[item[0]].SubItems[21].Text;
                string repeat_type = "";
                //1:1回、2:1時間毎、3:日毎、4:週毎、5:月毎
                switch (this.m_taskList.Items[item[0]].SubItems[22].Text)
                {
                    case "1回":
                        repeat_type = "1";
                        break;
                    case "1時間毎":
                        repeat_type = "2";
                        break;
                    case "日毎":
                        repeat_type = "3";
                        break;
                    case "週毎":
                        repeat_type = "4";
                        break;
                    case "月毎":
                        repeat_type = "5";
                        break;
                    default:
                        repeat_type = "";
                        break;
                }

                timeDS.repeat_type = repeat_type;

                timeDS.status = this.m_taskList.Items[item[0]].SubItems[23].Text;
                timeDS.start_date = this.m_taskList.Items[item[0]].SubItems[24].Text;
                timeDS.end_date = this.m_taskList.Items[item[0]].SubItems[25].Text;
                timerList.Add(timeDS);
            }
            //タイマー③
            timeDS = new timerDS();
            if (this.m_taskList.Items[item[0]].SubItems[26].Text != "")
            {
                timeDS.timerid = "3";
                timeDS.timername = this.m_taskList.Items[item[0]].SubItems[26].Text;
                timeDS.alert_time = this.m_taskList.Items[item[0]].SubItems[27].Text;
                string repeat_type = "";
                //1:1回、2:1時間毎、3:日毎、4:週毎、5:月毎
                switch (this.m_taskList.Items[item[0]].SubItems[28].Text)
                {
                    case "1回":
                        repeat_type = "1";
                        break;
                    case "1時間毎":
                        repeat_type = "2";
                        break;
                    case "日毎":
                        repeat_type = "3";
                        break;
                    case "週毎":
                        repeat_type = "4";
                        break;
                    case "月毎":
                        repeat_type = "5";
                        break;
                    default:
                        repeat_type = "";
                        break;
                }

                timeDS.repeat_type = repeat_type;

                timeDS.status = this.m_taskList.Items[item[0]].SubItems[29].Text;
                timeDS.start_date = this.m_taskList.Items[item[0]].SubItems[30].Text;
                timeDS.end_date = this.m_taskList.Items[item[0]].SubItems[31].Text;
                timerList.Add(timeDS);
            }

            //タイマー④
            timeDS = new timerDS();
            if (this.m_taskList.Items[item[0]].SubItems[32].Text != "")
            {
                timeDS.timerid = "4";
                timeDS.timername = this.m_taskList.Items[item[0]].SubItems[32].Text;
                timeDS.alert_time = this.m_taskList.Items[item[0]].SubItems[33].Text;
                string repeat_type = "";
                //1:1回、2:1時間毎、3:日毎、4:週毎、5:月毎
                switch (this.m_taskList.Items[item[0]].SubItems[34].Text)
                {
                    case "1回":
                        repeat_type = "1";
                        break;
                    case "1時間毎":
                        repeat_type = "2";
                        break;
                    case "日毎":
                        repeat_type = "3";
                        break;
                    case "週毎":
                        repeat_type = "4";
                        break;
                    case "月毎":
                        repeat_type = "5";
                        break;
                    default:
                        repeat_type = "";
                        break;
                }

                timeDS.repeat_type = repeat_type;

                timeDS.status = this.m_taskList.Items[item[0]].SubItems[35].Text;
                timeDS.start_date = this.m_taskList.Items[item[0]].SubItems[36].Text;
                timeDS.end_date = this.m_taskList.Items[item[0]].SubItems[37].Text;
                timerList.Add(timeDS);
            }

            //タイマー⑤
            timeDS = new timerDS();
            if (this.m_taskList.Items[item[0]].SubItems[38].Text != "")
            {
                timeDS.timerid = "5";
                timeDS.timername = this.m_taskList.Items[item[0]].SubItems[38].Text;
                timeDS.alert_time = this.m_taskList.Items[item[0]].SubItems[39].Text;
                string repeat_type = "";
                //1:1回、2:1時間毎、3:日毎、4:週毎、5:月毎
                switch (this.m_taskList.Items[item[0]].SubItems[40].Text)
                {
                    case "1回":
                        repeat_type = "1";
                        break;
                    case "1時間毎":
                        repeat_type = "2";
                        break;
                    case "日毎":
                        repeat_type = "3";
                        break;
                    case "週毎":
                        repeat_type = "4";
                        break;
                    case "月毎":
                        repeat_type = "5";
                        break;
                    default:
                        repeat_type = "";
                        break;
                }

                timeDS.repeat_type = repeat_type;

                timeDS.status = this.m_taskList.Items[item[0]].SubItems[41].Text;
                timeDS.start_date = this.m_taskList.Items[item[0]].SubItems[42].Text;
                timeDS.end_date = this.m_taskList.Items[item[0]].SubItems[43].Text;
                timerList.Add(timeDS);
            }

            //タスク情報の表示

            //入力クリア
            inputClear();

            gettask(taskdt, timerList);

        }

        void inputClear()
        {
            m_taskno.Text = "";
            
            m_schedule_combo.SelectedIndex = 0;
            m_userno.Text = "";
            if(m_templeteCombo.Enabled == true)
                m_templeteCombo.SelectedIndex = 0;

            m_startDate.Value = DateTime.Now;
            m_statusCombo.SelectedIndex = 0;
            m_endDate.Value = DateTime.Now;
            m_naiyou.Text = "";
            m_biko.Text = "";

            GroupBox[] radioGroup = new GroupBox[] { radioGroup1, radioGroup2, radioGroup3, radioGroup4, radioGroup5 };

            Control[] titles = new Control[] { m_title1, m_title2, m_title3, m_title4, m_title5 };
            Control[] alertDates = new Control[] { m_alermDate1, m_alermDate2, m_alermDate3, m_alermDate4, m_alermDate5 };
            Control[] statuses = new Control[] { m_statusCombo1, m_statusCombo2, m_statusCombo3, m_statusCombo4, m_statusCombo5 };
            Control[] start_dates = new Control[] { m_startDate1, m_startDate2, m_startDate3, m_startDate4, m_startDate5 };
            Control[] end_dates = new Control[] { m_endDate1, m_endDate2, m_endDate3, m_endDate4, m_endDate5 };

            Control[] Radioone = new Control[] { m_radio_one1, m_radio_one2, m_radio_one3, m_radio_one4, m_radio_one5 };
            Control[] RadioHours = new Control[] { m_radio_hour1, m_radio_hour2, m_radio_hour3, m_radio_hour4, m_radio_hour5 };
            Control[] RadioDays = new Control[] { m_radio_day1, m_radio_day2, m_radio_day3, m_radio_day4, m_radio_day5 };
            Control[] RadioWeeks = new Control[] { m_radio_week1, m_radio_week2, m_radio_week3, m_radio_week4, m_radio_week5 };
            Control[] RadioMonths = new Control[] { m_radio_month1, m_radio_month2, m_radio_month3, m_radio_month4, m_radio_month5 };

            int i = 0;
            int ridx = 0;
            GroupBox gb = null;
            for (i=0;i< 5; i++) {
                gb = radioGroup[i];
                ridx = 0;
                //ラジオボタンの値を取得
                foreach (RadioButton rb1 in gb.Controls)
                {
                    if (rb1.Text =="1回" )
                    {
                        rb1.Checked = true;
                        break;
                    }
                    ridx++;
                }
            }

            //タイマー名
            foreach (Control title in titles)
                title.Text = "";

            //ステータス
            foreach (Control statusobj in statuses)
                statusobj.Text = "有効";

            //開始日時
            foreach (DateTimePicker start_d in start_dates) 
                start_d.Checked = false ;

            //終了日時
            foreach (DateTimePicker end_d in end_dates)
                end_d.Checked = false;

        }
        //タスク区分が変更されたとき
        private void m_schedule_combo_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            dispTempleteCombo();
        }
        //削除ボタン
        private void button1_Click_1(object sender, EventArgs e)
        {

            ListView.SelectedIndexCollection selectitem = m_taskList.SelectedIndices;
            int count = selectitem.Count;


            //確認メッセージ
            if (MessageBox.Show("一覧に選択された行 " + count + "件 の削除を行います。その際参照している他のテーブルデータも削除されます。" + Environment.NewLine +
                "よろしいですか？", "タスク削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int ret = deleteschedule(selectitem);
            if (ret == -1)
                return;

            //リストの表示上からけす
            int i = 0;
            //削除するインターフェイス番号の取得
            int[] indices = new int[selectitem.Count];
            int cnt = m_taskList.SelectedIndices.Count;


            m_taskList.SelectedIndices.CopyTo(indices, 0);

            DataRowCollection items = task_list.Rows;
            for (i = cnt - 1; i >= 0; --i)
                items.RemoveAt(indices[i]);

            //総件数を変更し再表示を行う
            this.m_taskList.VirtualListSize = items.Count;

        }
        private int deleteschedule(ListView.SelectedIndexCollection item)
        {

            string scheduleno;
            int ret = 0;

            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "WITH DELETED AS (DELETE FROM timer where schedule_no = :no " +
                "RETURNING schedule_no) " +
                "DELETE FROM task where schedule_no = :no";

            using (var transaction = con.BeginTransaction())
            {
                int i = 0;
                for (i = 0; i < item.Count; i++)
                {
                    scheduleno = this.m_taskList.Items[item[i]].SubItems[0].Text;

                        var command = new NpgsqlCommand(@sql, con);
                    command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = int.Parse(scheduleno) });

                    Int32 rowsaffected;
                    try
                    {
                        //削除処理
                        rowsaffected = command.ExecuteNonQuery();

                        //if (rowsaffected < 1)
                        //{
                            //MessageBox.Show("削除できませんでした。タスク通番:" + scheduleno, "タスク情報削除");
                            //transaction.Rollback();
                            //return -1;
                        //}
                        //else
                        //{
                            //タイマー情報を削除する
                            ret = deleteTimer(scheduleno);
                            if(ret != 1)
                            {
                                MessageBox.Show("削除できませんでした。タスク通番:" + scheduleno, "タスク情報削除");
                                transaction.Rollback();
                                return -1;

                            }

                        //}
                    }
                    catch (Exception ex)
                    {
                        //エラー時メッセージ表示
                        MessageBox.Show("タスク情報削除時エラーが発生しました。 " + ex.Message);
                        if (transaction.Connection != null) transaction.Rollback();
                        return -1;
                    }
                }
                if (ret == 1)
                {
                    
                    MessageBox.Show("削除完了しました。", "タスク情報削除");
                    transaction.Commit();
                }
            }
            return ret;
        }

    }
}
