using InvAutomation;
using Microsoft.Win32;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

internal static class App
{
    [STAThread]
    private static void Main()
    {
        Mutex mutex;
        try
        {
            mutex = new Mutex(false, "InvAutomation");
        }
        catch (Exception)
        {
            return;
        }
        using (mutex)
        {
            if (!mutex.WaitOne(10, false))
            {
                string str = ConfigurationManager.AppSettings["StartFileName"];
                string keyName = ConfigurationManager.AppSettings["SoftPathRegPath"];
                string valueName = ConfigurationManager.AppSettings["SoftPathRegKey"];
                str = Path.Combine(Registry.GetValue(keyName, valueName, "").ToString(), str);
                new Process { StartInfo = { FileName = str, WindowStyle = ProcessWindowStyle.Normal } }.Start();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                
                Application.Run(new DlgDown());
            }
        }
    }
}

