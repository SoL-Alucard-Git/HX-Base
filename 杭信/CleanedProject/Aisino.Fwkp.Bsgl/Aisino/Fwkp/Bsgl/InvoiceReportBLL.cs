namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.PrintGrid;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.FTaxBase;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    public class InvoiceReportBLL
    {
        private static ILog loger = LogUtil.GetLogger<InvoiceReportBLL>();
        private List<InvHistory> m_InvHistory;
        private List<InvHistory> m_InvHistoryHY = new List<InvHistory>();
        private List<InvHistory> m_InvHistoryJDC = new List<InvHistory>();
        private List<InvHistory> m_InvHistoryJSFP = new List<InvHistory>();
        private List<InvHistory> m_InvHistoryPT = new List<InvHistory>();
        private List<InvHistory> m_InvHistoryPTDZ = new List<InvHistory>();
        private List<InvHistory> m_InvHistoryZY = new List<InvHistory>();
        private InvoiceReportDAL m_invoiceReportDAL = new InvoiceReportDAL();
        private List<InvVolumeEntity> m_InvVolumeEntityList = new List<InvVolumeEntity>();
        private List<InvVolume> m_InvVolumeList = new List<InvVolume>();
        private TaxCard taxCard = TaxCardFactory.CreateTaxCard();
        private TaxStateInfo taxStatInfo = new TaxStateInfo();

        public InvoiceReportBLL()
        {
            this.taxStatInfo = this.taxCard.get_StateInfo();
            this.m_InvHistory = new List<InvHistory>();
        }

        public bool AddInvVolumeData()
        {
            try
            {
                return this.m_invoiceReportDAL.AddInvVolumeData(this.m_InvVolumeEntityList);
            }
            catch (Exception exception)
            {
                loger.Debug("exception:" + exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            return false;
        }

        public bool AddMonthReportTableData(int _nMonth)
        {
            try
            {
                return this.m_invoiceReportDAL.AddMonthReportTableData(this.m_InvVolumeList, _nMonth);
            }
            catch (Exception exception)
            {
                loger.Debug("exception:" + exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            return false;
        }

        public bool CheckIntegrity(int nMonth)
        {
            try
            {
                DateTime dtStart = Convert.ToDateTime(string.Concat(new object[] { this.taxCard.GetCardClock().Year.ToString(), "-", nMonth, "-01" }));
                DateTime dtEnd = dtStart;
                if (nMonth == 12)
                {
                    dtEnd = Convert.ToDateTime(((this.taxCard.GetCardClock().Year + 1)).ToString() + "-01-01");
                }
                else
                {
                    dtEnd = Convert.ToDateTime(this.taxCard.GetCardClock().Year.ToString() + "-" + ((nMonth + 1)).ToString() + "-01");
                }
                ArrayList taxStatData = this.m_invoiceReportDAL.GetTaxStatData(dtStart, dtEnd);
                if (taxStatData.Count <= 0)
                {
                    return false;
                }
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary = taxStatData[0] as Dictionary<string, object>;
                int num = 0;
                double num2 = 0.0;
                double num3 = 0.0;
                if (!dictionary.ContainsKey("TotalCount"))
                {
                    return false;
                }
                if (dictionary["TotalCount"].ToString() != "")
                {
                    num = Convert.ToInt32(dictionary["TotalCount"].ToString());
                }
                if (!dictionary.ContainsKey("TotalCash"))
                {
                    return false;
                }
                if (dictionary["TotalCash"].ToString() != "")
                {
                    num2 = Convert.ToDouble(dictionary["TotalCash"].ToString());
                }
                if (!dictionary.ContainsKey("TotalTax"))
                {
                    return false;
                }
                if (dictionary["TotalTax"].ToString() != "")
                {
                    num3 = Convert.ToDouble(dictionary["TotalTax"].ToString());
                }
                int num4 = Convert.ToInt32(this.taxCard.GetInvCount(nMonth));
                double num5 = 0.0;
                double num6 = 0.0;
                TaxStatisData data = this.taxCard.GetMonthStatistics(this.taxCard.GetCardClock().Year, nMonth, 0);
                InvAmountTaxStati stati = data.InvTypeStatData(0);
                num5 += (stati.get_Total().SJXSJE + stati.get_Total().XXZFJE) - stati.get_Total().XXFFJE;
                num6 += (stati.get_Total().SJXXSE + stati.get_Total().XXZFSE) - stati.get_Total().XXFFSE;
                stati = data.InvTypeStatData(2);
                num5 += (stati.get_Total().SJXSJE + stati.get_Total().XXZFJE) - stati.get_Total().XXFFJE;
                num6 += (stati.get_Total().SJXXSE + stati.get_Total().XXZFSE) - stati.get_Total().XXFFSE;
                stati = data.InvTypeStatData(11);
                num5 += (stati.get_Total().SJXSJE + stati.get_Total().XXZFJE) - stati.get_Total().XXFFJE;
                num6 += (stati.get_Total().SJXXSE + stati.get_Total().XXZFSE) - stati.get_Total().XXFFSE;
                stati = data.InvTypeStatData(12);
                num5 += (stati.get_Total().SJXSJE + stati.get_Total().XXZFJE) - stati.get_Total().XXFFJE;
                num6 += (stati.get_Total().SJXXSE + stati.get_Total().XXZFSE) - stati.get_Total().XXFFSE;
                num5 = Math.Round(num5, 2);
                num2 = Math.Round(num2, 2);
                num6 = Math.Round(num6, 2);
                num3 = Math.Round(num3, 2);
                if (num != num4)
                {
                    return false;
                }
                if (num2 != num5)
                {
                    return false;
                }
                if (num3 != num6)
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
                return false;
            }
            return true;
        }

        public DataTable CreateDataTable(int _nYear, int _nMonth)
        {
            DataTable table = new DataTable();
            try
            {
                table.Columns.Add("发票种类");
                table.Columns.Add("类别代码");
                table.Columns.Add("期初库存份数");
                table.Columns.Add("期初库存号码");
                table.Columns.Add("本期领购份数");
                table.Columns.Add("本期领购号码");
                table.Columns.Add("本期开具份数");
                table.Columns.Add("本期开具号码");
                table.Columns.Add("作废丢失份数");
                table.Columns.Add("作废丢失号码");
                table.Columns.Add("误售退回份数");
                table.Columns.Add("误售退回号码");
                table.Columns.Add("期末库存份数");
                table.Columns.Add("期末库存号码");
                List<InvVolume> list = this.GetInvStockMonthStat_(_nYear, _nMonth);
                if (list == null)
                {
                    return null;
                }
                for (int i = 0; i < list.Count; i++)
                {
                    string str = "";
                    if (list[i].InvType == null)
                    {
                        str = "专用发票";
                    }
                    else if (list[i].InvType == 1)
                    {
                        str = "废旧物资发票";
                    }
                    else if (list[i].InvType == 2)
                    {
                        str = "普通发票";
                    }
                    else if (list[i].InvType == 11)
                    {
                        str = "货物运输业增值税专用发票";
                    }
                    else if (list[i].InvType == 12)
                    {
                        str = "机动车销售统一发票";
                    }
                    else if (list[i].InvType == 0x33)
                    {
                        str = "电子增值税普通发票";
                    }
                    else if (list[i].InvType == 0x29)
                    {
                        str = "增值税普通发票(卷票)";
                    }
                    else
                    {
                        str = "未知类型发票";
                    }
                    object[] values = new object[] { str, list[i].TypeCode, list[i].PrdEarlyStockNum.ToString(), list[i].PrdEarlyStockNO, list[i].PrdThisBuyNum.ToString(), list[i].PrdThisBuyNO, list[i].PrdThisIssueNum.ToString(), list[i].PrdThisIssueNO, list[i].WasteNum.ToString(), list[i].WasteNO, list[i].MistakeNum.ToString(), list[i].MistakeNO, list[i].PrdEndStockNum.ToString(), list[i].PrdEndStockNO };
                    table.Rows.Add(values);
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            return table;
        }

        public bool DeleteInvVolumeData()
        {
            try
            {
                return this.m_invoiceReportDAL.DeleteInvVolumeData();
            }
            catch (Exception exception)
            {
                loger.Debug("exception:" + exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            return false;
        }

        public bool DeleteMonthReportTable()
        {
            try
            {
                return this.m_invoiceReportDAL.DeleteMonthReportTable();
            }
            catch (Exception exception)
            {
                loger.Debug("exception:" + exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            return false;
        }

        public DateTime GetDateTime()
        {
            return this.taxCard.GetCardClock();
        }

        private List<InvVolume> GetInvStockMonthStat_(int _nYear, int _nMonth)
        {
            List<InvVolume> list = new List<InvVolume>();
            try
            {
                List<InvVolumeApp> invStock = this.taxCard.GetInvStock();
                if (invStock == null)
                {
                    loger.Debug("taxCard.InvoiceStock:Failed");
                    return null;
                }
                List<InvVolumeApp> list3 = new List<InvVolumeApp>();
                List<InvVolumeApp> list4 = new List<InvVolumeApp>();
                List<InvVolumeApp> list5 = new List<InvVolumeApp>();
                List<InvVolumeApp> list6 = new List<InvVolumeApp>();
                List<InvVolumeApp> list7 = new List<InvVolumeApp>();
                List<InvVolumeApp> list8 = new List<InvVolumeApp>();
                foreach (InvVolumeApp app in invStock)
                {
                    switch (app.InvType)
                    {
                        case 0:
                            list3.Add(app);
                            break;

                        case 2:
                            list4.Add(app);
                            break;

                        case 11:
                            list5.Add(app);
                            break;

                        case 12:
                            list6.Add(app);
                            break;

                        case 0x29:
                            list8.Add(app);
                            break;

                        case 0x33:
                            list7.Add(app);
                            break;
                    }
                }
                int num = Convert.ToInt32(this.taxCard.GetInvCount(_nYear, _nMonth));
                for (int i = 0; i < 6; i++)
                {
                    Dictionary<string, InvVolume> dictionary = new Dictionary<string, InvVolume>();
                    List<InvVolumeApp> list9 = new List<InvVolumeApp>();
                    switch (i)
                    {
                        case 0:
                            list9 = list3;
                            break;

                        case 1:
                            list9 = list4;
                            break;

                        case 2:
                            list9 = list5;
                            break;

                        case 3:
                            list9 = list6;
                            break;

                        case 4:
                            list9 = list7;
                            break;

                        case 5:
                            list9 = list8;
                            break;
                    }
                    if (list9.Count != 0)
                    {
                        string key = "";
                        string s = "";
                        string typeCode = "";
                        int num3 = 0;
                        int num4 = 0;
                        int num5 = 0;
                        int num6 = 0;
                        int num7 = 0;
                        int num8 = 0;
                        int num9 = 0;
                        int num10 = 0;
                        int num11 = num;
                        while (num9 < list9.Count)
                        {
                            int num14;
                            InvDetail detail2;
                            s = list9[num9].InvType.ToString();
                            typeCode = list9[num9].TypeCode;
                            DateTime time = new DateTime(list9[num9].BuyDate.Year, list9[num9].BuyDate.Month, 1);
                            DateTime time2 = new DateTime(_nYear, _nMonth, 1);
                            InvVolume volume = new InvVolume {
                                InvType = (InvoiceType) list9[num9].InvType,
                                TypeCode = typeCode
                            };
                            if (list9[num9].Status.ToString().Equals("B"))
                            {
                                if (DateTime.Compare(time, time2) < 0)
                                {
                                    list9[num9].Status = 'E';
                                }
                                else if (DateTime.Compare(time, time2) > 0)
                                {
                                    num9++;
                                    continue;
                                }
                            }
                            if ((!list9[num9].Status.ToString().Equals("B") && !list9[num9].Status.ToString().Equals("C")) && !list9[num9].Status.ToString().Equals("D"))
                            {
                                volume.HeadCode = list9[num9].HeadCode;
                            }
                            uint num12 = 0;
                            if (((list9[num9].BuyNumber.ToString().Trim() != "") && (_nYear == time.Year)) && (_nMonth == time.Month))
                            {
                                num12 = Convert.ToUInt32(list9[num9].BuyNumber);
                            }
                            if (!list9[num9].Status.ToString().Equals("B"))
                            {
                                volume.EndCode = list9[num9].HeadCode + num12;
                            }
                            key = s + typeCode + "0" + num10.ToString("00") + list9[num9].HeadCode.ToString("D8");
                            if ((!list9[num9].Status.ToString().Equals("B") && !list9[num9].Status.ToString().Equals("C")) && !list9[num9].Status.ToString().Equals("D"))
                            {
                                if (DateTime.Compare(time, time2) < 0)
                                {
                                    volume.PrdThisBuyNum = 0;
                                    volume.PrdThisBuyNO = "";
                                }
                                else if (DateTime.Compare(time, time2) == 0)
                                {
                                    volume.PrdThisBuyNum = list9[num9].BuyNumber;
                                    volume.PrdThisBuyNO = ((list9[num9].HeadCode - Convert.ToUInt32(list9[num9].BuyNumber)) + Convert.ToUInt32(list9[num9].Number)).ToString("D8");
                                }
                                else if (DateTime.Compare(time, time2) > 0)
                                {
                                    num9++;
                                    continue;
                                }
                                if (s.Equals("0"))
                                {
                                    this.m_InvHistory = this.m_InvHistoryZY;
                                }
                                else if (s.Equals("2"))
                                {
                                    this.m_InvHistory = this.m_InvHistoryPT;
                                }
                                else if (s.Equals("11"))
                                {
                                    this.m_InvHistory = this.m_InvHistoryHY;
                                }
                                else if (s.Equals("12"))
                                {
                                    this.m_InvHistory = this.m_InvHistoryJDC;
                                }
                                else
                                {
                                    int num21 = 0x33;
                                    if (s.Equals(num21.ToString()))
                                    {
                                        this.m_InvHistory = this.m_InvHistoryPTDZ;
                                    }
                                }
                                if ((_nYear == this.GetDateTime().Year) && (_nMonth == this.GetDateTime().Month))
                                {
                                    volume.PrdEndStockNum = list9[num9].Number;
                                    volume.PrdEndStockNO = list9[num9].HeadCode.ToString("D8");
                                }
                                else if (this.m_InvHistory.Count > 0)
                                {
                                    int num13 = this.m_InvHistory.Count / (list9.Count + 1);
                                    volume.PrdEndStockNum = this.m_InvHistory[((list9.Count + 1) * (num13 - 1)) + num9].m_nInvCount;
                                    volume.PrdEndStockNO = this.m_InvHistory[((list9.Count + 1) * (num13 - 1)) + num9].m_strInvCode;
                                }
                                else
                                {
                                    volume.PrdEndStockNum = 0;
                                    volume.PrdEndStockNO = "";
                                }
                                num14 = 0;
                                if (!this.taxCard.get_IsLargeStorage())
                                {
                                    goto Label_08D8;
                                }
                                int invStockCount = this.taxCard.GetInvStockCount(_nYear, _nMonth);
                                int num16 = 0;
                                int num17 = 0;
                                bool flag = true;
                                while (flag)
                                {
                                    if ((invStockCount < 30) || (num17 >= invStockCount))
                                    {
                                        num17 = invStockCount - 1;
                                        flag = false;
                                    }
                                    else
                                    {
                                        num17 = 0x1d;
                                    }
                                    num11 = this.taxCard.GetStockInvCount(_nYear, _nMonth, num16, num17);
                                    num14 = 0;
                                    while (num11-- > 0)
                                    {
                                        InvDetail invDetail = this.taxCard.GetInvDetail((long) num14++);
                                        if (((invDetail.InvType.ToString() == s) && invDetail.TypeCode.Equals(list9[num9].TypeCode)) && ((invDetail.Date.Year == _nYear) && (invDetail.Date.Month == _nMonth)))
                                        {
                                            if (invDetail.CancelFlag)
                                            {
                                                volume.WasteNum++;
                                                volume.WasteNO = volume.WasteNO + invDetail.InvNo.ToString("D8") + " ";
                                            }
                                            volume.PrdThisIssueNum++;
                                            if (volume.PrdThisIssueNO == "")
                                            {
                                                volume.PrdThisIssueNO = volume.PrdThisIssueNO + invDetail.InvNo.ToString("D8");
                                            }
                                        }
                                    }
                                    num16 += 30;
                                    num17 += 30;
                                }
                            }
                            goto Label_08E5;
                        Label_07E3:
                            detail2 = this.taxCard.GetInvDetail((long) num14++);
                            if (((detail2.InvType.ToString() == s) && detail2.TypeCode.Equals(list9[num9].TypeCode)) && ((detail2.Date.Year == _nYear) && (detail2.Date.Month == _nMonth)))
                            {
                                if (detail2.CancelFlag)
                                {
                                    volume.WasteNum++;
                                    volume.WasteNO = volume.WasteNO + detail2.InvNo.ToString("D8") + " ";
                                }
                                volume.PrdThisIssueNum++;
                                if (volume.PrdThisIssueNO == "")
                                {
                                    volume.PrdThisIssueNO = volume.PrdThisIssueNO + detail2.InvNo.ToString("D8");
                                }
                            }
                        Label_08D8:
                            if (num11-- > 0)
                            {
                                goto Label_07E3;
                            }
                        Label_08E5:
                            if ((list9[num9].Status.ToString().Equals("B") || list9[num9].Status.ToString().Equals("C")) || list9[num9].Status.ToString().Equals("D"))
                            {
                                volume.MistakeNum = list9[num9].Number;
                                volume.MistakeNO = volume.MistakeNO + ((list9[num9].HeadCode - (list9[num9].BuyNumber - list9[num9].BuyNumber))).ToString("D8") + " ";
                            }
                            if ((!list9[num9].Status.ToString().Equals("B") && !list9[num9].Status.ToString().Equals("C")) && !list9[num9].Status.ToString().Equals("D"))
                            {
                                volume.PrdEarlyStockNum = ((volume.PrdEndStockNum - volume.PrdThisBuyNum) + volume.PrdThisIssueNum) + volume.MistakeNum;
                                if (volume.PrdEarlyStockNum != 0)
                                {
                                    if (!string.IsNullOrEmpty(volume.PrdEndStockNO))
                                    {
                                        volume.PrdEarlyStockNO = (Convert.ToInt32(volume.PrdEndStockNO) - (volume.PrdThisIssueNum + volume.MistakeNum)).ToString("D8");
                                    }
                                }
                                else
                                {
                                    volume.PrdEarlyStockNO = "";
                                }
                                if (volume.PrdEarlyStockNum < 0)
                                {
                                    volume.PrdThisIssueNum = -volume.PrdEarlyStockNum;
                                    volume.PrdEarlyStockNum = 0;
                                }
                            }
                            num3 += volume.PrdEarlyStockNum;
                            num4 += volume.PrdThisBuyNum;
                            num5 += volume.PrdThisIssueNum;
                            num6 += volume.WasteNum;
                            num7 += volume.MistakeNum;
                            num8 += volume.PrdEndStockNum;
                            if (!dictionary.ContainsKey(key))
                            {
                                dictionary.Add(key, volume);
                            }
                            num9++;
                            num10++;
                        }
                        if (list9.Count > 0)
                        {
                            InvVolume volume2 = new InvVolume();
                            num10 = 0;
                            volume2.InvType = (InvoiceType) int.Parse(s);
                            volume2.TypeCode = "小计";
                            key = s + typeCode + "1" + num10.ToString("00");
                            volume2.PrdEarlyStockNum = num3;
                            volume2.PrdThisBuyNum = num4;
                            volume2.PrdThisIssueNum = num5;
                            volume2.WasteNum = num6;
                            volume2.MistakeNum = num7;
                            volume2.PrdEndStockNum = num8;
                            volume2.PrdEarlyStockNO = "";
                            volume2.PrdThisBuyNO = "";
                            volume2.PrdThisIssueNO = "";
                            volume2.MistakeNO = "";
                            volume2.WasteNO = "";
                            volume2.PrdEndStockNO = "";
                            if (!dictionary.ContainsKey(key))
                            {
                                dictionary.Add(key, volume2);
                            }
                        }
                        foreach (KeyValuePair<string, InvVolume> pair in dictionary)
                        {
                            InvVolume item = pair.Value;
                            list.Add(item);
                            if (pair.Value.InvType == null)
                            {
                                InvHistory history = new InvHistory {
                                    m_nInvCount = item.PrdEarlyStockNum,
                                    m_strInvCode = item.PrdEarlyStockNO
                                };
                                this.m_InvHistoryZY.Add(history);
                            }
                            if (pair.Value.InvType == 2)
                            {
                                InvHistory history2 = new InvHistory {
                                    m_nInvCount = item.PrdEarlyStockNum,
                                    m_strInvCode = item.PrdEarlyStockNO
                                };
                                this.m_InvHistoryPT.Add(history2);
                            }
                            if (pair.Value.InvType == 11)
                            {
                                InvHistory history3 = new InvHistory {
                                    m_nInvCount = item.PrdEarlyStockNum,
                                    m_strInvCode = item.PrdEarlyStockNO
                                };
                                this.m_InvHistoryHY.Add(history3);
                            }
                            if (pair.Value.InvType == 12)
                            {
                                InvHistory history4 = new InvHistory {
                                    m_nInvCount = item.PrdEarlyStockNum,
                                    m_strInvCode = item.PrdEarlyStockNO
                                };
                                this.m_InvHistoryJDC.Add(history4);
                            }
                            if (pair.Value.InvType == 0x33)
                            {
                                InvHistory history5 = new InvHistory {
                                    m_nInvCount = item.PrdEarlyStockNum,
                                    m_strInvCode = item.PrdEarlyStockNO
                                };
                                this.m_InvHistoryPTDZ.Add(history5);
                            }
                            if (pair.Value.InvType == 0x29)
                            {
                                InvHistory history6 = new InvHistory {
                                    m_nInvCount = item.PrdEarlyStockNum,
                                    m_strInvCode = item.PrdEarlyStockNO
                                };
                                this.m_InvHistoryJSFP.Add(history6);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list;
        }

        public int GetLastRepDateMonth()
        {
            return this.taxCard.get_LastRepDateMonth();
        }

        public int GetLastRepDateYear()
        {
            return this.taxCard.get_LastRepDateYear();
        }

        private int GetLastRepPeroid(int invType)
        {
            int num = 0;
            List<int> periodCount = new List<int>();
            try
            {
                periodCount = this.taxCard.GetPeriodCount(invType);
                if (periodCount.Count > 1)
                {
                    num = periodCount[0];
                }
            }
            catch (Exception exception)
            {
                loger.Debug("exception:" + exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            return num;
        }

        public List<int> GetMonthCollect(int nAccYear)
        {
            List<int> monthStatPeriod = new List<int>();
            try
            {
                monthStatPeriod = this.taxCard.GetMonthStatPeriod(nAccYear);
                if ((nAccYear == this.taxCard.get_SysYear()) && (monthStatPeriod.Count == 0))
                {
                    monthStatPeriod.Add(this.taxCard.get_SysMonth());
                }
            }
            catch (Exception exception)
            {
                loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            return monthStatPeriod;
        }

        public DateTime GetStartDate()
        {
            return this.taxCard.GetCardClock().AddMonths(-13);
        }

        public bool GetTaxcardVersion()
        {
            if (this.taxCard.get_ECardType() == null)
            {
                loger.Debug("old card");
                return false;
            }
            loger.Debug("new card");
            return true;
        }

        public bool HasInvData()
        {
            if (this.taxStatInfo.TBBuyInv == 0)
            {
                return false;
            }
            loger.Debug("HasInvData");
            return true;
        }

        public bool HasReturnInv()
        {
            if (this.taxStatInfo.ICRetInv == 0)
            {
                return false;
            }
            loger.Debug("HasReturnInv");
            return true;
        }

        public bool InvoiceRepair(int nMonth)
        {
            object[] objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.Fpkj.FaPiaoXiuFu", new object[] { nMonth });
            if (objArray == null)
            {
                return false;
            }
            if (objArray.Length != 1)
            {
                return false;
            }
            return Convert.ToBoolean(objArray[0]);
        }

        public bool IsBranchMachine()
        {
            if (this.taxStatInfo.IsMainMachine == 0)
            {
                loger.Debug("branch");
                return true;
            }
            loger.Debug("main");
            return false;
        }

        public bool IsLocked()
        {
            if (this.taxStatInfo.IsLockReached == 0)
            {
                return false;
            }
            loger.Debug("locked");
            return true;
        }

        public void MakeTable(ref CustomStyleDataGrid dataGridView, int nYear, int nMonth)
        {
            try
            {
                dataGridView.ReadOnly = true;
                if (dataGridView.Rows.Count > 0)
                {
                    int count = dataGridView.Rows.Count;
                    while (count-- > 0)
                    {
                        dataGridView.Rows.RemoveAt(0);
                    }
                }
                DataTable table = new DataTable();
                table.Columns.Add("发票种类");
                table.Columns.Add("类别代码");
                table.Columns.Add("期初库存份数");
                table.Columns.Add("期初库存号码");
                table.Columns.Add("本期领购份数");
                table.Columns.Add("本期领购号码");
                table.Columns.Add("本期开具份数");
                table.Columns.Add("本期开具号码");
                table.Columns.Add("作废丢失份数");
                table.Columns.Add("作废丢失号码");
                table.Columns.Add("误售退回份数");
                table.Columns.Add("误售退回号码");
                table.Columns.Add("期末库存份数");
                table.Columns.Add("期末库存号码");
                List<InvVolume> invStockMonthStat = this.taxCard.GetInvStockMonthStat(nYear, nMonth);
                dataGridView.AllowUserToAddRows = false;
                for (int i = 0; i < invStockMonthStat.Count; i++)
                {
                    string str = "";
                    if (invStockMonthStat[i].InvType == null)
                    {
                        str = "专用发票";
                    }
                    else if (invStockMonthStat[i].InvType == 2)
                    {
                        str = "普通发票";
                    }
                    else if (invStockMonthStat[i].InvType == 11)
                    {
                        str = "货物运输业增值税专用发票";
                    }
                    else if (invStockMonthStat[i].InvType == 12)
                    {
                        str = "机动车销售统一发票";
                    }
                    object[] values = new object[] { str, invStockMonthStat[i].TypeCode, invStockMonthStat[i].PrdEarlyStockNum.ToString(), invStockMonthStat[i].PrdEarlyStockNO, invStockMonthStat[i].PrdThisBuyNum.ToString(), invStockMonthStat[i].PrdThisBuyNO, invStockMonthStat[i].PrdThisIssueNum.ToString(), invStockMonthStat[i].PrdThisIssueNO, invStockMonthStat[i].WasteNum.ToString(), invStockMonthStat[i].WasteNO, invStockMonthStat[i].MistakeNum.ToString(), invStockMonthStat[i].MistakeNO, invStockMonthStat[i].PrdEndStockNum.ToString(), invStockMonthStat[i].PrdEndStockNO };
                    table.Rows.Add(values);
                }
                dataGridView.DataSource = table;
            }
            catch (Exception exception)
            {
                loger.Debug("exception:" + exception.Message);
                ExceptionHandler.HandleError(exception);
            }
        }

        public bool PrintTable(ref CustomStyleDataGrid dataGridView, string strTitle, List<PrinterItems> _PIHead, List<PrinterItems> _PIFoot)
        {
            try
            {
                return DataGridPrintTools.Print(dataGridView, dataGridView.Parent, strTitle, _PIHead, _PIFoot, true);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
                return false;
            }
        }

        private class InvHistory
        {
            public int m_nInvCount = 0;
            public string m_strInvCode = "";
        }
    }
}

