using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;


namespace moss_AP
{
    public partial class Form_kanri_menu : Form
    {
        public NpgsqlConnection con { get; set; }
        public opeDS loginDS { get; set; }
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
            Form_scheduleInsert timerfm = new Form_scheduleInsert();
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
                xlBook = xlBooks.Open(System.IO.Path.GetFullPath(@str));
                xlApp.Visible = true;

                // Book解放
                //xlBook.Close();
                // System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
                // System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBooks);

                // Excelアプリケーションを解放
                //xlApp.Quit();
                // System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excelファイルの表示時に障害が発生しました。" + ex.Message);
                logger.ErrorFormat("Excelファイルの表示時に障害が発生しました" + ex.Message);


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

            try { 

                StreamReader reader = new StreamReader(filePath, Encoding.GetEncoding("Shift_JIS"));

                TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(","); // 区切り文字はコンマ


                while (!parser.EndOfData)
                {
                    uds = new userDS();
                    string[] cols = parser.ReadFields(); // 1行読み込み

                    //1文字目#はコメント
                    string str = "";
                    if(str.StartsWith("#"))
                        continue;
                    uds.customerID = cols[6];
                    //カスタマ名
                    uds.username = cols[0];
                    //カスタマメイカナ
                    uds.username_kana = cols[1];
                    //カスタマ名略
                    uds.username_sum = cols[2];

                    //レポート有無
                    if(cols[6] == "○")
                        uds.report_status = "1";
                    else
                        uds.report_status = "0";

                    //備考
                    uds.biko = cols[3];

                    //最終更新日時
                    uds.chk_date = cols[7];


                    list_userDS.Add(uds);


                }
                reader.Close();

                if (MessageBox.Show(list_userDS.Count + "件登録します。よろしいですか？", "カスタマインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

            }
            catch(Exception ex)
            {
                MessageBox.Show("CSVファイルの読み込み時にエラーが発生しました。" + ex.Message);
                logger.ErrorFormat("カスタマのインポートエラー。CSVファイルの読み込み時にエラーが発生しました。 ");

                return;

            }
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
                                logger.ErrorFormat("カスタマ登録に失敗しました。カスタマ名:{0}", us.username);
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
                        transaction.Rollback();
                        MessageBox.Show("カスタマ登録時エラー " + ex.Message);
                        logger.ErrorFormat("カスタマ登録に失敗しました。カスタマ名:{0}", us.username);

                        return;
                    }


                }
                //終わったらコミットする
                transaction.Commit();
                MessageBox.Show("カスタマインポート" + i + "件 ");

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
                MessageBox.Show("カスタマNOが取得できませんでした エラー : " + ex.Message);
                logger.ErrorFormat("カスタマNOが取得できませんでした。カスタマ名:{0}", Customername);

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

                    cmd = new NpgsqlCommand(@"SELECT systemno,systemname FROM system WHERE userno = :userno ", con);

