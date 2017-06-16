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


        //登録ボタン
        private void button3_Click(object sender, EventArgs e)
        {
            //テンプレート名
            if (m_templete.Text == "")
            {
                MessageBox.Show("テンプレート名が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //サブジェクト
            if (m_subject.Text == "")
            {
                MessageBox.Show("件名が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //本文
            if (m_context.Text == "")
            {
                MessageBox.Show("本文が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //確認
            MessageBox.Show("メールテンプレートを登録します。よろしいですか？", "メールテンプレート登録", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            string attach1 = m_attach1.Text;
            string attach2 = m_attach2.Text;
            string attach3 = m_attach3.Text;
            string attach4 = m_attach4.Text;
            string attach5 = m_attach5.Text;



            //DB接続
            NpgsqlCommand cmd;
            using (var transaction = con.BeginTransaction()) { 
                try
                {
                    if (con.FullState != ConnectionState.Open) con.Open();
                    Int32 rowsaffected;
                    //データ登録
                    cmd = new NpgsqlCommand(@"insert into mail_form (template_name,subject,body,attach1,attach2,attach3,attach4,attach5,chk_name_id) 
                        values ( :template_name,:subject,:body,:attach1,:attach2,:attach3,:attach4,:attach5,:chk_name_id); " +
                        "select currval('mail_form_templateno_seq') ;", con);
                    cmd.Parameters.Add(new NpgsqlParameter("template_name", DbType.String) { Value = m_templete.Text });
                    cmd.Parameters.Add(new NpgsqlParameter("subject", DbType.String) { Value = m_subject.Text });
                    cmd.Parameters.Add(new NpgsqlParameter("body", DbType.String) { Value = m_context.Text });
                    cmd.Parameters.Add(new NpgsqlParameter("attach1", DbType.String) { Value = attach1 });
                    cmd.Parameters.Add(new NpgsqlParameter("attach2", DbType.String) { Value = attach2 });
                    cmd.Parameters.Add(new NpgsqlParameter("attach3", DbType.String) { Value = attach3 });
                    cmd.Parameters.Add(new NpgsqlParameter("attach4", DbType.String) { Value = attach4 });
                    cmd.Parameters.Add(new NpgsqlParameter("attach5", DbType.String) { Value = attach5 });
                    cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = m_idlabel.Text });

                    //OUTパラメータをセットする
                    NpgsqlParameter firstColumn = new NpgsqlParameter("firstcolumn", DbType.Int32);
                    firstColumn.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(firstColumn);

                    rowsaffected = cmd.ExecuteNonQuery();

                    int currval = int.Parse(firstColumn.NpgsqlValue.ToString());

                    if (rowsaffected != 1)
                    {
                        MessageBox.Show("登録できませんでした。", "メールテンプレート登録");
                    }
                    else
                    {

                        int ret = make_sendaddress(currval);
                        if(ret == 1) { 

                            //登録成功
                            MessageBox.Show("登録完了", "メールテンプレート登録");
                            transaction.Commit();
                        }
                        else
                            transaction.Rollback();

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("メールテンプレート登録時エラー " + ex.Message);
                    return;
                }
            }
        }
        //メール送信アドレス
        public int  make_sendaddress(int tempno)
        {
            string csv;
            int ret = 1;
            MailaddressDS addressds = new MailaddressDS();
            //Toのとき
            for (int i = 0;i< this.m_To_list.Items.Count; i++) {
                addressds.addressname = this.m_To_list.Items[i].SubItems[0].Text;
                addressds.mailAddress= this.m_To_list.Items[i].SubItems[1].Text;
                addressds.user_tantou_name = this.m_To_list.Items[i].SubItems[2].Text;
                csv = this.m_To_list.Items[i].Tag.ToString();
                string[] stArray = csv.Split(',');
                addressds.addressNo = stArray[0];
                string ss = "";
                if (stArray[1] != "")
                {
                    if(stArray[1] == "オペレータ")
                    {
                        ss = "1";
                    }
                    else if(stArray[1] == "担当者")
                    {
                        ss = "2";
                    }
                }
                addressds.kubun = ss;
                addressds.opetantouno = stArray[2];

                ret =insertSendAddress(tempno, addressds, csv,"To");
                if (ret == -1) return -1;
            }

            //Ccのとき
            for (int i = 0; i < this.m_Cc_list.Items.Count; i++)
            {
                addressds.addressname = this.m_Cc_list.Items[i].SubItems[0].Text;
                addressds.mailAddress = this.m_Cc_list.Items[i].SubItems[1].Text;
                addressds.user_tantou_name = this.m_Cc_list.Items[i].SubItems[2].Text;
                csv = this.m_Cc_list.Items[i].Tag.ToString();
                string[] stArray = csv.Split(',');
                addressds.addressNo = stArray[0];
                string ss = "";
                if (stArray[1] != "")
                {
                    if (stArray[1] == "オペレータ")
                    {
                        ss = "1";
                    }
                    else if (stArray[1] == "担当者")
                    {
                        ss = "2";
                    }
                }
                addressds.kubun = ss;
                addressds.opetantouno = stArray[2];

                ret = insertSendAddress(tempno, addressds, csv, "Cc");
                if (ret == -1) return -1;
            }
            //Bccのとき
            for (int i = 0; i < this.m_Bcc_list.Items.Count; i++)
            {
                addressds.addressname = this.m_Bcc_list.Items[i].SubItems[0].Text;
                addressds.mailAddress = this.m_Bcc_list.Items[i].SubItems[1].Text;
                addressds.user_tantou_name = this.m_Bcc_list.Items[i].SubItems[2].Text;
                csv = this.m_Bcc_list.Items[i].Tag.ToString();
                string[] stArray = csv.Split(',');
                addressds.addressNo = stArray[0];
                string ss = "";
                if (stArray[1] != "")
                {
                    if (stArray[1] == "オペレータ")
                    {
                        ss = "1";
                    }
                    else if (stArray[1] == "担当者")
                    {
                        ss = "2";
                    }
                }
                addressds.kubun = ss;
                addressds.opetantouno = stArray[2];

                ret = insertSendAddress(tempno, addressds, csv, "Cc");
                if (ret == -1) return -1;
            }


            return ret;
        }
        //送信メールを追加する。
        private int insertSendAddress(int templeteno, MailaddressDS adds, String csv,string ToCcBcc)
        {
            //DB接続
            NpgsqlCommand cmd;
            try
            {
                string addressno = adds.addressNo;
                string opetantouno = adds.opetantouno;
                string kubun = adds.kubun;

                if (con.FullState != ConnectionState.Open) con.Open();
                Int32 rowsaffected;
                //データ登録
                cmd = new NpgsqlCommand(@"insert into mail_send_address (kubun,opetantouno,toccbcc,addressno,templateno,chk_name_id) 
                    values ( :kubun,:opetantouno,:toccbcc,:addressno,:templateno,:chk_name_id)", con);
                cmd.Parameters.Add(new NpgsqlParameter("kubun", DbType.String) { Value = kubun });
                cmd.Parameters.Add(new NpgsqlParameter("opetantouno", DbType.Int32) { Value = Int32.Parse(opetantouno) });
                cmd.Parameters.Add(new NpgsqlParameter("toccbcc", DbType.String) { Value = ToCcBcc });
                cmd.Parameters.Add(new NpgsqlParameter("addressno", DbType.Int32) { Value = Int32.Parse(addressno) });
                cmd.Parameters.Add(new NpgsqlParameter("templateno", DbType.Int32) { Value = templeteno });
                cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = m_idlabel.Text });


                rowsaffected = cmd.ExecuteNonQuery();


                if (rowsaffected != 1)
                {
                    MessageBox.Show("送信メールを登録できませんでした。", "メールテンプレート登録");
                    return -1;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("メールテンプレート登録時エラー " + ex.Message);
                return -1;
            }


            return 1;
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        //リンクをクリックする
        private void To_add_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //メールアドレス選択画面を表示
            Form_addressSelect form = new Form_addressSelect();
            form.con = con;
            form.Owner = this;
            form.loginDS = loginDS;
            form.return_address = new List<MailaddressDS>();
            form.ShowDialog();
            m_To_list.Columns.Add("アドレス名");
            m_To_list.Columns.Add("アドレス");
            m_To_list.Columns.Add("区分");
            m_To_list.Columns.Add("担当者名");
            ListViewItem lvi;
            if (form.return_address != null)
            {
                foreach (MailaddressDS ma in form.return_address)
                {
                    
                    lvi = m_To_list.Items.Add(ma.addressname);
                    lvi.SubItems.Add(ma.mailAddress);
                    lvi.SubItems.Add(ma.kubun);
                    lvi.SubItems.Add(ma.user_tantou_name);

                    //非表示項目
                    lvi.Tag = ma.addressNo + "," + ma.kubun + "," + ma.opetantouno;

                }
                m_To_list.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            }
        }
        

        //Toアドレスを削除する
        private void m_To_list_KeyDown(object sender, KeyEventArgs e)
        {
            //削除キーが押された時
            if (e.KeyCode == Keys.Delete)
            {

                foreach(ListViewItem item in m_To_list.SelectedItems)
                { 
                    m_To_list.Items.Remove(item);
                }
            }
        }


        private void m_Cc_list_KeyDown(object sender, KeyEventArgs e)
        {
            //削除キーが押された時
            if (e.KeyCode == Keys.Delete)
            {

                foreach (ListViewItem item in m_Cc_list.SelectedItems)
                {
                    m_Cc_list.Items.Remove(item);
                }
            }
        }

        private void m_Bcc_list_KeyDown(object sender, KeyEventArgs e)
        {
            //削除キーが押された時
            if (e.KeyCode == Keys.Delete)
            {

                foreach (ListViewItem item in m_Bcc_list.SelectedItems)
                {
                    m_Bcc_list.Items.Remove(item);
                }
            }
        }



        private void Cc_add_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //メールアドレス選択画面を表示
            Form_addressSelect form = new Form_addressSelect();
            form.con = con;
            form.Owner = this;
            form.loginDS = loginDS;
            form.return_address = new List<MailaddressDS>();
            form.ShowDialog();
            m_Cc_list.Columns.Add("アドレス名");
            m_Cc_list.Columns.Add("アドレス");
            m_Cc_list.Columns.Add("区分");
            m_Cc_list.Columns.Add("担当者名");
            ListViewItem lvi;
            if (form.return_address != null)
            {
                foreach (MailaddressDS ma in form.return_address)
                {

                    lvi = m_Cc_list.Items.Add(ma.addressname);
                    lvi.SubItems.Add(ma.mailAddress);
                    lvi.SubItems.Add(ma.kubun);
                    lvi.SubItems.Add(ma.user_tantou_name);

                    //非表示項目
                    lvi.Tag = ma.addressNo + "," + ma.kubun + "," + ma.opetantouno;

                }
                m_Cc_list.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            }
        }

        private void Bcc_add_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //メールアドレス選択画面を表示
            Form_addressSelect form = new Form_addressSelect();
            form.con = con;
            form.Owner = this;
            form.loginDS = loginDS;
            form.return_address = new List<MailaddressDS>();
            form.ShowDialog();
            m_Bcc_list.Columns.Add("アドレス名");
            m_Bcc_list.Columns.Add("アドレス");
            m_Bcc_list.Columns.Add("区分");
            m_Bcc_list.Columns.Add("担当者名");
            ListViewItem lvi;
            if (form.return_address != null)
            {
                foreach (MailaddressDS ma in form.return_address)
                {

                    lvi = m_Bcc_list.Items.Add(ma.addressname);
                    lvi.SubItems.Add(ma.mailAddress);
                    lvi.SubItems.Add(ma.kubun);
                    lvi.SubItems.Add(ma.user_tantou_name);

                    //非表示項目
                    lvi.Tag = ma.addressNo + "," + ma.kubun + "," + ma.opetantouno;

                }
                m_Bcc_list.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            }
        }
    }
}
