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
        public Form_UserTantouInsert()
        {
            InitializeComponent();
        }

        //メールアドレス登録
        private void button4_Click(object sender, EventArgs e)
        {
            Form_addressInsert addfm = new Form_addressInsert();
            addfm.Show();

        }
    }
}
