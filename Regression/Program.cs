using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Regression.Linear
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static int a = 20;
        public static string a1 = "Mzcy";
        public static string b = "NjE2";
        public static string  c = "Nw==";
        public static string d = "MTI=";
        public static string type1 = "prototype";
        public static string type2 = "num_comp";
        public static string type3 = "isguestlogin";
        public static string type4 = "service";
        public static string type5 = "rerror_rate";
        public static string type6 = "same_srv_rate";
        public static string type7 = "host_srv_rate";
        public static string vtime = "";
        public static string otime = "";
        //public static int x11 = 240;
        //public static int x12 = 240;
        //public static int x13 = 240;
        //public static int x14 = 240;
        //public static int x15 = 240;
        //public static int x16 = 240;
        //public static int x17 = 256;
        //public static int x18 = 240;
        //public static int x19 = 240;
        //public static int y21 = 256;
        //public static int y22 = 256;
        //public static int y23 = 256;
        //public static int y24 = 256;
        //public static int y25 = 256;
        //public static int y26 = 256;
        //public static int y27 = 256;
        //public static int y28 = 256;
        //public static int y29 = 256;

        public static string f1 = "normal.xls";
        public static string f2="dos.xls";
        public static string f3="probe.xls";
        public static string f4="r2l.xls";
        public static string f5 = "u2r.xls";
        public static string f6 = "mydata3.csv";
        public static string f7 = "mdata.csv";
        public static string f8 = "mydata2.csv";
        public static string f9 = "mdata4.csv";



        [STAThread]
        static void Main()
        {
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new adminlogin());
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
