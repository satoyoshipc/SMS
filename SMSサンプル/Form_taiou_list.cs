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
    public partial class Form_taiou_list : Form
    {

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        //ListViewのソートの際に使用する
        private Class_ListViewColumnSorter _columnSorter;

        //DBコネクション
        public NpgsqlConnection con { get; set; }

        //アラームリスト
        public List<alermDS> almList { get; set; }


        public Form_taiou_list()
        {
            InitializeComponent();
        }

        //検索ボタン
        private void m_kensaku_Click(object sender, EventArgs e)
        {
            disp_alermList();
        }
        /// <summary>
        /// 表示前処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_taiou_list_Load(object sender, EventArgs e)
        {
            _columnSorter = new Class_ListViewColumnSorter();
            m_taioulist.ListViewItemSorter = _columnSorter;


        }

        //対応状況を取得する
        private void disp_alermList()
        {
            m_taioulist.Clear();
            DISP_dataSet dset = new DISP_dataSet();
            Dictionary<string, string> param_dict = new Dictionary<string, string>();
            Class_Detaget dg = new Class_Detaget();


            param_dict["alertdatetime_Before"] = m_alertdatetime_Before.Value.ToString("yyyy-MM-dd HH:mm");
            param_dict["alertdatetime_After"] = m_alertdatetime_After.Value.ToString("yyyy-MM-dd HH:mm");
            param_dict["taiou"] = m_taiouchk.Checked.ToString();
            if (m_taiou.Text != "")
                param_dict["opeid"] = m_taiou.Text;


            this.m_taioulist.FullRowSelect = true;
            this.m_taioulist.HideSelection = false;
            this.m_taioulist.HeaderStyle = ColumnHeaderStyle.Clickable;

            this.m_taioulist.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
            this.m_taioulist.Columns.Insert(1, "スケジュールタイプ", 90, HorizontalAlignment.Left);
            this.m_taioulist.Columns.Insert(2, "繰り返し区分", 60, HorizontalAlignment.Left);
            this.m_taioulist.Columns.Insert(3, "アラーム日時", 120, HorizontalAlignment.Left);
            this.m_taioulist.Columns.Insert(4, "対応者", 90, HorizontalAlignment.Left);
            this.m_taioulist.Columns.Insert(5, "対応日時", 120, HorizontalAlignment.Left);
            this.m_taioulist.Columns.Insert(6, "タイマーID", 30, HorizontalAlignment.Left);
            this.m_taioulist.Columns.Insert(7, "タイマー名", 120, HorizontalAlignment.Left);
            this.m_taioulist.Columns.Insert(8, "内容", 80, HorizontalAlignment.Left);
            this.m_taioulist.Columns.Insert(9, "カスタマ名", 80, HorizontalAlignment.Left);




            almList = dg.gettaiouRireki(param_dict,con);
            if (almList != null && almList.Count > 0)
            {
                //リストに表示
                foreach (alermDS ads in almList)
                {
                    ListViewItem itemx1 = new ListViewItem();
                    itemx1.Text = ads.schedule_no;


                    //スケジュールタイプ
                    string str = "";
                    if (ads.schedule_type == "1")
                    {
                        str = "インシデント対応";
                    }
                    else if (ads.schedule_type == "2")
                    {
                        str = "定期作業";
                    }
                    else if (ads.schedule_type == "3")
                    {
                        str = "計画作業";
                    }
                    else if (ads.schedule_type == "4")
                    {
                        str = "特別対応";
                    }

                    itemx1.SubItems.Add(str);

                    //繰り返しタイプ //1:1回、2:1時間毎、3:日毎、4:週毎、5:月毎
                    if (ads.repeat_type == "1")
                    {
                        str = "1回";
                    }
                    else if (ads.repeat_type == "2")
                    {
                        str = "時間毎";
                    }
                    else if (ads.repeat_type == "3")
                    {
                        str = "日毎";
                    }
                    else if (ads.repeat_type == "4")
                    {
                        str = "週毎";
                    }
                    else if (ads.repeat_type == "5")
                    {
                        str = "月毎";
                    }
                    itemx1.SubItems.Add(str);

                    itemx1.SubItems.Add(ads.alertdatetime);
                    itemx1.SubItems.Add(ads.opeid);
                    itemx1.SubItems.Add(ads.taioudate);
                    itemx1.SubItems.Add(ads.timerid);
                    itemx1.SubItems.Add(ads.timername);
                    itemx1.SubItems.Add(ads.naiyou);
                    itemx1.SubItems.Add(ads.username);

                    this.m_taioulist.Items.Add(itemx1);
                }
            }
        

        }
        //削除
        private void m_deleteBtn_Click(object sender, EventArgs e)
        {
            int count = m_taioulist.SelectedItems.Count;

            //確認メッセージ
            if (MessageBox.Show("一覧に選択された行 " + count + "件 の削除を行います。" + Environment.NewLine +
                "よろしいですか？", "アラーム削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;


            int ret = deleteTimer();
            if (ret == -1)
            {

                return;
            }

            //リストの表示上からけす
            foreach (ListViewItem item in m_taioulist.SelectedItems)
            {
                m_taioulist.Items.Remove(item);
            }
        }
        //削除
        private int deleteTimer()
        {

            string scheduleno = "";
            string alertDtString = "";
            string timerID = "";

            
            int ret = 0;

            if (con.FullState != ConnectionState.Open) con.Open();



            string sql = "DELETE FROM timer_taiou where schedule_no =:no AND alertdatetime=:alertdatetime AND timerID=:timerID";

            using (var transaction = con.BeginTransaction())
            {
                
                foreach (ListViewItem item in m_taioulist.SelectedItems)
                {
                    scheduleno = item.SubItems[0].Text;
                    alertDtString  = item.SubItems[3].Text;

                    //文字列からDatetimeに変換
                    System.DateTime dd1 = DateTime.ParseExact(alertDtString, "yyyy/MM/dd HH:mm:ss", null);

                    timerID = item.SubItems[6].Text;

                    var command = new NpgsqlCommand(@sql, con);
                    command.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = int.Parse(scheduleno) });
                    command.Parameters.Add(new NpgsqlParameter("alertdatetime",DbType.DateTime) { Value = dd1 });
                    command.Parameters.Add(new NpgsqlParameter("timerID", DbType.Int32) { Value = timerID });

                    Int32 rowsaffected;
                    try
                    {
                        //削除処理
                        rowsaffected = command.ExecuteNonQuery();

                        if (rowsaffected < 1)
                        {
                            MessageBox.Show("削除できませんでした。:" + scheduleno, "アラーム情報削除");
                            logger.ErrorFormat("アラーム情報削除エラー。 スケジュール番号:{0}", sql, scheduleno);

                            transaction.Rollback();
                            return -1;
                        }
                        else
                        {
                            ret = 1;

                        }
                    }
                    catch (Exception ex)
                    {
                        //エラー時メッセージ表示
                        MessageBox.Show("アラーム削除時エラーが発生しました。 " + ex.Message);
                        logger.ErrorFormat("アラーム情報削除エラー。 スケジュール番号:{0}", sql, scheduleno);

                        if (transaction.Connection != null) transaction.Rollback();
                        return -1;
                    }
                }
                if (ret == 1)
                {
                    MessageBox.Show("削除完了しました。", "アラーム情報削除");
                    logger.ErrorFormat("アラーム情報削除完了。 スケジュール番号:{0}", sql, scheduleno);

                    transaction.Commit();
                }
            }
            return ret;
        }

        private void m_taioulist_ColumnClick(object sender, ColumnClickEventArgs e)
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
            m_taioulist.Sort();
        }
    }
}
