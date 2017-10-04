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
    public partial class Form_DispMail : Form
    {
        private string printingText;
        private int printingPosition;
        private Font printFont;

        public Form_DispMail()
        {
            InitializeComponent();
        }
        public mailTempleteDS mailtempDS { get; set; }
        
        //表示前処理
        private void Form_DispMail_Load(object sender, EventArgs e)
        {
            //m_subject.Text = mailtempDS.subject;
            m_body.Text = mailtempDS.body;
            //m_attach1.Text = mailtempDS.attach1;
            //m_attach2.Text = mailtempDS.attach2;
            //m_attach3.Text = mailtempDS.attach3;
            //m_attach4.Text = mailtempDS.attach4;
            //m_attach5.Text = mailtempDS.attach5;
            //m_To.Text = mailtempDS.Toaddress;
            //m_Cc.Text = mailtempDS.CcAddress;
            //m_Bcc.Text = mailtempDS.BccAddress;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //印刷する文字列と位置を設定する
            printingText = m_body.Text + Environment.NewLine + m_body.Text;
            printingPosition = 0;
            //印刷に使うフォントを指定する
            printFont = new Font("ＭＳ Ｐゴシック", 10);
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
        }
        private void pd_PrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e)
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
    }
}
