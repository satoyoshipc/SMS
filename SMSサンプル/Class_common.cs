using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMSサンプル
{
    class Class_common
    {

        //DB接続
        public NpgsqlConnection DB_connection() {

            StringBuilder sb = new StringBuilder();

            sb.Append("Server=" + System.Configuration.ConfigurationManager.AppSettings["dbserverip"] + "; ");
            sb.Append("Port=" + System.Configuration.ConfigurationManager.AppSettings["port"] + ";");
            sb.Append("User Id=" + System.Configuration.ConfigurationManager.AppSettings["userid"] + ";");
            sb.Append("Password=" + System.Configuration.ConfigurationManager.AppSettings["password"] + ";");
            sb.Append("Database=" + System.Configuration.ConfigurationManager.AppSettings["dbname"]);

            NpgsqlConnection con = null;

            String connString = sb.ToString();
            try { 
                con = new NpgsqlConnection(connString);
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB接続エラー" + ex.Message);
            }
            return con;
        }
        //ファイル選択ダイアログを表示
        public string Disp_FileSelectDlg(string file_extention = "*")
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
            ofd.Filter =
                "すべてのファイル(*." + file_extention + ")|*." + file_extention;
            //[ファイルの種類]ではじめに
            //「すべてのファイル」が選択されているようにする
            ofd.FilterIndex = 2;
            //タイトルを設定する
            ofd.Title = "開くファイルを選択してください";
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
    }
}
