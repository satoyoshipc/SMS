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
    public partial class Form_addressSelect : Form
    {
        //DBコネクション
        public NpgsqlConnection con { get; set; }
        public List<MailaddressDS> addressList_tantou;
        public List<MailaddressDS> addressList_ope;
        public List<MailaddressDS> return_address;

        //ログイン情報
        public opeDS loginDS { get; set; }

        public Form_addressSelect()
        {
            InitializeComponent();
        }
        //OKボタン
        private void button1_Click(object sender, EventArgs e)
        {
            if (tabcontrol1.SelectedIndex == 0)
                //メールアドレスをメール画面に表示
                mailtempleteInsert(m_operaterList);
            else if (tabcontrol1.SelectedIndex == 1)
                //メールアドレスをメール画面に表示
                mailtempleteInsert(m_user_tanntouList);

            this.Close();
        }

        //アドレスの表示
        private void Form_addressSelect_Load(object sender, EventArgs e)
        {
            m_selectKoumoku.Items.Add("ユーザID(通番)");
            m_selectKoumoku.Items.Add("アドレス番号");
            m_selectKoumoku.Items.Add("メールアドレス");
            m_selectKoumoku.Items.Add("アドレス名");
            m_selectKoumoku.Items.Add("更新者");

            Class_Detaget dataget = new Class_Detaget();
            Dictionary<string, string> param_dict = new Dictionary<string, string>();
            List<MailaddressDS> Maillist = new List<MailaddressDS>();

            if (tabcontrol1.SelectedIndex == 0)
            {
                //オペレータが選択されていたとき
                Maillist = dataget.selectMailList_ope(param_dict, con);
                addressList_ope = Maillist;
                disp_addressList(Maillist, m_operaterList);
            }
            else if (tabcontrol1.SelectedIndex == 1)
            {
                //カスタマ担当者が選択されていたとき
                Maillist = dataget.selectMailList_Tantou(param_dict, con);
                addressList_tantou = Maillist;
                disp_addressList(Maillist, m_user_tanntouList);
            }



        }
        private void disp_addressList(List<MailaddressDS> Maillist, ListView m_listviewobj)
        {


            m_listviewobj.FullRowSelect = true;
            m_listviewobj.HideSelection = false;
            m_listviewobj.HeaderStyle = ColumnHeaderStyle.Clickable;

            m_listviewobj.Columns.Insert(0, "ユーザ区分", 30, HorizontalAlignment.Left);
            m_listviewobj.Columns.Insert(1, "ユーザID", 40, HorizontalAlignment.Left);
            m_listviewobj.Columns.Insert(2, "ユーザ名", 120, HorizontalAlignment.Left);
            m_listviewobj.Columns.Insert(3, "カスタマ名", 100, HorizontalAlignment.Left);
            m_listviewobj.Columns.Insert(4, "アドレス番号", 40, HorizontalAlignment.Left);
            m_listviewobj.Columns.Insert(5, "メールアドレス", 150, HorizontalAlignment.Left);
            m_listviewobj.Columns.Insert(6, "アドレス名", 120, HorizontalAlignment.Left);
            m_listviewobj.Columns.Insert(7, "更新日時", 50, HorizontalAlignment.Left);
            m_listviewobj.Columns.Insert(8, "更新者", 50, HorizontalAlignment.Left);

            //リストに表示
            if (Maillist != null)
            {
                //メール一覧
                foreach (MailaddressDS s_ds in Maillist)
                {

                    ListViewItem itemx1 = new ListViewItem();
                    if(s_ds.kubun == "1")
                        itemx1.Text = "オペレータ";
                    if (s_ds.kubun == "2")
                        itemx1.Text = "担当者";

                    itemx1.SubItems.Add(s_ds.opetantouno);
                    itemx1.SubItems.Add(s_ds.user_tantou_name);
                    itemx1.SubItems.Add(s_ds.username);
                    itemx1.SubItems.Add(s_ds.addressNo);
                    itemx1.SubItems.Add(s_ds.mailAddress);
                    itemx1.SubItems.Add(s_ds.addressname);
                    itemx1.SubItems.Add(s_ds.chk_date);
                    itemx1.SubItems.Add(s_ds.chk_name_id);
                    m_listviewobj.Items.Add(itemx1);

                }
                 
            }

        }
        //検索ボタン
        private void m_selectBtn_Click(object sender, EventArgs e)
        {

            ListView lv;

            if (tabcontrol1.SelectedIndex == 1)
                lv = m_user_tanntouList;            
            else
                lv = m_operaterList;

            lv.Clear();
            DISP_dataSet dset = new DISP_dataSet();
            Dictionary<string, string> param_dict = new Dictionary<string, string>();
            Class_Detaget dg = new Class_Detaget();

            if (m_selecttext.Text != "")
            {

                if (this.m_selectKoumoku.SelectedIndex.ToString() != "")
                {

                    switch (this.m_selectKoumoku.SelectedIndex)
                    {
                        //ホスト名
                        case 0:
                            param_dict["opetantouno"] = m_selecttext.Text;
                            break;
                        //ホスト名日本
                        case 1:
                            param_dict["addressNo"] = m_selecttext.Text;
                            break;
                        case 2:
                            param_dict["mailAddress"] = m_selecttext.Text;
                            break;
                        case 3:
                            param_dict["addressname"] = m_selecttext.Text;
                            break;
                        //更新者
                        case 4:
                            param_dict["chk_name_id"] = m_selecttext.Text;
                            break;
                        default:
                            break;
                    }
                }
            }
            List<MailaddressDS> addressList;
            if(tabcontrol1.SelectedIndex == 1) { 
                //カスタマ担当者
                addressList_tantou = dg.selectMailList(param_dict, con, "2");
                addressList = addressList_tantou;
            }
            else 
            {
                //オペレータ
                addressList_ope = dg.selectMailList(param_dict, con,"1");
                addressList = addressList_ope;
            }

            //リストに表示
            if (addressList != null)
            {
                //リストに表示
                disp_addressList(addressList, lv);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //タブが変更されたとき
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Class_Detaget dataget = new Class_Detaget();
            Dictionary<string, string> param_dict = new Dictionary<string, string>();
            List<MailaddressDS> Maillist = new List<MailaddressDS>();
            if (tabcontrol1.SelectedIndex == 0)
            {
                if (addressList_ope == null) { 
                    //オペレータが選択されていたとき
                    Maillist = dataget.selectMailList_ope(param_dict, con);
                    addressList_ope = Maillist;
                    disp_addressList(Maillist, m_operaterList);
                }
            }
            else if (tabcontrol1.SelectedIndex == 1)
            {
                if (addressList_tantou == null)
                { 
                    //カスタマ担当者が選択されていたとき
                    Maillist = dataget.selectMailList_Tantou(param_dict, con);
                    addressList_tantou = Maillist;
                    disp_addressList(Maillist, m_user_tanntouList);
                }

            }
        }
        //カスタマ担当者ダブルクリック
        private void m_user_tanntouList_DoubleClick(object sender, EventArgs e)
        {
            //メールアドレスをメール画面に表示
            mailtempleteInsert(m_user_tanntouList);
        }

        private void mailtempleteInsert(ListView m_listviewobj)
        {

            this.return_address = new List<MailaddressDS>();
            foreach (ListViewItem item in m_listviewobj.SelectedItems)
            {
                MailaddressDS mailDS = new MailaddressDS();
                mailDS.kubun = item.Text;
                mailDS.opetantouno = item.SubItems[1].Text;
                mailDS.user_tantou_name = item.SubItems[2].Text;
                mailDS.addressNo = item.SubItems[4].Text;

                mailDS.mailAddress = item.SubItems[5].Text;
                mailDS.addressname = item.SubItems[6].Text;
                mailDS.chk_name_id = loginDS.opeid;
                this.return_address.Add(mailDS);
            }
            this.Close();
        }
        //オペレータのダブルクリック
        private void m_operaterList_DoubleClick(object sender, EventArgs e)
        {
            //メールアドレスをメール画面に表示
            mailtempleteInsert(m_operaterList);
        }
    }
}
