using System;
using System.Windows.Forms;
using NDesk.Options;

namespace GeoToMap
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string fileName = "";
            bool runInmediately = false;
            bool closeAfterRun = false;
            OptionSet optionSet =  new OptionSet();
            optionSet.Add("h|?|help","Show this help message\n",showhelp);
            optionSet.Add("f=", "Text file to open\n", file => fileName = file);
            optionSet.Add("r|run", "Run inmediately\n", v => runInmediately = true);
            optionSet.Add("c|close", "Close inmediately to run\n", v => closeAfterRun = true);
            optionSet.Parse(args);
            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmMain());
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmMain(fileName,runInmediately,closeAfterRun)); 
            }
        }

        static void showhelp(string m)
        {
            System.Console.WriteLine("ayuda " + m);
        }



 
    }
}
