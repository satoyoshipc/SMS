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
using Outlook = Microsoft.Office.Interop.Outlook;

namespace moss_AP
{
    public partial class Form_mailTempleteList : Form
    {

        //ログイン情報
        public opeDS loginDS { get; set; }

        public List<mailsendaddressDS> mailsendaddressDSList { get; set; }

        //DBコネクション
        public NpgsqlConnection con { get; set; }

        //ListViewのソートの際に使用する
        private Class_ListViewColumnSorter _columnSorter;


        public Form_mailTempleteList()
        {
            InitializeComponent();
        }
        //表示前
        private void Form_mailTempleteList_Load(object sender, EventArgs e)
        {
            _columnSorter = new Class_ListViewColumnSorter();
            m_mailTempleteList.ListViewItemSorter = _columnSorter;



            List<mailTempleteDS> templeteList = new List<mailTempleteDS>();
            Dictionary<string, string> param_dict = new Dictionary<string, string>();
            Class_Detaget dg = new Class_Detaget();

            //メールテンプレートの取得
            templeteList = dg.selectMailtemplete(param_dict, con);
            m_To_list.Columns.Add("アドレス名",120);
            m_To_list.Columns.Add("アドレス", 120);
            m_To_list.Columns.Add("区分",20);
            m_Cc_list.Columns.Add("アドレス名", 120);
            m_Cc_list.Columns.Add("アドレス", 120);
            m_Cc_list.Columns.Add("区分", 20);
            m_Bcc_list.Columns.Add("アドレス名", 120);
            m_Bcc_list.Columns.Add("アドレス", 120);
            m_Bcc_list.Columns.Add("区分", 20);



            disp_mailtemplete(templeteList);
        }

        //メールテンプレート一覧の表示
        private void disp_mailtemplete(List<mailTempleteDS> templeteList)
        {
            this.m_mailTempleteList.FullRowSelect = true;
            this.m_mailTempleteList.HideSelection = false;
            this.m_mailTempleteList.HeaderStyle = ColumnHeaderStyle.Clickable;

            this.m_mailTempleteList.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_mailTempleteList.Columns.Insert(1, "テンプレート名", 200, HorizontalAlignment.Left);
            this.m_mailTempleteList.Columns.Insert(2, "表題", 200, HorizontalAlignment.Left);
            this.m_mailTempleteList.Columns.Insert(3, "本文", 200, HorizontalAlignment.Left);
            this.m_mailTempleteList.Columns.Insert(4, "送信アカウント", 200, HorizontalAlignment.Left);


            this.m_mailTempleteList.Columns.Insert(5, "添付ファイル1", 50, HorizontalAlignment.Left);
            this.m_mailTempleteList.Columns.Insert(6, "添付ファイル2", 50, HorizontalAlignment.Left);
            this.m_mailTempleteList.Columns.Insert(7, "添付ファイル3", 50, HorizontalAlignment.Left);
            this.m_mailTempleteList.Columns.Insert(8, "添付ファイル4", 50, HorizontalAlignment.Left);
            this.m_mailTempleteList.Columns.Insert(9, "添付ファイル5", 50, HorizontalAlignment.Left);
            this.m_mailTempleteList.Columns.Insert(10, "更新日時", 50, HorizontalAlignment.Left);
            this.m_mailTempleteList.Columns.Insert(11, "更新者", 50, HorizontalAlignment.Left);

            //リストに表示
            if (templeteList != null)
            {
                foreach (mailTempleteDS t_ds in templeteList)
                {

                    ListViewItem itemx1 = new ListViewItem();
                    itemx1.Text = t_ds.templateno;

                    itemx1.SubItems.Add(t_ds.template_name);
                    itemx1.SubItems.Add(t_ds.subject);
                    itemx1.SubItems.Add(t_ds.body);
                    itemx1.SubItems.Add(t_ds.account);
                    itemx1.SubItems.Add(t_ds.attach1);
                    itemx1.SubItems.Add(t_ds.attach2);
                    itemx1.SubItems.Add(t_ds.attach3);
                    itemx1.SubItems.Add(t_ds.attach4);
                    itemx1.SubItems.Add(t_ds.attach5);
                    itemx1.SubItems.Add(t_ds.chk_date);
                    itemx1.SubItems.Add(t_ds.chk_name_id);

                    this.m_mailTempleteList.Items.Add(itemx1);
                }
            }
        }

