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
    public partial class Form_kanri_menu : Form
    {
        public NpgsqlConnection con { get; set; }
        public opeDS loginDS { get; set; }


        public List<userDS> userList;
        public List<systemDS> systemList;
        public List<siteDS> siteList;

        public Form_kanri_menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form_opeInsert ope = new Form_opeInsert();
            ope.con = con;
            ope.loginDS = loginDS;
            ope.ShowDialog(this);
        }

        //メールテンプレート登録
        private void button3_Click(object sender, EventArgs e)
        {
            Form_MailTempleteInsert mailInsert = new Form_MailTempleteInsert();
            mailInsert.con = con;
            mailInsert.loginDS = loginDS;
            mailInsert.ShowDialog(this);
            
        }
        //タイマー登録
        private void button2_Click_1(object sender, EventArgs e)
        {
            Form_TimerInsert timerfm = new Form_TimerInsert();
            timerfm.con = con;
            if (userList != null)
                timerfm.userList = userList;
            if (systemList != null)
                timerfm.systemList = systemList;
            if (siteList != null)
                timerfm.siteList = siteList;
            timerfm.loginDS = loginDS;
            timerfm.ShowDialog(this);
        }
        //オペレータ編集
        private void button4_Click(object sender, EventArgs e)
        {
            Form_opeDetail mntForm = new Form_opeDetail();
            mntForm.con = con;
            mntForm.loginDS = loginDS;
            mntForm.Show(this);

        }

        //表示前処理
        private void Form_kanri_menu_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }

        //CSVインポート
        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
