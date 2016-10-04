namespace MainExecute
{
    using IWshRuntimeLibrary;
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    internal static class MainE
    {
        public const int LOCALE_SLONGDATE = 0x20;
        public const int LOCALE_SSHORTDATE = 0x1f;
        public const int LOCALE_STIME = 0x1003;
        internal static string UnInfo;

        [DllImport("ReadAreaCode.dll", CallingConvention=CallingConvention.Cdecl)]
        private static extern int GetKpNo(byte[] machineno, byte[] path);
        [DllImport("ReadAreaCode.dll", CallingConvention=CallingConvention.Cdecl)]
        private static extern int GetRealTaxCode(byte[] taxcode, byte[] path);
        [DllImport("kernel32.dll")]
        public static extern int GetSystemDefaultLCID();
        [STAThread]
        private static void Main(string[] args)
        {
            try
            {
                string str12;
                string str20;
                SetDateTimeFormat();
                string str = ((args == null) || (args.Length <= 0)) ? "" : args[0];
                if ((str != null) && !str.Equals("uninst"))
                {
                    Integration integration = new Integration();
                    integration.HandleSZHY();
                    integration.HandleAreaCode();
                }
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwkp.exe", true);
                string[] subKeyNames = key.GetSubKeyNames();
                if (subKeyNames.Length == 0)
                {
                    MessageBox.Show("检测到本计算机没有安装【税控发票开票软件（金税盘版）】！", "错误");
                    return;
                }
                Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>();
                for (int i = 0; i < subKeyNames.Length; i++)
                {
                    RegistryKey key2 = key.OpenSubKey(subKeyNames[i]);
                    dictionary.Add(subKeyNames[i], new string[] { key2.GetValue("code", "") as string, key2.GetValue("machine", "") as string, key2.GetValue("orgcode", "") as string, key2.GetValue("Version", "") as string, key2.GetValue("Path", "") as string, key2.GetValue("", "") as string, key2.GetValue("DataBaseVersion", "") as string });
                }
                if (subKeyNames.Length == 1)
                {
                    string[] strArray2 = dictionary[subKeyNames[0]];
                    key.SetValue("code", strArray2[0]);
                    key.SetValue("machine", strArray2[1]);
                    key.SetValue("orgcode", strArray2[2]);
                    key.SetValue("Version", strArray2[3]);
                    key.SetValue("Path", strArray2[4]);
                    key.SetValue("", strArray2[5]);
                    key.SetValue("DataBaseVersion", strArray2[6]);
                    ProcessStartInfo info = new ProcessStartInfo();
                    if (str.Equals("uninst"))
                    {
                        UnInfo = subKeyNames[0];
                        info.FileName = Path.Combine(strArray2[4], "uninst.exe");
                    }
                    else
                    {
                        info.FileName = strArray2[5];
                    }
                    info.Arguments = "";
                    info.UseShellExecute = true;
                    info.WindowStyle = ProcessWindowStyle.Normal;
                    Process.Start(info);
                }
                else
                {
                    int realTaxCode = -1;
                    int kpNo = -1;
                    string str2 = "";
                    if (!str.Equals("uninst"))
                    {
                        string str3 = "";
                        foreach (string[] strArray3 in dictionary.Values)
                        {
                            str3 = Path.Combine(strArray3[4], "Bin/JSDiskDLL.dll");
                            if (File.Exists(str3))
                            {
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(str3))
                        {
                            MessageBox.Show("【税控发票开票软件(金税盘版)】安装文件异常！", "异常");
                            return;
                        }
                        byte[] taxcode = new byte[0x19];
                        realTaxCode = GetRealTaxCode(taxcode, Encoding.GetEncoding("GB18030").GetBytes(str3));
                        string str4 = Encoding.GetEncoding("GB18030").GetString(taxcode).Trim(new char[1]);
                        byte[] machineno = new byte[5];
                        kpNo = GetKpNo(machineno, Encoding.GetEncoding("GB18030").GetBytes(str3));
                        string str5 = Encoding.GetEncoding("GB18030").GetString(machineno).Trim(new char[1]);
                        str2 = str4 + "." + str5;
                    }
                    if (((realTaxCode + kpNo) == 0) && dictionary.ContainsKey(str2))
                    {
                        string[] strArray4 = dictionary[str2];
                        key.SetValue("code", strArray4[0]);
                        key.SetValue("machine", strArray4[1]);
                        key.SetValue("orgcode", strArray4[2]);
                        key.SetValue("Version", strArray4[3]);
                        key.SetValue("Path", strArray4[4]);
                        key.SetValue("", strArray4[5]);
                        key.SetValue("DataBaseVersion", strArray4[6]);
                        ProcessStartInfo info2 = new ProcessStartInfo {
                            FileName = strArray4[5],
                            Arguments = "",
                            UseShellExecute = true,
                            WindowStyle = ProcessWindowStyle.Normal
                        };
                        Process.Start(info2);
                    }
                    else
                    {
                        MainForm.infos = new string[(subKeyNames.Length > 20) ? 20 : subKeyNames.Length];
                        MainForm.flag = str;
                        Array.Copy(subKeyNames, MainForm.infos, MainForm.infos.Length);
                        MainForm.corpinfo = dictionary;
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new MainForm());
                    }
                }
                if (string.IsNullOrEmpty(UnInfo) || !str.Equals("uninst"))
                {
                    return;
                }
                Thread.Sleep(0x1388);
                bool createdNew = false;
                do
                {
                    new Mutex(true, "开票软件_UnInstall", out createdNew).Close();
                    Thread.Sleep(500);
                }
                while (!createdNew);
                subKeyNames = key.GetSubKeyNames();
                for (int j = 0; j < subKeyNames.Length; j++)
                {
                    string str6 = key.OpenSubKey(subKeyNames[j]).GetValue("Path", "") as string;
                    if ((str6.Length == 0) || !Directory.Exists(str6))
                    {
                        key.DeleteSubKey(subKeyNames[j]);
                    }
                }
                subKeyNames = key.GetSubKeyNames();
                if (subKeyNames.Length == 0)
                {
                    try
                    {
                        key.Close();
                        key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths", true);
                        key.DeleteSubKey("fwkp.exe");
                        key.Close();
                        key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true);
                        key.DeleteSubKeyTree("开票软件");
                        key.Close();
                        string str7 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "开票软件.lnk");
                        if (File.Exists(str7))
                        {
                            File.Delete(str7);
                        }
                        str7 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory), "开票软件.lnk");
                        if (File.Exists(str7))
                        {
                            File.Delete(str7);
                        }
                        str7 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Programs), "开票软件");
                        if (Directory.Exists(str7))
                        {
                            Directory.Delete(str7, true);
                        }
                        str7 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonPrograms), "开票软件");
                        if (Directory.Exists(str7))
                        {
                            Directory.Delete(str7, true);
                        }
                        str7 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "税控发票开票软件（金税盘版）.lnk");
                        if (File.Exists(str7))
                        {
                            File.Delete(str7);
                        }
                    }
                    catch (Exception)
                    {
                    }
                    string str8 = dictionary[UnInfo][4].Substring(0, dictionary[UnInfo][4].LastIndexOf(@"\") + 1);
                    string[] strArray5 = Directory.GetDirectories(str8);
                    string[] strArray6 = Directory.GetFiles(str8);
                    if ((strArray5.Length == 0) && (strArray6.Length == 2))
                    {
                        string str9 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "remove.bat");
                        StreamWriter writer = new StreamWriter(str9, false, Encoding.Default);
                        writer.WriteLine("ping /n 2 127.1>nul");
                        writer.WriteLine("cd ..");
                        writer.WriteLine(string.Format("del \"{0}\" /q", str8));
                        writer.WriteLine(string.Format("rmdir \"{0}\" /q", str8));
                        writer.WriteLine(string.Format("del \"{0}\" /q", str9));
                        writer.Close();
                        ProcessStartInfo info3 = new ProcessStartInfo(str9) {
                            WindowStyle = ProcessWindowStyle.Hidden
                        };
                        Process.Start(info3);
                        Environment.Exit(0);
                    }
                    return;
                }
                string str10 = (key.GetValue("code") as string) + "." + (key.GetValue("machine") as string);
                if (!subKeyNames.Contains<string>(str10))
                {
                    key.DeleteValue("code", false);
                    key.DeleteValue("machine", false);
                    key.DeleteValue("orgcode", false);
                    key.DeleteValue("Version", false);
                    key.DeleteValue("Path", false);
                    key.DeleteValue("", false);
                    key.DeleteValue("DataBaseVersion", false);
                }
                string path = dictionary[UnInfo][4].Substring(0, dictionary[UnInfo][4].LastIndexOf(@"\") + 1);
                string[] directories = Directory.GetDirectories(path);
                string[] files = Directory.GetFiles(path);
                if ((directories.Length != 0) || (files.Length != 2))
                {
                    return;
                }
                int index = 0;
                while (index < subKeyNames.Length)
                {
                    str12 = Path.Combine(Directory.GetParent(dictionary[subKeyNames[index]][4]).FullName, "MainExecute.exe");
                    if (Directory.Exists(dictionary[subKeyNames[index]][4]) && File.Exists(str12))
                    {
                        goto Label_085E;
                    }
                    index++;
                }
                goto Label_0E96;
            Label_085E:
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\开票软件", true);
                key.SetValue("DisplayIcon", str12);
                key.SetValue("UninstallString", "\"" + str12 + "\"  uninst");
                string str13 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "开票软件.lnk");
                if (File.Exists(str13))
                {
                    WshShell shell = (WshShell) Activator.CreateInstance(System.Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8")));
                    IWshShortcut shortcut = (IWshShortcut) shell.CreateShortcut(str13);
                    shortcut.TargetPath = str12;
                    shortcut.Arguments = "";
                    shortcut.WindowStyle = 1;
                    shortcut.Save();
                }
                str13 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory), "开票软件.lnk");
                if (File.Exists(str13))
                {
                    WshShell shell2 = (WshShell) Activator.CreateInstance(System.Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8")));
                    IWshShortcut shortcut2 = (IWshShortcut) shell2.CreateShortcut(str13);
                    shortcut2.TargetPath = str12;
                    shortcut2.Arguments = "";
                    shortcut2.WindowStyle = 1;
                    shortcut2.Save();
                }
                str13 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Programs), "开票软件");
                if (Directory.Exists(str13))
                {
                    string pathLink = Path.Combine(str13, "开票软件.lnk");
                    WshShell shell3 = (WshShell) Activator.CreateInstance(System.Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8")));
                    IWshShortcut shortcut3 = (IWshShortcut) shell3.CreateShortcut(pathLink);
                    shortcut3.TargetPath = str12;
                    shortcut3.Arguments = "";
                    shortcut3.WindowStyle = 1;
                    shortcut3.Save();
                    string str15 = Path.Combine(str13, "卸载.lnk");
                    WshShell shell4 = (WshShell) Activator.CreateInstance(System.Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8")));
                    IWshShortcut shortcut4 = (IWshShortcut) shell4.CreateShortcut(str15);
                    shortcut4.TargetPath = str12;
                    shortcut4.Arguments = "uninst";
                    shortcut4.WindowStyle = 1;
                    shortcut4.Save();
                    string str16 = Path.Combine(str13, "税务数字证书管理工具.lnk");
                    WshShell shell1 = (WshShell) Activator.CreateInstance(System.Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8")));
                    IWshShortcut shortcut5 = (IWshShortcut) shell4.CreateShortcut(str16);
                    shortcut5.TargetPath = Path.Combine(dictionary[subKeyNames[index]][4], @"Bin\CertManageruser.exe");
                    shortcut5.Arguments = "";
                    shortcut5.WindowStyle = 1;
                    shortcut5.Save();
                }
                str13 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonPrograms), "开票软件");
                if (Directory.Exists(str13))
                {
                    string str17 = Path.Combine(str13, "开票软件.lnk");
                    WshShell shell5 = (WshShell) Activator.CreateInstance(System.Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8")));
                    IWshShortcut shortcut6 = (IWshShortcut) shell5.CreateShortcut(str17);
                    shortcut6.TargetPath = str12;
                    shortcut6.Arguments = "";
                    shortcut6.WindowStyle = 1;
                    shortcut6.Save();
                    string str18 = Path.Combine(str13, "卸载.lnk");
                    WshShell shell6 = (WshShell) Activator.CreateInstance(System.Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8")));
                    IWshShortcut shortcut7 = (IWshShortcut) shell6.CreateShortcut(str18);
                    shortcut7.TargetPath = "\"" + str12 + "\"  uninst";
                    shortcut7.Arguments = "";
                    shortcut7.WindowStyle = 1;
                    shortcut7.Save();
                    string str19 = Path.Combine(str13, "税务数字证书管理工具.lnk");
                    WshShell shell8 = (WshShell) Activator.CreateInstance(System.Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8")));
                    IWshShortcut shortcut8 = (IWshShortcut) shell6.CreateShortcut(str19);
                    shortcut8.TargetPath = Path.Combine(dictionary[subKeyNames[index]][4], @"Bin\CertManageruser.exe");
                    shortcut8.Arguments = "";
                    shortcut8.WindowStyle = 1;
                    shortcut8.Save();
                }
                str13 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "税控发票开票软件（金税盘版）.lnk");
                if (File.Exists(str13))
                {
                    WshShell shell7 = (WshShell) Activator.CreateInstance(System.Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8")));
                    IWshShortcut shortcut9 = (IWshShortcut) shell7.CreateShortcut(str13);
                    shortcut9.TargetPath = str12;
                    shortcut9.Arguments = "";
                    shortcut9.WindowStyle = 1;
                    shortcut9.Save();
                }
            Label_0E96:
                str20 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "remove.bat");
                StreamWriter writer2 = new StreamWriter(str20, false, Encoding.Default);
                writer2.WriteLine("ping /n 2 127.1>nul");
                writer2.WriteLine("cd ..");
                writer2.WriteLine(string.Format("del \"{0}\" /q", path));
                writer2.WriteLine(string.Format("rmdir \"{0}\" /q", path));
                writer2.WriteLine(string.Format("del \"{0}\" /q", str20));
                writer2.Close();
                ProcessStartInfo startInfo = new ProcessStartInfo(str20) {
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                Process.Start(startInfo);
                Environment.Exit(0);
            }
            catch (Exception exception)
            {
                MessageBox.Show(string.Format("【税控发票开票软件(金税盘版)】异常！\n{0}", exception.ToString()), "异常");
            }
        }

        public static void SetDateTimeFormat()
        {
            try
            {
                int systemDefaultLCID = GetSystemDefaultLCID();
                SetLocaleInfoA(systemDefaultLCID, 0x1003, "HH:mm:ss");
                SetLocaleInfoA(systemDefaultLCID, 0x1f, "yyyy-MM-dd");
                SetLocaleInfoA(systemDefaultLCID, 0x20, "yyyy-MM-dd");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        [DllImport("kernel32.dll")]
        public static extern int SetLocaleInfoA(int Locale, int LCType, string lpLCData);
    }
}

