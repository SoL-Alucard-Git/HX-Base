namespace MainExecute
{
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    public class Integration
    {
        private List<string> NecessaryDllNames = new List<string> { "JSDiskDLL.dll", "SQLite.Interop.dll", "ss1178.dll", "System.Data.SQLite.dll" };

        private void ChageUninstallInfo(string newFolderName, string oldFolderName, string uninstallFileInfoPath)
        {
            IniProperty.GetUninstallInfo(uninstallFileInfoPath);
            IniProperty.InitUninstallInfo();
            IniProperty.SetUninstallInfo(newFolderName, oldFolderName);
            IniProperty.SaveUninstallInfo();
        }

        private void CheckAndMoveNecessaryDll(string oldFolderName)
        {
            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwkp.exe", false);
                string[] subKeyNames = key.GetSubKeyNames();
                if ((subKeyNames != null) && (subKeyNames.Length >= 1))
                {
                    string path = key.GetValue("Path", "") as string;
                    path = path.Substring(0, path.LastIndexOf(@"\"));
                    if (Directory.Exists(path + @"\" + oldFolderName))
                    {
                        FileInfo[] files = new DirectoryInfo(path).GetFiles();
                        List<string> necessaryDllNames = this.NecessaryDllNames;
                        foreach (FileInfo info2 in files)
                        {
                            using (List<string>.Enumerator enumerator = this.NecessaryDllNames.GetEnumerator())
                            {
                                string current;
                                while (enumerator.MoveNext())
                                {
                                    current = enumerator.Current;
                                    if (info2.Name == current)
                                    {
                                        goto Label_00C3;
                                    }
                                }
                                continue;
                            Label_00C3:
                                necessaryDllNames.Remove(current);
                            }
                        }
                        foreach (string str3 in necessaryDllNames)
                        {
                            if (File.Exists(path + @"\" + oldFolderName + @"\bin\" + str3))
                            {
                                new FileInfo(path + @"\" + oldFolderName + @"\bin\" + str3).CopyTo(path + @"\" + str3, true);
                            }
                        }
                        key.Close();
                    }
                }
                else
                {
                    key.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        private void ExcuteZJDllBat(string path)
        {
            if (((Directory.Exists(path) && Directory.Exists(@"C:\Windows\Microsoft.NET\Framework\v4.0.30319")) && (File.Exists(@"C:\Windows\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe") && File.Exists(path + @"\bin\xixixi.dll"))) && File.Exists(path + @"\bin\TaxCardX.dll"))
            {
                try
                {
                    Process process = new Process {
                        StartInfo = { FileName = "cmd.exe", UseShellExecute = false, RedirectStandardInput = true, RedirectStandardOutput = true, RedirectStandardError = true, CreateNoWindow = true }
                    };
                    process.Start();
                    string str = "regsvr32 /s \"" + path + "\\bin\\TaxCardX.dll \"";
                    process.StandardInput.WriteLine(str);
                    process.StandardInput.AutoFlush = true;
                    str = "C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\RegAsm.exe \"" + path + "\\bin\\xixixi.dll\" /codebase ";
                    process.StandardInput.WriteLine(str + "&exit");
                    process.StandardInput.AutoFlush = true;
                    process.WaitForExit();
                    process.Close();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("ExcuteZJDllBat异常：" + exception.ToString());
                }
            }
        }

        [DllImport("ss1178.dll")]
        public static extern void FileCryptEx(int i, string path, string code);
        [DllImport("ReadAreaCode.dll", CallingConvention=CallingConvention.Cdecl)]
        private static extern int GetAreaCode(byte[] areacode, byte[] path);
        [DllImport("ReadAreaCode.dll", CallingConvention=CallingConvention.Cdecl)]
        private static extern int GetKpNo(byte[] machineno, byte[] path);
        [DllImport("ReadAreaCode.dll", CallingConvention=CallingConvention.Cdecl)]
        private static extern int GetOldTaxCode(byte[] oldtaxcode, byte[] path);
        [DllImport("ReadAreaCode.dll", CallingConvention=CallingConvention.Cdecl)]
        private static extern int GetRealTaxCode(byte[] taxcode, byte[] path);
        public void HandleAreaCode()
        {
            try
            {
                string str5;
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwkp.exe", true);
                string[] subKeyNames = key.GetSubKeyNames();
                if ((subKeyNames == null) || (subKeyNames.Length < 1))
                {
                    goto Label_034E;
                }
                string path = "";
                string dbpath = "";
                string str3 = key.GetValue("Path", "") as string;
                str3 = str3.Substring(0, str3.LastIndexOf(@"\"));
                dbpath = str3;
                path = str3 + @"\JSDiskDLL.dll";
                if (File.Exists(path))
                {
                    goto Label_00E9;
                }
                string fileName = "";
                for (int i = 0; i < subKeyNames.Length; i++)
                {
                    str5 = Path.Combine(key.OpenSubKey(subKeyNames[i]).GetValue("Path") as string, @"Bin\JSDiskDLL.dll");
                    if (File.Exists(str5))
                    {
                        goto Label_00C5;
                    }
                }
                goto Label_00C9;
            Label_00C5:
                fileName = str5;
            Label_00C9:
                if (fileName != "")
                {
                    new FileInfo(fileName).CopyTo(path);
                }
            Label_00E9:
                if ((path == "") || !File.Exists(path))
                {
                    goto Label_0346;
                }
                int realTaxCode = -1;
                int kpNo = -1;
                int areaCode = -1;
                string name = "";
                byte[] taxcode = new byte[0x19];
                realTaxCode = GetRealTaxCode(taxcode, Encoding.GetEncoding("GB18030").GetBytes(path));
                string oldTaxcode = Encoding.GetEncoding("GB18030").GetString(taxcode).Trim(new char[1]);
                if (((oldTaxcode == null) || (oldTaxcode == "")) || (realTaxCode != 0))
                {
                    goto Label_033E;
                }
                byte[] machineno = new byte[5];
                kpNo = GetKpNo(machineno, Encoding.GetEncoding("GB18030").GetBytes(path));
                string str8 = Encoding.GetEncoding("GB18030").GetString(machineno).Trim(new char[1]);
                if (((str8 == null) || (str8 == "")) || (kpNo != 0))
                {
                    return;
                }
                byte[] areacode = new byte[0x19];
                areaCode = GetAreaCode(areacode, Encoding.GetEncoding("GB18030").GetBytes(path));
                string str9 = Encoding.GetEncoding("GB18030").GetString(areacode).Trim(new char[1]);
                if (((str9 == null) || (str9 == "")) || (areaCode != 0))
                {
                    goto Label_0334;
                }
                foreach (string str10 in subKeyNames)
                {
                    if (str10 == (oldTaxcode + "." + str8))
                    {
                        name = str10;
                        break;
                    }
                }
                goto Label_0276;
            Label_0276:
                if (name == "")
                {
                    key.Close();
                }
                else
                {
                    RegistryKey key3 = key.OpenSubKey(name, true);
                    string str11 = key3.GetValue("orgcode", "") as string;
                    if (str11 != str9)
                    {
                        key3.SetValue("orgcode", str9);
                        key3.Close();
                        key.Close();
                        string str12 = dbpath;
                        dbpath = str12 + @"\" + oldTaxcode + "." + str8 + @"\bin\cc3268.dll";
                        this.UpdateBSZT(oldTaxcode, dbpath, oldTaxcode);
                    }
                    else
                    {
                        key3.Close();
                        key.Close();
                    }
                }
                return;
            Label_0334:
                key.Close();
                return;
            Label_033E:
                key.Close();
                return;
            Label_0346:
                key.Close();
                return;
            Label_034E:
                key.Close();
            }
            catch (Exception)
            {
            }
        }

        public void HandleSZHY()
        {
            try
            {
                string str6;
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwkp.exe", true);
                string[] subKeyNames = key.GetSubKeyNames();
                if ((subKeyNames == null) || (subKeyNames.Length < 1))
                {
                    goto Label_0725;
                }
                string path = "";
                string str2 = key.GetValue("Path", "") as string;
                string dbpath = "";
                str2 = str2.Substring(0, str2.LastIndexOf(@"\"));
                dbpath = str2;
                path = str2 + @"\JSDiskDLL.dll";
                string str4 = str2 + @"\temp";
                if (!Directory.Exists(str4))
                {
                    Directory.CreateDirectory(str4);
                }
                DirectoryInfo info = new DirectoryInfo(str4) {
                    Attributes = FileAttributes.Hidden
                };
                FileInfo[] files = info.GetFiles();
                try
                {
                    foreach (FileInfo info2 in files)
                    {
                        info2.Delete();
                    }
                }
                catch (Exception)
                {
                }
                if (File.Exists(path))
                {
                    goto Label_0147;
                }
                string fileName = "";
                for (int i = 0; i < subKeyNames.Length; i++)
                {
                    str6 = Path.Combine(key.OpenSubKey(subKeyNames[i]).GetValue("Path") as string, @"Bin\JSDiskDLL.dll");
                    if (File.Exists(str6))
                    {
                        goto Label_0123;
                    }
                }
                goto Label_0127;
            Label_0123:
                fileName = str6;
            Label_0127:
                if (fileName != "")
                {
                    new FileInfo(fileName).CopyTo(path);
                }
            Label_0147:
                if ((path == "") || !File.Exists(path))
                {
                    goto Label_071D;
                }
                byte[] oldtaxcode = new byte[0x19];
                int oldTaxCode = GetOldTaxCode(oldtaxcode, Encoding.GetEncoding("GB18030").GetBytes(path));
                string oldTaxcode = Encoding.GetEncoding("GB18030").GetString(oldtaxcode).Trim(new char[1]);
                if (((oldTaxCode != 0) || (oldTaxcode == null)) || (oldTaxcode.Trim() == ""))
                {
                    goto Label_0715;
                }
                byte[] taxcode = new byte[0x19];
                int realTaxCode = GetRealTaxCode(taxcode, Encoding.GetEncoding("GB18030").GetBytes(path));
                string newTaxcode = Encoding.GetEncoding("GB18030").GetString(taxcode).Trim(new char[1]);
                if (((realTaxCode != 0) || (newTaxcode == null)) || (newTaxcode.Trim() == ""))
                {
                    goto Label_070D;
                }
                byte[] machineno = new byte[5];
                int kpNo = GetKpNo(machineno, Encoding.GetEncoding("GB18030").GetBytes(path));
                string str9 = Encoding.GetEncoding("GB18030").GetString(machineno).Trim(new char[1]);
                if (((kpNo != 0) || (str9 == null)) || (str9.Trim() == ""))
                {
                    goto Label_0705;
                }
                byte[] areacode = new byte[5];
                int areaCode = GetAreaCode(areacode, Encoding.GetEncoding("GB18030").GetBytes(path));
                string str10 = Encoding.GetEncoding("GB18030").GetString(areacode).Trim(new char[1]);
                if (((areaCode != 0) || (str10 == null)) || (str10.Trim() == ""))
                {
                    goto Label_06FD;
                }
                string oldFolderName = "";
                for (int j = 0; j < subKeyNames.Length; j++)
                {
                    if (subKeyNames[j].Equals(newTaxcode + "." + str9))
                    {
                        goto Label_06F5;
                    }
                    if (subKeyNames[j].Equals(oldTaxcode + "." + str9))
                    {
                        oldFolderName = subKeyNames[j];
                    }
                }
                if (oldFolderName == "")
                {
                    key.Close();
                }
                else
                {
                    this.CheckAndMoveNecessaryDll(oldFolderName);
                    RegistryKey key3 = key.OpenSubKey(oldFolderName);
                    string str12 = Path.Combine(key3.GetValue("Path") as string, @"Bin\AddedRealTax.dll");
                    if (File.Exists(str12))
                    {
                        new FileInfo(str12).CopyTo(str4 + @"\AddedRealTax.dll");
                        string code = oldTaxcode;
                        if (oldTaxcode.Length < 15)
                        {
                            for (int k = 0; k < (15 - oldTaxcode.Length); k++)
                            {
                                code = code + "0";
                            }
                        }
                        string str14 = newTaxcode;
                        if (newTaxcode.Length < 15)
                        {
                            for (int m = 0; m < (15 - newTaxcode.Length); m++)
                            {
                                str14 = str14 + "0";
                            }
                        }
                        FileCryptEx(1, str4 + @"\AddedRealTax.dll", code);
                        FileCryptEx(2, str4 + @"\AddedRealTax.dll", str14);
                        new FileInfo(str4 + @"\AddedRealTax.dll").CopyTo(str12, true);
                        string str15 = key3.GetValue("Path", "") as string;
                        string destDirName = str15.Replace(oldFolderName, newTaxcode + "." + str9);
                        new DirectoryInfo(str15).MoveTo(destDirName);
                        string str17 = key3.GetValue("", "") as string;
                        str17 = str17.Replace(oldFolderName, newTaxcode + "." + str9);
                        string str18 = newTaxcode;
                        string str19 = key3.GetValue("DataBaseVersion", "") as string;
                        string str20 = str9;
                        key3.GetValue("orgcode", "");
                        string str21 = key3.GetValue("Version", "") as string;
                        key3.Close();
                        key3 = null;
                        key.DeleteSubKey(oldFolderName);
                        key3 = key.CreateSubKey(newTaxcode + "." + str9, RegistryKeyPermissionCheck.ReadWriteSubTree);
                        key.SetValue("", str17);
                        key.SetValue("code", str18);
                        key.SetValue("machine", str20);
                        key.SetValue("DataBaseVersion", str19);
                        key.SetValue("orgcode", str10);
                        key.SetValue("Path", destDirName);
                        key.SetValue("Version", str21);
                        key3.SetValue("", str17);
                        key3.SetValue("code", str18);
                        key3.SetValue("machine", str20);
                        key3.SetValue("DataBaseVersion", str19);
                        key3.SetValue("orgcode", str10);
                        key3.SetValue("Path", destDirName);
                        key3.SetValue("Version", str21);
                        string str22 = dbpath;
                        dbpath = str22 + @"\" + newTaxcode + "." + str9 + @"\bin\cc3268.dll";
                        this.UpdateBSZT(oldTaxcode, dbpath, newTaxcode);
                        info.Delete(true);
                        this.SaveNewOldTaxCode(newTaxcode, oldTaxcode);
                        this.ChageUninstallInfo(newTaxcode + "." + str9, oldFolderName, Path.Combine(key3.GetValue("Path") as string, "Uninstall.dat"));
                        key3.Close();
                        key.Close();
                        this.ExcuteZJDllBat(str2 + @"\" + newTaxcode + "." + str9);
                    }
                }
                return;
            Label_06F5:
                key.Close();
                return;
            Label_06FD:
                key.Close();
                return;
            Label_0705:
                key.Close();
                return;
            Label_070D:
                key.Close();
                return;
            Label_0715:
                key.Close();
                return;
            Label_071D:
                key.Close();
                return;
            Label_0725:
                key.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show("【税控发票开票软件(金税盘版)】一体化变更异常：" + exception.Message, "异常");
            }
        }

        private void SaveNewOldTaxCode(string newTaxcode, string oldTaxCode)
        {
            IniProperty.InitProperty();
            IniProperty.SetProperty(oldTaxCode, newTaxcode);
            IniProperty.SaveIniFile();
        }

        private void UpdateBSZT(string oldTaxcode, string dbpath, string newTaxcode)
        {
            if ((File.Exists(dbpath) && (oldTaxcode != null)) && (oldTaxcode != ""))
            {
                try
                {
                    string connectionString = string.Format("Data Source={0};Pooling=true;Password=LoveR1314;", dbpath);
                    string safeSql = "UPDATE XXFP SET BSZT='1',BSRZ='已做过一体化变更，状态置为已报送。' WHERE XFSH='" + oldTaxcode + "'";
                    string str3 = "UPDATE XTSWXX SET QYBH='" + newTaxcode + "' WHERE QYBH='" + oldTaxcode + "'";
                    DBHelper.ConnValue(connectionString);
                    DBHelper.ExecuteCommand(safeSql);
                    DBHelper.ExecuteCommand(str3);
                    DBHelper.CloseConn();
                }
                catch (Exception)
                {
                }
            }
        }
    }
}

