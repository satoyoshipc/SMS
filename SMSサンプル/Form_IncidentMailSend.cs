using System;
using Npgsql;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;

namespace moss_AP
{
    public partial class Form_IncidentMailSend : Form
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        //DBコネクション
        public NpgsqlConnection con { get; set; }

        //カスタマ
        public List<userDS> userDSList { get; set; }

        //システム
        public List<systemDS> systemDSList { get; set; }

        //拠点
        public List<siteDS> siteDSList { get; set; }
        //ホスト
        public List<hostDS> hostDSList { get; set; }
        //インターフェイス
        public List<watch_InterfaceDS> interfaceDSList { get; set; }

        //ログイン情報
        public opeDS loginDS;

        public taskDS taskds { get; set; }
        public timerDS timerds { get; set; }
        public List<timerDS> timerDSList { get; set; }

        //テンプレート
        public templeteDS templetedt { get; set; }

        //テンプレート一覧
        public List<templeteDS> templist { get; set; }

        DISP_dataSet dsp_L;


        public Form_IncidentMailSend()
        {
            InitializeComponent();
        }

        //コンテキストメニューが開く前処理
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            // コンテキストメニューをクリア
            this.contextMenuStrip1.Items.Clear();

            DateTime dd = m_date.Value;
            dd.ToString() ;
            contextMenuStrip1.Items.Add(dd.ToString("yyyy/MM/dd HH:mm"), null, date_click);
            contextMenuStrip1.Items.Add(dd.ToShortDateString(), null, shortdate_click);
            contextMenuStrip1.Items.Add(dd.ToString("MM/dd"), null, tukihi_click);
            contextMenuStrip1.Items.Add(dd.ToShortTimeString(), null, time_click);
        }
        private void date_click(object sender, EventArgs e)
        {

            string[] str = { m_date.Value.ToString("yyyy/MM/dd hh:mm") };
            insertChangeWords(str);
        }
        private void shortdate_click(object sender, EventArgs e)
        {
            string[] str = { m_date.Value.ToShortDateString() };
            insertChangeWords(str);
        }
        private void tukihi_click(object sender, EventArgs e)
        {
            string[] str = { m_date.Value.ToString("MM/dd") };
            insertChangeWords(str);
        }
        private void time_click(object sender, EventArgs e)
        {
            string[] str = { m_date.Value.ToShortTimeString() };
            insertChangeWords(str);
        }
        private void contextMenuStrip_closeClick(object sender, EventArgs e)
        {
            contextMenuStrip1.Close();
        }
        private void contextMenuStrip_SubMenuClick(object sender, EventArgs e)
        {
            // sender にはクリックされたメニューの ToolStripMenuItem が入ってきますので、
            // 必要に応じて処理を行います
            string str = "";

            ToolStripMenuItem contextmenu = (ToolStripMenuItem)sender;
            str = contextmenu.Text;
            //insertChangeWords(str);
#if DEBUG
            Console.WriteLine(str);
#endif

            contextMenuStrip1.Close();

        }

        // 第2階層のメニュー項目のクリックイベント
        private void contextMenuStripCombo_SubMenuClick(object sender, EventArgs e)
        {
            // sender にはクリックされたメニューの ToolStripMenuItem が入ってきますので、
            // 必要に応じて処理を行います
            string str = "";

            ToolStripComboBox combo = (ToolStripComboBox)sender;
            str = combo.Text;
           // insertChangeWords(str);
#if DEBUG
            Console.WriteLine(str);
#endif

            contextMenuStrip1.Close();

        }
        //パラメータの文字列を挿入して色を変える
        private void insertChangeWords(IEnumerable<String> words)
        {
            
            //文字列の選択状態
            if(m_body.SelectedText.Length > 0){

                //選択状態を解除しておく
                m_body.SelectedText = "";
                m_body.SelectionLength = 0;

                //赤にする
                m_body.SelectionColor = Color.Red;
                //BoidをFontStyleに追加したFontを作成する
                Font baseFont = m_body.SelectionFont;
                Font fnt = new Font(baseFont.FontFamily,
                    baseFont.Size,
                    baseFont.Style | FontStyle.Bold);

                //Fontを変更する
                m_body.SelectionFont = fnt;

                //文字列を挿入する
                m_body.SelectedText = string.Join(", ", words.Select(x => x.ToString()));

                baseFont.Dispose();
                fnt.Dispose();
            }
            else if(m_templetename.SelectedText.Length > 0)
            {
                //選択状態を解除しておく
                m_templetename.SelectedText = "";
                m_templetename.SelectionLength = 0;

                //赤にする
                m_templetename.SelectionColor = Color.Red;
                //BoidをFontStyleに追加したFontを作成する
                Font baseFont = m_templetename.SelectionFont;
                Font fnt = new Font(baseFont.FontFamily,
                    baseFont.Size,
                    baseFont.Style | FontStyle.Bold);
                //Fontを変更する
                m_templetename.SelectionFont = fnt;

                //文字列を挿入する

                m_templetename.SelectedText = string.Join(", ", words.Select(x => x.ToString()));

                baseFont.Dispose();
                fnt.Dispose();
            }
            else
            {

                MessageBox.Show("挿入対象が選択されていません");
            }

        }

        //表示前処理
        private void Form_IncidentMailSend_Load(object sender, EventArgs e)
        {
            //カスタマ名コンボボックスに表示する値を取得する

            DataTable cutomerTable = new DataTable();
            cutomerTable.Columns.Add("ID", typeof(string));
            cutomerTable.Columns.Add("NAME", typeof(string));

            Class_Detaget getuser = new Class_Detaget();
            getuser.con = con;

            //カスタマ名を取得
            userDSList = getuser.getUserList();

            if (userDSList == null)
                return;

            //カスタマ情報を取得する
            foreach (userDS v in userDSList)
            {
                DataRow row = cutomerTable.NewRow();
                row["ID"] = v.userno;
                row["NAME"] = v.username;
                cutomerTable.Rows.Add(row);
            }

            //データテーブルを割り当てる
            m_usernameCombo.DataSource = cutomerTable;
            m_usernameCombo.DisplayMember = "NAME";
            m_usernameCombo.ValueMember = "ID";

            //Read_systemCombo();
            //ラベルに反映
            if (m_usernameCombo.SelectedValue != null)
            {
                m_userno.Text = m_usernameCombo.SelectedValue.ToString();
                m_cutomername.Text = m_usernameCombo.Text;
            }

        }

        //カスタマ名コンボボックスが変更されたとき
        private void m_usernameCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (m_usernameCombo.Text == "")
            {
                m_userno.Text = "";
                return;
            }
            //ラベルに反映
            if (m_usernameCombo.SelectedValue != null)
            { 
                m_userno.Text = m_usernameCombo.SelectedValue.ToString();
                m_cutomername.Text = m_usernameCombo.Text;
            }
            taskchange();
        }
        //タスク区分コンボが変更された時
        private void taskchange()
        {

            //インシデントタスクテンプレートを取得する
            m_templeteCombo.Enabled = true;

            string userno = m_userno.Text;
            m_templeteCombo.DataSource = null;
            Class_Detaget dg = new Class_Detaget();
            templist
                = dg.getTempleteList(userno, "1", con, true);

            //コンボボックス
            DataTable templeteTable = new DataTable();
            templeteTable.Columns.Add("ID", typeof(string));
            templeteTable.Columns.Add("NAME", typeof(string));

            if (templist == null)
                return;

            //空白行を追加
            templeteDS tmp = new templeteDS();
            tmp.templeteno = "";
            tmp.templetename = "";

            List<templeteDS> temptemplist = new List<templeteDS>();
            temptemplist.Add(tmp);

            //テンプレート情報を取得する
            foreach (templeteDS v in templist)
            {
                DataRow row = templeteTable.NewRow();
                row["ID"] = v.templeteno;
                row["NAME"] = v.templetename;
                templeteTable.Rows.Add(row);
            }

            //データテーブルを割り当てる
            m_templeteCombo.DataSource = templeteTable;
            m_templeteCombo.DisplayMember = "NAME";
            m_templeteCombo.ValueMember = "ID";
            //初期値を反映させる
            templeteSelect();

        }
        private void templeteSelect()
        {
            //m_title.Text = "";
            //mailsendaddressDS?te.Text = "";


            //テンプレート件数分ループを行う
            foreach (templeteDS v in templist)
            {
                if (m_templeteCombo.SelectedValue != null)
                {
                    if (v.templeteno == m_templeteCombo.SelectedValue.ToString())
                    {
                        m_templetename.Text = v.title;
                        m_body.Text = v.text;
                    }

                }
            }
        }
        //テンプレート名を選択
        private void m_templeteCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            templeteSelect();


        }
        //検索ボタン
        private void m_selectBtn_Click(object sender, EventArgs e)
        {
            this.m_selectBtn.Enabled = false;
            try
            {
                m_system_list.Rows.Clear();
                m_site_list.Rows.Clear();
                m_host_list.Rows.Clear();
                m_interface_list.Rows.Clear();


                Dictionary<string, string> param_dict = new Dictionary<string, string>();

                Class_Detaget getdata = new Class_Detaget();

                dsp_L = new DISP_dataSet();

                //カスタマ名コンボボックス
                if (m_usernameCombo.Text != "")
                {
                    param_dict["username"] = m_usernameCombo.Text;
                }

                //構成データの取得
                getdata.con = con;
                dsp_L = getdata.getSelectDataFor_Interface(param_dict, con, dsp_L);


                disp_system(dsp_L);

                disp_site(dsp_L);

                disp_host(dsp_L);

                disp_interface(dsp_L);

                //テンプレートを取得
                taskchange();


            }
            catch (Exception ex)
            {
                MessageBox.Show("構成情報の表示時にエラーが発生しました。" + ex.Message, "構成情報表示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                logger.ErrorFormat("構成情報の表示時にエラーが発生しました。" + ex.Message );
            }
            finally
            {
                this.m_selectBtn.Enabled = true;
            }

            this.m_selectBtn.Enabled = true;
        }

        //システム情報の表示
        private void disp_system(DISP_dataSet dsp_L)
        {
 
            //システム情報
            if (dsp_L.system_L != null)
            {
                string fastline = "";

                systemDSList = new List<systemDS>();

                foreach (systemDS sys in dsp_L.system_L)
                {

                    if (fastline == sys.systemno)
                        continue;

                    string[] h = Enumerable.Range(1, 7).Select(x =>
                                 {
                                     switch (x)
                                     {
                                         case 1:
                                             return sys.systemno == null || sys.systemno == "" ? "" : sys.systemno;
                                         case 2:
                                             return sys.status == null || sys.status == "" ? "" : sys.status;
                                         case 3:
                                             return sys.systemname == null || sys.systemname == "" ? "" : sys.systemname;
                                         case 4:
                                             return sys.systemkana == null || sys.systemkana == "" ? "" : sys.systemkana;
                                         case 5:
                                             return sys.biko == null || sys.biko == "" ? "" : sys.biko;
                                         case 6:
                                             return sys.chk_date == null || sys.chk_date == "" ? "" : sys.chk_date;
                                         case 7:
                                             return sys.chk_name_id == null || sys.chk_name_id == "" ? "" : sys.chk_name_id;
                                         default: return "";
                                     }

                                 }).ToArray();

                    //システム情報の表示
                    this.m_system_list.Rows.Add(h);
                    fastline = sys.systemno;

                    systemDSList.Add(sys);
                }
                //件数を書き込む
                //this.m_system_count.Text = system_list.Rows.Count.ToString() + "件";

                //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                m_system_list.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            }
        }
        //拠点情報一覧の表示
        private void disp_site(DISP_dataSet dsp_L, String systemno = null)
        {

            if (dsp_L == null)
                return;
            //拠点情報
            if (dsp_L.site_L != null)
            {
                siteDSList = new List<siteDS>();

                HashSet<string> ary1 = new HashSet<string>();

                foreach (siteDS s in dsp_L.site_L)
                {
                    //チェックボックスがOFFになっている場合は表示しない
    //                if (this.m_site_umu_check.Checked == false && s.status == "無効")
    //                    continue;

                    //絞込みの時
                    if (systemno != null)
                    {
                        if (systemno == s.systemno)
                        {
                            //重複チェック
                            if (ary1.Add(s.siteno))
                            {

                                string[] si = Enumerable.Range(1, 9)
                                    .Select(x =>
                                    {
                                        switch (x)
                                        {
                                            case 1: return s.siteno == null || s.siteno == "" ? "" : s.siteno;
                                            case 2: return s.status == null || s.status == "" ? "" : s.status;
                                   
                                            case 3: return s.sitename == null || s.sitename == "" ? "" : s.sitename;
                                                //郵便番号
                                            case 4: return s.address1 == null || s.address1 == "" ? "" : s.address1;
                                                //住所
                                            case 5: return s.address2 == null || s.address2 == "" ? "" : s.address2;
                                            case 6: return s.telno == null || s.telno == "" ? "" : s.telno;
                                            case 7: return s.biko == null || s.biko == "" ? "" : s.biko;
                                            case 8: return s.chk_date == null || s.chk_date == "" ? "" : s.chk_date;
                                            case 9: return s.chk_name_id == null || s.chk_name_id == "" ? "" : s.chk_name_id;
                                            default: return "";
                                        }
                                    }).ToArray();


                                //拠点情報の表示
                                this.m_site_list.Rows.Add(si);

                                siteDSList.Add(s);
                            }
                        }
                    }
                    else
                    {
                        //重複チェック
                        if (ary1.Add(s.siteno))
                        {


                            string[] si = Enumerable.Range(1, 9)
                                .Select(x =>
                                {
                                    switch (x)
                                    {
                                        case 1: return s.siteno == null || s.siteno == "" ? "" : s.siteno;
                                        case 2: return s.status == null || s.status == "" ? "" : s.status;
                                        case 3: return s.sitename == null || s.sitename == "" ? "" : s.sitename;
                                        //郵便番号
                                        case 4: return s.address1 == null || s.address1 == "" ? "" : s.address1;
                                        //住所
                                        case 5: return s.address2 == null || s.address2 == "" ? "" : s.address2;
                                        case 6: return s.telno == null || s.telno == "" ? "" : s.telno;
                                        case 7: return s.biko == null || s.biko == "" ? "" : s.biko;
                                        case 8: return s.chk_date == null || s.chk_date == "" ? "" : s.chk_date;
                                        case 9: return s.chk_name_id == null || s.chk_name_id == "" ? "" : s.chk_name_id;
                                        default: return "";
                                    }
                                }).ToArray();

                            //拠点情報の表示
                            this.m_site_list.Rows.Add(si);
                            siteDSList.Add(s);
                        }
                    }
                }
                //件数を書き込む
                //            this.m_site_count.Text = site_list.Rows.Count.ToString() + "件";
                m_site_list.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            }
        }

        //ホスト
        private void disp_host(DISP_dataSet dsp_L, String siteno = null)
        {
            //機器
            if (dsp_L == null)
                return;

            //機器情報
            if (dsp_L.host_L != null)
            {
                if (hostDSList != null) hostDSList.Clear();
                hostDSList = new List<hostDS>();

                HashSet<string> ary1 = new HashSet<string>();
                foreach (hostDS h in dsp_L.host_L)
                {
                    //チェックボックスがOFFになっている場合は表示しない
    //                if (this.m_host_umu_check.Checked == false && h.status == "無効")
    //                    continue;

                    //絞込みの時
                    if (siteno != null)
                    {
                        if (siteno == h.siteno)
                        {
                            //重複チェック
                            if (ary1.Add(h.host_no))
                            {

                                string[] hos = Enumerable.Range(1, 14).Select(x =>
                               {
                                   switch (x)
                                   {
                                       case 1: return h.host_no == null || h.host_no == "" ? "" : h.host_no;
                                       case 2: return h.status == null || h.status == "" ? "" : h.status;
                                       case 3: return h.hostname == null || h.hostname == "" ? "" : h.hostname;
                                       case 4: return h.device == null || h.device == "" ? "" : h.device;
                                       case 5: return h.location == null || h.location == "" ? "" : h.location;
                                       case 6: return h.usefor == null || h.usefor == "" ? "" : h.usefor;
                                       case 7: return h.settikikiid == null || h.settikikiid == "" ? "" : h.settikikiid;
                                       case 8: return h.kansiStartdate == null || h.kansiStartdate == "" ? "" : h.kansiStartdate;
                                       case 9: return h.kansiEndsdate == null || h.kansiEndsdate == "" ? "" : h.kansiEndsdate;
                                       case 10: return h.hosyukanri == null || h.hosyukanri == "" ? "" : h.hosyukanri;
                                       case 11: return h.hosyuinfo == null || h.hosyuinfo == "" ? "" : h.hosyuinfo;
                                       case 12: return h.biko == null || h.biko == "" ? "" : h.biko;
                                       case 13: return h.chk_date == null || h.chk_date == "" ? "" : h.chk_date;
                                       case 14: return h.chk_name_id == null || h.chk_name_id == "" ? "" : h.chk_name_id;
                                       default: return "";
                                   }
                               }).ToArray();


                                //ホスト情報の表示
                                this.m_host_list.Rows.Add(hos);
                                hostDSList.Add(h);
                            }

                        }
                    }
                    else
                    {
                        //重複チェック
                        if (ary1.Add(h.host_no))
                        {
                            string[] hos = Enumerable.Range(1, 14).Select(x =>
                            {
                                switch (x)
                                {
                                    case 1: return h.host_no == null || h.host_no == "" ? "" : h.host_no;
                                    case 2: return h.status == null || h.status == "" ? "" : h.status;
                                    case 3: return h.hostname == null || h.hostname == "" ? "" : h.hostname;
                                    case 4: return h.device == null || h.device == "" ? "" : h.device;
                                    case 5: return h.location == null || h.location == "" ? "" : h.location;
                                    case 6: return h.usefor == null || h.usefor == "" ? "" : h.usefor;
                                    case 7: return h.settikikiid == null || h.settikikiid == "" ? "" : h.settikikiid;
                                    case 8: return h.kansiStartdate == null || h.kansiStartdate == "" ? "" : h.kansiStartdate;
                                    case 9: return h.kansiEndsdate == null || h.kansiEndsdate == "" ? "" : h.kansiEndsdate;
                                    case 10: return h.hosyukanri == null || h.hosyukanri == "" ? "" : h.hosyukanri;
                                    case 11: return h.hosyuinfo == null || h.hosyuinfo == "" ? "" : h.hosyuinfo;
                                    case 12: return h.biko == null || h.biko == "" ? "" : h.biko;
                                    case 13: return h.chk_date == null || h.chk_date == "" ? "" : h.chk_date;
                                    case 14: return h.chk_name_id == null || h.chk_name_id == "" ? "" : h.chk_name_id;
                                    default: return "";
                                }
                            }).ToArray();

                            //ホスト情報の表示
                            this.m_host_list.Rows.Add(hos);

                            hostDSList.Add(h);
                        }
                    }
                }
                //件数を書き込む
                //            this.m_host_count.Text = m_host_list.Rows.Count.ToString() + "件";
                m_host_list.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }

        }
        //インターフェイス
        private void disp_interface(DISP_dataSet dsp_L, String siteno = null, String hostno = null)
        {

            if (dsp_L == null)
                return;
            //インターフェイス情報
            if (dsp_L.watch_L != null)
            {
                HashSet<string> ary1 = new HashSet<string>();
                int dispflg = 0;
                if (interfaceDSList != null) interfaceDSList.Clear();
                interfaceDSList = new List<watch_InterfaceDS>();

                foreach (watch_InterfaceDS w in dsp_L.watch_L)
                {
                    dispflg = 0;

                    //チェックボックスがOFFになっている場合は表示しない
    //                if (this.m_interface_umu_check.Checked == false && w.status == "無効")
    //                    continue;

                    //絞込みの時
                    if (siteno != null)
                    {

                        //拠点番号が同じなら表示
                        if (siteno == w.siteno)
                            dispflg = 1;
                    }
                    //拠点ごとではなくホストごとのとき
                    else if (hostno != null)
                    {
                        if (hostno == w.host_no)
                            dispflg = 1;
                    }
                    //普通の表示
                    else
                        dispflg = 1;


                    //表示対象
                    if (dispflg == 1)
                    {
                        //重複チェック
                        if (ary1.Add(w.watch_Interfaceno))
                        {

                            string[] wi = Enumerable.Range(1, 12)
                                .Select(x =>
                                {
                                    switch (x)
                                    {
                                        case 1: return w.watch_Interfaceno == null || w.watch_Interfaceno == "" ? "" : w.watch_Interfaceno;
                                        case 2: return w.status == null || w.status == "" ? "" : w.status;
                                        case 3: return w.interfacename == null || w.interfacename == "" ? "" : w.interfacename;
                                        case 4: return w.type == null || w.type == "" ? "" : w.type;
                                        case 5: return w.kanshi == null || w.kanshi == "" ? "" : w.kanshi;
                                        case 6: return w.border == null || w.border == "" ? "" : w.border;
                                        case 7: return w.IPaddress == null || w.IPaddress == "" ? "" : w.IPaddress;
                                        case 8: return w.IPaddressNAT == null || w.IPaddressNAT == "" ? "" : w.IPaddressNAT;
                                        case 9: return w.biko == null || w.biko == "" ? "" : w.biko;
                                        case 10: return w.chk_date == null || w.chk_date == "" ? "" : w.chk_date;
                                        case 11: return w.chk_name_id == null || w.chk_name_id == "" ? "" : w.chk_name_id;
                                        default: return "";
                                    }
                                }).ToArray();

                            //インターフェイス情報の表示
                            this.m_interface_list.Rows.Add(wi);
                            interfaceDSList.Add(w);
                        }
                    }
                }

                //件数を書き込む
                //            this.m_interface_count.Text = interface_list.Rows.Count.ToString() + "件";

                m_interface_list.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
        }

        //キャンセルボタン
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //システムリストダブルクリック
        private void m_system_List_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string st = "";

            for (int j = 0; j < m_system_list.Columns.Count; j++)
            {
                if (this.m_system_list.Rows[e.RowIndex].Cells[j].Value != null)
                   st = m_system_list.Rows[e.RowIndex].Cells[j].Value.ToString();
            }
        }

        private void kousei_menu_Click(object sender, EventArgs e)
        {

        }
        //挿入
        private void m_insertLink_Click(object sender, EventArgs e)
        {
            DataGridView source;

            Control menu = contextMenuDataGrid.SourceControl;
            if(menu != null)
            {
                source = (DataGridView)menu;
                //単語のインサート
                ins_word(source);
            }

        }
        //選択された文字列を挿入する
        private void ins_word(DataGridView gridview)
        {

            //選択されている件数
            int selcnt = gridview.GetCellCount(DataGridViewElementStates.Selected);
            
            //選択された文字列を配列で取得
            String[] selectname = Enumerable.Range(1, selcnt).Where(x =>
                gridview.SelectedCells[x - 1].FormattedValueType == Type.GetType("System.String"))
                .Select(x =>
                {
                    return gridview.SelectedCells[x - 1].FormattedValue.ToString();
                }).ToArray();

            //文字列を挿入
            insertChangeWords(selectname);
        }
        //選択クリアボタン
        private void m_selectclearlink_Click(object sender, EventArgs e)
        {
            Control c = this.ActiveControl;
            if (0 <= c.Name.IndexOf("_list")) { 
                //選択をクリアする
                DataGridView gridview = (DataGridView)c;
                gridview.ClearSelection();
            }
        }
        //カスタマ名ラベルの挿入
        private void 挿入ctrlcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(m_cutomername.Text != "")
            {
                string[] str = new string[] { m_cutomername.Text };
                //文字列を挿入
                insertChangeWords(str);
            }
        }
        //送信ボタン
        private void button1_Click(object sender, EventArgs e)
        {
            Form_DispMail maildisp = new Form_DispMail();
            mailTempleteDS maildt = new mailTempleteDS();
            
            maildt.subject = m_templetename.Text;
            maildt.body = m_body.Text.Replace("\n", "\r\n");
            maildisp.mailtempDS = maildt;

            maildisp.con = con;
            maildisp.loginDS = loginDS;

            //テンプレート表示
            maildisp.Show();

        }
    }
}
