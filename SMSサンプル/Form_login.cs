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
using log4net;

namespace SMSサンプル
{
    public partial class Form_login : Form
    {
        // log 
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //リターンコード
        public int ret_value { get; set; }
        public opeDS opeData { get; set; }

        public Form_login()
        {
            InitializeComponent();
        }


        //キャンセルボタン
        private void button2_Click(object sender, EventArgs e)
        {
            ret_value = -1;
            this.Close();
        }

        //OKボタン
        private void button1_Click(object sender, EventArgs e)
        {

            login_process();

        }

        //ログインのプロセス
        void login_process()
        {
            if (m_opeid.Text == "")
            {
                MessageBox.Show("オペレータ名を入力してください。", "ログイン", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (m_pass.Text == "")
            {
                MessageBox.Show("パスワードを入力してください。", "ログイン", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            //DBに接続
            Class_common common = new Class_common();
            NpgsqlConnection con = common.DB_connection();
            int ret = getOpeAuth(m_opeid.Text, m_pass.Text, con);

            if (ret < 0)
            {
                //ログイン失敗
                ret_value = -1;
                con.Close();
            }
            else {
                //ログイン成功
                ret_value = 1;
                con.Close();
                this.Close();
            }


        }
        //認証 正常:0  異常:-1
        public int getOpeAuth(String userid, string pass, NpgsqlConnection conn)
        {

            NpgsqlCommand cmd;
            try
            {
                int ret = 0;
                if (conn.FullState != ConnectionState.Open) conn.Open();

                String sql = @"SELECT openo,opeid,lastname,fastname,type FROM ope WHERE opeid = :opeid AND password=:pass ";

                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.Add(new NpgsqlParameter("opeid", DbType.String) { Value = m_opeid.Text });
                cmd.Parameters.Add(new NpgsqlParameter("pass", DbType.String) { Value = m_pass.Text });

                opeData = new opeDS();
                int i = 0;
                var dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {

                    opeData.openo = dataReader["openo"].ToString();
                    opeData.opeid = dataReader["opeid"].ToString();
                    opeData.lastname = dataReader["lastname"].ToString();
                    opeData.fastname = dataReader["fastname"].ToString();
                    opeData.type = dataReader["type"].ToString();
                    i++;

                }

                //取得できない場合はエラー
                if (i <= 0)
                {
                    MessageBox.Show("ログインに失敗しました。", "ログイン", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    logger.InfoFormat("ログインに失敗しました: User -> {0}", m_opeid.Text);
                    ret = -1;
                }
                else if (i > 0)
                {
                    ret = 0;
                }

                return ret;
            }
            catch (Exception ex)
            {
                MessageBox.Show("オペレータ情報の取得に失敗しました。" + ex.Message, "オペレータ情報取得", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.ErrorFormat("オペレータ情報の取得に失敗しました MSG: {0}:  User -> {1}, pass -> {2}", ex.Message, m_opeid.Text, m_pass.Text);
                return -1;
            }

        }
        //ENTERキーが押されたら認証を行う
        private void m_pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                login_process();
            }

        }

        private void m_opeid_KeyDown(object sender, KeyEventArgs e)
        {
            //ENTERでタブの移動
            if (e.KeyCode == Keys.Enter)
                this.m_pass.Focus();
        }

        private void Form_login_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void Form_login_Load(object sender, EventArgs e)
        {
            //バージョン情報
            System.Diagnostics.FileVersionInfo ver =
                System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
            this.m_versioninfo.Text = "ver:" + ver.FileVersion;


        }
    }
}
