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
    public partial class Form_TimerInsert : Form
    {
        public Form_TimerInsert()
        {
            InitializeComponent();
        }
        //テストサウンド
        private void button4_Click(object sender, EventArgs e)
        {
            if (m_soudpath.Text == "" )
            {
                MessageBox.Show("wavファイルを入力してください。", "サウンドテスト", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //拡張子がwavファイル
            string stExtension = System.IO.Path.GetExtension(m_soudpath.Text);
            if(stExtension != ".wav")
            {
                MessageBox.Show("wavファイルを入力してください。", "サウンドテスト", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Form_testSound soundfm = new Form_testSound();
            soundfm.strParam = m_soudpath.Text;
            soundfm.ShowDialog(this);

        }
        //参照
        private void button1_Click(object sender, EventArgs e)
        {
            string str = "";
            //
            Class_common common = new Class_common();
            str = common.Disp_FileSelectDlg("wav");

            m_soudpath.Text = str;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