        private void m_mailTempleteList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //リストをダブルクリック
        private void m_mailTempleteList_DoubleClick(object sender, EventArgs e)
        {

            if (this.m_mailTempleteList.SelectedItems.Count > 0)
            {
                m_To_list.Clear();
                m_Cc_list.Clear();
                m_Bcc_list.Clear();
                m_To_list.Columns.Add("アドレス名", 120);
                m_To_list.Columns.Add("アドレス", 120);
                m_To_list.Columns.Add("区分", 20);
                m_Cc_list.Columns.Add("アドレス名", 120);
                m_Cc_list.Columns.Add("アドレス", 120);
                m_Cc_list.Columns.Add("区分", 20);
                m_Bcc_list.Columns.Add("アドレス名", 120);
                m_Bcc_list.Columns.Add("アドレス", 120);
                m_Bcc_list.Columns.Add("区分", 20);

                ListView.SelectedIndexCollection item = m_mailTempleteList.SelectedIndices;
                mailTempleteDS maildt = new mailTempleteDS();
                maildt.templateno = this.m_mailTempleteList.Items[item[0]].SubItems[0].Text;
                maildt.template_name = this.m_mailTempleteList.Items[item[0]].SubItems[1].Text;
                maildt.subject = this.m_mailTempleteList.Items[item[0]].SubItems[2].Text;
                maildt.body = this.m_mailTempleteList.Items[item[0]].SubItems[3].Text;
                maildt.account = this.m_mailTempleteList.Items[item[0]].SubItems[4].Text;

                maildt.attach1 = this.m_mailTempleteList.Items[item[0]].SubItems[5].Text;
                maildt.attach2 = this.m_mailTempleteList.Items[item[0]].SubItems[6].Text;
                maildt.attach3 = this.m_mailTempleteList.Items[item[0]].SubItems[7].Text;
                maildt.attach4 = this.m_mailTempleteList.Items[item[0]].SubItems[8].Text;
                maildt.attach5 = this.m_mailTempleteList.Items[item[0]].SubItems[9].Text;
                maildt.chk_date = this.m_mailTempleteList.Items[item[0]].SubItems[10].Text;
                maildt.chk_name_id = this.m_mailTempleteList.Items[item[0]].SubItems[11].Text;
                //アドレスを取得
                Class_Detaget dg = new Class_Detaget();
                mailsendaddressDSList = dg.getToCcBcc(maildt.templateno, con);
                DispMailtemplete(maildt);

            }
        }
        //表示
        private void DispMailtemplete(mailTempleteDS maildt)
        {

            m_tempno.Text = maildt.templateno;
            m_templetename.Text = maildt.template_name;
            m_subject.Text = maildt.subject;
            m_body.Text = maildt.body;

            m_account.Text = maildt.account;

            m_update.Text = maildt.chk_date;
            m_updateOpe.Text = maildt.chk_name_id;
            m_temp1.Text = maildt.attach1;
            m_temp2.Text = maildt.attach2;
            m_temp3.Text = maildt.attach3;
            m_temp4.Text = maildt.attach4;
            m_temp5.Text = maildt.attach5;

            Dictionary<string, string> address = new Dictionary<string, string>();
            List<String> toList = new List<String>();
            List<String> CcList = new List<String>();
            List<String> BccList = new List<String>();
 

            //宛先の表示
            foreach (mailsendaddressDS ms in mailsendaddressDSList)
            {
                MailaddressDS addressds = new MailaddressDS();

                string tocc = "";
                tocc = ms.toccbcc;

                if (tocc != "")
                {
                    if (tocc == "To")
                    {
                        
                        ListViewItem lvi;

                        lvi = m_To_list.Items.Add(ms.addressname);
                        lvi.SubItems.Add(ms.mailaddress);

                        //1:オペレータ 2:カスタマ担当者
                        if (ms.kubun == "1")
                        {
                            lvi.SubItems.Add("オペレータ");
                        }
                        //2:カスタマ担当者
                        else if (ms.kubun == "2")
                        {
                            lvi.SubItems.Add("担当者");
                        }

                        //非表示項目
                        lvi.Tag = ms.addressno + "," + ms.kubun + "," + ms.opetantouno;
                    }
                    if (tocc == "Cc")
                    {

                        ListViewItem lvi;
                        lvi = m_Cc_list.Items.Add(ms.addressname);
                        lvi.SubItems.Add(ms.mailaddress);

                        //1:オペレータ 2:カスタマ担当者
                        if (ms.kubun == "1")
                        {
                            lvi.SubItems.Add("オペレータ");
                        }
                        //2:カスタマ担当者
                        else if (ms.kubun == "2")
                        {
                            lvi.SubItems.Add("担当者");
                        }

                        //非表示項目
                        lvi.Tag = ms.addressno + "," + ms.kubun + "," + ms.opetantouno;

                    }

                    if (tocc == "Bcc")
                    {

                        ListViewItem lvi;
                        lvi = m_Bcc_list.Items.Add(ms.addressname);
                        lvi.SubItems.Add(ms.mailaddress);
                        //1:オペレータ 2:カスタマ担当者
                        if (ms.kubun == "1")
                        {
                            lvi.SubItems.Add("オペレータ");
                        }
                        //2:カスタマ担当者
                        else if (ms.kubun == "2")
                        {
                            lvi.SubItems.Add("担当者");
                        }

                        //非表示項目
                        lvi.Tag = ms.addressno + "," + ms.kubun + "," + ms.opetantouno;
                    }

                }
            }
        }