                    cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = int.Parse(userno) });

                    dataReader = cmd.ExecuteReader();

                    //データを読み取る
                    while (dataReader.Read())
                    {
                        systemno = dataReader["systemno"].ToString();
                        if (systemno == null || systemno == "")
                        {
                            logger.Warn("システムNOが取得できませんでした。システム名:" + Systemname);
                            return "";

                        }
                        else
                        {
                            logger.Warn("システムNOが取得できませんでした。存在するシステムIDを返します。　システム名:" + Systemname );

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー : " + ex.Message);
                logger.ErrorFormat("システムNOが取得できませんでした。システム名:{0}", Systemname);
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
                logger.ErrorFormat("拠点NOが取得できませんでした。拠点名:{0}", sitename);
                return "";

            }
            return siteno;

        }

        //ホスト名
        private String getHostNo(String userno, String systemno, String siteno,String hostname, NpgsqlConnection con)
        {
            String hostno = "";
            NpgsqlCommand cmd;
            try
            {
                cmd = new NpgsqlCommand(@"SELECT host_no,hostname FROM host WHERE userno = :userno AND systemno = :systemno AND siteno = :siteno AND hostname =:hostname", con);

                cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = int.Parse(userno) });
                cmd.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = int.Parse(systemno) });
                cmd.Parameters.Add(new NpgsqlParameter("siteno", DbType.Int32) { Value = int.Parse(siteno) });
                cmd.Parameters.Add(new NpgsqlParameter("hostname", DbType.String) { Value = hostname });

                var dataReader = cmd.ExecuteReader();

                //データを読み取る
                while (dataReader.Read())
                {
                    hostno = dataReader["host_no"].ToString();

                }
                //
                if (hostno == null || hostno == "")
                {
                    // MessageBox.Show("ホストNOが取得できませんでした。拠点名：" + hostname, "ホストインポート", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー : " + ex.Message);
                logger.ErrorFormat("ホストNOが取得できませんでした。ホスト名:{0}", hostname);

                return "";

            }
            return hostno;
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
                logger.ErrorFormat("ユーザ担当者NOが取得できませんでした。ユーザ担当者:{0}", UsertanntouName);
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

            try
            {

                TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(","); // 区切り文字はコンマ


                while (!parser.EndOfData)
                {
                    sds = new systemDS();
                    string[] cols = parser.ReadFields(); // 1行読み込み
                                                     
                    //1行目#はコメント
                    string str = "";
                    str = cols[0];
                    if (str.StartsWith("#"))
                        continue;

                    //会社名
                    sds.username = cols[0];
                    //システム名
                    sds.systemname = cols[1];
                    //備考(CSC管理番号)
                    sds.biko = cols[3] + Environment.NewLine + "NESIC-CSC管理番号:" + cols[4] + Environment.NewLine;

                    //更新日時
                    sds.chk_date = cols[5];

                    list_systemDS.Add(sds);
                
                }
                parser.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSVファイルの読み込み時にエラーが発生しました。" + ex.Message);
                logger.ErrorFormat("システム情報のインポートエラー。CSVファイルの読み込み時にエラーが発生しました。");
                
                return;
            }
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
                        logger.ErrorFormat("システム情報のインポートエラー。{0} システム:{1}", ex.Message, us.systemname);
                    
                        return;
                    }


                }
                //終わったらコミットする
                transaction.Commit();

                //システム名がないカスタマをチェックする
                Int64 nosysCnt = check_noSystem();
                if (nosysCnt >= 1)
                {
                    if (MessageBox.Show("システムが存在しないカスタマが" + nosysCnt.ToString() + "件存在します。「NW監視」で固定登録します。" + Environment.NewLine
                        + "よろしいですか?", "システムインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //システムを固定で挿入する
                        systemInsert();

                }


                MessageBox.Show("システム情報のインポート" + i + "件 ");

            }
            
        }
        //システムが存在しないカスタマがあるか確認する 件数を返す
        private Int64 check_noSystem()
        {
            String sql = "select count(userno) from user_tbl where not exists (select systemno from system where user_tbl.userno = system.userno)";

            Int64 Count = 0;

            NpgsqlCommand cmd;


            try
            {
                //DB接続
                if (con.FullState != ConnectionState.Open) con.Open();

                //SELECT実行
                cmd = new NpgsqlCommand(@sql, con);
                Count = (Int64)cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                MessageBox.Show("システムテーブルチェック時にエラー " + ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.ErrorFormat("システムテーブルチェック時にエラー メソッド名：{0}。MSG：{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return -1;
            }

            return Count;
        }
        //システムを固定でインサートする
        private void systemInsert()
        {
            //DB接続
            if (con.FullState != ConnectionState.Open) con.Open();

            NpgsqlCommand cmd;

            using (var transaction = con.BeginTransaction())
            {

                //インサート文

                cmd = new NpgsqlCommand(@"  insert into system (systemname,systemkana,userno,chk_date,chk_name_id) " +
                    "select 'NM監視','ネットワーク監視',userno,:chk_date,:chk_name_id from user_tbl where not exists (select systemno from system where user_tbl.userno = system.userno)", con);

                try
                {
                    Int32 rowsaffected;
                    //データ登録
                    cmd.Parameters.Add(new NpgsqlParameter("chk_date", DbType.DateTime) { Value = DateTime.Now });
                    cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = loginDS.opeid });

                    rowsaffected = cmd.ExecuteNonQuery();

                    if (rowsaffected < 1)
                    {

                        transaction.Rollback();
                        MessageBox.Show("固定のシステム「NW監視」が登録できませんでした。", "システムインサート", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        logger.ErrorFormat("固定のシステムが登録できませんでした。");
                        return;
                        
                    }
                    else
                    {
                        //終わったらコミットする
                        transaction.Commit();
                        MessageBox.Show("固定のシステム「NW監視システム」を登録しました " + rowsaffected + "件 ");
                    }
                }
                catch (Exception ex)
                {
                    logger.ErrorFormat("固定のシステム「NW監視システム」を登録時にエラーが発生しました。{0}", ex.Message);
                    MessageBox.Show("固定のシステム「NW監視システム」を登録時にエラーが発生しました。{0}" + ex.Message);
                    //transaction.Rollback();
                    return;
                }


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

            try
            {
                TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(","); // 区切り文字はコンマ

                while (!parser.EndOfData)
                {
                    sds = new siteDS();
                    string[] cols = parser.ReadFields(); // 1行読み込み

                    //1行目#はコメント
                    string str = "";
                    str = cols[0];
                    if (str.StartsWith("#"))
                        continue;

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
                    //郵便番号
                    sds.address1 = cols[10];
                    //住所
                    sds.address2 = cols[11];

                    //備考
                    sds.biko = cols[12] + Environment.NewLine + ":" + cols[16];

                    //更新日時
                    sds.chk_date = cols[13];


                    list_siteDS.Add(sds);


                }
                parser.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("CSVファイルの読み込み時にエラーが発生しました。" + ex.Message);
                logger.ErrorFormat("拠点情報のインポートエラー。CSVファイルの読み込み時にエラーが発生しました。");

                return;

            }
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
//                    if (si.systemno == "")
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
                                logger.ErrorFormat("拠点名を登録できませんでした。{0} ", si.sitename);

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
                        logger.ErrorFormat("拠点情報のインポート時" + cnt + "件目でエラーが発生しました。{0}", ex.Message);
                        MessageBox.Show("拠点情報のインポート時" + cnt + "件目でエラーが発生しました。" +  ex.Message);
                        //transaction.Rollback();
                        return;
                    }


                }
                //終わったらコミットする
                transaction.Commit();
                MessageBox.Show("拠点情報のインポート" + i + "件 ");

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
            try
            {
                TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(","); // 区切り文字はコンマ

                while (!parser.EndOfData)
                {
                    hds = new hostDS();
                    string[] cols = parser.ReadFields(); // 1行読み込み

                    //1行目#はコメント
                    string str = "";
                    str = cols[0];
                    if (str.StartsWith("#"))
                        continue;


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
                    hds.usefor = "";

                    //設置機器ID
                    hds.settikikiid = cols[9];

                    //開始日時
                    hds.kansiStartdate = cols[10];
                    //終了日時
                    hds.kansiEndsdate = cols[11];

                    //機種保守情報を入れる
                    hds.hosyuinfo = cols[12];
                    //機種管理番号を入れる
                    hds.hosyukanri = cols[13];

                    hds.biko = cols[14] + " :" + cols[15] + " :" + cols[16];

                    //更新日時
                    hds.chk_date = cols[23];

                    list_hostDS.Add(hds);

                }
                parser.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSVファイルの読み込み時にエラーが発生しました。" + ex.Message);
                logger.ErrorFormat("ホストのインポートエラー。CSVファイルの読み込み時にエラーが発生しました。");

                return;
            }
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
                    cmd = new NpgsqlCommand(@"insert into host(hostname,settikikiid,status,device,location,usefor,kansistartdate,kansiendsdate,hosyukanri,hosyuinfo,biko,userno,systemno,siteno,chk_date,chk_name_id) 
                                       values ( :hostname,:settikikiid,:status,:device,:location,:usefor,:kansistartdate,:kansiendsdate,:hosyukanri,:hosyuinfo,:biko,:userno,:systemno,:siteno,:chk_date,:chk_name_id)", con);

                    try
                    {
                        int systemno = 0;
                        int.TryParse(si.systemno, out systemno);

                        Int32 rowsaffected;
                        //データ登録
                        cmd.Parameters.Add(new NpgsqlParameter("hostname", DbType.String) { Value = si.hostname });
                        cmd.Parameters.Add(new NpgsqlParameter("settikikiid", DbType.String) { Value = si.settikikiid });
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

                            if (MessageBox.Show("ホスト情報を登録できませんでした。ホスト名:" + si.hostname + Environment.NewLine + " 継続しますか？", "ホスト情報のインポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                transaction.Rollback();
                                logger.ErrorFormat("ホスト情報を登録できませんでした。{0} ", si.hostname);
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
                //終わったらコミットする
                transaction.Commit();
                MessageBox.Show("ホスト情報のインポート" + i + "件 ");
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
            try
            {
                TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(","); // 区切り文字はコンマ


                while (!parser.EndOfData)
                {
                    sds = new tantouDS();
                    string[] cols = parser.ReadFields(); // 1行読み込み

                    //1行目#はコメント
                    string str = "";
                    str = cols[0];
                    if (str.StartsWith("#"))
                        continue;



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
                    sds.yakusyoku = cols[5];

                    //部署名
                    sds.busho_name = cols[8];


                    //備考
                    sds.biko = cols[11];

                    //更新日時
                    sds.chk_date = cols[12];


                    list_tantouDS.Add(sds);


                }
                parser.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSVファイルの読み込み時にエラーが発生しました。" + ex.Message);
                logger.ErrorFormat("カスタマ担当者のインポートエラー。CSVファイルの読み込み時にエラーが発生しました。");

                return;
            }
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
                //終わったらコミットする
                transaction.Commit();
                MessageBox.Show("担当者名情報のインポート" + i + "件 ");

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
            try
            {
                TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(","); // 区切り文字はコンマ


                while (!parser.EndOfData)
                {
                    sds = new tantouDS();
                    string[] cols = parser.ReadFields(); // 1行読み込み

                    //1行目#はコメント
                    string str = "";
                    str = cols[0];
                    if (str.StartsWith("#"))
                        continue;



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
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSVファイルの読み込み時にエラーが発生しました。" + ex.Message);
                logger.ErrorFormat("カスタマ担当者のインポートエラー。CSVファイルの読み込み時にエラーが発生しました。");

                return;
            }

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
                //終わったらコミットする
                transaction.Commit();
                MessageBox.Show("電話番号のインポート" + i + "件 ");

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

            try
            {

                TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(","); // 区切り文字はコンマ
                String u_tantou = "";
                int idx = 1;
                while (!parser.EndOfData)
                {
                    sds = new MailaddressDS();
                    string[] cols = parser.ReadFields(); // 1行読み込み

                    //1行目#はコメント
                    string str = "";
                    str = cols[0];
                    if (str.StartsWith("#"))
                        continue;



                    //担当名が重複する場合
                    if (u_tantou != cols[4])
                    {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSVファイルの読み込み時にエラーが発生しました。" + ex.Message);
                logger.ErrorFormat("メールアドレスのインポートエラー。CSVファイルの読み込み時にエラーが発生しました。");

                return;
            }
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
                        logger.ErrorFormat("メールアドレスのインポート時" + cnt + "件目でエラーが発生しました。MSG:{0}", ex.Message);

                        //transaction.Rollback();
                        return;
                    }

                }
                //終わったらコミットする
                transaction.Commit();
                MessageBox.Show("電話番号のインポート" + i + "件 ");

            }

        }

        private void 回線情報LToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ファイル選択ダイアログ
            string filePath = Disp_FileSelectDlg();
            if (filePath == "")
                return;

            kaisenDS kds;
            List<kaisenDS> list_kaisen = new List<kaisenDS>();

            try { 
                TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(","); // 区切り文字はコンマ

                String username = "";
                String systemname = "";
                String sitename = "";
                String hostname = "";


                while (!parser.EndOfData)
                {
                    kds = new kaisenDS();
                    string[] cols = parser.ReadFields(); // 1行読み込み                         //ステータス

                    //1行目#はコメント
                    string str = "";
                    str = cols[0];
                    if (str.StartsWith("#"))
                        continue;



                    kds.career = cols[2] + " " + cols[3];
                    kds.type = cols[4];
                    kds.kaisenid = cols[5];
                    kds.telno1 = cols[6];
                    kds.telno2 = cols[7];
                    kds.telno3 = cols[8];

                    //isp
                    kds.isp = cols[9] + " " + cols[10];
                    kds.servicetype = cols[11];
                    kds.serviceid = cols[12];

                    hostname = cols[15];
                    systemname = cols[17];
                    sitename = cols[20];
                    username = cols[22];
                    kds.status = "1";

                    //カスタマNOの取得
                    kds.userno = getCustomerNo(username, con);
                    if (kds.userno == "")
                        //継続
                        continue;

                    //システムNOの取得
                    kds.systemno = getSystemNo(kds.userno, systemname, con);
                    if (kds.systemno != "") {

                        //拠点NOの取得
                        kds.siteno = getSiteNo(kds.userno, kds.systemno, sitename, con);
                        if (kds.siteno != "") {
                            //ホストNOの取得
                            kds.host_no = getHostNo(kds.userno, kds.systemno, kds.siteno, hostname, con);
                        }
                    }


                    //更新日時
                    kds.chk_date = cols[24];

                    list_kaisen.Add(kds);
                }
                parser.Close();
            }

            catch(Exception ex){
                MessageBox.Show("CSVファイルの読み込み時にエラーが発生しました。" + ex.Message);
                logger.ErrorFormat("回線情報のインポートエラー。CSVファイルの読み込み時にエラーが発生しました。");

                return;
            }

            if (MessageBox.Show(list_kaisen.Count + "件の回線情報を登録します。よろしいですか？", "回線情報インポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //DB接続
            if (con.FullState != ConnectionState.Open) con.Open();

            NpgsqlCommand cmd;

            using (var transaction = con.BeginTransaction())
            {

                //インサート文
                int i = 0;
                int cnt = 0;
                string status = "";
                foreach (kaisenDS sikds in list_kaisen)
                {
                    if (sikds.status == "有効")
                    {
                        status = "1";
                    }
                    if (sikds.status == "無効")
                    {
                        status = "0";

                    }


                    cmd = new NpgsqlCommand(@"insert into kaisen(status,userno,systemno,siteno,host_no,career,type,kaisenid,telno1,telno2,telno3,isp,servicetype,serviceid,chk_date,chk_name_id) 
                    values (:status,:userno,:systemno,:siteno,:host_no,:career,:type,:kaisenid,:telno1,:telno2,:telno3,:isp,:servicetype,:serviceid,:chk_date,:chk_name_id)", con);
                    try
                    {
                        DateTime datet;
                        DateTime.TryParse(sikds.chk_date, out datet);

                        Int32 rowsaffected;

                        //データ登録
                        int ret_userno;
                        int ret_systemno;
                        int ret_siteno;
                        int ret_host_no;

                        int.TryParse(sikds.userno, out ret_userno);
                        int.TryParse(sikds.systemno, out ret_systemno);
                        int.TryParse(sikds.siteno, out ret_siteno);
                        int.TryParse(sikds.host_no, out ret_host_no);
                        cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                        cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = ret_userno  });
                        cmd.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = ret_systemno });
                        cmd.Parameters.Add(new NpgsqlParameter("siteno", DbType.Int32) { Value = ret_siteno });
                        cmd.Parameters.Add(new NpgsqlParameter("host_no", DbType.Int32) { Value = ret_host_no });
                        cmd.Parameters.Add(new NpgsqlParameter("career", DbType.String) { Value = sikds.career });
                        cmd.Parameters.Add(new NpgsqlParameter("type", DbType.String) { Value = sikds.type });
                        cmd.Parameters.Add(new NpgsqlParameter("kaisenid", DbType.String) { Value = sikds.kaisenid });
                        cmd.Parameters.Add(new NpgsqlParameter("telno1", DbType.String) { Value = sikds.telno1 });
                        cmd.Parameters.Add(new NpgsqlParameter("telno2", DbType.String) { Value = sikds.telno2 });
                        cmd.Parameters.Add(new NpgsqlParameter("telno3", DbType.String) { Value = sikds.telno3 });
                        cmd.Parameters.Add(new NpgsqlParameter("isp", DbType.String) { Value = sikds.isp });
                        cmd.Parameters.Add(new NpgsqlParameter("servicetype", DbType.String) { Value = sikds.servicetype });
                        cmd.Parameters.Add(new NpgsqlParameter("serviceid", DbType.String) { Value = sikds.serviceid });
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
                        MessageBox.Show("回線情報のインポート時" + cnt + "件目でエラーが発生しました。" + ex.Message);
                        logger.ErrorFormat("回線情報のインポート時{0}件目でエラーが発生しました。:{1}", i, ex.Message);

                        //transaction.Rollback();
                        return;
                    }

                }
                //終わったらコミットする
                transaction.Commit();
                MessageBox.Show("回線情報のインポート" + i + "件 ");

            }
        }

        private void インターフェイスkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ファイル選択ダイアログ
            string filePath = Disp_FileSelectDlg();
            if (filePath == "")
                return;

            watch_InterfaceDS kds;
            List<watch_InterfaceDS> list_interface = new List<watch_InterfaceDS>();
            try
            {
                TextFieldParser parser = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"));
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(","); // 区切り文字はコンマ

                String username = "";
                String systemname = "";
                String sitename = "";
                String hostname = "";

                while (!parser.EndOfData)
                {
                    kds = new watch_InterfaceDS();
                    string[] cols = parser.ReadFields(); // 1行読み込み

                    //1行目#はコメント
                    string str = "";
                    str = cols[0];
                    if (str.StartsWith("#"))
                        continue;

                    username = cols[13];
                    systemname = cols[11];
                    sitename = cols[9];
                    hostname = cols[7];

                    //カスタマNOの取得
                    kds.userno = getCustomerNo(username, con);
                    if (kds.userno == "")
                    {
                        logger.ErrorFormat("カスタマ通番が取得できませんでした。カスタマ名：" + username);
                        //継続
                        continue;
                    }
                    //システムNOの取得
                    kds.systemno = getSystemNo(kds.userno, systemname, con);
                    if (kds.systemno == "")
                    {
                        logger.ErrorFormat("システム通番が取得できませんでした。");
                        //継続
                        continue;
                    }

                    //拠点NOの取得
                    kds.siteno = getSiteNo(kds.userno, kds.systemno, sitename, con);
                    if (kds.siteno == "")
                    {
                        logger.ErrorFormat("拠点通番が取得できませんでした。");
                        //継続
                        continue;
                    }
                    //ホストNOの取得
                    kds.host_no = getHostNo(kds.userno, kds.systemno, kds.siteno, hostname, con);
                    if (kds.siteno == "")
                    {
                        logger.ErrorFormat("ホスト通番が取得できませんでした。");
                        //継続
                        continue;
                    }

                    kds.interfacename = cols[0];
                    kds.status = "1";
                    kds.type = cols[2];
                    kds.kanshi = cols[3];

                    kds.IPaddress = cols[4];
                    kds.border = cols[5];
                    kds.IPaddressNAT = cols[6];


                    //更新日時
                    kds.chk_date = cols[14];

                    list_interface.Add(kds);
                }
                parser.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("CSVファイルの読み込み時にエラーが発生しました。" + ex.Message);
                logger.ErrorFormat("監視インターフェイスのインポートエラー。CSVファイルの読み込み時にエラーが発生しました。");

                return;
            }

            if (MessageBox.Show(list_interface.Count + "件のインターフェイス情報を登録します。よろしいですか？", "インターフェイス情報インポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //DB接続
            if (con.FullState != ConnectionState.Open) con.Open();

            NpgsqlCommand cmd;

            using (var transaction = con.BeginTransaction())
            {

                //インサート文
                int i = 0;
                string status = "";
                foreach (watch_InterfaceDS sikds in list_interface)
                {
                    if (sikds.status == "有効")
                    {

                        status = "1";
                    }
                    if (sikds.status == "無効")
                    {   
                        status = "0";

                    }

                    cmd = new NpgsqlCommand(@"insert into watch_Interface(interfacename,status,type,kanshi,border,ipaddress,ipaddressnat,biko,host_no,userno,systemno,siteno,chk_date,chk_name_id) 
                    values (:interfacename,:status,:type,:kanshi,:border,:ipaddress,:ipaddressnat,:biko,:host_no,:userno,:systemno,:siteno,:chk_date,:chk_name_id)", con);
                    try
                    {
                        DateTime datet;
                        DateTime.TryParse(sikds.chk_date, out datet);
                        if (sikds.chk_date == null || sikds.chk_date == "")
                            datet = DateTime.Now;



                        Console.WriteLine(datet.ToString());


                        Int32 rowsaffected;

                        //データ登録
                        int ret_userno;
                        int ret_systemno;
                        int ret_siteno;
                        int ret_host_no;

                        int.TryParse(sikds.userno, out ret_userno);
                        int.TryParse(sikds.systemno, out ret_systemno);
                        int.TryParse(sikds.siteno, out ret_siteno);
                        int.TryParse(sikds.host_no, out ret_host_no);
                        cmd.Parameters.Add(new NpgsqlParameter("interfacename", DbType.String) { Value = sikds.interfacename });
                        cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                        cmd.Parameters.Add(new NpgsqlParameter("type", DbType.String) { Value = sikds.type });
                        cmd.Parameters.Add(new NpgsqlParameter("kanshi", DbType.String) { Value = sikds.kanshi });

                        cmd.Parameters.Add(new NpgsqlParameter("border", DbType.String) { Value = sikds.border });
                        cmd.Parameters.Add(new NpgsqlParameter("ipaddress", DbType.String) { Value = sikds.IPaddress });
                        cmd.Parameters.Add(new NpgsqlParameter("ipaddressnat", DbType.String) { Value = sikds.IPaddressNAT });
                        cmd.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = sikds.biko });
                        cmd.Parameters.Add(new NpgsqlParameter("host_no", DbType.Int32) { Value = ret_host_no });
                        cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = ret_userno });
                        cmd.Parameters.Add(new NpgsqlParameter("systemno", DbType.Int32) { Value = ret_systemno });
                        cmd.Parameters.Add(new NpgsqlParameter("siteno", DbType.Int32) { Value = ret_siteno });
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
                        MessageBox.Show("監視インターフェイス情報のインポート時" + i + "件目でエラーが発生しました。" + ex.Message);
                        logger.ErrorFormat("監視インターフェイス情報のインポート時{0}件目でエラーが発生しました。:{1}",i,ex.Message);

                        //transaction.Rollback();
                        return;
                    }

                }
                //終わったらコミットする
                transaction.Commit();

                MessageBox.Show("監視インターフェイス情報" + i + "件 ");
            }
        }
        //集計機能
        private void button5_Click_1(object sender, EventArgs e)
        {
            //インシデントの受付日時から手配日時の時間が15分以上のインシデントを取得する
            Form_incidentSummary icdForm = new Form_incidentSummary();
            icdForm.con = con;
            icdForm.loginDS = loginDS;
            icdForm.Show(this);
        }

        private void m_inc_templete_insert_btn_Click(object sender, EventArgs e)
        {
            Form_inc_templete_insert templeteInsert = new Form_inc_templete_insert();
            templeteInsert.con = con;
            if (userList != null)
                templeteInsert.userList = userList;
            templeteInsert.loginDS = loginDS;
            templeteInsert.ShowDialog(this);
        }

        private void m_inc_templete_update_btn_Click(object sender, EventArgs e)
        {
            Form_inc_templete_update templeteUpdate = new Form_inc_templete_update();
            templeteUpdate.con = con;
            if (userList != null)
                templeteUpdate.userList = userList;
            templeteUpdate.loginDS = loginDS;
            templeteUpdate.ShowDialog(this);
        }
    }
}
