namespace Aisino.Fwkp.Wbjk.BLL
{
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.Model;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class ResolverTxt : Aisino.Fwkp.Wbjk.BLL.Resolver
    {
        private string BeginStr = "SJJK0101";
        private string path;
        private string SplitStr = "~~";
        private string[] SplitStrArray = new string[] { "~~", ",", " " };

        public ResolverTxt()
        {
            base.Importtype = "txt";
        }

        protected TempXSDJ GetTempXSDJ(strDanju danju)
        {
            int num;
            if (danju.str.Count < 7)
            {
                for (num = danju.str.Count; num < 7; num++)
                {
                    danju.str.Add("");
                }
            }
            TempXSDJ pxsdj = new TempXSDJ {
                Djh = danju.str[0]
            };
            int result = 0;
            if (!int.TryParse(danju.str[1], out result))
            {
                throw new CustomException("传入txt格式不正确");
            }
            pxsdj.Sphs = result;
            pxsdj.Gfmc = danju.str[2];
            pxsdj.Gfsh = danju.str[3];
            pxsdj.Gfdzdh = danju.str[4];
            pxsdj.Gfyhzh = danju.str[5];
            pxsdj.Bz = danju.str[6];
            if (danju.str.Count > 7)
            {
                pxsdj.Fhr = danju.str[7];
            }
            if (danju.str.Count > 8)
            {
                pxsdj.Skr = danju.str[8];
            }
            if (danju.str.Count > 9)
            {
                pxsdj.Qdspmc = danju.str[9];
            }
            if ((danju.str.Count > 10) && !string.IsNullOrEmpty(danju.str[10]))
            {
                DateTime now = DateTime.Now;
                DateTime.TryParse(danju.str[10], out now);
                pxsdj.Djrq = now.Date;
            }
            if (danju.str.Count > 11)
            {
                pxsdj.Xfyhzh = danju.str[11];
            }
            if (danju.str.Count > 12)
            {
                pxsdj.Xfdzdh = danju.str[12];
            }
            if (danju.str.Count > 13)
            {
                pxsdj.SFZJY = CommonTool.ToStringBool(danju.str[13]);
            }
            if (danju.str.Count > 14)
            {
                pxsdj.HYSY = CommonTool.ToBoolString(danju.str[14]);
            }
            for (num = 0; num < danju.mingxi.Count; num++)
            {
                TempXSDJ_MX item = this.GetTempXSDJ_MX(danju.mingxi[num], pxsdj.Djh, num + 1);
                if (item != null)
                {
                    pxsdj.Mingxi.Add(item);
                }
            }
            return pxsdj;
        }

        private TempXSDJ_MX GetTempXSDJ_MX(strMingxi MingXi, string Djbh, int Hs)
        {
            TempXSDJ_MX pxsdj_mx = new TempXSDJ_MX();
            for (int i = MingXi.str.Count; i < 13; i++)
            {
                MingXi.str.Add("");
            }
            pxsdj_mx.Hwmc = MingXi.str[0];
            pxsdj_mx.Jldw = MingXi.str[1];
            pxsdj_mx.Gg = MingXi.str[2];
            pxsdj_mx.Sl = CommonTool.TodoubleNew(MingXi.str[3]);
            pxsdj_mx.Bhsje = CommonTool.TodoubleNew_x(MingXi.str[4]);
            pxsdj_mx.Slv = CommonTool.ToSlv(MingXi.str[5]);
            pxsdj_mx.Spsm = MingXi.str[6];
            pxsdj_mx.Zkje = new double?(CommonTool.TodoubleNew(MingXi.str[7]));
            pxsdj_mx.Se = CommonTool.TodoubleNew_x(MingXi.str[8]);
            pxsdj_mx.Zkse = CommonTool.ToZKSE(MingXi.str[9]);
            pxsdj_mx.Zkl = CommonTool.ToZKL(MingXi.str[10]);
            pxsdj_mx.Dj = CommonTool.Todouble(MingXi.str[11]);
            pxsdj_mx.Jgfs = CommonTool.ToStringBool(MingXi.str[12]);
            pxsdj_mx.DJBH = Djbh;
            pxsdj_mx.HangShu = Hs;
            return pxsdj_mx;
        }

        private XSDJ GetXSDJ()
        {
            strBiao biao = this.JieXiShuXing();
            XSDJ xsdj = new XSDJ();
            if (biao.str.Count > 0)
            {
                xsdj.Bdbs = biao.str[0];
                if (biao.str.Count > 2)
                {
                    xsdj.Bdmc = biao.str[1];
                    xsdj.Bdfz = biao.str[2];
                }
            }
            for (int i = 0; i < biao.danju.Count; i++)
            {
                TempXSDJ item = new TempXSDJ();
                item = this.GetTempXSDJ(biao.danju[i]);
                if (item != null)
                {
                    xsdj.Dj.Add(item);
                }
            }
            return xsdj;
        }

        public void ImportTxt(string Path, PriceType JGFS, InvType FPLX)
        {
            this.path = Path;
            base.Pricemode = JGFS;
            base.FaPiaomode = FPLX;
            XSDJ xSDJ = this.GetXSDJ();
            base.SaveImport(xSDJ);
        }

        public strBiao JieXiShuXing()
        {
            strBiao biao = new strBiao();
            string[] strArray = File.ReadAllLines(this.path, ToolUtil.GetEncoding());
            List<string> list = new List<string>();
            for (int i = 0; i < strArray.Length; i++)
            {
                if (!((strArray[i].Trim().Trim(new char[1]).Length == 0) || strArray[i].Trim().StartsWith("//")))
                {
                    list.Add(strArray[i]);
                }
            }
            int num2 = 0;
            if (list.Count < 2)
            {
                throw new CustomException("001");
            }
            string row = list[num2];
            if (row.Contains("~~"))
            {
                if (!row.Substring(0, row.IndexOf(this.SplitStr)).Equals(this.BeginStr))
                {
                    throw new CustomException("001");
                }
            }
            else if (row.Contains(" ") | row.Contains(","))
            {
                this.SplitStr = " ";
            }
            if (this.SplitStr.Equals("~~"))
            {
                biao.str = this.StringSplit(row);
            }
            else
            {
                num2 = -1;
            }
            while (true)
            {
                bool flag = true;
                num2++;
                if (num2 >= list.Count)
                {
                    return biao;
                }
                row = list[num2];
                strDanju item = new strDanju {
                    str = this.StringSplit(row)
                };
                int result = 0;
                if (item.str.Count > 1)
                {
                    if (!int.TryParse(item.str[1], out result))
                    {
                        base.errorResolver.AddError("单据商品行数有错误", item.str[0], 1, false);
                    }
                    else
                    {
                        base.errorResolver.ImportTotal++;
                        while (0 < result--)
                        {
                            num2++;
                            int lineNum = int.Parse(item.str[1]) - result;
                            if (num2 < list.Count)
                            {
                                strMingxi mingxi = new strMingxi {
                                    str = this.StringSplit(list[num2])
                                };
                                item.mingxi.Add(mingxi);
                            }
                            else
                            {
                                base.errorResolver.AddError("缺少商品行", item.str[0], lineNum, false);
                                flag = false;
                            }
                        }
                        if (flag)
                        {
                            biao.danju.Add(item);
                        }
                        else
                        {
                            base.errorResolver.AbandonCount++;
                        }
                    }
                }
                else
                {
                    base.errorResolver.AddError("单据仅有单据号", item.str[0], 1, false);
                }
            }
        }

        protected List<string> StringSplit(string row)
        {
            string[] strArray;
            List<string> list = new List<string>();
            row = row.Trim();
            if (this.SplitStr == "~~")
            {
                strArray = row.Split(new string[] { this.SplitStr }, StringSplitOptions.None);
                foreach (string str in strArray)
                {
                    list.Add(str.Trim());
                }
                return list;
            }
            strArray = row.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in strArray)
            {
                if (str.Contains(","))
                {
                    string[] strArray2 = str.Split(new string[] { "," }, StringSplitOptions.None);
                    for (int i = 0; i < strArray2.Length; i++)
                    {
                        if (!((i == 0) | (i == (strArray2.Length - 1))) || (strArray2[i].Length != 0))
                        {
                            list.Add(strArray2[i].Trim(new char[] { '"' }));
                        }
                    }
                }
                else
                {
                    list.Add(str.Trim(new char[] { '"' }));
                }
            }
            return list;
        }
    }
}

