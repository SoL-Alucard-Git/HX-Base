namespace Aisino.Fwkp.Bsgl
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.PrintGrid;
    using Aisino.Framework.Plugin.Core.Util;
    using Aisino.FTaxBase;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;

    public class InvStatBLL
    {
        public bool bHasChild;
        public bool bIsMainMachine;
        private ILog loger = LogUtil.GetLogger<InvStatBLL>();
        private List<CInvStatData> m_CInvStatDataList = new List<CInvStatData>();
        private TaxCard taxCard = TaxCardFactory.CreateTaxCard();
        private TaxStatisData taxMonthStatDataCXDY;

        public InvStatBLL()
        {
            this.GetMachineState();
        }

        public void CreateMonthlyDataGrid(ref CustomStyleDataGrid dataGridView, int nYear, int nMonth, int nPeriod, INV_TYPE InvType)
        {
            try
            {
                dataGridView.ReadOnly = true;
                dataGridView.AllowUserToAddRows = false;
                DataTable table = new DataTable();
                table.Columns.Add("项目名称");
                table.Columns.Add("合计");
                table.Columns.Add("17%");
                table.Columns.Add("13%");
                table.Columns.Add("6%");
                table.Columns.Add("4%");
                table.Columns.Add("其他");
                TaxStatisData data = this.taxCard.GetMonthStatistics(nYear, nMonth, nPeriod);
                if (data != null)
                {
                    List<string> item = new List<string>();
                    List<List<string>> list2 = new List<List<string>>();
                    List<string> list3 = new List<string> { "销项正废金额", "销项正数金额", "销项负废金额", "销项负数金额", "实际销售金额", "销项正废税额", "销项正数税额", "销项负废税额", "销项负数税额", "实际销项税额" };
                    for (int i = 0; i < data.get_Count(); i++)
                    {
                        InvAmountTaxStati stati = data.get_Item(i);
                        if (stati == null)
                        {
                            return;
                        }
                        if ((stati.get_InvTypeStr().Length > 0) && (stati.get_InvTypeStr().Trim() == InvTypeEntity.GetInvName(InvType)))
                        {
                            item.Add(stati.get_Total().XXZFJE.ToString("0.00"));
                            item.Add(stati.get_TaxClass17().XXZFJE.ToString("0.00"));
                            item.Add(stati.get_TaxClass13().XXZFJE.ToString("0.00"));
                            item.Add(stati.get_TaxClass6().XXZFJE.ToString("0.00"));
                            item.Add(stati.get_TaxClass4().XXZFJE.ToString("0.00"));
                            item.Add(stati.get_TaxClassOther().XXZFJE.ToString("0.00"));
                            list2.Add(item);
                            item = new List<string> {
                                stati.get_Total().XXZSJE.ToString("0.00"),
                                stati.get_TaxClass17().XXZSJE.ToString("0.00"),
                                stati.get_TaxClass13().XXZSJE.ToString("0.00"),
                                stati.get_TaxClass6().XXZSJE.ToString("0.00"),
                                stati.get_TaxClass4().XXZSJE.ToString("0.00"),
                                stati.get_TaxClassOther().XXZSJE.ToString("0.00"),
                                item
                            };
                            item = new List<string> {
                                stati.get_Total().XXFFJE.ToString("0.00"),
                                stati.get_TaxClass17().XXFFJE.ToString("0.00"),
                                stati.get_TaxClass13().XXFFJE.ToString("0.00"),
                                stati.get_TaxClass6().XXFFJE.ToString("0.00"),
                                stati.get_TaxClass4().XXFFJE.ToString("0.00"),
                                stati.get_TaxClassOther().XXFFJE.ToString("0.00"),
                                item
                            };
                            item = new List<string> {
                                stati.get_Total().XXFSJE.ToString("0.00"),
                                stati.get_TaxClass17().XXFSJE.ToString("0.00"),
                                stati.get_TaxClass13().XXFSJE.ToString("0.00"),
                                stati.get_TaxClass6().XXFSJE.ToString("0.00"),
                                stati.get_TaxClass4().XXFSJE.ToString("0.00"),
                                stati.get_TaxClassOther().XXFSJE.ToString("0.00"),
                                item
                            };
                            item = new List<string> {
                                stati.get_Total().SJXSJE.ToString("0.00"),
                                stati.get_TaxClass17().SJXSJE.ToString("0.00"),
                                stati.get_TaxClass13().SJXSJE.ToString("0.00"),
                                stati.get_TaxClass6().SJXSJE.ToString("0.00"),
                                stati.get_TaxClass4().SJXSJE.ToString("0.00"),
                                stati.get_TaxClassOther().SJXSJE.ToString("0.00"),
                                item
                            };
                            item = new List<string> {
                                stati.get_Total().XXZFSE.ToString("0.00"),
                                stati.get_TaxClass17().XXZFSE.ToString("0.00"),
                                stati.get_TaxClass13().XXZFSE.ToString("0.00"),
                                stati.get_TaxClass6().XXZFSE.ToString("0.00"),
                                stati.get_TaxClass4().XXZFSE.ToString("0.00"),
                                stati.get_TaxClassOther().XXZFSE.ToString("0.00"),
                                item
                            };
                            item = new List<string> {
                                stati.get_Total().XXZSSE.ToString("0.00"),
                                stati.get_TaxClass17().XXZSSE.ToString("0.00"),
                                stati.get_TaxClass13().XXZSSE.ToString("0.00"),
                                stati.get_TaxClass6().XXZSSE.ToString("0.00"),
                                stati.get_TaxClass4().XXZSSE.ToString("0.00"),
                                stati.get_TaxClassOther().XXZSSE.ToString("0.00"),
                                item
                            };
                            item = new List<string> {
                                stati.get_Total().XXFFSE.ToString("0.00"),
                                stati.get_TaxClass17().XXFFSE.ToString("0.00"),
                                stati.get_TaxClass13().XXFFSE.ToString("0.00"),
                                stati.get_TaxClass6().XXFFSE.ToString("0.00"),
                                stati.get_TaxClass4().XXFFSE.ToString("0.00"),
                                stati.get_TaxClassOther().XXFFSE.ToString("0.00"),
                                item
                            };
                            item = new List<string> {
                                stati.get_Total().XXFSSE.ToString("0.00"),
                                stati.get_TaxClass17().XXFSSE.ToString("0.00"),
                                stati.get_TaxClass13().XXFSSE.ToString("0.00"),
                                stati.get_TaxClass6().XXFSSE.ToString("0.00"),
                                stati.get_TaxClass4().XXFSSE.ToString("0.00"),
                                stati.get_TaxClassOther().XXFSSE.ToString("0.00"),
                                item
                            };
                            item = new List<string> {
                                stati.get_Total().SJXXSE.ToString("0.00"),
                                stati.get_TaxClass17().SJXXSE.ToString("0.00"),
                                stati.get_TaxClass13().SJXXSE.ToString("0.00"),
                                stati.get_TaxClass6().SJXXSE.ToString("0.00"),
                                stati.get_TaxClass4().SJXXSE.ToString("0.00"),
                                stati.get_TaxClassOther().SJXXSE.ToString("0.00"),
                                item
                            };
                        }
                    }
                    if (dataGridView.Rows.Count > 0)
                    {
                        int count = dataGridView.Rows.Count;
                        while (count-- > 0)
                        {
                            dataGridView.Rows.RemoveAt(0);
                        }
                    }
                    if (list2.Count != list3.Count)
                    {
                        MessageManager.ShowMsgBox("INP-253107", new string[] { "资料统计出错" });
                    }
                    else
                    {
                        for (int j = 0; j < list3.Count; j++)
                        {
                            List<object> list4 = new List<object> {
                                list3[j]
                            };
                            object[] objArray = list2[j].ToArray();
                            for (int m = 1; m < table.Columns.Count; m++)
                            {
                                list4.Add(objArray[m - 1]);
                            }
                            object[] values = list4.ToArray();
                            table.Rows.Add(values);
                        }
                        dataGridView.DataSource = table;
                        for (int k = 0; k < dataGridView.Columns.Count; k++)
                        {
                            dataGridView.Columns[k].SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
        }

        public void CreateMonthlyDataGridCXDY(ref CustomStyleDataGrid dataGridView, int nYear, int nMonth, int nPeriod, INV_TYPE InvType)
        {
            try
            {
                dataGridView.ReadOnly = true;
                dataGridView.AllowUserToAddRows = false;
                DataTable table = new DataTable();
                string[] strArray = new string[] { "项目名称", "合计", "17%", "13%", "6%", "4%", "其他" };
                int num = 0;
                table.Columns.Add(strArray[num++], typeof(string));
                table.Columns.Add(strArray[num++], typeof(string));
                table.Columns.Add(strArray[num++], typeof(string));
                table.Columns.Add(strArray[num++], typeof(string));
                table.Columns.Add(strArray[num++], typeof(string));
                table.Columns.Add(strArray[num++], typeof(string));
                table.Columns.Add(strArray[num++], typeof(string));
                if (this.taxMonthStatDataCXDY != null)
                {
                    List<string> item = new List<string>();
                    List<List<string>> list2 = new List<List<string>>();
                    List<string> list3 = new List<string> { "销项正废金额", "销项正数金额", "销项负废金额", "销项负数金额", "实际销售金额", "销项正废税额", "销项正数税额", "销项负废税额", "销项负数税额", "实际销项税额" };
                    for (int i = 0; i < this.taxMonthStatDataCXDY.get_Count(); i++)
                    {
                        InvAmountTaxStati stati = this.taxMonthStatDataCXDY.get_Item(i);
                        if (stati == null)
                        {
                            return;
                        }
                        if ((stati.get_InvTypeStr().Length > 0) && (stati.get_InvTypeStr().Trim() == InvTypeEntity.GetInvName(InvType)))
                        {
                            item.Add(stati.get_Total().XXZFJE.ToString("0.00"));
                            item.Add(stati.get_TaxClass17().XXZFJE.ToString("0.00"));
                            item.Add(stati.get_TaxClass13().XXZFJE.ToString("0.00"));
                            item.Add(stati.get_TaxClass6().XXZFJE.ToString("0.00"));
                            item.Add(stati.get_TaxClass4().XXZFJE.ToString("0.00"));
                            item.Add(stati.get_TaxClassOther().XXZFJE.ToString("0.00"));
                            list2.Add(item);
                            item = new List<string> {
                                stati.get_Total().XXZSJE.ToString("0.00"),
                                stati.get_TaxClass17().XXZSJE.ToString("0.00"),
                                stati.get_TaxClass13().XXZSJE.ToString("0.00"),
                                stati.get_TaxClass6().XXZSJE.ToString("0.00"),
                                stati.get_TaxClass4().XXZSJE.ToString("0.00"),
                                stati.get_TaxClassOther().XXZSJE.ToString("0.00"),
                                item
                            };
                            item = new List<string> {
                                stati.get_Total().XXFFJE.ToString("0.00"),
                                stati.get_TaxClass17().XXFFJE.ToString("0.00"),
                                stati.get_TaxClass13().XXFFJE.ToString("0.00"),
                                stati.get_TaxClass6().XXFFJE.ToString("0.00"),
                                stati.get_TaxClass4().XXFFJE.ToString("0.00"),
                                stati.get_TaxClassOther().XXFFJE.ToString("0.00"),
                                item
                            };
                            item = new List<string> {
                                stati.get_Total().XXFSJE.ToString("0.00"),
                                stati.get_TaxClass17().XXFSJE.ToString("0.00"),
                                stati.get_TaxClass13().XXFSJE.ToString("0.00"),
                                stati.get_TaxClass6().XXFSJE.ToString("0.00"),
                                stati.get_TaxClass4().XXFSJE.ToString("0.00"),
                                stati.get_TaxClassOther().XXFSJE.ToString("0.00"),
                                item
                            };
                            item = new List<string> {
                                stati.get_Total().SJXSJE.ToString("0.00"),
                                stati.get_TaxClass17().SJXSJE.ToString("0.00"),
                                stati.get_TaxClass13().SJXSJE.ToString("0.00"),
                                stati.get_TaxClass6().SJXSJE.ToString("0.00"),
                                stati.get_TaxClass4().SJXSJE.ToString("0.00"),
                                stati.get_TaxClassOther().SJXSJE.ToString("0.00"),
                                item
                            };
                            item = new List<string> {
                                stati.get_Total().XXZFSE.ToString("0.00"),
                                stati.get_TaxClass17().XXZFSE.ToString("0.00"),
                                stati.get_TaxClass13().XXZFSE.ToString("0.00"),
                                stati.get_TaxClass6().XXZFSE.ToString("0.00"),
                                stati.get_TaxClass4().XXZFSE.ToString("0.00"),
                                stati.get_TaxClassOther().XXZFSE.ToString("0.00"),
                                item
                            };
                            item = new List<string> {
                                stati.get_Total().XXZSSE.ToString("0.00"),
                                stati.get_TaxClass17().XXZSSE.ToString("0.00"),
                                stati.get_TaxClass13().XXZSSE.ToString("0.00"),
                                stati.get_TaxClass6().XXZSSE.ToString("0.00"),
                                stati.get_TaxClass4().XXZSSE.ToString("0.00"),
                                stati.get_TaxClassOther().XXZSSE.ToString("0.00"),
                                item
                            };
                            item = new List<string> {
                                stati.get_Total().XXFFSE.ToString("0.00"),
                                stati.get_TaxClass17().XXFFSE.ToString("0.00"),
                                stati.get_TaxClass13().XXFFSE.ToString("0.00"),
                                stati.get_TaxClass6().XXFFSE.ToString("0.00"),
                                stati.get_TaxClass4().XXFFSE.ToString("0.00"),
                                stati.get_TaxClassOther().XXFFSE.ToString("0.00"),
                                item
                            };
                            item = new List<string> {
                                stati.get_Total().XXFSSE.ToString("0.00"),
                                stati.get_TaxClass17().XXFSSE.ToString("0.00"),
                                stati.get_TaxClass13().XXFSSE.ToString("0.00"),
                                stati.get_TaxClass6().XXFSSE.ToString("0.00"),
                                stati.get_TaxClass4().XXFSSE.ToString("0.00"),
                                stati.get_TaxClassOther().XXFSSE.ToString("0.00"),
                                item
                            };
                            item = new List<string> {
                                stati.get_Total().SJXXSE.ToString("0.00"),
                                stati.get_TaxClass17().SJXXSE.ToString("0.00"),
                                stati.get_TaxClass13().SJXXSE.ToString("0.00"),
                                stati.get_TaxClass6().SJXXSE.ToString("0.00"),
                                stati.get_TaxClass4().SJXXSE.ToString("0.00"),
                                stati.get_TaxClassOther().SJXXSE.ToString("0.00"),
                                item
                            };
                        }
                    }
                    if (dataGridView.Rows.Count > 0)
                    {
                        int count = dataGridView.Rows.Count;
                        while (count-- > 0)
                        {
                            dataGridView.Rows.RemoveAt(0);
                        }
                    }
                    if (list2.Count != list3.Count)
                    {
                        MessageManager.ShowMsgBox("INP-253107", new string[] { "资料统计出错" });
                    }
                    else
                    {
                        for (int j = 0; j < list3.Count; j++)
                        {
                            List<object> list4 = new List<object> {
                                list3[j]
                            };
                            object[] objArray = list2[j].ToArray();
                            for (int m = 1; m < table.Columns.Count; m++)
                            {
                                list4.Add(objArray[m - 1]);
                            }
                            object[] values = list4.ToArray();
                            table.Rows.Add(values);
                        }
                        dataGridView.DataSource = table;
                        for (int k = 0; k < dataGridView.Columns.Count; k++)
                        {
                            dataGridView.Columns[k].SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
        }

        public string[] CreateMonthlyHead(int nYear, int nMonth, int nPeriod, INV_TYPE InvType)
        {
            string[] strArray = new string[0];
            List<string> list = new List<string>();
            try
            {
                TaxStatisData data = this.taxCard.GetMonthStatistics(nYear, nMonth, nPeriod);
                if (data == null)
                {
                    return null;
                }
                for (int i = 0; i < data.get_Count(); i++)
                {
                    InvAmountTaxStati stati = data.get_Item(i);
                    if (stati == null)
                    {
                        return null;
                    }
                    if ((stati.get_InvTypeStr().Length > 0) && (stati.get_InvTypeStr().Trim() == InvTypeEntity.GetInvName(InvType)))
                    {
                        list.Add(stati.PeriodEarlyStockNum.ToString());
                        list.Add(stati.BuyNum.ToString());
                        list.Add(stati.ReturnInvNum.ToString());
                        list.Add(stati.PlusInvoiceNum.ToString());
                        list.Add(stati.PlusInvWasteNum.ToString());
                        list.Add(stati.PeriodEndStockNum.ToString());
                        list.Add(stati.NegativeInvoiceNum.ToString());
                        list.Add(stati.NegativeInvWasteNum.ToString());
                        if (this.bIsMainMachine && this.bHasChild)
                        {
                            list.Add(stati.AllotInvNum.ToString());
                            list.Add(stati.ReclaimStockNum.ToString());
                        }
                    }
                }
                strArray = list.ToArray();
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            return strArray;
        }

        public string[] CreateMonthlyHeadCXDY(int nYear, int nMonth, int nPeriod, INV_TYPE InvType)
        {
            string[] strArray = new string[0];
            List<string> list = new List<string>();
            try
            {
                if (this.taxMonthStatDataCXDY == null)
                {
                    return null;
                }
                for (int i = 0; i < this.taxMonthStatDataCXDY.get_Count(); i++)
                {
                    InvAmountTaxStati stati = this.taxMonthStatDataCXDY.get_Item(i);
                    if (stati == null)
                    {
                        return null;
                    }
                    if ((stati.get_InvTypeStr().Length > 0) && (stati.get_InvTypeStr().Trim() == InvTypeEntity.GetInvName(InvType)))
                    {
                        list.Add(stati.PeriodEarlyStockNum.ToString());
                        list.Add(stati.BuyNum.ToString());
                        list.Add(stati.ReturnInvNum.ToString());
                        list.Add(stati.PlusInvoiceNum.ToString());
                        list.Add(stati.PlusInvWasteNum.ToString());
                        list.Add(stati.PeriodEndStockNum.ToString());
                        list.Add(stati.NegativeInvoiceNum.ToString());
                        list.Add(stati.NegativeInvWasteNum.ToString());
                        if (this.bIsMainMachine && this.bHasChild)
                        {
                            list.Add(stati.AllotInvNum.ToString());
                            list.Add(stati.ReclaimStockNum.ToString());
                        }
                    }
                }
                strArray = list.ToArray();
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            return strArray;
        }

        public List<CInvStatData> CreateMonthlyStat(int nYear, int nMonth, int nPeriod)
        {
            List<CInvStatData> list = new List<CInvStatData>();
            try
            {
                TaxStatisData data = this.taxCard.GetMonthStatistics(nYear, nMonth, nPeriod);
                if (data == null)
                {
                    return null;
                }
                if (this.taxCard.get_RetCode() != 0)
                {
                    MessageBoxHelper.Show(MessageManager.GetMessageInfo(this.taxCard.get_ErrCode()));
                    return null;
                }
                List<string> list2 = new List<string> { "销项正废金额", "销项正数金额", "销项负废金额", "销项负数金额", "实际销售金额", "销项正废税额", "销项正数税额", "销项负废税额", "销项负数税额", "实际销项税额" };
                for (int i = 0; i < data.get_Count(); i++)
                {
                    InvAmountTaxStati stati = data.get_Item(i);
                    if (stati != null)
                    {
                        List<List<string>> list3 = new List<List<string>>();
                        if (stati.get_InvTypeStr().Length > 0)
                        {
                            List<string> list5;
                            List<string> list4 = new List<string> {
                                stati.PeriodEarlyStockNum.ToString(),
                                stati.BuyNum.ToString(),
                                stati.ReturnInvNum.ToString(),
                                stati.PlusInvoiceNum.ToString(),
                                stati.PlusInvWasteNum.ToString(),
                                stati.PeriodEndStockNum.ToString(),
                                stati.NegativeInvoiceNum.ToString(),
                                stati.NegativeInvWasteNum.ToString()
                            };
                            if (this.bIsMainMachine && this.bHasChild)
                            {
                                list4.Add(stati.AllotInvNum.ToString());
                                list4.Add(stati.ReclaimStockNum.ToString());
                            }
                            list5 = new List<string> {
                                stati.get_Total().XXZFJE.ToString("0.00"),
                                stati.get_TaxClass17().XXZFJE.ToString("0.00"),
                                stati.get_TaxClass13().XXZFJE.ToString("0.00"),
                                stati.get_TaxClass6().XXZFJE.ToString("0.00"),
                                stati.get_TaxClass4().XXZFJE.ToString("0.00"),
                                stati.get_TaxClassOther().XXZFJE.ToString("0.00"),
                                list5
                            };
                            list5 = new List<string> {
                                stati.get_Total().XXZSJE.ToString("0.00"),
                                stati.get_TaxClass17().XXZSJE.ToString("0.00"),
                                stati.get_TaxClass13().XXZSJE.ToString("0.00"),
                                stati.get_TaxClass6().XXZSJE.ToString("0.00"),
                                stati.get_TaxClass4().XXZSJE.ToString("0.00"),
                                stati.get_TaxClassOther().XXZSJE.ToString("0.00"),
                                list5
                            };
                            list5 = new List<string> {
                                stati.get_Total().XXFFJE.ToString("0.00"),
                                stati.get_TaxClass17().XXFFJE.ToString("0.00"),
                                stati.get_TaxClass13().XXFFJE.ToString("0.00"),
                                stati.get_TaxClass6().XXFFJE.ToString("0.00"),
                                stati.get_TaxClass4().XXFFJE.ToString("0.00"),
                                stati.get_TaxClassOther().XXFFJE.ToString("0.00"),
                                list5
                            };
                            list5 = new List<string> {
                                stati.get_Total().XXFSJE.ToString("0.00"),
                                stati.get_TaxClass17().XXFSJE.ToString("0.00"),
                                stati.get_TaxClass13().XXFSJE.ToString("0.00"),
                                stati.get_TaxClass6().XXFSJE.ToString("0.00"),
                                stati.get_TaxClass4().XXFSJE.ToString("0.00"),
                                stati.get_TaxClassOther().XXFSJE.ToString("0.00"),
                                list5
                            };
                            list5 = new List<string> {
                                stati.get_Total().SJXSJE.ToString("0.00"),
                                stati.get_TaxClass17().SJXSJE.ToString("0.00"),
                                stati.get_TaxClass13().SJXSJE.ToString("0.00"),
                                stati.get_TaxClass6().SJXSJE.ToString("0.00"),
                                stati.get_TaxClass4().SJXSJE.ToString("0.00"),
                                stati.get_TaxClassOther().SJXSJE.ToString("0.00"),
                                list5
                            };
                            list5 = new List<string> {
                                stati.get_Total().XXZFSE.ToString("0.00"),
                                stati.get_TaxClass17().XXZFSE.ToString("0.00"),
                                stati.get_TaxClass13().XXZFSE.ToString("0.00"),
                                stati.get_TaxClass6().XXZFSE.ToString("0.00"),
                                stati.get_TaxClass4().XXZFSE.ToString("0.00"),
                                stati.get_TaxClassOther().XXZFSE.ToString("0.00"),
                                list5
                            };
                            list5 = new List<string> {
                                stati.get_Total().XXZSSE.ToString("0.00"),
                                stati.get_TaxClass17().XXZSSE.ToString("0.00"),
                                stati.get_TaxClass13().XXZSSE.ToString("0.00"),
                                stati.get_TaxClass6().XXZSSE.ToString("0.00"),
                                stati.get_TaxClass4().XXZSSE.ToString("0.00"),
                                stati.get_TaxClassOther().XXZSSE.ToString("0.00"),
                                list5
                            };
                            list5 = new List<string> {
                                stati.get_Total().XXFFSE.ToString("0.00"),
                                stati.get_TaxClass17().XXFFSE.ToString("0.00"),
                                stati.get_TaxClass13().XXFFSE.ToString("0.00"),
                                stati.get_TaxClass6().XXFFSE.ToString("0.00"),
                                stati.get_TaxClass4().XXFFSE.ToString("0.00"),
                                stati.get_TaxClassOther().XXFFSE.ToString("0.00"),
                                list5
                            };
                            list5 = new List<string> {
                                stati.get_Total().XXFSSE.ToString("0.00"),
                                stati.get_TaxClass17().XXFSSE.ToString("0.00"),
                                stati.get_TaxClass13().XXFSSE.ToString("0.00"),
                                stati.get_TaxClass6().XXFSSE.ToString("0.00"),
                                stati.get_TaxClass4().XXFSSE.ToString("0.00"),
                                stati.get_TaxClassOther().XXFSSE.ToString("0.00"),
                                list5
                            };
                            list5 = new List<string> {
                                stati.get_Total().SJXXSE.ToString("0.00"),
                                stati.get_TaxClass17().SJXXSE.ToString("0.00"),
                                stati.get_TaxClass13().SJXXSE.ToString("0.00"),
                                stati.get_TaxClass6().SJXXSE.ToString("0.00"),
                                stati.get_TaxClass4().SJXXSE.ToString("0.00"),
                                stati.get_TaxClassOther().SJXXSE.ToString("0.00"),
                                list5
                            };
                            if (list3.Count == list2.Count)
                            {
                                DataTable table = new DataTable();
                                table.Columns.Add("项目名称");
                                table.Columns.Add("合计");
                                table.Columns.Add("17%");
                                table.Columns.Add("13%");
                                table.Columns.Add("6%");
                                table.Columns.Add("4%");
                                table.Columns.Add("其他");
                                for (int j = 0; j < list2.Count; j++)
                                {
                                    List<object> list6 = new List<object> {
                                        list2[j]
                                    };
                                    object[] objArray = list3[j].ToArray();
                                    for (int k = 1; k < table.Columns.Count; k++)
                                    {
                                        list6.Add(objArray[k - 1]);
                                    }
                                    object[] values = list6.ToArray();
                                    table.Rows.Add(values);
                                }
                                CInvStatData item = new CInvStatData {
                                    m_strInvTypeName = stati.get_InvTypeStr(),
                                    m_strHeadValue = list4.ToArray(),
                                    m_DataTableGrid = table
                                };
                                list.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            return list;
        }

        public List<CInvStatData> CreateYearlyStat(int nYear, int nStartMonth, int nEndMonth)
        {
            List<CInvStatData> list = new List<CInvStatData>();
            try
            {
                TaxStatisData data = this.taxCard.GetYearStatistics(nYear, nStartMonth, nEndMonth);
                if (data == null)
                {
                    return null;
                }
                if (this.taxCard.get_RetCode() != 0)
                {
                    MessageBoxHelper.Show(MessageManager.GetMessageInfo(this.taxCard.get_ErrCode()));
                    return null;
                }
                List<string> list2 = new List<string> { "销项正数金额", "销项负数金额", "实际销售金额", "销项正数税额", "销项负数税额", "实际销项税额" };
                for (int i = 0; i < data.get_Count(); i++)
                {
                    InvAmountTaxStati stati = data.get_Item(i);
                    if ((stati != null) && (stati.get_InvTypeStr().Length > 0))
                    {
                        List<string> list3 = new List<string> {
                            stati.PeriodEarlyStockNum.ToString(),
                            stati.BuyNum.ToString(),
                            stati.ReturnInvNum.ToString(),
                            stati.PlusInvoiceNum.ToString(),
                            stati.PlusInvWasteNum.ToString(),
                            stati.PeriodEndStockNum.ToString(),
                            stati.NegativeInvoiceNum.ToString(),
                            stati.NegativeInvWasteNum.ToString()
                        };
                        if (this.bIsMainMachine && this.bHasChild)
                        {
                            list3.Add(stati.AllotInvNum.ToString());
                            list3.Add(stati.ReclaimStockNum.ToString());
                        }
                        List<string> list4 = new List<string> {
                            stati.get_Total().XXZSJE.ToString("0.00"),
                            stati.get_Total().XXFSJE.ToString("0.00"),
                            stati.get_Total().SJXSJE.ToString("0.00"),
                            stati.get_Total().XXZSSE.ToString("0.00"),
                            stati.get_Total().XXFSSE.ToString("0.00"),
                            stati.get_Total().SJXXSE.ToString("0.00")
                        };
                        if (list2.Count == list4.Count)
                        {
                            DataTable table = new DataTable();
                            table.Columns.Add("项目名称");
                            table.Columns.Add("合计");
                            int count = list2.Count;
                            for (int j = 0; j < count; j++)
                            {
                                List<object> list5 = new List<object> {
                                    list2[j],
                                    list4[j]
                                };
                                for (int k = 2; k < table.Columns.Count; k++)
                                {
                                    list5.Add("");
                                }
                                object[] values = list5.ToArray();
                                table.Rows.Add(values);
                            }
                            CInvStatData item = new CInvStatData {
                                m_strInvTypeName = stati.get_InvTypeStr(),
                                m_strHeadValue = list3.ToArray(),
                                m_DataTableGrid = table
                            };
                            list.Add(item);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
            return list;
        }

        private void GetMachineState()
        {
            TaxStateInfo info = this.taxCard.get_StateInfo();
            if (info.IsMainMachine == 0)
            {
                this.bIsMainMachine = false;
            }
            else
            {
                this.bIsMainMachine = true;
            }
            if (info.IsWithChild == 0)
            {
                this.bHasChild = false;
            }
            else
            {
                this.bHasChild = true;
            }
        }

        public void InitTaxMonthStatDataCXDY(int nYear, int nMonth, int nPeriod)
        {
            try
            {
                this.taxMonthStatDataCXDY = this.taxCard.GetMonthStatistics(nYear, nMonth, nPeriod);
                if (this.taxMonthStatDataCXDY == null)
                {
                    this.loger.Info("InitTaxMonthStatDataCXDY函数内GetMonthStatistics返回值为空！");
                }
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
            }
        }

        public bool PrintTable(ref CustomStyleDataGrid dataGridView, string strTitle, List<PrinterItems> _PIHead, List<PrinterItems> _PIFoot, bool _bIsShow)
        {
            try
            {
                return DataGridPrintToolsN.Print(dataGridView, dataGridView.Parent, strTitle, _PIHead, _PIFoot, _bIsShow);
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                ExceptionHandler.HandleError(exception);
                return false;
            }
        }

        public bool PrintTableSerial(ref CustomStyleDataGrid dataGridView, string strTitle, List<PrinterItems> _PIHead, List<PrinterItems> _PIFoot, bool _bIsShow, bool _isSerialPrint, string showText)
        {
            try
            {
                return DataGridPrintToolsN.PrintSerial(dataGridView, dataGridView.Parent, strTitle, _PIHead, _PIFoot, _bIsShow, _isSerialPrint, showText);
            }
            catch (Exception exception)
            {
                this.loger.Info(exception.Message);
                if (exception.Message.Equals("用户放弃连续打印"))
                {
                    throw exception;
                }
                return false;
            }
        }

        public void SetColumnStyles(ref CustomStyleDataGrid dataGridView, string xmlFileName)
        {
        }
    }
}

