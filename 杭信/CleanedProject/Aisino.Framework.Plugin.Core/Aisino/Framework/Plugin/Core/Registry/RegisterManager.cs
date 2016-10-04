namespace Aisino.Framework.Plugin.Core.Registry
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Crypto;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;

    public class RegisterManager
    {
        private static Dictionary<string, RegFileInfo> dictionary_0;
        public static string errCode;
        public static string FilterTaxCode;
        private static ILog ilog_0;
        private static TaxCard taxCard_0;

        static RegisterManager()
        {
            
            FilterTaxCode = null;
            taxCard_0 = TaxCardFactory.CreateTaxCard();
            ilog_0 = LogUtil.GetLogger<RegisterManager>();
            dictionary_0 = new Dictionary<string, RegFileInfo>();
            errCode = "0000";
        }

        public RegisterManager()
        {
            
        }

        public static bool CheckRegFile(string string_0)
        {
            return CheckRegFile(string_0, null);
        }

        public static bool CheckRegFile(string string_0, TaxCard taxCard_1)
        {
            return smethod_5(GetRegFileInfo(string_0, taxCard_1), taxCard_1, true);
        }

        public static byte[] CheckRegFile(string string_0, byte[] byte_0, TaxCard taxCard_1)
        {
            return smethod_11(GetRegFileInfo(string_0, taxCard_1), byte_0, taxCard_1);
        }

        public static RegFileInfo GetRegFileInfo(string string_0)
        {
            return GetRegFileInfo(string_0, null);
        }

        public static RegFileInfo GetRegFileInfo(string string_0, TaxCard taxCard_1)
        {
            try
            {
                if (string_0.Length == 2)
                {
                    string key = string_0.Substring(0, 2);
                    if (dictionary_0.ContainsKey(key))
                    {
                        ilog_0.Debug("[GetRegFileInfo] 从缓存中返回：" + key);
                        return dictionary_0[key];
                    }
                }
                if (taxCard_1 == null)
                {
                    taxCard_1 = taxCard_0;
                }
                else
                {
                    taxCard_0 = taxCard_1;
                }
                if ((string_0 != null) && (string_0.Length >= 2))
                {
                    RegFileInfo info = null;
                    if (string_0.Length == 2)
                    {
                        info = smethod_6(string_0, (Enum15) 0);
                    }
                    else if (string_0.Length == 4)
                    {
                        RegFileInfo info3 = smethod_6(string_0.Substring(0, 2), (Enum15) 0);
                        info = smethod_6(string_0, (Enum15) 1);
                        if (info3.FileName != info.FileName)
                        {
                            info = null;
                        }
                    }
                    else
                    {
                        FileInfo info4 = new FileInfo(string_0);
                        if (!info4.Exists)
                        {
                            return null;
                        }
                        info = smethod_3(string_0, taxCard_1);
                    }
                    if (smethod_5(info, taxCard_1, false))
                    {
                        if (string_0.Length == 2)
                        {
                            string str2 = string_0.Substring(0, 2);
                            dictionary_0.Add(str2, info);
                            ilog_0.Debug("[GetRegFileInfo] 加入缓存：" + str2);
                        }
                        return info;
                    }
                    return null;
                }
                return null;
            }
            catch (Exception exception)
            {
                ilog_0.Error(exception.Message);
                return null;
            }
        }

        public static bool MakeExportedRegFile(string string_0, TaxCard taxCard_1)
        {
            try
            {
                if (taxCard_1.TaxMode == CTaxCardMode.tcmHave)
                {
                    RegFileInfo regFileInfo = GetRegFileInfo(string_0, taxCard_1);
                    if ((regFileInfo != null) && regFileInfo.CheckedOk)
                    {
                        byte[] buffer = new byte[0x10];
                        for (int i = 0; i < buffer.Length; i++)
                        {
                            buffer[i] = (byte) i;
                        }
                        return (Xihaa.MakeTransFile(regFileInfo.FileName, buffer) == 0);
                    }
                }
                return false;
            }
            catch (Exception exception)
            {
                ilog_0.Error(exception.Message);
                return false;
            }
        }

        public static bool PreMakeInvoice()
        {
            try
            {
                if (taxCard_0.TaxMode != CTaxCardMode.tcmHave)
                {
                    return true;
                }
                byte[] buffer = new byte[0x10];
                byte[] buffer2 = null;
                ilog_0.Debug("[注册] 调用9Bit接口开始");
                taxCard_0.Get9BitHashTaxCode(out buffer2, 0);
                ilog_0.Debug("[注册] 调用9Bit接口结束");
                if (buffer2 == null)
                {
                    ilog_0.Error("调用接口返回的Hash税号为空");
                    return false;
                }
                Xihaa.GenProKey(buffer, buffer2);
                if (buffer.Length < 0x10)
                {
                    ilog_0.Error("调用接口返回的过程密钥为空");
                    return false;
                }
                ilog_0.Debug("[注册] 调用Pre接口开始");
                string str = taxCard_0.PreMakeInvoice(buffer, 0);
                ilog_0.DebugFormat("[注册] 调用Pre接口结束，返回值={0}", str);
                if (ToolUtil.GetReturnErrCode(str) == 0)
                {
                    return true;
                }
                ilog_0.Error("调用开票初始化接口失败，错误号=" + errCode);
                return false;
            }
            catch (Exception exception)
            {
                ilog_0.Error(exception.Message);
                return false;
            }
        }

        public static RegFileSetupResult SetupRegFile()
        {
            return SetupRegFile(taxCard_0);
        }

        public static RegFileSetupResult SetupRegFile(TaxCard taxCard_1)
        {
            try
            {
                if (taxCard_1 == null)
                {
                    taxCard_1 = taxCard_0;
                }
                else
                {
                    taxCard_0 = taxCard_1;
                }
                List<RegFileInfo> list6 = new List<RegFileInfo>();
                List<RegFileInfo> list = new List<RegFileInfo>();
                List<RegFileInfo> list4 = new List<RegFileInfo>();
                List<RegFileInfo> list2 = new List<RegFileInfo>();
                List<string> regFiles = smethod_0();
                if (regFiles == null)
                {
                    return null;
                }
                List<RegFileInfo> list3 = smethod_4(regFiles, taxCard_1);
                for (int i = 0; i < list3.Count; i++)
                {
                    if (!list2.Contains(list3[i]) && !list4.Contains(list3[i]))
                    {
                        if (!list3[i].CheckedOk)
                        {
                            list4.Add(list3[i]);
                        }
                        else
                        {
                            int num2 = i;
                            for (int k = i + 1; k < list3.Count; k++)
                            {
                                if (list3[num2].VerFlag == list3[k].VerFlag)
                                {
                                    if (list3[num2].FileModifyDate < list3[k].FileModifyDate)
                                    {
                                        list3[num2].ErrCode = "910102";
                                        list4.Add(list3[num2]);
                                        num2 = k;
                                    }
                                    else
                                    {
                                        list3[k].ErrCode = "910102";
                                        list4.Add(list3[k]);
                                    }
                                }
                            }
                            list2.Add(list3[num2]);
                        }
                    }
                }
                for (int j = 0; j < list2.Count; j++)
                {
                    RegFileInfo info = list2[j];
                    if (smethod_5(info, taxCard_1, true))
                    {
                        if (!info.VerFlag.Equals("KP"))
                        {
                            list6.Add(info);
                        }
                    }
                    else if (info.ErrCode == "910105")
                    {
                        list.Add(info);
                    }
                    else
                    {
                        list4.Add(info);
                    }
                }
                return new RegFileSetupResult { NormalRegFiles = list6, OutOfDateRegFiles = list, InvalidRegFiles = list4 };
            }
            catch (Exception exception)
            {
                ilog_0.Error(exception.Message);
                return null;
            }
        }

        public static RegFileSetupResult SetupRegFile(List<string> files)
        {
            return SetupRegFile(files, null);
        }

        public static RegFileSetupResult SetupRegFile(List<string> files, TaxCard taxCard_1)
        {
            RegFileSetupResult result = SetupRegFile(taxCard_1);
            RegFileSetupResult result2 = new RegFileSetupResult(result);
            for (int i = result.NormalRegFiles.Count - 1; i >= 0; i--)
            {
                if (!files.Contains(result.NormalRegFiles[i].FileName))
                {
                    result2.NormalRegFiles.RemoveAt(i);
                }
            }
            for (int j = result.OutOfDateRegFiles.Count - 1; j >= 0; j--)
            {
                if (!files.Contains(result.OutOfDateRegFiles[j].FileName))
                {
                    result2.OutOfDateRegFiles.RemoveAt(j);
                }
            }
            for (int k = result.InvalidRegFiles.Count - 1; k >= 0; k--)
            {
                if (!files.Contains(result.InvalidRegFiles[k].FileName))
                {
                    result2.InvalidRegFiles.RemoveAt(k);
                }
            }
            return result2;
        }

        private static List<string> smethod_0()
        {
            return smethod_2(null, null, null);
        }

        private static List<string> smethod_1(string string_0)
        {
            return smethod_2(null, null, string_0);
        }

        private static byte[] smethod_10(string string_0, byte[] byte_0)
        {
            return CheckRegFile(string_0, byte_0, null);
        }

        private static byte[] smethod_11(RegFileInfo regFileInfo_0, byte[] array_0, TaxCard taxCard_1)
        {
            DateTime time;
            errCode = "0000";
            if (taxCard_1 == null)
            {
                taxCard_1 = taxCard_0;
            }
            else
            {
                taxCard_0 = taxCard_1;
            }
            if (array_0.Length != 0x10)
            {
                throw new ArgumentException("验证注册码时，传入的参数不正确");
            }
            byte[] destinationArray = new byte[0x40];
            byte[] bytes = BitConverter.GetBytes(regFileInfo_0.FileContent.SoftwareType);
            Array.Copy(bytes, 0, destinationArray, 0, bytes.Length);
            byte[] sourceArray = Encoding.ASCII.GetBytes(regFileInfo_0.FileContent.SoftwareID);
            Array.Copy(sourceArray, 0, destinationArray, 2, sourceArray.Length);
            byte[] buffer4 = BitConverter.GetBytes(regFileInfo_0.FileContent.SerialNo);
            Array.Copy(buffer4, 0, destinationArray, 8, buffer4.Length);
            byte[] array = BitConverter.GetBytes(Convert.ToInt32(new string(regFileInfo_0.FileContent.StopDate), 0x10));
            Array.Reverse(array);
            Array.Copy(array, 0, destinationArray, 12, array.Length);
            if (regFileInfo_0.FileContent.SoftwareType == 1)
            {
                destinationArray[0x10] = 1;
            }
            else
            {
                destinationArray[0x10] = 0;
            }
            Array.Copy(regFileInfo_0.FileContent.Verify, 0, destinationArray, 0x18, regFileInfo_0.FileContent.Verify.Length);
            Array.Copy(array_0, 0, destinationArray, 40, array_0.Length);
            byte num = 0;
            for (int i = 0; i < 0x3f; i++)
            {
                num = (byte) (num + destinationArray[i]);
            }
            destinationArray[0x3f] = num;
            TextRegHead head = new TextRegHead {
                Tax_Mw_No = new byte[15]
            };
            string compressCode = taxCard_1.CompressCode;
            if ((regFileInfo_0.FileContent.SoftwareType > 0xff) && compressCode.StartsWith("91"))
            {
                compressCode = "50" + compressCode.Substring(2, compressCode.Length - 2);
            }
            for (int j = 0; j < compressCode.Length; j++)
            {
                if (j >= 15)
                {
                    break;
                }
                head.Tax_Mw_No[j] = (byte) compressCode[j];
            }
            head.Date = new byte[4];
            taxCard_1.GetCardClock(out time, 0);
            byte[] buffer6 = BitConverter.GetBytes(Convert.ToInt32(time.ToString("yyyyMMdd"), 0x10));
            Array.Reverse(buffer6);
            Array.Copy(buffer6, head.Date, 4);
            head.MachinNo = regFileInfo_0.FileContent.BranchNo;
            head.buf = new byte[0x18];
            Array.Copy(destinationArray, head.buf, 0x18);
            byte[] buffer7 = new byte[0x10];
            byte[] buffer8 = new byte[0x10];
            Xihaa.GenTextRegKey(buffer7, buffer8, ref head);
            Array.Copy(buffer7, 0, destinationArray, 0x18, 0x10);
            byte[] buffer9 = new byte[0x10];
            for (int k = 0; k < 0x10; k++)
            {
                buffer9[k] = (byte) (array_0[k] ^ regFileInfo_0.FileContent.Transfer[k]);
            }
            byte[] buffer10 = new byte[0x10];
            for (int m = 0; m < 0x10; m++)
            {
                buffer10[m] = (byte) (buffer9[m] ^ buffer8[m]);
            }
            Array.Copy(buffer10, 0, destinationArray, 40, 0x10);
            num = 0;
            for (int n = 0; n < 0x3f; n++)
            {
                num = (byte) (num + destinationArray[n]);
            }
            destinationArray[0x3f] = num;
            byte[] buffer11 = null;
            ilog_0.Debug("[注册] 调用校验接口开始");
            string str2 = taxCard_1.CheckRegCode(destinationArray, out buffer11, 0);
            ilog_0.DebugFormat("[注册] 调用校验接口结束，返回值={0}", str2);
            if (ToolUtil.GetReturnErrCode(str2) == 0)
            {
                for (int num9 = 0; num9 < 0x10; num9++)
                {
                    if ((array_0[num9] ^ regFileInfo_0.FileContent.Transfer[num9]) != buffer11[num9])
                    {
                        ilog_0.Debug("验证注册文件失败，第" + num9.ToString() + "位校验失败");
                    }
                }
                ilog_0.Debug("注册文件验证成功");
                return buffer11;
            }
            ilog_0.Error("注册文件校验失败：接口错误号=" + taxCard_1.ErrCode);
            errCode = taxCard_1.ErrCode;
            return null;
        }

        private static List<string> smethod_2(string string_0, List<string> fileExt, string string_1)
        {
            List<string> list = new List<string>();
            string pattern = @"^(([a-zA-Z]:\\)|(\\{2}\w+)\$?)((([^/\\\?\*])(\\?))*)$";
            if (((string_0 == null) || (string_0.Length == 0)) || !Regex.IsMatch(string_0, pattern))
            {
                string_0 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
            if (fileExt == null)
            {
                fileExt = new List<string>();
                if (taxCard_0.TaxMode == CTaxCardMode.tcmHave)
                {
                    fileExt.Add(".RFHX");
                }
                else if (taxCard_0.TaxMode == CTaxCardMode.tcmNone)
                {
                    fileExt.Add(".RFHX");
                    fileExt.Add(".TFHX");
                }
            }
            DirectoryInfo info = new DirectoryInfo(string_0);
            foreach (FileInfo info2 in info.GetFiles())
            {
                if (fileExt.Contains(info2.Extension.ToUpper()))
                {
                    if ((string_1 != null) && (string_1.Length > 0))
                    {
                        if (info2.Name.Contains(string_1))
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

        private static RegFileInfo smethod_3(string string_0, TaxCard taxCard_1)
        {
            DateTime time;
            if (taxCard_1 == null)
            {
                taxCard_1 = taxCard_0;
            }
            else
            {
                taxCard_0 = taxCard_1;
            }
            string taxCode = taxCard_1.TaxCode;
            if (((taxCard_1.TaxCode.Length > 15) && (SetupOrgCode != null)) && (SetupOrgCode.Length > 0))
            {
                taxCode = taxCode + SetupOrgCode;
            }
            string b = taxCard_1.TaxCode;
            if ((taxCard_1.CorpCode != null) && (taxCard_1.CorpCode.Length >= 15))
            {
                b = taxCard_1.TaxCode.Substring(0, 6) + taxCard_1.CorpCode.Substring(6, 9);
            }
            qwe qwe = new qwe();
            taxCard_1.GetCardClock(out time, 0);
            if (Xihaa.abc(string_0, taxCode, (ushort) taxCard_1.Machine, time.ToString("yyyyMMdd"), ref qwe) == 0)
            {
                RegFileInfo info = new RegFileInfo(string_0, qwe, new FileInfo(string_0).LastWriteTime);
                string a = new string(info.FileContent.TaxCode);
                string str2 = a;
                if (a.StartsWith("50"))
                {
                    a = "91" + a.Substring(2);
                }
                if (((!string.Equals(str2, taxCard_1.CompressCode) && !string.Equals(a, taxCard_1.CompressCode)) && (!string.Equals(a, b) && !string.Equals(a, taxCard_1.CorpCode))) && !string.Equals(a, taxCard_1.TaxCode))
                {
                    ilog_0.DebugFormat("文件原始税号={0}，压缩税号={1}；变换后税号1={2}，变换后税号2={3}；CorpCode税号={4}，TaxCode税号={5}；", new object[] { str2, taxCard_1.CompressCode, a, b, taxCard_1.CorpCode, taxCard_1.TaxCode });
                    info.ErrCode = "910103";
                    return info;
                }
                if (taxCard_1.Machine != info.FileContent.BranchNo)
                {
                    ilog_0.DebugFormat("开票机号={0}，文件开票机号={1}", taxCard_1.Machine, info.FileContent.BranchNo);
                    info.ErrCode = "910104";
                    return info;
                }
                info.ErrCode = "0000";
                return info;
            }
            return new RegFileInfo(string_0, qwe, new FileInfo(string_0).LastWriteTime) { ErrCode = "910101" };
        }

        private static List<RegFileInfo> smethod_4(List<string> regFiles, TaxCard taxCard_1)
        {
            if (taxCard_1 == null)
            {
                taxCard_1 = taxCard_0;
            }
            else
            {
                taxCard_0 = taxCard_1;
            }
            List<RegFileInfo> list = new List<RegFileInfo>();
            foreach (string str in regFiles)
            {
                list.Add(smethod_3(str, taxCard_1));
            }
            return list;
        }

        private static bool smethod_5(RegFileInfo regFileInfo_0, TaxCard taxCard_1, bool bool_0)
        {
            bool flag;
            try
            {
                if ((regFileInfo_0 == null) || !regFileInfo_0.CheckedOk)
                {
                    goto Label_02BA;
                }
                if (taxCard_1 == null)
                {
                    taxCard_1 = taxCard_0;
                }
                else
                {
                    taxCard_0 = taxCard_1;
                }
                if (regFileInfo_0.VerFlag != "KP")
                {
                    DateTime time;
                    string strA = new string(regFileInfo_0.FileContent.StopDate);
                    taxCard_1.GetCardClock(out time, 0);
                    if (string.Compare(strA, time.ToString("yyyyMMdd")) < 0)
                    {
                        regFileInfo_0.ErrCode = "910105";
                        return false;
                    }
                    if ((taxCard_1.TaxMode != CTaxCardMode.tcmHave) || !bool_0)
                    {
                        return true;
                    }
                    if (regFileInfo_0.FileContent.SoftwareType == 1)
                    {
                        byte[] buffer = smethod_9(regFileInfo_0, taxCard_1);
                        if (((buffer != null) && (buffer.Length > 3)) && (buffer[2] == 1))
                        {
                            return true;
                        }
                    }
                    byte[] buffer2 = new byte[0x10];
                    for (int i = 0; i < buffer2.Length; i++)
                    {
                        buffer2[i] = (byte) (i ^ regFileInfo_0.FileContent.Transfer[i]);
                    }
                    byte[] buffer3 = smethod_11(regFileInfo_0, buffer2, taxCard_1);
                    if ((buffer3 == null) || (buffer3.Length < 0x10))
                    {
                        goto Label_02A7;
                    }
                    for (int j = 0; j < 0x10; j++)
                    {
                        if (buffer3[j] != j)
                        {
                            goto Label_0298;
                        }
                    }
                    FileStream stream = new FileStream(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "verflag.dat"), FileMode.OpenOrCreate);
                    try
                    {
                        string str4 = "<RSAKeyValue><Modulus>ghCLpZiwHiZIDYve7yGZusVydX406Qd4JqIYFsl/wUK/y1xjEsT3zQvCfpwASRwpHg0bi8XZ4EILPJt4NXVXftRlD7ZlG17sAIDp3OUSSGxI5hkXB7BJPrw2wbqs/6hfZr6vmYnwpDb8IAZmt8xlJucIUWjEVuu4NnOx1/iiqlM=</Modulus><Exponent>AQAB</Exponent><P>tvY6Rtiwahg6keaITiLw42GCjXLK3BDjtHfa2uMSndK5qBhQQ+7bMM11H/7spU+25SgXBdSHVWy/y8KFvT5ISw==</P><Q>tfx+BCB6dw/4ShgTqbxX3X8xoRapWr4XMvVrdLnc/txHpHhn9pNjtM2Xb3GVlltzCEQkzBcXnk0SeBYjIR3xGQ==</Q><DP>T6PIZDRIPjZDsGSHqnNdJay5NjbkhHw5kcGmGydCYD5sn/XNYnSjJpAYTpAZlC+prgAQXXJQYmfO6LPIoUJuFw==</DP><DQ>n891ngwjXxDgGbjg84oYosLCg1KSL8SEPNS1o1BgWFJ6e1zc9vRhd3GfTVcyZFI0RwsIQUz6CaJm2JugB8HyaQ==</DQ><InverseQ>oxe42fx2yLATcCG4lbQ5f8Qo8c8ACkT4NxqYl3GXdrojBorBzbvht2+KHq2bJorWtcPNnsmumhV6BIV7zCW0kw==</InverseQ><D>E92nFsH9lH1QYBFPGcNOEcL6uotuVXF4np3/g+t/AevKE6umzkUbfEwhhukY+hG9DgP+gxjTMHel87njYHbtyA+23TdhIzhyYcSg0ifotDhgD8+9lBrn29hyddFigLDoXnZR1SQmvn7xjuGKtZ/HaKZPetSxgVf1mSPdzl37CGE=</D></RSAKeyValue>";
                        StreamReader reader = new StreamReader(stream);
                        string str5 = reader.ReadToEnd();
                        bool flag2 = false;
                        string input = string.Empty;
                        if (!string.IsNullOrEmpty(str5))
                        {
                            byte[] buffer4 = Convert.FromBase64String(str5);
                            byte[] buffer5 = RSA_Crypt.Decrypt(str4, buffer4);
                            if (buffer5 == null)
                            {
                                goto Label_01F5;
                            }
                            input = ToolUtil.GetString(buffer5);
                            foreach (string str7 in Regex.Split(input, ";"))
                            {
                                if (str7 == regFileInfo_0.VerFlag)
                                {
                                    goto Label_01E2;
                                }
                            }
                        }
                        goto Label_0204;
                    Label_01E2:
                        reader.Close();
                        stream.Close();
                        flag2 = true;
                        goto Label_0204;
                    Label_01F5:
                        ilog_0.Error("[CheckRegFile] 读取已注册版本信息失败，解密失败");
                    Label_0204:
                        if (!flag2)
                        {
                            stream.SetLength(0L);
                            StreamWriter writer = new StreamWriter(stream);
                            string str8 = input + regFileInfo_0.VerFlag + ";";
                            string str9 = Convert.ToBase64String(RSA_Crypt.Encrypt(str4, ToolUtil.GetBytes(str8)));
                            writer.Write(str9);
                            writer.Flush();
                            writer.Close();
                        }
                        stream.Close();
                    }
                    catch (Exception exception)
                    {
                        ilog_0.Error("[CheckRegFile] 出现异常：" + exception.Message);
                        stream.Close();
                    }
                }
                return true;
            Label_0298:
                regFileInfo_0.ErrCode = "910107";
                return false;
            Label_02A7:
                regFileInfo_0.ErrCode = "910106";
                return false;
            Label_02BA:
                flag = false;
            }
            catch (Exception exception2)
            {
                ilog_0.Error(exception2.Message);
                flag = false;
            }
            return flag;
        }

        private static RegFileInfo smethod_6(string string_0, Enum15 enum15_0)
        {
            RegFileInfo info = null;
            List<RegFileInfo> list = new List<RegFileInfo>();
            List<string> list2 = smethod_1(FilterTaxCode);
            string verFlag = null;
            foreach (string str2 in list2)
            {
                RegFileInfo item = smethod_3(str2, taxCard_0);
                if ((item != null) && item.CheckedOk)
                {
                    if (enum15_0 == ((Enum15) 0))
                    {
                        verFlag = item.VerFlag;
                    }
                    else if (enum15_0 == ((Enum15) 1))
                    {
                        verFlag = item.SoftFlag;
                    }
                    if (verFlag == string_0)
                    {
                        list.Add(item);
                    }
                }
            }
            DateTime minValue = DateTime.MinValue;
            foreach (RegFileInfo info3 in list)
            {
                if (info3.FileModifyDate > minValue)
                {
                    info = info3;
                    minValue = info3.FileModifyDate;
                }
            }
            return info;
        }

        private static byte[] smethod_7(string string_0)
        {
            return smethod_8(string_0, null);
        }

        private static byte[] smethod_8(string string_0, TaxCard taxCard_1)
        {
            return smethod_9(GetRegFileInfo(string_0, taxCard_1), taxCard_1);
        }

        private static byte[] smethod_9(RegFileInfo regFileInfo_0, TaxCard taxCard_1)
        {
            if (taxCard_1 == null)
            {
                taxCard_1 = taxCard_0;
            }
            else
            {
                taxCard_0 = taxCard_1;
            }
            byte[] buffer2 = new byte[] { (byte) (regFileInfo_0.FileContent.SoftwareType >> 8), (byte) regFileInfo_0.FileContent.SoftwareType };
            byte[] buffer = null;
            string str = taxCard_1.QueryRegCode(buffer2, out buffer, 0);
            if ((buffer != null) && (buffer.Length >= 0x17))
            {
                ilog_0.Error("QueryRegCode调用失败，错误号=" + str);
                return buffer;
            }
            return null;
        }

        public static ushort SetupMachine
        {
            get
            {
                return ushort.Parse(PropertyUtil.GetValue("MAIN_MACHINE", "0"));
            }
        }

        public static string SetupOrgCode
        {
            get
            {
                return PropertyUtil.GetValue("MAIN_ORGCODE", "");
            }
        }

        public static string SetupTaxCode
        {
            get
            {
                return PropertyUtil.GetValue("MAIN_CODE", "");
            }
        }
    }
}

