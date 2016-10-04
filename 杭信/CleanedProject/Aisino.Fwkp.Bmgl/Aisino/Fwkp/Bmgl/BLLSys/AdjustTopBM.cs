namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Dao;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Common;
    using Aisino.Fwkp.Bmgl.Model;
    using System;
    using System.Data;

    internal sealed class AdjustTopBM : AbstractService
    {
        private IBaseDAO baseDAO = BaseDAOFactory.GetBaseDAOSQLite();

        protected override object[] doService(object[] param)
        {
            try
            {
                bool flag = false;
                string tableName = ((string) param[0]).Trim();
                DataTable topNodeTable = this.baseDAO.querySQLDataTable("SELECT * FROM " + tableName + " WHERE SJBM IS NULL OR SJBM = ''");
                if ((topNodeTable != null) && (topNodeTable.Rows.Count != 0))
                {
                    int maxLenOfTopNodes = 0;
                    string str2 = "";
                    if (topNodeTable.Rows.Count > 0)
                    {
                        str2 = GetSafeData.ValidateValue<string>(topNodeTable.Rows[0], "BM");
                    }
                    foreach (DataRow row in topNodeTable.Rows)
                    {
                        string str3 = GetSafeData.ValidateValue<string>(row, "BM");
                        if (str3.Length > maxLenOfTopNodes)
                        {
                            maxLenOfTopNodes = str3.Length;
                        }
                        if (str2.Length != str3.Length)
                        {
                            flag = true;
                        }
                    }
                    if (maxLenOfTopNodes > 6)
                    {
                        this.ReFirstNodes(tableName, topNodeTable, maxLenOfTopNodes, false);
                    }
                    else if (flag)
                    {
                        this.ReFirstNodes(tableName, topNodeTable, maxLenOfTopNodes, true);
                    }
                    else
                    {
                        return new object[] { "true" };
                    }
                    bool flag2 = false;
                    DataTable table2 = new DataTable();
                    flag2 = false;
                    foreach (DataRow row2 in this.baseDAO.querySQLDataTable("SELECT * FROM " + tableName).Rows)
                    {
                        if (GetSafeData.ValidateValue<string>(row2, "BM").Length > 0x10)
                        {
                            flag2 = true;
                        }
                    }
                    if (flag2)
                    {
                        this.ReNode(tableName, "");
                    }
                }
                return new object[] { "true" };
            }
            catch (Exception)
            {
                return new object[] { "false" };
            }
        }

        private void ReFirstNodes(string tableName, DataTable topNodeTable, int maxLenOfTopNodes, bool padFlag)
        {
            try
            {
                int num3;
                int num = 0;
                int result = 0;
                switch (tableName)
                {
                    case "BM_KH":
                        num = 0;
                        result = 0;
                        foreach (DataRow row in topNodeTable.Rows)
                        {
                            BMKHModel customer = new BMKHModel();
                            BMKHManager manager = new BMKHManager();
                            customer.SJBM = GetSafeData.ValidateValue<string>(row, "SJBM");
                            string s = GetSafeData.ValidateValue<string>(row, "BM");
                            if (padFlag)
                            {
                                customer.BM = s.PadLeft(maxLenOfTopNodes, '0');
                            }
                            else
                            {
                                int num4 = num + 1;
                                customer.BM = customer.SJBM + num4.ToString().PadLeft(topNodeTable.Rows.Count.ToString().Length + 2, '0');
                            }
                            customer.MC = GetSafeData.ValidateValue<string>(row, "MC");
                            customer.JM = GetSafeData.ValidateValue<string>(row, "JM");
                            customer.KJM = GetSafeData.ValidateValue<string>(row, "KJM");
                            customer.SH = GetSafeData.ValidateValue<string>(row, "SH");
                            customer.DZDH = GetSafeData.ValidateValue<string>(row, "DZDH");
                            customer.YHZH = GetSafeData.ValidateValue<string>(row, "YHZH");
                            customer.YJDZ = GetSafeData.ValidateValue<string>(row, "YJDZ");
                            customer.BZ = GetSafeData.ValidateValue<string>(row, "BZ");
                            customer.YSKM = GetSafeData.ValidateValue<string>(row, "YSKM");
                            customer.DQBM = GetSafeData.ValidateValue<string>(row, "DQBM");
                            customer.DQMC = GetSafeData.ValidateValue<string>(row, "DQMC");
                            customer.DQKM = GetSafeData.ValidateValue<string>(row, "DQKM");
                            customer.SFZJY = GetSafeData.ValidateValue<bool>(row, "SFZJY");
                            customer.WJ = GetSafeData.ValidateValue<int>(row, "WJ");
                            if (!int.TryParse(s, out result))
                            {
                                result++;
                            }
                            for (string str2 = manager.UpdateSubNodesSJBM(customer, s); "e1" == str2; str2 = manager.UpdateSubNodesSJBM(customer, s))
                            {
                                if (padFlag)
                                {
                                    result++;
                                    customer.BM = result.ToString().PadLeft(maxLenOfTopNodes, '0');
                                }
                                else
                                {
                                    num++;
                                    int num6 = num + 1;
                                    customer.BM = customer.SJBM + num6.ToString().PadLeft(topNodeTable.Rows.Count.ToString().Length + 2, '0');
                                }
                            }
                            num++;
                        }
                        return;

                    case "BM_SP":
                        num = 0;
                        result = 0;
                        foreach (DataRow row2 in topNodeTable.Rows)
                        {
                            BMSPModel merchandise = new BMSPModel();
                            BMSPManager manager2 = new BMSPManager();
                            merchandise.SJBM = GetSafeData.ValidateValue<string>(row2, "SJBM");
                            string str3 = GetSafeData.ValidateValue<string>(row2, "BM");
                            if (padFlag)
                            {
                                merchandise.BM = str3.PadLeft(maxLenOfTopNodes, '0');
                            }
                            else
                            {
                                int num8 = num + 1;
                                merchandise.BM = merchandise.SJBM + num8.ToString().PadLeft(topNodeTable.Rows.Count.ToString().Length + 2, '0');
                            }
                            merchandise.MC = GetSafeData.ValidateValue<string>(row2, "MC");
                            merchandise.JM = GetSafeData.ValidateValue<string>(row2, "JM");
                            merchandise.KJM = GetSafeData.ValidateValue<string>(row2, "KJM");
                            merchandise.SLV = GetSafeData.ValidateValue<double>(row2, "SLV");
                            merchandise.SPSM = GetSafeData.ValidateValue<string>(row2, "SPSM");
                            merchandise.GGXH = GetSafeData.ValidateValue<string>(row2, "GGXH");
                            merchandise.JLDW = GetSafeData.ValidateValue<string>(row2, "JLDW");
                            merchandise.DJ = GetSafeData.ValidateValue<double>(row2, "DJ");
                            merchandise.HSJBZ = GetSafeData.ValidateValue<bool>(row2, "HSJBZ");
                            merchandise.XSSRKM = GetSafeData.ValidateValue<string>(row2, "XSSRKM");
                            merchandise.YJZZSKM = GetSafeData.ValidateValue<string>(row2, "YJZZSKM");
                            merchandise.XSTHKM = GetSafeData.ValidateValue<string>(row2, "XSTHKM");
                            merchandise.HYSY = GetSafeData.ValidateValue<bool>(row2, "HYSY");
                            merchandise.XTHASH = GetSafeData.ValidateValue<string>(row2, "XTHASH");
                            merchandise.XTCODE = GetSafeData.ValidateValue<string>(row2, "XTCODE");
                            merchandise.ISHIDE = GetSafeData.ValidateValue<string>(row2, "ISHIDE");
                            merchandise.WJ = GetSafeData.ValidateValue<int>(row2, "WJ");
                            if (!int.TryParse(str3, out result))
                            {
                                result++;
                            }
                            for (string str4 = manager2.UpdateSubNodesSJBM(merchandise, str3); "e1" == str4; str4 = manager2.UpdateSubNodesSJBM(merchandise, str3))
                            {
                                if (padFlag)
                                {
                                    result++;
                                    merchandise.BM = result.ToString().PadLeft(maxLenOfTopNodes, '0');
                                }
                                else
                                {
                                    num++;
                                    int num10 = num + 1;
                                    merchandise.BM = merchandise.SJBM + num10.ToString().PadLeft(topNodeTable.Rows.Count.ToString().Length + 2, '0');
                                }
                            }
                            num++;
                        }
                        return;

                    case "BM_SFHR":
                        num = 0;
                        result = 0;
                        foreach (DataRow row3 in topNodeTable.Rows)
                        {
                            BMSFHRModel model3 = new BMSFHRModel();
                            BMSFHRManager manager3 = new BMSFHRManager();
                            model3.SJBM = GetSafeData.ValidateValue<string>(row3, "SJBM");
                            string str5 = GetSafeData.ValidateValue<string>(row3, "BM");
                            if (padFlag)
                            {
                                model3.BM = str5.PadLeft(maxLenOfTopNodes, '0');
                            }
                            else
                            {
                                int num12 = num + 1;
                                model3.BM = model3.SJBM + num12.ToString().PadLeft(topNodeTable.Rows.Count.ToString().Length + 2, '0');
                            }
                            model3.MC = GetSafeData.ValidateValue<string>(row3, "MC");
                            model3.JM = GetSafeData.ValidateValue<string>(row3, "JM");
                            model3.SH = GetSafeData.ValidateValue<string>(row3, "SH");
                            model3.DZDH = GetSafeData.ValidateValue<string>(row3, "DZDH");
                            model3.YHZH = GetSafeData.ValidateValue<string>(row3, "YHZH");
                            model3.YZBM = GetSafeData.ValidateValue<string>(row3, "YZBM");
                            model3.WJ = GetSafeData.ValidateValue<int>(row3, "WJ");
                            if (!int.TryParse(str5, out result))
                            {
                                result++;
                            }
                            for (string str6 = manager3.UpdateSubNodesSJBM(model3, str5); "e1" == str6; str6 = manager3.UpdateSubNodesSJBM(model3, str5))
                            {
                                if (padFlag)
                                {
                                    result++;
                                    model3.BM = result.ToString().PadLeft(maxLenOfTopNodes, '0');
                                }
                                else
                                {
                                    num++;
                                    int num14 = num + 1;
                                    model3.BM = model3.SJBM + num14.ToString().PadLeft(topNodeTable.Rows.Count.ToString().Length + 2, '0');
                                }
                            }
                            num++;
                        }
                        return;

                    case "BM_FYXM":
                        num = 0;
                        result = 0;
                        foreach (DataRow row4 in topNodeTable.Rows)
                        {
                            BMFYXMModel feiyong = new BMFYXMModel();
                            BMFYXMManager manager4 = new BMFYXMManager();
                            feiyong.SJBM = GetSafeData.ValidateValue<string>(row4, "SJBM");
                            string str7 = GetSafeData.ValidateValue<string>(row4, "BM");
                            if (padFlag)
                            {
                                feiyong.BM = str7.PadLeft(maxLenOfTopNodes, '0');
                            }
                            else
                            {
                                int num16 = num + 1;
                                feiyong.BM = feiyong.SJBM + num16.ToString().PadLeft(topNodeTable.Rows.Count.ToString().Length + 2, '0');
                            }
                            feiyong.MC = GetSafeData.ValidateValue<string>(row4, "MC");
                            feiyong.JM = GetSafeData.ValidateValue<string>(row4, "JM");
                            feiyong.WJ = GetSafeData.ValidateValue<int>(row4, "WJ");
                            if (!int.TryParse(str7, out result))
                            {
                                result++;
                            }
                            for (string str8 = manager4.UpdateSubNodesSJBM(feiyong, str7); "e1" == str8; str8 = manager4.UpdateSubNodesSJBM(feiyong, str7))
                            {
                                if (padFlag)
                                {
                                    result++;
                                    feiyong.BM = result.ToString().PadLeft(maxLenOfTopNodes, '0');
                                }
                                else
                                {
                                    num++;
                                    int num18 = num + 1;
                                    feiyong.BM = feiyong.SJBM + num18.ToString().PadLeft(topNodeTable.Rows.Count.ToString().Length + 2, '0');
                                }
                            }
                            num++;
                        }
                        return;

                    case "BM_GHDW":
                        num = 0;
                        result = 0;
                        foreach (DataRow row5 in topNodeTable.Rows)
                        {
                            BMGHDWModel purchase = new BMGHDWModel();
                            BMGHDWManager manager5 = new BMGHDWManager();
                            purchase.SJBM = GetSafeData.ValidateValue<string>(row5, "SJBM");
                            string str9 = GetSafeData.ValidateValue<string>(row5, "BM");
                            if (padFlag)
                            {
                                purchase.BM = str9.PadLeft(maxLenOfTopNodes, '0');
                            }
                            else
                            {
                                num3 = num + 1;
                                purchase.BM = purchase.SJBM + num3.ToString().PadLeft(topNodeTable.Rows.Count.ToString().Length + 2, '0');
                            }
                            purchase.MC = GetSafeData.ValidateValue<string>(row5, "MC");
                            purchase.JM = GetSafeData.ValidateValue<string>(row5, "JM");
                            purchase.SH = GetSafeData.ValidateValue<string>(row5, "SH");
                            purchase.DZDH = GetSafeData.ValidateValue<string>(row5, "DZDH");
                            purchase.YHZH = GetSafeData.ValidateValue<string>(row5, "YHZH");
                            purchase.IDCOC = GetSafeData.ValidateValue<string>(row5, "IDCOC");
                            purchase.WJ = GetSafeData.ValidateValue<int>(row5, "WJ");
                            if (!int.TryParse(str9, out result))
                            {
                                result++;
                            }
                            for (string str10 = manager5.UpdateSubNodesSJBM(purchase, str9); "e1" == str10; str10 = manager5.UpdateSubNodesSJBM(purchase, str9))
                            {
                                if (padFlag)
                                {
                                    result++;
                                    purchase.BM = result.ToString().PadLeft(maxLenOfTopNodes, '0');
                                }
                                else
                                {
                                    num++;
                                    num3 = num + 1;
                                    purchase.BM = purchase.SJBM + num3.ToString().PadLeft(topNodeTable.Rows.Count.ToString().Length + 2, '0');
                                }
                            }
                            num++;
                        }
                        return;

                    case "BM_CL":
                        num = 0;
                        result = 0;
                        foreach (DataRow row6 in topNodeTable.Rows)
                        {
                            BMCLModel car = new BMCLModel();
                            BMCLManager manager6 = new BMCLManager();
                            car.SJBM = GetSafeData.ValidateValue<string>(row6, "SJBM");
                            string str11 = GetSafeData.ValidateValue<string>(row6, "BM");
                            if (padFlag)
                            {
                                car.BM = str11.PadLeft(maxLenOfTopNodes, '0');
                            }
                            else
                            {
                                num3 = num + 1;
                                car.BM = car.SJBM + num3.ToString().PadLeft(topNodeTable.Rows.Count.ToString().Length + 2, '0');
                            }
                            car.MC = GetSafeData.ValidateValue<string>(row6, "MC");
                            car.JM = GetSafeData.ValidateValue<string>(row6, "JM");
                            car.CPXH = GetSafeData.ValidateValue<string>(row6, "CPXH");
                            car.CD = GetSafeData.ValidateValue<string>(row6, "CD");
                            car.SCCJMC = GetSafeData.ValidateValue<string>(row6, "SCCJMC");
                            car.WJ = GetSafeData.ValidateValue<int>(row6, "WJ");
                            if (!int.TryParse(str11, out result))
                            {
                                result++;
                            }
                            for (string str12 = manager6.UpdateSubNodesSJBM(car, str11); "e1" == str12; str12 = manager6.UpdateSubNodesSJBM(car, str11))
                            {
                                if (padFlag)
                                {
                                    result++;
                                    car.BM = result.ToString().PadLeft(maxLenOfTopNodes, '0');
                                }
                                else
                                {
                                    num++;
                                    num3 = num + 1;
                                    car.BM = car.SJBM + num3.ToString().PadLeft(topNodeTable.Rows.Count.ToString().Length + 2, '0');
                                }
                            }
                            num++;
                        }
                        return;

                    case "BM_XHDW":
                        break;

                    default:
                        return;
                }
                num = 0;
                result = 0;
                foreach (DataRow row7 in topNodeTable.Rows)
                {
                    BMXHDWModel model7 = new BMXHDWModel();
                    BMXHDWManager manager7 = new BMXHDWManager();
                    model7.SJBM = GetSafeData.ValidateValue<string>(row7, "SJBM");
                    string str13 = GetSafeData.ValidateValue<string>(row7, "BM");
                    if (padFlag)
                    {
                        model7.BM = str13.PadLeft(maxLenOfTopNodes, '0');
                    }
                    else
                    {
                        num3 = num + 1;
                        model7.BM = model7.SJBM + num3.ToString().PadLeft(topNodeTable.Rows.Count.ToString().Length + 2, '0');
                    }
                    model7.MC = GetSafeData.ValidateValue<string>(row7, "MC");
                    model7.JM = GetSafeData.ValidateValue<string>(row7, "JM");
                    model7.SH = GetSafeData.ValidateValue<string>(row7, "SH");
                    model7.DZDH = GetSafeData.ValidateValue<string>(row7, "DZDH");
                    model7.YHZH = GetSafeData.ValidateValue<string>(row7, "YHZH");
                    model7.YZBM = GetSafeData.ValidateValue<string>(row7, "YZBM");
                    model7.WJ = GetSafeData.ValidateValue<int>(row7, "WJ");
                    if (!int.TryParse(str13, out result))
                    {
                        result++;
                    }
                    for (string str14 = manager7.UpdateSubNodesSJBM(model7, str13); "e1" == str14; str14 = manager7.UpdateSubNodesSJBM(model7, str13))
                    {
                        if (padFlag)
                        {
                            result++;
                            model7.BM = result.ToString().PadLeft(maxLenOfTopNodes, '0');
                        }
                        else
                        {
                            num++;
                            num3 = num + 1;
                            model7.BM = model7.SJBM + num3.ToString().PadLeft(topNodeTable.Rows.Count.ToString().Length + 2, '0');
                        }
                    }
                    num++;
                }
            }
            catch (Exception)
            {
            }
        }

        private void ReNode(string tableName, string SJBM)
        {
            try
            {
                int num;
                DataRow row2;
                DataRow row3;
                DataRow row4;
                DataRow row5;
                DataRow row6;
                DataRow row7;
                DataRow row8;
                DataRow[] rowArray = this.baseDAO.querySQLDataTable("SELECT * FROM " + tableName).Select("SJBM = '" + SJBM + "'");
                bool flag = false;
                foreach (DataRow row in rowArray)
                {
                    if (GetSafeData.ValidateValue<string>(row, "BM").Length > 0x10)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    goto Label_0CE4;
                }
                switch (tableName)
                {
                    case "BM_KH":
                        num = 0;
                        num = 0;
                        goto Label_0322;

                    case "BM_SP":
                        num = 0;
                        num = 0;
                        goto Label_056E;

                    case "BM_SFHR":
                        num = 0;
                        num = 0;
                        goto Label_06FC;

                    case "BM_FYXM":
                        num = 0;
                        num = 0;
                        goto Label_083E;

                    case "BM_GHDW":
                        num = 0;
                        num = 0;
                        goto Label_09CC;

                    case "BM_CL":
                        num = 0;
                        num = 0;
                        goto Label_0B47;

                    case "BM_XHDW":
                        num = 0;
                        num = 0;
                        goto Label_0CD5;

                    default:
                        return;
                }
            Label_0129:
                row2 = rowArray[num];
                BMKHModel customer = new BMKHModel();
                BMKHManager manager = new BMKHManager();
                string yuanBM = GetSafeData.ValidateValue<string>(row2, "BM");
                int num4 = num + 1;
                int length = rowArray.Length;
                customer.BM = SJBM + num4.ToString().PadLeft(length.ToString().Length + 1, '0');
                customer.MC = GetSafeData.ValidateValue<string>(row2, "MC");
                customer.JM = GetSafeData.ValidateValue<string>(row2, "JM");
                customer.KJM = GetSafeData.ValidateValue<string>(row2, "KJM");
                customer.SH = GetSafeData.ValidateValue<string>(row2, "SH");
                customer.DZDH = GetSafeData.ValidateValue<string>(row2, "DZDH");
                customer.YHZH = GetSafeData.ValidateValue<string>(row2, "YHZH");
                customer.YJDZ = GetSafeData.ValidateValue<string>(row2, "YJDZ");
                customer.BZ = GetSafeData.ValidateValue<string>(row2, "BZ");
                customer.YSKM = GetSafeData.ValidateValue<string>(row2, "YSKM");
                customer.DQBM = GetSafeData.ValidateValue<string>(row2, "DQBM");
                customer.DQMC = GetSafeData.ValidateValue<string>(row2, "DQMC");
                customer.DQKM = GetSafeData.ValidateValue<string>(row2, "DQKM");
                customer.SJBM = GetSafeData.ValidateValue<string>(row2, "SJBM");
                customer.SFZJY = GetSafeData.ValidateValue<bool>(row2, "SFZJY");
                customer.WJ = GetSafeData.ValidateValue<int>(row2, "WJ");
                string str3 = manager.UpdateSubNodesSJBM(customer, yuanBM);
                while ("e1" == str3)
                {
                    num++;
                    int num6 = num + 1;
                    int num7 = rowArray.Length;
                    customer.BM = SJBM + num6.ToString().PadLeft(num7.ToString().Length + 1, '0');
                    str3 = manager.UpdateSubNodesSJBM(customer, yuanBM);
                }
                if ("0" == str3)
                {
                    this.ReNode(tableName, customer.BM);
                }
                num++;
            Label_0322:
                if (num < rowArray.Length)
                {
                    goto Label_0129;
                }
                return;
            Label_033C:
                row3 = rowArray[num];
                BMSPModel merchandise = new BMSPModel();
                BMSPManager manager2 = new BMSPManager();
                string str4 = GetSafeData.ValidateValue<string>(row3, "BM");
                int num8 = num + 1;
                int num9 = rowArray.Length;
                merchandise.BM = SJBM + num8.ToString().PadLeft(num9.ToString().Length + 1, '0');
                merchandise.MC = GetSafeData.ValidateValue<string>(row3, "MC");
                merchandise.JM = GetSafeData.ValidateValue<string>(row3, "JM");
                merchandise.KJM = GetSafeData.ValidateValue<string>(row3, "KJM");
                merchandise.SJBM = GetSafeData.ValidateValue<string>(row3, "SJBM");
                merchandise.SLV = GetSafeData.ValidateValue<double>(row3, "SLV");
                merchandise.SPSM = GetSafeData.ValidateValue<string>(row3, "SPSM");
                merchandise.GGXH = GetSafeData.ValidateValue<string>(row3, "GGXH");
                merchandise.JLDW = GetSafeData.ValidateValue<string>(row3, "JLDW");
                merchandise.DJ = GetSafeData.ValidateValue<double>(row3, "DJ");
                merchandise.HSJBZ = GetSafeData.ValidateValue<bool>(row3, "HSJBZ");
                merchandise.XSSRKM = GetSafeData.ValidateValue<string>(row3, "XSSRKM");
                merchandise.YJZZSKM = GetSafeData.ValidateValue<string>(row3, "YJZZSKM");
                merchandise.XSTHKM = GetSafeData.ValidateValue<string>(row3, "XSTHKM");
                merchandise.HYSY = GetSafeData.ValidateValue<bool>(row3, "HYSY");
                merchandise.XTHASH = GetSafeData.ValidateValue<string>(row3, "XTHASH");
                merchandise.XTCODE = GetSafeData.ValidateValue<string>(row3, "XTCODE");
                merchandise.ISHIDE = GetSafeData.ValidateValue<string>(row3, "ISHIDE");
                merchandise.WJ = GetSafeData.ValidateValue<int>(row3, "WJ");
                string str5 = manager2.UpdateSubNodesSJBM(merchandise, str4);
                while ("e1" == str5)
                {
                    num++;
                    int num10 = num + 1;
                    int num11 = rowArray.Length;
                    merchandise.BM = SJBM + num10.ToString().PadLeft(num11.ToString().Length + 1, '0');
                    str5 = manager2.UpdateSubNodesSJBM(merchandise, str4);
                }
                if ("0" == str5)
                {
                    this.ReNode(tableName, merchandise.BM);
                }
                num++;
            Label_056E:
                if (num < rowArray.Length)
                {
                    goto Label_033C;
                }
                return;
            Label_0588:
                row4 = rowArray[num];
                BMSFHRModel model3 = new BMSFHRModel();
                BMSFHRManager manager3 = new BMSFHRManager();
                string str6 = GetSafeData.ValidateValue<string>(row4, "BM");
                int num12 = num + 1;
                int num13 = rowArray.Length;
                model3.BM = SJBM + num12.ToString().PadLeft(num13.ToString().Length + 1, '0');
                model3.MC = GetSafeData.ValidateValue<string>(row4, "MC");
                model3.JM = GetSafeData.ValidateValue<string>(row4, "JM");
                model3.SJBM = GetSafeData.ValidateValue<string>(row4, "SJBM");
                model3.SH = GetSafeData.ValidateValue<string>(row4, "SH");
                model3.DZDH = GetSafeData.ValidateValue<string>(row4, "DZDH");
                model3.YHZH = GetSafeData.ValidateValue<string>(row4, "YHZH");
                model3.YZBM = GetSafeData.ValidateValue<string>(row4, "YZBM");
                model3.WJ = GetSafeData.ValidateValue<int>(row4, "WJ");
                string str7 = manager3.UpdateSubNodesSJBM(model3, str6);
                while ("e1" == str7)
                {
                    num++;
                    int num14 = num + 1;
                    int num15 = rowArray.Length;
                    model3.BM = SJBM + num14.ToString().PadLeft(num15.ToString().Length + 1, '0');
                    str7 = manager3.UpdateSubNodesSJBM(model3, str6);
                }
                if ("0" == str7)
                {
                    this.ReNode(tableName, model3.BM);
                }
                num++;
            Label_06FC:
                if (num < rowArray.Length)
                {
                    goto Label_0588;
                }
                return;
            Label_0716:
                row5 = rowArray[num];
                BMFYXMModel feiyong = new BMFYXMModel();
                BMFYXMManager manager4 = new BMFYXMManager();
                string str8 = GetSafeData.ValidateValue<string>(row5, "BM");
                int num16 = num + 1;
                int num3 = rowArray.Length;
                feiyong.BM = SJBM + num16.ToString().PadLeft(num3.ToString().Length + 1, '0');
                feiyong.MC = GetSafeData.ValidateValue<string>(row5, "MC");
                feiyong.JM = GetSafeData.ValidateValue<string>(row5, "JM");
                feiyong.SJBM = GetSafeData.ValidateValue<string>(row5, "SJBM");
                feiyong.WJ = GetSafeData.ValidateValue<int>(row5, "WJ");
                string str9 = manager4.UpdateSubNodesSJBM(feiyong, str8);
                while ("e1" == str9)
                {
                    num++;
                    num3 = num + 1;
                    num3 = rowArray.Length;
                    string introduced65 = num3.ToString();
                    feiyong.BM = SJBM + introduced65.PadLeft(num3.ToString().Length + 1, '0');
                    str9 = manager4.UpdateSubNodesSJBM(feiyong, str8);
                }
                if ("0" == str9)
                {
                    this.ReNode(tableName, feiyong.BM);
                }
                num++;
            Label_083E:
                if (num < rowArray.Length)
                {
                    goto Label_0716;
                }
                return;
            Label_0858:
                row6 = rowArray[num];
                BMGHDWModel purchase = new BMGHDWModel();
                BMGHDWManager manager5 = new BMGHDWManager();
                string str10 = GetSafeData.ValidateValue<string>(row6, "BM");
                num3 = num + 1;
                num3 = rowArray.Length;
                string introduced66 = num3.ToString();
                purchase.BM = SJBM + introduced66.PadLeft(num3.ToString().Length + 1, '0');
                purchase.MC = GetSafeData.ValidateValue<string>(row6, "MC");
                purchase.JM = GetSafeData.ValidateValue<string>(row6, "JM");
                purchase.SH = GetSafeData.ValidateValue<string>(row6, "SH");
                purchase.DZDH = GetSafeData.ValidateValue<string>(row6, "DZDH");
                purchase.YHZH = GetSafeData.ValidateValue<string>(row6, "YHZH");
                purchase.IDCOC = GetSafeData.ValidateValue<string>(row6, "IDCOC");
                purchase.SJBM = GetSafeData.ValidateValue<string>(row6, "SJBM");
                purchase.WJ = GetSafeData.ValidateValue<int>(row6, "WJ");
                string str11 = manager5.UpdateSubNodesSJBM(purchase, str10);
                while ("e1" == str11)
                {
                    num++;
                    num3 = num + 1;
                    num3 = rowArray.Length;
                    string introduced67 = num3.ToString();
                    purchase.BM = SJBM + introduced67.PadLeft(num3.ToString().Length + 1, '0');
                    str11 = manager5.UpdateSubNodesSJBM(purchase, str10);
                }
                if ("0" == str11)
                {
                    this.ReNode(tableName, purchase.BM);
                }
                num++;
            Label_09CC:
                if (num < rowArray.Length)
                {
                    goto Label_0858;
                }
                return;
            Label_09E6:
                row7 = rowArray[num];
                BMCLModel car = new BMCLModel();
                BMCLManager manager6 = new BMCLManager();
                string str12 = GetSafeData.ValidateValue<string>(row7, "BM");
                num3 = num + 1;
                num3 = rowArray.Length;
                string introduced68 = num3.ToString();
                car.BM = SJBM + introduced68.PadLeft(num3.ToString().Length + 1, '0');
                car.MC = GetSafeData.ValidateValue<string>(row7, "MC");
                car.JM = GetSafeData.ValidateValue<string>(row7, "JM");
                car.SJBM = GetSafeData.ValidateValue<string>(row7, "SJBM");
                car.CPXH = GetSafeData.ValidateValue<string>(row7, "CPXH");
                car.CD = GetSafeData.ValidateValue<string>(row7, "CD");
                car.SCCJMC = GetSafeData.ValidateValue<string>(row7, "SCCJMC");
                car.WJ = GetSafeData.ValidateValue<int>(row7, "WJ");
                string str13 = manager6.UpdateSubNodesSJBM(car, str12);
                while ("e1" == str13)
                {
                    num++;
                    num3 = num + 1;
                    num3 = rowArray.Length;
                    string introduced69 = num3.ToString();
                    car.BM = SJBM + introduced69.PadLeft(num3.ToString().Length + 1, '0');
                    str13 = manager6.UpdateSubNodesSJBM(car, str12);
                }
                if ("0" == str13)
                {
                    this.ReNode(tableName, car.BM);
                }
                num++;
            Label_0B47:
                if (num < rowArray.Length)
                {
                    goto Label_09E6;
                }
                return;
            Label_0B61:
                row8 = rowArray[num];
                BMXHDWModel model7 = new BMXHDWModel();
                BMXHDWManager manager7 = new BMXHDWManager();
                string str14 = GetSafeData.ValidateValue<string>(row8, "BM");
                num3 = num + 1;
                num3 = rowArray.Length;
                string introduced70 = num3.ToString();
                model7.BM = SJBM + introduced70.PadLeft(num3.ToString().Length + 1, '0');
                model7.MC = GetSafeData.ValidateValue<string>(row8, "MC");
                model7.JM = GetSafeData.ValidateValue<string>(row8, "JM");
                model7.SJBM = GetSafeData.ValidateValue<string>(row8, "SJBM");
                model7.SH = GetSafeData.ValidateValue<string>(row8, "SH");
                model7.DZDH = GetSafeData.ValidateValue<string>(row8, "DZDH");
                model7.YHZH = GetSafeData.ValidateValue<string>(row8, "YHZH");
                model7.YZBM = GetSafeData.ValidateValue<string>(row8, "YZBM");
                model7.WJ = GetSafeData.ValidateValue<int>(row8, "WJ");
                string str15 = manager7.UpdateSubNodesSJBM(model7, str14);
                while ("e1" == str15)
                {
                    num++;
                    num3 = num + 1;
                    num3 = rowArray.Length;
                    string introduced71 = num3.ToString();
                    model7.BM = SJBM + introduced71.PadLeft(num3.ToString().Length + 1, '0');
                    str15 = manager7.UpdateSubNodesSJBM(model7, str14);
                }
                if ("0" == str15)
                {
                    this.ReNode(tableName, model7.BM);
                }
                num++;
            Label_0CD5:
                if (num < rowArray.Length)
                {
                    goto Label_0B61;
                }
                return;
            Label_0CE4:;
                foreach (DataRow row9 in this.baseDAO.querySQLDataTable("SELECT * FROM " + tableName + " WHERE SJBM ='" + SJBM + "'").Rows)
                {
                    string sJBM = GetSafeData.ValidateValue<string>(row9, "BM");
                    this.ReNode(tableName, sJBM);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}

