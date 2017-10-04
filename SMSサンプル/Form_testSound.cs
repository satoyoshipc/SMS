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
    public partial class Form_testSound : Form
    {
        private System.Media.SoundPlayer player = null;

        //サウンドパスプロパティ
        public String strParam { get; set; }

        public void play(string pass) { PlaySound(pass); }


    //フォームインスタンスを保持するフィールド
    private static Form_testSound _instance;

        //フォームにアクセスするためのプロパティ
        public static Form_testSound Instance
        {
            get
            {
                //_instanseがnullまたは破棄されているときは新しくインスタンスを生成
                //新インスタンスを生成する
                if (_instance == null || _instance.IsDisposed)
                    _instance = new Form_testSound();
                return _instance;
            }
        }

        public Form_testSound()
        {
            InitializeComponent();
        }

        //再生ボタン
        private void button1_Click(object sender, EventArgs e)
        {
            PlaySound(strParam);
        }
        //再生
        private void Form_testSound_Load(object sender, EventArgs e)
        {
            this.TopMost =true;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            //画面右下
            int Screenheigth = System.Windows.Forms.Screen.GetWorkingArea(this).Height;
            int ScreenWidth = System.Windows.Forms.Screen.GetWorkingArea(this).Width;
            int AppWidth = this.Width;
            int AppHeight = this.Height;

            int AppLeftXPos = ScreenWidth - AppWidth;
            int AppLeftYPos = Screenheigth - AppHeight;

            Rectangle tempRect = new Rectangle(AppLeftXPos, AppLeftYPos, AppWidth, AppHeight);
            this.DesktopBounds = tempRect;

            PlaySound(strParam);
        }
        //WAVEファイルを再生する
        private void PlaySound(string waveFile)
        {
            try { 

                //再生されているときは止める
                if (player != null)
                    StopSound();

                //読み込む
                player = new System.Media.SoundPlayer(waveFile);

                //非同期再生する
                player.Play();

                //次のようにすると、ループ再生される
                player.PlayLooping();

                //次のようにすると、最後まで再生し終えるまで待機する
                //player.PlaySync();
            }
            catch(Exception ex)
            {
                MessageBox.Show("サウンドファイルが再生できません。" + ex.Message, "テスト再生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        //再生されている音を止める
        private void StopSound()
        {
            if (player != null)
            {
                player.Stop();
                player.Dispose();
                player = null;
            }
        }

        //停止
        private void button2_Click(object sender, EventArgs e)
        {
            StopSound();
        }
        //画面閉じたら音も消す
        private void Form_testSound_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopSound();
        }
    }
}
