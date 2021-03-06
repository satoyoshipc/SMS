﻿using Npgsql;
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

    public partial class Form_systemInsert : Form
    {
        //DBコネクション
        public NpgsqlConnection con { get; set; }
        public opeDS loginDS { get; set; }

        //カスタマ
        public List<userDS> userList { get; set; }


        public Form_systemInsert()
        {
            InitializeComponent();
        }

        //表示前処理
        private void Form_systemInsert_Load(object sender, EventArgs e)
        {
            m_statusCombo.Text = "有効";
            m_idlabel.Text = loginDS.opeid;
            m_labelinputOpe.Text = loginDS.lastname + loginDS.fastname;


            DataTable cutomerTable = new DataTable();
            cutomerTable.Columns.Add("ID", typeof(string));
            cutomerTable.Columns.Add("NAME", typeof(string));

            if (userList == null)
                return;
            //カスタマ情報を取得する
            foreach (userDS v in userList)
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

        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //選択の変更
        private void m_usernameCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            try { 
                m_userID.Text = m_usernameCombo.SelectedValue.ToString();
            }
            catch (Exception ex )
            {
                MessageBox.Show("カスタマ情報の取得に失敗しました。" + ex.Message);
            }

        }
        //登録ボタン
        private void button3_Click(object sender, EventArgs e)
        {
            //カスタマ名
            if (m_usernameCombo.Text == "")
            {
                MessageBox.Show("カスタマ名が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //システム名
            if (m_systemname.Text == "")
            {
                MessageBox.Show("システム名が入力されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //確認
            if (MessageBox.Show("システム情報を登録します。よろしいですか？", "登録確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string systemname = m_systemname.Text;
            string systemkana = m_systemnamekana.Text;
            string biko = m_biko.Text;
            string userno = m_userID.Text;

            //ステータス
            string status = "";
            if (m_statusCombo.SelectedIndex == 0)
                //有効
                status = "1";
            else
                //無効
                status = "0";

            //DB接続
            NpgsqlCommand cmd;
            try
            {
                if (con.FullState != ConnectionState.Open) con.Open();
                Int32 rowsaffected;
                //データ登録
                cmd = new NpgsqlCommand(@"insert into system(systemname,systemkana,status,biko,userno,chk_name_id) 
                    values ( :systemname,:systemkana,:biko,:userno,:chk_name_id)", con);
                cmd.Parameters.Add(new NpgsqlParameter("systemname", DbType.String) { Value = systemname });
                cmd.Parameters.Add(new NpgsqlParameter("systemkana", DbType.String) { Value = systemkana });
                cmd.Parameters.Add(new NpgsqlParameter("systemkana", DbType.String) { Value = systemkana });
                cmd.Parameters.Add(new NpgsqlParameter("status", DbType.String) { Value = status });
                cmd.Parameters.Add(new NpgsqlParameter("biko", DbType.String) { Value = biko });
                cmd.Parameters.Add(new NpgsqlParameter("userno", DbType.Int32) { Value = userno });
                cmd.Parameters.Add(new NpgsqlParameter("chk_name_id", DbType.String) { Value = m_idlabel.Text });
                rowsaffected = cmd.ExecuteNonQuery();

                if (rowsaffected != 1)
                {
                    MessageBox.Show("登録できませんでした。", "システム登録");
                }
                else
                {
                    //登録成功
                    MessageBox.Show("登録完了", "システム登録");
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("システム登録時エラー " + ex.Message);
                return;
            }

        }
    }
}
