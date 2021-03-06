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
    public partial class Form_hostInsert : Form
    {
        //カスタマ
        public List<userDS> userList { get; set; }

        //システム
        public List<systemDS> systemList { get; set; }
        //拠点
        public List<siteDS> siteList { get; set; }


        //DBコネクション
        public NpgsqlConnection con { get; set; }
        public opeDS loginDS { get; set; }


        public Form_hostInsert()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        //表示前
        private void Form_hostInsert_Load(object sender, EventArgs e)
        {
            m_idlabel.Text = loginDS.opeid;
            m_labelinputOpe.Text = loginDS.lastname + loginDS.fastname;

            //コンボボックスの初期値
            m_statusCombo.SelectedIndex = 0;

            //コンボボックス
            DataTable cutomerTable = new DataTable();
            cutomerTable.Columns.Add("ID", typeof(string));
            cutomerTable.Columns.Add("NAME", typeof(string));

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
            Read_siteCombo();
        }

        //キャンセルボタン
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //システム名が選択されたとき
        private void m_systemCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //カスタマ名コンボボックスが変更されたときに発生
        private void m_usernameCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
            Read_systemCombo();

            Read_siteCombo();
        }
        //カスタマ名のコンボボックスが変更されたときの処理
        private void Read_systemCombo()
        {

            m_systemno.Text = "";
            m_systemCombo.DataSource = null;


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

            foreach (systemDS v in systemList)
            {
                //カスタマNOで区別する
                if (m_usernameCombo.SelectedValue != null) { 
                    if (v.userno == m_usernameCombo.SelectedValue.ToString())
                    {
                        DataRow row = systemTable.NewRow();
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
        //システム名コンボボックスが変更されたときに発生
        private void m_systemCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (m_systemCombo.SelectedValue != null)
                m_systemno.Text = m_systemCombo.SelectedValue.ToString();

            Read_siteCombo();

        }
        //拠点コンボボックスが変更されたとき
        private void m_siteCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (m_siteCombo.SelectedValue != null)
                m_siteno.Text = m_siteCombo.SelectedValue.ToString();
        }
        //システム名のコンボボックスが変更されたときの処理
        private void Read_siteCombo()
        {

            m_siteCombo.DataSource = null;
            m_siteno.Text = "";
            if (m_systemno.Text == "")
                return;

            //コンボボックス
            DataTable siteTable = new DataTable();
            siteTable.Columns.Add("ID", typeof(string));
            siteTable.Columns.Add("NAME", typeof(string));

            //システム情報を取得する

            if (siteList == null || siteList.Count <= 0)
            {

                try { 
                Class_Detaget getuser = new Class_Detaget();

                //検索
                List<siteDS> siteDSList = getuser.getSiteList(m_systemno.Text, con, true);

                //空白行を追加
                siteDS tmp = new siteDS();
                tmp.sitename = "";
                tmp.siteno = "";
                List<siteDS> tmpsiteDSList = new List<siteDS>();
                tmpsiteDSList.Add(tmp);

                //取得した行を空行についか
                if (tmpsiteDSList != null)
                    tmpsiteDSList.AddRange(siteDSList);

                m_siteCombo.DataSource = tmpsiteDSList;
                m_siteCombo.DisplayMember = "sitename";
                m_siteCombo.ValueMember = "siteno";
                //拠点名ラベルを表示
                if (siteDSList.Count > 0)
                    m_siteno.Text = m_siteCombo.SelectedValue.ToString();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("拠点情報取得に失敗しました。  " + ex.Message,"ホスト登録",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            return;
           
        }
        //ホスト名の登録
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

            //拠点
            if (m_siteCombo.Text == "")
            {
                MessageBox.Show("拠点名が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //ホスト名
            if (m_hostname.Text == "")
            {
                MessageBox.Show("ホスト名が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            //確認
            if (MessageBox.Show("ホスト情報を登録します。よろしいですか？", "登録確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string userno = m_userno.Text;
            string systemno = m_systemno.Text;
            string siteno = m_siteno.Text;
            string hostname = m_hostname.Text;
            string usefor = m_usefor.Text;
            string device = m_device.Text;
            string location = m_location.Text;
            //ステータス
            string status = "";
            if (m_statusCombo.SelectedIndex == 0)
                //有効
                status = "1";

            else
                //無効
                status = "0";

            string settikikiID = m_settikikiID.Text;
            DateTime startdate = m_startdate.Value;
            DateTime enddate = m_enddate.Value;
            string hosyuno = m_hosyuno.Text;
            string hosyuinfo = m_hosyuinfo.Text;
            string biko = m_biko.Text;



            //DB接続
            NpgsqlCommand cmd;
            try
            {
                if (con.FullState != ConnectionState.Open) con.Open();
                Int32 rowsaffected;
                //データ登録
                cmd = new NpgsqlCommand(@"insert into host(hostname,settikikiID,status,device,location,usefor,kansiStartdate,kansiEndsdate,hosyukanri,hosyuinfo,biko,systemno,siteno,userno,chk_name_id) 
                    values ( :hostname,:settikikiid,:status,:device,:location,:usefor,:kansiStartdate,:kansiEndsdate,:hosyukanri,:hosyuinfo,:biko,:systemno,:siteno,:userno,:chk_name_id)", con);
                cmd.Parameters.Add(new NpgsqlParameter("hostname", DbType.String) { Value = hostname });
                cmd.Parameters.Add(new NpgsqlParameter("settikikiID", DbType.String) { Value = settikikiID });
                cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                cmd.Parameters.Add(new NpgsqlParameter("device", DbType.String) { Value = device });
                cmd.Parameters.Add(new NpgsqlParameter("location", DbType.String) { Value = location });
                cmd.Parameters.Add(new NpgsqlParameter("usefor", DbType.String) { Value = usefor });
                cmd.Parameters.Add(new NpgsqlParameter("kansiStartdate", DbType.DateTime) { Value = startdate});
                cmd.Parameters.Add(new NpgsqlParameter("kansiEndsdate", DbType.DateTime) { Value = enddate });
                cmd.Parameters.Add(new NpgsqlParameter("hosyukanri", DbType.String) { Value = hosyuno });
                cmd.Parameters.Add(new NpgsqlParameter("hosyuinfo", DbType.String) { Value = hosyuinfo });
                cmd.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = biko });
                cmd.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = systemno });
                cmd.Parameters.Add(new NpgsqlParameter("siteno", DbType.Int32) { Value = siteno });
                cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = userno });
                cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = m_idlabel.Text });
                rowsaffected = cmd.ExecuteNonQuery();

                if (rowsaffected != 1)
                {
                    MessageBox.Show("登録できませんでした。", "ホスト名登録");
                }
                else
                {
                    //登録成功
                    MessageBox.Show("登録完了", "ホスト名登録");
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ホスト名登録時エラー " + ex.Message);
                return;
            }


        }

    }
}
