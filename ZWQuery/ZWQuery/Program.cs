using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;

namespace ZWQuery
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string strRunMode = ConfigurationManager.AppSettings["RunMode"];

            Application.Run(new ZWGPTS.FormQuery(strRunMode));
        }
    }
}
