using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMSサンプル
{
    class Class_Detaget
    {
        //DBコネクション
        public NpgsqlConnection con { get; set; }

        //カスタマ通番からカスタマ名を取得する
        public string getCustomername(string userno )
        {
            NpgsqlCommand cmd;
            string username = "";
            NpgsqlDataReader dataReader = null;
            try
            {

                if (con.FullState != ConnectionState.Open) con.Open();

                cmd = new NpgsqlCommand(@"select userno,username FROM user_tbl WHERE userno=:no", con);
                cmd.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = userno });

                dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows)
                {

                    dataReader.Read();
                    username = dataReader["username"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("カスタマ名の取得に失敗しました。 " + ex.Message, "カスタマ名取得", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if(dataReader != null)
                    dataReader.Close();

            }
            return username;
        }
        //システム通番からシステム名を取得する
        public string getSystemname(string systemno)
        {
            NpgsqlCommand cmd;
            string systemname = "";
            NpgsqlDataReader dataReader = null;
            try
            {

                if (con.FullState != ConnectionState.Open) con.Open();

                cmd = new NpgsqlCommand(@"select systemno,systemname FROM system WHERE systemno=:no", con);
                cmd.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = systemno });

                dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {

                    dataReader.Read();
                    systemname = dataReader["systemname"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("システム名の取得に失敗しました。 " + ex.Message, "システム名取得", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dataReader != null)
                    dataReader.Close();

            }
            return systemname;
        }
        //拠点通番から拠点名を取得する
        public string getSitename(string siteno) 
        {
            NpgsqlCommand cmd;
            string sitename = "";
            NpgsqlDataReader dataReader = null;
            try
            {

                if (con.FullState != ConnectionState.Open) con.Open();

                cmd = new NpgsqlCommand(@"select siteno,sitename FROM site WHERE siteno=:no", con);
                cmd.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = siteno });

                dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    sitename = dataReader["sitename"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("拠点名の取得に失敗しました。 " + ex.Message, "拠点名取得", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dataReader != null)
                    dataReader.Close();

            }
            return sitename;

        }

        //ホスト名
        public string getHostname(string hostno)
        {
            NpgsqlCommand cmd;
            string hostname = "";
            NpgsqlDataReader dataReader = null;
            try
            {

                if (con.FullState != ConnectionState.Open) con.Open();

                cmd = new NpgsqlCommand(@"select host_no,hostname FROM host WHERE host_no=:no", con);
                cmd.Parameters.Add(new NpgsqlParameter("no", DbType.Int32) { Value = hostno });

                dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows) { 
                    dataReader.Read();
                    hostname = dataReader["hostname"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ホスト名の取得に失敗しました。 " + ex.Message, "ホスト名取得", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dataReader != null)
                    dataReader.Close();

            }
            return hostname;

        }


        //カスタマデータ一覧を取得する
        public List<userDS> getUserList()
        {

            NpgsqlCommand cmd;
            userDS ds;
            List<userDS> retList = null;
            //DB接続
            try
            {

                if (con.FullState != ConnectionState.Open) con.Open();

                cmd = new NpgsqlCommand(@"select u.userno,u.username,u.username_kana,u.username_sum,u.status,u.report_status,u.biko,u.chk_date,u.chk_name_id,o.lastname,o.fastname from user_tbl u INNER JOIN ope o ON u.chk_name_id = o.opeid ", con);
                var dataReader = cmd.ExecuteReader();

                //カスタマ情報の取得
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
                    ds.lastname = dataReader["lastname"].ToString();
                    ds.fastname = dataReader["fastname"].ToString();


                    retList.Add(ds);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("カスタマ情報一覧取得エラー " + ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return retList;
        }

        //システム情報一覧を取得する(引数 検索条件)
        public List<systemDS> getSystemList(string searchcom, Boolean searchflg = false)
        {

            string searchstring = "";
            if (searchflg)
            {
                searchstring = " where u.userno = " + searchcom;

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
                    if (con.FullState != ConnectionState.Open) con.Open();

                    //SELECT実行
                    cmd = new NpgsqlCommand(@"select sys.systemno,sys.systemname,sys.systemkana,sys.biko,sys.userno,u.username,sys.chk_date,sys.chk_name_id from system sys" +
                        " INNER JOIN user_tbl u ON u.userno = sys.userno" + searchstring, con);
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
                        ds.username = dataReader["username"].ToString();

                        ds.chk_date = dataReader["chk_date"].ToString();
                        ds.chk_name_id = dataReader["chk_name_id"].ToString();


                        retList.Add(ds);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("システム情報一覧取得エラー " + ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return retList;
            }
        }
        //拠点情報一覧を取得する(引数 検索条件)
        public List<siteDS> getSiteList(string systemid, NpgsqlConnection con, Boolean searchflg = false)
        {
            string searchstring = "";
            if (searchflg)
                searchstring = " where systemno=" + systemid;

            NpgsqlCommand cmd;
            siteDS ds;
            List<siteDS> retList = null;

            try
            {
                if (con.FullState != ConnectionState.Open) con.Open();

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
                MessageBox.Show("拠点情報一覧取得エラー " + ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return retList;
        }

        //機器情報一覧を取得する(引数 検索条件)
        public List<hostDS> getHostList(string siteID, NpgsqlConnection con, Boolean searchflg = false)
        {

            string searchstring = "";
            if (searchflg)
            {
                searchstring = " where siteno=" + siteID;

            }
            NpgsqlCommand cmd;
            hostDS ds;
            List<hostDS> retList = null;

            try
            {
                if (con.FullState != ConnectionState.Open) con.Open(); 

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
                    ds.systemno = dataReader["systemno"].ToString();
                    ds.chk_name_id = dataReader["chk_name_id"].ToString();
                    ds.chk_date = dataReader["chk_date"].ToString();
                       
                    retList.Add(ds);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("機器情報一覧取得エラー " + ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return retList;
        }

        //回線情報の取得
        public void getSelectKaisenInfo(DataTable dt, DISP_dataSet retDS, NpgsqlConnection conn)
        {
            
            String sql = "SELECT k.kaisenno,k.status,k.career,k.type, k.kaisenid,k.isp,k.servicetype,k.serviceid,k.userno,k.systemno,k.siteno,k.host_no,k.chk_date,k.chk_name_id,o.lastname " +
                        "FROM Kaisen k INNER JOIN ope o ON o.opeid = k.chk_name_id";

            String param = "";
            int i = 0;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (i == 0)
                    {
                        param += " where userno=" + row[0];
                    }
                    else
                        param += " or userno=" + row[0];


                    i++;
                }
                sql += param;
            }
            NpgsqlCommand cmd;
            kaisenDS k_ds;

            List<kaisenDS> kaisen_List = null;

            //DB接続

            try
            {
                if (conn.FullState != ConnectionState.Open) conn.Open();

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
                    k_ds.userno = dataReader["userno"].ToString();
                    k_ds.serviceid = dataReader["systemno"].ToString();
                    k_ds.siteno = dataReader["siteno"].ToString();
                    k_ds.host_no = dataReader["host_no"].ToString();
                    k_ds.chk_name_id = dataReader["lastname"].ToString();
                    k_ds.chk_date = dataReader["chk_date"].ToString();

                    kaisen_List.Add(k_ds);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("回線情報の取得に失敗しました。" + ex.Message, "回線情報検索", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            retDS.kaisen_L = kaisen_List;

        }

        //インシデント一覧データを取得
        public List<incidentDS> getOpenIncident(NpgsqlConnection conn)
        {

            String sql = "SELECT u.username,sys.systemname,s.sitename,h.hostname," +
                "i.incident_no,i.status,i.mpms_incident,i.s_cube_id,i.incident_type,i.content,i.matflg,i.matcommand,i.uketukedate,i.tehaidate," +
                "i.fukyudate,i.enddate,i.timer,i.kakunin,i.hostno,i.opeid,o.lastname,i.siteno,i.userno,i.systemno,i.chk_date,i.chk_name_id FROM incident i " +
                "LEFT OUTER JOIN ope o ON i.opeid = o.opeid " +
                "LEFT OUTER JOIN user_tbl u ON i.userno = u.userno " +
                "LEFT OUTER JOIN system sys ON sys.systemno = i.systemno " +
                "LEFT OUTER JOIN site s ON s.siteno=i.siteno " +
                "LEFT OUTER JOIN host h ON i.hostno = h.host_no " +
                "WHERE i.status = '0'";

            NpgsqlCommand cmd;
            incidentDS inc_ds;

            List<incidentDS> incidnet_List = null;

            //DB接続
            try
            {
                if (conn.FullState != ConnectionState.Open) conn.Open();

                //SELECT実行
                cmd = new NpgsqlCommand(@sql, conn);
                var dataReader = cmd.ExecuteReader();

                //構成情報の取得
                incidnet_List = new List<incidentDS>();
                while (dataReader.Read())
                {

                    inc_ds = new incidentDS();

                    inc_ds.username = dataReader["username"].ToString();
                    inc_ds.systemname = dataReader["systemname"].ToString();
                    inc_ds.sitename = dataReader["sitename"].ToString();
                    inc_ds.hostname = dataReader["hostname"].ToString();
                    inc_ds.incident_no = dataReader["incident_no"].ToString();
                    inc_ds.status = dataReader["status"].ToString();
                    inc_ds.mpms_incident = dataReader["mpms_incident"].ToString();
                    inc_ds.s_cube_id = dataReader["s_cube_id"].ToString();
                    inc_ds.incident_type = dataReader["incident_type"].ToString();
                    inc_ds.content = dataReader["content"].ToString();
                    inc_ds.matflg = dataReader["matflg"].ToString();
                    inc_ds.matcommand = dataReader["matcommand"].ToString();
                    inc_ds.uketukedate = dataReader["uketukedate"].ToString();
                    inc_ds.tehaidate = dataReader["tehaidate"].ToString();
                    inc_ds.fukyudate = dataReader["fukyudate"].ToString();
                    inc_ds.enddate = dataReader["enddate"].ToString();
                    inc_ds.hostno = dataReader["hostno"].ToString();
                    inc_ds.opeid = dataReader["opeid"].ToString();
                    inc_ds.siteno = dataReader["siteno"].ToString();
                    inc_ds.userno = dataReader["userno"].ToString();
                    inc_ds.chk_name_id = dataReader["chk_name_id"].ToString();
                    inc_ds.chk_date = dataReader["chk_date"].ToString();

                    incidnet_List.Add(inc_ds);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("インシデント情報の取得に失敗しました。" + ex.Message, "インシデント情報検索", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return incidnet_List;

        }


        //カスタマ情報の取得
        public DISP_dataSet getSelectUser(Dictionary<string, string> param_dict, NpgsqlConnection conn, DISP_dataSet displist, Boolean detailflg = false)
        {

            String sql = "select u.userno,u.username,u.username_kana,u.username_sum,u.status user_status,u.report_status,u.biko,u.chk_date user_chk_date, u.chk_name_id user_chk_name_id " +
                        "from user_tbl u,ope o  where  o.opeid = u.chk_name_id ";

            String param = "";

            if (param_dict.Count > 0)
            {
                foreach (KeyValuePair<string, string> vdict in param_dict)
                {
                    if(detailflg)
                    {
                        param += " and u." + vdict.Key + "='" + vdict.Value + "'";
                    }
                    else if (vdict.Key == "username")
                    {
                        param += " and u." + vdict.Key + "='" + vdict.Value + "'";
                    }
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
                if (conn.FullState != ConnectionState.Open) conn.Open();

                //SELECT実行
                cmd = new NpgsqlCommand(@sql, conn);
                var dataReader = cmd.ExecuteReader();

                //構成情報の取得
                user_List = new List<userDS>();

                while (dataReader.Read())
                {

                    //カスタマ情報の取得
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
                MessageBox.Show("カスタマ情報一覧取得エラー " + ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return displist;
        }

        //システム情報の取得
        public DISP_dataSet getSelectSystem(Dictionary<string, string> param_dict, NpgsqlConnection conn, DISP_dataSet displist, Boolean detailflg = false)
        {

            String sql = "select sys.systemno,sys.systemname,sys.systemkana,sys.biko,u.userno,u.username,sys.chk_date system_chk_date,o.lastname system_chk_name_id " +
                        "from system sys,ope o,user_tbl u where o.opeid = sys.chk_name_id and u.userno = sys.userno";

            String param = "";

            if (param_dict.Count > 0)
            {
                foreach (KeyValuePair<string, string> vdict in param_dict)
                {
                    if (detailflg)
                    {
                        param += " and sys." + vdict.Key + "='" + vdict.Value + "'";
                    }
                    else {

                        if (vdict.Key == "systemname")
                            param += " and sys." + vdict.Key + "='" + vdict.Value + "'";
                        if (vdict.Key == "username")
                            param += " and u." + vdict.Key + "='" + vdict.Value + "'";
                    }
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
                if (conn.FullState != ConnectionState.Open) conn.Open();

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
                    sys_ds.userno = dataReader["userno"].ToString();
                    sys_ds.username = dataReader["username"].ToString();

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
        public DISP_dataSet getSelectSite(Dictionary<string, string> param_dict, NpgsqlConnection conn, DISP_dataSet displist, Boolean detailflg = false)
        {

            String sql = "select u.username,sys.systemname,s.siteno,s.sitename,s.address1,s.address2,s.telno,s.status site_STATUS,s.biko,s.chk_date site_chk_date,s.userno,s.systemno,o.lastname site_chk_name_id " +
                         "from site s,ope o,user_tbl u,system sys where o.opeid = s.chk_name_id and s.userno = u.userno and sys.systemno=s.systemno";
            String param = "";

            if (param_dict.Count > 0)
            {
                foreach (KeyValuePair<string, string> vdict in param_dict)
                {
                    if(detailflg)
                        if (vdict.Key == "systemno")
                            param += " and s." + vdict.Key + "=" + vdict.Value;
                        else
                            param += " and s." + vdict.Key + "='" + vdict.Value + "'";
                    else { 
                        if (vdict.Key == "sitename")
                            param += " and s." + vdict.Key + "='" + vdict.Value + "'";
                        else if (vdict.Key == "systemno")
                            param += " and s." + vdict.Key + "=" + vdict.Value;
                    }
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
                if (conn.FullState != ConnectionState.Open) conn.Open();

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
        public DISP_dataSet getSelectHost(Dictionary<string, string> param_dict, NpgsqlConnection conn, DISP_dataSet displist,Boolean detailflg = false)
        {


            String sql = "select h.userno,h.systemno,h.siteno,h.host_no,h.hostname,h.hostname_ja,h.status host_status, h.device,h.location,h.usefor,h.kansiStartdate,h.kansiEndsdate,h.hosyukanri,h.hosyuinfo,h.biko,h.chk_date host_chk_date, o.lastname host_chk_name_id " +
                        "from host h,ope o,system sys,site s,user_tbl u where o.opeid = h.chk_name_id and u.userno=h.userno and h.systemno=sys.systemno and h.siteno=s.siteno";
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
                if (conn.FullState != ConnectionState.Open) conn.Open();

                //SELECT実行
                cmd = new NpgsqlCommand(@sql, conn);
                var dataReader = cmd.ExecuteReader();

                //構成情報の取得
                host_List = new List<hostDS>();

                while (dataReader.Read())
                {

                    //機器情報の取得
                    h_ds = new hostDS();
                    h_ds.userno = dataReader["userno"].ToString();
                    h_ds.systemno = dataReader["systemno"].ToString();
                    h_ds.siteno = dataReader["siteno"].ToString();

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
        public DISP_dataSet getSelectInterface(Dictionary<string, string> param_dict, NpgsqlConnection conn, DISP_dataSet displist, Boolean detailflg = false)
        {

            String sql = "select  u.userno, u.username, u.username_kana, u.username_sum, u.status, u.report_status, u.biko, u.chk_date, u.chk_name_id," +
                "sys.systemno, sys.systemname, sys.systemkana, sys.biko, sys.chk_date user_up_date, sys.chk_name_id user_up_name, s.siteno," +
                "s.sitename,s.address1,s.address2,s.telno,s.status,s.biko,s.chk_date site_up_date,s.chk_name_id site_up_name," +
                "h.host_no,h.hostname,h.hostname_ja,h.status,h.device,h.location,h.usefor, h.kansistartdate,h.kansiendsdate,h.hosyukanri,h.hosyuinfo,h.biko,h.chk_date host_up_date,h.chk_name_id host_up_date," +
                "w.userno,w.systemno, w.siteno, w.host_no, w.kennshino, w.interfacename, w.status watch_status, w.type, w.kanshi, w.start_date, w.end_date, w.border, w.IPaddress, w.IPaddressNAT, w.chk_date watch_chk_date, o.lastname watch_chk_name_id  from watch_interface w,ope o, user_tbl u,system sys, site s,host h " +
                " where o.opeid = w.chk_name_id and w.userno = u.userno and w.systemno = sys.systemno and w.siteno = s.siteno and h.host_no = w.host_no ";

            String param = "";

            if (param_dict.Count > 0)
            {
                if (detailflg)
                {
                    foreach (KeyValuePair<string, string> vdict in param_dict)
                    {
                        if(vdict.Key == "userno" || vdict.Key == "systemno" || vdict.Key == "siteno" || vdict.Key == "host_no" || vdict.Key == "kennshino")
                            param += " and w." + vdict.Key + "=" + vdict.Value ;
                        else 
                            param += " and w." + vdict.Key + "='" + vdict.Value + "'";
                    }
                }
                else { 
                    foreach (KeyValuePair<string, string> vdict in param_dict)
                    {
                        if (vdict.Key == "IPaddress")
                            param += " and w.IPaddress='" + vdict.Value + "' or w.IPaddressNAT='" + vdict.Value + "'";
                    }
                }
                sql += param;
            }

            NpgsqlCommand cmd;
            watch_InterfaceDS w_ds;

            List<watch_InterfaceDS> interface_List = null;

            try
            {
                if (conn.FullState != ConnectionState.Open) conn.Open();

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
                    w_ds.watch_Interfaceno = dataReader["kennshino"].ToString();
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
        //回線
        public DISP_dataSet getSelectKaisenList(Dictionary<string, string> param_dict, NpgsqlConnection conn, DISP_dataSet displist, Boolean detailflg = false)
        {


            String sql = "SELECT k.userno,k,systemno,k.siteno,k.host_no,k.kaisenno,k.status,k.career,k.type, k.kaisenid,k.isp,k.servicetype,k.serviceid,k.chk_date,k.chk_name_id,o.lastname " +
                        "FROM Kaisen k INNER JOIN ope o ON o.opeid = k.chk_name_id ";
            String param = "";

            if (param_dict.Count > 0)
            {
                int i = 0;
                foreach (KeyValuePair<string, string> vdict in param_dict)
                {
                    if (i == 0)
                    {
                        param += " where k." + vdict.Key + "=" + vdict.Value;
                        i++;

                    }
                    else {
                        if (vdict.Key == "userno" || vdict.Key == "systemno" || vdict.Key == "siteno" || vdict.Key == "host_no" || vdict.Key == "kennshino" ||
                        vdict.Key == "kaisenno")
                        {
                            param += " and k." + vdict.Key + "=" + vdict.Value;
                            i++;
                        }
                        else
                        {
                            param += " and k." + vdict.Key + "='" + vdict.Value + "'";
                            i++;
                        }
                    }
                }

                sql += param;
            }

            NpgsqlCommand cmd;
            kaisenDS kai_ds;

            List<kaisenDS> kaisen_List = null;

            //DB接続
            Class_common common = new Class_common();
            try
            {
                if (conn.FullState != ConnectionState.Open) conn.Open();

                //SELECT実行
                cmd = new NpgsqlCommand(@sql, conn);
                var dataReader = cmd.ExecuteReader();
                
                kaisen_List = new List<kaisenDS>();
                while (dataReader.Read())
                {

                    kai_ds = new kaisenDS();

                    kai_ds.kaisenno =   dataReader["kaisenno"].ToString();
                    kai_ds.status =     dataReader["status"].ToString();
                    kai_ds.career =     dataReader["career"].ToString();
                    kai_ds.type =       dataReader["type"].ToString();
                    kai_ds.kaisenid =   dataReader["kaisenid"].ToString();
                    kai_ds.isp =        dataReader["isp"].ToString();
                    kai_ds.servicetype=dataReader["servicetype"].ToString();
                    kai_ds.serviceid =  dataReader["serviceid"].ToString();
                    kai_ds.chk_name_id = dataReader["chk_name_id"].ToString();
                    kai_ds.userno = dataReader["userno"].ToString();
                    kai_ds.systemno = dataReader["systemno"].ToString();
                    kai_ds.siteno = dataReader["siteno"].ToString();
                    kai_ds.host_no = dataReader["host_no"].ToString();
                    kai_ds.chk_date = dataReader["chk_date"].ToString();
                    kai_ds.chk_name_id = dataReader["chk_name_id"].ToString();

                    kaisen_List.Add(kai_ds);

                }
                displist.kaisen_L = kaisen_List;
            }
            catch (Exception ex)
            {
                MessageBox.Show("回線情報一覧取得エラー " + ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return displist;
        }
        //作業情報を取得する
        public List<scheduleDS> getSelectSagyoList( NpgsqlConnection con)
        {

            String sql = "select sc,schedule_no,sc.timer_name,sc.schedule_type,sc.repeat_type,sc.start_date,sc.end_date,sc.status,sc.alerm_message," +
                        "incident_no,sc.chk_date,i.userno,i.chk_name_id from " +
                        "schedule sc INNER JOIN ope o ON i.chk_name_id = o.opeid ";


            NpgsqlCommand cmd;
            scheduleDS sa_ds;

            List<scheduleDS> sagyo_List = null;

            try
            {
                if (con.FullState != ConnectionState.Open) con.Open();

                //SELECT実行
                cmd = new NpgsqlCommand(@sql, con);
                var dataReader = cmd.ExecuteReader();

                //構成情報の取得
                sagyo_List = new List<scheduleDS>();
                while (dataReader.Read())
                {

                    sa_ds = new scheduleDS();
                    sa_ds.schedule_no= dataReader["schedule_no"].ToString();
                    sa_ds.timer_name = dataReader["timer_name"].ToString();
                    sa_ds.start_date = dataReader["start_date"].ToString();
                    sa_ds.end_date = dataReader["end_date"].ToString();
                    sa_ds.chk_name_id = dataReader["chk_name_id"].ToString();
                    sa_ds.chk_date = dataReader["chk_date"].ToString();

                    sagyo_List.Add(sa_ds);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("作業情報の取得に失敗しました。" + ex.Message, "作業情報検索", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return sagyo_List;


        }
        //オペレータ
        public opeDS get_opeName(Dictionary<string, string> param_dict, NpgsqlConnection conn)
        {
            String sql = "SELECT  openo,opeid,lastname,fastname,password,type,biko,chk_date,chk_name_id FROM ope";

            if (param_dict.Count > 0)
            {
                foreach (KeyValuePair<string, string> vdict in param_dict)
                {
                    sql += " WHERE " + vdict.Key + "='" + vdict.Value + "'";
                }
            }

            NpgsqlCommand cmd;
            opeDS o_ds = new opeDS();

            //DB接続

            try
            {
                if (conn.FullState != ConnectionState.Open) conn.Open();

                //SELECT実行
                cmd = new NpgsqlCommand(@sql, conn);
                var dataReader = cmd.ExecuteReader();

                //構成情報の取得
                while (dataReader.Read())
                {

                    o_ds.openo = dataReader["openo"].ToString();
                    o_ds.opeid = dataReader["opeid"].ToString();
                    o_ds.lastname = dataReader["lastname"].ToString();
                    o_ds.fastname = dataReader["fastname"].ToString();
                    o_ds.password = dataReader["password"].ToString();
                    o_ds.type = dataReader["type"].ToString();
                    o_ds.biko = dataReader["biko"].ToString();
                    o_ds.chk_date = dataReader["chk_date"].ToString();
                    o_ds.chk_name_id = dataReader["chk_name_id"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("オペレータ情報一覧取得エラー " + ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return o_ds;
        }
        //担当者
        public List<tantouDS> get_tantouName(Dictionary<string, string> param_dict, NpgsqlConnection conn)
        {
            String sql = "SELECT userno,user_tantou_no,user_tantou_name,user_tantou_name_kana,busho_name,telno1,telno2,yakusyoku,status,biko,chk_date,chk_name_id FROM user_tanntou";

            if (param_dict.Count > 0)
            {
                foreach (KeyValuePair<string, string> vdict in param_dict)
                {
                    sql += " WHERE " + vdict.Key + "='" + vdict.Value + "'";
                }
            }

            NpgsqlCommand cmd;
            tantouDS t_ds;
            List<tantouDS> tatou_list = new List<tantouDS>();
            //DB接続

            try
            {
                if (conn.FullState != ConnectionState.Open) conn.Open();

                //SELECT実行
                cmd = new NpgsqlCommand(@sql, conn);
                var dataReader = cmd.ExecuteReader();

                //構成情報の取得
                while (dataReader.Read())
                {
                    t_ds = new tantouDS();
                    t_ds.user_tantou_no = dataReader["user_tantou_no"].ToString();
                    t_ds.userno = dataReader["userno"].ToString();
                    t_ds.user_tantou_name= dataReader["user_tantou_name"].ToString();
                    t_ds.user_tantou_name_kana = dataReader["user_tantou_name_kana"].ToString();
                    t_ds.busho_name= dataReader["busho_name"].ToString();
                    t_ds.telno1 = dataReader["telno1"].ToString();
                    t_ds.telno2 = dataReader["telno2"].ToString();
                    t_ds.yakusyoku = dataReader["yakusyoku"].ToString();
                    t_ds.status = dataReader["status"].ToString();
                    t_ds.biko = dataReader["biko"].ToString();

                    t_ds.chk_date = dataReader["chk_date"].ToString();
                    t_ds.chk_name_id = dataReader["chk_name_id"].ToString();
                    tatou_list.Add(t_ds);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("オペレータ情報一覧取得エラー " + ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return tatou_list;
        }
        //検索データ
        public DISP_dataSet getSelectDataFor_Interface(Dictionary<string, string> param_dict, NpgsqlConnection conn, DISP_dataSet displist)
        {
            string[] sql= new string[2];

            string username,systemname,sitename,hostname,ipadd;

            param_dict.TryGetValue("IPaddress", out ipadd);
            param_dict.TryGetValue("hostname", out hostname);
            param_dict.TryGetValue("sitename", out sitename);
            param_dict.TryGetValue("systemname", out systemname);
            param_dict.TryGetValue("username", out username);

            if (ipadd != null)
                sql[0] = "select u.userno,u.username,u.username_kana,u.username_sum,u.status u_status,u.report_status,u.biko userbiko,u.chk_date u_chk_date,u.chk_name_id u_chk_name_id," +
                    "sys.systemno,sys.systemname,sys.systemkana, sys.biko sysbiko, sys.chk_date sys_chk_date, sys.chk_name_id sys_chk_name_id," +
                    "s.siteno,s.sitename,s.address1,s.address2,s.telno, s.status s_status, s.biko sitebiko, s.chk_date s_chk_date, s.chk_name_id s_chk_name_id," +
                    "h.host_no, h.hostname, h.hostname_ja, h.status h_status, h.device, h.location, h.usefor, h.kansistartdate, h.kansiendsdate, h.hosyukanri, h.hosyuinfo, h.biko hostbiko, h.chk_date h_chk_date,h.chk_name_id h_chk_name_id," +
                    "w.kennshino, w.interfacename, w.status w_status,w.type,w.kanshi,w.start_date,w.end_date,w.border,w.IPaddress,w.IPaddressNAT,w.chk_date w_chk_date,w.chk_name_id w_chk_name_id " +
                    "from watch_interface w INNER JOIN user_tbl u ON u.userno = w.userno INNER JOIN system sys ON sys.systemno = w.systemno INNER JOIN site s ON s.siteno = w.siteno INNER JOIN host h ON h.host_no = w.host_no ";
            else if (hostname != null)
            {
                sql[0] = "select u.userno,u.username,u.username_kana,u.username_sum,u.status u_status,u.report_status,u.biko userbiko,u.chk_date u_chk_date,u.chk_name_id u_chk_name_id," +
                    "sys.systemno,sys.systemname,sys.systemkana, sys.biko sysbiko, sys.chk_date sys_chk_date, sys.chk_name_id sys_chk_name_id," +
                    "s.siteno,s.sitename,s.address1,s.address2,s.telno, s.status s_status, s.biko sitebiko, s.chk_date s_chk_date, s.chk_name_id s_chk_name_id," +
                    "h.host_no, h.hostname, h.hostname_ja, h.status h_status, h.device, h.location, h.usefor, h.kansistartdate, h.kansiendsdate, h.hosyukanri, h.hosyuinfo, h.biko hostbiko, h.chk_date h_chk_date,h.chk_name_id h_chk_name_id " +
                    "from host h INNER JOIN user_tbl u ON u.userno = h.userno INNER JOIN system sys ON sys.systemno = h.systemno INNER JOIN site s ON s.siteno = h.siteno ";

                sql[1] = "select w.userno,w.systemno,w.siteno, w.host_no, w.kennshino, w.interfacename, w.status w_status, w.type,w.kanshi,w.start_date,w.end_date,w.border,w.IPaddress,w.IPaddressNAT,w.chk_date w_chk_date, w.chk_name_id w_chk_name_id " +
                    "from user_tbl u INNER JOIN system sys ON u.userno = sys.userno INNER JOIN site s ON s.systemno=sys.systemno INNER JOIN host h ON h.siteno = s.siteno  LEFT OUTER JOIN watch_interface w ON  h.host_no = w.host_no ";
            }
            else if (sitename != null)
            {
                sql[0] = "select u.userno,u.username,u.username_kana,u.username_sum,u.status u_status,u.report_status,u.biko userbiko,u.chk_date u_chk_date,u.chk_name_id u_chk_name_id," +
                    "sys.systemno,sys.systemname,sys.systemkana, sys.biko sysbiko, sys.chk_date sys_chk_date, sys.chk_name_id sys_chk_name_id," +
                    "s.siteno,s.sitename,s.address1,s.address2,s.telno, s.status s_status, s.biko sitebiko, s.chk_date s_chk_date, s.chk_name_id s_chk_name_id " +
                    "from site s INNER JOIN user_tbl u ON u.userno = s.userno INNER JOIN system sys ON sys.systemno = s.systemno ";

                sql[1] = "select w.kennshino, w.interfacename, w.status w_status, w.type,w.kanshi,w.start_date,w.end_date,w.border,w.IPaddress,w.IPaddressNAT,w.chk_date w_chk_date, w.chk_name_id w_chk_name_id, " +
                    "h.userno,h.systemno,h.siteno,h.host_no, h.hostname, h.hostname_ja, h.status h_status, h.device, h.location, h.usefor, h.kansistartdate, h.kansiendsdate, h.hosyukanri, h.hosyuinfo, h.biko hostbiko, h.chk_date h_chk_date,h.chk_name_id h_chk_name_id " +
                    "from user_tbl u INNER JOIN system sys ON u.userno = sys.userno INNER JOIN site s ON s.systemno=sys.systemno INNER JOIN host h ON s.siteno = h.siteno INNER JOIN watch_interface w ON s.siteno = w.siteno ";
            }
            else if (systemname != null)
            {
                sql[0] = "select u.userno,u.username,u.username_kana,u.username_sum,u.status u_status,u.report_status,u.biko userbiko,u.chk_date u_chk_date,u.chk_name_id u_chk_name_id," +
                    "sys.systemno,sys.systemname,sys.systemkana, sys.biko sysbiko, sys.chk_date sys_chk_date, sys.chk_name_id sys_chk_name_id " +
                    "from system sys INNER JOIN user_tbl u ON u.userno = sys.userno ";

                sql[1] = "select u.userno,u.username,sys.systemno,sys.systemname,w.kennshino, w.interfacename, w.status w_status, w.type,w.kanshi,w.start_date,w.end_date,w.border,w.IPaddress,w.IPaddressNAT,w.chk_date w_chk_date, w.chk_name_id w_chk_name_id," +
                    "h.host_no, h.hostname, h.hostname_ja, h.status h_status, h.device, h.location, h.usefor, h.kansistartdate, h.kansiendsdate, h.hosyukanri, h.hosyuinfo, h.biko hostbiko, h.chk_date h_chk_date,h.chk_name_id h_chk_name_id, " +
                    "s.siteno,s.sitename,s.address1,s.address2,s.telno, s.status s_status, s.biko sitebiko, s.chk_date s_chk_date, s.chk_name_id s_chk_name_id " +
                    "from user_tbl u INNER JOIN system sys ON u.userno = sys.userno INNER JOIN site s ON s.systemno = sys.systemno LEFT OUTER JOIN host h ON h.systemno = s.systemno LEFT OUTER JOIN watch_interface w ON w.systemno = s.systemno ";
            }
            else if (username != null)

                sql[0] = "select w.kennshino, w.interfacename, w.status w_status, w.type,w.kanshi,w.start_date,w.end_date,w.border,w.IPaddress,w.IPaddressNAT,w.chk_date w_chk_date, w.chk_name_id w_chk_name_id," +
                    "h.host_no, h.hostname, h.hostname_ja, h.status h_status, h.device, h.location, h.usefor, h.kansistartdate, h.kansiendsdate, h.hosyukanri, h.hosyuinfo, h.biko hostbiko, h.chk_date h_chk_date,h.chk_name_id h_chk_name_id, " +
                    "s.siteno,s.sitename,s.address1,s.address2,s.telno, s.status s_status, s.biko sitebiko, s.chk_date s_chk_date, s.chk_name_id s_chk_name_id," +
                    "sys.systemno,sys.systemname,sys.systemkana, sys.biko sysbiko, sys.chk_date sys_chk_date, sys.chk_name_id sys_chk_name_id," +
                    "u.userno,u.username,u.username_kana,u.username_sum,u.status u_status, u.report_status,u.biko userbiko, u.chk_date u_chk_date, u.chk_name_id u_chk_name_id " +
                    "from user_tbl u LEFT OUTER JOIN system sys ON u.userno = sys.userno INNER JOIN site s ON s.systemno = sys.systemno LEFT OUTER JOIN host h ON h.systemno = s.systemno LEFT OUTER JOIN watch_interface w ON w.systemno = s.systemno ";


             

                String param = "";

            if (param_dict.Count > 0)
            {
                int i = 0;
                foreach (KeyValuePair<string, string> vdict in param_dict)
                {

                    if (vdict.Key == "IPaddress")
                        if (i >= 1)
                            param += " and w.IPaddress='" + vdict.Value + "' OR w.IPaddressNAT = '" + vdict.Value + "'";
                        else
                            param += "WHERE w.IPaddress='" + vdict.Value + "' OR w.IPaddressNAT = '" + vdict.Value + "'";

                    if (vdict.Key == "hostname")
                        if (i >= 1)
                            param += " and h.hostname='" + vdict.Value + "'";
                        else
                            param += "WHERE h.hostname='" + vdict.Value + "'";

                    if (vdict.Key == "username")
                        if (i >= 1)
                            param += " and u.username='" + vdict.Value + "'";
                        else
                            param += "WHERE u.username='" + vdict.Value + "'";

                    if (vdict.Key == "systemname")
                        if (i >= 1)
                            param += " and sys.systemname='" + vdict.Value + "'";
                        else
                            param += "WHERE sys.systemname='" + vdict.Value + "'";

                    if (vdict.Key == "sitename")
                        if (i >= 1)
                            param += " and s.sitename='" + vdict.Value + "'";
                        else
                            param += "WHERE s.sitename='" + vdict.Value + "'";

                    if (vdict.Key == "host")
                        if (i >= 1)
                            param += " and h.host='" + vdict.Value + "'";
                        else
                            param += "WHERE h.host='" + vdict.Value + "'";
                    i++;
                }
            }

            sql[0] += param;
            if (sql[1] != null) sql[1] += param;
            
            NpgsqlCommand cmd;

            userDS u_ds;
            systemDS sys_ds;
            siteDS s_ds;
            hostDS h_ds;
            watch_InterfaceDS w_ds;

            List<userDS> user_List = null;
            List<systemDS> system_List = null;
            List<siteDS> site_List = null;
            List<hostDS> host_List = null;
            List<watch_InterfaceDS> interface_List = null;
            
            try
            {
                if (con.FullState != ConnectionState.Open) conn.Open();



                for (int i= 0;i < 2; i++) {
                    if (sql[i] == null)
                        break ;

                    //SELECT実行
                    cmd = new NpgsqlCommand(sql[i], conn);
                    var dataReader = cmd.ExecuteReader();

                    //構成情報の取得
                    user_List = new List<userDS>();
                    system_List = new List<systemDS>();
                    site_List = new List<siteDS>();
                    host_List = new List<hostDS>();
                    interface_List = new List<watch_InterfaceDS>();

                    while (dataReader.Read())
                    {
                        u_ds = new userDS();
                        sys_ds = new systemDS();
                        s_ds = new siteDS();
                        h_ds = new hostDS();
                        w_ds = new watch_InterfaceDS();

                        if (ipadd != null && i == 0)
                        {
                            u_ds.userno = dataReader["userno"].ToString();
                            u_ds.username = dataReader["username"].ToString();
                            u_ds.username_kana = dataReader["username_kana"].ToString();
                            u_ds.username_sum = dataReader["username_sum"].ToString();
                            u_ds.status = dataReader["u_status"].ToString();
                            u_ds.report_status = dataReader["report_status"].ToString();
                            u_ds.biko = dataReader["userbiko"].ToString();
                            u_ds.chk_date = dataReader["u_chk_date"].ToString();
                            u_ds.chk_name_id = dataReader["u_chk_name_id"].ToString();
                            user_List.Add(u_ds);

                            sys_ds.systemno = dataReader["systemno"].ToString();
                            sys_ds.systemname = dataReader["systemname"].ToString();
                            sys_ds.systemkana = dataReader["systemkana"].ToString();
                            sys_ds.biko = dataReader["sysbiko"].ToString();
                            sys_ds.chk_date = dataReader["sys_chk_date"].ToString();
                            sys_ds.chk_name_id = dataReader["sys_chk_name_id"].ToString();
                            system_List.Add(sys_ds);

                            s_ds.siteno = dataReader["siteno"].ToString();
                            s_ds.sitename = dataReader["sitename"].ToString();
                            s_ds.address1 = dataReader["address1"].ToString();
                            s_ds.address2 = dataReader["address2"].ToString();
                            s_ds.telno = dataReader["telno"].ToString();
                            s_ds.userno = dataReader["userno"].ToString();
                            s_ds.systemno = dataReader["systemno"].ToString();
                            s_ds.status = dataReader["s_status"].ToString();
                            s_ds.biko = dataReader["sitebiko"].ToString();
                            s_ds.chk_date = dataReader["s_chk_date"].ToString();
                            s_ds.chk_name_id = dataReader["s_chk_name_id"].ToString();
                            site_List.Add(s_ds);

                            h_ds.host_no = dataReader["host_no"].ToString();
                            h_ds.hostname = dataReader["hostname"].ToString();
                            h_ds.hostname_ja = dataReader["hostname_ja"].ToString();
                            h_ds.status = dataReader["h_status"].ToString();
                            h_ds.device = dataReader["device"].ToString();
                            h_ds.location = dataReader["location"].ToString();
                            h_ds.usefor = dataReader["usefor"].ToString();
                            h_ds.kansiStartdate = dataReader["kansiStartdate"].ToString();
                            h_ds.kansiEndsdate = dataReader["kansiEndsdate"].ToString();
                            h_ds.hosyukanri = dataReader["hosyukanri"].ToString();
                            h_ds.hosyuinfo = dataReader["hosyuinfo"].ToString();
                            h_ds.biko = dataReader["hostbiko"].ToString();
                            h_ds.userno = dataReader["userno"].ToString();
                            h_ds.systemno = dataReader["systemno"].ToString();
                            h_ds.siteno = dataReader["siteno"].ToString();
                            h_ds.chk_date = dataReader["h_chk_date"].ToString();
                            h_ds.chk_name_id = dataReader["h_chk_name_id"].ToString();
                            host_List.Add(h_ds);

                            w_ds.watch_Interfaceno = dataReader["kennshino"].ToString();
                            w_ds.interfacename = dataReader["interfacename"].ToString();
                            w_ds.status = dataReader["w_status"].ToString();
                            w_ds.type = dataReader["type"].ToString();
                            w_ds.kanshi = dataReader["kanshi"].ToString();
                            w_ds.start_date = dataReader["start_date"].ToString();
                            w_ds.end_date = dataReader["end_date"].ToString();
                            w_ds.border = dataReader["border"].ToString();
                            w_ds.IPaddress = dataReader["IPaddress"].ToString();
                            w_ds.IPaddressNAT = dataReader["IPaddressNAT"].ToString();
                            w_ds.userno = dataReader["userno"].ToString();
                            w_ds.host_no = dataReader["host_no"].ToString();
                            w_ds.systemno = dataReader["systemno"].ToString();
                            w_ds.siteno = dataReader["siteno"].ToString();
                            w_ds.chk_date = dataReader["w_chk_date"].ToString();
                            w_ds.chk_name_id = dataReader["w_chk_name_id"].ToString();
                            interface_List.Add(w_ds);
                        }
                        if (hostname != null && i == 0)
                        {
                            u_ds.userno = dataReader["userno"].ToString();
                            u_ds.username = dataReader["username"].ToString();
                            u_ds.username_kana = dataReader["username_kana"].ToString();
                            u_ds.username_sum = dataReader["username_sum"].ToString();
                            u_ds.status = dataReader["u_status"].ToString();
                            u_ds.report_status = dataReader["report_status"].ToString();
                            u_ds.biko = dataReader["userbiko"].ToString();
                            u_ds.chk_date = dataReader["u_chk_date"].ToString();
                            u_ds.chk_name_id = dataReader["u_chk_name_id"].ToString();
                            user_List.Add(u_ds);

                            sys_ds.systemno = dataReader["systemno"].ToString();
                            sys_ds.systemname = dataReader["systemname"].ToString();
                            sys_ds.systemkana = dataReader["systemkana"].ToString();
                            sys_ds.biko = dataReader["sysbiko"].ToString();
                            sys_ds.chk_date = dataReader["sys_chk_date"].ToString();
                            sys_ds.chk_name_id = dataReader["sys_chk_name_id"].ToString();
                            system_List.Add(sys_ds);


                            s_ds.siteno = dataReader["siteno"].ToString();
                            s_ds.sitename = dataReader["sitename"].ToString();
                            s_ds.address1 = dataReader["address1"].ToString();
                            s_ds.address2 = dataReader["address2"].ToString();
                            s_ds.telno = dataReader["telno"].ToString();
                            s_ds.status = dataReader["s_status"].ToString();
                            s_ds.userno = dataReader["userno"].ToString();
                            s_ds.systemno = dataReader["systemno"].ToString();

                            s_ds.biko = dataReader["sitebiko"].ToString();
                            s_ds.chk_date = dataReader["s_chk_date"].ToString();
                            s_ds.chk_name_id = dataReader["s_chk_name_id"].ToString();
                            site_List.Add(s_ds);


                            h_ds.host_no = dataReader["host_no"].ToString();
                            h_ds.hostname = dataReader["hostname"].ToString();
                            h_ds.hostname_ja = dataReader["hostname_ja"].ToString();
                            h_ds.status = dataReader["h_status"].ToString();
                            h_ds.device = dataReader["device"].ToString();
                            h_ds.location = dataReader["location"].ToString();
                            h_ds.usefor = dataReader["usefor"].ToString();
                            h_ds.kansiStartdate = dataReader["kansiStartdate"].ToString();
                            h_ds.kansiEndsdate = dataReader["kansiEndsdate"].ToString();
                            h_ds.hosyukanri = dataReader["hosyukanri"].ToString();
                            h_ds.hosyuinfo = dataReader["hosyuinfo"].ToString();
                            h_ds.biko = dataReader["hostbiko"].ToString();
                            h_ds.userno = dataReader["userno"].ToString();
                            h_ds.systemno = dataReader["systemno"].ToString();
                            h_ds.siteno = dataReader["siteno"].ToString();

                            h_ds.chk_date = dataReader["h_chk_date"].ToString();
                            h_ds.chk_name_id = dataReader["h_chk_name_id"].ToString();
                            host_List.Add(h_ds);
                        }
                        else if (sitename != null && i == 0)
                        {
                            u_ds.userno = dataReader["userno"].ToString();
                            u_ds.username = dataReader["username"].ToString();
                            u_ds.username_kana = dataReader["username_kana"].ToString();
                            u_ds.username_sum = dataReader["username_sum"].ToString();
                            u_ds.status = dataReader["u_status"].ToString();
                            u_ds.report_status = dataReader["report_status"].ToString();
                            u_ds.biko = dataReader["userbiko"].ToString();
                            u_ds.chk_date = dataReader["u_chk_date"].ToString();
                            u_ds.chk_name_id = dataReader["u_chk_name_id"].ToString();
                            user_List.Add(u_ds);

                            sys_ds.systemno = dataReader["systemno"].ToString();
                            sys_ds.systemname = dataReader["systemname"].ToString();
                            sys_ds.systemkana = dataReader["systemkana"].ToString();
                            sys_ds.biko = dataReader["sysbiko"].ToString();
                            sys_ds.chk_date = dataReader["sys_chk_date"].ToString();
                            sys_ds.chk_name_id = dataReader["sys_chk_name_id"].ToString();
                            system_List.Add(sys_ds);


                            s_ds.siteno = dataReader["siteno"].ToString();
                            s_ds.sitename = dataReader["sitename"].ToString();
                            s_ds.address1 = dataReader["address1"].ToString();
                            s_ds.address2 = dataReader["address2"].ToString();
                            s_ds.telno = dataReader["telno"].ToString();
                            s_ds.userno = dataReader["userno"].ToString();
                            s_ds.systemno = dataReader["systemno"].ToString();

                            s_ds.status = dataReader["s_status"].ToString();
                            s_ds.biko = dataReader["sitebiko"].ToString();
                            s_ds.chk_date = dataReader["s_chk_date"].ToString();
                            s_ds.chk_name_id = dataReader["s_chk_name_id"].ToString();
                            site_List.Add(s_ds);
                        }
                        else if (systemname != null && i == 0)
                        {
                            u_ds.userno = dataReader["userno"].ToString();
                            u_ds.username = dataReader["username"].ToString();
                            u_ds.username_kana = dataReader["username_kana"].ToString();
                            u_ds.username_sum = dataReader["username_sum"].ToString();
                            u_ds.status = dataReader["u_status"].ToString();
                            u_ds.report_status = dataReader["report_status"].ToString();
                            u_ds.biko = dataReader["userbiko"].ToString();
                            u_ds.chk_date = dataReader["u_chk_date"].ToString();
                            u_ds.chk_name_id = dataReader["u_chk_name_id"].ToString();
                            user_List.Add(u_ds);

                            sys_ds.systemno = dataReader["systemno"].ToString();
                            sys_ds.systemname = dataReader["systemname"].ToString();
                            sys_ds.systemkana = dataReader["systemkana"].ToString();
                            sys_ds.biko = dataReader["sysbiko"].ToString();
                            sys_ds.chk_date = dataReader["sys_chk_date"].ToString();
                            sys_ds.chk_name_id = dataReader["sys_chk_name_id"].ToString();
                            system_List.Add(sys_ds);
                        }
                        else if(username != null && i == 0)
                        {
   
                                u_ds.userno = dataReader["userno"].ToString();
                                u_ds.username = dataReader["username"].ToString();
                                u_ds.username_kana = dataReader["username_kana"].ToString();
                                u_ds.username_sum = dataReader["username_sum"].ToString();
                                u_ds.status = dataReader["u_status"].ToString();
                                u_ds.report_status = dataReader["report_status"].ToString();
                                u_ds.biko = dataReader["userbiko"].ToString();
                                u_ds.chk_date = dataReader["u_chk_date"].ToString();
                                u_ds.chk_name_id = dataReader["u_chk_name_id"].ToString();
                                user_List.Add(u_ds);

                                sys_ds.systemno = dataReader["systemno"].ToString();
                                sys_ds.systemname = dataReader["systemname"].ToString();
                                sys_ds.systemkana = dataReader["systemkana"].ToString();
                                sys_ds.biko = dataReader["sysbiko"].ToString();
                                sys_ds.chk_date = dataReader["sys_chk_date"].ToString();
                                sys_ds.chk_name_id = dataReader["sys_chk_name_id"].ToString();
                                system_List.Add(sys_ds);

                                s_ds.siteno = dataReader["siteno"].ToString();
                                s_ds.sitename = dataReader["sitename"].ToString();
                                s_ds.address1 = dataReader["address1"].ToString();
                                s_ds.address2 = dataReader["address2"].ToString();
                                s_ds.telno = dataReader["telno"].ToString();
                                s_ds.status = dataReader["s_status"].ToString();
                                s_ds.userno = dataReader["userno"].ToString();
                                s_ds.systemno = dataReader["systemno"].ToString();
                                s_ds.biko = dataReader["sitebiko"].ToString();
                                s_ds.chk_date = dataReader["s_chk_date"].ToString();
                                s_ds.chk_name_id = dataReader["s_chk_name_id"].ToString();
                                site_List.Add(s_ds);

                                h_ds.host_no = dataReader["host_no"].ToString();
                                h_ds.hostname = dataReader["hostname"].ToString();
                                h_ds.hostname_ja = dataReader["hostname_ja"].ToString();
                                h_ds.status = dataReader["h_status"].ToString();
                                h_ds.device = dataReader["device"].ToString();
                                h_ds.location = dataReader["location"].ToString();
                                h_ds.usefor = dataReader["usefor"].ToString();
                                h_ds.kansiStartdate = dataReader["kansiStartdate"].ToString();
                                h_ds.kansiEndsdate = dataReader["kansiEndsdate"].ToString();
                                h_ds.hosyukanri = dataReader["hosyukanri"].ToString();
                                h_ds.hosyuinfo = dataReader["hosyuinfo"].ToString();
                                h_ds.biko = dataReader["hostbiko"].ToString();
                                h_ds.userno = dataReader["userno"].ToString();
                                h_ds.systemno = dataReader["systemno"].ToString();
                                h_ds.siteno = dataReader["siteno"].ToString();
                                h_ds.chk_date = dataReader["h_chk_date"].ToString();
                                h_ds.chk_name_id = dataReader["h_chk_name_id"].ToString();
                                host_List.Add(h_ds);

                                w_ds.watch_Interfaceno = dataReader["kennshino"].ToString();
                                w_ds.interfacename = dataReader["interfacename"].ToString();
                                w_ds.status = dataReader["w_status"].ToString();
                                w_ds.type = dataReader["type"].ToString();
                                w_ds.kanshi = dataReader["kanshi"].ToString();
                                w_ds.start_date = dataReader["start_date"].ToString();
                                w_ds.end_date = dataReader["end_date"].ToString();
                                w_ds.border = dataReader["border"].ToString();
                                w_ds.IPaddress = dataReader["IPaddress"].ToString();
                                w_ds.IPaddressNAT = dataReader["IPaddressNAT"].ToString();
                                w_ds.chk_date = dataReader["w_chk_date"].ToString();
                                w_ds.chk_name_id = dataReader["w_chk_name_id"].ToString();
                                w_ds.userno = dataReader["userno"].ToString();
                                w_ds.systemno = dataReader["systemno"].ToString();
                                w_ds.siteno = dataReader["siteno"].ToString();
                                w_ds.host_no = dataReader["host_no"].ToString();

                            interface_List.Add(w_ds);
                         
                        }




                        if (i == 1)
                        {
                            if (hostname != null)
                            {
                                w_ds.watch_Interfaceno = dataReader["kennshino"].ToString();
                                w_ds.interfacename = dataReader["interfacename"].ToString();
                                w_ds.status = dataReader["w_status"].ToString();
                                w_ds.type = dataReader["type"].ToString();
                                w_ds.kanshi = dataReader["kanshi"].ToString();
                                w_ds.start_date = dataReader["start_date"].ToString();
                                w_ds.end_date = dataReader["end_date"].ToString();
                                w_ds.border = dataReader["border"].ToString();
                                w_ds.IPaddress = dataReader["IPaddress"].ToString();
                                w_ds.IPaddressNAT = dataReader["IPaddressNAT"].ToString();
                                w_ds.userno = dataReader["userno"].ToString();
                                w_ds.systemno = dataReader["systemno"].ToString();
                                w_ds.siteno = dataReader["siteno"].ToString();
                                w_ds.host_no = dataReader["host_no"].ToString();

                                w_ds.chk_date = dataReader["w_chk_date"].ToString();
                                w_ds.chk_name_id = dataReader["w_chk_name_id"].ToString();
                                interface_List.Add(w_ds);
                            }
                            else if (sitename != null)
                            {
                                h_ds.host_no = dataReader["host_no"].ToString();
                                h_ds.hostname = dataReader["hostname"].ToString();
                                h_ds.hostname_ja = dataReader["hostname_ja"].ToString();
                                h_ds.status = dataReader["h_status"].ToString();
                                h_ds.device = dataReader["device"].ToString();
                                h_ds.location = dataReader["location"].ToString();
                                h_ds.usefor = dataReader["usefor"].ToString();
                                h_ds.kansiStartdate = dataReader["kansiStartdate"].ToString();
                                h_ds.kansiEndsdate = dataReader["kansiEndsdate"].ToString();
                                h_ds.hosyukanri = dataReader["hosyukanri"].ToString();
                                h_ds.hosyuinfo = dataReader["hosyuinfo"].ToString();
                                h_ds.biko = dataReader["hostbiko"].ToString();
                                h_ds.chk_date = dataReader["h_chk_date"].ToString();
                                h_ds.chk_name_id = dataReader["h_chk_name_id"].ToString();
                                host_List.Add(h_ds);

                                w_ds.watch_Interfaceno = dataReader["kennshino"].ToString();
                                w_ds.interfacename = dataReader["interfacename"].ToString();
                                w_ds.status = dataReader["w_status"].ToString();
                                w_ds.type = dataReader["type"].ToString();
                                w_ds.kanshi = dataReader["kanshi"].ToString();
                                w_ds.start_date = dataReader["start_date"].ToString();
                                w_ds.end_date = dataReader["end_date"].ToString();
                                w_ds.border = dataReader["border"].ToString();
                                w_ds.IPaddress = dataReader["IPaddress"].ToString();
                                w_ds.IPaddressNAT = dataReader["IPaddressNAT"].ToString();
                                w_ds.chk_date = dataReader["w_chk_date"].ToString();
                                w_ds.chk_name_id = dataReader["w_chk_name_id"].ToString();
                                w_ds.userno = dataReader["userno"].ToString();
                                w_ds.systemno = dataReader["systemno"].ToString();
                                w_ds.siteno = dataReader["siteno"].ToString();
                                w_ds.host_no = dataReader["host_no"].ToString();

                                interface_List.Add(w_ds);

                            }
                            else if (systemname != null)
                            {
                                s_ds.siteno = dataReader["siteno"].ToString();
                                s_ds.sitename = dataReader["sitename"].ToString();
                                s_ds.address1 = dataReader["address1"].ToString();
                                s_ds.address2 = dataReader["address2"].ToString();
                                s_ds.telno = dataReader["telno"].ToString();
                                s_ds.userno = dataReader["userno"].ToString();
                                s_ds.systemno = dataReader["systemno"].ToString();

                                s_ds.status = dataReader["s_status"].ToString();
                                s_ds.biko = dataReader["sitebiko"].ToString();
                                s_ds.chk_date = dataReader["s_chk_date"].ToString();
                                s_ds.chk_name_id = dataReader["s_chk_name_id"].ToString();
                                site_List.Add(s_ds);

                                h_ds.host_no = dataReader["host_no"].ToString();
                                h_ds.hostname = dataReader["hostname"].ToString();
                                h_ds.hostname_ja = dataReader["hostname_ja"].ToString();
                                h_ds.status = dataReader["h_status"].ToString();
                                h_ds.device = dataReader["device"].ToString();
                                h_ds.location = dataReader["location"].ToString();
                                h_ds.usefor = dataReader["usefor"].ToString();
                                h_ds.kansiStartdate = dataReader["kansiStartdate"].ToString();
                                h_ds.kansiEndsdate = dataReader["kansiEndsdate"].ToString();
                                h_ds.hosyukanri = dataReader["hosyukanri"].ToString();
                                h_ds.hosyuinfo = dataReader["hosyuinfo"].ToString();
                                h_ds.biko = dataReader["hostbiko"].ToString();
                                h_ds.chk_date = dataReader["h_chk_date"].ToString();
                                h_ds.chk_name_id = dataReader["h_chk_name_id"].ToString();
                                host_List.Add(h_ds);

                                w_ds.watch_Interfaceno = dataReader["kennshino"].ToString();
                                w_ds.interfacename = dataReader["interfacename"].ToString();
                                w_ds.status = dataReader["w_status"].ToString();
                                w_ds.type = dataReader["type"].ToString();
                                w_ds.kanshi = dataReader["kanshi"].ToString();
                                w_ds.start_date = dataReader["start_date"].ToString();
                                w_ds.end_date = dataReader["end_date"].ToString();
                                w_ds.border = dataReader["border"].ToString();
                                w_ds.IPaddress = dataReader["IPaddress"].ToString();
                                w_ds.IPaddressNAT = dataReader["IPaddressNAT"].ToString();
                                w_ds.chk_date = dataReader["w_chk_date"].ToString();
                                w_ds.chk_name_id = dataReader["w_chk_name_id"].ToString();
                                w_ds.userno = dataReader["userno"].ToString();
                                w_ds.systemno = dataReader["systemno"].ToString();
                                w_ds.siteno = dataReader["siteno"].ToString();
                                w_ds.host_no = dataReader["host_no"].ToString();

                                interface_List.Add(w_ds);
                            }
                        }
                    }
                    if (displist.user_L == null || displist.user_L.Count==0) displist.user_L = user_List;
                    if (displist.system_L == null || displist.system_L.Count == 0) displist.system_L = system_List;
                    if (displist.site_L == null || displist.site_L.Count == 0) displist.site_L = site_List;

                    if (displist.host_L == null || displist.host_L.Count == 0) displist.host_L = host_List;
                    if (displist.watch_L == null || displist.watch_L.Count == 0) displist.watch_L = interface_List;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("情報一覧取得エラー " + ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return displist;
        }

        //タイマーで有効なものをすべて表示する
        public List<scheduleDS> getTimerList(string userno,string systemno,Boolean searchflg = false)
        {

            string searchstring = "";
            DateTime nowdt = DateTime.Now;
            string nowString = nowdt.ToString("yyyy-MM-dd HH:mm");

            //未完了のものを表示
            searchstring = "where sc.status= '0' AND sc.end_date <= '" + nowString + "'";
            if (searchflg)
            {
                searchstring += " AND sc.userno=" + userno + "and sc.systemno=" + systemno;

            }
            NpgsqlCommand cmd;
            scheduleDS ds;
            List<scheduleDS> retList = null;
            //DB接続
            try
            {
                if(con.FullState != ConnectionState.Open) con.Open();

                //SELECT実行
                cmd = new NpgsqlCommand(@"SELECT sc.schedule_no,sc.userno,sc.systemno,sc.timer_name,sc.schedule_type,sc.status,sc.repeat_type,sc.start_date,sc.end_date,sc.status,sc.alerm_message,sc.incident_no,sc.userno,u.username,sc.systemno,sys.systemname,sc.chk_date,sc.chk_name_id " +
                    "FROM schedule sc LEFT OUTER JOIN user_tbl u ON sc.userno = u.userno "  +
                    "LEFT OUTER JOIN system sys ON sys.systemno = sc.systemno " + searchstring, con);

                var dataReader = cmd.ExecuteReader();

                //スケジュール情報の取得
                retList = new List<scheduleDS>();

                while (dataReader.Read())
                {

                    ds = new scheduleDS();

                    ds.schedule_no = dataReader["schedule_no"].ToString();
                    ds.userno = dataReader["userno"].ToString();
                    ds.systemno = dataReader["systemno"].ToString();
                    ds.timer_name = dataReader["timer_name"].ToString();
                    ds.schedule_type = dataReader["schedule_type"].ToString();
                    ds.status = dataReader["status"].ToString();
                    ds.repeat_type = dataReader["repeat_type"].ToString();
                    ds.start_date = dataReader["start_date"].ToString();
                    ds.end_date = dataReader["end_date"].ToString();
                    ds.kakunin = dataReader["alerm_message"].ToString();
                    ds.incident_no = dataReader["incident_no"].ToString();
                    ds.chk_name_id = dataReader["chk_name_id"].ToString();
                    ds.chk_date = dataReader["chk_date"].ToString();


                    retList.Add(ds);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("定期作業情報一覧取得エラー " + ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }

            return retList;
        }

        //アラート通知が必要なものを取得する
        public List<alermDS> getAlert()
        {
            NpgsqlCommand cmd;
            alermDS ds;
            List<alermDS> retList = null;
            DateTime nowdt = DateTime.Now;
            string nowString = nowdt.ToString("yyyy-MM-dd HH:mm");
            //DB接続
            try
            {
                if (con.FullState != ConnectionState.Open) con.Open();

                //SELECT実行
                cmd = new NpgsqlCommand(@"select u.userno,u.username,sys.systemno,sys.systemname,s.timer_name,s.sound,s.incident_no,s.alerm_message,t.schedule_no,t.schedule_type, t.alertdatetime,t.chk_name_id,t.chk_date from " + 
                    "timer_taiou t INNER JOIN schedule s ON t.schedule_no = s.schedule_no " +
                    "LEFT OUTER JOIN user_tbl u ON s.userno = u.userno " +
                    "LEFT OUTER JOIN system sys ON s.systemno = sys.systemno " +
                    "where s.status='0' and  s.start_date <= '" + nowString +   "' and end_date >= '" + nowString + "' " +
                    "and t.alertdatetime <= '" + nowString + "' and t.taioudate is null", con);
                var dataReader = cmd.ExecuteReader();

                //スケジュール情報の取得
                retList = new List<alermDS>();
                int i = 0;
                while (dataReader.Read())
                {

                    ds = new alermDS();
                    ds.userno = dataReader["userno"].ToString();
                    ds.username = dataReader["username"].ToString();
                    ds.systemno = dataReader["systemno"].ToString();
                    ds.systemname = dataReader["systemname"].ToString();

                    ds.alerm_message = dataReader["alerm_message"].ToString();

                    ds.timer_name = dataReader["timer_name"].ToString();

                    ds.sound = dataReader["sound"].ToString();

                    if (!dataReader.IsDBNull(dataReader.GetOrdinal("sound")))
                    {
                        File.WriteAllBytes("sound" + i.ToString() + ".wav", (byte[])dataReader["sound"]);

                        if (dataReader["sound"] != null)
                            ds.sound = "sound" + i.ToString() + ".wav";
                    }
                    
                    ds.incident_no = dataReader["incident_no"].ToString();
                    ds.alerm_message = dataReader["alerm_message"].ToString();
                    ds.schedule_no = dataReader["schedule_no"].ToString();
                    ds.schedule_type = dataReader["schedule_type"].ToString();
                    ds.alertdatetime = dataReader["alertdatetime"].ToString();
                    ds.chk_name_id = dataReader["chk_name_id"].ToString();
                    ds.chk_date = dataReader["chk_date"].ToString();

                    retList.Add(ds);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("タイマーアラート情報一覧取得エラー " + ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }

            return retList;

        }
    }
}