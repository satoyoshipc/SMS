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
    public partial class Form_kaisenInsert : Form
    {
        //ユーザ
        public List<userDS> userList { get; set; }

        //システム
        public List<systemDS> systemList { get; set; }

        //拠点
        public List<siteDS> siteList { get; set; }

        public Form_kaisenInsert()
        {
            InitializeComponent();
        }
        //登録ボタン
        private void button3_Click(object sender, EventArgs e)
        {
            //ユーザ名
            if (m_usernameCombo.Text == "")
            {
                MessageBox.Show("ユーザ名が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //キャリア
            if (m_career.Text == "")
            {
                MessageBox.Show("キャリアが入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            //確認
            MessageBox.Show("回線情報を登録します。よろしいですか？", "登録確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            Int32 userno = (m_userno.Text == "") ? 0 : int.Parse(m_userno.Text);
            Int32 systemno = (m_systemno.Text == "") ? 0 : int.Parse(m_systemno.Text);
            Int32 siteno = (m_siteno.Text == "") ? 0 : int.Parse(m_siteno.Text);
            Int32 hostno = (m_hostno.Text == "") ? 0 : int.Parse(m_hostno.Text);
            string status = m_statusCombo.SelectedIndex.ToString();
            string career = m_career.Text;
            string kaisensyubetu = m_kaisensyubetu.Text;
            string kaisenid = m_kaisenid.Text;
            string isp = m_isp.Text;
            string servicetype = m_servicetype.Text;
            string serviceid = m_serviceid.Text;



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
                    cmd = new NpgsqlCommand(@"insert into Kaisen(status,career,type,kaisenid,isp,servicetype,serviceid,siteno,userno,host_no,chk_name_id) 
                        values ( :status,:career,:type,:kaisenid,:isp,:servicetype,:serviceid,:siteno,:userno,:host_no,:chk_name_id)", con);
                    cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                    cmd.Parameters.Add(new NpgsqlParameter("career", DbType.String) { Value = career });
                    cmd.Parameters.Add(new NpgsqlParameter("type", DbType.String) { Value = kaisensyubetu });
                    cmd.Parameters.Add(new NpgsqlParameter("kaisenid", DbType.String) { Value = kaisenid });
                    cmd.Parameters.Add(new NpgsqlParameter("isp", DbType.String) { Value = isp });
                    cmd.Parameters.Add(new NpgsqlParameter("servicetype", DbType.String) { Value = servicetype });
                    cmd.Parameters.Add(new NpgsqlParameter("serviceid", DbType.String) { Value = serviceid });
                    cmd.Parameters.Add(new NpgsqlParameter("siteno", DbType.Int32) { Value = siteno });
                    cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = userno });
                    cmd.Parameters.Add(new NpgsqlParameter("host_no", DbType.Int32) { Value = hostno });
                    cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = "111" });
                    rowsaffected = cmd.ExecuteNonQuery();

                    if (rowsaffected != 1)
                    {
                        MessageBox.Show("登録できませんでした。", "回線情報登録");
                        con.Close();
                    }
                    else
                    {
                        //登録成功
                        MessageBox.Show("登録完了", "回線情報登録");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("回線情報登録時エラー " + ex.Message);
                    con.Close();
                    return;
                }

            }

        
        }

        //表示前処理
        private void Form_kaisenInsert_Load(object sender, EventArgs e)
        {
            //コンボボックスの初期値
            m_statusCombo.SelectedIndex = 0;

            //コンボボックス
            DataTable cutomerTable = new DataTable();
            cutomerTable.Columns.Add("ID", typeof(string));
            cutomerTable.Columns.Add("NAME", typeof(string));
            //ユーザ情報を取得する
            if (userList == null)
                return;
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
            
            //システム情報の読み込み
            Read_systemCombo();

        }
        //ユーザ名のコンボボックスが変更されたときの処理
        private void Read_systemCombo()
        {
            try {
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
                int i = 0;
                foreach (systemDS v in systemList)
                {
                    //
                    if (i == 0)
                        systemTable.Rows.Add("");

                    //ユーザNOで区別する
                    if (m_usernameCombo.SelectedValue != null)
                    {
                        if (v.userno == m_usernameCombo.SelectedValue.ToString())
                        {

                            DataRow row = systemTable.NewRow();
                            row["ID"] = v.systemno;
                            row["NAME"] = v.systemname;
                            systemTable.Rows.Add(row);
                        }
                    }
                    i++;
                }
                //データテーブルを割り当てる
                m_systemCombo.DataSource = systemTable;
                m_systemCombo.DisplayMember = "NAME";
                m_systemCombo.ValueMember = "ID";
                if (systemTable.Rows.Count > 0)
                    m_systemno.Text = m_systemCombo.SelectedValue.ToString();
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message,"システム情報読込",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }
        //キャンセルボタン
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        //システム名のコンボボックスが変更されたときの処理
        private void Read_siteCombo()
        {
            try { 
                m_siteCombo.DataSource = null;
                m_siteno.Text = "";

                //コンボボックス
                DataTable siteTable = new DataTable();
                siteTable.Columns.Add("ID", typeof(string));
                siteTable.Columns.Add("NAME", typeof(string));

                //システム情報を取得する
                if (siteList.Count <= 0)
                    return;

                int i = 0;

                //拠点情報を取得する
                foreach (siteDS v in siteList)
                {
                    if (m_systemCombo.SelectedValue != null)
                    {
                        if (i == 0)
                            siteTable.Rows.Add("");

                        if (v.systemno == m_systemCombo.SelectedValue.ToString())
                        {
                            DataRow row = siteTable.NewRow();
                            row["ID"] = v.siteno;
                            row["NAME"] = v.sitename;
                            siteTable.Rows.Add(row);
                        }
                    }
                    i++;
                }
                //データテーブルを割り当てる
                m_siteCombo.DataSource = siteTable;
                m_siteCombo.DisplayMember = "NAME";
                m_siteCombo.ValueMember = "ID";
                if (siteTable.Rows.Count > 0)
                    m_siteno.Text = m_siteCombo.SelectedValue.ToString();

            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message,"システム情報読込",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }
        //ホスト名を読み込む
        private void Read_hostCombo(string siteno)
        {
            try {
                m_hostCombo.DataSource = null;
                m_hostno.Text = "";

                Class_Detaget getuser = new Class_Detaget();

                //ホスト名を検索

                //空白行を追加
                hostDS tmp = new hostDS();
                tmp.hostname = "";
                tmp.host_no = "";
                List<hostDS> hostDSList = new List<hostDS>();
                hostDSList.Add(tmp);

                //リストの取得
                List<hostDS> hostDSList1 = getuser.getHostList(siteno, true);
                hostDSList.AddRange(hostDSList1);

                m_hostCombo.DataSource = hostDSList;
                m_hostCombo.DisplayMember = "hostname";
                m_hostCombo.ValueMember = "host_no";
                
                //ホスト名ラベルを表示
                if (hostDSList.Count > 0)
                    m_hostno.Text = m_hostCombo.SelectedValue.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"ホスト情報の読み込みに失敗",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }
        //ユーザ名が選択されたときの処理
        private void m_usernameCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            m_systemno.Text = "";
            m_siteno.Text = "";
            m_hostno.Text = "";
            m_siteCombo.DataSource = null;
            m_hostCombo.DataSource = null;

            Read_systemCombo();


        }
        //システムコンボボックスが変更されたとき
        private void m_systemCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            m_hostno.Text = "";
            m_hostCombo.DataSource = null;
            if (m_systemCombo.SelectedValue != null)
                m_systemno.Text = m_systemCombo.SelectedValue.ToString();
            Read_siteCombo();
            //Read_hostCombo(this.m_siteno.Text);


        }
        //拠点コンボボックスの値が変更されたとき
        private void m_siteCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            m_hostno.Text = "";
            m_hostCombo.DataSource = null;

            if (m_siteCombo.SelectedValue != null)
                m_siteno.Text = m_siteCombo.SelectedValue.ToString();

            if (m_siteno.Text != "") Read_hostCombo(this.m_siteno.Text);
            
        }
        //機器名コンボボックスの値が変更されたとき
        private void m_hostCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (m_hostCombo.SelectedValue != null)
                m_hostno.Text = m_hostCombo.SelectedValue.ToString();
        }
    }
}
