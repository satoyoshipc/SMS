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
    public partial class Form_SiteDetail : Form
    {
        //ログイン情報
        public opeDS loginDS { get; set; }
         

        //
        public siteDS sitedt { get; set; }
        //ユーザ情報一覧
        public List<siteDS> userList { get; set; }

        //システム情報一覧
        public List<siteDS> systemList{ get; set; }

        //DBコネクション
        public NpgsqlConnection con { get; set; }

        public Form_SiteDetail()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        
        //表示前処理
        //取得したデータを読み取り表示する
        private void Form_SystemDetail_Load(object sender, EventArgs e)
        {

            m_selectKoumoku.Items.Add("拠点通番");
            m_selectKoumoku.Items.Add("拠点名");
            m_selectKoumoku.Items.Add("住所1");
            m_selectKoumoku.Items.Add("住所2");
            m_selectKoumoku.Items.Add("TEL/FAX");
            m_selectKoumoku.Items.Add("ステータス");
            m_selectKoumoku.Items.Add("備考");
            m_selectKoumoku.Items.Add("更新日時");
            m_selectKoumoku.Items.Add("更新者");

            getsite(sitedt);

        }
        //拠点一覧を取得する
        private void getsite(siteDS sitedt)
        {
            this.m_siteno.Text = sitedt.siteno;
            this.m_userno.Text = sitedt.userno;
            this.m_systemno.Text = sitedt.systemno;
            this.m_systemname.Text = sitedt.systemname;
            this.m_sitename.Text = sitedt.sitename;
            this.m_address1.Text = sitedt.address1;
            this.m_address2.Text = sitedt.address2;
            this.m_tel.Text = sitedt.telno;
            this.m_statusCombo.Text = sitedt.status;
            this.m_biko.Text = sitedt.biko;
            this.m_update.Text = sitedt.chk_date;
            this.m_updateOpe.Text = sitedt.chk_name_id;

            Class_Detaget dg = new Class_Detaget();
            dg.con = con;
            if (sitedt.userno != "") {
                this.m_cutomername.Text = dg.getCustomername(sitedt.userno);
            }
            if (sitedt.systemno != "") {

                this.m_systemname.Text = dg.getSystemname(sitedt.systemno);

            }
        }
        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        //検索ボタン
        private void m_selectBtn_Click(object sender, EventArgs e)
        {
            m_Site_List.Clear();
            DISP_dataSet dset = new DISP_dataSet();
            Dictionary<string, string> param_dict = new Dictionary<string, string>();
            Class_Detaget dg = new Class_Detaget();

            if (m_selecttext.Text != "")
            {

                if (this.m_selectKoumoku.SelectedIndex.ToString() != "")
                {
                    switch (this.m_selectKoumoku.SelectedIndex)
                    {
                        //拠点通番
                        case 0:
                            param_dict["siteno"] = m_selecttext.Text;
                            break;
                        //拠点名
                        case 1:
                            param_dict["sitename"] = m_selecttext.Text;
                            break;
                        //住所1
                        case 2:
                            param_dict["address1"] = m_selecttext.Text;
                            break;

                        //住所2
                        case 3:
                            param_dict["address2"] = m_selecttext.Text;
                            break;

                        //TEL/FAX
                        case 4:
                            param_dict["telno"] = m_selecttext.Text;
                            break;
                        //ステータス
                        case 5:
                            param_dict["status"] = m_selecttext.Text;
                            break;
                        //備考
                        case 6:
                            param_dict["biko"] = m_selecttext.Text;
                            break;
                        //カスタマ通番
                        case 7:
                            param_dict["userno"] = m_selecttext.Text;
                            break;

                        //システム通番
                        case 8:
                            param_dict["systemno"] = m_selecttext.Text;
                            break;

                        //更新日時
                        case 9:
                            param_dict["chk_date"] = m_selecttext.Text;
                            break;
                        //更新者
                        case 10:
                            param_dict["chk_name_id"] = m_selecttext.Text;
                            break;

                        default:
                            break;


                    }
                }
            }

            //システム一覧を取得する
            dset = dg.getSelectSite(param_dict, con, dset,true);
            
            this.m_Site_List.FullRowSelect = true;
            this.m_Site_List.HideSelection = false;
            this.m_Site_List.HeaderStyle = ColumnHeaderStyle.Clickable;

            this.m_Site_List.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_Site_List.Columns.Insert(1, "拠点名", 120, HorizontalAlignment.Left);
            this.m_Site_List.Columns.Insert(2, "住所1", 120, HorizontalAlignment.Left);
            this.m_Site_List.Columns.Insert(3, "住所2", 90, HorizontalAlignment.Left);
            this.m_Site_List.Columns.Insert(4, "TEL/FAX", 80, HorizontalAlignment.Left);
            this.m_Site_List.Columns.Insert(5, "ステータス", 50, HorizontalAlignment.Left);
            this.m_Site_List.Columns.Insert(6, "カスタマ番号", 50, HorizontalAlignment.Left);
            this.m_Site_List.Columns.Insert(7, "カスタマ名", 120, HorizontalAlignment.Left);
            this.m_Site_List.Columns.Insert(8, "システム番号", 120, HorizontalAlignment.Left);
            this.m_Site_List.Columns.Insert(9, "システム名", 120, HorizontalAlignment.Left);
            this.m_Site_List.Columns.Insert(10, "更新日時", 50, HorizontalAlignment.Left);
            this.m_Site_List.Columns.Insert(11, "更新者", 50, HorizontalAlignment.Left);

            //リストに表示
            if(dset.site_L != null)
            { 
                foreach (siteDS s_ds in dset.site_L) { 
            

                    ListViewItem itemx1 = new ListViewItem();
                    itemx1.Text = s_ds.siteno;

                    itemx1.SubItems.Add(s_ds.sitename);
                    itemx1.SubItems.Add(s_ds.address1);
                    itemx1.SubItems.Add(s_ds.address2);
                    itemx1.SubItems.Add(s_ds.telno);
                    itemx1.SubItems.Add(s_ds.status);
                    itemx1.SubItems.Add(s_ds.userno);
                    itemx1.SubItems.Add(s_ds.username);
                    itemx1.SubItems.Add(s_ds.systemno);
                    itemx1.SubItems.Add(s_ds.systemname);
                    itemx1.SubItems.Add(s_ds.chk_date);
                    itemx1.SubItems.Add(s_ds.chk_name_id);

                    this.m_Site_List.Items.Add(itemx1);
                }
            }
        }

        //戻るボタン
        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        //更新ボタン
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (m_sitename.Text == "")
            {
                MessageBox.Show("拠点名を入力して下さい。", "拠点修正", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            //確認ダイアログ
            if (MessageBox.Show("拠点データの更新を行います。よろしいですか？", "拠点データ更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string status = "";
            if (m_statusCombo.Text == "有効")
                status = "1";
            else if (m_statusCombo.Text == "無効" )
                status = "0";


            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "update site set sitename=:name,address1=:ad1,address2=:ad2,telno=:tel,status=:status,biko=:biko,chk_name_id =:ope,chk_date=:chdate where siteno = :no";
            using (var transaction = con.BeginTransaction())
            {
                var command = new NpgsqlCommand(@sql, con);
                command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = m_siteno.Text });
                command.Parameters.Add(new NpgsqlParameter("name", DbType.String) { Value = m_sitename.Text });
                command.Parameters.Add(new NpgsqlParameter("ad1", DbType.String) { Value = m_address1.Text });
                command.Parameters.Add(new NpgsqlParameter("ad2", DbType.String) { Value = m_address2.Text });
                command.Parameters.Add(new NpgsqlParameter("tel", DbType.String) { Value = m_tel.Text });
                command.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                command.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = m_biko.Text });
                command.Parameters.Add(new NpgsqlParameter("ope", DbType.String) { Value = loginDS.opeid });
                command.Parameters.Add(new NpgsqlParameter("chdate", DbType.DateTime) { Value = DateTime.Now });
                Int32 rowsaffected;
                try
                {
                    //更新処理
                    rowsaffected = command.ExecuteNonQuery();
                    transaction.Commit();

                    if (rowsaffected != 1)
                        MessageBox.Show("更新できませんでした。", "システム更新");
                    else
                        MessageBox.Show("更新されました。", "システム更新");
                }
                catch (Exception ex)
                {
                    //エラー時メッセージ表示
                    MessageBox.Show(ex.Message);
                    transaction.Rollback();
                    return;
                }
            }

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void m_siteno_DoubleClick(object sender, EventArgs e)
        {

        }
        //ダブルクリックのとき
        private void m_Site_List_DoubleClick(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection item = m_Site_List.SelectedIndices;
            siteDS sitedt = new siteDS();

            sitedt.siteno = this.m_Site_List.Items[item[0]].SubItems[0].Text;
            sitedt.sitename = this.m_Site_List.Items[item[0]].SubItems[1].Text;
            sitedt.address1 = this.m_Site_List.Items[item[0]].SubItems[2].Text;
            sitedt.address2 = this.m_Site_List.Items[item[0]].SubItems[3].Text;
            sitedt.telno = this.m_Site_List.Items[item[0]].SubItems[4].Text;
            sitedt.status = this.m_Site_List.Items[item[0]].SubItems[5].Text;
            sitedt.userno = this.m_Site_List.Items[item[0]].SubItems[6].Text;
            sitedt.username = this.m_Site_List.Items[item[0]].SubItems[7].Text;
            sitedt.systemno = this.m_Site_List.Items[item[0]].SubItems[8].Text;
            sitedt.systemname = this.m_Site_List.Items[item[0]].SubItems[9].Text;
            sitedt.chk_date = this.m_Site_List.Items[item[0]].SubItems[10].Text;
            sitedt.chk_name_id = this.m_Site_List.Items[item[0]].SubItems[11].Text;

            getsite(sitedt);
        }
    }
}
