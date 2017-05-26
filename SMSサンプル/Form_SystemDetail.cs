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
    public partial class Form_SystemDetail : Form
    {
        //ログイン情報
        public opeDS loginDS { get; set; }
         

        //システム
        public systemDS systemdt { get; set; }

        //システム情報一覧
        public List<systemDS> systemList{ get; set; }

        //DBコネクション
        public NpgsqlConnection con { get; set; }

        public Form_SystemDetail()
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

            m_selectKoumoku.Items.Add("システム通番");
            m_selectKoumoku.Items.Add("システム名");
            m_selectKoumoku.Items.Add("システム名カナ");
            m_selectKoumoku.Items.Add("備考");
            m_selectKoumoku.Items.Add("カスタマ通番");
            m_selectKoumoku.Items.Add("更新日時");
            m_selectKoumoku.Items.Add("更新者");

            getsystem(systemdt);
        }
        //システム一覧を取得する
        private void getsystem(systemDS systemdt)
        {
            this.m_systemno.Text = systemdt.systemno;
            this.m_userno.Text = systemdt.userno;
            this.m_cutomername.Text = systemdt.username;
            this.m_systemname.Text = systemdt.systemname;
            this.m_systemname_kana.Text = systemdt.systemkana;
            this.m_biko.Text = systemdt.biko;
            this.m_update.Text = systemdt.chk_date;
            this.m_updateOpe.Text = systemdt.chk_name_id;

        }
        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        //検索ボタン
        private void m_selectBtn_Click(object sender, EventArgs e)
        {
            m_System_List.Clear();
            DISP_dataSet dset = new DISP_dataSet();
            Dictionary<string, string> param_dict = new Dictionary<string, string>();
            Class_Detaget dg = new Class_Detaget();

            if (m_selecttext.Text != "")
            {

                if (this.m_selectKoumoku.SelectedIndex.ToString() != "")
                {
                    switch (this.m_selectKoumoku.SelectedIndex)
                    {
                        //システム通番
                        case 0:
                            param_dict["systemno"] = m_selecttext.Text;
                            break;
                        //システム名
                        case 1:
                            param_dict["systemname"] = m_selecttext.Text;
                            break;
                        //システム名カナ
                        case 2:
                            param_dict["systemkana"] = m_selecttext.Text;
                            break;

                        //備考
                        case 3:
                            param_dict["biko"] = m_selecttext.Text;
                            break;

                        //カスタマ通番
                        case 4:
                            param_dict["userno"] = m_selecttext.Text;
                            break;

                        //更新日時
                        case 5:
                            param_dict["chk_date"] = m_selecttext.Text;
                            break;
                        //更新者
                        case 6:
                            param_dict["chk_name_id"] = m_selecttext.Text;
                            break;

                        default:
                            break;


                    }
                }
            }

            //システム一覧を取得する
            dset = dg.getSelectSystem(param_dict, con, dset,true);
            
            this.m_System_List.FullRowSelect = true;
            this.m_System_List.HideSelection = false;
            this.m_System_List.HeaderStyle = ColumnHeaderStyle.Clickable;

            this.m_System_List.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_System_List.Columns.Insert(1, "システム名", 120, HorizontalAlignment.Left);
            this.m_System_List.Columns.Insert(2, "システム名カナ", 120, HorizontalAlignment.Left);
            this.m_System_List.Columns.Insert(3, "カスタマ通番", 90, HorizontalAlignment.Left);
            this.m_System_List.Columns.Insert(4, "カスタマ名", 80, HorizontalAlignment.Left);
            this.m_System_List.Columns.Insert(5, "備考", 50, HorizontalAlignment.Left);
            this.m_System_List.Columns.Insert(6, "更新日時", 50, HorizontalAlignment.Left);
            this.m_System_List.Columns.Insert(7, "更新者", 50, HorizontalAlignment.Left);

            //リストに表示
            if(dset.system_L != null)
            { 
                foreach (systemDS s_ds in dset.system_L) { 
            

                    ListViewItem itemx1 = new ListViewItem();
                    itemx1.Text = s_ds.systemno;

                    itemx1.SubItems.Add(s_ds.systemname);
                    itemx1.SubItems.Add(s_ds.systemkana);
                    itemx1.SubItems.Add(s_ds.userno);
                    itemx1.SubItems.Add(s_ds.username);
                    itemx1.SubItems.Add(s_ds.biko);
                    itemx1.SubItems.Add(s_ds.chk_date);
                    itemx1.SubItems.Add(s_ds.chk_name_id);

                    this.m_System_List.Items.Add(itemx1);
                }
            }
        }

        //システム一覧がダブルクリックされたとき
        private void m_Ststem_List_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView.SelectedIndexCollection item = m_System_List.SelectedIndices;
            systemDS systemdt = new systemDS();
            systemdt.systemno = this.m_System_List.Items[item[0]].SubItems[0].Text;
            systemdt.systemname = this.m_System_List.Items[item[0]].SubItems[1].Text;
            systemdt.systemkana = this.m_System_List.Items[item[0]].SubItems[2].Text;
            systemdt.userno = this.m_System_List.Items[item[0]].SubItems[3].Text;
            systemdt.username = this.m_System_List.Items[item[0]].SubItems[4].Text;

            systemdt.biko =           this.m_System_List.Items[item[0]].SubItems[5].Text;
            systemdt.chk_date =       this.m_System_List.Items[item[0]].SubItems[6].Text;
            systemdt.chk_name_id =    this.m_System_List.Items[item[0]].SubItems[7].Text;

            getsystem(systemdt);
        }
        //戻るボタン
        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        //更新ボタン
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (m_systemname.Text == "")
            {
                MessageBox.Show("システム名を入力して下さい。", "システム修正", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            //確認ダイアログ
            if (MessageBox.Show("システムデータの更新を行います。よろしいですか？", "システムデータ更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            if (con.FullState != ConnectionState.Open) con.Open();

            string sql = "update system set systemname =:name,systemkana =:kana,biko=:biko,chk_name_id =:ope,chk_date=:chdate where systemno = :no";
            using (var transaction = con.BeginTransaction())
            {
                var command = new NpgsqlCommand(@sql, con);
                command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = m_systemno.Text });
                command.Parameters.Add(new NpgsqlParameter("name", DbType.String) { Value = m_systemname.Text });
                command.Parameters.Add(new NpgsqlParameter("kana", DbType.String) { Value = m_systemname_kana.Text });
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
    }
}
