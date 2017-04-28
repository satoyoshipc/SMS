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
        public Form_kanri_menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form_opeInsert ope = new Form_opeInsert();
            ope.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        //メールテンプレート登録
        private void button3_Click(object sender, EventArgs e)
        {
            Form_MailTempleteInsert mailInsert = new Form_MailTempleteInsert();
            mailInsert.ShowDialog(this);
            
        }
        //タイマー登録
        private void button2_Click_1(object sender, EventArgs e)
        {
            Form_TimerInsert timerfm = new Form_TimerInsert();
            timerfm.ShowDialog(this);
        }
        //表示前処理
        private void Form_kanri_menu_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }
    }
}
