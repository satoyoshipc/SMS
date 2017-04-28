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

    public partial class Form_systemInsert : Form
    {

        //ユーザ
        public List<userDS> userList { get; set; }


        public Form_systemInsert()
        {
            InitializeComponent();
        }

        //表示前処理
        private void Form_systemInsert_Load(object sender, EventArgs e)
        {
            DataTable cutomerTable = new DataTable();
            cutomerTable.Columns.Add("ID", typeof(string));
            cutomerTable.Columns.Add("NAME", typeof(string));

            if (userList == null)
                return;
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

        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //選択の変更
        private void m_usernameCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            try { 
                m_userID.Text = m_usernameCombo.SelectedValue.ToString();
            }
            catch (Exception ex )
            {
                MessageBox.Show("ユーザ情報の取得に失敗しました。" + ex.Message);
            }

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
            //システム名
            if (m_systemname.Text == "")
            {
                MessageBox.Show("システム名が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //確認
            MessageBox.Show("システム情報を登録します。よろしいですか？", "登録確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            string systemname = m_systemname.Text;
            string systemkana = m_systemnamekana.Text;
            string biko = m_biko.Text;
            string userno = m_userID.Text;
            
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
                    cmd = new NpgsqlCommand(@"insert into system(systemname,systemkana,biko,userno,chk_name_id) 
                        values ( :systemname,:systemkana,:biko,:userno,:chk_name_id)", con);
                    cmd.Parameters.Add(new NpgsqlParameter("systemname", DbType.String) { Value = systemname });
                    cmd.Parameters.Add(new NpgsqlParameter("systemkana", DbType.String) { Value = systemkana });
                    cmd.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = biko });
                    cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = userno });
                    cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = "111" });
                    rowsaffected = cmd.ExecuteNonQuery();

                    if (rowsaffected != 1)
                    {
                        MessageBox.Show("登録できませんでした。", "システム登録");
                        con.Close();
                    }
                    else
                    {
                        //登録成功
                        MessageBox.Show("登録完了", "システム登録");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("システム登録時エラー " + ex.Message);
                    con.Close();
                    return;
                }

            }
        }
    }
}