        private void m_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //変更ボタン
        private void m_OK_Click(object sender, EventArgs e)
        {
            if (m_templetename.Text == "")
            {
                MessageBox.Show("テンプレート名を入力して下さい。", "メールテンプレート更新", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //確認ダイアログ
            if (MessageBox.Show("メールテンプレートの更新を行います。よろしいですか？", "メールテンプレート更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "update mail_form set " +
               "template_name=:template_name," +
                "subject=:subject," +
                "body=:body," +
                "mail_account=:mail_account," +
                "attach1=:attach1," +
                "attach2=:attach2," +
                "attach3=:attach3," +
                "attach4=:attach4," +
                "attach5=:attach5," +
                "chk_name_id =:ope,chk_date=:chdate " +
                "where templateno = :no";
            
            using (var transaction = con.BeginTransaction())
            {
                var command = new NpgsqlCommand(@sql, con, transaction);
                command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = m_tempno.Text });
                command.Parameters.Add(new NpgsqlParameter("template_name", DbType.String) { Value = m_templetename.Text });
                command.Parameters.Add(new NpgsqlParameter("subject", DbType.String) { Value = m_subject.Text });
                command.Parameters.Add(new NpgsqlParameter("body", DbType.String) { Value = m_body.Text });
                command.Parameters.Add(new NpgsqlParameter("mail_account", DbType.String) { Value = m_account.Text});
                command.Parameters.Add(new NpgsqlParameter("attach1", DbType.String) { Value = m_temp1.Text });
                command.Parameters.Add(new NpgsqlParameter("attach2", DbType.String) { Value = m_temp2.Text });
                command.Parameters.Add(new NpgsqlParameter("attach3", DbType.String) { Value = m_temp3.Text });
                command.Parameters.Add(new NpgsqlParameter("attach4", DbType.String) { Value = m_temp4.Text });
                command.Parameters.Add(new NpgsqlParameter("attach5", DbType.String) { Value = m_temp5.Text });
                command.Parameters.Add(new NpgsqlParameter("ope", DbType.String) { Value = loginDS.opeid });
                command.Parameters.Add(new NpgsqlParameter("chdate", DbType.DateTime) { Value = DateTime.Now });
                Int32 rowsaffected;
                try
                {
                    //更新処理
                    rowsaffected = command.ExecuteNonQuery();


                    if (rowsaffected != 1)
                        MessageBox.Show("更新できませんでした。", "メールテンプレート情報更新");
                    else
                    {
                        //送信先更新
                        if (updatemailaddress(m_tempno.Text, con, transaction) == -1)
                        {
                            MessageBox.Show("メールテンプレート(メールアドレス)の更新に失敗しました", "メールテンプレート更新", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            transaction.Rollback();
                            return;
                        }
                    }
                    transaction.Commit();
                    MessageBox.Show("更新されました。", "メールテンプレート更新");

                }
                catch (Exception ex)
                {
                    //エラー時メッセージ表示
                    MessageBox.Show("更新する際サーバでエラーが発生しました。 " + ex.Message);
//                    transaction.Rollback();
                    return;
                }
            }

        }

        //メールアドレス情報を更新する
        private int updatemailaddress(string templete_no, NpgsqlConnection con, NpgsqlTransaction transaction)
        {
            string csv;
            int ret = 1;
            MailaddressDS addressds = new MailaddressDS();
            //とりあえず今あるものは消す
            if (DeletedSendAddress(templete_no, con, transaction) != 1)
            {
                //                    MessageBox.Show("メールテンプレートの更新（送信先メールアドレス削除)に失敗しました。","");
                //                  return -1;
            }

            //Toのとき
            for (int i = 0; i < this.m_To_list.Items.Count; i++)
            {
                addressds.addressname = this.m_To_list.Items[i].SubItems[0].Text;
                addressds.mailAddress = this.m_To_list.Items[i].SubItems[1].Text;
                string kubunstr = this.m_To_list.Items[i].SubItems[2].Text;
                csv = this.m_To_list.Items[i].Tag.ToString();
                string[] stArray = csv.Split(',');
                addressds.addressNo = stArray[0];
                string ss = "";
                if (kubunstr != "")
                {
                    if (kubunstr == "オペレータ")
                    {
                        ss = "1";
                    }
                    else if (kubunstr == "担当者")
                    {
                        ss = "2";
                    }
                }
                addressds.kubun = ss;
                addressds.opetantouno = stArray[2];

                ret = updateSendAddress(m_tempno.Text, addressds, csv, "To", transaction);
                if (ret == -1) return -1;
            }

            //Ccのとき
            for (int i = 0; i < this.m_Cc_list.Items.Count; i++)
            {
                addressds.addressname = this.m_Cc_list.Items[i].SubItems[0].Text;
                addressds.mailAddress = this.m_Cc_list.Items[i].SubItems[1].Text;
                string kubunstr = this.m_Cc_list.Items[i].SubItems[2].Text;
                csv = this.m_Cc_list.Items[i].Tag.ToString();
                string[] stArray = csv.Split(',');
                addressds.addressNo = stArray[0];
                string ss = "";
                if (kubunstr != "")
                {
                    if (kubunstr == "オペレータ")
                    {
                        ss = "1";
                    }
                    else if (kubunstr == "担当者")
                    {
                        ss = "2";
                    }
                }
                addressds.kubun = ss;
                addressds.opetantouno = stArray[2];

                ret = updateSendAddress(m_tempno.Text, addressds, csv, "Cc", transaction);
                if (ret == -1) return -1;
            }
            //Bccのとき
            for (int i = 0; i < this.m_Bcc_list.Items.Count; i++)
            {
                addressds.addressname = this.m_Bcc_list.Items[i].SubItems[0].Text;
                addressds.mailAddress = this.m_Bcc_list.Items[i].SubItems[1].Text;
                string kubunstr = this.m_Bcc_list.Items[i].SubItems[2].Text;
                csv = this.m_Bcc_list.Items[i].Tag.ToString();
                string[] stArray = csv.Split(',');
                addressds.addressNo = stArray[0];
                string ss = "";
                if (kubunstr != "")
                {
                    if (kubunstr == "オペレータ")
                    {
                        ss = "1";
                    }
                    else if (kubunstr == "担当者")
                    {
                        ss = "2";
                    }
                }
                addressds.kubun = ss;
                addressds.opetantouno = stArray[2];

                ret = updateSendAddress(m_tempno.Text, addressds, csv, "Bcc", transaction);
                if (ret == -1) return -1;
            }

            return ret;
        }

        //
        private int DeletedSendAddress(string templno, NpgsqlConnection con,NpgsqlTransaction transaction)
        {
            //DB接続
            NpgsqlCommand cmd;
            try
            {
                if (con.FullState != ConnectionState.Open) con.Open();
                Int32 rowsaffected;
                //データ登録
                cmd = new NpgsqlCommand(@"DELETE FROM mail_send_address WHERE templateno=:templateno", con, transaction);
                cmd.Parameters.Add(new NpgsqlParameter("templateno", DbType.Int32) { Value = templno });

                rowsaffected = cmd.ExecuteNonQuery();
                
                if (rowsaffected != 1)
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("メールテンプレート更新時エラー " + ex.Message);
                return -1;
            }

            return 1;
        }
        //メールアドレスの更新
        private int updateSendAddress(string templno, MailaddressDS addressds,string csv ,string ToCcBcc, NpgsqlTransaction transaction)
        {

            //DB接続
            NpgsqlCommand cmd;
            try
            {


                string addressno = addressds.addressNo;
                string opetantouno = addressds.opetantouno;
                string kubun = addressds.kubun;

                if (con.FullState != ConnectionState.Open) con.Open();
                Int32 rowsaffected;
                //データ登録
                cmd = new NpgsqlCommand(@"insert into mail_send_address (kubun,opetantouno,toccbcc,addressno,templateno,chk_name_id) 
                    values ( :kubun,:opetantouno,:toccbcc,:addressno,:templateno,:chk_name_id)", con, transaction);
                cmd.Parameters.Add(new NpgsqlParameter("kubun", DbType.String) { Value = kubun });
                cmd.Parameters.Add(new NpgsqlParameter("opetantouno", DbType.Int32) { Value = Int32.Parse(opetantouno) });
                cmd.Parameters.Add(new NpgsqlParameter("toccbcc", DbType.String) { Value = ToCcBcc });
                cmd.Parameters.Add(new NpgsqlParameter("addressno", DbType.Int32) { Value = Int32.Parse(addressno) });
                cmd.Parameters.Add(new NpgsqlParameter("templateno", DbType.Int32) { Value = templno });
                cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = m_updateOpe.Text });

                rowsaffected = cmd.ExecuteNonQuery();

                if (rowsaffected != 1)
                {
                    MessageBox.Show("送信メールを登録できませんでした。", "メールテンプレート登録");
                    return -1;
                }

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (0 <= msg.IndexOf("mail_send_address_pkc"))
                    msg = "To Cc Bccで同じ宛先を登録することはできません。";

                MessageBox.Show("メールテンプレート登録時エラー " + msg);
                return -1;
            }

            return 1;
        }
        private void To_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //メールアドレス選択画面を表示
            Form_addressSelect form = new Form_addressSelect();
            form.con = con;
            form.Owner = this;
            form.loginDS = loginDS;
            form.return_address = new List<MailaddressDS>();
            form.ShowDialog();

            ListViewItem lvi;
            if (form.return_address != null)
            {
                foreach (MailaddressDS ma in form.return_address)
                {

                    lvi = m_To_list.Items.Add(ma.addressname);
                    lvi.SubItems.Add(ma.mailAddress);
                    lvi.SubItems.Add(ma.kubun);

                    //非表示項目
                    lvi.Tag = ma.addressNo + "," + ma.kubun + "," + ma.opetantouno;

                }
                m_To_list.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            }
        }

        private void Cc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //メールアドレス選択画面を表示
            Form_addressSelect form = new Form_addressSelect();
            form.con = con;
            form.Owner = this;
            form.loginDS = loginDS;
            form.return_address = new List<MailaddressDS>();
            form.ShowDialog();
            ListViewItem lvi;
            if (form.return_address != null)
            {
                foreach (MailaddressDS ma in form.return_address)
                {

                    lvi = m_Cc_list.Items.Add(ma.addressname);
                    lvi.SubItems.Add(ma.mailAddress);
                    lvi.SubItems.Add(ma.kubun);

                
                    //非表示項目
                    lvi.Tag = ma.addressNo + "," + ma.kubun + "," + ma.opetantouno;

                }
                m_Cc_list.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            }
        }

