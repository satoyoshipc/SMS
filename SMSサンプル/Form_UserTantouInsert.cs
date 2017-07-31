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
    public partial class Form_UserTantouInsert : Form
    {
        //DBコネクション
        public NpgsqlConnection con { get; set; }
        public opeDS loginDS { get; set; }

        //カスタマ
        public List<userDS> userList { get; set; }

        public Form_UserTantouInsert()
        {
            InitializeComponent();
        }

        //メールアドレス登録
        private void button4_Click(object sender, EventArgs e)
        {
            Form_addressInsert addfm = new Form_addressInsert();
            addfm.loginDS = loginDS;
            addfm.con = con;
            //カスタマ担当者なので"2"
            addfm.kbn = 2;


            addfm.Show();

        }
        //担当者名
        private void Form_UserTantouInsert_Load(object sender, EventArgs e)
        {
            m_idlabel.Text = loginDS.opeid;
            m_labelinputOpe.Text = loginDS.lastname + loginDS.fastname;

            DataTable cutomerTable = new DataTable();
            cutomerTable.Columns.Add("ID", typeof(string));
            cutomerTable.Columns.Add("NAME", typeof(string));

            Class_Detaget getuser = new Class_Detaget();
            getuser.con = con;
            userList = getuser.getUserList();
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

            //選択されているカスタマのNOを表示する
            m_userno.Text = m_usernameCombo.SelectedValue.ToString();
        }

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
        //キャンセルボタン
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //登録ボタン
        private void button3_Click(object sender, EventArgs e)
        {
            //担当者名は必須
            if (m_tantou_name.Text == "")
            {
                MessageBox.Show("担当者名を入力して下さい。", "カスタマ担当者登録", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //確認
            if (MessageBox.Show("カスタマ担当者を登録します。よろしいですか?", "カスタマ担当者登録", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            
            NpgsqlCommand cmd;

            //ステータス
            string status = "0";
            if (m_yukouRadio.Checked)
                //1有効
                status = "1";
            else
                //0無効
                status = "0";

            try
            {
                if (con.FullState != ConnectionState.Open) con.Open();
                Int32 rowsaffected;
                //データ登録
                cmd = new NpgsqlCommand(@"insert into user_tanntou(user_tantou_name,user_tantou_name_kana,busho_name,telno1,telno2,yakusyoku,status,biko,userno,chk_name_id) 
                        values ( :user_tantou_name,:user_tantou_name_kana,:busho_name,:telno1,:telno2,:yakusyoku,:status,:biko,:userno,:chk_name_id);" +
                        "select currval('user_tanntou_user_tantou_no_seq') ;", con);
                cmd.Parameters.Add(new NpgsqlParameter("user_tantou_name", DbType.String) { Value = m_tantou_name.Text });
                cmd.Parameters.Add(new NpgsqlParameter("user_tantou_name_kana", DbType.String) { Value = m_tantoukana.Text });
                cmd.Parameters.Add(new NpgsqlParameter("busho_name", DbType.String) { Value = m_busyo.Text });
                cmd.Parameters.Add(new NpgsqlParameter("telno1", DbType.String) { Value = m_tel1.Text });
                cmd.Parameters.Add(new NpgsqlParameter("telno2", DbType.String) { Value = m_tel2.Text });
                cmd.Parameters.Add(new NpgsqlParameter("yakusyoku", DbType.String) { Value = m_yakusyoku.Text });
                cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                cmd.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = m_biko.Text });
                cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = m_userno.Text });
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
                    MessageBox.Show("登録できませんでした。", "カスタマ担当者登録");
                }
                else
                {
                    //登録成功 YESを選択でメールアドレス登録画面に遷移
                    if(MessageBox.Show("登録が完了しました。"+ Environment.NewLine + " 続いてメールアドレスを登録しますか？", "カスタマ担当者登録",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Form_addressInsert addfm = new Form_addressInsert();
                        addfm.loginDS = loginDS;
                        addfm.con = con;
                        //カスタマ担当者なので"2"
                        addfm.kbn = 2;
                        addfm.userid = currval.ToString();
                        addfm.username = m_tantou_name.Text;

                        addfm.Show();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("カスタマ担当者の登録時にエラーが発生しました。　" + ex.Message, "担当者登録", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
