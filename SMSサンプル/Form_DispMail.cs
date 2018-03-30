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
    public partial class Form_DispMail : Form
    {
        //DBコネクション
        public NpgsqlConnection con { get; set; }
        //アラームリスト
        public List<alermDS> almList { get; set; }

        //ログイン情報
        public opeDS loginDS { get; set; }

        public Form_DispMail()
        {
            InitializeComponent();
        }
        public mailTempleteDS mailtempDS { get; set; }
        
        //表示前処理
        private void Form_DispMail_Load(object sender, EventArgs e)
        {
            m_subject.Text = mailtempDS.subject;
            m_body.Text = mailtempDS.body;
            //m_attach1.Text = mailtempDS.attach1;
            //m_attach2.Text = mailtempDS.attach2;
            //m_attach3.Text = mailtempDS.attach3;
            //m_attach4.Text = mailtempDS.attach4;
            //m_attach5.Text = mailtempDS.attach5;
            //m_To.Text = mailtempDS.Toaddress;
            //m_Cc.Text = mailtempDS.CcAddress;
            //m_Bcc.Text = mailtempDS.BccAddress;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //メール作成画面を表示する
            Form_mailTempleteList mailtmp = new Form_mailTempleteList();
            mailtmp.mailBody = m_body.Text;
            mailtmp.mailTitle = m_subject.Text;

            mailtmp.incidentmail_flg = true;
            mailtmp.con = con;
            mailtmp.loginDS = loginDS;
            mailtmp.Show();
        }

    }
}
