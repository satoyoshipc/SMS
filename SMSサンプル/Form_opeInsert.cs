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
        //DBコネクション
        public NpgsqlConnection con { get; set; }
        //ログイン情報
        public opeDS loginDS { get; set; }

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
        //表示前処理
        private void Form_opeInsert_Load(object sender, EventArgs e)
        {
            m_idlabel.Text =     loginDS.opeid;
            m_labelinputOpe.Text = loginDS.lastname + loginDS.fastname;
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
                return;
            

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
            try
            {

                if (con.FullState != ConnectionState.Open) con.Open();
                Int32 rowsaffected;

                //データ登録
                cmd = new NpgsqlCommand(@"insert into ope(opeid,lastname,fastname,password,type,biko,chk_name_id) values 
                    ( :opeid,:lastname,:fastname,:password,:type,:biko,:chk_name_id);" +
                    "select currval('ope_openo_seq') ;", con);
                cmd.Parameters.Add(new NpgsqlParameter("opeid", DbType.String) { Value = opeid });
                cmd.Parameters.Add(new NpgsqlParameter("lastname", DbType.String) { Value = lastname });
                cmd.Parameters.Add(new NpgsqlParameter("fastname", DbType.String) { Value = fastname });
                cmd.Parameters.Add(new NpgsqlParameter("password", DbType.String) { Value = password });
                cmd.Parameters.Add(new NpgsqlParameter("type", DbType.String) { Value = type });
                cmd.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = biko });
                cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = m_idlabel.Text });
                //OUTパラメータをセットする
                NpgsqlParameter firstColumn = new NpgsqlParameter("firstcolumn", DbType.Int32);
                firstColumn.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(firstColumn);



                rowsaffected = cmd.ExecuteNonQuery();

                //値の取得
                int currval = int.Parse(firstColumn.NpgsqlValue.ToString());

                if (rowsaffected != 1)
                {
                    MessageBox.Show("登録できませんでした。", "オペレータ登録");
                }
                else
                {
                    //登録成功 YESを選択でメールアドレス登録画面に遷移
                    if (MessageBox.Show("オペレータの登録完了しました。" + Environment.NewLine + " 続いてメールアドレスを登録しますか？", "オペレータ登録", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Form_addressInsert addfm = new Form_addressInsert();
                        addfm.loginDS = loginDS;
                        addfm.con = con;
                        //オペレータなので"1"
                        addfm.kbn = 1;
                        addfm.userid = currval.ToString();
                        addfm.username = m_opeID.Text;

                        addfm.Show();

                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("オペレータの登録時にエラーが発生しました。　" + ex.Message, "オペレータ登録", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
            NpgsqlCommand cmd;

            try
            {
                if (con.FullState != ConnectionState.Open) con.Open();

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


            }
            catch (Exception ex)
            {
                MessageBox.Show("データ取得エラー", "重複チェック時に失敗しました。" + ex.Message);
            }

        }
    }
}
