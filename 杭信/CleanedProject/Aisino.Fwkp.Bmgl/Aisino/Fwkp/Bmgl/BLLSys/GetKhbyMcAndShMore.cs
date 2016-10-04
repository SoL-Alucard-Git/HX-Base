namespace Aisino.Fwkp.Bmgl.BLLSys
{
    using Aisino.Framework.Plugin.Core.Controls;
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Bmgl.BLL;
    using Aisino.Fwkp.Bmgl.Forms;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;

    internal sealed class GetKhbyMcAndShMore : AbstractService
    {
        private BMKHManager _khmanager = new BMKHManager();

        protected override object[] doService(object[] param)
        {
            object[] objArray;
            if (!CheckPermission.Check("KH"))
            {
                return null;
            }
            try
            {
                string str = (string) param[0];
                string str2 = (string) param[1];
                string str3 = string.Empty;
                if (param.Length == 3)
                {
                    str3 = (string) param[2];
                }
                string[] source = str3.Split(new char[] { ',' });
                List<object> list = new List<object>();
                AisinoDataSet aisinoDs = new AisinoDataSet();
                if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(str2))
                {
                    aisinoDs = this._khmanager.AppendKhByMcAndSh(str, str2);
                }
                else if (!string.IsNullOrEmpty(str))
                {
                    aisinoDs = this._khmanager.AppendKhByMc(str);
                }
                else if (!string.IsNullOrEmpty(str2))
                {
                    aisinoDs = this._khmanager.AppendKhBySh(str2);
                }
                else
                {
                    BMKHSelect select = new BMKHSelect(str);
                    if (select.ShowDialog() == DialogResult.OK)
                    {
                        DataTable dt = this._khmanager.GetKH(select.SelectedBM);
                        DataRow dr = dt.Rows[0];
                        foreach (string t in source)
                        {
                            if (!dt.Columns.Contains(t))
                            {
                                list.Add(" ");
                            }
                            else list.Add(dr[t]);
                        }
                        //list.AddRange(source.Select<string, object>(delegate (string t) {
                        //    if (!dt.Columns.Contains(t))
                        //    {
                        //        return " ";
                        //    }
                        //    return dr[t];
                        //}));
                        return list.ToArray();
                    }
                    return null;
                }
                if (aisinoDs.Data.Rows.Count != 0)
                {
                    if (aisinoDs.Data.Rows.Count == 1)
                    {
                        DataRow dr = aisinoDs.Data.Rows[0];
                        foreach (string t in source)
                        {
                            if (!aisinoDs.Data.Columns.Contains(t))
                            {
                                list.Add(" ");
                            }
                            else list.Add(dr[t]);
                        }
                        //list.AddRange(source.Select<string, object>(delegate (string t) {
                        //    if (!aisinoDs.Data.Columns.Contains(t))
                        //    {
                        //        return " ";
                        //    }
                        //    return dr[t];
                        //}));
                        goto Label_0251;
                    }
                    BMKHSelect select2 = new BMKHSelect(aisinoDs);
                    if (select2.ShowDialog() == DialogResult.OK)
                    {
                        DataTable dt = this._khmanager.GetKH(select2.SelectedBM);
                        DataRow dr = dt.Rows[0];
                        foreach (string t in source)
                        {
                            if (!aisinoDs.Data.Columns.Contains(t))
                            {
                                list.Add(" ");
                            }
                            else list.Add(dr[t]);
                        }
                        //list.AddRange(source.Select<string, object>(delegate (string t) {
                        //    if (!dt.Columns.Contains(t))
                        //    {
                        //        return " ";
                        //    }
                        //    return dr[t];
                        //}));
                        goto Label_0251;
                    }
                }
                return null;
            Label_0251:
                objArray = list.ToArray();
            }
            catch
            {
                objArray = null;
            }
            return objArray;
        }
    }
}

