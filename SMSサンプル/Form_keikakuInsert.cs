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
    public partial class Form_sagyoInsert : Form
    {
        //DBコネクション
        public NpgsqlConnection con { get; set; }
        public opeDS loginDS { get; set; }


        //カスタマ
        public List<userDS> userList { get; set; }

        public Form_sagyoInsert()
        {
            InitializeComponent();
        }
        //
        private void Form_sagyoInsert_Load(object sender, EventArgs e)
        {
            m_idlabel.Text = loginDS.opeid;
            m_labelinputOpe.Text = loginDS.lastname + loginDS.fastname;

            //コンボボックス
            DataTable cutomerTable = new DataTable();
            cutomerTable.Columns.Add("ID", typeof(string));
            cutomerTable.Columns.Add("NAME", typeof(string));
            //カスタマ情報を取得する
            if (userList == null)
                return;
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

            //カスタマ名ラベルを表示
            if (userList != null)
                m_userno.Text = m_usernameCombo.SelectedValue.ToString();
            
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
            //作業内容
            if (m_naiyou.Text == "")
            {
                MessageBox.Show("作業内容が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //確認
            if(MessageBox.Show("作業情報を登録します。よろしいですか？", "登録確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            Int32 userno = (m_userno.Text == "") ? 0 : int.Parse(m_userno.Text);
            string naiyou = m_naiyou.Text;
            DateTime startdate = m_start_date.Value;
            DateTime enddate = m_end_date.Value;


            //DB接続

            NpgsqlCommand cmd;
            try
            {
                if (con.FullState != ConnectionState.Open) con.Open();
                Int32 rowsaffected;
                //データ登録
                cmd = new NpgsqlCommand(@"insert into task_info(text,start_date,end_date,userno,chk_name_id)
                                        values (:text,:start_date,:end_date,:userno,:chk_name_id)", con);
                cmd.Parameters.Add(new NpgsqlParameter("text", DbType.String) { Value = naiyou });
                cmd.Parameters.Add(new NpgsqlParameter("start_date", DbType.DateTime) { Value = startdate });
                cmd.Parameters.Add(new NpgsqlParameter("end_date", DbType.DateTime) { Value = enddate });
                cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = userno });
                cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = m_idlabel.Text });

                rowsaffected = cmd.ExecuteNonQuery();

                if (rowsaffected != 1)
                {
                    MessageBox.Show("登録できませんでした。", "作業情報登録");
                }
                else
                {
                    //登録成功
                    MessageBox.Show("登録完了", "作業情報登録");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("作業情報登録時エラー " + ex.Message);
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
            //カスタマ名ラベルを表示
            if (userList != null)
                m_userno.Text = m_usernameCombo.SelectedValue.ToString();
        }
    }
}
