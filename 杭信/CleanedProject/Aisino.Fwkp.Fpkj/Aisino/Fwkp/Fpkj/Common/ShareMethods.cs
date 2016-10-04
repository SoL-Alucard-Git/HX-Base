namespace Aisino.Fwkp.Fpkj.Common
{
    using Aisino.Framework.Plugin.Core.Controls;
    using log4net;
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Framework.Plugin.Core.Util;
    internal class ShareMethods
    {
        private static ILog loger = LogUtil.GetLogger<ShareMethods>();

        public static DateTime CellToDateTime(DataGridViewCell cell)
        {
            DateTime time = new DateTime(0x7d0, 1, 1);
            try
            {
                if (cell.Value == null)
                {
                    return time;
                }
                return time;
            }
            catch (Exception exception)
            {
                loger.Error("CellToDateTime函数异常" + exception.Message);
                return time;
            }
        }

        public static decimal CellToDecimal(DataGridViewCell cell)
        {
            try
            {
                decimal result = 0M;
                if ((cell.Value != null) && decimal.TryParse(cell.Value.ToString().Trim(), out result))
                {
                    return ToDecimal_2(result);
                }
                return 0M;
            }
            catch (Exception exception)
            {
                loger.Error("CellToDecimal函数异常" + exception.Message);
                return 0M;
            }
        }

        public static double CellToDouble(DataGridViewCell cell)
        {
            try
            {
                double result = 0.0;
                if ((cell.Value != null) && double.TryParse(cell.Value.ToString().Trim(), out result))
                {
                    return ToDouble_2(result);
                }
                return 0.0;
            }
            catch (Exception exception)
            {
                loger.Error("CellToDouble异常" + exception.Message);
                return 0.0;
            }
        }

        public static int CellToInt(DataGridViewCell cell)
        {
            try
            {
                int result = 0;
                if ((cell.Value != null) && int.TryParse(cell.Value.ToString().Trim(), out result))
                {
                    return result;
                }
                return 0;
            }
            catch (Exception exception)
            {
                loger.Error("CellToInt异常" + exception.Message);
                return 0;
            }
        }

        public static string CellToString(DataGridViewCell cell)
        {
            try
            {
                if (cell.Value == null)
                {
                    return string.Empty;
                }
                if (string.IsNullOrEmpty(cell.Value.ToString().Trim()))
                {
                    return string.Empty;
                }
                return cell.Value.ToString().Trim();
            }
            catch (Exception exception)
            {
                loger.Error("CellToString异常" + exception.Message);
                return string.Empty;
            }
        }

        public static bool CheckMailAddr(string MailAddress)
        {
            bool flag = true;
            try
            {
                if (!string.IsNullOrEmpty(MailAddress.Trim()))
                {
                    string str = MailAddress.Trim();
                    int num = 0;
                    for (int i = 1; i < str.Length; i++)
                    {
                        if (str[i] == '@')
                        {
                            num++;
                        }
                    }
                    if ((num == 0) || (num > 1))
                    {
                        return false;
                    }
                    if ((str[1] == '@') || (str[str.Length - 1] == '@'))
                    {
                        return false;
                    }
                    int index = str.IndexOf('@');
                    str = str.Substring(index + 1, (str.Length - index) - 1);
                    if (str.Length >= 3)
                    {
                        int num4 = str.IndexOf('.');
                        if (((num4 != 0) && (num4 != 1)) && (num4 != (str.Length - 1)))
                        {
                            return flag;
                        }
                    }
                }
                return false;
            }
            catch (Exception exception)
            {
                loger.Error("CheckMailAddr函数异常" + exception.Message);
                return false;
            }
            return flag;
        }

        public static string FPHMTo8Wei(int iValue)
        {
            try
            {
                if ((iValue > 0x5f5e0ff) || (iValue < 0))
                {
                    iValue = 0;
                }
                return string.Format(DingYiZhiFuChuan1.strFphmFormat, iValue);
            }
            catch (Exception exception)
            {
                loger.Error("函数FPHMTo8Wei" + exception.Message);
                return new string('0', 8);
            }
        }

        public static string FPHMTo8Wei(string strValue)
        {
            try
            {
                if (strValue == null)
                {
                    return new string('0', 8);
                }
                strValue = strValue.Trim();
                int result = 0;
                int.TryParse(strValue, out result);
                return FPHMTo8Wei(result);
            }
            catch (Exception exception)
            {
                loger.Error("FPHMTo8Wei函数异常" + exception.Message);
                return new string('0', 8);
            }
        }

        public static string FPHMTo8Wei(uint iValue)
        {
            try
            {
                if ((iValue > 0x5f5e0ff) || (iValue < 0))
                {
                    iValue = 0;
                }
                return string.Format(DingYiZhiFuChuan1.strFphmFormat, iValue);
            }
            catch (Exception exception)
            {
                loger.Error("FPHMTo8Wei异常22" + exception.Message);
                return new string('0', 8);
            }
        }

        public static string GetFileName(string strFilePathName)
        {
            try
            {
                if (strFilePathName == null)
                {
                    return string.Empty;
                }
                int startIndex = strFilePathName.LastIndexOf(@"\") + 1;
                return strFilePathName.Substring(startIndex, strFilePathName.Length - startIndex);
            }
            catch (Exception exception)
            {
                loger.Error("GetFileName函数异常" + exception.Message);
                return string.Empty;
            }
        }

        public static void SetGroupBoxBackColor(Control.ControlCollection Controls, Color corlor)
        {
            try
            {
                if (Controls != null)
                {
                    foreach (Control control in Controls)
                    {
                        if ((control is Form) && (control.Controls != null))
                        {
                            foreach (Control control2 in control.Controls)
                            {
                                if (control2 is AisinoGRP)
                                {
                                    control2.BackColor = corlor;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                loger.Error("SetGroupBoxBackColor函数异常" + exception.Message);
            }
        }

        public static decimal ToDecimal_2(decimal dValue)
        {
            try
            {
                double num = decimal.ToDouble(dValue) + 0.005;
                num = Math.Round((double) (num * 100.0)) / 100.0;
                return Convert.ToDecimal(num);
            }
            catch (Exception exception)
            {
                loger.Error("ToDecimal_2函数异常" + exception.Message);
                return dValue;
            }
        }

        public static double ToDouble_2(double dValue)
        {
            try
            {
                dValue += 0.005;
                dValue = Math.Round((double) (dValue * 100.0)) / 100.0;
                return dValue;
            }
            catch (Exception exception)
            {
                loger.Error("ToDouble_2函数异常" + exception.Message);
                return dValue;
            }
        }
    }
}

