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
    public partial class Form_siteInsert : Form
    {

        public opeDS loginDS { get; set; }
        //DBコネクション
        public NpgsqlConnection con { get; set; }
        //カスタマ
        public List<userDS> userList { get; set; }

        //システム
        public List<systemDS> systemList { get; set; }

        public Form_siteInsert()
        {
            InitializeComponent();
        }


        //表示前処理
        private void Form_siteInsert_Load(object sender, EventArgs e)
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
        }

        //キャンセルボタン
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //システム名
        private void m_systemCombo_SelectedIndexChanged(object sender, EventArgs e)
        {


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

            if (m_sitename.Text == "")
            {
                MessageBox.Show("拠点名が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //確認
            if (MessageBox.Show("拠点情報を登録します。よろしいですか？", "登録確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string userno = m_userno.Text;
            string systemno = m_systemno.Text;
            string sitename = m_sitename.Text;
            string address1 = m_address1.Text;
            string address2 = m_address2.Text;
            string tel = m_tel.Text;

            //ステータス
            string status = "";
            if (m_statusCombo.SelectedIndex == 0 )
                //有効
                status = "1";

            else
                //無効
                status = "0";

            string biko = m_biko.Text;

            //DB接続
            Class_common common = new Class_common();
            NpgsqlCommand cmd;
            using (var con = common.DB_connection())
            {
                try
                {
                    if (con.FullState != ConnectionState.Open) con.Open();
                    Int32 rowsaffected;
                    //データ登録
                    cmd = new NpgsqlCommand(@"insert into site(userno,systemno,sitename,address1,address2,telno,status,biko,chk_name_id) 
                        values ( :userno,:systemno,:sitename,:address1,:address2,:telno,:status,:biko,:chk_name_id)", con);
                    cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = userno });
                    cmd.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = systemno });
                    cmd.Parameters.Add(new NpgsqlParameter("sitename", DbType.String) { Value = sitename });
                    cmd.Parameters.Add(new NpgsqlParameter("address1", DbType.String) { Value = address1 });
                    cmd.Parameters.Add(new NpgsqlParameter("address2", DbType.String) { Value = address2 });
                    cmd.Parameters.Add(new NpgsqlParameter("telno", DbType.String) { Value = tel });
                    cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                    cmd.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = biko });
                    cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = m_idlabel.Text });
                    rowsaffected = cmd.ExecuteNonQuery();

                    if (rowsaffected != 1)
                    {
                        MessageBox.Show("登録できませんでした。", "拠点登録");
                    }
                    else
                    {
                        //登録成功
                        MessageBox.Show("登録完了", "拠点登録");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("拠点登録時エラー " + ex.Message);
                    return;
                }

            }
        }
        //カスタマコンボボックスが変更されたときに発生
        private void m_usernameCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Read_systemCombo();


        }

        //システムコンボボックスが変更されたときに発生
        private void m_systemCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            m_systemno.Text = m_systemCombo.SelectedValue.ToString();
        }
        private void Read_systemCombo()
        {
            m_systemno.Text = "";

            m_systemCombo.DataSource = null;
            if (m_usernameCombo.SelectedValue != null)
                m_userno.Text = m_usernameCombo.SelectedValue.ToString();
            //コンボボックス
            DataTable systemTable = new DataTable();
            systemTable.Columns.Add("ID", typeof(string));
            systemTable.Columns.Add("NAME", typeof(string));


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
            if(m_systemCombo.SelectedValue != null)
                m_systemno.Text = m_systemCombo.SelectedValue.ToString();


        }


    }
}
