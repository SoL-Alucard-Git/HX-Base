namespace BSDC
{
    using Aisino.Framework.Plugin.Core;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.FTaxBase;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CommFun
    {
        public CommFun()
        {
            
        }

        public int ConvertArrayListToDict(ArrayList arrayList_0, ref Dictionary<string, object> dictionary_0, string string_0)
        {
            dictionary_0.Clear();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            string str = "";
            int num = 0;
            while (num < arrayList_0.Count)
            {
                dictionary = arrayList_0[num] as Dictionary<string, object>;
                str = dictionary[string_0].ToString();
                dictionary_0.Add(num.ToString(), str);
                num++;
            }
            return num;
        }

        public List<InvTypeEntity> GetInvTypeCollect()
        {
            List<InvTypeEntity> list = new List<InvTypeEntity>();
            InvTypeEntity item = new InvTypeEntity();
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if (card.QYLX.ISZYFP)
            {
                item.m_invType = INV_TYPE.INV_SPECIAL;
                item.m_strInvName = "增值税专用发票";
                list.Add(item);
            }
            if (card.QYLX.ISPTFP)
            {
                item = new InvTypeEntity {
                    m_invType = INV_TYPE.INV_COMMON,
                    m_strInvName = "增值税普通发票"
                };
                list.Add(item);
            }
            if (card.QYLX.ISHY)
            {
                item = new InvTypeEntity {
                    m_invType = INV_TYPE.INV_TRANSPORTATION,
                    m_strInvName = "货物运输业增值税专用发票"
                };
                list.Add(item);
            }
            if (card.QYLX.ISJDC)
            {
                item = new InvTypeEntity {
                    m_invType = INV_TYPE.INV_VEHICLESALES,
                    m_strInvName = "机动车销售统一发票"
                };
                list.Add(item);
            }
            if (card.QYLX.ISPTFPDZ)
            {
                item = new InvTypeEntity {
                    m_invType = INV_TYPE.INV_PTFPDZ,
                    m_strInvName = "电子增值税普通发票"
                };
                list.Add(item);
            }
            if (card.QYLX.ISPTFPJSP)
            {
                item = new InvTypeEntity {
                    m_invType = INV_TYPE.INV_JSFP,
                    m_strInvName = "增值税普通发票(卷票)"
                };
                list.Add(item);
            }
            if (string.IsNullOrEmpty(card.SQInfo.DHYBZ))
            {
                item.m_invType = INV_TYPE.INV_SPECIAL;
                item.m_strInvName = "增值税专用发票";
                list.Add(item);
                item = new InvTypeEntity {
                    m_invType = INV_TYPE.INV_COMMON,
                    m_strInvName = "增值税普通发票"
                };
                list.Add(item);
            }
            return list;
        }

        public List<InvTypeEntity> GetInvTypeCollect(bool bool_0, int int_0, int int_1, int int_2)
        {
            TaxCard card = TaxCardFactory.CreateTaxCard();
            if (card == null)
            {
                return null;
            }
            List<InvTypeEntity> list = new List<InvTypeEntity>();
            TaxStatisData data = null;
            if (bool_0)
            {
                data = card.GetMonthStatistics(int_0, int_1, int_2);
            }
            else
            {
                data = card.GetYearStatistics(int_0, int_1, int_1);
            }
            if (data == null)
            {
                return null;
            }
            for (int i = 0; i < data.Count; i++)
            {
                InvAmountTaxStati stati = data[i];
                if (stati == null)
                {
                    return null;
                }
                if (stati.InvTypeStr.Length > 0)
                {
                    string str = stati.InvTypeStr.Trim();
                    if (str.Equals("专用发票"))
                    {
                        InvTypeEntity item = new InvTypeEntity {
                            m_invType = INV_TYPE.INV_SPECIAL,
                            m_strInvName = "增值税专用发票"
                        };
                        list.Add(item);
                    }
                    else if (str.Equals("普通发票"))
                    {
                        InvTypeEntity entity2 = new InvTypeEntity {
                            m_invType = INV_TYPE.INV_COMMON,
                            m_strInvName = "增值税普通发票"
                        };
                        list.Add(entity2);
                    }
                    else if (str.Equals("货物运输业增值税专用发票"))
                    {
                        InvTypeEntity entity3 = new InvTypeEntity {
                            m_invType = INV_TYPE.INV_TRANSPORTATION,
                            m_strInvName = "货物运输业增值税专用发票"
                        };
                        list.Add(entity3);
                    }
                    else if (str.Equals("机动车销售统一发票"))
                    {
                        InvTypeEntity entity4 = new InvTypeEntity {
                            m_invType = INV_TYPE.INV_VEHICLESALES,
                            m_strInvName = "机动车销售统一发票"
                        };
                        list.Add(entity4);
                    }
                    else
                    {
                        InvTypeEntity entity5 = new InvTypeEntity {
                            m_invType = INV_TYPE.INV_OTHER,
                            m_strInvName = str
                        };
                        list.Add(entity5);
                    }
                }
            }
            return list;
        }

        public List<ItemEntity> GetItemTypeCollect()
        {
            List<ItemEntity> list = new List<ItemEntity>();
            ItemEntity item = new ItemEntity {
                m_ItemType = ITEM_ACTION.ITEM_TOTAL,
                m_strItemName = "增值税发票汇总表",
                m_bStatus = false
            };
            list.Add(item);
            item = new ItemEntity {
                m_ItemType = ITEM_ACTION.ITEM_PLUS,
                m_strItemName = "正数发票清单",
                m_bStatus = false
            };
            list.Add(item);
            item = new ItemEntity {
                m_ItemType = ITEM_ACTION.ITEM_MINUS,
                m_strItemName = "负数发票清单",
                m_bStatus = false
            };
            list.Add(item);
            item = new ItemEntity {
                m_ItemType = ITEM_ACTION.ITEM_PLUS_WASTE,
                m_strItemName = "正数发票废票清单",
                m_bStatus = false
            };
            list.Add(item);
            item = new ItemEntity {
                m_ItemType = ITEM_ACTION.ITEM_MINUS_WASTE,
                m_strItemName = "负数发票废票清单",
                m_bStatus = false
            };
            list.Add(item);
            return list;
        }

        public object[] GetTaxCardInfo()
        {
            object[] objArray = null;
            try
            {
                string taxCode = TaxCardFactory.CreateTaxCard().TaxCode;
                object[] objArray3 = new object[] { taxCode };
                objArray = ServiceFactory.InvokePubService("Aisino.Fwkp.QueryCorporationInfo", objArray3);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleError(exception);
            }
            return objArray;
        }
    }
}

