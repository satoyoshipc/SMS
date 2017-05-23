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
    public partial class Form_addressInsert : Form
    {
        //DBコネクション
        public NpgsqlConnection con { get; set; }
        public opeDS loginDS { get; set; }

        //1:オペレータ 2:カスタマ担当者
        public int kbn { get; set; }

        public string userid { get; set; }

        public string username { get; set; }



        public Form_addressInsert()
        {
            InitializeComponent();
        }
        //表示前処理
        private void Form_addressInsert_Load(object sender, EventArgs e)
        {
            m_idlabel.Text = loginDS.opeid;
            m_labelinputOpe.Text = loginDS.lastname + loginDS.fastname;

            //区分
            //1:オペレータ 2:カスタマ担当者
            if (kbn == 1)
            {
                m_Customerkbn_combo.SelectedIndex = 0;

            }
            else if(kbn == 2)
            {

                m_Customerkbn_combo.SelectedIndex = 1;

            }
            //
            m_opeID.Text = userid;
            m_opename.Text = username;

        }
        //登録ボタン
        private void button3_Click(object sender, EventArgs e)
        {

            if (m_Customerkbn_combo.Text == "")
            {
                MessageBox.Show("区分を入力して下さい。", "メールアドレス登録", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (m_opeID.Text == "")
            {
                MessageBox.Show("ユーザIDを入力して下さい。", "メールアドレス登録", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (m_addressno.Text == "")
            {
                MessageBox.Show("アドレス番号を入力して下さい。", "メールアドレス登録", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (m_address.Text == "")
            {
                MessageBox.Show("メールアドレスを入力して下さい。", "メールアドレス登録", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //確認
            if (MessageBox.Show("メールアドレスを登録します。よろしいですか?", "メールアドレス登録", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            if(m_Customerkbn_combo.SelectedIndex == 0)
            {

            }
            NpgsqlCommand cmd;

            try
            {
                if (con.FullState != ConnectionState.Open) con.Open();
                Int32 rowsaffected;
                //データ登録
                cmd = new NpgsqlCommand(@"insert into mailaddress(kubun,opetantouno,addressno,mailaddress,addressname,chk_name_id) 
                        values ( :kubun,:opetantouno,:addressno,:mailaddress,:addressname,:chk_name_id)" , con);
                cmd.Parameters.Add(new NpgsqlParameter("kubun", DbType.String) { Value = m_Customerkbn_combo.SelectedIndex });
                cmd.Parameters.Add(new NpgsqlParameter("opetantouno", DbType.Int32) { Value = m_opeID.Text });
                cmd.Parameters.Add(new NpgsqlParameter("addressno", DbType.Int32) { Value = m_addressno.Text });
                cmd.Parameters.Add(new NpgsqlParameter("mailaddress", DbType.String) { Value = m_address.Text });
                cmd.Parameters.Add(new NpgsqlParameter("addressname", DbType.String) { Value = m_addressname.Text });
                cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = m_idlabel.Text });
                
                rowsaffected = cmd.ExecuteNonQuery();


                if (rowsaffected != 1)
                {
                    MessageBox.Show("登録できませんでした。", "メールアドレス登録");
                }
                else
                {
                    //登録成功 YESを選択でメールアドレス登録画面に遷移
                    MessageBox.Show("登録が完了しました。", "メールアドレス登録", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("メールアドレス登録時にエラーが発生しました。　" + ex.Message, "メールアドレス登録", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    
        //キャンセルボタン
        private void button2_Click(object sender, EventArgs e)
        {

            this.Close();
        }


    }
}
