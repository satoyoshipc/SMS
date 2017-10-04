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
    public partial class Form_UserInsert : Form
    {
        //DBコネクション
        public NpgsqlConnection con { get; set; }
        public opeDS loginDS { get; set; }

        //担当者登録時に使用する
        public List<userDS> userList { get; set; }

        
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

            //カスタマ名
            if (m_username.Text == ""){
                MessageBox.Show("カスタマ名が入力されていません。", "",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            //カスタマ名カナ
            if (m_userkana.Text == "")
            {
                MessageBox.Show("カスタマ名カナが入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //カスタマ名略称
            if (m_userryaku.Text == "")
            {
                MessageBox.Show("カスタマ名略が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //確認
            if (MessageBox.Show("カスタマ情報を登録します。よろしいですか？", "登録確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return; ;

            //カスタマID
            string customerID = m_customerID.Text;

            //カスタマ名
            string username = m_username.Text;
            //カスタマ名カナ
            string userkana = m_userkana.Text;
            //カスタマ名略
            string userryaku = m_userryaku.Text;
            //SLO対象
            string reportchk = "0";
            if (m_reportchk.Checked)
                //1有効
                reportchk = "1";
            else
                //0無効
                reportchk = "0";

            //ステータス
            string status = "0";
            if(m_status.Checked)
                //1有効
                status = "1";
            else
                //0無効
                status = "0";

            string biko = m_biko.Text;

            //int OpeNo = ;
            string OpeNo = m_dispOpeNo.Text;
            NpgsqlCommand cmd;

            //DB接続
            try {
                if (con.FullState != ConnectionState.Open) con.Open();
                Int32 rowsaffected;
                //データ登録
                cmd = new NpgsqlCommand(@"insert into user_tbl(customerID,username, username_kana,username_sum,status,report_status,biko,chk_name_id) 
                        values ( :customerID,:username,:username_kana,:username_sum,:status,:report_status,:biko,:chk_name_id)", con);
                cmd.Parameters.Add(new NpgsqlParameter("customerID", DbType.String) { Value = customerID });
                cmd.Parameters.Add(new NpgsqlParameter("username", DbType.String) { Value = username });
                cmd.Parameters.Add(new NpgsqlParameter("username_kana", DbType.String) { Value = userkana });
                cmd.Parameters.Add(new NpgsqlParameter("username_sum", DbType.String) { Value = userryaku });
                cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                cmd.Parameters.Add(new NpgsqlParameter("report_status", DbType.String) { Value = reportchk });
                cmd.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = biko });
                cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = OpeNo });
                rowsaffected = cmd.ExecuteNonQuery();
                
                if(rowsaffected != 1)
                {
                    MessageBox.Show("登録できませんでした。", "カスタマ登録");
                }
                else
                {
                    //登録成功
                    MessageBox.Show("登録完了", "カスタマ登録");
                    
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("登録時エラー " + ex.Message);
                return;
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
            //担当者登録
            Form_UserTantouInsert Usertantoufm = new Form_UserTantouInsert();
            Usertantoufm.loginDS = loginDS;
            Usertantoufm.customername = m_username.Text;
            Usertantoufm.con = con;

            Usertantoufm.Show();
        }
        //表示前処理
        private void Form_UserInsert_Load(object sender, EventArgs e)
        {

            m_dispOpeNo.Text = loginDS.opeid;
            m_dispOpename.Text = loginDS.lastname + loginDS.fastname;


        }
    }
}
