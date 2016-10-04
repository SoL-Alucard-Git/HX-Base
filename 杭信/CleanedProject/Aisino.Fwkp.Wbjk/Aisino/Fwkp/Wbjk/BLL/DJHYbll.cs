namespace Aisino.Fwkp.Wbjk.BLL
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;

    internal class DJHYbll
    {
        private int _currentPage = 1;
        private DJHYdal djhyDAL = new DJHYdal();
        private string FlagChar = string.Empty;
        private List<string> XhList = new List<string>();

        private bool CanBeResume1(string BH)
        {
            int num;
            string text = CommonTool.ReverseS(BH);
            bool flag = false;
            if ((((text.IndexOf("_") > 0) && (text.IndexOf("~") > 0)) && (text.IndexOf("_") < text.IndexOf("~"))) || ((text.IndexOf("_") > 0) && (text.IndexOf("~") == -1)))
            {
                this.FlagChar = "_";
            }
            else if ((((text.IndexOf("_") > 0) && (text.IndexOf("~") > 0)) && (text.IndexOf("_") > text.IndexOf("~"))) || ((text.IndexOf("_") == -1) && (text.IndexOf("~") > 0)))
            {
                this.FlagChar = "~";
            }
            text = CommonTool.ReverseS(text);
            if (this.FlagChar == "_")
            {
                text = this.GetRealBillNo(text);
                DataTable table = this.djhyDAL.CFHYCheck1(text);
                if (table.Rows.Count > 0)
                {
                    for (num = 0; num < table.Rows.Count; num++)
                    {
                        this.XhList.Add(table.Rows[num][0].ToString());
                    }
                    return flag;
                }
                return true;
            }
            if (this.FlagChar == "~")
            {
                DataTable table2 = this.djhyDAL.HBHYCheck1(text);
                if (table2.Rows.Count > 0)
                {
                    List<string> list = new List<string>();
                    for (num = 0; num < table2.Rows.Count; num++)
                    {
                        list.Add(table2.Rows[num][0].ToString());
                    }
                    foreach (string str2 in list)
                    {
                        if (this.djhyDAL.HBHYCheck2(str2).Rows.Count > 0)
                        {
                            this.XhList.Add(str2);
                        }
                    }
                }
                if (this.XhList.Count == 0)
                {
                    flag = true;
                }
            }
            return flag;
        }

        private bool CanBeResume2(string BH)
        {
            bool flag = false;
            if (this.FlagChar == "_")
            {
                int num;
                string str3;
                string bH = this.GetRealBillNo(BH) + this.FlagChar;
                DataTable table = this.djhyDAL.CFHYCheck2(bH);
                if (table.Rows.Count > 0)
                {
                    for (num = 0; num < table.Rows.Count; num++)
                    {
                        str3 = table.Rows[num][0].ToString();
                        if ((this.CharCount(str3, '_') == 1) && (str3.IndexOf(bH) == 0))
                        {
                            this.XhList.Add(str3);
                        }
                    }
                }
                if (this.XhList.Count == 0)
                {
                    flag = true;
                }
                if (flag)
                {
                    DataTable table2 = this.djhyDAL.CFHYCheck3(bH);
                    if (table2.Rows.Count <= 0)
                    {
                        return flag;
                    }
                    for (num = 0; num < table2.Rows.Count; num++)
                    {
                        str3 = table2.Rows[num][0].ToString();
                        if (str3.IndexOf(bH) == 0)
                        {
                            this.XhList.Add(str3);
                        }
                    }
                }
                return flag;
            }
            if (this.FlagChar == "~")
            {
                this.XhList.Add(BH);
                flag = true;
            }
            return flag;
        }

        private int CharCount(string s, char sChar)
        {
            int num = 0;
            if (s.Length > 0)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == sChar)
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        private bool CheckHuanYuan(string BH)
        {
            int num;
            string str = string.Empty;
            if (!this.CanBeResume1(BH))
            {
                if (this.XhList.Count > 0)
                {
                    if (this.XhList.Count < 6)
                    {
                        for (num = 0; num < this.XhList.Count; num++)
                        {
                            str = str + "    " + this.XhList[num] + "\n";
                        }
                        MessageManager.ShowMsgBox("INP-272406", "", new string[] { str });
                    }
                    else
                    {
                        num = 0;
                        while (num < 5)
                        {
                            str = str + "    " + this.XhList[num] + "\n";
                            num++;
                        }
                        MessageManager.ShowMsgBox("INP-272407", "", new string[] { str, Convert.ToString(this.XhList.Count) });
                    }
                }
                return false;
            }
            if (this.CanBeResume2(BH))
            {
                return true;
            }
            if (this.XhList.Count < 6)
            {
                for (num = 0; num < this.XhList.Count; num++)
                {
                    str = str + "    " + this.XhList[num] + "\n";
                }
                MessageManager.ShowMsgBox("INP-272402", "", new string[] { str });
            }
            else
            {
                for (num = 0; num < 5; num++)
                {
                    str = str + "    " + this.XhList[num] + "\n";
                }
                MessageManager.ShowMsgBox("INP-272408", "", new string[] { str, Convert.ToString(this.XhList.Count) });
            }
            return false;
        }

        public AisinoDataSet GetDJMX(string CurrentBH, int pagesize, int pageno)
        {
            return this.djhyDAL.GetDJMX(CurrentBH, pagesize, pageno);
        }

        private string GetRealBillNo(string BH)
        {
            if (BH.IndexOf(this.FlagChar) == -1)
            {
                return BH;
            }
            string str = CommonTool.ReverseS(BH);
            return CommonTool.ReverseS(str.Substring(str.IndexOf(this.FlagChar) + 1, (str.Length - str.IndexOf(this.FlagChar)) - 1));
        }

        public string GetReferDanJu(string BH, string DJBH)
        {
            return this.djhyDAL.GetReferDanJu(BH, DJBH);
        }

        public AisinoDataSet GetYSDJ(string CurrentBH, int pagesize, int pageno)
        {
            return this.djhyDAL.GetYSDJ(CurrentBH, pagesize, pageno);
        }

        public AisinoDataSet GetYSDJMX(string OriginalBH, int pagesize, int pageno)
        {
            return this.djhyDAL.GetYSDJMX(OriginalBH, pagesize, pageno);
        }

        public AisinoDataSet QueryXSDJ(string KeyWord, int pagesize, int pageno)
        {
            return this.djhyDAL.QueryXSDJ(KeyWord, pagesize, pageno);
        }

        public string SaveHuanYuan(string BH)
        {
            int num;
            this.XhList.Clear();
            DialogResult none = DialogResult.None;
            string str = "\n";
            if (!this.CheckHuanYuan(BH))
            {
                return "Error";
            }
            if (this.XhList.Count < 6)
            {
                if (this.XhList.Count > 1)
                {
                    for (num = 0; num < this.XhList.Count; num++)
                    {
                        if (this.XhList[num] != BH)
                        {
                            str = str + "    " + this.XhList[num] + "\n";
                        }
                    }
                    none = MessageManager.ShowMsgBox("INP-272403", "", new string[] { str });
                }
                else if (this.XhList.Count == 1)
                {
                    str = str + "  " + this.XhList[0] + "\n";
                    none = MessageManager.ShowMsgBox("INP-272405", "", new string[] { str });
                }
            }
            else
            {
                for (num = 0; num < 5; num++)
                {
                    str = str + "    " + this.XhList[num] + "\n";
                }
                str = str + "等，共" + this.XhList.Count.ToString() + "张单据\n";
                none = MessageManager.ShowMsgBox("INP-272403", "", new string[] { str, Convert.ToString(this.XhList.Count) });
            }
            if (none == DialogResult.OK)
            {
                string realBillNo = this.GetRealBillNo(BH);
                if (this.djhyDAL.SaveHuanYuan(realBillNo, BH, this.FlagChar, this.XhList) > 0)
                {
                    MessageManager.ShowMsgBox("INP-272404");
                    return "OK";
                }
                MessageManager.ShowMsgBox("INP-272405");
                return "Error";
            }
            return "Cancel";
        }

        public int CurrentPage
        {
            get
            {
                return this._currentPage;
            }
            set
            {
                this._currentPage = value;
            }
        }

        public int Pagesize
        {
            get
            {
                return PropValue.Pagesize;
            }
            set
            {
                PropValue.Pagesize = value;
            }
        }
    }
}

