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
    public partial class Form_UserInsert : Form
    {
        public Form_UserInsert()
        {
            InitializeComponent();
        }

        //キャンセルボタン
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //登録ボタン
        private void button3_Click(object sender, EventArgs e)
        {

            //ユーザ名
            if(m_username.Text == ""){
                MessageBox.Show("ユーザ名が入力されていません。","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            //ユーザ名カナ
            if (m_userkana.Text == "")
            {
                MessageBox.Show("ユーザ名カナが入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //ユーザ名略称
            if (m_userryaku.Text == "")
            {
                MessageBox.Show("ユーザ名略が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //確認
            MessageBox.Show("ユーザ情報を登録します。よろしいですか？", "登録確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            //ユーザ名
            string username = m_username.Text;
            //ユーザ名カナ
            string userkana = m_userkana.Text;
            //ユーザ名略
            string userryaku = m_userryaku.Text;
            //レポート出力有無
            string reportchk = "0";
            if (m_reportchk.Checked)
                //1無効
                reportchk = "1";
            else
                //0無効
                reportchk = "0";

            //ステータス
            string status = "0";
            if(m_status.Checked)
                //1無効
                status = "1";
            else
                //0無効
                status = "0";


            string biko = m_biko.Text;

            //int OpeNo = ;
            string OpeNo = m_dispOpeNo.Text;
            NpgsqlCommand cmd;

            //DB接続
            Class_common common = new Class_common();
            using (var con = common.DB_connection())
            {
                try { 
                con.Open();
                Int32 rowsaffected;
                //データ登録
                cmd = new NpgsqlCommand(@"insert into user_tbl(username, username_kana,username_sum,status,report_status,biko,chk_name_id) 
                        values ( :username,:username_kana,:username_sum,:status,:report_status,:biko,:chk_name_id)", con);
                cmd.Parameters.Add(new NpgsqlParameter("username", DbType.String) { Value = username });
                cmd.Parameters.Add(new NpgsqlParameter("username_kana", DbType.String) { Value = userkana });
                cmd.Parameters.Add(new NpgsqlParameter("username_sum", DbType.String) { Value = userryaku });
                cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                cmd.Parameters.Add(new NpgsqlParameter("report_status", DbType.String) { Value = reportchk });
                cmd.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = biko });
                cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = "111" });
                rowsaffected = cmd.ExecuteNonQuery();
                
                    if(rowsaffected != 1)
                    {
                        MessageBox.Show("登録できませんでした。","ユーザ登録");
                        con.Close();
                    }
                    else
                    {
                        //登録成功
                        MessageBox.Show("登録完了", "ユーザ登録");
                        
                    }

                }
                catch(Exception ex)
                {
                    MessageBox.Show("登録時エラー " + ex.Message);
                    con.Close();
                    return;
                }
                
            }
            
        }

        //CSV登録
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
        //担当者登録
        private void button4_Click(object sender, EventArgs e)
        {
            Form_UserTantouInsert Usertantoufm = new Form_UserTantouInsert();
            Usertantoufm.Show();
        }
    }
}
