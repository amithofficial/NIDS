using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.OleDb;


namespace Regression.Linear
{
    public class ExcelReader
    {

        private string path;
        private string strConnection;


        public ExcelReader(string path, bool hasHeaders, bool hasMixedData)
        {
            this.path = path;
            OleDbConnectionStringBuilder strBuilder = new OleDbConnectionStringBuilder();
            strBuilder.Provider = "Microsoft.Jet.OLEDB.4.0";
            strBuilder.DataSource = path;
            strBuilder.Add("Extended Properties", "Excel 8.0;"+
                "HDR=" + (hasHeaders ? "Yes" : "No") + ';' +
                "Imex=" + (hasMixedData ? "2" : "0") + ';' +
              ""); 

            
            strConnection = strBuilder.ToString();
        }

        public string[] GetWorksheetList()
        {
            string[] worksheets;

            try
            {
                OleDbConnection connection = new OleDbConnection(strConnection);
                connection.Open();
                DataTable tableWorksheets = connection.GetSchema("Tables");
                connection.Close();

                worksheets = new string[tableWorksheets.Rows.Count];

                for (int i = 0; i < worksheets.Length; i++)
                {
                    worksheets[i] = (string)tableWorksheets.Rows[i]["TABLE_NAME"];
                    worksheets[i] = worksheets[i].Remove(worksheets[i].Length - 1).Trim('"', '\'');
                    // removes the trailing $ and other characters appended in the table name
                    while (worksheets[i].EndsWith("$"))
                        worksheets[i] = worksheets[i].Remove(worksheets[i].Length - 1).Trim('"', '\'');
                }
            }
            catch (OleDbException ex)
            {
                /* 
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        MessageBox.Show("Index #" + i + "\n" +
                        "Message: " + myException.Errors[i].Message + "\n" +
                        "Native: " +
                            myException.Errors[i].NativeError.ToString() + "\n" +
                        "Source: " + myException.Errors[i].Source + "\n" +
                        "SQL: " + myException.Errors[i].SQLState + "\n");
                    }
                 */
                throw;
            }

            return worksheets;
        }

        public string[] GetColumnsList(string worksheet)
        {
            string[] columns;

            try
            {
                OleDbConnection connection = new OleDbConnection(strConnection);
                connection.Open();
                DataTable tableColumns = connection.GetSchema("Columns", new string[] {null, null, worksheet + '$', null});
                connection.Close();

                columns = new string[tableColumns.Rows.Count];

                for (int i = 0; i < columns.Length; i++)
                {
                    columns[i] = (string)tableColumns.Rows[i]["COLUMN_NAME"];
                }
            }
            catch
            {
                throw;
            }

            return columns;
        }

        public DataTable GetWorksheet(string worksheet)
        {
            DataTable ws;
           // string str = "SELECT F1 As duration, udp As protocol, private As flag, SF As dst_bytes, F5 As land, F6 As wrong_fragment, F7 As urgent, F8 As hot, F9 As num_failed_logins, F10 As logged_in, F11 As num_compromised, F12 As root_shell, F13 As su_attempted, F14 As num_root, F15 As num_file_creations, F16 As num_shells, F17 As num_access_files, F18 As num_outbound_cmds, F19 As is_host_login, F20 As is_guest_login, F21 As count, F22 As srv_count, F23 As vserror_rate, F24 As srv_serror_rate, F25 As rerror_rate, F26 As srv_rerror_rate, F27 As same_srv_rate, F28 As diff_srv_rate, F29 As srv_diff_host_rate, F30 As dst_host_count, F31 As dst_host_srv_count, F32 As dst_host_same_srv_rate, F33 As dst_host_diff_srv_rate, F34 As dst_host_same_src_port_rate, F35 As dst_host_srv_diff_host_rate, F36 As dst_host_serror_rate, F37 As dst_host_srv_serror_rate, F38 As dst_host_rerror_rate, F39 As dst_host_srv_rerror_rate, F40 class1  FROM [{0}$]";
           // string str1 = "SELECT F1 As duration, udp As protocol, private As flag, SF As dstbytes, F5 As land, F6 As wrongfragment, F7 As urgent, F8 As hot, F9 As numfailedlogins,F10 As loggedin, F11 As numcompromised, F12 As rootshell, F13 As suattempted, F14 As numroot, F15 As numfilecreations, F16 As numshells, F17 As numaccessfiles, F18 As numoutboundcmds, F19 As ishostlogin, F20 As isguestlogin, F21 As count1, F22 As srvcount FROM [{0}$]";
            

                string str2 = "SELECT F1 As duration, udp As protocol, private As flag, SF As dstbytes, F5 As land, F6 As wrongfragment, F7 As urgent, F8 As hot, F9 As numfailedlogins, F10 As loggedin, F11 As numcompromised, F12 As rootshell, F13 As suattempted, F14 As numroot, F15 As numfilecreations, F16 As numshells, F17 As numaccessfiles, F18 As numoutboundcmds, F19 As ishostlogin, F20 As isguestlogin, F21 As count1, F22 As srvcount, F23 As vserrorrate, F24 As srvserrorrate, F25 As rerrorrate, F26 As srvrerrorrate, F27 As samesrvrate, F28 As diffsrvrate, F29 As srvdiffhostrate, F30 As dsthostcount, F31 As dsthostsrvcount, F32 As dsthostsamesrvrate, F33 As dsthostdiffsrvrate, F34 As dsthostsamesrcportrate, F35 As dsthostsrvdiffhostrate, F36 As dsthostserrorrate, F37 As dsthostsrvserrorrate, F38 As dsthostrerrorrate, F39 As dsthostsrvrerrorrate, F40 As class1  FROM [{0}$]";
                OleDbConnection connection = new OleDbConnection(strConnection);
                OleDbDataAdapter adaptor = new OleDbDataAdapter(String.Format("SELECT * FROM [{0}$]", worksheet), connection);
              //  OleDbDataAdapter adaptor = new OleDbDataAdapter(String.Format(str2, worksheet), connection);
                string str111 = String.Format(str2, worksheet);
             //   OleDbDataAdapter adaptor = new OleDbDataAdapter(String.Format("SELECT * FROM [{0}${1}]", worksheet, "A2:ZZ"), connection);
                ws = new DataTable(worksheet);
                adaptor.FillSchema(ws, SchemaType.Source);
                adaptor.Fill(ws);

                adaptor.Dispose();
                connection.Close();

                return ws;
           
        }

        public DataSet GetWorkplace()
        {
            DataSet workplace;

            OleDbConnection connection = new OleDbConnection(strConnection);
            OleDbDataAdapter adaptor = new OleDbDataAdapter("SELECT * FROM *", connection);
            workplace = new DataSet();
            adaptor.FillSchema(workplace, SchemaType.Source);
            adaptor.Fill(workplace);

            adaptor.Dispose();
            connection.Close();

            return workplace;
        }
    }
}
