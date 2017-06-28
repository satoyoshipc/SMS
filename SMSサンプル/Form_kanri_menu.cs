using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
namespace SMSサンプル
{
    public partial class Form_kanri_menu : Form
    {
        public NpgsqlConnection con { get; set; }
        public opeDS loginDS { get; set; }


        public List<userDS> userList;
        public List<systemDS> systemList;
        public List<siteDS> siteList;

        public Form_kanri_menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form_opeInsert ope = new Form_opeInsert();
            ope.con = con;
            ope.loginDS = loginDS;
            ope.ShowDialog(this);
        }

        //メールテンプレート登録
        private void button3_Click(object sender, EventArgs e)
        {
            Form_MailTempleteInsert mailInsert = new Form_MailTempleteInsert();
            mailInsert.con = con;
            mailInsert.loginDS = loginDS;
            mailInsert.ShowDialog(this);

        }
        //タイマー登録
        private void button2_Click_1(object sender, EventArgs e)
        {
            Form_TimerInsert timerfm = new Form_TimerInsert();
            timerfm.con = con;
            if (userList != null)
                timerfm.userList = userList;
            if (systemList != null)
                timerfm.systemList = systemList;
            if (siteList != null)
                timerfm.siteList = siteList;
            timerfm.loginDS = loginDS;
            timerfm.ShowDialog(this);
        }
        //オペレータ編集
        private void button4_Click(object sender, EventArgs e)
        {
            Form_opeDetail mntForm = new Form_opeDetail();
            mntForm.con = con;
            mntForm.loginDS = loginDS;
            mntForm.Show(this);

        }

