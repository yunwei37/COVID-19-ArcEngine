using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        /// 
        [STAThread]


        static void Main()

        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            Application.Run(new Form1());

        }
    }
}
