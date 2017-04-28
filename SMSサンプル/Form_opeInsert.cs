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
    public partial class Form_opeInsert : Form
    {
        public Form_opeInsert()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //ファイル選択ダイアログの表示
            string str = "";
            Class_common common = new Class_common();
            str = common.Disp_FileSelectDlg();
            if (str != "")
            {


            }
        }

        private void Form_opeInsert_Load(object sender, EventArgs e)
        {

        }
        //登録ボタン
        private void button3_Click(object sender, EventArgs e)
        {
            //validation
            if(m_opeID.Text == "")
            {
                MessageBox.Show("オペレータIDを入力してください。");
                m_opeID.Focus();
                return;
            }
            if (m_lastname.Text == "")
            {
                MessageBox.Show("オペレータ名(姓)を入力してください。");
                m_lastname.Focus();
                return;
            }
            if (m_pass.Text == "")
            {
                MessageBox.Show("パスワードを入力してください。");
                m_pass.Focus();
                return;
            }


            //確認
            if (MessageBox.Show("オペレータ情報を登録します。よろしいですか？", "登録確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            string opeid = m_opeID.Text;
            string lastname = m_lastname.Text;
            string fastname = m_fastname.Text;
            string password = m_pass.Text;
            string type = "";
            NpgsqlCommand cmd;

            //権限チェックボックス
            if (m_kanriRadio.Checked)
                type = "1";
            else if (m_riyouRadio.Checked)
                type = "2";

            string biko = m_biko.Text;

            Class_common common = new Class_common();
            using (var con = common.DB_connection())
            {
                con.Open();

                //データ登録
                cmd = new NpgsqlCommand(@"insert into ope(opeid,lastname,fastname,password,type,biko,chk_name_id) values 
                    ( :opeid,:lastname,:fastname,:password,:type,:biko,:chk_name_id)", con);
                cmd.Parameters.Add(new NpgsqlParameter("opeid", DbType.String) { Value = opeid });
                cmd.Parameters.Add(new NpgsqlParameter("lastname", DbType.String) { Value = lastname });
                cmd.Parameters.Add(new NpgsqlParameter("fastname", DbType.String) { Value = fastname });
                cmd.Parameters.Add(new NpgsqlParameter("password", DbType.String) { Value = password });
                cmd.Parameters.Add(new NpgsqlParameter("type", DbType.String) { Value = type });
                cmd.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = biko });
                cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = "111" });
                cmd.ExecuteNonQuery();


                MessageBox.Show("オペレータの登録完了しました。","オペレータ登録");
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form_addressInsert addressfm = new Form_addressInsert();
            addressfm.Show();
        }

        private void m_opeID_TextChanged(object sender, EventArgs e)
        {
             
        }
        //重複チェックを行う
        private void button5_Click(object sender, EventArgs e)
        {

            if (m_opeID.Text == "")
            {
                MessageBox.Show("オペレータIDを入力してください。","重複チェック",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            Class_common common = new Class_common();
            NpgsqlCommand cmd;

            using (var con = common.DB_connection())
            {
                try
                {
                    con.Open();

                    //データ検索
                    cmd = new NpgsqlCommand(@"select count(*) from ope where opeid= :operid", con);
                    cmd.Parameters.Add(new NpgsqlParameter("operid", DbType.String) { Value = m_opeID.Text });

                    //int count = cmd.ExecuteScalar();
                    int count = int.Parse(cmd.ExecuteScalar().ToString());
                    if (count > 0)
                    {
                        m_duplicationResult.ForeColor = Color.Red;
                        m_duplicationResult.Text = "そのIDは既に登録されています。";
                    }
                    else if (count == 0)
                    {
                        m_duplicationResult.ForeColor = Color.Green;

                        m_duplicationResult.Text = "登録可能です。";
                    }

                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("データ取得エラー", "重複チェック時に失敗しました。" + ex.Message);
                    con.Close();
                }

            }
        }
    }
}
