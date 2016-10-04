namespace Aisino.Fwkp.Wbjk.BLL
{
    using Aisino.Fwkp.Wbjk;
    using Aisino.Fwkp.Wbjk.Common;
    using Aisino.Fwkp.Wbjk.Model;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;

    public class ResolverExcel : Aisino.Fwkp.Wbjk.BLL.Resolver
    {
        private string File1 = string.Empty;
        private string File2 = string.Empty;
        public static string FileFilter = "Excel文件(*.xls,*.xlsx)|*.xls;*.xlsx";

        public ResolverExcel()
        {
            base.Importtype = "Excel";
        }

        private DataTable ExcelLoad()
        {
            DataTable table2;
            try
            {
                string str = this.File1;
                string str2 = this.File2;
                string privateProfileString = IniRead.GetPrivateProfileString("File", "TableInFile1");
                string str4 = IniRead.GetPrivateProfileString("File", "TableInFile2");
                string s = IniRead.GetPrivateProfileString("FieldCon", "FileNumber");
                int result = 1;
                int.TryParse(s, out result);
                List<ExcelMappingItem.Relation> yingShe = new List<ExcelMappingItem.Relation>();
                DataTable table = WenBenItem.Items();
                foreach (DataRow row in table.Rows)
                {
                    string key = row["key"].ToString();
                    string str7 = IniRead.GetPrivateProfileString("FieldCon", key);
                    ExcelMappingItem.Relation item = new ExcelMappingItem.Relation {
                        Key = row["key"].ToString()
                    };
                    int num2 = 0;
                    int.TryParse(str7.Substring(0, str7.IndexOf('.')), out num2);
                    item.TableFlag = num2;
                    num2 = 0;
                    int.TryParse(str7.Substring(str7.LastIndexOf('.') + 1), out num2);
                    item.ColumnName = num2;
                    yingShe.Add(item);
                }
                string defaultFuHeRen = IniRead.GetPrivateProfileString("FieldCon", "DefaultFuHeRen");
                string defaultShouKuanRen = IniRead.GetPrivateProfileString("FieldCon", "DefaultShouKuanRen");
                string defaultShuiLv = IniRead.GetPrivateProfileString("FieldCon", "DefaultShuiLv");
                int num3 = 0;
                int.TryParse(IniRead.GetPrivateProfileString("TableCon", "MainTableField"), out num3);
                int num4 = num3;
                num3 = 0;
                int.TryParse(IniRead.GetPrivateProfileString("TableCon", "AssistantTableField"), out num3);
                int num5 = num3;
                num3 = 0;
                int.TryParse(IniRead.GetPrivateProfileString("TableCon", "MainTableIgnoreRow"), out num3);
                int num6 = num3;
                num3 = 0;
                int.TryParse(IniRead.GetPrivateProfileString("TableCon", "AssistantTableIgnoreRow"), out num3);
                int subHeadline = num3;
                switch (result)
                {
                    case 1:
                        return this.GetFileData(str, privateProfileString, num6, yingShe, defaultFuHeRen, defaultShouKuanRen, defaultShuiLv);

                    case 2:
                        return GetFileData(str, str2, privateProfileString, str4, num6, subHeadline, num4, num5, yingShe, defaultFuHeRen, defaultShouKuanRen, defaultShuiLv);
                }
                table2 = null;
            }
            catch (Exception)
            {
                throw;
            }
            return table2;
        }

        public DataTable GetFileData(string File1, string Sheet1, int row1, List<ExcelMappingItem.Relation> YingShe, string DefaultFuHeRen, string DefaultShouKuanRen, string DefaultShuiLv)
        {
            return GetFileData(File1, string.Empty, Sheet1, string.Empty, row1, 0, 0, 0, YingShe, DefaultFuHeRen, DefaultShouKuanRen, DefaultShuiLv);
        }

        public static DataTable GetFileData(string File1, string File2, string sheet1, string sheet2, int MainHeadline, int SubHeadline, int key1, int key2, List<ExcelMappingItem.Relation> Relationlist, string DefaultFuHeRen, string DefaultShouKuanRen, string DefaultShuiLv)
        {
            DataRow current;
            int num5;
            string str;
            int num8;
            int num9;
            string str3;
            long num10;
            string str4;
            DataTable table = new DataTable();
            DataTable table2 = WenBenItem.Items();
            using (IEnumerator enumerator = table2.Rows.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    current = (DataRow) enumerator.Current;
                    table.Columns.Add(current["key"].ToString());
                }
            }
            DataTable table3 = ExcelRead.ExcelToDataTable(MainHeadline, File1, sheet1 + "$");
            DataTable table4 = new DataTable();
            if (File2.Length > 0)
            {
                table4 = ExcelRead.ExcelToDataTable(SubHeadline, File2, sheet2 + "$");
            }
            int num = 0;
            int num2 = 0;
            int columnName = 0;
            int num4 = 0;
            for (num5 = 0; num5 < Relationlist.Count; num5++)
            {
                ExcelMappingItem.Relation relation = Relationlist[num5];
                if (relation.TableFlag == 1)
                {
                    num++;
                    if (columnName < Relationlist[num5].ColumnName)
                    {
                        columnName = Relationlist[num5].ColumnName;
                    }
                }
                else if ((File2.Length > 0) && (relation.TableFlag == 2))
                {
                    num2++;
                    if (num4 < Relationlist[num5].ColumnName)
                    {
                        num4 = Relationlist[num5].ColumnName;
                    }
                }
            }
            if (columnName > table3.Columns.Count)
            {
                int count = columnName - table3.Columns.Count;
                CommonTool.AddBlankColumns(table3, count);
            }
            if ((File2.Length > 0) && (num4 > table4.Columns.Count))
            {
                int num7 = num4 - table4.Columns.Count;
                CommonTool.AddBlankColumns(table4, num7);
            }
            if (File2 == string.Empty)
            {
                for (num5 = 0; num5 < table3.Rows.Count; num5++)
                {
                    current = table.NewRow();
                    foreach (ExcelMappingItem.Relation relation2 in Relationlist)
                    {
                        if (relation2.TableFlag == 1)
                        {
                            str = table3.Rows[num5][relation2.ColumnName - 1].ToString();
                            current[relation2.Key] = table3.Rows[num5][relation2.ColumnName - 1];
                        }
                        else if ((relation2.TableFlag == 2) && (File2 != string.Empty))
                        {
                            foreach (DataRow row2 in table4.Rows)
                            {
                                if (row2[key2].ToString() == table3.Rows[num5][key1].ToString())
                                {
                                    if (!(relation2.Key == "DanJuHaoMa"))
                                    {
                                        current[relation2.Key] = row2[relation2.ColumnName - 1];
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    num8 = 0;
                    num9 = 0;
                    while (num9 < table.Columns.Count)
                    {
                        if (current[num9].ToString().Trim().Length > 0)
                        {
                            break;
                        }
                        num8++;
                        num9++;
                    }
                    if (num8 != table.Columns.Count)
                    {
                        if (Convert.ToString(current["FuHeRen"]) == string.Empty)
                        {
                            current["FuHeRen"] = DefaultFuHeRen;
                        }
                        if (Convert.ToString(current["ShouKuanRen"]) == string.Empty)
                        {
                            current["ShouKuanRen"] = DefaultShouKuanRen;
                        }
                        if (Convert.ToString(current["ShuiLv"]) == string.Empty)
                        {
                            current["ShuiLv"] = DefaultShuiLv;
                        }
                        str3 = Convert.ToString(current["GouFangShuiHao"]).ToUpper();
                        if (str3.Contains("E+") || str3.Contains("E-"))
                        {
                            num10 = 0L;
                            if (long.TryParse(str3, NumberStyles.Any, null, out num10))
                            {
                                str4 = num10.ToString();
                                current["GouFangShuiHao"] = str4;
                            }
                        }
                        table.Rows.Add(current);
                    }
                }
                return table;
            }
            for (num5 = 0; num5 < table3.Rows.Count; num5++)
            {
                for (int i = 0; i < table4.Rows.Count; i++)
                {
                    if (table3.Rows[num5][key1].ToString() != table4.Rows[i][key2].ToString())
                    {
                        continue;
                    }
                    current = table.NewRow();
                    foreach (ExcelMappingItem.Relation relation2 in Relationlist)
                    {
                        if (relation2.TableFlag == 1)
                        {
                            str = table3.Rows[num5][relation2.ColumnName - 1].ToString();
                            current[relation2.Key] = table3.Rows[num5][relation2.ColumnName - 1];
                        }
                        else if (((relation2.TableFlag == 2) && (File2 != string.Empty)) && (relation2.Key != "DanJuHaoMa"))
                        {
                            current[relation2.Key] = table4.Rows[i][relation2.ColumnName - 1];
                        }
                    }
                    num8 = 0;
                    for (num9 = 0; num9 < table.Columns.Count; num9++)
                    {
                        if (current[num9].ToString().Trim().Length > 0)
                        {
                            break;
                        }
                        num8++;
                    }
                    if (num8 != table.Columns.Count)
                    {
                        if (Convert.ToString(current["FuHeRen"]) == string.Empty)
                        {
                            current["FuHeRen"] = DefaultFuHeRen;
                        }
                        if (Convert.ToString(current["ShouKuanRen"]) == string.Empty)
                        {
                            current["ShouKuanRen"] = DefaultShouKuanRen;
                        }
                        if (Convert.ToString(current["ShuiLv"]) == string.Empty)
                        {
                            current["ShuiLv"] = DefaultShuiLv;
                        }
                        str3 = Convert.ToString(current["GouFangShuiHao"]).ToUpper();
                        if (str3.Contains("E+") || str3.Contains("E-"))
                        {
                            num10 = 0L;
                            if (long.TryParse(str3, NumberStyles.Any, null, out num10))
                            {
                                str4 = num10.ToString();
                                current["GouFangShuiHao"] = str4;
                            }
                        }
                        table.Rows.Add(current);
                    }
                }
            }
            return table;
        }

        protected TempXSDJ GetTempXSDJ(string ID, XSDJ Xsdj)
        {
            foreach (TempXSDJ pxsdj in Xsdj.Dj)
            {
                if (pxsdj.Djh == ID)
                {
                    return pxsdj;
                }
            }
            return null;
        }

        protected XSDJ GetXSDJ()
        {
            XSDJ xsdj = new XSDJ {
                Bdbs = "SJJK0101",
                Bdmc = "Excel数据导入",
                Bdfz = "Excel数据导入"
            };
            DataTable table = this.ExcelLoad();
            foreach (DataRow row in table.Rows)
            {
                string iD = Convert.ToString(row["DanJuHaoMa"]);
                TempXSDJ tempXSDJ = this.GetTempXSDJ(iD, xsdj);
                if (tempXSDJ == null)
                {
                    tempXSDJ = new TempXSDJ {
                        Djh = iD,
                        Sphs = 0,
                        Gfmc = Convert.ToString(row["GouFangMingCheng"]),
                        Gfsh = Convert.ToString(row["GouFangShuiHao"]),
                        Gfdzdh = Convert.ToString(row["GouFangDiZhiDianHua"]),
                        Gfyhzh = Convert.ToString(row["GouFangYinHangZhangHao"]),
                        Bz = Convert.ToString(row["BeiZhu"]),
                        Fhr = Convert.ToString(row["FuHeRen"]),
                        Skr = Convert.ToString(row["ShouKuanRen"]),
                        Qdspmc = Convert.ToString(row["QingDanHangShangPinMingCheng"])
                    };
                    DateTime result = new DateTime(0x76c, 1, 1);
                    if (DateTime.TryParse(row["DanJuRiQi"].ToString(), out result))
                    {
                        tempXSDJ.Djrq = result;
                    }
                    else
                    {
                        tempXSDJ.Djrq = DateTime.Now.Date;
                    }
                    tempXSDJ.Xfyhzh = Convert.ToString(row["XiaoFangYinHangZhangHao"]);
                    tempXSDJ.Xfdzdh = Convert.ToString(row["XiaoFangDiZhiDianHua"]);
                    tempXSDJ.SFZJY = CommonTool.ToStringBool(row["ShenFenZhengJiaoYan"].ToString());
                    string str2 = Convert.ToString(row["HaiYangShiYou"]);
                    tempXSDJ.HYSY = CommonTool.ToBoolString(str2);
                    xsdj.Dj.Add(tempXSDJ);
                }
                TempXSDJ_MX item = new TempXSDJ_MX {
                    Hwmc = Convert.ToString(row["HuoWuMingCheng"]),
                    Jldw = Convert.ToString(row["JiLiangDanWei"]),
                    Gg = Convert.ToString(row["GuiGe"]),
                    Sl = CommonTool.TodoubleNew(Convert.ToString(row["ShuLiang"])),
                    Bhsje = CommonTool.TodoubleNew_x(row["BuHanShuiJinE"].ToString()),
                    Slv = CommonTool.ToSlv(row["ShuiLv"].ToString()),
                    Spsm = Convert.ToString(row["ShangPinShuiMu"]),
                    Zkje = new double?(CommonTool.TodoubleNew(row["ZheKouJinE"].ToString())),
                    Se = CommonTool.TodoubleNew_x(row["ShuiE"].ToString()),
                    Zkse = CommonTool.ToZKSE(row["ZheKouShuiE"].ToString()),
                    Zkl = CommonTool.ToZKL(row["ZheKouLv"].ToString()),
                    Dj = CommonTool.Todouble(row["DanJia"].ToString()),
                    Jgfs = CommonTool.ToStringBool(row["JiaGeFangShi"].ToString()),
                    HangShu = tempXSDJ.Mingxi.Count + 1,
                    DJBH = tempXSDJ.Djh
                };
                tempXSDJ.Mingxi.Add(item);
            }
            base.errorResolver.ImportTotal = xsdj.Dj.Count;
            return xsdj;
        }

        public void ImportExcel(string File1Path, string File2Path, PriceType JiaGeFangShi, InvType FaPiaoLeiXing)
        {
            this.File1 = File1Path;
            this.File2 = File2Path;
            base.Pricemode = JiaGeFangShi;
            base.FaPiaomode = FaPiaoLeiXing;
            XSDJ xSDJ = this.GetXSDJ();
            base.SaveImport(xSDJ);
        }
    }
}

