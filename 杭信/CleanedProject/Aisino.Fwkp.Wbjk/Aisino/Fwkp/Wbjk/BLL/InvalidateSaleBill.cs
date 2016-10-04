namespace Aisino.Fwkp.Wbjk.BLL
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.DAL;
    using Aisino.Fwkp.Wbjk.Model;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;

    public class InvalidateSaleBill
    {
        private DJZFCRdal MakeBillInvalidDAL = new DJZFCRdal();
        private string strBeginFlag = "SJJK0102";
        private string strRemark = "//";
        private string strSplitter = "~~";
        private string strWasteFilePath;

        private List<string> GetBillList(string strBillPath)
        {
            List<string> list = new List<string>();
            string[] strArray = File.ReadAllLines(strBillPath, ToolUtil.GetEncoding());
            for (int i = 0; i < strArray.Length; i++)
            {
                if (!(!(strArray[i].Trim() != "") || strArray[i].Trim().StartsWith(this.strRemark)))
                {
                    list.Add(strArray[i]);
                }
            }
            if (list.Count <= 0)
            {
                throw new Exception("不是作废销售单据");
            }
            string str = list[0];
            if (!str.Substring(0, str.IndexOf(this.strSplitter)).Equals(this.strBeginFlag))
            {
                throw new Exception("不是作废销售单据");
            }
            list.RemoveAt(0);
            return list;
        }

        private bool GetBillList(string strBillPath, ref List<string> strBillList, ref string _strCause)
        {
            try
            {
                string[] strArray = File.ReadAllLines(strBillPath, ToolUtil.GetEncoding());
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (!(!(strArray[i].Trim() != "") || strArray[i].Trim().StartsWith(this.strRemark)))
                    {
                        strBillList.Add(strArray[i].Trim().Trim(new char[1]));
                    }
                }
                if (strBillList.Count > 0)
                {
                    string str = strBillList[0];
                    if (!str.Substring(0, str.IndexOf(this.strSplitter)).Equals(this.strBeginFlag))
                    {
                        _strCause = "不是作废单据文本文件";
                        return false;
                    }
                    strBillList.RemoveAt(0);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw new Exception("不是作废单据文本文件");
            }
            return true;
        }

        private void MakeBillInvalid(string strBillNum, DJZFImportResult ImportResult)
        {
            try
            {
                string str = "";
                string str2 = "";
                ArrayList billState = this.MakeBillInvalidDAL.GetBillState(strBillNum);
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                for (int i = 0; i < billState.Count; i++)
                {
                    dictionary = billState[i] as Dictionary<string, object>;
                    str = dictionary["DJZT"].ToString();
                    str2 = dictionary["KPZT"].ToString();
                }
                if (!ImportResult.Result.ContainsKey(strBillNum))
                {
                    if (!(this.MakeBillInvalidDAL.RecordIsExist(strBillNum) && (billState.Count >= 0)))
                    {
                        ImportResult.Undo++;
                        ImportResult.Result.Add(strBillNum, "单据不存在");
                    }
                    else if (str.ToUpper() == "W")
                    {
                        ImportResult.Undo++;
                        ImportResult.Result.Add(strBillNum, "已经作废");
                    }
                    else
                    {
                        int num2 = this.MakeBillInvalidDAL.MakeBillInvalid(strBillNum);
                        if (((str2.ToUpper() == "Y") || (str2.ToUpper() == "P")) || (str2.ToUpper() == "A"))
                        {
                            ImportResult.Undo++;
                            ImportResult.Result.Add(strBillNum, "已经开票");
                        }
                        else if ((str2 == "") || (str2.ToUpper() == "X"))
                        {
                            ImportResult.Undo++;
                            ImportResult.Result.Add(strBillNum, "单据不存在");
                        }
                        else if (num2 > 0)
                        {
                            ImportResult.Success++;
                            ImportResult.Result.Add(strBillNum, "作废成功");
                        }
                        else
                        {
                            ImportResult.Undo++;
                            ImportResult.Result.Add(strBillNum, "未知错误");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
        }

        public DJZFImportResult MakeInvalidExecute(string strWastePath, ref string _strCause)
        {
            DJZFImportResult importResult = new DJZFImportResult();
            List<string> strBillList = new List<string>();
            if (!this.GetBillList(strWastePath, ref strBillList, ref _strCause))
            {
                return null;
            }
            for (int i = 0; i < strBillList.Count; i++)
            {
                this.MakeBillInvalid(strBillList[i], importResult);
            }
            return importResult;
        }

        public string strBillImportPath
        {
            get
            {
                this.strWasteFilePath = ReadWriteXml.Read("TxtZuoFeiImport", "Path");
                return this.strWasteFilePath;
            }
            set
            {
                ReadWriteXml.Write("TxtZuoFeiImport", "Path", value);
                this.strWasteFilePath = value;
            }
        }
    }
}

