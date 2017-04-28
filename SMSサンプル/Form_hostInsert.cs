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
    public partial class Form_hostInsert : Form
    {
        //ユーザ
        public List<userDS> userList { get; set; }

        //システム
        public List<systemDS> systemList { get; set; }
        //拠点
        public List<siteDS> siteList { get; set; }

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
            //コンボボックスの初期値
            m_statusCombo.SelectedIndex = 0;

            //コンボボックス
            DataTable cutomerTable = new DataTable();
            cutomerTable.Columns.Add("ID", typeof(string));
            cutomerTable.Columns.Add("NAME", typeof(string));

            //ユーザ情報を取得する
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

        //ユーザ名コンボボックスが変更されたときに発生
        private void m_usernameCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
            Read_systemCombo();
            Read_siteCombo();
        }
        //ユーザ名のコンボボックスが変更されたときの処理
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
                //ユーザNOで区別する
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

            //コンボボックス
            DataTable siteTable = new DataTable();
            siteTable.Columns.Add("ID", typeof(string));
            siteTable.Columns.Add("NAME", typeof(string));

            //システム情報を取得する
            if (siteList.Count <= 0)
                return;
            //拠点情報を取得する
            foreach (siteDS v in siteList)
            {
                if (m_systemCombo.SelectedValue != null) { 
                    if (v.systemno == m_systemCombo.SelectedValue.ToString())
                        { 
                            DataRow row = siteTable.NewRow();
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
            if(siteTable.Rows.Count > 0) 
                m_siteno.Text = m_siteCombo.SelectedValue.ToString();
           
        }
        //ホスト名の登録
        private void button3_Click(object sender, EventArgs e)
        {
            //ユーザ名
            if (m_usernameCombo.Text == "")
            {
                MessageBox.Show("ユーザ名が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            MessageBox.Show("ホスト情報を登録します。よろしいですか？", "登録確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            string userno = m_userno.Text;
            string systemno = m_systemno.Text;
            string siteno = m_siteno.Text;
            string hostname = m_hostname.Text;
            string hostname_ja = m_hostname_ja.Text;
            string device = m_device.Text;
            string location = m_location.Text;
            string status = m_statusCombo.SelectedIndex.ToString();
            string usefor = m_usefor.Text;
            DateTime startdate = m_startdate.Value;
            DateTime enddate = m_enddate.Value;
            string hosyuno = m_hosyuno.Text;
            string hosyuinfo = m_hosyuinfo.Text;
            string biko = m_biko.Text;



            //DB接続
            Class_common common = new Class_common();
            NpgsqlCommand cmd;
            using (var con = common.DB_connection())
            {
                try
                {
                    con.Open();
                    Int32 rowsaffected;
                    //データ登録
                    cmd = new NpgsqlCommand(@"insert into host(hostname,hostname_ja,status,device,location,usefor,kansiStartdate,kansiEndsdate,hosyukanri,hosyuinfo,biko,systemno,siteno,userno,chk_name_id) 
                        values ( :hostname,:hostname_ja,:status,:device,:location,:usefor,:kansiStartdate,:kansiEndsdate,:hosyukanri,:hosyuinfo,:biko,:systemno,:siteno,:userno,:chk_name_id)", con);
                    cmd.Parameters.Add(new NpgsqlParameter("hostname", DbType.String) { Value = hostname });
                    cmd.Parameters.Add(new NpgsqlParameter("hostname_ja", DbType.String) { Value = hostname_ja });
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
                    cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = "111" });
                    rowsaffected = cmd.ExecuteNonQuery();

                    if (rowsaffected != 1)
                    {
                        MessageBox.Show("登録できませんでした。", "ホスト名登録");
                        con.Close();
                    }
                    else
                    {
                        //登録成功
                        MessageBox.Show("登録完了", "ホスト名登録");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("ホスト名登録時エラー " + ex.Message);
                    con.Close();
                    return;
                }

            }

        }

    }
}