        private void Bcc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //メールアドレス選択画面を表示
            Form_addressSelect form = new Form_addressSelect();
            form.con = con;
            form.Owner = this;
            form.loginDS = loginDS;
            form.return_address = new List<MailaddressDS>();
            form.ShowDialog();



            ListViewItem lvi;
            if (form.return_address != null)
            {

                foreach (MailaddressDS ma in form.return_address)
                {

                    lvi = m_Bcc_list.Items.Add(ma.addressname);
                    lvi.SubItems.Add(ma.mailAddress);
                    lvi.SubItems.Add(ma.kubun);

                    //非表示項目
                    lvi.Tag = ma.addressNo + "," + ma.kubun + "," + ma.opetantouno;

                }
                m_Bcc_list.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            }
        }

        private void m_To_list_KeyDown(object sender, KeyEventArgs e)
        {
            //削除キーが押された時
            if (e.KeyCode == Keys.Delete)
            {
                foreach (ListViewItem item in m_To_list.SelectedItems)
                    m_To_list.Items.Remove(item);
            }
        }

        private void m_Cc_list_KeyDown(object sender, KeyEventArgs e)
        {
            //削除キーが押された時
            if (e.KeyCode == Keys.Delete)
            {
                foreach (ListViewItem item in m_Cc_list.SelectedItems)
                    m_Cc_list.Items.Remove(item);
            }
        }


