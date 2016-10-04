namespace Aisino.Fwkp.Fpkj.Common
{
    using Aisino.Framework.Plugin.Core.Service;
    using Aisino.Fwkp.Fpkj.DAL;
    using log4net;
    using System;
    using System.Runtime.CompilerServices;
    using Framework.Plugin.Core.Util;
    internal sealed class FPPrecisionShareMethod : AbstractService
    {
        private ILog loger = LogUtil.GetLogger<FPPrecisionShareMethod>();
        private const double Ten = 10.0;
        [DecimalConstant(0, 0, (uint) 0, (uint) 0, (uint) 0)]
        private static readonly decimal Zero;

        public string ComputeFloatNumber(string FloatNumber, int percision)
        {
            int length = FloatNumber.Length;
            int index = FloatNumber.IndexOf('.');
            bool flag = true;
            if ((length > 0) && (FloatNumber[0] == '-'))
            {
                FloatNumber = FloatNumber.Substring(1, length - 1);
                length = FloatNumber.Length;
                index = FloatNumber.IndexOf('.');
                flag = false;
            }
            if (index == -1)
            {
                percision = length;
            }
            else if (index > percision)
            {
                percision = index + 1;
            }
            if (length > percision)
            {
                FloatNumber = FloatNumber.Substring(0, percision + 1);
                length = FloatNumber.Length;
                decimal mSource = Tool.ObjectToDecimal(FloatNumber);
                int num4 = (length - index) - 2;
                if (num4 < 0)
                {
                    num4 = 0;
                }
                decimal num5 = this.RoundedDecimal(mSource, (double) num4);
                if (!flag)
                {
                    return ("-" + num5.ToString());
                }
                return num5.ToString();
            }
            if (index != -1)
            {
                int num6 = 0;
                num6 = index + 1;
                while (num6 < length)
                {
                    if (FloatNumber[num6] != '0')
                    {
                        break;
                    }
                    num6++;
                }
                if (num6 == length)
                {
                    FloatNumber = FloatNumber.Substring(0, index);
                }
            }
            if (!flag)
            {
                return ("-" + FloatNumber);
            }
            return FloatNumber;
        }

        protected override object[] doService(object[] param)
        {
            object[] objArray = new object[] { 0 };
            new XXFP(false);
            try
            {
                if ((param == null) || (param.Length < 2))
                {
                    this.loger.Error("发票精度参数设置错误");
                    return null;
                }
                string floatNumber = param[0].ToString();
                int percision = Tool.ObjectToInt(param[1]);
                objArray[0] = this.ComputeFloatNumber(floatNumber, percision);
                return objArray;
            }
            catch (Exception exception)
            {
                this.loger.Error(exception.Message);
                return objArray;
            }
        }

        private decimal RoundedDecimal(decimal mSource, double dDigits)
        {
            decimal num = (mSource < 0M) ? -0.5M : 0.5M;
            decimal num2 = Convert.ToDecimal(Math.Pow(10.0, dDigits));
            return (decimal.Truncate((mSource * num2) + num) / num2);
        }
    }
}

