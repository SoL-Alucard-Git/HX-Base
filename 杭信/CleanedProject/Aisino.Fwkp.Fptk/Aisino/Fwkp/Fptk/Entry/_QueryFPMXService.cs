namespace Aisino.Fwkp.Fptk.Entry
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.BusinessObject;
    using Aisino.Fwkp.Fptk.Form;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    internal sealed class _QueryFPMXService : AbstractService
    {
        protected override object[] doService(object[] param)
        {
            if (param.Length == 3)
            {
                bool flag = param[0].Equals("1");
                int result = 0;
                int.TryParse((string) param[1], out result);
                List<string[]> data = param[2] as List<string[]>;
                if (data != null)
                {
                    string[] strArray = data[result];
                    FPLX fplx = Invoice.ParseFPLX(strArray[0]);
                    if (((int)fplx == 0) || ((int)fplx == 2))
                    {
                        InvoiceShowForm form = new InvoiceShowForm(flag, result, data);
                        int index = form.index;
                        if (form.ShowDialog() != DialogResult.Ignore)
                        {
                            return new object[] { index, form.data };
                        }
                        object[] objArray = new object[] { param[0], index.ToString(), form.data };
                        return ServiceFactory.InvokePubService("Aisino.Fwkp.QueryFPMX", objArray);
                    }
                    if ((int)fplx == 0x33)
                    {
                        InvoiceShowForm_DZ m_dz = new InvoiceShowForm_DZ(flag, result, data);
                        int num3 = m_dz.index;
                        if (m_dz.ShowDialog() != DialogResult.Ignore)
                        {
                            return new object[] { num3, m_dz.data };
                        }
                        object[] objArray2 = new object[] { param[0], num3.ToString(), m_dz.data };
                        return ServiceFactory.InvokePubService("Aisino.Fwkp.QueryFPMX", objArray2);
                    }
                    if ((int)fplx == 0x29)
                    {
                        InvoiceShowForm_JS m_js = new InvoiceShowForm_JS(flag, result, data);
                        int num4 = m_js.index;
                        if (m_js.ShowDialog() != DialogResult.Ignore)
                        {
                            return new object[] { num4, m_js.data };
                        }
                        object[] objArray3 = new object[] { param[0], num4.ToString(), m_js.data };
                        return ServiceFactory.InvokePubService("Aisino.Fwkp.QueryFPMX", objArray3);
                    }
                    if ((int)fplx == 11)
                    {
                        HYInvoiceForm form2 = new HYInvoiceForm(flag, result, data);
                        int num5 = form2.index;
                        if (form2.ShowDialog() != DialogResult.Ignore)
                        {
                            return new object[] { num5, form2.data };
                        }
                        object[] objArray4 = new object[] { param[0], num5.ToString(), form2.data };
                        return ServiceFactory.InvokePubService("Aisino.Fwkp.QueryFPMX", objArray4);
                    }
                    if ((int)fplx == 12)
                    {
                        if (strArray[5].Substring(4, 1) == "1")
                        {
                            JDCInvoiceForm_old _old = new JDCInvoiceForm_old(flag, result, data);
                            int num6 = _old.index;
                            if (_old.ShowDialog() != DialogResult.Ignore)
                            {
                                return new object[] { num6, _old.data };
                            }
                            object[] objArray5 = new object[] { param[0], num6.ToString(), _old.data };
                            return ServiceFactory.InvokePubService("Aisino.Fwkp.QueryFPMX", objArray5);
                        }
                        JDCInvoiceForm_new _new = new JDCInvoiceForm_new(flag, result, data);
                        int num7 = _new.index;
                        if (_new.ShowDialog() != DialogResult.Ignore)
                        {
                            return new object[] { num7, _new.data };
                        }
                        object[] objArray6 = new object[] { param[0], num7.ToString(), _new.data };
                        return ServiceFactory.InvokePubService("Aisino.Fwkp.QueryFPMX", objArray6);
                    }
                }
            }
            return null;
        }
    }
}

