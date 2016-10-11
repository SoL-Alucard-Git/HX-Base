using Aisino.Framework.Plugin.Core.Controls;
using Aisino.Fwkp.Fpkj.Common;
using Aisino.Fwkp.Fpkj.Form.FPZF;
using InternetWare.Lodging.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InternetWare.Util.Client
{
    internal class ZuoFeiClient : BaseClient
    {
        private ZuoFeiArgs _args;
        public ZuoFeiClient(ZuoFeiArgs args)
        {
            _args = args;
        }

        internal override ResultBase DoService()
        {
            FaPiaoZuoFei_YiKai form = new FaPiaoZuoFei_YiKai();
            bool hasError = false;
            int num = 0;
            ErrorBase error = new ErrorBase();
            try
            {
                if (_args == null || _args.list == null || _args.list.Count == 0)
                {
                    hasError = true;
                    error.ErrorDescription = "已开发票作废，输入参数为null或为空数组";
                }
                else
                {
                    List<DaiKaiXml.SWDKDMHM> swdkZyList = null;
                    List<DaiKaiXml.SWDKDMHM> swdkPtList = null;
                    if (Aisino.Fwkp.Fpkj.Common.Tool.IsShuiWuDKSQ())
                    {
                        swdkZyList = new List<DaiKaiXml.SWDKDMHM>();
                        swdkPtList = new List<DaiKaiXml.SWDKDMHM>();
                    }
                    int count = _args.list.Count;
                    form.WasteFpCondition.Clear();
                    for (int i = 0; i < count; i++)
                    {
                        DataGridViewRow row = _args.list[i];
                        form.gridviewRowDict.Clear();
                        for (int j = 0; j < form.aisinoGrid.Columns.Count; j++)
                        {
                            string key = form.aisinoGrid.Columns[j].Name.ToString();
                            string fplb = row.Cells[key].Value.ToString();
                            string str8 = key;
                            if (str8 != null)
                            {
                                if (!(str8 == "FPZL"))
                                {
                                    if (str8 == "SLV")
                                    {
                                        goto Label_014E;
                                    }
                                    if (str8 == "PZZT")
                                    {
                                        goto Label_0159;
                                    }
                                    if (str8 == "BSZT")
                                    {
                                        goto Label_0164;
                                    }
                                }
                                else
                                {
                                    fplb = Aisino.Fwkp.Fpkj.Common.Tool.GetFPDBType(fplb);
                                }
                            }
                            goto Label_016D;
                        Label_014E:
                            fplb = Aisino.Fwkp.Fpkj.Common.Tool.GetDBSlv(fplb);
                            goto Label_016D;
                        Label_0159:
                            fplb = Aisino.Fwkp.Fpkj.Common.Tool.GetDBPZZT(fplb);
                            goto Label_016D;
                        Label_0164:
                            fplb = Aisino.Fwkp.Fpkj.Common.Tool.GetDBPZZT(fplb);
                        Label_016D:
                            form.gridviewRowDict.Add(key, fplb);
                        }
                        string fPDBType = Aisino.Fwkp.Fpkj.Common.Tool.GetFPDBType(row.Cells["FPZL"].Value.ToString());
                        string fpdm = row.Cells["FPDM"].Value.ToString();
                        string fphm = row.Cells["FPHM"].Value.ToString();
                        string str6 = form.YiKaiZuoFeiMainFunction(fPDBType, fpdm, fphm, swdkZyList, swdkPtList, 0);
                        if ("0000" == str6)
                        {
                            num++;
                        }
                        else 
                        {
                            error.ErrorDescription += str6 + ";";
                        }
                        if (str6.Contains("0001")) break;
                    }
                    form.SaveToDB();
                    string message = string.Concat(new object[] { "本期作废发票:", _args.list.Count, "张，\r\n成功作废发票:  ", num, "张，\r\n失败作废发票:  ", _args.list.Count - num, "张。" });
                    if (Aisino.Fwkp.Fpkj.Common.Tool.IsShuiWuDKSQ())
                    {
                        new DaiKaiXml().DaiKaiFpZuoFeiUpload(swdkZyList, swdkPtList);
                    }
                }
            }
            catch (Exception exception)
            {
                hasError = true;
                error = new ErrorBase() { ErrorDescription = $"错误类型：{exception.GetType()} || 错误信息:{exception.Message}" };
            }
            return new CountableResult(_args, null, hasError, error,_args.list.Count,num,_args.list.Count - num);
        }
    }
}
