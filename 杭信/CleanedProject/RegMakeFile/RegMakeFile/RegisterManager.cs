namespace RegMakeFile
{
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    public class RegisterManager
    {
        public static string errCode = "0000";
        public static string FilterTaxCode = null;

        private static bool CheckRegFile(RegFileInfo regFile, bool complexChecked)
        {
            Exception exception;
            try
            {
                if (!((regFile != null) && regFile.CheckedOk))
                {
                    return false;
                }
                if (regFile.VerFlag != "KP")
                {
                    FileStream stream = new FileStream(Path.Combine(Path.Combine(Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwkp.exe").GetValue("Path").ToString(), @"Bin\"), "verflag.dat"), FileMode.OpenOrCreate);
                    try
                    {
                        string xmlKey = "<RSAKeyValue><Modulus>ghCLpZiwHiZIDYve7yGZusVydX406Qd4JqIYFsl/wUK/y1xjEsT3zQvCfpwASRwpHg0bi8XZ4EILPJt4NXVXftRlD7ZlG17sAIDp3OUSSGxI5hkXB7BJPrw2wbqs/6hfZr6vmYnwpDb8IAZmt8xlJucIUWjEVuu4NnOx1/iiqlM=</Modulus><Exponent>AQAB</Exponent><P>tvY6Rtiwahg6keaITiLw42GCjXLK3BDjtHfa2uMSndK5qBhQQ+7bMM11H/7spU+25SgXBdSHVWy/y8KFvT5ISw==</P><Q>tfx+BCB6dw/4ShgTqbxX3X8xoRapWr4XMvVrdLnc/txHpHhn9pNjtM2Xb3GVlltzCEQkzBcXnk0SeBYjIR3xGQ==</Q><DP>T6PIZDRIPjZDsGSHqnNdJay5NjbkhHw5kcGmGydCYD5sn/XNYnSjJpAYTpAZlC+prgAQXXJQYmfO6LPIoUJuFw==</DP><DQ>n891ngwjXxDgGbjg84oYosLCg1KSL8SEPNS1o1BgWFJ6e1zc9vRhd3GfTVcyZFI0RwsIQUz6CaJm2JugB8HyaQ==</DQ><InverseQ>oxe42fx2yLATcCG4lbQ5f8Qo8c8ACkT4NxqYl3GXdrojBorBzbvht2+KHq2bJorWtcPNnsmumhV6BIV7zCW0kw==</InverseQ><D>E92nFsH9lH1QYBFPGcNOEcL6uotuVXF4np3/g+t/AevKE6umzkUbfEwhhukY+hG9DgP+gxjTMHel87njYHbtyA+23TdhIzhyYcSg0ifotDhgD8+9lBrn29hyddFigLDoXnZR1SQmvn7xjuGKtZ/HaKZPetSxgVf1mSPdzl37CGE=</D></RSAKeyValue>";
                        StreamReader reader = new StreamReader(stream);
                        string str5 = reader.ReadToEnd();
                        bool flag = false;
                        string input = string.Empty;
                        if (!string.IsNullOrEmpty(str5))
                        {
                            byte[] inputByt = Convert.FromBase64String(str5);
                            byte[] bytes = RSA_Crypt.Decrypt(xmlKey, inputByt);
                            if (bytes != null)
                            {
                                input = Encoding.GetEncoding("GBK").GetString(bytes);
                                foreach (string str7 in Regex.Split(input, ";"))
                                {
                                    if (str7 == regFile.VerFlag)
                                    {
                                        goto Label_0111;
                                    }
                                }
                            }
                        }
                        goto Label_0121;
                    Label_0111:
                        reader.Close();
                        stream.Close();
                        flag = true;
                    Label_0121:
                        if (!flag)
                        {
                            stream.SetLength(0L);
                            StreamWriter writer = new StreamWriter(stream);
                            string s = input + regFile.VerFlag + ";";
                            string str9 = Convert.ToBase64String(RSA_Crypt.Encrypt(xmlKey, Encoding.GetEncoding("GBK").GetBytes(s)));
                            writer.Write(str9);
                            writer.Flush();
                            writer.Close();
                        }
                        stream.Close();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        string message = exception.Message;
                        stream.Close();
                    }
                }
                return true;
            }
            catch (Exception exception2)
            {
                exception = exception2;
                return false;
            }
        }

        private static RegFileInfo ParseRegFile(string regFile)
        {
            string setupTaxCode = SetupTaxCode;
            if (((SetupTaxCode.Length > 15) && (SetupOrgCode != null)) && (SetupOrgCode.Length > 0))
            {
                setupTaxCode = setupTaxCode + SetupOrgCode;
            }
            qwe qw = new qwe();
            if (Xihaa.abc(regFile, setupTaxCode, SetupMachine, DateTime.Now.ToString("yyyyMMdd"), ref qw) == 0)
            {
                return new RegFileInfo(regFile, qw, new FileInfo(regFile).LastWriteTime) { ErrCode = "0000" };
            }
            return new RegFileInfo(regFile, qw, new FileInfo(regFile).LastWriteTime) { ErrCode = "910101" };
        }

        private static List<RegFileInfo> ParseRegFiles(List<string> regFiles)
        {
            List<RegFileInfo> list = new List<RegFileInfo>();
            foreach (string str in regFiles)
            {
                list.Add(ParseRegFile(str));
            }
            return list;
        }

        private static List<string> SearchRegFile()
        {
            return SearchRegFile(null, null, null);
        }

        private static List<string> SearchRegFile(string taxCode)
        {
            return SearchRegFile(null, null, taxCode);
        }

        private static List<string> SearchRegFile(string filePath, List<string> fileExt, string partialFileName)
        {
            List<string> list = new List<string>();
            string pattern = @"^(([a-zA-Z]:\\)|(\\{2}\w+)\$?)((([^/\\\?\*])(\\?))*)$";
            if (!(((filePath != null) && (filePath.Length != 0)) && Regex.IsMatch(filePath, pattern)))
            {
                filePath = Path.Combine(Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwkp.exe").GetValue("Path").ToString(), @"Bin\");
            }
            if (fileExt == null)
            {
                fileExt = new List<string>();
                fileExt.Add(".RFHX");
            }
            DirectoryInfo info = new DirectoryInfo(filePath);
            foreach (FileInfo info2 in info.GetFiles())
            {
                if (fileExt.Contains(info2.Extension.ToUpper()))
                {
                    if ((partialFileName != null) && (partialFileName.Length > 0))
                    {
                        if (info2.Name.Contains(partialFileName))
                        {
                            list.Add(info2.FullName);
                        }
                    }
                    else
                    {
                        list.Add(info2.FullName);
                    }
                }
            }
            return list;
        }

        public static RegFileSetupResult SetupRegFile()
        {
            try
            {
                int num;
                string path = Path.Combine(Path.Combine(Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwkp.exe").GetValue("Path").ToString(), @"Bin\"), "verflag.dat");
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                List<RegFileInfo> list = new List<RegFileInfo>();
                List<RegFileInfo> list2 = new List<RegFileInfo>();
                List<RegFileInfo> list3 = new List<RegFileInfo>();
                List<RegFileInfo> list4 = new List<RegFileInfo>();
                List<string> regFiles = SearchRegFile();
                if (regFiles == null)
                {
                    return null;
                }
                List<RegFileInfo> list6 = ParseRegFiles(regFiles);
                for (num = 0; num < list6.Count; num++)
                {
                    if (!list4.Contains(list6[num]) && !list3.Contains(list6[num]))
                    {
                        if (!list6[num].CheckedOk)
                        {
                            list3.Add(list6[num]);
                        }
                        else
                        {
                            int num2 = num;
                            for (int i = num + 1; i < list6.Count; i++)
                            {
                                if (list6[num2].VerFlag == list6[i].VerFlag)
                                {
                                    if (list6[num2].FileModifyDate < list6[i].FileModifyDate)
                                    {
                                        list6[num2].ErrCode = "910102";
                                        list3.Add(list6[num2]);
                                        num2 = i;
                                    }
                                    else
                                    {
                                        list6[i].ErrCode = "910102";
                                        list3.Add(list6[i]);
                                    }
                                }
                            }
                            list4.Add(list6[num2]);
                        }
                    }
                }
                for (num = 0; num < list4.Count; num++)
                {
                    RegFileInfo regFile = list4[num];
                    if (CheckRegFile(regFile, true))
                    {
                        if (!regFile.VerFlag.Equals("KP"))
                        {
                            list.Add(regFile);
                        }
                    }
                    else if (regFile.ErrCode == "910105")
                    {
                        list2.Add(regFile);
                    }
                    else
                    {
                        list3.Add(regFile);
                    }
                }
                return new RegFileSetupResult { NormalRegFiles = list, OutOfDateRegFiles = list2, InvalidRegFiles = list3 };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static ushort SetupMachine
        {
            get
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwkp.exe");
                string s = (key.GetValue("machine") == null) ? "0" : key.GetValue("machine").ToString();
                return ushort.Parse(s);
            }
        }

        public static string SetupOrgCode
        {
            get
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwkp.exe");
                return ((key.GetValue("orgcode") == null) ? string.Empty : key.GetValue("orgcode").ToString());
            }
        }

        public static string SetupTaxCode
        {
            get
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\fwkp.exe");
                return ((key.GetValue("code") == null) ? string.Empty : key.GetValue("code").ToString());
            }
        }
    }
}

