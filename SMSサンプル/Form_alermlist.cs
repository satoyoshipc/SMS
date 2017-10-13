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
    public partial class Form_alermlist : Form
    {

        public Form_alermlist()
        {
            InitializeComponent();
        }
        //DBコネクション
        public NpgsqlConnection con { get; set; }

        //アラームリスト
        public List<alermDS> almList { get; set; }

        //ListViewのソートの際に使用する
        private Class_ListViewColumnSorter _columnSorter;

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        //表示前
        private void Form_alermlist_Load(object sender, EventArgs e)
        {
            _columnSorter = new Class_ListViewColumnSorter();
            m_alerm_list.ListViewItemSorter = _columnSorter;


            //最前面に表示
            this.TopMost = true;


            this.m_alerm_list.FullRowSelect = true;
            this.m_alerm_list.HideSelection = false;
            this.m_alerm_list.HeaderStyle = ColumnHeaderStyle.Clickable;

            this.m_alerm_list.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_alerm_list.Columns.Insert(1, "インシデントタイプ", 90, HorizontalAlignment.Left);
            this.m_alerm_list.Columns.Insert(2, "アラーム日時", 120, HorizontalAlignment.Left);
            this.m_alerm_list.Columns.Insert(3, "アラーム名", 90, HorizontalAlignment.Left);
            this.m_alerm_list.Columns.Insert(4, "メッセージ", 120, HorizontalAlignment.Left);
            this.m_alerm_list.Columns.Insert(5, "カスタマ名", 80, HorizontalAlignment.Left);
            this.m_alerm_list.Columns.Insert(6, "システム名", 80, HorizontalAlignment.Left);
            this.m_alerm_list.Columns.Insert(7, "インシデント番号", 50, HorizontalAlignment.Left);

            Boolean flg = false;

            Boolean soundflg = false;
            
            //リストに表示
            foreach (alermDS ads in almList)
            {

                //対象のものはアラートを表示
                string title;
                //1:インシデント処理 2:定期作業業務促し 3:作業情報の警告 4:資料展開 5:サブタスク
                string type;
                type = ads.schedule_type;
                switch (type)
                {
                    case "1":
                        title = "インシデント";
                        break;
                    case "2":
                        title = "定期作業";
                        break;
                    case "3":
                        title = "計画作業";
                        break;
                    case "4":
                        title = "特別対応";
                        break;
                    case "5":
                        title = "サブタスク";
                        break;
                    default:
                        title = "アラーム出力";
                        break;

                }
                
                ListViewItem itemx1 = new ListViewItem();
                itemx1.Text = ads.userno;
                //表示しない項目
                itemx1.Name = ads.schedule_no;

                itemx1.SubItems.Add(title);
                itemx1.SubItems.Add(ads.alertdatetime);
                itemx1.SubItems.Add(ads.timer_name);
                itemx1.SubItems.Add(ads.alerm_message);
                itemx1.SubItems.Add(ads.username);
                itemx1.SubItems.Add(ads.systemname);
                itemx1.SubItems.Add(ads.incident_no);

                this.m_alerm_list.Items.Add(itemx1);

                //計画情報の表示
                //MessageBox.Show(ads.alerm_message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //インシデントのは特定の音 
                //オーブコムの定期作業のときも
                if (type == "1" || (ads.username == "オーブコムジャパン" && ads.systemname == "衛星運用監視"))
                {

                    String s_path = System.Configuration.ConfigurationManager.AppSettings["sound_path"];
                    ads.sound = s_path + "標準サウンド.wav";

                }


                //拡張子がwavファイル
                string stExtension = System.IO.Path.GetExtension(ads.sound);
                if (stExtension != ".wav")
                {
                    soundflg = false;
                }
                else
                {
                    soundflg = true;
                    if (flg == false)
                    {
                        //音の再生
                        //Form_testSound soundfm = new Form_testSound();


                        //再生ダイアログは1画面以上は表示しない
                        Form_testSound.Instance.strParam = ads.sound;
                        Form_testSound.Instance.Show();
                        Form_testSound.Instance.play(ads.sound);
                        //soundfm.strParam = ads.sound;
                        //soundfm.Show();
                        //1回で1音のみ鳴らす
                        flg = true;
                        //読み込む
                        //System.Media.SoundPlayer player = null;
                        //player = new System.Media.SoundPlayer(ads.sound);
                        //非同期再生する
                        //player.Play();
                    }
                }
            }
            
            if(soundflg==false && flg == false)
                MessageBox.Show("wavファイルが見つかりませんでした。", "サウンドテスト", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        //登録
        private void button1_Click(object sender, EventArgs e)
        {

            //項目が１つも選択されていない場合
            int count = this.m_alerm_list.SelectedItems.Count;
            if (count == 0) { 
                //処理を抜ける
                MessageBox.Show("処理を行うアラームを選択してください。", "選択エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //対応者
            if (m_taiouOpe.Text == "")
            {
                MessageBox.Show("対応者を入力してください。");
                return;
            }


            if (m_syoriDate.Text == "")
            {
                MessageBox.Show("対応日時を入力してください。");
                return;
            }

            //確認
            DialogResult ret =MessageBox.Show(count.ToString() + "件登録します。よろしいですか？","アラーム対処更新",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (ret == DialogResult.No)            
                return;

            int i=0;
            for (i = 0; i < count;i++) {

                //1番目に選択されれいるアイテムをitemxに格納
                string opeid = m_taiouOpe.Text;

                //アラート日時
                ListViewItem itemx = new ListViewItem();
                itemx = this.m_alerm_list.SelectedItems[i];
                int no = int.Parse(itemx.Name);

                string ss = itemx.SubItems[2].Text;

                string[] dateArrayData = ss.Split(' ',':');
                string datestr = "";
                string timestr = "";
                int t= 0;
                foreach (string str in dateArrayData)
                {
                    //最初は日付
                    if (t == 0)
                    {
                        datestr = str;
                    }
                    else if(t > 0)
                    {
                        timestr += str.PadLeft(2,'0');
                    }
                    t++;
                }

                datestr += " " + timestr.Insert(2, ":").Insert(5, ":");

                DateTime alermDate = DateTime.ParseExact(datestr, "yyyy/MM/dd HH:mm:ss", null);
                

                DateTime syoriDate = m_syoriDate.Value;


                //DB接続
                NpgsqlCommand cmd;
                try
                {
                    if (con.FullState != ConnectionState.Open) con.Open();

                    Int32 rowsaffected;
                    //データ登録
                    cmd = new NpgsqlCommand(@"UPDATE timer_taiou SET opeid=:opid ,taioudate=:d WHERE schedule_no=:s_no AND alertdatetime BETWEEN :alertdate_start AND :alertdate_end", con);
                    cmd.Parameters.Add(new NpgsqlParameter("opid", DbType.String) { Value = opeid });
                    cmd.Parameters.Add(new NpgsqlParameter("d", DbType.DateTime) { Value = syoriDate });
                    cmd.Parameters.Add(new NpgsqlParameter("s_no", DbType.Int32) { Value = no });
                    cmd.Parameters.Add(new NpgsqlParameter("alertdate_start", DbType.DateTime) { Value = alermDate });
                    cmd.Parameters.Add(new NpgsqlParameter("alertdate_end", DbType.DateTime) { Value = alermDate.AddMinutes(1) });

                    cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = opeid });
                    rowsaffected = cmd.ExecuteNonQuery();

                    if (rowsaffected != 1)
                    {
                        MessageBox.Show("処理情報を更新できませんでした。アラーム番号:"+ no , "アラーム");
                        con.Close();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("タイマー登録時エラー " + ex.Message);
                    con.Close();
                    return;
                }

            }
            //登録完了
            MessageBox.Show(i.ToString() + "件 更新完了", "アラーム");
            con.Close();

        }
        //何もせずに終了する
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_alerm_list_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void m_alerm_list_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //項目が１つも選択されていない場合
            int count = this.m_alerm_list.SelectedItems.Count;
            if (count == 0)
            {
                return;
            }
            
            m_alermno.Text = this.m_alerm_list.SelectedItems[0].Name;
            m_alermtitle.Text = this.m_alerm_list.SelectedItems[0].SubItems[3].Text;
            m_alerm_message.Text = this.m_alerm_list.SelectedItems[0].SubItems[4].Text;

            m_customer_name.Text = this.m_alerm_list.SelectedItems[0].SubItems[5].Text;
            m_system_name.Text = this.m_alerm_list.SelectedItems[0].SubItems[6].Text;
            
        }

        private void m_alerm_list_ColumnClick(object sender, ColumnClickEventArgs e)
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
            m_alerm_list.Sort();
        }

        private void Form_alermlist_FormClosed(object sender, FormClosedEventArgs e)
        {
            string strpath = "";
            foreach (alermDS ads in almList)
            {

                strpath = ads.sound;
                   if (strpath != null && strpath != "" && ads.sound != "標準サウンド.wav")
                {
                    System.IO.File.Delete(@strpath);
                }
            }
        }
    }

}