        //表示前処理
        private void Form_kanri_menu_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }

        //CSVインポート
        private void button5_Click(object sender, EventArgs e)
        {

        }
        //オーブコム月間予定表
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                String str = System.Configuration.ConfigurationManager.AppSettings["orbcomm_path"];
                // Excel操作用オブジェクト
                Microsoft.Office.Interop.Excel.Application xlApp = null;
                Microsoft.Office.Interop.Excel.Workbooks xlBooks = null;
                Microsoft.Office.Interop.Excel.Workbook xlBook = null;
                //                Microsoft.Office.Interop.Excel.Sheets xlSheets = null;
                //                Microsoft.Office.Interop.Excel.Worksheet xlSheet = null;

                // Excelアプリケーション生成
                xlApp = new Microsoft.Office.Interop.Excel.Application();

                // ◆操作対象のExcelブックを開く◆
                // Openメソッド
                xlBooks = xlApp.Workbooks;
                xlBook = xlBooks.Open(System.IO.Path.GetFullPath(@"C:\Users\PC-USER\Desktop\月間運用予定表_完全自動版3.xls"));


                // Book解放
                //xlBook.Close();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBooks);

                // Excelアプリケーションを解放
                //xlApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excelファイルの表示時に障害が発生しました。" + ex.Message);

            }
        }




        //カスタマインポート
        private void カスタマ情報ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = Disp_FileSelectDlg();
            if (filePath == "")
            {
                return;
            }

            userDS uds;
            List<userDS> list_userDS = new List<userDS>();


            StreamReader reader = new StreamReader(filePath, Encoding.GetEncoding("Shift_JIS"));

            TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(","); // 区切り文字はコンマ


            while (!parser.EndOfData)
            {
                uds = new userDS();
                string[] cols = parser.ReadFields(); // 1行読み込み
                //カスタマ名
                uds.username = cols[1];
                //カスタマメイカナ
                uds.username_kana = cols[2];
                //カスタマ名略
                uds.username_sum = cols[3];

                //レポート有無
                if(cols[6] == "○")
                    uds.report_status = "1";
                else
                    uds.report_status = "0";

                //備考(CSC管理番号)
                uds.biko = cols[4] + Environment.NewLine + "NESIC-CSC管理番号:" + cols[7];

                //最終更新日時
                uds.chk_date = cols[8];


                list_userDS.Add(uds);


            }
            reader.Close();

            if (MessageBox.Show(list_userDS.Count + "件登録します。よろしいですか？", "カスタマインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            //DB接続

            if (con.FullState != ConnectionState.Open) con.Open();

            NpgsqlCommand cmd;
            string rep_status;


            using (var transaction = con.BeginTransaction())
            {

                //インサート文
                int i = 0;
                foreach (userDS us in list_userDS)
                {
                    cmd = new NpgsqlCommand(@"insert into user_tbl(username, username_kana,username_sum,status,report_status,biko,chk_date,chk_name_id) 
                    values ( :username,:username_kana,:username_sum,:status,:report_status,:biko,:chk_date,:chk_name_id)", con);
                    try { 
                        //ステータス
                        if (us.report_status == "有効")
                            rep_status = "1";
                        else
                            rep_status = "0";

                        Int32 rowsaffected;
                        //データ登録
                        cmd.Parameters.Add(new NpgsqlParameter("username", DbType.String) { Value = us.username });
                        cmd.Parameters.Add(new NpgsqlParameter("username_kana", DbType.String) { Value = us.username_kana });
                        cmd.Parameters.Add(new NpgsqlParameter("username_sum", DbType.String) { Value = us.username_sum });
                        cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = "1" });
                        cmd.Parameters.Add(new NpgsqlParameter("report_status", DbType.String) { Value = rep_status });
                        cmd.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = us.biko });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = DateTime.Parse(us.chk_date) });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });

                        rowsaffected = cmd.ExecuteNonQuery();

                        if (rowsaffected != 1)
                        {

                            if (MessageBox.Show("登録できませんでした。カスタマ名:" + us.username + Environment.NewLine + " 継続しますか？", "カスタマ登録", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            { 
                                transaction.Rollback();
                                return;
                            }
                        }
                        else
                        {
                            //登録成功
                            i++;
                            //MessageBox.Show("登録完了", "カスタマ登録");
                        }    
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("カスタマ登録時エラー " + ex.Message);
                        transaction.Rollback();
                        return;
                    }


                }
                MessageBox.Show("カスタマインポート" + i + "件 ");
                //終わったらコミットする
                transaction.Commit();
            }
        }

        //ファイル選択ダイアログを表示
        private string Disp_FileSelectDlg()
        {

            string retStr = "";

            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();

            //はじめのファイル名を指定する
            //はじめに「ファイル名」で表示される文字列を指定する
            ofd.FileName = "";
            //はじめに表示されるフォルダを指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            ofd.InitialDirectory = "";
            //[ファイルの種類]に表示される選択肢を指定する
            //指定しないとすべてのファイルが表示される
            ofd.Filter = "CSVファイル(*.csv)|*.csv";
            //[ファイルの種類]ではじめに
            //「すべてのファイル」が選択されているようにする
            ofd.FilterIndex = 2;
            //タイトルを設定する
            ofd.Title = "CSVファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            ofd.RestoreDirectory = true;
            //存在しないファイルの名前が指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckFileExists = true;
            //存在しないパスが指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckPathExists = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき
                //選択されたファイル名を表示する
                retStr = ofd.FileName;
            }

            return retStr;
        }

        private String getCustomerNo(String Customername,NpgsqlConnection con)
        {
            String userno = "";
            NpgsqlCommand cmd;
            try {
                //DB接続
                if (con.FullState != ConnectionState.Open) con.Open();


                cmd = new NpgsqlCommand(@"SELECT userno,username FROM user_tbl WHERE username = :username", con);

                cmd.Parameters.Add(new NpgsqlParameter("username", DbType.String) { Value = Customername });

                var dataReader = cmd.ExecuteReader();

                //データを読み取る
                while (dataReader.Read())
                {
                    userno = dataReader["userno"].ToString();

                }
                //
                if (userno == null || userno == "")
                {
                   // MessageBox.Show("カスタマNOが取得できませんでした。カスタマ名：" + Customername, "システムインポート", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return "";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("エラー : " + ex.Message);
                return "";

            }
            return userno;

        }

        //システムNOの取得
        private String getSystemNo(String userno,String Systemname, NpgsqlConnection con)
        {
            String systemno = "";
            NpgsqlCommand cmd;
            try
            {
                cmd = new NpgsqlCommand(@"SELECT systemno,systemname FROM system WHERE userno = :userno AND systemname = :systemname", con);

                cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = int.Parse(userno) });
                cmd.Parameters.Add(new NpgsqlParameter("systemname", DbType.String) { Value = Systemname });

                var dataReader = cmd.ExecuteReader();

                //データを読み取る
                while (dataReader.Read())
                {
                    systemno = dataReader["systemno"].ToString();

                }
                //
                if (systemno == null || systemno == "")
                {
                   // MessageBox.Show("システムNOが取得できませんでした。システム名：" + Systemname, "拠点インポート", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー : " + ex.Message);
                return "";

            }
            return systemno;

        }
        
        //拠点NOの取得
        private String getSiteNo(String userno,String systemno,String sitename, NpgsqlConnection con)
        {
            String siteno = "";
            NpgsqlCommand cmd;
            try
            {
                cmd = new NpgsqlCommand(@"SELECT siteno,sitename FROM site WHERE userno = :userno AND systemno = :systemno AND sitename = :sitename", con);

                cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = int.Parse(userno) });
                cmd.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = int.Parse(systemno) });
                cmd.Parameters.Add(new NpgsqlParameter("sitename", DbType.String) { Value = sitename });

                var dataReader = cmd.ExecuteReader();

                //データを読み取る
                while (dataReader.Read())
                {
                    siteno = dataReader["siteno"].ToString();

                }
                //
                if (siteno == null || siteno == "")
                {
                    // MessageBox.Show("拠点NOが取得できませんでした。拠点名：" + Sitename, "ホストインポート", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー : " + ex.Message);
                return "";

            }
            return siteno;

        }
        
        //ユーザ担当者番号の取得
        private String getuserTantouNo(String userno, String UsertanntouName, NpgsqlConnection con)
        {
            String tantouno = "";
            NpgsqlCommand cmd;
            try
            {
                cmd = new NpgsqlCommand(@"SELECT user_tantou_no,user_tantou_name FROM user_tanntou WHERE userno = :userno AND user_tantou_name = :user_tantou_name", con);

                cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = int.Parse(userno) });
                cmd.Parameters.Add(new NpgsqlParameter("user_tantou_name", DbType.String) { Value = UsertanntouName });

                var dataReader = cmd.ExecuteReader();

                //データを読み取る
                while (dataReader.Read())
                {
                    tantouno = dataReader["user_tantou_no"].ToString();
                }
                //
                if (tantouno == null || tantouno == "")
                {
                    // MessageBox.Show("ユーザ担当者NOが取得できませんでした。ユーザ担当者番号：" + tantouno, "ホストインポート", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー : " + ex.Message);
                return "";
            }
            return tantouno;
        }
        //システムデータのインポート
        private void システムSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ファイル選択ダイアログ
            string filePath = Disp_FileSelectDlg();
            if(filePath == "")
            {
                return;
            }
            systemDS sds;
            List<systemDS> list_systemDS = new List<systemDS>();


            TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(","); // 区切り文字はコンマ


            while (!parser.EndOfData)
            {
                sds = new systemDS();
                string[] cols = parser.ReadFields(); // 1行読み込み
                //会社名
                sds.username = cols[1];
                //システム名
                sds.systemname = cols[4];
                //備考(CSC管理番号)
                sds.biko = cols[8] + Environment.NewLine + "NESIC-CSC管理番号:" + cols[8] + Environment.NewLine + cols[11];

                //更新日時
                sds.chk_date = cols[13];

               

                list_systemDS.Add(sds);


            }
            parser.Close();

            if (MessageBox.Show(list_systemDS.Count + "件登録します。よろしいですか？", "システムインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            //DB接続

            if (con.FullState != ConnectionState.Open) con.Open();

            NpgsqlCommand cmd;

            using (var transaction = con.BeginTransaction())
            {

                //インサート文
                int i = 0;
                foreach (systemDS us in list_systemDS)
                {
                    //カスタマNOの取得
                    us.userno=getCustomerNo(us.username,con);
                    if (us.userno == "")
                        //継続
                        continue;
                    

                    cmd = new NpgsqlCommand(@"insert into system(systemname,biko,userno,chk_date,chk_name_id) 
                    values ( :systemname,:biko,:userno,:chk_date,:chk_name_id)", con);

                    try
                    {

                        Int32 rowsaffected;
                        //データ登録
                        cmd.Parameters.Add(new NpgsqlParameter("systemname", DbType.String) { Value = us.systemname });
                        cmd.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = us.biko });
                        cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = int.Parse(us.userno) });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = DateTime.Parse(us.chk_date) });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });

                        rowsaffected = cmd.ExecuteNonQuery();

                        if (rowsaffected != 1)
                        {

                            if (MessageBox.Show("登録できませんでした。システム名:" + us.username + Environment.NewLine + " 継続しますか？", "システム情報のインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                transaction.Rollback();
                                return;
                            }
                        }
                        else
                        {
                            //登録成功
                            i++;
                            //MessageBox.Show("登録完了", "システム名");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("システム情報のインポート" + ex.Message);
                        //transaction.Rollback();
                        return;
                    }


                }
                MessageBox.Show("システム情報のインポート" + i + "件 ");
                //終わったらコミットする
                transaction.Commit();
            }
            
        }
        //拠点インポート
        private void 拠点KToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ファイル選択ダイアログ
            string filePath = Disp_FileSelectDlg();
            if (filePath == "")
            {
                return;
            }
            siteDS sds;
            List<siteDS> list_siteDS = new List<siteDS>();

            TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(","); // 区切り文字はコンマ


            while (!parser.EndOfData)
            {
                sds = new siteDS();
                string[] cols = parser.ReadFields(); // 1行読み込み
                //会社名
                sds.username = cols[0];
                //システム名
                sds.systemname = cols[16];


                //ステータス
                if (cols[1] == "○")
                    sds.status = "1";
                if (cols[1] == "×")
                    sds.status = "2";
                //拠点名
                sds.sitename = cols[3];

                //電話番号1
                sds.telno = cols[4] + " :" + cols[5] + " :" + cols[6] + " :" + cols[7] + cols[8] + " :" + cols[9];
                //住所1
                sds.address1 = cols[10] ;
                //住所2
                sds.address2 = cols[11];

                //備考
                sds.biko = cols[12] + Environment.NewLine + ":" + cols[16] ;

                //更新日時
                sds.chk_date = cols[13];


                list_siteDS.Add(sds);


            }
            parser.Close();

            if (MessageBox.Show(list_siteDS.Count + "件登録します。よろしいですか？", "拠点インポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            //DB接続

            if (con.FullState != ConnectionState.Open) con.Open();

            NpgsqlCommand cmd;

            using (var transaction = con.BeginTransaction())
            {

                //インサート文
                int i = 0;
                int cnt = 0;
                foreach (siteDS si in list_siteDS)
                {
                    //カスタマNOの取得
                    si.userno = getCustomerNo(si.username, con);
                    if (si.userno == "")
                        //継続
                        continue;

                    //システム番号の取得
                    si.systemno = getSystemNo(si.userno,si.systemname, con);
                    //if (si.systemno == "")
                        //継続
                     //   continue;
                    //ステータス
                    string stat = "";
                    if (si.status == "有効")
                        stat = "1";
                    else if (si.status == "無効")
                        stat = "2";
                    cmd = new NpgsqlCommand(@"insert into site(sitename,address1,address2,telno,status,biko,userno,systemno,chk_date,chk_name_id) 
                    values ( :sitename,:address1,:address2,:telno,:status,:biko,:userno,:systemno,:chk_date,:chk_name_id)", con);

                    try
                    {
                        int systemno = 0;
                        int.TryParse(si.systemno, out systemno);

                        Int32 rowsaffected;
                        //データ登録
                        cmd.Parameters.Add(new NpgsqlParameter("sitename", DbType.String) { Value = si.sitename });
                        cmd.Parameters.Add(new NpgsqlParameter("address1", DbType.String) { Value = si.address1 });
                        cmd.Parameters.Add(new NpgsqlParameter("address2", DbType.String) { Value = si.address2 });
                        cmd.Parameters.Add(new NpgsqlParameter("telno", DbType.String) { Value = si.telno });
                        cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = stat });
                        cmd.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = si.biko });
                        cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = int.Parse(si.userno) });
                        cmd.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = systemno });

                        DateTime datet;
                        if (si.chk_date == "")
                            datet = DateTime.Now;
                        DateTime.TryParse(si.chk_date, out datet);
                        cmd.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = datet });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });

                        rowsaffected = cmd.ExecuteNonQuery();

                        if (rowsaffected != 1)
                        {

                            if (MessageBox.Show("登録できませんでした。拠点名:" + si.sitename + Environment.NewLine + " 継続しますか？", "拠点情報のインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                transaction.Rollback();
                                return;
                            }
                        }
                        else
                        {
                            //登録成功
                            i++;
                            //MessageBox.Show("登録完了", "システム名");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("拠点情報のインポート時" + cnt + "件目でエラーが発生しました。" +  ex.Message);
                        //transaction.Rollback();
                        return;
                    }


                }
                MessageBox.Show("拠点情報のインポート" + i + "件 ");
                //終わったらコミットする
                transaction.Commit();
            }
            
        }
        //ホスト情報のインポート
        private void ホストHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ファイル選択ダイアログ
            string filePath = Disp_FileSelectDlg();
            if (filePath == "")
            {
                return;
            }
            hostDS hds;
            List<hostDS> list_hostDS = new List<hostDS>();

            TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(","); // 区切り文字はコンマ

            while (!parser.EndOfData)
            {
                hds = new hostDS();
                string[] cols = parser.ReadFields(); // 1行読み込み
                //会社名
                hds.username = cols[0];
                //システム名
                hds.systemname = cols[3];


                //ステータス
                if (cols[1] == "○")
                    hds.status = "1";
                if (cols[1] == "×")
                    hds.status = "2";
                //拠点名
                hds.sitename = cols[4];
                //ホスト名
                hds.hostname = cols[6];
                //機種
                hds.device = cols[7];
                //設置場所
                hds.location = cols[8];
                //用途
                hds.usefor = cols[9];

                //開始日時
                hds.kansiStartdate= cols[10];
                //終了日時
                hds.kansiEndsdate = cols[11];

                //機種保守情報を入れる
                hds.hosyuinfo = cols[12];
                //機種管理番号を入れる
                hds.hosyukanri = cols[13];

                hds.biko = cols[14] + " :" + cols[15] + " :" + cols[16] + " :" + cols[17] + cols[18] + " :" + cols[19] + " :" + cols[21];

                //更新日時
                hds.chk_date = cols[22];


                list_hostDS.Add(hds);


            }
            parser.Close();

            if (MessageBox.Show(list_hostDS.Count + "件登録します。よろしいですか？", "ホストインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            //DB接続
            if (con.FullState != ConnectionState.Open) con.Open();

            NpgsqlCommand cmd;

            using (var transaction = con.BeginTransaction())
            {

                //インサート文
                int i = 0;
                int cnt = 0;
                foreach (hostDS si in list_hostDS)
                {
                    //カスタマNOの取得
                    si.userno = getCustomerNo(si.username, con);
                    if (si.userno == "")
                        //継続
                        continue;

                    //システム番号の取得
                    si.systemno = getSystemNo(si.userno,si.systemname, con);

                    //if (si.systemno == "")
                    //継続
                    //   continue;
                    //拠点番号の取得
                    si.siteno = getSiteNo(si.userno, si.systemno, si.sitename, con);

                    //ステータス
                    string stat = "";
                    if (si.status == "有効")
                        stat = "1";
                    else if (si.status == "無効")
                        stat = "2";
                    cmd = new NpgsqlCommand(@"insert into host(hostname,status,device,location,usefor,kansistartdate,kansiendsdate,hosyukanri,hosyuinfo,biko,userno,systemno,siteno,chk_date,chk_name_id) 
                    values ( :hostname,:status,:device,:location,:usefor,:kansistartdate,:kansiendsdate,:hosyukanri,:hosyuinfo,:biko,:userno,:systemno,:siteno,:chk_date,:chk_name_id)", con);

                    try
                    {
                        int systemno = 0;
                        int.TryParse(si.systemno, out systemno);

                        Int32 rowsaffected;
                        //データ登録
                        cmd.Parameters.Add(new NpgsqlParameter("hostname", DbType.String) { Value = si.hostname });
                        cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = stat });
                        cmd.Parameters.Add(new NpgsqlParameter("device", DbType.String) { Value = si.device });
                        cmd.Parameters.Add(new NpgsqlParameter("location", DbType.String) { Value = si.location });
                        cmd.Parameters.Add(new NpgsqlParameter("usefor", DbType.String) { Value = si.usefor });

                        //監視開始日時
                        if (si.kansiStartdate == "" || si.kansiStartdate == null)
                        {
                            cmd.Parameters.Add(new NpgsqlParameter("kansistartdate", DbType.DateTime) { Value = null });
                        }
                        else
                        {
                            DateTime startdate;
                            DateTime.TryParse(si.kansiStartdate, out startdate);

                            cmd.Parameters.Add(new NpgsqlParameter("kansistartdate", DbType.DateTime) { Value = startdate });
                        }

                        //監視終了日時
                        if(si.kansiEndsdate == "" || si.kansiEndsdate == null) { 

                            cmd.Parameters.Add(new NpgsqlParameter("kansiendsdate", DbType.DateTime) { Value = null });
                        }
                        else {
                            DateTime enddate;
                            DateTime.TryParse(si.kansiEndsdate, out enddate);

                            cmd.Parameters.Add(new NpgsqlParameter("kansiendsdate", DbType.DateTime) { Value = enddate });
                        }
                        cmd.Parameters.Add(new NpgsqlParameter("hosyukanri", DbType.String) { Value = si.hosyukanri });
                        cmd.Parameters.Add(new NpgsqlParameter("hosyuinfo", DbType.String) { Value = si.hosyuinfo });
                        cmd.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = si.biko });
                        cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = int.Parse(si.userno) });
                        cmd.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = systemno });
                        cmd.Parameters.Add(new NpgsqlParameter("siteno", DbType.Int32) { Value = si.siteno });

                        DateTime datet;
                        if (si.chk_date == "")
                            datet = DateTime.Now;
                        DateTime.TryParse(si.chk_date, out datet);
                        cmd.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = datet });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });

                        rowsaffected = cmd.ExecuteNonQuery();

                        if (rowsaffected != 1)
                        {

                            if (MessageBox.Show("登録できませんでした。ホスト名:" + si.sitename + Environment.NewLine + " 継続しますか？", "ホスト情報のインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                transaction.Rollback();
                                cnt++;
                                return;
                            }
                        }
                        else
                        {
                            //登録成功
                            i++;
                            //MessageBox.Show("登録完了", "システム名");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ホスト情報のインポート時" + cnt + "行目にエラーが発生しました。  ホスト名：" + si.hostname + "  " + ex.Message);
                        //transaction.Rollback();
                        cnt++;
                        return;
                    }

                    cnt++;
                }
                MessageBox.Show("ホスト情報のインポート" + i + "件 ");
                //終わったらコミットする
                transaction.Commit();
            }

        }
        //カスタマ担当者
        private void カスタマ担当者TToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ファイル選択ダイアログ
            string filePath = Disp_FileSelectDlg();
            if (filePath == "")
            {
                return;
            }
            tantouDS sds;
            List<tantouDS> list_tantouDS = new List<tantouDS>();

            TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(","); // 区切り文字はコンマ


            while (!parser.EndOfData)
            {
                sds = new tantouDS();
                string[] cols = parser.ReadFields(); // 1行読み込み

                //ステータス
                if (cols[0] == "○")
                    sds.status = "1";
                if (cols[0] == "×")
                    sds.status = "2";


                //社員名
                sds.user_tantou_name = cols[1];
                sds.user_tantou_name_kana = cols[2];

                //会社名
                sds.username = cols[7];

                //役職
                sds.yakusyoku= cols[5];

                //部署名
                sds.busho_name= cols[8];


                //住所1
                sds.biko = cols[11];

                //更新日時
                sds.chk_date = cols[12];


                list_tantouDS.Add(sds);


            }
            parser.Close();

            if (MessageBox.Show(list_tantouDS.Count + "件登録します。よろしいですか？", "担当者インポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            //DB接続

            if (con.FullState != ConnectionState.Open) con.Open();

            NpgsqlCommand cmd;

            using (var transaction = con.BeginTransaction())
            {

                //インサート文
                int i = 0;
                int cnt = 0;
                foreach (tantouDS si in list_tantouDS)
                {
                    //カスタマNOの取得
                    si.userno = getCustomerNo(si.username, con);
                    if (si.userno == "")
                        //継続
                        continue;

                    //ステータス
                    string stat = "";
                    if (si.status == "有効")
                        stat = "1";
                    else if (si.status == "無効")
                        stat = "2";

                    DateTime datet;
                    DateTime.TryParse(si.chk_date, out datet);


                    cmd = new NpgsqlCommand(@"insert into user_tanntou(user_tantou_name,user_tantou_name_kana,busho_name,yakusyoku,status,biko,userno,chk_date,chk_name_id) 
                    values (:user_tantou_name,:user_tantou_name_kana,:busho_name,:yakusyoku,:status,:biko,:userno,:chk_date,:chk_name_id)", con);

                    try
                    {
                        Int32 rowsaffected;
                        //データ登録
                        cmd.Parameters.Add(new NpgsqlParameter("user_tantou_name", DbType.String) { Value = si.user_tantou_name });
                        cmd.Parameters.Add(new NpgsqlParameter("user_tantou_name_kana", DbType.String) { Value = si.user_tantou_name_kana });
                        cmd.Parameters.Add(new NpgsqlParameter("busho_name", DbType.String) { Value = si.busho_name });
                        cmd.Parameters.Add(new NpgsqlParameter("yakusyoku", DbType.String) { Value = si.yakusyoku });
                        cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = stat });
                        cmd.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = si.biko });
                        cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = int.Parse(si.userno) });

                        cmd.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = datet });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });

                        rowsaffected = cmd.ExecuteNonQuery();

                        if (rowsaffected != 1)
                        {

                            if (MessageBox.Show("登録できませんでした。担当者名:" + si.user_tantou_name + Environment.NewLine + " 継続しますか？", "担当者情報のインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                transaction.Rollback();
                                return;
                            }
                        }
                        else
                        {
                            //登録成功
                            i++;
                            //MessageBox.Show("登録完了", "システム名");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("担当者名情報のインポート時" + cnt + "件目でエラーが発生しました。" + ex.Message);
                        //transaction.Rollback();
                        return;
                    }


                }
                MessageBox.Show("担当者名情報のインポート" + i + "件 ");
                //終わったらコミットする
                transaction.Commit();
            }
        }
        //電話番号のみアップデートで登録を行う。
        private void カスタマ担当者電話番号DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ファイル選択ダイアログ
            string filePath = Disp_FileSelectDlg();
            if (filePath == "")
            {
                return;
            }
            tantouDS sds;
            List<tantouDS> list_tantouDS = new List<tantouDS>();

            TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(","); // 区切り文字はコンマ


            while (!parser.EndOfData)
            {
                sds = new tantouDS();
                string[] cols = parser.ReadFields(); // 1行読み込み

                //ステータス
                if (cols[0] == "○")
                    sds.status = "1";
                if (cols[0] == "×")
                    sds.status = "2";

                if (cols[1] == "TEL")
                    sds.telno1 = cols[2];
                if (cols[1] == "携帯")
                    sds.telno2 = cols[2];

                //社員名
                sds.user_tantou_name = cols[5];

                //更新日時
                sds.chk_date = cols[13];

                list_tantouDS.Add(sds);

            }
            parser.Close();

            if (MessageBox.Show(list_tantouDS.Count + "件の電話番号を登録します。よろしいですか？", "電話番号インポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //DB接続
            if (con.FullState != ConnectionState.Open) con.Open();

            NpgsqlCommand cmd;

            using (var transaction = con.BeginTransaction())
            {

                //インサート文
                int i = 0;
                int cnt = 0;
                foreach (tantouDS si in list_tantouDS)
                {

                    cmd = new NpgsqlCommand(@"update user_tanntou SET telno1=:telno1 ,telno2=:telno2,chk_date=:chk_date WHERE user_tantou_name=:user_tantou_name", con);

                    try
                    {
                        DateTime datet;
                        DateTime.TryParse(si.chk_date, out datet);

                        Int32 rowsaffected;
                        //データ登録
                        cmd.Parameters.Add(new NpgsqlParameter("user_tantou_name", DbType.String) { Value = si.user_tantou_name });
                        cmd.Parameters.Add(new NpgsqlParameter("telno1", DbType.String) { Value = si.telno1 });
                        cmd.Parameters.Add(new NpgsqlParameter("telno2", DbType.String) { Value = si.telno2 });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = datet });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });

                        rowsaffected = cmd.ExecuteNonQuery();

                        if (rowsaffected != 1)
                        {

                            //if (MessageBox.Show("電話番号を登録できませんでした。担当者名:" + si.user_tantou_name + Environment.NewLine + " 継続しますか？", "担当者情報(電話)のインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            //{
                            //    transaction.Rollback();
                            //    return;
                            //}
                        }
                        else
                        {
                            //登録成功
                            i++;
                            //MessageBox.Show("登録完了", "システム名");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("電話番号のインポート時" + cnt + "件目でエラーが発生しました。" + ex.Message);
                        //transaction.Rollback();
                        return;
                    }

                }
                MessageBox.Show("電話番号のインポート" + i + "件 ");
                //終わったらコミットする
                transaction.Commit();
            }
        }
        //メールアドレスを追加する
        private void メールアドレスMToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //ファイル選択ダイアログ
            string filePath = Disp_FileSelectDlg();
            if (filePath == "")
                return;
            
            MailaddressDS sds;
            List<MailaddressDS> list_mailaddressDS = new List<MailaddressDS>();

            TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(","); // 区切り文字はコンマ
            String u_tantou= "";
            int idx = 1;
            while (!parser.EndOfData)
            {
                sds = new MailaddressDS();
                string[] cols = parser.ReadFields(); // 1行読み込み

                //担当名が重複する場合
                if (u_tantou != cols[4]) { 
                    u_tantou = cols[4];
                    idx = 1;
                }
                //if (0 <= cols[1].IndexOf("Mail")) {
                //    if (0 <= cols[1].IndexOf("会社"))
                //sds.addressNo = idx.ToString() ;
                //else if (0 <= cols[1].IndexOf("携帯"))
                //sds.addressNo = idx.ToString();
                //else
                //sds.addressNo = idx.ToString();
                //}
                sds.addressNo = idx.ToString();
                idx++;


                //会社名
                sds.username = cols[8];
                
                //カスタマNOの取得
                sds.userno = getCustomerNo(sds.username, con);
                if (sds.userno == "")
                    //継続
                    continue;

                //社員名
                sds.user_tantou_name = cols[4];
                //社員NOの取得
                sds.opetantouno = getuserTantouNo(sds.userno,sds.user_tantou_name, con);
                if (sds.opetantouno == "")
                    //継続
                    continue;


                //メールアドレス
                sds.mailAddress = cols[2];
                //メール名
                sds.addressname = cols[17];


                //更新日時
                sds.chk_date = cols[13];


                list_mailaddressDS.Add(sds);

            }
            parser.Close();

            if (MessageBox.Show(list_mailaddressDS.Count + "件のメールアドレスを登録します。よろしいですか？", "メールアドレスインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //DB接続
            if (con.FullState != ConnectionState.Open) con.Open();

            NpgsqlCommand cmd;

            using (var transaction = con.BeginTransaction())
            {

                //インサート文
                int i = 0;
                int cnt = 0;
                foreach (MailaddressDS si in list_mailaddressDS)
                {

                    cmd = new NpgsqlCommand(@"insert into mailaddress(kubun,opetantouno,addressno,mailaddress,addressname,chk_date,chk_name_id) 
                    values (:kubun,:opetantouno,:addressno,:mailaddress,:addressname,:chk_date,:chk_name_id)", con);
                    try
                    {
                        DateTime datet;
                        DateTime.TryParse(si.chk_date, out datet);

                        Int32 rowsaffected;
                        //データ登録
                        //カスタマ担当者
                        cmd.Parameters.Add(new NpgsqlParameter("kubun", DbType.String) { Value = "2" });
                        cmd.Parameters.Add(new NpgsqlParameter("opetantouno", DbType.Int32) { Value = int.Parse(si.opetantouno) });
                        cmd.Parameters.Add(new NpgsqlParameter("addressno", DbType.Int32) { Value = int.Parse(si.addressNo) });
                        cmd.Parameters.Add(new NpgsqlParameter("mailaddress", DbType.String) { Value = si.mailAddress });
                        cmd.Parameters.Add(new NpgsqlParameter("addressname", DbType.String) { Value = si.addressname });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = datet });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });

                        rowsaffected = cmd.ExecuteNonQuery();

                        if (rowsaffected != 1)
                        {

                            //if (MessageBox.Show("メールアドレスを登録できませんでした。担当者名:" + si.user_tantou_name + Environment.NewLine + " 継続しますか？", "担当者情報(電話)のインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            //{
                            //    transaction.Rollback();
                            //    return;
                            //}
                        }
                        else
                        {
                            //登録成功
                            i++;
                            //MessageBox.Show("登録完了", "システム名");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("メールアドレスのインポート時" + cnt + "件目でエラーが発生しました。" + ex.Message);
                        //transaction.Rollback();
                        return;
                    }

                }
                MessageBox.Show("電話番号のインポート" + i + "件 ");
                //終わったらコミットする
                transaction.Commit();
            }

        }

        private void 回線情報LToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ファイル選択ダイアログ
            string filePath = Disp_FileSelectDlg();
            if (filePath == "")
                return;

            MailaddressDS sds;
            List<MailaddressDS> list_mailaddressDS = new List<MailaddressDS>();

            TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(","); // 区切り文字はコンマ
            String u_tantou = "";
            int idx = 1;
            while (!parser.EndOfData)
            {
                sds = new MailaddressDS();
                string[] cols = parser.ReadFields(); // 1行読み込み

                //回線情報
                if (u_tantou != cols[4])
                {
                    u_tantou = cols[4];
                    idx = 1;
                }

                sds.addressNo = idx.ToString();
                idx++;

                //会社名
                sds.username = cols[8];

                //カスタマNOの取得
                sds.userno = getCustomerNo(sds.username, con);
                if (sds.userno == "")
                    //継続
                    continue;

                //社員名
                sds.user_tantou_name = cols[4];
                //社員NOの取得
                sds.opetantouno = getuserTantouNo(sds.userno, sds.user_tantou_name, con);
                if (sds.opetantouno == "")
                    //継続
                    continue;


                //メールアドレス
                sds.mailAddress = cols[2];
                //メール名
                sds.addressname = cols[17];


                //更新日時
                sds.chk_date = cols[13];


                list_mailaddressDS.Add(sds);

            }
            parser.Close();

            if (MessageBox.Show(list_mailaddressDS.Count + "件のメールアドレスを登録します。よろしいですか？", "メールアドレスインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //DB接続
            if (con.FullState != ConnectionState.Open) con.Open();

            NpgsqlCommand cmd;

            using (var transaction = con.BeginTransaction())
            {

                //インサート文
                int i = 0;
                int cnt = 0;
                foreach (MailaddressDS si in list_mailaddressDS)
                {

                    cmd = new NpgsqlCommand(@"insert into mailaddress(kubun,opetantouno,addressno,mailaddress,addressname,chk_date,chk_name_id) 
                    values (:kubun,:opetantouno,:addressno,:mailaddress,:addressname,:chk_date,:chk_name_id)", con);
                    try
                    {
                        DateTime datet;
                        DateTime.TryParse(si.chk_date, out datet);

                        Int32 rowsaffected;
                        //データ登録
                        //カスタマ担当者
                        cmd.Parameters.Add(new NpgsqlParameter("kubun", DbType.String) { Value = "2" });
                        cmd.Parameters.Add(new NpgsqlParameter("opetantouno", DbType.Int32) { Value = int.Parse(si.opetantouno) });
                        cmd.Parameters.Add(new NpgsqlParameter("addressno", DbType.Int32) { Value = int.Parse(si.addressNo) });
                        cmd.Parameters.Add(new NpgsqlParameter("mailaddress", DbType.String) { Value = si.mailAddress });
                        cmd.Parameters.Add(new NpgsqlParameter("addressname", DbType.String) { Value = si.addressname });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = datet });
                        cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });

                        rowsaffected = cmd.ExecuteNonQuery();

                        if (rowsaffected != 1)
                        {

                            //if (MessageBox.Show("メールアドレスを登録できませんでした。担当者名:" + si.user_tantou_name + Environment.NewLine + " 継続しますか？", "担当者情報(電話)のインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            //{
                            //    transaction.Rollback();
                            //    return;
                            //}
                        }
                        else
                        {
                            //登録成功
                            i++;
                            //MessageBox.Show("登録完了", "システム名");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("メールアドレスのインポート時" + cnt + "件目でエラーが発生しました。" + ex.Message);
                        //transaction.Rollback();
                        return;
                    }

                }
                MessageBox.Show("電話番号のインポート" + i + "件 ");
                //終わったらコミットする
                transaction.Commit();
            }
        }
    }
}
