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
    public partial class Form_MailTempleteInsert : Form
    {
        public NpgsqlConnection con { get; set; }
        //ログイン情報
        public opeDS loginDS { get; set; }

        public Form_MailTempleteInsert()
        {
            InitializeComponent();
        }
        //表示前処理
        private void Form_MailTempleteInsert_Load(object sender, EventArgs e)
        {
            m_idlabel.Text = loginDS.opeid;
            m_labelinputOpe.Text = loginDS.lastname + loginDS.fastname;
        }


    }
}