        private void m_Bcc_list_KeyDown(object sender, KeyEventArgs e)
        {
            //削除キーが押された時
            if (e.KeyCode == Keys.Delete)
            {
                foreach (ListViewItem item in m_Bcc_list.SelectedItems)
                    m_Bcc_list.Items.Remove(item);
            }
        }


        //OutLookの下書きフォルダにメールを格納する。(デフォルトのアカウントに保存する
        private void CreateMailItem()
        {
            try { 
            mailTempleteDS ds = new mailTempleteDS();
            ds.subject = m_subject.Text;
            ds.body = m_body.Text;
            ds.account = m_account.Text;
            ds.attach1 = m_temp1.Text;
            ds.attach2 = m_temp2.Text;
            ds.attach3 = m_temp3.Text;
            ds.attach4 = m_temp4.Text;
            ds.attach5 = m_temp5.Text;

            string mailaddress;
            string name;
            //アドレス編集
            for (int i = 0; i < this.m_To_list.Items.Count; i++)
            {
                name = this.m_To_list.Items[i].SubItems[0].Text;
                mailaddress = this.m_To_list.Items[i].SubItems[1].Text;
                ds.Toaddress += name + " <" + mailaddress + ">;";
            }

            for (int i = 0; i < this.m_Cc_list.Items.Count; i++)
            {
                name = this.m_Cc_list.Items[i].SubItems[0].Text;
                mailaddress = this.m_Cc_list.Items[i].SubItems[1].Text;
                ds.CcAddress += name + " <" + mailaddress + ">;";
            }
            for (int i = 0; i < this.m_Bcc_list.Items.Count; i++)
            {
                name = this.m_Bcc_list.Items[i].SubItems[0].Text;
                mailaddress = this.m_Bcc_list.Items[i].SubItems[1].Text;
                ds.BccAddress += name + " <" + mailaddress + ">;";
            }

            //指定されたアカウントを設定する
            String smtpAddress = "";
            smtpAddress = m_account.Text;
            var app = new Outlook.Application();
            Outlook.Account account = null ;
            if (smtpAddress != null && smtpAddress != "")
            {


                Outlook.Accounts accounts = app.Session.Accounts;

                foreach (Outlook.Account maccount in accounts)
                {
                    // When the e-mail address matches, return the account.
                    if (maccount.SmtpAddress == smtpAddress)
                    {
                        account = maccount;
                    }
                }

            }
            //
            Outlook.MailItem mail = app.CreateItem(Outlook.OlItemType.olMailItem) as Outlook.MailItem;
            if(account != null)
                //送信アカウントを設定
                mail.SendUsingAccount = account;


            mail.Subject = m_subject.Text; 

            mail.To = ds.Toaddress;
            mail.CC = ds.CcAddress;
            mail.BCC = ds.BccAddress;



            //Outlook.AddressEntry currentUser = app.Session.CurrentUser.AddressEntry;
            mail.Body = m_body.Text;

            //添付ファイル
            // ファイルが存在しているかどうか確認する
            if (System.IO.File.Exists(ds.attach1))
                mail.Attachments.Add(ds.attach1);
            if (System.IO.File.Exists(ds.attach2))
                mail.Attachments.Add(ds.attach2);
            if (System.IO.File.Exists(ds.attach3))
                mail.Attachments.Add(ds.attach3);
            if (System.IO.File.Exists(ds.attach4))
                mail.Attachments.Add(ds.attach4);
            if (System.IO.File.Exists(ds.attach5))
                mail.Attachments.Add(ds.attach5);


            //アドレス帳を参照する
            mail.Recipients.ResolveAll();

            //下書きに保存
            mail.Save();

            //送信してしまう
            //mail.Send(); 
            MessageBox.Show("OutLookに出力しました。");
            }
            catch(Exception ex)
            {
                MessageBox.Show("OutLookに出力する際にエラーが発生しました。エラー：" + ex.Message,"メール出力");
            }
        }


