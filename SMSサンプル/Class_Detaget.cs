using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMSサンプル
{
    class Class_Detaget
    {




        //ユーザデータ一覧を取得する
        public List<userDS> getUserList()
        {

            NpgsqlCommand cmd;
            userDS ds;
            List<userDS> retList = null;
            //DB接続
            Class_common common = new Class_common();
            using (var con = common.DB_connection())
            {
                try
                {
                    con.Open();


                    cmd = new NpgsqlCommand(@"select * from user_tbl", con);
                    var dataReader = cmd.ExecuteReader();

                    //ユーザ情報の取得
                    retList = new List<userDS>();

                    while (dataReader.Read())
                    {

                        ds = new userDS();

                        ds.userno = dataReader["userno"].ToString();
                        ds.username = dataReader["username"].ToString();
                        ds.username_kana = dataReader["username_kana"].ToString();
                        ds.username_sum = dataReader["username_sum"].ToString();
                        ds.status = dataReader["status"].ToString();
                        ds.report_status = dataReader["report_status"].ToString();
                        ds.biko = dataReader["biko"].ToString();
                        ds.chk_date = dataReader["chk_date"].ToString();
                        ds.chk_name_id = dataReader["chk_name_id"].ToString();


                        retList.Add(ds);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("ユーザ情報一覧取得エラー " + ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return retList;
            }
        }

        //システム情報一覧を取得する(引数 検索条件)
        public List<systemDS> getSystemList(string searchcom, Boolean searchflg = false)
        {

            string searchstring = "";
            if (searchflg)
            {
                searchstring = " where userno = " + searchcom;

            }
            NpgsqlCommand cmd;
            systemDS ds;
            List<systemDS> retList = null;
            //DB接続
            Class_common common = new Class_common();
            using (var con = common.DB_connection())
            {
                try
                {
                    con.Open();

                    //SELECT実行
                    cmd = new NpgsqlCommand(@"select * from system" + searchstring, con);
                    var dataReader = cmd.ExecuteReader();

                    //システム情報の取得
                    retList = new List<systemDS>();

                    while (dataReader.Read())
                    {

                        ds = new systemDS();

                        ds.systemno = dataReader["systemno"].ToString();
                        ds.systemname = dataReader["systemname"].ToString();
                        ds.systemkana = dataReader["systemkana"].ToString();
                        ds.biko = dataReader["biko"].ToString();
                        ds.userno = dataReader["userno"].ToString();
                        ds.chk_date = dataReader["chk_date"].ToString();
                        ds.chk_name_id = dataReader["chk_name_id"].ToString();


                        retList.Add(ds);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("システム情報一覧取得エラー " + ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
                return retList;
            }
        }
        //拠点情報一覧を取得する(引数 検索条件)
        public List<siteDS> getSiteList(string systemid, Boolean searchflg = false)
        {

            string searchstring = "";
            if (searchflg)
            {
                searchstring = " where systemno=" + systemid;

            }
            NpgsqlCommand cmd;
            siteDS ds;
            List<siteDS> retList = null;
            //DB接続
            Class_common common = new Class_common();
            using (var con = common.DB_connection())
            {
                try
                {
                    con.Open();

                    //SELECT実行
                    cmd = new NpgsqlCommand(@"select * from site" + searchstring, con);
                    var dataReader = cmd.ExecuteReader();

                    //システム情報の取得
                    retList = new List<siteDS>();

                    while (dataReader.Read())
                    {

                        ds = new siteDS();

                        ds.siteno = dataReader["siteno"].ToString();
                        ds.sitename = dataReader["sitename"].ToString();
                        ds.address1 = dataReader["address1"].ToString();
                        ds.address2 = dataReader["address2"].ToString();
                        ds.telno = dataReader["telno"].ToString();
                        ds.status = dataReader["status"].ToString();
                        ds.systemno = dataReader["systemno"].ToString();
                        ds.userno = dataReader["userno"].ToString();
                        ds.chk_date = dataReader["chk_date"].ToString();
                        ds.chk_name_id = dataReader["chk_name_id"].ToString();


                        retList.Add(ds);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("システム情報一覧取得エラー " + ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
                return retList;
            }
        }

        //機器情報一覧を取得する(引数 検索条件)
        public List<hostDS> getHostList(string siteID, Boolean searchflg = false)
        {

            string searchstring = "";
            if (searchflg)
            {
                searchstring = " where siteno=" + siteID;

            }
            NpgsqlCommand cmd;
            hostDS ds;
            List<hostDS> retList = null;
            //DB接続
            Class_common common = new Class_common();
            using (var con = common.DB_connection())
            {
                try
                {
                    con.Open();

                    //SELECT実行
                    cmd = new NpgsqlCommand(@"select * from host" + searchstring, con);
                    var dataReader = cmd.ExecuteReader();

                    //機器情報の取得
                    retList = new List<hostDS>();

                    while (dataReader.Read())
                    {

                        ds = new hostDS();

                        ds.host_no = dataReader["host_no"].ToString();
                        ds.hostname = dataReader["hostname"].ToString();
                        ds.hostname_ja = dataReader["hostname_ja"].ToString();
                        ds.status = dataReader["status"].ToString();
                        ds.device = dataReader["device"].ToString();
                        ds.location = dataReader["location"].ToString();
                        ds.usefor = dataReader["usefor"].ToString();
                        ds.kansiStartdate = dataReader["kansiStartdate"].ToString();
                        ds.kansiEndsdate = dataReader["kansiEndsdate"].ToString();
                        ds.hosyukanri = dataReader["hosyukanri"].ToString();
                        ds.hosyuinfo = dataReader["hosyuinfo"].ToString();
                        ds.biko = dataReader["biko"].ToString();
                        ds.siteno = dataReader["siteno"].ToString();
                        ds.userno = dataReader["userno"].ToString();
                        ds.chk_name_id = dataReader["chk_name_id"].ToString();
                        ds.chk_date = dataReader["chk_date"].ToString();


                        retList.Add(ds);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("機器情報一覧取得エラー " + ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
                return retList;
            }
        }

        //回線情報の取得
        public void getSelectKaisenInfo(DataTable dt, DISP_dataSet retDS, NpgsqlConnection conn)
        {


            String sql = "select * from Kaisen,ope where ope.opeid = Kaisen.chk_name_id ";

            String param = "";

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {

                   param += " or userno=" + row[0];

                    sql += param;
                }
            }
            NpgsqlCommand cmd;
            kaisenDS k_ds;

            List<kaisenDS> kaisen_List = null;

            //DB接続
            Class_common common = new Class_common();
            try
            {
                //conn.Open();

                //SELECT実行
                cmd = new NpgsqlCommand(@sql, conn);
                var dataReader = cmd.ExecuteReader();

                //構成情報の取得
                kaisen_List = new List<kaisenDS>();
                while (dataReader.Read())
                {

                    k_ds = new kaisenDS();

                    k_ds.kaisenno = dataReader["kaisenno"].ToString();
                    k_ds.status = dataReader["status"].ToString();
                    k_ds.career = dataReader["career"].ToString();
                    k_ds.type = dataReader["type"].ToString();
                    k_ds.kaisenid = dataReader["kaisenid"].ToString();
                    k_ds.isp = dataReader["isp"].ToString();
                    k_ds.servicetype = dataReader["servicetype"].ToString();
                    k_ds.serviceid = dataReader["serviceid"].ToString();
                    k_ds.chk_name_id = dataReader["lastname"].ToString();
                    k_ds.chk_date = dataReader["chk_date"].ToString();

                    kaisen_List.Add(k_ds);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("回線情報の取得に失敗しました。" + ex.Message, "回線情報検索",MessageBoxButtons.OK,MessageBoxIcon.Error);

            }
            retDS.kaisen_L = kaisen_List;

            
        }
        //システム情報の取得
        public DISP_dataSet getSelectUser(Dictionary<string, string> param_dict, NpgsqlConnection conn)
        {
            DISP_dataSet displist = new DISP_dataSet();

            String sql = "select u.userno,u.username,u.username_kana,u.username_sum,u.status user_status,u.report_status,u.biko,u.chk_date user_chk_date,o.lastname user_chk_name_id "+
                        "from user_tbl u,ope o where  o.opeid = u.chk_name_id ";

            String param = "";

            if (param_dict.Count > 0)
            {
                foreach (KeyValuePair<string, string> vdict in param_dict)
                {

                    param += " and u." + vdict.Key + "='" + vdict.Value + "'";
                }

                sql += param;
            }

            NpgsqlCommand cmd;
            userDS u_ds;

            List<userDS> user_List = null;

            //DB接続
            Class_common common = new Class_common();
            try
            {
                conn.Open();

                //SELECT実行
                cmd = new NpgsqlCommand(@sql, conn);
                var dataReader = cmd.ExecuteReader();

                //構成情報の取得
                user_List = new List<userDS>();

                while (dataReader.Read())
                {

                    //ユーザ情報の取得
                    u_ds = new userDS();
                    u_ds.userno = dataReader["userno"].ToString();
                    u_ds.username = dataReader["username"].ToString();
                    u_ds.username_kana = dataReader["username_kana"].ToString();
                    u_ds.username_sum = dataReader["username_sum"].ToString();
                    u_ds.status = dataReader["user_status"].ToString();
                    u_ds.report_status = dataReader["report_status"].ToString();
                    u_ds.biko = dataReader["biko"].ToString();
                    u_ds.chk_date = dataReader["user_chk_date"].ToString();
                    u_ds.chk_name_id = dataReader["user_chk_name_id"].ToString();
                    user_List.Add(u_ds);


                }

                displist.user_L = user_List;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ユーザ情報一覧取得エラー " + ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return displist;
        }

        //システム情報の取得
        public DISP_dataSet getSelectSystem(Dictionary<string, string> param_dict, NpgsqlConnection conn)
        {
            DISP_dataSet displist = new DISP_dataSet();

            String sql = "select sys.systemno,sys.systemname,sys.systemkana,sys.biko,sys.chk_date system_chk_date,o.lastname system_chk_name_id "+
                        "from system sys,ope o where o.opeid = sys.chk_name_id ";

            String param = "";

            if (param_dict.Count > 0)
            {
                foreach (KeyValuePair<string, string> vdict in param_dict)
                {

                    param += " and sys." + vdict.Key + "='" + vdict.Value + "'";
                }

                sql += param;
            }

            NpgsqlCommand cmd;
            systemDS sys_ds;

            List<systemDS> system_List = null;

            //DB接続
            Class_common common = new Class_common();
            try
            {
                conn.Open();

                //SELECT実行
                cmd = new NpgsqlCommand(@sql, conn);
                var dataReader = cmd.ExecuteReader();

                //構成情報の取得
                system_List = new List<systemDS>();

                while (dataReader.Read())
                {


                    //システム情報の取得
                    sys_ds = new systemDS();
                    sys_ds.systemno = dataReader["systemno"].ToString();
                    sys_ds.systemname = dataReader["systemname"].ToString();
                    sys_ds.systemkana = dataReader["systemkana"].ToString();
                    sys_ds.biko = dataReader["biko"].ToString();
                    sys_ds.chk_date = dataReader["system_chk_date"].ToString();
                    sys_ds.chk_name_id = dataReader["system_chk_name_id"].ToString();
                    system_List.Add(sys_ds);


                }

                displist.system_L = system_List;
            }
            catch (Exception ex)
            {
                MessageBox.Show("システム情報一覧取得エラー " + ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return displist;
        }

        //拠点情報の取得
        public DISP_dataSet getSelectSite(Dictionary<string, string> param_dict, NpgsqlConnection conn)
        {
            DISP_dataSet displist = new DISP_dataSet();

            String sql = "select s.siteno,s.sitename,s.address1,s.address2,s.telno,s.status site_STATUS,s.biko,s.chk_date site_chk_date,o.lastname site_chk_name_id "+
                         "from site s,ope o where o.opeid = s.chk_name_id ";
            String param = "";

            if (param_dict.Count > 0)
            {
                foreach (KeyValuePair<string, string> vdict in param_dict)
                {

                    param += " and s." + vdict.Key + "='" + vdict.Value + "'";
                }

                sql += param;
            }

            NpgsqlCommand cmd;
            siteDS s_ds;

            List<siteDS> site_List = null;

            //DB接続
            Class_common common = new Class_common();
            try
            {
                conn.Open();

                //SELECT実行
                cmd = new NpgsqlCommand(@sql, conn);
                var dataReader = cmd.ExecuteReader();

                //構成情報の取得
                site_List = new List<siteDS>();

                while (dataReader.Read())
                {

                    //拠点情報の取得
                    s_ds = new siteDS();
                    s_ds.siteno = dataReader["siteno"].ToString();
                    s_ds.sitename = dataReader["sitename"].ToString();
                    s_ds.address1 = dataReader["address1"].ToString();
                    s_ds.address2 = dataReader["address2"].ToString();
                    s_ds.telno = dataReader["telno"].ToString();
                    s_ds.status = dataReader["site_status"].ToString();
                    s_ds.biko = dataReader["biko"].ToString();
                    s_ds.chk_date = dataReader["site_chk_date"].ToString();
                    s_ds.chk_name_id = dataReader["site_chk_name_id"].ToString();
                    site_List.Add(s_ds);


                }

                displist.site_L = site_List;
            }
            catch (Exception ex)
            {
                MessageBox.Show("拠点情報一覧取得エラー " + ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return displist;
        }
        //監視ホスト
        public DISP_dataSet getSelectHost(Dictionary<string, string> param_dict, NpgsqlConnection conn)
        {
            DISP_dataSet displist = new DISP_dataSet();

            String sql = "select h.userno,h.systemno,h.siteno,h.host_no,h.hostname,h.hostname_ja,h.status host_status, h.device,h.location,h.usefor,h.kansiStartdate,h.kansiEndsdate,h.hosyukanri,h.hosyuinfo,h.biko,h.chk_date host_chk_date, o.lastname host_chk_name_id " +
                        "from host h,ope o where o.opeid = h.chk_name_id ";
            String param = "";

            if (param_dict.Count > 0)
            {
                foreach (KeyValuePair<string, string> vdict in param_dict)
                {
                    param += " and h." + vdict.Key + "='" + vdict.Value + "'";
                }

                sql += param;
            }

            NpgsqlCommand cmd;
            hostDS h_ds;

            List<hostDS> host_List = null;

            //DB接続
            Class_common common = new Class_common();
            try
            {
                conn.Open();

                //SELECT実行
                cmd = new NpgsqlCommand(@sql, conn);
                var dataReader = cmd.ExecuteReader();

                //構成情報の取得
                host_List = new List<hostDS>();

                while (dataReader.Read())
                {

                    //機器情報の取得
                    h_ds = new hostDS();
                    h_ds.host_no = dataReader["host_no"].ToString();
                    h_ds.hostname = dataReader["hostname"].ToString();
                    h_ds.hostname_ja = dataReader["hostname_ja"].ToString();
                    h_ds.status = dataReader["host_status"].ToString();
                    h_ds.device = dataReader["device"].ToString();
                    h_ds.location = dataReader["location"].ToString();
                    h_ds.usefor = dataReader["usefor"].ToString();
                    h_ds.kansiStartdate = dataReader["kansiStartdate"].ToString();
                    h_ds.kansiEndsdate = dataReader["kansiEndsdate"].ToString();
                    h_ds.hosyukanri = dataReader["hosyukanri"].ToString();
                    h_ds.hosyuinfo = dataReader["hosyuinfo"].ToString();
                    h_ds.biko = dataReader["biko"].ToString();
                    h_ds.chk_date = dataReader["host_chk_date"].ToString();
                    h_ds.chk_name_id = dataReader["host_chk_name_id"].ToString();
                    host_List.Add(h_ds);

                }

                displist.host_L = host_List;
            }
            catch (Exception ex)
            {
                MessageBox.Show("機器情報一覧取得エラー " + ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return displist;


        }
        //監視インターフェイス
        public DISP_dataSet getSelectInterface(Dictionary<string, string> param_dict, NpgsqlConnection conn)
        {
            DISP_dataSet displist = new DISP_dataSet();
                
            String sql = "select w.userno,w.systemno,w.siteno,w.host_no,w.kennshino,w.interfacename,w.status watch_status,w.type,w.kanshi,w.start_date,w.end_date,w.border,w.IPaddress,w.IPaddressNAT,w.chk_date watch_chk_date,o.lastname watch_chk_name_id " + 
                        "from watch_interface w,ope o where o.opeid = w.chk_name_id ";
            String param = "";

            if (param_dict.Count > 0){
                foreach (KeyValuePair<string, string> vdict in param_dict) {

                    if (vdict.Key == "IPaddress")
                        param += " and w.IPaddress='" + vdict.Value + "' or w.IPaddressNAT='" + vdict.Value + "'";
                }

                sql += param;
            }

            NpgsqlCommand cmd;
            watch_InterfaceDS w_ds;

            List<watch_InterfaceDS> interface_List = null;

            //DB接続
            Class_common common = new Class_common();
            try
            {
                conn.Open();

                //SELECT実行
                cmd = new NpgsqlCommand(@sql, conn);
                var dataReader = cmd.ExecuteReader();

                //構成情報の取得
                interface_List = new List<watch_InterfaceDS>();

                while (dataReader.Read())
                {







                    //監視インターフェース情報の取得
                    w_ds = new watch_InterfaceDS();
                    w_ds.userno = dataReader["userno"].ToString();
                    w_ds.systemno = dataReader["systemno"].ToString();
                    w_ds.siteno = dataReader["siteno"].ToString();
                    w_ds.host_no = dataReader["host_no"].ToString();
                    w_ds.kennshino = dataReader["kennshino"].ToString();
                    w_ds.interfacename = dataReader["interfacename"].ToString();
                    w_ds.status = dataReader["watch_status"].ToString();
                    w_ds.type = dataReader["type"].ToString();
                    w_ds.kanshi = dataReader["kanshi"].ToString();
                    w_ds.start_date = dataReader["start_date"].ToString();
                    w_ds.end_date = dataReader["end_date"].ToString();
                    w_ds.border = dataReader["border"].ToString();
                    w_ds.IPaddress = dataReader["IPaddress"].ToString();
                    w_ds.IPaddressNAT = dataReader["IPaddressNAT"].ToString();
                    w_ds.chk_date = dataReader["watch_chk_date"].ToString();
                    w_ds.chk_name_id = dataReader["watch_chk_name_id"].ToString();
                    interface_List.Add(w_ds);
                }

                displist.watch_L = interface_List;
            }
            catch (Exception ex)
            {
                MessageBox.Show("構成情報一覧取得エラー " + ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return displist;

        }
        //作業情報を取得する
        public void getSelectSagyoInfo(DataTable dt, DISP_dataSet retDS, NpgsqlConnection conn)
        {
            int i = 0;

            String sql = "select * from task_info ";

            String param = "";

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (i == 0)
                    {
                        param += "where userno=" + row[0];

                    }
                    else if (i > 0)
                    {
                        param += " or userno=" + row[0];
                    }
                    i++;
                    sql += param;
                }
            }
            NpgsqlCommand cmd;
            sagyoDS sa_ds;

            List<sagyoDS> sagyo_List = null;

            //DB接続
            Class_common common = new Class_common();
            try
            {
                //conn.Open();

                //SELECT実行
                cmd = new NpgsqlCommand(@sql, conn);
                var dataReader = cmd.ExecuteReader();

                //構成情報の取得
                sagyo_List = new List<sagyoDS>();
                while (dataReader.Read())
                {

                    sa_ds = new sagyoDS();
                    sa_ds.taskno = dataReader["taskno"].ToString();
                    sa_ds.text = dataReader["text"].ToString();
                    sa_ds.start_date = dataReader["start_date"].ToString();
                    sa_ds.end_date= dataReader["end_date"].ToString();
                    sa_ds.chk_name_id = dataReader["chk_name_id"].ToString();
                    sa_ds.chk_date = dataReader["chk_date"].ToString();

                    sagyo_List.Add(sa_ds);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("作業情報の取得に失敗しました。" + ex.Message, "作業情報検索", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            retDS.sagyo_L = sagyo_List;


        }
    }
}
