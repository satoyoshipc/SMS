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
    public partial class Form_kanshiInterfaceInsert : Form
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

        public Form_kanshiInterfaceInsert()
        {
            InitializeComponent();
        }
        //表示前処理
        private void Form_kanshiInterfaceInsert_Load(object sender, EventArgs e)
        {
            m_idlabel.Text = loginDS.opeid;
            m_labelinputOpe.Text = loginDS.lastname + loginDS.fastname;

            //コンボボックスの初期値
            m_statusCombo.SelectedIndex = 0;

            //コンボボックス
            DataTable cutomerTable = new DataTable();
            cutomerTable.Columns.Add("ID", typeof(string));
            cutomerTable.Columns.Add("NAME", typeof(string));

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
            Read_siteCombo();
            if (m_siteno.Text != null && m_siteno.Text != "")
                Read_hostCombo(m_siteno.Text);
        }
        //システム情報の読み込み
        private void Read_systemCombo()
        {
            if(m_usernameCombo.SelectedValue != null)
                m_userno.Text = m_usernameCombo.SelectedValue.ToString();

            m_systemno.Text = "";
            m_systemCombo.DataSource = null;

            //コンボボックス
            DataTable systemTable = new DataTable();
            systemTable.Columns.Add("ID", typeof(string));
            systemTable.Columns.Add("NAME", typeof(string));

            if (systemList == null)
                return;
            //カスタマ情報を取得する
            foreach (systemDS v in systemList)
            {
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
            if (m_systemCombo.SelectedValue != null)
            
                m_systemno.Text = m_systemCombo.SelectedValue.ToString();


        }
        //システム名のコンボボックスが変更されたとき拠点情報を読み込む
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

                try
                {
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
                catch (Exception ex)
                {
                    MessageBox.Show("拠点情報取得に失敗しました。  " + ex.Message, "ホスト登録", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return;
        }
        //ホスト名を読み込む
        private void Read_hostCombo(string siteno)
        {
            m_hostCombo.DataSource = null;
            m_hostno.Text = "";

            Class_Detaget getuser = new Class_Detaget();

            //ホスト名を検索
            List<hostDS> hostDSList = getuser.getHostList(siteno, con,true);
            m_hostCombo.DataSource = hostDSList;
            m_hostCombo.DisplayMember = "hostname";
            m_hostCombo.ValueMember = "host_no";
            //ホスト名ラベルを表示
            if (hostDSList.Count > 0)               
                m_hostno.Text = m_hostCombo.SelectedValue.ToString();
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

            //拠点
            if (m_siteCombo.Text == "")
            {
                MessageBox.Show("拠点名が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //ホスト名
            if (m_hostCombo.Text == "")
            {
                MessageBox.Show("ホスト名が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //確認
            MessageBox.Show("監視インターフェイス情報を登録します。よろしいですか？", "登録確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            string userno = m_userno.Text;
            string systemno = m_systemno.Text;
            string siteno = m_siteno.Text;
            string hostno = m_hostno.Text;

            //ステータス
            string status = "";
            if (m_statusCombo.SelectedIndex == 0)
                //有効
                status = "1";

            else
                //無効
                status = "0";

            string interfacename = m_interfacename.Text;
            string type = m_type.Text;
            string kanshi = m_koumoku.Text;
            DateTime startdate = m_kansiStartdate.Value;
            DateTime enddate = m_kansiEnddate.Value;
            string border = m_border.Text;
            string ipaddress = m_ipaddress.Text;
            string ipaddressNAT = m_ipaddressNAT.Text;
            
            //DB接続

            NpgsqlCommand cmd;
            try
            {
                if (con.FullState != ConnectionState.Open) con.Open();

                Int32 rowsaffected;

                //データ登録
                cmd = new NpgsqlCommand(@"insert into watch_Interface(interfacename,status,type,kanshi,start_date,end_date,border,IPaddress,IPaddressNAT,host_no,userno,systemno,siteno,chk_name_id) 
                    values ( :interfacename,:status,:type,:kanshi,:start_date,:end_date,:border,:IPaddress,:IPaddressNAT,:host_no,:userno,:systemno,:siteno,:chk_name_id)", con);
                cmd.Parameters.Add(new NpgsqlParameter("interfacename", DbType.String) { Value = interfacename });
                cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                cmd.Parameters.Add(new NpgsqlParameter("type", DbType.String) { Value = type });
                cmd.Parameters.Add(new NpgsqlParameter("kanshi", DbType.String) { Value = kanshi });
                cmd.Parameters.Add(new NpgsqlParameter("start_date", DbType.DateTime) { Value = startdate });
                cmd.Parameters.Add(new NpgsqlParameter("end_date", DbType.DateTime) { Value = enddate });
                cmd.Parameters.Add(new NpgsqlParameter("border", DbType.String) { Value = border });
                cmd.Parameters.Add(new NpgsqlParameter("IPaddress", DbType.String) { Value = ipaddress });
                cmd.Parameters.Add(new NpgsqlParameter("IPaddressNAT", DbType.String) { Value = ipaddressNAT });
                cmd.Parameters.Add(new NpgsqlParameter("host_no", DbType.Int32) { Value = hostno });
                cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = userno });
                cmd.Parameters.Add(new NpgsqlParameter("host_no", DbType.Int32) { Value = hostno });
                cmd.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = systemno });
                cmd.Parameters.Add(new NpgsqlParameter("siteno", DbType.Int32) { Value = siteno });
                cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = m_idlabel.Text });
                rowsaffected = cmd.ExecuteNonQuery();

                if (rowsaffected != 1)
                {
                    MessageBox.Show("登録できませんでした。", "監視インターフェイス登録");
                }
                else
                {
                    //登録成功
                    MessageBox.Show("登録完了", "監視インターフェイス登録");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("監視インターフェイス5登録時エラー " + ex.Message);
                return;
            }

        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //カスタマ名の値が変更されたとき
        private void m_userCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Read_systemCombo();
            Read_siteCombo();
            if(m_siteno.Text != null && m_siteno.Text != "" )
                Read_hostCombo(m_siteno.Text);
            else
            {
                m_hostCombo.DataSource = null;
                m_hostno.Text = "";
            }   
        }
        //システム名の値が変更されたとき
        private void m_systemCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if(m_systemCombo.SelectedValue != null)
                m_systemno.Text = m_systemCombo.SelectedValue.ToString();
            Read_siteCombo();
            if (m_siteno.Text != null && m_siteno.Text != "")
                Read_hostCombo(m_siteno.Text);

        }
        //拠点名の値が変更されたとき
        private void m_siteCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (m_siteCombo.SelectedValue != null)
                m_siteno.Text = m_siteCombo.SelectedValue.ToString();
            if (m_siteno.Text != null && m_siteno.Text != "")
                Read_hostCombo(m_siteno.Text);

        }
        //ホスト名コンボボックスの値が変更されたとき
        private void m_hostCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (m_hostCombo.SelectedValue != null)
                m_hostno.Text = m_hostCombo.SelectedValue.ToString();
        }
    }
}