        //メール出力 入力されている内容を出力する
        private void m_mailDispBtn_Click(object sender, EventArgs e)
        {
            CreateMailItem();

        }

        //削除
        private void m_deleteBtn_Click(object sender, EventArgs e)
        {
            int count = m_mailTempleteList.SelectedItems.Count;

            //確認メッセージ
            if (MessageBox.Show("一覧に選択された行 " + count + "件 の削除を行います。その際参照している他のテーブルデータも削除されます。" + Environment.NewLine +
                "よろしいですか？", "メールテンプレート削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)

                return;


            int ret = deletemailTemplete();
            if (ret == -1)
            {
                return;
            }

            //リストの表示上からけす
            foreach (ListViewItem item in m_mailTempleteList.SelectedItems)
            {
                m_mailTempleteList.Items.Remove(item);
            }
        }
        //削除
        private int deletemailTemplete()
        {
            string templateno;
            int ret = -1;
            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "";

            using (var transaction = con.BeginTransaction())
            {
                foreach (ListViewItem item in m_mailTempleteList.SelectedItems)
                {
                    sql = "WITH DELETED AS (DELETE FROM mail_form where templateno = :no " +
                        "RETURNING templateno) " +
                        "DELETE FROM mail_send_address WHERE templateno IN (SELECT templateno FROM DELETED)";

                    templateno = item.SubItems[0].Text;

                    var command = new NpgsqlCommand(@sql, con,transaction);
                    command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = int.Parse(templateno) });

                    Int32 rowsaffected;
                    try
                    {
                        //削除処理
                        rowsaffected = command.ExecuteNonQuery();

                        if (rowsaffected < 0)
                        {
                            MessageBox.Show("削除できませんでした。テンプレート通番:" + templateno, "テンプレート情報削除");
                            transaction.Rollback();
                            ret = -1;
                        }
                        else {
                            ret = 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        //エラー時メッセージ表示
                        MessageBox.Show("テンプレート情報削除時エラーが発生しました。 " + ex.Message);
                        if(transaction.Connection != null)
                            transaction.Rollback();
                        return -1;
                    }
                }
                if(ret == 1)
                {
                    MessageBox.Show("削除完了しました。");
                    transaction.Commit();
                }
            }
            return ret;
        }

        //メールテンプレートのクリック
        private void m_mailTempleteList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == _columnSorter.SortColumn)
            {
                if (_columnSorter.Order == SortOrder.Ascending)
                {
                    _columnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    _columnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                _columnSorter.SortColumn = e.Column;
                _columnSorter.Order = SortOrder.Ascending;
            }
            m_mailTempleteList.Sort();
        }
    }

}
