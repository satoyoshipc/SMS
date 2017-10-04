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
    public partial class Form_print : Form
    {
        private string printingText;
        private int printingPosition;
        private Font printFont;


        //ログイン情報
        public opeDS loginDS { get; set; }

        //インシデントリスト
        public List<incidentDS> incidentDSList;
        public List<taskDS> scheduleList;
        public String kubunstr; 

        //DBコネクション
        public NpgsqlConnection con { get; set; }

        public Form_print()
        {
            InitializeComponent();
        }

        //印刷ボタン
        private void button3_Click(object sender, EventArgs e)
        {
            //インシデント一覧の取得
            //印刷する文字列と位置を設定する
            String context = "";
            if(kubunstr == "インシデント対応")
                context = makeList(1);
            else if (kubunstr == "計画作業")
                context = makeList(3);
            else if (kubunstr == "特別対応")
                context = makeList(4);

            printingText = context + Environment.NewLine ;
            printingPosition = 0;
            //印刷に使うフォントを指定する
            printFont = new Font("ＭＳ ゴシック", 12);
            //PrintDocumentオブジェクトの作成
            System.Drawing.Printing.PrintDocument pd =
                new System.Drawing.Printing.PrintDocument();
            //PrintPageイベントハンドラの追加
            pd.PrintPage +=
                new System.Drawing.Printing.PrintPageEventHandler(pd_PrintPage);

            //PrintPreviewDialogオブジェクトの作成
            PrintPreviewDialog ppd = new PrintPreviewDialog();
            //プレビューするPrintDocumentを設定
            ppd.Document = pd;
            //印刷プレビューダイアログを表示する
            ppd.ShowDialog();

            //印刷を開始する
            //pd.Print();

            this.Close();
        }
        private void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (printingPosition == 0)
            {
                //改行記号を'\n'に統一する
                printingText = printingText.Replace("\r\n", "\n");
                printingText = printingText.Replace("\r", "\n");
            }

            //印刷する初期位置を決定
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;

            //1ページに収まらなくなるか、印刷する文字がなくなるかまでループ
            while (e.MarginBounds.Bottom > y + printFont.Height &&
                printingPosition < printingText.Length)
            {
                string line = "";
                for (;;)
                {
                    //印刷する文字がなくなるか、
                    //改行の時はループから抜けて印刷する
                    if (printingPosition >= printingText.Length ||
                        printingText[printingPosition] == '\n')
                    {
                        printingPosition++;
                        break;
                    }
                    //一文字追加し、印刷幅を超えるか調べる
                    line += printingText[printingPosition];
                    if (e.Graphics.MeasureString(line, printFont).Width
                        > e.MarginBounds.Width)
                    {
                        //印刷幅を超えたため、折り返す
                        line = line.Substring(0, line.Length - 1);
                        break;
                    }
                    //印刷文字位置を次へ
                    printingPosition++;
                }
                //一行書き出す
                e.Graphics.DrawString(line, printFont, Brushes.Black, x, y);
                //次の行の印刷位置を計算
                y += (int)printFont.GetHeight(e.Graphics);
            }

            //次のページがあるか調べる
            if (printingPosition >= printingText.Length)
            {
                e.HasMorePages = false;
                //初期化する
                printingPosition = 0;
            }
            else
                e.HasMorePages = true;
        }

        //印刷本文を作成する
        private String makeList(int kubun)
        {
            StringBuilder str_b = new StringBuilder();
            //インシデント
            if (kubun == 1)
            {
                str_b.Append("インシデント対応"+ Environment.NewLine +　"\t\t期間：" + m_start_date.Value.ToString() + "～" + m_end_date.Value.ToString() + Environment.NewLine + Environment.NewLine);
                //ループ
                foreach (incidentDS il in incidentDSList)
                {

                    //未完了のみ
                    if (il.status == "未完了")
                    {
                        str_b.Append("------------------------------------------------------------" + Environment.NewLine);

                        str_b.Append(String.Format("開始：{0,-30}\t カスタマ：{1}" + Environment.NewLine, il.uketukedate, il.username));
                        str_b.Append(String.Format("終了：{0,-30}\t 拠点：{1}" + Environment.NewLine, il.enddate, il.sitename));
                        str_b.Append(String.Format("\t内容：{0}", il.content));
                        str_b.Append(Environment.NewLine);

                    }

                }

            }
            //計画作業
            else if (kubun == 3)
            {

                str_b.Append("計画作業" + Environment.NewLine + "\t\t期間：" + m_start_date.Value.ToString() + "～" + m_end_date.Value.ToString() + Environment.NewLine + Environment.NewLine);
                //ループ
                foreach (taskDS sl in scheduleList)
                {

                    //未完了のみ
                    if (sl.status == "未完了")
                    {
                        if (DateTime.Parse(sl.startdate) <= m_end_date.Value && DateTime.Parse(sl.enddate) >= m_start_date.Value)
                        {

                            str_b.Append("------------------------------------------------------------" + Environment.NewLine);

                            str_b.Append(String.Format("開始：{0,-30}\t カスタマ：{1}" + Environment.NewLine, sl.startdate, sl.username));
                            str_b.Append(String.Format("終了：{0,-30}\t " + Environment.NewLine, sl.enddate));
                            str_b.Append(String.Format("\t内容：{0}", sl.naiyou));
                            str_b.Append(Environment.NewLine);
                        }

                    }


                }
                return str_b.ToString();
            }
            //特別対応
            else if (kubun == 4)
            {

                str_b.Append("特別対応" + Environment.NewLine + "\t\t期間：" + m_start_date.Value.ToString() + "～" + m_end_date.Value.ToString() + Environment.NewLine + Environment.NewLine);
                //ループ
                foreach (taskDS sl in scheduleList)
                {

                    //未完了のみ
                    if (sl.status == "未完了")
                    {
                        str_b.Append("------------------------------------------------------------" + Environment.NewLine);

                        str_b.Append(String.Format("開始：{0,-30}\t カスタマ：{1}" + Environment.NewLine, sl.startdate, sl.username));
                        str_b.Append(String.Format("終了：{0,-30}\t " + Environment.NewLine, sl.enddate));
                        str_b.Append(String.Format("\t内容：{0}", sl.naiyou));
                        str_b.Append(Environment.NewLine);

                    }

                }


            }
            return str_b.ToString();
        }


    //表示前
        private void Form_print_Load(object sender, EventArgs e)
        {
            m_schedule_Type.Text = kubunstr;
            DateTime dt = DateTime.Now;
            //本日の00:00:00を設定する
            m_start_date.Value = new DateTime(dt.Year, dt.Month, dt.Day, 00, 00, 00);

            //本日の00:00:00を設定する
            m_end_date.Value = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);

        }
        //本日チェックボックスが選択されたとき
        private void m_todaychk_CheckedChanged(object sender, EventArgs e)
        {
            //今日
            if (m_todaychk.Checked)
            {
                DateTime dt = DateTime.Now;
                
                //本日の00:00:00を設定する
                m_start_date.Value = new DateTime(dt.Year,dt.Month,dt.Day,00,00,00);

                //本日の00:00:00を設定する
                m_end_date.Value = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);

            }

        }
        //キャンセルボタン
        private void button2_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }
    }
}
