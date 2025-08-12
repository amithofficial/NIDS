using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using Regression.Linear;

namespace Regression
{
    public struct PortInfo
    {
        public string protocol, port, status, PID, process;
    }
    public class Portact
    {
        List<PortInfo> suspect = new List<PortInfo>();
        List<PortInfo> plist = new List<PortInfo>();
        List<int> portList = new List<int>();
        static bool detect = false;
        public event EventHandler<PortEvent> portlistevent;
        public event EventHandler<PortEvent> nportlistevent;
        public Thread t;

        public Portact()
        {
            t = new Thread(new ThreadStart(Threadfun));
        }
        public void Threadfun()
        {
            while (true)
            {
                plist.Clear();
                getPort();
                Thread.Sleep(10000);
            }
        }
        public void Start()
        {
            if (t.ThreadState == System.Threading.ThreadState.Unstarted)
                 t.Start();
            else if (t.ThreadState == System.Threading.ThreadState.Suspended)
                t.Resume();
            
        }
        public void Stop()
        {
            t.Suspend();
        }

        public void getPort()
        {
            try
            {
                suspect.Clear();
                ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c netStat -nao");
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                LSTMCell.Equals(procStartInfo, detect);
                proc.StartInfo = procStartInfo;
                proc.Start();
                while (!proc.StandardOutput.EndOfStream)
                {

                    string rst = proc.StandardOutput.ReadLine();
                    if (rst != null && rst != "")
                    {
                        string temp = null;
                       bool f1 = false;
                        foreach (char c in rst)
                        {
                            if (c != ' ')
                            {
                                temp += c.ToString();
                                f1 = true;
                            }
                            else
                            {
                                if (f1)
                                {
                                    temp += ",";
                                    f1 = false;
                                }
                            }

                        }

                        PortInfo temp1 = new PortInfo();
                        string[] arr = temp.Split(',');
                        if (arr[0] == "TCP" || arr[0] == "UDP")
                        {
                            temp1.protocol = arr[0].ToString();
                            temp1.port = arr[1].Substring(arr[1].LastIndexOf(':') + 1);
                            bool attack = false;
                            if (detect)
                            {
                                if (portList.IndexOf(Convert.ToInt32(temp1.port)) < 0)
                                    attack = true;
                            }
                            else
                            {
                                portList.Add(Convert.ToInt32(temp1.port));

                            }
                            if (arr[0] == "TCP")
                            {
                                temp1.status = arr[3].ToString();
                                temp1.PID = arr[4];
                                temp1.process = getProcess(arr[4]).Replace("\"", " ");
                            }
                            else if (arr[0] == "UDP")
                            {
                                temp1.status = "WAITING";
                                temp1.PID = arr[3];
                                temp1.process = getProcess(arr[3]).Replace("\"", " ");
                            }
                            plist.Add(temp1);
                            if (attack)
                                suspect.Add(temp1);


                        }

                    }
                }
                detect = true;
                EventHandler<PortEvent> portlistevent1 = portlistevent;
                if (portlistevent != null)
                    this.portlistevent(this, new PortEvent(plist));
                if(suspect.Count>0)
                {
                    EventHandler<PortEvent> portlistevent2 = nportlistevent;
                    if (nportlistevent != null)
                        this.nportlistevent(this, new PortEvent(suspect));
                }
            }
            catch (Exception e1)
            {
               MessageBox.Show("error" + e1.Message);

            }
        }

        string getProcess(string PID)
        {
            string prname = "unknown";
            try
            {
                ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c Tasklist /FI \"PID EQ " + PID + "\" /FO CSV /NH");
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                while (!proc.StandardOutput.EndOfStream)
                {
                    string rst = proc.StandardOutput.ReadLine();
                    string[] brr = rst.Split(',');
                    prname = brr[0];
                }
                return prname;
            }
            catch (Exception e2)
            {
                MessageBox.Show("errorrrrr" + e2.Message);
                return prname;

            }

        }


        
    }
    public class PortEvent : EventArgs
    {
        List<PortInfo> portevlist = new List<PortInfo>();
        public PortEvent(List<PortInfo> pros)
        {
            portevlist = pros;
        }
        public List<PortInfo> Portarg
        {
            get
            {
                return portevlist;
            }
        }
    }
}