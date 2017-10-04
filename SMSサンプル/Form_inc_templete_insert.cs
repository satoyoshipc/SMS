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
    public partial class Form_inc_templete_insert : Form
    {
        public NpgsqlConnection con { get; set; }

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public opeDS loginDS { get; set; }
        //カスタマ
        public List<userDS> userList { get; set; }

        public Form_inc_templete_insert()
        {
            InitializeComponent();
        }

        //表示前
        private void Form_inc_templete_insert_Load(object sender, EventArgs e)
        {
            m_idlabel.Text = loginDS.opeid;
            m_labelinputOpe.Text = loginDS.lastname + loginDS.fastname;

            //計画作業
            //インシデント
            if (m_templete_type_combo.Text == "タスク(インシデントタスク・計画作業)")
                m_title.Enabled = false;
            else if (m_templete_type_combo.Text == "インシデント")
                m_title.Enabled = true;

            DataTable cutomerTable = new DataTable();
            cutomerTable.Columns.Add("ID", typeof(string));
            cutomerTable.Columns.Add("NAME", typeof(string));

            if (userList == null)
                return;

            //空行の挿入
            DataRow row = cutomerTable.NewRow();
            row["ID"] = "0";
            row["NAME"] = "全てのカスタマ";
            cutomerTable.Rows.Add(row);

            //カスタマ情報を取得する
            foreach (userDS v in userList)
            {
                row = cutomerTable.NewRow();
                row["ID"] = v.userno;
                row["NAME"] = v.username;
                cutomerTable.Rows.Add(row);

            }
            //データテーブルを割り当てる
            m_usernameCombo.DataSource = cutomerTable;
            m_usernameCombo.DisplayMember = "NAME";
            m_usernameCombo.ValueMember = "ID";
            if (cutomerTable.Rows.Count > 0)
                m_userno.Text = m_usernameCombo.SelectedValue.ToString();

        }

        //テンプレート種別コンボボックス
        private void m_templete_type_combo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //計画作業
            //インシデント
            if (m_templete_type_combo.Text == "タスク(インシデントタスク・計画作業)")
                m_title.Enabled = false;
            else if (m_templete_type_combo.Text == "インシデント")
                m_title.Enabled = true;

        }
        //登録
        private void button3_Click(object sender, EventArgs e)
        {
            //種別
            if (m_templete_type_combo.Text == "")
            {
                MessageBox.Show("テンプレート種別が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //カスタマ名の入力チェック
            if (m_usernameCombo.Text == "")
            {
                MessageBox.Show("カスタマ名が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //テンプレート名の入力チェック
            if (m_templete_name.Text == "")
            {
                MessageBox.Show("テンプレート名が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //確認
            string str = m_templete_type_combo.Text;
            if (MessageBox.Show(str + " のテンプレート情報を登録します。よろしいですか？", "登録確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string templete = m_templete_name.Text;
            string customerno = m_userno.Text;

            string title = m_title.Text;
            string contents = m_content.Text;

            string templetename = m_templete_name.Text;


            string templetetype = "";

            if (str == "インシデント")
            {
                templetetype = "1"; 
            }

            else if (str == "タスク(インシデントタスク・計画作業)")
            {
                templetetype = "2";
            }

            //DB接続
            NpgsqlCommand cmd;
            try
            {
                if (con.FullState != ConnectionState.Open) con.Open();
                Int32 rowsaffected;
                //データ登録
                cmd = new NpgsqlCommand(@"insert into templete(templetetype,templetename,title,text,userno,chk_name_id) 
                    values ( :templetetype,:templetename,:title,:text,:userno,:chk_name_id)", con);
                cmd.Parameters.Add(new NpgsqlParameter("templetetype", DbType.String) { Value = templetetype });
                cmd.Parameters.Add(new NpgsqlParameter("templetename", DbType.String) { Value = templetename });
                cmd.Parameters.Add(new NpgsqlParameter("title", DbType.String) { Value = title });
                cmd.Parameters.Add(new NpgsqlParameter("text", DbType.String) { Value = contents });
                cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = customerno });
                cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = m_labelinputOpe.Text });
                rowsaffected = cmd.ExecuteNonQuery();

                if (rowsaffected != 1)
                {
                    MessageBox.Show("登録できませんでした。", "テンプレート登録");
                }
                else
                {
                    //登録成功
                    MessageBox.Show("登録完了", "テンプレート登録");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("テンプレート登録時エラー " + ex.Message);
                return;
            }

        }

        //キャンセルボタン
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //カスタマコンボボックスが変更されたとき
        private void m_usernameCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                m_userno.Text = m_usernameCombo.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("カスタマ情報の取得に失敗しました。" + ex.Message);
            }

        }

    }
}
